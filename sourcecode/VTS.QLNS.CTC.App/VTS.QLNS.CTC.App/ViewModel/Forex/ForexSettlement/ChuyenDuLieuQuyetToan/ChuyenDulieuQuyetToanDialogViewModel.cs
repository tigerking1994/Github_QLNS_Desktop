using AutoMapper;
using log4net;
using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using VTS.QLNS.CTC.App.Command;
using VTS.QLNS.CTC.App.Helper;
using VTS.QLNS.CTC.App.Model;
using VTS.QLNS.CTC.App.Model.Control;
using VTS.QLNS.CTC.App.Properties;
using VTS.QLNS.CTC.App.Service;
using VTS.QLNS.CTC.App.Service.Impl;
using VTS.QLNS.CTC.App.View.Forex.ForexSettlement.ChuyenDuLieuQuyetToan;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Service;
using VTS.QLNS.CTC.Utility;
using VTS.QLNS.CTC.Utility.Enum;

namespace VTS.QLNS.CTC.App.ViewModel.Forex.ForexSettlement.ChuyenDuLieuQuyetToan
{
    public class ChuyenDulieuQuyetToanDialogViewModel: DialogViewModelBase<NhQtChuyenQuyetToanModel>
    {
        private readonly ILog _logger;
        private readonly IMapper _mapper;
        private readonly INsDonViService _nsDonViService;
        private readonly ISessionService _sessionService;
        private readonly INsMucLucNganSachService _nsMucLucNganSachService;
        private readonly INhQtChuyenQuyetToanService _nhQtChuyenQuyetToanService;
        private readonly INhQtChuyenQuyetToanChiTietService _nhQtChuyenQuyetToanChiTietService;
        private ICollectionView _itemsCollectionView;
        private SessionInfo _sessionInfo;

        private ObservableCollection<DonViModel> _itemsDonVi;
        public ObservableCollection<DonViModel> ItemsDonVi
        {
            get => _itemsDonVi;
            set => SetProperty(ref _itemsDonVi, value);
        }

        private DonViModel _selectedDonVi;
        public DonViModel SelectedDonVi
        {
            get => _selectedDonVi;
            set => SetProperty(ref _selectedDonVi, value);
        }

        private ObservableCollection<ComboboxItem> _itemsLoaiThoiGian;
        public ObservableCollection<ComboboxItem> ItemsLoaiThoiGian
        {
            get => _itemsLoaiThoiGian;
            set => SetProperty(ref _itemsLoaiThoiGian, value);
        }

        private ComboboxItem _selectedLoaiThoiGian;
        public ComboboxItem SelectedLoaiThoiGian
        {
            get => _selectedLoaiThoiGian;
            set
            {
                SetProperty(ref _selectedLoaiThoiGian, value);
                LoadThoiGian();
            }
        }

        private ObservableCollection<ComboboxItem> _itemsThoiGian;
        public ObservableCollection<ComboboxItem> ItemsThoiGian
        {
            get => _itemsThoiGian;
            set => SetProperty(ref _itemsThoiGian, value);
        }

        private ComboboxItem _selectedThoiGian;
        public ComboboxItem SelectedThoiGian
        {
            get => _selectedThoiGian;
            set => SetProperty(ref _selectedThoiGian, value);
        }

        private NhQtChuyenQuyetToanChiTietModel _selectedChuyenQuyetToanChiTiet;
        public NhQtChuyenQuyetToanChiTietModel SelectedChuyenQuyetToanChiTiet
        {
            get => _selectedChuyenQuyetToanChiTiet;
            set => SetProperty(ref _selectedChuyenQuyetToanChiTiet, value);
        }

        private ObservableCollection<NhQtChuyenQuyetToanChiTietModel> _itemsCQTChiTiet;
        public ObservableCollection<NhQtChuyenQuyetToanChiTietModel> ItemsCQTChiTiet
        {
            get => _itemsCQTChiTiet;
            set => SetProperty(ref _itemsCQTChiTiet, value);
        }

        private ObservableCollection<ComboboxItem> _itemsSLNS;
        public ObservableCollection<ComboboxItem> ItemsSLNS
        {
            get => _itemsSLNS;
            set => SetProperty(ref _itemsSLNS, value);
        }

        private ComboboxItem _selectedSLNS;
        public ComboboxItem SelectedSLNS
        {
            get => _selectedSLNS;
            set => SetProperty(ref _selectedSLNS, value);
        }

        private NhQtChuyenQuyetToanChiTietModel _itemsMLNSFilter;
        public NhQtChuyenQuyetToanChiTietModel ItemsMLNSFilter
        {
            get => _itemsMLNSFilter;
            set => SetProperty(ref _itemsMLNSFilter, value);
        }

        public override Type ContentType => typeof(ChuyenDuLieuQuyetToanDialog);
        public override string Title => "Chuyển dữ liệu quyết toán";
        public override string Name => "Chuyển dữ liệu quyết toán";
        public bool IsDetail { get; set; }
        public bool IsEditable => Model != null && !Model.Id.IsNullOrEmpty() && !Model.Id.Equals(Guid.Empty);

        public RelayCommand SearchCommand { get; }

        public ChuyenDulieuQuyetToanDialogViewModel(
            ILog logger,
            IMapper mapper,
            ISessionService sessionService,
            INsDonViService nsDonViService,
            INsMucLucNganSachService nsMucLucNganSachService,
            INhQtChuyenQuyetToanService nhQtChuyenQuyetToanService,
            INhQtChuyenQuyetToanChiTietService nhQtChuyenQuyetToanChiTietService)
        {
            _logger = logger;
            _mapper = mapper;
            _sessionService = sessionService;
            _nsDonViService = nsDonViService;
            _nsMucLucNganSachService = nsMucLucNganSachService;
            _nhQtChuyenQuyetToanService = nhQtChuyenQuyetToanService;
            _nhQtChuyenQuyetToanChiTietService = nhQtChuyenQuyetToanChiTietService;

            SearchCommand = new RelayCommand(obj => _itemsCollectionView.Refresh());
        }

        public override void Init()
        {
            LoadDefault();
            LoadDonVi();
            LoadLoaiThoiGian();
            //LoadSLNS();
            LoadData();
        }

        private void LoadDefault()
        {
            _sessionInfo = _sessionService.Current;
            ItemsMLNSFilter = new NhQtChuyenQuyetToanChiTietModel();
        }

        private void LoadSLNS()
        {
            DataTable dtsLNS = _nsMucLucNganSachService.FindLNSByYear(_sessionService.Current.YearOfWork);
            DataRow dtr = dtsLNS.NewRow();
            dtr["sLNS"] = "0";
            dtr["TenLNS"] = "--Tất cả--";
            dtsLNS.Rows.InsertAt(dtr, 0);
            IList<ComboboxItem> items = dtsLNS.AsEnumerable().Select(row =>
                new ComboboxItem
                {
                    ValueItem = row.Field<string>("sLNS"),
                    DisplayItem = row.Field<string>("TenLNS")
                }
            ).ToList();
            ItemsSLNS = _mapper.Map<ObservableCollection<ComboboxItem>>(items);
            _selectedSLNS = ItemsSLNS.FirstOrDefault();
            OnPropertyChanged(nameof(ItemsSLNS));
            OnPropertyChanged(nameof(SelectedSLNS));
        }

        private void LoadDonVi()
        {
            var data = _nsDonViService.FindAll().Where(x => x.NamLamViec == _sessionService.Current.YearOfWork).OrderBy(x => x.IIDMaDonVi);
            ItemsDonVi = _mapper.Map<ObservableCollection<DonViModel>>(data);
            OnPropertyChanged(nameof(ItemsDonVi));
        }

        private void LoadLoaiThoiGian()
        {
            _itemsLoaiThoiGian = new ObservableCollection<ComboboxItem>();
            _itemsLoaiThoiGian.Add(new ComboboxItem("Tháng", "1"));
            _itemsLoaiThoiGian.Add(new ComboboxItem("Quý", "2"));
            OnPropertyChanged(nameof(ItemsLoaiThoiGian));
        }

        private void LoadThoiGian()
        {
            _itemsThoiGian = new ObservableCollection<ComboboxItem>();
            if (SelectedLoaiThoiGian != null)
            {
                switch (SelectedLoaiThoiGian.ValueItem)
                {
                    case "1":
                        ComboboxItem month;
                        for (int i = 1; i <= 12; i++)
                        {
                            month = new ComboboxItem("Tháng " + i, i.ToString());
                            _itemsThoiGian.Add(month);
                        }
                        break;
                    case "2":
                        ComboboxItem quy;
                        for (int i = 1; i <= 4; i++)
                        {
                            quy = new ComboboxItem(LoaiQuyEnum.Get(i), i.ToString());
                            _itemsThoiGian.Add(quy);
                        }
                        break;
                    default:
                        break;
                }
            }
            OnPropertyChanged(nameof(ItemsThoiGian));
        }

        public override void LoadData(params object[] args)
        {
            try {
                if (Model.Id.IsNullOrEmpty() || Model.Id.Equals(Guid.Empty))
                {
                    IconKind = PackIconKind.PlaylistPlus;
                    Description = "Thêm mới chuyển dữ liệu quyết toán";
                    var mucLucNganSachList = _nsMucLucNganSachService.FindAll(_sessionService.Current.YearOfWork).Where(x => !string.IsNullOrEmpty(x.Lns) && x.Lns.StartsWith("3")).OrderBy(s => s.XauNoiMa);
                    ItemsCQTChiTiet = _mapper.Map<ObservableCollection<NhQtChuyenQuyetToanChiTietModel>>(mucLucNganSachList);
                    _selectedDonVi = null;
                    _selectedLoaiThoiGian = null;
                    _selectedThoiGian = null;
                }
                else
                {
                    NhQtChuyenQuyetToan entity = _nhQtChuyenQuyetToanService.FindById(Model.Id);
                    Model = _mapper.Map<NhQtChuyenQuyetToanModel>(entity);
                    IconKind = PackIconKind.NoteEditOutline;
                    Description = "Cập nhật chuyển dữ liệu quyết toán";

                    _selectedDonVi = _itemsDonVi.FirstOrDefault(x => x.IIDMaDonVi.Equals(Model.iID_MaDonVi));
                    _selectedLoaiThoiGian = _itemsLoaiThoiGian.FirstOrDefault(t => Model.iLoaiThoiGian.HasValue && t.ValueItem.Equals(Model.iLoaiThoiGian.Value.ToString()));
                    LoadThoiGian();
                    _selectedThoiGian = _itemsThoiGian.FirstOrDefault(t => Model.iThoiGian.HasValue && t.ValueItem.Equals(Model.iThoiGian.Value.ToString()));

                    var mucLucNganSachList = _nhQtChuyenQuyetToanChiTietService.FindAll().Where(x => x.iID_ChuyenQuyetToanID.HasValue && x.iID_ChuyenQuyetToanID.Value.Equals(Model.Id)).OrderBy(s => s.sXauNoiMa);
                    ItemsCQTChiTiet = _mapper.Map<ObservableCollection<NhQtChuyenQuyetToanChiTietModel>>(mucLucNganSachList);
                }

                foreach (var item in ItemsCQTChiTiet)
                {
                    item.PropertyChanged += DetailModel_PropertyChanged;
                }

                var idsMLNSCha = ItemsCQTChiTiet.Where(x => !x.BHangCha).Select(y => y.iID_MaMucLucNganSach_Cha).Distinct();
                foreach(var idCha in idsMLNSCha)
                {
                    CalculateMLNS(idCha);
                }
                _itemsCollectionView = CollectionViewSource.GetDefaultView(ItemsCQTChiTiet);
                _itemsCollectionView.Filter = Items_Filter;
                OnPropertyChanged(nameof(ItemsCQTChiTiet));

                OnPropertyChanged(nameof(Model));
                OnPropertyChanged(nameof(SelectedDonVi));
                OnPropertyChanged(nameof(SelectedLoaiThoiGian));
                OnPropertyChanged(nameof(SelectedThoiGian));
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        private void DetailModel_PropertyChanged(object sender, PropertyChangedEventArgs args)
        {
            var obj = (NhQtChuyenQuyetToanChiTietModel)sender;
            switch (args.PropertyName)
            {
                case nameof(NhQtChuyenQuyetToanChiTietModel.fGiaTriUSD):
                    CalculateMLNS(obj.iID_MaMucLucNganSach_Cha);
                    break;
            }
        }

        private void CalculateMLNS(Guid? iID_MaMucLucNganSach_Cha)
        {
            var childs = ItemsCQTChiTiet.Where(x => x.iID_MaMucLucNganSach_Cha == iID_MaMucLucNganSach_Cha);
            var parent = ItemsCQTChiTiet.Where(x => x.iID_MaMucLucNganSach == iID_MaMucLucNganSach_Cha).FirstOrDefault();
            if (!childs.IsEmpty())
            {
                parent.PropertyChanged -= DetailModel_PropertyChanged;
                parent.fGiaTriUSD = childs.Sum(x => x.fGiaTriUSD);
                parent.PropertyChanged += DetailModel_PropertyChanged;
                if (parent.iID_MaMucLucNganSach_Cha != null)
                {
                    CalculateMLNS(parent.iID_MaMucLucNganSach_Cha);
                }
            }
        }

        public void ChuyenDuLieu_BeginningEditHanlder(DataGridBeginningEditEventArgs e)
        {
            SelectedChuyenQuyetToanChiTiet = (NhQtChuyenQuyetToanChiTietModel)e.Row.Item;
            var childs = ItemsCQTChiTiet.Where(x => x.iID_MaMucLucNganSach_Cha == SelectedChuyenQuyetToanChiTiet.iID_MaMucLucNganSach);
            if (e.Column.SortMemberPath.Equals(nameof(NhQtChuyenQuyetToanChiTietModel.fGiaTriUSD)))
            {
                if (SelectedChuyenQuyetToanChiTiet != null && SelectedChuyenQuyetToanChiTiet.BHangCha && !childs.IsEmpty())
                {
                    e.Cancel = true;
                }
            }
        }

        private bool Items_Filter(object obj)
        {
            if (obj is NhQtChuyenQuyetToanChiTietModel item)
            {
                bool result = true;
                if (ItemsMLNSFilter != null)
                {
                    if (!string.IsNullOrWhiteSpace(ItemsMLNSFilter.sLNS)) result = result && item.sLNS.ToLower().Contains(ItemsMLNSFilter.sLNS.Trim().ToLower());
                    if (!string.IsNullOrWhiteSpace(ItemsMLNSFilter.sL)) result = result && item.sL.ToLower().Contains(ItemsMLNSFilter.sL.Trim().ToLower());
                    if (!string.IsNullOrWhiteSpace(ItemsMLNSFilter.sK)) result = result && item.sK.ToLower().Contains(ItemsMLNSFilter.sK.Trim().ToLower());
                    if (!string.IsNullOrWhiteSpace(ItemsMLNSFilter.sM)) result = result && item.sM.ToLower().Contains(ItemsMLNSFilter.sM.Trim().ToLower());
                    if (!string.IsNullOrWhiteSpace(ItemsMLNSFilter.sTM)) result = result && item.sTM.ToLower().Contains(ItemsMLNSFilter.sTM.Trim().ToLower());
                    if (!string.IsNullOrWhiteSpace(ItemsMLNSFilter.sTTM)) result = result && item.sTTM.ToLower().Contains(ItemsMLNSFilter.sTTM.Trim().ToLower());
                    if (!string.IsNullOrWhiteSpace(ItemsMLNSFilter.sNG)) result = result && item.sNG.ToLower().Contains(ItemsMLNSFilter.sNG.Trim().ToLower());
                    if (!string.IsNullOrWhiteSpace(ItemsMLNSFilter.sTNG)) result = result && item.sTNG.ToLower().Contains(ItemsMLNSFilter.sTNG.Trim().ToLower());
                    if (!string.IsNullOrWhiteSpace(ItemsMLNSFilter.sMoTa)) result = result && item.sMoTa.ToLower().Contains(ItemsMLNSFilter.sMoTa.Trim().ToLower());
                }
                return result;
            }
            return false;
        }

        public override void OnSave(object obj)
        {
            if (SelectedDonVi != null)
            {
                Model.iID_DonViID = SelectedDonVi.Id;
                Model.iID_MaDonVi = SelectedDonVi.IIDMaDonVi;
            }
            if (SelectedLoaiThoiGian != null) Model.iLoaiThoiGian = int.Parse(SelectedLoaiThoiGian.ValueItem);
            if (SelectedThoiGian != null) Model.iThoiGian = int.Parse(SelectedThoiGian.ValueItem);
            if (!ValidateViewModelHelper.Validate(Model)) return;
            if (!Validate()) return;

            BackgroundWorkerHelper.Run((s, e) =>
            {
                IsLoading = true;

                NhQtChuyenQuyetToan entity;
                if (Model.Id.IsNullOrEmpty() || Model.Id.Equals(Guid.Empty))
                {
                    // Thêm mới
                    entity = _mapper.Map<NhQtChuyenQuyetToan>(Model);
                    entity.dNgayTao = DateTime.Now;
                    entity.sNguoiTao = _sessionInfo.Principal;
                    _nhQtChuyenQuyetToanService.Add(entity);
                }
                else
                {
                    // Cập nhật
                    entity = _nhQtChuyenQuyetToanService.FindById(Model.Id);
                    if (entity != null)
                    {
                        _mapper.Map(Model, entity);
                        entity.dNgaySua = DateTime.Now;
                        entity.sNguoiSua = _sessionInfo.Principal;
                        _nhQtChuyenQuyetToanService.Update(entity);
                    }
                }

                List<NhQtChuyenQuyetToanChiTiet> entities = new List<NhQtChuyenQuyetToanChiTiet>();
                NhQtChuyenQuyetToanChiTiet entityDetail;
                foreach (var item in ItemsCQTChiTiet)
                {
                    entityDetail = _mapper.Map<NhQtChuyenQuyetToanChiTiet>(item);
                    entityDetail.Id = Guid.NewGuid();
                    entityDetail.iID_ChuyenQuyetToanID = (Model.Id.IsNullOrEmpty() || Model.Id.Equals(Guid.Empty)) ? entity.Id : Model.Id;
                    entities.Add(entityDetail);
                }
                _nhQtChuyenQuyetToanService.SaveNhQtChuyenQuyetToanChiTiet(entities, Model.Id);
                e.Result = entity;
            }, (s, e) =>
            {
                if (e.Error == null)
                {
                    // Reload data
                    Model = _mapper.Map<NhQtChuyenQuyetToanModel>(e.Result);

                    // Invoke message
                    MessageBoxHelper.Info(Resources.MsgSaveDone);

                    SavedAction?.Invoke(Model);
                    var view = obj as Window;
                    view.Close();
                }
                else
                {
                    _logger.Error(e.Error.Message);
                }
                IsLoading = false;
            });
        }

        private bool Validate()
        {
            //List<string> lstError = new List<string>();
            //if (Model.sSoChungTu.IsEmpty(""))
            //{
            //    lstError.Add(string.Format(Resources.MsgInputRequire, "số chứng từ!"));
            //}
            //if (SelectedDonVi == null)
            //{
            //    lstError.Add(Resources.MsgCheckDonVi);
            //}
            //if (SelectedLoaiThoiGian == null)
            //{
            //    lstError.Add(string.Format(Resources.MsgInputDropdownRequire, "loại thời gian"));
            //}
            //if (SelectedThoiGian == null)
            //{
            //    lstError.Add(string.Format(Resources.MsgInputDropdownRequire, "thời gian"));
            //}
            //if (lstError.Count > 0)
            //{
            //    MessageBoxHelper.Warning(string.Join("\n", lstError));
            //    return false;
            //}
            if (_nhQtChuyenQuyetToanService.CheckExistsCQTByTimeAndDonvi(Model.Id, SelectedDonVi.Id, int.Parse(SelectedLoaiThoiGian.ValueItem), int.Parse(SelectedThoiGian.ValueItem)))
            {
                MessageBoxHelper.Error(Resources.MsgErrorExistChuyenQuyetToan);
                return false;
            }
            return true;
        }

        public override void OnClosing()
        {
            if (!ItemsDonVi.IsEmpty()) ItemsDonVi.Clear();
            if (!ItemsLoaiThoiGian.IsEmpty()) ItemsLoaiThoiGian.Clear();
            if (!ItemsThoiGian.IsEmpty()) ItemsThoiGian.Clear();
            //if (!ItemsSLNS.IsEmpty()) ItemsSLNS.Clear();
            if (!ItemsCQTChiTiet.IsEmpty()) ItemsCQTChiTiet.Clear();
            SelectedDonVi = null;
            SelectedLoaiThoiGian = null;
            SelectedThoiGian = null;
        }

        public override void OnClose(object obj)
        {
            if (obj is Window window)
            {
                window.Close();
            }
        }
    }
}
