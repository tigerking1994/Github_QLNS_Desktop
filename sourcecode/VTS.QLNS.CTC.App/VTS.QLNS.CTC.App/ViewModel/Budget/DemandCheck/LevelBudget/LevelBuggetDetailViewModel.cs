using AutoMapper;
using MaterialDesignThemes.Wpf;
using log4net;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Data;
using VTS.QLNS.CTC.App.Command;
using VTS.QLNS.CTC.App.Helper;
using VTS.QLNS.CTC.App.Model;
using VTS.QLNS.CTC.App.Model.Control;
using VTS.QLNS.CTC.App.Properties;
using VTS.QLNS.CTC.App.Service;
using VTS.QLNS.CTC.App.View.Budget.DemandCheck.LevelBudget;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Criteria;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Service;
using VTS.QLNS.CTC.Utility;

namespace VTS.QLNS.CTC.App.ViewModel.Budget.DemandCheck.LevelBudget
{
    public class LevelBuggetDetailViewModel : DetailViewModelBase<LevelBuggetModel, LevelBuggetDetailModel>
    {
        private ILbChungTuService _chungTuService;
        private IMapper _mapper;
        private ISessionService _sessionService;
        private ILbChungTuChiTietService _chungTuChiTietService;
        private INsDonViService _nsDonViService;
        private AllocationDetailCriteria _searchCondition;
        private ICollectionView _dataDetailFilter;
        private ICollectionView _budgetCatalogFilter;
        private INsMucLucNganSachService _mucLucNganSachService;
        private readonly ILog _logger;
        private IDanhMucService _danhMucService;
        private ILbChungTuChiTietPhanCapService _lbChungTuChiTietPhanCapService;

        public delegate void DataChangedEventHandler(object sender, EventArgs e);
        public event DataChangedEventHandler ClosePopup;
        public LevelBuggetDetailChildViewModel LevelBuggetDetailChildViewModel { get; }

        public override Type ContentType => typeof(View.Budget.DemandCheck.LevelBudget.LevelBuggetDetail);
        public override PackIconKind IconKind => PackIconKind.FileDocumentBoxMultiple;
        public bool IsSaveData => Items.Any(item => item.IsModified || item.IsDeleted);
        public bool IsDeleteAll => Items.Any(item => !item.IsModified && item.HasData);
        public bool IsEnableButtonDelete => SelectedItem != null && !SelectedItem.IsHangCha;
        public bool IsNganSachQP => (Model != null && Model.LoaiChungTu.HasValue && Model.LoaiChungTu.ToString() == NguonNganSach.NSQP.ToString());

        public int NamLamViec { get; set; }

        private bool _isOpenRefresh;
        public bool IsOpenRefresh
        {
            get => _isOpenRefresh;
            set => SetProperty(ref _isOpenRefresh, value);
        }

        private ObservableCollection<ComboboxItem> _typeDisplays;
        public ObservableCollection<ComboboxItem> TypeDisplays
        {
            get => _typeDisplays;
            set => SetProperty(ref _typeDisplays, value);
        }

        private string _selectedTypeDisplays;
        public string SelectedTypeDisplays
        {
            get => _selectedTypeDisplays;
            set
            {
                if (SetProperty(ref _selectedTypeDisplays, value) && _dataDetailFilter != null)
                {
                    OnRefeshFilter();
                }
            }
        }

        private AllocationDetailFilterModel _detailFilter;
        public AllocationDetailFilterModel DetailFilter
        {
            get => _detailFilter;
            set => SetProperty(ref _detailFilter, value);
        }

        private string _selectedLNS;
        public string SelectedLNS
        {
            get => _selectedLNS;
            set => SetProperty(ref _selectedLNS, value);
        }

        private bool _isOpenLnsPopup;
        public bool IsOpenLnsPopup
        {
            get => _isOpenLnsPopup;
            set
            {
                SetProperty(ref _isOpenLnsPopup, value);
            }
        }

        private string _searchLNS;
        public string SearchLNS
        {
            get => _searchLNS;
            set
            {
                if (SetProperty(ref _searchLNS, value))
                {
                    _budgetCatalogFilter.Refresh();
                }
            }
        }

        public Visibility ShowTotal => Items.Count > 0 ? Visibility.Visible : Visibility.Hidden;

        private ObservableCollection<NsMuclucNgansachModel> _budgetCatalogItems;
        public ObservableCollection<NsMuclucNgansachModel> BudgetCatalogItems
        {
            get => _budgetCatalogItems;
            set => SetProperty(ref _budgetCatalogItems, value);
        }

        private NsMuclucNgansachModel _selectedBudgetCatalog;
        public NsMuclucNgansachModel SelectedBudgetCatalog
        {
            get => _selectedBudgetCatalog;
            set
            {
                if (SetProperty(ref _selectedBudgetCatalog, value))
                {
                    OnRefeshFilter();
                }
                if (_selectedBudgetCatalog != null)
                    SelectedLNS = _selectedBudgetCatalog.Lns;
                IsOpenLnsPopup = false;
            }
        }

        public RelayCommand SaveCommand { get; }
        public RelayCommand SearchCommand { get; }
        public RelayCommand RefreshAllDataCommand { get; }
        public RelayCommand CloseWindowCommand { get; }
        public RelayCommand RemoveFilterCommand { get; }
        public RelayCommand ShowPopupChildCommand { get; }

        public LevelBuggetDetailViewModel(ILbChungTuService chungTuService,
            ILbChungTuChiTietService chungTuChiTietService,
            IDanhMucService danhMucService,
            IMapper mapper,
            ISessionService sessionService,
            INsDonViService nsDonViService,
            ILog logger,
            INsMucLucNganSachService mucLucNganSachService,
            ILbChungTuChiTietPhanCapService lbChungTuChiTietPhanCapService,
            LevelBuggetDetailChildViewModel levelBuggetDetailChildViewModel) : base(danhMucService, sessionService)
        {
            _mapper = mapper;
            _chungTuService = chungTuService;
            _danhMucService = danhMucService;
            _chungTuChiTietService = chungTuChiTietService;
            _sessionService = sessionService;
            _nsDonViService = nsDonViService;
            _mucLucNganSachService = mucLucNganSachService;
            _lbChungTuChiTietPhanCapService = lbChungTuChiTietPhanCapService;
            _logger = logger;

            LevelBuggetDetailChildViewModel = levelBuggetDetailChildViewModel;

            SaveCommand = new RelayCommand(obj => OnSaveData());
            SearchCommand = new RelayCommand(obj =>
            {
                OnRefeshFilter();
            });
            ShowPopupChildCommand = new RelayCommand(o => OnShowPopupChild());
            RefreshAllDataCommand = new RelayCommand(obj => OnRefreshAllData());
            CloseWindowCommand = new RelayCommand(obj => OnCloseWindow());
            RemoveFilterCommand = new RelayCommand(obj => OnRemoveFilter());
        }

        private void OnRemoveFilter()
        {
            DetailFilter.L = string.Empty;
            DetailFilter.K = string.Empty;
            DetailFilter.M = string.Empty;
            DetailFilter.TM = string.Empty;
            DetailFilter.TTM = string.Empty;
            DetailFilter.NG = string.Empty;
            SelectedLNS = string.Empty;
            OnRefeshFilter();
        }

        public override void Init()
        {
            base.Init();
            try
            {
                NamLamViec = _sessionService.Current.YearOfWork;
                MarginRequirement = new System.Windows.Thickness(10);
                ResetSearchCondition();
                LoadTypeDisplay();
                LoadBudgetIndexCondition();
                LoadData();
                OnPropertyChanged(nameof(IsNganSachQP));
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        private void ResetSearchCondition()
        {
            DetailFilter = new AllocationDetailFilterModel();
            if (Model != null)
            {
                _searchCondition = new AllocationDetailCriteria
                {
                    VoucherId = Model.Id.ToString(),
                    LNS = Model.Lns,
                    YearOfWork = _sessionService.Current.YearOfWork,
                    YearOfBudget = _sessionService.Current.YearOfBudget,
                    Type = Model.ILoai,
                    BudgetSource = Model.NguonNganSach.HasValue ? Model.NguonNganSach.Value : 0,
                    AgencyId = Model.IdDonVi,
                    VoucherDate = Model.NgayChungTu,
                    UserName = _sessionService.Current.Principal
                };
            }
            Items = new ObservableCollection<LevelBuggetDetailModel>();
        }

        private void LoadTypeDisplay()
        {
            TypeDisplays = new ObservableCollection<ComboboxItem>();
            TypeDisplays.Add(new ComboboxItem { ValueItem = TypeDisplay.CO_DU_LIEU, DisplayItem = "Có dữ liệu" });
            TypeDisplays.Add(new ComboboxItem { ValueItem = TypeDisplay.TAT_CA, DisplayItem = "Hiển thị tất cả" });
            SelectedTypeDisplays = TypeDisplay.TAT_CA;
        }

        private void LoadBudgetIndexCondition()
        {
            List<NsMuclucNgansachModel> listBudgetCatalog = new List<NsMuclucNgansachModel>();
            BudgetIndexForBudgetCriteria searchCondition = new BudgetIndexForBudgetCriteria
            {
                LNS = string.Join(",", Model != null ? Model.Lns : string.Empty),
                YearOfWork = _sessionService.Current.YearOfWork,
                GenerateAgencyId = _sessionService.Current.IdDonVi
            };
            List<NsMucLucNganSach> listMucLucNganSach = _mucLucNganSachService.FindByDefenseBudget(searchCondition);
            listMucLucNganSach = listMucLucNganSach.GroupBy(n => n.XauNoiMa).Select(n => n.First()).ToList();
            listBudgetCatalog = _mapper.Map<List<NsMuclucNgansachModel>>(listMucLucNganSach);
            listBudgetCatalog.Insert(0, new NsMuclucNgansachModel(string.Empty, "-- TẤT CẢ --"));
            BudgetCatalogItems = new ObservableCollection<NsMuclucNgansachModel>(listBudgetCatalog);
            _budgetCatalogFilter = CollectionViewSource.GetDefaultView(BudgetCatalogItems);
            _budgetCatalogFilter.Filter = OnBudgetCatalogFilter;
        }

        public override void LoadData(params object[] args)
        {
            try
            {
                base.LoadData(args);
                if (_searchCondition == null)
                    return;
                _searchCondition.Type = Model.LoaiChungTu.Value.ToString();
                List<LbChungTuChiTietQuery> data = _chungTuChiTietService.FindChungTuChiTietByCondition(_searchCondition).ToList();
                if (data != null && data.Count > 0)
                {
                    data.Select(n => { n.IdDonVi = Model.IdDonVi; n.TenDonVi = Model.TenDonVi; return n; }).ToList();
                }

                if (Model != null && Model.LoaiChungTu.HasValue && Model.LoaiChungTu.ToString() == NguonNganSach.NSQP.ToString())
                {
                    List<DanhMuc> listDanhMuc = _danhMucService.FindByType("NS_Nganh", _sessionService.Current.YearOfWork).Where(n => n.SGiaTri == Model.IdDonVi).ToList();
                    if (listDanhMuc != null && listDanhMuc.Count > 0)
                    {
                        List<LbChungTuChiTietQuery> listChild = data.Where(n => listDanhMuc.Select(m => m.IIDMaDanhMuc).ToList().Contains(n.Ng)).ToList();
                        List<string> listParentXauNoiMa = StringUtils.GetListXauNoiMaParent(listChild.Select(n => n.XauNoiMa).ToList());
                        data = data.Where(n => listParentXauNoiMa.Contains(n.XauNoiMa)).ToList();
                    }
                }
                //LoadDataPhanCap(ref data);

                Items = _mapper.Map<ObservableCollection<Model.LevelBuggetDetailModel>>(data);

                _dataDetailFilter = CollectionViewSource.GetDefaultView(Items);
                _dataDetailFilter.Filter = DataDetailFilter;
                if (Items != null && Items.Count > 0)
                {
                    SelectedItem = Items.FirstOrDefault();
                }
                foreach (LevelBuggetDetailModel model in Items)
                {
                    if (!model.IsHangCha)
                    {
                        model.PropertyChanged += DetailModel_PropertyChanged;
                    }
                }
                CalculateData();
                OnPropertyChanged(nameof(Items));
                OnPropertyChanged(nameof(IsSaveData));
                OnPropertyChanged(nameof(IsDeleteAll));
                OnRefeshFilter();
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        //private void LoadDataPhanCap(ref List<LbChungTuChiTietQuery> data)
        //{
        //    foreach (LbChungTuChiTietQuery item in data.Where(x => x.IdChungTu != Guid.Empty && x.IdChungTu != null).ToList())
        //    {
        //        List<NsNganhChungTuChiTietPhanCap> dataPhanCap = _lbChungTuChiTietPhanCapService.FindByChiTietId(item.Id.ToString(), item.IdDonVi, Model.NamLamViec.Value);
        //        if (dataPhanCap != null && dataPhanCap.Count > 0)
        //        {
        //            item.SoChuaPhan = item.PhanCap - dataPhanCap.Where(n => n.IIdMlns == item.MlnsId).Select(n => n.FPhanCap.HasValue ? n.FPhanCap.Value : 0).Sum();
        //        }
        //    }
        //}

        private void CalculateData()
        {
            // Reset value parrent
            Items.Where(x => x.IsHangCha)
                .Select(x => { x.TuChi = 0; x.PhanCap = 0; x.PhanCapCon = 0; x.ChuaPhanCap = 0; x.HangNhap = 0; x.HangMua = 0; return x; }).ToList();
            // Caculate value child
            foreach (var item in Items.Where(x => x.IsFilter && !x.IsHangCha && !x.IsDeleted && (x.TuChi != 0 || x.PhanCap != 0 || x.PhanCapCon != 0 || x.ChuaPhanCap != 0 || x.HangNhap != 0 || x.HangMua != 0)))
            {
                CalculateParent(item, item);
            }
            // Caculate total
            CalculateTotal();
        }

        private void OnRefeshFilter()
        {
            _dataDetailFilter.Refresh();
            CalculateData();
        }

        private void CalculateParent(LevelBuggetDetailModel currentItem, LevelBuggetDetailModel selfItem)
        {
            var parentItem = Items.Where(x => x.MlnsId == currentItem.MlnsIdParent).FirstOrDefault();
            if (parentItem == null) return;
            parentItem.TuChi += selfItem.TuChi;
            parentItem.PhanCap += selfItem.PhanCap;
            parentItem.PhanCapCon += selfItem.PhanCapCon;
            parentItem.ChuaPhanCap += selfItem.ChuaPhanCap;
            parentItem.HangNhap += selfItem.HangNhap;
            parentItem.HangMua += selfItem.HangMua;
            CalculateParent(parentItem, selfItem);
        }

        private void OnSaveData()
        {
            try
            {
                List<LevelBuggetDetailModel> dataDetailsAdd = Items.Where(x => x.IsModified && (x.IdChungTu == Guid.Empty || x.IdChungTu == null) && !x.IsDeleted).ToList();
                List<LevelBuggetDetailModel> dataDetailsUpdate = Items.Where(x => x.IsModified && x.IdChungTu != Guid.Empty && x.IdChungTu != null && !x.IsDeleted).ToList();
                List<LevelBuggetDetailModel> dataDetailsDelete = Items.Where(x => x.IsDeleted && x.IdChungTu != Guid.Empty && x.IdChungTu != null).ToList();

                // Thêm mới chứng từ chi tiết
                if (dataDetailsAdd.Count > 0)
                {
                    dataDetailsAdd = dataDetailsAdd.Select(x =>
                    {
                        x.IdChungTu = Model.Id;
                        x.ILoai = Model.ILoai;
                        x.ITrangThai = 1;
                        x.NamLamViec = Model.NamLamViec;
                        x.NamNganSach = Model.NamNganSach;
                        x.IsModified = false;
                        x.DateCreated = DateTime.Now;
                        x.NguonNganSach = Model.NguonNganSach.HasValue ? Model.NguonNganSach.Value : 0;
                        x.UserCreator = _sessionService.Current.Principal;
                        return x;
                    }).ToList();
                    List<NsNganhChungTuChiTiet> listChungTuChiTiets = new List<NsNganhChungTuChiTiet>();
                    listChungTuChiTiets = _mapper.Map<List<NsNganhChungTuChiTiet>>(dataDetailsAdd);
                    listChungTuChiTiets.Select(n => { n.Id = Guid.Empty; return n; }).ToList();
                    _chungTuChiTietService.AddRange(listChungTuChiTiets);
                }

                // Cập nhật chứng từ chi tiết
                if (dataDetailsUpdate.Count > 0)
                {
                    foreach (var item in dataDetailsUpdate)
                    {
                        item.IsModified = false;
                        NsNganhChungTuChiTiet chungTuChiTiet = _chungTuChiTietService.Find(item.Id);
                        _mapper.Map(item, chungTuChiTiet);
                        chungTuChiTiet.DNgaySua = DateTime.Now;
                        chungTuChiTiet.INamNganSach = Model.NamNganSach;
                        chungTuChiTiet.SNguoiSua = _sessionService.Current.Principal;
                        _chungTuChiTietService.Update(chungTuChiTiet);
                    }
                }

                // Delete
                if (dataDetailsDelete.Count > 0)
                {
                    foreach (var item in dataDetailsDelete)
                    {
                        _chungTuChiTietService.Delete(item.Id);
                    }
                }
                _chungTuService.UpdateTotalLbChungTu(Model.Id.ToString(), _sessionService.Current.Principal);
                MessageBoxHelper.Info(Resources.MsgSaveDone);
                LoadData();
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        protected override void OnLockUnLock()
        {
            try
            {
                string msgConfirm = string.Format(Model.IsLocked ? Resources.MsgUnLock : Resources.MsgLock, Environment.NewLine, Environment.NewLine);
                string msgDone = Model.IsLocked ? Resources.MsgUnLockDone : Resources.MsgLockDone;
                var result = MessageBoxHelper.Confirm(msgConfirm);

                if (result == MessageBoxResult.Yes)
                {
                    _chungTuService.LockOrUnLock(Model.Id, !Model.IsLocked);
                    Model.IsLocked = !Model.IsLocked;
                    OnPropertyChanged(nameof(IsSaveData));
                    OnPropertyChanged(nameof(IsDeleteAll));
                    MessageBoxHelper.Info(msgDone);
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        protected override void OnAdd()
        {
            if (SelectedItem != null)
            {
                int currentRow = Items.IndexOf(SelectedItem);
                int targetRow = Items.ToList().FindIndex(currentRow, x => x.IsEditable);
                if (targetRow > -1)
                {
                    LevelBuggetDetailModel sourceItem = Items.ElementAt(targetRow);
                    LevelBuggetDetailModel targetItem = ObjectCopier.Clone(sourceItem);

                    targetItem.Id = Guid.NewGuid();
                    targetItem.IdChungTu = null;
                    targetItem.IdDonVi = SelectedItem.IdDonVi;
                    targetItem.TuChi = 0;
                    targetItem.PhanCap = 0;
                    targetItem.ChuaPhanCap = 0;
                    targetItem.HangNhap = 0;
                    targetItem.HangMua = 0;
                    targetItem.GhiChu = null;
                    targetItem.IsModified = true;
                    targetItem.PropertyChanged += DetailModel_PropertyChanged;

                    Items.Insert(targetRow + 1, targetItem);
                    OnPropertyChanged(nameof(Items));
                    OnPropertyChanged(nameof(IsSaveData));
                    OnPropertyChanged(nameof(IsDeleteAll));
                }
            }
        }

        protected override void OnDelete()
        {
            if (Items != null && Items.Count > 0 && SelectedItem != null && !SelectedItem.IsHangCha && !Model.IsLocked)
            {
                SelectedItem.IsDeleted = !SelectedItem.IsDeleted;
                CalculateData();
                OnPropertyChanged(nameof(IsSaveData));
                OnPropertyChanged(nameof(IsDeleteAll));
            }
        }

        protected override void OnRefresh()
        {
            IsOpenRefresh = !IsOpenRefresh;
        }

        private void OnRefreshAllData()
        {
            try
            {
                if (IsSaveData)
                {
                    var result = MessageBoxHelper.ConfirmCancel(Resources.MsgConfirmEdit);
                    if (result == MessageBoxResult.Yes)
                    {
                        OnSaveData();
                        LoadData();
                    }
                    else if (result == MessageBoxResult.No)
                    {
                        LoadData();
                    }
                }
                else
                {
                    LoadData();
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        private void OnCloseWindow()
        {
            DataChangedEventHandler handler = ClosePopup;
            if (handler != null)
            {
                handler(this, new EventArgs());
            }
        }

        private void CalculateTotal()
        {
            Model.TongTuChi = 0;
            Model.TongPhanCap = 0;
            Model.TongSoChuaPhan = 0;
            Model.TongChuaPhanCap = 0;
            Model.TongHangNhap = 0;
            Model.TongHangMua = 0;
            List<LevelBuggetDetailModel> listChildren = Items.Where(x => !x.IsHangCha && !x.IsDeleted && x.IsFilter).ToList();
            foreach (LevelBuggetDetailModel item in listChildren)
            {
                Model.TongTuChi += item.TuChi;
                Model.TongPhanCap += item.PhanCap;
                Model.TongSoChuaPhan += item.SoChuaPhan;
                Model.TongChuaPhanCap += item.ChuaPhanCap;
                Model.TongHangNhap += item.HangNhap;
                Model.TongHangMua += item.HangMua;
            }
        }

        private bool DataDetailFilter(object obj)
        {
            bool result = true;
            var item = (LevelBuggetDetailModel)obj;
            if (!string.IsNullOrEmpty(SelectedLNS))
                result = result && item.Lns.ToLower().StartsWith(SelectedLNS.Trim().ToLower());

            if (!string.IsNullOrEmpty(SelectedTypeDisplays))
            {
                if (SelectedTypeDisplays == TypeDisplay.CO_DU_LIEU)
                    result = result && (item.TuChi != 0 || item.PhanCap != 0 || item.ChuaPhanCap != 0 || item.HangNhap != 0 || item.HangMua != 0 || (item.IsModified && (item.IdChungTu == Guid.Empty || item.IdChungTu == null) && !item.IsDeleted));
            }

            if (!string.IsNullOrEmpty(DetailFilter.L))
                result = result && item.L.ToLower().StartsWith(DetailFilter.L.Trim().ToLower());
            if (!string.IsNullOrEmpty(DetailFilter.K))
                result = result && item.K.ToLower().StartsWith(DetailFilter.K.Trim().ToLower());
            if (!string.IsNullOrEmpty(DetailFilter.M))
                result = result && item.M.ToLower().StartsWith(DetailFilter.M.Trim().ToLower());
            if (!string.IsNullOrEmpty(DetailFilter.TM))
                result = result && item.Tm.ToLower().StartsWith(DetailFilter.TM.Trim().ToLower());
            if (!string.IsNullOrEmpty(DetailFilter.TTM))
                result = result && item.Ttm.ToLower().StartsWith(DetailFilter.TTM.Trim().ToLower());
            if (!string.IsNullOrEmpty(DetailFilter.NG))
                result = result && item.Ng.ToLower().StartsWith(DetailFilter.NG.Trim().ToLower());
            item.IsFilter = result;
            return result;
        }

        private bool OnBudgetCatalogFilter(object obj)
        {
            if (string.IsNullOrWhiteSpace(_searchLNS))
            {
                return true;
            }
            return obj is NsMuclucNgansachModel item && item.Lns.ToLower().Contains(_searchLNS!.ToLower());
        }

        public void OnShowPopupChild()
        {
            if (SelectedItem.IsHangCha || SelectedItem.IsDeleted || SelectedItem.IdChungTu == null || SelectedItem.IdChungTu == Guid.Empty || SelectedItem.PhanCap == 0)
                return;
            //base.OnSelectionDoubleClick(obj);
            OpenDetailDialog();
        }

        private void OpenDetailDialog()
        {
            if (SelectedItem == null)
                return;
            List<NsMucLucNganSach> mucluc = _mucLucNganSachService.FindAll(_sessionService.Current.YearOfWork).ToList();

            if (Model != null && Model.LoaiChungTu.HasValue && Model.LoaiChungTu.ToString() == NguonNganSach.NSQP.ToString()
                && (mucluc.Where(n => n.XauNoiMa == SelectedItem.XauNoiMa.Replace("1040100", "1020100")).ToList().Count == 0))
            {
                return;
            }
            else if (mucluc.Where(n => n.XauNoiMa == SelectedItem.XauNoiMa).ToList().Count == 0)
            {
                return;
            }

            if (Model != null && Model.LoaiChungTu.HasValue && Model.LoaiChungTu.ToString() == NguonNganSach.NSQP.ToString())
            {
                LevelBuggetDetailChildViewModel.XauNoiMa = SelectedItem.XauNoiMa.Replace("1040100", "1020100");
                LevelBuggetDetailChildViewModel.ListXauNoiMa = string.Join(",", StringUtils.SplitXauNoiMaParent(SelectedItem.XauNoiMa.Replace("1040100", "1020100")));
            }
            else
            {
                LevelBuggetDetailChildViewModel.XauNoiMa = SelectedItem.XauNoiMa;
                LevelBuggetDetailChildViewModel.ListXauNoiMa = string.Join(",", StringUtils.SplitXauNoiMaParent(SelectedItem.XauNoiMa));
            }
            LevelBuggetDetailChildViewModel.IdChiTiet = SelectedItem.Id.ToString();
            LevelBuggetDetailChildViewModel.TotalGlobal = SelectedItem.PhanCap;
            LevelBuggetDetailChildViewModel.Id_DonVi = Model.IdDonVi;
            LevelBuggetDetailChildViewModel.TenDonVi = Model.TenDonVi;
            LevelBuggetDetailChildViewModel.Model = Model;
            LevelBuggetDetailChildViewModel.SavedAction = obj =>
            {
                this.LoadData();
            };
            LevelBuggetDetailChildViewModel.Init();
            var view = new LevelBuggetDetailChild() { DataContext = LevelBuggetDetailChildViewModel };
            view.ShowDialog();
        }

        protected override void OnSelectedItemChanged()
        {
            base.OnSelectedItemChanged();
            OnPropertyChanged(nameof(IsEnableButtonDelete));
        }

        private void DetailModel_PropertyChanged(object sender, PropertyChangedEventArgs args)
        {
            if (args.PropertyName == nameof(LevelBuggetDetailModel.TuChi) ||
                args.PropertyName == nameof(LevelBuggetDetailModel.GhiChu) ||
                args.PropertyName == nameof(LevelBuggetDetailModel.PhanCap) ||
                args.PropertyName == nameof(LevelBuggetDetailModel.SoChuaPhan) ||
                args.PropertyName == nameof(LevelBuggetDetailModel.HangNhap) ||
                args.PropertyName == nameof(LevelBuggetDetailModel.HangMua) ||
                args.PropertyName == nameof(LevelBuggetDetailModel.ChuaPhanCap)
                )
            {
                LevelBuggetDetailModel item = Items.Where(x => x.Id == ((LevelBuggetDetailModel)sender).Id).First();
                item.IsModified = true;
                if (args.PropertyName == nameof(LevelBuggetDetailModel.TuChi) || args.PropertyName == nameof(LevelBuggetDetailModel.PhanCap) || args.PropertyName == nameof(LevelBuggetDetailModel.SoChuaPhan) || args.PropertyName == nameof(LevelBuggetDetailModel.ChuaPhanCap)
                    || args.PropertyName == nameof(LevelBuggetDetailModel.HangNhap) || args.PropertyName == nameof(LevelBuggetDetailModel.HangMua)
                    )
                {
                    CalculateData();
                }
                OnPropertyChanged(nameof(IsSaveData));
                OnPropertyChanged(nameof(IsDeleteAll));
            }
        }

        protected override void OnDeleteAll()
        {
            base.OnDeleteAll();
            var result = MessageBoxHelper.Confirm(Resources.DeleteAllChungTuChiTiet);
            if (result == MessageBoxResult.No)
                return;
            else if (result == MessageBoxResult.Yes)
            {
                //_chungTuChiTietService.DeleteByChungTuId(Model.Id);
                List<LevelBuggetDetailModel> dataDetailsDelete = Items.Where(x => x.IdChungTu != Guid.Empty && x.IdChungTu != null && x.IsFilter).ToList();
                if (dataDetailsDelete.Count > 0)
                {
                    foreach (var item in dataDetailsDelete)
                    {
                        _chungTuChiTietService.Delete(item.Id);
                    }
                }
                _chungTuService.UpdateTotalLbChungTu(Model.Id.ToString(), _sessionService.Current.Principal);
                LoadData();
                MessageBoxHelper.Info(Resources.MsgDeleteSuccess);
                OnPropertyChanged(nameof(IsDeleteAll));
            }
        }
    }
}