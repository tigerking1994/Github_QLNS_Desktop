using AutoMapper;
using MaterialDesignThemes.Wpf;
using log4net;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using VTS.QLNS.CTC.App.Command;
using VTS.QLNS.CTC.App.Helper;
using VTS.QLNS.CTC.App.Model;
using VTS.QLNS.CTC.App.Model.Control;
using VTS.QLNS.CTC.App.Properties;
using VTS.QLNS.CTC.App.Service;
using VTS.QLNS.CTC.App.View.Category;
using VTS.QLNS.CTC.App.ViewModel.Category;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Service;
using VTS.QLNS.CTC.Utility;
using VTS.QLNS.CTC.Utility.Enum;
using VTS.QLNS.CTC.App.Service.Impl;
using System.IO;
using VTS.QLNS.CTC.App.ViewModel;
using VTS.QLNS.CTC.App.View.Forex.ForexMuaSam.MuaSamTrongNuoc.MSTNKeHoachDatHang;
using System.ComponentModel;
using System.Windows.Controls;
using VTS.QLNS.CTC.Core.Domain.Query.Shared;
using VTS.QLNS.CTC.App.Extensions;

namespace VTS.QLNS.CTC.App.ViewModel.Forex.ForexMuaSam.MuaSamTrongNuoc.MSTNKeHoachDatHang
{
    public class MSTNKeHoachDatHangDialogViewModel : DialogAttachmentViewModelBase<NhMstnKeHoachDatHangModel>
    {
        #region Private
        private readonly INhMstnKeHoachDatHangService _service;
        private readonly INhMstnKeHoachDatHangDanhMucService _danhMucService;
        private readonly INsDonViService _dvService;
        private readonly INhKhTongTheService _iNhKhTongTheService;
        private readonly ISessionService _sessionService;
        private readonly INhDmTiGiaService _nhDmTiGiaService;
        private readonly INhDmTiGiaChiTietService _nhDmTiGiaChiTietService;
        private readonly INhDmNhiemVuChiService _nhDmNhiemVuChiService;
        private readonly INhDmNhaThauService _iNhDmNhaThauService;
        private readonly INhDaHopDongHangMucService _iNhHDHangMucService;
        private readonly ILog _logger;
        #endregion

        #region Public
        public override string Name => "THÔNG TIN KẾ HOẠCH ĐẶT HÀNG";
        public override Type ContentType => typeof(MSTNKeHoachDatHangDialog);
        public bool BIsDieuChinh => IsAdd && Model.IIdParentID.HasValue && !IsDetail;
        public bool IsEdit => !IsAdd || BIsDieuChinh || IsDetail;
        public bool IsInsert => Model == null || Model.Id.IsNullOrEmpty();
        #endregion

        #region Items
        private bool _isAdd;
        public bool IsAdd
        {
            get => _isAdd;
            set => SetProperty(ref _isAdd, value);
        }

        private bool _isDetail;
        public bool IsDetail
        {
            get => _isDetail;
            set => SetProperty(ref _isDetail, value);
        }

        private bool _isVisibleTiGiaNhap;
        public bool IsVisibleTiGiaNhap
        {
            get => _isVisibleTiGiaNhap;
            set => SetProperty(ref _isVisibleTiGiaNhap, value);
        }

        private string _sTitlePheDuyetUsd;
        public string STitlePheDuyetUsd
        {
            get => _sTitlePheDuyetUsd;
            set => SetProperty(ref _sTitlePheDuyetUsd, value);
        }

        private string _sTitlePheDuyetVnd;
        public string STitlePheDuyetVnd
        {
            get => _sTitlePheDuyetVnd;
            set => SetProperty(ref _sTitlePheDuyetVnd, value);
        }

        private string _sTitlePheDuyetEur;
        public string STitlePheDuyetEur
        {
            get => _sTitlePheDuyetEur;
            set => SetProperty(ref _sTitlePheDuyetEur, value);
        }

        private string _sTitlePheDuyetKhac;
        public string STitlePheDuyetKhac
        {
            get => _sTitlePheDuyetKhac;
            set => SetProperty(ref _sTitlePheDuyetKhac, value);
        }

        private ObservableCollection<NhMstnKeHoachDatHangDanhMucModel> _itemsDanhMuc;
        public ObservableCollection<NhMstnKeHoachDatHangDanhMucModel> ItemsDanhMuc
        {
            get => _itemsDanhMuc;
            set => SetProperty(ref _itemsDanhMuc, value);
        }

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
            set
            {
                SetProperty(ref _selectedDonVi, value);
                if (value != null)
                {
                    LoadKeHoachTongThe();
                }
            }
        }

        private ObservableCollection<ComboboxItem> _itemsKeHoachTongThe;
        public ObservableCollection<ComboboxItem> ItemsKeHoachTongThe
        {
            get => _itemsKeHoachTongThe;
            set => SetProperty(ref _itemsKeHoachTongThe, value);
        }

        private ComboboxItem _selectedKeHoachTongThe;
        public ComboboxItem SelectedKeHoachTongThe
        {
            get => _selectedKeHoachTongThe;
            set
            {
                SetProperty(ref _selectedKeHoachTongThe, value);
                if (value != null)
                {
                    LoadChuongTrinh();
                }
            }
        }

        private ObservableCollection<NhDmNhiemVuChiModel> _itemsChuongTrinh;
        public ObservableCollection<NhDmNhiemVuChiModel> ItemsChuongTrinh
        {
            get => _itemsChuongTrinh;
            set => SetProperty(ref _itemsChuongTrinh, value);
        }

        private NhDmNhiemVuChiModel _selectedChuongTrinh;
        public NhDmNhiemVuChiModel SelectedChuongTrinh
        {
            get => _selectedChuongTrinh;
            set => SetProperty(ref _selectedChuongTrinh, value);
        }

        private ObservableCollection<NhDmTiGiaModel> _itemsTiGia;
        public ObservableCollection<NhDmTiGiaModel> ItemsTiGia
        {
            get => _itemsTiGia;
            set => SetProperty(ref _itemsTiGia, value);
        }

        private NhDmTiGiaModel _selectedTiGia;
        public NhDmTiGiaModel SelectedTiGia
        {
            get => _selectedTiGia;
            set
            {
                SetProperty(ref _selectedTiGia, value);
                if (value != null)
                {
                    //LoadTiGiaChiTiet();
                    //SetLabelTiGiaChiTiet();
                    IsVisibleTiGiaNhap = true;
                    ShowTiGiaNhap();
                }
            }
        }

        private ObservableCollection<NhDmTiGiaChiTietModel> _itemsTiGiaChiTiet;
        public ObservableCollection<NhDmTiGiaChiTietModel> ItemsTiGiaChiTiet
        {
            get => _itemsTiGiaChiTiet;
            set => SetProperty(ref _itemsTiGiaChiTiet, value);
        }

        private ObservableCollection<NhDmTiGiaChiTietModel> _itemsTiGiaChiTietChinh;
        public ObservableCollection<NhDmTiGiaChiTietModel> ItemsTiGiaChiTietChinh
        {
            get => _itemsTiGiaChiTietChinh;
            set => SetProperty(ref _itemsTiGiaChiTietChinh, value);
        }

        private NhDmTiGiaChiTietModel _selectedTiGiaChiTiet;
        public NhDmTiGiaChiTietModel SelectedTiGiaChiTiet
        {
            get => _selectedTiGiaChiTiet;
            set
            {
                SetProperty(ref _selectedTiGiaChiTiet, value);
                //if (value != null)
                //{
                //    SetLabelTiGiaChiTiet();
                //}
            }
        }

        private double? _fGiaTriVND;
        public double? FGiaTriVND
        {
            get => _fGiaTriVND;
            set => SetProperty(ref _fGiaTriVND, value);
        }

        private double? _fGiaTriUSD;
        public double? FGiaTriUSD
        {
            get => _fGiaTriUSD;
            set => SetProperty(ref _fGiaTriUSD, value);
        }

        private double? _fGiaTriEUR;
        public double? FGiaTriEUR
        {
            get => _fGiaTriEUR;
            set => SetProperty(ref _fGiaTriEUR, value);
        }

        private double? _fGiaTriNgoaiTeKhac;
        public double? FGiaTriNgoaiTeKhac
        {
            get => _fGiaTriNgoaiTeKhac;
            set => SetProperty(ref _fGiaTriNgoaiTeKhac, value);
        }

        private string _sSoQuyetDinh;
        public string SSoQuyetDinh
        {
            get => _sSoQuyetDinh;
            set => SetProperty(ref _sSoQuyetDinh, value);
        }

        private DateTime? _dNgayQuyetDinh;
        public DateTime? DNgayQuyetDinh
        {
            get => _dNgayQuyetDinh;
            set => SetProperty(ref _dNgayQuyetDinh, value);
        }

        private string _sMoTa;
        public string SMoTa
        {
            get => _sMoTa;
            set => SetProperty(ref _sMoTa, value);
        }

        private string _sTiGia;
        public string STiGia
        {
            get => _sTiGia;
            set => SetProperty(ref _sTiGia, value);
        }

        private double? _fTiGiaNhap;
        public double? FTiGiaNhap
        {
            get => _fTiGiaNhap;
            set
            {
                if (SetProperty(ref _fTiGiaNhap, value))
                {
                    if (ItemsDanhMuc != null)
                    {
                        CalculateHangMuc();
                        CalculateGiaTriDuocDuyet();
                    }
                }
            }
        }

        private IEnumerable<NhDmTiGiaChiTiet> _lstTiGiaChiTiet;
        public IEnumerable<NhDmTiGiaChiTiet> LstTiGiaChiTiet
        {
            get => _lstTiGiaChiTiet;
            set => SetProperty(ref _lstTiGiaChiTiet, value);
        }

        private NhMstnKeHoachDatHangDanhMucModel _selectedDanhMuc;
        public NhMstnKeHoachDatHangDanhMucModel SelectedDanhMuc
        {
            get => _selectedDanhMuc;
            set => SetProperty(ref _selectedDanhMuc, value);
        }

        private ObservableCollection<ComboboxItem> _itemsNhaThau;
        public ObservableCollection<ComboboxItem> ItemsNhaThau
        {
            get => _itemsNhaThau;
            set => SetProperty(ref _itemsNhaThau, value);
        }
        #endregion

        #region RelayCommnand
        public RelayCommand AddDanhMucCommand { get; }
        public RelayCommand DeleteDanhMucCommand { get; }
        public RelayCommand SaveDataCommand { get; set; }
        #endregion

        public MSTNKeHoachDatHangDialogViewModel(
            INhMstnKeHoachDatHangService service,
            INhMstnKeHoachDatHangDanhMucService danhMucService,
            INsDonViService dvService,
            INhDaHopDongHangMucService iNhHDHangMucService,
            INhKhTongTheService iNhKhTongTheService,
            INhDmTiGiaService nhDmTiGiaService,
            INhDmTiGiaChiTietService nhDmTiGiaChiTietService,
            INhDmNhaThauService iNhDmNhaThauService,
            ISessionService sessionService,
            IStorageServiceFactory storageServiceFactory,
            IAttachmentService attachService,
            INhDmNhiemVuChiService nhDmNhiemVuChiService,
            IMapper mapper,
            ILog logger) : base(mapper, storageServiceFactory, attachService)
        {
            _service = service;
            _nhDmTiGiaService = nhDmTiGiaService;
            _nhDmTiGiaChiTietService = nhDmTiGiaChiTietService;
            _iNhDmNhaThauService = iNhDmNhaThauService;
            _danhMucService = danhMucService;
            _iNhKhTongTheService = iNhKhTongTheService;
            _nhDmNhiemVuChiService = nhDmNhiemVuChiService;
            _iNhHDHangMucService = iNhHDHangMucService;
            _dvService = dvService;
            _sessionService = sessionService;
            _logger = logger;

            AddDanhMucCommand = new RelayCommand(obj => OnAddDanhMuc(obj), obj => !BIsReadOnly && ((bool)obj || (SelectedDanhMuc != null)));
            DeleteDanhMucCommand = new RelayCommand(obj => OnDeleteDanhMuc(), obj => !BIsReadOnly && SelectedDanhMuc != null && CheckDelete());
            SaveDataCommand = new RelayCommand(obj => OnSaveData(obj));
        }

        #region Event
        public override void Init()
        {
            MarginRequirement = new Thickness(10);
            LoadTiGia();
            //LoadTiGiaChiTiet();
            LoadDonViQuanLy();
            LoadNhaThau();
            LoadData();
        }

        public override void LoadData(params object[] args)
        {
            if (Model.Id == Guid.Empty)
            {
                IconKind = PackIconKind.PlaylistPlus;
                Description = "Thêm mới kế hoạch đặt hàng";
                Title = "THÊM MỚI KẾ HOẠCH ĐẶT HÀNG";
                SelectedDonVi = null;
                SelectedKeHoachTongThe = null;
                SelectedChuongTrinh = null;
                SelectedTiGia= null;
                SSoQuyetDinh = string.Empty;
                DNgayQuyetDinh = DateTime.Now;
                SMoTa = string.Empty;
                FGiaTriUSD = null;
                FGiaTriVND = null;
                //FGiaTriEUR = null;
                //FGiaTriNgoaiTeKhac = null;
                FTiGiaNhap = null;
                ItemsDanhMuc = null;
                SelectedDanhMuc = null;
                IsVisibleTiGiaNhap = false;
            }
            else
            {
                if (BIsReadOnly)
                {
                    IconKind = PackIconKind.Details;
                    Description = "Chi tiết kế hoạch đặt hàng";
                    Title = "CHI TIẾT KẾ HOẠCH ĐẶT HÀNG";
                }
                else
                {
                    if (!IsDieuChinh)
                    {
                        IconKind = PackIconKind.NoteEditOutline;
                        Description = "Cập nhập kế hoạch đặt hàng";
                        Title = "CẬP NHẬP KẾ HOẠCH ĐẶT HÀNG";
                    }
                    else
                    {
                        IconKind = PackIconKind.Adjust;
                        Description = "Điều chỉnh kế hoạch đặt hàng";
                        Title = "ĐIỀU CHỈNH KẾ HOẠCH ĐẶT HÀNG";
                    }
                }
                SelectedDonVi = ItemsDonVi.FirstOrDefault(n => n.Id == Model.IIdDonViQuanLy);
                if (SelectedDonVi != null)
                {
                    SelectedKeHoachTongThe = ItemsKeHoachTongThe.FirstOrDefault(x => x.ValueItem.ToString().Equals(Model.IIdKhtongTheId.GetValueOrDefault().ToString()));
                    _selectedChuongTrinh = ItemsChuongTrinh.FirstOrDefault(x => x.IIdKHTTNhiemVuChiId == Model.IIdKHTTNhiemVuChiId);
                }
                SelectedTiGia = ItemsTiGia.FirstOrDefault(t => t.Id == Model.IIdTiGiaID);
                //SelectedTiGiaChiTiet = ItemsTiGiaChiTiet.FirstOrDefault(t => t.SMaTienTeQuyDoi == Model.SMaNgoaiTeKhac);
                if (SelectedTiGia != null)
                {
                    IsVisibleTiGiaNhap = true;
                }
                SSoQuyetDinh = Model.SSoQuyetDinh;
                DNgayQuyetDinh = Model.DNgayQuyetDinh;
                SMoTa = Model.SMoTa;
                FGiaTriUSD = Model.FGiaTriUsd;
                FGiaTriVND = Model.FGiaTriVnd;
                //FGiaTriEUR = Model.FGiaTriEur;
                //FGiaTriNgoaiTeKhac = Model.FGiaTriNgoaiTeKhac;
                FTiGiaNhap = Model.FTiGiaNhap;

                var data = _danhMucService.FindByKHDatHangId(Model.Id).ToList();
                ItemsDanhMuc = _mapper.Map<ObservableCollection<NhMstnKeHoachDatHangDanhMucModel>>(data);
                UpdateTreeItems();
                var sortingListQueries = new List<SortingListQuery<NhMstnKeHoachDatHangDanhMucModel>>();
                sortingListQueries = SortingHierarchicalNumber<NhMstnKeHoachDatHangDanhMucModel>.GetSortingLists(ItemsDanhMuc.Select(x => new SortingListQuery<NhMstnKeHoachDatHangDanhMucModel>() { Key = x, SortKey = x.SMaOrder }).ToList(), '-');
                ItemsDanhMuc = new ObservableCollection<NhMstnKeHoachDatHangDanhMucModel>(sortingListQueries.Select(x => x.Key));
                ItemsDanhMuc.ForAll(s => s.CanEditValue = !ItemsDanhMuc.Any(y => y.IID_ParentID == s.Id));
                foreach (var item in ItemsDanhMuc)
                {
                    if (IsDieuChinh)
                    {
                        var idNew = Guid.NewGuid();
                        ItemsDanhMuc.Where(x => x.IID_ParentID == item.Id).ForAll(y => y.IID_ParentID = idNew);
                        item.Id = idNew;
                    }
                    item.SMaDanhMuc = StringUtils.ConvertMaOrder(item.SMaOrder);
                    item.PropertyChanged += HangMuc_PropertyChanged;
                }
                OnPropertyChanged(nameof(ItemsDanhMuc));
            }
            OnPropertyChanged(nameof(SelectedChuongTrinh));
            OnPropertyChanged(nameof(SelectedDonVi));
            OnPropertyChanged(nameof(SelectedKeHoachTongThe));
            OnPropertyChanged(nameof(SSoQuyetDinh));
            OnPropertyChanged(nameof(DNgayQuyetDinh));
            OnPropertyChanged(nameof(SMoTa));
        }

        private void ShowTiGiaNhap()
        {
            IEnumerable<NhDmTiGiaChiTiet> tiGiaChiTietList = _nhDmTiGiaChiTietService.FindByTiGiaId(SelectedTiGia.Id);
            NhDmTiGiaChiTiet tiGiaChiTietObj = tiGiaChiTietList.FirstOrDefault(x => x.SMaTienTeQuyDoi.ToUpper().Equals(LoaiTienTeEnum.TypeCode.VND));
            if (tiGiaChiTietObj != null)
            {
                double? fQuyDoi = tiGiaChiTietObj.FTiGia;
                if (fQuyDoi != null && fQuyDoi.HasValue && fQuyDoi.Value != 0)
                {
                    FTiGiaNhap = fQuyDoi;
                }
                else
                {
                    FTiGiaNhap = null;
                }
            }
            else
            {
                FTiGiaNhap = null;
            }
        }

        public void LoadNhaThau()
        {
            var results = _iNhDmNhaThauService.FindAll();
            if (results == null)
                ItemsNhaThau = new ObservableCollection<ComboboxItem>();
            else
                ItemsNhaThau = new ObservableCollection<ComboboxItem>(results.Select(n => new ComboboxItem()
                {
                    ValueItem = n.Id.ToString(),
                    DisplayItem = n.STenNhaThau,
                    Id = n.Id
                }));
            OnPropertyChanged(nameof(ItemsNhaThau));
        }

        private void UpdateTreeItems()
        {
            if (!ItemsDanhMuc.IsEmpty())
            {
                ItemsDanhMuc.ForAll(s => s.CanEditValue = !ItemsDanhMuc.Any(y => y.IID_ParentID == s.Id));
                ItemsDanhMuc.ForAll(x =>
                {
                    if (x.IID_ParentID.IsNullOrEmpty() || !ItemsDanhMuc.Any(y => y.Id == x.IID_ParentID)) x.IsHangCha = true;
                    if (!x.CanEditValue) x.IsHangCha = true;
                    else if (ItemsDanhMuc.Any(y => y.IID_ParentID == x.IID_ParentID && !y.CanEditValue)) x.IsHangCha = true;
                });
            }
            OnPropertyChanged(nameof(ItemsDanhMuc));
        }

        public void HangMuc_BeginningEditHanlder(DataGridBeginningEditEventArgs e)
        {
            SelectedDanhMuc = (NhMstnKeHoachDatHangDanhMucModel)e.Row.Item;
            if (SelectedDanhMuc.IsHangCha)
            {
                var lstChilren = ItemsDanhMuc.Where(x => x.IID_ParentID == SelectedDanhMuc.Id && x.IsDeleted != true);
                if (lstChilren.Count() != 0)
                {
                    if (e.Column.SortMemberPath.Equals(nameof(NhMstnKeHoachDatHangDanhMucModel.FDonGia_VND)) ||
                        e.Column.SortMemberPath.Equals(nameof(NhMstnKeHoachDatHangDanhMucModel.ISoLuong)))
                    {
                        e.Cancel = true;
                    }
                }
            }
        }

        private void HangMuc_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            foreach (var item in _itemsDanhMuc)
            {
                item.PropertyChanged -= HangMuc_PropertyChanged;
            }
            if (e.PropertyName == nameof(NhMstnKeHoachDatHangDanhMucModel.FDonGia_VND) ||
                e.PropertyName == nameof(NhMstnKeHoachDatHangDanhMucModel.ISoLuong))
            {
                NhMstnKeHoachDatHangDanhMucModel objectSender = (NhMstnKeHoachDatHangDanhMucModel)sender;
                //if (SelectedTiGia != null && SelectedTiGiaChiTiet != null)
                //if (SelectedTiGia != null)
                //{
                    //LstTiGiaChiTiet = _nhDmTiGiaChiTietService.FindByTiGiaId(SelectedTiGia.Id);
                CalculateHangMuc();
                CalculateGiaTriDuocDuyet();
                //}
                OnPropertyChanged(nameof(ItemsDanhMuc));
            }
            foreach (var item in _itemsDanhMuc)
            {
                item.PropertyChanged += HangMuc_PropertyChanged;
            }
        }

        private void CalculateGiaTriDuocDuyet()
        {
            var parents = ItemsDanhMuc.Where(x => x.IID_ParentID == null && !x.IsDeleted);
            FGiaTriUSD = parents.Sum(x => x.FGiaTriUsd);
            FGiaTriVND = parents.Sum(x => x.FGiaTriVnd);
            //FGiaTriEUR = parents.Sum(x => x.FGiaTriEur);
            //FGiaTriNgoaiTeKhac = parents.Sum(x => x.FGiaTriNgoaiTeKhac);
            OnPropertyChanged(nameof(FGiaTriUSD));
            OnPropertyChanged(nameof(FGiaTriVND));
            //OnPropertyChanged(nameof(FGiaTriEUR));
            //OnPropertyChanged(nameof(FGiaTriNgoaiTeKhac));
        }

        private void CalculateHangMuc()
        {
            var parents = ItemsDanhMuc.Where(x => x.IID_ParentID == null);
            if (parents.Count() > 0)
            {
                foreach (var item in parents)
                {
                    CalculateHangMuc(item);
                    var childs = ItemsDanhMuc.Where(x => x.IID_ParentID == item.Id && !x.IsDeleted);
                    if (childs.Count() == 0)
                    {
                        if (item.FDonGia_VND == null || item.ISoLuong == null)
                        {
                            item.FGiaTriUsd = 0;
                            item.FGiaTriVnd = 0;
                            //item.FGiaTriEur = 0;
                            //item.FGiaTriNgoaiTeKhac = 0;
                        }
                        else
                        {
                            item.FGiaTriVnd = item.ISoLuong * item.FDonGia_VND;
                            if (FTiGiaNhap != null && FTiGiaNhap.HasValue && FTiGiaNhap.Value != 0)
                            {
                                item.FGiaTriUsd = (item.FGiaTriVnd != null && item.FGiaTriVnd.HasValue) ? (item.FGiaTriVnd.Value / FTiGiaNhap.Value) : 0;
                            }
                            else
                            {
                                item.FGiaTriUsd = 0;
                            }
                            //item.FGiaTriUsd = _nhDmTiGiaService.CurrencyExchange(LoaiTienTeEnum.TypeCode.VND, LoaiTienTeEnum.TypeCode.USD, _selectedTiGia.SMaTienTeGoc, LstTiGiaChiTiet, item.FGiaTriVnd.Value);
                            //item.FGiaTriEur = _nhDmTiGiaService.CurrencyExchange(LoaiTienTeEnum.TypeCode.VND, LoaiTienTeEnum.TypeCode.EUR, _selectedTiGia.SMaTienTeGoc, LstTiGiaChiTiet, item.FGiaTriVnd.Value);
                            //item.FGiaTriNgoaiTeKhac = _nhDmTiGiaService.CurrencyExchange(LoaiTienTeEnum.TypeCode.VND, SelectedTiGiaChiTiet.SMaTienTeQuyDoi, _selectedTiGia.SMaTienTeGoc, LstTiGiaChiTiet, item.FGiaTriVnd.Value);
                        }
                    }
                }
            }
        }

        private void CalculateHangMuc(NhMstnKeHoachDatHangDanhMucModel parentItem)
        {
            var childs = ItemsDanhMuc.Where(x => x.IID_ParentID == parentItem.Id && !x.IsDeleted);
            if (childs.Count() > 0)
            {
                foreach (var item in childs)
                {
                    CalculateHangMuc(item);
                    if (item.ISoLuong != null && item.FDonGia_VND != null)
                    {
                        item.FGiaTriVnd = item.ISoLuong * item.FDonGia_VND;
                        if (FTiGiaNhap != null && FTiGiaNhap.HasValue && FTiGiaNhap.Value != 0)
                        {
                            item.FGiaTriUsd = (item.FGiaTriVnd != null && item.FGiaTriVnd.HasValue) ? (item.FGiaTriVnd.Value / FTiGiaNhap.Value) : 0;
                        }
                        else
                        {
                            item.FGiaTriUsd = 0;
                        }
                        //item.FGiaTriUsd = _nhDmTiGiaService.CurrencyExchange(LoaiTienTeEnum.TypeCode.VND, LoaiTienTeEnum.TypeCode.USD, _selectedTiGia.SMaTienTeGoc, LstTiGiaChiTiet, item.FGiaTriVnd.Value);
                        //item.FGiaTriEur = _nhDmTiGiaService.CurrencyExchange(LoaiTienTeEnum.TypeCode.VND, LoaiTienTeEnum.TypeCode.EUR, _selectedTiGia.SMaTienTeGoc, LstTiGiaChiTiet, item.FGiaTriVnd.Value);
                        //item.FGiaTriNgoaiTeKhac = _nhDmTiGiaService.CurrencyExchange(LoaiTienTeEnum.TypeCode.VND, SelectedTiGiaChiTiet.SMaTienTeQuyDoi, _selectedTiGia.SMaTienTeGoc, LstTiGiaChiTiet, item.FGiaTriVnd.Value);
                    }
                }
                parentItem.ISoLuong = null;
                parentItem.FDonGia_VND = null;
                parentItem.FGiaTriUsd = childs.Sum(x => x.FGiaTriUsd);
                parentItem.FGiaTriVnd = childs.Sum(x => x.FGiaTriVnd);
                //parentItem.FGiaTriEur = childs.Sum(x => x.FGiaTriEur);
                //parentItem.FGiaTriNgoaiTeKhac = childs.Sum(x => x.FGiaTriNgoaiTeKhac);
            }
        }

        private void OnAddDanhMuc(object obj)
        {
            if (ItemsDanhMuc == null) ItemsDanhMuc = new ObservableCollection<NhMstnKeHoachDatHangDanhMucModel>();

            NhMstnKeHoachDatHangDanhMucModel sourceItem = SelectedDanhMuc;
            NhMstnKeHoachDatHangDanhMucModel targetItem = new NhMstnKeHoachDatHangDanhMucModel();
            bool isParent = (bool)obj;
            int currentRow = -1;
            if (!ItemsDanhMuc.IsEmpty())
            {
                if (sourceItem != null)
                {
                    int indexCurrent = _itemsDanhMuc.IndexOf(sourceItem);
                    if (sourceItem.IID_ParentID == null && isParent)
                    {
                        currentRow = _itemsDanhMuc.Count() - 1;
                    }
                    else
                    {
                        currentRow = indexCurrent + CountTreeChildItems(sourceItem);
                        if (sourceItem.IID_ParentID != null && isParent)
                        {
                            currentRow += CountTreeChildItems(sourceItem, true);
                        }
                    }
                }
                else
                {
                    // Thêm vào cuối danh sách
                    currentRow = ItemsDanhMuc.Count() - 1;
                }
            }

            if (sourceItem != null)
            {
                targetItem.IID_ParentID = isParent ? sourceItem.IID_ParentID : sourceItem.Id;
                targetItem.IID_KeHoachDatHang = sourceItem.IID_KeHoachDatHang;
            }
            else
                targetItem.IID_KeHoachDatHang = Model.Id;
            targetItem.IsAdded = true;
            targetItem.Id = Guid.NewGuid();
            targetItem.PropertyChanged += HangMuc_PropertyChanged;
            ItemsDanhMuc.Insert(currentRow + 1, targetItem);

            OrderItems(targetItem.IID_ParentID);
            UpdateTreeItems();
            OnPropertyChanged(nameof(ItemsDanhMuc));
        }

        private void OnDeleteDanhMuc()
        {
            if (SelectedDanhMuc == null) return;
            DeleteTreeItems(SelectedDanhMuc, !SelectedDanhMuc.IsDeleted);
            OrderItems();
            //if (SelectedTiGia != null && SelectedTiGiaChiTiet != null)
            //if (SelectedTiGia != null)
            //{
                //LstTiGiaChiTiet = _nhDmTiGiaChiTietService.FindByTiGiaId(SelectedTiGia.Id);
            CalculateHangMuc();
            CalculateGiaTriDuocDuyet();
            //}
            OnPropertyChanged(nameof(ItemsDanhMuc));
        }

        private void DeleteTreeItems(NhMstnKeHoachDatHangDanhMucModel currentItem, bool status)
        {
            if (currentItem != null)
            {
                var items = ItemsDanhMuc;
                currentItem.IsDeleted = status;
                var childs = items.Where(x => x.IID_ParentID == currentItem.Id);
                if (!childs.IsEmpty())
                {
                    foreach (var item in childs)
                    {
                        DeleteTreeItems(item, status);
                    }
                }
            }
        }

        private void OnSaveData(object obj)
        {
            if (!ValidateForm()) return;
            if (!ValidateDanhMuc()) return;
            var objKhDatHang = SaveKhDatHangData();
            if (!IsDieuChinh)
            {
                SaveDanhMuc(objKhDatHang.Id);
            }
            MessageBoxHelper.Info(Resources.MsgSaveDone);
            Window window = obj as Window;
            SavedAction?.Invoke(Model);
            window.Close();
        }
        #endregion

        #region Helper
        private bool ValidateForm()
        {
            List<string> lstError = new List<string>();

            //if (SelectedTiGia == null)
            //    lstError.Add(string.Format(Resources.MsgCheckTiGiaNgoaiHoi));
            if (SelectedDonVi == null)
                lstError.Add(string.Format(Resources.MsgCheckDonVi));
            if (string.IsNullOrWhiteSpace(SSoQuyetDinh))
                lstError.Add(string.Format(Resources.MsgCheckSoQD));
            if (!string.IsNullOrWhiteSpace(SSoQuyetDinh) && _service.CheckDuplicateSoQD(SSoQuyetDinh, Model.Id))
            {
                lstError.Add(Resources.MsgTrungSoQD);
            }
            if (DNgayQuyetDinh == null || !DNgayQuyetDinh.HasValue)
            {
                lstError.Add(string.Format(Resources.ErrorNgayQuyetDinhEmpty));
            }
            if (SelectedChuongTrinh == null)
            {
                lstError.Add(string.Format(Resources.MsgCheckChuongTrinh));
            }
            //if (SelectedTiGiaChiTiet == null)
            //{
            //    lstError.Add(string.Format(Resources.MsgCheckMaNgoaiTeNgoaiHoi));
            //}

            if (lstError.Count != 0)
            {
                MessageBoxHelper.Error(string.Join("\n", lstError));
                return false;
            }
            return true;
        }

        private void LoadDonViQuanLy()
        {
            try
            {
                var data = _dvService.FindByNamLamViec(_sessionService.Current.YearOfWork);
                _itemsDonVi = _mapper.Map<ObservableCollection<DonViModel>>(data);
                OnPropertyChanged(nameof(ItemsDonVi));
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        private void LoadKeHoachTongThe()
        {
            var lstKeHoachTongThe = _iNhKhTongTheService.FindByDonViId(SelectedDonVi.Id).ToList();
            if (lstKeHoachTongThe.Any())
            {
                var result = lstKeHoachTongThe.Select(x =>
                {
                    ComboboxItem cb = new ComboboxItem();
                    if (x.INamKeHoach.HasValue)
                    {
                        cb.DisplayItem = "KHTT " + x.INamKeHoach.Value + "- Số KH: " + x.SSoKeHoachBqp;
                        cb.ValueItem = x.Id.ToString();
                        cb.Id = x.Id;
                    }
                    else
                    {
                        cb.DisplayItem = "KHTT " + x.IGiaiDoanTu_BQP.GetValueOrDefault() + "-" + x.IGiaiDoanDen_BQP.GetValueOrDefault() + " - Số KH: " + x.SSoKeHoachBqp;
                        cb.ValueItem = x.Id.ToString();
                        cb.Id = x.Id;
                    }
                    return cb;
                }).ToList();
                _itemsKeHoachTongThe = new ObservableCollection<ComboboxItem>(result);
            }
            else
            {
                _itemsKeHoachTongThe = new ObservableCollection<ComboboxItem>();
            }
            OnPropertyChanged(nameof(ItemsKeHoachTongThe));
        }

        private void LoadChuongTrinh()
        {
            ItemsChuongTrinh = new ObservableCollection<NhDmNhiemVuChiModel>();
            if (_selectedKeHoachTongThe != null && _selectedDonVi != null)
            {
                var data = _nhDmNhiemVuChiService.FindByKhTongTheIdAndDonViId(SelectedKeHoachTongThe.Id, SelectedDonVi.Id);
                _itemsChuongTrinh = _mapper.Map<ObservableCollection<NhDmNhiemVuChiModel>>(data);
            }
            OnPropertyChanged(nameof(ItemsChuongTrinh));
        }

        private NhMSTNKeHoachDatHang SaveKhDatHangData()
        {
            NhMSTNKeHoachDatHang objKhDatHang = new NhMSTNKeHoachDatHang();
            // Nếu có id thì là sửa hoặc điều chỉnh
            if (Model.Id != Guid.Empty)
            {
                if (IsDieuChinh)
                {
                    //Nếu là điều chỉnh thì update bản ghi gốc
                    var objOld = _service.Find(Model.Id);
                    objOld.BIsActive = false;
                    _service.Update(objOld);

                    // sau đó tạo mới 1 obj và cập nhật thêm một số trường trường active, lần điều chỉnh, gốc, parent
                    objKhDatHang.DNgayTao = DateTime.Now;
                    objKhDatHang.SNguoiTao = _sessionService.Current.Principal;
                    objKhDatHang.BIsGoc = false;
                    objKhDatHang.ILanDieuChinh = objOld.ILanDieuChinh + 1;
                    objKhDatHang.IIdParentId = objOld.Id;
                }
                else
                {
                    // Nếu là cập nhật thì cập nhật ngày sửa
                    objKhDatHang = _service.Find(Model.Id);
                    objKhDatHang.DNgaySua = DateTime.Now;
                    objKhDatHang.SNguoiSua = _sessionService.Current.Principal;
                }
            }
            else
            {
                // Nếu ko có id thì là thêm mới
                objKhDatHang.DNgayTao = DateTime.Now;
                objKhDatHang.SNguoiTao = _sessionService.Current.Principal;
                objKhDatHang.IIdParentId = null;
                objKhDatHang.BIsGoc = true;
                objKhDatHang.ILanDieuChinh = 0;
            }

            // Lấy giá trị đơn vị
            if (SelectedDonVi != null)
            {
                objKhDatHang.IIdDonViQuanLyId = SelectedDonVi.Id;
                objKhDatHang.IIdMaDonViQuanLy = SelectedDonVi.IIDMaDonVi;
            }

            // Lấy giá trị chương trình
            if (SelectedChuongTrinh != null)
                objKhDatHang.IIdKHTTNhiemVuChiId = SelectedChuongTrinh.IIdKHTTNhiemVuChiId;

            // Lấy tỉ giá
            if (SelectedTiGia != null)
            {
                objKhDatHang.IIdTiGiaId = SelectedTiGia.Id;
            }

            // Lấy giá trị mã ngoại tệ
            //objKhDatHang.SMaNgoaiTeKhac = SelectedTiGiaChiTiet.SMaTienTeQuyDoi;
            objKhDatHang.SSoQuyetDinh = SSoQuyetDinh;
            objKhDatHang.DNgayQuyetDinh = DNgayQuyetDinh;
            objKhDatHang.SMoTa = SMoTa;
            objKhDatHang.FGiaTriVnd = FGiaTriVND;
            objKhDatHang.FGiaTriUsd = FGiaTriUSD;
            //objKhDatHang.FGiaTriEur = FGiaTriEUR;
            //objKhDatHang.FGiaTriNgoaiTeKhac = FGiaTriNgoaiTeKhac;
            objKhDatHang.FTiGiaNhap = FTiGiaNhap;
            objKhDatHang.BIsActive = true;

            // Nếu obj vừa khởi tạo đã có Id thì là update, không có id thì là thêm mới hoặc điều chỉnh
            if (objKhDatHang.Id.IsNullOrEmpty())
            {
                _service.Insert(objKhDatHang);
                // Nếu không có id thì llà điều chỉnh hoặc thêm mới => insert

                if (IsDieuChinh)
                {
                    // Insert list gói thầu điều chỉnh
                    List<NhMSTNKeHoachDatHangDanhMuc> lstDanhMucDieuChinh = new List<NhMSTNKeHoachDatHangDanhMuc>();
                    foreach (var item in ItemsDanhMuc.Where(x => !x.IsDeleted))
                    {
                        item.IID_KeHoachDatHang = objKhDatHang.Id;
                        lstDanhMucDieuChinh.Add(SetDataDanhMuc(item));
                    }
                    if (lstDanhMucDieuChinh.Count != 0)
                        _danhMucService.AddRange(lstDanhMucDieuChinh);
                }
            }
            else
            {
                // Nếu có id thì là update
                _service.Update(objKhDatHang);
            }

            return objKhDatHang;
        }

        private bool SaveDanhMuc(Guid iIdKhDatHangId)
        {
            List<NhMSTNKeHoachDatHangDanhMuc> lstDanhMuc = new List<NhMSTNKeHoachDatHangDanhMuc>();
            List<NhMSTNKeHoachDatHangDanhMuc> lstDanhMucAdd = new List<NhMSTNKeHoachDatHangDanhMuc>();
            List<NhMSTNKeHoachDatHangDanhMuc> lstDanhMucUpdate = new List<NhMSTNKeHoachDatHangDanhMuc>();
            List<NhMSTNKeHoachDatHangDanhMuc> lstDanhMucDelete = new List<NhMSTNKeHoachDatHangDanhMuc>();
            List<Guid> lstIdDeleteDetail = ItemsDanhMuc.Where(n => n.IsDeleted).Select(n => n.Id).ToList();
            foreach (var item in ItemsDanhMuc)
            {
                if (item.IsAdded && !item.IsDeleted)
                    lstDanhMuc.Add(SetDataDanhMuc(item));
                else if (!item.IsAdded && !item.IsDeleted)
                {
                    item.IsModified = true;
                    lstDanhMuc.Add(SetDataDanhMuc(item));
                }
                else if (!item.IsAdded && item.IsDeleted)
                    lstDanhMuc.Add(SetDataDanhMuc(item));
            }
            _danhMucService.AddOrUpdate(iIdKhDatHangId, lstDanhMuc);
            return true;
        }

        private bool ValidateDanhMuc()
        {
            List<string> messErrors = new List<string>();
            if (!ItemsDanhMuc.Any(n => !n.IsDeleted)) messErrors.Add(string.Format(Resources.MsgErrorDataEmpty, "danh mục"));

            if (messErrors.Count > 0)
            {
                MessageBoxHelper.Error(string.Join("\n", messErrors));
                return false;
            }
            List<string> fieldNames = new List<string>();
            foreach (var item in ItemsDanhMuc.Where(n => !n.IsDeleted))
            {
                fieldNames.Clear();
                if (string.IsNullOrWhiteSpace(item.STenDanhMuc))
                {
                    fieldNames.Add("Tên danh mục");
                }
                if ((item.ISoLuong == null || !item.ISoLuong.HasValue || item.ISoLuong.Value == 0) && !item.IsHangCha)
                {
                    fieldNames.Add("Số lượng");
                }
                if ((item.FDonGia_VND == null || !item.FDonGia_VND.HasValue || item.FDonGia_VND.Value == 0) && !item.IsHangCha)
                {
                    fieldNames.Add("Đơn giá (VND)");
                }
                if (fieldNames.Count() > 0)
                {
                    messErrors.Add("Dòng [" + item.SMaDanhMuc + "]: " + string.Join(", ", fieldNames) + " chưa được nhập!");
                    break;
                }
            }
            if (messErrors.Count != 0)
            {
                MessageBoxHelper.Error(string.Join("\n", messErrors));
                return false;
            }
            return true;
        }

        private void LoadTiGia()
        {
            var drpData = _nhDmTiGiaService.FindAll();
            ItemsTiGia = _mapper.Map<ObservableCollection<NhDmTiGiaModel>>(drpData);
            //if (Model.IIdTiGiaID.HasValue)
            //    SelectedTiGia = ItemsTiGia.FirstOrDefault(n => n.Id == Model.IIdTiGiaID.Value);
        }

        private void LoadTiGiaChiTiet()
        {
            _itemsTiGiaChiTiet = new ObservableCollection<NhDmTiGiaChiTietModel>();
            List<string> lstTiGiaDef = new List<string>() { "USD", "VND", "EUR" };
            if (SelectedTiGia != null)
            {
                // Lấy danh sách tỉ giá chi tiết
                IEnumerable<NhDmTiGiaChiTiet> listTiGiaChiTiet = _nhDmTiGiaChiTietService.FindByTiGiaId(_selectedTiGia.Id);

                // Lọc ra danh sách tỉ giá ngoại tệ khác
                ItemsTiGiaChiTiet = _mapper.Map<ObservableCollection<NhDmTiGiaChiTietModel>>(listTiGiaChiTiet.Where(x => !lstTiGiaDef.Contains(x.SMaTienTeQuyDoi.ToUpper())));
                // Lọc ra danh sách tỉ giá chính (USD, VND, EUR)
                //ItemsTiGiaChiTietChinh = _mapper.Map<ObservableCollection<NhDmTiGiaChiTietModel>>(listTiGiaChiTiet.Where(x => lstTiGiaDef.Contains(x.SMaTienTeQuyDoi)));
            }
            else
            {
                ItemsTiGiaChiTiet = new ObservableCollection<NhDmTiGiaChiTietModel>();
                //ItemsTiGiaChiTietChinh = new ObservableCollection<NhDmTiGiaChiTietModel>();
                SelectedTiGiaChiTiet = null;
            }
        }

        private void SetLabelTiGiaChiTiet()
        {
            if (ItemsTiGiaChiTiet != null && ItemsTiGiaChiTiet.Count > 0 && ItemsTiGiaChiTietChinh != null && _selectedTiGia != null)
            {
                var label = new StringBuilder();
                // Load thông tin tỉ giá chính (USD, VND, EUR)
                foreach (var tg in ItemsTiGiaChiTietChinh)
                {
                    label.Append("1 " + _selectedTiGia.SMaTienTeGoc + " = " + tg.FTiGia + " " + tg.SMaTienTeQuyDoi + ". ");
                }

                // Load thêm các thông tin tỉ giá phụ (Ngoại tệ khác)
                if (SelectedTiGiaChiTiet != null)
                {
                    var tgNgoaiTeKhac = ItemsTiGiaChiTiet.FirstOrDefault(x => x.Id == SelectedTiGiaChiTiet.Id);
                    label.Append("1 " + _selectedTiGia.SMaTienTeGoc + " = " + tgNgoaiTeKhac.FTiGia + " " + tgNgoaiTeKhac.SMaTienTeQuyDoi + ". ");
                }

                Model.SMaNgoaiTeKhac = label.ToString();
            }
            else
            {
                Model.SMaNgoaiTeKhac = null;
            }
        }

        private NhMSTNKeHoachDatHangDanhMuc SetDataDanhMuc(NhMstnKeHoachDatHangDanhMucModel data)
        {
            NhMSTNKeHoachDatHangDanhMuc item = new NhMSTNKeHoachDatHangDanhMuc();
            item.Id = data.Id;
            item.STenDanhMuc = data.STenDanhMuc;
            item.SDonViTinh = data.SDonViTinh;
            item.SGhiChu = data.SGhiChu;
            item.SMaOrder = data.SMaOrder;
            item.ISoLuong = data.ISoLuong;
            item.IID_KeHoachDatHang = data.IID_KeHoachDatHang;
            item.IID_NhaThauID = data.IID_NhaThauID;
            item.IID_ParentID = data.IID_ParentID;
            item.FDonGia_VND = data.FDonGia_VND;
            //item.FGiaTriEur = data.FGiaTriEur;
            //item.FGiaTriNgoaiTeKhac = data.FGiaTriNgoaiTeKhac;
            item.FGiaTriUsd = data.FGiaTriUsd;
            item.FGiaTriVnd = data.FGiaTriVnd;
            item.IsAdded = data.IsAdded;
            item.IsModified = data.IsModified;
            item.IsDeleted = data.IsDeleted;
            return item;
        }

        private int CountTreeChildItems(NhMstnKeHoachDatHangDanhMucModel currentItem, bool isCountSiblings = false)
        {
            var items = _itemsDanhMuc;
            int count = 0;
            if (!isCountSiblings)
            {
                var childs = ItemsDanhMuc.Where(x => x.IID_ParentID != null && x.IID_ParentID == currentItem.Id);
                if (!childs.IsEmpty())
                {
                    count += childs.Count();
                    foreach (var item in childs)
                    {
                        count += CountTreeChildItems(item);
                    }
                }
            }
            else
            {
                var itemSiblings = ItemsDanhMuc.Where(x => x.IID_ParentID != null
                                            && x.Id != currentItem.Id
                                            && items.IndexOf(currentItem) < items.IndexOf(x)
                                            && x.IID_ParentID == currentItem.IID_ParentID);
                if (!itemSiblings.IsEmpty())
                {
                    count += itemSiblings.Count();
                    foreach (var item in itemSiblings)
                    {
                        count += CountTreeChildItems(item);
                    }
                }
            }
            return count;
        }

        private void OrderItems(Guid? parentId = null)
        {
            var childs = ItemsDanhMuc.Where(x => x.IID_ParentID == parentId && !x.IsDeleted);
            if (!childs.IsEmpty())
            {
                var parent = ItemsDanhMuc.FirstOrDefault(x => x.Id == parentId);
                int index = 1;
                foreach (var child in childs)
                {
                    if (parent != null)
                    {
                        child.SMaOrder = string.Format("{0}-{1}", parent.SMaOrder, index.ToString("D2"));
                    }
                    else
                    {
                        child.SMaOrder = index.ToString("D2");
                    }
                    child.SMaDanhMuc = StringUtils.ConvertMaOrder(child.SMaOrder);
                    OrderItems(child.Id);
                    index++;
                }
            }
        }

        private bool CheckDelete()
        {
            var itemHDHangMuc = _iNhHDHangMucService.FindAll(x => x.IIdKeHoachDatHangDanhMucId == SelectedDanhMuc.Id).ToList();
            return itemHDHangMuc == null || itemHDHangMuc.Count == 0;
        }
        #endregion
    }
}
