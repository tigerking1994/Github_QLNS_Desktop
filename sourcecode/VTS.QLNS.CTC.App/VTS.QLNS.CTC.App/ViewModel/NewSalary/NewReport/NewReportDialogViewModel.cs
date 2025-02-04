using AutoMapper;
using log4net;
using Microsoft.Extensions.Configuration;
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Linq;
using System.Windows.Data;
using VTS.QLNS.CTC.App.Command;
using VTS.QLNS.CTC.App.Helper;
using VTS.QLNS.CTC.App.Model;
using VTS.QLNS.CTC.App.Model.Control;
using VTS.QLNS.CTC.App.Model.Report;
using VTS.QLNS.CTC.App.Properties;
using VTS.QLNS.CTC.App.Service;
using VTS.QLNS.CTC.App.Service.Impl;
using VTS.QLNS.CTC.App.Service.UserFunction;
using VTS.QLNS.CTC.App.View.NewSalary.NewReport;
using VTS.QLNS.CTC.App.ViewModel.Category;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Service;
using VTS.QLNS.CTC.Utility;
using VTS.QLNS.CTC.Utility.Enum;

namespace VTS.QLNS.CTC.App.ViewModel.NewSalary.NewReport
{
    public class NewReportDialogViewModel : DialogViewModelBase<TlBaoCaoModel>
    {
        private readonly IMapper _mapper;
        private readonly ILog _logger;
        private readonly ISessionService _sessionService;
        private readonly ITlDmDonViNq104Service _tlDmDonViService;
        private readonly IExportService _exportService;
        private readonly ITlCanBoPhuCapBridgeNq104Service _tlCanBoPhuCapBridgeNq104Service;
        private readonly ITlBangLuongThangBridgeNq104Service _tlBangLuongBridgeService;
        private readonly ITlBangLuongThangNq104Service _tlBangLuongThangService;
        private readonly ITlDsCapNhapBangLuongNq104Service _tlDsCapNhapBangLuongService;
        private readonly ITlDmCanBoNq104Service _tlDmCanboService;
        private readonly ITlDmCapBacService _iTlDmCapBacService;
        private readonly ITlDmCapBacNq104Service _iTlDmCapBacNq104Service;
        private readonly ITlDmPhuCapNq104Service _iTlDmPhuCapService;
        private readonly IDmChuKyService _iDmChuKyService;
        private readonly IDanhMucService _iDanhMucService;
        private readonly INsDonViService _donViService;
        private readonly ITlDieuChinhQsKeHoachNq104Service _tlDieuChinhQsKeHoachService;
        private readonly ITlDmCanBoKeHoachNq104Service _tlDmCanBoKeHoachService;
        private readonly ITlDmCachTinhLuongTruyLinhNq104Service _iDmCachTinhLuongTruyLinhService;
        private readonly IServiceProvider _serviceProvider;
        private readonly IConfiguration _configuration;
        private readonly ITlDmCachTinhLuongChuanNq104Service _tlDmCachTinhLuongChuanService;
        private readonly IDanhMucService _danhMucService;
        private readonly ITlQsChungTuNq104Service _tlQsChungTuService;
        private readonly ITlQuanLyThuNopBhxhChiTietService _tlThuNopBhxhChiTietService;
        private readonly ITlQuanLyThuNopBhxhService _tlThuNopBhxhService;
        private SessionInfo _sessionInfo;
        private ICollectionView _dataDonViView;
        private ICollectionView _dataPhuCapView;
        private ICollectionView _dataPhuCapTongLuongView;
        private ICollectionView _dataPhuCapPhaiTruView;
        private ICollectionView _dataYeuToLuongDocView;
        private DmChuKy _dmChuKy;
        private string _appSettingConfigPath;
        private string _typeChuky;
        public string TypeChuky
        {
            get => _typeChuky;
            set => SetProperty(ref _typeChuky, value);
        }
        private string _diaDiem;

        private static readonly Dictionary<string, string> _mapPhuCap = new Dictionary<string, string>()
        {
            {PhuCap.LHT_TT, "Tiền lương" },
            {PhuCap.PCCV_TT, "Phụ cấp chức vụ" },
            {PhuCap.PCTRA_SUM, "Phụ cấp trách nhiệm" },
            {PhuCap.PCTN_TT, "Phụ cấp thâm niên" },
            {PhuCap.PCTNVK_TT, "Phụ cấp thâm niên vượt khung" },
            {PhuCap.PCDACTHU_SUM, "Phụ cấp đặc thù" },
            {PhuCap.PCKIE_TT, "Phụ cấp kiêm nhiệm" },
            {PhuCap.PCCOV_TT, "Phụ cấp công vụ" },
            {PhuCap.HSBL_TT, "Bảo lưu" },
        };

        public override string FuncCode => NSFunctionCode.NEW_SALARY_MANAGEMENT_REPORTS_DIALOG;
        public override Type ContentType => typeof(NewReportDialog);
        public override string Title => Model.TenBaoCao;
        public override string Description => Model.TenBaoCao;
        public bool IsShowYeuToLuong => LoaiBaoCao == BaoCaoLuong.GTCTTL;
        public bool IsShowLoaiTongHop => (LoaiBaoCao == BaoCaoLuong.DSCPL_A4 || LoaiBaoCao == BaoCaoLuong.DSCTCN) && IsSummary;
        public bool IsShowMaHuongLuong => LoaiBaoCao == BaoCaoLuong.DSCPL_A4;
        private BaoCaoLuong _loaiBaoCao;
        public string MaDonVi { get; set; }
        public BaoCaoLuong LoaiBaoCao
        {
            get => _loaiBaoCao;
            set
            {
                if (SetProperty(ref _loaiBaoCao, value))
                {
                    OnPropertyChanged(nameof(IsShowPhuCap));
                    OnPropertyChanged(nameof(IsShowKichThuocIn));
                    OnPropertyChanged(nameof(IsShowYeuToLuong));
                    OnPropertyChanged(nameof(IsChiTietQuanSo));
                    OnPropertyChanged(nameof(IsShowInBoSungHSQCS));
                    OnPropertyChanged(nameof(IsShowInHuongNuocNgoai));
                }
            }
        }

        private List<ComboboxItem> _itemsMonth;
        public List<ComboboxItem> ItemsMonth
        {
            get => _itemsMonth;
            set => SetProperty(ref _itemsMonth, value);
        }

        private ComboboxItem _selectedMonth;
        public ComboboxItem SelectedMonth
        {
            get => _selectedMonth;
            set
            {
                SetProperty(ref _selectedMonth, value);
                LoadDonViData();
                LoadPhuCapData();
            }
        }

        private List<ComboboxItem> _monthQuarters;
        public List<ComboboxItem> MonthQuarters
        {
            get => _monthQuarters;
            set => SetProperty(ref _monthQuarters, value);
        }

        private ComboboxItem _selectedMonthQuarter;
        public ComboboxItem SelectedMonthQuarter
        {
            get => _selectedMonthQuarter;
            set
            {
                SetProperty(ref _selectedMonthQuarter, value);
                LoadDonViData();
            }
        }

        private List<ComboboxItem> _itemsYear;
        public List<ComboboxItem> ItemsYear
        {
            get => _itemsYear;
            set => SetProperty(ref _itemsYear, value);
        }

        private ComboboxItem _selectedYear;
        public ComboboxItem SelectedYear
        {
            get => _selectedYear;
            set
            {
                SetProperty(ref _selectedYear, value);
                LoadDonViData();
                LoadPhuCapData();
            }
        }

        private List<ComboboxItem> _itemsFileExport;
        public List<ComboboxItem> ItemsFileExport
        {
            get => _itemsFileExport;
            set => SetProperty(ref _itemsFileExport, value);
        }

        private ComboboxItem _selectedFileExport;
        public ComboboxItem SelectedFileExport
        {
            get => _selectedFileExport;
            set => SetProperty(ref _selectedFileExport, value);
        }

        private ObservableCollection<TlDmDonViNq104Model> _itemsDonVi;
        public ObservableCollection<TlDmDonViNq104Model> ItemsDonVi
        {
            get => _itemsDonVi;
            set => SetProperty(ref _itemsDonVi, value);
        }

        private TlDmDonViNq104Model _selectedDonVi;
        public TlDmDonViNq104Model SelectedDonVi
        {
            get => _selectedDonVi;
            set
            {
                SetProperty(ref _selectedDonVi, value);
            }
        }

        private string _searchDonVi;
        public string SearchDonVi
        {
            get => _searchDonVi;
            set
            {
                if (SetProperty(ref _searchDonVi, value) && _dataDonViView != null)
                {
                    _dataDonViView.Refresh();
                }
            }
        }

        private string _searchPhuCap;
        public string SearchPhuCap
        {
            get => _searchPhuCap;
            set
            {
                if (SetProperty(ref _searchPhuCap, value) && _dataPhuCapView != null)
                {
                    _dataPhuCapView.Refresh();
                }
            }
        }

        private bool _selectAllAgency;
        public bool SelectAllAgency
        {
            get => ItemsDonVi.All(item => item.IsSelected);
            set
            {
                SetProperty(ref _selectAllAgency, value);
                foreach (var item in ItemsDonVi)
                    item.IsSelected = _selectAllAgency;
            }
        }

        private bool _bIsShowNoiDung;
        public bool BIsShowNoiDung
        {
            get => _bIsShowNoiDung;
            set => SetProperty(ref _bIsShowNoiDung, value);
        }

        private string _sNoiDung;
        public string SNoiDung
        {
            get => _sNoiDung;
            set => SetProperty(ref _sNoiDung, value);
        }

        private string _reportName;
        public string ReportName
        {
            get => _reportName;
            set => SetProperty(ref _reportName, value);
        }

        public string LabelSelectedCountAgency
        {
            get
            {
                var totalCount = ItemsDonVi.Count;
                var totalSelected = ItemsDonVi.Count(item => item.IsSelected);
                return $"ĐƠN VỊ ({totalSelected}/{totalCount})";
            }
        }

        #region Param Index ViewModel
        private List<string> _lstMaDonViIndexViewSelected;
        public List<string> LstMaDonViIndexViewSelected
        {
            get => _lstMaDonViIndexViewSelected;
            set => SetProperty(ref _lstMaDonViIndexViewSelected, value);
        }
        #endregion

        private bool _selectAllPhuCap;
        public bool SelectAllPhuCap
        {
            get => ItemsPhuCap != null && ItemsPhuCap.All(item => item.IsSelected);
            set
            {
                SetProperty(ref _selectAllPhuCap, value);
                foreach (var item in ItemsPhuCap)
                    item.IsSelected = _selectAllPhuCap;
            }
        }

        public string LabelSelectedCountPhuCap
        {
            get
            {
                if (ItemsPhuCap != null)
                {
                    var totalCount = ItemsPhuCap.Count;
                    var totalSelected = ItemsPhuCap.Count(item => item.IsSelected);
                    return $"PHỤ CẤP ({totalSelected}/{totalCount})";
                }
                return string.Empty;
            }
        }

        public string LabelSelectedCountPhuCapBcCauHinhDong
        {
            get
            {
                if (ItemsPhuCap != null)
                {
                    var totalCount = ItemsPhuCap.Count;
                    var totalSelected = ItemsPhuCap.Count(item => item.IsSelected);
                    return $"CHỌN PHỤ CẤP THÊM ({totalSelected}/{totalCount})";
                }
                return string.Empty;
            }
        }

        public string LabelSelectedCountPhuCapBotBcCauHinhDong
        {
            get
            {
                if (ItemsPhuCapBot != null)
                {
                    var totalCount = ItemsPhuCapBot.Count;
                    var totalSelected = ItemsPhuCapBot.Count(item => item.IsSelected);
                    return $"CHỌN PHỤ CẤP BỚT ({totalSelected}/{totalCount})";
                }
                return string.Empty;
            }
        }

        private ObservableCollection<TlDmPhuCapNq104Model> _itemsPhuCap;
        public ObservableCollection<TlDmPhuCapNq104Model> ItemsPhuCap
        {
            get => _itemsPhuCap;
            set => SetProperty(ref _itemsPhuCap, value);
        }

        private ObservableCollection<TlDmPhuCapNq104Model> _itemsPhuCapBot;
        public ObservableCollection<TlDmPhuCapNq104Model> ItemsPhuCapBot
        {
            get => _itemsPhuCapBot;
            set => SetProperty(ref _itemsPhuCapBot, value);
        }

        private bool _selectAllPhuCapTongLuong;
        public bool SelectAllPhuCapTongLuong
        {
            get => ItemsPhuCapTongLuong != null && ItemsPhuCapTongLuong.All(item => item.IsSelected);
            set
            {
                SetProperty(ref _selectAllPhuCapTongLuong, value);
                foreach (var item in ItemsPhuCapTongLuong)
                    item.IsSelected = _selectAllPhuCapTongLuong;
            }
        }

        public string LabelSelectedCountPhuCapTongLuong
        {
            get
            {
                if (ItemsPhuCapTongLuong != null)
                {
                    var totalCount = ItemsPhuCapTongLuong.Count;
                    var totalSelected = ItemsPhuCapTongLuong.Count(item => item.IsSelected);
                    return $"PHỤ CẤP ({totalSelected}/{totalCount})";
                }

                return "";
            }
        }

        private ObservableCollection<TlDmPhuCapNq104Model> _itemsPhuCapTongLuong;
        public ObservableCollection<TlDmPhuCapNq104Model> ItemsPhuCapTongLuong
        {
            get => _itemsPhuCapTongLuong;
            set => SetProperty(ref _itemsPhuCapTongLuong, value);
        }

        private bool _selectAllPhuCapPhaiTru;
        public bool SelectAllPhuCapPhaiTru
        {
            get => ItemsPhuCapPhaiTru != null && ItemsPhuCapPhaiTru.All(item => item.IsSelected);
            set
            {
                SetProperty(ref _selectAllPhuCapPhaiTru, value);
                foreach (var item in ItemsPhuCapPhaiTru)
                    item.IsSelected = _selectAllPhuCapPhaiTru;
            }
        }

        public string LabelSelectedCountPhuCapPhaiTru
        {
            get
            {
                if (ItemsPhuCapPhaiTru != null)
                {
                    var totalCount = ItemsPhuCapPhaiTru.Count;
                    var totalSelected = ItemsPhuCapPhaiTru.Count(item => item.IsSelected);
                    return $"PHỤ CẤP PHẢI TRỪ ({totalSelected}/{totalCount})";
                }
                return "";
            }
        }

        private ObservableCollection<TlDmPhuCapNq104Model> _itemsPhuCapPhaiTru;
        public ObservableCollection<TlDmPhuCapNq104Model> ItemsPhuCapPhaiTru
        {
            get => _itemsPhuCapPhaiTru;
            set => SetProperty(ref _itemsPhuCapPhaiTru, value);
        }

        private string _searchYeuToLuongDoc;
        public string SearchYeuToLuongDoc
        {
            get => _searchYeuToLuongDoc;
            set
            {
                if (SetProperty(ref _searchYeuToLuongDoc, value) && _dataYeuToLuongDocView != null)
                {
                    _dataYeuToLuongDocView.Refresh();
                }
            }
        }

        private ObservableCollection<TlDmPhuCapNq104Model> _itemsYeuToLuong;
        public ObservableCollection<TlDmPhuCapNq104Model> ItemsYeuToLuong
        {
            get => _itemsYeuToLuong;
            set => SetProperty(ref _itemsYeuToLuong, value);
        }

        private ObservableCollection<TlDmPhuCapNq104Model> _itemsYeuToLuongDoc;
        public ObservableCollection<TlDmPhuCapNq104Model> ItemsYeuToLuongDoc
        {
            get => _itemsYeuToLuongDoc;
            set => SetProperty(ref _itemsYeuToLuongDoc, value);
        }

        private TlDmPhuCapNq104Model _selectedYeuToluong;
        public TlDmPhuCapNq104Model SelectedYeuToluong
        {
            get => _selectedYeuToluong;
            set => SetProperty(ref _selectedYeuToluong, value);
        }

        private TlDmPhuCapNq104Model _selectedYeuToluongDoc;
        public TlDmPhuCapNq104Model SelectedYeuToluongDoc
        {
            get => _selectedYeuToluongDoc;
            set => SetProperty(ref _selectedYeuToluongDoc, value);
        }

        private bool _isChecked;
        public bool IsChecked
        {
            get => _isChecked;
            set => SetProperty(ref _isChecked, value);
        }

        private bool _isSummary;
        public bool IsSummary
        {
            get => _isSummary;
            set
            {
                SetProperty(ref _isSummary, value);
                OnPropertyChanged(nameof(IsShowLoaiTongHop));
                if (LoaiBaoCao == BaoCaoLuong.THQSQT || LoaiBaoCao == BaoCaoLuong.QTQS)
                {
                    LoadDonViData();
                }
            }
        }

        private bool _isSummaryNew;
        public bool IsSummaryNew
        {
            get => _isSummaryNew;
            set => SetProperty(ref _isSummaryNew, value);
        }

        private bool _isExportAllCadres;
        public bool IsExportAllCadres
        {
            get => _isExportAllCadres;
            set => SetProperty(ref _isExportAllCadres, value);
        }

        private ObservableCollection<ComboboxItem> _itemsUnitTypes = new ObservableCollection<ComboboxItem>();
        public ObservableCollection<ComboboxItem> ItemsUnitType
        {
            get => _itemsUnitTypes;
            set => SetProperty(ref _itemsUnitTypes, value);
        }

        private ComboboxItem _selectedUnitType;
        public ComboboxItem SelectedUnitType
        {
            get => _selectedUnitType;
            set => SetProperty(ref _selectedUnitType, value);
        }

        private ObservableCollection<ComboboxItem> _itemsLoaiTongHop = new ObservableCollection<ComboboxItem>();
        public ObservableCollection<ComboboxItem> ItemsLoaiTongHop
        {
            get => _itemsLoaiTongHop;
            set => SetProperty(ref _itemsLoaiTongHop, value);
        }

        private ComboboxItem _selectedLoaiTongHop;
        public ComboboxItem SelectedLoaiTongHop
        {
            get => _selectedLoaiTongHop;
            set => SetProperty(ref _selectedLoaiTongHop, value);
        }

        private List<ComboboxItem> _itemsKhoIn;
        public List<ComboboxItem> ItemsKhoIn
        {
            get => _itemsKhoIn;
            set => SetProperty(ref _itemsKhoIn, value);
        }

        private ComboboxItem _selectedKhoIn;
        public ComboboxItem SelectedKhoIn
        {
            get => _selectedKhoIn;
            set => SetProperty(ref _selectedKhoIn, value);
        }

        private bool _isCheckedYeuToLuong;
        public bool IsCheckedYeuToLuong
        {
            get => _isCheckedYeuToLuong;
            set
            {
                SetProperty(ref _isCheckedYeuToLuong, value);
                OnPropertyChanged(nameof(IsShowGridYeuToLuong));
            }
        }

        private bool _isHuongNN;
        public bool IsHuongNN
        {
            get => _isHuongNN;
            set
            {
                SetProperty(ref _isHuongNN, value);
            }
        }

        private bool _isCheckedMaHuongLuong;
        public bool IsCheckedMaHuongLuong
        {
            get => _isCheckedMaHuongLuong;
            set
            {
                SetProperty(ref _isCheckedMaHuongLuong, value);
            }
        }

        private bool _isCheckedInChieuDoc;
        public bool IsCheckedInChieuDoc
        {
            get => _isCheckedInChieuDoc;
            set
            {
                SetProperty(ref _isCheckedInChieuDoc, value);
                LoadYeuToLuongDoc();
                OnPropertyChanged(nameof(IsShowGridYeuToLuong));
            }
        }

        private ObservableCollection<ComboboxItem> _itemsGrades;
        public ObservableCollection<ComboboxItem> ItemsGrades
        {
            get => _itemsGrades;
            set => SetProperty(ref _itemsGrades, value);
        }

        private ComboboxItem _selectedGrade;
        public ComboboxItem SelectedGrade
        {
            get => _selectedGrade;
            set => SetProperty(ref _selectedGrade, value);
        }

        //private ObservableCollection<ComboboxItem> _itemsLoaiPhuCap;
        //public ObservableCollection<ComboboxItem> ItemsLoaiPhuCap
        //{
        //    get => _itemsLoaiPhuCap;
        //    set => SetProperty(ref _itemsLoaiPhuCap, value);
        //}

        //private ComboboxItem _selectedLoaiPhuCap;
        //public ComboboxItem SelectedLoaiPhuCap
        //{
        //    get => _selectedLoaiPhuCap;
        //    set
        //    {
        //        SetProperty(ref _selectedLoaiPhuCap, value);
        //        CheckPhuCapTheoLoai();
        //    }
        //}

        private ObservableCollection<ComboboxItem> _itemsDinhDangPhuCap;
        public ObservableCollection<ComboboxItem> ItemsDinhDangPhuCap
        {
            get => _itemsDinhDangPhuCap;
            set => SetProperty(ref _itemsDinhDangPhuCap, value);
        }

        private ComboboxItem _selectedDinhDangPhuCap;
        public ComboboxItem SelectedDinhDangPhuCap
        {
            get => _selectedDinhDangPhuCap;
            set
            {
                SetProperty(ref _selectedDinhDangPhuCap, value);
                CheckPhuCapTheoLoai();
            }
        }

        private ObservableCollection<ComboboxItem> _itemsQuanSo;
        public ObservableCollection<ComboboxItem> ItemsQuanSo
        {
            get => _itemsQuanSo;
            set => SetProperty(ref _itemsQuanSo, value);
        }

        private ComboboxItem _selectedLoaiQuanSo;
        public ComboboxItem SelectedLoaiQuanSo
        {
            get => _selectedLoaiQuanSo;
            set => SetProperty(ref _selectedLoaiQuanSo, value);
        }

        public bool IsShowInHuongNuocNgoai => (LoaiBaoCao == BaoCaoLuong.DSCPL_A4 || LoaiBaoCao == BaoCaoLuong.DSCPPC);
        public bool IsShowInBoSungHSQCS => !(LoaiBaoCao == BaoCaoLuong.DSCPPC || LoaiBaoCao == BaoCaoLuong.GTCTPC_HQS || LoaiBaoCao == BaoCaoLuong.GTCTRQXN || LoaiBaoCao == BaoCaoLuong.THQSQT || LoaiBaoCao == BaoCaoLuong.QTQS);
        public bool IsShowInTruTroCapBHXH => !(LoaiBaoCao == BaoCaoLuong.THQSQT || LoaiBaoCao == BaoCaoLuong.QTQS);
        public bool IsShowComboboxThang
        {
            get
            {
                return LoaiBaoCao != BaoCaoLuong.QTTTNCN && LoaiBaoCao != BaoCaoLuong.DCQSNKH && LoaiBaoCao != BaoCaoLuong.CTQSRQ
                    && LoaiBaoCao != BaoCaoLuong.CTQSNH && LoaiBaoCao != BaoCaoLuong.THLNKH && LoaiBaoCao != BaoCaoLuong.CTLNKH
                    && LoaiBaoCao != BaoCaoLuong.BDQHKE && LoaiBaoCao != BaoCaoLuong.LKHMLNS && LoaiBaoCao != BaoCaoLuong.QTQS;
            }
        }

        public bool IsShowComboboxThoiGian
        {
            get
            {
                return LoaiBaoCao == BaoCaoLuong.QTQS;
            }
        }

        public bool IsThuNopBhxh { get; set; }
        public bool IsShowCheckBoxGiaGriAm
        {
            get
            {
                return LoaiBaoCao == BaoCaoLuong.DSCPL_A4 && !IsShowBangBcLuongDong && IsThuNopBhxh == false;
            }
        }

        public string LoaiPhuCap
        {
            get
            {
                if (LoaiBaoCao == BaoCaoLuong.GTCTPC_TX)
                {
                    return "1";
                }
                else if (LoaiBaoCao == BaoCaoLuong.GTCTPC_NV)
                {
                    return "2";
                }
                else if (LoaiBaoCao == BaoCaoLuong.GTCTPC_TNK)
                {
                    return "3";
                }
                else if (LoaiBaoCao == BaoCaoLuong.GTCTPC_GTK)
                {
                    return "4";
                }
                return "";
            }
        }

        public bool IsShowBangBcLuongDong => Model.MaBaoCao == "1.19" ? true : false;

        public bool IsShowKichThuocIn => LoaiBaoCao == BaoCaoLuong.QTQS || LoaiBaoCao == BaoCaoLuong.GTCTTL
            || LoaiBaoCao == BaoCaoLuong.DSCPL_A4 || LoaiBaoCao == BaoCaoLuong.GTCTPC_TX || LoaiBaoCao == BaoCaoLuong.GTCTPC_NV
            || LoaiBaoCao == BaoCaoLuong.GTCTPC_TNK || LoaiBaoCao == BaoCaoLuong.GTCTPC_GTK || LoaiBaoCao == BaoCaoLuong.THLPC_DV
            || LoaiBaoCao == BaoCaoLuong.THLPC_N_DV || LoaiBaoCao == BaoCaoLuong.TTNCN || LoaiBaoCao == BaoCaoLuong.THLPC
            || LoaiBaoCao == BaoCaoLuong.QTTTNCN || LoaiBaoCao == BaoCaoLuong.GTPCTN || LoaiBaoCao == BaoCaoLuong.DSCPPC;

        public bool IsShowPhuCap => LoaiBaoCao == BaoCaoLuong.GTCTPC_TX || LoaiBaoCao == BaoCaoLuong.GTCTPC_NV
            || LoaiBaoCao == BaoCaoLuong.GTCTPC_TNK || LoaiBaoCao == BaoCaoLuong.GTCTPC_GTK || LoaiBaoCao == BaoCaoLuong.GTPCTN
            || Model.MaBaoCao == "1.19";
        /*public bool IsShowSummary => LoaiBaoCao == BaoCaoLuong.DSCTCN_CT
            || LoaiBaoCao == BaoCaoLuong.GTCTTL || LoaiBaoCao == BaoCaoLuong.DSCTCN
            || LoaiBaoCao == BaoCaoLuong.QTTTNCN || LoaiBaoCao == BaoCaoLuong.THLPC
            || LoaiBaoCao == BaoCaoLuong.TTNCN || LoaiBaoCao == BaoCaoLuong.DSCPL_A4
            || LoaiBaoCao == BaoCaoLuong.TTNCN || LoaiBaoCao == BaoCaoLuong.QTQS 
            || LoaiBaoCao == BaoCaoLuong.TLBCTA || LoaiBaoCao == BaoCaoLuong.GTCTPC;*/
        public bool IsShowGridYeuToLuong => IsCheckedYeuToLuong && !IsCheckedInChieuDoc;
        public bool IsShowSummary => LoaiBaoCao == BaoCaoLuong.DSCTCN_CT
            || LoaiBaoCao == BaoCaoLuong.GTCTTL || LoaiBaoCao == BaoCaoLuong.DSCTCN
            || LoaiBaoCao == BaoCaoLuong.QTTTNCN || LoaiBaoCao == BaoCaoLuong.THLPC
            || LoaiBaoCao == BaoCaoLuong.TTNCN || LoaiBaoCao == BaoCaoLuong.DSCPL_A4
            || LoaiBaoCao == BaoCaoLuong.TTNCN || LoaiBaoCao == BaoCaoLuong.QTQS
            || LoaiBaoCao == BaoCaoLuong.TLBCTA || LoaiBaoCao == BaoCaoLuong.DSCPPC
            || LoaiBaoCao == BaoCaoLuong.BTLTL || LoaiBaoCao == BaoCaoLuong.GTCTPC_TX || LoaiBaoCao == BaoCaoLuong.THQSQT;
        public bool IsShowSummaryNew => LoaiBaoCao == BaoCaoLuong.DSCPL_A4;
        public bool IsShowInChieuDoc => LoaiBaoCao == BaoCaoLuong.GTCTTL;
        public bool IsShowExportAllCadres => LoaiBaoCao == BaoCaoLuong.TTNCN || LoaiBaoCao == BaoCaoLuong.QTTTNCN;
        public bool IsDonViCbEnabled => LoaiBaoCao != BaoCaoLuong.LKHMLNS;
        public bool IsShowEmployeeNameCheckbox => LoaiBaoCao == BaoCaoLuong.DSCTCN;
        public bool IsShowDynamicPhuCap => false;
        public bool IsShowButtonDynamicPhuCap => LoaiBaoCao == BaoCaoLuong.BTLTL || LoaiBaoCao == BaoCaoLuong.BTLTT;
        public bool IsChiTraCaNhanKhacCheckbox => LoaiBaoCao == BaoCaoLuong.DSCTCN;
        public bool IsChiTietQuanSo => LoaiBaoCao == BaoCaoLuong.CTQSTG;
        public bool IsQtQuanSoMau2 => LoaiBaoCao == BaoCaoLuong.QTQS;

        private bool _isDynamicPhuCap;
        public bool IsDynamicPhuCap
        {
            get => _isDynamicPhuCap;
            set
            {
                SetProperty(ref _isDynamicPhuCap, value);
                OnPropertyChanged(nameof(IsShowDynamicPhuCap));
            }
        }

        private bool _isShowEmployeeName;
        public bool IsShowEmployeeName
        {
            get => _isShowEmployeeName;
            set
            {
                SetProperty(ref _isShowEmployeeName, value);
                OnPropertyChanged(nameof(IsShowKieuIn));
            }
        }

        private bool _isChiTraCaNhanKhac;
        public bool IsChiTraCaNhanKhac
        {
            get => _isChiTraCaNhanKhac;
            set => SetProperty(ref _isChiTraCaNhanKhac, value);
        }

        private bool _isInNgayThang;
        public bool IsInNgayThang
        {
            get => _isInNgayThang;
            set => SetProperty(ref _isInNgayThang, value);
        }

        private bool _isOrderTheoChucVu;
        public bool IsOrderTheoChucVu
        {
            get => _isOrderTheoChucVu;
            set => SetProperty(ref _isOrderTheoChucVu, value);
        }

        private bool _isGiaTriAm;
        public bool IsGiaTriAm
        {
            get => _isGiaTriAm;
            set => SetProperty(ref _isGiaTriAm, value);
        }

        public bool IsShowOrderTheoChucVu { get; set; }

        private bool _isToanQuan;
        public bool IsToanQuan
        {
            get => _isToanQuan;
            set
            {
                SetProperty(ref _isToanQuan, value);
                LoadYeuToLuong();
                OnPropertyChanged(nameof(IsBienPhong));
            }
        }

        private ObservableCollection<ComboboxItem> _huongNNItems;
        public ObservableCollection<ComboboxItem> HuongNNItems
        {
            get => _huongNNItems;
            set => SetProperty(ref _huongNNItems, value);
        }

        private ComboboxItem _selectedHuongNN;
        public ComboboxItem SelectedHuongNN
        {
            get => _selectedHuongNN;
            set => SetProperty(ref _selectedHuongNN, value);
        }

        public bool IsBienPhong => !IsToanQuan;

        private ObservableCollection<ComboboxItem> _itemsPhanLoaiBaoCaos;
        public ObservableCollection<ComboboxItem> ItemsPhanLoaiBaoCaos
        {
            get => _itemsPhanLoaiBaoCaos;
            set => SetProperty(ref _itemsPhanLoaiBaoCaos, value);
        }

        private ComboboxItem _selectedBaoCao;
        public ComboboxItem SelectedBaoCao
        {
            get => _selectedBaoCao;
            set
            {
                SetProperty(ref _selectedBaoCao, value);
                LoadYeuToLuong();
            }
        }

        private List<ComboboxItem> _itemsKieuIn;
        public List<ComboboxItem> ItemsKieuIn
        {
            get => _itemsKieuIn;
            set => SetProperty(ref _itemsKieuIn, value);
        }

        private ComboboxItem _selectedKieuIn;
        public ComboboxItem SelectedKieuIn
        {
            get => _selectedKieuIn;
            set => SetProperty(ref _selectedKieuIn, value);
        }

        public bool IsInCanBoMoiCheckbox => LoaiBaoCao == BaoCaoLuong.DSCPL_A4;

        private bool _isInCanBoMoi;
        public bool IsInCanBoMoi
        {
            get => _isInCanBoMoi;
            set
            {
                SetProperty(ref _isInCanBoMoi, value);
            }
        }

        private bool _isReduceBHXH;
        public bool IsReduceBHXH
        {
            get => _isReduceBHXH;
            set
            {
                SetProperty(ref _isReduceBHXH, value);
            }
        }

        private bool _isBoSungHSQCS;
        public bool IsBoSungHSQCS
        {
            get => _isBoSungHSQCS;
            set
            {
                SetProperty(ref _isBoSungHSQCS, value);
                LoadDonViData();
            }
        }

        public bool IsShowKieuIn => LoaiBaoCao == BaoCaoLuong.DSCTCN && !IsShowEmployeeName;

        public bool IsShowButtonGiaiThichPCTLK => LoaiBaoCao == BaoCaoLuong.BTLTL;

        private bool _isGiaiThichPCTLK;
        public bool IsGiaiThichPCTLK
        {
            get => _isGiaiThichPCTLK;
            set
            {
                SetProperty(ref _isGiaiThichPCTLK, value);
            }
        }

        public TlRptLuongKeHoachNq104Model TlRptLuongKeHoachModel { get; set; }

        public RelayCommand ExportCommand { get; }
        public RelayCommand ExportSignatureActionCommand { get; }

        public NewReportDialogViewModel(
            IMapper mapper,
            ILog logger,
            ISessionService sessionService,
            ITlDmDonViNq104Service tlDmDonViService,
            IExportService exportService,
            ITlBangLuongThangNq104Service tlBangLuongThangService,
            ITlDmCanBoNq104Service tlDmCanboService,
            ITlDmCapBacService iTlDmCapBacService,
            ITlDmPhuCapNq104Service iTlDmPhuCapService,
            IServiceProvider serviceProvider,
            ITlQsChungTuNq104Service tlQsChungTuService,
            IDmChuKyService iDmChuKyService,
            IDanhMucService iDanhMucService,
            INsDonViService donViService,
            IConfiguration configuration,
            ITlCanBoPhuCapBridgeNq104Service tlCanBoPhuCapBridgeNq104Service,
            ITlBangLuongThangBridgeNq104Service tlBangThuongBridgeService,
            ITlDsCapNhapBangLuongNq104Service tlDsCapNhapBangLuongService,
            ITlDieuChinhQsKeHoachNq104Service tlDieuChinhQsKeHoachService,
            ITlDmCanBoKeHoachNq104Service tlDmCanBoKeHoachService,
            IDanhMucService danhMucService,
            ITlDmCachTinhLuongTruyLinhNq104Service iDmCachTinhLuongTruyLinhService,
            ITlDmCachTinhLuongChuanNq104Service tlDmCachTinhLuongChuanService,
            ITlQuanLyThuNopBhxhChiTietService tlThuNopBhxhChiTiet,
            ITlQuanLyThuNopBhxhService tlThuNopBhxh,
            ITlDmCapBacNq104Service tlDmCapBacNq104Service)
        {
            _mapper = mapper;
            _logger = logger;
            _sessionService = sessionService;
            _tlDmDonViService = tlDmDonViService;
            _exportService = exportService;
            _tlBangLuongThangService = tlBangLuongThangService;
            _tlDmCanboService = tlDmCanboService;
            _iTlDmCapBacService = iTlDmCapBacService;
            _iTlDmPhuCapService = iTlDmPhuCapService;
            _tlQsChungTuService = tlQsChungTuService;
            _serviceProvider = serviceProvider;
            _iDmChuKyService = iDmChuKyService;
            _iDanhMucService = iDanhMucService;
            _configuration = configuration;
            _tlCanBoPhuCapBridgeNq104Service = tlCanBoPhuCapBridgeNq104Service;
            _tlBangLuongBridgeService = tlBangThuongBridgeService;
            _tlDsCapNhapBangLuongService = tlDsCapNhapBangLuongService;
            _tlDieuChinhQsKeHoachService = tlDieuChinhQsKeHoachService;
            _tlDmCanBoKeHoachService = tlDmCanBoKeHoachService;
            _iDmCachTinhLuongTruyLinhService = iDmCachTinhLuongTruyLinhService;
            _tlDmCachTinhLuongChuanService = tlDmCachTinhLuongChuanService;
            _sessionInfo = _sessionService.Current;
            _danhMucService = danhMucService;
            _donViService = donViService;
            _tlThuNopBhxhChiTietService = tlThuNopBhxhChiTiet;
            _tlThuNopBhxhService = tlThuNopBhxh;
            _iTlDmCapBacNq104Service = tlDmCapBacNq104Service;

            ExportCommand = new RelayCommand(o => OnExport());
            ExportSignatureActionCommand = new RelayCommand(o => OnOpenCauHinhChuKyDialog());
        }

        public override void Init()
        {
            base.Init();
            _sessionInfo = _sessionService.Current;
            _appSettingConfigPath = _configuration.GetSection(ConfigHelper.CONFIG_REPORT_LUONG_TRUY_LINH_SETTING_PATH).Value;
            IsCheckedYeuToLuong = false;
            IsInNgayThang = false;
            IsOrderTheoChucVu = true;
            IsShowOrderTheoChucVu = false;
            IsToanQuan = true;
            IsGiaTriAm = false;
            BIsShowNoiDung = false;
            IsCheckedMaHuongLuong = true;
            IsInCanBoMoi = false;
            IsGiaiThichPCTLK = false;
            SNoiDung = string.Empty;
            IsReduceBHXH = false;
            LoadData();
            LoadMonths();
            LoadMonthQuarters();
            LoadYears();
            LoadPhuCapData();
            LoadPhuCapBotData();
            LoadCatUnitTypes();
            LoadTypeChuKy();
            LoadDonViData();
            LoadLoaiFileOutPut();
            LoadKhoIn();
            LoadKieuIn();
            LoadDiaDiem();
            LoadShowOrderTheoChucVu();
            LoadLoaiTongHop();
            if (IsShowButtonDynamicPhuCap)
            {
                LoadPhuCapTongLuong();
                LoadPhuCapPhaiTru();
                LoadAppSetting();
            }
            //LoadLoaiPhuCap();
            LoadNgach();
            if (IsShowYeuToLuong)
            {
                LoadLoaiBaoCao();
                LoadYeuToLuong();
            }
            if (IsCheckedInChieuDoc)
            {
                LoadYeuToLuongDoc();
            }
            LoadDinhDangPhuCap();
            LoadLoaiQuanSo();
            LoadHuongNN();
        }

        private void LoadDiaDiem()
        {
            var danhMucDiaDiem = _iDanhMucService.FindByType(TypeDanhMuc.DM_CAUHINH, _sessionService.Current.YearOfWork).Where(n => n.IIDMaDanhMuc == MaDanhMuc.DIADIEM).FirstOrDefault();
            _diaDiem = danhMucDiaDiem == null ? string.Empty : danhMucDiaDiem.SGiaTri;
        }

        private void LoadShowOrderTheoChucVu()
        {
            if (LoaiBaoCao == BaoCaoLuong.DSCPL_A4
                || LoaiBaoCao == BaoCaoLuong.BTLTL
                || LoaiBaoCao == BaoCaoLuong.GTCTPC_TX
                || LoaiBaoCao == BaoCaoLuong.GTCTPC_NV
                || LoaiBaoCao == BaoCaoLuong.GTCTPC_TNK
                || LoaiBaoCao == BaoCaoLuong.GTCTPC_GTK
                || LoaiBaoCao == BaoCaoLuong.TTNCN
                || LoaiBaoCao == BaoCaoLuong.QTTTNCN
                || LoaiBaoCao == BaoCaoLuong.BTLTT
                || LoaiBaoCao == BaoCaoLuong.TLCCDSQ
                || LoaiBaoCao == BaoCaoLuong.TLCCDQNCN
                || LoaiBaoCao == BaoCaoLuong.BLTBP
                || LoaiBaoCao == BaoCaoLuong.DSCTCN
                || LoaiBaoCao == BaoCaoLuong.GTPCTN)
            {
                IsShowOrderTheoChucVu = true;
            }
            OnPropertyChanged(nameof(IsShowOrderTheoChucVu));
        }

        public override void LoadData(params object[] args)
        {
            base.LoadData(args);

            IsChecked = true;
            SearchDonVi = string.Empty;
            switch (Model.MaBaoCao)
            {
                case "1.1":
                    LoaiBaoCao = BaoCaoLuong.DSCPL_A4_CU;
                    break;
                case "1.2":
                case "1.19":
                    LoaiBaoCao = BaoCaoLuong.DSCPL_A4;
                    break;
                case "1.3":
                    LoaiBaoCao = BaoCaoLuong.DSCPL_A3;
                    break;
                case "1.4":
                    LoaiBaoCao = BaoCaoLuong.GTCTTL;
                    break;
                case "1.5":
                    LoaiBaoCao = BaoCaoLuong.GTCTPCTNVKTHD;
                    break;
                case "1.6":
                    LoaiBaoCao = BaoCaoLuong.GTCTPC_TX;
                    break;
                case "1.7":
                    LoaiBaoCao = BaoCaoLuong.GTCTPC_NV;
                    break;
                case "1.8":
                    LoaiBaoCao = BaoCaoLuong.GTCTPC_TNK;
                    break;
                case "1.9":
                    LoaiBaoCao = BaoCaoLuong.GTCTPC_GTK;
                    break;
                case "1.10":
                    LoaiBaoCao = BaoCaoLuong.TTNCN;
                    break;
                case "1.11":
                    LoaiBaoCao = BaoCaoLuong.BLTBP;
                    break;
                case "1.12":
                    LoaiBaoCao = BaoCaoLuong.QTTTNCN;
                    break;
                case "1.13":
                    LoaiBaoCao = BaoCaoLuong.BTLTL;
                    break;
                case "1.14":
                    LoaiBaoCao = BaoCaoLuong.BTLTT;
                    break;
                case "1.21":
                    LoaiBaoCao = BaoCaoLuong.GTPCTN;
                    break;
                case "2.1":
                    LoaiBaoCao = BaoCaoLuong.DSCPPC;
                    break;
                case "2.2":
                    LoaiBaoCao = BaoCaoLuong.GTCTPC_HQS;
                    break;
                case "2.3":
                    LoaiBaoCao = BaoCaoLuong.GTCTRQXN;
                    break;
                case "3":
                    LoaiBaoCao = BaoCaoLuong.DSCTCN;
                    break;
                case "4":
                    LoaiBaoCao = BaoCaoLuong.DSCTCN_CT;
                    _bIsShowNoiDung = true;
                    OnPropertyChanged(nameof(BIsShowNoiDung));
                    break;
                case "5":
                    LoaiBaoCao = BaoCaoLuong.THPCCV;
                    break;
                case "6":
                    LoaiBaoCao = BaoCaoLuong.THLPC;
                    break;
                case "7":
                    LoaiBaoCao = BaoCaoLuong.THLPC_DV;
                    break;
                case "8":
                    LoaiBaoCao = BaoCaoLuong.THLPC_N_DV;
                    break;
                case "9":
                    LoaiBaoCao = BaoCaoLuong.BTHLPC;
                    break;
                case "10":
                    LoaiBaoCao = BaoCaoLuong.THQSQT;
                    break;
                case "11":
                    LoaiBaoCao = BaoCaoLuong.INKIEM;
                    break;
                case "12.1":
                    LoaiBaoCao = BaoCaoLuong.DCQSNKH;
                    break;
                case "12.2":
                    LoaiBaoCao = BaoCaoLuong.CTQSRQ;
                    break;
                case "12.3":
                    LoaiBaoCao = BaoCaoLuong.CTQSNH;
                    break;
                case "15.1":
                    LoaiBaoCao = BaoCaoLuong.QTQS;
                    break;
                case "12.4":
                    LoaiBaoCao = BaoCaoLuong.BDQHKE;
                    break;
                case "12.5":
                    LoaiBaoCao = BaoCaoLuong.LKHMLNS;
                    break;
                case "1.15":
                    LoaiBaoCao = BaoCaoLuong.TLCCDSQ;
                    break;
                case "1.16":
                    LoaiBaoCao = BaoCaoLuong.TLCCDQNCN;
                    break;
                case "1.17":
                    LoaiBaoCao = BaoCaoLuong.TLBCTA;
                    break;
                case "1.18":
                    LoaiBaoCao = BaoCaoLuong.BTLTLNN;
                    break;
                case "17":
                    LoaiBaoCao = BaoCaoLuong.CTQSTG;
                    break;
                case "1.20":
                    LoaiBaoCao = BaoCaoLuong.BTHLPCBP;
                    break;
                default:
                    break;
            }
        }

        private void LoadLoaiBaoCao()
        {
            _itemsPhanLoaiBaoCaos = new ObservableCollection<ComboboxItem>();
            _itemsPhanLoaiBaoCaos.Add(new ComboboxItem("Chi tiết phụ cấp (theo đơn vị)", "1"));
            _itemsPhanLoaiBaoCaos.Add(new ComboboxItem("Chi tiết phụ cấp (theo hệ số)", "2"));
            _selectedBaoCao = _itemsPhanLoaiBaoCaos.FirstOrDefault();
            OnPropertyChanged(nameof(_itemsPhanLoaiBaoCaos));
            OnPropertyChanged(nameof(_selectedBaoCao));
        }

        private void LoadMonths()
        {
            _itemsMonth = new List<ComboboxItem>();
            for (int i = 1; i <= 12; i++)
            {
                ComboboxItem month = new ComboboxItem(i.ToString(), i.ToString());
                _itemsMonth.Add(month);
            }

            if (Model.SelectedMonth != null)
            {
                _selectedMonth = _itemsMonth.FirstOrDefault(x => x.ValueItem.Equals(Model.SelectedMonth.ToString()));
            }
            else
            {
                var thang = _sessionService.Current.Month;
                _selectedMonth = _itemsMonth.FirstOrDefault(x => x.ValueItem == thang.ToString());
            }

            OnPropertyChanged(nameof(SelectedMonth));
            OnPropertyChanged(nameof(ItemsMonth));
        }

        private void LoadMonthQuarters()
        {
            _monthQuarters = new List<ComboboxItem>();
            _monthQuarters.Add(new ComboboxItem("Cả năm", "1,2,3,4,5,6,7,8,9,10,11,12"));
            _monthQuarters.Add(new ComboboxItem("Quý I", "1,2,3"));
            _monthQuarters.Add(new ComboboxItem("Quý II", "4,5,6"));
            _monthQuarters.Add(new ComboboxItem("Quý III", "7,8,9"));
            _monthQuarters.Add(new ComboboxItem("Quý IV", "10,11,12"));
            for (int i = 1; i <= 12; i++)
            {
                ComboboxItem month = new ComboboxItem("Tháng " + i.ToString(), i.ToString());
                _monthQuarters.Add(month);
            }
            if (Model.SelectedMonth != null)
            {
                _selectedMonthQuarter = _monthQuarters.FirstOrDefault(x => x.ValueItem.Equals(Model.SelectedMonth.ToString()));
            }
            else
            {
                var thang = _sessionService.Current.Month;
                _selectedMonthQuarter = _monthQuarters.FirstOrDefault(x => x.ValueItem == thang.ToString());
            }
        }

        private void LoadNgach()
        {
            _itemsGrades = new ObservableCollection<ComboboxItem>();
            _itemsGrades.Add(new ComboboxItem("Sĩ quan", "1"));
            _itemsGrades.Add(new ComboboxItem("Quân nhân chuyên nghiệp", "2"));
            _itemsGrades.Add(new ComboboxItem("Công nhân viên chức quốc phòng", "3"));
            _itemsGrades.Add(new ComboboxItem("Hạ sĩ quan, chiến sĩ", "4"));
            SelectedGrade = _itemsGrades.FirstOrDefault();
            OnPropertyChanged(nameof(ItemsGrades));
        }


        private void LoadLoaiQuanSo()
        {
            ItemsQuanSo = new ObservableCollection<ComboboxItem>
            {
                new ComboboxItem()
                {
                    ValueItem = "2",
                    DisplayItem = "Tăng"
                },
                new ComboboxItem()
                {
                    ValueItem = "3",
                    DisplayItem = "Giảm"
                },
                new ComboboxItem()
                {
                    ValueItem = "4",
                    DisplayItem = "Toàn bộ"
                }
            };
            SelectedLoaiQuanSo = ItemsQuanSo.FirstOrDefault();
        }

        //private void LoadLoaiPhuCap()
        //{
        //    ItemsLoaiPhuCap = new ObservableCollection<ComboboxItem>
        //    {
        //        new ComboboxItem()
        //        {
        //            ValueItem = LoaiPhuCapLuong.QUYET_TOAN.ToString(),
        //            DisplayItem = LoaiPhuCapLuongString.QUYET_TOAN
        //        },
        //        new ComboboxItem()
        //        {
        //            ValueItem = LoaiPhuCapLuong.NGHIEP_VU.ToString(),
        //            DisplayItem = LoaiPhuCapLuongString.NGHIEP_VU
        //        }
        //    };
        //    SelectedLoaiPhuCap = ItemsLoaiPhuCap.FirstOrDefault();
        //}

        private void LoadDinhDangPhuCap()
        {
            ItemsDinhDangPhuCap = new ObservableCollection<ComboboxItem>
            {
                new ComboboxItem()
                {
                    ValueItem = DinhDangPhuCapLuong.TIEN.ToString(),
                    DisplayItem = DinhDangPhuCapLuongString.TIEN
                },
                new ComboboxItem()
                {
                    ValueItem = DinhDangPhuCapLuong.HE_SO.ToString(),
                    DisplayItem = DinhDangPhuCapLuongString.HE_SO
                },
                new ComboboxItem()
                {
                    ValueItem = DinhDangPhuCapLuong.KHAC.ToString(),
                    DisplayItem = DinhDangPhuCapLuongString.KHAC
                }
            };
            _selectedDinhDangPhuCap = null;
        }

        private void LoadYears()
        {
            _itemsYear = new List<ComboboxItem>();
            for (int i = DateTime.Now.Year - 29; i <= DateTime.Now.Year + 29; i++)
            {
                var year = new ComboboxItem(i.ToString(), i.ToString());
                _itemsYear.Add(year);
            }
            OnPropertyChanged(nameof(ItemsYear));

            if (Model.SelectedYear != null)
            {
                _selectedYear = _itemsYear.FirstOrDefault(x => x.ValueItem.Equals(Model.SelectedYear.ToString()));
            }
            else
            {
                var nam = _sessionService.Current.YearOfWork;
                _selectedYear = _itemsYear.FirstOrDefault(x => x.ValueItem == nam.ToString());
            }
            OnPropertyChanged(nameof(SelectedYear));
        }

        private void LoadKhoIn()
        {
            ComboboxItem A3 = new ComboboxItem("Khổ", "A3");
            ComboboxItem A4 = new ComboboxItem("Khổ", "A4");
            ItemsKhoIn = new List<ComboboxItem>() { A3, A4 };
            SelectedKhoIn = A3;
        }

        private void LoadKieuIn()
        {
            ComboboxItem One = new ComboboxItem("In theo 1 hàng", "1");
            ComboboxItem Two = new ComboboxItem("In theo 2 hàng", "2");
            ItemsKieuIn = new List<ComboboxItem>() { One, Two };
            SelectedKieuIn = Two;
        }

        private void LoadLoaiFileOutPut()
        {
            ItemsFileExport = new List<ComboboxItem>();
            _itemsFileExport.Add(new ComboboxItem("Excel", "Excel"));
            _itemsFileExport.Add(new ComboboxItem("PDF", "PDF"));
            SelectedFileExport = ItemsFileExport.FirstOrDefault(x => x.ValueItem == "PDF");
        }

        private void LoadDonViData()
        {
            var data = _tlDmDonViService.FindAllDonViBaoCaoNq104();
            var lstDonViNganSach = _donViService.FindByCondition(n => n.NamLamViec == _sessionService.Current.YearOfWork);
            Dictionary<string, DonVi> dicDonVi = new Dictionary<string, DonVi>();
            if (lstDonViNganSach != null)
            {
                foreach (var item in lstDonViNganSach)
                {
                    if (!dicDonVi.ContainsKey(item.IIDMaDonVi))
                        dicDonVi.Add(item.IIDMaDonVi, item);
                }
            }
            List<TlDmDonViNq104> lstKhongBangLuong = new List<TlDmDonViNq104>();
            if (LoaiBaoCao != BaoCaoLuong.LKHMLNS && LoaiBaoCao != BaoCaoLuong.CTQSNH && LoaiBaoCao != BaoCaoLuong.CTQSRQ
                && LoaiBaoCao != BaoCaoLuong.DCQSNKH && LoaiBaoCao != BaoCaoLuong.BDQHKE && LoaiBaoCao != BaoCaoLuong.QTQS
                && LoaiBaoCao != BaoCaoLuong.THQSQT && LoaiBaoCao != BaoCaoLuong.CTQSTG && LoaiBaoCao != BaoCaoLuong.DSCPL_A4
                && LoaiBaoCao != BaoCaoLuong.DSCPPC)
            {
                if (IsShowComboboxThang)
                {
                    foreach (var item in data)
                    {
                        if (SelectedYear != null && SelectedMonth != null)
                        {
                            if (LoaiBaoCao == BaoCaoLuong.BTLTL)
                            {
                                var lstTruyLinh = _tlDsCapNhapBangLuongService.FindHaveDataByCondition(CachTinhLuong.CACH5, item.MaDonVi, int.Parse(SelectedMonth.ValueItem ?? "0"), int.Parse(SelectedYear.ValueItem)).OrderBy(x => x.MaCbo);
                                //var lstBangLuong = item.TlDsCapNhapBangLuongsNq104.Where(x => x.Thang == int.Parse(SelectedMonth.ValueItem) && x.Nam == int.Parse(SelectedYear.ValueItem) && x.IsTongHop != true && x.MaCachTl == CachTinhLuong.CACH5);
                                if (lstTruyLinh.IsEmpty())
                                {
                                    lstKhongBangLuong.Add(item);
                                }
                            }
                            else
                            {
                                var lstBangLuong = item.TlDsCapNhapBangLuongsNq104.Where(x => x.Thang == int.Parse(SelectedMonth.ValueItem) && x.Nam == int.Parse(SelectedYear.ValueItem) && x.IsTongHop != true);
                                if (lstBangLuong.IsEmpty())
                                {
                                    lstKhongBangLuong.Add(item);
                                }
                            }

                        }
                    }
                }
                else if (IsShowComboboxThoiGian)
                {
                    foreach (var item in data)
                    {
                        if (SelectedYear != null && SelectedMonthQuarter != null)
                        {
                            var lstBangLuong = item.TlDsCapNhapBangLuongsNq104.Where(x => SelectedMonthQuarter.ValueItem.Contains(x.Thang.ToString()) && x.Nam == int.Parse(SelectedYear.ValueItem) && x.IsTongHop != true);
                            if (lstBangLuong.IsEmpty())
                            {
                                lstKhongBangLuong.Add(item);
                            }
                        }
                    }
                }
                else
                {
                    foreach (var item in data)
                    {
                        if (SelectedYear != null)
                        {
                            var lstBangLuong = item.TlDsCapNhapBangLuongsNq104.Where(x => x.Nam == int.Parse(SelectedYear.ValueItem) && x.IsTongHop != true);
                            if (lstBangLuong.IsEmpty())
                            {
                                lstKhongBangLuong.Add(item);
                            }
                        }
                    }
                }
                var lstHienThi = data.Except(lstKhongBangLuong);

                ItemsDonVi = _mapper.Map<ObservableCollection<TlDmDonViNq104Model>>(lstHienThi);
            }
            else if (LoaiBaoCao == BaoCaoLuong.QTQS || LoaiBaoCao == BaoCaoLuong.THQSQT || LoaiBaoCao == BaoCaoLuong.CTQSTG)
            {
                data = _tlDmDonViService.FindDonViBaoCaoQuanSo(_sessionInfo.YearOfWork);
                var listDonViHasChungTu = new List<string>();
                if (SelectedYear != null && SelectedMonthQuarter != null)
                {
                    if (LoaiBaoCao == BaoCaoLuong.THQSQT || LoaiBaoCao == BaoCaoLuong.QTQS)
                    {
                        if (IsSummary)
                        {
                            listDonViHasChungTu = _tlQsChungTuService.FindAll().Where(x => SelectedMonthQuarter.ValueItem == x.Thang.ToString() && x.Nam == int.Parse(SelectedYear.ValueItem) && !string.IsNullOrEmpty(x.STongHop)).Select(n => n.MaDonVi).ToList();
                        }
                        else
                        {
                            listDonViHasChungTu = _tlQsChungTuService.FindAll().Where(x => SelectedMonthQuarter.ValueItem == x.Thang.ToString() && x.Nam == int.Parse(SelectedYear.ValueItem) && string.IsNullOrEmpty(x.STongHop)).Select(n => n.MaDonVi).ToList();
                        }
                    }
                    else
                    {
                        listDonViHasChungTu = _tlQsChungTuService.FindAll().Where(x => SelectedMonthQuarter.ValueItem == x.Thang.ToString() && x.Nam == int.Parse(SelectedYear.ValueItem)).Select(n => n.MaDonVi).ToList();
                    }

                }
                else
                {
                    listDonViHasChungTu = _tlQsChungTuService.FindAll().Select(n => n.MaDonVi).ToList();
                }
                var listHienThi = new List<TlDmDonViNq104>();
                foreach (var item in listDonViHasChungTu)
                {
                    if (data.Any(n => n.MaDonVi == item))
                    {
                        listHienThi.Add(data.FirstOrDefault(n => n.MaDonVi == item));
                    }
                    else if (dicDonVi.ContainsKey(item))
                    {
                        listHienThi.Add(new TlDmDonViNq104() { MaDonVi = dicDonVi[item].IIDMaDonVi, TenDonVi = dicDonVi[item].TenDonVi });
                    }
                }
                ItemsDonVi = _mapper.Map<ObservableCollection<TlDmDonViNq104Model>>(listHienThi);
            }
            else if (LoaiBaoCao == BaoCaoLuong.DSCPL_A4)
            {
                var lstDonViBangLuong = new List<TlDmDonViNq104>();
                var lstDonViBangLuongExist = new List<TlDmDonViNq104>();
                if (SelectedYear != null && SelectedMonth != null)
                {
                    if (IsThuNopBhxh)
                    {
                        lstDonViBangLuong = _tlDmDonViService.FindDonViBangLuongThang(int.Parse(SelectedMonth.ValueItem), int.Parse(SelectedYear.ValueItem), CachTinhLuong.CACH0, true, true).ToList();
                    }
                    else
                    {
                        lstDonViBangLuong = _tlDmDonViService.FindDonViBangLuongThang(int.Parse(SelectedMonth.ValueItem), int.Parse(SelectedYear.ValueItem), CachTinhLuong.CACH0, false, true).ToList();
                        if (IsBoSungHSQCS)
                        {
                            var lstDonViHSQCS = _tlDmDonViService.FindDonViPhuCap(int.Parse(SelectedMonth.ValueItem), int.Parse(SelectedYear.ValueItem), CachTinhLuong.CACH0, true).ToList();
                            foreach (var item in lstDonViHSQCS)
                            {
                                var lstDonViExist = lstDonViBangLuong.Where(x => x.MaDonVi == item.MaDonVi).FirstOrDefault();
                                if (lstDonViExist is null)
                                {
                                    lstDonViBangLuongExist.Add(item);
                                }
                            }
                            lstDonViBangLuong.AddRange(lstDonViBangLuongExist);
                            lstDonViBangLuong = lstDonViBangLuong.OrderBy(x => x.MaDonVi).ToList();
                        }
                    }
                }

                ItemsDonVi = _mapper.Map<ObservableCollection<TlDmDonViNq104Model>>(lstDonViBangLuong);
            }
            else if (LoaiBaoCao == BaoCaoLuong.DSCPPC)
            {
                var lstDonViBangLuong = new List<TlDmDonViNq104>();
                if (SelectedYear != null && SelectedMonth != null)
                    lstDonViBangLuong = _tlDmDonViService.FindDonViPhuCap(int.Parse(SelectedMonth.ValueItem), int.Parse(SelectedYear.ValueItem), CachTinhLuong.CACH0, true).ToList();
                ItemsDonVi = _mapper.Map<ObservableCollection<TlDmDonViNq104Model>>(lstDonViBangLuong);
            }
            else
            {
                ItemsDonVi = _mapper.Map<ObservableCollection<TlDmDonViNq104Model>>(data);
            }

            if (ItemsDonVi != null && ItemsDonVi.Count > 0)
            {
                SelectedDonVi = ItemsDonVi.FirstOrDefault();
            }

            _dataDonViView = CollectionViewSource.GetDefaultView(ItemsDonVi);
            _dataDonViView.Filter = ListDonViFilter;

            Dictionary<string, int> dicDonViIndexViewSelected = new Dictionary<string, int>();
            if (LstMaDonViIndexViewSelected != null)
            {
                dicDonViIndexViewSelected = LstMaDonViIndexViewSelected.Distinct().ToDictionary(n => n, n => 1);
            }

            foreach (var org in ItemsDonVi)
            {
                if (dicDonViIndexViewSelected.ContainsKey(org.MaDonVi))
                    org.IsSelected = true;

                org.PropertyChanged += (sender, args) =>
                {
                    OnPropertyChanged(nameof(LabelSelectedCountAgency));
                    OnPropertyChanged(nameof(SelectAllAgency));
                };
            }
            if (MaDonVi != null)
            {
                var lstDonVi = new ObservableCollection<TlDmDonViNq104Model>();
                var donvi = ItemsDonVi.Where(n => n.MaDonVi.Equals(MaDonVi)).FirstOrDefault();
                if (donvi != null)
                    donvi.IsSelected = true;
                foreach (var item in ItemsDonVi)
                {
                    if (item.IsSelected)
                        lstDonVi.Insert(0, item);
                    else
                        lstDonVi.Add(item);
                }
                ItemsDonVi = ObjectCopier.Clone(lstDonVi);
            }
            OnPropertyChanged(nameof(LabelSelectedCountAgency));
            OnPropertyChanged(nameof(SelectAllAgency));
        }

        private void LoadPhuCapData()
        {
            if (LoaiBaoCao != BaoCaoLuong.GTPCTN && LoaiBaoCao != BaoCaoLuong.GTCTPC_TX && LoaiBaoCao != BaoCaoLuong.GTCTPC_NV && LoaiBaoCao != BaoCaoLuong.GTCTPC_TNK && LoaiBaoCao != BaoCaoLuong.GTCTPC_GTK && !IsShowBangBcLuongDong)
            {
                return;
            }

            if (SelectedYear == null || SelectedMonth == null)
            {
                return;
            }

            _itemsPhuCap = new ObservableCollection<TlDmPhuCapNq104Model>();
            if (!IsShowBangBcLuongDong)
            {
                int nam = int.Parse(SelectedYear.ValueItem);
                int thang = int.Parse(SelectedMonth.ValueItem);
                _tlBangLuongBridgeService.DataPreprocess(thang, nam, null, CachTinhLuong.CACH0);
                var data = _iTlDmPhuCapService.FindHasDataBangLuong(nam, thang, CachTinhLuong.CACH0);
                _itemsPhuCap = _mapper.Map<ObservableCollection<TlDmPhuCapNq104Model>>(data);
                CheckPhuCapTheoLoai();
            }
            else
            {
                List<string> lstPhuCapExclude = new List<string>() { PhuCap.LHT_TT, PhuCap.PCCV_TT, PhuCap.PCTN_TT, PhuCap.PCKV_TT, PhuCap.PCCOV_TT };
                var objFormula = _tlDmCachTinhLuongChuanService.FindByMaCot(PhuCap.PCKHAC_SUM);
                if (objFormula != null)
                {
                    var lstPhuCapInclude = ExtensionMethods.GetMaPhuCapByFormula(objFormula.CongThuc);
                    var data = _iTlDmPhuCapService.FindAll(n => lstPhuCapInclude.Contains(n.MaPhuCap) && !lstPhuCapExclude.Contains(n.MaPhuCap));
                    _itemsPhuCap = _mapper.Map<ObservableCollection<TlDmPhuCapNq104Model>>(data);
                }
            }
            OnPropertyChanged(nameof(ItemsPhuCap));
            if (IsShowPhuCap)
            {
                foreach (var org in _itemsPhuCap)
                {
                    org.PropertyChanged += (sender, args) =>
                    {
                        OnPropertyChanged(nameof(LabelSelectedCountPhuCapBcCauHinhDong));
                        OnPropertyChanged(nameof(LabelSelectedCountPhuCap));
                        OnPropertyChanged(nameof(SelectAllPhuCap));
                    };
                }
                OnPropertyChanged(nameof(LabelSelectedCountPhuCapBcCauHinhDong));
                OnPropertyChanged(nameof(LabelSelectedCountPhuCap));
                OnPropertyChanged(nameof(SelectAllPhuCap));
            }

            _dataPhuCapView = CollectionViewSource.GetDefaultView(ItemsPhuCap);
            _dataPhuCapView.Filter = ListPhuCapFilter;
        }

        private void LoadPhuCapBotData()
        {
            if (!IsShowBangBcLuongDong)
            {
                return;
            }
            _itemsPhuCapBot = new ObservableCollection<TlDmPhuCapNq104Model>();
            List<string> lstInclude = new List<string> { PhuCap.BHXHCN_TT, PhuCap.BHYTCN_TT, PhuCap.BHTNCN_TT, PhuCap.THUETNCN_TT, PhuCap.TA_TONG, PhuCap.PHAITRUKHAC_SUM };
            var data = _iTlDmPhuCapService.FindAll(n => lstInclude.Contains(n.MaPhuCap));
            _itemsPhuCapBot = _mapper.Map<ObservableCollection<TlDmPhuCapNq104Model>>(data);
            foreach (var item in _itemsPhuCapBot)
            {
                switch (item.MaPhuCap)
                {
                    case PhuCap.BHYTCN_TT:
                    case PhuCap.BHTNCN_TT:
                    case PhuCap.THUETNCN_TT:
                        item.IsSelected = true;
                        break;
                    default:
                        break;
                }
                item.PropertyChanged += (sender, args) =>
                {
                    if (args.PropertyName == nameof(TlDmPhuCapNq104Model.IsSelected))
                    {
                        var obj = (TlDmPhuCapNq104Model)sender;
                        if (obj.IsSelected && _itemsPhuCapBot.Where(n => n.IsSelected).Count() > 3)
                        {
                            obj.IsSelected = false;
                        }
                    }
                };
            }
            OnPropertyChanged(nameof(ItemsPhuCapBot));
        }

        private void CheckPhuCapTheoLoai()
        {
            ObservableCollection<TlDmPhuCapNq104Model> lstPhuCap = new ObservableCollection<TlDmPhuCapNq104Model>();
            if (ItemsPhuCap != null)
            {
                foreach (var pc in ItemsPhuCap)
                {
                    if (LoaiPhuCap.Equals(pc.ILoai.GetValueOrDefault(-1).ToString()))
                    {
                        pc.IsSelected = true;
                        lstPhuCap.Insert(0, pc);
                    }
                    else
                    {
                        pc.IsSelected = false;
                        lstPhuCap.Add(pc);
                    }
                }
            }
            ItemsPhuCap = ObjectCopier.Clone(lstPhuCap);
        }

        private void LoadPhuCapTongLuong()
        {
            var delimiters = new char[] { '+', '-', '*', '/', '(', ')' };
            var congThucLuongThang = _iDmCachTinhLuongTruyLinhService.FindByMaCot(PhuCap.LUONGTHANG_SUM);
            List<string> lstPhuCap = new List<string>();
            if (congThucLuongThang != null && !string.IsNullOrEmpty(congThucLuongThang.CongThuc))
            {
                lstPhuCap = congThucLuongThang.CongThuc.Split(delimiters).ToList();
            }


            var data = _iTlDmPhuCapService.FindAll(x => lstPhuCap.Contains(x.MaPhuCap));
            ItemsPhuCapTongLuong = new ObservableCollection<TlDmPhuCapNq104Model>();
            ItemsPhuCapTongLuong = _mapper.Map<ObservableCollection<TlDmPhuCapNq104Model>>(data);
            _dataPhuCapTongLuongView = CollectionViewSource.GetDefaultView(ItemsPhuCapTongLuong);
            _dataPhuCapTongLuongView.Filter = ListPhuCapFilter;
            if (IsShowDynamicPhuCap)
            {
                foreach (var org in ItemsPhuCapTongLuong)
                {
                    org.PropertyChanged += (sender, args) =>
                    {
                        if (args.PropertyName == nameof(TlDmPhuCapNq104Model.IsSelected))
                        {
                            OnPropertyChanged(nameof(LabelSelectedCountPhuCapTongLuong));
                            OnPropertyChanged(nameof(SelectAllPhuCapTongLuong));
                        }
                    };
                }
                OnPropertyChanged(nameof(LabelSelectedCountPhuCapTongLuong));
                OnPropertyChanged(nameof(SelectAllPhuCapTongLuong));
            }
        }

        private void LoadPhuCapPhaiTru()
        {
            var delimiters = new char[] { '+', '-', '*', '/', '(', ')' };
            var congThucLuongThang = _iDmCachTinhLuongTruyLinhService.FindByMaCot(PhuCap.PHAITRU_SUM);
            List<string> lstPhuCap = new List<string>();
            if (congThucLuongThang != null && !string.IsNullOrEmpty(congThucLuongThang.CongThuc))
            {
                lstPhuCap = congThucLuongThang.CongThuc.Split(delimiters).ToList();
            }

            var data = _iTlDmPhuCapService.FindAll(x => lstPhuCap.Contains(x.MaPhuCap));
            ItemsPhuCapPhaiTru = new ObservableCollection<TlDmPhuCapNq104Model>();
            ItemsPhuCapPhaiTru = _mapper.Map<ObservableCollection<TlDmPhuCapNq104Model>>(data);
            _dataPhuCapPhaiTruView = CollectionViewSource.GetDefaultView(ItemsPhuCapPhaiTru);
            _dataPhuCapPhaiTruView.Filter = ListPhuCapFilter;
            if (IsShowDynamicPhuCap)
            {
                foreach (var org in ItemsPhuCapTongLuong)
                {
                    org.PropertyChanged += (sender, args) =>
                    {
                        if (args.PropertyName == nameof(TlDmPhuCapNq104Model.IsSelected))
                        {
                            OnPropertyChanged(nameof(LabelSelectedCountPhuCapTongLuong));
                            OnPropertyChanged(nameof(SelectAllPhuCapTongLuong));
                        }
                    };
                }

                OnPropertyChanged(nameof(LabelSelectedCountPhuCapPhaiTru));
                OnPropertyChanged(nameof(SelectAllPhuCapPhaiTru));
            }
        }

        private List<TlDmPhuCapNq104Model> GetPhuCapTruyLinhKhacSum()
        {
            var delimiters = new char[] { '+', '-', '*', '/', '(', ')' };
            var congThucLuongThang = _iDmCachTinhLuongTruyLinhService.FindByMaCot(PhuCap.TRUYLINHKHAC_SUM);
            List<string> lstPhuCap = new List<string>();
            if (congThucLuongThang != null && !string.IsNullOrEmpty(congThucLuongThang.CongThuc))
            {
                lstPhuCap = congThucLuongThang.CongThuc.Split(delimiters).ToList();
            }

            List<TlDmPhuCapNq104> data = new List<TlDmPhuCapNq104>();
            foreach (var pc in lstPhuCap)
            {
                var phuCap = _iTlDmPhuCapService.FindByMaPhuCap(pc);
                if (phuCap != null)
                {
                    data.Add(phuCap);
                }
            }
            return _mapper.Map<List<TlDmPhuCapNq104Model>>(data);
        }

        private void LoadYeuToLuong()
        {
            List<string> lstStringPhuCapHeSo = new List<string>() { "LHT_TT", "PCTNVK_TT", "HSBL_TT", "PCCV_TT", "PCTN_TT", "PCCOV_TT", "PCTRA_SUM", "PCDACTHU_SUM", "PCKHAC_SUM", "PCKIE_TT", "PCKV_TT", "PC8_TT", "PCTHUHUT_TT", "PCBVBG_TT", "PCLNBG_TT", "PCANQP_TT", "PCDTQUANSU_TT" };

            var data = _iTlDmPhuCapService.FindAll(x => lstStringPhuCapHeSo.Contains(x.MaPhuCap));
            if (SelectedBaoCao != null && SelectedBaoCao.ValueItem.Equals("2") && IsBienPhong)
            {
                lstStringPhuCapHeSo = new List<string>() { "LHT_HS", "PCTNVK_HS", "HSBL_HS", "PCCV_HS", "PCTN_HS", "PCCOV_HS", "PCBCV_HS", "PCKIE_HS", "PCKV_HS", "PC8_HS", "PCTHUHUT_HS", "PCBVBG_HS", "PCLNBG_HS", "PCANQP_HS", "PCDTQUANSU_HS" };
                data = _iTlDmPhuCapService.FindAll(x => lstStringPhuCapHeSo.Contains(x.MaPhuCap));
            }
            ItemsYeuToLuong = _mapper.Map<ObservableCollection<TlDmPhuCapNq104Model>>(data);
            SelectedYeuToluong = ItemsYeuToLuong.FirstOrDefault();
        }

        private void LoadYeuToLuongDoc()
        {
            List<string> lstStringPhuCapHeSo = new List<string>() { "LHT_TT", "PCTNVK_TT", "HSBL_TT", "PCCV_TT", "PCTN_TT", "PCCOV_TT", "PCTRA_SUM", "PCDACTHU_SUM", "PCKHAC_SUM", "PCKIE_TT", "PCKV_TT", "PC8_TT", "PCTHUHUT_TT", "PCBVBG_TT", "PCLNBG_TT", "PCANQP_TT", "PCDTQUANSU_TT" };

            var data = _iTlDmPhuCapService.FindAll(x => lstStringPhuCapHeSo.Contains(x.MaPhuCap));
            if (SelectedBaoCao != null && SelectedBaoCao.ValueItem.Equals("2") && IsBienPhong)
            {
                lstStringPhuCapHeSo = new List<string>() { "LHT_HS", "PCTNVK_HS", "HSBL_HS", "PCCV_HS", "PCTN_HS", "PCCOV_HS", "PCBCV_HS", "PCKIE_HS", "PCKV_HS", "PC8_HS", "PCTHUHUT_HS", "PCBVBG_HS", "PCLNBG_HS", "PCANQP_HS", "PCDTQUANSU_HS" };
                data = _iTlDmPhuCapService.FindAll(x => lstStringPhuCapHeSo.Contains(x.MaPhuCap));
            }
            ItemsYeuToLuongDoc = _mapper.Map<ObservableCollection<TlDmPhuCapNq104Model>>(data);
            SelectedYeuToluong = ItemsYeuToLuongDoc.FirstOrDefault();
            _dataYeuToLuongDocView = CollectionViewSource.GetDefaultView(ItemsYeuToLuongDoc);
            _dataYeuToLuongDocView.Filter = ListYeuToLuongDocFilter;
        }

        public void LoadCatUnitTypes()
        {
            var listDonViTinh = _iDanhMucService.FindByCondition(x => x.SType.Contains(TypeDanhMuc.DM_DONVITINH) && x.INamLamViec == _sessionService.Current.YearOfWork && x.ITrangThai == StatusType.ACTIVE)
                .ToList();
            var expenseTypes = new List<ComboboxItem>();
            if (listDonViTinh.Count <= 0)
            {
                ComboboxItem cb = new ComboboxItem();
                cb.DisplayItem = "Đồng";
                cb.ValueItem = "1";
                cb.Type = "Đồng";
                expenseTypes.Add(cb);
            }
            else
            {
                listDonViTinh = listDonViTinh.OrderBy(x => x.SGiaTri).ToList();
                foreach (var dvt in listDonViTinh)
                {
                    ComboboxItem cb = new ComboboxItem();
                    cb.DisplayItem = dvt.STen;
                    cb.ValueItem = dvt.SGiaTri;
                    cb.Type = dvt.SMoTa;
                    expenseTypes.Add(cb);
                }
            }
            ItemsUnitType = new ObservableCollection<ComboboxItem>(expenseTypes);
            _selectedUnitType = expenseTypes.ElementAt(0);
        }

        public void LoadLoaiTongHop()
        {
            var lstLoaiTongHop = new ObservableCollection<ComboboxItem>();
            if (LoaiBaoCao == BaoCaoLuong.DSCPL_A4)
                lstLoaiTongHop = new ObservableCollection<ComboboxItem>() {
                    new ComboboxItem("Chi tiết theo Loại đối tượng", "1"),
                    new ComboboxItem("Chi tiết theo đơn vị", "2")
                };
            else
                lstLoaiTongHop = new ObservableCollection<ComboboxItem>() {
                    new ComboboxItem("Chi tiết cá nhân", "1"),
                    new ComboboxItem("Chi tiết theo đơn vị", "2")
                };
            ItemsLoaiTongHop = new ObservableCollection<ComboboxItem>(lstLoaiTongHop);
            _selectedLoaiTongHop = ItemsLoaiTongHop.ElementAt(0);
        }

        private void LoadTypeChuKy()
        {
            switch (LoaiBaoCao)
            {
                case BaoCaoLuong.DSCPL_A4:
                    TypeChuky = TypeChuKy.RPT_TL_LUONG_THANG_NEW;
                    break;
                case BaoCaoLuong.BLTBP:
                    TypeChuky = TypeChuKy.RPT_TL_LUONG_THANG_BIEN_PHONG_NEW;
                    break;
                case BaoCaoLuong.GTCTTL:
                    TypeChuky = TypeChuKy.RPT_TL_GIAI_THICH_CHI_TIET_LUONG_NEW;
                    break;
                case BaoCaoLuong.GTCTPC_TX:
                    TypeChuky = TypeChuKy.RPT_TL_GIAITHICH_CHITIET_PHUCAP_KHAC_NEW;
                    break;
                case BaoCaoLuong.GTCTPC_NV:
                    TypeChuky = TypeChuKy.RPT_TL_GIAITHICH_CHITIET_PHUCAP_KHAC_NEW;
                    break;
                case BaoCaoLuong.GTCTPC_TNK:
                    TypeChuky = TypeChuKy.RPT_TL_GIAITHICH_CHITIET_PHUCAP_KHAC_NEW;
                    break;
                case BaoCaoLuong.GTCTPC_GTK:
                    TypeChuky = TypeChuKy.RPT_TL_GIAITHICH_CHITIET_PHUCAP_KHAC_NEW;
                    break;
                case BaoCaoLuong.TTNCN:
                    TypeChuky = TypeChuKy.RPT_TL_BANGKE_TRICHTHUETNCN_NEW;
                    break;
                case BaoCaoLuong.QTTTNCN:
                    TypeChuky = TypeChuKy.RPT_TL_QUYET_TOAN_TTNCN_NEW;
                    break;
                case BaoCaoLuong.BTLTL:
                    TypeChuky = TypeChuKy.RPT_TL_LUONG_TRUY_LINH_NEW;
                    break;
                case BaoCaoLuong.BTLBH:
                    TypeChuky = TypeChuKy.RPT_TL_LUONG_BAO_HIEM_NEW;
                    break;
                case BaoCaoLuong.DSCTCN:
                    TypeChuky = TypeChuKy.RPT_TL_DANHSACH_CHITRA_LUONGCN_NEW;
                    TypeChuky = TypeChuKy.RPT_TL_CHITRA_NGANHANG_THUNHAPKHAC_NEW;
                    break;
                case BaoCaoLuong.DSCTCN_CT:
                    TypeChuky = TypeChuKy.RPT_TL_DS_TRA_NGAN_HANG_NEW;
                    break;
                case BaoCaoLuong.THLPC:
                    TypeChuky = TypeChuKy.RPT_TL_TONG_HOP_LUONG_PHU_CAP_THEO_NGACH_NEW;
                    break;
                case BaoCaoLuong.THLPC_DV:
                    TypeChuky = TypeChuKy.RPT_TL_TONGHOP_LUONG_PHUCAP_DONVI_NEW;
                    break;
                case BaoCaoLuong.THLPC_N_DV:
                    TypeChuky = TypeChuKy.RPT_TL_TONGHOP_LUONG_NGACHDONVI_NEW;
                    break;
                case BaoCaoLuong.THQSQT:
                    TypeChuky = TypeChuKy.RPT_TL_QUYETTOAN_QUANSO_NEW;
                    break;
                case BaoCaoLuong.GTCTPCTNVKTHD:
                    TypeChuky = TypeChuKy.RPT_TL_GIAITHICH_CHITIET_PHUCAPTNVKTHD_NEW;
                    break;
                case BaoCaoLuong.DCQSNKH:
                    TypeChuky = TypeChuKy.RPT_TL_DIEUCHINH_QUANSO_KEHOACH_NEW;
                    break;
                case BaoCaoLuong.CTQSRQ:
                    TypeChuky = TypeChuKy.RPT_TL_CHITIET_QS_RAQUAN_NEW;
                    break;
                case BaoCaoLuong.CTQSNH:
                    TypeChuky = TypeChuKy.RPT_TL_CHITIET_QS_NGHIHUU_NEW;
                    break;
                case BaoCaoLuong.DSCPPC:
                    TypeChuky = TypeChuKy.RPT_TL_DS_CAPPHAT_PHUCAP_NEW;
                    break;
                case BaoCaoLuong.BDQHKE:
                    TypeChuky = TypeChuKy.RPT_TL_BIENDONG_QUANHAM_KEHOACH_NEW;
                    break;
                case BaoCaoLuong.LKHMLNS:
                    TypeChuky = TypeChuKy.RPT_TL_QUYETTOAN_LUONG_NAM_KH_NEW;
                    break;
                case BaoCaoLuong.TLCCDSQ:
                    TypeChuky = TypeChuKy.RPT_TL_TRUYLINH_CHUYENCHEDO_SQ_NEW;
                    break;
                case BaoCaoLuong.TLCCDQNCN:
                    TypeChuky = TypeChuKy.RPT_TL_TRUYLINH_CHUYENCHEDO_QNCN_NEW;
                    break;
                case BaoCaoLuong.TLBCTA:
                    TypeChuky = TypeChuKy.RPT_TL_BAOCAO_TIENAN_NEW;
                    break;
                case BaoCaoLuong.BTLTT:
                    TypeChuky = TypeChuKy.RPT_TL_LUONG_TRUY_THU_NEW;
                    break;
                case BaoCaoLuong.GTCTRQXN:
                    TypeChuky = TypeChuKy.RPT_TL_GIAITHICH_CHITIET_RAQUAN_XUATNGU_NEW;
                    break;
                case BaoCaoLuong.BTLTLNN:
                    TypeChuky = TypeChuKy.RPT_TL_BANG_TRUYLINH_NHIEU_QD_NEW;
                    break;
                case BaoCaoLuong.CTQSTG:
                    TypeChuky = TypeChuKy.RPT_TL_CHITIET_QUANSO_TANGGIAM_NEW;
                    break;
                case BaoCaoLuong.QTQS:
                    TypeChuky = TypeChuKy.RPT_TL_QS_CHUNGTU_NEW;
                    break;
                case BaoCaoLuong.BTHLPCBP:
                    TypeChuky = TypeChuKy.RPT_TL_TONGHOP_LUONG_PHUCAP_BIENPHONG_NEW;
                    break;
                case BaoCaoLuong.GTPCTN:
                    TypeChuky = TypeChuKy.RPT_TL_GIAITHICH_PHUCAP_THEONGAY_NEW;
                    break;
            }
            OnPropertyChanged(nameof(TypeChuky));
        }

        private bool ListDonViFilter(object obj)
        {
            if (string.IsNullOrEmpty(_searchDonVi))
            {
                return true;
            }
            var item = (TlDmDonViNq104Model)obj;
            var condition = item.TenDonVi.ToLower().Contains(SearchDonVi.Trim().ToLower());
            return condition;
        }

        private bool ListPhuCapFilter(object obj)
        {
            if (string.IsNullOrEmpty(_searchPhuCap))
            {
                return true;
            }
            var item = (TlDmPhuCapNq104Model)obj;
            var condition = item.TenPhuCap.ToLower().Contains(SearchPhuCap.Trim().ToLower()) || item.MaPhuCap.ToLower().Contains(SearchPhuCap.Trim().ToLower());
            return condition;
        }

        private bool ListYeuToLuongDocFilter(object obj)
        {
            if (string.IsNullOrEmpty(_searchYeuToLuongDoc))
            {
                return true;
            }
            var item = (TlDmPhuCapNq104Model)obj;
            var condition = item.TenPhuCap.ToLower().Contains(SearchYeuToLuongDoc.Trim().ToLower()) || item.MaPhuCap.ToLower().Contains(SearchYeuToLuongDoc.Trim().ToLower());
            return condition;
        }

        private void OnExport()
        {
            string message = GetMessageValidate();
            if (!string.IsNullOrEmpty(message))
            {
                MessageBoxHelper.Warning(message);
            }
            else
            {
                if (string.IsNullOrEmpty(TypeChuky))
                {
                    TypeChuky = GetMaBaoCaoNotFound(Model.MaBaoCao);
                }
                ExportType exportType = SelectedFileExport != null && "PDF".Equals(SelectedFileExport.ValueItem) ? ExportType.PDF : ExportType.EXCEL;
                SavedAction?.Invoke(Model);
                switch (LoaiBaoCao)
                {
                    case BaoCaoLuong.DSCPL_A4_CU:
                    case BaoCaoLuong.BTHLPC:
                    case BaoCaoLuong.DSCPL_A3:
                    case BaoCaoLuong.GTPCCV:
                    case BaoCaoLuong.TTNCN_CB:
                    case BaoCaoLuong.DSCPPC:
                        if (IsSummary)
                        {
                            ExportDanhSachCapPhatPhuCapSummary(exportType);
                        }
                        else
                        {
                            ExportDanhSachCapPhatPhuCap(exportType);
                        }
                        break;
                    case BaoCaoLuong.GTCTPC_HQS:
                        ReportGiaiThichchiTietPhuCap(exportType);
                        break;
                    case BaoCaoLuong.INKIEM:
                        ReportInKiem(exportType);
                        break;
                    case BaoCaoLuong.THPCCV:
                        MessageBoxHelper.Info(Resources.FeatureIsUpdating);
                        break;
                    case BaoCaoLuong.DSCPL_A4:
                        if (IsThuNopBhxh)
                        {
                            if (IsShowBangBcLuongDong)
                            {
                                ExportBangLuongThangDong(exportType);
                            }
                            else
                            {
                                if ((IsSummary || IsSummaryNew) && !IsCheckedYeuToLuong)
                                {
                                    ExportThuNopBhxhSummary(exportType);
                                }
                                else
                                {
                                    ExportThuNopBhxh(exportType);
                                }
                            }
                        }
                        else if (IsShowBangBcLuongDong)
                        {
                            ExportBangLuongThangDong(exportType);
                        }
                        else
                        {
                            if ((IsSummary || IsSummaryNew) && !IsCheckedYeuToLuong)
                            {
                                ExportBangLuongThangSummary(exportType);
                            }
                            else
                            {
                                ExportBangLuongThang(exportType);
                            }
                        }
                        break;
                    case BaoCaoLuong.GTCTTL:
                        if (IsCheckedInChieuDoc)
                        {
                            ExportBangLuongThangTheoChieuDoc(exportType);
                        }
                        else
                        {
                            if (IsCheckedYeuToLuong && IsToanQuan)
                            {
                                ExportBangLuongThangTheoYeuToLuong(exportType);
                            }
                            else if (IsCheckedYeuToLuong && IsBienPhong)
                            {
                                if (SelectedBaoCao.ValueItem.Equals("1"))
                                {
                                    ExportGiaiThichPhuCapBienPhong(exportType);
                                }
                                else if (SelectedBaoCao.ValueItem.Equals("2"))
                                {
                                    ExportGiaiThichPhuCapBienPhongTheoHeSo(exportType, IsSummary);
                                }
                            }
                            else if (!IsCheckedYeuToLuong)
                            {
                                ExportChiTietBangLuongThang(exportType, IsSummary);
                            }
                        }
                        break;
                    case BaoCaoLuong.GTCTPC_TX:
                    case BaoCaoLuong.GTCTPC_NV:
                    case BaoCaoLuong.GTCTPC_TNK:
                    case BaoCaoLuong.GTCTPC_GTK:
                        if (IsSummary)
                        {
                            ExportGiaiThichChiTietPhuCapKhacSummary(exportType);
                        }
                        else
                        {
                            ExportGiaiThichChiTietPhuCapKhac(exportType);
                        }
                        break;
                    case BaoCaoLuong.TTNCN:
                        if (IsSummary)
                        {
                            ExportBangKeTrichThueTNCNSummary(exportType);
                        }
                        else
                        {
                            ExportBangKeTrichThueTNCN(exportType);
                        }
                        break;
                    case BaoCaoLuong.QTTTNCN:
                        if (IsSummary)
                        {
                            ExportThueThuNhapCaNhanCaNamSummary(exportType);
                        }
                        else
                        {
                            ExportThueThuNhapCaNhanCaNam(exportType);
                        }
                        break;
                    case BaoCaoLuong.BTLTL:
                        if (IsGiaiThichPCTLK)
                        {
                            if (IsSummary)
                            {
                                ExportBangLuongGiaiThichPhuCapTruyLinhKhacSummary(exportType);
                            }
                            else
                            {
                                ExportBangLuongGiaiThichPhuCapTruyLinhKhac(exportType);
                            }
                        }
                        else if (IsDynamicPhuCap)
                        {
                            if (IsSummary)
                            {
                                ExportBangLuongTruyLinhDongPhuCapSummary(exportType, true);
                            }
                            else
                            {
                                ExportBangLuongTruyLinhDongPhuCap(exportType, true);
                            }
                        }
                        else
                        {
                            if (IsSummary)
                            {
                                ExportBangLuongTruyLinhSummary(exportType, true);
                            }
                            else
                            {
                                ExportBangLuongTruyLinh(exportType, true);
                            }
                        }
                        break;
                    case BaoCaoLuong.BTLTLNN:
                        ExportBangLuongTruyLinhNhieuQuyetDinh(exportType);
                        break;
                    case BaoCaoLuong.BTLTT:
                        if (IsDynamicPhuCap)
                        {
                            ExportBangLuongTruyLinhDongPhuCap(exportType, false);
                        }
                        else
                        {
                            ExportBangLuongTruyLinh(exportType, false);
                        }
                        break;
                    case BaoCaoLuong.DSCTCN:
                        if (IsChiTraCaNhanKhac && IsSummary)
                        {
                            ExportChiTraNganHangThuNhapKhacIsummary(exportType);
                        }
                        else if (IsChiTraCaNhanKhac)
                        {
                            ExportChiTraNganHangThuNhapKhac(exportType);
                        }
                        else
                        {
                            ExportDanhSachChiTraNganHang(exportType, IsSummary);
                        }
                        break;
                    case BaoCaoLuong.DSCTCN_CT:
                        if (IsSummary)
                        {
                            ExportDanhSachChiTraCaNhanNganHangSummary(exportType);
                        }
                        else
                        {
                            ExportDanhSachChiTraCaNhanNganHang(exportType);
                        }
                        break;
                    case BaoCaoLuong.THLPC:
                        if (IsSummary)
                        {
                            ExportLuongTheoNgachSummary(exportType);
                        }
                        else
                        {
                            ExportLuongTheoNgach(exportType);
                        }
                        break;
                    case BaoCaoLuong.THLPC_DV:
                        ExportTHLuongDonVi(exportType);
                        break;
                    case BaoCaoLuong.THLPC_N_DV:
                        ExportTHLuongNgachDonVi(exportType);
                        break;
                    case BaoCaoLuong.THQSQT:
                        ExportQuyetToanQuanSo(exportType);
                        break;
                    case BaoCaoLuong.GTCTPCTNVKTHD:
                        ExportGiaiThichChiTietPCTNVKTHD(exportType);
                        break;
                    case BaoCaoLuong.DCQSNKH:
                        ExportDieuChinhQuanSoKeHoach(exportType);
                        break;
                    case BaoCaoLuong.CTQSRQ:
                        ExportQSRaQuan(exportType);
                        break;
                    case BaoCaoLuong.CTQSNH:
                        ExportQSNghiHuu(exportType);
                        break;
                    case BaoCaoLuong.QTQS:
                        if (IsSummary)
                        {
                            ExportQTQSMau2Summary(exportType);
                        }
                        else
                        {
                            ExportQTQSMau2(exportType);
                        }
                        break;
                    case BaoCaoLuong.BDQHKE:
                        ExportBienDongQuanHamKeHoach(exportType);
                        break;
                    case BaoCaoLuong.LKHMLNS:
                        ExportQuyetToanLuongKeHoach(exportType);
                        break;
                    case BaoCaoLuong.TLCCDSQ:
                        ExportChuyenCheDoSiQuan(exportType);
                        break;
                    case BaoCaoLuong.TLCCDQNCN:
                        ExportChuyenCheDoQNCN(exportType);
                        break;
                    case BaoCaoLuong.TLBCTA:
                        if (IsSummary)
                        {
                            ExportPhanTichTienAn(exportType);
                        }
                        else
                        {
                            ExportPhanTichTienAnSummary(exportType);
                        }
                        break;
                    case BaoCaoLuong.BLTBP:
                        if (IsSummary)
                        {
                            ExportBangLuongThangSummary(exportType, true);
                        }
                        else
                        {
                            ExportBangLuongThang(exportType, true);
                        }
                        break;
                    case BaoCaoLuong.GTCTRQXN:
                        ExportGiaiThichRaQuanXuatNgu(exportType);
                        break;
                    case BaoCaoLuong.CTQSTG:
                        ExportChiTietQuanSoTangGiam(exportType);
                        break;
                    case BaoCaoLuong.BTHLPCBP:
                        ExportBangTongHopLuongPhuCapBienPhong(exportType);
                        break;
                    case BaoCaoLuong.GTPCTN:
                        if (IsSummary)
                        {
                            ExportGiaiThichPhuCapTheoNgaySummary(exportType);
                        }
                        else
                        {
                            ExportGiaiThichPhuCapTheoNgay(exportType);
                        }
                        break;
                    default:
                        break;
                }
            }
        }

        private void ExportQuyetToanLuongKeHoach(ExportType exportType)
        {
            BackgroundWorkerHelper.Run((s, e) =>
            {
                IsLoading = true;
                List<ExportResult> results = new List<ExportResult>();
                string templateFileName = Path.Combine(ExportPrefix.PATH_TL_LUONG_NEW, ExportFileName.RPT_TL_QT_NAM_KEHOACH_NEW);
                var nam = int.Parse(SelectedYear.ValueItem);
                var thang = int.Parse(SelectedMonth.ValueItem);
                FormatNumber formatNumber = new FormatNumber(1, exportType);
                var lstDonVi = ItemsDonVi.Where(x => x.IsSelected);

                foreach (var item in lstDonVi)
                {
                    var maDonVi = item.MaDonVi;
                    string tenDonVi = string.Empty;
                    if (IsChecked)
                    {
                        tenDonVi = item.TenDonVi;
                    }

                    List<TlQtChungTuChiTietKeHoachNq104Model> data = new List<TlQtChungTuChiTietKeHoachNq104Model>();
                    data = TlRptLuongKeHoachModel.ItemsChungTuChiTiet
                        .Where(x => (x.TongCong.HasValue && x.TongCong != 0) ||
                            (x.TongNamTruoc.HasValue && x.TongNamTruoc != 0) ||
                            (x.DieuChinh.HasValue && x.DieuChinh != 0) ||
                            (x.ChenhLech.HasValue && x.ChenhLech != 0))
                        .ToList();
                    data = new List<TlQtChungTuChiTietKeHoachNq104Model>(data.OrderBy(x => x.XauNoiMa));
                    //data.Where(n => !n.BHangCha.GetValueOrDefault()).Select(n => { n.Lns = string.Empty; n.L = string.Empty; n.K = string.Empty; n.M = string.Empty; n.Tm = string.Empty; return n; }).ToList();
                    //data.Where(n => n.BHangCha.GetValueOrDefault() && !string.IsNullOrEmpty(n.M)).Select(n => { n.L = string.Empty; n.K = string.Empty; n.Lns = string.Empty; return n; }).ToList();
                    //data.Where(n => n.BHangCha.GetValueOrDefault() && !string.IsNullOrEmpty(n.Tm)).Select(n => { n.M = string.Empty; return n; }).ToList();

                    foreach (var itemChiTiet in data.Where(x => !string.IsNullOrEmpty(x.L)).OrderByDescending(x => x.XauNoiMa))
                    {
                        var parent = data.Where(x => x.MlnsId == itemChiTiet.MlnsIdParent).LastOrDefault();
                        if (parent != null && itemChiTiet.M != string.Empty)
                        {
                            if (!string.IsNullOrEmpty(parent.M))
                                itemChiTiet.M = string.Empty;
                            if (!string.IsNullOrEmpty(parent.Tm))
                                itemChiTiet.Tm = string.Empty;
                            if (!string.IsNullOrEmpty(parent.Ttm))
                                itemChiTiet.Ttm = string.Empty;
                            if (!string.IsNullOrEmpty(parent.Ng))
                                itemChiTiet.Ng = string.Empty;
                            if (!string.IsNullOrEmpty(parent.Tng))
                                itemChiTiet.Tng = string.Empty;
                            if (!string.IsNullOrEmpty(parent.Tng1))
                                itemChiTiet.Tng1 = string.Empty;
                            if (!string.IsNullOrEmpty(parent.Tng2))
                                itemChiTiet.Tng2 = string.Empty;
                            if (!string.IsNullOrEmpty(parent.Tng3))
                                itemChiTiet.Tng3 = string.Empty;
                        }
                    }

                    string title = string.Format("Năm {0}", nam);
                    Dictionary<string, object> dic = new Dictionary<string, object>();
                    dic.Add("Cap1", _danhMucService.FindDonViQuanLy(_sessionInfo.YearOfWork).ToUpper());
                    dic.Add("Cap2", GetHeader2Report());
                    dic.Add("FormatNumber", formatNumber);
                    dic.Add("TieuDe", title);
                    dic.Add("Items", data);
                    dic.Add("TongQuyetToan", TlRptLuongKeHoachModel.TongQuyetToan);
                    dic.Add("ChiPhiVaoQuan", TlRptLuongKeHoachModel.ChiPhiVaoQuan);
                    dic.Add("PhuCapRaQuan", TlRptLuongKeHoachModel.PhuCapRaQuan);
                    dic.Add("PhanTram", TlRptLuongKeHoachModel.PhanTram);
                    dic.Add("SaiSo", TlRptLuongKeHoachModel.SaiSo);
                    dic.Add("TongCong", TlRptLuongKeHoachModel.TongCong);
                    dic.Add("TenDonVi", tenDonVi.ToUpper());
                    dic.Add("DonViCha", _sessionService?.Current?.TenDonViReportHeader); // Đon vị cha dùng đúng đơn vị đang dùng phần mềm

                    string tenDonViCha = string.Empty;
                    if (!string.IsNullOrEmpty(item.ParentId))
                    {
                        var donViCha = _tlDmDonViService.FindAll().FirstOrDefault(x => x.MaDonVi.Equals(item.ParentId));
                        if (donViCha != null)
                        {
                            tenDonViCha = donViCha.TenDonVi.ToUpper();
                        }
                    }
                    //dic.Add("DonViCha", tenDonViCha);
                    AddChuKy(dic, TypeChuky);

                    string fileNamePrefix = string.Format("rpt_Bang_Luong_Ke_Hoach_Nam_{0}_{1}", nam, tenDonVi);
                    string fileNameWithoutExtension = StringUtils.CreateExportFileName(fileNamePrefix);
                    var xlsFile = _exportService.Export<TlQtChungTuChiTietKeHoachNq104Model>(templateFileName, dic);
                    results.Add(new ExportResult(string.Format("{0} - {1}", maDonVi, tenDonVi), fileNameWithoutExtension, null, xlsFile));
                }
                e.Result = results;
            }, (s, e) =>
            {
                if (e.Error == null)
                {
                    var result = (List<ExportResult>)e.Result;
                    _exportService.Open(result, exportType);
                }
                else
                {
                    _logger.Error(e.Error.Message);
                }
                IsLoading = false;
            });
        }

        private void ExportBangTongHopLuongPhuCapBienPhong(ExportType exportType)
        {
            BackgroundWorkerHelper.Run((s, e) =>
            {
                IsLoading = true;
                List<ExportResult> results = new List<ExportResult>();
                string templateFileName = Path.Combine(ExportPrefix.PATH_TL_LUONG_NEW, ExportFileName.RPT_TL_TONGHOP_LUONG_PHUCAP_BIENPHONG_NEW);
                var nam = int.Parse(SelectedYear.ValueItem);
                var thang = int.Parse(SelectedMonth.ValueItem);
                FormatNumber formatNumber = new FormatNumber(1, exportType);
                var lstDonVi = ItemsDonVi.Where(x => x.IsSelected);
                var listDonViSelect = _mapper.Map<List<TlDmDonViNq104>>(lstDonVi);

                _tlBangLuongBridgeService.DataPreprocess(thang, nam, string.Join(",", listDonViSelect.Select(x => x.MaDonVi)), "CACH0");

                DataTable dataAll = _tlBangLuongThangService.GetDataBangLuongPhuCapTongHopBienPhong(listDonViSelect, thang, nam);

                foreach (var item in lstDonVi)
                {
                    var tenDonVi = item.TenDonVi;
                    var maDonVi = item.MaDonVi;
                    string title = string.Format("Năm {0}", nam);
                    Dictionary<string, object> dic = new Dictionary<string, object>();
                    dic.Add("Cap1", _danhMucService.FindDonViQuanLy(_sessionInfo.YearOfWork).ToUpper());
                    dic.Add("Cap2", GetHeader2Report());
                    dic.Add("DonViTinh", "Đơn vị tính: " + (_selectedUnitType != null ? _selectedUnitType.DisplayItem : ""));
                    dic.Add("FormatNumber", formatNumber);
                    dic.Add("TieuDe2", title);
                    dic.Add("Items", dataAll);
                    dic.Add("TenDonVi", tenDonVi.ToUpper());

                    string tenDonViCha = string.Empty;
                    if (!string.IsNullOrEmpty(item.ParentId))
                    {
                        var donViCha = _tlDmDonViService.FindAll().FirstOrDefault(x => x.MaDonVi.Equals(item.ParentId));
                        if (donViCha != null)
                        {
                            tenDonViCha = donViCha.TenDonVi.ToUpper();
                        }
                    }
                    AddChuKy(dic, TypeChuky);

                    string fileNamePrefix = string.Format("rpt_Tong_Hop_Luong_PhuCap_BienPhong_Thang_{0}_Nam_{0}_{1}", thang, nam, tenDonVi);
                    string fileNameWithoutExtension = StringUtils.CreateExportFileName(fileNamePrefix);
                    var xlsFile = _exportService.Export<TlQtChungTuChiTietKeHoachNq104Model>(templateFileName, dic);
                    results.Add(new ExportResult(string.Format("{0} - {1}", maDonVi, tenDonVi), fileNameWithoutExtension, null, xlsFile));
                }
                e.Result = results;
            }, (s, e) =>
            {
                if (e.Error == null)
                {
                    var result = (List<ExportResult>)e.Result;
                    _exportService.Open(result, exportType);
                }
                else
                {
                    _logger.Error(e.Error.Message);
                }
                IsLoading = false;
            });
        }

        private void ExportDanhSachCapPhatPhuCap(ExportType exportType)
        {
            try
            {
                BackgroundWorkerHelper.Run((s, e) =>
                {
                    IsLoading = true;

                    List<ExportResult> results = new List<ExportResult>();
                    string templateFileName = Path.Combine(ExportPrefix.PATH_TL_LUONG_NEW, ExportFileName.RPT_TL_DS_CAPPHAT_PHUCAP_NEW);
                    if (SelectedKhoIn.ValueItem == "A3")
                    {
                        templateFileName = Path.Combine(ExportPrefix.PATH_TL_LUONG_NEW, ExportFileName.RPT_TL_DS_CAPPHAT_PHUCAP_A3_NEW);
                    }

                    int donViTinh = int.Parse(_selectedUnitType.ValueItem);
                    var nam = int.Parse(SelectedYear.ValueItem);
                    var thang = int.Parse(SelectedMonth.ValueItem);
                    FormatNumber formatNumber = new FormatNumber(donViTinh, exportType);

                    var lstDonVi = ItemsDonVi.Where(x => x.IsSelected);
                    var listDonViSelect = _mapper.Map<List<TlDmDonViNq104>>(lstDonVi);
                    var dsNgach = new Dictionary<string, string>()
                    {
                        { "4", "HSQ - CS"}
                    };

                    _tlBangLuongBridgeService.DataPreprocess(thang, nam, string.Join(",", lstDonVi.Select(x => x.MaDonVi)), "CACH0");

                    DataTable dataAll = _tlBangLuongThangService.GetDataDanhSachCapPhat(listDonViSelect, thang, nam);
                    for (int i = 0; i < dataAll.Rows.Count; i++)
                    {
                        int tmp = Convert.ToInt32(dataAll.Rows[i][PhuCap.NTN]);
                        dataAll.Rows[i][PhuCap.NTN] = tmp > 0 ? tmp : 0;
                    }

                    foreach (var item in listDonViSelect)
                    {
                        var maDonVi = item.MaDonVi;
                        string tenDonVi = string.Empty;
                        if (IsChecked)
                        {
                            tenDonVi = item.TenDonVi;
                        }

                        var items = _tlBangLuongThangService.ReportBangLuongThang(item, dataAll, donViTinh, IsOrderTheoChucVu, dsNgach);
                        if (items.Rows.Count == 0)
                        {
                            continue;
                        }

                        Dictionary<string, object> data = new Dictionary<string, object>();
                        data.Add("Cap1", _iDanhMucService.FindDonViQuanLy(_sessionInfo.YearOfWork).ToUpper());
                        data.Add("Cap2", GetHeader2Report());
                        data.Add("FormatNumber", formatNumber);
                        data.Add("DonViTinh", "Đơn vị tính: " + (_selectedUnitType != null ? _selectedUnitType.DisplayItem : ""));
                        data.Add("TieuDe2", string.Format("Tháng {0} năm {1}", thang, nam));
                        data.Add("TenDonVi", tenDonVi.ToUpper());
                        data.Add("DonViCha", _sessionService?.Current?.TenDonViReportHeader);
                        data.Add("Unit", donViTinh);
                        data.Add("Items", items);
                        data.Add("ReportName", ReportName);
                        AddChuKy(data, TypeChuky);

                        var xlsFile = _exportService.Export(templateFileName, data);
                        string fileNamePrefix = string.Format("rpt_Danh_Sach_CapPhat_PhuCap_{0}_Nam_{1}_{2}", thang, nam, tenDonVi);
                        string fileNameWithoutExtension = StringUtils.CreateExportFileName(fileNamePrefix);
                        results.Add(new ExportResult(string.Format("{0} - {1}", item.MaDonVi, item.TenDonVi), fileNameWithoutExtension, null, xlsFile));
                    }
                    e.Result = results;
                }, (s, e) =>
                {
                    if (e.Error == null)
                    {
                        var result = (List<ExportResult>)e.Result;
                        _exportService.Open(result, exportType);
                    }
                    else
                    {
                        _logger.Error(e.Error.Message);
                    }
                    IsLoading = false;
                });
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        private void ExportDanhSachCapPhatPhuCapSummary(ExportType exportType)
        {
            try
            {
                BackgroundWorkerHelper.Run((s, e) =>
                {
                    IsLoading = true;

                    List<ExportResult> results = new List<ExportResult>();
                    string templateFileName = Path.Combine(ExportPrefix.PATH_TL_LUONG_NEW, ExportFileName.RPT_TL_DS_CAPPHAT_PHUCAP_TONGHOP_NEW);
                    if (SelectedKhoIn.ValueItem == "A3")
                    {
                        templateFileName = Path.Combine(ExportPrefix.PATH_TL_LUONG_NEW, ExportFileName.RPT_TL_DS_CAPPHAT_PHUCAP_TONGHOP_A3_NEW);
                    }

                    int donViTinh = int.Parse(_selectedUnitType.ValueItem);
                    var nam = int.Parse(SelectedYear.ValueItem);
                    var thang = int.Parse(SelectedMonth.ValueItem);
                    FormatNumber formatNumber = new FormatNumber(donViTinh, exportType);

                    var lstDonVi = ItemsDonVi.Where(x => x.IsSelected);
                    var listDonViSelect = _mapper.Map<List<TlDmDonViNq104>>(lstDonVi);
                    var dsNgach = new Dictionary<string, string>()
                    {
                        { "4", "HSQ - CS"}
                    };

                    var items = _tlBangLuongThangService.ReportBangLuongThang(listDonViSelect, thang, nam, donViTinh, IsOrderTheoChucVu, IsGiaTriAm, dsNgach, true, IsReduceBHXH);
                    //DataTable items = _tlBangLuongThangService.ReportBangLuongThang(lstDonViSelect, thang, nam, donViTinh, IsOrderTheoChucVu, IsGiaTriAm, dsNgach);

                    Dictionary<string, object> data = new Dictionary<string, object>();
                    data.Add("Cap1", _iDanhMucService.FindDonViQuanLy(_sessionInfo.YearOfWork).ToUpper());
                    data.Add("Cap2", GetHeader2Report());
                    data.Add("FormatNumber", formatNumber);
                    data.Add("DonViTinh", "Đơn vị tính: " + (_selectedUnitType != null ? _selectedUnitType.DisplayItem : ""));
                    data.Add("TieuDe2", string.Format("Tháng {0} năm {1}", thang, nam));
                    data.Add("DonViCha", _sessionService?.Current?.TenDonViReportHeader);
                    data.Add("Unit", donViTinh);
                    data.Add("Items", items);
                    data.Add("ReportName", ReportName);
                    AddChuKy(data, _typeChuky);

                    var xlsFile = _exportService.Export(templateFileName, data);
                    string fileNamePrefix = string.Format("rpt_Danh_Sach_CapPhat_PhuCap_TongHop_{0}_Nam_{1}", thang, nam);
                    string fileNameWithoutExtension = StringUtils.CreateExportFileName(fileNamePrefix);
                    results.Add(new ExportResult(String.Format("TongHop_Thang_{0}_Nam_{1}", thang, nam), fileNameWithoutExtension, null, xlsFile));

                    e.Result = results;
                }, (s, e) =>
                {
                    if (e.Error == null)
                    {
                        var result = (List<ExportResult>)e.Result;
                        _exportService.Open(result, exportType);
                    }
                    else
                    {
                        _logger.Error(e.Error.Message);
                    }
                    IsLoading = false;
                });
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        private void ExportQSNghiHuu(ExportType exportType)
        {
            try
            {
                BackgroundWorkerHelper.Run((s, e) =>
                {
                    IsLoading = true;

                    List<ExportResult> results = new List<ExportResult>();
                    string templateFileName = Path.Combine(ExportPrefix.PATH_TL_LUONG_NEW, ExportFileName.RPT_TL_CHITIET_QS_NGHIHUU_KEHOACH_NEW);
                    string fileNamePrefix;
                    string fileNameWithoutExtension;

                    var lstDonVi = ItemsDonVi.Where(x => x.IsSelected);
                    foreach (var item in lstDonVi)
                    {
                        var nam = int.Parse(SelectedYear.ValueItem);
                        var maDonVi = item.MaDonVi;
                        string tenDonVi = string.Empty;
                        if (IsChecked)
                        {
                            tenDonVi = item.TenDonVi;
                        }

                        var predicate = PredicateBuilder.True<TlDmCanBoKeHoachNq104>();
                        predicate = predicate.And(x => x.Nam == nam);
                        predicate = predicate.And(x => x.Parent == maDonVi);
                        predicate = predicate.And(x => x.IsDelete == false);
                        predicate = predicate.And(x => x.MaCb.StartsWith("1") || x.MaCb.StartsWith("2"));

                        var itemsEntity = _tlDmCanBoKeHoachService.FindByCondition(predicate);
                        var items = _mapper.Map<ObservableCollection<TlDmCanBoKeHoachNq104Model>>(itemsEntity);

                        var items1 = items.Where(x => x.MaCb.StartsWith("1")).ToList();
                        var items2 = items.Where(x => x.MaCb.StartsWith("2")).ToList();

                        int index = 0;
                        int index1 = 0;
                        foreach (var item1 in items1)
                        {
                            item1.Stt = ++index;
                        }

                        foreach (var item2 in items2)
                        {
                            item2.Stt = ++index1;
                        }

                        var lstData = new List<TlDmCanBoKeHoachNq104Model>();
                        if (items1.Count > 0)
                        {
                            var siQuan = new TlDmCanBoKeHoachNq104Model();
                            siQuan.TenCanBo = "Sĩ Quan";
                            siQuan.IsHangCha = true;
                            lstData.Add(siQuan);
                            lstData.AddRange(items1);
                        }

                        if (items2.Count > 0)
                        {
                            var qncn = new TlDmCanBoKeHoachNq104Model();
                            qncn.TenCanBo = "QNCN";
                            qncn.IsHangCha = true;
                            lstData.Add(qncn);
                            lstData.AddRange(items2);
                        }

                        Dictionary<string, object> data = new Dictionary<string, object>();
                        data.Add("Cap1", _iDanhMucService.FindDonViQuanLy(_sessionInfo.YearOfWork).ToUpper());
                        data.Add("Cap2", GetHeader2Report());
                        data.Add("Nam", nam);
                        data.Add("DonVi", tenDonVi.ToUpper());

                        if (!string.IsNullOrEmpty(item.ParentId))
                        {
                            var donViCha = _tlDmDonViService.FindAll().FirstOrDefault(x => x.MaDonVi.Equals(item.ParentId));
                            if (donViCha != null)
                            {
                                data.Add("DonViCha", donViCha.TenDonVi.ToUpper());
                            }
                            else
                            {
                                data.Add("DonViCha", string.Empty);
                            }
                        }
                        else
                        {
                            data.Add("DonViCha", string.Empty);
                        }

                        data.Add("Items", lstData);
                        data.Add("ReportName", ReportName);
                        AddChuKy(data, TypeChuky);

                        fileNamePrefix = string.Format("rpt_ChiTiet_QuanSo_NghiHuu_KeHoach_Nam_{0}_{1}", nam, tenDonVi);
                        fileNameWithoutExtension = StringUtils.CreateExportFileName(fileNamePrefix);
                        var xlsFile = _exportService.Export<TlDmCanBoKeHoachNq104Model>(templateFileName, data);
                        results.Add(new ExportResult(string.Format("{0} - {1}", maDonVi, tenDonVi), fileNameWithoutExtension, null, xlsFile));
                    }
                    e.Result = results;
                }, (s, e) =>
                {
                    if (e.Error == null)
                    {
                        var result = (List<ExportResult>)e.Result;
                        _exportService.Open(result, exportType);
                    }
                    else
                    {
                        _logger.Error(e.Error.Message);
                    }
                    IsLoading = false;
                });
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        private void ExportQSRaQuan(ExportType exportType)
        {
            try
            {
                BackgroundWorkerHelper.Run((s, e) =>
                {
                    IsLoading = true;

                    List<ExportResult> results = new List<ExportResult>();
                    string templateFileName = Path.Combine(ExportPrefix.PATH_TL_LUONG_NEW, ExportFileName.RPT_TL_CHITIET_QS_RAQUAN_KEHOACH_NEW);
                    string fileNamePrefix;
                    string fileNameWithoutExtension;

                    var lstDonVi = ItemsDonVi.Where(x => x.IsSelected);
                    foreach (var item in lstDonVi)
                    {
                        var nam = int.Parse(SelectedYear.ValueItem);
                        var maDonVi = item.MaDonVi;
                        string tenDonVi = string.Empty;
                        if (IsChecked)
                        {
                            tenDonVi = item.TenDonVi;
                        }

                        var predicate = PredicateBuilder.True<TlDmCanBoKeHoachNq104>();
                        predicate = predicate.And(x => x.Nam == nam);
                        predicate = predicate.And(x => x.Parent == maDonVi);
                        predicate = predicate.And(x => x.IsDelete == false);
                        predicate = predicate.And(x => x.MaCb.StartsWith("0"));

                        var itemsEntity = _tlDmCanBoKeHoachService.FindByCondition(predicate).ToList();
                        var items = _mapper.Map<ObservableCollection<TlDmCanBoKeHoachNq104Model>>(itemsEntity);
                        int index = 0;
                        foreach (var item1 in items)
                        {
                            item1.Stt = ++index;
                        }

                        Dictionary<string, object> data = new Dictionary<string, object>();
                        data.Add("Cap1", _iDanhMucService.FindDonViQuanLy(_sessionInfo.YearOfWork).ToUpper());
                        data.Add("Cap2", GetHeader2Report());
                        data.Add("Nam", nam);
                        data.Add("DonVi", tenDonVi.ToUpper());

                        if (!string.IsNullOrEmpty(item.ParentId))
                        {
                            var donViCha = _tlDmDonViService.FindAll().FirstOrDefault(x => x.MaDonVi.Equals(item.ParentId));
                            if (donViCha != null)
                            {
                                data.Add("DonViCha", donViCha.TenDonVi.ToUpper());
                            }
                            else
                            {
                                data.Add("DonViCha", string.Empty);
                            }
                        }
                        else
                        {
                            data.Add("DonViCha", string.Empty);
                        }

                        data.Add("Items", items);
                        data.Add("ReportName", ReportName);
                        AddChuKy(data, TypeChuky);

                        fileNamePrefix = string.Format("rpt_ChiTiet_QuanSo_RaQuan_KeHoach_Nam_{0}_{1}", nam, tenDonVi);
                        fileNameWithoutExtension = StringUtils.CreateExportFileName(fileNamePrefix);
                        var xlsFile = _exportService.Export<TlDmCanBoKeHoachNq104Model>(templateFileName, data);
                        results.Add(new ExportResult(string.Format("{0} - {1}", maDonVi, tenDonVi), fileNameWithoutExtension, null, xlsFile));
                    }
                    e.Result = results;
                }, (s, e) =>
                {
                    if (e.Error == null)
                    {
                        var result = (List<ExportResult>)e.Result;
                        _exportService.Open(result, exportType);
                    }
                    else
                    {
                        _logger.Error(e.Error.Message);
                    }
                    IsLoading = false;
                });
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        private void ExportDieuChinhQuanSoKeHoach(ExportType exportType)
        {
            try
            {
                BackgroundWorkerHelper.Run((s, e) =>
                {
                    IsLoading = true;

                    List<ExportResult> results = new List<ExportResult>();
                    string templateFileName = Path.Combine(ExportPrefix.PATH_TL_LUONG_NEW, ExportFileName.RPT_TL_DIEUCHINH_QS_KEHOACH_NEW);
                    string fileNamePrefix;
                    string fileNameWithoutExtension;

                    int donViTinh = int.Parse(_selectedUnitType.ValueItem);
                    FormatNumber formatNumber = new FormatNumber(donViTinh, exportType);
                    var lstDonVi = ItemsDonVi.Where(x => x.IsSelected);
                    foreach (var item in lstDonVi)
                    {
                        int nam = int.Parse(SelectedYear.ValueItem);
                        string tenDonVi = string.Empty;
                        if (IsChecked)
                        {
                            tenDonVi = item.TenDonVi;
                        }

                        var lstData = _tlDieuChinhQsKeHoachService.FindData(nam, item.MaDonVi).ToList();
                        foreach (var item1 in lstData)
                        {
                            item1.FPcrqBinhNhat /= donViTinh;
                            item1.FPcrqBinhNhi /= donViTinh;
                            item1.FPcrqHaSi /= donViTinh;
                            item1.FPcrqTrungSi /= donViTinh;
                            item1.FPcrqThuongSi /= donViTinh;
                        }

                        Dictionary<string, object> data = new Dictionary<string, object>();
                        data.Add("Cap1", _iDanhMucService.FindDonViQuanLy(_sessionInfo.YearOfWork).ToUpper());
                        data.Add("Cap2", GetHeader2Report());
                        data.Add("FormatNumber", formatNumber);
                        data.Add("DonViTinh", "Đơn vị tính: " + (_selectedUnitType != null ? _selectedUnitType.DisplayItem : ""));
                        data.Add("Nam", nam);
                        data.Add("DonVi", tenDonVi.ToUpper());

                        if (!string.IsNullOrEmpty(item.ParentId))
                        {
                            var donViCha = _tlDmDonViService.FindAll().FirstOrDefault(x => x.MaDonVi.Equals(item.ParentId));
                            if (donViCha != null)
                            {
                                data.Add("DonViCha", donViCha.TenDonVi.ToUpper());
                            }
                            else
                            {
                                data.Add("DonViCha", string.Empty);
                            }
                        }
                        else
                        {
                            data.Add("DonViCha", string.Empty);
                        }

                        data.Add("Items", lstData);
                        data.Add("ReportName", ReportName);
                        AddChuKy(data, TypeChuky);

                        fileNamePrefix = string.Format("rpt_DieuChinh_QuanSo_Nam_{0}_KeHoach_{1}", nam, tenDonVi);
                        fileNameWithoutExtension = StringUtils.CreateExportFileName(fileNamePrefix);
                        var xlsFile = _exportService.Export<TlRptQuanSoKeHoachQuery>(templateFileName, data);
                        results.Add(new ExportResult(string.Format("{0} - {1}", item.MaDonVi, item.TenDonVi), fileNameWithoutExtension, null, xlsFile));
                    }
                    e.Result = results;
                }, (s, e) =>
                {
                    if (e.Error == null)
                    {
                        var result = (List<ExportResult>)e.Result;
                        _exportService.Open(result, exportType);
                    }
                    else
                    {
                        _logger.Error(e.Error.Message);
                    }
                    IsLoading = false;
                });
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        private void ExportBangLuongTruyLinhDongPhuCap(ExportType exportType, bool isTruyLinh)
        {
            try
            {
                BackgroundWorkerHelper.Run((s, e) =>
                {
                    IsLoading = true;

                    List<ExportResult> results = new List<ExportResult>();
                    string templateFileName;
                    int donViTinh = int.Parse(_selectedUnitType.ValueItem);
                    var nam = int.Parse(SelectedYear.ValueItem);
                    var thang = int.Parse(SelectedMonth.ValueItem);
                    var lstDonVi = _mapper.Map<List<TlDmDonViNq104>>(ItemsDonVi.Where(x => x.IsSelected));
                    var lstPhuCapDynamic = GetPhuCapTruyLinhKhacSum();
                    var lstPhuCap = _mapper.Map<List<TlDmPhuCapNq104>>(lstPhuCapDynamic);
                    // Set condition and data

                    Dictionary<string, List<TlDmPhuCapNq104Model>> cacKhoan;
                    //string condtion;
                    if (isTruyLinh)
                    {
                        // Set header
                        List<TlDmPhuCapNq104Model> lstPhuCapDuocHuong = new List<TlDmPhuCapNq104Model>()
                        {
                            new TlDmPhuCapNq104Model() { TenPhuCap = "Số tháng truy lĩnh" },
                            new TlDmPhuCapNq104Model() { TenPhuCap = "Lương cơ bản" },
                            new TlDmPhuCapNq104Model() { TenPhuCap = "Phụ cấp chức vụ" },
                            new TlDmPhuCapNq104Model() { TenPhuCap = "Phụ cấp công vụ" },
                            new TlDmPhuCapNq104Model() { TenPhuCap = "Phụ cấp thâm niên" }
                        };

                        lstPhuCapDuocHuong.AddRange(lstPhuCapDynamic);
                        lstPhuCapDuocHuong.Add(new TlDmPhuCapNq104Model() { TenPhuCap = "Tổng cộng" });

                        List<TlDmPhuCapNq104Model> lstTruBHXH = new List<TlDmPhuCapNq104Model>() { new TlDmPhuCapNq104Model() { TenPhuCap = "Trừ BHXH" } };
                        List<TlDmPhuCapNq104Model> lstSoTienDuocNhan = new List<TlDmPhuCapNq104Model>() { new TlDmPhuCapNq104Model() { TenPhuCap = "Số tiền được nhận" } };
                        List<TlDmPhuCapNq104Model> lstKy = new List<TlDmPhuCapNq104Model>() { new TlDmPhuCapNq104Model() { TenPhuCap = "Ký" } };

                        cacKhoan = new Dictionary<string, List<TlDmPhuCapNq104Model>>()
                        {
                            { "Các khoản được hưởng", lstPhuCapDuocHuong },
                            { "Trừ BHXH", lstTruBHXH },
                            { "Số tiền được nhận", lstSoTienDuocNhan },
                            { "Ký", lstKy}
                        };

                        templateFileName = Path.Combine(ExportPrefix.PATH_TL_LUONG_NEW, ExportFileName.RPT_TL_LUONG_TRUY_LINH_DONG_PHU_CAP_NEW);
                        if (lstPhuCapDynamic.Count > 2)
                        {
                            templateFileName = Path.Combine(ExportPrefix.PATH_TL_LUONG_NEW, ExportFileName.RPT_TL_LUONG_TRUY_LINH_DONG_PHU_CAP2_NEW);
                        }
                    }
                    else
                    {
                        // Set header
                        List<TlDmPhuCapNq104Model> lstPhuCapDuocHuong = new List<TlDmPhuCapNq104Model>()
                        {
                            new TlDmPhuCapNq104Model() { TenPhuCap = "Số tháng truy thu" },
                            new TlDmPhuCapNq104Model() { TenPhuCap = "Lương cơ bản" },
                            new TlDmPhuCapNq104Model() { TenPhuCap = "Phụ cấp chức vụ" },
                            new TlDmPhuCapNq104Model() { TenPhuCap = "Phụ cấp công vụ" },
                            new TlDmPhuCapNq104Model() { TenPhuCap = "Phụ cấp thâm niên" }
                        };

                        lstPhuCapDuocHuong.AddRange(lstPhuCapDynamic);
                        lstPhuCapDuocHuong.Add(new TlDmPhuCapNq104Model() { TenPhuCap = "Tổng cộng" });

                        List<TlDmPhuCapNq104Model> lstTruBHXH = new List<TlDmPhuCapNq104Model>() { new TlDmPhuCapNq104Model() { TenPhuCap = "Trừ BHXH" } };
                        List<TlDmPhuCapNq104Model> lstSoTienDuocNhan = new List<TlDmPhuCapNq104Model>() { new TlDmPhuCapNq104Model() { TenPhuCap = "Số tiền truy thu" } };
                        List<TlDmPhuCapNq104Model> lstKy = new List<TlDmPhuCapNq104Model>() { new TlDmPhuCapNq104Model() { TenPhuCap = "Ký" } };

                        cacKhoan = new Dictionary<string, List<TlDmPhuCapNq104Model>>()
                        {
                            { "Số tiền phụ cấp bị truy thu", lstPhuCapDuocHuong },
                            { "Trừ BHXH", lstTruBHXH },
                            { "Số tiền truy thu", lstSoTienDuocNhan },
                            { "Ký", lstKy}
                        };

                        templateFileName = Path.Combine(ExportPrefix.PATH_TL_LUONG_NEW, ExportFileName.RPT_TL_LUONG_TRUY_THU_DONG_PHU_CAP_NEW);
                        if (lstPhuCapDynamic.Count > 2)
                        {
                            templateFileName = Path.Combine(ExportPrefix.PATH_TL_LUONG_NEW, ExportFileName.RPT_TL_LUONG_TRUY_THU_DONG_PHU_CAP2_NEW);
                        }
                    }

                    // Xử lý header
                    List<HeaderReportLuongTruyLinhNq104DongPhuCap> headers = new List<HeaderReportLuongTruyLinhNq104DongPhuCap>();
                    var columnStart = 6;
                    foreach (var khoan in cacKhoan)
                    {
                        var mergeRange = "";
                        var columnStartName = GetExcelColumnName(columnStart);
                        var columnEndName = GetExcelColumnName(khoan.Value.Count + columnStart - 1);
                        mergeRange = columnStartName + "7" + ":" + columnEndName + "7";

                        // Tinh range merge BHXH, STN, Ky
                        var mergeRangeRow = columnStartName + "7" + ":" + columnStartName + "8";

                        HeaderReportLuongTruyLinhNq104DongPhuCap header = new HeaderReportLuongTruyLinhNq104DongPhuCap();
                        header.MergeRange = mergeRange;
                        header.MergeRangeRow = mergeRangeRow;
                        header.LstHeader1 = new List<TlDmPhuCapNq104Model>();
                        header.LstHeader2 = new List<TlDmPhuCapNq104Model>();
                        header.TenNhomPhuCap = khoan.Key;
                        TlDmPhuCapNq104Model subHeader;
                        foreach (var phuCap in khoan.Value)
                        {
                            subHeader = new TlDmPhuCapNq104Model();
                            subHeader.SttExport = !header.LstHeader1.Any() ? 1 : 2;
                            subHeader.TenPhuCap = khoan.Key;
                            if (khoan.Value.Count == 1)
                            {
                                // Nếu có 1 phần tử thì merge 2 row
                                subHeader.MaPhuCap = "1";
                            }
                            header.LstHeader1.Add(subHeader);
                            header.LstHeader2.Add(phuCap);
                        }

                        if (khoan.Value.Any())
                        {
                            headers.Add(header);
                        }
                    }

                    var data = _tlBangLuongThangService.ReportBangLuongTruyLinhDongPhuCap(lstDonVi, lstPhuCap, nam, thang, CachTinhLuong.CACH5, isTruyLinh, donViTinh, IsOrderTheoChucVu);
                    foreach (var item in lstDonVi)
                    {
                        string maDonVi = item.MaDonVi;
                        string tenDonVi = string.Empty;
                        if (IsChecked)
                        {
                            tenDonVi = item.TenDonVi;
                        }

                        var tenDonViCha = _sessionService?.Current?.TenDonViReportHeader;

                        var items = _tlBangLuongThangService.ReportBangLuongTruyLinhDongPhuCap(data, item, lstPhuCap, isTruyLinh);
                        if (items.Count == 0)
                        {
                            continue;
                        }
                        Dictionary<string, object> report = new Dictionary<string, object>();
                        FormatNumber formatNumber = new FormatNumber(donViTinh, exportType);
                        report.Add("Cap1", _danhMucService.FindDonViQuanLy(_sessionInfo.YearOfWork).ToUpper());
                        report.Add("Cap2", GetHeader2Report());
                        report.Add("FormatNumber", formatNumber);
                        report.Add("DonViTinh", "Đơn vị tính: " + (_selectedUnitType != null ? _selectedUnitType.DisplayItem : ""));
                        report.Add("TieuDe2", string.Format("Tháng {0} năm {1}", thang, nam));
                        report.Add("TenDonVi", tenDonVi);
                        report.Add("DonViCha", tenDonViCha);
                        report.Add("Unit", donViTinh);
                        report.Add("ReportName", ReportName);
                        report.Add("Headers", headers);
                        report.Add("Items", items["Items"]);
                        report.Add("ItemsTotal", items["ItemsTotal"]);
                        AddChuKy(report, TypeChuky);

                        var xlsFile = _exportService.Export<ReportBangLuongTruyLinhDongPhuCapNq104Query, HeaderReportLuongTruyLinhNq104DongPhuCap>(templateFileName, report);
                        string fileNamePrefix = string.Format("rpt_Bang_Luong_Thang_Truy_Linh_Thang_{0}_Nam_{1}_{2}", thang, nam, maDonVi);
                        string fileNameWithoutExtension = StringUtils.CreateExportFileName(fileNamePrefix);
                        results.Add(new ExportResult(string.Format("{0} - {1}", maDonVi, tenDonVi), fileNameWithoutExtension, null, xlsFile));
                    }
                    e.Result = results;
                }, (s, e) =>
                {
                    if (e.Error == null)
                    {
                        var result = (List<ExportResult>)e.Result;
                        _exportService.Open(result, exportType);
                    }
                    else
                    {
                        _logger.Error(e.Error.Message);
                    }
                    IsLoading = false;
                });
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        private void ExportBangLuongTruyLinhDongPhuCapSummary(ExportType exportType, bool isTruyLinh)
        {
            try
            {
                BackgroundWorkerHelper.Run((s, e) =>
                {
                    IsLoading = true;

                    List<ExportResult> results = new List<ExportResult>();
                    string templateFileName;
                    int donViTinh = int.Parse(_selectedUnitType.ValueItem);
                    var nam = int.Parse(SelectedYear.ValueItem);
                    var thang = int.Parse(SelectedMonth.ValueItem);
                    var lstDonVi = _mapper.Map<List<TlDmDonViNq104>>(ItemsDonVi.Where(x => x.IsSelected));
                    var lstPhuCapDynamic = GetPhuCapTruyLinhKhacSum();
                    var lstPhuCap = _mapper.Map<List<TlDmPhuCapNq104>>(lstPhuCapDynamic);
                    // Set condition and data

                    Dictionary<string, List<TlDmPhuCapNq104Model>> cacKhoan;
                    //string condtion;
                    if (isTruyLinh)
                    {
                        // Set header
                        List<TlDmPhuCapNq104Model> lstPhuCapDuocHuong = new List<TlDmPhuCapNq104Model>()
                        {
                            new TlDmPhuCapNq104Model() { TenPhuCap = "Số tháng truy lĩnh" },
                            new TlDmPhuCapNq104Model() { TenPhuCap = "Lương cơ bản" },
                            new TlDmPhuCapNq104Model() { TenPhuCap = "Phụ cấp chức vụ" },
                            new TlDmPhuCapNq104Model() { TenPhuCap = "Phụ cấp công vụ" },
                            new TlDmPhuCapNq104Model() { TenPhuCap = "Phụ cấp thâm niên" }
                        };

                        lstPhuCapDuocHuong.AddRange(lstPhuCapDynamic);
                        lstPhuCapDuocHuong.Add(new TlDmPhuCapNq104Model() { TenPhuCap = "Tổng cộng" });

                        List<TlDmPhuCapNq104Model> lstTruBHXH = new List<TlDmPhuCapNq104Model>() { new TlDmPhuCapNq104Model() { TenPhuCap = "Trừ BHXH" } };
                        List<TlDmPhuCapNq104Model> lstSoTienDuocNhan = new List<TlDmPhuCapNq104Model>() { new TlDmPhuCapNq104Model() { TenPhuCap = "Số tiền được nhận" } };
                        List<TlDmPhuCapNq104Model> lstKy = new List<TlDmPhuCapNq104Model>() { new TlDmPhuCapNq104Model() { TenPhuCap = "Ký" } };

                        cacKhoan = new Dictionary<string, List<TlDmPhuCapNq104Model>>()
                        {
                            { "Các khoản được hưởng", lstPhuCapDuocHuong },
                            { "Trừ BHXH", lstTruBHXH },
                            { "Số tiền được nhận", lstSoTienDuocNhan },
                            { "Ký", lstKy}
                        };

                        templateFileName = Path.Combine(ExportPrefix.PATH_TL_LUONG_NEW, ExportFileName.RPT_TL_LUONG_TRUY_LINH_DONG_PHU_CAP_TONG_HOP_NEW);
                        if (lstPhuCapDynamic.Count > 2)
                        {
                            templateFileName = Path.Combine(ExportPrefix.PATH_TL_LUONG_NEW, ExportFileName.RPT_TL_LUONG_TRUY_LINH_DONG_PHU_CAP2_TONG_HOP_NEW);
                        }
                    }
                    else
                    {
                        // Set header
                        List<TlDmPhuCapNq104Model> lstPhuCapDuocHuong = new List<TlDmPhuCapNq104Model>()
                        {
                            new TlDmPhuCapNq104Model() { TenPhuCap = "Số tháng truy thu" },
                            new TlDmPhuCapNq104Model() { TenPhuCap = "Lương cơ bản" },
                            new TlDmPhuCapNq104Model() { TenPhuCap = "Phụ cấp chức vụ" },
                            new TlDmPhuCapNq104Model() { TenPhuCap = "Phụ cấp công vụ" },
                            new TlDmPhuCapNq104Model() { TenPhuCap = "Phụ cấp thâm niên" }
                        };

                        lstPhuCapDuocHuong.AddRange(lstPhuCapDynamic);
                        lstPhuCapDuocHuong.Add(new TlDmPhuCapNq104Model() { TenPhuCap = "Tổng cộng" });

                        List<TlDmPhuCapNq104Model> lstTruBHXH = new List<TlDmPhuCapNq104Model>() { new TlDmPhuCapNq104Model() { TenPhuCap = "Trừ BHXH" } };
                        List<TlDmPhuCapNq104Model> lstSoTienDuocNhan = new List<TlDmPhuCapNq104Model>() { new TlDmPhuCapNq104Model() { TenPhuCap = "Số tiền truy thu" } };
                        List<TlDmPhuCapNq104Model> lstKy = new List<TlDmPhuCapNq104Model>() { new TlDmPhuCapNq104Model() { TenPhuCap = "Ký" } };

                        cacKhoan = new Dictionary<string, List<TlDmPhuCapNq104Model>>()
                        {
                            { "Số tiền phụ cấp bị truy thu", lstPhuCapDuocHuong },
                            { "Trừ BHXH", lstTruBHXH },
                            { "Số tiền truy thu", lstSoTienDuocNhan },
                            { "Ký", lstKy}
                        };

                        templateFileName = Path.Combine(ExportPrefix.PATH_TL_LUONG_NEW, ExportFileName.RPT_TL_LUONG_TRUY_THU_DONG_PHU_CAP_NEW);
                        if (lstPhuCapDynamic.Count > 2)
                        {
                            templateFileName = Path.Combine(ExportPrefix.PATH_TL_LUONG_NEW, ExportFileName.RPT_TL_LUONG_TRUY_THU_DONG_PHU_CAP2_NEW);
                        }
                    }

                    // Xử lý header
                    List<HeaderReportLuongTruyLinhNq104DongPhuCap> headers = new List<HeaderReportLuongTruyLinhNq104DongPhuCap>();
                    var columnStart = 6;
                    foreach (var khoan in cacKhoan)
                    {
                        var mergeRange = "";
                        var columnStartName = GetExcelColumnName(columnStart);
                        var columnEndName = GetExcelColumnName(khoan.Value.Count + columnStart - 1);
                        mergeRange = columnStartName + "7" + ":" + columnEndName + "7";

                        // Tinh range merge BHXH, STN, Ky
                        var mergeRangeRow = columnStartName + "7" + ":" + columnStartName + "8";

                        HeaderReportLuongTruyLinhNq104DongPhuCap header = new HeaderReportLuongTruyLinhNq104DongPhuCap();
                        header.MergeRange = mergeRange;
                        header.MergeRangeRow = mergeRangeRow;
                        header.LstHeader1 = new List<TlDmPhuCapNq104Model>();
                        header.LstHeader2 = new List<TlDmPhuCapNq104Model>();
                        header.TenNhomPhuCap = khoan.Key;
                        TlDmPhuCapNq104Model subHeader;
                        foreach (var phuCap in khoan.Value)
                        {
                            subHeader = new TlDmPhuCapNq104Model();
                            subHeader.SttExport = !header.LstHeader1.Any() ? 1 : 2;
                            subHeader.TenPhuCap = khoan.Key;
                            if (khoan.Value.Count == 1)
                            {
                                // Nếu có 1 phần tử thì merge 2 row
                                subHeader.MaPhuCap = "1";
                            }
                            header.LstHeader1.Add(subHeader);
                            header.LstHeader2.Add(phuCap);
                        }

                        if (khoan.Value.Any())
                        {
                            headers.Add(header);
                        }
                    }

                    var data = _tlBangLuongThangService.ReportBangLuongTruyLinhDongPhuCap(lstDonVi, lstPhuCap, nam, thang, CachTinhLuong.CACH5, isTruyLinh, donViTinh, IsOrderTheoChucVu);

                    var items = _tlBangLuongThangService.ReportBangLuongTruyLinhDongPhuCapSummary(data, lstPhuCap, isTruyLinh);

                    Dictionary<string, object> report = new Dictionary<string, object>();
                    FormatNumber formatNumber = new FormatNumber(donViTinh, exportType);
                    report.Add("Cap1", _danhMucService.FindDonViQuanLy(_sessionInfo.YearOfWork).ToUpper());
                    report.Add("Cap2", GetHeader2Report());
                    report.Add("FormatNumber", formatNumber);
                    report.Add("DonViTinh", "Đơn vị tính: " + (_selectedUnitType != null ? _selectedUnitType.DisplayItem : ""));
                    report.Add("TieuDe2", string.Format("Tháng {0} năm {1}", thang, nam));
                    report.Add("Unit", donViTinh);
                    report.Add("ReportName", ReportName);
                    report.Add("Headers", headers);
                    report.Add("Items", items["Items"]);
                    report.Add("ItemsTotal", items["ItemsTotal"]);
                    AddChuKy(report, _typeChuky);

                    var xlsFile = _exportService.Export<ReportBangLuongTruyLinhDongPhuCapNq104Query, HeaderReportLuongTruyLinhNq104DongPhuCap>(templateFileName, report);
                    string fileNamePrefix = string.Format("rpt_Bang_Luong_Thang_Truy_Linh_Thang_TongHop_{0}_Nam_{1}", thang, nam);
                    string fileNameWithoutExtension = StringUtils.CreateExportFileName(fileNamePrefix);
                    results.Add(new ExportResult(string.Format("TongHop_Thang_{0}_Nam_{1}", thang, nam), fileNameWithoutExtension, null, xlsFile));

                    e.Result = results;
                }, (s, e) =>
                {
                    if (e.Error == null)
                    {
                        var result = (List<ExportResult>)e.Result;
                        _exportService.Open(result, exportType);
                    }
                    else
                    {
                        _logger.Error(e.Error.Message);
                    }
                    IsLoading = false;
                });
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        private void ExportBangLuongGiaiThichPhuCapTruyLinhKhac(ExportType exportType)
        {
            try
            {
                BackgroundWorkerHelper.Run((s, e) =>
                {
                    IsLoading = true;

                    List<ExportResult> results = new List<ExportResult>();
                    string templateFileName;
                    string fileNamePrefix;
                    string fileNameWithoutExtension;

                    int donViTinh = int.Parse(_selectedUnitType.ValueItem);
                    FormatNumber formatNumber = new FormatNumber(donViTinh, exportType);
                    var thang = int.Parse(SelectedMonth.ValueItem);
                    var nam = int.Parse(SelectedYear.ValueItem);

                    var lstDonVi = _mapper.Map<List<TlDmDonViNq104>>(ItemsDonVi.Where(x => x.IsSelected));
                    var lstPhuCapDynamic = GetPhuCapTruyLinhKhacSum();
                    var lstPhuCap = _mapper.Map<List<TlDmPhuCapNq104>>(lstPhuCapDynamic);
                    var lstNgach = new Dictionary<string, string>()
                    {
                        { "1", "Sĩ quan" },
                        { "2", "QNCN" },
                        { "3", "Công nhân viên chức quốc phòng" },
                        { "4", "HSQ - CS" }
                    };
                    var data = _tlBangLuongThangService.ReportGiaiThichChiTietPhuCapTruyLinhKhac(lstDonVi, lstPhuCap, nam, thang, CachTinhLuong.CACH5, donViTinh, IsOrderTheoChucVu);
                    foreach (var donVi in lstDonVi)
                    {
                        int page = 1;
                        var maDonVi = donVi.MaDonVi;
                        var tenDonVi = donVi.TenDonVi;
                        var tenDonViCha = string.Empty;
                        if (!string.IsNullOrEmpty(donVi.ParentId))
                        {
                            var donViCha = _tlDmDonViService.FindAll().FirstOrDefault(x => x.MaDonVi.Equals(donVi.ParentId));
                            if (donViCha != null)
                            {
                                tenDonViCha = donViCha.TenDonVi.ToUpper();
                            }
                        }

                        List<TlDmPhuCapNq104Model> lstPhuCapHasData = new List<TlDmPhuCapNq104Model>();
                        foreach (var pc in lstPhuCapDynamic)
                        {
                            var value = data.Compute(string.Format("SUM({0})", pc.MaPhuCap), string.Format("MaDonVi='{0}'", donVi.MaDonVi));
                            if (!string.IsNullOrEmpty(Convert.ToString(value)) && decimal.Parse(Convert.ToString(value)) != 0)
                            {
                                lstPhuCapHasData.Add(pc);
                            }
                        }

                        int limit = 14;
                        var lstPhuCapOffset = SplitList(lstPhuCapHasData, limit).ToList();
                        foreach (var phuCapOffset in lstPhuCapOffset)
                        {
                            // Thêm cột trắng
                            int index = 1;
                            int countItemPage = phuCapOffset.Count;
                            int numColEmpty = 0;
                            if (countItemPage < limit)
                            {
                                numColEmpty = limit - countItemPage;
                                for (int k = 0; k < numColEmpty; k++)
                                {
                                    phuCapOffset.Add(new TlDmPhuCapNq104Model());
                                }
                            }

                            List<RptGiaiThichChiTietNq104PhuCapKhacModel> items = new List<RptGiaiThichChiTietNq104PhuCapKhacModel>();
                            foreach (var ngach in lstNgach)
                            {
                                string filter = string.Format("MaDonVi='{0}' AND XauNoiMa LIKE '{1}%'", donVi.MaDonVi, ngach.Key);
                                var rows = data.Select(filter);
                                if (rows.Any())
                                {
                                    var subData = rows.CopyToDataTable();

                                    // Parent item
                                    RptGiaiThichChiTietNq104PhuCapKhacModel itemParent = new RptGiaiThichChiTietNq104PhuCapKhacModel();
                                    itemParent.iStt = 0;
                                    itemParent.sTenCbo = ngach.Value;
                                    itemParent.sMaCb = string.Empty;
                                    itemParent.isParent = true;
                                    itemParent.ListGiaTri = new List<TlBangLuongThangNq104Model>();
                                    foreach (var phucap in phuCapOffset)
                                    {
                                        TlBangLuongThangNq104Model giaTri = new TlBangLuongThangNq104Model();
                                        if (!string.IsNullOrEmpty(phucap.MaPhuCap))
                                        {
                                            string value = subData.Compute(string.Format("SUM({0})", phucap.MaPhuCap), string.Empty).ToString();
                                            giaTri.MaPhuCap = phucap.MaPhuCap;
                                            giaTri.GiaTri = string.IsNullOrEmpty(value) ? 0 : decimal.Parse(value);
                                        }

                                        itemParent.ListGiaTri.Add(giaTri);
                                    }
                                    items.Add(itemParent);

                                    // Detail item
                                    RptGiaiThichChiTietNq104PhuCapKhacModel itemDetail;
                                    for (int j = 0; j < rows.Length; j++)
                                    {
                                        var rowDetail = rows[j];

                                        itemDetail = new RptGiaiThichChiTietNq104PhuCapKhacModel();
                                        itemDetail.iStt = index++;
                                        itemDetail.sTenCbo = rowDetail[ExportColumnHeader.TEN_CAN_BO].ToString();
                                        itemDetail.sMaCb = rowDetail[ExportColumnHeader.MA_CAP_BAC].ToString();
                                        itemDetail.ListGiaTri = new List<TlBangLuongThangNq104Model>();

                                        foreach (var phucap in phuCapOffset)
                                        {
                                            TlBangLuongThangNq104Model giaTri = new TlBangLuongThangNq104Model();
                                            if (!string.IsNullOrEmpty(phucap.MaPhuCap))
                                            {
                                                string val = rowDetail[phucap.MaPhuCap]?.ToString();
                                                giaTri.MaPhuCap = phucap.MaPhuCap;
                                                giaTri.GiaTri = string.IsNullOrEmpty(val) ? 0 : decimal.Parse(val);
                                            }

                                            itemDetail.ListGiaTri.Add(giaTri);
                                        }
                                        items.Add(itemDetail);
                                    }
                                }
                            }

                            List<RptGiaiThichChiTietNq104PhuCapKhacModel> itemsTotal = new List<RptGiaiThichChiTietNq104PhuCapKhacModel>();
                            var listTotal = new RptGiaiThichChiTietNq104PhuCapKhacModel();
                            listTotal.ListGiaTriTotal = new List<TlBangLuongThangNq104Model>();
                            foreach (var phucap in phuCapOffset)
                            {
                                TlBangLuongThangNq104Model giaTri = new TlBangLuongThangNq104Model();
                                if (!string.IsNullOrEmpty(phucap.MaPhuCap))
                                {
                                    var value = data.Compute(string.Format("SUM({0})", phucap.MaPhuCap), string.Format("MaDonVi='{0}'", donVi.MaDonVi));
                                    giaTri.MaPhuCap = phucap.MaPhuCap;
                                    giaTri.GiaTri = decimal.Parse(value.ToString());
                                    if (phucap.MaPhuCap == PhuCap.LHT_HS)
                                        giaTri.GiaTri = 0;
                                }

                                listTotal.ListGiaTriTotal.Add(giaTri);
                            }
                            itemsTotal.Add(listTotal);

                            if (items.Count == 0)
                            {
                                continue;
                            }

                            Dictionary<string, object> report = new Dictionary<string, object>();
                            report.Add("Cap1", _iDanhMucService.FindDonViQuanLy(_sessionInfo.YearOfWork).ToUpper());
                            report.Add("Cap2", GetHeader2Report());
                            report.Add("FormatNumber", formatNumber);
                            report.Add("DonViTinh", "Đơn vị tính: " + (_selectedUnitType != null ? _selectedUnitType.DisplayItem : ""));
                            report.Add("TieuDe1", Model.TenBaoCao);
                            report.Add("TieuDe2", string.Format("Tháng {0} năm {1}", thang, nam));
                            report.Add("TenDonVi", tenDonVi);
                            report.Add("DonViCha", _sessionService?.Current?.TenDonViReportHeader);
                            report.Add("Items", items);
                            report.Add("Headers", phuCapOffset);
                            report.Add("ItemsTotal", itemsTotal);
                            report.Add("ReportName", ReportName);
                            AddChuKy(report, TypeChuky);

                            var nameColunmMerge = GetExcelColumnName(phuCapOffset.Count + 3);
                            report.Add("rangeTieuDe", "A4:" + nameColunmMerge + "4");
                            report.Add("rangeTieuDe1", "A5:" + nameColunmMerge + "5");
                            report.Add("rangeTieuDe2", "A6:" + nameColunmMerge + "6");
                            report.Add("rangeDonVi", "A8:" + nameColunmMerge + "8");
                            report.Add("rangeNgay", "D16:" + nameColunmMerge + "16");
                            report.Add("rangeThuaLenh1", "D17:" + nameColunmMerge + "17");
                            report.Add("rangeChucDanh1", "D18:" + nameColunmMerge + "18");
                            report.Add("rangeGhiChuKy1", "D19:" + nameColunmMerge + "19");
                            report.Add("rangeTen1", "D25:" + nameColunmMerge + "25");
                            report.Add("rangeDonViTinh", "A10:" + nameColunmMerge + "10");

                            if (page == 1)
                            {
                                //if (SelectedKhoIn.ValueItem == "A4")
                                //{
                                templateFileName = Path.Combine(ExportPrefix.PATH_TL_LUONG_NEW, ExportFileName.RPT_TL_GIAITHICH_CHITIET_PHUCAP_TRUYLINH_KHAC_TRANG1_NEW);
                                //}
                                //else
                                //{
                                //templateFileName = Path.Combine(ExportPrefix.PATH_TL_LUONG, ExportFileName.RPT_TL_GIAITHICH_CHITIET_PHUCAP_TRUYLINH_KHAC_TRANG1_A3);
                                //}
                                fileNamePrefix = string.Format("rptLuong_GiaiThich_ChiTiet_PhuCap_TruyLinh_Khac_{0}_Nam_{1}_DonVi_{2}_Trang1", thang, nam, maDonVi);
                                fileNameWithoutExtension = StringUtils.CreateExportFileName(fileNamePrefix);
                            }
                            else
                            {
                                //if (SelectedKhoIn.ValueItem == "A4")
                                //{
                                templateFileName = Path.Combine(ExportPrefix.PATH_TL_LUONG_NEW, ExportFileName.RPT_TL_GIAITHICH_CHITIET_PHUCAP_TRUYLINH_KHAC_TRANG2_NEW);
                                //}
                                //else
                                //{
                                //templateFileName = Path.Combine(ExportPrefix.PATH_TL_LUONG, ExportFileName.RPT_TL_GIAITHICH_CHITIET_PHUCAP_TRUYLINH_KHAC_TRANG2_A3);
                                //}
                                fileNamePrefix = string.Format("rptLuong_GiaiThich_ChiTiet_PhuCap_TruyLinh_Khac_{0}_Nam_{1}_DonVi_{2}_Trang_{3}", thang, nam, maDonVi, page);
                                fileNameWithoutExtension = StringUtils.CreateExportFileName(fileNamePrefix);
                            }

                            var xlsFile = _exportService.Export<RptGiaiThichChiTietNq104PhuCapKhacModel, TlDmPhuCapNq104Model>(templateFileName, report);
                            results.Add(new ExportResult(string.Format("{0} - {1}", donVi.MaDonVi, donVi.TenDonVi), fileNameWithoutExtension, null, xlsFile));
                            page++;
                        }
                    }

                    e.Result = results;
                }, (s, e) =>
                {
                    if (e.Error == null)
                    {
                        var result = (List<ExportResult>)e.Result;
                        _exportService.Open(result, exportType);
                    }
                    else
                    {
                        _logger.Error(e.Error.Message);
                    }
                    IsLoading = false;
                });
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        private void ExportBangLuongGiaiThichPhuCapTruyLinhKhacSummary(ExportType exportType)
        {
            try
            {
                BackgroundWorkerHelper.Run((s, e) =>
                {
                    IsLoading = true;

                    List<ExportResult> results = new List<ExportResult>();
                    string templateFileName;
                    string fileNamePrefix;
                    string fileNameWithoutExtension;

                    int donViTinh = int.Parse(_selectedUnitType.ValueItem);
                    FormatNumber formatNumber = new FormatNumber(donViTinh, exportType);
                    var thang = int.Parse(SelectedMonth.ValueItem);
                    var nam = int.Parse(SelectedYear.ValueItem);

                    var lstDonVi = _mapper.Map<List<TlDmDonViNq104>>(ItemsDonVi.Where(x => x.IsSelected));
                    var lstPhuCapDynamic = GetPhuCapTruyLinhKhacSum();
                    var lstPhuCap = _mapper.Map<List<TlDmPhuCapNq104>>(lstPhuCapDynamic);
                    var lstNgach = new Dictionary<string, string>()
                    {
                        { "1", "Sĩ quan" },
                        { "2", "QNCN" },
                        { "4", "Công nhân viên chức quốc phòng" },
                        { "0", "HSQ - CS" }
                    };
                    var data = _tlBangLuongThangService.ReportGiaiThichChiTietPhuCapTruyLinhKhac(lstDonVi, lstPhuCap, nam, thang, CachTinhLuong.CACH5, donViTinh, IsOrderTheoChucVu);

                    var tenDonViCha = string.Empty;

                    List<TlDmPhuCapNq104Model> lstPhuCapHasData = new List<TlDmPhuCapNq104Model>();
                    foreach (var pc in lstPhuCapDynamic)
                    {
                        var value = data.Compute(string.Format("SUM({0})", pc.MaPhuCap), string.Empty);
                        if (!string.IsNullOrEmpty(Convert.ToString(value)) && decimal.Parse(Convert.ToString(value)) != 0)
                        {
                            lstPhuCapHasData.Add(pc);
                        }
                    }



                    int limit = 14;
                    var lstPhuCapOffset = SplitList(lstPhuCapHasData, limit).ToList();
                    foreach (var phuCapOffset in lstPhuCapOffset)
                    {
                        // Thêm cột trắng
                        int index = 1;
                        int countItemPage = phuCapOffset.Count;
                        int numColEmpty = 0;
                        if (countItemPage < limit)
                        {
                            numColEmpty = limit - countItemPage;
                            for (int k = 0; k < numColEmpty; k++)
                            {
                                phuCapOffset.Add(new TlDmPhuCapNq104Model());
                            }
                        }

                        List<RptGiaiThichChiTietNq104PhuCapKhacModel> items = new List<RptGiaiThichChiTietNq104PhuCapKhacModel>();
                        foreach (var ngach in lstNgach)
                        {
                            string filter = string.Format("MaCapBac LIKE '{0}%'", ngach.Key);
                            var rows = data.Select(filter);
                            if (rows.Any())
                            {
                                var subData = rows.CopyToDataTable();

                                // Parent item
                                RptGiaiThichChiTietNq104PhuCapKhacModel itemParent = new RptGiaiThichChiTietNq104PhuCapKhacModel();
                                itemParent.iStt = 0;
                                itemParent.sTenCbo = ngach.Value;
                                itemParent.sMaCb = string.Empty;
                                itemParent.isParent = true;
                                itemParent.ListGiaTri = new List<TlBangLuongThangNq104Model>();
                                foreach (var phucap in phuCapOffset)
                                {
                                    TlBangLuongThangNq104Model giaTri = new TlBangLuongThangNq104Model();
                                    if (!string.IsNullOrEmpty(phucap.MaPhuCap))
                                    {
                                        var value = subData.Compute(string.Format("SUM({0})", phucap.MaPhuCap), string.Empty);
                                        giaTri.MaPhuCap = phucap.MaPhuCap;
                                        giaTri.GiaTri = string.IsNullOrEmpty(value.ToString()) ? 0 : decimal.Parse(value.ToString());
                                    }

                                    itemParent.ListGiaTri.Add(giaTri);
                                }
                                items.Add(itemParent);

                                // Detail item
                                RptGiaiThichChiTietNq104PhuCapKhacModel itemDetail;
                                for (int j = 0; j < rows.Length; j++)
                                {
                                    var rowDetail = rows[j];

                                    itemDetail = new RptGiaiThichChiTietNq104PhuCapKhacModel();
                                    itemDetail.iStt = index++;
                                    itemDetail.sTenCbo = rowDetail[ExportColumnHeader.TEN_CAN_BO].ToString();
                                    itemDetail.sMaCb = rowDetail[ExportColumnHeader.MA_CAP_BAC].ToString();
                                    itemDetail.ListGiaTri = new List<TlBangLuongThangNq104Model>();

                                    foreach (var phucap in phuCapOffset)
                                    {
                                        TlBangLuongThangNq104Model giaTri = new TlBangLuongThangNq104Model();
                                        if (!string.IsNullOrEmpty(phucap.MaPhuCap))
                                        {
                                            var val = rowDetail[phucap.MaPhuCap];
                                            giaTri.MaPhuCap = phucap.MaPhuCap;
                                            giaTri.GiaTri = string.IsNullOrEmpty(val.ToString()) ? 0 : decimal.Parse(val.ToString());
                                        }

                                        itemDetail.ListGiaTri.Add(giaTri);
                                    }
                                    items.Add(itemDetail);
                                }
                            }
                        }

                        List<RptGiaiThichChiTietNq104PhuCapKhacModel> itemsTotal = new List<RptGiaiThichChiTietNq104PhuCapKhacModel>();
                        var listTotal = new RptGiaiThichChiTietNq104PhuCapKhacModel();
                        listTotal.ListGiaTriTotal = new List<TlBangLuongThangNq104Model>();
                        foreach (var phucap in phuCapOffset)
                        {
                            TlBangLuongThangNq104Model giaTri = new TlBangLuongThangNq104Model();
                            if (!string.IsNullOrEmpty(phucap.MaPhuCap))
                            {
                                var value = data.Compute(string.Format("SUM({0})", phucap.MaPhuCap), string.Empty);
                                giaTri.MaPhuCap = phucap.MaPhuCap;
                                giaTri.GiaTri = string.IsNullOrEmpty(value.ToString()) ? 0 : decimal.Parse(value.ToString());
                                if (phucap.MaPhuCap == PhuCap.LHT_HS)
                                    giaTri.GiaTri = 0;
                            }

                            listTotal.ListGiaTriTotal.Add(giaTri);
                        }
                        itemsTotal.Add(listTotal);

                        if (items.Count == 0)
                        {
                            continue;
                        }

                        Dictionary<string, object> report = new Dictionary<string, object>();
                        report.Add("Cap1", _iDanhMucService.FindDonViQuanLy(_sessionInfo.YearOfWork).ToUpper());
                        report.Add("Cap2", GetHeader2Report());
                        report.Add("FormatNumber", formatNumber);
                        report.Add("DonViTinh", "Đơn vị tính: " + (_selectedUnitType != null ? _selectedUnitType.DisplayItem : ""));
                        report.Add("TieuDe1", Model.TenBaoCao);
                        report.Add("TieuDe2", string.Format("Tháng {0} năm {1}", thang, nam));
                        report.Add("DonViCha", _sessionService?.Current?.TenDonViReportHeader);
                        report.Add("Items", items);
                        report.Add("Headers", phuCapOffset);
                        report.Add("ItemsTotal", itemsTotal);
                        report.Add("ReportName", ReportName);
                        AddChuKy(report, TypeChuky);

                        var nameColunmMerge = GetExcelColumnName(phuCapOffset.Count + 3);
                        report.Add("rangeTieuDe", "A4:" + nameColunmMerge + "4");
                        report.Add("rangeTieuDe1", "A5:" + nameColunmMerge + "5");
                        report.Add("rangeTieuDe2", "A6:" + nameColunmMerge + "6");
                        report.Add("rangeDonVi", "A8:" + nameColunmMerge + "8");
                        report.Add("rangeNgay", "D16:" + nameColunmMerge + "16");
                        report.Add("rangeThuaLenh1", "D17:" + nameColunmMerge + "17");
                        report.Add("rangeChucDanh1", "D18:" + nameColunmMerge + "18");
                        report.Add("rangeGhiChuKy1", "D19:" + nameColunmMerge + "19");
                        report.Add("rangeTen1", "D25:" + nameColunmMerge + "25");
                        report.Add("rangeDonViTinh", "A10:" + nameColunmMerge + "10");

                        //if (SelectedKhoIn.ValueItem == "A4")
                        //{
                        templateFileName = Path.Combine(ExportPrefix.PATH_TL_LUONG_NEW, ExportFileName.RPT_TL_GIAITHICH_CHITIET_PHUCAP_TRUYLINH_KHAC_SUMMARY_NEW);
                        //}
                        //else
                        //{
                        //templateFileName = Path.Combine(ExportPrefix.PATH_TL_LUONG, ExportFileName.RPT_TL_GIAITHICH_CHITIET_PHUCAP_TRUYLINH_KHAC_SUMMARY_A3);
                        //}
                        fileNamePrefix = string.Format("rptLuong_GiaiThich_ChiTiet_PhuCap_TruyLinh_Khac_{0}_Nam_{1}_Summary", thang, nam);
                        fileNameWithoutExtension = StringUtils.CreateExportFileName(fileNamePrefix);


                        var xlsFile = _exportService.Export<RptGiaiThichChiTietNq104PhuCapKhacModel, TlDmPhuCapNq104Model>(templateFileName, report);
                        results.Add(new ExportResult(string.Format("GiaiThich_PhuCap_TruyLinh_Khac_TongHop_thang {0} - {1}", thang, nam), fileNameWithoutExtension, null, xlsFile));

                    }

                    e.Result = results;
                }, (s, e) =>
                {
                    if (e.Error == null)
                    {
                        var result = (List<ExportResult>)e.Result;
                        _exportService.Open(result, exportType);
                    }
                    else
                    {
                        _logger.Error(e.Error.Message);
                    }
                    IsLoading = false;
                });
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        private void ExportBangLuongTruyLinh(ExportType exportType, bool isTruyLinh)
        {
            try
            {
                BackgroundWorkerHelper.Run((s, e) =>
                {
                    IsLoading = true;

                    List<ExportResult> results = new List<ExportResult>();
                    string templateFileName;
                    if (isTruyLinh)
                    {
                        templateFileName = Path.Combine(ExportPrefix.PATH_TL_LUONG_NEW, ExportFileName.RPT_TL_LUONG_TRUY_LINH_NEW);
                    }
                    else
                    {
                        templateFileName = Path.Combine(ExportPrefix.PATH_TL_LUONG_NEW, ExportFileName.RPT_TL_LUONG_TRUY_THU_NEW);
                    }

                    int donViTinh = int.Parse(_selectedUnitType.ValueItem);
                    FormatNumber formatNumber = new FormatNumber(donViTinh, exportType);

                    var thang = int.Parse(SelectedMonth.ValueItem);
                    var nam = int.Parse(SelectedYear.ValueItem);
                    var lstDonVi = ItemsDonVi.Where(x => x.IsSelected);
                    var listDonViSelect = _mapper.Map<List<TlDmDonViNq104>>(lstDonVi);
                    _tlBangLuongBridgeService.DataPreprocess(thang, nam, string.Join(",", lstDonVi.Select(x => x.MaDonVi)), "CACH5");

                    var data = _tlBangLuongThangService.GetDataBangLuongTruyLinh(listDonViSelect, thang, nam, isTruyLinh, IsOrderTheoChucVu);
                    string donViCha = _sessionService?.Current?.TenDonViReportHeader;
                    foreach (var item in listDonViSelect)
                    {
                        var maDonVi = item.MaDonVi;
                        var tenDonVi = item.TenDonVi;
                        var items = _tlBangLuongThangService.ReportBangLuongTruyLinh(item, data, donViTinh);

                        if (items.Rows.Count == 0)
                        {
                            continue;
                        }

                        Dictionary<string, object> map = new Dictionary<string, object>();
                        map.Add("Cap1", _danhMucService.FindDonViQuanLy(_sessionInfo.YearOfWork).ToUpper());
                        map.Add("Cap2", GetHeader2Report());
                        map.Add("SDateTimeNow", DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                        map.Add("FormatNumber", formatNumber);
                        map.Add("Unit", donViTinh);
                        map.Add("DonViTinh", "Đơn vị tính: " + (_selectedUnitType != null ? _selectedUnitType.DisplayItem : ""));
                        map.Add("TieuDe2", string.Format("Tháng{0} năm {1}", thang, nam));
                        map.Add("TenDonVi", item.TenDonVi.ToUpper());
                        map.Add("DonViCha", donViCha);
                        map.Add("Items", items);
                        map.Add("ReportName", ReportName);
                        AddChuKy(map, TypeChuky);

                        var xlsFile = _exportService.Export<TlDmCanBoKeHoachNq104Model>(templateFileName, map);
                        string fileNamePrefix = "";
                        if (isTruyLinh)
                        {
                            fileNamePrefix = string.Format("rpt_Bang_Luong_Thang_Truy_Linh_{0}_Nam_{1}_{2}", thang, nam, maDonVi);
                        }
                        else
                        {
                            fileNamePrefix = string.Format("rpt_Bang_Luong_Thang_Truy_Thu_{0}_Nam_{1}_{2}", thang, nam, maDonVi);
                        }
                        string fileNameWithoutExtension = StringUtils.CreateExportFileName(fileNamePrefix);
                        results.Add(new ExportResult(string.Format("{0} - {1}", maDonVi, tenDonVi), fileNameWithoutExtension, null, xlsFile));
                    }
                    e.Result = results;
                }, (s, e) =>
                {
                    IsLoading = false;

                    if (e.Error == null)
                    {
                        var result = (List<ExportResult>)e.Result;
                        _exportService.Open(result, exportType);
                    }
                    else
                    {
                        _logger.Error(e.Error.Message);
                    }
                });
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        private void ExportBangLuongTruyLinhSummary(ExportType exportType, bool isTruyLinh)
        {
            try
            {
                BackgroundWorkerHelper.Run((s, e) =>
                {
                    IsLoading = true;

                    List<ExportResult> results = new List<ExportResult>();
                    string templateFileName;
                    if (isTruyLinh)
                    {
                        templateFileName = Path.Combine(ExportPrefix.PATH_TL_LUONG_NEW, ExportFileName.RPT_TL_LUONG_TRUY_LINH_TONG_HOP_NEW);
                    }
                    else
                    {
                        templateFileName = Path.Combine(ExportPrefix.PATH_TL_LUONG_NEW, ExportFileName.RPT_TL_LUONG_TRUY_THU_NEW);
                    }

                    int donViTinh = int.Parse(_selectedUnitType.ValueItem);
                    FormatNumber formatNumber = new FormatNumber(donViTinh, exportType);

                    var thang = int.Parse(SelectedMonth.ValueItem);
                    var nam = int.Parse(SelectedYear.ValueItem);
                    var lstDonVi = ItemsDonVi.Where(x => x.IsSelected);
                    var listDonViSelect = _mapper.Map<List<TlDmDonViNq104>>(lstDonVi);
                    _tlBangLuongBridgeService.DataPreprocess(thang, nam, string.Join(",", lstDonVi.Select(x => x.MaDonVi)), "CACH5");
                    var data = _tlBangLuongThangService.GetDataBangLuongTruyLinh(listDonViSelect, thang, nam, isTruyLinh, IsOrderTheoChucVu);

                    var items = _tlBangLuongThangService.ReportBangLuongTruyLinhSummary(data, donViTinh);

                    Dictionary<string, object> map = new Dictionary<string, object>();
                    map.Add("Cap1", _danhMucService.FindDonViQuanLy(_sessionInfo.YearOfWork).ToUpper());
                    map.Add("Cap2", GetHeader2Report());
                    map.Add("SDateTimeNow", DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                    map.Add("FormatNumber", formatNumber);
                    map.Add("Unit", donViTinh);
                    map.Add("DonViTinh", "Đơn vị tính: " + (_selectedUnitType != null ? _selectedUnitType.DisplayItem : ""));
                    map.Add("TieuDe2", string.Format("Tháng: {0} - Năm: {1}", thang, nam));
                    map.Add("Items", items);
                    map.Add("ReportName", ReportName);
                    AddChuKy(map, _typeChuky);

                    var xlsFile = _exportService.Export<TlDmCanBoKeHoachNq104Model>(templateFileName, map);
                    string fileNamePrefix = "";
                    if (isTruyLinh)
                    {
                        fileNamePrefix = string.Format("rpt_Bang_Luong_Thang_Truy_Linh_{0}_Nam_{1}", thang, nam);
                    }
                    else
                    {
                        fileNamePrefix = string.Format("rpt_Bang_Luong_Thang_Truy_Thu_{0}_Nam_{1}", thang, nam);
                    }
                    string fileNameWithoutExtension = StringUtils.CreateExportFileName(fileNamePrefix);
                    results.Add(new ExportResult(string.Format("TongHop"), fileNameWithoutExtension, null, xlsFile));

                    e.Result = results;
                }, (s, e) =>
                {
                    IsLoading = false;

                    if (e.Error == null)
                    {
                        var result = (List<ExportResult>)e.Result;
                        _exportService.Open(result, exportType);
                    }
                    else
                    {
                        _logger.Error(e.Error.Message);
                    }
                });
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        private void ExportBangLuongThangTheoYeuToLuong(ExportType exportType)
        {
            try
            {
                BackgroundWorkerHelper.Run((s, e) =>
                {
                    IsLoading = true;
                    List<ExportResult> results = new List<ExportResult>();
                    int donViTinh = int.Parse(_selectedUnitType.ValueItem);
                    FormatNumber formatNumber = new FormatNumber(donViTinh, exportType);
                    var thang = int.Parse(SelectedMonth.ValueItem);
                    var nam = int.Parse(SelectedYear.ValueItem);
                    var lstDonVi = ItemsDonVi.Where(x => x.IsSelected).ToList();
                    var maDonVi = string.Join(",", lstDonVi.Select(x => x.MaDonVi));
                    var maPhuCap = SelectedYeuToluong.MaPhuCap;
                    var tenPhuCap = SelectedYeuToluong.TenPhuCap;
                    var maNgach = SelectedGrade.ValueItem;
                    var tenNgach = SelectedGrade.DisplayItem;

                    _tlBangLuongBridgeService.DataPreprocess(thang, nam, string.Join(",", lstDonVi.Select(x => x.MaDonVi)), CachTinhLuong.CACH0);
                    var lstTlBangLuongThangDong = _tlBangLuongThangService.ReportBangLuongThangDong(maDonVi, maNgach, maPhuCap, thang, nam).ToList();
                    if (lstTlBangLuongThangDong.IsEmpty())
                    {
                        e.Result = results;
                        return;
                    }

                    var lstCbHsl = lstTlBangLuongThangDong.GroupBy(x => new { x.MaCb, x.HeSoLuong }).Select(x => x.First()).OrderBy(x => x.MaCb).ToList();
                    int numberPage = lstDonVi.Count / 5 + 1;
                    for (int i = 1; i <= numberPage; i++)
                    {
                        Dictionary<string, object> data = _tlBangLuongThangService.ReportBangLuongThangDongData(lstTlBangLuongThangDong, lstCbHsl, _mapper.Map<List<TlDmDonViNq104>>(lstDonVi), i, numberPage);
                        data.Add("Cap1", _danhMucService.FindDonViQuanLy(_sessionInfo.YearOfWork).ToUpper());
                        data.Add("Cap2", GetHeader2Report());
                        data.Add("FormatNumber", formatNumber);
                        data.Add("TieuDe1", string.Format("Tháng {0} năm {1}", thang, nam));
                        data.Add("TieuDe2", string.Format("Tổng hợp quyết toán {0} {1}", tenPhuCap, tenNgach));
                        AddChuKy(data, TypeChuky);

                        string templateFileName = Path.Combine(ExportPrefix.PATH_TL_LUONG_NEW, ExportFileName.RPT_TL_THANG_THEO_YEU_TO_LUONG_TO_1_NEW);
                        if (i == 1)
                        {
                            templateFileName = Path.Combine(ExportPrefix.PATH_TL_LUONG_NEW, ExportFileName.RPT_TL_THANG_THEO_YEU_TO_LUONG_TO_2_NEW);
                        }
                        var xlsFile = _exportService.Export<TlRptBangLuongThangDong>(templateFileName, data);
                        string fileNamePrefix = string.Format("rpt_Bang_Luong_Thang_{0}_Nam_{1}_{2}_Tờ_{3}", thang, nam, "Tổng hợp", i);
                        string fileNameWithoutExtension = StringUtils.CreateExportFileName(fileNamePrefix);
                        results.Add(new ExportResult(string.Format("{0} - {1}", "Tổng hợp", "Tổng hợp"), fileNameWithoutExtension, null, xlsFile));
                    }
                    e.Result = results;
                }, (s, e) =>
                {
                    IsLoading = false;

                    if (e.Error == null)
                    {
                        var result = (List<ExportResult>)e.Result;
                        if (!result.IsEmpty())
                        {
                            _exportService.Open(result, exportType);
                        }
                        else
                        {
                            MessageBoxHelper.Info("Không có dữ liệu.");
                        }
                    }
                    else
                    {
                        _logger.Error(e.Error.Message);
                        MessageBoxHelper.Info(e.Error.Message);
                    }
                });
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        private void LoadHuongNN()
        {
            HuongNNItems = new ObservableCollection<ComboboxItem>()
            {
                new ComboboxItem("40 %", "0.4"),
                new ComboboxItem("100 %", "1")
            };

            SelectedHuongNN = HuongNNItems.ElementAt(0);
        }

        private void ExportBangLuongThangTheoChieuDoc(ExportType exportType)
        {
            try
            {
                BackgroundWorkerHelper.Run((s, e) =>
                {
                    IsLoading = true;
                    List<ExportResult> results = new List<ExportResult>();
                    int donViTinh = int.Parse(_selectedUnitType.ValueItem);
                    FormatNumber formatNumber = new FormatNumber(donViTinh, exportType);
                    var thang = int.Parse(SelectedMonth.ValueItem);
                    var nam = int.Parse(SelectedYear.ValueItem);
                    var lstDonVi = ItemsDonVi.Where(x => x.IsSelected).ToList();
                    var listDonViSelect = _mapper.Map<List<TlDmDonViNq104>>(lstDonVi);
                    string donViCha = _sessionService?.Current?.TenDonViReportHeader;
                    var maDonVi = string.Join(",", lstDonVi.Select(x => x.MaDonVi));
                    var maPhuCap = SelectedYeuToluong.MaPhuCap;
                    var maNgach = SelectedGrade.ValueItem;
                    var tenNgach = SelectedGrade.DisplayItem;

                    var data = _tlBangLuongThangService.ReportBangLuongThangDoc(maDonVi, maNgach, maPhuCap, thang, nam);
                    foreach (var item in listDonViSelect)
                    {

                        var tenDonVi = item.TenDonVi;
                        var madv = item.MaDonVi;
                        var items = _tlBangLuongThangService.ReportBangLuongThangDoc(item, data);
                        if (items.Rows.Count == 0)
                        {
                            continue;
                        }

                        Dictionary<string, object> map = new Dictionary<string, object>();
                        map.Add("Cap1", _danhMucService.FindDonViQuanLy(_sessionInfo.YearOfWork).ToUpper());
                        map.Add("Cap2", GetHeader2Report());
                        map.Add("FormatNumber", formatNumber);
                        map.Add("Unit", donViTinh);
                        map.Add("DonViTinh", "Đơn vị tính: " + (_selectedUnitType != null ? _selectedUnitType.DisplayItem : ""));
                        map.Add("TieuDe1", string.Format(""));
                        map.Add("TieuDe2", string.Format(""));
                        map.Add("TenDonVi", item.TenDonVi.ToUpper());
                        map.Add("DonViCha", donViCha);
                        map.Add("Items", items);
                        map.Add("ReportName", ReportName);
                        AddChuKy(map, TypeChuky);

                        string templateFileName = Path.Combine(ExportPrefix.PATH_TL_LUONG_NEW, ExportFileName.RPT_TL_THANG_THEO_CHIEU_DOC_NEW);
                        var xlsFile = _exportService.Export<TlBangLuongThangDocQuery>(templateFileName, map);
                        string fileNamePrefix = string.Format("rpt_Bang_Luong_Thang_Chieu_Doc_{0}_Nam_{1}", thang, nam);
                        string fileNameWithoutExtension = StringUtils.CreateExportFileName(fileNamePrefix);
                        results.Add(new ExportResult(string.Format("{0} - {1}", madv, tenDonVi), fileNameWithoutExtension, null, xlsFile));
                    }

                    e.Result = results;
                }, (s, e) =>
                {
                    IsLoading = false;

                    if (e.Error == null)
                    {
                        var result = (List<ExportResult>)e.Result;
                        if (!result.IsEmpty())
                        {
                            _exportService.Open(result, exportType);
                        }
                        else
                        {
                            MessageBoxHelper.Info("Không có dữ liệu.");
                        }
                    }
                    else
                    {
                        _logger.Error(e.Error.Message);
                        MessageBoxHelper.Info(e.Error.Message);
                    }
                });
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        private void ExportBangLuongThangDong(ExportType exportType)
        {
            try
            {
                List<int> lstColumnHidden = new List<int>() { 12, 13, 14 };
                Dictionary<string, int> ColumnBotIndex = new Dictionary<string, int>();


                if (ItemsPhuCapBot != null && ItemsPhuCapBot.Where(n => n.IsSelected).Count() > 3)
                {
                    MessageBoxHelper.Error("Chỉ được chọn tối đa 3 phụ cấp bớt !");
                    return;
                }
                if (ItemsPhuCapBot.Where(n => n.IsSelected).Count() != ItemsPhuCap.Where(n => n.IsSelected).Count())
                {
                    MessageBoxHelper.Error("Số lượng phụ cấp thêm phải bằng số lượng phụ cấp bớt !");
                    return;
                }

                lstColumnHidden.RemoveRange(0, ItemsPhuCap.Where(n => n.IsSelected).Count());

                BackgroundWorkerHelper.Run((s, e) =>
                {
                    IsLoading = true;
                    List<string> lstColumnInclude = ItemsPhuCap.Where(n => n.IsSelected).Select(n => n.MaPhuCap).ToList();
                    int countColumn = ItemsPhuCap.Where(n => n.IsSelected).Count();
                    for (int i = 0; i < (3 - countColumn); ++i)
                    {
                        lstColumnInclude.Add("NULL");
                    }

                    List<ExportResult> results = new List<ExportResult>();
                    string templateFileName;

                    if (SelectedKhoIn.ValueItem == "A3")
                    {
                        ColumnBotIndex.Add("BHXHCN_TT", 17);
                        ColumnBotIndex.Add("BHYTCN_TT", 18);
                        ColumnBotIndex.Add("BHTNCN_TT", 19);
                        ColumnBotIndex.Add("THUETNCN_TT", 20);
                        ColumnBotIndex.Add("TA_TONG", 21);
                        ColumnBotIndex.Add("PHAITRUKHAC_SUM", 22);
                        templateFileName = Path.Combine(ExportPrefix.PATH_TL_LUONG_NEW, ExportFileName.RPT_TL_LUONG_THANG_A3_DONG_NEW);
                    }
                    else
                    {
                        ColumnBotIndex.Add("BHCN_TT", 17);
                        ColumnBotIndex.Add("THUETNCN_TT", 18);
                        ColumnBotIndex.Add("PHAITRUKHAC_SUM", 19);
                        templateFileName = Path.Combine(ExportPrefix.PATH_TL_LUONG_NEW, ExportFileName.RPT_TL_LUONG_THANG_DONG_NEW);
                    }
                    foreach (var column in ItemsPhuCapBot.Where(n => n.IsSelected).Select(n => n.MaPhuCap))
                    {
                        if (ColumnBotIndex.ContainsKey(column))
                            lstColumnHidden.Add(ColumnBotIndex[column]);
                    }

                    int donViTinh = int.Parse(_selectedUnitType.ValueItem);
                    FormatNumber formatNumber = new FormatNumber(donViTinh, exportType);

                    var thang = int.Parse(SelectedMonth.ValueItem);
                    var nam = int.Parse(SelectedYear.ValueItem);
                    var lstDonVi = ItemsDonVi.Where(x => x.IsSelected);
                    var lstDonViSelect = _mapper.Map<List<TlDmDonViNq104>>(lstDonVi);
                    _tlBangLuongBridgeService.DataPreprocess(thang, nam, string.Join(",", lstDonVi.Select(x => x.MaDonVi)), CachTinhLuong.CACH0);
                    var data = _tlBangLuongThangService.ReportBangLuongThangDong(lstDonViSelect, thang, nam, IsOrderTheoChucVu, lstColumnInclude);
                    var donViCha = _sessionService?.Current?.TenDonViReportHeader;

                    var dsNgach = new Dictionary<string, string>()
                    {
                        { "1", "Sĩ quan"},
                        { "2", "QNCN"},
                        { "3", "Công nhân viên chức quốc phòng"}
                    };

                    foreach (var item in lstDonViSelect)
                    {
                        var maDonVi = item.MaDonVi;
                        var tenDonVi = item.TenDonVi;
                        var items = _tlBangLuongThangService.ReportBangLuongThang(item, data, donViTinh, IsOrderTheoChucVu, dsNgach);
                        if (items.Rows.Count == 0)
                        {
                            continue;
                        }
                        List<string> lstHeader = new List<string>();
                        foreach (var child in lstColumnInclude)
                        {
                            var obj = _iTlDmPhuCapService.FindByMaPhuCap(child);
                            if (obj != null)
                            {
                                lstHeader.Add(obj.TenPhuCap);
                            }
                            else
                            {
                                lstHeader.Add(string.Empty);
                            }
                        }

                        Dictionary<string, object> map = new Dictionary<string, object>();
                        map.Add("Cap1", _danhMucService.FindDonViQuanLy(_sessionInfo.YearOfWork).ToUpper());
                        map.Add("Cap2", GetHeader2Report());
                        map.Add("FormatNumber", formatNumber);
                        map.Add("Unit", donViTinh);
                        map.Add("SDateTimeNow", DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                        map.Add("DonViTinh", "Đơn vị tính: " + (_selectedUnitType != null ? _selectedUnitType.DisplayItem : ""));
                        map.Add("TieuDe2", string.Format("Tháng: {0} - Năm: {1}", thang, nam));
                        map.Add("TenDonVi", IsChecked ? item.TenDonVi.ToUpper() : (item.MaDonVi != null ? item.MaDonVi : ""));
                        map.Add("DonViCha", donViCha);
                        map.Add("Items", items);
                        map.Add("Header1", lstHeader[0]);
                        map.Add("Header2", lstHeader[1]);
                        map.Add("Header3", lstHeader[2]);
                        map.Add("ReportName", ReportName);
                        AddChuKy(map, TypeChuky);

                        var xlsFile = _exportService.Export<TlDmCanBoKeHoachNq104Model>(templateFileName, map, lstColumnHidden);
                        string fileNamePrefix = string.Format("rpt_Bang_Luong_Thang_{0}_Nam_{1}_{2}", thang, nam, maDonVi);
                        string fileNameWithoutExtension = StringUtils.CreateExportFileName(fileNamePrefix);
                        results.Add(new ExportResult(string.Format("{0} - {1}", maDonVi, tenDonVi), fileNameWithoutExtension, null, xlsFile));
                    }
                    e.Result = results;
                }, (s, e) =>
                {
                    IsLoading = false;

                    if (e.Error == null)
                    {
                        var result = (List<ExportResult>)e.Result;
                        _exportService.Open(result, exportType);
                    }
                    else
                    {
                        _logger.Error(e.Error.Message);
                    }
                });
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        private void ExportThuNopBhxhSummary(ExportType exportType)
        {
            try
            {
                BackgroundWorkerHelper.Run((s, e) =>
                {
                    IsLoading = true;

                    List<ExportResult> results = new List<ExportResult>();
                    string templateFileName;

                    if (SelectedKhoIn.ValueItem == "A3")
                    {
                        templateFileName = Path.Combine(ExportPrefix.PATH_TL_LUONG_NEW, ExportFileName.RPT_TL_THU_NOP_BHXH_A3_NEW);
                    }
                    else
                    {
                        templateFileName = Path.Combine(ExportPrefix.PATH_TL_LUONG_NEW, ExportFileName.RPT_TL_THU_NOP_BHXH_NEW);
                    }

                    var thang = int.Parse(SelectedMonth.ValueItem);
                    var nam = int.Parse(SelectedYear.ValueItem);
                    int donViTinh = int.Parse(_selectedUnitType.ValueItem);
                    FormatNumber formatNumber = new FormatNumber(donViTinh, exportType);
                    var lstDonVi = ItemsDonVi.Where(x => x.IsSelected);
                    var lstDonViSelect = _mapper.Map<ObservableCollection<TlDmDonViNq104>>(lstDonVi).ToList();
                    var dsNgach = new Dictionary<string, string>()
                    {
                        //{ "0", "HSQCS" },
                        { "1", "Sĩ quan"},
                        { "2", "QNCN"},
                        { "3", "Công nhân viên chức quốc phòng"},
                        { "4", "Hạ sĩ quan - chiến sĩ"}
                    };
                    DataTable items = new DataTable();
                    if (SelectedLoaiTongHop.ValueItem.Equals("2"))
                    {
                        items = _tlThuNopBhxhChiTietService.ReportThuNopBhxhTongHopTheoDonVi(lstDonViSelect, thang, nam, donViTinh, IsOrderTheoChucVu, _sessionInfo.IdDonVi, dsNgach, IsCheckedMaHuongLuong);
                    }
                    else
                    {
                        var dataRepost = _tlThuNopBhxhChiTietService.GetDataReportThuNopBhxh(lstDonViSelect, thang, nam, IsOrderTheoChucVu, true, _sessionInfo.IdDonVi, IsCheckedMaHuongLuong, IsInCanBoMoi);
                        items = _tlThuNopBhxhChiTietService.ReportThuNopBhxhCalculate(dataRepost, donViTinh, dsNgach);
                    }
                    Dictionary<string, object> data = new Dictionary<string, object>();
                    data.Add("Cap1", _danhMucService.FindDonViQuanLy(_sessionInfo.YearOfWork).ToUpper());
                    data.Add("Cap2", GetHeader2Report());
                    data.Add("FormatNumber", formatNumber);
                    data.Add("Unit", donViTinh);
                    data.Add("SDATETIMENOW", DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                    data.Add("DonViTinh", "Đơn vị tính: " + (_selectedUnitType != null ? _selectedUnitType.DisplayItem : ""));
                    data.Add("TieuDe2", string.Format("Tháng: {0} - Năm: {1}", thang, nam));
                    data.Add("TieuDe", string.Format("Báo cáo tổng hợp").ToUpper());
                    data.Add("Items", items);
                    data.Add("ReportName", "BÁO CÁO TỔNG HỢP THU NỘP BẢO HIỂM XÃ HỘI");
                    data.Add("TenDonVi", string.Empty);
                    AddChuKy(data, TypeChuky);

                    var xlsFile = _exportService.Export<TlDmCanBoKeHoachNq104Model>(templateFileName, data);
                    string fileNamePrefix = string.Format("rpt_Thu_nop_bhxh_thang_{0}_Nam_{1}", thang, nam);
                    string fileNameWithoutExtension = StringUtils.CreateExportFileName(fileNamePrefix);
                    results.Add(new ExportResult(string.Format("ThuNopBHXH_thang{0}-{1}", thang, nam), fileNameWithoutExtension, null, xlsFile));

                    e.Result = results;
                }, (s, e) =>
                {
                    if (e.Error == null)
                    {
                        var result = (List<ExportResult>)e.Result;
                        _exportService.Open(result, exportType);
                    }
                    else
                    {
                        _logger.Error(e.Error.Message);
                    }
                    IsLoading = false;
                });
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }


        private void ExportThuNopBhxh(ExportType exportType, bool isBienPhong = false)
        {
            try
            {
                BackgroundWorkerHelper.Run((s, e) =>
                {
                    IsLoading = true;

                    List<ExportResult> results = new List<ExportResult>();
                    string templateFileName;
                    if (isBienPhong)
                    {
                        templateFileName = Path.Combine(ExportPrefix.PATH_TL_LUONG_NEW, ExportFileName.RPT_TL_LUONG_THANG_BIEN_PHONG_1_NEW);
                    }
                    else
                    {
                        if (SelectedKhoIn.ValueItem == "A3")
                        {
                            templateFileName = Path.Combine(ExportPrefix.PATH_TL_LUONG_NEW, ExportFileName.RPT_TL_THU_NOP_BHXH_A3_NEW);
                        }
                        else
                        {
                            templateFileName = Path.Combine(ExportPrefix.PATH_TL_LUONG_NEW, ExportFileName.RPT_TL_THU_NOP_BHXH_NEW);
                        }
                    }

                    int donViTinh = int.Parse(_selectedUnitType.ValueItem);
                    FormatNumber formatNumber = new FormatNumber(donViTinh, exportType);

                    var thang = int.Parse(SelectedMonth.ValueItem);
                    var nam = int.Parse(SelectedYear.ValueItem);
                    var lstDonVi = ItemsDonVi.Where(x => x.IsSelected);
                    var lstDonViSelect = _mapper.Map<List<TlDmDonViNq104>>(lstDonVi);
                    var data = _tlThuNopBhxhChiTietService.GetDataReportThuNopBhxh(lstDonViSelect, thang, nam, IsOrderTheoChucVu, false, string.Empty, IsCheckedMaHuongLuong, IsInCanBoMoi);
                    var donViCha = _sessionService?.Current?.TenDonViReportHeader;

                    var dsNgach = new Dictionary<string, string>()
                    {
                        { "1", "Sĩ quan"},
                        { "2", "QNCN"},
                        { "3", "Công nhân viên chức quốc phòng"},
                        { "4", "Hạ sĩ quan - chiến sĩ"}
                    };

                    foreach (var item in lstDonViSelect)
                    {
                        var maDonVi = item.MaDonVi;
                        var tenDonVi = item.TenDonVi;
                        var items = _tlThuNopBhxhChiTietService.ReportThuNopBhxh(item, data, donViTinh, IsOrderTheoChucVu, dsNgach);
                        if (items.Rows.Count == 0)
                        {
                            continue;
                        }

                        Dictionary<string, object> map = new Dictionary<string, object>();
                        map.Add("Cap1", _danhMucService.FindDonViQuanLy(_sessionInfo.YearOfWork).ToUpper());
                        map.Add("Cap2", GetHeader2Report());
                        map.Add("FormatNumber", formatNumber);
                        map.Add("Unit", donViTinh);
                        map.Add("SDateTimeNow", DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                        map.Add("DonViTinh", "Đơn vị tính: " + (_selectedUnitType != null ? _selectedUnitType.DisplayItem : ""));
                        map.Add("TieuDe2", string.Format("Tháng: {0} - Năm: {1}", thang, nam));
                        map.Add("TenDonVi", IsChecked ? item.TenDonVi.ToUpper() : (item.MaDonVi != null ? item.MaDonVi : ""));
                        map.Add("DonViCha", donViCha);
                        map.Add("Items", items);
                        map.Add("ReportName", "BÁO CÁO THU NỘP BẢO HIỂM XÃ HỘI");
                        AddChuKy(map, TypeChuky);

                        var xlsFile = _exportService.Export<TlDmCanBoKeHoachNq104Model>(templateFileName, map);
                        string fileNamePrefix = string.Format("rpt_Thu_Nop_BHXH_Thang_{0}_Nam_{1}_{2}", thang, nam, maDonVi);
                        string fileNameWithoutExtension = StringUtils.CreateExportFileName(fileNamePrefix);
                        results.Add(new ExportResult(string.Format("{0} - {1}", maDonVi, tenDonVi), fileNameWithoutExtension, null, xlsFile));
                    }
                    e.Result = results;
                }, (s, e) =>
                {
                    IsLoading = false;

                    if (e.Error == null)
                    {
                        var result = (List<ExportResult>)e.Result;
                        _exportService.Open(result, exportType);
                    }
                    else
                    {
                        _logger.Error(e.Error.Message);
                    }
                });
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }


        private void ExportBangLuongThang(ExportType exportType, bool isBienPhong = false)
        {
            try
            {
                BackgroundWorkerHelper.Run((s, e) =>
                {
                    IsLoading = true;
                    List<ExportResult> results = new List<ExportResult>();
                    string templateFileName;
                    if (isBienPhong)
                    {
                        templateFileName = Path.Combine(ExportPrefix.PATH_TL_LUONG_NEW, ExportFileName.RPT_TL_LUONG_THANG_BIEN_PHONG_1_NEW);
                    }
                    else
                    {
                        if (SelectedKhoIn.ValueItem == "A3")
                        {
                            templateFileName = Path.Combine(ExportPrefix.PATH_TL_LUONG_NEW, ExportFileName.RPT_TL_LUONG_THANG_A3_NEW);
                        }
                        else
                        {
                            templateFileName = Path.Combine(ExportPrefix.PATH_TL_LUONG_NEW, ExportFileName.RPT_TL_LUONG_THANG_NEW);
                        }
                    }

                    int donViTinh = int.Parse(_selectedUnitType.ValueItem);
                    FormatNumber formatNumber = new FormatNumber(donViTinh, exportType);

                    var thang = int.Parse(SelectedMonth.ValueItem);
                    var nam = int.Parse(SelectedYear.ValueItem);
                    var lstDonVi = ItemsDonVi.Where(x => x.IsSelected);
                    _tlBangLuongBridgeService.DataPreprocess(thang, nam, string.Join(",", lstDonVi.Select(x => x.MaDonVi)), "CACH0");

                    var lstDonViSelect = _mapper.Map<List<TlDmDonViNq104>>(lstDonVi);
                    decimal tyLeHuong = !IsHuongNN ? 0m : Decimal.TryParse(SelectedHuongNN.ValueItem, out decimal value) ? value : 0m;

                    var data = IsReduceBHXH
                        ? _tlBangLuongThangService.GetDataBangLuongThangTruBHXH(lstDonViSelect, thang, nam, IsOrderTheoChucVu, IsGiaTriAm, IsCheckedMaHuongLuong, IsInCanBoMoi, tyLeHuong)
                        : _tlBangLuongThangService.GetDataBangLuongThang(lstDonViSelect, thang, nam, IsOrderTheoChucVu, IsGiaTriAm, IsCheckedMaHuongLuong, IsInCanBoMoi, tyLeHuong);

                    var donViCha = _sessionService?.Current?.TenDonViReportHeader;

                    //var lstlDmCapBacNq104 = _iTlDmCapBacNq104Service.FindAll().Where(x => x.Nam == nam && string.IsNullOrEmpty(x.Parent)).GroupBy(x => x.MaCb).ToList();
                    //var dsNgach = lstlDmCapBacNq104.ToDictionary(x => x.FirstOrDefault().MaCb, x => x.FirstOrDefault().TenCb);

                    var dsNgach = new Dictionary<string, string>()
                    {
                        { "1", "Sĩ quan"},
                        { "2", "QNCN"},
                        { "3.1", "Công nhân quốc phòng"},
                        { "3.2", "Công chức quốc phòng"},
                        { "3.3", "Viên chức quốc phòng"},
                        //{ "4", "Hạ sỹ quan, chiến sỹ" },
                        { "5", "Hàm cơ yếu"},
                        { "6", "Chuyên môn kỹ thuật cơ yếu"}
                    };

                    if (IsBoSungHSQCS)
                        dsNgach.Add("4", "HSQ-CS");

                    foreach (var item in lstDonViSelect)
                    {
                        var maDonVi = item.MaDonVi;
                        var tenDonVi = item.TenDonVi;
                        var items = _tlBangLuongThangService.ReportBangLuongThang(item, data, donViTinh, IsOrderTheoChucVu, dsNgach);
                        if (items.Rows.Count == 0)
                        {
                            continue;
                        }

                        Dictionary<string, object> map = new Dictionary<string, object>();
                        map.Add("Cap1", _danhMucService.FindDonViQuanLy(_sessionInfo.YearOfWork).ToUpper());
                        map.Add("Cap2", GetHeader2Report());
                        map.Add("FormatNumber", formatNumber);
                        map.Add("Unit", donViTinh);
                        map.Add("SDateTimeNow", DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                        map.Add("DonViTinh", "Đơn vị tính: " + (_selectedUnitType != null ? _selectedUnitType.DisplayItem : ""));
                        map.Add("TieuDe2", string.Format("Tháng {0} năm {1}", thang, nam));
                        map.Add("TenDonVi", IsChecked ? item.TenDonVi.ToUpper() : (item.MaDonVi != null ? item.MaDonVi : ""));
                        map.Add("DonViCha", donViCha);
                        map.Add("Items", items);
                        map.Add("ReportName", ReportName);
                        AddChuKy(map, TypeChuky);

                        var xlsFile = _exportService.Export<TlDmCanBoKeHoachNq104Model>(templateFileName, map);
                        string fileNamePrefix = string.Format("rpt_Bang_Luong_Thang_{0}_Nam_{1}_{2}", thang, nam, maDonVi);
                        string fileNameWithoutExtension = StringUtils.CreateExportFileName(fileNamePrefix);
                        results.Add(new ExportResult(string.Format("{0} - {1}", maDonVi, tenDonVi), fileNameWithoutExtension, null, xlsFile));
                    }
                    e.Result = results;
                }, (s, e) =>
                {
                    IsLoading = false;

                    if (e.Error == null)
                    {
                        var result = (List<ExportResult>)e.Result;
                        _exportService.Open(result, exportType);
                    }
                    else
                    {
                        _logger.Error(e.Error.Message);
                    }
                });
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        private void ExportBangLuongThangSummary(ExportType exportType, bool isBienPhong = false)
        {
            try
            {
                BackgroundWorkerHelper.Run((s, e) =>
                {
                    IsLoading = true;
                    decimal tyLeHuong = 0;
                    List<ExportResult> results = new List<ExportResult>();
                    string templateFileName;
                    if (isBienPhong)
                    {
                        templateFileName = Path.Combine(ExportPrefix.PATH_TL_LUONG_NEW, ExportFileName.RPT_TL_LUONG_THANG_BIEN_PHONG_1_NEW);
                    }
                    else
                    {
                        if (SelectedKhoIn.ValueItem == "A3")
                        {
                            templateFileName = Path.Combine(ExportPrefix.PATH_TL_LUONG_NEW, ExportFileName.RPT_TL_LUONG_THANG_A3_SUMMARY_NEW);
                        }
                        else
                        {
                            templateFileName = Path.Combine(ExportPrefix.PATH_TL_LUONG_NEW, ExportFileName.RPT_TL_LUONG_THANG_SUMMARY_NEW);
                        }
                    }

                    var thang = int.Parse(SelectedMonth.ValueItem);
                    var nam = int.Parse(SelectedYear.ValueItem);
                    int donViTinh = int.Parse(_selectedUnitType.ValueItem);
                    FormatNumber formatNumber = new FormatNumber(donViTinh, exportType);
                    var lstDonVi = ItemsDonVi.Where(x => x.IsSelected);
                    var lstDonViSelect = _mapper.Map<ObservableCollection<TlDmDonViNq104>>(lstDonVi).ToList();

                    var lstlDmCapBacNq104 = _iTlDmCapBacNq104Service.FindAll().Where(x => x.Nam == nam && string.IsNullOrEmpty(x.Parent)).GroupBy(x => x.MaCb).ToList();

                    //var dsNgach = lstlDmCapBacNq104.ToDictionary(x => x.FirstOrDefault().MaCb, x => x.FirstOrDefault().TenCb);
                    var dsNgach = new Dictionary<string, string>()
                    {
                        { "1", "Sĩ quan"},
                        { "2", "QNCN"},
                        { "3.1", "Công nhân quốc phòng"},
                        { "3.2", "Công chức quốc phòng"},
                        { "3.3", "Viên chức quốc phòng"},
                        { "3.4", "Lao động hợp đồng"},
                        //{ "4", "Hạ sỹ quan, chiến sỹ" },
                        { "5", "Hàm cơ yếu"},
                        { "6", "Chuyên môn kỹ thuật cơ yếu"}
                    };

                    if (IsBoSungHSQCS)
                        dsNgach.Add("4", "HSQ-CS");
                    DataTable items = new DataTable();
                    _tlBangLuongBridgeService.DataPreprocess(thang, nam, string.Join(",", lstDonVi.Select(x => x.MaDonVi)), "CACH0");

                    if (IsHuongNN)
                    {
                        tyLeHuong = decimal.Parse(SelectedHuongNN?.ValueItem);
                    }

                    if (SelectedLoaiTongHop.ValueItem.Equals("2"))
                    {
                        items = _tlBangLuongThangService.ReportBangLuongThangTheoDonVi(lstDonViSelect, thang, nam, donViTinh, IsOrderTheoChucVu, IsGiaTriAm, dsNgach, IsCheckedMaHuongLuong, IsReduceBHXH, tyLeHuong);
                    }
                    else
                    {

                        items = _tlBangLuongThangService.ReportBangLuongThang(lstDonViSelect, thang, nam, donViTinh, IsOrderTheoChucVu, IsGiaTriAm, dsNgach, IsCheckedMaHuongLuong, IsReduceBHXH, tyLeHuong);
                    }
                    Dictionary<string, object> data = new Dictionary<string, object>();
                    data.Add("Cap1", _danhMucService.FindDonViQuanLy(_sessionInfo.YearOfWork).ToUpper());
                    data.Add("Cap2", GetHeader2Report());
                    data.Add("FormatNumber", formatNumber);
                    data.Add("Unit", donViTinh);
                    data.Add("SDATETIMENOW", DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                    data.Add("DonViTinh", "Đơn vị tính: " + (_selectedUnitType != null ? _selectedUnitType.DisplayItem : ""));
                    data.Add("TieuDe2", string.Format("Tháng {0} Năm {1}", thang, nam));
                    data.Add("TieuDe", string.Format("Báo cáo tổng hợp").ToUpper());
                    data.Add("Items", items);
                    data.Add("ReportName", ReportName);
                    data.Add("DonViCha", _sessionService?.Current?.TenDonViReportHeader);
                    AddChuKy(data, TypeChuky);

                    var xlsFile = _exportService.Export<TlDmCanBoKeHoachNq104Model>(templateFileName, data);
                    string fileNamePrefix = string.Format("rpt_Luong_BangLuong_{0}_Nam_{1}", thang, nam);
                    string fileNameWithoutExtension = StringUtils.CreateExportFileName(fileNamePrefix);
                    results.Add(new ExportResult(string.Format("BangLuongTongHop_thang{0}-{1}", thang, nam), fileNameWithoutExtension, null, xlsFile));

                    e.Result = results;
                }, (s, e) =>
                {
                    if (e.Error == null)
                    {
                        var result = (List<ExportResult>)e.Result;
                        _exportService.Open(result, exportType);
                    }
                    else
                    {
                        _logger.Error(e.Error.Message);
                    }
                    IsLoading = false;
                });
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        private void ExportThueThuNhapCaNhanCaNamSummary(ExportType exportType)
        {
            try
            {
                BackgroundWorkerHelper.Run((s, e) =>
                {
                    IsLoading = true;

                    List<ExportResult> results = new List<ExportResult>();
                    string templateFileName;
                    if (SelectedKhoIn.ValueItem == "A4")
                    {
                        templateFileName = Path.Combine(ExportPrefix.PATH_TL_LUONG_NEW, ExportFileName.RPT_TL_QUYET_TOAN_TTNCN_NEW);
                    }
                    else
                    {
                        templateFileName = Path.Combine(ExportPrefix.PATH_TL_LUONG_NEW, ExportFileName.RPT_TL_QUYET_TOAN_TTNCN_A3_NEW);
                    }
                    int donViTinh = int.Parse(_selectedUnitType.ValueItem);
                    FormatNumber formatNumber = new FormatNumber(donViTinh, exportType);
                    var thang = int.Parse(SelectedMonth.ValueItem);
                    var nam = int.Parse(SelectedYear.ValueItem);
                    var lstDonVi = ItemsDonVi.Where(x => x.IsSelected);

                    var bangLuongPredicate = PredicateBuilder.True<TlBangLuongThangNq104>();
                    bangLuongPredicate = bangLuongPredicate.And(x => x.Nam == nam);
                    bangLuongPredicate = bangLuongPredicate.And(x => x.MaCachTl == CachTinhLuong.CACH0 || x.MaCachTl == CachTinhLuong.CACH5);
                    DataTable items = _tlBangLuongThangService.ReportThueThuNhapCaNhanNamSummary(_mapper.Map<List<TlDmDonViNq104>>(lstDonVi), donViTinh, IsExportAllCadres, nam, IsOrderTheoChucVu);

                    Dictionary<string, object> data = new Dictionary<string, object>();
                    data.Add("FormatNumber", formatNumber);
                    data.Add("Unit", donViTinh);
                    data.Add("DonViTinh", "Đơn vị tính: " + (_selectedUnitType != null ? _selectedUnitType.DisplayItem : ""));
                    data.Add("nam", "Năm: " + nam);
                    data.Add("ngaythangnam", string.Format("Ngày {0} Tháng {1} Năm {2}", DateTime.Now.Date.ToString("dd"), DateTime.Now.Month, DateTime.Now.Year));
                    data.Add("Items", items);
                    data.Add("donvi", string.Empty);
                    data.Add("DonViCha", _sessionService?.Current?.TenDonViReportHeader);
                    data.Add("ReportName", ReportName);
                    data.Add("Cap1", _danhMucService.FindDonViQuanLy(_sessionInfo.YearOfWork).ToUpper());
                    data.Add("Cap2", GetHeader2Report());
                    AddChuKy(data, TypeChuky);

                    var xlsFile = _exportService.Export<TlDmCanBoKeHoachNq104Model>(templateFileName, data);
                    string fileNamePrefix = string.Format("rpt_Quyet_Toan_Nam_ThueTncn_Nam_{0}", nam, "Tổng hợp");
                    string fileNameWithoutExtension = StringUtils.CreateExportFileName(fileNamePrefix);
                    results.Add(new ExportResult(string.Format("{0} - {1}", "Tổng hợp", "Tổng hợp"), fileNameWithoutExtension, null, xlsFile));
                    e.Result = results;
                }, (s, e) =>
                {
                    if (e.Error == null)
                    {
                        var result = (List<ExportResult>)e.Result;
                        _exportService.Open(result, exportType);
                    }
                    else
                    {
                        _logger.Error(e.Error.Message);
                    }
                    IsLoading = false;
                });
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        private void ExportThueThuNhapCaNhanCaNam(ExportType exportType)
        {
            try
            {
                BackgroundWorkerHelper.Run((s, e) =>
                {
                    IsLoading = true;

                    List<ExportResult> results = new List<ExportResult>();
                    string templateFileName;
                    if (SelectedKhoIn.ValueItem == "A4")
                    {
                        templateFileName = Path.Combine(ExportPrefix.PATH_TL_LUONG_NEW, ExportFileName.RPT_TL_QUYET_TOAN_TTNCN_NEW);
                    }
                    else
                    {
                        templateFileName = Path.Combine(ExportPrefix.PATH_TL_LUONG_NEW, ExportFileName.RPT_TL_QUYET_TOAN_TTNCN_A3_NEW);
                    }
                    int donViTinh = int.Parse(_selectedUnitType.ValueItem);
                    FormatNumber formatNumber = new FormatNumber(donViTinh, exportType);
                    var thang = int.Parse(SelectedMonth.ValueItem);
                    var nam = int.Parse(SelectedYear.ValueItem);
                    var lstDonVi = ItemsDonVi.Where(x => x.IsSelected);
                    var lstDonViStr = lstDonVi.Select(x => x.MaDonVi);

                    _tlBangLuongBridgeService.DataPreprocess(thang, nam, string.Join(",", lstDonVi.Select(x => x.MaDonVi)));
                    DataTable itemsAll = _tlBangLuongThangService.ReportThueThuNhapCaNhanNam(string.Join(",", lstDonViStr), donViTinh, IsExportAllCadres, nam, IsOrderTheoChucVu);

                    foreach (var item in lstDonVi)
                    {
                        int index = 1;
                        var subData = itemsAll.Select(string.Format("MaDonVi='{0}'", item.MaDonVi));
                        var items = new DataTable();
                        if (subData.Any())
                        {
                            foreach (DataRow dataRow in subData)
                            {
                                dataRow[ExportColumnHeader.STT] = index++;
                            }
                            items = subData.CopyToDataTable();
                        }
                        else
                        {
                            continue;
                        }

                        string tenDonVi = string.Empty;
                        if (IsChecked)
                        {
                            tenDonVi = item.TenDonVi;
                        }

                        Dictionary<string, object> data = new Dictionary<string, object>();
                        data.Add("Cap1", _danhMucService.FindDonViQuanLy(_sessionInfo.YearOfWork).ToUpper());
                        data.Add("Cap2", GetHeader2Report());
                        data.Add("FormatNumber", formatNumber);
                        data.Add("Unit", donViTinh);
                        data.Add("DonViTinh", "Đơn vị tính: " + (_selectedUnitType != null ? _selectedUnitType.DisplayItem : ""));
                        data.Add("nam", "Năm: " + nam);
                        data.Add("donvi", item.TenDonVi.ToUpper());
                        data.Add("DonViCha", _sessionService?.Current?.TenDonViReportHeader);
                        data.Add("ngaythangnam", string.Format("Ngày {0} Tháng {1} Năm {2}", DateTime.Now.Date.ToString("dd"), DateTime.Now.Month, DateTime.Now.Year));
                        data.Add("Items", items);
                        data.Add("ReportName", ReportName);
                        AddChuKy(data, TypeChuky);

                        var xlsFile = _exportService.Export<TlDmCanBoKeHoachNq104Model>(templateFileName, data);
                        string fileNamePrefix = string.Format("rpt_Quyet_Toan_Nam_ThueTncn_Nam_{0}_{1}", nam, tenDonVi);
                        string fileNameWithoutExtension = StringUtils.CreateExportFileName(fileNamePrefix);
                        results.Add(new ExportResult(string.Format("{0} - {1}", item.MaDonVi, item.TenDonVi), fileNameWithoutExtension, null, xlsFile));
                    }
                    e.Result = results;
                }, (s, e) =>
                {
                    if (e.Error == null)
                    {
                        var result = (List<ExportResult>)e.Result;
                        _exportService.Open(result, exportType);
                    }
                    else
                    {
                        _logger.Error(e.Error.Message);
                    }
                    IsLoading = false;
                });
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        private void ExportChiTietBangLuongThang(ExportType exportType, bool isSummary)
        {
            try
            {
                BackgroundWorkerHelper.Run((s, e) =>
                {
                    IsLoading = true;

                    List<ExportResult> results = new List<ExportResult>();
                    string templateFileName;
                    if (SelectedKhoIn.ValueItem.Equals("A3"))
                    {
                        templateFileName = Path.Combine(ExportPrefix.PATH_TL_LUONG_NEW, ExportFileName.RPT_TL_GIAI_THICH_CHI_TIET_LUONG_NEW);
                    }
                    else
                    {
                        templateFileName = Path.Combine(ExportPrefix.PATH_TL_LUONG_NEW, ExportFileName.RPT_TL_GIAI_THICH_CHI_TIET_LUONG_A4_NEW);
                    }

                    int donViTinh = int.Parse(_selectedUnitType.ValueItem);
                    FormatNumber formatNumber = new FormatNumber(donViTinh, exportType);
                    var nam = int.Parse(SelectedYear.ValueItem);
                    var thang = int.Parse(SelectedMonth.ValueItem);
                    var lstDonVi = _mapper.Map<List<TlDmDonViNq104>>(ItemsDonVi.Where(x => x.IsSelected));
                    _tlBangLuongBridgeService.DataPreprocess(thang, nam, string.Join(",", lstDonVi.Select(x => x.MaDonVi)), CachTinhLuong.CACH0);
                    var dataLuongThang = _tlBangLuongThangService.GetDataReportGiaiThichLuongChiTiet(lstDonVi, thang, nam, CachTinhLuong.CACH0, donViTinh, isSummary);
                    _tlBangLuongBridgeService.DataPreprocess(thang, nam, string.Join(",", lstDonVi.Select(x => x.MaDonVi)), CachTinhLuong.CACH5);
                    var dataTruyLinh = _tlBangLuongThangService.GetDataReportGiaiThichLuongChiTiet(lstDonVi, thang, nam, CachTinhLuong.CACH5, donViTinh, isSummary);

                    if (isSummary)
                    {
                        var itemsLuongThang = _tlBangLuongThangService.ReportGiaiThichLuongChiTiet(dataLuongThang, CachTinhLuong.CACH0);
                        var itemsTruyLinh = _tlBangLuongThangService.ReportGiaiThichLuongChiTiet(dataTruyLinh, CachTinhLuong.CACH5);
                        if ((itemsLuongThang != null && itemsLuongThang.Rows.Count != 0) || (itemsTruyLinh != null && itemsTruyLinh.Rows.Count != 0))
                        {
                            var items = itemsLuongThang.AsEnumerable().Union(itemsTruyLinh.AsEnumerable()).CopyToDataTable();

                            Dictionary<string, object> report = new Dictionary<string, object>();
                            report.Add("Cap1", _danhMucService.FindDonViQuanLy(_sessionInfo.YearOfWork).ToUpper());
                            report.Add("Cap2", GetHeader2Report());
                            report.Add("FormatNumber", formatNumber);
                            report.Add("DonViTinh", "Đơn vị tính: " + (_selectedUnitType != null ? _selectedUnitType.DisplayItem : ""));
                            report.Add("Items", items);
                            report.Add("TieuDe", "GIẢI THÍCH CHI TIẾT LƯƠNG TỔNG HỢP");
                            report.Add("TieuDe2", string.Format("Tháng: {0} - Năm: {1}", thang, nam));
                            report.Add("TenDonVi", string.Empty);
                            report.Add("DonViCha", _sessionService?.Current?.TenDonViReportHeader);
                            report.Add("Unit", donViTinh);
                            report.Add("ReportName", ReportName);
                            AddChuKy(report, TypeChuky);

                            var xlsFile = _exportService.Export<TlDmCanBoKeHoachNq104Model>(templateFileName, report);
                            string fileNamePrefix = string.Format("rpt_Giai_Thich_Chi_Tiet_Luong_Tong_Hop_{0}_Nam_{1}_{2}", thang, nam, DateTime.Now.ToString("ddMMyyyyhhss"));
                            string fileNameWithoutExtension = StringUtils.CreateExportFileName(fileNamePrefix);
                            results.Add(new ExportResult("rpt_Giai_Thich_Chi_Tiet_Luong_Tong_Hop", fileNameWithoutExtension, null, xlsFile));
                        }
                    }
                    else
                    {
                        foreach (var item in lstDonVi)
                        {
                            var maDonVi = item.MaDonVi;
                            var tenDonVi = item.TenDonVi;
                            var tenDonViCha = string.Empty;
                            if (!string.IsNullOrEmpty(item.ParentId))
                            {
                                var donViCha = _tlDmDonViService.FindAll().FirstOrDefault(x => x.MaDonVi.Equals(item.ParentId));
                                if (donViCha != null)
                                {
                                    tenDonViCha = donViCha.TenDonVi.ToUpper();
                                }
                            }

                            DataTable items = new DataTable();
                            var itemsLuongThang = _tlBangLuongThangService.ReportGiaiThichLuongChiTiet(item, dataLuongThang, CachTinhLuong.CACH0);
                            var itemsTruyLinh = _tlBangLuongThangService.ReportGiaiThichLuongChiTiet(item, dataTruyLinh, CachTinhLuong.CACH5);
                            if (itemsLuongThang != null && itemsLuongThang.Rows.Count != 0 && itemsTruyLinh != null && itemsTruyLinh.Rows.Count != 0)
                            {
                                items = itemsLuongThang.AsEnumerable().Union(itemsTruyLinh.AsEnumerable()).CopyToDataTable();
                            }
                            else if (itemsLuongThang != null && itemsLuongThang.Rows.Count != 0)
                            {
                                items = itemsLuongThang;
                            }
                            else
                            {
                                items = itemsTruyLinh;
                            }

                            if (items.Rows.Count == 0)
                            {
                                continue;
                            }

                            Dictionary<string, object> report = new Dictionary<string, object>();
                            report.Add("Cap1", _danhMucService.FindDonViQuanLy(_sessionInfo.YearOfWork).ToUpper());
                            report.Add("Cap2", GetHeader2Report());
                            report.Add("FormatNumber", formatNumber);
                            report.Add("DonViTinh", "Đơn vị tính: " + (_selectedUnitType != null ? _selectedUnitType.DisplayItem : ""));
                            report.Add("Items", items);
                            report.Add("TieuDe", "GIẢI THÍCH CHI TIẾT LƯƠNG");
                            report.Add("TieuDe2", string.Format("Tháng: {0} - Năm: {1}", thang, nam));
                            report.Add("TenDonVi", tenDonVi);
                            report.Add("DonViCha", _sessionService?.Current?.TenDonViReportHeader);
                            report.Add("Unit", donViTinh);
                            report.Add("ReportName", ReportName);
                            AddChuKy(report, TypeChuky);

                            var xlsFile = _exportService.Export<TlDmCanBoKeHoachNq104Model>(templateFileName, report);
                            string fileNamePrefix = string.Format("rpt_Giai_Thich_Chi_Tiet_Luong_{0}_Nam_{1}_{2}", thang, nam, maDonVi);
                            string fileNameWithoutExtension = StringUtils.CreateExportFileName(fileNamePrefix);
                            results.Add(new ExportResult(string.Format("{0} - {1}", maDonVi, tenDonVi), fileNameWithoutExtension, null, xlsFile));
                        }
                    }

                    e.Result = results;
                }, (s, e) =>
                {
                    if (e.Error == null)
                    {
                        var result = (List<ExportResult>)e.Result;
                        _exportService.Open(result, exportType);
                    }
                    else
                    {
                        _logger.Error(e.Error.Message);
                    }
                    IsLoading = false;
                });
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        private void ExportGiaiThichChiTietPCTNVKTHD(ExportType exportType)
        {
            try
            {
                BackgroundWorkerHelper.Run((s, e) =>
                {
                    IsLoading = true;

                    int donViTinh = int.Parse(_selectedUnitType.ValueItem);
                    FormatNumber formatNumber = new FormatNumber(donViTinh, exportType);
                    var nam = int.Parse(SelectedYear.ValueItem);
                    var thang = int.Parse(SelectedMonth.ValueItem);
                    var lstDonVi = _mapper.Map<List<TlDmDonViNq104>>(ItemsDonVi.Where(x => x.IsSelected));
                    _tlBangLuongBridgeService.DataPreprocess(thang, nam, string.Join(",", lstDonVi.Select(x => x.MaDonVi)), CachTinhLuong.CACH0);
                    DataTable items = _tlBangLuongThangService.ReportGiaiThichChiTietPhuCapTNVKTHD(lstDonVi, nam, thang, CachTinhLuong.CACH0, donViTinh);
                    if (items.Rows.Count > 0)
                    {
                        Dictionary<string, object> data = new Dictionary<string, object>();
                        data.Add("Cap1", _iDanhMucService.FindDonViQuanLy(_sessionInfo.YearOfWork).ToUpper());
                        data.Add("Cap2", GetHeader2Report());
                        data.Add("FormatNumber", formatNumber);
                        data.Add("DonViTinh", "Đơn vị tính: " + (_selectedUnitType != null ? _selectedUnitType.DisplayItem : ""));
                        data.Add("TieuDe1", Model.TenBaoCao);
                        data.Add("TieuDe2", string.Format("Tháng {0} năm {1}", thang, nam));
                        data.Add("TenDonVi", string.Join(" - ", lstDonVi.Select(x => x.TenDonVi)));
                        data.Add("DonViCha", _sessionService?.Current?.TenDonViReportHeader);
                        data.Add("Items", items);
                        data.Add("Unit", donViTinh);
                        data.Add("ReportName", ReportName);
                        AddChuKy(data, TypeChuky);

                        string templateFileName = Path.Combine(ExportPrefix.PATH_TL_LUONG_NEW, ExportFileName.RPT_TL_GIAITHICH_CHITIET_PHUCAPTNVKTHD_NEW);
                        string fileNamePrefix = string.Format("rptLuong_GiaiThich_ChiTiet_PhuCapTNVKTHD_{0}_Nam_{1}", thang, nam);
                        string fileNameWithoutExtension = StringUtils.CreateExportFileName(fileNamePrefix);
                        var xlsFile = _exportService.Export(templateFileName, data);
                        e.Result = new ExportResult(fileNameWithoutExtension, fileNameWithoutExtension, null, xlsFile);
                    }
                    else
                    {
                        return;
                    }
                }, (s, e) =>
                {
                    if (e.Error == null)
                    {
                        var result = (ExportResult)e.Result;
                        if (result != null)
                        {
                            _exportService.Open(result, exportType);
                        }
                    }
                    else
                    {
                        _logger.Error(e.Error.Message);
                    }
                    IsLoading = false;
                });
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        private void ExportTHLuongNgachDonVi(ExportType exportType)
        {
            try
            {
                BackgroundWorkerHelper.Run((s, e) =>
                {
                    IsLoading = true;

                    int donViTinh = int.Parse(_selectedUnitType.ValueItem);
                    FormatNumber formatNumber = new FormatNumber(donViTinh, exportType);
                    var thang = int.Parse(SelectedMonth.ValueItem);
                    var nam = int.Parse(SelectedYear.ValueItem);
                    var lstDonVi = ItemsDonVi.Where(x => x.IsSelected);

                    _tlBangLuongBridgeService.DataPreprocess(thang, nam, string.Join(",", lstDonVi.Select(x => x.MaDonVi)), CachTinhLuong.CACH0);
                    DataTable itemsThang = _tlBangLuongThangService.ExportTongHopLuongTheoNgachDonVi(thang, nam, _mapper.Map<List<TlDmDonViNq104>>(lstDonVi), CachTinhLuong.CACH0, donViTinh);

                    _tlBangLuongBridgeService.DataPreprocess(thang, nam, string.Join(",", lstDonVi.Select(x => x.MaDonVi)), CachTinhLuong.CACH5);
                    DataTable itemsTruyLinh = _tlBangLuongThangService.ExportTongHopLuongTheoNgachDonVi(thang, nam, _mapper.Map<List<TlDmDonViNq104>>(lstDonVi), CachTinhLuong.CACH5, donViTinh);

                    var items = itemsThang.AsEnumerable().Union(itemsTruyLinh.AsEnumerable()).CopyToDataTable();

                    Dictionary<string, object> data = new Dictionary<string, object>();
                    data.Add("Cap1", _iDanhMucService.FindDonViQuanLy(_sessionInfo.YearOfWork).ToUpper());
                    data.Add("Cap2", GetHeader2Report());
                    data.Add("FormatNumber", formatNumber);
                    data.Add("DonViTinh", "Đơn vị tính: " + (_selectedUnitType != null ? _selectedUnitType.DisplayItem : ""));
                    data.Add("Unit", donViTinh);
                    data.Add("TieuDe2", string.Format("Tháng {0} năm {1}", thang, nam));
                    data.Add("TenDonVi", string.Empty);
                    data.Add("Items", items);
                    data.Add("ReportName", ReportName);
                    data.Add("DonViCha", _sessionService?.Current?.TenDonViReportHeader);
                    AddChuKy(data, TypeChuky);

                    string templateFileName;
                    if (SelectedKhoIn.ValueItem == "A4")
                    {
                        templateFileName = Path.Combine(ExportPrefix.PATH_TL_LUONG_NEW, ExportFileName.RPT_TL_TONGHOP_LUONG_NGACHDONVI_NEW);
                    }
                    else
                    {
                        templateFileName = Path.Combine(ExportPrefix.PATH_TL_LUONG_NEW, ExportFileName.RPT_TL_TONGHOP_LUONG_NGACHDONVI_A3_NEW);
                    }
                    string fileNamePrefix = string.Format("rptLuong_TongHop_Luong_NgachDonVi_{0}_Nam_{1}", thang, nam);
                    string fileNameWithoutExtension = StringUtils.CreateExportFileName(fileNamePrefix);
                    var xlsFile = _exportService.Export(templateFileName, data);
                    e.Result = new ExportResult(fileNameWithoutExtension, fileNameWithoutExtension, null, xlsFile);
                }, (s, e) =>
                {
                    if (e.Error == null)
                    {
                        var result = (ExportResult)e.Result;
                        if (result != null)
                        {
                            _exportService.Open(result, exportType);
                        }
                    }
                    else
                    {
                        _logger.Error(e.Error.Message);
                    }
                    IsLoading = false;
                });
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        private void ExportLuongTheoNgach(ExportType exportType)
        {
            try
            {
                BackgroundWorkerHelper.Run((s, e) =>
                {
                    IsLoading = true;

                    List<ExportResult> results = new List<ExportResult>();
                    string templateFileName;
                    if (SelectedKhoIn.ValueItem == "A3")
                    {
                        templateFileName = Path.Combine(ExportPrefix.PATH_TL_LUONG_NEW, ExportFileName.RPT_TL_TONG_HOP_LUONG_PHU_CAP_THEO_NGACH_A3_NEW);
                    }
                    else
                    {
                        templateFileName = Path.Combine(ExportPrefix.PATH_TL_LUONG_NEW, ExportFileName.RPT_TL_TONG_HOP_LUONG_PHU_CAP_THEO_NGACH_NEW);
                    }

                    int donViTinh = int.Parse(_selectedUnitType.ValueItem);
                    FormatNumber formatNumber = new FormatNumber(donViTinh, exportType);
                    var thang = int.Parse(SelectedMonth.ValueItem);
                    var nam = int.Parse(SelectedYear.ValueItem);
                    var lstDonViModel = ItemsDonVi.Where(x => x.IsSelected);

                    var lstDonVi = _mapper.Map<ObservableCollection<TlDmDonViNq104>>(lstDonViModel).ToList();
                    string sDonvi = lstDonVi.IsEmpty() ? string.Empty : string.Join(",", lstDonVi.Select(x => x.MaDonVi));
                    _tlBangLuongBridgeService.DataPreprocess(thang, nam, sDonvi, CachTinhLuong.CACH0);
                    var dataAllThang = _tlBangLuongThangService.ExportTongHopLuongTheoNgach(thang, nam, lstDonVi, CachTinhLuong.CACH0, donViTinh, IsSummary);
                    _tlBangLuongBridgeService.DataPreprocess(thang, nam, sDonvi, CachTinhLuong.CACH5);
                    var dataAllTruyLinh = _tlBangLuongThangService.ExportTongHopLuongTheoNgach(thang, nam, lstDonVi, CachTinhLuong.CACH5, donViTinh, IsSummary);

                    foreach (var item in lstDonVi)
                    {
                        var items = new DataTable();
                        items = dataAllThang.Clone();
                        var dataRowsThang = dataAllThang.AsEnumerable().Where(x => item.MaDonVi.Equals(x.Field<string>("MaDonVi")));
                        var dataRowsTruylinh = dataAllTruyLinh.AsEnumerable().Where(x => item.MaDonVi.Equals(x.Field<string>("MaDonVi")));

                        if (!dataRowsThang.Any() && !dataRowsTruylinh.Any())
                        {
                            continue;
                        }

                        if (dataRowsThang.Any())
                        {
                            var rowFirst = items.NewRow();
                            rowFirst["TenNgach"] = "I. Tổng hợp tiền lương tháng";
                            rowFirst[ExportColumnHeader.IS_PARENT] = true;
                            rowFirst["IsHeader"] = true;
                            items.Rows.Add(rowFirst);
                            foreach (DataRow item1 in dataRowsThang)
                            {
                                items.Rows.Add(item1.ItemArray);
                            }
                        }

                        if (dataRowsTruylinh.Any())
                        {
                            var rowFirst = items.NewRow();
                            rowFirst["TenNgach"] = "II. Tổng hợp tiền lương truy lĩnh";
                            rowFirst[ExportColumnHeader.IS_PARENT] = true;
                            rowFirst["IsHeader"] = true;
                            items.Rows.Add(rowFirst);
                            foreach (DataRow item1 in dataRowsTruylinh)
                            {
                                items.Rows.Add(item1.ItemArray);
                            }
                        }

                        Dictionary<string, object> data = new Dictionary<string, object>();
                        data.Add("Cap1", _iDanhMucService.FindDonViQuanLy(_sessionInfo.YearOfWork).ToUpper());
                        data.Add("Cap2", GetHeader2Report());
                        data.Add("FormatNumber", formatNumber);
                        data.Add("DonViTinh", "Đơn vị tính: " + (_selectedUnitType != null ? _selectedUnitType.DisplayItem : ""));
                        data.Add("Items", items);
                        data.Add("TieuDe2", string.Format("Tháng: {0} - Năm: {1}", thang, nam));
                        data.Add("TenDonVi", item.TenDonVi.ToUpper());
                        data.Add("DonViCha", _sessionService?.Current?.TenDonViReportHeader);
                        data.Add("Unit", donViTinh);
                        data.Add("ReportName", ReportName);
                        AddChuKy(data, TypeChuky);

                        var xlsFile = _exportService.Export<TlDmCanBoKeHoachNq104Model>(templateFileName, data);
                        string fileNamePrefix = string.Format("rpt_Tong_Hop_Luong_Phu_Cap_Theo_Ngach_{0}_Nam_{1}_{2}", thang, nam, item.TenDonVi);
                        string fileNameWithoutExtension = StringUtils.CreateExportFileName(fileNamePrefix);
                        results.Add(new ExportResult(string.Format("{0} - {1}", item.MaDonVi, item.TenDonVi), fileNameWithoutExtension, null, xlsFile));
                    }
                    e.Result = results;
                }, (s, e) =>
                {
                    if (e.Error == null)
                    {
                        var result = (List<ExportResult>)e.Result;
                        _exportService.Open(result, exportType);
                    }
                    else
                    {
                        _logger.Error(e.Error.Message);
                    }
                    IsLoading = false;
                });
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        private void ExportLuongTheoNgachSummary(ExportType exportType)
        {
            try
            {
                BackgroundWorkerHelper.Run((s, e) =>
                {
                    IsLoading = true;

                    List<ExportResult> results = new List<ExportResult>();
                    string templateFileName;
                    if (SelectedKhoIn.ValueItem == "A3")
                    {
                        templateFileName = Path.Combine(ExportPrefix.PATH_TL_LUONG_NEW, ExportFileName.RPT_TL_TONG_HOP_LUONG_PHU_CAP_THEO_NGACH_A3_NEW);
                    }
                    else
                    {
                        templateFileName = Path.Combine(ExportPrefix.PATH_TL_LUONG_NEW, ExportFileName.RPT_TL_TONG_HOP_LUONG_PHU_CAP_THEO_NGACH_NEW);
                    }

                    int donViTinh = int.Parse(_selectedUnitType.ValueItem);
                    FormatNumber formatNumber = new FormatNumber(donViTinh, exportType);
                    var thang = int.Parse(SelectedMonth.ValueItem);
                    var nam = int.Parse(SelectedYear.ValueItem);
                    var lstDonViModel = ItemsDonVi.Where(x => x.IsSelected);
                    var lstDonVi = _mapper.Map<ObservableCollection<TlDmDonViNq104>>(lstDonViModel).ToList();
                    string sDonvi = lstDonVi.IsEmpty() ? string.Empty : string.Join(",", lstDonVi.Select(x => x.MaDonVi));
                    _tlBangLuongBridgeService.DataPreprocess(thang, nam, sDonvi, CachTinhLuong.CACH0);
                    var itemsLuongThang = _tlBangLuongThangService.ExportTongHopLuongTheoNgach(thang, nam, lstDonVi, CachTinhLuong.CACH0, donViTinh, IsSummary);
                    _tlBangLuongBridgeService.DataPreprocess(thang, nam, sDonvi, CachTinhLuong.CACH5);
                    var itemsTruyLinh = _tlBangLuongThangService.ExportTongHopLuongTheoNgach(thang, nam, lstDonVi, CachTinhLuong.CACH5, donViTinh, IsSummary);

                    var items = itemsLuongThang.AsEnumerable().Union(itemsTruyLinh.AsEnumerable()).CopyToDataTable();

                    Dictionary<string, object> data = new Dictionary<string, object>();
                    data.Add("Cap1", _danhMucService.FindDonViQuanLy(_sessionInfo.YearOfWork).ToUpper());
                    data.Add("Cap2", GetHeader2Report());
                    data.Add("FormatNumber", formatNumber);
                    data.Add("DonViTinh", "Đơn vị tính: " + (_selectedUnitType != null ? _selectedUnitType.DisplayItem : ""));
                    data.Add("Items", items);
                    data.Add("TieuDe2", string.Format("Tháng: {0} - Năm: {1}", thang, nam));
                    data.Add("TenDonVi", string.Empty);
                    data.Add("DonViCha", _sessionService?.Current?.TenDonViReportHeader);
                    data.Add("Unit", donViTinh);
                    data.Add("ReportName", ReportName);
                    AddChuKy(data, TypeChuky);

                    var xlsFile = _exportService.Export<TlDmCanBoKeHoachNq104Model>(templateFileName, data);
                    string fileNamePrefix = string.Format("rpt_Tong_Hop_Luong_Phu_Cap_Theo_Ngach_{0}_Nam_{1}", thang, nam);
                    string fileNameWithoutExtension = StringUtils.CreateExportFileName(fileNamePrefix);
                    results.Add(new ExportResult("rpt_TongHopLuong_PhuCap_TheoNgach", fileNameWithoutExtension, null, xlsFile));

                    e.Result = results;
                }, (s, e) =>
                {
                    if (e.Error == null)
                    {
                        var result = (List<ExportResult>)e.Result;
                        _exportService.Open(result, exportType);
                    }
                    else
                    {
                        _logger.Error(e.Error.Message);
                    }
                    IsLoading = false;
                });
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        private void ExportBangKeTrichThueTNCNSummary(ExportType exportType)
        {
            try
            {
                BackgroundWorkerHelper.Run((s, e) =>
                {
                    IsLoading = true;

                    int donViTinh = int.Parse(_selectedUnitType.ValueItem);
                    FormatNumber formatNumber = new FormatNumber(donViTinh, exportType);
                    int thang = int.Parse(SelectedMonth.ValueItem);
                    int nam = int.Parse(SelectedYear.ValueItem);
                    var lstDonVi = _mapper.Map<List<TlDmDonViNq104>>(ItemsDonVi.Where(x => x.IsSelected));
                    DataTable items = _tlBangLuongThangService.ReportBangKeTrichThueTNCNSummary(thang, nam, CachTinhLuong.CACH0, lstDonVi, donViTinh, IsExportAllCadres, IsOrderTheoChucVu);

                    Dictionary<string, object> data = new Dictionary<string, object>();
                    data.Add("Cap1", _danhMucService.FindDonViQuanLy(_sessionInfo.YearOfWork).ToUpper());
                    data.Add("Cap2", GetHeader2Report());
                    data.Add("FormatNumber", formatNumber);
                    data.Add("DonViTinh", "Đơn vị tính: " + (_selectedUnitType != null ? _selectedUnitType.DisplayItem : ""));
                    data.Add("Unit", donViTinh);
                    data.Add("TieuDe2", string.Format("Tháng {0} năm {1}", thang, nam));
                    data.Add("TenDonVi", string.Empty);
                    data.Add("DonViCha", _sessionService?.Current?.TenDonViReportHeader);
                    data.Add("h1", "Đơn vị tính: Đồng");
                    data.Add("Items", items);
                    data.Add("ReportName", ReportName);
                    AddChuKy(data, TypeChuky);

                    string templateFileName;
                    if (SelectedKhoIn.ValueItem == "A4")
                    {
                        templateFileName = Path.Combine(ExportPrefix.PATH_TL_LUONG_NEW, ExportFileName.RPT_TL_BANGKE_TRICHTHUETNCN_NEW);
                    }
                    else
                    {
                        templateFileName = Path.Combine(ExportPrefix.PATH_TL_LUONG_NEW, ExportFileName.RPT_TL_BANGKE_TRICHTHUETNCN_A3_NEW);
                    }
                    string fileNamePrefix = string.Format("rptLuong_BangKe_TrichThueTNCN_TongHop_Thang_{0}_Nam_{1}", thang, nam);
                    string fileNameWithoutExtension = StringUtils.CreateExportFileName(fileNamePrefix);
                    var xlsFile = _exportService.Export<TlDmCanBoKeHoachNq104Model>(templateFileName, data);
                    e.Result = new ExportResult("Tổng hợp", fileNameWithoutExtension, null, xlsFile);
                }, (s, e) =>
                {
                    IsLoading = false;
                    if (e.Error == null)
                    {
                        var result = (ExportResult)e.Result;
                        _exportService.Open(result, exportType);
                    }
                    else
                    {
                        _logger.Error(e.Error.Message);
                    }
                });
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        private void ExportBangKeTrichThueTNCN(ExportType exportType)
        {
            try
            {
                BackgroundWorkerHelper.Run((s, e) =>
                {
                    IsLoading = true;

                    List<ExportResult> results = new List<ExportResult>();
                    string templateFileName;
                    if (SelectedKhoIn.ValueItem == "A4")
                    {
                        templateFileName = Path.Combine(ExportPrefix.PATH_TL_LUONG_NEW, ExportFileName.RPT_TL_BANGKE_TRICHTHUETNCN_NEW);
                    }
                    else
                    {
                        templateFileName = Path.Combine(ExportPrefix.PATH_TL_LUONG_NEW, ExportFileName.RPT_TL_BANGKE_TRICHTHUETNCN_A3_NEW);
                    }

                    int donViTinh = int.Parse(_selectedUnitType.ValueItem);
                    FormatNumber formatNumber = new FormatNumber(donViTinh, exportType);
                    var thang = int.Parse(SelectedMonth.ValueItem);
                    var nam = int.Parse(SelectedYear.ValueItem);
                    var lstDonVi = ItemsDonVi.Where(x => x.IsSelected);
                    var lstDonViStr = lstDonVi.Select(x => x.MaDonVi);

                    _tlBangLuongBridgeService.DataPreprocess(thang, nam, string.Join(",", lstDonVi.Select(x => x.MaDonVi)), CachTinhLuong.CACH0);
                    DataTable itemsAll = _tlBangLuongThangService.ReportBangKeTrichThueTNCN(thang, nam, CachTinhLuong.CACH0, string.Join(",", lstDonViStr), donViTinh, IsExportAllCadres, IsOrderTheoChucVu);

                    foreach (var item in lstDonVi)
                    {
                        var maDonVi = item.MaDonVi;
                        var tenDonVi = item.TenDonVi;

                        var items = new DataTable();

                        var dataRows = itemsAll.AsEnumerable().Where(x => x.Field<string>("MaDonVi").Equals(maDonVi));

                        var index = 1;
                        if (dataRows.Any())
                        {
                            foreach (DataRow dataRow in dataRows)
                            {
                                dataRow[ExportColumnHeader.STT] = index++;
                            }
                            items = dataRows.CopyToDataTable();
                        }
                        else
                        {
                            continue;
                        }

                        Dictionary<string, object> data = new Dictionary<string, object>();
                        data.Add("Cap1", _danhMucService.FindDonViQuanLy(_sessionInfo.YearOfWork).ToUpper());
                        data.Add("Cap2", GetHeader2Report());
                        data.Add("FormatNumber", formatNumber);
                        data.Add("DonViTinh", "Đơn vị tính: " + (_selectedUnitType != null ? _selectedUnitType.DisplayItem : ""));
                        data.Add("Unit", donViTinh);
                        data.Add("TieuDe2", string.Format("Tháng {0} năm {1}", thang, nam));
                        data.Add("TenDonVi", item.TenDonVi.ToUpper());
                        data.Add("DonViCha", _sessionService?.Current?.TenDonViReportHeader);
                        data.Add("h1", "Đơn vị tính: Đồng");
                        data.Add("Items", items);
                        data.Add("ReportName", ReportName);
                        AddChuKy(data, TypeChuky);

                        var xlsFile = _exportService.Export<TlDmCanBoKeHoachNq104Model>(templateFileName, data);
                        string fileNamePrefix = string.Format("rptLuong_BangKe_TrichThueTNCN_Thang_{0}_Nam_{1}_{2}", thang, nam, maDonVi);
                        string fileNameWithoutExtension = StringUtils.CreateExportFileName(fileNamePrefix);
                        results.Add(new ExportResult(string.Format("{0} - {1}", item.MaDonVi, item.TenDonVi), fileNameWithoutExtension, null, xlsFile));
                    }
                    e.Result = results;
                }, (s, e) =>
                {
                    IsLoading = false;
                    if (e.Error == null)
                    {
                        var result = (List<ExportResult>)e.Result;
                        _exportService.Open(result, exportType);
                    }
                    else
                    {
                        _logger.Error(e.Error.Message);
                    }
                });
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        private void ExportDanhSachChiTraNganHang(ExportType exportType, bool isSummary)
        {
            try
            {
                BackgroundWorkerHelper.Run((s, e) =>
                {
                    IsLoading = true;

                    List<ExportResult> results = new List<ExportResult>();
                    string templateFileName;
                    bool inMotHang = IsShowEmployeeName;
                    if (IsShowEmployeeName)
                    {
                        templateFileName = Path.Combine(ExportPrefix.PATH_TL_LUONG_NEW, ExportFileName.RPT_TL_DANHSACH_CHITRA_LUONGCN_NEW);
                    }
                    else
                    {
                        templateFileName = Path.Combine(ExportPrefix.PATH_TL_LUONG_NEW, ExportFileName.RPT_TL_DS_CHITRA_LUONGCN_AN_DANH_NEW);
                        if (SelectedKieuIn.ValueItem.Equals("1"))
                        {
                            inMotHang = true;
                            templateFileName = Path.Combine(ExportPrefix.PATH_TL_LUONG_NEW, ExportFileName.RPT_TL_DS_CHITRA_LUONGCN_AN_DANH_1HANG_NEW);
                        }
                    }

                    var thang = int.Parse(SelectedMonth.ValueItem);
                    var nam = int.Parse(SelectedYear.ValueItem);
                    string tieuDe = string.Format("Tháng {0} năm {1}", thang, nam);
                    int donViTinh = int.Parse(_selectedUnitType.ValueItem);
                    string donViTinhStr = "Đơn vị tính: " + (_selectedUnitType != null ? _selectedUnitType.DisplayItem : "");
                    FormatNumber formatNumber = new FormatNumber(donViTinh, exportType);
                    var lstDonVi = _mapper.Map<List<TlDmDonViNq104>>(ItemsDonVi.Where(x => x.IsSelected));

                    _tlBangLuongBridgeService.DataPreprocess(thang, nam, string.Join(",", lstDonVi.Select(x => x.MaDonVi)), null);
                    var data = _tlBangLuongThangService.GetDataReportDanhSachChiTraNganHang(lstDonVi, nam, thang);
                    bool isChiTietTheoDoiTuong = SelectedLoaiTongHop.ValueItem.Equals("1");

                    if (isSummary)
                    {
                        DataTable items = new DataTable();
                        if (isChiTietTheoDoiTuong)
                        {
                            items = _tlBangLuongThangService.ReportDanhSachChiTraNganHangCaNhan(lstDonVi, data, donViTinh, isSummary, inMotHang);
                        }
                        else
                        {
                            items = _tlBangLuongThangService.ReportDanhSachChiTraNganHang(lstDonVi, data, donViTinh, isSummary, inMotHang);
                        }
                        Dictionary<string, object> report = new Dictionary<string, object>();
                        report.Add("FormatNumber", formatNumber);
                        report.Add("DonViTinh", donViTinhStr);
                        report.Add("Unit", donViTinh);
                        report.Add("TieuDe2", tieuDe);
                        report.Add("TenDonVi", string.Empty);
                        report.Add("DonViCha", _sessionService?.Current?.TenDonViReportHeader);
                        report.Add("h1", donViTinhStr);
                        report.Add("Items", items);
                        report.Add("ReportName", ReportName);
                        report.Add("Cap1", _danhMucService.FindDonViQuanLy(_sessionInfo.YearOfWork).ToUpper());
                        report.Add("Cap2", GetHeader2Report());
                        AddChuKy(report, TypeChuky);

                        var xlsFile = _exportService.Export<TlDmCanBoKeHoachNq104Model>(templateFileName, report);
                        string fileNamePrefix = string.Format("rptLuong_DanhSach_ChiTra_LuongCN_{0}_Nam_{1}_{2}", thang, nam, "Tổng hợp");
                        string fileNameWithoutExtension = StringUtils.CreateExportFileName(fileNamePrefix);
                        results.Add(new ExportResult(string.Format("{0} - {1}", "Tổng hợp", "Tổng hợp"), fileNameWithoutExtension, null, xlsFile));
                    }
                    else
                    {
                        foreach (var item in lstDonVi)
                        {
                            var maDonVi = item.MaDonVi;
                            var tenDonVi = item.TenDonVi;

                            DataTable items = _tlBangLuongThangService.ReportDanhSachChiTraNganHang(item, data, donViTinh, isSummary, inMotHang);
                            if (items.Rows.Count == 0)
                            {
                                continue;
                            }
                            Dictionary<string, object> report = new Dictionary<string, object>();
                            report.Add("Cap1", _iDanhMucService.FindDonViQuanLy(_sessionInfo.YearOfWork).ToUpper());
                            report.Add("Cap2", GetHeader2Report());
                            report.Add("FormatNumber", formatNumber);
                            report.Add("DonViTinh", donViTinhStr);
                            report.Add("Unit", donViTinh);
                            report.Add("TieuDe2", tieuDe);
                            report.Add("TenDonVi", item.TenDonVi.ToUpper());
                            report.Add("DonViCha", _sessionService?.Current?.TenDonViReportHeader);
                            report.Add("h1", donViTinhStr);
                            report.Add("Items", items);
                            report.Add("ReportName", ReportName);
                            AddChuKy(report, TypeChuky);

                            var xlsFile = _exportService.Export<TlDmCanBoKeHoachNq104Model>(templateFileName, report);
                            string fileNamePrefix = string.Format("rptLuong_DanhSach_ChiTra_LuongCN_{0}_Nam_{1}_{2}", thang, nam, maDonVi);
                            string fileNameWithoutExtension = StringUtils.CreateExportFileName(fileNamePrefix);
                            results.Add(new ExportResult(string.Format("{0} - {1}", maDonVi, tenDonVi), fileNameWithoutExtension, null, xlsFile));
                        }
                    }

                    e.Result = results;
                }, (s, e) =>
                {
                    if (e.Error == null)
                    {
                        var result = (List<ExportResult>)e.Result;
                        _exportService.Open(result, exportType);
                    }
                    else
                    {
                        _logger.Error(e.Error.Message);
                    }
                    IsLoading = false;
                });
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        private void ExportTHLuongDonVi(ExportType exportType)
        {
            try
            {
                BackgroundWorkerHelper.Run((s, e) =>
                {
                    IsLoading = true;

                    int donViTinh = int.Parse(_selectedUnitType.ValueItem);
                    FormatNumber formatNumber = new FormatNumber(donViTinh, exportType);
                    var thang = int.Parse(SelectedMonth.ValueItem);
                    var nam = int.Parse(SelectedYear.ValueItem);
                    var lstIdDonViSelected = ItemsDonVi.Where(x => x.IsSelected).ToList();

                    _tlBangLuongBridgeService.DataPreprocess(thang, nam, string.Join(",", lstIdDonViSelected.Select(x => x.MaDonVi)), CachTinhLuong.CACH0);
                    var itemsThang = _tlBangLuongThangService.ExportTongHopLuongTheoDonVi(thang, nam, _mapper.Map<List<TlDmDonViNq104>>(lstIdDonViSelected), CachTinhLuong.CACH0, donViTinh);
                    _tlBangLuongBridgeService.DataPreprocess(thang, nam, string.Join(",", lstIdDonViSelected.Select(x => x.MaDonVi)), CachTinhLuong.CACH5);
                    var itemsTruylinh = _tlBangLuongThangService.ExportTongHopLuongTheoDonVi(thang, nam, _mapper.Map<List<TlDmDonViNq104>>(lstIdDonViSelected), CachTinhLuong.CACH5, donViTinh);

                    var items = itemsThang.AsEnumerable().Union(itemsTruylinh.AsEnumerable()).CopyToDataTable();

                    //var row = items.AsEnumerable().FirstOrDefault(x => x.Field<string>("DoiTuong").Equals("Tổng truy lĩnh"));
                    //if (row != null)
                    //{
                    //    row[ExportColumnHeader.IS_PARENT] = true;
                    //}

                    Dictionary<string, object> data = new Dictionary<string, object>();
                    data.Add("Cap1", _iDanhMucService.FindDonViQuanLy(_sessionInfo.YearOfWork).ToUpper());
                    data.Add("Cap2", GetHeader2Report());
                    data.Add("FormatNumber", formatNumber);
                    data.Add("DonViTinh", "Đơn vị tính: " + (_selectedUnitType != null ? _selectedUnitType.DisplayItem : ""));
                    data.Add("Unit", donViTinh);
                    data.Add("TieuDe2", string.Format("Tháng {0} năm {1}", thang, nam));
                    data.Add("TenDonVi", SelectedDonVi.TenDonVi);
                    data.Add("Items", items);
                    data.Add("ReportName", ReportName);
                    data.Add("DonViCha", _sessionService?.Current?.TenDonViReportHeader);
                    AddChuKy(data, TypeChuky);

                    string templateFileName;
                    if (SelectedKhoIn.ValueItem == "A4")
                    {
                        templateFileName = Path.Combine(ExportPrefix.PATH_TL_LUONG_NEW, ExportFileName.RPT_TL_TONGHOP_LUONG_PHUCAP_DONVI_NEW);
                    }
                    else
                    {
                        templateFileName = Path.Combine(ExportPrefix.PATH_TL_LUONG_NEW, ExportFileName.RPT_TL_TONGHOP_LUONG_PHUCAP_DONVI_A3_NEW);
                    }
                    string fileNamePrefix = string.Format("rptLuong_TongHop_Luong_PhuCap_DonVi_{0}_Nam_{1}", thang, nam);
                    string fileNameWithoutExtension = StringUtils.CreateExportFileName(fileNamePrefix);
                    var xlsFile = _exportService.Export(templateFileName, data);
                    e.Result = new ExportResult(fileNameWithoutExtension, fileNameWithoutExtension, null, xlsFile);
                }, (s, e) =>
                {
                    if (e.Error == null)
                    {
                        var result = (ExportResult)e.Result;
                        if (result != null)
                        {
                            _exportService.Open(result, exportType);
                        }
                    }
                    else
                    {
                        _logger.Error(e.Error.Message);
                    }
                    IsLoading = false;
                });
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        private void ExportQuyetToanQuanSo(ExportType exportType)
        {
            try
            {
                BackgroundWorkerHelper.Run((s, e) =>
                {
                    IsLoading = true;
                    var donviSelect = ItemsDonVi.Where(x => x.IsSelected);

                    int donViTinh = int.Parse(_selectedUnitType.ValueItem);
                    FormatNumber formatNumber = new FormatNumber(donViTinh, exportType);
                    var thang = int.Parse(SelectedMonth.ValueItem);
                    var nam = int.Parse(SelectedYear.ValueItem);
                    //string agencyId = string.Join(",", DonViItems.Where(x => x.IsSelected).Select(x => x.MaDonVi).ToArray());
                    //var predicate = PredicateBuilder.True<TlDmCanBo>();
                    //predicate = predicate.And(x => x.Thang == thang);
                    //predicate = predicate.And(x => x.Nam == nam);
                    //predicate = predicate.And(x => x.MaTangGiam == null || !x.MaTangGiam.StartsWith("3"));
                    //predicate = predicate.And(x => x.IdDonVi == donVi.MaDonVi);
                    var listDonVi = _mapper.Map<ObservableCollection<TlDmDonViNq104>>(donviSelect).ToList();

                    var quansoPredicate = PredicateBuilder.True<TlQsChungTuChiTietNq104>();
                    quansoPredicate = quansoPredicate.And(x => x.NamLamViec == nam);
                    quansoPredicate = quansoPredicate.And(x => x.Thang == thang);
                    quansoPredicate = quansoPredicate.And(x => x.XauNoiMa.Equals(MA_TANG_GIAM.QUAN_SO_QT_THANG_NAY));

                    var items = _tlBangLuongThangService.ReportQtQuanSo(quansoPredicate, listDonVi);

                    Dictionary<string, object> data = new Dictionary<string, object>();
                    data.Add("Cap1", _danhMucService.FindDonViQuanLy(_sessionInfo.YearOfWork).ToUpper());
                    data.Add("Cap2", GetHeader2Report());
                    data.Add("FormatNumber", formatNumber);
                    data.Add("DonViTinh", "Đơn vị tính: " + (_selectedUnitType != null ? _selectedUnitType.DisplayItem : ""));
                    data.Add("Items", items);
                    data.Add("TieuDe2", string.Format("Tháng {0} năm {1}", thang, nam));
                    data.Add("ReportName", ReportName);
                    data.Add("DonViCha", _sessionService?.Current?.TenDonViReportHeader);
                    AddChuKy(data, TypeChuky);

                    string templateFileName = Path.Combine(ExportPrefix.PATH_TL_LUONG_NEW, ExportFileName.RPT_TL_QUYETTOAN_QUANSO_NEW);
                    string fileNamePrefix = string.Format("rptLuong_QuyetToan_QuanSo_{0}_Nam_{1}", thang, nam);
                    string fileNameWithoutExtension = StringUtils.CreateExportFileName(fileNamePrefix);
                    var xlsFile = _exportService.Export<TlQsChungTuChiTietNq104Model>(templateFileName, data);
                    e.Result = new ExportResult(fileNameWithoutExtension, fileNameWithoutExtension, null, xlsFile);

                }, (s, e) =>
                {
                    if (e.Error == null)
                    {
                        var result = (ExportResult)e.Result;
                        if (result != null)
                        {
                            _exportService.Open(result, exportType);
                        }
                    }
                    else
                    {
                        _logger.Error(e.Error.Message);
                    }
                    IsLoading = false;
                });
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        private void ExportGiaiThichChiTietPhuCapKhac(ExportType exportType)
        {
            try
            {
                BackgroundWorkerHelper.Run((s, e) =>
                {
                    IsLoading = true;

                    List<ExportResult> results = new List<ExportResult>();
                    string templateFileName;
                    string fileNamePrefix;
                    string fileNameWithoutExtension;

                    int donViTinh = int.Parse(_selectedUnitType.ValueItem);
                    FormatNumber formatNumber = new FormatNumber(donViTinh, exportType);
                    var thang = int.Parse(SelectedMonth.ValueItem);
                    var nam = int.Parse(SelectedYear.ValueItem);

                    var lstDonVi = _mapper.Map<List<TlDmDonViNq104>>(ItemsDonVi.Where(x => x.IsSelected));
                    var lstPhuCapSelect = ItemsPhuCap.Where(item => item.IsSelected).ToList();
                    var lstPhuCap = _mapper.Map<List<TlDmPhuCapNq104>>(lstPhuCapSelect);

                    var lstlDmCapBacNq104 = _iTlDmCapBacNq104Service.FindAll().Where(x => x.Nam == nam && string.IsNullOrEmpty(x.Parent)).GroupBy(x => x.MaCb).ToList();
                    var lstNgach = lstlDmCapBacNq104.ToDictionary(x => x.FirstOrDefault().MaCb, x => x.FirstOrDefault().TenCb);
                    //var lstNgach = new Dictionary<string, string>()
                    //{
                    //    { "1", "Sĩ quan" },
                    //    { "2", "QNCN" },
                    //    { "3", "Công nhân viên chức quốc phòng" },
                    //    { "4", "HSQ - CS" },
                    //    { "4", "HSQ - CS" },
                    //    { "4", "HSQ - CS" }
                    //};

                    _tlBangLuongBridgeService.DataPreprocess(thang, nam, string.Join(",", lstDonVi.Select(x => x.MaDonVi)), "CACH0");
                    var data = _tlBangLuongThangService.ReportGiaiThichChiTietPhuCapKhac(lstDonVi, lstPhuCap, nam, thang, CachTinhLuong.CACH0, donViTinh, IsOrderTheoChucVu);
                    foreach (var donVi in lstDonVi)
                    {
                        int page = 1;
                        var maDonVi = donVi.MaDonVi;
                        var tenDonVi = donVi.TenDonVi;
                        var tenDonViCha = string.Empty;
                        if (!string.IsNullOrEmpty(donVi.ParentId))
                        {
                            var donViCha = _tlDmDonViService.FindAll().FirstOrDefault(x => x.MaDonVi.Equals(donVi.ParentId));
                            if (donViCha != null)
                            {
                                tenDonViCha = donViCha.TenDonVi.ToUpper();
                            }
                        }

                        List<TlDmPhuCapNq104Model> lstPhuCapHasData = new List<TlDmPhuCapNq104Model>();
                        foreach (var pc in lstPhuCapSelect)
                        {
                            var value = data.Compute(string.Format("SUM({0})", pc.MaPhuCap), string.Format("MaDonVi='{0}'", donVi.MaDonVi));
                            if (!string.IsNullOrEmpty(Convert.ToString(value)) && decimal.Parse(Convert.ToString(value)) != 0)
                            {
                                lstPhuCapHasData.Add(pc);
                            }
                        }

                        int limit = 14;
                        var lstPhuCapOffset = SplitList(lstPhuCapHasData, limit).ToList();
                        foreach (var phuCapOffset in lstPhuCapOffset)
                        {
                            // Thêm cột trắng
                            int index = 1;
                            int countItemPage = phuCapOffset.Count;
                            int numColEmpty = 0;
                            if (countItemPage < limit)
                            {
                                numColEmpty = limit - countItemPage;
                                for (int k = 0; k < numColEmpty; k++)
                                {
                                    phuCapOffset.Add(new TlDmPhuCapNq104Model());
                                }
                            }

                            List<RptGiaiThichChiTietNq104PhuCapKhacModel> items = new List<RptGiaiThichChiTietNq104PhuCapKhacModel>();
                            foreach (var ngach in lstNgach)
                            {
                                string filter = string.Format("MaDonVi='{0}' AND XauNoiMa LIKE '{1}%'", donVi.MaDonVi, ngach.Key);
                                var rows = data.Select(filter);
                                if (rows.Any())
                                {
                                    var subData = rows.CopyToDataTable();

                                    // Parent item
                                    RptGiaiThichChiTietNq104PhuCapKhacModel itemParent = new RptGiaiThichChiTietNq104PhuCapKhacModel();
                                    itemParent.iStt = 0;
                                    itemParent.sTenCbo = ngach.Value;
                                    itemParent.sMaCb = string.Empty;
                                    itemParent.isParent = true;
                                    itemParent.ListGiaTri = new List<TlBangLuongThangNq104Model>();
                                    foreach (var phucap in phuCapOffset)
                                    {
                                        TlBangLuongThangNq104Model giaTri = new TlBangLuongThangNq104Model();
                                        if (!string.IsNullOrEmpty(phucap.MaPhuCap))
                                        {
                                            string value = subData.Compute(string.Format("SUM({0})", phucap.MaPhuCap), string.Empty).ToString();
                                            giaTri.MaPhuCap = phucap.MaPhuCap;
                                            giaTri.GiaTri = string.IsNullOrEmpty(value) ? 0 : decimal.Parse(value);
                                        }

                                        itemParent.ListGiaTri.Add(giaTri);
                                    }
                                    items.Add(itemParent);

                                    // Detail item
                                    RptGiaiThichChiTietNq104PhuCapKhacModel itemDetail;
                                    for (int j = 0; j < rows.Length; j++)
                                    {
                                        var rowDetail = rows[j];

                                        itemDetail = new RptGiaiThichChiTietNq104PhuCapKhacModel();
                                        itemDetail.iStt = index++;
                                        itemDetail.sTenCbo = rowDetail[ExportColumnHeader.TEN_CAN_BO].ToString();
                                        itemDetail.sMaCb = rowDetail[ExportColumnHeader.MA_CAP_BAC].ToString();
                                        itemDetail.ListGiaTri = new List<TlBangLuongThangNq104Model>();

                                        foreach (var phucap in phuCapOffset)
                                        {
                                            TlBangLuongThangNq104Model giaTri = new TlBangLuongThangNq104Model();
                                            if (!string.IsNullOrEmpty(phucap.MaPhuCap))
                                            {
                                                string val = rowDetail[phucap.MaPhuCap]?.ToString();
                                                giaTri.MaPhuCap = phucap.MaPhuCap;
                                                giaTri.GiaTri = string.IsNullOrEmpty(val) ? 0 : decimal.Parse(val);
                                            }

                                            itemDetail.ListGiaTri.Add(giaTri);
                                        }
                                        items.Add(itemDetail);
                                    }
                                }
                            }

                            List<RptGiaiThichChiTietNq104PhuCapKhacModel> itemsTotal = new List<RptGiaiThichChiTietNq104PhuCapKhacModel>();
                            var listTotal = new RptGiaiThichChiTietNq104PhuCapKhacModel();
                            listTotal.ListGiaTriTotal = new List<TlBangLuongThangNq104Model>();
                            foreach (var phucap in phuCapOffset)
                            {
                                TlBangLuongThangNq104Model giaTri = new TlBangLuongThangNq104Model();
                                if (!string.IsNullOrEmpty(phucap.MaPhuCap))
                                {
                                    var value = data.Compute(string.Format("SUM({0})", phucap.MaPhuCap), string.Format("MaDonVi='{0}'", donVi.MaDonVi));
                                    giaTri.MaPhuCap = phucap.MaPhuCap;
                                    giaTri.GiaTri = decimal.Parse(value.ToString());
                                    if (phucap.MaPhuCap == PhuCap.LHT_HS)
                                        giaTri.GiaTri = 0;
                                }

                                listTotal.ListGiaTriTotal.Add(giaTri);
                            }
                            itemsTotal.Add(listTotal);

                            if (items.Count == 0)
                            {
                                continue;
                            }

                            Dictionary<string, object> report = new Dictionary<string, object>();
                            report.Add("Cap1", _iDanhMucService.FindDonViQuanLy(_sessionInfo.YearOfWork).ToUpper());
                            report.Add("Cap2", GetHeader2Report());
                            report.Add("FormatNumber", formatNumber);
                            report.Add("DonViTinh", "Đơn vị tính: " + (_selectedUnitType != null ? _selectedUnitType.DisplayItem : ""));
                            report.Add("TieuDe1", Model.TenBaoCao);
                            report.Add("TieuDe2", string.Format("Tháng {0} năm {1}", thang, nam));
                            report.Add("TenDonVi", tenDonVi);
                            report.Add("DonViCha", _sessionService?.Current?.TenDonViReportHeader);
                            report.Add("Items", items);
                            report.Add("Headers", phuCapOffset);
                            report.Add("ItemsTotal", itemsTotal);
                            report.Add("ReportName", ReportName);
                            AddChuKy(report, TypeChuky);

                            var nameColunmMerge = GetExcelColumnName(phuCapOffset.Count + 3);
                            report.Add("rangeTieuDe", "A4:" + nameColunmMerge + "4");
                            report.Add("rangeTieuDe1", "A5:" + nameColunmMerge + "5");
                            report.Add("rangeTieuDe2", "A6:" + nameColunmMerge + "6");
                            report.Add("rangeDonVi", "A8:" + nameColunmMerge + "8");
                            report.Add("rangeNgay", "D16:" + nameColunmMerge + "16");
                            report.Add("rangeThuaLenh1", "D17:" + nameColunmMerge + "17");
                            report.Add("rangeChucDanh1", "D18:" + nameColunmMerge + "18");
                            report.Add("rangeGhiChuKy1", "D19:" + nameColunmMerge + "19");
                            report.Add("rangeTen1", "D25:" + nameColunmMerge + "25");
                            report.Add("rangeDonViTinh", "A10:" + nameColunmMerge + "10");

                            if (page == 1)
                            {
                                if (SelectedKhoIn.ValueItem == "A4")
                                {
                                    templateFileName = Path.Combine(ExportPrefix.PATH_TL_LUONG_NEW, ExportFileName.RPT_TL_GIAITHICH_CHITIET_PHUCAP_KHAC_TRANG1_NEW);
                                }
                                else
                                {
                                    templateFileName = Path.Combine(ExportPrefix.PATH_TL_LUONG_NEW, ExportFileName.RPT_TL_GIAITHICH_CHITIET_PHUCAP_KHAC_TRANG1_A3_NEW);
                                }
                                fileNamePrefix = string.Format("rptLuong_GiaiThich_ChiTiet_PhuCap_Khac_{0}_Nam_{1}_DonVi_{2}_Trang1", thang, nam, maDonVi);
                                fileNameWithoutExtension = StringUtils.CreateExportFileName(fileNamePrefix);
                            }
                            else
                            {
                                if (SelectedKhoIn.ValueItem == "A4")
                                {
                                    templateFileName = Path.Combine(ExportPrefix.PATH_TL_LUONG_NEW, ExportFileName.RPT_TL_GIAITHICH_CHITIET_PHUCAP_KHAC_TRANG2_NEW);
                                }
                                else
                                {
                                    templateFileName = Path.Combine(ExportPrefix.PATH_TL_LUONG_NEW, ExportFileName.RPT_TL_GIAITHICH_CHITIET_PHUCAP_KHAC_TRANG2_A3_NEW);
                                }
                                fileNamePrefix = string.Format("rptLuong_GiaiThich_ChiTiet_PhuCap_Khac_{0}_Nam_{1}_DonVi_{2}_Trang_{3}", thang, nam, maDonVi, page);
                                fileNameWithoutExtension = StringUtils.CreateExportFileName(fileNamePrefix);
                            }

                            var xlsFile = _exportService.Export<RptGiaiThichChiTietNq104PhuCapKhacModel, TlDmPhuCapNq104Model>(templateFileName, report);
                            results.Add(new ExportResult(string.Format("{0} - {1}", donVi.MaDonVi, donVi.TenDonVi), fileNameWithoutExtension, null, xlsFile));
                            page++;
                        }
                    }

                    e.Result = results;
                }, (s, e) =>
                {
                    if (e.Error == null)
                    {
                        var result = (List<ExportResult>)e.Result;
                        _exportService.Open(result, exportType);
                    }
                    else
                    {
                        _logger.Error(e.Error.Message);
                    }
                    IsLoading = false;
                });
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        private void ExportGiaiThichChiTietPhuCapKhacSummary(ExportType exportType)
        {
            try
            {
                BackgroundWorkerHelper.Run((s, e) =>
                {
                    IsLoading = true;

                    List<ExportResult> results = new List<ExportResult>();
                    string templateFileName;
                    string fileNamePrefix;
                    string fileNameWithoutExtension;

                    int donViTinh = int.Parse(_selectedUnitType.ValueItem);
                    FormatNumber formatNumber = new FormatNumber(donViTinh, exportType);
                    var thang = int.Parse(SelectedMonth.ValueItem);
                    var nam = int.Parse(SelectedYear.ValueItem);

                    var lstDonVi = _mapper.Map<List<TlDmDonViNq104>>(ItemsDonVi.Where(x => x.IsSelected));
                    var lstPhuCapSelect = ItemsPhuCap.Where(item => item.IsSelected).ToList();
                    var lstPhuCap = _mapper.Map<List<TlDmPhuCapNq104>>(lstPhuCapSelect);
                    var lstNgach = new Dictionary<string, string>()
                    {
                        { "1", "Sĩ quan" },
                        { "2", "QNCN" },
                        { "4", "Công nhân viên chức quốc phòng" },
                        { "0", "HSQ - CS" }
                    };
                    var data = _tlBangLuongThangService.ReportGiaiThichChiTietPhuCapKhac(lstDonVi, lstPhuCap, nam, thang, CachTinhLuong.CACH0, donViTinh, IsOrderTheoChucVu);

                    var tenDonViCha = string.Empty;

                    List<TlDmPhuCapNq104Model> lstPhuCapHasData = new List<TlDmPhuCapNq104Model>();
                    foreach (var pc in lstPhuCapSelect)
                    {
                        var value = data.Compute(string.Format("SUM({0})", pc.MaPhuCap), string.Empty);
                        if (!string.IsNullOrEmpty(Convert.ToString(value)) && decimal.Parse(Convert.ToString(value)) != 0)
                        {
                            lstPhuCapHasData.Add(pc);
                        }
                    }



                    int limit = 14;
                    var lstPhuCapOffset = SplitList(lstPhuCapHasData, limit).ToList();
                    foreach (var phuCapOffset in lstPhuCapOffset)
                    {
                        // Thêm cột trắng
                        int index = 1;
                        int countItemPage = phuCapOffset.Count;
                        int numColEmpty = 0;
                        if (countItemPage < limit)
                        {
                            numColEmpty = limit - countItemPage;
                            for (int k = 0; k < numColEmpty; k++)
                            {
                                phuCapOffset.Add(new TlDmPhuCapNq104Model());
                            }
                        }

                        List<RptGiaiThichChiTietNq104PhuCapKhacModel> items = new List<RptGiaiThichChiTietNq104PhuCapKhacModel>();
                        foreach (var ngach in lstNgach)
                        {
                            string filter = string.Format("MaCapBac LIKE '{0}%'", ngach.Key);
                            var rows = data.Select(filter);
                            if (rows.Any())
                            {
                                var subData = rows.CopyToDataTable();

                                // Parent item
                                RptGiaiThichChiTietNq104PhuCapKhacModel itemParent = new RptGiaiThichChiTietNq104PhuCapKhacModel();
                                itemParent.iStt = 0;
                                itemParent.sTenCbo = ngach.Value;
                                itemParent.sMaCb = string.Empty;
                                itemParent.isParent = true;
                                itemParent.ListGiaTri = new List<TlBangLuongThangNq104Model>();
                                foreach (var phucap in phuCapOffset)
                                {
                                    TlBangLuongThangNq104Model giaTri = new TlBangLuongThangNq104Model();
                                    if (!string.IsNullOrEmpty(phucap.MaPhuCap))
                                    {
                                        var value = subData.Compute(string.Format("SUM({0})", phucap.MaPhuCap), string.Empty);
                                        giaTri.MaPhuCap = phucap.MaPhuCap;
                                        giaTri.GiaTri = string.IsNullOrEmpty(value.ToString()) ? 0 : decimal.Parse(value.ToString());
                                    }

                                    itemParent.ListGiaTri.Add(giaTri);
                                }
                                items.Add(itemParent);

                                // Detail item
                                RptGiaiThichChiTietNq104PhuCapKhacModel itemDetail;
                                for (int j = 0; j < rows.Length; j++)
                                {
                                    var rowDetail = rows[j];

                                    itemDetail = new RptGiaiThichChiTietNq104PhuCapKhacModel();
                                    itemDetail.iStt = index++;
                                    itemDetail.sTenCbo = rowDetail[ExportColumnHeader.TEN_CAN_BO].ToString();
                                    itemDetail.sMaCb = rowDetail[ExportColumnHeader.MA_CAP_BAC].ToString();
                                    itemDetail.ListGiaTri = new List<TlBangLuongThangNq104Model>();

                                    foreach (var phucap in phuCapOffset)
                                    {
                                        TlBangLuongThangNq104Model giaTri = new TlBangLuongThangNq104Model();
                                        if (!string.IsNullOrEmpty(phucap.MaPhuCap))
                                        {
                                            var val = rowDetail[phucap.MaPhuCap];
                                            giaTri.MaPhuCap = phucap.MaPhuCap;
                                            giaTri.GiaTri = string.IsNullOrEmpty(val.ToString()) ? 0 : decimal.Parse(val.ToString());
                                        }

                                        itemDetail.ListGiaTri.Add(giaTri);
                                    }
                                    items.Add(itemDetail);
                                }
                            }
                        }

                        List<RptGiaiThichChiTietNq104PhuCapKhacModel> itemsTotal = new List<RptGiaiThichChiTietNq104PhuCapKhacModel>();
                        var listTotal = new RptGiaiThichChiTietNq104PhuCapKhacModel();
                        listTotal.ListGiaTriTotal = new List<TlBangLuongThangNq104Model>();
                        foreach (var phucap in phuCapOffset)
                        {
                            TlBangLuongThangNq104Model giaTri = new TlBangLuongThangNq104Model();
                            if (!string.IsNullOrEmpty(phucap.MaPhuCap))
                            {
                                var value = data.Compute(string.Format("SUM({0})", phucap.MaPhuCap), string.Empty);
                                giaTri.MaPhuCap = phucap.MaPhuCap;
                                giaTri.GiaTri = string.IsNullOrEmpty(value.ToString()) ? 0 : decimal.Parse(value.ToString());
                                if (phucap.MaPhuCap == PhuCap.LHT_HS)
                                    giaTri.GiaTri = 0;
                            }

                            listTotal.ListGiaTriTotal.Add(giaTri);
                        }
                        itemsTotal.Add(listTotal);

                        if (items.Count == 0)
                        {
                            continue;
                        }

                        Dictionary<string, object> report = new Dictionary<string, object>();
                        report.Add("Cap1", _iDanhMucService.FindDonViQuanLy(_sessionInfo.YearOfWork).ToUpper());
                        report.Add("Cap2", GetHeader2Report());
                        report.Add("FormatNumber", formatNumber);
                        report.Add("DonViTinh", "Đơn vị tính: " + (_selectedUnitType != null ? _selectedUnitType.DisplayItem : ""));
                        report.Add("TieuDe2", string.Format("Tháng {0} năm {1}", thang, nam));
                        report.Add("DonViCha", _sessionService?.Current?.TenDonViReportHeader);
                        report.Add("Items", items);
                        report.Add("Headers", phuCapOffset);
                        report.Add("ItemsTotal", itemsTotal);
                        report.Add("ReportName", ReportName);
                        AddChuKy(report, TypeChuky);

                        var nameColunmMerge = GetExcelColumnName(phuCapOffset.Count + 3);
                        report.Add("rangeTieuDe", "A4:" + nameColunmMerge + "4");
                        report.Add("rangeTieuDe1", "A5:" + nameColunmMerge + "5");
                        report.Add("rangeTieuDe2", "A6:" + nameColunmMerge + "6");
                        report.Add("rangeDonVi", "A8:" + nameColunmMerge + "8");
                        report.Add("rangeNgay", "D16:" + nameColunmMerge + "16");
                        report.Add("rangeThuaLenh1", "D17:" + nameColunmMerge + "17");
                        report.Add("rangeChucDanh1", "D18:" + nameColunmMerge + "18");
                        report.Add("rangeGhiChuKy1", "D19:" + nameColunmMerge + "19");
                        report.Add("rangeTen1", "D25:" + nameColunmMerge + "25");
                        report.Add("rangeDonViTinh", "A10:" + nameColunmMerge + "10");

                        if (SelectedKhoIn.ValueItem == "A4")
                        {
                            templateFileName = Path.Combine(ExportPrefix.PATH_TL_LUONG_NEW, ExportFileName.RPT_TL_GIAITHICH_CHITIET_PHUCAP_KHAC_SUMMARY_NEW);
                        }
                        else
                        {
                            templateFileName = Path.Combine(ExportPrefix.PATH_TL_LUONG_NEW, ExportFileName.RPT_TL_GIAITHICH_CHITIET_PHUCAP_KHAC_SUMMARY_A3_NEW);
                        }
                        fileNamePrefix = string.Format("rptLuong_GiaiThich_ChiTiet_PhuCap_Khac_{0}_Nam_{1}_Summary", thang, nam);
                        fileNameWithoutExtension = StringUtils.CreateExportFileName(fileNamePrefix);


                        var xlsFile = _exportService.Export<RptGiaiThichChiTietNq104PhuCapKhacModel, TlDmPhuCapNq104Model>(templateFileName, report);
                        results.Add(new ExportResult(string.Format("GiaiThich_PhuCap_Khac_TongHop_thang {0} - {1}", thang, nam), fileNameWithoutExtension, null, xlsFile));

                    }

                    e.Result = results;
                }, (s, e) =>
                {
                    if (e.Error == null)
                    {
                        var result = (List<ExportResult>)e.Result;
                        _exportService.Open(result, exportType);
                    }
                    else
                    {
                        _logger.Error(e.Error.Message);
                    }
                    IsLoading = false;
                });
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        private void ExportGiaiThichPhuCapTheoNgaySummary(ExportType exportType) { }
        private void ExportGiaiThichPhuCapTheoNgay(ExportType exportType)
        {
            try
            {
                BackgroundWorkerHelper.Run((s, e) =>
                {
                    IsLoading = true;

                    List<ExportResult> results = new List<ExportResult>();
                    string templateFileName;
                    string fileNamePrefix;
                    string fileNameWithoutExtension;

                    int donViTinh = int.Parse(_selectedUnitType.ValueItem);
                    FormatNumber formatNumber = new FormatNumber(donViTinh, exportType);
                    var thang = int.Parse(SelectedMonth.ValueItem);
                    var nam = int.Parse(SelectedYear.ValueItem);

                    var lstDonVi = _mapper.Map<List<TlDmDonViNq104>>(ItemsDonVi.Where(x => x.IsSelected));
                    var lstPhuCapSelect = ItemsPhuCap.Where(item => item.IsSelected).ToList();
                    var lstPhuCap = _mapper.Map<List<TlDmPhuCapNq104>>(lstPhuCapSelect);
                    var lstNgach = new Dictionary<string, string>()
                    {
                        { "1", "Sĩ quan" },
                        { "2", "QNCN" },
                        { "3", "Công nhân viên chức quốc phòng" },
                        { "4", "HSQ - CS" }
                    };

                    _tlBangLuongBridgeService.DataPreprocess(thang, nam, string.Join(",", lstDonVi.Select(x => x.MaDonVi)), CachTinhLuong.CACH0);
                    _tlCanBoPhuCapBridgeNq104Service.DataPreprocess(thang, nam);
                    var dataGiaTri = _tlBangLuongThangService.ReportGiaiThichPhuCapTheoNgay(lstDonVi, lstPhuCap, nam, thang, CachTinhLuong.CACH0, donViTinh, IsOrderTheoChucVu);
                    var dataSoNgay = _tlBangLuongThangService.ReportGiaiThichSoNgayPhuCapTheoNgay(lstDonVi, lstPhuCap, nam, thang, CachTinhLuong.CACH0, donViTinh, IsOrderTheoChucVu);
                    foreach (var donVi in lstDonVi)
                    {
                        int page = 1;
                        var maDonVi = donVi.MaDonVi;
                        var tenDonVi = donVi.TenDonVi;
                        var tenDonViCha = string.Empty;
                        if (!string.IsNullOrEmpty(donVi.ParentId))
                        {
                            var donViCha = _tlDmDonViService.FindAll().FirstOrDefault(x => x.MaDonVi.Equals(donVi.ParentId));
                            if (donViCha != null)
                            {
                                tenDonViCha = donViCha.TenDonVi.ToUpper();
                            }
                        }

                        List<TlDmPhuCapNq104Model> lstGiaTriPhuCapHasData = new List<TlDmPhuCapNq104Model>();
                        List<TlDmPhuCapNq104Model> lstSoNgayPhuCapHasData = new List<TlDmPhuCapNq104Model>();

                        foreach (var pc in lstPhuCapSelect)
                        {
                            var valueGiaTri = dataGiaTri.Compute(string.Format("SUM({0})", pc.MaPhuCap), string.Format("MaDonVi='{0}'", donVi.MaDonVi));
                            if (!string.IsNullOrEmpty(Convert.ToString(valueGiaTri)) && decimal.Parse(Convert.ToString(valueGiaTri)) != 0)
                            {
                                lstGiaTriPhuCapHasData.Add(pc);
                            }
                        }

                        int limit = 3;
                        var lstPhuCapOffset = SplitList(lstGiaTriPhuCapHasData, limit).ToList();
                        foreach (var phuCapOffset in lstPhuCapOffset)
                        {
                            // Thêm cột trắng
                            int index = 1;
                            int countItemPage = phuCapOffset.Count;
                            int numColEmpty = 0;
                            if (countItemPage < limit)
                            {
                                numColEmpty = limit - countItemPage;
                                for (int k = 0; k < numColEmpty; k++)
                                {
                                    phuCapOffset.Add(new TlDmPhuCapNq104Model());
                                }
                            }

                            List<RptGiaiThichPhuCapTheoNgay> items = new List<RptGiaiThichPhuCapTheoNgay>();

                            List<RptGiaiThichPhuCapTheoNgay> itemsTotal = new List<RptGiaiThichPhuCapTheoNgay>();
                            var listTotal = new RptGiaiThichPhuCapTheoNgay();
                            listTotal.ListGiaTriTotal = new List<RptGiaiThichPhuCapTheoNgayData>();
                            foreach (var ngach in lstNgach)
                            {
                                string filter = string.Format("MaDonVi='{0}' AND XauNoiMa LIKE '{1}%'", donVi.MaDonVi, ngach.Key);
                                var rowsGiaTri = dataGiaTri.Select(filter);
                                var rowsSoNgay = dataSoNgay.Select(filter);
                                if (rowsGiaTri.Any() && rowsSoNgay.Any())
                                {
                                    var subDataGiaTri = rowsGiaTri.CopyToDataTable();
                                    var subDataSoNgay = rowsSoNgay.CopyToDataTable();

                                    // Parent item
                                    //RptGiaiThichPhuCapTheoNgay itemParent = new RptGiaiThichPhuCapTheoNgay();
                                    //itemParent.iStt = 0;
                                    //itemParent.sTenCbo = ngach.Value;
                                    //itemParent.sMaCb = string.Empty;
                                    //itemParent.isParent = true;
                                    //itemParent.ListGiaTri = new List<RptGiaiThichPhuCapTheoNgayData>();
                                    //foreach (var phucap in phuCapOffset)
                                    //{
                                    //    RptGiaiThichPhuCapTheoNgayData giaTri = new RptGiaiThichPhuCapTheoNgayData();
                                    //    if (!string.IsNullOrEmpty(phucap.MaPhuCap))
                                    //    {
                                    //        //string value = subData.Compute(string.Format("SUM({0})", phucap.MaPhuCap), string.Empty).ToString();
                                    //        string value = 0;
                                    //        giaTri.MaPhuCap = phucap.MaPhuCap;
                                    //        giaTri.GiaTri = string.IsNullOrEmpty(value) ? 0 : decimal.Parse(value);
                                    //    }

                                    //    itemParent.ListGiaTri.Add(giaTri);
                                    //}
                                    //items.Add(itemParent);

                                    // Detail item

                                    RptGiaiThichPhuCapTheoNgay itemDetail;
                                    for (int j = 0; j < rowsGiaTri.Length; j++)
                                    {
                                        var rowDetailGiaTri = rowsGiaTri[j];
                                        var rowDetailSoNgay = rowsSoNgay[j];

                                        itemDetail = new RptGiaiThichPhuCapTheoNgay();
                                        itemDetail.iStt = index++;
                                        itemDetail.sTenCbo = rowDetailGiaTri[ExportColumnHeader.TEN_CAN_BO].ToString();
                                        itemDetail.sMaCb = rowDetailGiaTri[ExportColumnHeader.MA_CAP_BAC].ToString();
                                        itemDetail.ListGiaTri = new List<RptGiaiThichPhuCapTheoNgayData>();
                                        itemDetail.TongCong = 0;

                                        foreach (var phucap in phuCapOffset)
                                        {
                                            RptGiaiThichPhuCapTheoNgayData giaTri = new RptGiaiThichPhuCapTheoNgayData();
                                            if (!string.IsNullOrEmpty(phucap.MaPhuCap))
                                            {
                                                var giatri = rowDetailGiaTri[phucap.MaPhuCap].ToString();
                                                var songay = rowDetailSoNgay[phucap.MaPhuCap].ToString();

                                                var sotientrenngay = Math.Round(decimal.Parse(giatri) / decimal.Parse(songay), 2);

                                                giaTri.GiaTri = string.IsNullOrEmpty(giatri) ? 0 : decimal.Parse(giatri);
                                                giaTri.SoNgay = decimal.Parse(songay.ToString());
                                                giaTri.SoTienTheoNgay = giaTri.SoNgay == 0 ? 0 : Math.Round(giaTri.GiaTri / giaTri.SoNgay, 2);
                                                itemDetail.TongCong += giaTri.GiaTri;
                                            }

                                            itemDetail.ListGiaTri.Add(giaTri);
                                        }
                                        items.Add(itemDetail);
                                    }


                                }
                            }

                            listTotal.TongCong = 0;

                            foreach (var phucap in phuCapOffset)
                            {
                                RptGiaiThichPhuCapTheoNgayData giaTri = new RptGiaiThichPhuCapTheoNgayData();
                                if (!string.IsNullOrEmpty(phucap.MaPhuCap))
                                {
                                    var valueGiaTri = dataGiaTri.Compute(string.Format("SUM({0})", phucap.MaPhuCap), string.Format("MaDonVi='{0}'", donVi.MaDonVi));
                                    var valueSoNgay = dataSoNgay.Compute(string.Format("SUM({0})", phucap.MaPhuCap), string.Format("MaDonVi='{0}'", donVi.MaDonVi));
                                    giaTri.GiaTri = decimal.Parse(valueGiaTri.ToString());
                                    giaTri.SoNgay = decimal.Parse(valueSoNgay.ToString());
                                    giaTri.SoTienTheoNgay = giaTri.SoNgay == 0 ? 0 : Math.Round(giaTri.GiaTri / giaTri.SoNgay, 2);
                                    listTotal.TongCong += giaTri.GiaTri;
                                }

                                listTotal.ListGiaTriTotal.Add(giaTri);
                            }
                            itemsTotal.Add(listTotal);

                            if (items.Count == 0)
                            {
                                continue;
                            }

                            int columnStarts = 4;

                            var listHeader = new List<HeaderReportGiaiThichPhuCapTheoNgay>();
                            var listTile = new List<HeaderReportGiaiThichPhuCapTheoNgay>();

                            for (int i = 0; i < phuCapOffset.Count(); i++)
                            {
                                var phucapDefault = new HeaderReportGiaiThichPhuCapTheoNgay();
                                phucapDefault.TenPhuCap = "pc";
                                phucapDefault.MergeRange = "";
                                var titleDefault = new HeaderReportGiaiThichPhuCapTheoNgay();
                                titleDefault.SN = "SN";
                                titleDefault.SoTienTrenNgay = "Số tiền / Ngày";
                                titleDefault.TongTien = "Tổng tiền";
                                var columnStartName = GetExcelColumnName(columnStarts);
                                var columnEndName = GetExcelColumnName(columnStarts + 2);
                                var phucap = new HeaderReportGiaiThichPhuCapTheoNgay();

                                phucap.TenPhuCap = phuCapOffset[i].TenPhuCap;
                                phucap.MergeRange = columnStartName + "7" + ":" + columnEndName + "7";
                                listHeader.Add(phucap);
                                listHeader.Add(phucapDefault);
                                listHeader.Add(phucapDefault);
                                listTile.Add(titleDefault);
                            }

                            Dictionary<string, object> report = new Dictionary<string, object>();
                            report.Add("Cap1", _iDanhMucService.FindDonViQuanLy(_sessionInfo.YearOfWork).ToUpper());
                            report.Add("Cap2", GetHeader2Report());
                            report.Add("FormatNumber", formatNumber);
                            report.Add("DonViTinh", "Đơn vị tính: " + (_selectedUnitType != null ? _selectedUnitType.DisplayItem : ""));
                            report.Add("TieuDe1", Model.TenBaoCao);
                            report.Add("TieuDe2", string.Format("Tháng {0} năm {1}", thang, nam));
                            report.Add("TenDonVi", tenDonVi);
                            report.Add("DonViCha", _sessionService?.Current?.TenDonViReportHeader);
                            report.Add("Items", items);
                            report.Add("Headers", listHeader);
                            report.Add("LstTile", listTile);
                            report.Add("ItemsTotal", itemsTotal);
                            report.Add("ReportName", ReportName);
                            AddChuKy(report, TypeChuky);

                            if (page == 1)
                            {
                                if (SelectedKhoIn.ValueItem == "A4")
                                {
                                    templateFileName = Path.Combine(ExportPrefix.PATH_TL_LUONG_NEW, ExportFileName.RPT_TL_LUONG_GIAITHICH_PHUCAP_THEO_NGAY_NEW);
                                }
                                else
                                {
                                    templateFileName = Path.Combine(ExportPrefix.PATH_TL_LUONG_NEW, ExportFileName.RPT_TL_LUONG_GIAITHICH_PHUCAP_THEO_NGAY_NEW);
                                }
                                fileNamePrefix = string.Format("rptLuong_GiaiThich_PhuCap_TheoNgay_{0}_Nam_{1}_DonVi_{2}_Trang1", thang, nam, maDonVi);
                                fileNameWithoutExtension = StringUtils.CreateExportFileName(fileNamePrefix);
                            }
                            else
                            {
                                if (SelectedKhoIn.ValueItem == "A4")
                                {
                                    templateFileName = Path.Combine(ExportPrefix.PATH_TL_LUONG_NEW, ExportFileName.RPT_TL_LUONG_GIAITHICH_PHUCAP_THEO_NGAY_NEW);
                                }
                                else
                                {
                                    templateFileName = Path.Combine(ExportPrefix.PATH_TL_LUONG_NEW, ExportFileName.RPT_TL_LUONG_GIAITHICH_PHUCAP_THEO_NGAY_NEW);
                                }
                                fileNamePrefix = string.Format("rptLuong_GiaiThich_PhuCap_TheoNgay_{0}_Nam_{1}_DonVi_{2}_Trang_{3}", thang, nam, maDonVi, page);
                                fileNameWithoutExtension = StringUtils.CreateExportFileName(fileNamePrefix);
                            }

                            var xlsFile = _exportService.Export<RptGiaiThichPhuCapTheoNgay, HeaderReportGiaiThichPhuCapTheoNgay>(templateFileName, report);
                            results.Add(new ExportResult(string.Format("{0} - {1}", donVi.MaDonVi, donVi.TenDonVi), fileNameWithoutExtension, null, xlsFile));
                            page++;
                        }
                    }

                    e.Result = results;
                }, (s, e) =>
                {
                    if (e.Error == null)
                    {
                        var result = (List<ExportResult>)e.Result;
                        _exportService.Open(result, exportType);
                    }
                    else
                    {
                        _logger.Error(e.Error.Message);
                    }
                    IsLoading = false;
                });
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        public void ReportInKiem(ExportType exportType)
        {
            try
            {
                BackgroundWorkerHelper.Run((s, e) =>
                {
                    IsLoading = true;

                    List<ExportResult> results = new List<ExportResult>();
                    string templateFileName = Path.Combine(ExportPrefix.PATH_TL_LUONG_NEW, ExportFileName.RPT_TL_IN_KIEM_NEW);

                    int donViTinh = int.Parse(_selectedUnitType.ValueItem);
                    FormatNumber formatNumber = new FormatNumber(donViTinh, exportType);
                    var lstDonVi = ItemsDonVi.Where(x => x.IsSelected);
                    var thang = int.Parse(SelectedMonth.ValueItem);
                    var nam = int.Parse(SelectedYear.ValueItem);
                    string thangNam = nam.ToString() + thang.ToString();
                    foreach (var donVi in lstDonVi)
                    {
                        var items = _tlBangLuongThangService.ReportInKiem(nam, thang, donVi.MaDonVi, donViTinh);
                        if (items.Rows.Count == 0)
                        {
                            continue;
                        }
                        Dictionary<string, object> data = new Dictionary<string, object>();
                        data.Add("Cap1", _iDanhMucService.FindDonViQuanLy(_sessionInfo.YearOfWork).ToUpper());
                        data.Add("Cap2", GetHeader2Report());
                        data.Add("FormatNumber", formatNumber);
                        data.Add("DonViTinh", "Đơn vị tính: " + (_selectedUnitType != null ? _selectedUnitType.DisplayItem : ""));
                        data.Add("TieuDe", string.Format("Tháng: {0} - Năm: {1} - {2}", thang, nam, donVi.TenDonVi));
                        data.Add("Items", items);
                        data.Add("ReportName", ReportName);
                        data.Add("TenDonVi", donVi.TenDonVi);
                        data.Add("DonViCha", _sessionService?.Current?.TenDonViReportHeader);
                        AddChuKy(data, TypeChuky);

                        var xlsFile = _exportService.Export(templateFileName, data);
                        string fileNamePrefix = string.Format("rpt_In_Kiem_{0}_Nam_{1}_{2}", thang, nam, donVi.TenDonVi);
                        string fileNameWithoutExtension = StringUtils.CreateExportFileName(fileNamePrefix);
                        results.Add(new ExportResult(string.Format("{0} - {1}", donVi.MaDonVi, donVi.TenDonVi), fileNameWithoutExtension, null, xlsFile));
                    }
                    e.Result = results;
                }, (s, e) =>
                {
                    if (e.Error == null)
                    {
                        var result = (List<ExportResult>)e.Result;
                        _exportService.Open(result, exportType);
                    }
                    else
                    {
                        _logger.Error(e.Error.Message);
                    }
                    IsLoading = false;
                });
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        public void ReportGiaiThichchiTietPhuCap(ExportType exportType)
        {
            try
            {
                BackgroundWorkerHelper.Run((s, e) =>
                {
                    IsLoading = true;
                    List<ExportResult> results = new List<ExportResult>();
                    string templateFileName = Path.Combine(ExportPrefix.PATH_TL_LUONG_NEW, ExportFileName.RPT_TL_PHUCAP_CHITIET_NEW);
                    int donViTinh = int.Parse(_selectedUnitType.ValueItem);
                    FormatNumber formatNumber = new FormatNumber(donViTinh, exportType);
                    var thang = int.Parse(SelectedMonth.ValueItem);
                    var nam = int.Parse(SelectedYear.ValueItem);
                    var lstDonVi = ItemsDonVi.Where(x => x.IsSelected);
                    _tlBangLuongBridgeService.DataPreprocess(thang, nam, string.Join(",", lstDonVi.Select(x => x.MaDonVi)), "CACH0");

                    DataTable itemsAll = _tlBangLuongThangService.ReportGiaiThichChiTietPhuCapHsqCs(_mapper.Map<List<TlDmDonViNq104>>(lstDonVi), nam, thang, donViTinh);

                    foreach (var item in lstDonVi)
                    {
                        var dataRows = itemsAll.AsEnumerable().Where(x => x.Field<string>("MaDonVi").Equals(item.MaDonVi));
                        var items = itemsAll.Clone();
                        if (dataRows.Count() == 0)
                        {
                            continue;
                        }
                        foreach (DataRow item1 in dataRows)
                        {
                            items.Rows.Add(item1.ItemArray);
                        }

                        Dictionary<string, object> data = new Dictionary<string, object>();
                        data.Add("Cap1", _danhMucService.FindDonViQuanLy(_sessionInfo.YearOfWork).ToUpper());
                        data.Add("Cap2", GetHeader2Report());
                        data.Add("FormatNumber", formatNumber);
                        data.Add("Unit", donViTinh);
                        data.Add("DonViTinh", "Đơn vị tính: " + (_selectedUnitType != null ? _selectedUnitType.DisplayItem : ""));
                        data.Add("TieuDe", string.Format("Tháng: {0} - Năm: {1}", thang, nam));
                        data.Add("nam", "Năm: " + nam);
                        data.Add("donvi", item.TenDonVi.ToUpper());
                        data.Add("DonViCha", _sessionService?.Current?.TenDonViReportHeader);
                        data.Add("ngaythangnam", string.Format("Ngày {0} Tháng {1} Năm {2}", DateTime.Now.Date.ToString("dd"), DateTime.Now.Month, DateTime.Now.Year));
                        data.Add("Items", items);
                        data.Add("ReportName", ReportName);
                        AddChuKy(data, TypeChuky);
                        var xlsFile = _exportService.Export(templateFileName, data);
                        string fileNamePrefix = string.Format("rptLuong_PhuCap_ChiTiet_{0}_{1}_{2}", thang, nam, item.TenDonVi);
                        string fileNameWithoutExtension = StringUtils.CreateExportFileName(fileNamePrefix);
                        results.Add(new ExportResult(string.Format("{0} - {1}", item.MaDonVi, item.TenDonVi), fileNameWithoutExtension, null, xlsFile));
                    }
                    e.Result = results;
                }, (s, e) =>
                {
                    if (e.Error == null)
                    {
                        var result = (List<ExportResult>)e.Result;
                        _exportService.Open(result, exportType);
                    }
                    else
                    {
                        _logger.Error(e.Error.Message);
                    }
                    IsLoading = false;
                });
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        public void ExportQTQSMau2(ExportType exportType)
        {
            try
            {
                BackgroundWorkerHelper.Run((s, e) =>
                {
                    IsLoading = true;
                    List<ExportResult> results = new List<ExportResult>();
                    string templateFileName;
                    if (SelectedKhoIn.ValueItem == "A4")
                    {
                        templateFileName = Path.Combine(ExportPrefix.PATH_TL_LUONG_NEW, ExportFileName.RPT_TL_QS_CHUNGTU_A3_NEW);
                    }
                    else
                    {
                        templateFileName = Path.Combine(ExportPrefix.PATH_TL_LUONG_NEW, ExportFileName.RPT_TL_QS_CHUNGTU_A4_NEW);
                    }

                    int donViTinh = int.Parse(_selectedUnitType.ValueItem);
                    FormatNumber formatNumber = new FormatNumber(donViTinh, exportType);
                    var nam = int.Parse(SelectedYear.ValueItem);
                    var thang = SelectedMonthQuarter.ValueItem;
                    var namTruoc = int.Parse(SelectedYear.ValueItem);
                    var thangTruoc = "";
                    if ("Cả năm".Equals(_selectedMonthQuarter.DisplayItem))
                    {
                        thangTruoc = _selectedMonthQuarter.ValueItem;
                        namTruoc -= 1;
                    }
                    else if ("1,2,3".Equals(_selectedMonthQuarter.ValueItem))
                    {
                        thangTruoc = "10,11,12";
                        namTruoc -= int.Parse(SelectedYear.ValueItem) - 1;
                    }
                    else if ("4,5,6".Equals(_selectedMonthQuarter.ValueItem))
                    {
                        thangTruoc = "1,2,3";
                    }
                    else if ("7,8,9".Equals(_selectedMonthQuarter.ValueItem))
                    {
                        thangTruoc = "4,5,6";
                    }
                    else if ("10,11,12".Equals(_selectedMonthQuarter.ValueItem))
                    {
                        thangTruoc = "7,8,9";
                    }
                    else if ("1".Equals(SelectedMonthQuarter.ValueItem))
                    {
                        thangTruoc = "12";
                        namTruoc = int.Parse(SelectedYear.ValueItem) - 1;
                    }
                    else
                    {
                        thangTruoc = (int.Parse(SelectedMonthQuarter.ValueItem) - 1).ToString();
                    }
                    var lstDonVi = ItemsDonVi.Where(x => x.IsSelected);
                    foreach (var item in lstDonVi)
                    {
                        var bangLuongPredicate = PredicateBuilder.True<TlQsChungTuNq104>();
                        bangLuongPredicate = bangLuongPredicate.And(x => item.MaDonVi.Equals(x.MaDonVi));
                        bangLuongPredicate = bangLuongPredicate.And(x => x.Nam == nam);
                        bangLuongPredicate = bangLuongPredicate.And(x => thang.Split(',').Contains(x.Thang.ToString()));
                        var dataList = _tlBangLuongThangService.FindQuyetToanQuanSo(item.MaDonVi, thang, nam, thangTruoc, namTruoc);
                        //var dataList = _tlBangLuongThangService.FindChungTuChiTiet(bangLuongPredicate).OrderBy(x => x.XauNoiMa);
                        var items = _mapper.Map<ObservableCollection<TlQsChungTuChiTietNq104Model>>(dataList);

                        if (items.Count() == 0)
                        {
                            continue;
                        }

                        items.Select(x =>
                        {
                            x.IsParent = string.IsNullOrEmpty(x.MlnsIdParent) ? true : false;
                            return x;
                        }).ToList();

                        Dictionary<string, object> data = new Dictionary<string, object>();
                        data.Add("Cap1", _danhMucService.FindDonViQuanLy(_sessionInfo.YearOfWork).ToUpper());
                        data.Add("Cap2", GetHeader2Report());
                        data.Add("FormatNumber", formatNumber);
                        data.Add("Unit", donViTinh);
                        data.Add("DonViTinh", "Đơn vị tính: " + (_selectedUnitType != null ? _selectedUnitType.DisplayItem : ""));
                        if ("Cả năm".Equals(_selectedMonthQuarter.DisplayItem))
                            data.Add("TieuDe", string.Format("Năm: {0}", nam));
                        else
                            data.Add("TieuDe", string.Format("{0} năm: {1}", _selectedMonthQuarter.DisplayItem, nam));
                        data.Add("nam", "Năm: " + nam);
                        data.Add("DonVi", item.TenDonVi.ToUpper());
                        data.Add("DonViCha", _sessionService?.Current?.TenDonViReportHeader);
                        data.Add("ngaythangnam", string.Format("Ngày {0} Tháng {1} Năm {2}", DateTime.Now.Date.ToString("dd"), DateTime.Now.Month, DateTime.Now.Year));
                        data.Add("Items", items);
                        data.Add("ReportName", ReportName);
                        AddChuKy(data, TypeChuky);
                        var xlsFile = _exportService.Export<TlQsChungTuChiTietNq104Model>(templateFileName, data);
                        string fileNamePrefix = string.Format("rptLuong_QS_ChungTu_{0}_{1}_{2}", thang, nam, item.TenDonVi);
                        string fileNameWithoutExtension = StringUtils.CreateExportFileName(fileNamePrefix);
                        results.Add(new ExportResult(string.Format("{0} - {1}", item.MaDonVi, item.TenDonVi), fileNameWithoutExtension, null, xlsFile));
                    }
                    e.Result = results;
                }, (s, e) =>
                {
                    if (e.Error == null)
                    {
                        var result = (List<ExportResult>)e.Result;
                        _exportService.Open(result, exportType);
                    }
                    else
                    {
                        _logger.Error(e.Error.Message);
                    }
                    IsLoading = false;
                });
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        public void ExportQTQSMau2Summary(ExportType exportType)
        {
            try
            {
                BackgroundWorkerHelper.Run((s, e) =>
                {
                    IsLoading = true;
                    List<ExportResult> results = new List<ExportResult>();
                    string templateFileName;
                    if (SelectedKhoIn.ValueItem == "A4")
                    {
                        templateFileName = Path.Combine(ExportPrefix.PATH_TL_LUONG_NEW, ExportFileName.RPT_TL_QS_CHUNGTU_A3_NEW);
                    }
                    else
                    {
                        templateFileName = Path.Combine(ExportPrefix.PATH_TL_LUONG_NEW, ExportFileName.RPT_TL_QS_CHUNGTU_A4_NEW);
                    }

                    int donViTinh = int.Parse(_selectedUnitType.ValueItem);
                    FormatNumber formatNumber = new FormatNumber(donViTinh, exportType);
                    var thang = int.Parse(SelectedMonth.ValueItem);
                    var nam = int.Parse(SelectedYear.ValueItem);
                    var lstDonVi = ItemsDonVi.Where(x => x.IsSelected).Select(x => x.MaDonVi).Distinct().ToList();

                    var bangLuongPredicate = PredicateBuilder.True<TlQsChungTuNq104>();
                    //bangLuongPredicate = bangLuongPredicate.And(x => item.MaDonVi.Equals(x.MaDonVi));
                    bangLuongPredicate = bangLuongPredicate.And(x => x.Nam == nam);
                    bangLuongPredicate = bangLuongPredicate.And(x => x.Thang == thang);
                    var dataList = _tlBangLuongThangService.FindChungTuChiTietSummary(bangLuongPredicate, lstDonVi).OrderBy(x => x.XauNoiMa);
                    var items = _mapper.Map<ObservableCollection<TlQsChungTuChiTietNq104Model>>(dataList);
                    items.Select(x =>
                    {
                        x.IsParent = string.IsNullOrEmpty(x.MlnsIdParent) ? true : false;
                        return x;
                    }).ToList();

                    Dictionary<string, object> data = new Dictionary<string, object>();
                    data.Add("Cap1", _danhMucService.FindDonViQuanLy(_sessionInfo.YearOfWork).ToUpper());
                    data.Add("Cap2", GetHeader2Report());
                    data.Add("FormatNumber", formatNumber);
                    data.Add("Unit", donViTinh);
                    data.Add("DonViTinh", "Đơn vị tính: " + (_selectedUnitType != null ? _selectedUnitType.DisplayItem : ""));
                    data.Add("TieuDe", string.Format("Tháng: {0} - Năm: {1}", thang, nam));
                    data.Add("nam", "Năm: " + nam);
                    data.Add("DonVi", string.Empty);
                    data.Add("DonViCha", _sessionService?.Current?.TenDonViReportHeader);
                    data.Add("ngaythangnam", string.Format("Ngày {0} Tháng {1} Năm {2}", DateTime.Now.Date.ToString("dd"), DateTime.Now.Month, DateTime.Now.Year));
                    data.Add("Items", items);
                    data.Add("ReportName", ReportName);
                    AddChuKy(data, TypeChuky);
                    var xlsFile = _exportService.Export<TlQsChungTuChiTietNq104Model>(templateFileName, data);
                    string fileNamePrefix = string.Format("rptLuong_QS_ChungTu_{0}_{1}", thang, nam);
                    string fileNameWithoutExtension = StringUtils.CreateExportFileName(fileNamePrefix);
                    results.Add(new ExportResult(string.Format("{0}", "TongHop"), fileNameWithoutExtension, null, xlsFile));

                    e.Result = results;
                }, (s, e) =>
                {
                    if (e.Error == null)
                    {
                        var result = (List<ExportResult>)e.Result;
                        _exportService.Open(result, exportType);
                    }
                    else
                    {
                        _logger.Error(e.Error.Message);
                    }
                    IsLoading = false;
                });
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        private void ExportBienDongQuanHamKeHoach(ExportType exportType)
        {
            BackgroundWorkerHelper.Run((s, e) =>
            {
                IsLoading = true;

                List<ExportResult> results = new List<ExportResult>();
                string templateFileName = Path.Combine(ExportPrefix.PATH_TL_LUONG_NEW, ExportFileName.RPT_TL_BIENDONG_QUANHAM_KEHOACH_NEW);
                string fileNamePrefix;
                string fileNameWithoutExtension;

                var lstDonVi = ItemsDonVi.Where(x => x.IsSelected);
                foreach (var item in lstDonVi)
                {
                    var nam = int.Parse(SelectedYear.ValueItem);
                    var maDonVi = item.MaDonVi;
                    string tenDonVi = string.Empty;
                    if (IsChecked)
                    {
                        tenDonVi = item.TenDonVi;
                    }

                    List<TlBienDongQHKeHoachModel> Items = new List<TlBienDongQHKeHoachModel>();
                    var predicate = PredicateBuilder.True<TlDmCanBoKeHoachNq104>();
                    predicate = predicate.And(x => x.Nam == nam);
                    predicate = predicate.And(x => maDonVi.Equals(x.Parent));
                    predicate = predicate.And(x => x.Loai.Contains(LoaiCanBoKehoach.THAYDOIQH_NANGLUONG));
                    var data = _tlDmCanBoKeHoachService.FindByCondition(predicate);

                    var lstSiQuan = data.Where(x => x.MaCb.StartsWith("1"));
                    var lstQncn = data.Where(x => x.MaCb.StartsWith("2"));
                    var lstHaSqCs = data.Where(x => x.MaCb.StartsWith("0"));
                    var lstLaoDong = data.Where(x => x.MaCb.StartsWith("4"));

                    if (lstSiQuan != null && lstSiQuan.Count() > 0)
                    {
                        var bdkhModel = new TlBienDongQHKeHoachModel();
                        bdkhModel.IsHangCha = true;
                        bdkhModel.TenCanBo = "Sĩ quan";
                        Items.Add(bdkhModel);

                        for (int i = 1; i <= 12; i++)
                        {
                            int stt = 0;
                            var lstSiQuanThang = lstSiQuan.Where(x => x.Thang == i);
                            foreach (var item1 in lstSiQuanThang)
                            {
                                TlBienDongQHKeHoachModel tlBienDongLuongThangChiTietModel = new TlBienDongQHKeHoachModel();
                                tlBienDongLuongThangChiTietModel.ThangTangQh = string.Format("Tháng {0}", i);
                                tlBienDongLuongThangChiTietModel.Stt = ++stt;
                                tlBienDongLuongThangChiTietModel.TenCanBo = item1.TenCanBo;
                                tlBienDongLuongThangChiTietModel.NamThamNien = (int)item1.NamTn;
                                tlBienDongLuongThangChiTietModel.NgayNn = item1.NgayNn;

                                var cb = _iTlDmCapBacService.FindByMaCapBac(item1.MaCb);
                                tlBienDongLuongThangChiTietModel.CapBacMoi = cb.Note;

                                var canBoNamTruoc = _tlDmCanboService.FindByMaCanBo(string.Format("{0}{1}{2}", item1.Nam - 1, i.ToString("D2"), item1.MaHieuCanBo));
                                //tlBienDongLuongThangChiTietModel.CapBacCu = canBoNamTruoc == null ? string.Empty : canBoNamTruoc.TlDmCapBacNq104.Note;
                                Items.Add(tlBienDongLuongThangChiTietModel);
                            }
                        }
                    }

                    if (lstQncn != null && lstQncn.Count() > 0)
                    {
                        var bdkhModel = new TlBienDongQHKeHoachModel();
                        bdkhModel.IsHangCha = true;
                        bdkhModel.TenCanBo = "QNCN";
                        Items.Add(bdkhModel);

                        for (int i = 1; i <= 12; i++)
                        {
                            int stt = 0;
                            var lstSiQuanThang = lstQncn.Where(x => x.Thang == i);
                            foreach (var item1 in lstSiQuanThang)
                            {
                                TlBienDongQHKeHoachModel tlBienDongLuongThangChiTietModel = new TlBienDongQHKeHoachModel();
                                tlBienDongLuongThangChiTietModel.ThangTangQh = string.Format("Tháng {0}", i);
                                tlBienDongLuongThangChiTietModel.Stt = ++stt;
                                tlBienDongLuongThangChiTietModel.TenCanBo = item1.TenCanBo;
                                tlBienDongLuongThangChiTietModel.NamThamNien = (int)item1.NamTn;
                                tlBienDongLuongThangChiTietModel.NgayNn = item1.NgayNn;
                                tlBienDongLuongThangChiTietModel.CapBacMoi = item1.HeSoLuong.ToString();

                                var canBoNamTruoc = _tlDmCanboService.FindByMaCanBo(string.Format("{0}{1}{2}", item1.Nam - 1, i.ToString("D2"), item1.MaHieuCanBo));
                                tlBienDongLuongThangChiTietModel.CapBacCu = canBoNamTruoc == null ? string.Empty : canBoNamTruoc.HeSoLuong.ToString();
                                Items.Add(tlBienDongLuongThangChiTietModel);
                            }
                        }
                    }

                    if (lstHaSqCs != null && lstHaSqCs.Count() > 0)
                    {
                        var bdkhModel = new TlBienDongQHKeHoachModel();
                        bdkhModel.IsHangCha = true;
                        bdkhModel.TenCanBo = "Hạ sĩ quan - chiến sĩ";
                        Items.Add(bdkhModel);

                        for (int i = 1; i <= 12; i++)
                        {
                            int stt = 0;
                            var lstSiQuanThang = lstHaSqCs.Where(x => x.Thang == i);
                            foreach (var item1 in lstSiQuanThang)
                            {
                                TlBienDongQHKeHoachModel tlBienDongLuongThangChiTietModel = new TlBienDongQHKeHoachModel();
                                tlBienDongLuongThangChiTietModel.ThangTangQh = string.Format("Tháng {0}", i);
                                tlBienDongLuongThangChiTietModel.Stt = ++stt;
                                tlBienDongLuongThangChiTietModel.TenCanBo = item1.TenCanBo;
                                tlBienDongLuongThangChiTietModel.NamThamNien = (int)item1.NamTn;
                                tlBienDongLuongThangChiTietModel.NgayNn = item1.NgayNn;

                                var cb = _iTlDmCapBacService.FindByMaCapBac(item1.MaCb);
                                tlBienDongLuongThangChiTietModel.CapBacMoi = cb.Note;

                                var canBoNamTruoc = _tlDmCanboService.FindByMaCanBo(string.Format("{0}{1}{2}", item1.Nam - 1, i.ToString("D2"), item1.MaHieuCanBo));
                                //tlBienDongLuongThangChiTietModel.CapBacCu = canBoNamTruoc == null ? string.Empty : canBoNamTruoc.TlDmCapBacNq104.Note;
                                Items.Add(tlBienDongLuongThangChiTietModel);
                            }
                        }
                    }

                    if (lstLaoDong != null && lstLaoDong.Count() > 0)
                    {
                        var bdkhModel = new TlBienDongQHKeHoachModel();
                        bdkhModel.IsHangCha = true;
                        bdkhModel.TenCanBo = "Công nhân viên chức quốc phòng";
                        Items.Add(bdkhModel);

                        for (int i = 1; i <= 12; i++)
                        {
                            int stt = 0;
                            var lstSiQuanThang = lstLaoDong.Where(x => x.Thang == i);
                            foreach (var item1 in lstSiQuanThang)
                            {
                                TlBienDongQHKeHoachModel tlBienDongLuongThangChiTietModel = new TlBienDongQHKeHoachModel();
                                tlBienDongLuongThangChiTietModel.ThangTangQh = string.Format("Tháng {0}", i);
                                tlBienDongLuongThangChiTietModel.Stt = ++stt;
                                tlBienDongLuongThangChiTietModel.TenCanBo = item1.TenCanBo;
                                tlBienDongLuongThangChiTietModel.NamThamNien = (int)item1.NamTn;
                                tlBienDongLuongThangChiTietModel.NgayNn = item1.NgayNn;
                                tlBienDongLuongThangChiTietModel.CapBacMoi = item1.HeSoLuong.ToString();

                                var canBoNamTruoc = _tlDmCanboService.FindByMaCanBo(string.Format("{0}{1}{2}", item1.Nam - 1, i.ToString("D2"), item1.MaHieuCanBo));
                                tlBienDongLuongThangChiTietModel.CapBacCu = canBoNamTruoc == null ? string.Empty : canBoNamTruoc.HeSoLuong.ToString();
                                Items.Add(tlBienDongLuongThangChiTietModel);
                            }
                        }
                    }

                    FormatNumber formatNumber = new FormatNumber(1, exportType);
                    Dictionary<string, object> dataExport = new Dictionary<string, object>();
                    dataExport.Add("Cap1", _iDanhMucService.FindDonViQuanLy(_sessionInfo.YearOfWork).ToUpper());
                    dataExport.Add("Cap2", GetHeader2Report());
                    dataExport.Add("Nam", nam);
                    dataExport.Add("DonVi", tenDonVi.ToUpper());

                    if (!string.IsNullOrEmpty(item.ParentId))
                    {
                        var donViCha = _tlDmDonViService.FindAll().FirstOrDefault(x => x.MaDonVi.Equals(item.ParentId));
                        if (donViCha != null)
                        {
                            dataExport.Add("DonViCha", donViCha.TenDonVi.ToUpper());
                        }
                        else
                        {
                            dataExport.Add("DonViCha", string.Empty);
                        }
                    }
                    else
                    {
                        dataExport.Add("DonViCha", string.Empty);
                    }

                    dataExport.Add("ReportName", ReportName);
                    dataExport.Add("Items", Items);
                    dataExport.Add("FormatNumber", formatNumber);
                    AddChuKy(dataExport, TypeChuky);

                    fileNamePrefix = string.Format("rpt_ChiTiet_BienDong_QuanHam_KeHoach_Nam_{0}_{1}", nam, tenDonVi);
                    fileNameWithoutExtension = StringUtils.CreateExportFileName(fileNamePrefix);
                    var xlsFile = _exportService.Export<TlBienDongQHKeHoachModel>(templateFileName, dataExport);
                    results.Add(new ExportResult(string.Format("{0} - {1}", maDonVi, tenDonVi), fileNameWithoutExtension, null, xlsFile));
                }
                e.Result = results;
            }, (s, e) =>
            {
                if (e.Error == null)
                {
                    var result = (List<ExportResult>)e.Result;
                    _exportService.Open(result, exportType);
                }
                else
                {
                    _logger.Error(e.Error.Message);
                }
                IsLoading = false;
            });
        }

        private void ExportDanhSachChiTraCaNhanNganHangSummary(ExportType exportType)
        {
            try
            {
                BackgroundWorkerHelper.Run((s, e) =>
                {
                    IsLoading = true;

                    List<ExportResult> results = new List<ExportResult>();
                    string templateFileName = Path.Combine(ExportPrefix.PATH_TL_LUONG_NEW, ExportFileName.RPT_TL_DS_TRA_NGAN_HANG_NEW);

                    int donViTinh = int.Parse(_selectedUnitType.ValueItem);
                    FormatNumber formatNumber = new FormatNumber(donViTinh, exportType);
                    var nam = int.Parse(SelectedYear.ValueItem);
                    var thang = int.Parse(SelectedMonth.ValueItem);
                    var lstDonVi = ItemsDonVi.Where(x => x.IsSelected).ToList();

                    _tlBangLuongBridgeService.DataPreprocess(thang, nam, string.Join(",", lstDonVi.Select(x => x.MaDonVi)), null);
                    DataTable items = _tlBangLuongThangService.ReportDanhSachChiTraNganHangTongHopTheoDonVi(nam, thang, donViTinh, SNoiDung, _mapper.Map<List<TlDmDonViNq104>>(lstDonVi));

                    Dictionary<string, object> data = new Dictionary<string, object>();
                    data.Add("FormatNumber", formatNumber);
                    data.Add("DonViTinh", "Đơn vị tính: " + (_selectedUnitType != null ? _selectedUnitType.DisplayItem : ""));
                    data.Add("Unit", donViTinh);
                    data.Add("Tieude1", "(Kèm theo UNC ...………….. Ngày ….. Tháng ….. Năm …….)");
                    data.Add("ThangNam", string.Format("Tháng {0} năm {1}", thang, nam));
                    data.Add("DonVi", "Báo cáo tổng hợp");
                    data.Add("ngaythangnam", string.Format("Ngày {0} Tháng {1} Năm {2}", DateTime.Now.Date.ToString("dd"), thang, nam));
                    data.Add("Items", items);
                    data.Add("ReportName", ReportName);
                    data.Add("Cap1", _iDanhMucService.FindDonViQuanLy(_sessionInfo.YearOfWork).ToUpper());
                    data.Add("Cap2", GetHeader2Report());
                    AddChuKy(data, _typeChuky);

                    var xlsFile = _exportService.Export<TlDmCanBoKeHoachNq104Model>(templateFileName, data);
                    string fileNamePrefix = string.Format("rpt_Danh_sach_chi_tra_qua_ngan_hang_thang_{0}_nam_{1}_{2}", thang, nam, "Tổng hợp");
                    string fileNameWithoutExtension = StringUtils.CreateExportFileName(fileNamePrefix);
                    results.Add(new ExportResult(string.Format("{0} - {1}", "Tổng hợp", "Tổng hợp"), fileNameWithoutExtension, null, xlsFile));

                    e.Result = results;
                }, (s, e) =>
                {
                    if (e.Error == null)
                    {
                        var result = (List<ExportResult>)e.Result;
                        _exportService.Open(result, exportType);
                    }
                    else
                    {
                        _logger.Error(e.Error.Message);
                    }
                    IsLoading = false;
                });
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        private void ExportDanhSachChiTraCaNhanNganHang(ExportType exportType)
        {
            try
            {
                BackgroundWorkerHelper.Run((s, e) =>
                {
                    IsLoading = true;

                    List<ExportResult> results = new List<ExportResult>();
                    string templateFileName = Path.Combine(ExportPrefix.PATH_TL_LUONG_NEW, ExportFileName.RPT_TL_DS_TRA_NGAN_HANG_NEW);

                    int donViTinh = int.Parse(_selectedUnitType.ValueItem);
                    FormatNumber formatNumber = new FormatNumber(donViTinh, exportType);
                    var nam = int.Parse(SelectedYear.ValueItem);
                    var thang = int.Parse(SelectedMonth.ValueItem);
                    var lstDonVi = ItemsDonVi.Where(x => x.IsSelected);
                    _tlBangLuongBridgeService.DataPreprocess(thang, nam, string.Join(",", lstDonVi.Select(x => x.MaDonVi)), null);
                    foreach (var item in lstDonVi)
                    {
                        var maDonVi = item.MaDonVi;
                        var tenDonVi = item.TenDonVi;
                        var predicate = PredicateBuilder.True<TlBangLuongThangNq104>();
                        predicate = predicate.And(x => x.Thang == thang);
                        predicate = predicate.And(x => x.Nam == nam);
                        predicate = predicate.And(x => x.MaCachTl == CachTinhLuong.CACH0);
                        predicate = predicate.And(x => x.MaDonVi == maDonVi);

                        DataTable items = _tlBangLuongThangService.ReportDanhSachChiTraNganHang(nam, thang, donViTinh, SNoiDung, _mapper.Map<TlDmDonViNq104>(item));

                        Dictionary<string, object> data = new Dictionary<string, object>();
                        data.Add("FormatNumber", formatNumber);
                        data.Add("DonViTinh", "Đơn vị tính: " + (_selectedUnitType != null ? _selectedUnitType.DisplayItem : ""));
                        data.Add("Unit", donViTinh);
                        data.Add("Tieude1", "(Kèm theo UNC ...………….. Ngày ….. Tháng ….. Năm …….)");
                        data.Add("ThangNam", string.Format("Tháng {0} năm {1}", thang, nam));
                        data.Add("DonVi", item.TenDonVi);
                        data.Add("ngaythangnam", string.Format("Ngày {0} Tháng {1} Năm {2}", DateTime.Now.Date.ToString("dd"), thang, nam));
                        data.Add("Items", items);
                        data.Add("ReportName", ReportName);
                        data.Add("Cap1", _iDanhMucService.FindDonViQuanLy(_sessionInfo.YearOfWork).ToUpper());
                        data.Add("Cap2", GetHeader2Report());
                        AddChuKy(data, _typeChuky);

                        var xlsFile = _exportService.Export<TlDmCanBoKeHoachNq104Model>(templateFileName, data);
                        string fileNamePrefix = string.Format("rpt_Danh_sach_chi_tra_qua_ngan_hang_thang_{0}_nam_{1}_{2}", thang, nam, tenDonVi);
                        string fileNameWithoutExtension = StringUtils.CreateExportFileName(fileNamePrefix);
                        results.Add(new ExportResult(string.Format("{0} - {1}", item.MaDonVi, item.TenDonVi), fileNameWithoutExtension, null, xlsFile));
                    }
                    e.Result = results;
                }, (s, e) =>
                {
                    if (e.Error == null)
                    {
                        var result = (List<ExportResult>)e.Result;
                        _exportService.Open(result, exportType);
                    }
                    else
                    {
                        _logger.Error(e.Error.Message);
                    }
                    IsLoading = false;
                });
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        private void ExportChuyenCheDoSiQuan(ExportType exportType)
        {
            try
            {
                BackgroundWorkerHelper.Run((s, e) =>
                {
                    IsLoading = true;

                    List<ExportResult> results = new List<ExportResult>();
                    string templateFileName = Path.Combine(ExportPrefix.PATH_TL_LUONG_NEW, ExportFileName.RPT_TL_TRUYLINH_CHUYENCHEDO_SQ_NEW);
                    int donViTinh = int.Parse(_selectedUnitType.ValueItem);
                    FormatNumber formatNumber = new FormatNumber(donViTinh, exportType);
                    var thang = int.Parse(SelectedMonth.ValueItem);
                    var nam = int.Parse(SelectedYear.ValueItem);
                    var lstDonVi = ItemsDonVi.Where(x => x.IsSelected);
                    var listDonVi = _mapper.Map<ObservableCollection<TlDmDonViNq104>>(lstDonVi);

                    var lstDonViStr = listDonVi.Select(x => x.MaDonVi);

                    var predicate = PredicateBuilder.True<TlDmCanBoNq104>();
                    predicate = predicate.And(x => x.Nam == nam);
                    predicate = predicate.And(x => x.Thang == thang);
                    predicate = predicate.And(x => x.MaCb.StartsWith("1"));
                    predicate = predicate.And(x => !string.IsNullOrEmpty(x.MaCbCu) && x.MaCbCu.StartsWith("2"));
                    predicate = predicate.And(x => lstDonViStr.Contains(x.Parent));

                    _tlBangLuongBridgeService.DataPreprocess(thang, nam, string.Join(",", lstDonVi.Select(x => x.MaDonVi)), null);
                    var dataQuery = _tlBangLuongThangService.ExportTruyLinhChuyenCheDo(predicate, string.Join(",", lstDonViStr), thang, nam, IsOrderTheoChucVu);
                    var itemData = _mapper.Map<List<TlRptTruyLinhChuyenCheDoModel>>(dataQuery);

                    foreach (var item in lstDonVi)
                    {
                        var items = itemData.Where(x => x.MaDonVi.Equals(item.MaDonVi)).ToList();
                        if (items.Count == 0)
                        {
                            continue;
                        }

                        int stt = 0;
                        for (int i = 0; i <= itemData.Count - 1; i++)
                        {
                            items[i].Stt = ++stt;
                        }

                    AddDic:
                        Dictionary<string, object> data = new Dictionary<string, object>();
                        data.Add("Cap1", _iDanhMucService.FindDonViQuanLy(_sessionInfo.YearOfWork).ToUpper());
                        data.Add("Cap2", GetHeader2Report());
                        data.Add("FormatNumber", formatNumber);
                        data.Add("Unit", donViTinh);
                        data.Add("DonViTinh", "Đơn vị tính: " + (_selectedUnitType != null ? _selectedUnitType.DisplayItem : ""));
                        data.Add("TieuDe", string.Format("Tháng: {0} - Năm: {1}", thang, nam));
                        data.Add("nam", "Năm: " + nam);
                        data.Add("DonVi", item.TenDonVi.ToUpper());
                        data.Add("DonViCha", _sessionService?.Current?.TenDonViReportHeader);
                        data.Add("ngaythangnam", string.Format("Ngày {0} Tháng {1} Năm {2}", DateTime.Now.Date.ToString("dd"), DateTime.Now.Month, DateTime.Now.Year));
                        data.Add("Items", items);
                        //data.Add("Items2", itemData[1]);
                        data.Add("ReportName", ReportName);
                        AddChuKy(data, TypeChuky);
                        var xlsFile = _exportService.Export<TlRptTruyLinhChuyenCheDoModel>(templateFileName, data);
                        string fileNamePrefix = string.Format("rptLuong_ChuyenCheDo_SiQuan_{0}_{1}_{2}", thang, nam, item.TenDonVi);
                        string fileNameWithoutExtension = StringUtils.CreateExportFileName(fileNamePrefix);
                        results.Add(new ExportResult(string.Format("{0} - {1}", item.MaDonVi, item.TenDonVi), fileNameWithoutExtension, null, xlsFile));
                    }
                    e.Result = results;
                }, (s, e) =>
                {
                    if (e.Error == null)
                    {
                        var result = (List<ExportResult>)e.Result;
                        _exportService.Open(result, exportType);
                    }
                    else
                    {
                        _logger.Error(e.Error.Message);
                    }
                    IsLoading = false;
                });
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        public void ExportChuyenCheDoQNCN(ExportType exportType)
        {
            try
            {
                BackgroundWorkerHelper.Run((s, e) =>
                {
                    IsLoading = true;

                    List<ExportResult> results = new List<ExportResult>();
                    string templateFileName = Path.Combine(ExportPrefix.PATH_TL_LUONG_NEW, ExportFileName.RPT_TL_TRUYLINH_CHUYENCHEDO_QNCN_NEW);
                    int donViTinh = int.Parse(_selectedUnitType.ValueItem);
                    FormatNumber formatNumber = new FormatNumber(donViTinh, exportType);
                    var thang = int.Parse(SelectedMonth.ValueItem);
                    var nam = int.Parse(SelectedYear.ValueItem);
                    var lstDonVi = ItemsDonVi.Where(x => x.IsSelected);
                    var listDonVi = _mapper.Map<ObservableCollection<TlDmDonViNq104>>(lstDonVi);

                    var lstDonViStr = listDonVi.Select(x => x.MaDonVi);

                    var predicate = PredicateBuilder.True<TlDmCanBoNq104>();
                    predicate = predicate.And(x => x.Nam == nam);
                    predicate = predicate.And(x => x.Thang == thang);
                    predicate = predicate.And(x => x.MaCb.StartsWith("2"));
                    predicate = predicate.And(x => !string.IsNullOrEmpty(x.MaCbCu) && x.MaCbCu.StartsWith("4"));
                    predicate = predicate.And(x => lstDonViStr.Contains(x.Parent));
                    var dataQuery = _tlBangLuongThangService.ExportTruyLinhChuyenCheDo(predicate, string.Join(",", lstDonViStr), thang, nam, IsOrderTheoChucVu);
                    var itemData = _mapper.Map<List<TlRptTruyLinhChuyenCheDoModel>>(dataQuery);

                    foreach (var item in lstDonVi)
                    {
                        var items = itemData.Where(x => x.MaDonVi.Equals(item.MaDonVi)).ToList();
                        if (items.Count == 0)
                        {
                            continue;
                        }

                        int stt = 0;
                        for (int i = 0; i <= itemData.Count - 1; i++)
                        {
                            itemData[i].Stt = ++stt;
                        }

                    AddDic:
                        Dictionary<string, object> data = new Dictionary<string, object>();
                        data.Add("Cap1", _iDanhMucService.FindDonViQuanLy(_sessionInfo.YearOfWork).ToUpper());
                        data.Add("Cap2", GetHeader2Report());
                        data.Add("FormatNumber", formatNumber);
                        data.Add("Unit", donViTinh);
                        data.Add("DonViTinh", "Đơn vị tính: " + (_selectedUnitType != null ? _selectedUnitType.DisplayItem : ""));
                        data.Add("TieuDe", string.Format("Tháng: {0} - Năm: {1}", thang, nam));
                        data.Add("nam", "Năm: " + nam);
                        data.Add("DonVi", item.TenDonVi.ToUpper());
                        data.Add("DonViCha", _sessionService?.Current?.TenDonViReportHeader);
                        data.Add("ngaythangnam", string.Format("Ngày {0} Tháng {1} Năm {2}", DateTime.Now.Date.ToString("dd"), DateTime.Now.Month, DateTime.Now.Year));
                        data.Add("Items", itemData);
                        //data.Add("Items2", itemData[1]);
                        data.Add("ReportName", ReportName);
                        AddChuKy(data, TypeChuky);
                        var xlsFile = _exportService.Export<TlRptTruyLinhChuyenCheDoModel>(templateFileName, data);
                        string fileNamePrefix = string.Format("rptLuong_ChuyenCheDo_QNCN_{0}_{1}_{2}", thang, nam, item.TenDonVi);
                        string fileNameWithoutExtension = StringUtils.CreateExportFileName(fileNamePrefix);
                        results.Add(new ExportResult(string.Format("{0} - {1}", item.MaDonVi, item.TenDonVi), fileNameWithoutExtension, null, xlsFile));
                    }
                    e.Result = results;
                }, (s, e) =>
                {
                    if (e.Error == null)
                    {
                        var result = (List<ExportResult>)e.Result;
                        _exportService.Open(result, exportType);
                    }
                    else
                    {
                        _logger.Error(e.Error.Message);
                    }
                    IsLoading = false;
                });
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        public void ExportPhanTichTienAn(ExportType exportType)
        {
            try
            {
                BackgroundWorkerHelper.Run((s, e) =>
                {
                    IsLoading = true;

                    List<ExportResult> results = new List<ExportResult>();
                    string templateFileName = Path.Combine(ExportPrefix.PATH_TL_LUONG_NEW, ExportFileName.RPT_TL_BAOCAO_TIENAN_NEW);
                    int donViTinh = int.Parse(_selectedUnitType.ValueItem);
                    FormatNumber formatNumber = new FormatNumber(donViTinh, exportType);
                    var thang = int.Parse(SelectedMonth.ValueItem);
                    var nam = int.Parse(SelectedYear.ValueItem);
                    var lstDonViModel = ItemsDonVi.Where(x => x.IsSelected).ToList();
                    var lstDonVi = _mapper.Map<ObservableCollection<TlDmDonViNq104>>(lstDonViModel).ToList();
                    var maDonVi = lstDonVi.Select(x => x.MaDonVi).Distinct();
                    var lstPhuCap = _iTlDmPhuCapService.FindAll().ToList();
                    //var listDonVi = _mapper.Map<ObservableCollection<TlDmDonVi>>(lstDonVi);
                    _tlBangLuongBridgeService.DataPreprocess(thang, nam, string.Join(",", lstDonVi.Select(x => x.MaDonVi)), null);
                    var dataAll = _tlBangLuongThangService.GetDataTienAn(thang, nam, string.Join(",", maDonVi));
                    var itemData = _tlBangLuongThangService.ExportBaoCaoTienAn(dataAll, lstPhuCap);

                    Dictionary<string, object> data = new Dictionary<string, object>();
                    data.Add("Cap1", _danhMucService.FindDonViQuanLy(_sessionInfo.YearOfWork).ToUpper());
                    data.Add("Cap2", GetHeader2Report());
                    data.Add("FormatNumber", formatNumber);
                    data.Add("Unit", donViTinh);
                    data.Add("DonViTinh", "Đơn vị tính: " + (_selectedUnitType != null ? _selectedUnitType.DisplayItem : ""));
                    data.Add("TieuDe", string.Format("Tháng: {0} - Năm: {1}", thang, nam));
                    data.Add("nam", "Năm: " + nam);
                    //data.Add("DonVi", item.TenDonVi);
                    data.Add("ngaythangnam", string.Format("Ngày {0} Tháng {1} Năm {2}", DateTime.Now.Date.ToString("dd"), DateTime.Now.Month, DateTime.Now.Year));
                    data.Add("Items", itemData);
                    //data.Add("Items2", itemData[1]);
                    data.Add("ReportName", ReportName);
                    data.Add("DonViCha", _sessionService?.Current?.TenDonViReportHeader);
                    AddChuKy(data, TypeChuky);
                    var xlsFile = _exportService.Export(templateFileName, data);
                    string fileNamePrefix = string.Format("rptLuong_BaoCao_PhanTich_TienAn_{0}_{1}", thang, nam);
                    string fileNameWithoutExtension = StringUtils.CreateExportFileName(fileNamePrefix);
                    results.Add(new ExportResult(string.Format("{0}", "BaoCaoTienAn"), fileNameWithoutExtension, null, xlsFile));

                    e.Result = results;
                }, (s, e) =>
                {
                    if (e.Error == null)
                    {
                        var result = (List<ExportResult>)e.Result;
                        _exportService.Open(result, exportType);
                    }
                    else
                    {
                        _logger.Error(e.Error.Message);
                    }
                    IsLoading = false;
                });
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        public void ExportPhanTichTienAnSummary(ExportType exportType)
        {
            try
            {
                BackgroundWorkerHelper.Run((s, e) =>
                {
                    IsLoading = true;

                    List<ExportResult> results = new List<ExportResult>();
                    string templateFileName = Path.Combine(ExportPrefix.PATH_TL_LUONG_NEW, ExportFileName.RPT_TL_BAOCAO_TIENAN_NEW);
                    int donViTinh = int.Parse(_selectedUnitType.ValueItem);
                    FormatNumber formatNumber = new FormatNumber(donViTinh, exportType);
                    var thang = int.Parse(SelectedMonth.ValueItem);
                    var nam = int.Parse(SelectedYear.ValueItem);
                    var lstDonViModel = ItemsDonVi.Where(x => x.IsSelected).ToList();
                    var maDonVi = lstDonViModel.Select(x => x.MaDonVi).Distinct();
                    _tlBangLuongBridgeService.DataPreprocess(thang, nam, string.Join(",", lstDonViModel.Select(x => x.MaDonVi)), null);
                    _tlCanBoPhuCapBridgeNq104Service.DataPreprocess(thang, nam);
                    var dataAll = _tlBangLuongThangService.GetDataTienAn(thang, nam, string.Join(",", maDonVi));
                    var lstPhuCap = _iTlDmPhuCapService.FindAll().ToList();

                    foreach (var donVi in lstDonViModel)
                    {
                        var itemData = _tlBangLuongThangService.GetDataTienAn(dataAll, donVi.MaDonVi, lstPhuCap);
                        if (itemData.Rows.Count == 0)
                        {
                            continue;
                        }

                        Dictionary<string, object> data = new Dictionary<string, object>();
                        data.Add("Cap1", _iDanhMucService.FindDonViQuanLy(_sessionInfo.YearOfWork).ToUpper());
                        data.Add("Cap2", GetHeader2Report());
                        data.Add("FormatNumber", formatNumber);
                        data.Add("Unit", donViTinh);
                        data.Add("DonViTinh", "Đơn vị tính: " + (_selectedUnitType != null ? _selectedUnitType.DisplayItem : ""));
                        data.Add("TieuDe", string.Format("Tháng: {0} - Năm: {1}", thang, nam));
                        data.Add("nam", "Năm: " + nam);
                        data.Add("ngaythangnam", string.Format("Ngày {0} Tháng {1} Năm {2}", DateTime.Now.Date.ToString("dd"), DateTime.Now.Month, DateTime.Now.Year));
                        data.Add("Items", itemData);
                        data.Add("ReportName", ReportName);
                        data.Add("DonViCha", _sessionService?.Current?.TenDonViReportHeader);
                        AddChuKy(data, TypeChuky);
                        var xlsFile = _exportService.Export(templateFileName, data);
                        string fileNamePrefix = string.Format("rptLuong_BaoCao_PhanTich_TienAn_{0}_{1}_{2}", thang, nam, donVi.MaDonVi);
                        string fileNameWithoutExtension = StringUtils.CreateExportFileName(fileNamePrefix);
                        results.Add(new ExportResult(string.Format("{0}", "BaoCaoTienAn"), fileNameWithoutExtension, null, xlsFile));
                    }
                    e.Result = results;
                }, (s, e) =>
                {
                    if (e.Error == null)
                    {
                        var result = (List<ExportResult>)e.Result;
                        _exportService.Open(result, exportType);
                    }
                    else
                    {
                        _logger.Error(e.Error.Message);
                    }
                    IsLoading = false;
                });
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        private void ExportChiTraNganHangThuNhapKhac(ExportType exportType)
        {
            try
            {
                BackgroundWorkerHelper.Run((s, e) =>
                {
                    IsLoading = true;
                    List<ExportResult> results = new List<ExportResult>();
                    string templateFileName = Path.Combine(ExportPrefix.PATH_TL_LUONG_NEW, ExportFileName.RPT_TL_CHITRA_NGANHANG_THUNHAPKHAC_NEW);
                    int donViTinh = int.Parse(_selectedUnitType.ValueItem);
                    FormatNumber formatNumber = new FormatNumber(donViTinh, exportType);
                    var thang = int.Parse(SelectedMonth.ValueItem);
                    var nam = int.Parse(SelectedYear.ValueItem);
                    var lstDonViModel = ItemsDonVi.Where(x => x.IsSelected).ToList();
                    var lstDonVi = _mapper.Map<ObservableCollection<TlDmDonViNq104>>(lstDonViModel).ToList();

                    _tlBangLuongBridgeService.DataPreprocess(thang, nam, string.Join(",", lstDonVi.Select(x => x.MaDonVi)), null);
                    var dataAll = _tlBangLuongThangService.ReportChiTraNganHangThuNhapKhac(lstDonVi, thang, nam, true, IsOrderTheoChucVu);
                    dataAll.Columns.Add("STT", typeof(int));
                    foreach (var item in lstDonVi)
                    {
                        var dataRows = dataAll.AsEnumerable().Where(x => x.Field<string>("MaDonVi").Equals(item.MaDonVi));
                        var items = dataAll.Clone();
                        int i = 1;

                        if (!dataRows.Any())
                        {
                            continue;
                        }

                        foreach (DataRow item1 in dataRows)
                        {
                            item1["STT"] = i;
                            items.Rows.Add(item1.ItemArray);
                            i++;
                        }

                        Dictionary<string, object> data = new Dictionary<string, object>();
                        data.Add("Cap1", _iDanhMucService.FindDonViQuanLy(_sessionInfo.YearOfWork).ToUpper());
                        data.Add("Cap2", GetHeader2Report());
                        data.Add("FormatNumber", formatNumber);
                        data.Add("DonViTinh", "Đơn vị tính: " + (_selectedUnitType != null ? _selectedUnitType.DisplayItem : ""));
                        data.Add("Items", items);
                        data.Add("TieuDe", string.Format("Tháng: {0} - Năm: {1}", thang, nam));
                        data.Add("TenDonVi", item.TenDonVi.ToUpper());
                        data.Add("DonViCha", _sessionService?.Current?.TenDonViReportHeader);

                        data.Add("Unit", donViTinh);
                        data.Add("ReportName", ReportName);
                        AddChuKy(data, TypeChuky);

                        var xlsFile = _exportService.Export<TlDmCanBoKeHoachNq104Model>(templateFileName, data);
                        string fileNamePrefix = string.Format("rpt_Chi_Tra_Ngan_Hang_Thu_Nhap_Khac_{0}_Nam_{1}_{2}", thang, nam, item.TenDonVi);
                        string fileNameWithoutExtension = StringUtils.CreateExportFileName(fileNamePrefix);
                        results.Add(new ExportResult(string.Format("{0} - {1}", item.MaDonVi, item.TenDonVi), fileNameWithoutExtension, null, xlsFile));
                    }
                    e.Result = results;
                }, (s, e) =>
                {
                    if (e.Error == null)
                    {
                        var result = (List<ExportResult>)e.Result;
                        _exportService.Open(result, exportType);
                    }
                    else
                    {
                        _logger.Error(e.Error.Message);
                    }
                    IsLoading = false;
                });
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message);
            }
        }

        private void ExportChiTraNganHangThuNhapKhacIsummary(ExportType exportType)
        {
            try
            {
                BackgroundWorkerHelper.Run((s, e) =>
                {
                    IsLoading = true;
                    List<ExportResult> results = new List<ExportResult>();
                    string templateFileName = Path.Combine(ExportPrefix.PATH_TL_LUONG_NEW, ExportFileName.RPT_TL_CHITRA_NGANHANG_THUNHAPKHAC_NEW);
                    int donViTinh = int.Parse(_selectedUnitType.ValueItem);
                    FormatNumber formatNumber = new FormatNumber(donViTinh, exportType);
                    var thang = int.Parse(SelectedMonth.ValueItem);
                    var nam = int.Parse(SelectedYear.ValueItem);
                    var lstDonViModel = ItemsDonVi.Where(x => x.IsSelected).ToList();
                    var lstDonVi = _mapper.Map<ObservableCollection<TlDmDonViNq104>>(lstDonViModel).ToList();

                    _tlBangLuongBridgeService.DataPreprocess(thang, nam, string.Join(",", lstDonVi.Select(x => x.MaDonVi)), "CACH0");
                    var dataAll = _tlBangLuongThangService.ReportChiTraNganHangThuNhapKhac(lstDonVi, thang, nam, true, IsOrderTheoChucVu);
                    dataAll.Columns.Add("STT", typeof(int));
                    int i = 1;
                    foreach (DataRow item1 in dataAll.Rows)
                    {
                        item1["STT"] = i;
                        i++;
                    }

                    Dictionary<string, object> data = new Dictionary<string, object>();
                    data.Add("Cap1", _danhMucService.FindDonViQuanLy(_sessionInfo.YearOfWork).ToUpper());
                    data.Add("Cap2", GetHeader2Report());
                    data.Add("FormatNumber", formatNumber);
                    data.Add("DonViTinh", "Đơn vị tính: " + (_selectedUnitType != null ? _selectedUnitType.DisplayItem : ""));
                    data.Add("Items", dataAll);
                    data.Add("TieuDe", string.Format("Tháng: {0} - Năm: {1}", thang, nam));
                    data.Add("TenDonVi", string.Empty);
                    data.Add("DonViCha", _sessionService?.Current?.TenDonViReportHeader);

                    data.Add("Unit", donViTinh);
                    data.Add("ReportName", ReportName);
                    AddChuKy(data, TypeChuky);

                    var xlsFile = _exportService.Export<TlDmCanBoKeHoachNq104Model>(templateFileName, data);
                    string fileNamePrefix = string.Format("rpt_Tong_Hop_Chi_Tra_Ngan_Hang_Thu_Nhap_Khac_{0}_Nam_{1}", thang, nam);
                    string fileNameWithoutExtension = StringUtils.CreateExportFileName(fileNamePrefix);
                    results.Add(new ExportResult(string.Format("Tong_hop"), fileNameWithoutExtension, null, xlsFile));
                    e.Result = results;
                }, (s, e) =>
                {
                    if (e.Error == null)
                    {
                        var result = (List<ExportResult>)e.Result;
                        _exportService.Open(result, exportType);
                    }
                    else
                    {
                        _logger.Error(e.Error.Message);
                    }
                    IsLoading = false;
                });
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message);
            }
        }


        private void ExportGiaiThichRaQuanXuatNgu(ExportType exportType)
        {
            try
            {
                BackgroundWorkerHelper.Run((s, e) =>
                {
                    IsLoading = true;

                    List<ExportResult> results = new List<ExportResult>();
                    string templateFileName = Path.Combine(ExportPrefix.PATH_TL_LUONG_NEW, ExportFileName.RPT_TL_GIAITHICH_RAQUAN_XUATNGU_NEW);

                    int donViTinh = int.Parse(_selectedUnitType.ValueItem);
                    var nam = int.Parse(SelectedYear.ValueItem);
                    var thang = int.Parse(SelectedMonth.ValueItem);
                    FormatNumber formatNumber = new FormatNumber(donViTinh, exportType);

                    var lstDonVi = ItemsDonVi.Where(x => x.IsSelected);
                    var listDonViSelect = _mapper.Map<List<TlDmDonViNq104>>(lstDonVi);
                    string donViCha = _sessionService?.Current?.TenDonViReportHeader;
                    string sMaDonVi = string.Join(",", listDonViSelect.Select(x => x.MaDonVi));
                    _tlBangLuongBridgeService.DataPreprocess(thang, nam, sMaDonVi, null);
                    DataTable dataAll = _tlBangLuongThangService.GetDataRaQuanXuatNgu(listDonViSelect, thang, nam, donViTinh);

                    foreach (var item in listDonViSelect)
                    {
                        var maDonVi = item.MaDonVi;
                        string tenDonVi = string.Empty;
                        if (IsChecked)
                        {
                            tenDonVi = item.TenDonVi;
                        }

                        var dataRows = dataAll.AsEnumerable().Where(x => x.Field<string>("MaDonVi").Equals(item.MaDonVi));

                        if (!dataRows.Any())
                        {
                            continue;
                        }

                        var items = dataAll.Clone();
                        var index = 1;
                        foreach (DataRow item1 in dataRows)
                        {
                            item1["STT"] = index++;
                            items.Rows.Add(item1.ItemArray);
                        }

                        Dictionary<string, object> data = new Dictionary<string, object>();
                        data.Add("Cap1", _danhMucService.FindDonViQuanLy(_sessionInfo.YearOfWork).ToUpper());
                        data.Add("Cap2", GetHeader2Report());
                        data.Add("FormatNumber", formatNumber);
                        data.Add("DonViTinh", "Đơn vị tính: " + (_selectedUnitType != null ? _selectedUnitType.DisplayItem : ""));
                        data.Add("TieuDe2", string.Format("Tháng {0} năm {1}", thang, nam));
                        data.Add("TenDonVi", tenDonVi.ToUpper());
                        data.Add("DonViCha", donViCha);

                        data.Add("Unit", donViTinh);
                        data.Add("Items", items);
                        data.Add("ReportName", ReportName);
                        AddChuKy(data, TypeChuky);

                        var xlsFile = _exportService.Export(templateFileName, data);
                        string fileNamePrefix = string.Format("rpt_Giai_Thich_Ra_Quan_Xuat_Ngu_{0}_Nam_{1}_{2}", thang, nam, tenDonVi);
                        string fileNameWithoutExtension = StringUtils.CreateExportFileName(fileNamePrefix);
                        results.Add(new ExportResult(string.Format("{0} - {1}", item.MaDonVi, item.TenDonVi), fileNameWithoutExtension, null, xlsFile));
                    }
                    e.Result = results;
                }, (s, e) =>
                {
                    if (e.Error == null)
                    {
                        var result = (List<ExportResult>)e.Result;
                        _exportService.Open(result, exportType);
                    }
                    else
                    {
                        _logger.Error(e.Error.Message);
                    }
                    IsLoading = false;
                });
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        private void ExportBangLuongTruyLinhNhieuQuyetDinh(ExportType exportType)
        {
            try
            {
                BackgroundWorkerHelper.Run((s, e) =>
                {
                    IsLoading = true;

                    List<ExportResult> results = new List<ExportResult>();
                    string templateFileName = Path.Combine(ExportPrefix.PATH_TL_LUONG_NEW, ExportFileName.RPT_TL_LUONG_TRUY_LINH_NHIEU_QD_NEW);

                    int donViTinh = int.Parse(_selectedUnitType.ValueItem);
                    FormatNumber formatNumber = new FormatNumber(donViTinh, exportType);

                    var thang = int.Parse(SelectedMonth.ValueItem);
                    var nam = int.Parse(SelectedYear.ValueItem);
                    var lstDonVi = ItemsDonVi.Where(x => x.IsSelected);
                    var listDonViSelect = _mapper.Map<List<TlDmDonViNq104>>(lstDonVi);
                    var data = _tlBangLuongThangService.GetDataBangLuongTruyLinh(listDonViSelect, thang, nam, true, true);
                    string donViCha = _sessionService?.Current?.TenDonViReportHeader;
                    foreach (var item in listDonViSelect)
                    {
                        var maDonVi = item.MaDonVi;
                        var tenDonVi = item.TenDonVi;
                        var items = _tlBangLuongThangService.ReportBangLuongTruyLinh(item, data, donViTinh);

                        Dictionary<string, object> map = new Dictionary<string, object>();
                        map.Add("Cap1", _danhMucService.FindDonViQuanLy(_sessionInfo.YearOfWork).ToUpper());
                        map.Add("Cap2", GetHeader2Report());
                        map.Add("FormatNumber", formatNumber);
                        map.Add("Unit", donViTinh);
                        map.Add("DonViTinh", "Đơn vị tính: " + (_selectedUnitType != null ? _selectedUnitType.DisplayItem : ""));
                        map.Add("TieuDe2", string.Format("Tháng: {0} - Năm: {1}", thang, nam));
                        map.Add("TenDonVi", item.TenDonVi.ToUpper());
                        map.Add("DonViCha", donViCha);
                        map.Add("Items", items);
                        map.Add("ReportName", ReportName);
                        AddChuKy(map, TypeChuky);

                        var xlsFile = _exportService.Export<TlDmCanBoKeHoachNq104Model>(templateFileName, map);
                        string fileNamePrefix = string.Format("rpt_Bang_Luong_Thang_Truy_Linh_Nhieu_QuyetDinh_{0}_Nam_{1}_{2}", thang, nam, maDonVi);
                        string fileNameWithoutExtension = StringUtils.CreateExportFileName(fileNamePrefix);
                        results.Add(new ExportResult(string.Format("{0} - {1}", maDonVi, tenDonVi), fileNameWithoutExtension, null, xlsFile));
                    }
                    e.Result = results;
                }, (s, e) =>
                {
                    IsLoading = false;

                    if (e.Error == null)
                    {
                        var result = (List<ExportResult>)e.Result;
                        _exportService.Open(result, exportType);
                    }
                    else
                    {
                        _logger.Error(e.Error.Message);
                    }
                });
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        private void ExportChiTietQuanSoTangGiam(ExportType exportType)
        {
            try
            {
                BackgroundWorkerHelper.Run((s, e) =>
                {
                    IsLoading = true;

                    List<ExportResult> results = new List<ExportResult>();
                    string templateFileName = Path.Combine(ExportPrefix.PATH_TL_LUONG_NEW, ExportFileName.RPT_TL_CHITIET_QUANSO_TANGGIAM_NEW);

                    var thang = int.Parse(SelectedMonth.ValueItem);
                    var nam = int.Parse(SelectedYear.ValueItem);
                    var lstDonVi = ItemsDonVi.Where(x => x.IsSelected);
                    string donViCha = _sessionService?.Current?.TenDonViReportHeader;
                    var lstStrDonVi = lstDonVi.Select(x => x.MaDonVi);

                    // quan so tang
                    var itemsTang = _tlDmCanboService.ReportChiTietQsTangGiam(string.Join(",", lstStrDonVi), thang, nam, "2");
                    // quan so giam
                    var itemsGiam = _tlDmCanboService.ReportChiTietQsTangGiam(string.Join(",", lstStrDonVi), thang, nam, "3");

                    Dictionary<string, object> map = new Dictionary<string, object>();
                    map.Add("Cap1", _danhMucService.FindDonViQuanLy(_sessionInfo.YearOfWork).ToUpper());
                    map.Add("Cap2", GetHeader2Report());
                    map.Add("TieuDe2", string.Format("Tháng: {0} - Năm: {1}", thang, nam));
                    map.Add("TieuDe1", "GIẢI THÍCH QUÂN SỐ TĂNG/GIẢM");
                    map.Add("DonViCha", donViCha);
                    map.Add("ItemsTang", itemsTang);
                    map.Add("ItemsGiam", itemsGiam);
                    map.Add("sum", SelectedLoaiQuanSo.DisplayItem);
                    AddChuKy(map, TypeChuky);

                    var xlsFile = _exportService.Export<TlDmCanBoKeHoachNq104Model>(templateFileName, map);
                    string fileNamePrefix = string.Format("rpt_GiaiThich_QuanSo_{0}_Nam_{1}", thang, nam);
                    string fileNameWithoutExtension = StringUtils.CreateExportFileName(fileNamePrefix);
                    results.Add(new ExportResult(string.Format("{0} - {1}", "Tổng hợp", "Tổng hợp"), fileNameWithoutExtension, null, xlsFile));

                    e.Result = results;
                }, (s, e) =>
                {
                    IsLoading = false;

                    if (e.Error == null)
                    {
                        var result = (List<ExportResult>)e.Result;
                        _exportService.Open(result, exportType);
                    }
                    else
                    {
                        _logger.Error(e.Error.Message);
                    }
                });
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        private void ExportGiaiThichPhuCapBienPhong(ExportType exportType)
        {
            try
            {
                BackgroundWorkerHelper.Run((s, e) =>
                {
                    IsLoading = true;
                    List<ExportResult> results = new List<ExportResult>();
                    int donViTinh = int.Parse(_selectedUnitType.ValueItem);
                    FormatNumber formatNumber = new FormatNumber(donViTinh, exportType);
                    var thang = int.Parse(SelectedMonth.ValueItem);
                    var nam = int.Parse(SelectedYear.ValueItem);
                    var lstDonVi = ItemsDonVi.Where(x => x.IsSelected).ToList();
                    var maDonVi = string.Join(",", lstDonVi.Select(x => x.MaDonVi));
                    var maPhuCap = SelectedYeuToluong.MaPhuCap;

                    _tlBangLuongBridgeService.DataPreprocess(thang, nam, string.Join(",", lstDonVi.Select(x => x.MaDonVi)), CachTinhLuong.CACH0);
                    var lstTlBangLuongThangDong = _tlBangLuongThangService.ReportGiaiThichBienPhong(maDonVi, nam, thang, donViTinh, maPhuCap);
                    if (lstTlBangLuongThangDong == null || lstTlBangLuongThangDong.Rows.Count == 0)
                    {
                        e.Result = results;
                        return;
                    }
                    Dictionary<string, object> data = new Dictionary<string, object>();
                    data.Add("Cap1", _danhMucService.FindDonViQuanLy(_sessionInfo.YearOfWork).ToUpper());
                    data.Add("Cap2", GetHeader2Report());
                    data.Add("FormatNumber", formatNumber);
                    data.Add("TieuDe", string.Format("Tháng {0} năm {1}", thang, nam));
                    data.Add("TenPhuCap", SelectedYeuToluong.TenPhuCap.ToUpper());
                    data.Add("DonViCha", _sessionService?.Current?.TenDonViReportHeader);
                    data.Add("TenDonVi", string.Empty);
                    data.Add("Items", lstTlBangLuongThangDong);
                    AddChuKy(data, TypeChuky);

                    string templateFileName = Path.Combine(ExportPrefix.PATH_TL_LUONG_NEW, ExportFileName.RPT_TL_GIAITHICH_BIENPHONG_NEW);
                    var xlsFile = _exportService.Export<TlRptBangLuongThangDong>(templateFileName, data);
                    string fileNamePrefix = string.Format("rpt_Bang_Giai_Thich_Chi_Tiet_Bien_Phong_{0}_Nam_{1}_{2}", thang, nam, "Tổng hợp");
                    string fileNameWithoutExtension = StringUtils.CreateExportFileName(fileNamePrefix);
                    results.Add(new ExportResult(string.Format("{0} - {1}", "Tổng hợp", "Tổng hợp"), fileNameWithoutExtension, null, xlsFile));

                    e.Result = results;
                }, (s, e) =>
                {
                    IsLoading = false;

                    if (e.Error == null)
                    {
                        var result = (List<ExportResult>)e.Result;
                        if (!result.IsEmpty())
                        {
                            _exportService.Open(result, exportType);
                        }
                        else
                        {
                            MessageBoxHelper.Info("Không có dữ liệu.");
                        }
                    }
                    else
                    {
                        _logger.Error(e.Error.Message);
                        MessageBoxHelper.Info(e.Error.Message);
                    }
                });
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        private void ExportGiaiThichPhuCapBienPhongTheoHeSo(ExportType exportType, bool IsSummary)
        {
            try
            {
                BackgroundWorkerHelper.Run((s, e) =>
                {
                    IsLoading = true;
                    List<ExportResult> results = new List<ExportResult>();
                    int donViTinh = int.Parse(_selectedUnitType.ValueItem);
                    FormatNumber formatNumber = new FormatNumber(donViTinh, exportType);
                    var thang = int.Parse(SelectedMonth.ValueItem);
                    var nam = int.Parse(SelectedYear.ValueItem);
                    var lstDonVi = ItemsDonVi.Where(x => x.IsSelected).ToList();
                    var maDonVi = string.Join(",", lstDonVi.Select(x => x.MaDonVi));
                    var maPhuCap = SelectedYeuToluong.MaPhuCap;
                    var splitPhuCap = maPhuCap.Split("_");

                    _tlBangLuongBridgeService.DataPreprocess(thang, nam, string.Join(",", lstDonVi.Select(x => x.MaDonVi)), CachTinhLuong.CACH0);
                    var lstTlBangLuongThangDong = _tlBangLuongThangService.ReportGiaiThichBienPhongTheoHeSo(maDonVi, nam, thang, donViTinh, maPhuCap, string.Format("{0}{1}", splitPhuCap.FirstOrDefault(), "_TT"));
                    if (lstTlBangLuongThangDong == null || lstTlBangLuongThangDong.Rows.Count == 0)
                    {
                        e.Result = results;
                        return;
                    }
                    lstTlBangLuongThangDong.Columns.Add("STT");
                    lstTlBangLuongThangDong.Columns.Add("IsHeader", typeof(bool));

                    if (IsSummary)
                    {
                        var items = lstTlBangLuongThangDong.Clone();
                        var index = 1;

                        foreach (var item in lstDonVi)
                        {
                            var dtr = lstTlBangLuongThangDong.AsEnumerable().Where(x => x.Field<string>("MaDonVi").Equals(item.MaDonVi));
                            if (dtr == null || dtr.Count() == 0)
                            {
                                return;
                            }

                            DataRow parentRow = items.NewRow();
                            parentRow["MaDonVi"] = item.TenDonVi;
                            parentRow["IsHeader"] = true;
                            items.Rows.Add(parentRow);

                            foreach (DataRow dataRow in dtr)
                            {
                                dataRow["STT"] = index++;
                                items.Rows.Add(dataRow.ItemArray);
                            }
                        }

                        Dictionary<string, object> data = new Dictionary<string, object>();
                        data.Add("Cap1", _danhMucService.FindDonViQuanLy(_sessionInfo.YearOfWork).ToUpper());
                        data.Add("Cap2", GetHeader2Report());
                        data.Add("FormatNumber", formatNumber);
                        data.Add("TieuDe", string.Format("Tháng {0} năm {1}", thang, nam));
                        data.Add("TenPhuCap", SelectedYeuToluong.TenPhuCap.ToUpper());
                        data.Add("DonViCha", _sessionService?.Current?.TenDonViReportHeader);
                        data.Add("TenDonVi", string.Empty);
                        data.Add("Items", items);
                        AddChuKy(data, TypeChuky);

                        string templateFileName = Path.Combine(ExportPrefix.PATH_TL_LUONG_NEW, ExportFileName.RPT_TL_GIAITHICH_BIENPHONG_HESO_NEW);
                        var xlsFile = _exportService.Export<TlRptBangLuongThangDong>(templateFileName, data);
                        string fileNamePrefix = string.Format("rpt_Bang_Giai_Thich_Chi_Tiet_Bien_Phong_{0}_Nam_{1}", thang, nam);
                        string fileNameWithoutExtension = StringUtils.CreateExportFileName(fileNamePrefix);
                        results.Add(new ExportResult(string.Format("Tổng hợp"), fileNameWithoutExtension, null, xlsFile));
                    }
                    else
                    {
                        foreach (var item in lstDonVi)
                        {
                            var dtr = lstTlBangLuongThangDong.AsEnumerable().Where(x => x.Field<string>("MaDonVi").Equals(item.MaDonVi));
                            if (dtr == null || dtr.Count() == 0)
                            {
                                continue;
                            }

                            var items = lstTlBangLuongThangDong.Clone();
                            var index = 1;
                            foreach (DataRow dataRow in dtr)
                            {
                                dataRow["STT"] = index++;
                                items.Rows.Add(dataRow.ItemArray);
                            }

                            Dictionary<string, object> data = new Dictionary<string, object>();
                            data.Add("Cap1", _danhMucService.FindDonViQuanLy(_sessionInfo.YearOfWork).ToUpper());
                            data.Add("Cap2", GetHeader2Report());
                            data.Add("FormatNumber", formatNumber);
                            data.Add("TieuDe", string.Format("Tháng {0} năm {1}", thang, nam));
                            data.Add("TenPhuCap", SelectedYeuToluong.TenPhuCap.ToUpper());
                            data.Add("DonViCha", _sessionService?.Current?.TenDonViReportHeader);
                            data.Add("TenDonVi", item.TenDonVi);
                            data.Add("Items", items);
                            AddChuKy(data, TypeChuky);

                            string templateFileName = Path.Combine(ExportPrefix.PATH_TL_LUONG_NEW, ExportFileName.RPT_TL_GIAITHICH_BIENPHONG_HESO_NEW);
                            var xlsFile = _exportService.Export<TlRptBangLuongThangDong>(templateFileName, data);
                            string fileNamePrefix = string.Format("rpt_Bang_Giai_Thich_Chi_Tiet_Bien_Phong_{0}_Nam_{1}_{2}", thang, nam, item.TenDonVi);
                            string fileNameWithoutExtension = StringUtils.CreateExportFileName(fileNamePrefix);
                            results.Add(new ExportResult(string.Format("{0} - {1}", item.MaDonVi, item.TenDonVi), fileNameWithoutExtension, null, xlsFile));
                        }
                    }

                    e.Result = results;
                }, (s, e) =>
                {
                    IsLoading = false;

                    if (e.Error == null)
                    {
                        var result = (List<ExportResult>)e.Result;
                        if (!result.IsEmpty())
                        {
                            _exportService.Open(result, exportType);
                        }
                        else
                        {
                            MessageBoxHelper.Info("Không có dữ liệu.");
                        }
                    }
                    else
                    {
                        _logger.Error(e.Error.Message);
                        MessageBoxHelper.Info(e.Error.Message);
                    }
                });
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        public static IEnumerable<List<T>> SplitList<T>(List<T> bigList, int nSize = 3)
        {
            for (int i = 0; i < bigList.Count; i += nSize)
            {
                yield return bigList.GetRange(i, Math.Min(nSize, bigList.Count - i));
            }
        }

        private string GetExcelColumnName(int columnNumber)
        {
            string columnName = "";

            while (columnNumber > 0)
            {
                int modulo = (columnNumber - 1) % 26;
                columnName = Convert.ToChar('A' + modulo) + columnName;
                columnNumber = (columnNumber - modulo) / 26;
            }
            return columnName;
        }

        private string GetMessageValidate()
        {
            IList<string> messages = new List<string>();

            var lstDonVi = ItemsDonVi.Where(x => x.IsSelected);
            if (lstDonVi.IsEmpty())
            {
                messages.Add(string.Format(Resources.UnitNull));
                goto End;
            }
            if (SelectedFileExport == null)
            {
                messages.Add(string.Format(Resources.FileOutputTypeNull));
                goto End;
            }

        End:
            return string.Join(Environment.NewLine, messages);
        }

        private void OnOpenCauHinhChuKyDialog()
        {
            try
            {
                string idTypeBc = TypeChuky;
                if (string.IsNullOrEmpty(idTypeBc))
                {
                    idTypeBc = TypeChuky = GetMaBaoCaoNotFound(Model.MaBaoCao);
                }
                var dmChuKy = _iDmChuKyService.FindByCondition(x => x.IdType.Equals(idTypeBc)).FirstOrDefault();
                DmChuKyDialogViewModel dmChuKyDialogViewModel = new DmChuKyDialogViewModel(_mapper, _serviceProvider, _sessionService);
                dmChuKyDialogViewModel.DmChuKyModel =
                    dmChuKy != null ? _mapper.Map<DmChuKyModel>(dmChuKy) : new DmChuKyModel()
                    {
                        IdType = idTypeBc,
                        IdCode = "xx"
                    };
                dmChuKyDialogViewModel.HasAddedSign4 = IsQtQuanSoMau2 || LoaiBaoCao.Equals(BaoCaoLuong.DSCPL_A4);
                dmChuKyDialogViewModel.IsLuong = true;
                dmChuKyDialogViewModel.BaoCaoLuongModel = Model;
                dmChuKyDialogViewModel.Init();
                dmChuKyDialogViewModel.ShowDialog();
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        public string GetIdTypeBaoCao()
        {
            switch (LoaiBaoCao)
            {
                case BaoCaoLuong.DSCPL_A4:
                    return TypeChuKy.RPT_TL_LUONG_THANG_NEW;
                case BaoCaoLuong.GTCTTL:
                    return TypeChuKy.RPT_TL_GIAI_THICH_CHI_TIET_LUONG_NEW;
                case BaoCaoLuong.GTCTPC_TX:
                case BaoCaoLuong.GTCTPC_NV:
                case BaoCaoLuong.GTCTPC_TNK:
                case BaoCaoLuong.GTCTPC_GTK:
                    return TypeChuKy.RPT_TL_GIAITHICH_CHITIET_PHUCAP_KHAC_NEW;
                case BaoCaoLuong.GTPCTN:
                    return TypeChuKy.RPT_TL_GIAITHICH_PHUCAP_THEONGAY_NEW;
                case BaoCaoLuong.TTNCN:
                    return TypeChuKy.RPT_TL_BANGKE_TRICHTHUETNCN_NEW;
                case BaoCaoLuong.QTTTNCN:
                    return TypeChuKy.RPT_TL_QUYET_TOAN_TTNCN_NEW;
                case BaoCaoLuong.BTLTL:
                    return TypeChuKy.RPT_TL_LUONG_TRUY_LINH_NEW;
                case BaoCaoLuong.DSCTCN:
                    return TypeChuKy.RPT_TL_DANHSACH_CHITRA_LUONGCN_NEW;
                case BaoCaoLuong.DSCTCN_CT:
                    return TypeChuKy.RPT_TL_DS_TRA_NGAN_HANG_NEW;
                case BaoCaoLuong.THLPC:
                    return TypeChuKy.RPT_TL_TONG_HOP_LUONG_PHU_CAP_THEO_NGACH_NEW;
                case BaoCaoLuong.THLPC_DV:
                    return TypeChuKy.RPT_TL_TONGHOP_LUONG_PHUCAP_DONVI_NEW;
                case BaoCaoLuong.THLPC_N_DV:
                    return TypeChuKy.RPT_TL_TONGHOP_LUONG_NGACHDONVI_NEW;
                case BaoCaoLuong.THQSQT:
                    return TypeChuKy.RPT_TL_QUYETTOAN_QUANSO_NEW;
                case BaoCaoLuong.GTCTPCTNVKTHD:
                    return TypeChuKy.RPT_TL_GIAITHICH_CHITIET_PHUCAPTNVKTHD_NEW;
                case BaoCaoLuong.DCQSNKH:
                    return TypeChuKy.RPT_TL_DIEUCHINH_QUANSO_KEHOACH_NEW;
                case BaoCaoLuong.CTQSRQ:
                    return TypeChuKy.RPT_TL_CHITIET_QS_RAQUAN_NEW;
                case BaoCaoLuong.CTQSNH:
                    return TypeChuKy.RPT_TL_CHITIET_QS_NGHIHUU_NEW;
                case BaoCaoLuong.BDQHKE:
                    return TypeChuKy.RPT_TL_BIENDONG_QUANHAM_KEHOACH_NEW;
                case BaoCaoLuong.LKHMLNS:
                    return TypeChuKy.RPT_TL_QUYETTOAN_LUONG_NAM_KH_NEW;
                default:
                    return "";
            }
        }

        public void AddChuKy(Dictionary<string, object> data, string idType)
        {
            //add ngày địa điểm
            data.Add("Ngay", GetNgayThangNamBaoCao());
            data.Add("DiaDiem", _diaDiem);
            // add chữ ký
            var dmChyKy = _iDmChuKyService.FindByCondition(x => x.IdType.Equals(idType) && x.ITrangThai == StatusType.ACTIVE).FirstOrDefault();
            //if (dmChyKy != null && !string.IsNullOrEmpty(dmChyKy.ThuaLenh1MoTa))
            if (dmChyKy != null)
            {
                data.Add("ThuaLenh1", dmChyKy.ThuaLenh1MoTa);
                data.Add("ChucDanh1", dmChyKy.ChucDanh1MoTa);
                //data.Add("GhiChuKy1", "(Ký, họ tên)");
                data.Add("Ten1", dmChyKy.Ten1MoTa);
            }
            //if (dmChyKy != null && !string.IsNullOrEmpty(dmChyKy.ThuaLenh2MoTa))
            if (dmChyKy != null)
            {
                data.Add("ThuaLenh2", dmChyKy.ThuaLenh2MoTa);
                data.Add("ChucDanh2", dmChyKy.ChucDanh2MoTa);
                //data.Add("GhiChuKy2", "(Ký, họ tên)");
                data.Add("Ten2", dmChyKy.Ten2MoTa);
            }
            if (!IsQtQuanSoMau2)
            {
                if (dmChyKy != null)
                //if (dmChyKy != null && !string.IsNullOrEmpty(dmChyKy.ThuaLenh3MoTa))
                {
                    data.Add("ThuaLenh3", dmChyKy.ThuaLenh3MoTa);
                    data.Add("ChucDanh3", dmChyKy.ChucDanh3MoTa);
                    //data.Add("GhiChuKy3", "(Ký, họ tên, đóng dấu)");
                    data.Add("Ten3", dmChyKy.Ten3MoTa);
                }
                if (dmChyKy != null)
                //if (dmChyKy != null && !string.IsNullOrEmpty(dmChyKy.ThuaLenh4MoTa))
                {
                    data.Add("ThuaLenh4", dmChyKy.ThuaLenh4MoTa);
                    data.Add("ChucDanh4", dmChyKy.ChucDanh4MoTa);
                    //data.Add("GhiChuKy4", "(Ký, họ tên, đóng dấu)");
                    data.Add("Ten4", dmChyKy.Ten4MoTa);
                }
                if (dmChyKy != null && (!dmChyKy.ThuaLenh4MoTa.IsEmpty() || !dmChyKy.ChucDanh4MoTa.IsEmpty() || !dmChyKy.Ten4MoTa.IsEmpty()))
                {
                    data.Add("Co4ChuKy", true);
                }
            }
            else
            {
                if (dmChyKy != null)
                //if (dmChyKy != null && !string.IsNullOrEmpty(dmChyKy.ThuaLenh3MoTa))
                {
                    data.Add("ThuaLenh3", dmChyKy.ThuaLenh3MoTa);
                    data.Add("ChucDanh3", dmChyKy.ChucDanh3MoTa);
                    //data.Add("GhiChuKy3", "(Ký, họ tên)");
                    data.Add("Ten3", dmChyKy.Ten3MoTa);
                }
                if (dmChyKy != null)
                //if (dmChyKy != null && !string.IsNullOrEmpty(dmChyKy.ThuaLenh4MoTa))
                {
                    data.Add("ThuaLenh4", dmChyKy.ThuaLenh4MoTa);
                    data.Add("ChucDanh4", dmChyKy.ChucDanh4MoTa);
                    //data.Add("GhiChuKy4", "(Ký, họ tên, đóng dấu)");
                    data.Add("Ten4", dmChyKy.Ten4MoTa);
                }
            }
        }

        private void UpdateAppSetting()
        {
            List<ReportLuongTruyLinhSetting> setting = new List<ReportLuongTruyLinhSetting>();
            if (ItemsPhuCapTongLuong != null && ItemsPhuCapTongLuong.Count > 0)
            {
                ReportLuongTruyLinhSetting st = new ReportLuongTruyLinhSetting();
                st.LoaiPhuCap = LOAI_PHU_CAP.TONG_CONG;
                st.LstMaPhuCap = ItemsPhuCapTongLuong.Where(x => x.IsSelected).Select(x => x.MaPhuCap).ToList();
                setting.Add(st);
            }
            if (ItemsPhuCapPhaiTru != null && ItemsPhuCapPhaiTru.Count > 0)
            {
                ReportLuongTruyLinhSetting st = new ReportLuongTruyLinhSetting();
                st.LoaiPhuCap = LOAI_PHU_CAP.PHAI_TRU;
                st.LstMaPhuCap = ItemsPhuCapPhaiTru.Where(x => x.IsSelected).Select(x => x.MaPhuCap).ToList();
                setting.Add(st);
            }
            ConfigHelper.UpdateSetting<List<ReportLuongTruyLinhSetting>>(_appSettingConfigPath, setting);
        }

        private void LoadAppSetting()
        {
            List<ReportLuongTruyLinhSetting> lstPhuCapSelected = Helper.ConfigHelper.ReadSetting<List<ReportLuongTruyLinhSetting>>(_appSettingConfigPath);
            if (lstPhuCapSelected != null && lstPhuCapSelected.Count > 0)
            {
                foreach (var it in lstPhuCapSelected)
                {
                    if (it.LoaiPhuCap.Equals(LOAI_PHU_CAP.TONG_CONG) && it.LstMaPhuCap.Count > 0 && ItemsPhuCapTongLuong != null)
                    {
                        foreach (var pc in ItemsPhuCapTongLuong)
                        {
                            if (it.LstMaPhuCap.Contains(pc.MaPhuCap))
                            {
                                pc.IsSelected = true;
                            }
                        }
                    }
                    if (it.LoaiPhuCap.Equals(LOAI_PHU_CAP.PHAI_TRU) && it.LstMaPhuCap.Count > 0 && ItemsPhuCapPhaiTru != null)
                    {
                        foreach (var pc in ItemsPhuCapPhaiTru)
                        {
                            if (it.LstMaPhuCap.Contains(pc.MaPhuCap))
                            {
                                pc.IsSelected = true;
                            }
                        }
                    }
                }
            }
        }

        public string GetNgayThangNamBaoCao()
        {
            if (IsInNgayThang)
            {
                return DateUtils.GetCurrentDateReport();
            }

            return "Ngày    tháng    năm     ";
        }

        public string GetHeader2Report()
        {
            DonVi donViParent = _donViService.FindByLoai(LoaiDonVi.ROOT, _sessionInfo.YearOfWork);
            return donViParent != null ? donViParent.TenDonVi.ToUpper() : string.Empty;
        }

        private string GetMaBaoCaoNotFound(string sMaBaoCao)
        {
            switch (sMaBaoCao)
            {
                case "15.1":
                    return "rptLuong_QS_ChungTu";
                default:
                    return string.Empty;
            }
        }
    }
}
