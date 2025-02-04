using AutoMapper;
using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows;
using VTS.QLNS.CTC.App.Command;
using VTS.QLNS.CTC.App.Service;
using VTS.QLNS.CTC.App.Model;
using VTS.QLNS.CTC.App.Model.Control;
using VTS.QLNS.CTC.App.Properties;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Service;
using VTS.QLNS.CTC.Utility.Enum;
using static VTS.QLNS.CTC.Utility.Enum.MediumTermPlan;
using log4net;
using VTS.QLNS.CTC.Utility;
using VTS.QLNS.CTC.Core.Domain;
using System.Linq.Expressions;
using VTS.QLNS.CTC.App.Service.UserFunction;
using FlexCel.Core;
using System.IO;
using VTS.QLNS.CTC.App.Helper;
using VTS.QLNS.CTC.App.Service.Impl;
using VTS.QLNS.CTC.App.ViewModel.Category;
using VTS.QLNS.CTC.App.ViewModel.Investment.MediumTermPlan.PrintReport;

namespace VTS.QLNS.CTC.App.ViewModel.Investment.MediumTermPlan.PrintReport
{
    public class PrintReportMediumTermPlanViewModel : ViewModelBase
    {
        #region Private
        private static string[] lstDonViExclude = new string[] { "0", "1" };
        private readonly ISessionService _sessionService;
        private readonly INsNguonNganSachService _nguonNganSachService;
        private readonly IVdtKhvKeHoach5NamChiTietService _vdtKhvKeHoach5NamChiTietService;
        private readonly IVdtKhvKeHoach5NamDeXuatChiTietService _vdtKhvKeHoach5NamChiTietDexuatService;
        private readonly IVdtKhvKeHoach5NamService _vdtKhvKeHoach5NamService;
        private readonly IVdtKhvKeHoach5NamDeXuatService _vdtKhvKeHoach5NamDeXuat;
        private readonly IVdtDmDonViThucHienDuAnService _vdtDmDonViThucHienDuAnService;
        private readonly IDmLoaiCongTrinhService _loaiCongTrinhService;
        private readonly INsDonViService _nsDonViService;
        private readonly IExportService _exportService;
        private readonly IMapper _mapper;
        private readonly IVdtDmLoaiCongTrinhService _vdtDmLoaiCongTrinhService;
        private readonly IDmChuKyService _dmChuKyService;
        private readonly ILog _logger;
        private DmChuKyDialogViewModel DmChuKyDialogViewModel;
        private DmChuKy _dmChuKy;
        private const string RPT_TONGHOP = "1";
        private const string RPT_DONVI = "2";
        private const string RPT_GOC = "1";
        private const string RPT_DIEUCHINH = "2";
        #endregion

        #region Item
        public ReportMediumType ReportMediumTypes { get; set; }
        public override string Title
        {
            get
            {
                string sTitle = string.Empty;
                if (ReportMediumTypes.Equals(ReportMediumType.SUGGESTION_CTMM))
                {
                    sTitle = "In báo cáo kế hoạch trung hạn đề xuất công trình mở mới";
                }
                else if (ReportMediumTypes.Equals(ReportMediumType.SUGGESTION_CTCT))
                {
                    sTitle = "In báo cáo kế hoạch trung hạn đề xuất công trình chuyển tiếp";
                }
                else if (ReportMediumTypes.Equals(ReportMediumType.APPROVED_CTMM))
                {
                    sTitle = "In báo cáo kế hoạch trung hạn được duyệt công trình mở mới";
                }
                else
                {
                    sTitle = "In báo cáo kế hoạch trung hạn được duyệt công trình chuyển tiếp";
                }

                return sTitle;
            }
        }

        private ObservableCollection<ComboboxItem> _drpVoucherTypes;
        public ObservableCollection<ComboboxItem> DrpVoucherTypes
        {
            get => _drpVoucherTypes;
            set => SetProperty(ref _drpVoucherTypes, value);
        }

        private ComboboxItem _drpVoucherTypeSelected;
        public ComboboxItem DrpVoucherTypeSelected
        {
            get => _drpVoucherTypeSelected;
            set
            {
                SetProperty(ref _drpVoucherTypeSelected, value);
                OnPropertyChanged(nameof(LoaiCongTrinhVisibility));

                LoadHeader();
                LoadVouchers();
            }
        }

        private ObservableCollection<VdtKhvKeHoach5NamReportQuery> _itemsReport;
        public ObservableCollection<VdtKhvKeHoach5NamReportQuery> ItemsReport
        {
            get => _itemsReport;
            set => SetProperty(ref _itemsReport, value);
        }

        private ObservableCollection<VdtKhvKeHoach5NamChuyenTiepReportQuery> _itemsReportChuyenTiep;
        public ObservableCollection<VdtKhvKeHoach5NamChuyenTiepReportQuery> ItemsReportChuyenTiep
        {
            get => _itemsReportChuyenTiep;
            set => SetProperty(ref _itemsReportChuyenTiep, value);
        }

        private ObservableCollection<VdtKhvKeHoach5NamDeXuatReportQuery> _itemsReportDeXuat;
        public ObservableCollection<VdtKhvKeHoach5NamDeXuatReportQuery> ItemsReportDeXuat
        {
            get => _itemsReportDeXuat;
            set => SetProperty(ref _itemsReportDeXuat, value);
        }

        private ObservableCollection<VdtKhvKeHoach5NamDeXuatDieuChinhReportQuery> _itemsReportDeXuatDieuChinh;
        public ObservableCollection<VdtKhvKeHoach5NamDeXuatDieuChinhReportQuery> ItemsReportDeXuatDieuChinh
        {
            get => _itemsReportDeXuatDieuChinh;
            set => SetProperty(ref _itemsReportDeXuatDieuChinh, value);
        }

        private ObservableCollection<CheckBoxItem> _dataUnit;
        public ObservableCollection<CheckBoxItem> DataUnit
        {
            get => _dataUnit;
            set => SetProperty(ref _dataUnit, value);
        }

        private string _sNamBatDau;
        public string SNamBatDau
        {
            get => _sNamBatDau;
            set
            {
                SetProperty(ref _sNamBatDau, value);
                _iNamKetThuc = !string.IsNullOrEmpty(_sNamBatDau) ? Int32.Parse(_sNamBatDau) + 4 : 0;
                OnPropertyChanged(nameof(INamKetThuc));
                LoadVouchers();
            }
        }

        private int _iNamKetThuc;
        public int INamKetThuc
        {
            get => _iNamKetThuc;
            set => SetProperty(ref _iNamKetThuc, value);
        }

        private ObservableCollection<ComboboxItem> _drpReportTypes;
        public ObservableCollection<ComboboxItem> DrpReportTypes
        {
            get => _drpReportTypes;
            set => SetProperty(ref _drpReportTypes, value);
        }

        private ComboboxItem _drpReportTypeSelected;
        public ComboboxItem DrpReportTypeSelected
        {
            get => _drpReportTypeSelected;
            set
            {
                SetProperty(ref _drpReportTypeSelected, value);

                if (value != null && value.ValueItem.Equals(RPT_TONGHOP) && DrpDonVi != null && DrpDonVi.Count() > 0)
                {
                    DrpDonViSelected = DrpDonVi.Where(x => x.ValueItem.Equals(_sessionService.Current.IdDonVi)).FirstOrDefault();
                    LoadVouchers();
                }

                OnPropertyChanged(nameof(UnitVisibility));
                OnPropertyChanged(nameof(UnitDrpVisibility));
                OnPropertyChanged(nameof(BudgetVisibility));
                OnPropertyChanged(nameof(BudgetDrpVisibility));
                OnPropertyChanged(nameof(VoucherVisibility));
            }
        }

        private ObservableCollection<ComboboxItem> _drpNguonVon;
        public ObservableCollection<ComboboxItem> DrpNguonVon
        {
            get => _drpNguonVon;
            set => SetProperty(ref _drpNguonVon, value);
        }

        private ComboboxItem _drpNguonVonSelected;
        public ComboboxItem DrpNguonVonSelected
        {
            get => _drpNguonVonSelected;
            set => SetProperty(ref _drpNguonVonSelected, value);
        }

        private ObservableCollection<ComboboxItem> _drpDonVi;
        public ObservableCollection<ComboboxItem> DrpDonVi
        {
            get => _drpDonVi;
            set => SetProperty(ref _drpDonVi, value);
        }

        private ComboboxItem _drpDonViSelected;
        public ComboboxItem DrpDonViSelected
        {
            get => _drpDonViSelected;
            set => SetProperty(ref _drpDonViSelected, value);
        }

        private ObservableCollection<ComboboxItem> _drpDonViTinhs;
        public ObservableCollection<ComboboxItem> DrpDonViTinhs
        {
            get => _drpDonViTinhs;
            set => SetProperty(ref _drpDonViTinhs, value);
        }

        private ComboboxItem _drpDonViTinhSelected;
        public ComboboxItem DrpDonViTinhSelected
        {
            get => _drpDonViTinhSelected;
            set => SetProperty(ref _drpDonViTinhSelected, value);
        }

        private ObservableCollection<ComboboxItem> _drpLoaiCongTrinh;
        public ObservableCollection<ComboboxItem> DrpLoaiCongTrinh
        {
            get => _drpLoaiCongTrinh;
            set => SetProperty(ref _drpLoaiCongTrinh, value);
        }

        private ComboboxItem _drpLoaiCongTrinhSelected;
        public ComboboxItem DrpLoaiCongTrinhSelected
        {
            get => _drpLoaiCongTrinhSelected;
            set => SetProperty(ref _drpLoaiCongTrinhSelected, value);
        }

        private ObservableCollection<CheckBoxItem> _dataBudget;
        public ObservableCollection<CheckBoxItem> DataBudget
        {
            get => _dataBudget;
            set => SetProperty(ref _dataBudget, value);
        }

        private ObservableCollection<ComboboxItem> _drpVouchers;
        public ObservableCollection<ComboboxItem> DrpVouchers
        {
            get => _drpVouchers;
            set => SetProperty(ref _drpVouchers, value);
        }

        private ComboboxItem _drpVoucherSelected;
        public ComboboxItem DrpVoucherSelected
        {
            get => _drpVoucherSelected;
            set => SetProperty(ref _drpVoucherSelected, value);
        }


        private bool _selectAllDataUnit;
        public bool SelectAllDataUnit
        {
            get => (DataUnit == null || !DataUnit.Any()) ? false : DataUnit.All(item => item.IsChecked);
            set
            {
                SetProperty(ref _selectAllDataUnit, value);
                if (DataUnit != null)
                {
                    DataUnit.Select(c => { c.IsChecked = _selectAllDataUnit; return c; }).ToList();
                }
            }
        }

        private bool _electAllDataBudget;
        public bool SelectAllDataBudget
        {
            get => (DataBudget == null || !DataBudget.Any()) ? false : DataBudget.All(item => item.IsChecked);
            set
            {
                SetProperty(ref _electAllDataBudget, value);
                if (DataBudget != null)
                {
                    DataBudget.Select(c => { c.IsChecked = _electAllDataBudget; return c; }).ToList();
                }
            }
        }

        private string _txtHeader1;
        public string TxtHeader1
        {
            get => _txtHeader1;
            set => SetProperty(ref _txtHeader1, value);
        }

        private string _txtHeader2;
        public string TxtHeader2
        {
            get => _txtHeader2;
            set => SetProperty(ref _txtHeader2, value);
        }

        private string _txtHeader3;
        public string TxtHeader3
        {
            get => _txtHeader3;
            set => SetProperty(ref _txtHeader3, value);
        }

        private string _txtUnitHeader;
        public string TxtUnitHeader
        {
            get => _txtUnitHeader;
            set => SetProperty(ref _txtUnitHeader, value);
        }

        private string _txtChuKyKhthdd;
        public string TxtChuKyKhthdd
        {
            get => _txtChuKyKhthdd;
            set => SetProperty(ref _txtChuKyKhthdd, value);
        }

        public Visibility UnitVisibility
        {
            get
            {
                if ((ReportMediumTypes.Equals(ReportMediumType.SUGGESTION_CTMM) || ReportMediumTypes.Equals(ReportMediumType.SUGGESTION_CTCT)
                    || ReportMediumTypes.Equals(ReportMediumType.APPROVED_CTMM) || ReportMediumTypes.Equals(ReportMediumType.APPROVED_CTCT))
                    && (DrpReportTypeSelected == null || (DrpReportTypeSelected != null && DrpReportTypeSelected.ValueItem.Equals(RPT_TONGHOP))))
                {
                    return Visibility.Collapsed;
                }
                else
                {
                    return Visibility.Visible;
                }
            }
        }

        public Visibility UnitDrpVisibility
        {
            get
            {
                return DrpReportTypeSelected == null || (DrpReportTypeSelected != null && DrpReportTypeSelected.ValueItem.Equals(RPT_TONGHOP)) ? Visibility.Visible : Visibility.Collapsed;
            }
        }

        public Visibility BudgetVisibility
        {
            get
            {
                if ((ReportMediumTypes.Equals(ReportMediumType.APPROVED_CTMM) || ReportMediumTypes.Equals(ReportMediumType.APPROVED_CTCT)))
                {
                    return Visibility.Collapsed;
                }
                else
                {
                    return Visibility.Visible;
                }
            }
        }

        public Visibility BudgetDrpVisibility
        {
            get
            {
                if ((ReportMediumTypes.Equals(ReportMediumType.SUGGESTION_CTMM) || ReportMediumTypes.Equals(ReportMediumType.SUGGESTION_CTCT)))
                {
                    return Visibility.Collapsed;
                }
                else
                {
                    return Visibility.Visible;
                }
            }
        }

        public Visibility ReportVisibility
        {
            get => (ReportMediumTypes.Equals(ReportMediumType.APPROVED_CTMM) || ReportMediumTypes.Equals(ReportMediumType.APPROVED_CTCT)) ? Visibility.Collapsed : Visibility.Visible;
        }

        public Visibility VoucherVisibility
        {
            get => DrpReportTypeSelected != null && DrpReportTypeSelected.ValueItem.Equals(RPT_TONGHOP) ? Visibility.Visible : Visibility.Collapsed;
        }

        public Visibility LoaiCongTrinhVisibility
        {
            get => DrpVoucherTypeSelected != null && DrpVoucherTypeSelected.ValueItem.Equals(RPT_DIEUCHINH)
                    && ReportMediumTypes.Equals(ReportMediumType.SUGGESTION_CTMM) ? Visibility.Collapsed : Visibility.Visible;
        }
        #endregion

        #region Relay Command
        public RelayCommand PrintBrowserCommand { get; }
        public RelayCommand PrintExcelCommand { get; }
        public RelayCommand ConfigSignCommand { get; }
        #endregion

        public PrintReportMediumTermPlanViewModel(
            ISessionService sessionService,
            IMapper mapper,
            IExportService exportService,
            INsDonViService nsDonViService,
            IVdtKhvKeHoach5NamChiTietService vdtKhvKeHoach5NamChiTietService,
            IDmLoaiCongTrinhService loaiCongTrinhService,
            INsNguonNganSachService nguonNganSachService,
            IVdtDmLoaiCongTrinhService vdtDmLoaiCongTrinhService,
            IVdtKhvKeHoach5NamService vdtKhvKeHoach5NamService,
            IVdtKhvKeHoach5NamDeXuatChiTietService vdtKhvKeHoach5NamChiTietDexuat,
            IVdtKhvKeHoach5NamDeXuatService vdtKhvKeHoach5NamDeXuat,
            IVdtDmDonViThucHienDuAnService vdtDmDonViThucHien,
            IDmChuKyService dmChuKyService,
            ILog logger,
            DmChuKyDialogViewModel dmChuKyDialogViewModel)
        {
            _sessionService = sessionService;
            _nguonNganSachService = nguonNganSachService;
            _vdtKhvKeHoach5NamChiTietService = vdtKhvKeHoach5NamChiTietService;
            _loaiCongTrinhService = loaiCongTrinhService;
            _vdtDmLoaiCongTrinhService = vdtDmLoaiCongTrinhService;
            _vdtKhvKeHoach5NamService = vdtKhvKeHoach5NamService;
            _vdtKhvKeHoach5NamDeXuat = vdtKhvKeHoach5NamDeXuat;
            _vdtDmDonViThucHienDuAnService = vdtDmDonViThucHien;
            _vdtKhvKeHoach5NamChiTietDexuatService = vdtKhvKeHoach5NamChiTietDexuat;
            _nsDonViService = nsDonViService;
            _exportService = exportService;
            _dmChuKyService = dmChuKyService;
            _mapper = mapper;
            _logger = logger;
            DmChuKyDialogViewModel = dmChuKyDialogViewModel;

            PrintBrowserCommand = new RelayCommand(obj => OnPrintBrowser());
            PrintExcelCommand = new RelayCommand(obj => OnPrintExcel());
            ConfigSignCommand = new RelayCommand(obj => OnConfigSign());
        }

        #region RelayCommand
        public override void Init()
        {
            LoadData();
        }

        public override void LoadData(params object[] args)
        {
            try
            {
                //_sNamBatDau = DateTime.Now.Year.ToString();
                _sNamBatDau = _vdtKhvKeHoach5NamDeXuat.FindCurrentPeriod(_sessionService.Current.YearOfWork).ToString();
                _iNamKetThuc = Int32.Parse(_sNamBatDau) + 4;

                LoadDonViTinh();
                LoadVocherTypes();
                LoadNguonVon();
                LoadDonViQuanLy();
                LoadLoaiCongTrinh();
                LoadReportType();
                LoadHeader();
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        private void LoadDonViTinh()
        {
            List<ComboboxItem> lstDonViTinh = new List<ComboboxItem>()
            {
                new ComboboxItem(){DisplayItem = DonViTinh.DONG, ValueItem =  DonViTinh.DONG_VALUE},
                new ComboboxItem(){DisplayItem = DonViTinh.NGHIN_DONG, ValueItem = DonViTinh.NGHIN_DONG_VALUE},
                new ComboboxItem(){DisplayItem = DonViTinh.TRIEU_DONG, ValueItem = DonViTinh.TRIEU_VALUE},
                new ComboboxItem(){DisplayItem = DonViTinh.TY_DONG, ValueItem = DonViTinh.TY_VALUE}
            };

            DrpDonViTinhs = new ObservableCollection<ComboboxItem>(lstDonViTinh);

            DrpDonViTinhSelected = DrpDonViTinhs[2];
        }

        private void LoadVocherTypes()
        {
            List<ComboboxItem> lstVocherTypes = new List<ComboboxItem>() {
                new ComboboxItem {DisplayItem = "Gốc", ValueItem = RPT_GOC}
            };

            if (ReportMediumTypes.Equals(ReportMediumType.SUGGESTION_CTMM))
            {
                ComboboxItem itemCombobox = new ComboboxItem() { DisplayItem = "Điều chỉnh", ValueItem = RPT_DIEUCHINH };
                lstVocherTypes.Add(itemCombobox);
            }

            DrpVoucherTypes = new ObservableCollection<ComboboxItem>(lstVocherTypes);

            if (DrpVoucherTypes != null && DrpVoucherTypes.Count > 0)
            {
                _drpVoucherTypeSelected = DrpVoucherTypes.FirstOrDefault();
            }

            OnPropertyChanged(nameof(DrpVoucherTypeSelected));
        }

        private void LoadHeader()
        {
            if (ReportMediumTypes.Equals(ReportMediumType.APPROVED_CTCT) || ReportMediumTypes.Equals(ReportMediumType.SUGGESTION_CTCT))
            {
                if (string.IsNullOrEmpty(TxtHeader2))
                {
                    TxtHeader2 = "(Công trình chuyển tiếp)";
                }
            }
            else
            {
                if (string.IsNullOrEmpty(TxtHeader2))
                {
                    TxtHeader2 = "(Công trình mở mới)";
                }
            }

            if (!ReportMediumTypes.Equals(ReportMediumType.APPROVED_CTMM) && DrpVoucherTypeSelected != null && DrpVoucherTypeSelected.ValueItem.Equals("2"))
            {
                if (string.IsNullOrEmpty(TxtHeader1))
                {
                    TxtHeader1 = "KẾ HOẠCH ĐẦU TƯ TRUNG HẠN NGUỒN VỐN ……….GIAI ĐOẠN 20... - 20...";
                }
                if (string.IsNullOrEmpty(TxtHeader3))
                {
                    TxtHeader3 = "(Kèm theo công văn số ……. ngày …. tháng….. năm 2021 của …...)";
                }
            }
            else if (!ReportMediumTypes.Equals(ReportMediumType.APPROVED_CTMM))
            {
                if (string.IsNullOrEmpty(TxtHeader1))
                {
                    TxtHeader1 = "KẾ HOẠCH ĐẦU TƯ TRUNG HẠN .... GIAI ĐOẠN ....";
                }
                if (string.IsNullOrEmpty(TxtHeader3))
                {
                    TxtHeader3 = "(Kèm theo Quyết định số..ngày..tháng..năm..)";
                }
            }
            else if (ReportMediumTypes.Equals(ReportMediumType.APPROVED_CTCT))
            {
                _txtChuKyKhthdd = "NGƯỜI LẬP BIỂU";
                if (string.IsNullOrEmpty(TxtHeader1))
                {
                    TxtHeader1 = "DANH MỤC, HẠN MỨC ĐẦU TƯ, KẾ HOẠCH BỐ TRÍ VỐN CÁC DỰ ÁN CHUYỂN TIẾP NĂM ... THUỘC KẾ HOẠCH ĐẦU TƯ TRUNG HẠN 5 NĂM ...";
                }
                if (string.IsNullOrEmpty(TxtHeader3))
                {
                    TxtHeader3 = "(Kèm theo Quyết định số      /QĐ-BQP ngày       tháng         năm        của Bộ trưởng BQP";
                }
            }
            else if (ReportMediumTypes.Equals(ReportMediumType.APPROVED_CTMM))
            {
                if (string.IsNullOrEmpty(TxtHeader1))
                {
                    TxtHeader1 = "DANH MỤC, HẠN MỨC ĐẦU TƯ, KẾ HOẠCH BỐ TRÍ VỐN CÁC DỰ ÁN MỞ MỚI NĂM ... THUỘC KẾ HOẠCH ĐẦU TƯ TRUNG HẠN 5 NĂM ...";
                }
                if (string.IsNullOrEmpty(TxtHeader3))
                {
                    TxtHeader3 = "(Kèm theo Quyết định số      /QĐ-BQP ngày       tháng         năm        của Bộ trưởng BQP";
                }
            }
        }

        private void LoadVouchers()
        {
            try
            {
                if (ReportMediumTypes.Equals(ReportMediumType.SUGGESTION_CTMM))
                {
                    List<VdtKhvKeHoach5NamDeXuat> lstQuery = new List<VdtKhvKeHoach5NamDeXuat>();
                    if (_drpDonViSelected != null && !string.IsNullOrEmpty(_sNamBatDau))
                    {
                        lstQuery = _vdtKhvKeHoach5NamDeXuat.FindByIdDonViParent(_drpDonViSelected.ValueItem, (int)LoaiDuAnEnum.Type.KHOI_CONG_MOI, Int32.Parse(_sNamBatDau), _iNamKetThuc).ToList();
                    }
                    if (_drpVoucherTypeSelected != null && _drpVoucherTypeSelected.ValueItem.Equals(RPT_DIEUCHINH))
                    {
                        //lstQuery = lstQuery.Where(x => !x.BIsGoc.Value).ToList();
                        var lstDieuChinh = GetListKHTHDeXuatDieuChinh();
                        lstQuery = lstQuery.Where(x => lstDieuChinh.Any(y => x.STongHop.Contains(y.Id.ToString()))).ToList();
                    }
                    else
                    {
                        //lstQuery = lstQuery.Where(x => x.BIsGoc.Value).ToList();
                        var lstDieuChinh = GetListKHTHDeXuatDieuChinh();
                        lstQuery = lstQuery.Where(x => lstDieuChinh.All(y => !x.STongHop.Contains(y.Id.ToString()))).ToList();
                    }

                    _drpVouchers = _mapper.Map<ObservableCollection<ComboboxItem>>(lstQuery);
                }
                else if (ReportMediumTypes.Equals(ReportMediumType.APPROVED_CTMM))
                {
                    List<VdtKhvKeHoach5Nam> lstQuery = new List<VdtKhvKeHoach5Nam>();
                    if (_drpDonViSelected != null)
                    {
                        lstQuery = _vdtKhvKeHoach5NamService.FindByIdDonViParent(_drpDonViSelected.ValueItem, (int)LoaiDuAnEnum.Type.KHOI_CONG_MOI, Int32.Parse(_sNamBatDau), _iNamKetThuc).ToList();
                    }

                    _drpVouchers = _mapper.Map<ObservableCollection<ComboboxItem>>(lstQuery);
                }
                else if (ReportMediumTypes.Equals(ReportMediumType.SUGGESTION_CTCT))
                {
                    List<VdtKhvKeHoach5NamDeXuat> lstQuery = new List<VdtKhvKeHoach5NamDeXuat>();
                    if (_drpDonViSelected != null)
                    {
                        lstQuery = _vdtKhvKeHoach5NamDeXuat.FindByIdDonViParent(_drpDonViSelected.ValueItem, (int)LoaiDuAnEnum.Type.CHUYEN_TIEP, Int32.Parse(_sNamBatDau), _iNamKetThuc).ToList();
                    }

                    if (_drpVoucherTypeSelected != null && _drpVoucherTypeSelected.ValueItem.Equals(RPT_DIEUCHINH))
                    {
                        lstQuery = lstQuery.Where(x => !x.BIsGoc.Value).ToList();
                    }
                    else
                    {
                        lstQuery = lstQuery.Where(x => x.BIsGoc.Value).ToList();
                    }

                    _drpVouchers = _mapper.Map<ObservableCollection<ComboboxItem>>(lstQuery);
                }
                else if (ReportMediumTypes.Equals(ReportMediumType.APPROVED_CTCT))
                {
                    List<VdtKhvKeHoach5Nam> lstQuery = new List<VdtKhvKeHoach5Nam>();
                    if (_drpDonViSelected != null)
                    {
                        lstQuery = _vdtKhvKeHoach5NamService.FindByIdDonViParent(_drpDonViSelected.ValueItem, (int)LoaiDuAnEnum.Type.CHUYEN_TIEP, Int32.Parse(_sNamBatDau), _iNamKetThuc).ToList();
                    }

                    _drpVouchers = _mapper.Map<ObservableCollection<ComboboxItem>>(lstQuery);
                }

                OnPropertyChanged(nameof(DrpVouchers));
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        public List<VdtKhvKeHoach5NamDeXuatModel> GetListKHTHDeXuatDieuChinh()
        {
            var predicate = PredicateBuilder.True<VdtKhvKeHoach5NamDeXuat>();
            predicate = predicate.And(x => x.NamLamViec == _sessionService.Current.YearOfWork);
            predicate = predicate.And(x => x.IIdParentId != null);
            var lstResult = _vdtKhvKeHoach5NamDeXuat.FindByCondition(predicate).ToList();
            return _mapper.Map<List<VdtKhvKeHoach5NamDeXuatModel>>(lstResult);
        }

        private void LoadReportType()
        {
            try
            {
                List<ComboboxItem> lstReportType = new List<ComboboxItem>() {
                    new ComboboxItem { DisplayItem = "Báo cáo theo đơn vị", ValueItem = RPT_DONVI },
                    new ComboboxItem { DisplayItem = "Báo cáo tổng hợp", ValueItem = RPT_TONGHOP }
                };

                DrpReportTypes = new ObservableCollection<ComboboxItem>(lstReportType);

                if (DrpReportTypes != null && DrpReportTypes.Count > 0)
                {
                    DrpReportTypeSelected = DrpReportTypes.FirstOrDefault();
                }

                OnPropertyChanged(nameof(DrpReportTypeSelected));
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        public Expression<Func<VdtKhvKeHoach5Nam, bool>> CreatePredicate()
        {
            var predicate = PredicateBuilder.True<VdtKhvKeHoach5Nam>();
            if (DrpReportTypeSelected == null || DrpReportTypeSelected.ValueItem.Equals(RPT_TONGHOP))
            {
                predicate = predicate.And(x => x.Id == Guid.Parse(_drpVoucherSelected.ValueItem));
            }
            else
            {
                predicate = predicate.And(x => x.IGiaiDoanTu == Int32.Parse(SNamBatDau));
                predicate = predicate.And(x => x.IGiaiDoanDen == INamKetThuc);
                predicate = predicate.And(x => x.NamLamViec == _sessionService.Current.YearOfWork);

                if (ReportMediumTypes.Equals(ReportMediumType.APPROVED_CTCT))
                {
                    predicate = predicate.And(x => x.ILoai == (int)LoaiDuAnEnum.Type.CHUYEN_TIEP);
                }
                else if (ReportMediumTypes.Equals(ReportMediumType.APPROVED_CTMM))
                {
                    predicate = predicate.And(x => x.ILoai == (int)LoaiDuAnEnum.Type.KHOI_CONG_MOI);
                }
            }

            return predicate;
        }

        public Expression<Func<VdtKhvKeHoach5NamDeXuat, bool>> CreatePredicateDeXuat()
        {
            var predicate = PredicateBuilder.True<VdtKhvKeHoach5NamDeXuat>();

            if (DrpReportTypeSelected == null || DrpReportTypeSelected.ValueItem.Equals(RPT_TONGHOP))
            {
                predicate = predicate.And(x => x.Id == Guid.Parse(_drpVoucherSelected.ValueItem));
            }
            else
            {
                List<string> lstDonViQuanLy = DataUnit.Where(x => x.IsChecked).Select(x => x.ValueItem).ToList();
                predicate = predicate.And(x => lstDonViQuanLy.Contains(x.IIdMaDonViQuanLy));
                predicate = predicate.And(x => x.NamLamViec == _sessionService.Current.YearOfWork);

                predicate = predicate.And(x => x.IGiaiDoanTu == Int32.Parse(SNamBatDau));
                predicate = predicate.And(x => x.IGiaiDoanDen == INamKetThuc);
                predicate = predicate.And(x => string.IsNullOrEmpty(x.STongHop));
                predicate = predicate.And(x => x.IIdParentId == null || x.IIdParentId.Equals(Guid.Empty));

                if (ReportMediumTypes.Equals(ReportMediumType.SUGGESTION_CTCT))
                {
                    predicate = predicate.And(x => x.ILoai == (int)LoaiDuAnEnum.Type.CHUYEN_TIEP);
                }
                else if (ReportMediumTypes.Equals(ReportMediumType.SUGGESTION_CTMM))
                {
                    predicate = predicate.And(x => x.ILoai == (int)LoaiDuAnEnum.Type.KHOI_CONG_MOI);
                }
            }

            return predicate;
        }

        public Expression<Func<VdtKhvKeHoach5NamDeXuat, bool>> CreatePredicateDeXuatDieuChinh()
        {
            var predicate = PredicateBuilder.True<VdtKhvKeHoach5NamDeXuat>();

            if (DrpReportTypeSelected == null || DrpReportTypeSelected.ValueItem.Equals(RPT_TONGHOP))
            {
                predicate = predicate.And(x => x.Id == Guid.Parse(_drpVoucherSelected.ValueItem));
            }
            else
            {
                List<string> lstDonViQuanLy = DataUnit.Where(x => x.IsChecked).Select(x => x.ValueItem).ToList();
                predicate = predicate.And(x => lstDonViQuanLy.Contains(x.IIdMaDonViQuanLy));
                predicate = predicate.And(x => x.NamLamViec == _sessionService.Current.YearOfWork);
                predicate = predicate.And(x => x.IGiaiDoanTu == Int32.Parse(SNamBatDau));
                predicate = predicate.And(x => x.IGiaiDoanDen == INamKetThuc);
                predicate = predicate.And(x => string.IsNullOrEmpty(x.STongHop));
                predicate = predicate.And(x => x.BActive);
            }
            return predicate;
        }

        private StringBuilder Validation()
        {
            try
            {
                StringBuilder sError = new StringBuilder();

                if (ReportMediumTypes.Equals(ReportMediumType.APPROVED_CTMM) && DrpNguonVonSelected == null)
                {
                    sError.AppendLine(string.Format(Resources.MsgErrorRequire, "Nguồn vốn"));
                }

                if (ReportMediumTypes.Equals(ReportMediumType.SUGGESTION_CTMM) && !DataBudget.Any(x => x.IsChecked))
                {
                    sError.AppendLine(string.Format(Resources.MsgErrorRequire, "Nguồn vốn"));
                }

                if ((DrpReportTypeSelected != null && DrpReportTypeSelected.ValueItem.Equals(RPT_DONVI)) && string.IsNullOrEmpty(SNamBatDau))
                {
                    sError.AppendLine(string.Format(Resources.MsgErrorRequire, "Giai đoạn bắt đầu"));
                }

                if ((DrpReportTypeSelected != null && DrpReportTypeSelected.ValueItem.Equals(RPT_TONGHOP)) && DrpVoucherSelected == null)
                {
                    sError.AppendLine(string.Format(Resources.MsgErrorRequire, "Chứng từ"));
                }

                if (!(ReportMediumTypes.Equals(ReportMediumType.SUGGESTION_CTMM)
                    && DrpVoucherTypeSelected != null && DrpVoucherTypeSelected.ValueItem.Equals(RPT_DIEUCHINH)) && DrpLoaiCongTrinhSelected == null)
                {
                    sError.AppendLine(string.Format(Resources.MsgErrorRequire, "Loại công trình"));
                }

                if (DrpDonViTinhSelected == null)
                {
                    sError.AppendLine(string.Format(Resources.MsgErrorRequire, "Đơn vị tính"));
                }

                return sError;
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
                return new StringBuilder();
            }
        }

        private string GetParentUnit()
        {
            string sParent = NSConstants.BO_QUOC_PHONG;
            string idDonVi = "";
            if (_drpReportTypeSelected != null && _drpReportTypeSelected.ValueItem.Equals(RPT_TONGHOP))
            {
                idDonVi = _sessionService.Current.IdDonVi;
            }
            else if (_drpReportTypeSelected != null && _drpReportTypeSelected.ValueItem.Equals(RPT_DONVI))
            {
                if (DataUnit.Where(x => x.IsChecked).Count() == 1)
                {
                    idDonVi = DataUnit.Where(x => x.IsChecked).FirstOrDefault().ValueItem;
                }
                else
                {
                    idDonVi = _sessionService.Current.IdDonVi;
                }
            }
            DonVi donvi = _nsDonViService.FindByIdDonVi(idDonVi, _sessionService.Current.YearOfWork);
            if (!"0".Equals(donvi?.Loai))
            {
                DonVi donViCapTren = _nsDonViService.FindByLoai("0", _sessionService.Current.YearOfWork);
                sParent = donViCapTren?.TenDonVi;
            }
            return sParent;
        }

        private string GetNameUnit()
        {
            try
            {
                string sTenDonVi = string.Empty;

                if (_drpReportTypeSelected != null && _drpReportTypeSelected.ValueItem.Equals(RPT_TONGHOP))
                {
                    sTenDonVi = _sessionService.Current.TenDonVi;
                }
                else if (_drpReportTypeSelected != null && _drpReportTypeSelected.ValueItem.Equals(RPT_DONVI))
                {
                    if (DataUnit.Where(x => x.IsChecked).Count() == 1)
                    {
                        string sTen = DataUnit.Where(x => x.IsChecked).FirstOrDefault().DisplayItem;
                        if (!string.IsNullOrEmpty(sTen) && sTen.Contains("-"))
                        {
                            sTen = sTen.Split("-")[1];
                        }
                        sTenDonVi = sTen;
                    }
                    else
                    {
                        sTenDonVi = _sessionService.Current.TenDonVi;
                    }
                }

                return sTenDonVi;
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
                return _sessionService.Current.TenDonVi;
            }
        }

        private void OnPrintReport(ExportType exportType)
        {
            try
            {
                StringBuilder sError = Validation();
                if (sError.Length != 0)
                {
                    MessageBox.Show(sError.ToString());
                    return;
                }

                sError = AuthorizationReport();
                List<DonVi> userAgency = _nsDonViService.FindByUser(_sessionService.Current.Principal, _sessionService.Current.YearOfWork, LoaiDonVi.ROOT);
                if (!userAgency.Any(x => x.Loai == LoaiDonVi.ROOT) && sError.Length != 0)
                {
                    MessageBox.Show(string.Format(Resources.UserManagerKHTHReportWarning, _sessionService.Current.Principal, sError.ToString()));
                    return;
                }

                var predicate = CreatePredicate();
                var itemQuery = _vdtKhvKeHoach5NamService.FindByCondition(predicate).ToList();
                if (itemQuery == null || itemQuery.Count == 0)
                {
                    MessageBox.Show(Resources.MsgErrorDataNotFound);
                    return;
                }

                BackgroundWorkerHelper.Run((s, e) =>
                {
                    IsLoading = true;

                    List<VdtKhvKeHoach5NamModel> item = _mapper.Map<List<VdtKhvKeHoach5NamModel>>(itemQuery);
                    List<VdtKhvKeHoach5NamModel> itemtmp = new List<VdtKhvKeHoach5NamModel>(item);
                    List<string> sLoaiCongTrinh = new List<string>();
                    if (_drpLoaiCongTrinhSelected.ValueItem.Equals("1"))
                    {
                        sLoaiCongTrinh = _drpLoaiCongTrinh.Where(x => !x.ValueItem.Equals("1")).Select(x => x.ValueItem).ToList();
                    }
                    else
                    {
                        sLoaiCongTrinh.Add(_drpLoaiCongTrinhSelected.ValueItem);
                    }
                    List<Guid?> lstIdParent = item.Select(x => x.IIdParentId).ToList();
                    foreach (VdtKhvKeHoach5NamModel n in itemtmp)
                    {
                        if (lstIdParent.Contains(n.Id))
                            item.Remove(n);
                    }
                    string lstLoaiCongTrinh = string.Join(",", sLoaiCongTrinh.ToList()).ToString();
                    string lstId = string.Join(",", item.Select(x => x.Id.ToString()).ToList());
                    string lstDonViThucHienDuAn = string.Join(",", DataUnit.Where(x => x.IsChecked).Select(x => x.ValueItem).ToList());
                    List<VdtKhvKeHoach5NamReportQuery> lstData = _vdtKhvKeHoach5NamChiTietService.FindByReportKeHoachTrungHan(lstId, lstLoaiCongTrinh,
                        int.Parse(_drpNguonVonSelected.ValueItem), int.Parse(_drpReportTypeSelected.ValueItem), Double.Parse(DrpDonViTinhSelected.ValueItem), lstDonViThucHienDuAn).ToList();

                    ItemsReport = new ObservableCollection<VdtKhvKeHoach5NamReportQuery>(lstData);

                    CalculateDataReport();
                    HandleDataReport();
                    var itemSumary = SetDataSumary();

                    Dictionary<string, object> data = new Dictionary<string, object>();

                    string sTenDonVi = GetNameUnit();
                    if (!string.IsNullOrEmpty(sTenDonVi))
                    {
                        sTenDonVi = sTenDonVi.ToUpper();
                    }
                    data.Add("CapTren", GetParentUnit());
                    data.Add("YearBefore", (Int32.Parse(SNamBatDau) - 1).ToString());
                    data.Add("YearAfter", INamKetThuc);
                    data.Add("Period", string.Format("{0} - {1}", SNamBatDau, INamKetThuc));
                    data.Add("TenDonVi", sTenDonVi);
                    data.Add("NguonNganSach", _drpNguonVonSelected != null ? _drpNguonVonSelected.DisplayItem : string.Empty);
                    data.Add("Items", ItemsReport);
                    data.Add("Header1", TxtHeader1);
                    data.Add("Header3", TxtHeader3);
                    data.Add("DonViTinh", DrpDonViTinhSelected.DisplayItem);
                    data.Add("FHanMucDauTu", itemSumary.FHanMucDauTu);
                    data.Add("FVonDaGiao", itemSumary.FVonDaGiao);
                    data.Add("FTongVonBoTri", itemSumary.FTongVonBoTri);
                    data.Add("FGiaTriKeHoach", itemSumary.FGiaTriKeHoach);
                    data.Add("FVonBoTri", itemSumary.FVonBoTri);
                    FormatNumber formatNumber = new FormatNumber(Int32.Parse(DrpDonViTinhSelected.ValueItem), exportType);
                    data.Add("FormatNumber", formatNumber);
                    _dmChuKyService.GetConfigSign(TypeChuKy.RPT_VDT_KEHOACHTRUNGHAN_DUOCDUYET_MO_MOI, ref data);

                    string templateFileName = Path.Combine(ExportPrefix.PATH_TL_KHTH, MediumTermPlanType.REPORT_KE_HOACH_TRUNG_HAN_DUOC_DUYET_TEMPLATE);
                    string fileNamePrefix = Path.GetFileNameWithoutExtension(MediumTermPlanType.REPORT_KE_HOACH_TRUNG_HAN_DUOC_DUYET_TEMPLATE);
                    string fileNameWithoutExtension = StringUtils.CreateExportFileName(fileNamePrefix);
                    var xlsFile = _exportService.Export<VdtKhvKeHoach5NamReportQuery, VdtKhvKeHoach5NamReportQuery>(templateFileName, data);
                    _exportService.FormatAllRowHeight(ItemsReport, "STenDuAn", 12, 55, xlsFile);
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

        private VdtKhvKeHoach5NamReportQuery SetDataSumary()
        {
            VdtKhvKeHoach5NamReportQuery item = new VdtKhvKeHoach5NamReportQuery();
            if (ItemsReport == null || ItemsReport.Count == 0) return item;
            item.FHanMucDauTu = ItemsReport.Where(n => n.LoaiParent == 0).Sum(n => n.FHanMucDauTu);
            item.FVonDaGiao = ItemsReport.Where(n => n.LoaiParent == 0).Sum(n => n.FVonDaGiao);
            item.FTongVonBoTri = ItemsReport.Where(n => n.LoaiParent == 0).Sum(n => n.FTongVonBoTri);
            item.FGiaTriKeHoach = ItemsReport.Where(n => n.LoaiParent == 0).Sum(n => n.FGiaTriKeHoach);
            item.FVonBoTri = ItemsReport.Where(n => n.LoaiParent == 0).Sum(n => n.FVonBoTri);
            return item;
        }

        private void HandleDataReportDeXuat()
        {
            try
            {
                List<VdtKhvKeHoach5NamDeXuatReportQuery> lstItem = ItemsReportDeXuat.Where(n => n.LoaiParent.Equals(0)).ToList();
                lstItem.Select(n => { n.STT = RomanNumber.ToRoman((lstItem.IndexOf(n) + 1)).ToString(); return n; }).ToList();
                List<VdtKhvKeHoach5NamDeXuatReportQuery> lstItemLevel = ItemsReportDeXuat.Where(x => x.IdLoaiCongTrinhParent != null && x.IsHangCha && x.LoaiParent.Equals(1)).ToList();
                var dctItemLevel = lstItemLevel.GroupBy(x => x.IdLoaiCongTrinhParent).ToDictionary(x => x.Key, x => x.ToList());
                dctItemLevel.Keys.Select(x =>
                {
                    List<VdtKhvKeHoach5NamDeXuatReportQuery> lstItemStt = dctItemLevel[x].ToList();
                    lstItemStt.Select(x => { x.STT = (lstItemStt.IndexOf(x) + 1).ToString(); return x; }).ToList();
                    return x;
                }).ToList();

                if (_drpReportTypeSelected.ValueItem.Equals("1"))
                {
                    foreach (var item in lstItemLevel)
                    {
                        List<VdtKhvKeHoach5NamDeXuatReportQuery> lstChildrent = ItemsReportDeXuat.Where(x => x.IdLoaiCongTrinh == item.IdLoaiCongTrinh && !x.IsHangCha).ToList();
                        lstChildrent.Select(x => { x.STT = string.Format("{0}.{1}", item.STT, (lstChildrent.IndexOf(x) + 1).ToString()); return x; }).ToList();
                    }
                    List<VdtKhvKeHoach5NamDeXuatReportQuery> lstLctParent = ItemsReportDeXuat.Where(x => x.LoaiParent.Equals(0)).ToList();
                    var dctItemParent = ItemsReportDeXuat.Where(x => !x.IsHangCha && x.IdLoaiCongTrinh.HasValue
                        && lstLctParent.Select(y => y.IdLoaiCongTrinh).Contains(x.IdLoaiCongTrinh)).GroupBy(z => z.IdLoaiCongTrinh).ToDictionary(z => z.Key.ToString(), z => z.ToList());
                    foreach (var item in dctItemParent.Keys)
                    {
                        List<VdtKhvKeHoach5NamDeXuatReportQuery> itemStt = dctItemParent[item];
                        itemStt.Select(x => { x.STT = string.Format("{0}", (itemStt.IndexOf(x) + 1).ToString()); return x; }).ToList();
                    }
                }
                else
                {
                    foreach (var item in lstItemLevel)
                    {
                        List<VdtKhvKeHoach5NamDeXuatReportQuery> lstChildrent = ItemsReportDeXuat.Where(x => x.IdLoaiCongTrinhParent != null && x.IdLoaiCongTrinh == item.IdLoaiCongTrinh && x.IsHangCha && x.LoaiParent.Equals(2)).ToList();
                        lstChildrent.Select(x => { x.STT = string.Format("{0}.{1}", item.STT, (lstChildrent.IndexOf(x) + 1).ToString()); return x; }).ToList();
                    }

                    foreach (var item in ItemsReportDeXuat.Where(x => x.IsHangCha && x.LoaiParent.Equals(2)))
                    {
                        List<VdtKhvKeHoach5NamDeXuatReportQuery> lstChildrent = ItemsReportDeXuat.Where(x => x.IdLoaiCongTrinhParent != null && x.IdLoaiCongTrinh == item.IdLoaiCongTrinh && !x.IsHangCha && x.iID_KeHoach5NamID == item.iID_KeHoach5NamID).ToList();
                        lstChildrent.Select(x =>
                        {
                            x.STT = string.Format("{0}.{1}", item.STT, (lstChildrent.IndexOf(x) + 1).ToString());
                            return x;
                        }).ToList();
                    }

                    List<VdtKhvKeHoach5NamDeXuatReportQuery> lstParentDonVi = ItemsReportDeXuat.Where(x => x.IdLoaiCongTrinhParent == null && x.IsHangCha && x.LoaiParent.Equals(2)).ToList();
                    lstParentDonVi.Select(x => { x.STT = (lstParentDonVi.IndexOf(x) + 1).ToString(); return x; }).ToList();
                    foreach (var item in lstParentDonVi)
                    {
                        List<VdtKhvKeHoach5NamDeXuatReportQuery> lstChildrent = ItemsReportDeXuat.Where(x => x.IdLoaiCongTrinhParent == null && !x.IsHangCha && x.LoaiParent.Equals(2) && x.IdMaDonViQuanLy.Equals(item.IdMaDonViQuanLy)).ToList();
                        lstChildrent.Select(x =>
                        {
                            x.STT = string.Format("{0}.{1}", item.STT, (lstChildrent.IndexOf(x) + 1).ToString());
                            return x;
                        }).ToList();
                    }
                    //lstParentDonVi.Select(item =>
                    //{
                    //    List<VdtKhvKeHoach5NamDeXuatReportQuery> lstChildrent = ItemsReportDeXuat.Where(x => x.IdLoaiCongTrinhParent == null && !x.IsHangCha && x.LoaiParent.Equals(2)).ToList();


                    //    lstChildrent.Select(x =>
                    //    {
                    //        x.STT = string.Format("{0}.{1}", item.STT, (lstChildrent.IndexOf(x) + 1).ToString());
                    //        return x;
                    //    }).ToList();
                    //    return item;
                    //}).ToList();
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        private void HandleDataHanMucDauTu()
        {
            foreach (var item1 in ItemsReportDeXuat.ToList())
            {
                if (item1.IIdNguonVon.Equals((int)MediumTermType.Nsqp))
                {
                    item1.FHanMucDauTuQP = item1.FHanMucDauTu;
                }
                else if (item1.IIdNguonVon.Equals((int)MediumTermType.Nsnn))
                {
                    item1.FHanMucDauTuNN = item1.FHanMucDauTu;
                }
                else if (item1.IIdNguonVon.Equals((int)MediumTermType.Nsdp))
                {
                    item1.FHanMucDauTuDP = item1.FHanMucDauTu;
                }
                else
                {
                    item1.FHanMucDauTuOrther = item1.FHanMucDauTu;
                }

                if (!item1.IIdNguonVon.Equals((int)MediumTermType.Nsqp))
                {
                    item1.FTongSoNhuCau = 0;
                }
            }
        }

        private void CalculateDataReportDeXuat()
        {
            try
            {
                if (_drpReportTypeSelected != null && _drpReportTypeSelected.ValueItem.Equals(RPT_TONGHOP))
                {
                    List<VdtKhvKeHoach5NamDeXuatReportQuery> childrent = ItemsReportDeXuat.Where(x => !x.IsHangCha).ToList();
                    List<VdtKhvKeHoach5NamDeXuatReportQuery> parent = ItemsReportDeXuat.Where(x => x.IsHangCha && (x.LoaiParent.Equals(1) || x.LoaiParent.Equals(0))).ToList();

                    ItemsReportDeXuat.Where(x => x.IsHangCha && x.LoaiParent.Equals(1)).Select(x =>
                    {
                        x.FHanMucDauTu = 0;
                        x.FTongSoNhuCau = 0;
                        x.FTongSo = 0;
                        x.FGiaTriNamThuNhat = 0;
                        x.FGiaTriNamThuHai = 0;
                        x.FGiaTriNamThuBa = 0;
                        x.FGiaTriNamThuTu = 0;
                        x.FGiaTriNamThuNam = 0;
                        x.TongLuyKe = 0;
                        x.LuyKeVonNSQPDaBoTri = 0;
                        x.LuyKeVonNSQPDeNghiBoTri = 0;
                        x.FHanMucDauTuQP = 0;
                        x.FHanMucDauTuNN = 0;
                        x.FHanMucDauTuDP = 0;
                        x.FHanMucDauTuOrther = 0;
                        x.FGiaTriBoTri = 0;
                        return x;
                    }).ToList();

                    foreach (var pr in parent)
                    {
                        List<VdtKhvKeHoach5NamDeXuatReportQuery> lstChilrent = childrent.Where(x => x.IdLoaiCongTrinh.Equals(pr.IdLoaiCongTrinh)).ToList();
                        foreach (var item in lstChilrent.Where(x => (x.FHanMucDauTu != 0 || x.FTongSoNhuCau != 0 || x.FTongSo != 0
                                 || x.FGiaTriNamThuNhat != 0 || x.FGiaTriNamThuHai != 0 || x.FGiaTriNamThuBa != 0 || x.FGiaTriBoTri != 0
                                 || x.FGiaTriNamThuTu != 0 || x.FGiaTriNamThuNam != 0 || x.TongLuyKe != 0 || x.LuyKeVonNSQPDaBoTri != 0 || x.LuyKeVonNSQPDeNghiBoTri != 0)))
                        {
                            pr.FHanMucDauTu += item.FHanMucDauTu ?? 0;
                            pr.FTongSoNhuCau += item.FTongSoNhuCau ?? 0;
                            pr.FTongSo += item.FTongSo ?? 0;
                            pr.FGiaTriNamThuNhat += item.FGiaTriNamThuNhat ?? 0;
                            pr.FGiaTriNamThuHai += item.FGiaTriNamThuHai ?? 0;
                            pr.FGiaTriNamThuBa += item.FGiaTriNamThuBa ?? 0;
                            pr.FGiaTriNamThuTu += item.FGiaTriNamThuTu ?? 0;
                            pr.FGiaTriNamThuNam += item.FGiaTriNamThuNam ?? 0;
                            pr.FGiaTriBoTri += item.FGiaTriBoTri ?? 0;
                            pr.TongLuyKe += item.TongLuyKe ?? 0;
                            pr.LuyKeVonNSQPDaBoTri += item.LuyKeVonNSQPDaBoTri ?? 0;
                            pr.LuyKeVonNSQPDeNghiBoTri += item.LuyKeVonNSQPDeNghiBoTri ?? 0;
                            pr.FHanMucDauTuQP += item.FHanMucDauTuQP ?? 0;
                            pr.FHanMucDauTuNN += item.FHanMucDauTuNN ?? 0;
                            pr.FHanMucDauTuDP += item.FHanMucDauTuDP ?? 0;
                            pr.FHanMucDauTuOrther += item.FHanMucDauTuOrther ?? 0;
                        }
                    }

                    foreach (var item in parent.Where(x => (x.FHanMucDauTu != 0 || x.FTongSoNhuCau != 0 || x.FTongSo != 0
                                || x.FGiaTriNamThuNhat != 0 || x.FGiaTriNamThuHai != 0 || x.FGiaTriNamThuBa != 0 || x.FGiaTriBoTri != 0
                                || x.FGiaTriNamThuTu != 0 || x.FGiaTriNamThuNam != 0 || x.TongLuyKe != 0 || x.LuyKeVonNSQPDaBoTri != 0 || x.LuyKeVonNSQPDeNghiBoTri != 0
                                || x.LuyKeVonNSQPDaBoTri != 0 || x.LuyKeVonNSQPDeNghiBoTri != 0)))
                    {
                        CalculateParentDeXuat(item, item);
                    }
                }
                else
                {
                    List<VdtKhvKeHoach5NamDeXuatReportQuery> lstDonViparent = ItemsReportDeXuat.Where(x => x.IsHangCha && x.LoaiParent.Equals(2)).ToList();
                    List<VdtKhvKeHoach5NamDeXuatReportQuery> lstLoaiCongTrinhparent = ItemsReportDeXuat.Where(x => x.IsHangCha && x.LoaiParent.Equals(1)).ToList();

                    ItemsReportDeXuat.Where(x => x.IsHangCha && x.LoaiParent.Equals(1)).Select(x =>
                    {
                        x.FHanMucDauTu = 0;
                        x.FTongSoNhuCau = 0;
                        x.FTongSo = 0;
                        x.FGiaTriNamThuNhat = 0;
                        x.FGiaTriNamThuHai = 0;
                        x.FGiaTriNamThuBa = 0;
                        x.FGiaTriNamThuTu = 0;
                        x.FGiaTriNamThuNam = 0;
                        x.FHanMucDauTuQP = 0;
                        x.FHanMucDauTuNN = 0;
                        x.FHanMucDauTuDP = 0;
                        x.FHanMucDauTuOrther = 0;
                        x.LuyKeVonNSQPDeNghiBoTri = 0;
                        x.LuyKeVonNSQPDaBoTri = 0;
                        x.FGiaTriBoTri = 0;
                        x.TongLuyKe = 0;
                        return x;
                    }).ToList();

                    foreach (var pr in lstLoaiCongTrinhparent)
                    {
                        List<VdtKhvKeHoach5NamDeXuatReportQuery> lstChilrent = lstDonViparent.Where(x => x.IdLoaiCongTrinh.Equals(pr.IdLoaiCongTrinh)).ToList();
                        foreach (var item in lstChilrent.Where(x => (x.FHanMucDauTu != 0 || x.FHanMucDauTuQP != 0 || x.FHanMucDauTuNN != 0
                                 || x.FHanMucDauTuNN != 0 || x.FHanMucDauTuOrther != 0 || x.FTongSoNhuCau != 0 || x.FTongSo != 0
                                 || x.FGiaTriNamThuNhat != 0 || x.FGiaTriNamThuHai != 0 || x.FGiaTriNamThuBa != 0 || x.FGiaTriBoTri != 0
                                 || x.FGiaTriNamThuTu != 0 || x.FGiaTriNamThuNam != 0 || x.LuyKeVonNSQPDeNghiBoTri != 0 || x.LuyKeVonNSQPDaBoTri != 0)))
                        {
                            pr.FHanMucDauTu += item.FHanMucDauTu ?? 0;
                            pr.FTongSoNhuCau += item.FTongSoNhuCau ?? 0;
                            pr.FTongSo += item.FTongSo ?? 0;
                            pr.FGiaTriBoTri += item.FGiaTriBoTri ?? 0;
                            pr.FGiaTriNamThuNhat += item.FGiaTriNamThuNhat ?? 0;
                            pr.FGiaTriNamThuHai += item.FGiaTriNamThuHai ?? 0;
                            pr.FGiaTriNamThuBa += item.FGiaTriNamThuBa ?? 0;
                            pr.FGiaTriNamThuTu += item.FGiaTriNamThuTu ?? 0;
                            pr.FGiaTriNamThuNam += item.FGiaTriNamThuNam ?? 0;
                            pr.FHanMucDauTuQP += item.FHanMucDauTuQP ?? 0;
                            pr.FHanMucDauTuNN += item.FHanMucDauTuNN ?? 0;
                            pr.FHanMucDauTuDP += item.FHanMucDauTuDP ?? 0;
                            pr.FHanMucDauTuOrther += item.FHanMucDauTuOrther ?? 0;
                            pr.LuyKeVonNSQPDaBoTri += item.LuyKeVonNSQPDaBoTri ?? 0;
                            pr.LuyKeVonNSQPDeNghiBoTri += item.LuyKeVonNSQPDeNghiBoTri ?? 0;
                            pr.TongLuyKe += item.TongLuyKe ?? 0;
                        }
                    }

                    foreach (var item in lstLoaiCongTrinhparent.Where(x => (x.FHanMucDauTu != 0 || x.FTongSoNhuCau != 0 || x.FTongSo != 0
                                || x.FGiaTriNamThuNhat != 0 || x.FGiaTriNamThuHai != 0 || x.FGiaTriNamThuBa != 0 || x.FGiaTriBoTri != 0
                                || x.FGiaTriNamThuTu != 0 || x.FGiaTriNamThuNam != 0 || x.LuyKeVonNSQPDaBoTri != 0 || x.LuyKeVonNSQPDeNghiBoTri != 0)))
                    {
                        CalculateParentDeXuat(item, item);
                    }
                }

                List<VdtKhvKeHoach5NamDeXuatReportQuery> lstDataChildrent = ItemsReportDeXuat.Where(x => (x.FHanMucDauTu != 0 || x.FTongSoNhuCau != 0 || x.FTongSo != 0
                                || x.FGiaTriNamThuNhat != 0 || x.FGiaTriNamThuHai != 0 || x.FGiaTriNamThuBa != 0
                                || x.FGiaTriNamThuTu != 0 || x.FGiaTriNamThuNam != 0 || x.LuyKeVonNSQPDaBoTri != 0 || x.LuyKeVonNSQPDeNghiBoTri != 0)).ToList();

                List<VdtKhvKeHoach5NamDeXuatReportQuery> lstDataParent = ItemsReportDeXuat.Where(x => x.LoaiParent.Equals(0)
                    && lstDataChildrent.Select(x => x.IdLoaiCongTrinh).ToList().Contains(x.IdLoaiCongTrinh)).Select(x =>
                    {
                        x.FHanMucDauTu = 0;
                        x.FTongSoNhuCau = 0;
                        x.FTongSo = 0;
                        x.FGiaTriNamThuNhat = 0;
                        x.FGiaTriNamThuHai = 0;
                        x.FGiaTriNamThuBa = 0;
                        x.FGiaTriNamThuTu = 0;
                        x.FGiaTriNamThuNam = 0;
                        x.FGiaTriBoTri = 0;
                        x.FHanMucDauTuQP = 0;
                        x.FHanMucDauTuNN = 0;
                        x.FHanMucDauTuDP = 0;
                        x.FHanMucDauTuOrther = 0;
                        x.LuyKeVonNSQPDaBoTri = 0;
                        x.LuyKeVonNSQPDeNghiBoTri = 0;
                        x.TongLuyKe = 0;

                        return x;
                    }).ToList();

                foreach (var pr in lstDataParent)
                {
                    //foreach (var item in ItemsReportDeXuat.Where(x => x.IsHangCha && x.LoaiParent.Equals(2) && (x.FHanMucDauTu != 0 || x.FTongSoNhuCau != 0 || x.FTongSo != 0
                    //    || x.FGiaTriNamThuNhat != 0 || x.FGiaTriNamThuHai != 0 || x.FGiaTriNamThuBa != 0
                    //    || x.FGiaTriNamThuTu != 0 || x.FGiaTriNamThuNam != 0 || x.LuyKeVonNSQPDaBoTri != 0 || x.LuyKeVonNSQPDeNghiBoTri != 0)))
                    foreach (var item in ItemsReportDeXuat.Where(x => !x.IsHangCha && (x.SMaLoaiCongTrinh == pr.SMaLoaiCongTrinh || x.IdLoaiCongTrinhParent == pr.IdLoaiCongTrinh) && (x.FHanMucDauTu != 0 || x.FTongSoNhuCau != 0 || x.FTongSo != 0
                        || x.FGiaTriNamThuNhat != 0 || x.FGiaTriNamThuHai != 0 || x.FGiaTriNamThuBa != 0
                        || x.FGiaTriNamThuTu != 0 || x.FGiaTriNamThuNam != 0 || x.LuyKeVonNSQPDaBoTri != 0 || x.LuyKeVonNSQPDeNghiBoTri != 0)))
                    {
                        pr.FHanMucDauTu += item.FHanMucDauTu;
                        pr.FTongSoNhuCau += item.FTongSoNhuCau;
                        pr.FTongSo += item.FTongSo;
                        pr.FGiaTriNamThuNhat += item.FGiaTriNamThuNhat;
                        pr.FGiaTriNamThuHai += item.FGiaTriNamThuHai;
                        pr.FGiaTriNamThuBa += item.FGiaTriNamThuBa;
                        pr.FGiaTriNamThuTu += item.FGiaTriNamThuTu;
                        pr.FGiaTriNamThuNam += item.FGiaTriNamThuNam;
                        pr.FGiaTriBoTri += item.FGiaTriBoTri;
                        pr.FHanMucDauTuQP += item.FHanMucDauTuQP;
                        pr.FHanMucDauTuNN += item.FHanMucDauTuNN;
                        pr.FHanMucDauTuDP += item.FHanMucDauTuDP;
                        pr.FHanMucDauTuOrther += item.FHanMucDauTuOrther;
                        pr.LuyKeVonNSQPDaBoTri += item.LuyKeVonNSQPDaBoTri;
                        pr.LuyKeVonNSQPDeNghiBoTri += item.LuyKeVonNSQPDeNghiBoTri;
                        pr.TongLuyKe += item.TongLuyKe;
                    }
                }

                List<VdtKhvKeHoach5NamDeXuatReportQuery> listGroup = ItemsReportDeXuat.Where(x => (x.FHanMucDauTu != 0 || x.FTongSoNhuCau != 0 || x.FTongSo != 0
                                || x.FGiaTriNamThuNhat != 0 || x.FGiaTriNamThuHai != 0 || x.FGiaTriNamThuBa != 0
                                || x.FGiaTriNamThuTu != 0 || x.FGiaTriNamThuNam != 0 || x.LuyKeVonNSQPDaBoTri != 0 || x.LuyKeVonNSQPDeNghiBoTri != 0
                                || lstDataParent.Select(y => y.IdLoaiCongTrinh).ToList().Contains(x.IdLoaiCongTrinh))).ToList().GroupBy(x => new
                                {
                                    x.STT,
                                    x.IdLoaiCongTrinh,
                                    x.IdLoaiCongTrinhParent,
                                    x.SMaLoaiCongTrinh,
                                    x.STenDuAn,
                                    x.STenDonVi,
                                    x.SDiaDiem,
                                    x.SThoiGianThucHien,
                                    x.SGhiChu,
                                    x.Loai,
                                    x.LoaiParent,
                                    x.IsHangCha
                                }).Select(x => new VdtKhvKeHoach5NamDeXuatReportQuery()
                                {
                                    STT = x.Key.STT,
                                    IdLoaiCongTrinh = x.Key.IdLoaiCongTrinh,
                                    IdLoaiCongTrinhParent = x.Key.IdLoaiCongTrinhParent,
                                    SMaLoaiCongTrinh = x.Key.SMaLoaiCongTrinh,
                                    STenDuAn = x.Key.STenDuAn,
                                    STenDonVi = x.Key.STenDonVi,
                                    SDiaDiem = x.Key.SDiaDiem,
                                    SThoiGianThucHien = x.Key.SThoiGianThucHien,
                                    FHanMucDauTu = x.Sum(rpt => rpt.FHanMucDauTu),
                                    FTongSoNhuCau = x.Sum(rpt => rpt.FTongSoNhuCau),
                                    FTongSo = x.Sum(rpt => rpt.FTongSo),
                                    FGiaTriNamThuNhat = x.Sum(rpt => rpt.FGiaTriNamThuNhat),
                                    FGiaTriNamThuHai = x.Sum(rpt => rpt.FGiaTriNamThuHai),
                                    FGiaTriNamThuBa = x.Sum(rpt => rpt.FGiaTriNamThuBa),
                                    FGiaTriNamThuTu = x.Sum(rpt => rpt.FGiaTriNamThuTu),
                                    FGiaTriNamThuNam = x.Sum(rpt => rpt.FGiaTriNamThuNam),
                                    FGiaTriBoTri = x.Sum(rpt => rpt.FGiaTriBoTri),
                                    SGhiChu = x.Key.SGhiChu,
                                    FHanMucDauTuQP = x.Sum(rpt => rpt.FHanMucDauTuQP),
                                    FHanMucDauTuNN = x.Sum(rpt => rpt.FHanMucDauTuNN),
                                    FHanMucDauTuDP = x.Sum(rpt => rpt.FHanMucDauTuDP),
                                    FHanMucDauTuOrther = x.Sum(rpt => rpt.FHanMucDauTuOrther),
                                    LuyKeVonNSQPDaBoTri = x.Sum(rpt => rpt.LuyKeVonNSQPDaBoTri),
                                    LuyKeVonNSQPDeNghiBoTri = x.Sum(rpt => rpt.LuyKeVonNSQPDeNghiBoTri),
                                    TongLuyKe = x.Sum(rpt => rpt.TongLuyKe ?? 0),
                                    Loai = x.Key.Loai,
                                    LoaiParent = x.Key.LoaiParent,
                                    IsHangCha = x.Key.IsHangCha,
                                    SSoQuyetDinhNgayQuyetDinh = x.FirstOrDefault() != null ? x.FirstOrDefault().SSoQuyetDinhNgayQuyetDinh : string.Empty,
                                    iID_KeHoach5NamID = x.FirstOrDefault() != null ? x.FirstOrDefault().iID_KeHoach5NamID : Guid.Empty,
                                    IdMaDonViQuanLy = x.Any() ? x.First().IdMaDonViQuanLy : string.Empty
                                }).ToList();

                ItemsReportDeXuat = new ObservableCollection<VdtKhvKeHoach5NamDeXuatReportQuery>(listGroup);
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        private void CalculateParentDeXuat(VdtKhvKeHoach5NamDeXuatReportQuery currentItem, VdtKhvKeHoach5NamDeXuatReportQuery seftItem)
        {
            var parrentItem = ItemsReportDeXuat.Where(x => x.IdLoaiCongTrinh == currentItem.IdLoaiCongTrinhParent).FirstOrDefault();
            if (parrentItem == null) return;
            parrentItem.FHanMucDauTu += seftItem.FHanMucDauTu;
            parrentItem.FTongSoNhuCau += seftItem.FTongSoNhuCau;
            parrentItem.FTongSo += seftItem.FTongSo;
            parrentItem.FGiaTriNamThuNhat += seftItem.FGiaTriNamThuNhat;
            parrentItem.FGiaTriNamThuHai += seftItem.FGiaTriNamThuHai;
            parrentItem.FGiaTriNamThuBa += seftItem.FGiaTriNamThuBa;
            parrentItem.FGiaTriNamThuTu += seftItem.FGiaTriNamThuTu;
            parrentItem.FGiaTriNamThuNam += seftItem.FGiaTriNamThuNam;
            parrentItem.FGiaTriBoTri += seftItem.FGiaTriBoTri;
            parrentItem.TongLuyKe += seftItem.TongLuyKe;
            parrentItem.LuyKeVonNSQPDaBoTri += seftItem.LuyKeVonNSQPDaBoTri;
            parrentItem.LuyKeVonNSQPDeNghiBoTri += seftItem.LuyKeVonNSQPDeNghiBoTri;
            parrentItem.FHanMucDauTuQP += seftItem.FHanMucDauTuQP;
            parrentItem.FHanMucDauTuNN += seftItem.FHanMucDauTuNN;
            parrentItem.FHanMucDauTuDP += seftItem.FHanMucDauTuDP;
            parrentItem.FHanMucDauTuOrther += seftItem.FHanMucDauTuOrther;
            parrentItem.LuyKeVonNSQPDeNghiBoTri += seftItem.LuyKeVonNSQPDeNghiBoTri;
            parrentItem.LuyKeVonNSQPDaBoTri += seftItem.LuyKeVonNSQPDaBoTri;
            CalculateParentDeXuat(parrentItem, seftItem);
        }

        private void HandleDataDeXuatDieuChinh()
        {
            try
            {
                if (DrpReportTypeSelected != null && DrpReportTypeSelected.ValueItem.Equals(RPT_TONGHOP))
                {
                    ItemsReportDeXuatDieuChinh.Select(x => { x.STT = (ItemsReportDeXuatDieuChinh.IndexOf(x) + 1).ToString(); return x; }).ToList();
                }
                else
                {
                    List<VdtKhvKeHoach5NamDeXuatDieuChinhReportQuery> lstDv = ItemsReportDeXuatDieuChinh.Where(x => x.IsHangCha).ToList();
                    lstDv.Select(x => { x.STT = (lstDv.IndexOf(x) + 1).ToString(); return x; }).ToList();
                    lstDv.Select(item =>
                    {
                        List<VdtKhvKeHoach5NamDeXuatDieuChinhReportQuery> lstChildrent = ItemsReportDeXuatDieuChinh.Where(x => !x.IsHangCha && x.IdDonViQuanLy == item.IdDonViQuanLy).ToList();
                        lstChildrent.Select(x => { x.STT = (string.Format("{0}.{1}", item.STT, lstChildrent.IndexOf(x) + 1)).ToString(); return x; }).ToList();
                        return item;
                    }).ToList();
                }

                var listGroup = ItemsReportDeXuatDieuChinh.Where(x => x.FHanMucDauTuDuocDuyet != 0 || x.FTongSoDuocDuyet != 0 || x.FVonBoTriTuNamDenNamDuocDuyet != 0
                || x.FVonBoTriSauNamDuocDuyet != 0 || x.FHanMucDauTuDeXuat != 0 || x.FTongSoDeXuat != 0 || x.FTongCongDeXuat != 0 || x.FGiaTriNamThuNhatDeXuat != 0
                || x.FGiaTriNamThuHaiDeXuat != 0 || x.FGiaTriNamThuBaDeXuat != 0 || x.FGiaTriNamThuTuDeXuat != 0 || x.FGiaTriNamThuNamDeXuat != 0 
                || x.FGiaTriSauNamDeXuat != 0 || x.FHanMucDauTuChenhLech != 0 || x.FTongSoChenhLech != 0 || x.FVonBoTriTuNamDenNamChenhLech != 0 
                || x.FVonBoTriSauNamChenhLech != 0).ToList().GroupBy(x => new
                {
                    x.STenDuAn,
                    x.IdDonViQuanLy,
                    x.SGhiChu,
                    x.Loai,
                    x.IsHangCha
                }).Select(x => new VdtKhvKeHoach5NamDeXuatDieuChinhReportQuery()
                {
                    STT = null,
                    STenDuAn = x.Key.STenDuAn,
                    IdDonViQuanLy = x.Key.IdDonViQuanLy,
                    SGhiChu = x.Key.SGhiChu,
                    Loai = x.Key.Loai,
                    IsHangCha = x.Key.IsHangCha,
                    FHanMucDauTuDuocDuyet = x.Sum(rpt => rpt.FHanMucDauTuDuocDuyet),
                    FTongSoDuocDuyet = x.Sum(rpt => rpt.FTongSoDuocDuyet),
                    FVonBoTriTuNamDenNamDuocDuyet = x.Sum(rpt => rpt.FVonBoTriTuNamDenNamDuocDuyet),
                    FVonBoTriSauNamDuocDuyet = x.Sum(rpt => rpt.FVonBoTriSauNamDuocDuyet),
                    FHanMucDauTuDeXuat = x.Sum(rpt => rpt.FHanMucDauTuDeXuat),
                    FTongSoDeXuat = x.Sum(rpt => rpt.FTongSoDeXuat),
                    FTongCongDeXuat = x.Sum(rpt => rpt.FTongCongDeXuat),
                    FGiaTriNamThuNhatDeXuat = x.Sum(rpt => rpt.FGiaTriNamThuNhatDeXuat),
                    FGiaTriNamThuHaiDeXuat = x.Sum(rpt => rpt.FGiaTriNamThuHaiDeXuat),
                    FGiaTriNamThuBaDeXuat = x.Sum(rpt => rpt.FGiaTriNamThuBaDeXuat),
                    FGiaTriNamThuTuDeXuat = x.Sum(rpt => rpt.FGiaTriNamThuTuDeXuat),
                    FGiaTriNamThuNamDeXuat = x.Sum(rpt => rpt.FGiaTriNamThuNamDeXuat),
                    FGiaTriSauNamDeXuat = x.Sum(rpt => rpt.FGiaTriSauNamDeXuat),
                    FHanMucDauTuChenhLech = x.Sum(rpt => rpt.FHanMucDauTuChenhLech),
                    FTongSoChenhLech = x.Sum(rpt => rpt.FTongSoChenhLech),
                    FVonBoTriTuNamDenNamChenhLech = x.Sum(rpt => rpt.FVonBoTriTuNamDenNamChenhLech),
                    FVonBoTriSauNamChenhLech = x.Sum(rpt => rpt.FVonBoTriSauNamChenhLech)
                });                

                ItemsReportDeXuatDieuChinh = new ObservableCollection<VdtKhvKeHoach5NamDeXuatDieuChinhReportQuery>(listGroup);

                SetSTTDataDieuChinh();
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        private void SetSTTDataDieuChinh()
        {
            try
            {
                List<VdtKhvKeHoach5NamDeXuatDieuChinhReportQuery> lstItemLevel = ItemsReportDeXuatDieuChinh.Where(x => x.IsHangCha).ToList();
                var dctItemLevel = lstItemLevel.GroupBy(x => x.IdDonViQuanLy).ToDictionary(x => x.Key, x => x.ToList());
                dctItemLevel.Keys.Select(x =>
                {
                    List<VdtKhvKeHoach5NamDeXuatDieuChinhReportQuery> lstItemStt = dctItemLevel[x].ToList();
                    lstItemStt.Select(x => { x.STT = (lstItemStt.IndexOf(x) + 1).ToString(); return x; }).ToList();
                    return x;
                }).ToList();            

                List<VdtKhvKeHoach5NamDeXuatDieuChinhReportQuery> lstParentDonVi = ItemsReportDeXuatDieuChinh.Where(x => x.IsHangCha).ToList();
                lstParentDonVi.Select(x => { x.STT = (lstParentDonVi.IndexOf(x) + 1).ToString(); return x; }).ToList();
                foreach (var item in lstParentDonVi)
                {
                    List<VdtKhvKeHoach5NamDeXuatDieuChinhReportQuery> lstChildrent = ItemsReportDeXuatDieuChinh.Where(x => !x.IsHangCha && x.IdDonViQuanLy == item.IdDonViQuanLy).ToList();
                    lstChildrent.Select(x =>
                    {
                        x.STT = string.Format("{0}.{1}", item.STT, (lstChildrent.IndexOf(x) + 1).ToString());
                        return x;
                    }).ToList();
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        private void CalculateDataReport()
        {
            try
            {
                if (_drpReportTypeSelected != null && _drpReportTypeSelected.ValueItem.Equals("1"))
                {
                    List<VdtKhvKeHoach5NamReportQuery> childrent = ItemsReport.Where(x => !x.IsHangCha).ToList();
                    List<VdtKhvKeHoach5NamReportQuery> parent = ItemsReport.Where(x => x.IsHangCha && (x.LoaiParent.Equals(0) || x.LoaiParent.Equals(1))).ToList();

                    ItemsReport.Where(x => x.IsHangCha && x.LoaiParent.Equals(1)).Select(x =>
                    {
                        x.FHanMucDauTu = 0;
                        x.FGiaTriKeHoach = 0;
                        x.FVonDaGiao = 0;
                        x.FVonBoTri = 0;
                        x.FTongVonBoTri = 0;
                        return x;
                    }).ToList();

                    foreach (var pr in parent)
                    {
                        List<VdtKhvKeHoach5NamReportQuery> lstChilrent = childrent.Where(x => x.IdLoaiCongTrinh.Equals(pr.IdLoaiCongTrinh)).ToList();
                        foreach (var item in lstChilrent.Where(x => (x.FHanMucDauTu != 0 || x.FGiaTriKeHoach != 0 || x.FVonDaGiao != 0 || x.FVonBoTri != 0 || x.FTongVonBoTri != 0)))
                        {
                            pr.FHanMucDauTu += item.FHanMucDauTu ?? 0;
                            pr.FGiaTriKeHoach += item.FGiaTriKeHoach ?? 0;
                            pr.FVonDaGiao += item.FVonDaGiao ?? 0;
                            pr.FVonBoTri += item.FVonBoTri ?? 0;
                            pr.FTongVonBoTri += item.FTongVonBoTri ?? 0;
                        }
                    }

                    foreach (var item in parent.Where(x => (x.FHanMucDauTu != 0 || x.FGiaTriKeHoach != 0 || x.FVonDaGiao != 0 || x.FVonBoTri != 0 || x.FTongVonBoTri != 0)))
                    {
                        CalculateParent(item, item);
                    }
                }
                else
                {
                    List<VdtKhvKeHoach5NamReportQuery> lstDonViparent = ItemsReport.Where(x => x.IsHangCha && x.LoaiParent.Equals(2)).ToList();
                    List<VdtKhvKeHoach5NamReportQuery> lstLoaiCongTrinhparent = ItemsReport.Where(x => x.IsHangCha && x.LoaiParent.Equals(1)).ToList();

                    ItemsReport.Where(x => x.IsHangCha && x.LoaiParent.Equals(1)).Select(x =>
                    {
                        x.FHanMucDauTu = 0;
                        x.FGiaTriKeHoach = 0;
                        x.FVonDaGiao = 0;
                        x.FVonBoTri = 0;
                        x.FTongVonBoTri = 0;
                        return x;
                    }).ToList();

                    foreach (var pr in lstLoaiCongTrinhparent)
                    {
                        List<VdtKhvKeHoach5NamReportQuery> lstChilrent = lstDonViparent.Where(x => x.IdLoaiCongTrinh.Equals(pr.IdLoaiCongTrinh)).ToList();
                        foreach (var item in lstChilrent.Where(x => (x.FHanMucDauTu != 0 || x.FGiaTriKeHoach != 0 || x.FVonDaGiao != 0 || x.FVonBoTri != 0 || x.FTongVonBoTri != 0)))
                        {
                            pr.FHanMucDauTu += item.FHanMucDauTu ?? 0;
                            pr.FGiaTriKeHoach += item.FGiaTriKeHoach ?? 0;
                            pr.FVonDaGiao += item.FVonDaGiao ?? 0;
                            pr.FVonBoTri += item.FVonBoTri ?? 0;
                            pr.FTongVonBoTri += item.FTongVonBoTri ?? 0;
                        }
                    }

                    foreach (var item in lstLoaiCongTrinhparent.Where(x => (x.FHanMucDauTu != 0 || x.FGiaTriKeHoach != 0 || x.FVonDaGiao != 0 || x.FVonBoTri != 0 || x.FTongVonBoTri != 0)))
                    {
                        CalculateParent(item, item);
                    }
                }

                List<VdtKhvKeHoach5NamReportQuery> lstDataChildrent = ItemsReport.Where(x => (x.FHanMucDauTu != 0 || x.FGiaTriKeHoach != 0
                                        || x.FVonDaGiao != 0 || x.FVonBoTri != 0 || x.FTongVonBoTri != 0)).ToList();

                List<VdtKhvKeHoach5NamReportQuery> lstDataParent = ItemsReport.Where(x => x.LoaiParent.Equals(0)
                            && lstDataChildrent.Select(x => x.IdLoaiCongTrinh).ToList().Contains(x.IdLoaiCongTrinh)).Select(x =>
                            {
                                x.FHanMucDauTu = 0;
                                x.FGiaTriKeHoach = 0;
                                x.FVonDaGiao = 0;
                                x.FVonBoTri = 0;
                                x.FTongVonBoTri = 0;
                                return x;
                            }).ToList();

                foreach (var pr in lstDataParent)
                {
                    foreach (var item in ItemsReport.Where(x => x.IsHangCha && x.LoaiParent.Equals(2) && (x.IdLoaiCongTrinh.Equals(pr.IdLoaiCongTrinh) || x.IdLoaiCongTrinhParent.Equals(pr.IdLoaiCongTrinh)) && (x.FHanMucDauTu != 0 || x.FGiaTriKeHoach != 0 || x.FVonDaGiao != 0 || x.FVonBoTri != 0 || x.FTongVonBoTri != 0)))
                    {
                        pr.FHanMucDauTu += item.FHanMucDauTu ?? 0;
                        pr.FGiaTriKeHoach += item.FGiaTriKeHoach ?? 0;
                        pr.FVonDaGiao += item.FVonDaGiao ?? 0;
                        pr.FVonBoTri += item.FVonBoTri ?? 0;
                        pr.FTongVonBoTri += item.FTongVonBoTri ?? 0;
                    }
                }

                List<VdtKhvKeHoach5NamReportQuery> listGroup = ItemsReport.Where(x => (x.FHanMucDauTu != 0 || x.FGiaTriKeHoach != 0
                                        || x.FVonDaGiao != 0 || x.FVonBoTri != 0 || x.FTongVonBoTri != 0
                                        || lstDataParent.Select(y => y.IdLoaiCongTrinh).ToList().Contains(x.IdLoaiCongTrinh))).ToList().GroupBy(x => new
                                        {
                                            x.IdLoaiCongTrinh,
                                            x.IdLoaiCongTrinhParent,
                                            x.Loai,
                                            x.LoaiParent,
                                            x.STenDuAn,
                                            x.GhiChu,
                                            x.IsHangCha,
                                            x.STT
                                        }).Select(x => new VdtKhvKeHoach5NamReportQuery()
                                        {
                                            STT = x.Key.STT,
                                            IsHangCha = x.Key.IsHangCha,
                                            GhiChu = x.Key.GhiChu,
                                            STenDuAn = x.Key.STenDuAn,
                                            LoaiParent = x.Key.LoaiParent,
                                            Loai = x.Key.Loai,
                                            IdLoaiCongTrinhParent = x.Key.IdLoaiCongTrinhParent,
                                            IdLoaiCongTrinh = x.Key.IdLoaiCongTrinh,
                                            FHanMucDauTu = x.Sum(rpt => rpt.FHanMucDauTu),
                                            FVonDaGiao = x.Sum(rpt => rpt.FVonDaGiao),
                                            FTongVonBoTri = x.Sum(rpt => rpt.FTongVonBoTri),
                                            FGiaTriKeHoach = x.Sum(rpt => rpt.FGiaTriKeHoach),
                                            FVonBoTri = x.Sum(rpt => rpt.FVonBoTri)
                                        }).ToList();
                ItemsReport = new ObservableCollection<VdtKhvKeHoach5NamReportQuery>(listGroup);
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        private void CalculateParent(VdtKhvKeHoach5NamReportQuery currentItem, VdtKhvKeHoach5NamReportQuery seftItem)
        {
            var parrentItem = ItemsReport.Where(x => x.IdLoaiCongTrinh == currentItem.IdLoaiCongTrinhParent).FirstOrDefault();
            if (parrentItem == null) return;
            parrentItem.FHanMucDauTu += seftItem.FHanMucDauTu;
            parrentItem.FGiaTriKeHoach += seftItem.FGiaTriKeHoach;
            parrentItem.FVonDaGiao += seftItem.FVonDaGiao;
            parrentItem.FVonBoTri += seftItem.FVonBoTri;
            parrentItem.FTongVonBoTri += seftItem.FTongVonBoTri;
            CalculateParent(parrentItem, seftItem);
        }

        private void CalculateDataReportChuyenTiep()
        {
            try
            {
                List<VdtKhvKeHoach5NamChuyenTiepReportQuery> childrent = ItemsReportChuyenTiep.Where(x => !x.IsHangCha.Value).ToList();
                List<VdtKhvKeHoach5NamChuyenTiepReportQuery> parent = ItemsReportChuyenTiep.Where(x => x.IsHangCha.Value).ToList();
                List<VdtKhvKeHoach5NamChuyenTiepReportQuery> result = new List<VdtKhvKeHoach5NamChuyenTiepReportQuery>();

                parent.Select(x =>
                {
                    x.TongMucDauTu = 0;
                    x.TongMucDauTuNSQP = 0;
                    x.ChiPhiDuPhong = 0;
                    x.TongSo = 0;
                    x.VonBoTriHetNam = 0;
                    x.TongMucDauTuPhaiBoTri = 0;
                    x.KeHoachVonNamDoDuyet = 0;
                    x.KeHoachVonDeNghiBoTriNam = 0;
                    x.ChenhLechSoVoiQuyetDinhBo = 0;

                    return x;
                }).ToList();

                foreach (var pr in parent)
                {
                    List<VdtKhvKeHoach5NamChuyenTiepReportQuery> lstChilrent = childrent.Where(x => x.IIdMaDonVi.Equals(pr.IIdMaDonVi)).ToList();
                    foreach (var item in lstChilrent.Where(x => (x.TongMucDauTu != 0 || x.TongMucDauTuNSQP != 0
                                || x.ChiPhiDuPhong != 0 || x.TongSo != 0 || x.VonBoTriHetNam != 0 || x.TongMucDauTuPhaiBoTri != 0 || x.KeHoachVonNamDoDuyet != 0
                                || x.KeHoachVonDeNghiBoTriNam != 0 || x.ChenhLechSoVoiQuyetDinhBo != 0)))
                    {
                        pr.TongMucDauTu += item.TongMucDauTu;
                        pr.TongMucDauTuNSQP += item.TongMucDauTuNSQP;
                        pr.ChiPhiDuPhong += item.ChiPhiDuPhong;
                        pr.TongSo += item.TongSo;
                        pr.VonBoTriHetNam += item.VonBoTriHetNam;
                        pr.TongMucDauTuPhaiBoTri += item.TongMucDauTuPhaiBoTri;
                        pr.KeHoachVonNamDoDuyet += item.KeHoachVonNamDoDuyet;
                        pr.KeHoachVonDeNghiBoTriNam += item.KeHoachVonDeNghiBoTriNam;
                        pr.ChenhLechSoVoiQuyetDinhBo += item.ChenhLechSoVoiQuyetDinhBo;
                    }
                }

                ItemsReportChuyenTiep = new ObservableCollection<VdtKhvKeHoach5NamChuyenTiepReportQuery>(ItemsReportChuyenTiep.Where(x => (x.TongMucDauTu != 0 || x.TongMucDauTuNSQP != 0
                                || x.ChiPhiDuPhong != 0 || x.TongSo != 0 || x.VonBoTriHetNam != 0 || x.TongMucDauTuPhaiBoTri != 0 || x.KeHoachVonNamDoDuyet != 0
                                || x.KeHoachVonDeNghiBoTriNam != 0 || x.ChenhLechSoVoiQuyetDinhBo != 0)).ToList());

            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        private void HandleDataReport()
        {
            try
            {
                List<VdtKhvKeHoach5NamReportQuery> lstItem = ItemsReport.Where(n => n.LoaiParent.Equals(0)).ToList();
                lstItem.Select(n => { n.STT = RomanNumber.ToRoman((lstItem.IndexOf(n) + 1)).ToString(); return n; }).ToList();

                List<VdtKhvKeHoach5NamReportQuery> lstItemLevel = ItemsReport.Where(x => x.IdLoaiCongTrinhParent != null && x.IsHangCha && x.LoaiParent.Equals(1)).ToList();
                var dctItemLevel = lstItemLevel.GroupBy(x => x.IdLoaiCongTrinhParent).ToDictionary(x => x.Key, x => x.ToList());
                foreach (var key in dctItemLevel.Keys)
                {
                    List<VdtKhvKeHoach5NamReportQuery> lstItemStt = dctItemLevel[key].ToList();
                    lstItemStt.Select(x => { x.STT = (lstItemStt.IndexOf(x) + 1).ToString(); return x; }).ToList();
                }

                if (_drpReportTypeSelected.ValueItem.Equals("1"))
                {
                    foreach (var item in lstItemLevel)
                    {
                        List<VdtKhvKeHoach5NamReportQuery> lstChildrent = ItemsReport.Where(x => x.IdLoaiCongTrinh == item.IdLoaiCongTrinh && !x.IsHangCha).ToList();
                        lstChildrent.Select(x => { x.STT = string.Format("{0}.{1}", item.STT, (lstChildrent.IndexOf(x) + 1).ToString()); return x; }).ToList();
                    }

                    List<VdtKhvKeHoach5NamReportQuery> lstLctParent = ItemsReport.Where(x => x.LoaiParent.Equals(0)).ToList();
                    var dctItemParent = ItemsReport.Where(x => !x.IsHangCha && x.IdLoaiCongTrinh.HasValue
                        && lstLctParent.Select(y => y.IdLoaiCongTrinh).Contains(x.IdLoaiCongTrinh)).GroupBy(z => z.IdLoaiCongTrinh).ToDictionary(z => z.Key.ToString(), z => z.ToList());
                    foreach (var item in dctItemParent.Keys)
                    {
                        List<VdtKhvKeHoach5NamReportQuery> itemStt = dctItemParent[item];
                        itemStt.Select(x => { x.STT = string.Format("{0}", (itemStt.IndexOf(x) + 1).ToString()); return x; }).ToList();
                    }
                }
                else
                {
                    foreach (var item in lstItemLevel)
                    {
                        List<VdtKhvKeHoach5NamReportQuery> lstChildrent = ItemsReport.Where(x => x.IdLoaiCongTrinh == item.IdLoaiCongTrinh && x.IsHangCha && x.LoaiParent.Equals(2)).ToList();
                        lstChildrent.Select(x => { x.STT = string.Format("{0}.{1}", item.STT, (lstChildrent.IndexOf(x) + 1).ToString()); return x; }).ToList();
                    }

                    foreach (var item in ItemsReport.Where(x => x.IsHangCha && x.LoaiParent.Equals(2)))
                    {
                        List<VdtKhvKeHoach5NamReportQuery> lstChildrent = ItemsReport.Where(x => x.IdLoaiCongTrinh == item.IdLoaiCongTrinh && !x.IsHangCha && x.LoaiParent.Equals(2)).ToList();
                        lstChildrent.Select(x =>
                        {
                            if (string.IsNullOrEmpty(item.STT))
                            {
                                x.STT = string.Format("{0}", (lstChildrent.IndexOf(x) + 1).ToString());
                            }
                            else
                            {
                                x.STT = string.Format("{0}.{1}", item.STT, (lstChildrent.IndexOf(x) + 1).ToString());
                            }
                            return x;
                        }).ToList();
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        private Dictionary<string, string> GetMaDonVi()
        {
            try
            {
                Dictionary<string, string> dctDonVi = new Dictionary<string, string>();
                if (DrpReportTypeSelected != null && DrpReportTypeSelected.ValueItem.Equals(RPT_TONGHOP))
                {
                    if (!dctDonVi.ContainsKey(_drpReportTypeSelected.ValueItem))
                    {
                        dctDonVi.Add(_drpReportTypeSelected.ValueItem, _drpReportTypeSelected.DisplayItem);
                    }
                }
                else
                {
                    var lstDvSelected = DataUnit.Where(x => x.IsChecked).Select(x => new ComboboxItem() { ValueItem = x.ValueItem, DisplayItem = x.DisplayItem }).ToList();
                    lstDvSelected.Select(item =>
                    {
                        if (!dctDonVi.ContainsKey(item.ValueItem))
                        {
                            dctDonVi.Add(item.ValueItem, item.DisplayItem);
                        }
                        return item;
                    }).ToList();
                }

                return dctDonVi;
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
                return new Dictionary<string, string>();
            }
        }

        private StringBuilder AuthorizationReport()
        {
            StringBuilder sError = new StringBuilder();
            Dictionary<string, string> dctMaDonVi = GetMaDonVi();
            var lstUnitManager = _sessionService.Current.IdsDonViQuanLy;
            List<string> lstDv = new List<string>();
            if (lstUnitManager.Contains(","))
            {
                lstDv = lstUnitManager.Split(",").ToList();
            }
            else
            {
                lstDv.Add(lstUnitManager);
            }

            dctMaDonVi.Keys.Select(x =>
            {
                if (!lstDv.Contains(x))
                {
                    var itemUnit = dctMaDonVi[x];
                    sError.AppendLine(itemUnit);
                }
                return x;
            }).ToList();
            return sError;
        }

        private void OnPrintReportDeXuat(ExportType exportType)
        {
            try
            {
                StringBuilder sError = Validation();
                if (sError.Length != 0)
                {
                    MessageBox.Show(sError.ToString());
                    return;
                }
                sError = AuthorizationReport();
                List<DonVi> userAgency = _nsDonViService.FindByUser(_sessionService.Current.Principal, _sessionService.Current.YearOfWork, LoaiDonVi.ROOT);
                if (!userAgency.Any(x => x.Loai == LoaiDonVi.ROOT) && sError.Length != 0)
                {
                    MessageBox.Show(string.Format(Resources.UserManagerKHTHReportWarning, _sessionService.Current.Principal, sError.ToString()));
                    return;
                }

                Dictionary<string, object> data = new Dictionary<string, object>();
                var predicate = CreatePredicateDeXuat();
                var itemQuery = _vdtKhvKeHoach5NamDeXuat.FindByCondition(predicate).ToList();
                if (itemQuery == null || itemQuery.Count == 0)
                {
                    MessageBox.Show(Resources.MsgErrorDataNotFound);
                    return;
                }

                BackgroundWorkerHelper.Run((s, e) =>
                {
                    IsLoading = true;

                    List<VdtKhvKeHoach5NamDeXuatModel> item = _mapper.Map<List<VdtKhvKeHoach5NamDeXuatModel>>(itemQuery);
                    List<string> sLoaiCongTrinh = new List<string>();
                    if (_drpLoaiCongTrinhSelected.ValueItem.Equals("1"))
                    {
                        sLoaiCongTrinh = _drpLoaiCongTrinh.Where(x => !x.ValueItem.Equals("1")).Select(x => x.ValueItem).ToList();
                    }
                    else
                    {
                        sLoaiCongTrinh.Add(_drpLoaiCongTrinhSelected.ValueItem);
                    }

                    string lstLoaiCongTrinh = string.Join(",", sLoaiCongTrinh.ToList()).ToString();
                    string lstId = string.Join(",", item.Select(x => x.Id.ToString()).ToList());
                    string lstBudget = string.Join(",", DataBudget.Where(x => x.IsChecked).Select(x => x.ValueItem.ToString()).ToList());

                    List<VdtKhvKeHoach5NamDeXuatReportQuery> lstData = _vdtKhvKeHoach5NamChiTietDexuatService.FindByReportKeHoachTrungHanDeXuat(lstId, lstLoaiCongTrinh,
                        lstBudget, int.Parse(_drpReportTypeSelected.ValueItem), Double.Parse(DrpDonViTinhSelected.ValueItem), _sessionService.Current.YearOfWork).ToList();
                    ItemsReportDeXuat = new ObservableCollection<VdtKhvKeHoach5NamDeXuatReportQuery>();
                    //foreach(var itemDeXuat in lstData)
                    //{
                    //    ItemsReportDeXuat.Add(itemDeXuat);
                    //}
                    ItemsReportDeXuat = new ObservableCollection<VdtKhvKeHoach5NamDeXuatReportQuery>(lstData);

                    HandleDataHanMucDauTu();
                    CalculateDataReportDeXuat();
                    HandleDataReportDeXuat();

                    VdtKhvKeHoach5NamDeXuatReportQuery dataSummary = new VdtKhvKeHoach5NamDeXuatReportQuery();
                    if (_drpReportTypeSelected != null && _drpReportTypeSelected.ValueItem.Equals("1"))
                    {
                        dataSummary.FHanMucDauTu = ItemsReportDeXuat.Where(x => x.LoaiParent.Equals(0)).Sum(x => x.FHanMucDauTu);
                        dataSummary.FHanMucDauTuQP = ItemsReportDeXuat.Where(x => x.LoaiParent.Equals(0)).Sum(x => x.FHanMucDauTuQP);
                        dataSummary.FHanMucDauTuNN = ItemsReportDeXuat.Where(x => x.LoaiParent.Equals(0)).Sum(x => x.FHanMucDauTuNN);
                        dataSummary.FHanMucDauTuDP = ItemsReportDeXuat.Where(x => x.LoaiParent.Equals(0)).Sum(x => x.FHanMucDauTuDP);
                        dataSummary.FHanMucDauTuOrther = ItemsReportDeXuat.Where(x => x.LoaiParent.Equals(0)).Sum(x => x.FHanMucDauTuOrther);
                        dataSummary.FTongSoNhuCau = ItemsReportDeXuat.Where(x => x.LoaiParent.Equals(0)).Sum(x => x.FTongSoNhuCau);
                        dataSummary.FGiaTriNamThuNhat = ItemsReportDeXuat.Where(x => x.LoaiParent.Equals(0)).Sum(x => x.FGiaTriNamThuNhat);
                        dataSummary.FGiaTriNamThuHai = ItemsReportDeXuat.Where(x => x.LoaiParent.Equals(0)).Sum(x => x.FGiaTriNamThuHai);
                        dataSummary.FGiaTriNamThuBa = ItemsReportDeXuat.Where(x => x.LoaiParent.Equals(0)).Sum(x => x.FGiaTriNamThuBa);
                        dataSummary.FGiaTriNamThuTu = ItemsReportDeXuat.Where(x => x.LoaiParent.Equals(0)).Sum(x => x.FGiaTriNamThuTu);
                        dataSummary.FGiaTriNamThuNam = ItemsReportDeXuat.Where(x => x.LoaiParent.Equals(0)).Sum(x => x.FGiaTriNamThuNam);
                        dataSummary.FGiaTriBoTri = ItemsReportDeXuat.Where(x => x.LoaiParent.Equals(0)).Sum(x => x.FGiaTriBoTri);
                        dataSummary.FTongSo = ItemsReportDeXuat.Where(x => x.LoaiParent.Equals(0)).Sum(x => x.FTongSo);
                    }
                    else
                    {
                        dataSummary.FHanMucDauTu = ItemsReportDeXuat.Where(x => x.IsHangCha && x.LoaiParent.Equals(2)).Sum(x => x.FHanMucDauTu);
                        dataSummary.FHanMucDauTuQP = ItemsReportDeXuat.Where(x => x.IsHangCha && x.LoaiParent.Equals(2)).Sum(x => x.FHanMucDauTuQP);
                        dataSummary.FHanMucDauTuNN = ItemsReportDeXuat.Where(x => x.IsHangCha && x.LoaiParent.Equals(2)).Sum(x => x.FHanMucDauTuNN);
                        dataSummary.FHanMucDauTuDP = ItemsReportDeXuat.Where(x => x.IsHangCha && x.LoaiParent.Equals(2)).Sum(x => x.FHanMucDauTuDP);
                        dataSummary.FHanMucDauTuOrther = ItemsReportDeXuat.Where(x => x.IsHangCha && x.LoaiParent.Equals(2)).Sum(x => x.FHanMucDauTuOrther);
                        dataSummary.FTongSoNhuCau = ItemsReportDeXuat.Where(x => x.IsHangCha && x.LoaiParent.Equals(2)).Sum(x => x.FTongSoNhuCau);
                        dataSummary.FGiaTriNamThuNhat = ItemsReportDeXuat.Where(x => x.IsHangCha && x.LoaiParent.Equals(2)).Sum(x => x.FGiaTriNamThuNhat);
                        dataSummary.FGiaTriNamThuHai = ItemsReportDeXuat.Where(x => x.IsHangCha && x.LoaiParent.Equals(2)).Sum(x => x.FGiaTriNamThuHai);
                        dataSummary.FGiaTriNamThuBa = ItemsReportDeXuat.Where(x => x.IsHangCha && x.LoaiParent.Equals(2)).Sum(x => x.FGiaTriNamThuBa);
                        dataSummary.FGiaTriNamThuTu = ItemsReportDeXuat.Where(x => x.IsHangCha && x.LoaiParent.Equals(2)).Sum(x => x.FGiaTriNamThuTu);
                        dataSummary.FGiaTriNamThuNam = ItemsReportDeXuat.Where(x => x.IsHangCha && x.LoaiParent.Equals(2)).Sum(x => x.FGiaTriNamThuNam);
                        dataSummary.FGiaTriBoTri = ItemsReportDeXuat.Where(x => x.IsHangCha && x.LoaiParent.Equals(2)).Sum(x => x.FGiaTriBoTri);
                        dataSummary.FTongSo = ItemsReportDeXuat.Where(x => x.IsHangCha && x.LoaiParent.Equals(2)).Sum(x => x.FTongSo);
                    }

                    string sMuc = string.Join("+", ItemsReportDeXuat.Where(x => x.LoaiParent.Equals(0)).Select(x => x.STT).ToList());


                    string sTenDonVi = GetNameUnit();
                    if (!string.IsNullOrEmpty(sTenDonVi))
                    {
                        sTenDonVi = sTenDonVi.ToUpper();
                    }
                    data.Add("CapTren", GetParentUnit());
                    data.Add("Period", string.Format("{0}-{1}", SNamBatDau, INamKetThuc));
                    data.Add("Year1", SNamBatDau);
                    data.Add("Year2", (Int32.Parse(SNamBatDau) + 1).ToString());
                    data.Add("Year3", (Int32.Parse(SNamBatDau) + 2).ToString());
                    data.Add("Year4", (Int32.Parse(SNamBatDau) + 3).ToString());
                    data.Add("Year5", (Int32.Parse(SNamBatDau) + 4).ToString());
                    data.Add("TenDonVi", sTenDonVi);
                    data.Add("iNamLamViec", _sessionService.Current.YearOfWork);
                    data.Add("Items", ItemsReportDeXuat);
                    data.Add("Header1", TxtHeader1);
                    data.Add("Header2", TxtHeader2);
                    data.Add("Header3", TxtHeader3);
                    data.Add("DonViTinh", DrpDonViTinhSelected.DisplayItem);
                    data.Add("Muc", sMuc);
                    data.Add("FHanMucDauTuSum", dataSummary.FHanMucDauTu);
                    data.Add("FHanMucDauTuQPSum", dataSummary.FHanMucDauTuQP);
                    data.Add("FHanMucDauTuNNSum", dataSummary.FHanMucDauTuNN);
                    data.Add("FHanMucDauTuDPSum", dataSummary.FHanMucDauTuDP);
                    data.Add("FHanMucDauTuOrtherSum", dataSummary.FHanMucDauTuOrther);
                    data.Add("FTongSoNhuCauSum", dataSummary.FTongSoNhuCau);
                    data.Add("FTongSoSum", dataSummary.FTongSo);
                    data.Add("FGiaTriNamThuNhatSum", dataSummary.FGiaTriNamThuNhat);
                    data.Add("FGiaTriNamThuHaiSum", dataSummary.FGiaTriNamThuHai);
                    data.Add("FGiaTriNamThuBaSum", dataSummary.FGiaTriNamThuBa);
                    data.Add("FGiaTriNamThuTuSum", dataSummary.FGiaTriNamThuTu);
                    data.Add("FGiaTriNamThuNamSum", dataSummary.FGiaTriNamThuNam);
                    data.Add("FGiaTriBoTriSum", dataSummary.FGiaTriBoTri);
                    FormatNumber formatNumber = new FormatNumber(Int32.Parse(DrpDonViTinhSelected.ValueItem), exportType);
                    data.Add("FormatNumber", formatNumber);
                    _dmChuKyService.GetConfigSign(TypeChuKy.RPT_VDT_KEHOACHTRUNGHAN_DEXUAT_MO_MOI_GOC, ref data);

                    string templateFileName = Path.Combine(ExportPrefix.PATH_TL_KHTH, MediumTermPlanType.REPORT_KE_HOACH_TRUNG_HAN_DE_XUAT_TEMPLATE);
                    string fileName = Path.GetFileNameWithoutExtension(MediumTermPlanType.REPORT_KE_HOACH_TRUNG_HAN_DE_XUAT_TEMPLATE);
                    string fileNamePrefix = string.Format("{0}_{1}-{2}", fileName, SNamBatDau, INamKetThuc);
                    string fileNameWithoutExtension = StringUtils.CreateExportFileName(fileNamePrefix);
                    var xlsFile = _exportService.Export<VdtKhvKeHoach5NamDeXuatReportQuery, VdtKhvKeHoach5NamDeXuatReportQuery>(templateFileName, data);
                    _exportService.FormatAllRowHeight(ItemsReportDeXuat, "STenDuAn", 12, 40, xlsFile);
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

        private void OnPrintReportDeXuatChuyenTiep(ExportType exportType)
        {
            try
            {
                StringBuilder sError = Validation();
                if (sError.Length != 0)
                {
                    MessageBox.Show(sError.ToString());
                    return;
                }

                sError = AuthorizationReport();
                List<DonVi> userAgency = _nsDonViService.FindByUser(_sessionService.Current.Principal, _sessionService.Current.YearOfWork, LoaiDonVi.ROOT);
                if (!userAgency.Any(x => x.Loai == LoaiDonVi.ROOT) && sError.Length != 0)
                {
                    MessageBox.Show(string.Format(Resources.UserManagerKHTHReportWarning, _sessionService.Current.Principal, sError.ToString()));
                    return;
                }

                var predicate = CreatePredicateDeXuat();
                var itemQuery = _vdtKhvKeHoach5NamDeXuat.FindByCondition(predicate).ToList();
                if (itemQuery == null || itemQuery.Count == 0)
                {
                    MessageBox.Show(Resources.MsgErrorDataNotFound);
                    return;
                }

                BackgroundWorkerHelper.Run((s, e) =>
                {
                    IsLoading = true;

                    List<VdtKhvKeHoach5NamDeXuatModel> item = _mapper.Map<List<VdtKhvKeHoach5NamDeXuatModel>>(itemQuery);
                    List<string> sLoaiCongTrinh = new List<string>();
                    if (_drpLoaiCongTrinhSelected.ValueItem.Equals("1"))
                    {
                        sLoaiCongTrinh = _drpLoaiCongTrinh.Where(x => !x.ValueItem.Equals("1")).Select(x => x.ValueItem).ToList();
                    }
                    else
                    {
                        sLoaiCongTrinh.Add(_drpLoaiCongTrinhSelected.ValueItem);
                    }

                    string lstLoaiCongTrinh = string.Join(",", sLoaiCongTrinh.ToList()).ToString();
                    string lstId = string.Join(",", item.Select(x => x.Id.ToString()).ToList());
                    string lstBudget = string.Join(",", DataBudget.Where(x => x.IsChecked).Select(x => x.ValueItem.ToString()).ToList());
                    string lstUnit = string.Join(",", DataUnit.Where(x => x.IsChecked).Select(x => x.ValueItem.ToString()).ToList());
                    int yearPlan = item.FirstOrDefault().IGiaiDoanTu;
                    List<VdtKhvKeHoach5NamDeXuatReportQuery> lstQuery = _vdtKhvKeHoach5NamChiTietDexuatService.FindByReportKeHoachTrungHanDeXuatChuyenTiep(lstId, lstBudget, lstLoaiCongTrinh,
                        lstUnit, Int32.Parse(_drpReportTypeSelected.ValueItem), Double.Parse(DrpDonViTinhSelected.ValueItem)).ToList();

                    ItemsReportDeXuat = new ObservableCollection<VdtKhvKeHoach5NamDeXuatReportQuery>(lstQuery);
                    ItemsReportDeXuat.Select(x =>
                    {
                        if (!string.IsNullOrEmpty(x.SSoQuyetDinh) || x.DNgayQuyetDinh.HasValue)
                        {
                            x.SSoQuyetDinhNgayQuyetDinh = string.Format("{0}-{1}", x.SSoQuyetDinh, (x.DNgayQuyetDinh.HasValue ? x.DNgayQuyetDinh.Value.ToString("dd/MM/yyyy") : string.Empty));
                        }
                        x.FHanMucDauTu = x.FHanMucDauTuDP + x.FHanMucDauTuNN + x.FHanMucDauTuQP + x.FHanMucDauTuOrther;
                        x.TongLuyKe = x.LuyKeVonNSQPDaBoTri.GetValueOrDefault() + x.LuyKeVonNSQPDeNghiBoTri.GetValueOrDefault();
                        x.FTongSo = x.FGiaTriNamThuNhat.GetValueOrDefault() + x.FGiaTriNamThuHai.GetValueOrDefault() + x.FGiaTriNamThuBa.GetValueOrDefault()
                            + x.FGiaTriNamThuTu.GetValueOrDefault() + x.FGiaTriNamThuNam.GetValueOrDefault();
                        return x;
                    }).ToList();

                    CalculateDataReportDeXuat();
                    HandleDataReportDeXuat();

                    VdtKhvKeHoach5NamDeXuatReportQuery reportSummary = new VdtKhvKeHoach5NamDeXuatReportQuery();
                    reportSummary.FHanMucDauTu = ItemsReportDeXuat.Where(x => !x.IsHangCha).Sum(x => x.FHanMucDauTu);
                    reportSummary.FHanMucDauTuQP = ItemsReportDeXuat.Where(x => !x.IsHangCha).Sum(x => x.FHanMucDauTuQP);
                    reportSummary.FHanMucDauTuNN = ItemsReportDeXuat.Where(x => !x.IsHangCha).Sum(x => x.FHanMucDauTuNN);
                    reportSummary.FHanMucDauTuOrther = ItemsReportDeXuat.Where(x => !x.IsHangCha).Sum(x => x.FHanMucDauTuOrther);
                    reportSummary.TongLuyKe = ItemsReportDeXuat.Where(x => !x.IsHangCha).Sum(x => x.TongLuyKe);
                    reportSummary.LuyKeVonNSQPDaBoTri = ItemsReportDeXuat.Where(x => !x.IsHangCha).Sum(x => x.LuyKeVonNSQPDaBoTri);
                    reportSummary.LuyKeVonNSQPDeNghiBoTri = ItemsReportDeXuat.Where(x => !x.IsHangCha).Sum(x => x.LuyKeVonNSQPDeNghiBoTri);
                    reportSummary.FTongSo = ItemsReportDeXuat.Where(x => !x.IsHangCha).Sum(x => x.FTongSo);
                    reportSummary.FGiaTriNamThuNhat = ItemsReportDeXuat.Where(x => !x.IsHangCha).Sum(x => x.FGiaTriNamThuNhat);
                    reportSummary.FGiaTriNamThuHai = ItemsReportDeXuat.Where(x => !x.IsHangCha).Sum(x => x.FGiaTriNamThuHai);
                    reportSummary.FGiaTriNamThuBa = ItemsReportDeXuat.Where(x => !x.IsHangCha).Sum(x => x.FGiaTriNamThuBa);
                    reportSummary.FGiaTriNamThuTu = ItemsReportDeXuat.Where(x => !x.IsHangCha).Sum(x => x.FGiaTriNamThuTu);
                    reportSummary.FGiaTriNamThuNam = ItemsReportDeXuat.Where(x => !x.IsHangCha).Sum(x => x.FGiaTriNamThuNam);

                    string sMuc = string.Join("+", ItemsReportDeXuat.Where(x => x.LoaiParent.Equals(0)).Select(x => x.STT).ToList());
                    Dictionary<string, object> data = new Dictionary<string, object>();
                    string sTenDonVi = GetNameUnit();
                    if (!string.IsNullOrEmpty(sTenDonVi))
                    {
                        sTenDonVi = sTenDonVi.ToUpper();
                    }
                    data.Add("CapTren", GetParentUnit());
                    data.Add("Items", ItemsReportDeXuat);
                    data.Add("TenDonVi", sTenDonVi);
                    data.Add("Header1", _txtHeader1);
                    data.Add("Header2", _txtHeader2);
                    data.Add("Header3", _txtHeader3);
                    data.Add("BeforeYear", (yearPlan - 2).ToString());
                    data.Add("YearPlan", (yearPlan - 1).ToString());
                    data.Add("Year1", yearPlan.ToString());
                    data.Add("Year2", (yearPlan + 1).ToString());
                    data.Add("Year3", (yearPlan + 2).ToString());
                    data.Add("Year4", (yearPlan + 3).ToString());
                    data.Add("Year5", (yearPlan + 4).ToString());
                    data.Add("Period", string.Format("{0} - {1}", yearPlan, yearPlan + 4));
                    data.Add("DonViTinh", DrpDonViTinhSelected.DisplayItem);
                    data.Add("FHanMucDauTuSum", reportSummary.FHanMucDauTu);
                    data.Add("FHanMucDauTuQPSum", reportSummary.FHanMucDauTuQP);
                    data.Add("FHanMucDauTuNNSum", reportSummary.FHanMucDauTuNN);
                    data.Add("FHanMucDauTuDPSum", reportSummary.FHanMucDauTuDP);
                    data.Add("FHanMucDauTuOrtherSum", reportSummary.FHanMucDauTuOrther);
                    data.Add("TongLuyKeSum", reportSummary.TongLuyKe);
                    data.Add("LuyKeVonNSQPDaBoTriSum", reportSummary.LuyKeVonNSQPDaBoTri);
                    data.Add("LuyKeVonNSQPDeNghiBoTriSum", reportSummary.LuyKeVonNSQPDeNghiBoTri);
                    data.Add("FTongSoSum", reportSummary.FTongSo);
                    data.Add("FGiaTriNamThuNhatSum", reportSummary.FGiaTriNamThuNhat);
                    data.Add("FGiaTriNamThuHaiSum", reportSummary.FGiaTriNamThuHai);
                    data.Add("FGiaTriNamThuBaSum", reportSummary.FGiaTriNamThuBa);
                    data.Add("FGiaTriNamThuTuSum", reportSummary.FGiaTriNamThuTu);
                    data.Add("FGiaTriNamThuNamSum", reportSummary.FGiaTriNamThuNam);
                    data.Add("Muc", sMuc);

                    FormatNumber formatNumber = new FormatNumber(Int32.Parse(DrpDonViTinhSelected.ValueItem), exportType);
                    data.Add("FormatNumber", formatNumber);
                    _dmChuKyService.GetConfigSign(TypeChuKy.RPT_VDT_KEHOACHTRUNGHAN_DEXUAT_CHUYENTIEP, ref data);

                    string templateFileName = Path.Combine(ExportPrefix.PATH_TL_KHTH, MediumTermPlanType.REPORT_KE_HOACH_TRUNG_HAN_DE_XUAT_CHUYENTIEP_TEMPLATE);
                    string fileName = MediumTermPlanType.REPORT_KE_HOACH_TRUNG_HAN_DE_XUAT_CHUYENTIEP_TEMPLATE;
                    string fileNamePrefix = string.Format("{0}_{1}-{2}", fileName, SNamBatDau, INamKetThuc);
                    string fileNameWithoutExtension = StringUtils.CreateExportFileName(fileNamePrefix);
                    var xlsFile = _exportService.Export<VdtKhvKeHoach5NamDeXuatReportQuery, VdtKhvKeHoach5NamDeXuatReportQuery>(templateFileName, data);

                    _exportService.FormatAllRowHeight(ItemsReportDeXuat, "STenDuAn", 12, 25, xlsFile);
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

        private void OnPrintReportDuocDuyetChuyenTiep(ExportType exportType)
        {
            try
            {
                StringBuilder sError = Validation();
                if (sError.Length != 0)
                {
                    MessageBox.Show(sError.ToString());
                    return;
                }

                sError = AuthorizationReport();
                List<DonVi> userAgency = _nsDonViService.FindByUser(_sessionService.Current.Principal, _sessionService.Current.YearOfWork, LoaiDonVi.ROOT);
                if (!userAgency.Any(x => x.Loai == LoaiDonVi.ROOT) && sError.Length != 0)
                {
                    MessageBox.Show(string.Format(Resources.UserManagerKHTHReportWarning, _sessionService.Current.Principal, sError.ToString()));
                    return;
                }

                var predicate = CreatePredicate();
                var itemQuery = _vdtKhvKeHoach5NamService.FindByCondition(predicate).ToList();
                if (itemQuery == null || itemQuery.Count == 0)
                {
                    MessageBox.Show(Resources.MsgErrorDataNotFound);
                    return;
                }

                BackgroundWorkerHelper.Run((s, e) =>
                {
                    IsLoading = true;

                    string lstId = string.Join(",", itemQuery.Select(x => x.Id.ToString()).ToList());
                    string lstBudget = DrpNguonVonSelected.ValueItem;
                    string lstUnit = string.Join(",", DataUnit.Where(x => x.IsChecked).Select(x => x.ValueItem.ToString()).ToList());
                    int yearPlan = itemQuery.FirstOrDefault().IGiaiDoanTu;

                    List<VdtKhvKeHoach5NamChuyenTiepReportQuery> lstQuery = _vdtKhvKeHoach5NamChiTietService.FindByReportKeHoachTrungHanChuyenTiep(lstId, lstBudget, lstUnit,
                        Int32.Parse(_drpReportTypeSelected.ValueItem), Double.Parse(DrpDonViTinhSelected.ValueItem)).ToList();
                    lstQuery.Where(x => !x.IsHangCha.Value).Select(item =>
                    {
                        item.TongSo = item.VonBoTriHetNam + item.VonDaBoTriNam;
                        item.TongMucDauTuPhaiBoTri = item.KeHoachVonNamDoDuyet + item.KeHoachVonDeNghiBoTriNam;
                        item.ChenhLechSoVoiQuyetDinhBo = item.KeHoachVonDeNghiBoTriNam - item.KeHoachVonNamDoDuyet;
                        return item;
                    }).ToList();

                    ItemsReportChuyenTiep = new ObservableCollection<VdtKhvKeHoach5NamChuyenTiepReportQuery>(lstQuery);
                    VdtKhvKeHoach5NamChuyenTiepReportQuery dataSummary = new VdtKhvKeHoach5NamChuyenTiepReportQuery();
                    if (_drpReportTypeSelected.ValueItem.Equals("2"))
                    {
                        CalculateDataReportChuyenTiep();

                        dataSummary.TongMucDauTu = ItemsReportChuyenTiep.Where(x => x.IsHangCha.Value).Sum(x => x.TongMucDauTu);
                        dataSummary.TongMucDauTuNSQP = ItemsReportChuyenTiep.Where(x => x.IsHangCha.Value).Sum(x => x.TongMucDauTuNSQP);
                        dataSummary.ChiPhiDuPhong = ItemsReportChuyenTiep.Where(x => x.IsHangCha.Value).Sum(x => x.ChiPhiDuPhong);
                        dataSummary.VonBoTriHetNam = ItemsReportChuyenTiep.Where(x => x.IsHangCha.Value).Sum(x => x.VonBoTriHetNam);
                        dataSummary.VonDaBoTriNam = ItemsReportChuyenTiep.Where(x => x.IsHangCha.Value).Sum(x => x.VonDaBoTriNam);
                        dataSummary.TongMucDauTuPhaiBoTri = ItemsReportChuyenTiep.Where(x => x.IsHangCha.Value).Sum(x => x.TongMucDauTuPhaiBoTri);
                        dataSummary.KeHoachVonNamDoDuyet = ItemsReportChuyenTiep.Where(x => x.IsHangCha.Value).Sum(x => x.KeHoachVonNamDoDuyet);
                        dataSummary.KeHoachVonDeNghiBoTriNam = ItemsReportChuyenTiep.Where(x => x.IsHangCha.Value).Sum(x => x.KeHoachVonDeNghiBoTriNam);
                        dataSummary.ChenhLechSoVoiQuyetDinhBo = ItemsReportChuyenTiep.Where(x => x.IsHangCha.Value).Sum(x => x.ChenhLechSoVoiQuyetDinhBo);
                        dataSummary.TongSo = ItemsReportChuyenTiep.Where(x => x.IsHangCha.Value).Sum(x => x.TongSo);
                    }
                    else
                    {
                        dataSummary.TongMucDauTu = ItemsReportChuyenTiep.Where(x => !x.IsHangCha.Value).Sum(x => x.TongMucDauTu);
                        dataSummary.TongMucDauTuNSQP = ItemsReportChuyenTiep.Where(x => !x.IsHangCha.Value).Sum(x => x.TongMucDauTuNSQP);
                        dataSummary.ChiPhiDuPhong = ItemsReportChuyenTiep.Where(x => !x.IsHangCha.Value).Sum(x => x.ChiPhiDuPhong);
                        dataSummary.VonBoTriHetNam = ItemsReportChuyenTiep.Where(x => !x.IsHangCha.Value).Sum(x => x.VonBoTriHetNam);
                        dataSummary.VonDaBoTriNam = ItemsReportChuyenTiep.Where(x => !x.IsHangCha.Value).Sum(x => x.VonDaBoTriNam);
                        dataSummary.TongMucDauTuPhaiBoTri = ItemsReportChuyenTiep.Where(x => !x.IsHangCha.Value).Sum(x => x.TongMucDauTuPhaiBoTri);
                        dataSummary.KeHoachVonNamDoDuyet = ItemsReportChuyenTiep.Where(x => !x.IsHangCha.Value).Sum(x => x.KeHoachVonNamDoDuyet);
                        dataSummary.KeHoachVonDeNghiBoTriNam = ItemsReportChuyenTiep.Where(x => !x.IsHangCha.Value).Sum(x => x.KeHoachVonDeNghiBoTriNam);
                        dataSummary.ChenhLechSoVoiQuyetDinhBo = ItemsReportChuyenTiep.Where(x => !x.IsHangCha.Value).Sum(x => x.ChenhLechSoVoiQuyetDinhBo);
                        dataSummary.TongSo = ItemsReportChuyenTiep.Where(x => !x.IsHangCha.Value).Sum(x => x.TongSo);
                    }

                    ItemsReportChuyenTiep.Where(x => x.IsHangCha.Value).Select((item, index) => { item.STT = RomanNumber.ToRoman(index + 1); return item; }).ToList();
                    ItemsReportChuyenTiep.Where(x => !x.IsHangCha.Value).Select((item, index) => { item.STT = (index + 1).ToString(); return item; }).ToList();

                    Dictionary<string, object> data = new Dictionary<string, object>();
                    string sTenDonVi = GetNameUnit();
                    if (!string.IsNullOrEmpty(sTenDonVi))
                    {
                        sTenDonVi = sTenDonVi.ToUpper();
                    }
                    data.Add("CapTren", GetParentUnit());
                    data.Add("Items", ItemsReportChuyenTiep);
                    data.Add("BeforeYear", (yearPlan - 1).ToString());
                    data.Add("Year1", yearPlan.ToString());
                    data.Add("YearPeriod", string.Format("{0}-{1}", yearPlan, yearPlan + 4));
                    data.Add("TenDonVi", sTenDonVi);
                    data.Add("Header1", _txtHeader1);
                    data.Add("Header2", _txtHeader2);
                    data.Add("Header3", _txtHeader3);
                    data.Add("DonViTinh", DrpDonViTinhSelected.DisplayItem);
                    data.Add("TongMucDauTuSum", dataSummary.TongMucDauTu);
                    data.Add("TongMucDauTuNSQPSum", dataSummary.TongMucDauTuNSQP);
                    data.Add("ChiPhiDuPhongSum", dataSummary.ChiPhiDuPhong);
                    data.Add("TongSoSum", dataSummary.TongSo);
                    data.Add("VonBoTriHetNamSum", dataSummary.VonBoTriHetNam);
                    data.Add("VonDaBoTriNamSum", dataSummary.VonDaBoTriNam);
                    data.Add("TongMucDauTuPhaiBoTriSum", dataSummary.TongMucDauTuPhaiBoTri);
                    data.Add("KeHoachVonNamDoDuyetSum", dataSummary.KeHoachVonNamDoDuyet);
                    data.Add("KeHoachVonDeNghiBoTriNamSum", dataSummary.KeHoachVonDeNghiBoTriNam);
                    data.Add("ChenhLechSoVoiQuyetDinhBoSum", dataSummary.ChenhLechSoVoiQuyetDinhBo);
                    data.Add("TxtChuKyKhthdd", !string.IsNullOrEmpty(TxtChuKyKhthdd) ? TxtChuKyKhthdd.ToUpper() : string.Empty);

                    FormatNumber formatNumber = new FormatNumber(Int32.Parse(DrpDonViTinhSelected.ValueItem), exportType);
                    data.Add("FormatNumber", formatNumber);
                    _dmChuKyService.GetConfigSign(TypeChuKy.RPT_VDT_KEHOACHTRUNGHAN_DUOCDUYET_CHUYENTIEP, ref data);

                    string templateFileName = Path.Combine(ExportPrefix.PATH_TL_KHTH, MediumTermPlanType.REPORT_KE_HOACH_TRUNG_HAN_DUOC_DUYET_CHUYENTIEP);
                    string fileName = MediumTermPlanType.REPORT_KE_HOACH_TRUNG_HAN_DUOC_DUYET_CHUYENTIEP;
                    string fileNamePrefix = string.Format("{0}_{1}-{2}", fileName, SNamBatDau, INamKetThuc);
                    string fileNameWithoutExtension = StringUtils.CreateExportFileName(fileNamePrefix);
                    var xlsFile = _exportService.Export<VdtKhvKeHoach5NamChuyenTiepReportQuery, VdtKhvKeHoach5NamChuyenTiepReportQuery>(templateFileName, data);

                    _exportService.FormatAllRowHeight(ItemsReportChuyenTiep, "STenDuAn", 14, 35, xlsFile);
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

        private void OnPrintReportDeXuatModified(ExportType exportType)
        {
            try
            {
                StringBuilder sError = Validation();
                if (sError.Length != 0)
                {
                    MessageBox.Show(sError.ToString());
                    return;
                }
                sError = AuthorizationReport();
                List<DonVi> userAgency = _nsDonViService.FindByUser(_sessionService.Current.Principal, _sessionService.Current.YearOfWork, LoaiDonVi.ROOT);
                if (!userAgency.Any(x => x.Loai == LoaiDonVi.ROOT) && sError.Length != 0)
                {
                    MessageBox.Show(string.Format(Resources.UserManagerKHTHReportWarning, _sessionService.Current.Principal, sError.ToString()));
                    return;
                }

                var predicate = CreatePredicateDeXuatDieuChinh();
                var itemQuery = _vdtKhvKeHoach5NamDeXuat.FindByCondition(predicate).ToList();
                if (itemQuery == null || itemQuery.Count == 0)
                {
                    MessageBox.Show(Resources.MsgErrorDataNotFound);
                    return;
                }

                BackgroundWorkerHelper.Run((s, e) =>
                {
                    IsLoading = true;

                    string lstId = string.Join(",", itemQuery.Select(x => x.Id).ToList());
                    string lstDonVi = string.Empty;
                    string lstNgVon = string.Empty;
                    if (DrpReportTypeSelected.ValueItem.Equals("1"))
                    {
                        lstDonVi = string.Join(",", _drpDonViSelected.ValueItem);
                    }
                    else
                    {
                        lstDonVi = string.Join(",", DataUnit.Where(x => x.IsChecked).Select(x => x.ValueItem).ToList());
                    }

                    lstNgVon = string.Join(",", DataBudget.Where(x => x.IsChecked).Select(x => x.ValueItem).ToList());

                    List<VdtKhvKeHoach5NamDeXuatDieuChinhReportQuery> lstQuery = new List<VdtKhvKeHoach5NamDeXuatDieuChinhReportQuery>();
                    if (DrpReportTypeSelected != null && DrpReportTypeSelected.ValueItem.Equals(RPT_TONGHOP))
                    {
                        //lstQuery = _vdtKhvKeHoach5NamChiTietDexuatService.FindSuggestionReport(Int32.Parse(RPT_TONGHOP), lstId, lstDonVi, Double.Parse(DrpDonViTinhSelected.ValueItem)).ToList();
                        lstQuery = _vdtKhvKeHoach5NamChiTietDexuatService.FindSuggestionReport(Int32.Parse(RPT_TONGHOP), lstId, lstDonVi, Double.Parse(DrpDonViTinhSelected.ValueItem), lstNgVon).ToList();
                    }
                    else
                    {
                        //lstQuery = _vdtKhvKeHoach5NamChiTietDexuatService.FindSuggestionReport(Int32.Parse(RPT_DONVI), lstId, lstDonVi, Double.Parse(DrpDonViTinhSelected.ValueItem)).ToList();
                        lstQuery = _vdtKhvKeHoach5NamChiTietDexuatService.FindSuggestionReport(Int32.Parse(RPT_DONVI), lstId, lstDonVi, Double.Parse(DrpDonViTinhSelected.ValueItem), lstNgVon).ToList();
                    }
                    ItemsReportDeXuatDieuChinh = new ObservableCollection<VdtKhvKeHoach5NamDeXuatDieuChinhReportQuery>(lstQuery);

                    HandleDataDeXuatDieuChinh();

                    VdtKhvKeHoach5NamDeXuatDieuChinhReportQuery dataSummary = new VdtKhvKeHoach5NamDeXuatDieuChinhReportQuery();
                    dataSummary.FHanMucDauTuDuocDuyet = ItemsReportDeXuatDieuChinh.Where(x => !x.IsHangCha).Sum(x => x.FHanMucDauTuDuocDuyet);
                    dataSummary.FTongSoDuocDuyet = ItemsReportDeXuatDieuChinh.Where(x => !x.IsHangCha).Sum(x => x.FTongSoDuocDuyet);
                    dataSummary.FVonBoTriTuNamDenNamDuocDuyet = ItemsReportDeXuatDieuChinh.Where(x => !x.IsHangCha).Sum(x => x.FVonBoTriTuNamDenNamDuocDuyet);
                    dataSummary.FVonBoTriSauNamDuocDuyet = ItemsReportDeXuatDieuChinh.Where(x => !x.IsHangCha).Sum(x => x.FVonBoTriSauNamDuocDuyet);
                    dataSummary.FHanMucDauTuDeXuat = ItemsReportDeXuatDieuChinh.Where(x => !x.IsHangCha).Sum(x => x.FHanMucDauTuDeXuat);
                    dataSummary.FTongSoDeXuat = ItemsReportDeXuatDieuChinh.Where(x => !x.IsHangCha).Sum(x => x.FTongSoDeXuat);
                    dataSummary.FTongCongDeXuat = ItemsReportDeXuatDieuChinh.Where(x => !x.IsHangCha).Sum(x => x.FTongCongDeXuat);
                    dataSummary.FGiaTriNamThuNhatDeXuat = ItemsReportDeXuatDieuChinh.Where(x => !x.IsHangCha).Sum(x => x.FGiaTriNamThuNhatDeXuat);
                    dataSummary.FGiaTriNamThuHaiDeXuat = ItemsReportDeXuatDieuChinh.Where(x => !x.IsHangCha).Sum(x => x.FGiaTriNamThuHaiDeXuat);
                    dataSummary.FGiaTriNamThuBaDeXuat = ItemsReportDeXuatDieuChinh.Where(x => !x.IsHangCha).Sum(x => x.FGiaTriNamThuBaDeXuat);
                    dataSummary.FGiaTriNamThuTuDeXuat = ItemsReportDeXuatDieuChinh.Where(x => !x.IsHangCha).Sum(x => x.FGiaTriNamThuTuDeXuat);
                    dataSummary.FGiaTriNamThuNamDeXuat = ItemsReportDeXuatDieuChinh.Where(x => !x.IsHangCha).Sum(x => x.FGiaTriNamThuNamDeXuat);
                    dataSummary.FGiaTriSauNamDeXuat = ItemsReportDeXuatDieuChinh.Where(x => !x.IsHangCha).Sum(x => x.FGiaTriSauNamDeXuat);
                    dataSummary.FHanMucDauTuChenhLech = ItemsReportDeXuatDieuChinh.Where(x => !x.IsHangCha).Sum(x => x.FHanMucDauTuChenhLech);
                    dataSummary.FTongSoChenhLech = ItemsReportDeXuatDieuChinh.Where(x => !x.IsHangCha).Sum(x => x.FTongSoChenhLech);
                    dataSummary.FVonBoTriSauNamChenhLech = ItemsReportDeXuatDieuChinh.Where(x => !x.IsHangCha).Sum(x => x.FVonBoTriSauNamChenhLech);
                    dataSummary.FVonBoTriSauNamChenhLech = ItemsReportDeXuatDieuChinh.Where(x => !x.IsHangCha).Sum(x => x.FVonBoTriSauNamChenhLech);
                    dataSummary.FVonBoTriTuNamDenNamChenhLech = ItemsReportDeXuatDieuChinh.Where(x => !x.IsHangCha).Sum(x => x.FVonBoTriTuNamDenNamChenhLech);

                    int iNamBatDau = DateTime.Now.Year;
                    if (DrpReportTypeSelected.ValueItem.Equals("1"))
                    {
                        iNamBatDau = itemQuery.FirstOrDefault().IGiaiDoanTu;
                    }
                    else
                    {
                        iNamBatDau = Int32.Parse(SNamBatDau);
                    }
                    int iNamKetThuc = iNamBatDau + 4;

                    string sTenDonVi = GetNameUnit();
                    if (!string.IsNullOrEmpty(sTenDonVi))
                    {
                        sTenDonVi = sTenDonVi.ToUpper();
                    }

                    Dictionary<string, object> data = new Dictionary<string, object>();
                    data.Add("CapTren", GetParentUnit());
                    data.Add("TenDonVi", sTenDonVi);
                    data.Add("Header1", TxtHeader1);
                    data.Add("Header2", TxtHeader2);
                    data.Add("Header3", TxtHeader3);
                    data.Add("YearPeriod", string.Format("{0} - {1}", iNamBatDau, iNamKetThuc));
                    data.Add("Year1", iNamBatDau.ToString());
                    data.Add("Year2", (iNamBatDau + 1).ToString());
                    data.Add("Year3", (iNamBatDau + 2).ToString());
                    data.Add("Year4", (iNamBatDau + 3).ToString());
                    data.Add("Year5", (iNamBatDau + 4).ToString());
                    data.Add("Items", ItemsReportDeXuatDieuChinh);
                    data.Add("DonViTinh", DrpDonViTinhSelected.DisplayItem);
                    data.Add("FHanMucDauTuDuocDuyetSum", dataSummary.FHanMucDauTuDuocDuyet);
                    data.Add("FTongSoDuocDuyetSum", dataSummary.FTongSoDuocDuyet);
                    data.Add("FVonBoTriTuNamDenNamDuocDuyetSum", dataSummary.FVonBoTriTuNamDenNamDuocDuyet);
                    data.Add("FVonBoTriSauNamDuocDuyetSum", dataSummary.FVonBoTriSauNamDuocDuyet);
                    data.Add("FHanMucDauTuDeXuatSum", dataSummary.FHanMucDauTuDeXuat);
                    data.Add("FTongSoDeXuatSum", dataSummary.FTongSoDeXuat);
                    data.Add("FTongCongDeXuatSum", dataSummary.FTongCongDeXuat);
                    data.Add("FGiaTriNamThuNhatDeXuatSum", dataSummary.FGiaTriNamThuNhatDeXuat);
                    data.Add("FGiaTriNamThuHaiDeXuatSum", dataSummary.FGiaTriNamThuHaiDeXuat);
                    data.Add("FGiaTriNamThuBaDeXuatSum", dataSummary.FGiaTriNamThuBaDeXuat);
                    data.Add("FGiaTriNamThuTuDeXuatSum", dataSummary.FGiaTriNamThuTuDeXuat);
                    data.Add("FGiaTriNamThuNamDeXuatSum", dataSummary.FGiaTriNamThuNamDeXuat);
                    data.Add("FGiaTriSauNamDeXuatSum", dataSummary.FGiaTriSauNamDeXuat);
                    data.Add("FHanMucDauTuChenhLechSum", dataSummary.FHanMucDauTuChenhLech);
                    data.Add("FTongSoChenhLechSum", dataSummary.FTongSoChenhLech);
                    data.Add("FVonBoTriTuNamDenNamChenhLechSum", dataSummary.FVonBoTriTuNamDenNamChenhLech);
                    data.Add("FVonBoTriSauNamChenhLechSum", dataSummary.FVonBoTriSauNamChenhLech);

                    FormatNumber formatNumber = new FormatNumber(Int32.Parse(DrpDonViTinhSelected.ValueItem), exportType);
                    data.Add("FormatNumber", formatNumber);
                    _dmChuKyService.GetConfigSign(TypeChuKy.RPT_VDT_KEHOACHTRUNGHAN_DEXUAT_MO_MOI_DIEUCHINH, ref data);

                    string templateFileName = Path.Combine(ExportPrefix.PATH_TL_KHTH, MediumTermPlanType.REPORT_KE_HOACH_TRUNG_HAN_DE_XUAT_DIEU_CHINH);
                    string fileNamePrefix = Path.GetFileNameWithoutExtension(MediumTermPlanType.REPORT_KE_HOACH_TRUNG_HAN_DE_XUAT_DIEU_CHINH);
                    string fileNameWithoutExtension = StringUtils.CreateExportFileName(fileNamePrefix);
                    var xlsFile = _exportService.Export<VdtKhvKeHoach5NamDeXuatDieuChinhReportQuery, VdtKhvKeHoach5NamDeXuatDieuChinhReportQuery>(templateFileName, data);

                    _exportService.FormatAllRowHeight(ItemsReportDeXuatDieuChinh, "STenDuAn", 14, 35, xlsFile);
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

        private void OnPrintBrowser()
        {
            try
            {
                if (ReportMediumTypes.Equals(ReportMediumType.APPROVED_CTMM))
                {
                    OnPrintReport(ExportType.PDF);
                }
                else if (ReportMediumTypes.Equals(ReportMediumType.SUGGESTION_CTMM))
                {
                    if (DrpVoucherTypeSelected != null && DrpVoucherTypeSelected.ValueItem.Equals(RPT_GOC))
                    {
                        OnPrintReportDeXuat(ExportType.PDF);
                    }
                    else
                    {
                        OnPrintReportDeXuatModified(ExportType.PDF);
                    }
                }
                else if (ReportMediumTypes.Equals(ReportMediumType.SUGGESTION_CTCT))
                {
                    OnPrintReportDeXuatChuyenTiep(ExportType.PDF);
                }
                else if (ReportMediumTypes.Equals(ReportMediumType.APPROVED_CTCT))
                {
                    OnPrintReportDuocDuyetChuyenTiep(ExportType.PDF);
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        private void OnPrintExcel()
        {
            try
            {
                if (ReportMediumTypes.Equals(ReportMediumType.APPROVED_CTMM))
                {
                    OnPrintReport(ExportType.EXCEL);
                }
                else if (ReportMediumTypes.Equals(ReportMediumType.SUGGESTION_CTMM))
                {
                    if (DrpVoucherTypeSelected != null && DrpVoucherTypeSelected.ValueItem.Equals("1"))
                    {
                        OnPrintReportDeXuat(ExportType.EXCEL);
                    }
                    else
                    {
                        OnPrintReportDeXuatModified(ExportType.EXCEL);
                    }
                }
                else if (ReportMediumTypes.Equals(ReportMediumType.SUGGESTION_CTCT))
                {
                    OnPrintReportDeXuatChuyenTiep(ExportType.EXCEL);
                }
                else if (ReportMediumTypes.Equals(ReportMediumType.APPROVED_CTCT))
                {
                    OnPrintReportDuocDuyetChuyenTiep(ExportType.EXCEL);
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }
        #endregion

        #region Helper
        private void OnConfigSign()
        {
            try
            {
                DmChuKyModel chuKyModel = new DmChuKyModel();
                if (ReportMediumTypes.Equals(ReportMediumType.SUGGESTION_CTMM))
                {
                    if (_drpVoucherTypeSelected != null && _drpVoucherTypeSelected.ValueItem.Equals(RPT_GOC))
                    {
                        _dmChuKy = _dmChuKyService.FindByCondition(x => x.IdType.Equals(TypeChuKy.RPT_VDT_KEHOACHTRUNGHAN_DEXUAT_MO_MOI_GOC) && x.ITrangThai == StatusType.ACTIVE).FirstOrDefault();
                    }
                    else
                    {
                        _dmChuKy = _dmChuKyService.FindByCondition(x => x.IdType.Equals(TypeChuKy.RPT_VDT_KEHOACHTRUNGHAN_DEXUAT_MO_MOI_DIEUCHINH) && x.ITrangThai == StatusType.ACTIVE).FirstOrDefault();
                    }
                }
                else if (ReportMediumTypes.Equals(ReportMediumType.APPROVED_CTMM))
                {
                    _dmChuKy = _dmChuKyService.FindByCondition(x => x.IdType.Equals(TypeChuKy.RPT_VDT_KEHOACHTRUNGHAN_DUOCDUYET_MO_MOI) && x.ITrangThai == StatusType.ACTIVE).FirstOrDefault();
                }
                else if (ReportMediumTypes.Equals(ReportMediumType.SUGGESTION_CTCT))
                {
                    _dmChuKy = _dmChuKyService.FindByCondition(x => x.IdType.Equals(TypeChuKy.RPT_VDT_KEHOACHTRUNGHAN_DEXUAT_CHUYENTIEP) && x.ITrangThai == StatusType.ACTIVE).FirstOrDefault();
                }
                else if (ReportMediumTypes.Equals(ReportMediumType.APPROVED_CTCT))
                {
                    _dmChuKy = _dmChuKyService.FindByCondition(x => x.IdType.Equals(TypeChuKy.RPT_VDT_KEHOACHTRUNGHAN_DUOCDUYET_CHUYENTIEP) && x.ITrangThai == StatusType.ACTIVE).FirstOrDefault();
                }

                if (_dmChuKy == null)
                {
                    if (ReportMediumTypes.Equals(ReportMediumType.SUGGESTION_CTMM))
                    {
                        if (_drpVoucherTypeSelected != null && _drpVoucherTypeSelected.ValueItem.Equals(RPT_GOC))
                        {
                            chuKyModel.IdType = TypeChuKy.RPT_VDT_KEHOACHTRUNGHAN_DEXUAT_MO_MOI_GOC;
                        }
                        else
                        {
                            chuKyModel.IdType = TypeChuKy.RPT_VDT_KEHOACHTRUNGHAN_DEXUAT_MO_MOI_DIEUCHINH;
                        }
                    }
                    else if (ReportMediumTypes.Equals(ReportMediumType.APPROVED_CTMM))
                    {
                        chuKyModel.IdType = TypeChuKy.RPT_VDT_KEHOACHTRUNGHAN_DUOCDUYET_MO_MOI;
                    }
                    else if (ReportMediumTypes.Equals(ReportMediumType.SUGGESTION_CTCT))
                    {
                        chuKyModel.IdType = TypeChuKy.RPT_VDT_KEHOACHTRUNGHAN_DEXUAT_CHUYENTIEP;
                    }
                    else if (ReportMediumTypes.Equals(ReportMediumType.APPROVED_CTCT))
                    {
                        chuKyModel.IdType = TypeChuKy.RPT_VDT_KEHOACHTRUNGHAN_DUOCDUYET_CHUYENTIEP;
                    }
                }
                else
                {
                    chuKyModel = _mapper.Map<DmChuKyModel>(_dmChuKy);
                }

                DmChuKyDialogViewModel.DmChuKyModel = chuKyModel;
                DmChuKyDialogViewModel.SavedAction = obj =>
                {
                    DmChuKyModel chuKy = (DmChuKyModel)obj;
                    if (chuKy.TieuDe1MoTa != null)
                    {
                        TxtHeader1 = chuKy.TieuDe1MoTa;
                    }
                    if (chuKy.TieuDe2MoTa != null)
                    {
                        TxtHeader2 = chuKy.TieuDe2MoTa;
                    }
                    if (chuKy.TieuDe3MoTa != null)
                    {
                        TxtHeader3 = chuKy.TieuDe3MoTa;
                    }
                    if (chuKy.Ten1MoTa != null)
                    {
                        TxtChuKyKhthdd = chuKy.Ten1MoTa;
                    }
                };
                DmChuKyDialogViewModel.Init();
                DmChuKyDialogViewModel.ShowDialog();
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }
        private void LoadNguonVon()
        {
            try
            {
                List<NsNguonNganSach> cbxNguonVonData = _nguonNganSachService.FindNguonNganSach().ToList();
                _drpNguonVon = _mapper.Map<ObservableCollection<ComboboxItem>>(cbxNguonVonData.Select(n => new ComboboxItem()
                {
                    DisplayItem = n.STen,
                    ValueItem = n.IIdMaNguonNganSach.ToString()
                }));

                List<NguonNganSachModel> listMapper = _mapper.Map<List<NguonNganSachModel>>(cbxNguonVonData);
                DataBudget = _mapper.Map<ObservableCollection<CheckBoxItem>>(listMapper);
                DataBudget.Where(x => x.ValueItem == "1").FirstOrDefault().IsChecked = true;            // mặc định tick vào ngân sách quốc phòng
                if (_drpNguonVon != null && _drpNguonVon.Count > 0)
                {
                    DrpNguonVonSelected = _drpNguonVon.Where(x => x.ValueItem == "1").FirstOrDefault();         // mặc định là ngân sách quốc phòng trong dropdown list
                }
                OnPropertyChanged(nameof(DrpNguonVon));
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        private void LoadDonViQuanLy()
        {
            try
            {
                List<DonVi> cbxLoaiDonViData = _nsDonViService.FindByNamLamViec(_sessionService.Current.YearOfWork)
                    .Where(n => lstDonViExclude.Contains(n.Loai)).ToList();

                DrpDonVi = _mapper.Map<ObservableCollection<ComboboxItem>>(cbxLoaiDonViData.Select(n => new ComboboxItem()
                {
                    DisplayItem = n.TenDonVi,
                    ValueItem = n.IIDMaDonVi,
                    HiddenValue = n.Id.ToString()
                }));

                if (ReportMediumTypes.Equals(ReportMediumType.SUGGESTION_CTCT) || ReportMediumTypes.Equals(ReportMediumType.SUGGESTION_CTMM))
                {
                    _txtUnitHeader = "Đơn vị";
                    DataUnit = _mapper.Map<ObservableCollection<CheckBoxItem>>(cbxLoaiDonViData);
                    OnPropertyChanged(nameof(DrpDonVi));
                }
                else if (ReportMediumTypes.Equals(ReportMediumType.APPROVED_CTCT) || ReportMediumTypes.Equals(ReportMediumType.APPROVED_CTMM))
                {
                    _txtUnitHeader = "Đơn vị";
                    List<VdtDmDonViThucHienDuAn> cbxDonViThucHienDuAn = _vdtDmDonViThucHienDuAnService.FindAll().ToList();
                    DataUnit = _mapper.Map<ObservableCollection<CheckBoxItem>>(cbxDonViThucHienDuAn);
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        private void LoadLoaiCongTrinh()
        {
            try
            {
                var cbxLoaiDonViData = _loaiCongTrinhService.GetAll().Where(n => !n.IIdParent.HasValue).Distinct().ToList();
                _drpLoaiCongTrinh = _mapper.Map<ObservableCollection<ComboboxItem>>(cbxLoaiDonViData.Select(n => new ComboboxItem()
                {
                    DisplayItem = n.STenLoaiCongTrinh,
                    ValueItem = n.IIdLoaiCongTrinh.ToString()
                }));

                _drpLoaiCongTrinh.Add(new ComboboxItem() { DisplayItem = "Tất cả", ValueItem = "1" });
                if (_drpLoaiCongTrinh != null && _drpLoaiCongTrinh.Count > 0)
                {
                    DrpLoaiCongTrinhSelected = _drpLoaiCongTrinh.LastOrDefault();
                }
                OnPropertyChanged(nameof(DrpLoaiCongTrinh));
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }
        #endregion
    }
}
