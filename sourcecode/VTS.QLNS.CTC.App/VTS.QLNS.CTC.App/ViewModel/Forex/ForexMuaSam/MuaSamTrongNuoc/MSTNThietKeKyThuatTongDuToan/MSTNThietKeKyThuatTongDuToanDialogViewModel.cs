using AutoMapper;
using MaterialDesignThemes.Wpf;
using log4net;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Forms;
using VTS.QLNS.CTC.App.Command;
using VTS.QLNS.CTC.App.Helper;
using VTS.QLNS.CTC.App.Model;
using VTS.QLNS.CTC.App.Model.Control;
using VTS.QLNS.CTC.App.Properties;
using VTS.QLNS.CTC.App.Service;
using VTS.QLNS.CTC.App.Service.Impl;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Service;
using VTS.QLNS.CTC.Utility;
using VTS.QLNS.CTC.Utility.Enum;
using VTS.QLNS.CTC.App.ViewModel;
using VTS.QLNS.CTC.App.View.Forex.ForexMuaSam.MuaSamTrongNuoc.MSTNThietKeKyThuatTongDuToan;
using Microsoft.EntityFrameworkCore.Query.ExpressionTranslators.Internal;

namespace VTS.QLNS.CTC.App.ViewModel.Forex.ForexMuaSam.MuaSamTrongNuoc.MSTNThietKeKyThuatTongDuToan
{
    public class MSTNThietKeKyThuatTongDuToanDialogViewModel : DialogAttachmentViewModelBase<NhDaDuToanModel>
    {
        private readonly ILog _logger;
        private readonly ISessionService _sessionService;
        private readonly INsDonViService _nsDonViService;
        private readonly IDmChuDauTuService _chuDauTuService;
        private readonly INhDaDuAnService _nhDaDuAnService;
        private readonly INhDmTiGiaService _nhDmTiGiaService;
        private readonly INhDmTiGiaChiTietService _nhDmTiGiaChiTietService;
        private readonly INsNguonNganSachService _nsNguonNganSachService;
        private readonly INhDmLoaiCongTrinhService _nhDmLoaiCongTrinhService;
        private readonly INhDmChiPhiService _nhDmChiPhiService;
        private readonly INhDaQdDauTuNguonVonService _nhDaQdDauTuNguonVonService;
        private readonly INhDaQdDauTuChiPhiService _nhDaQdDauTuChiPhiService;
        private readonly INhDaQdDauTuHangMucService _nhDaQdDauTuHangMucService;
        private readonly INhDaQdDauTuService _nhDaQdDauTuService;
        private readonly INhDaDuToanNguonVonService _nhDaDuToanNguonVonService;
        private readonly INhDaDuToanChiPhiService _nhDaDuToanChiPhiService;
        private readonly INhDaDuToanHangMucService _nhDaDuToanHangMucService;
        private readonly INhDaDuToanService _service;
        private SessionInfo _sessionInfo;
        private List<NhDaQdDauTuHangMucModel> _itemsQdDauTuHangMuc;
        private readonly INhKhTongTheService _nhKhTongTheService;
        private readonly INhKhTongTheNhiemVuChiService _nhKhTongTheNhiemVuChiService;

        public override string FuncCode => NSFunctionCode.INVESTMENT_IMPLEMENTATION_TKTC_VA_TONG_DU_TOAN_DIALOG;
        public override string Name => "Thiết kế kỹ thuật và tổng dự toán";
        public override string Title { get; set; }
        public override string Description { get; set; }
        public override Type ContentType => typeof(ThietKeKyThuatTongDuToanDialog);
        public override AttachmentEnum.Type ModuleType => AttachmentEnum.Type.NH_TKKT_TONGDUTOAN;
        public bool IsDetail { get; set; }
        public int ILoai { get; set; }
        public bool IsEditable => Model == null || Model.Id.IsNullOrEmpty();
        public bool IsQuyetDinh => !IsDetail;
        public bool IsShowDuAn { get; set; }
        public bool IsDieuChinh { get; set; }
        public bool IsEnableQuyetDinhDauTuPheDuyet => !IsShowDuAn;
        public bool IsAddSpend { get; set; }
        public bool IsAddSpendRowChild { get; set; }
        private double? _tongGiaTriPheDuyetDuAn;
        public double? TongGiaTriPheDuyetDuAn
        {
            get => _tongGiaTriPheDuyetDuAn;
            set => SetProperty(ref _tongGiaTriPheDuyetDuAn, value);
        }

        private ObservableCollection<DonViModel> _itemsDonVi;
        public ObservableCollection<DonViModel> ItemsDonVi
        {
            get => _itemsDonVi;
            set => SetProperty(ref _itemsDonVi, value);
        }
        private bool _isVisibleTiGiaNhap;
        public bool IsVisibleTiGiaNhap
        {
            get => _isVisibleTiGiaNhap;
            set => SetProperty(ref _isVisibleTiGiaNhap, value);
        }
        private DonViModel _selectedDonVi;
        public DonViModel SelectedDonVi
        {
            get => _selectedDonVi;
            set
            {
                if (SetProperty(ref _selectedDonVi, value))
                {
                    LoadDuAn();
                    LoadKeHoachTongThe();
                    LoadNhiemVuChi();
                }
            }
        }
        private ObservableCollection<NhDaDuToanModel> _itemsLoaiDuToan;
        public ObservableCollection<NhDaDuToanModel> ItemsLoaiDuToan
        {
            get => _itemsLoaiDuToan;
            set => SetProperty(ref _itemsLoaiDuToan, value);
        }

        private NhDaDuToanModel _selectedLoaiDuToan;
        public NhDaDuToanModel SelectedLoaiDuToan
        {
            get => _selectedLoaiDuToan;
            set
            {
                if (SetProperty(ref _selectedLoaiDuToan, value))
                {
                    LoadTitlePheDuyet();
                }
            }
        }

        private double? _fGiaTriVnd;
        public double? FGiaTriVnd
        {
            get => _fGiaTriVnd;
            set => SetProperty(ref _fGiaTriVnd, value);
        }

        private double? _fGiaTriUsd;
        public double? FGiaTriUsd
        {
            get => _fGiaTriUsd;
            set => SetProperty(ref _fGiaTriUsd, value);
        }
        
        private double? _fSumGiaTriChiPhiVnd;
        public double? FSumGiaTriChiPhiVnd
        {
            get => _fSumGiaTriChiPhiVnd;
            set => SetProperty(ref _fSumGiaTriChiPhiVnd, value);
        }

        private double? _fSumGiaTriChiPhiUsd;
        public double? FSumGiaTriChiPhiUsd
        {
            get => _fSumGiaTriChiPhiUsd;
            set => SetProperty(ref _fSumGiaTriChiPhiUsd, value);
        }
        private ObservableCollection<NhDmChiPhiModel> _itemsDMChiPhi;
        public ObservableCollection<NhDmChiPhiModel> ItemsDMChiPhi
        {
            get => _itemsDMChiPhi;
            set => SetProperty(ref _itemsDMChiPhi, value);
        }


        //KhaiPD update
        private string _titlePheDuyet;
        public string TitlePheDuyet
        {
            get => _titlePheDuyet;
            set => SetProperty(ref _titlePheDuyet, value);
        }

        private string LoadTitlePheDuyet()
        {
            _titlePheDuyet = "GIÁ TRỊ PHÊ DUYỆT";
            if(SelectedLoaiDuToan != null && SelectedLoaiDuToan.IdLoaiDuToan == 1)
            {
                _titlePheDuyet = "GIÁ TRỊ DỰ TOÁN MUA SẮM PHÊ DUYỆT";
            }
            if (SelectedLoaiDuToan != null && SelectedLoaiDuToan.IdLoaiDuToan == 2)
            {
                _titlePheDuyet = "GIÁ TRỊ DỰ TOÁN ĐẶT HÀNG PHÊ DUYỆT";
            }
            OnPropertyChanged(nameof(TitlePheDuyet));
            return _titlePheDuyet;
        }

        //private void SetTitle()
        //{
        //    _itemsLoaiDuToan = new ObservableCollection<NhDaDuToanModel>();
        //    _itemsLoaiDuToan.Add(new NhDaDuToanModel() { IdLoaiDuToan = 1, Title ="GIÁ TRỊ DTCTN PHÊ DUYỆT" });
        //    _itemsLoaiDuToan.Add(new NhDaDuToanModel() { IdLoaiDuToan = 2, Title ="GIÁ TRỊ DCDH PHÊ DUYỆT" });
        //    OnPropertyChanged(nameof(ItemsLoaiDuToan));
        //}

        private ObservableCollection<DmChuDauTuModel> _itemsChuDauTu;
        public ObservableCollection<DmChuDauTuModel> ItemsChuDauTu
        {
            get => _itemsChuDauTu;
            set => SetProperty(ref _itemsChuDauTu, value);
        }

        private DmChuDauTuModel _selectedChuDauTu;
        public DmChuDauTuModel SelectedChuDauTu
        {
            get => _selectedChuDauTu;
            set => SetProperty(ref _selectedChuDauTu, value);
        }

        private ObservableCollection<NhDaDuAnModel> _itemsDuAn;
        public ObservableCollection<NhDaDuAnModel> ItemsDuAn
        {
            get => _itemsDuAn;
            set => SetProperty(ref _itemsDuAn, value);
        }

        private NhDaDuAnModel _selectedDuAn;
        public NhDaDuAnModel SelectedDuAn
        {
            get => _selectedDuAn;
            set
            {
                if (SetProperty(ref _selectedDuAn, value))
                {
                    LoadThongTinDuAn();
                }
            }
        }

        private ObservableCollection<NhDmTiGiaModel> _itemsTiGia;
        public ObservableCollection<NhDmTiGiaModel> ItemsTiGia
        {
            get => _itemsTiGia;
            set => SetProperty(ref _itemsTiGia, value);
        }

        private double? _fTiGiaNhap;
        public double? FTiGiaNhap
        {
            get => _fTiGiaNhap;
            set
            {
                if (SetProperty(ref _fTiGiaNhap, value))
                {
                    if (ItemsDuToanNguonVon != null)
                    {
                        CalculateNguonVon();
                        CalculateChiPhi();
                    }
                }
            }
        }
        private NhDmTiGiaModel _selectedTiGia;
        public NhDmTiGiaModel SelectedTiGia
        {
            get => _selectedTiGia;
            set
            {
                if (SetProperty(ref _selectedTiGia, value) && value != null)
                {
                    //LoadTiGiaChiTiet();
                    IsVisibleTiGiaNhap = true;
                    ShowTiGiaNhap();
                }
            }
        }

        private void ShowTiGiaNhap()
        {
            IEnumerable<NhDmTiGiaChiTiet> tiGiaChiTietList = _nhDmTiGiaChiTietService.FindByTiGiaId(SelectedTiGia.Id);
            NhDmTiGiaChiTiet tiGiaChiTietObj = tiGiaChiTietList.FirstOrDefault(x => x.SMaTienTeQuyDoi.ToUpper().Equals(LoaiTienTeEnum.TypeCode.VND));
            if (tiGiaChiTietObj != null)
            {
                double? fQuyDoi = tiGiaChiTietObj.FTiGia;
                if (fQuyDoi != null && fQuyDoi.HasValue)
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

        private ObservableCollection<NhDmTiGiaChiTietModel> _itemsTiGiaChiTiet;
        public ObservableCollection<NhDmTiGiaChiTietModel> ItemsTiGiaChiTiet
        {
            get => _itemsTiGiaChiTiet;
            set => SetProperty(ref _itemsTiGiaChiTiet, value);
        }

        private NhDmTiGiaChiTietModel _selectedTiGiaChiTiet;
        public NhDmTiGiaChiTietModel SelectedTiGiaChiTiet
        {
            get => _selectedTiGiaChiTiet;
            set => SetProperty(ref _selectedTiGiaChiTiet, value);
        }

        private ObservableCollection<ComboboxItem> _itemsLoaiCongTrinh;
        public ObservableCollection<ComboboxItem> ItemsLoaiCongTrinh
        {
            get => _itemsLoaiCongTrinh;
            set => SetProperty(ref _itemsLoaiCongTrinh, value);
        }

        //private ObservableCollection<ComboboxItem> _itemsNguonVon;
        //public ObservableCollection<ComboboxItem> ItemsNguonVon
        //{
        //    get => _itemsNguonVon;
        //    set => SetProperty(ref _itemsNguonVon, value);
        //}

        private ObservableCollection<NguonNganSachModel> _itemsNguonVon;
        public ObservableCollection<NguonNganSachModel> ItemsNguonVon
        {
            get => _itemsNguonVon;
            set => SetProperty(ref _itemsNguonVon, value);
        }

        private ObservableCollection<NhDaDuToanNguonVonModel> _itemsDuToanNguonVon;
        public ObservableCollection<NhDaDuToanNguonVonModel> ItemsDuToanNguonVon
        {
            get => _itemsDuToanNguonVon;
            set => SetProperty(ref _itemsDuToanNguonVon, value);
        }

        private NhDaDuToanNguonVonModel _selectedDuToanNguonVon;
        public NhDaDuToanNguonVonModel SelectedDuToanNguonVon
        {
            get => _selectedDuToanNguonVon;
            set => SetProperty(ref _selectedDuToanNguonVon, value);
        }

        private ObservableCollection<NhDaDuToanChiPhiModel> _itemsDuToanChiPhi = new ObservableCollection<NhDaDuToanChiPhiModel>();
        public ObservableCollection<NhDaDuToanChiPhiModel> ItemsDuToanChiPhi
        {
            get => _itemsDuToanChiPhi;
            set => SetProperty(ref _itemsDuToanChiPhi, value);
        }

        private NhDaDuToanChiPhiModel _selectedDuToanChiPhi;
        public NhDaDuToanChiPhiModel SelectedDuToanChiPhi
        {
            get => _selectedDuToanChiPhi;
            set => SetProperty(ref _selectedDuToanChiPhi, value);
        }

        private NhDaDuToanNguonVonModel _selectedNguonVon;
        public NhDaDuToanNguonVonModel SelectedNguonVon
        {
            get => _selectedNguonVon;
            set => SetProperty(ref _selectedNguonVon, value);
        }

        private ObservableCollection<NhKhTongTheModel> _itemsNhKhTongThe;
        public ObservableCollection<NhKhTongTheModel> ItemsNhKhTongThe
        {
            get => _itemsNhKhTongThe;
            set => SetProperty(ref _itemsNhKhTongThe, value);
        }

        private NhKhTongTheModel _selectedNhKhTongThe;
        public NhKhTongTheModel SelectedNhKhTongThe
        {
            get => _selectedNhKhTongThe;
            set
            {
                SetProperty(ref _selectedNhKhTongThe, value);
                if (value != null)
                {
                    LoadNhiemVuChi();
                }
            }
        }

        private ObservableCollection<NhKhTongTheNhiemVuChiModel> _itemsKhTongTheNhiemVuChi;
        public ObservableCollection<NhKhTongTheNhiemVuChiModel> ItemsKhTongTheNhiemVuChi
        {
            get => _itemsKhTongTheNhiemVuChi;
            set => SetProperty(ref _itemsKhTongTheNhiemVuChi, value);
        }

        private NhKhTongTheNhiemVuChiModel _selectedKhTongTheNhiemVuChi;
        public NhKhTongTheNhiemVuChiModel SelectedKhTongTheNhiemVuChi
        {
            get => _selectedKhTongTheNhiemVuChi;
            set => SetProperty(ref _selectedKhTongTheNhiemVuChi, value);
        }

        public bool? IsAllNGuonVonItemSelected
        {
            get
            {
                if (ItemsDuToanNguonVon != null)
                {
                    var selected = ItemsDuToanNguonVon.Select(x => x.IsSelected).Distinct().ToList();
                    return selected.Count == 1 ? selected.Single() : (bool?)null;
                }
                return false;
            }
            set
            {
                if (value.HasValue)
                {
                    SelectAll(value.Value, ItemsDuToanNguonVon);
                    OnPropertyChanged();
                }
            }
        }

        private List<NhDaDuToanChiPhiModel> ListSave = new List<NhDaDuToanChiPhiModel>();

        public MSTNThietKeKyThuatTongDuToanItemDialogViewModel DuToanHangMucDialogViewModel { get; }

        public RelayCommand AddDuToanChiPhiCommand { get; }
        public RelayCommand AddDuToanNguonVonCommand { get; }
        public RelayCommand PhuLucHangMucCommand { get; }
        public RelayCommand DeleteDuToanChiPhiCommand { get; }
        public RelayCommand DeleteDuToanNguonVonCommand { get; }

        public MSTNThietKeKyThuatTongDuToanDialogViewModel(
            ILog logger,
            IMapper mapper,
            ISessionService sessionService,
            INsDonViService nsDonViService,
            IDmChuDauTuService chuDauTuService,
            INhDmTiGiaService nhDmTiGiaService,
            INhDmTiGiaChiTietService nhDmTiGiaChiTietService,
            INsNguonNganSachService nsNguonNganSachService,
            INhDmLoaiCongTrinhService nhDmLoaiCongTrinhService,
            INhDmChiPhiService nhDmChiPhiService,
            INhDaDuAnService nhDaDuAnService,
            INhDaQdDauTuHangMucService nhDaQdDauTuHangMucService,
            INhDaQdDauTuNguonVonService nhDaQdDauTuNguonVonService,
            INhDaQdDauTuChiPhiService nhDaQdDauTuChiPhiService,
            INhDaQdDauTuService nhDaQdDauTuService,
            INhDaDuToanNguonVonService nhDaDuToanNguonVonService,
            INhDaDuToanChiPhiService nhDaDuToanChiPhiService,
            INhDaDuToanHangMucService nhDaDuToanHangMucService,
            INhDaDuToanService service,
            IStorageServiceFactory storageServiceFactory,
            IAttachmentService attachService,
            MSTNThietKeKyThuatTongDuToanItemDialogViewModel duToanHangMucDialogViewModel,
            INhKhTongTheService nhKhTongTheService,
            INhKhTongTheNhiemVuChiService nhKhTongTheNhiemVuChiService)
            : base(mapper, storageServiceFactory, attachService)
        {
            _logger = logger;
            _sessionService = sessionService;
            _nsDonViService = nsDonViService;
            _chuDauTuService = chuDauTuService;
            _nsNguonNganSachService = nsNguonNganSachService;
            _nhDmLoaiCongTrinhService = nhDmLoaiCongTrinhService;
            _nhDmChiPhiService = nhDmChiPhiService;
            _nhDaDuAnService = nhDaDuAnService;
            _nhDmTiGiaService = nhDmTiGiaService;
            _nhDmTiGiaChiTietService = nhDmTiGiaChiTietService;
            _nhDaQdDauTuNguonVonService = nhDaQdDauTuNguonVonService;
            _nhDaQdDauTuChiPhiService = nhDaQdDauTuChiPhiService;
            _nhDaQdDauTuHangMucService = nhDaQdDauTuHangMucService;
            _nhDaQdDauTuService = nhDaQdDauTuService;
            _nhDaDuToanNguonVonService = nhDaDuToanNguonVonService;
            _nhDaDuToanChiPhiService = nhDaDuToanChiPhiService;
            _nhDaDuToanHangMucService = nhDaDuToanHangMucService;
            _service = service;
            _nhKhTongTheService = nhKhTongTheService;
            _nhKhTongTheNhiemVuChiService = nhKhTongTheNhiemVuChiService;

            DuToanHangMucDialogViewModel = duToanHangMucDialogViewModel;

            AddDuToanChiPhiCommand = new RelayCommand(obj => OnAddDuToanChiPhi(obj));
            AddDuToanNguonVonCommand = new RelayCommand(obj => OnAddDuToanNguonVon());
            PhuLucHangMucCommand = new RelayCommand(obj => OnPhuLucHangMuc(obj));
            DeleteDuToanNguonVonCommand = new RelayCommand(obj => OnDeleteDuToanNguonVon());
            DeleteDuToanChiPhiCommand = new RelayCommand(obj => OnDeleteDuToanChiPhi());
        }

        public override void Init()
        {
            LoadTitlePheDuyet();
            LoadDefault();
            LoadDanhMucChiPhi();
            LoadAttach();
            LoadDonVi();
            LoadChuDauTu();
            LoadTiGia();
            LoadNguonVon();
            LoadLoaiCongTrinh();
            LoadLoaiDuToan();
            LoadNhiemVuChi();
            LoadKeHoachTongThe();
            LoadData();
            SwitchTitleAndShowDuAn();
        }
        private void LoadLoaiDuToan()
        {
            _itemsLoaiDuToan = new ObservableCollection<NhDaDuToanModel>();
            _itemsLoaiDuToan.Add(new NhDaDuToanModel() { IdLoaiDuToan = 1, STenLoaiDuToan = "Dự toán mua sắm được duyệt" });
            _itemsLoaiDuToan.Add(new NhDaDuToanModel() { IdLoaiDuToan = 2, STenLoaiDuToan = "Dự toán đặt hàng được duyệt" });
            OnPropertyChanged(nameof(ItemsLoaiDuToan));
        }

        private void SetEnableComboboxItemNguonVon()
        {
            if (!_itemsNguonVon.IsEmpty())
            {
                _itemsNguonVon.ForAll(x =>
                {
                    x.IsEnabled = _itemsDuToanNguonVon.IsEmpty() || !_itemsDuToanNguonVon.Any(y => y.IIdNguonVonId != null && y.IIdNguonVonId.Equals(x.IIdMaNguonNganSach));
                });
            }
        }
        public void SwitchTitleAndShowDuAn()
        {
            if (ILoai == 3 || ILoai == 4)
            {
                Title = "Thêm mới dự toán đặt hàng/ mua sắm được duyệt";
                Description = "Thêm mới dự toán đặt hàng/ mua sắm được duyệt";

                IsShowDuAn = true;
                OnPropertyChanged(nameof(IsShowDuAn));
            }
        }

        private void LoadDefault()
        {
            _sessionInfo = _sessionService.Current;
            IsAddSpend = false;
            ItemsDuToanChiPhi = new ObservableCollection<NhDaDuToanChiPhiModel>();
            OnPropertyChanged(nameof(IsAddSpend));
            IsAddSpendRowChild = false;
            OnPropertyChanged(nameof(IsAddSpendRowChild));
        }

        private void LoadDonVi()
        {
            var data = _nsDonViService.FindInternalByNamLamViec(_sessionInfo.YearOfWork);
            _itemsDonVi = _mapper.Map<ObservableCollection<DonViModel>>(data);
            OnPropertyChanged(nameof(ItemsDonVi));
        }

        private void LoadChuDauTu()
        {
            var data = _chuDauTuService.FindByNamLamViec(_sessionInfo.YearOfWork);
            _itemsChuDauTu = _mapper.Map<ObservableCollection<DmChuDauTuModel>>(data);
            OnPropertyChanged(nameof(ItemsChuDauTu));
        }

        private void LoadTiGia()
        {
            var drpData = _nhDmTiGiaService.FindAll().ToList();
            ItemsTiGia = _mapper.Map<ObservableCollection<NhDmTiGiaModel>>(drpData);
            if (Model.IIdTiGiaId.HasValue)
                SelectedTiGia = ItemsTiGia.FirstOrDefault(n => n.Id == Model.IIdTiGiaId.Value);
        }

        //private void LoadTiGiaChiTiet()
        //{
        //    _itemsTiGiaChiTiet = new ObservableCollection<NhDmTiGiaChiTietModel>();
        //    if (SelectedTiGia != null)
        //    {
        //        var data = _nhDmTiGiaChiTietService.FindByTiGiaId(SelectedTiGia.Id);
        //        _itemsTiGiaChiTiet = _mapper.Map<ObservableCollection<NhDmTiGiaChiTietModel>>(data);
        //    }
        //    OnPropertyChanged(nameof(ItemsTiGiaChiTiet));
        //}

        private void LoadNguonVon()
        {
            var data = _nsNguonNganSachService.FindAll();
            _itemsNguonVon = _mapper.Map<ObservableCollection<NguonNganSachModel>>(data);
            //_itemsNguonVon = _mapper.Map<ObservableCollection<ComboboxItem>>(data);
            SetEnableComboboxItemNguonVon();
            OnPropertyChanged(nameof(ItemsNguonVon));
        }

        private void LoadLoaiCongTrinh()
        {
            var data = _nhDmLoaiCongTrinhService.FindAll();
            _itemsLoaiCongTrinh = _mapper.Map<ObservableCollection<ComboboxItem>>(data);
            OnPropertyChanged(nameof(ItemsLoaiCongTrinh));
        }

        private void LoadKeHoachTongThe()
        {
            //var data = _nhKhTongTheService.FindAll().OrderByDescending(x => x.DNgayTao);
            //_itemsNhKhTongThe = _mapper.Map<ObservableCollection<NhKhTongTheModel>>(data);
            //OnPropertyChanged(nameof(ItemsNhKhTongThe));

            //----------Fixbug 31204-ThienCT-26/10/2022-Chọn đơn vị hiển thị kế hoạch tổng thể theo đơn vị đó

            //IEnumerable<NhKhTongThe> data = _nhKhTongTheService.FindAll(s => s.BIsActive).OrderByDescending(x => x.DNgayTao);
            if (!_itemsKhTongTheNhiemVuChi.IsEmpty()) _itemsKhTongTheNhiemVuChi.Clear();
            List<NhKhTongThe> data = new List<NhKhTongThe>();
            if (SelectedDonVi != null)
            {
                data = _nhKhTongTheService.FindByDonViId(SelectedDonVi.Id).ToList();
            }
            //-----------

            _itemsNhKhTongThe = _mapper.Map<ObservableCollection<NhKhTongTheModel>>(data);
            _itemsNhKhTongThe.ForAll(s =>
            {
                if (s.ILoai == Loai_KHTT.GIAIDOAN)
                {
                    //s.SSoKeHoachBqp = $"KHTT {s.IGiaiDoanTu} - {s.IGiaiDoanDen} - Số KH: {s.SSoKeHoachBqp}";
                    s.SSoKeHoachBqp = $"KHTT {s.IGiaiDoanTu_BQP} - {s.IGiaiDoanDen_BQP} - Số KH: {s.SSoKeHoachBqp}";
                }
                else
                {
                    s.SSoKeHoachBqp = $"KHTT {s.INamKeHoach} - Số KH: {s.SSoKeHoachBqp}";
                }
            });
            OnPropertyChanged(nameof(ItemsNhKhTongThe));
        }

        private void LoadNhiemVuChi()
        {
            if (SelectedDonVi != null && SelectedNhKhTongThe != null)
            {
                var data = _nhKhTongTheNhiemVuChiService.FindByIdKhTongTheAndMaDonViID(SelectedNhKhTongThe.Id, SelectedDonVi.Id);
                _itemsKhTongTheNhiemVuChi = _mapper.Map<ObservableCollection<NhKhTongTheNhiemVuChiModel>>(data);
            }
            OnPropertyChanged(nameof(ItemsKhTongTheNhiemVuChi));
        }

        public override void LoadData(params object[] args)
        {
            if (Model.Id.IsNullOrEmpty())
            {
                IconKind = PackIconKind.PlaylistPlus;
                Title = "Thêm mới dự toán đặt hàng/ mua sắm được duyệt";
                Description = "Thêm mới dự toán đặt hàng/ mua sắm được duyệt";
                Model.DNgayQuyetDinh = DateTime.Now;
            }
            else
            {
                NhDaDuToan entity = _service.FindById(Model.Id);
                Model = _mapper.Map<NhDaDuToanModel>(entity);
                if (IsDetail)
                {
                    IconKind = PackIconKind.Details;
                    Title = "Chi tiết dự toán đặt hàng/ mua sắm được duyệt";
                    Description = "Chi tiết dự toán đặt hàng/ mua sắm được duyệt";
                }
                else if (IsDieuChinh)
                {
                    IconKind = PackIconKind.Adjust;
                    Title = "Điều chỉnh dự toán đặt hàng/ mua sắm được duyệt";
                    Description = "Điều chỉnh dự toán đặt hàng/ mua sắm được duyệt";
                    //Model.SSoQuyetDinh = string.Empty;
                    //Model.DNgayQuyetDinh = DateTime.Now;
                }
                else
                {
                    IconKind = PackIconKind.NoteEditOutline;
                    Title = "Cập nhật dự toán đặt hàng/ mua sắm được duyệt";
                    Description = "Cập nhật dự toán đặt hàng/ mua sắm được duyệt";
                }

                _selectedDonVi = _itemsDonVi.FirstOrDefault(x => x.IIDMaDonVi.Equals(Model.IIdMaDonViQuanLy));
                if (SelectedDonVi != null)
                {
                    LoadKeHoachTongThe();
                }
                _selectedNhKhTongThe = _itemsNhKhTongThe.FirstOrDefault(s => s.Id == Model.IIdKHTongTheID);
                LoadDuAn();
                _selectedDuAn = _itemsDuAn.FirstOrDefault(x => x.Id == Model.IIdDuAnId);
                // Load tỉ giá và ngoại tệ khác
                _selectedTiGia = _itemsTiGia.FirstOrDefault(x => x.Id == Model.IIdTiGiaId);
                if (SelectedTiGia != null)
                {
                    IsVisibleTiGiaNhap = true;
                    FTiGiaNhap = Model.FTiGiaNhap;
                }
                //LoadTiGiaChiTiet();
                //_selectedTiGiaChiTiet = _itemsTiGiaChiTiet.FirstOrDefault(x => x.SMaTienTeQuyDoi.Equals(Model.SMaNgoaiTeKhac));
                LoadLoaiDuToan();
                _selectedLoaiDuToan = _itemsLoaiDuToan.FirstOrDefault(x => x.IdLoaiDuToan == Model.IdLoaiDuToan);
                if (Model.IIdKHTTNhiemVuChiId != null)
                {
                    var data = _nhKhTongTheNhiemVuChiService.FindAll();
                    Guid IIdKhTongTheId = data.Any(x => x.Id == Model.IIdKHTTNhiemVuChiId) ? data.FirstOrDefault(x => x.Id == Model.IIdKHTTNhiemVuChiId).IIdKhTongTheId : Guid.Empty;
                    _selectedNhKhTongThe = _itemsNhKhTongThe.FirstOrDefault(x => x.Id == IIdKhTongTheId);
                    var listNhiemVuChi = _nhKhTongTheNhiemVuChiService.FindByIdKhTongTheAndMaDonViID(IIdKhTongTheId, _selectedDonVi.Id);
                    _itemsKhTongTheNhiemVuChi = _mapper.Map<ObservableCollection<NhKhTongTheNhiemVuChiModel>>(listNhiemVuChi);
                    _selectedKhTongTheNhiemVuChi = _itemsKhTongTheNhiemVuChi.FirstOrDefault(x => x.Id == Model.IIdKHTTNhiemVuChiId);
                }
            }

            LoadDuToanNguonVon();
            // LoadDuToanChiPhi();
            CalculateChiPhi();
            OnPropertyChanged(nameof(Model));
            OnPropertyChanged(nameof(SelectedDonVi));
            OnPropertyChanged(nameof(SelectedDuAn));
            OnPropertyChanged(nameof(SelectedTiGia));
            OnPropertyChanged(nameof(SelectedTiGiaChiTiet));
            OnPropertyChanged(nameof(SelectedNhKhTongThe));
            OnPropertyChanged(nameof(SelectedKhTongTheNhiemVuChi));
            OnPropertyChanged(nameof(SelectedLoaiDuToan));
        }

        private void LoadDuAn()
        {
            _itemsDuAn = new ObservableCollection<NhDaDuAnModel>();
            if (_selectedDonVi != null)
            {
                int yearOfWork = _sessionInfo.YearOfWork;
                string maDonVi = _selectedDonVi.IIDMaDonVi;
                IEnumerable<NhDaDuAnQuery> listDuAn;
                if (Model.Id.IsNullOrEmpty())
                {
                    listDuAn = _nhDaDuAnService.FindFromDuToan(yearOfWork, maDonVi);
                }
                else
                {
                    listDuAn = _nhDaDuAnService.FindFromDuToan(yearOfWork, maDonVi, Model.Id);
                }
                _itemsDuAn = _mapper.Map<ObservableCollection<NhDaDuAnModel>>(listDuAn);
            }
            OnPropertyChanged(nameof(ItemsDuAn));
        }

        private void LoadThongTinDuAn()
        {
            if (SelectedDuAn != null)
            {
                // Lấy thông tin chủ trương đầu tư của dự án
                var qdDauTu = _nhDaQdDauTuService.FindByDuAnId(SelectedDuAn.Id);
                if (qdDauTu != null)
                {
                    var lstNguonVon = _nhDaQdDauTuNguonVonService.FindByQdDauTuId(qdDauTu.Id);
                    var lstChiPhi = _nhDaQdDauTuChiPhiService.FindByQdDauTuId(qdDauTu.Id);

                    // Fill thông tin quyết định đầu tư
                    //Model.FTongMucDauTuPheDuyetNgoaiTeKhac = lstNguonVon.Sum(x => x.FGiaTriNgoaiTeKhac);
                    //Model.FTongMucDauTuPheDuyetEur = lstNguonVon.Sum(x => x.FGiaTriEur);
                    Model.FTongMucDauTuPheDuyetUsd = lstNguonVon.Sum(x => x.FGiaTriUsd);
                    Model.FTongMucDauTuPheDuyetVnd = lstNguonVon.Sum(x => x.FGiaTriVnd);
                    Model.SDiaDiem = qdDauTu.SDiaDiem;
                    SelectedChuDauTu = ItemsChuDauTu.FirstOrDefault(x => x.IIDMaDonVi.Equals(qdDauTu.IIdMaChuDauTu));
                    OnPropertyChanged(nameof(Model));

                    // Nếu thêm mới load thông tin chi phí nguồn vốn
                    if (Model.Id.IsNullOrEmpty())
                    {
                        bool isExitsData = !_itemsDuToanNguonVon.IsEmpty()
                        || !_itemsDuToanChiPhi.IsEmpty() && _itemsDuToanChiPhi.Any(x => !x.DuToanHangMucs.IsEmpty());
                        if (isExitsData)
                        {
                            // Confirm xem có muốn load lại thông tin nguồn vốn và hạng mục hay không
                            var result = MessageBoxHelper.Confirm("Đồng chí có muốn tải lại thông tin nguồn vốn và hạng mục từ dự án được chọn không?");
                            if (result == MessageBoxResult.No) return;
                            //fixbug 30353-ThienCT-26/10/2022 change dự án clear dự toán chi phí
                            else
                                if (!_itemsDuToanChiPhi.IsEmpty()) _itemsDuToanChiPhi.Clear();
                            //

                        }

                        _itemsDuToanNguonVon = _mapper.Map<ObservableCollection<NhDaDuToanNguonVonModel>>(lstNguonVon);
                        foreach (var itemNguonVon in _itemsDuToanNguonVon)
                        {
                            itemNguonVon.IsAdded = true;
                            itemNguonVon.IIdQdDauTuNguonVonId = itemNguonVon.Id;
                            itemNguonVon.FGiaTriQdDauTuUsd = itemNguonVon.FGiaTriUsd;
                            itemNguonVon.FGiaTriQdDauTuVnd = itemNguonVon.FGiaTriVnd;
                            //itemNguonVon.FGiaTriQdDauTuEur = itemNguonVon.FGiaTriEur;
                            //itemNguonVon.FGiaTriQdDauTuNgoaiTeKhac = itemNguonVon.FGiaTriNgoaiTeKhac;
                            itemNguonVon.FGiaTriUsd = 0;
                            itemNguonVon.FGiaTriVnd = 0;
                            //itemNguonVon.FGiaTriEur = 0;
                            //itemNguonVon.FGiaTriNgoaiTeKhac = 0;
                            itemNguonVon.Id = Guid.NewGuid();

                            var listChiPhi = _nhDaQdDauTuChiPhiService.FindByNguonVonId((Guid)itemNguonVon.IIdQdDauTuNguonVonId);
                            itemNguonVon.DuToanChiPhis = _mapper.Map<ObservableCollection<NhDaDuToanChiPhiModel>>(listChiPhi);

                            var refChiPhiDictionary = itemNguonVon.DuToanChiPhis.ToDictionary(x => x.Id, x => Guid.NewGuid());

                            foreach (var itemChiPhi in itemNguonVon.DuToanChiPhis)
                            {
                                var lstHangMuc = _nhDaQdDauTuHangMucService.FindByQdDauTuChiPhiId(itemChiPhi.Id);

                                itemChiPhi.IsAdded = true;
                                itemChiPhi.IIdQdDauTuChiPhiId = itemChiPhi.Id;
                                itemChiPhi.FGiaTriQdDauTuUsd = itemChiPhi.FGiaTriUsd;
                                itemChiPhi.FGiaTriQdDauTuVnd = itemChiPhi.FGiaTriVnd;
                                //itemChiPhi.FGiaTriQdDauTuEur = itemChiPhi.FGiaTriEur;
                                //itemChiPhi.FGiaTriQdDauTuNgoaiTeKhac = itemChiPhi.FGiaTriNgoaiTeKhac;
                                itemChiPhi.FGiaTriUsd = 0;
                                itemChiPhi.FGiaTriVnd = 0;
                                //itemChiPhi.FGiaTriEur = 0;
                                //itemChiPhi.FGiaTriNgoaiTeKhac = 0;
                                itemChiPhi.IIdDuToanNguonVonId = itemNguonVon.Id;
                                itemChiPhi.STenNguonVon = ItemsNguonVon.FirstOrDefault(x => x.IIdMaNguonNganSach == itemNguonVon.IIdNguonVonId).STen;
                                itemChiPhi.Id = refChiPhiDictionary[itemChiPhi.Id];
                                if (!itemChiPhi.IIdParentId.IsNullOrEmpty())
                                {
                                    itemChiPhi.IIdParentId = refChiPhiDictionary[itemChiPhi.IIdParentId.Value];
                                }

                                itemChiPhi.DuToanHangMucs = _mapper.Map<ObservableCollection<NhDaDuToanHangMucModel>>(lstHangMuc);
                                if (itemChiPhi.DuToanHangMucs.Count() > 0)
                                {
                                    itemChiPhi.IsEnableEdit = false;
                                }
                                else itemChiPhi.IsEnableEdit = true;

                                var refHangMucDictionary = itemChiPhi.DuToanHangMucs.ToDictionary(x => x.Id, x => Guid.NewGuid());
                                foreach (var itemHangMuc in itemChiPhi.DuToanHangMucs)
                                {
                                    itemHangMuc.IsAdded = true;
                                    itemHangMuc.IIdQdDauTuHangMucId = itemHangMuc.Id;
                                    itemHangMuc.FGiaTriQdDauTuUsd = itemHangMuc.FGiaTriUsd;
                                    itemHangMuc.FGiaTriQdDauTuVnd = itemHangMuc.FGiaTriVnd;
                                    //itemHangMuc.FGiaTriQdDauTuEur = itemHangMuc.FGiaTriEur;
                                    //itemHangMuc.FGiaTriQdDauTuNgoaiTeKhac = itemHangMuc.FGiaTriNgoaiTeKhac;
                                    itemHangMuc.FGiaTriUsd = 0;
                                    itemHangMuc.FGiaTriVnd = 0;
                                    //itemHangMuc.FGiaTriEur = 0;
                                    //itemHangMuc.FGiaTriNgoaiTeKhac = 0;
                                    itemHangMuc.Id = refHangMucDictionary[itemHangMuc.Id];
                                    if (!itemHangMuc.IIdParentId.IsNullOrEmpty())
                                    {
                                        itemHangMuc.IIdParentId = refHangMucDictionary[itemHangMuc.IIdParentId.Value];
                                    }
                                    itemHangMuc.IIdDuToanChiPhiId = itemChiPhi.Id;
                                }
                                itemChiPhi.PropertyChanged += DuToanChiPhi_PropertyChanged;
                            }

                            itemNguonVon.PropertyChanged += DuToanNguonVon_PropertyChanged;
                        }
                        OnPropertyChanged(nameof(ItemsDuToanNguonVon));

                        /* foreach (var itemNguonVon in _itemsDuToanNguonVon)
                         {
                             var listChiPhi = _nhDaQdDauTuChiPhiService.FindByNguonVonId(itemNguonVon.Id);

                         }*/
                        /*_itemsDuToanChiPhi = _mapper.Map<ObservableCollection<NhDaDuToanChiPhiModel>>(lstChiPhi);
                        //var refChiPhiDictionary = _itemsDuToanChiPhi.ToDictionary(x => x.Id, x => Guid.NewGuid());
                        foreach (var itemChiPhi in _itemsDuToanChiPhi)
                        {
                            var lstHangMuc = _nhDaQdDauTuHangMucService.FindByQdDauTuChiPhiId(itemChiPhi.Id);

                            itemChiPhi.IsAdded = true;
                            itemChiPhi.IIdQdDauTuChiPhiId = itemChiPhi.Id;
                            itemChiPhi.FGiaTriQdDauTuUsd = itemChiPhi.FGiaTriUsd;
                            itemChiPhi.FGiaTriQdDauTuEur = itemChiPhi.FGiaTriEur;
                            itemChiPhi.FGiaTriQdDauTuVnd = itemChiPhi.FGiaTriVnd;
                            itemChiPhi.FGiaTriQdDauTuNgoaiTeKhac = itemChiPhi.FGiaTriNgoaiTeKhac;
                            itemChiPhi.FGiaTriUsd = 0;
                            itemChiPhi.FGiaTriEur = 0;
                            itemChiPhi.FGiaTriVnd = 0;
                            itemChiPhi.FGiaTriNgoaiTeKhac = 0;
                            itemChiPhi.Id = refChiPhiDictionary[itemChiPhi.Id];
                            if (!itemChiPhi.IIdParentId.IsNullOrEmpty())
                            {
                                itemChiPhi.IIdParentId = refChiPhiDictionary[itemChiPhi.IIdParentId.Value];
                            }
                            itemChiPhi.DuToanHangMucs = _mapper.Map<ObservableCollection<NhDaDuToanHangMucModel>>(lstHangMuc);

                            var refHangMucDictionary = itemChiPhi.DuToanHangMucs.ToDictionary(x => x.Id, x => Guid.NewGuid());
                            foreach (var itemHangMuc in itemChiPhi.DuToanHangMucs)
                            {
                                itemHangMuc.IsAdded = true;
                                itemHangMuc.IIdQdDauTuHangMucId = itemHangMuc.Id;
                                itemHangMuc.FGiaTriQdDauTuUsd = itemHangMuc.FGiaTriUsd;
                                itemHangMuc.FGiaTriQdDauTuEur = itemHangMuc.FGiaTriEur;
                                itemHangMuc.FGiaTriQdDauTuVnd = itemHangMuc.FGiaTriVnd;
                                itemHangMuc.FGiaTriQdDauTuNgoaiTeKhac = itemHangMuc.FGiaTriNgoaiTeKhac;
                                itemHangMuc.FGiaTriUsd = 0;
                                itemHangMuc.FGiaTriEur = 0;
                                itemHangMuc.FGiaTriVnd = 0;
                                itemHangMuc.FGiaTriNgoaiTeKhac = 0;
                                itemHangMuc.Id = refHangMucDictionary[itemHangMuc.Id];
                                if (!itemHangMuc.IIdParentId.IsNullOrEmpty())
                                {
                                    itemHangMuc.IIdParentId = refHangMucDictionary[itemHangMuc.IIdParentId.Value];
                                }
                                itemHangMuc.IIdDuToanChiPhiId = itemChiPhi.Id;
                            }
                            itemChiPhi.PropertyChanged += DuToanChiPhi_PropertyChanged;
                        }
                        OnPropertyChanged(nameof(ItemsDuToanChiPhi));*/
                    }
                }
            }
        }

        private void LoadDuToanNguonVon()
        {
            _itemsDuToanNguonVon = new ObservableCollection<NhDaDuToanNguonVonModel>();
            NhDaDuToanNguonVonModel targetItem = new NhDaDuToanNguonVonModel
            {
                Id = Guid.NewGuid(),
                IIdNguonVonId = 5,
                IsAdded = true,
                IsModified = true
            };


            LoadDuToanChiPhi(targetItem);
            targetItem.PropertyChanged += DuToanNguonVon_PropertyChanged;
            _itemsDuToanNguonVon.Insert(0, targetItem);

            if (!Model.Id.IsNullOrEmpty())
            {
                // Cập nhật hoặc Điều chỉnh
                var data = _nhDaDuToanNguonVonService.FindByDuToanId(Model.Id).ToList();
                _itemsDuToanNguonVon = _mapper.Map<ObservableCollection<NhDaDuToanNguonVonModel>>(data);

                foreach (var item in _itemsDuToanNguonVon)
                {
                    // Lấy thông tin
                    if (!item.IIdQdDauTuNguonVonId.IsNullOrEmpty())
                    {
                        var qdDauTuNguonVon = _nhDaQdDauTuNguonVonService.FindById(item.IIdQdDauTuNguonVonId.Value);
                        if (qdDauTuNguonVon != null)
                        {
                            item.FGiaTriQdDauTuUsd = qdDauTuNguonVon.FGiaTriUsd;
                            item.FGiaTriQdDauTuVnd = qdDauTuNguonVon.FGiaTriVnd;
                            //item.FGiaTriQdDauTuEur = qdDauTuNguonVon.FGiaTriEur;
                            //item.FGiaTriQdDauTuNgoaiTeKhac = qdDauTuNguonVon.FGiaTriNgoaiTeKhac;
                        }
                    }

                    //if (IsDieuChinh)
                    //{
                    //    // Renew id
                    //    item.Id = Guid.NewGuid();
                    //    item.IIdDuToanId = Guid.Empty;
                    //    item.IsAdded = true;
                    //}

                    LoadDuToanChiPhi(item);
                    item.PropertyChanged += DuToanNguonVon_PropertyChanged;
                    item.IsSelected = true;
                }
            }
            CalculateNguonVon();
            SetEnableComboboxItemNguonVon();
            OnPropertyChanged(nameof(ItemsDuToanNguonVon));
        }

        private void LoadDuToanChiPhi(NhDaDuToanNguonVonModel nguonVon)
        {
            //_itemsDuToanChiPhi = new ObservableCollection<NhDaDuToanChiPhiModel>();
            if (!Model.Id.IsNullOrEmpty())
            {
                // Cập nhật hoặc Điều chỉnh
                IEnumerable<NhDaDuToanChiPhi> data = _nhDaDuToanChiPhiService.FindByNguonVonId(nguonVon.Id);
                nguonVon.DuToanChiPhis = _mapper.Map<ObservableCollection<NhDaDuToanChiPhiModel>>(data);
                if (data.Any())
                {
                    nguonVon.IsEnableEdit = false;
                }
                else nguonVon.IsEnableEdit = true;

                // Renew id for adjust
                var refDictionary = nguonVon.DuToanChiPhis.ToDictionary(x => x.Id, x => Guid.NewGuid());
                foreach (var item in nguonVon.DuToanChiPhis)
                {
                    item.STenNguonVon = ItemsNguonVon.FirstOrDefault(x => x.IIdMaNguonNganSach == nguonVon.IIdNguonVonId).STen;
                    // Lấy thông tin qd đầu tư chi phí
                    if (!item.IIdQdDauTuChiPhiId.IsNullOrEmpty())
                    {
                        var qdDauTuChiPhi = _nhDaQdDauTuChiPhiService.FindById(item.IIdQdDauTuChiPhiId.Value);
                        if (qdDauTuChiPhi != null)
                        {
                            var qdDauTuHangMuc = _nhDaQdDauTuHangMucService.FindByQdDauTuChiPhiId(qdDauTuChiPhi.Id);
                            if (!qdDauTuHangMuc.IsEmpty())
                            {
                                item.FGiaTriQdDauTuUsd = qdDauTuHangMuc.Sum(x => x.FGiaTriUsd);
                                item.FGiaTriQdDauTuVnd = qdDauTuHangMuc.Sum(x => x.FGiaTriVnd);
                                //item.FGiaTriQdDauTuEur = qdDauTuHangMuc.Sum(x => x.FGiaTriEur);
                                //item.FGiaTriQdDauTuNgoaiTeKhac = qdDauTuHangMuc.Sum(x => x.FGiaTriNgoaiTeKhac);
                            }
                            else
                            {
                                item.FGiaTriQdDauTuUsd = qdDauTuChiPhi.FGiaTriUsd;
                                item.FGiaTriQdDauTuVnd = qdDauTuChiPhi.FGiaTriVnd;
                                //item.FGiaTriQdDauTuEur = qdDauTuChiPhi.FGiaTriEur;
                                //item.FGiaTriQdDauTuNgoaiTeKhac = qdDauTuChiPhi.FGiaTriNgoaiTeKhac;
                            }
                        }
                    }

                    // Load hạng mục. Cần optimize để lấy dữ liệu ngay từ lúc lấy chi phí để tối ưu hiệu năng
                    IEnumerable<NhDaDuToanHangMuc> dataHangMuc = _nhDaDuToanHangMucService.FindByDuToanChiPhiId(item.Id);
                    item.DuToanHangMucs = _mapper.Map<ObservableCollection<NhDaDuToanHangMucModel>>(dataHangMuc);
                    if (item.DuToanHangMucs.Count() > 0)
                    {
                        item.IsEnableEdit = false;
                    }
                    else item.IsEnableEdit = true;


                    // Tính lại giá trị của chi phí. Nếu có hạng mục thì tính tổng hạng mục. Không thì lấy trực tiếp theo chi phí
                    //if (!item.DuToanHangMucs.IsEmpty())
                    //{
                    //    item.FGiaTriUsd = item.DuToanHangMucs.Sum(x => x.FGiaTriUsd);
                    //    item.FGiaTriVnd = item.DuToanHangMucs.Sum(x => x.FGiaTriVnd);
                    //    //item.FGiaTriEur = item.DuToanHangMucs.Sum(x => x.FGiaTriEur);
                    //    //item.FGiaTriNgoaiTeKhac = item.DuToanHangMucs.Sum(x => x.FGiaTriNgoaiTeKhac);
                    //}

                    // Nếu là điều chỉnh, cần làm mới id liên quan
                    if (IsDieuChinh)
                    {
                        item.Id = refDictionary[item.Id];
                        if (!item.IIdParentId.IsNullOrEmpty())
                        {
                            item.IIdParentId = refDictionary[item.IIdParentId.Value];
                        }
                        item.IIdDuToanId = Guid.Empty;
                        item.IsAdded = true;

                        // Renew id hạng mục
                        var refItemDictionary = item.DuToanHangMucs.ToDictionary(x => x.Id, x => Guid.NewGuid());
                        foreach (var itemHangMuc in item.DuToanHangMucs)
                        {
                            itemHangMuc.Id = refItemDictionary[itemHangMuc.Id];
                            if (!itemHangMuc.IIdParentId.IsNullOrEmpty())
                            {
                                itemHangMuc.IIdParentId = refItemDictionary[itemHangMuc.IIdParentId.Value];
                            }
                            itemHangMuc.IIdDuToanChiPhiId = Guid.Empty;
                            itemHangMuc.IsAdded = true;
                        }
                    }
                    item.ItemsLoaiNoiDungChi = ItemsDMChiPhi;
                    item.PropertyChanged += DuToanChiPhi_PropertyChanged;
                }
            }

            if (IsDieuChinh)
            {
                // Renew id
                nguonVon.Id = Guid.NewGuid();
                //nguonVon.IIdDuToanId = Guid.Empty;
                nguonVon.IsAdded = true;
            }
            CalculateChiPhi();
            OnPropertyChanged(nameof(ItemsDuToanChiPhi));
        }

        protected void OnAddDuToanNguonVon()
        {
            if (_itemsDuToanNguonVon == null) _itemsDuToanNguonVon = new ObservableCollection<NhDaDuToanNguonVonModel>();

            int currentRow = -1;
            if (!_itemsDuToanNguonVon.IsEmpty())
            {
                currentRow = 0;
                if (SelectedDuToanNguonVon != null)
                {
                    currentRow = _itemsDuToanNguonVon.IndexOf(SelectedDuToanNguonVon);
                }
            }

            NhDaDuToanNguonVonModel targetItem = new NhDaDuToanNguonVonModel();
            targetItem.Id = Guid.NewGuid();
            targetItem.IsAdded = true;
            targetItem.IsModified = true;
            //IEnumerable<NhDmChiPhi> data = _nhDmChiPhiService.FindAll();
            //-----------Fixbug 31204 / 2. /ThienCT-26/10/2022-k hiển thị các chi phí, để người dùng tự tạo
            List<NhDmChiPhi> data = ILoai == 3 || ILoai == 4 ? _nhDmChiPhiService.FindAll().ToList() : new List<NhDmChiPhi>();
            //----------------------

            foreach (var chiPhi in data)
            {
                NhDaDuToanChiPhiModel duToanChiPhi = new NhDaDuToanChiPhiModel();
                duToanChiPhi.Id = Guid.NewGuid();
                duToanChiPhi.IIdDuToanNguonVonId = targetItem.Id;
                duToanChiPhi.IsAdded = true;
                duToanChiPhi.STenChiPhi = chiPhi.STenChiPhi;
                duToanChiPhi.IIdChiPhiId = chiPhi.IIdChiPhi;
                //duToanChiPhi.STenNguonVon = ItemsNguonVon.FirstOrDefault(x => x.ValueItem == targetItem.IIdNguonVonId.ToString()).DisplayItem;
                targetItem.DuToanChiPhis.Add(duToanChiPhi);
                duToanChiPhi.PropertyChanged += DuToanChiPhi_PropertyChanged;
            }
            targetItem.PropertyChanged += DuToanNguonVon_PropertyChanged;
            _itemsDuToanNguonVon.Insert(currentRow + 1, targetItem);
            OnPropertyChanged(nameof(ItemsDuToanNguonVon));
        }
        //private void SetEnableComboboxItemNguonVon()
        //{
        //    if (!_itemsNguonVon.IsEmpty())
        //    {
        //        _itemsNguonVon.ForAll(x =>
        //        {
        //            x.IsEnabled = _itemsDuToanNguonVon.IsEmpty() || !_itemsDuToanNguonVon.Any(y => y.IIdNguonVonId != null && y.IIdNguonVonId.Equals(x.IIdMaNguonNganSach));
        //        });
        //    }
        //}
        protected void OnAddDuToanChiPhi(object obj)
        {
            if (_itemsDuToanChiPhi == null) _itemsDuToanChiPhi = new ObservableCollection<NhDaDuToanChiPhiModel>();
            IsAddSpendRowChild = true;
            NhDaDuToanChiPhiModel sourceItem = SelectedDuToanChiPhi;
            NhDaDuToanChiPhiModel targetItem = new NhDaDuToanChiPhiModel();
            bool isParent = (bool)obj;
            int currentRow = -1;
            if (!_itemsDuToanChiPhi.IsEmpty())
            {
                currentRow = 0;
                if (sourceItem != null)
                {
                    currentRow = _itemsDuToanChiPhi.IndexOf(sourceItem);
                    if (isParent)
                    {
                        currentRow += CountTreeChildItems(sourceItem);
                    }
                }
            }

            if (sourceItem != null)
            {
                targetItem.IIdParentId = isParent ? sourceItem.IIdParentId : sourceItem.Id;
                if (isParent)
                {
                    targetItem.IsHasChildren = sourceItem.IsHasChildren;
                }
                else
                {
                    targetItem.IsHasChildren = true;
                    sourceItem.IsHasChildren = false;
                }
            }
            else
            {
                targetItem.IsHasChildren = true;
            }
            targetItem.Id = Guid.NewGuid();
            targetItem.ItemsLoaiNoiDungChi = ItemsDMChiPhi;
            targetItem.IsAdded = true;
            targetItem.IsModified = true;
            //targetItem.EnableEdit= true;
            if (targetItem != null)
            {
                //targetItem.IIdDuToanNguonVonId = sourceItem.IIdDuToanNguonVonId;
                targetItem.IIdDuToanNguonVonId = sourceItem != null ? sourceItem.IIdDuToanNguonVonId : SelectedDuToanNguonVon.Id;
            }
            //targetItem.STenNguonVon = sourceItem.STenNguonVon;
            targetItem.STenNguonVon = sourceItem != null ? sourceItem.STenNguonVon : ItemsNguonVon.FirstOrDefault(x => x.IIdMaNguonNganSach == SelectedDuToanNguonVon.IIdNguonVonId).STen;
            targetItem.PropertyChanged += DuToanChiPhi_PropertyChanged;
            foreach (var nguonVon in ItemsDuToanNguonVon)
            {
                if (nguonVon.Id == targetItem.IIdDuToanNguonVonId)
                {
                    nguonVon.DuToanChiPhis.Add(targetItem);
                    break;
                }
            }
            _itemsDuToanChiPhi.Insert(currentRow + 1, targetItem);
            if (ItemsDuToanChiPhi.Count > 0)
            {
                if (!IsAddSpendRowChild)
                {
                    IsAddSpendRowChild = true;
                    OnPropertyChanged(nameof(IsAddSpendRowChild));
                }
            }
            else
            {
                if (IsAddSpendRowChild)
                {
                    IsAddSpendRowChild = false;
                    OnPropertyChanged(nameof(IsAddSpendRowChild));
                }
            }
            OrderItems(targetItem.IIdParentId);
            OnPropertyChanged(nameof(ItemsDuToanChiPhi));
        }

        private void OnPhuLucHangMuc(object obj)
        {
            var currentModel = (NhDaDuToanChiPhiModel)obj;
            DuToanHangMucDialogViewModel.IsEnableQuyetDinhDauTuPheDuyet = IsEnableQuyetDinhDauTuPheDuyet;
            DuToanHangMucDialogViewModel.Model = currentModel;
            DuToanHangMucDialogViewModel.CurrencyExchangeAction = (obj, propName) => DuToanHangMucCurrencyExchange(obj, propName);
            DuToanHangMucDialogViewModel.FTiGiaNhap = FTiGiaNhap;
            DuToanHangMucDialogViewModel.idDuToan = SelectedLoaiDuToan.IdLoaiDuToan;
            DuToanHangMucDialogViewModel.Model.STenChiPhi = SelectedDuToanChiPhi.STenChiPhi;
            DuToanHangMucDialogViewModel.Init();
            DuToanHangMucDialogViewModel.IsDetail = IsDetail;
            DuToanHangMucDialogViewModel.SavedAction = obj =>
            {
                var data = (IEnumerable<NhDaDuToanHangMucModel>)obj;
                currentModel.DuToanHangMucs = _mapper.Map<ObservableCollection<NhDaDuToanHangMucModel>>(data);
            };
            DuToanHangMucDialogViewModel.ShowDialog();
        }

        private void DuToanHangMucCurrencyExchange(object sender, string propName)
        {
            if (SelectedTiGia != null)
            {
                if (propName.Equals(nameof(NhDaDuToanHangMucModel.FGiaTriVnd))
                //if (propName.Equals(nameof(NhDaDuToanHangMucModel.FGiaTriNgoaiTeKhac))
                    || propName.Equals(nameof(NhDaDuToanHangMucModel.FGiaTriUsd)))
                    //|| propName.Equals(nameof(NhDaDuToanHangMucModel.FGiaTriEur))
                    //|| propName.Equals(nameof(NhDaDuToanHangMucModel.FGiaTriNgoaiTeKhac)))
                {
                    NhDaDuToanHangMucModel objectSender = (NhDaDuToanHangMucModel)sender;
                    var listTiGiaChiTiet = _mapper.Map<IEnumerable<NhDmTiGiaChiTiet>>(ItemsTiGiaChiTiet);
                    string rootCurrency = SelectedTiGia.SMaTienTeGoc;
                    string sourceCurrency;
                    string otherCurrency = SelectedTiGiaChiTiet != null ? SelectedTiGiaChiTiet.SMaTienTeQuyDoi : "";
                    double value;
                    switch (propName)
                    {
                        case nameof(NhDaDuToanHangMucModel.FGiaTriVnd):
                            sourceCurrency = LoaiTienTeEnum.TypeCode.VND;
                            value = objectSender.FGiaTriVnd.Value;
                            break;
                        //case nameof(NhDaDuToanHangMucModel.FGiaTriEur):
                        //    sourceCurrency = LoaiTienTeEnum.TypeCode.EUR;
                        //    value = objectSender.FGiaTriEur.Value;
                        //    break;
                        //case nameof(NhDaDuToanHangMucModel.FGiaTriNgoaiTeKhac):
                        //    sourceCurrency = otherCurrency;
                        //    value = objectSender.FGiaTriNgoaiTeKhac.Value;
                        //    break;
                        default:
                            sourceCurrency = LoaiTienTeEnum.TypeCode.USD;
                            value = objectSender.FGiaTriUsd.Value;
                            break;
                    }
                    objectSender.FGiaTriVnd = _nhDmTiGiaService.CurrencyExchange(sourceCurrency, LoaiTienTeEnum.TypeCode.VND, rootCurrency, listTiGiaChiTiet, value);
                    objectSender.FGiaTriUsd = _nhDmTiGiaService.CurrencyExchange(sourceCurrency, LoaiTienTeEnum.TypeCode.USD, rootCurrency, listTiGiaChiTiet, value);
                    //objectSender.FGiaTriEur = _nhDmTiGiaService.CurrencyExchange(sourceCurrency, LoaiTienTeEnum.TypeCode.EUR, rootCurrency, listTiGiaChiTiet, value);
                    //objectSender.FGiaTriNgoaiTeKhac = _nhDmTiGiaService.CurrencyExchange(sourceCurrency, otherCurrency, rootCurrency, listTiGiaChiTiet, value);
                }
            }
        }

        private void OnDeleteDuToanNguonVon()
        {
            if (SelectedDuToanNguonVon != null)
            {
                SelectedDuToanNguonVon.IsDeleted = !SelectedDuToanNguonVon.IsDeleted;
            }
        }
        private void OnDeleteDuToanChiPhi()
        {
            if (SelectedDuToanChiPhi != null)
            {
                DeleteTreeItems(SelectedDuToanChiPhi, !SelectedDuToanChiPhi.IsDeleted);
            }
        }

        public override void OnSave(object obj)
        {
            // Convert entity
            if (SelectedDonVi != null)
            {
                Model.IIdDonViQuanLyId = SelectedDonVi.Id;
                Model.IIdMaDonViQuanLy = SelectedDonVi.IIDMaDonVi;
            }
            if (SelectedLoaiDuToan != null)
            {
                Model.IdLoaiDuToan = SelectedLoaiDuToan.IdLoaiDuToan;
            }
            if (SelectedNhKhTongThe != null)
            {
                Model.IIdKHTongTheID = SelectedNhKhTongThe.Id;
            }
            if (SelectedTiGia != null)
            {
                Model.IIdTiGiaId = SelectedTiGia.Id;
            }
            if (SelectedTiGiaChiTiet != null)
            {
                Model.SMaNgoaiTeKhac = SelectedTiGiaChiTiet.SMaTienTeQuyDoi;
            }
            if (SelectedKhTongTheNhiemVuChi != null)
            {
                Model.IIdKHTTNhiemVuChiId = SelectedKhTongTheNhiemVuChi.Id;
            }
            if (!ValidateViewModelHelper.Validate(Model)) return;
            if (!ValiDateData())
                return;
            // Chi tiết
            Model.DuToanNguonVons = _mapper.Map<ObservableCollection<NhDaDuToanNguonVonModel>>(ItemsDuToanNguonVon);
            Model.DuToanChiPhis = _mapper.Map<ObservableCollection<NhDaDuToanChiPhiModel>>(ItemsDuToanChiPhi);
            _ = BackgroundWorkerHelper.Run((s, e) =>
            {
                IsLoading = true;
                // Main process
                NhDaDuToan entity;
                if (Model.Id.IsNullOrEmpty())
                {
                    // Add NhDaDuToan
                    entity = _mapper.Map<NhDaDuToan>(Model);
                    entity.Id = Guid.NewGuid();
                    entity.BIsActive = true;
                    entity.BIsGoc = true;
                    entity.BIsXoa = false;
                    entity.ILanDieuChinh = 0;
                    entity.BIsKhoa = false;
                    entity.ILoai = ILoai;
                    entity.FGiaTriUsd = FGiaTriUsd;
                    entity.FGiaTriVnd = FGiaTriVnd;
                    entity.FTiGiaNhap = FTiGiaNhap;
                    entity.DNgayTao = DateTime.Now;
                    entity.SNguoiTao = _sessionService.Current.Principal;
                    if (ILoai != 3 && ILoai != 4)
                    {
                        entity.STenChuongTrinh = SelectedKhTongTheNhiemVuChi != null ? SelectedKhTongTheNhiemVuChi.STenNhiemVuChi : "";
                    }
                    _service.Add(entity);
                }
                else if (IsDieuChinh)
                {
                    // Điều chỉnh
                    entity = _mapper.Map<NhDaDuToan>(Model);
                    entity.Id = Guid.NewGuid();
                    entity.IIdParentId = Model.Id;
                    entity.BIsActive = true;
                    entity.BIsGoc = false;
                    entity.BIsKhoa = false;
                    entity.FGiaTriUsd = FGiaTriUsd;
                    entity.FGiaTriVnd = FGiaTriVnd;
                    entity.FTiGiaNhap = FTiGiaNhap;
                    entity.BIsXoa = false;
                    entity.ILanDieuChinh = entity.ILanDieuChinh + 1;
                    entity.DNgayTao = DateTime.Now;
                    entity.SNguoiTao = _sessionInfo.Principal;
                    if (ILoai != 3 && ILoai != 4)
                    {
                        entity.STenChuongTrinh = SelectedKhTongTheNhiemVuChi.STenNhiemVuChi;
                    }
                    _service.Adjust(entity);
                }
                else
                {
                    // Update
                    entity = _service.FindById(Model.Id);
                    _mapper.Map(Model, entity);
                    entity.DNgaySua = DateTime.Now;
                    entity.FGiaTriUsd = FGiaTriUsd;
                    entity.FGiaTriVnd = FGiaTriVnd;
                    entity.FTiGiaNhap = FTiGiaNhap;
                    entity.SNguoiSua = _sessionService.Current.Principal;
                    if (ILoai != 3 && ILoai != 4)
                    {
                        entity.STenChuongTrinh = SelectedKhTongTheNhiemVuChi.STenNhiemVuChi;
                    }
                    _service.Update(entity);
                }
                // Save attach file
                SaveAttachment(entity.Id);
                e.Result = entity;
            }, (s, e) =>
            {
                IsLoading = false;

                if (e.Error == null)
                {
                    // Reload data
                    Model = _mapper.Map<NhDaDuToanModel>(e.Result);
                    IsDieuChinh = false;
                    SavedAction?.Invoke(Model);
                    //LoadData();
                    //ItemsDuToanChiPhi.Clear();
                    // Invoke messagefitems
                    MessageBoxHelper.Info(Resources.MsgSaveDone);
                    OnClose(obj);
                }
                else
                {
                    _logger.Error(e.Error.Message);
                }
            });
        }
        public override void Dispose()
        {
            // Clear items
            if (!_itemsDuToanNguonVon.IsEmpty()) _itemsDuToanNguonVon.Clear();
            if (!_itemsDuToanChiPhi.IsEmpty()) _itemsDuToanChiPhi.Clear();
            if (!_itemsNguonVon.IsEmpty()) _itemsNguonVon.Clear();
            if (!_itemsLoaiCongTrinh.IsEmpty()) _itemsLoaiCongTrinh.Clear();
            if (!_itemsTiGiaChiTiet.IsEmpty()) _itemsTiGiaChiTiet.Clear();
            if (!_itemsTiGia.IsEmpty()) _itemsTiGia.Clear();
            if (!_itemsDuAn.IsEmpty()) _itemsDuAn.Clear();
            if (!_itemsDonVi.IsEmpty()) _itemsDonVi.Clear();
            if (!_itemsNhKhTongThe.IsEmpty()) _itemsNhKhTongThe.Clear();
            if (!_itemsKhTongTheNhiemVuChi.IsEmpty()) _itemsKhTongTheNhiemVuChi.Clear();
        }

        //ThienCT-26/10/2022-Bắt sự kiện closing để dispose
        public override void OnClose(object obj)
        {
            if (obj is Window window)
            {
                Dispose();
                window.Close();
            }
        }
        public override void OnClosing()
        {
            Dispose();
        }

        private void ChangeValueByRate(IEnumerable<NhDaDuToanNguonVonModel> ItemsNguonVon = null, IEnumerable<NhDaDuToanChiPhiModel> ItemsChiPhi = null, bool isUsD = true)
        {
            if (FTiGiaNhap != null && FTiGiaNhap.HasValue && FTiGiaNhap.Value != 0)
            {
                if (isUsD)
                {
                    if (!ItemsNguonVon.IsEmpty())
                        ItemsNguonVon.ForAll(item =>
                        {
                            item.FGiaTriVnd = (item.FGiaTriUsd != null && item.FGiaTriUsd.HasValue) ? (item.FGiaTriUsd.Value * FTiGiaNhap.Value) : 0;
                        });
                    if (!ItemsChiPhi.IsEmpty())
                        ItemsChiPhi.ForAll(item =>
                        {
                            item.FGiaTriVnd = (item.FGiaTriUsd != null && item.FGiaTriUsd.HasValue) ? (item.FGiaTriUsd.Value * FTiGiaNhap.Value) : 0;
                        });
                }
                else
                {
                    if (!ItemsNguonVon.IsEmpty())
                        ItemsNguonVon.ForAll(item =>
                        {
                            item.FGiaTriUsd = (item.FGiaTriVnd != null && item.FGiaTriVnd.HasValue) ? (item.FGiaTriVnd.Value / FTiGiaNhap.Value) : 0;
                        });
                    if (!ItemsChiPhi.IsEmpty())
                        ItemsChiPhi.ForAll(item =>
                        {
                            item.FGiaTriUsd = (item.FGiaTriVnd != null && item.FGiaTriVnd.HasValue) ? (item.FGiaTriVnd.Value / FTiGiaNhap.Value) : 0;
                        });
                }
            }
        }

        private void DuToanNguonVon_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            NhDaDuToanNguonVonModel objectSender = (NhDaDuToanNguonVonModel)sender;
            if (e.PropertyName.Equals(nameof(NhDaDuToanNguonVonModel.IsDeleted))
                //|| e.PropertyName.Equals(nameof(NhDaDuToanNguonVonModel.FGiaTriNgoaiTeKhac))
                //|| e.PropertyName.Equals(nameof(NhDaDuToanNguonVonModel.FGiaTriEur))
                || e.PropertyName.Equals(nameof(NhDaDuToanNguonVonModel.FGiaTriUsd))
                || e.PropertyName.Equals(nameof(NhDaDuToanNguonVonModel.FGiaTriVnd)))
            {
                if (SelectedTiGia != null && !e.PropertyName.Equals(nameof(NhDaDuToanNguonVonModel.IsDeleted)))
                {
                    //    var listTiGiaChiTiet = _mapper.Map<IEnumerable<NhDmTiGiaChiTiet>>(ItemsTiGiaChiTiet);
                    //    string rootCurrency = SelectedTiGia.SMaTienTeGoc;
                    //    string sourceCurrency;
                    //    string otherCurrency = SelectedTiGiaChiTiet != null ? SelectedTiGiaChiTiet.SMaTienTeQuyDoi : "";
                    //    double value;
                    //    switch (e.PropertyName)
                    //    {
                    //        case nameof(NhDaDuToanNguonVonModel.FGiaTriVnd):
                    //            sourceCurrency = LoaiTienTeEnum.TypeCode.VND;
                    //            value = objectSender.FGiaTriVnd.Value;
                    //            break;
                    //        case nameof(NhDaDuToanNguonVonModel.FGiaTriEur):
                    //            sourceCurrency = LoaiTienTeEnum.TypeCode.EUR;
                    //            value = objectSender.FGiaTriEur.Value;
                    //            break;
                    //        case nameof(NhDaDuToanNguonVonModel.FGiaTriNgoaiTeKhac):
                    //            sourceCurrency = otherCurrency;
                    //            value = objectSender.FGiaTriNgoaiTeKhac.Value;
                    //            break;
                    //        default:
                    //            sourceCurrency = LoaiTienTeEnum.TypeCode.USD;
                    //            value = objectSender.FGiaTriUsd.Value;
                    //            break;
                    //    }
                    //    objectSender.FGiaTriVnd = _nhDmTiGiaService.CurrencyExchange(sourceCurrency, LoaiTienTeEnum.TypeCode.VND, rootCurrency, listTiGiaChiTiet, value);
                    //    objectSender.FGiaTriEur = _nhDmTiGiaService.CurrencyExchange(sourceCurrency, LoaiTienTeEnum.TypeCode.EUR, rootCurrency, listTiGiaChiTiet, value);
                    //    objectSender.FGiaTriUsd = _nhDmTiGiaService.CurrencyExchange(sourceCurrency, LoaiTienTeEnum.TypeCode.USD, rootCurrency, listTiGiaChiTiet, value);
                    //    objectSender.FGiaTriNgoaiTeKhac = _nhDmTiGiaService.CurrencyExchange(sourceCurrency, otherCurrency, rootCurrency, listTiGiaChiTiet, value);
                    //foreach (var item in parents)
                    //{
                    //    if (FTiGiaNhap != null && FTiGiaNhap.HasValue && FTiGiaNhap.Value != 0)
                    //    {
                    //        item.FGiaTriUsd = (item.FGiaTriVnd != null && item.FGiaTriVnd.HasValue) ? (item.FGiaTriVnd.Value / FTiGiaNhap.Value) : 0;
                    //    }
                    //    else
                    //    {
                    //        item.FGiaTriUsd = 0;
                    //    }
                    //}
                    var parents = ItemsDuToanNguonVon.Where(x => x.IID_ParentID == null && !x.IsDeleted);
                    switch (e.PropertyName)
                    {
                        case nameof(NhDaDuToanNguonVonModel.FGiaTriVnd):
                            ChangeValueByRate(parents, null, false);
                            break;
                        case nameof(NhDaDuToanNguonVonModel.FGiaTriUsd):
                            ChangeValueByRate(parents, null, true);
                            break;
                        default:
                            ChangeValueByRate(parents, null, true);
                            break;
                    }
                }
                CalculateNguonVon();
                objectSender.IsModified = true;
            }
            if (e.PropertyName == nameof(NhDaDuToanNguonVonModel.IsSelected))
            {
                if (objectSender.IsSelected)
                {
                    foreach (var item in objectSender.DuToanChiPhis)
                    {
                        if (!IsDetail || item.IsHangCha == false)
                        {
                            item.IsHasChildren = true;
                        }
                        _itemsDuToanChiPhi.Add(item);
                    }
                }
                else
                {
                    foreach (var itemChiPhi in objectSender.DuToanChiPhis)
                    {
                        _itemsDuToanChiPhi.Remove(itemChiPhi);
                    }
                }
                OnPropertyChanged(nameof(ItemsDuToanChiPhi));

                if (ItemsDuToanNguonVon.Where(n => n.IsSelected).Count() > 0)
                {
                    IsAddSpend = true;
                    OnPropertyChanged(nameof(IsAddSpend));
                    SelectedDuToanChiPhi = ItemsDuToanChiPhi.FirstOrDefault();
                    // enable tạo mới dòng con
                    if (ItemsDuToanNguonVon.Count > 0)
                    {
                        if (!IsAddSpendRowChild)
                        {
                            IsAddSpendRowChild = true;
                            OnPropertyChanged(nameof(IsAddSpendRowChild));
                        }
                    }
                    else
                    {
                        if (IsAddSpendRowChild)
                        {
                            IsAddSpendRowChild = false;
                            OnPropertyChanged(nameof(IsAddSpendRowChild));
                        }
                    }
                }
                else
                {
                    IsAddSpend = false;
                    OnPropertyChanged(nameof(IsAddSpend));

                    IsAddSpendRowChild = false;
                    OnPropertyChanged(nameof(IsAddSpendRowChild));
                }
            }
            if (e.PropertyName == nameof(NhDaDuToanNguonVonModel.IIdNguonVonId))
            {
                string tenNguonVon = ItemsNguonVon.FirstOrDefault(x => x.IIdMaNguonNganSach == objectSender.IIdNguonVonId).STen;
                foreach (var chiPhi in objectSender.DuToanChiPhis)
                {
                    chiPhi.STenNguonVon = tenNguonVon;
                }
                SetEnableComboboxItemNguonVon();
            }
        }

        private void DuToanChiPhi_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            NhDaDuToanChiPhiModel objectSender = (NhDaDuToanChiPhiModel)sender;
            if (objectSender.DuToanHangMucs.Where(h => !h.IsDeleted).Count() > 0)
            {
                objectSender.IsEnableEdit = false;
            }
            else
            {
                objectSender.IsEnableEdit = true;
            }
            if (e.PropertyName.Equals(nameof(NhDaDuToanChiPhiModel.IIdChiPhiId)))
            {
                var itemChiPhiDM = ItemsDMChiPhi.FirstOrDefault(x => x.IIdChiPhi == objectSender.IIdChiPhiId && !x.IsDeleted);
                objectSender.STenChiPhi = itemChiPhiDM != null ? itemChiPhiDM.STenChiPhi : string.Empty;
            }

            if (e.PropertyName.Equals(nameof(NhDaDuToanChiPhiModel.IsDeleted))
                || e.PropertyName.Equals(nameof(NhDaDuToanChiPhiModel.DuToanHangMucs))
                //|| e.PropertyName.Equals(nameof(NhDaDuToanChiPhiModel.FGiaTriNgoaiTeKhac))
                //|| e.PropertyName.Equals(nameof(NhDaDuToanChiPhiModel.FGiaTriEur))
                || e.PropertyName.Equals(nameof(NhDaDuToanChiPhiModel.FGiaTriUsd))
                || e.PropertyName.Equals(nameof(NhDaDuToanChiPhiModel.FGiaTriVnd)))
            {
                if (e.PropertyName.Equals(nameof(NhDaDuToanChiPhiModel.DuToanHangMucs)))
                {                    
                    CalculateHangMuc(objectSender);
                }
                else if (SelectedTiGia != null && !e.PropertyName.Equals(nameof(NhDaDuToanChiPhiModel.IsDeleted)))
                {
                    //    var listTiGiaChiTiet = _mapper.Map<IEnumerable<NhDmTiGiaChiTiet>>(ItemsTiGiaChiTiet);
                    //    string rootCurrency = SelectedTiGia.SMaTienTeGoc;
                    //    string sourceCurrency;
                    //    string otherCurrency = SelectedTiGiaChiTiet != null ? SelectedTiGiaChiTiet.SMaTienTeQuyDoi : "";
                    //    double value;
                    //    switch (e.PropertyName)
                    //    {
                    //        case nameof(NhDaDuToanChiPhiModel.FGiaTriVnd):
                    //            sourceCurrency = LoaiTienTeEnum.TypeCode.VND;
                    //            value = objectSender.FGiaTriVnd.Value;
                    //            break;
                    //        case nameof(NhDaDuToanChiPhiModel.FGiaTriEur):
                    //            sourceCurrency = LoaiTienTeEnum.TypeCode.EUR;
                    //            value = objectSender.FGiaTriEur.Value;
                    //            break;
                    //        case nameof(NhDaDuToanChiPhiModel.FGiaTriNgoaiTeKhac):
                    //            sourceCurrency = otherCurrency;
                    //            value = objectSender.FGiaTriNgoaiTeKhac.Value;
                    //            break;
                    //        default:
                    //            sourceCurrency = LoaiTienTeEnum.TypeCode.USD;
                    //            value = objectSender.FGiaTriUsd.Value;
                    //            break;
                    //    }
                    //    objectSender.FGiaTriVnd = _nhDmTiGiaService.CurrencyExchange(sourceCurrency, LoaiTienTeEnum.TypeCode.VND, rootCurrency, listTiGiaChiTiet, value);
                    //    objectSender.FGiaTriEur = _nhDmTiGiaService.CurrencyExchange(sourceCurrency, LoaiTienTeEnum.TypeCode.EUR, rootCurrency, listTiGiaChiTiet, value);
                    //    objectSender.FGiaTriUsd = _nhDmTiGiaService.CurrencyExchange(sourceCurrency, LoaiTienTeEnum.TypeCode.USD, rootCurrency, listTiGiaChiTiet, value);
                    //    objectSender.FGiaTriNgoaiTeKhac = _nhDmTiGiaService.CurrencyExchange(sourceCurrency, otherCurrency, rootCurrency, listTiGiaChiTiet, value);
                    var parents = ItemsDuToanChiPhi;
                    switch (e.PropertyName)
                    {
                        case nameof(NhDaDuToanChiPhiModel.FGiaTriVnd):
                            ChangeValueByRate(null, parents, false);
                            break;
                        case nameof(NhDaDuToanChiPhiModel.FGiaTriUsd):
                            ChangeValueByRate(null, parents, true);
                            break;
                        default:
                            ChangeValueByRate(null, parents, true);
                            break;
                    }
                    //foreach (var item in parents)
                    //{
                    //    if (FTiGiaNhap != null && FTiGiaNhap.HasValue && FTiGiaNhap.Value != 0)
                    //    {
                    //        item.FGiaTriUsd = (item.FGiaTriVnd != null && item.FGiaTriVnd.HasValue) ? (item.FGiaTriVnd.Value / FTiGiaNhap.Value) : 0;
                    //    }
                    //    else
                    //    {
                    //        item.FGiaTriUsd = 0;
                    //    }
                    //}
                }
                var nguonVon = ItemsDuToanNguonVon.Where(n => n.Id == objectSender.IIdDuToanNguonVonId).FirstOrDefault();
                if (nguonVon != null)
                {
                    nguonVon.FGiaTriVnd = ItemsDuToanChiPhi.Where(n => n.IIdDuToanNguonVonId == nguonVon.Id && n.IIdParentId == null && !n.IsDeleted).Sum(n => n.FGiaTriVnd);
                    nguonVon.IsEnableEdit = false; // nguồn vốn bị disable khi có chi phí thuộc nó
                }

                // enable sửa nguồn vốn khi k còn chi phí nào thuộc nguồn vốn đó
                if(ItemsDuToanChiPhi.Where(n => n.IIdDuToanNguonVonId == nguonVon.Id && n.IIdParentId == null && !n.IsDeleted).Count() == 0)
                {
                    nguonVon.IsEnableEdit = true;
                }

                var chiPhi = ItemsDuToanChiPhi.Where(n => n.Id == objectSender.IIdParentId).FirstOrDefault();
                if (chiPhi != null)
                {
                    chiPhi.FGiaTriVnd = ItemsDuToanChiPhi.Where(n => n.IIdParentId == chiPhi.Id && !n.IsDeleted).Sum(n => n.FGiaTriVnd);
                }
            }
            var LstchiPhiNoParent = _itemsDuToanChiPhi.Where(x => x.IIdParentId == null && !x.IsDeleted).ToList();
            if (LstchiPhiNoParent != null)
            {
                foreach (var item in LstchiPhiNoParent)
                {
                    var itemChiPhiDM = ItemsDMChiPhi.Where(x => x.IIdChiPhi == item.IIdChiPhiId && !x.IsDeleted).FirstOrDefault();
                    item.STenChiPhi = itemChiPhiDM != null ? itemChiPhiDM.STenChiPhi : string.Empty;
                }
            }
            CalculateChiPhi();
            objectSender.IsModified = true;
        }

        private void CalculateNguonVon()
        {
            var parents = _itemsDuToanNguonVon.Where(x => x.IID_ParentID == null && !x.IsDeleted);
            //Model.FGiaTriNgoaiTeKhac = _itemsDuToanNguonVon.Where(x => !x.IsDeleted).Sum(x => x.FGiaTriNgoaiTeKhac);
            FGiaTriUsd = parents.Sum(x => x.FGiaTriUsd);
            //Model.FGiaTriEur = _itemsDuToanNguonVon.Where(x => !x.IsDeleted).Sum(x => x.FGiaTriEur);
            FGiaTriVnd = parents.Sum(x => x.FGiaTriVnd);
            foreach (var item in parents)
            {
                if (FTiGiaNhap != null && FTiGiaNhap.HasValue && FTiGiaNhap.Value != 0)
                {
                    item.FGiaTriUsd = (item.FGiaTriVnd != null && item.FGiaTriVnd.HasValue) ? (item.FGiaTriVnd.Value / FTiGiaNhap.Value) : 0;
                }
                else
                {
                    item.FGiaTriUsd = 0;
                }
            }
            //Model.FGiaTriConLaiUsd = (Model.FGiaTriUsd ?? 0) - (Model.FGiaTriChiPhiUsd ?? 0);
            //Model.FGiaTriConLaiEur = (Model.FGiaTriEur ?? 0) - (Model.FGiaTriChiPhiEur ?? 0);
            //Model.FGiaTriConLaiVnd = (Model.FGiaTriVnd ?? 0) - (Model.FGiaTriChiPhiVnd ?? 0);
            //Model.FGiaTriConLaiNgoaiTeKhac = (Model.FGiaTriNgoaiTeKhac ?? 0) - (Model.FGiaTriChiPhiNgoaiTeKhac ?? 0);

            OnPropertyChanged(nameof(FGiaTriUsd));
            OnPropertyChanged(nameof(FGiaTriVnd));
        }

        private void CalculateChiPhi()
        {
            //var parents = _itemsDuToanNguonVon.Where(x => x.IID_ParentID == null && !x.IsDeleted);
            //foreach (var item in parents)
            //{
            //    if (FTiGiaNhap != null && FTiGiaNhap.HasValue && FTiGiaNhap.Value != 0)
            //    {
            //        item.FGiaTriUsd = (item.FGiaTriVnd != null && item.FGiaTriVnd.HasValue) ? (item.FGiaTriVnd.Value / FTiGiaNhap.Value) : 0;
            //    }
            //    else
            //    {
            //        item.FGiaTriUsd = 0;
            //    }
            //}
            var parentss = _itemsDuToanChiPhi.Where(x => !x.IsDeleted && x.IIdParentId == null);
            foreach (var item in parentss)
            {
                if (FTiGiaNhap != null && FTiGiaNhap.HasValue && FTiGiaNhap.Value != 0)
                {
                    item.FGiaTriUsd = (item.FGiaTriVnd != null && item.FGiaTriVnd.HasValue) ? (item.FGiaTriVnd.Value / FTiGiaNhap.Value) : 0;
                    Model.FGiaTriChiPhiUsd = Model.FGiaTriChiPhiVnd = (Model.FGiaTriChiPhiVnd != null && Model.FGiaTriChiPhiVnd.HasValue) ? (Model.FGiaTriChiPhiVnd.Value / FTiGiaNhap.Value) : 0;
                }
                else
                {
                    item.FGiaTriUsd = 0;
                }
            }
            //Model.FGiaTriChiPhiNgoaiTeKhac = _itemsDuToanChiPhi.Where(x => !x.IsDeleted && x.IIdParentId == null).Sum(x => x.FGiaTriNgoaiTeKhac);
            Model.FGiaTriChiPhiUsd = _itemsDuToanChiPhi.Where(x => !x.IsDeleted && x.IIdParentId == null).Sum(x => x.FGiaTriUsd);
            FSumGiaTriChiPhiUsd = Model.FGiaTriChiPhiUsd;
            //Model.FGiaTriChiPhiEur = _itemsDuToanChiPhi.Where(x => !x.IsDeleted && x.IIdParentId == null).Sum(x => x.FGiaTriEur);
            Model.FGiaTriChiPhiVnd = _itemsDuToanChiPhi.Where(x => !x.IsDeleted && x.IIdParentId == null).Sum(x => x.FGiaTriVnd);
            FSumGiaTriChiPhiVnd = Model.FGiaTriChiPhiVnd;
            Model.FGiaTriConLaiUsd = (Model.FGiaTriUsd ?? 0) - (Model.FGiaTriChiPhiUsd ?? 0);
            //Model.FGiaTriConLaiEur = (Model.FGiaTriEur ?? 0) - (Model.FGiaTriChiPhiEur ?? 0);
            Model.FGiaTriConLaiVnd = (Model.FGiaTriVnd ?? 0) - (Model.FGiaTriChiPhiVnd ?? 0);
            //Model.FGiaTriConLaiNgoaiTeKhac = (Model.FGiaTriNgoaiTeKhac ?? 0) - (Model.FGiaTriChiPhiNgoaiTeKhac ?? 0);
            //Thay đổi nguồn vốn theo chi phí
            //Model.FGiaTriNgoaiTeKhac = _itemsDuToanChiPhi.Where(x => !x.IsDeleted && x.IIdParentId == null).Sum(x => x.FGiaTriNgoaiTeKhac);
            Model.FGiaTriUsd = _itemsDuToanChiPhi.Where(x => !x.IsDeleted && x.IIdParentId == null).Sum(x => x.FGiaTriUsd);
            //Model.FGiaTriEur = _itemsDuToanChiPhi.Where(x => !x.IsDeleted && x.IIdParentId == null).Sum(x => x.FGiaTriEur);
            Model.FGiaTriVnd = _itemsDuToanChiPhi.Where(x => !x.IsDeleted && x.IIdParentId == null).Sum(x => x.FGiaTriVnd);
            Model.FGiaTriConLaiUsd = (Model.FGiaTriUsd ?? 0) - (Model.FGiaTriChiPhiUsd ?? 0);
            //Model.FGiaTriConLaiEur = (Model.FGiaTriEur ?? 0) - (Model.FGiaTriChiPhiEur ?? 0);
            Model.FGiaTriConLaiVnd = (Model.FGiaTriVnd ?? 0) - (Model.FGiaTriChiPhiVnd ?? 0);
            //Model.FGiaTriConLaiNgoaiTeKhac = (Model.FGiaTriNgoaiTeKhac ?? 0) - (Model.FGiaTriChiPhiNgoaiTeKhac ?? 0);
            OnPropertyChanged(nameof(Model));
        }

        private void CalculateHangMuc(NhDaDuToanChiPhiModel item)
        {
            if (item != null && !item.DuToanHangMucs.IsEmpty())
            {
                var parents = ItemsDuToanChiPhi;
                foreach (var items in parents)
                {
                    if (FTiGiaNhap != null && FTiGiaNhap.HasValue && FTiGiaNhap.Value != 0)
                    {
                        items.FGiaTriUsd = (item.FGiaTriVnd != null && items.FGiaTriVnd.HasValue) ? (items.FGiaTriVnd.Value / FTiGiaNhap.Value) : 0;
                    }
                    else
                    {
                        items.FGiaTriUsd = 0;
                    }
                }
                //item.FGiaTriNgoaiTeKhac = item.DuToanHangMucs.Where(x => !x.IsDeleted && x.IIdParentId == null).Sum(x => x.FGiaTriNgoaiTeKhac);
                item.FGiaTriUsd = item.DuToanHangMucs.Where(x => !x.IsDeleted && x.IIdParentId == null).Sum(x => x.FGiaTriUsd);
                //item.FGiaTriEur = item.DuToanHangMucs.Where(x => !x.IsDeleted && x.IIdParentId == null).Sum(x => x.FGiaTriEur);
                item.FGiaTriVnd = item.DuToanHangMucs.Where(x => !x.IsDeleted && x.IIdParentId == null).Sum(x => x.FGiaTriVnd);
            }
        }

        private bool ValiDateData()
        {
            if (string.IsNullOrEmpty(Model.SSoQuyetDinh))
            {
                MessageBoxHelper.Error(Resources.MsgCheckSoQD);
                return false;
            }
            else if (_service.CheckDuplicateSoQD(Model.SSoQuyetDinh, Model.Id))
            {
                MessageBoxHelper.Error(Resources.MsgTrungSoQD);
                return false;
            }
            if (Model.DNgayQuyetDinh == null)
            {
                MessageBoxHelper.Error(Resources.MsgCheckNgayPheDuyet);
                return false;
            }
            if (SelectedDonVi == null)
            {
                MessageBoxHelper.Error(Resources.MsgCheckDonVi);
                return false;
            }
            if (SelectedDuAn == null && IsShowDuAn)
            {
                MessageBoxHelper.Error(Resources.MsgCheckDuAn);
                return false;
            }
            if (SelectedLoaiDuToan == null)
            {
                MessageBoxHelper.Error(Resources.MsgErrorRequire, "Vui lòng chọn loại dự toán");
                return false;
            }
            //if (ItemsDuToanNguonVon.Count > 0)
            //{
            //    var tongNguonVonUSD = ItemsDuToanNguonVon.Where(y => !y.IsDeleted).Sum(x => x.FGiaTriUSDQDDTPheDuyet);
            //    var tongNguonVonVND = ItemsDuToanNguonVon.Where(y => !y.IsDeleted).Sum(x => x.FGiaTriVNDQDDTPheDuyet);
            //    var tongNguonVonEUR = ItemsDuToanNguonVon.Where(y => !y.IsDeleted).Sum(x => x.FGiaTriEURQDDTPheDuyet);
            //    var tongNguonVonNgoaiTeKhac = ItemsDuToanNguonVon.Where(y => !y.IsDeleted).Sum(x => x.FGiaTriNgoaiTeKhacQDDTPheDuyet);

            //    if (!IsDieuChinhTKTC &&
            //        (Model.FGiaTriUSD > tongNguonVonUSD || Model.FGiaTriVND > tongNguonVonVND || Model.FGiaTriEUR > tongNguonVonEUR || Model.FGiaTriNgoaiTeKhac > tongNguonVonNgoaiTeKhac))
            //    {
            //        System.Windows.Forms.MessageBox.Show(Resources.ErrorNguonVonNotEqualPheDuyet, Resources.NotifiTitle, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //        return false;
            //    }
            //}

            //if (!IsDieuChinhTKTC && Model.FTongDuToanPheDuyet != null && ((Model.FTongDuToanPheDuyet + TongDuToanByDuAn) > TongGiaTriPheDuyetDuAn))
            //{
            //    System.Windows.Forms.MessageBox.Show(Resources.MsgTongDuToanLonHonTongMucDauTu, Resources.NotifiTitle, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //    return false;
            //}
            return true;
        }

        private void OrderItems(Guid? parentId = null)
        {
            var childs = _itemsDuToanChiPhi.Where(x => x.IIdParentId == parentId);
            if (!childs.IsEmpty())
            {
                var parent = _itemsDuToanChiPhi.FirstOrDefault(x => x.Id == parentId);
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
                    child.SMaChiPhi = StringUtils.ConvertMaOrder(child.SMaOrder);
                    OrderItems(child.Id);
                    index++;
                }
            }
        }

        private int CountTreeChildItems(NhDaDuToanChiPhiModel currentItem)
        {
            var items = _itemsDuToanChiPhi;
            int count = 0;
            var childs = items.Where(x => x.IIdParentId == currentItem.Id);
            if (!childs.IsEmpty())
            {
                count += childs.Count();
                foreach (var item in childs)
                {
                    count += CountTreeChildItems(item);
                }
            }
            return count;
        }

        private void DeleteTreeItems(NhDaDuToanChiPhiModel currentItem, bool status)
        {
            if (currentItem != null)
            {
                var items = _itemsDuToanChiPhi;
                currentItem.IsDeleted = status;
                var childs = items.Where(x => x.IIdParentId == currentItem.Id);
                if (!childs.IsEmpty())
                {
                    foreach (var item in childs)
                    {
                        DeleteTreeItems(item, status);
                    }
                }
            }
        }

        private static void SelectAll(bool select, IEnumerable<ModelBase> models)
        {
            foreach (var model in models)
            {
                model.IsSelected = select;
            }
        }
        private void LoadDanhMucChiPhi()
        {
            IEnumerable<NhDmChiPhi> data = _nhDmChiPhiService.FindAll();
            _itemsDMChiPhi = _mapper.Map<ObservableCollection<NhDmChiPhiModel>>(data);

            OnPropertyChanged(nameof(ItemsDMChiPhi));
        }
    }
}
