using AutoMapper;
using MaterialDesignThemes.Wpf;
using log4net;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Windows;
using System.Windows.Data;
using VTS.QLNS.CTC.App.Command;
using VTS.QLNS.CTC.App.Helper;
using VTS.QLNS.CTC.App.Model;
using VTS.QLNS.CTC.App.Model.Control;
using VTS.QLNS.CTC.App.Properties;
using VTS.QLNS.CTC.App.Service;
using VTS.QLNS.CTC.App.Service.UserFunction;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Criteria;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Service;
using VTS.QLNS.CTC.Utility;
using VTS.QLNS.CTC.Utility.Enum;
using VTS.QLNS.CTC.App.Service.Impl;
using FlexCel.Core;
using VTS.QLNS.CTC.App.ViewModel.Category;

namespace VTS.QLNS.CTC.App.ViewModel.Investment.InvestmentStandard.YearPlan
{
    public class PrintReportYearPlanViewModel : ReportViewModelBase<PhanBoVonModel, PhanBoVonChiTietModel, PhanBoVonChiTietModel>
    {

        private readonly IMapper _mapper;
        private readonly ISessionService _sessionService;
        private readonly ILog _logger;
        private readonly INsNguonNganSachService _nsNguonNganSachService;
        private readonly IDmLoaiCongTrinhService _dmLoaiCongTrinhService;
        private readonly IVdtKhvPhanBoVonChiTietService _phanBoVonChiTietService;
        private readonly IVdtKhvPhanBoVonService _vdtKhvPhanBoVonService;
        private readonly IDmChuKyService _dmChuKyService;
        private DmChuKyDialogViewModel DmChuKyDialogViewModel;
        private DmChuKy _dmChuKy;
        private readonly IExportService _exportService;
        private readonly INsDonViService _nsDonViService;
        private readonly IVdtDmDonViThucHienDuAnService _vdtDmDonViThucHienDuAnService;
        private const string RPT_GOC = "1";
        private const string RPT_TONGHOP = "1";
        private const string RPT_DONVI = "2";
        private ObservableCollection<VdtKhvVonNamDuocDuyetQuery> listItem = new ObservableCollection<VdtKhvVonNamDuocDuyetQuery>();
        private const string RPT_LOAICONGTRINH = "3";

        private ICollectionView _donViView;

        public override string Name
        {
            get => "Báo cáo kế hoạch vốn đầu năm";
        }

        public override string Title
        {
            get => "Báo cáo kế hoạch vốn đầu năm";
        }

        public override string Description
        {
            get => "Báo cáo kế hoạch vốn đầu năm";
        }

        public override Type ContentType => typeof(View.Investment.InvestmentStandard.PrintReport.YearPlanReport);

        private List<VdtDmLoaiCongTrinh> listAllVdtDmLoaiCongTrinh = new List<VdtDmLoaiCongTrinh>();

        private bool _isExportData;
        public bool IsExportData
        {
            get => ListDonVi.Any() && ListDonVi.Any(x => x.IsChecked);
            set => SetProperty(ref _isExportData, value);
        }

        private ObservableCollection<ComboboxItem> _namKeHoach = new ObservableCollection<ComboboxItem>();
        public ObservableCollection<ComboboxItem> NamKeHoach
        {
            get => _namKeHoach;
            set => SetProperty(ref _namKeHoach, value);
        }

        private ComboboxItem _namKeHoachSelected;
        public ComboboxItem NamKeHoachSelected
        {
            get => _namKeHoachSelected;
            set
            {
                SetProperty(ref _namKeHoachSelected, value);
                LoadVouchers();
            }
        }

        // start handle Don vi
        private ObservableCollection<CheckBoxItem> _listDonVi = new ObservableCollection<CheckBoxItem>();
        public ObservableCollection<CheckBoxItem> ListDonVi
        {
            get => _listDonVi;
            set => SetProperty(ref _listDonVi, value);
        }

        private string _labelSelectedCountDonVi;
        public string LabelSelectedCountDonVi
        {
            get => $"CHỌN ĐƠN VỊ ({ListDonVi.Count(item => item.IsChecked)}/{ListDonVi.Count})";
            set => SetProperty(ref _labelSelectedCountDonVi, value);
        }

        private bool _selectAllDonVi;

        public bool SelectAllDonVi
        {
            get => ListDonVi.Any() && ListDonVi.All(item => item.IsChecked);
            set
            {
                SetProperty(ref _selectAllDonVi, value);
                foreach (var item in ListDonVi) item.IsChecked = _selectAllDonVi;
            }
        }

        private string _searchDonVi;
        public string SearchDonVi
        {
            get => _searchDonVi;
            set
            {
                if (SetProperty(ref _searchDonVi, value))
                {
                    _donViView.Refresh();
                }
            }
        }
        // end handle Don vi

        private ObservableCollection<ComboboxItem> _nguonVon = new ObservableCollection<ComboboxItem>();
        public ObservableCollection<ComboboxItem> NguonVon
        {
            get => _nguonVon;
            set => SetProperty(ref _nguonVon, value);
        }

        private ComboboxItem _nguonVonSelected;
        public ComboboxItem NguonVonSelected
        {
            get => _nguonVonSelected;
            set => SetProperty(ref _nguonVonSelected, value);
        }

        private ObservableCollection<ComboboxItem> _loaiCongTrinh = new ObservableCollection<ComboboxItem>();
        public ObservableCollection<ComboboxItem> LoaiCongTrinh
        {
            get => _loaiCongTrinh;
            set => SetProperty(ref _loaiCongTrinh, value);
        }

        private ComboboxItem _loaiCongTrinhSelected;
        public ComboboxItem LoaiCongTrinhSelected
        {
            get => _loaiCongTrinhSelected;
            set => SetProperty(ref _loaiCongTrinhSelected, value);
        }

        private ObservableCollection<ComboboxItem> _cbxVoucherTypes;
        public ObservableCollection<ComboboxItem> CbxVoucherTypes
        {
            get => _cbxVoucherTypes;
            set => SetProperty(ref _cbxVoucherTypes, value);
        }

        private ComboboxItem _cbxVoucherTypeSelected;
        public ComboboxItem CbxVoucherTypeSelected
        {
            get => _cbxVoucherTypeSelected;
            set
            {
                SetProperty(ref _cbxVoucherTypeSelected, value);
                OnPropertyChanged(nameof(ReportYearModifiedVisibility));
            }
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

                if (value != null && value.ValueItem.Equals(RPT_TONGHOP))
                {
                    LoadVouchers();
                }
                OnPropertyChanged(nameof(ReportYearModifiedVisibility));
                OnPropertyChanged(nameof(VoucherVisibility));
                OnPropertyChanged(nameof(CbxUnitVisibility));
            }
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

        private string _tieuDe1;
        public string TieuDe1
        {
            get => _tieuDe1;
            set => SetProperty(ref _tieuDe1, value);
        }

        private string _tieuDe2;
        public string TieuDe2
        {
            get => _tieuDe2;
            set => SetProperty(ref _tieuDe2, value);
        }

        private string _txtDonViQuanLy;
        public string TxtDonViQuanLy
        {
            get => _txtDonViQuanLy;
            set => SetProperty(ref _txtDonViQuanLy, value);
        }

        public Visibility CbxUnitVisibility
        {
            get => DrpReportTypeSelected == null || (DrpReportTypeSelected != null && DrpReportTypeSelected.ValueItem.Equals(RPT_TONGHOP)) ? Visibility.Collapsed : Visibility.Visible;
        }

        public Visibility VoucherVisibility
        {
            get => DrpReportTypeSelected == null || (DrpReportTypeSelected != null && DrpReportTypeSelected.ValueItem.Equals(RPT_TONGHOP)) ? Visibility.Visible : Visibility.Collapsed;
        }

        public Visibility ReportYearModifiedVisibility
        {
            get => (_cbxVoucherTypeSelected == null || (_cbxVoucherTypeSelected != null && _cbxVoucherTypeSelected.ValueItem.Equals("1"))) && (DrpReportTypeSelected == null || DrpReportTypeSelected.ValueItem != RPT_LOAICONGTRINH) ? Visibility.Visible : Visibility.Collapsed;
        }

        public RelayCommand PrintActionCommand { get; set; }
        public RelayCommand ExportExcelActionCommand { get; set; }
        public RelayCommand ConfigSignCommand { get; set; }

        public PrintReportYearPlanViewModel(
            IMapper mapper,
            ISessionService sessionService,
            ILog logger,
            IExportService exportService,
            INsNguonNganSachService nsNguonNganSachService,
            IDmLoaiCongTrinhService dmLoaiCongTrinhService,
            IVdtKhvPhanBoVonChiTietService phanBoVonChiTietService,
            IVdtKhvPhanBoVonService vdtKhvPhanBoVonService,
            IDmChuKyService dmChuKyService,
            INsDonViService nsDonViService,
            IVdtDmDonViThucHienDuAnService vdtDmDonViThucHienDuAnService,
            DmChuKyDialogViewModel dmChuKyDialogViewModel
            )
        {
            _logger = logger;
            _mapper = mapper;
            _nsDonViService = nsDonViService;
            _dmChuKyService = dmChuKyService;
            _sessionService = sessionService;
            _exportService = exportService;
            _nsNguonNganSachService = nsNguonNganSachService;
            _dmLoaiCongTrinhService = dmLoaiCongTrinhService;
            _phanBoVonChiTietService = phanBoVonChiTietService;
            _vdtKhvPhanBoVonService = vdtKhvPhanBoVonService;
            _vdtDmDonViThucHienDuAnService = vdtDmDonViThucHienDuAnService;
            DmChuKyDialogViewModel = dmChuKyDialogViewModel;

            PrintActionCommand = new RelayCommand(obj => OnExport(ExportType.PDF));
            ExportExcelActionCommand = new RelayCommand(obj => OnExport(ExportType.EXCEL));
            ConfigSignCommand = new RelayCommand(obj => OnConfigSign());
        }

        public override void Init()
        {
            try
            {
                if (string.IsNullOrEmpty(_tieuDe1))
                {
                    _tieuDe1 = "DỰ TOÁN CHI XDCB NGUỒN NGÂN SÁCH QUỐC PHÒNG NĂM .....";
                }
                if (string.IsNullOrEmpty(_tieuDe2))
                {
                    _tieuDe2 = "(Kèm theo Quyết định số ....... ngày ........ tháng ..... năm ..... của ......)";
                }
                _txtDonViQuanLy = _sessionService.Current.TenDonVi;

                LoadReportType();
                LoadDonVi();
                LoadNamKeHoach();
                LoadNguonVon();
                LoadLoaiCongTrinh();
                LoadVoucherTypes();
                LoadDonViTinh();
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

        private void LoadVoucherTypes()
        {
            _cbxVoucherTypes = new ObservableCollection<ComboboxItem>()
            {
                new ComboboxItem(){DisplayItem = "Gốc", ValueItem = RPT_GOC}
            };

            if (_cbxVoucherTypes != null && _cbxVoucherTypes.Count() > 0)
            {
                _cbxVoucherTypeSelected = _cbxVoucherTypes.FirstOrDefault();
            }
        }

        private void LoadVouchers()
        {
            try
            {
                var predicate = PredicateBuilder.True<VdtKhvPhanBoVon>();
                if (CbxVoucherTypeSelected != null && CbxVoucherTypeSelected.ValueItem.Equals(RPT_GOC) && NamKeHoachSelected != null)
                {
                    predicate = predicate.And(x => x.IIDMaDonViQuanLy == _sessionService.Current.IdDonVi);
                    predicate = predicate.And(x => x.INamKeHoach == Int32.Parse(NamKeHoachSelected.ValueItem));
                    predicate = predicate.And(x => x.BIsGoc.Value);
                }

                List<VdtKhvPhanBoVon> lstQuery = _vdtKhvPhanBoVonService.FindByCondition(predicate).ToList();
                DrpVouchers = _mapper.Map<ObservableCollection<ComboboxItem>>(lstQuery);
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        private void LoadReportType()
        {
            List<ComboboxItem> lstReportType = new List<ComboboxItem>() {
                    new ComboboxItem { DisplayItem = "Báo cáo theo đơn vị", ValueItem = RPT_DONVI },
                    new ComboboxItem { DisplayItem = "Báo cáo tổng hợp", ValueItem = RPT_TONGHOP },
                    new ComboboxItem { DisplayItem = "Báo cáo theo Loại công trình", ValueItem = RPT_LOAICONGTRINH }
                };

            DrpReportTypes = new ObservableCollection<ComboboxItem>(lstReportType);

            if (DrpReportTypes != null && DrpReportTypes.Count > 0)
            {
                DrpReportTypeSelected = DrpReportTypes.FirstOrDefault();
            }

            OnPropertyChanged(nameof(DrpReportTypeSelected));
        }

        private Dictionary<string, object> HandleDataLoaiCongTrinh(ExportType exportType)
        {
            try
            {
                Dictionary<string, object> data = new Dictionary<string, object>();

                StringBuilder messageBuilder = new StringBuilder();

                if (NamKeHoachSelected == null)
                {
                    messageBuilder.AppendFormat(Resources.MsgErrorRequire, "Năm kế hoạch");
                }

                if (DrpDonViTinhSelected == null)
                {
                    messageBuilder.AppendFormat(Resources.MsgErrorRequire, "Đơn vị tính");
                }

                if (ListDonVi == null || !ListDonVi.Any(n => n.IsChecked))
                {
                    messageBuilder.AppendFormat(Resources.MsgErrorRequire, "Đơn vị");
                }

                if (messageBuilder.Length != 0)
                {
                    MessageBox.Show(messageBuilder.ToString());
                    LoadData();
                    return null;
                }

                var itemQuery = _vdtKhvPhanBoVonService.GetBcDuToanTheoLoaiCongTrinh(
                    int.Parse(CbxVoucherTypeSelected.ValueItem),
                    int.Parse(NamKeHoachSelected.ValueItem),
                    double.Parse(DrpDonViTinhSelected.ValueItem),
                    ListDonVi.Where(n => n.IsChecked).Select(n => n.ValueItem).ToList()).ToList();

                if (itemQuery == null || itemQuery.Count == 0)
                {
                    MessageBox.Show(Resources.MsgErrorDataNotFound);
                    return null;
                }

                string sDonViLap = ListDonVi.Where(x => x.IsChecked).Count() == 1 ? ListDonVi.Where(x => x.IsChecked).FirstOrDefault().DisplayItem : string.Empty;
                string sDonViCapTren = _sessionService.Current.TenDonVi;

                DonVi donViCapTren = _nsDonViService.FindByLoai("0", _sessionService.Current.YearOfWork);
                if (ListDonVi.Where(x => x.IsChecked).Count() > 1)
                {
                    sDonViLap = donViCapTren?.TenDonVi;
                    sDonViCapTren = NSConstants.BO_QUOC_PHONG;
                }
                else
                {
                    sDonViLap = ListDonVi.FirstOrDefault(x => x.IsChecked).DisplayItem;
                    sDonViCapTren = donViCapTren?.TenDonVi;
                }

                if (DrpReportTypeSelected != null && DrpReportTypeSelected.ValueItem == RPT_TONGHOP)
                {
                    sDonViLap = donViCapTren?.TenDonVi;
                }

                if (!string.IsNullOrEmpty(sDonViLap))
                {
                    sDonViLap = sDonViLap.ToUpper();
                }
                if (!string.IsNullOrEmpty(sDonViCapTren))
                {
                    sDonViCapTren = sDonViCapTren.ToUpper();
                }
                data.Add("DonViCapTren", sDonViCapTren);
                data.Add("DonViLap", sDonViLap);
                data.Add("iNamKeHoach", NamKeHoachSelected.ValueItem);
                data.Add("Header1", _tieuDe1);
                data.Add("Header2", _tieuDe2);
                data.Add("Items", itemQuery);
                data.Add("DonViTinh", DrpDonViTinhSelected != null ? DrpDonViTinhSelected.DisplayItem : string.Empty);
                FormatNumber formatNumber = new FormatNumber(Int32.Parse(DrpDonViTinhSelected.ValueItem), exportType);
                _dmChuKyService.GetConfigSign(TypeChuKy.RPT_VDT_KEHOACHVONNAM_DUOCDUYET_GOC, ref data);

                data.Add("FormatNumber", formatNumber);

                return data;
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
                return null;
            }
        }

        private Dictionary<string, object> HandleData(ExportType exportType)
        {
            try
            {
                Dictionary<string, object> data = new Dictionary<string, object>();

                StringBuilder messageBuilder = new StringBuilder();

                if (NamKeHoachSelected == null)
                {
                    messageBuilder.AppendFormat(Resources.MsgErrorRequire, "Năm kế hoạch");
                }

                if (NguonVonSelected == null)
                {
                    messageBuilder.AppendFormat(Resources.MsgErrorRequire, "Nguồn vốn");
                }

                if (LoaiCongTrinhSelected == null)
                {
                    messageBuilder.AppendFormat(Resources.MsgErrorRequire, "Loại công trình");
                }

                if (DrpDonViTinhSelected == null)
                {
                    messageBuilder.AppendFormat(Resources.MsgErrorRequire, "Đơn vị tính");
                }

                if (DrpReportTypeSelected == null)
                {
                    messageBuilder.AppendFormat(Resources.MsgErrorRequire, "Loại báo cáo");
                }

                if (messageBuilder.Length != 0)
                {
                    MessageBox.Show(messageBuilder.ToString());
                    LoadData();
                    return null;
                }

                var predicate = CreatePredicate();
                var itemQuery = _vdtKhvPhanBoVonService.FindByCondition(predicate).ToList();

                if (itemQuery == null || itemQuery.Count == 0)
                {
                    MessageBox.Show(Resources.MsgErrorDataNotFound);
                    return null;
                }

                string listLct = string.Empty;

                if (LoaiCongTrinhSelected != null && LoaiCongTrinhSelected.ValueItem.Equals("-1"))
                {
                    listLct = string.Join(",", LoaiCongTrinh.Where(x => !x.ValueItem.Equals("-1")).Select(x => x.ValueItem).ToList());
                }
                else
                {
                    listLct = LoaiCongTrinhSelected.ValueItem;
                }

                string lstId = string.Join(",", itemQuery.Select(x => x.Id).ToList());
                string lstDonVi = string.Join(",", ListDonVi.Where(x => x.IsChecked).Select(x => x.ValueItem).ToList());

                List<VdtKhvVonNamDuocDuyetQuery> lstGroup = new List<VdtKhvVonNamDuocDuyetQuery>();
                List<VdtKhvVonNamDuocDuyetQuery> lstKhoiCongMoiQuery = new List<VdtKhvVonNamDuocDuyetQuery>();
                List<VdtKhvVonNamDuocDuyetQuery> lstChuyenTiepQuery = new List<VdtKhvVonNamDuocDuyetQuery>();
                List<VdtKhvVonNamDuocDuyetQuery> lstDataExportKhoiCongMoi = new List<VdtKhvVonNamDuocDuyetQuery>();
                List<VdtKhvVonNamDuocDuyetQuery> lstDataExportChuyenTiep = new List<VdtKhvVonNamDuocDuyetQuery>();

                lstDataExportKhoiCongMoi.Add(new VdtKhvVonNamDuocDuyetQuery() { sTenDuAn = LoaiDuAnEnum.Get((int)LoaiDuAnEnum.Type.KHOI_CONG_MOI).ToUpper(), IsHangCha = true });
                lstDataExportChuyenTiep.Add(new VdtKhvVonNamDuocDuyetQuery() { sTenDuAn = LoaiDuAnEnum.Get((int)LoaiDuAnEnum.Type.CHUYEN_TIEP).ToUpper(), IsHangCha = true });

                if (DrpReportTypeSelected != null && DrpReportTypeSelected.ValueItem.Equals(RPT_TONGHOP))
                {
                    lstKhoiCongMoiQuery = _phanBoVonChiTietService.GetKeHoachVonNamDuocDuyetReport(lstId, listLct, Int32.Parse(RPT_TONGHOP), (int)LoaiDuAnEnum.Type.KHOI_CONG_MOI, lstDonVi, Double.Parse(DrpDonViTinhSelected.ValueItem)).ToList();
                    lstChuyenTiepQuery = _phanBoVonChiTietService.GetKeHoachVonNamDuocDuyetReport(lstId, listLct, Int32.Parse(RPT_TONGHOP), (int)LoaiDuAnEnum.Type.CHUYEN_TIEP, lstDonVi, Double.Parse(DrpDonViTinhSelected.ValueItem)).ToList();
                }
                else
                {
                    lstKhoiCongMoiQuery = _phanBoVonChiTietService.GetKeHoachVonNamDuocDuyetReport(lstId, listLct, Int32.Parse(RPT_DONVI), (int)LoaiDuAnEnum.Type.KHOI_CONG_MOI, lstDonVi, Double.Parse(DrpDonViTinhSelected.ValueItem)).ToList();
                    lstChuyenTiepQuery = _phanBoVonChiTietService.GetKeHoachVonNamDuocDuyetReport(lstId, listLct, Int32.Parse(RPT_DONVI), (int)LoaiDuAnEnum.Type.CHUYEN_TIEP, lstDonVi, Double.Parse(DrpDonViTinhSelected.ValueItem)).ToList();
                }

                lstDataExportKhoiCongMoi.AddRange(CalculateDataReport(lstKhoiCongMoiQuery));
                lstDataExportChuyenTiep.AddRange(CalculateDataReport(lstChuyenTiepQuery));
                //lstDataExportKhoiCongMoi.AddRange(CaculatorReport(lstKhoiCongMoiQuery));
                //lstDataExportChuyenTiep.AddRange(CaculatorReport(lstChuyenTiepQuery));
                lstGroup.AddRange(lstDataExportKhoiCongMoi);
                lstGroup.AddRange(lstDataExportChuyenTiep);

                var itemLoai = lstGroup.Where(x => !string.IsNullOrEmpty(x.sL)).ToList();
                var itemKhoan = lstGroup.Where(x => !string.IsNullOrEmpty(x.sK)).ToList();

                string sLoai = (itemLoai.Count() > 0 && itemLoai.GroupBy(x => x.sL).Count() == 1) ? itemLoai.FirstOrDefault().sL : "...";
                string sKhoan = (itemLoai.Count() > 0 && itemKhoan.GroupBy(x => x.sK).Count() == 1) ? itemKhoan.FirstOrDefault().sK : "...";
                string sDonViLap = ListDonVi.Where(x => x.IsChecked).Count() == 1 ? ListDonVi.Where(x => x.IsChecked).FirstOrDefault().DisplayItem : string.Empty;
                //string sDonViCapTren = _sessionService.Current.TenDonVi;

                //DonVi donViCapTren = _nsDonViService.FindByLoai("0", _sessionService.Current.YearOfWork);
                //if (ListDonVi.Where(x => x.IsChecked).Count() > 1)
                //{
                //    sDonViLap = donViCapTren?.TenDonVi;
                //    sDonViCapTren = NSConstants.BO_QUOC_PHONG;
                //}
                //else
                //{
                //    sDonViLap = ListDonVi.FirstOrDefault(x => x.IsChecked).DisplayItem;
                //    sDonViCapTren = donViCapTren?.TenDonVi;
                //}


                //if (DrpReportTypeSelected != null && DrpReportTypeSelected.ValueItem == RPT_TONGHOP)
                //{
                //    sDonViLap = donViCapTren?.TenDonVi;
                //}

                string sDonViCapTren = NSConstants.BO_QUOC_PHONG;

                if (!string.IsNullOrEmpty(sDonViLap))
                {
                    sDonViLap = sDonViLap.ToUpper();
                }
                if (!string.IsNullOrEmpty(sDonViCapTren))
                {
                    sDonViCapTren = sDonViCapTren.ToUpper();
                }
                data.Add("DonViCapTren", sDonViCapTren);
                data.Add("DonViLap", sDonViLap);
                data.Add("Year", NamKeHoachSelected.ValueItem);
                data.Add("Header1", _tieuDe1);
                data.Add("Header2", _tieuDe2);
                var item = lstGroup.Where(x => x.Loai.Equals(4)).FirstOrDefault();
                data.Add("LoaiKhoan", string.Format("Loại {0} - Khoản {1}", sLoai, sKhoan));
                data.Add("Items", lstGroup);
                data.Add("DonViTinh", DrpDonViTinhSelected != null ? DrpDonViTinhSelected.DisplayItem : string.Empty);
                FormatNumber formatNumber = new FormatNumber(Int32.Parse(DrpDonViTinhSelected.ValueItem), exportType);
                _dmChuKyService.GetConfigSign(TypeChuKy.RPT_VDT_KEHOACHVONNAM_DUOCDUYET_GOC, ref data);

                data.Add("FormatNumber", formatNumber);

                return data;
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
                return null;
            }
        }

        private List<VdtKhvVonNamDuocDuyetQuery> CaculatorReport(List<VdtKhvVonNamDuocDuyetQuery> data)
        {
            List<VdtKhvVonNamDuocDuyetQuery> result = new List<VdtKhvVonNamDuocDuyetQuery>();
            foreach (var item in data.Where(n => n.LoaiParent == 0))
            {
                result.AddRange(RecusiveReport(item, data));
            }
            return result;
        }

        private List<VdtKhvVonNamDuocDuyetQuery> RecusiveReport(VdtKhvVonNamDuocDuyetQuery item, List<VdtKhvVonNamDuocDuyetQuery> data)
        {
            List<VdtKhvVonNamDuocDuyetQuery> lstChild = new List<VdtKhvVonNamDuocDuyetQuery>();
            List<VdtKhvVonNamDuocDuyetQuery> results = new List<VdtKhvVonNamDuocDuyetQuery>();

            foreach (var child in data.Where(n => n.IdLoaiCongTrinh == item.IdLoaiCongTrinh).OrderBy(n => n.Loai).ThenBy(n => n.LoaiParent))
            {
                if (!child.IIdDuAn.HasValue)
                {
                    child.FCapPhatTaiKhoBac = 0;
                    child.FCapPhatBangLenhChi = 0;
                    child.TongSo = 0;
                }
                lstChild.Add(child);
            }
            foreach (var child in data.Where(n => !n.IIdDuAn.HasValue && n.IdLoaiCongTrinhParent == item.IdLoaiCongTrinh))
            {
                lstChild.AddRange(RecusiveReport(child, data));
            }
            results.AddRange(lstChild);
            return results;
        }

        private List<VdtKhvVonNamDuocDuyetQuery> CalculateDataReport(List<VdtKhvVonNamDuocDuyetQuery> data)
        {
            try
            {
                List<VdtKhvVonNamDuocDuyetQuery> result = new List<VdtKhvVonNamDuocDuyetQuery>();

                List<VdtKhvVonNamDuocDuyetQuery> childrent = data.Where(x => !x.IsHangCha).ToList();
                List<VdtKhvVonNamDuocDuyetQuery> parent = data.Where(x => x.IsHangCha && (x.LoaiParent.Equals(0) || x.LoaiParent.Equals(1))).ToList();

                data.Where(x => x.IsHangCha && x.LoaiParent.Equals(1)).Select(x =>
                {
                    x.FCapPhatTaiKhoBac = 0;
                    x.FCapPhatBangLenhChi = 0;
                    x.TongSo = 0;
                    return x;
                }).ToList();

                foreach (var pr in parent.Where(x => x.IdLoaiCongTrinh != null))
                {
                    List<VdtKhvVonNamDuocDuyetQuery> lstChilrent = childrent.Where(x => x.IdLoaiCongTrinh.Equals(pr.IdLoaiCongTrinh)).ToList();
                    foreach (var item in lstChilrent.Where(x => (x.FCapPhatTaiKhoBac != 0 || x.FCapPhatBangLenhChi != 0)))
                    {
                        pr.FCapPhatTaiKhoBac += item.FCapPhatTaiKhoBac;
                        pr.FCapPhatBangLenhChi += item.FCapPhatBangLenhChi;
                        pr.TongSo += item.TongSo;
                    }
                }

                foreach (var item in parent.Where(x => (x.FCapPhatTaiKhoBac != 0 || x.FCapPhatBangLenhChi != 0)))
                {
                    CalculateParent(item, item, data);
                }

                result = data.Where(x => (x.FCapPhatTaiKhoBac != 0 || x.FCapPhatBangLenhChi != 0)).ToList();

                return result;
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
                return new List<VdtKhvVonNamDuocDuyetQuery>();
            }
        }

        private void CalculateParent(VdtKhvVonNamDuocDuyetQuery currentItem, VdtKhvVonNamDuocDuyetQuery seftItem, List<VdtKhvVonNamDuocDuyetQuery> data)
        {
            var parrentItem = data.Where(x => x.IdLoaiCongTrinh != null && x.IdLoaiCongTrinh == currentItem.IdLoaiCongTrinhParent).FirstOrDefault();
            if (parrentItem == null) return;
            parrentItem.FCapPhatTaiKhoBac += seftItem.FCapPhatTaiKhoBac;
            parrentItem.FCapPhatBangLenhChi += seftItem.FCapPhatBangLenhChi;
            parrentItem.TongSo += seftItem.TongSo;
            CalculateParent(parrentItem, seftItem, data);
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
                    if (ListDonVi.Where(x => x.IsChecked).Count() == 1)
                    {
                        string sTen = ListDonVi.Where(x => x.IsChecked).FirstOrDefault().DisplayItem;
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
                    var lstDvSelected = ListDonVi.Where(x => x.IsChecked).Select(x => new ComboboxItem() { ValueItem = x.ValueItem, DisplayItem = x.DisplayItem }).ToList();
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

        private void OnExport(ExportType exportType)
        {
            try
            {
                StringBuilder sError = AuthorizationReport();
                List<DonVi> userAgency = _nsDonViService.FindByUser(_sessionService.Current.Principal, _sessionService.Current.YearOfWork, LoaiDonVi.ROOT);
                if (!userAgency.Any(x => x.Loai == LoaiDonVi.ROOT) && sError.Length != 0)
                {
                    System.Windows.MessageBox.Show(string.Format(Resources.UserManagerKHTHReportWarning, _sessionService.Current.Principal, sError.ToString()));
                    return;
                }

                BackgroundWorkerHelper.Run((s, e) =>
                {
                    IsLoading = true;
                    Dictionary<string, object> data = new Dictionary<string, object>();
                    string templateFileName = string.Empty;
                    string fileNamePrefix = string.Empty;
                    ExcelFile xlsFile = null;

                    if (DrpReportTypeSelected != null && DrpReportTypeSelected.ValueItem == RPT_LOAICONGTRINH)
                    {
                        data = HandleDataLoaiCongTrinh(exportType);
                        templateFileName = Path.Combine(ExportPrefix.PATH_TL_KHVN, YearPlanManagerType.RPT_KE_HOACH_VON_NAM_DUOC_DUYET_BY_LOAICONGTRINH);
                        fileNamePrefix = Path.GetFileNameWithoutExtension(YearPlanManagerType.RPT_KE_HOACH_VON_NAM_DUOC_DUYET_BY_LOAICONGTRINH);
                        xlsFile = _exportService.Export<BcDuToanTheoLoaiCongTrinhQuery>(templateFileName, data);
                    }
                    else
                    {
                        data = HandleData(exportType);
                        templateFileName = Path.Combine(ExportPrefix.PATH_TL_KHVN, YearPlanManagerType.RPT_KE_HOACH_VON_NAM_DUOC_DUYET);
                        fileNamePrefix = Path.GetFileNameWithoutExtension(YearPlanManagerType.RPT_KE_HOACH_VON_NAM_DUOC_DUYET);
                        xlsFile = _exportService.Export<VdtKhvVonNamDuocDuyetQuery>(templateFileName, data);
                    }

                    if (data == null || data.Count() == 0) return;

                    string fileNameWithoutExtension = StringUtils.CreateExportFileName(fileNamePrefix);
                    _exportService.FormatAllRowHeight(listItem, "sTenDuAn", 10, 50, xlsFile);


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

        public Expression<Func<VdtKhvPhanBoVon, bool>> CreatePredicate()
        {
            var predicate = PredicateBuilder.True<VdtKhvPhanBoVon>();
            List<string> lstDonViQuanLy = ListDonVi.Where(x => x.IsChecked).Select(x => x.ValueItem).ToList();

            if (_cbxVoucherTypeSelected != null && _cbxVoucherTypeSelected.ValueItem.Equals(RPT_GOC))
            {
                if (DrpReportTypeSelected != null && DrpReportTypeSelected.ValueItem.Equals(RPT_TONGHOP))
                {
                    predicate = predicate.And(x => x.Id == Guid.Parse(DrpVoucherSelected.ValueItem));
                }
                else
                {
                    predicate = predicate.And(x => x.INamKeHoach == Int32.Parse(NamKeHoachSelected.ValueItem));
                    predicate = predicate.And(x => x.IIdNguonVonId == Int32.Parse(NguonVonSelected.ValueItem));
                    predicate = predicate.And(x => x.BIsGoc.Value);
                }
            }

            return predicate;
        }

        private void LoadDonVi()
        {
            try
            {
                List<VdtDmDonViThucHienDuAn> lstDonVi = _vdtDmDonViThucHienDuAnService.FindAll().ToList();

                _listDonVi = _mapper.Map<ObservableCollection<CheckBoxItem>>(lstDonVi.OrderBy(x => x.IIdMaDonVi));

                OnPropertyChanged(nameof(LabelSelectedCountDonVi));
                OnPropertyChanged(nameof(SelectAllDonVi));
                OnPropertyChanged(nameof(IsExportData));
                // Filter
                _donViView = CollectionViewSource.GetDefaultView(ListDonVi);
                _donViView.Filter = obj => string.IsNullOrWhiteSpace(_searchDonVi)
                                           || (obj is CheckBoxItem item && item.DisplayItem.Contains(_searchDonVi, StringComparison.OrdinalIgnoreCase));

                foreach (var model in ListDonVi)
                {
                    model.PropertyChanged += (sender, args) =>
                    {
                        OnPropertyChanged(nameof(LabelSelectedCountDonVi));
                        OnPropertyChanged(nameof(SelectAllDonVi));
                        OnPropertyChanged(nameof(IsExportData));
                    };
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        private void LoadNamKeHoach()
        {
            try
            {
                NamKeHoach = new ObservableCollection<ComboboxItem>(Models.Where(model => model.iNamKeHoach != null)
                .GroupBy(model => model.iNamKeHoach)
                .Select(g => g.First())
                .OrderByDescending(model => model.iNamKeHoach)
                .Select(model => new ComboboxItem
                {
                    DisplayItem = model.iNamKeHoach?.ToString(),
                    ValueItem = model.iNamKeHoach?.ToString()
                }));

                if (NamKeHoach.Any())
                {
                    NamKeHoachSelected = NamKeHoach.ElementAt(0);
                }
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
                var listNsNguonNganSach = _nsNguonNganSachService.FindNguonNganSach().ToList();

                NguonVon = _mapper.Map<ObservableCollection<ComboboxItem>>(listNsNguonNganSach);

                if (NamKeHoach.Any())
                {
                    NguonVonSelected = NguonVon.ElementAt(0);
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
                listAllVdtDmLoaiCongTrinh = _dmLoaiCongTrinhService.GetAll();

                var vdtDmLoaiCongTrinhs = listAllVdtDmLoaiCongTrinh.Where(item => item.IIdParent == null);

                if (vdtDmLoaiCongTrinhs.Any())
                {
                    var comboboxItems = vdtDmLoaiCongTrinhs.Select(item => new ComboboxItem
                    {
                        DisplayItem = item.STenLoaiCongTrinh,
                        ValueItem = item.IIdLoaiCongTrinh.ToString()
                    }).ToList();
                    comboboxItems.Add(new ComboboxItem
                    {
                        DisplayItem = "Tất cả",
                        ValueItem = "-1"
                    });

                    LoaiCongTrinh = new ObservableCollection<ComboboxItem>(comboboxItems);
                    LoaiCongTrinhSelected = LoaiCongTrinh.ElementAt(0);
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        private void OnConfigSign()
        {
            try
            {
                DmChuKyModel chuKyModel = new DmChuKyModel();
                if (CbxVoucherTypeSelected != null)
                {
                    if (CbxVoucherTypeSelected.ValueItem.Equals(RPT_GOC) || CbxVoucherTypeSelected.ValueItem.Equals(RPT_LOAICONGTRINH))
                    {
                        _dmChuKy = _dmChuKyService.FindByCondition(x => x.IdType.Equals(TypeChuKy.RPT_VDT_KEHOACHVONNAM_DUOCDUYET_GOC) && x.ITrangThai == StatusType.ACTIVE).FirstOrDefault();
                    }
                }

                if (_dmChuKy == null)
                {
                    if (CbxVoucherTypeSelected != null)
                    {
                        if (CbxVoucherTypeSelected.ValueItem.Equals(RPT_GOC) || CbxVoucherTypeSelected.ValueItem.Equals(RPT_LOAICONGTRINH))
                        {
                            chuKyModel.IdType = TypeChuKy.RPT_VDT_KEHOACHVONNAM_DUOCDUYET_GOC;
                        }
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
                    TieuDe1 = chuKy.TieuDe1MoTa;
                    TieuDe2 = chuKy.TieuDe2MoTa;
                };
                DmChuKyDialogViewModel.Init();
                DmChuKyDialogViewModel.ShowDialog();
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }
    }
}
