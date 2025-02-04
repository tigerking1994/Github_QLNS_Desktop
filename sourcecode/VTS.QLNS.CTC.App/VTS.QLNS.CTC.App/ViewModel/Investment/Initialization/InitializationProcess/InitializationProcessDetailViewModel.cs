using AutoMapper;
using log4net;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using VTS.QLNS.CTC.App.Command;
using VTS.QLNS.CTC.App.Helper;
using VTS.QLNS.CTC.App.Model;
using VTS.QLNS.CTC.App.Model.Control;
using VTS.QLNS.CTC.App.Properties;
using VTS.QLNS.CTC.App.Service;
using VTS.QLNS.CTC.App.View.Investment.Initialization.InitializationProcess;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Service;
using VTS.QLNS.CTC.Core.Service.Impl;
using VTS.QLNS.CTC.Utility;
using VTS.QLNS.CTC.Utility.Enum;

namespace VTS.QLNS.CTC.App.ViewModel.Investment.Initialization.InitializationProcess
{
    public class InitializationProcessDetailViewModel : DetailViewModelBase<InitializationProcessModel, InitializationProcessDetailModel>
    {
        private IVdtKtKhoiTaoService _chungTuService;
        private IMapper _mapper;
        private ISessionService _sessionService;
        private IVdtKtKhoiTaoDuLieuChiTietService _chungTuChiTietService;
        private IVdtKtKhoiTaoDuLieuChiTietThanhToanService _detailContractService;
        private INsDonViService _nsDonViService;
        private IVdtDaDuAnService _duanService;
        private ICollectionView _dataDetailFilter;
        private ICollectionView _budgetCatalogFilter;
        private INsMucLucNganSachService _mucLucNganSachService;
        private IVdtDmLoaiCongTrinhService _loaicongtrinhService;
        private IVdtDuAnHangMucService _duanhangmucService;
        private readonly INsNguonNganSachService _nguonNganSachService;
        private readonly ITongHopNguonNSDauTuService _tonghopService;
        private readonly ILog _logger;
        private Dictionary<string, VdtDaDuAn> _dicDuAn;
        private Dictionary<string, List<ComboboxItem>> _dicLoaiCongTrinh;
        //private static Dictionary<string, string> _dicMucLucNganSach = new Dictionary<string, string>();
        private static string sL;
        private static string sK;

        private bool _bIsDetail;
        public bool BIsDetail
        {
            get => _bIsDetail;
            set => SetProperty(ref _bIsDetail, value);
        }

        public bool BDisableDetail => !BIsDetail;

        public List<NsMucLucNganSach> DataMucLucNganSach;
        public delegate void DataChangedEventHandler(object sender, EventArgs e);
        public event DataChangedEventHandler ClosePopup;
        public bool IsSaveData => Items.Any(item => item.IsModified || item.IsDeleted);
        public Action<int> SavedAction;

        private ObservableCollection<ComboboxItem> _dataDuAn;
        public ObservableCollection<ComboboxItem> DataDuAn
        {
            get => _dataDuAn;
            set => SetProperty(ref _dataDuAn, value);
        }

        private ObservableCollection<ComboboxItem> _itemsCoQuanTaiChinh;
        public ObservableCollection<ComboboxItem> ItemsCoQuanTaiChinh
        {
            get => _itemsCoQuanTaiChinh;
            set => SetProperty(ref _itemsCoQuanTaiChinh, value);
        }

        private ObservableCollection<ComboboxItem> _itemsNguonVon;
        public ObservableCollection<ComboboxItem> ItemsNguonVon
        {
            get => _itemsNguonVon;
            set => SetProperty(ref _itemsNguonVon, value);
        }

        public InitializationProcessContractDetailViewModel InitializationProcessContractDetailViewModel { get; set; }

        public RelayCommand AddParentCommand { get; }
        public RelayCommand SaveDataCommand { get; }
        public RelayCommand CloseWindowCommand { get; }
        public RelayCommand ShowPopupContractCommand { get; }

        public InitializationProcessDetailViewModel(
            InitializationProcessContractDetailViewModel initializationProcessContractDetailViewModel,
            IVdtKtKhoiTaoService cpChungTuService,
            ITongHopNguonNSDauTuService tonghopService,
            IVdtKtKhoiTaoDuLieuChiTietService chungTuChiTietService,
            IVdtKtKhoiTaoDuLieuChiTietThanhToanService detailContractService,
            IMapper mapper,
            ISessionService sessionService,
            INsDonViService nsDonViService,
            IVdtDaDuAnService duanService,
            INsNguonNganSachService nguonNganSachService,
            IVdtDmLoaiCongTrinhService loaicongtrinhService,
            IVdtDuAnHangMucService duanhangmucService,
            ILog logger,
            INsMucLucNganSachService mucLucNganSachService)
        {
            _mapper = mapper;
            _chungTuService = cpChungTuService;
            _chungTuChiTietService = chungTuChiTietService;
            _sessionService = sessionService;
            _nsDonViService = nsDonViService;
            _mucLucNganSachService = mucLucNganSachService;
            _tonghopService = tonghopService;
            _duanService = duanService;
            _nguonNganSachService = nguonNganSachService;
            _detailContractService = detailContractService;
            _loaicongtrinhService = loaicongtrinhService;
            _duanhangmucService = duanhangmucService;
            _logger = logger;

            InitializationProcessContractDetailViewModel = initializationProcessContractDetailViewModel;
            InitializationProcessContractDetailViewModel.ParentPage = this;

            AddParentCommand = new RelayCommand(obj => OnAddParent());
            SaveDataCommand = new RelayCommand(obj => OnSaveData());
            CloseWindowCommand = new RelayCommand(obj => OnCloseWindow());
            ShowPopupContractCommand = new RelayCommand(obj => OnShowContractDetail());
        }

        public override void Init()
        {
            try
            {
                MarginRequirement = new System.Windows.Thickness(10);
                GetNguonVonItems();
                LoadComboboxDuAn();
                LoadDropDownCoQuanThanhToan();
                LoadData();
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        private void LoadComboboxDuAn()
        {
            _dicDuAn = new Dictionary<string, VdtDaDuAn>();
            _dicLoaiCongTrinh = new Dictionary<string, List<ComboboxItem>>();
            Dictionary<Guid, VdtDmLoaiCongTrinh> dicLct = new Dictionary<Guid, VdtDmLoaiCongTrinh>();
            var dataLct = _loaicongtrinhService.FindAll();
            if (dataLct != null)
            {
                foreach (var item in dataLct)
                {
                    if (!dicLct.ContainsKey(item.IIdLoaiCongTrinh)) dicLct.Add(item.IIdLoaiCongTrinh, item);
                }
            }
            DataDuAn = new ObservableCollection<ComboboxItem>();
            if (Model != null && !string.IsNullOrEmpty(Model.IIdMaDonVi))
            {
                List<VdtDaDuAn> listDuAn = _duanService.FindByIdDonViQuanLy(Model.IIdMaDonVi).ToList();
                foreach (VdtDaDuAn itemDuAn in listDuAn)
                {
                    DataDuAn.Add(new ComboboxItem
                    {
                        ValueItem = itemDuAn.SMaDuAn,
                        HiddenValue = itemDuAn.Id.ToString(),
                        DisplayItem = itemDuAn.SMaDuAn + "-" + itemDuAn.STenDuAn
                    });
                    var lstLoaiCongTrinh = _duanhangmucService.FindByIdDuAn(itemDuAn.Id);
                    if (!_dicDuAn.ContainsKey(itemDuAn.SMaDuAn))
                    {
                        _dicDuAn.Add(itemDuAn.SMaDuAn, itemDuAn);
                        if (lstLoaiCongTrinh != null)
                        {
                            _dicLoaiCongTrinh.Add(itemDuAn.SMaDuAn, new List<ComboboxItem>());
                            foreach (var item in lstLoaiCongTrinh)
                            {
                                if (item.IdLoaiCongTrinh.HasValue && dicLct.ContainsKey(item.IdLoaiCongTrinh.Value))
                                    _dicLoaiCongTrinh[itemDuAn.SMaDuAn].Add(new ComboboxItem()
                                    {
                                        ValueItem = dicLct[item.IdLoaiCongTrinh.Value].IIdLoaiCongTrinh.ToString(),
                                        DisplayItem = dicLct[item.IdLoaiCongTrinh.Value].STenLoaiCongTrinh
                                    });
                            }
                        }
                    }
                }
            }
            OnPropertyChanged(nameof(DataDuAn));
        }

        protected override void OnDelete()
        {
            if (SelectedItem != null)
            {
                SelectedItem.IsDeleted = !SelectedItem.IsDeleted;
            }
            OnPropertyChanged(nameof(Items));
            OnPropertyChanged(nameof(IsSaveData));
        }

        private void OnAddParent()
        {
            InitializationProcessDetailModel itemAdd = new InitializationProcessDetailModel();
            itemAdd.PropertyChanged += DetailModel_PropertyChanged;
            itemAdd.IsModified = true;
            itemAdd.ItemsDuAn = ObjectCopier.Clone(DataDuAn);
            Items.Add(itemAdd);
            OnPropertyChanged(nameof(Items));
        }

        protected override void OnAdd()
        {
            if (SelectedItem == null)
            {
                MessageBoxHelper.Error(Resources.MsgRecordEmpty);
                return;
            }
            InitializationProcessDetailModel itemAdd = new InitializationProcessDetailModel();
            itemAdd.PropertyChanged += DetailModel_PropertyChanged;
            itemAdd.IsModified = true;
            itemAdd.SMaDuAn = SelectedItem.SMaDuAn;
            itemAdd.SMaDuAnParent = SelectedItem.SMaDuAn;
            int index = Items.IndexOf(SelectedItem);
            Items.Insert(index + 1, itemAdd);
            OnPropertyChanged(nameof(Items));
        }

        protected override void OnRefresh()
        {
            try
            {
                LoadData();
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        private void SetDuAnValue(ref InitializationProcessDetailModel item)
        {
            if (DataDuAn != null && DataDuAn.Count > 0)
            {
                string value = item.SMaDuAn;
                ComboboxItem itemSelected = DataDuAn.Where(n => n.ValueItem == value).FirstOrDefault();
                if (itemSelected != null && !string.IsNullOrEmpty(itemSelected.HiddenValue))
                {
                    item.IID_DuAnID = Guid.Parse(itemSelected.HiddenValue);
                }
            }
        }

        private void SetDuAnValue(ref List<InitializationProcessDetailModel> data)
        {
            foreach (InitializationProcessDetailModel item in data)
            {
                InitializationProcessDetailModel value = item;
                SetDuAnValue(ref value);
            }
        }

        public void OnSaveData()
        {
            DateTime dStartDate = DateTime.Now;
            StringBuilder messageBuilder = new StringBuilder();

            try
            {
                foreach (var item in Items.Where(n => n.IsModified && !n.IsDeleted))
                {
                    if (string.IsNullOrEmpty(item.SMaDuAn))
                    {
                        messageBuilder.AppendFormat(Resources.MsgInputDropdownRequire, "Dự án");
                        break;
                    }
                }

                if (Items.Where(n => !n.IsDeleted).GroupBy(n => new { n.SMaDuAn, n.IIDNguonVonID, n.ICoQuanThanhToan }).Any(n => n.Count() > 1))
                {
                    messageBuilder.Append(Resources.MsgErrorDetailInitialProcessDuplicate);
                }

                if (messageBuilder.Length != 0)
                {
                    System.Windows.MessageBox.Show(String.Join("\n", messageBuilder.ToString()));
                    return;
                }

                List<VdtKtKhoiTaoDuLieuChiTietThanhToan> lstDetailContract = new List<VdtKtKhoiTaoDuLieuChiTietThanhToan>();
                List<InitializationProcessDetailModel> dataDetailsAdd = Items.Where(x => x.IsModified && (x.IdDb == Guid.Empty || x.IdDb == null) && !x.IsDeleted).ToList();
                List<InitializationProcessDetailModel> dataDetailsUpdate = Items.Where(x => x.IsModified && x.IdDb != Guid.Empty && x.IdDb != null && !x.IsDeleted).ToList();
                List<InitializationProcessDetailModel> dataDetailsDelete = Items.Where(x => x.IsDeleted && x.IdDb != Guid.Empty && x.IdDb != null).ToList();

                // Thêm mới chứng từ chi tiết
                if (dataDetailsAdd.Count > 0)
                {
                    dataDetailsAdd = dataDetailsAdd.Select(x => { x.IID_KhoiTaoDuLieuID = Model.Id; return x; }).ToList();
                    List<VdtKtKhoiTaoDuLieuChiTiet> listChungTuChiTiets = new List<VdtKtKhoiTaoDuLieuChiTiet>();
                    SetDuAnValue(ref dataDetailsAdd);
                    listChungTuChiTiets = _mapper.Map<List<VdtKtKhoiTaoDuLieuChiTiet>>(dataDetailsAdd);

                    listChungTuChiTiets.Select(n => { n.Id = Guid.Empty; return n; }).ToList();
                    _chungTuChiTietService.AddRange(listChungTuChiTiets);
                    foreach (var child in listChungTuChiTiets.Where(n => n.lstContract != null && n.lstContract.Count() != 0))
                    {
                        lstDetailContract.AddRange(child.lstContract.Select(n => { n.IIdKhoiTaoDuLieuChiTietId = child.Id; return n; }));
                    }
                }

                // Cập nhật chứng từ chi tiết
                if (dataDetailsUpdate.Count > 0)
                {
                    foreach (var item in dataDetailsUpdate)
                    {
                        item.IsModified = false;
                        item.Id = item.IdDb.Value;
                        VdtKtKhoiTaoDuLieuChiTiet chungTuChiTiet = _chungTuChiTietService.Find(item.IdDb.Value);
                        if (chungTuChiTiet != null)
                        {
                            _mapper.Map(item, chungTuChiTiet);
                            _chungTuChiTietService.Update(chungTuChiTiet);
                        }
                        if (item.LstDetail != null && item.LstDetail.Count() != 0)
                        {
                            lstDetailContract.AddRange(_mapper.Map<List<VdtKtKhoiTaoDuLieuChiTietThanhToan>>(item.LstDetail).Select(n => { n.IIdKhoiTaoDuLieuChiTietId = item.Id; return n; }));
                        }
                    }
                }

                _detailContractService.DeleteByKhoiTaoDuLieuId(Model.Id);

                // Delete
                if (dataDetailsDelete.Count > 0)
                {
                    foreach (var item in dataDetailsDelete)
                    {
                        _chungTuChiTietService.Delete(item.IdDb.Value);
                    }
                }

                List<InitializationProcessDetailModel> lstDataTongHop = Items.Where(x => !x.IsDeleted).ToList();
                if (lstDetailContract != null && lstDetailContract.Count != 0)
                {
                    _detailContractService.AddRange(lstDetailContract);
                }
                SetupDataTongHop(_mapper.Map<List<VdtKtKhoiTaoDuLieuChiTiet>>(lstDataTongHop));
                System.Windows.Forms.MessageBox.Show(Resources.MsgSaveDone, Resources.NotifiTitle, MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadData();
                SavedAction?.Invoke(1);
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        public override void LoadData(params object[] args)
        {
            try
            {
                base.LoadData(args);
                if (Model == null || Model.Id == Guid.Empty)
                {
                    Items = new ObservableCollection<InitializationProcessDetailModel>();
                    OnPropertyChanged(nameof(IsSaveData));
                    OnPropertyChanged(nameof(Items));
                    return;
                }

                List<KhoiTaoDuLieuChiTietQuery> lstDetail = _chungTuChiTietService.FindDataKhoiTaoChiTiet(Model.Id.ToString()).ToList();
                Items = _mapper.Map<ObservableCollection<InitializationProcessDetailModel>>(lstDetail);

                IEnumerable<VdtKtKhoiTaoDuLieuChiTietThanhToanQuery> lstContract = _detailContractService.GetDetailByKTDLId(Model.Id);

                foreach (InitializationProcessDetailModel model in Items)
                {
                    model.ItemsDuAn = ObjectCopier.Clone(DataDuAn);
                    if (lstContract.Any(n => n.IIdKhoiTaoDuLieuChiTietId == model.IdDb))
                    {
                        model.LstDetail = _mapper.Map<List<VdtKtKhoiTaoDuLieuChiTietThanhToanModel>>(lstContract.Where(n => n.IIdKhoiTaoDuLieuChiTietId == model.IdDb));
                    }
                    model.SMaDuAnParent = model.SMaDuAn;
                    if (_dicLoaiCongTrinh.ContainsKey(model.SMaDuAn))
                    {
                        model.ItemsLoaiCongTrinh = _mapper.Map<ObservableCollection<ComboboxItem>>(_dicLoaiCongTrinh[model.SMaDuAn]);
                    }
                    model.PropertyChanged += DetailModel_PropertyChanged;
                }
                OnPropertyChanged(nameof(IsSaveData));
                OnPropertyChanged(nameof(Items));
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        private void DetailModel_PropertyChanged(object sender, PropertyChangedEventArgs args)
        {
            if (args.PropertyName == nameof(InitializationProcessDetailModel.IIDNguonVonID) ||
                args.PropertyName == nameof(InitializationProcessDetailModel.ICoQuanThanhToan))
                return;

            var selectedItem = (InitializationProcessDetailModel)sender;
            if (args.PropertyName == nameof(InitializationProcessDetailModel.SMaDuAn))
            {
                if (selectedItem.SMaDuAn != selectedItem.SMaDuAnParent && !string.IsNullOrEmpty(selectedItem.SMaDuAnParent))
                {
                    foreach (var item in Items.Where(n => n.SMaDuAn == selectedItem.SMaDuAnParent))
                    {
                        item.IsModified = true;
                        item.SMaDuAnParent = selectedItem.SMaDuAn;
                        item.SMaDuAn = selectedItem.SMaDuAn;
                    }
                    selectedItem.SMaDuAnParent = selectedItem.SMaDuAn;
                }
                selectedItem.ItemsLoaiCongTrinh = new ObservableCollection<ComboboxItem>();
                if (!string.IsNullOrEmpty(selectedItem.SMaDuAn) && _dicLoaiCongTrinh.ContainsKey(selectedItem.SMaDuAn))
                {
                    selectedItem.ItemsLoaiCongTrinh = _mapper.Map<ObservableCollection<ComboboxItem>>(_dicLoaiCongTrinh[selectedItem.SMaDuAn]);
                }
                OnPropertyChanged(nameof(Items));
            }
            selectedItem.IsModified = true;
            OnPropertyChanged(nameof(IsSaveData));
        }

        private void OnCloseWindow()
        {
            DataChangedEventHandler handler = ClosePopup;
            if (handler != null)
            {
                handler(this, new EventArgs());
            }
        }

        private void LoadDropDownCoQuanThanhToan()
        {
            List<ComboboxItem> lstItem = new List<ComboboxItem>();
            lstItem.Add(new ComboboxItem() { DisplayItem = CoQuanThanhToanEnum.TypeName.KHO_BAC, ValueItem = ((int)CoQuanThanhToanEnum.Type.KHO_BAC).ToString() });
            lstItem.Add(new ComboboxItem() { DisplayItem = CoQuanThanhToanEnum.TypeName.CQTC, ValueItem = ((int)CoQuanThanhToanEnum.Type.CQTC).ToString() });
            ItemsCoQuanTaiChinh = new ObservableCollection<ComboboxItem>(lstItem);
            OnPropertyChanged(nameof(lstItem));
        }

        private void SetupDataTongHop(List<VdtKtKhoiTaoDuLieuChiTiet> lstItem)
        {
            _tonghopService.DeleteTongHopNguonDauTu(LOAI_CHUNG_TU.QUYET_TOAN, Model.Id);
            List<TongHopNguonNSDauTuQuery> lstData = new List<TongHopNguonNSDauTuQuery>();
            foreach (var item in lstItem)
            {
                if ((item.FKhvnVonBoTriHetNamTruoc ?? 0) != 0)
                {
                    lstData.Add(new TongHopNguonNSDauTuQuery()
                    {
                        iID_ChungTu = Model.Id,
                        iID_DuAnID = item.IIdDuAnId.Value,
                        sMaNguon = (item.ICoQuanThanhToan == (int)CoQuanThanhToanEnum.Type.KHO_BAC) ? LOAI_CHUNG_TU.QT_LUYKE_KHVN_KHOBAC : LOAI_CHUNG_TU.QT_LUYKE_KHVN_LENHCHI,
                        sMaDich = LOAI_CHUNG_TU.CHU_DAU_TU,
                        fGiaTri = item.FKhvnVonBoTriHetNamTruoc,
                        IIdLoaiCongTrinh = item.IIdLoaiCongTrinh
                    });
                }

                if ((item.FKhvnLkvonDaThanhToanTuKhoiCongDenHetNamTruoc ?? 0) != 0)
                {
                    lstData.Add(new TongHopNguonNSDauTuQuery()
                    {
                        iID_ChungTu = Model.Id,
                        iID_DuAnID = item.IIdDuAnId.Value,
                        sMaNguon = LOAI_CHUNG_TU.CHU_DAU_TU,
                        sMaDich = (item.ICoQuanThanhToan == (int)CoQuanThanhToanEnum.Type.KHO_BAC) ? LOAI_CHUNG_TU.QT_LUYKE_TT_KHVN_KHOBAC : LOAI_CHUNG_TU.QT_LUYKE_TT_KHVN_LENHCHI,
                        sMaNguonCha = (item.ICoQuanThanhToan == (int)CoQuanThanhToanEnum.Type.KHO_BAC) ? LOAI_CHUNG_TU.QT_LUYKE_KHVN_KHOBAC : LOAI_CHUNG_TU.QT_LUYKE_KHVN_LENHCHI,
                        fGiaTri = item.FKhvnLkvonDaThanhToanTuKhoiCongDenHetNamTruoc,
                        IIdLoaiCongTrinh = item.IIdLoaiCongTrinh
                    });
                }

                if ((item.FKhvnTrongDoVonTamUngTheoCheDoChuaThuHoi ?? 0) != 0)
                {
                    lstData.Add(new TongHopNguonNSDauTuQuery()
                    {
                        iID_ChungTu = Model.Id,
                        iID_DuAnID = item.IIdDuAnId.Value,
                        sMaNguon = LOAI_CHUNG_TU.CHU_DAU_TU,
                        sMaDich = (item.ICoQuanThanhToan == (int)CoQuanThanhToanEnum.Type.KHO_BAC) ? LOAI_CHUNG_TU.QT_LUYKE_TU_CHUATH_KHOBAC : LOAI_CHUNG_TU.QT_LUYKE_TU_CHUATH_LENHCHI,
                        sMaNguonCha = (item.ICoQuanThanhToan == (int)CoQuanThanhToanEnum.Type.KHO_BAC) ? LOAI_CHUNG_TU.QT_LUYKE_KHVN_KHOBAC : LOAI_CHUNG_TU.QT_LUYKE_KHVN_LENHCHI,
                        fGiaTri = item.FKhvnTrongDoVonTamUngTheoCheDoChuaThuHoi,
                        IIdLoaiCongTrinh = item.IIdLoaiCongTrinh
                    });
                }

                if ((item.FKhvnKeHoachVonKeoDaiSangNam ?? 0) != 0)
                {
                    lstData.Add(new TongHopNguonNSDauTuQuery()
                    {
                        iID_ChungTu = Model.Id,
                        iID_DuAnID = item.IIdDuAnId.Value,
                        sMaNguon = (item.ICoQuanThanhToan == (int)CoQuanThanhToanEnum.Type.KHO_BAC) ? LOAI_CHUNG_TU.QT_KHOBAC_CHUYENNAMTRUOC : LOAI_CHUNG_TU.QT_LENHCHI_CHUYENNAMTRUOC,
                        sMaDich = LOAI_CHUNG_TU.CHU_DAU_TU,
                        fGiaTri = item.FKhvnKeHoachVonKeoDaiSangNam,
                        IIdLoaiCongTrinh = item.IIdLoaiCongTrinh
                    });
                }

                if ((item.FKhutVonBoTriHetNamTruoc ?? 0) != 0)
                {
                    lstData.Add(new TongHopNguonNSDauTuQuery()
                    {
                        iID_ChungTu = Model.Id,
                        iID_DuAnID = item.IIdDuAnId.Value,
                        sMaNguon = (item.ICoQuanThanhToan == (int)CoQuanThanhToanEnum.Type.KHO_BAC) ? LOAI_CHUNG_TU.QT_LUYKE_KHVU_KHOBAC : LOAI_CHUNG_TU.QT_LUYKE_KHVU_LENHCHI,
                        sMaDich = LOAI_CHUNG_TU.CHU_DAU_TU,
                        fGiaTri = item.FKhutVonBoTriHetNamTruoc,
                        IIdLoaiCongTrinh = item.IIdLoaiCongTrinh
                    });
                }

                if ((item.FKhutLkvonDaThanhToanTuKhoiCongDenHetNamTruoc ?? 0) != 0)
                {
                    lstData.Add(new TongHopNguonNSDauTuQuery()
                    {
                        iID_ChungTu = Model.Id,
                        iID_DuAnID = item.IIdDuAnId.Value,
                        sMaNguon = LOAI_CHUNG_TU.CHU_DAU_TU,
                        sMaDich = (item.ICoQuanThanhToan == (int)CoQuanThanhToanEnum.Type.KHO_BAC) ? LOAI_CHUNG_TU.QT_LUYKE_TT_KHVU_KHOBAC : LOAI_CHUNG_TU.QT_LUYKE_TT_KHVU_LENHCHI,
                        sMaNguonCha = (item.ICoQuanThanhToan == (int)CoQuanThanhToanEnum.Type.KHO_BAC) ? LOAI_CHUNG_TU.QT_LUYKE_KHVU_KHOBAC : LOAI_CHUNG_TU.QT_LUYKE_KHVU_LENHCHI,
                        fGiaTri = item.FKhutLkvonDaThanhToanTuKhoiCongDenHetNamTruoc,
                        IIdLoaiCongTrinh = item.IIdLoaiCongTrinh
                    });
                }

                if ((item.FKhutTrongDoVonTamUngTheoCheDoChuaThuHoi ?? 0) != 0)
                {
                    lstData.Add(new TongHopNguonNSDauTuQuery()
                    {
                        iID_ChungTu = Model.Id,
                        iID_DuAnID = item.IIdDuAnId.Value,
                        sMaNguon = LOAI_CHUNG_TU.CHU_DAU_TU,
                        sMaDich = (item.ICoQuanThanhToan == (int)CoQuanThanhToanEnum.Type.KHO_BAC) ? LOAI_CHUNG_TU.QT_LUYKE_TU_CHUATH_KHOBAC : LOAI_CHUNG_TU.QT_LUYKE_TU_CHUATH_LENHCHI,
                        sMaNguonCha = (item.ICoQuanThanhToan == (int)CoQuanThanhToanEnum.Type.KHO_BAC) ? LOAI_CHUNG_TU.QT_LUYKE_KHVU_KHOBAC : LOAI_CHUNG_TU.QT_LUYKE_KHVU_LENHCHI,
                        fGiaTri = item.FKhutTrongDoVonTamUngTheoCheDoChuaThuHoi,
                        IIdLoaiCongTrinh = item.IIdLoaiCongTrinh
                    });
                }

                if ((item.FKhutKeHoachUngTruocKeoDaiSangNam ?? 0) != 0)
                {
                    lstData.Add(new TongHopNguonNSDauTuQuery()
                    {
                        iID_ChungTu = Model.Id,
                        iID_DuAnID = item.IIdDuAnId.Value,
                        sMaNguon = (item.ICoQuanThanhToan == (int)CoQuanThanhToanEnum.Type.KHO_BAC) ? LOAI_CHUNG_TU.QT_UNG_KHOBAC_CHUYENNAMTRUOC : LOAI_CHUNG_TU.QT_UNG_LENHCHI_CHUYENNAMTRUOC,
                        sMaDich = LOAI_CHUNG_TU.CHU_DAU_TU,
                        fGiaTri = item.FKhutKeHoachUngTruocKeoDaiSangNam,
                        IIdLoaiCongTrinh = item.IIdLoaiCongTrinh
                    });
                }

                if ((item.FKhutKeHoachUngTruocChuaThuHoi ?? 0) != 0)
                {
                    lstData.Add(new TongHopNguonNSDauTuQuery()
                    {
                        iID_ChungTu = Model.Id,
                        iID_DuAnID = item.IIdDuAnId.Value,
                        sMaNguon = (item.ICoQuanThanhToan == (int)CoQuanThanhToanEnum.Type.KHO_BAC) ? LOAI_CHUNG_TU.QT_LUYKE_UNGTRUOC_CHUATHUHOI_KHOBAC : LOAI_CHUNG_TU.QT_LUYKE_UNGTRUOC_CHUATHUHOI_LENHCHI,
                        sMaDich = LOAI_CHUNG_TU.CHU_DAU_TU,
                        fGiaTri = item.FKhutKeHoachUngTruocChuaThuHoi,
                        IIdLoaiCongTrinh = item.IIdLoaiCongTrinh
                    });
                }
            }
            _tonghopService.InsertTongHopNguonDauTuQuyetToan(Model.Id, lstData);
        }

        private void GetNguonVonItems()
        {
            var data = _nguonNganSachService.FindAll().OrderBy(n => n.IStt)
                .Select(n => new ComboboxItem() { ValueItem = n.IIdMaNguonNganSach.ToString(), DisplayItem = n.STen });
            ItemsNguonVon = new ObservableCollection<ComboboxItem>(data);
            OnPropertyChanged(nameof(ItemsNguonVon));
        }

        private void OnShowContractDetail()
        {
            if (SelectedItem == null || DataDuAn == null || !DataDuAn.Any() || SelectedItem.ICoQuanThanhToan == null || SelectedItem.IIDNguonVonID == null) return;
            InitializationProcessContractDetailViewModel.BIsDetail = BIsDetail;
            InitializationProcessContractDetailViewModel.Model = SelectedItem;
            var objDuAn = DataDuAn.Where(n => n.ValueItem == SelectedItem.SMaDuAn).FirstOrDefault();
            if (objDuAn != null)
                InitializationProcessContractDetailViewModel.Model.IID_DuAnID = Guid.Parse(objDuAn.HiddenValue);
            if (SelectedItem.LstDetail == null)
                InitializationProcessContractDetailViewModel.ItemsDefault = new List<VdtKtKhoiTaoDuLieuChiTietThanhToanModel>();
            else
                InitializationProcessContractDetailViewModel.ItemsDefault = SelectedItem.LstDetail;
            InitializationProcessContractDetailViewModel.Description = string.Format("{0} - {1} - {2}",
                objDuAn.DisplayItem,
                ItemsNguonVon.FirstOrDefault(n => n.ValueItem == SelectedItem.IIDNguonVonID.Value.ToString()).DisplayItem,
                ItemsCoQuanTaiChinh.FirstOrDefault(n => n.ValueItem == SelectedItem.ICoQuanThanhToan.Value.ToString()).DisplayItem);
            InitializationProcessContractDetailViewModel.Init();
            InitializationProcessContractDetailViewModel.SavedAction = obj =>
            {
                SelectedItem.LstDetail = InitializationProcessContractDetailViewModel.Items.ToList();
                if (SelectedItem.LstDetail != null)
                {
                    SelectedItem.FKHVN_LKVonDaThanhToanTuKhoiCongDenHetNamTruoc =
                        SelectedItem.LstDetail.Sum(n => n.FLuyKeTtklhtNnKhvn ?? 0) + SelectedItem.LstDetail.Sum(n => n.FLuyKeTtklhtTnKhvn ?? 0);

                    SelectedItem.FKHVN_TrongDoVonTamUngTheoCheDoChuaThuHoi =
                        SelectedItem.LstDetail.Sum(n => n.FLuyKeTUChuaThuHoiNnKhvn ?? 0) + SelectedItem.LstDetail.Sum(n => n.FLuyKeTUChuaThuHoiTnKhvn ?? 0);

                    SelectedItem.FKHUT_LKVonDaThanhToanTuKhoiCongDenHetNamTruoc =
                        SelectedItem.LstDetail.Sum(n => n.FLuyKeTtklhtNnKhvu ?? 0) + SelectedItem.LstDetail.Sum(n => n.FLuyKeTtklhtTnKhvu ?? 0);

                    SelectedItem.FKHUT_TrongDoVonTamUngTheoCheDoChuaThuHoi =
                        SelectedItem.LstDetail.Sum(n => n.FLuyKeTUChuaThuHoiNnKhvu ?? 0) + SelectedItem.LstDetail.Sum(n => n.FLuyKeTUChuaThuHoiTnKhvu ?? 0);
                }
                OnPropertyChanged(nameof(Items));
            };
            var view = new InitializationProcessContractDetail
            {
                DataContext = InitializationProcessContractDetailViewModel
            };
            view.ShowDialog();
        }
    }
}
