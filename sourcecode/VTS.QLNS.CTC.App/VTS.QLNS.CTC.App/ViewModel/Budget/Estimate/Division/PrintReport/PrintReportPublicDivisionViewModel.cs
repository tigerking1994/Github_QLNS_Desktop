using AutoMapper;
using FlexCel.Core;
using log4net;
using MaterialDesignThemes.Wpf;
using System;
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
using VTS.QLNS.CTC.App.Properties;
using VTS.QLNS.CTC.App.Service;
using VTS.QLNS.CTC.App.Service.Impl;
using VTS.QLNS.CTC.App.Service.UserFunction;
using VTS.QLNS.CTC.App.View.Budget.Estimate.Division.PrintReport;
using VTS.QLNS.CTC.App.ViewModel.Category;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Criteria;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Service;
using VTS.QLNS.CTC.Utility;
using VTS.QLNS.CTC.Utility.Enum;

namespace VTS.QLNS.CTC.App.ViewModel.Budget.Estimate.Division.PrintReport
{
    public class PrintReportPublicDivisionViewModel : GridViewModelBase<DtChungTuModel>
    {
        private readonly ISessionService _sessionService;
        private readonly IMapper _mapper;
        private readonly INsDonViService _nSDonViService;
        private readonly INsDtChungTuService _dtChungTuService;
        private readonly INsDtChungTuChiTietService _dtChungTuChiTietService;
        private readonly IExportService _exportService;
        private readonly IDanhMucService _danhMucService;
        private readonly IDmChuKyService _dmChuKyService;
        private readonly ILog _logger;
        private readonly IDmCongKhaiTaiChinhService _dmCongKhaiTaiChinhService;
        private DmChuKyDialogViewModel DmChuKyDialogViewModel;
        private SessionInfo _sessionInfo;
        private EstimationVoucherDetailCriteria _searchCondition;
        private DtChungTuModel _dtChungTuModel;
        private ICollectionView _lnsView;
        private ICollectionView _specializedView;
        private string _voucherId;
        private DmChuKy _dmChuKy;
        private string _typeChuKy;
        private string _diaDiem;
        private string _chiTietToi;
        private string _cap1;
        private string _cap2;
        private string _ngay;

        public int LoaiChungTu;

        private bool isActive;

        public RelayCommand ExportExcelActionCommand { get; }
        public RelayCommand ExportSignatureActionCommand { get; }
        public RelayCommand PrintActionCommand { get; }
        public RelayCommand PrintBrowserCommand { get; }
        public RelayCommand ConfigSignCommand { get; }
        public override string Name
        {
            get => DivisionEstimateScreen.DivisionEstimatePrintTypeName[(int)DivisionPrintType];
        }

        public override string Title
        {
            get => DivisionEstimateScreen.DivisionEstimatePrintTypeTitle[(int)DivisionPrintType];
        }

        public override string Description
        {
            get => DivisionEstimateScreen.DivisionEstimatePrintTypeDescription[(int)DivisionPrintType];
        }

        public override Type ContentType => typeof(PrintReportReceiveDivision);
        public DivisionEstimatePrintType DivisionPrintType { get; set; }

        private ObservableCollection<ComboboxItem> _reportType = new ObservableCollection<ComboboxItem>();
        public ObservableCollection<ComboboxItem> ReportType
        {
            get => _reportType;
            set => SetProperty(ref _reportType, value);
        }

        private ComboboxItem _selectedReportType;
        public ComboboxItem SelectedReportType
        {
            get => _selectedReportType;
            set
            {
                SetProperty(ref _selectedReportType, value);
                LoadTypeChuKy();
            }
        }

        private ObservableCollection<ComboboxItem> _quarterMonths = new ObservableCollection<ComboboxItem>();
        public ObservableCollection<ComboboxItem> QuarterMonths
        {
            get => _quarterMonths;
            set => SetProperty(ref _quarterMonths, value);
        }

        private ComboboxItem _quarterMonthSelected;
        public ComboboxItem QuarterMonthSelected
        {
            get => _quarterMonthSelected;
            set
            {
                SetProperty(ref _quarterMonthSelected, value);
                LoadLNS();
            }
        }

        private string _txtTitleFirst;

        public string TxtTitleFirst
        {
            get => _txtTitleFirst;
            set => SetProperty(ref _txtTitleFirst, value);
        }

        private string _txtTitleSecond;
        public string TxtTitleSecond
        {
            get => _txtTitleSecond;
            set => SetProperty(ref _txtTitleSecond, value);
        }

        private string _txtTitleThird;
        public string TxtTitleThird
        {
            get => _txtTitleThird;
            set => SetProperty(ref _txtTitleThird, value);
        }

        private ObservableCollection<ComboboxItem> _catUnitTypes = new ObservableCollection<ComboboxItem>();

        public ObservableCollection<ComboboxItem> CatUnitTypes
        {
            get => _catUnitTypes;
            set => SetProperty(ref _catUnitTypes, value);
        }

        private ComboboxItem _catUnitTypeSelected;

        public ComboboxItem CatUnitTypeSelected
        {
            get => _catUnitTypeSelected;
            set => SetProperty(ref _catUnitTypeSelected, value);
        }

        private bool _selectAllLns;

        public bool SelectAllLns
        {
            get => ListLns.All(item => item.IsChecked);
            set
            {
                SetProperty(ref _selectAllLns, value);
                foreach (var item in ListLns) item.IsChecked = _selectAllLns;
            }
        }

        public string LabelSelectedCountLns
        {
            get => $"DANH MỤC CÔNG KHAI ({ListLns.Count(item => item.IsChecked)}/{ListLns.Count})";
        }

        private string _searchLns;

        public string SearchLns
        {
            get => _searchLns;
            set
            {
                if (SetProperty(ref _searchLns, value))
                {
                    _lnsView.Refresh();
                }
            }
        }

        private ObservableCollection<NsDanhMucCongKhaiCustomModel> _listLns;
        public ObservableCollection<NsDanhMucCongKhaiCustomModel> ListLns
        {
            get => _listLns;
            set => SetProperty(ref _listLns, value);
        }

        private ObservableCollection<ComboboxItem> _paperPrintTypes = new ObservableCollection<ComboboxItem>();

        public ObservableCollection<ComboboxItem> PaperPrintTypes
        {
            get => _paperPrintTypes;
            set => SetProperty(ref _paperPrintTypes, value);
        }

        private ComboboxItem _paperPrintTypeSelected;

        public ComboboxItem PaperPrintTypeSelected
        {
            get => _paperPrintTypeSelected;
            set => SetProperty(ref _paperPrintTypeSelected, value);
        }

        private ObservableCollection<ComboboxItem> _soQuyetDinhTuDots = new ObservableCollection<ComboboxItem>();

        public ObservableCollection<ComboboxItem> SoQuyetDinhTuDots
        {
            get => _soQuyetDinhTuDots;
            set => SetProperty(ref _soQuyetDinhTuDots, value);
        }

        private ComboboxItem _soQuyetDinhTuDotSelected;

        public ComboboxItem SoQuyetDinhTuDotSelected
        {
            get => _soQuyetDinhTuDotSelected;
            set => SetProperty(ref _soQuyetDinhTuDotSelected, value);
        }

        private ObservableCollection<ComboboxItem> _soQuyetDinhDenDots = new ObservableCollection<ComboboxItem>();

        public ObservableCollection<ComboboxItem> SoQuyetDinhDenDots
        {
            get => _soQuyetDinhDenDots;
            set => SetProperty(ref _soQuyetDinhDenDots, value);
        }

        private ComboboxItem _soQuyetDinhDenDotSelected;

        public ComboboxItem SoQuyetDinhDenDotSelected
        {
            get => _soQuyetDinhDenDotSelected;
            set => SetProperty(ref _soQuyetDinhDenDotSelected, value);
        }

        public List<Guid> ListId { get; set; }

        public PrintReportPublicDivisionViewModel(
            INsDtChungTuService dtChungTuService,
            INsDtChungTuChiTietService dtChungTuChiTietService,
            IExportService exportService,
            INsDonViService nSDonViService,
            ILog logger,
            IDanhMucService danhMucService,
            ISessionService sessionService,
            IMapper mapper,
            IDmChuKyService dmChuKyService,
            IDmCongKhaiTaiChinhService dmCongKhaiTaiChinhService,
            DmChuKyDialogViewModel dmChuKyDialogViewModel)
        {
            _sessionService = sessionService;
            _mapper = mapper;
            _dtChungTuService = dtChungTuService;
            _dtChungTuChiTietService = dtChungTuChiTietService;
            _exportService = exportService;
            _danhMucService = danhMucService;
            _nSDonViService = nSDonViService;
            _logger = logger;
            _dmChuKyService = dmChuKyService;
            DmChuKyDialogViewModel = dmChuKyDialogViewModel;
            _dmCongKhaiTaiChinhService = dmCongKhaiTaiChinhService;

            ExportExcelActionCommand = new RelayCommand(obj => OnExport(ExportType.EXCEL));
            ExportSignatureActionCommand = new RelayCommand(ExportSignature);
            PrintActionCommand = new RelayCommand(obj => OnExport(ExportType.PDF));
            PrintBrowserCommand = new RelayCommand(obj => OnExport(ExportType.PDF));
            ConfigSignCommand = new RelayCommand(obj => OnConfigSign());
        }

        public override void Init()
        {
            try
            {
                isActive = true;
                _sessionInfo = _sessionService.Current;
                LoadTypeReport();
                LoadQuarterMonths();
                LoadCatUnitTypes();
                GetCauHinhHeThong();
                LoadLNS();
                LoadTuDotDenDot();
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        private void LoadTypeReport()
        {
            var typeReport = new List<ComboboxItem>
            {
                new ComboboxItem {DisplayItem = "Biểu số 01/QĐ-CKNS, 02/CKNS-BC", ValueItem = "1"},
                new ComboboxItem {DisplayItem = "Biểu số 01/CKNS: Công khai dự toán nhận, phân bổ thu - chi", ValueItem = "2"},
                new ComboboxItem {DisplayItem = "Biểu số 02/CKNS: Công khai dự toán thu - chi NSNN", ValueItem = "3"},
                new ComboboxItem {DisplayItem = "Biểu số 05/CKNS: Dự toán chi NSNN hỗ trợ - Phân bổ đơn vị", ValueItem = "4"},
                new ComboboxItem {DisplayItem = "Biểu số 06/CKNS: Dự toán chi NSNN hỗ trợ - Tổng hợp", ValueItem = "5"}
            };

            ReportType = new ObservableCollection<ComboboxItem>(typeReport);
            SelectedReportType = ReportType.FirstOrDefault();
        }

        private void LoadQuarterMonths()
        {
            var quarterMonths = new List<ComboboxItem>
            {
                new ComboboxItem {DisplayItem = "Đầu năm", ValueItem = "0"},
                new ComboboxItem {DisplayItem = "3 tháng", ValueItem = "3"},
                new ComboboxItem {DisplayItem = "6 tháng", ValueItem = "6"},
                new ComboboxItem {DisplayItem = "9 tháng", ValueItem = "9"},
                new ComboboxItem {DisplayItem = "Cả năm", ValueItem = "12"}

            };

            QuarterMonths = new ObservableCollection<ComboboxItem>(quarterMonths);
            QuarterMonthSelected = QuarterMonths.FirstOrDefault();
        }


        private void LoadCatUnitTypes()
        {
            CatUnitTypes = new ObservableCollection<ComboboxItem>();
            List<DanhMuc> listDonViTinh = _danhMucService.FindByCondition(x => x.SType.Contains(TypeDanhMuc.DM_DONVITINH)
                && x.ITrangThai == StatusType.ACTIVE
                && x.INamLamViec == _sessionService.Current.YearOfWork).OrderBy(n => n.SGiaTri).ToList();
            if (listDonViTinh == null || listDonViTinh.Count <= 0)
            {
                CatUnitTypes.Add(new ComboboxItem { ValueItem = DonViTinh.DONG_VALUE, DisplayItem = DonViTinh.DONG });
            }
            foreach (var dvt in listDonViTinh)
            {
                CatUnitTypes.Add(new ComboboxItem { ValueItem = dvt.SGiaTri.ToString(), DisplayItem = dvt.STen });
            }
            _catUnitTypeSelected = CatUnitTypes.ElementAt(0);
        }

        private void AddChuKy(Dictionary<string, object> data)
        {
            data.Add("DiaDiem", string.Format("{0}, {1}", _diaDiem, DateUtils.FormatDateReport(ReportDate)));
            data.Add("ChucDanh1", _dmChuKy != null ? _dmChuKy.ChucDanh1MoTa : string.Empty);
            data.Add("ChucDanh2", _dmChuKy != null ? _dmChuKy.ChucDanh2MoTa : string.Empty);
            data.Add("ChucDanh3", _dmChuKy != null ? _dmChuKy.ChucDanh3MoTa : string.Empty);
            data.Add("ThuaLenh1", _dmChuKy != null ? _dmChuKy.ThuaLenh1MoTa : string.Empty);
            data.Add("ThuaLenh2", _dmChuKy != null ? _dmChuKy.ThuaLenh2MoTa : string.Empty);
            data.Add("ThuaLenh3", _dmChuKy != null ? _dmChuKy.ThuaLenh3MoTa : string.Empty);
            data.Add("Ten1", _dmChuKy != null ? _dmChuKy.Ten1MoTa : string.Empty);
            data.Add("Ten2", _dmChuKy != null ? _dmChuKy.Ten2MoTa : string.Empty);
            data.Add("Ten3", _dmChuKy != null ? _dmChuKy.Ten3MoTa : string.Empty);
        }

        private void GetCauHinhHeThong()
        {
            var lstCauHinh = _danhMucService.FindByType(TypeDanhMuc.DM_CAUHINH);
            _cap1 = string.Empty;
            _cap2 = _sessionService.Current.TenDonVi;
            _ngay = string.Format("ngày {0} tháng {1} năm {2}", DateTime.Now.ToString("dd"), DateTime.Now.ToString("MM"), DateTime.Now.ToString("yyyy"));

            if (lstCauHinh.Any(n => n.IIDMaDanhMuc == "DV_QUANLY"))
            {
                _cap1 = lstCauHinh.FirstOrDefault(n => n.IIDMaDanhMuc == "DV_QUANLY").SGiaTri;
            }
            if (lstCauHinh.Any(n => n.IIDMaDanhMuc == "DIADIEM"))
            {
                _diaDiem = lstCauHinh.FirstOrDefault(n => n.IIDMaDanhMuc == "DIADIEM").SGiaTri;
            }
        }

        private void LoadTuDotDenDot()
        {
            if (Items != null && Items.Any())
            {
                ObservableCollection<ComboboxItem> checkBoxItems = _mapper.Map<ObservableCollection<ComboboxItem>>(Items);
                SoQuyetDinhTuDots = checkBoxItems;
                _soQuyetDinhTuDotSelected = SoQuyetDinhTuDots.ElementAt(0);

                SoQuyetDinhDenDots = checkBoxItems;
                _soQuyetDinhDenDotSelected = SoQuyetDinhDenDots.ElementAt(1);
            }
        }

        private void LoadTieuDe()
        {
            _dmChuKy = _dmChuKyService.FindByCondition(x => x.IdType.Equals(_typeChuKy) && x.ITrangThai == StatusType.ACTIVE).FirstOrDefault();
            if (_dmChuKy == null)
                _dmChuKy = new DmChuKy();
            TxtTitleFirst = "Dự toán thu, chi ngân sách nhà nước";
            TxtTitleSecond = "(Kèm theo Quyết định số   /QĐ-     Ngày.....tháng....năm";


            if (!string.IsNullOrEmpty(_dmChuKy.TieuDe1MoTa))
                TxtTitleFirst = string.Format(_dmChuKy.TieuDe1MoTa);
            if (!string.IsNullOrEmpty(_dmChuKy.TieuDe2MoTa))
                TxtTitleSecond = string.Format(_dmChuKy.TieuDe2MoTa);
            if (!string.IsNullOrEmpty(_dmChuKy.TieuDe3MoTa))
                TxtTitleThird = string.Format(_dmChuKy.TieuDe3MoTa);
        }

        private void LoadLNS()
        {
            List<NsDanhMucCongKhai> listDanhMucCongKhai = new List<NsDanhMucCongKhai>();
            string listLNSSeparate = string.Empty;
            if (QuarterMonthSelected != null)
            {

                List<NsDanhMucCongKhai> lstDanhMucCongKhai = new List<NsDanhMucCongKhai>();
                var predicate_danhmuc = PredicateBuilder.True<NsDanhMucCongKhai>();
                predicate_danhmuc = predicate_danhmuc.And(x => x.iNamLamViec == _sessionInfo.YearOfWork);
                lstDanhMucCongKhai = _dmCongKhaiTaiChinhService.FindByCondition(predicate_danhmuc).Where(x => x.bHangCha || (!x.bHangCha && x.iID_DMCongKhai_Cha == null)).ToList();

                List<NsDanhMucCongKhaiCustomModel> lstDanhMucModel = new List<NsDanhMucCongKhaiCustomModel>();
                lstDanhMucModel = lstDanhMucCongKhai.Select(d => new NsDanhMucCongKhaiCustomModel
                {
                    Id = d.Id,
                    sMoTa = String.IsNullOrEmpty(d.STT) ? d.sMoTa : (d.STT + StringUtils.DOT_SPLIT + d.sMoTa),
                    iID_DMCongKhai_Cha = d.iID_DMCongKhai_Cha,
                    iNamLamViec = d.iNamLamViec,
                    bHangCha = d.bHangCha,
                    STT = d.STT,
                    sMa = d.sMa,
                    sMaCha = d.sMaCha
                }).ToList();

                OrderByTreeDanhMucCongKhai(lstDanhMucModel);
                lstDanhMucModel = lstDanhMucModel.OrderBy(x => x.sMa).ToList();

                ListLns = _mapper.Map<ObservableCollection<NsDanhMucCongKhaiCustomModel>>(lstDanhMucModel);

                // Filter
                _lnsView = CollectionViewSource.GetDefaultView(ListLns);
                _lnsView.Filter = ListLNSFilter;

                if (_listLns != null && _listLns.Count > 0)
                {
                    foreach (var model in _listLns)
                    {
                        model.PropertyChanged += (sender, args) =>
                        {
                            if (args.PropertyName == nameof(NsDanhMucCongKhaiCustomModel.IsChecked))
                            {
                                //TH1: Nếu check or uncheck cha thì check or uncheck all con
                                var lstChild = ListLns.Where(x => x.iID_DMCongKhai_Cha == model.Id).ToList();
                                var lstParent = ListLns.Where(x => x.Id == model.iID_DMCongKhai_Cha).ToList();
                                var lstCheck = ListLns.Where(x => x.iID_DMCongKhai_Cha == model.iID_DMCongKhai_Cha).ToList();
                                if (lstChild.Count() > 0 && lstChild.Any(x => x.IsChecked != model.IsChecked) && isActive)
                                {
                                    lstChild.Select(x => { x.IsChecked = model.IsChecked; return x; }).ToList();
                                }
                                if (lstParent.Count() > 0)
                                {
                                    isActive = false;
                                    if (!model.IsChecked || lstCheck.All(x => x.IsChecked)) //false
                                    {
                                        lstParent.Select(x => { x.IsChecked = model.IsChecked; return x; }).ToList();
                                    }
                                    isActive = true;
                                }

                                OnPropertyChanged(nameof(LabelSelectedCountLns));
                                OnPropertyChanged(nameof(SelectAllLns));
                            }
                        };
                    }
                }
            }
        }

        private void OrderByTreeDanhMucCongKhai(List<NsDanhMucCongKhaiCustomModel> lstDanhMucModel)
        {
            foreach (var item in lstDanhMucModel.Where(x => x.iID_DMCongKhai_Cha == null))
            {
                AddListTreeChilDanhMucCongKhai(item, lstDanhMucModel);

            }
        }

        private void AddListTreeChilDanhMucCongKhai(NsDanhMucCongKhaiCustomModel danhmuc, List<NsDanhMucCongKhaiCustomModel> lstDanhMucModel)
        {
            if (lstDanhMucModel.Any(n => n.iID_DMCongKhai_Cha == danhmuc.Id))
            {
                foreach (var item in lstDanhMucModel.Where(n => n.iID_DMCongKhai_Cha == danhmuc.Id))
                {

                    item.sSpace = danhmuc.sSpace + StringUtils.SPACE_DIVISION;
                    item.sMoTa = item.sSpace + item.sMoTa;
                    AddListTreeChilDanhMucCongKhai(item, lstDanhMucModel);
                }
            }
        }

        private bool ListLNSFilter(object obj)
        {
            bool result = true;
            var item = (NsDanhMucCongKhaiCustomModel)obj;
            if (!string.IsNullOrWhiteSpace(_searchLns))
                result = item.sMoTa.ToLower().Contains(_searchLns!.ToLower());
            item.IsFilter = result;
            return result;
        }

        private void OnExport(ExportType exportType)
        {
            try
            {
                switch (Convert.ToInt16(SelectedReportType.ValueItem))
                {
                    case 1:
                        ExportMau_01QdCkns_02CknsBc(exportType);
                        break;
                    case LOAI_BAOCAO_CONGKHAI.BIEUSO_02_CKNS:
                        ExportBaoCao_BieuSo02CKNS(exportType);
                        break;
                    case LOAI_BAOCAO_CONGKHAI.BIEUSO_06_CKNS:
                        ExportBaoCao_BieuSo06CKNS(exportType);
                        break;
                    case LOAI_BAOCAO_CONGKHAI.BIEUSO_01_CKNS:
                        ExportBaoCao_BieuSo01CKNS(exportType);
                        break;
                    case LOAI_BAOCAO_CONGKHAI.BIEUSO_05_CKNS:
                        ExportBaoCao_BieuSo01CKNS(exportType);
                        break;
                    default:
                        return;
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        private void ExportSignature(object param)
        {
            DialogHost.CloseDialogCommand.Execute(null, null);
        }


        private void OnConfigSign()
        {
            DmChuKyModel chuKyModel = new DmChuKyModel();
            _dmChuKy = _dmChuKyService.FindByCondition(x => x.IdType.Equals(_typeChuKy) && x.ITrangThai == StatusType.ACTIVE).FirstOrDefault();
            if (_dmChuKy == null)
                chuKyModel.IdType = _typeChuKy;
            else
                chuKyModel = _mapper.Map<DmChuKyModel>(_dmChuKy);
            DmChuKyDialogViewModel.DmChuKyModel = chuKyModel;
            DmChuKyDialogViewModel.SavedAction = obj =>
            {
                LoadTieuDe();
            };
            DmChuKyDialogViewModel.Init();
            DmChuKyDialogViewModel.ShowDialog();
        }

        private void LoadTypeChuKy()
        {
            _typeChuKy = string.Empty;
            if (SelectedReportType != null)
            {
                switch (Convert.ToInt16(SelectedReportType.ValueItem))
                {
                    case LOAI_BAOCAO_CONGKHAI.BIEUSO_01_QĐ_CKNS:
                        _typeChuKy = TypeChuKy.RPT_NS_DUTOAN_QD_CONGKHAINGANSACH;
                        break;
                    case LOAI_BAOCAO_CONGKHAI.BIEUSO_02_CKNS:
                        _typeChuKy = TypeChuKy.RPT_NS_DUTOAN_CONGKHAI_02CKNS;
                        break;
                    case LOAI_BAOCAO_CONGKHAI.BIEUSO_06_CKNS:
                        _typeChuKy = TypeChuKy.RPT_NS_DUTOAN_CONGKHAI_06CKNS;
                        break;
                }
            }
            LoadTieuDe();
        }

        public void ExportBaoCao_BieuSo02CKNS(ExportType exportType)
        {
            var DNgayQuyetDinhTuDot = _soQuyetDinhTuDotSelected.ValueItem != null ? Items.FirstOrDefault(x => x.IIdDotNhan.ToString().Equals(_soQuyetDinhTuDotSelected.ValueItem)).DNgayQuyetDinh : null;
            var DNgayQuyetDinhDenDot = _soQuyetDinhDenDotSelected.ValueItem != null ? Items.FirstOrDefault(x => x.IIdDotNhan.ToString().Equals(_soQuyetDinhDenDotSelected.ValueItem)).DNgayQuyetDinh : null;
            if (DNgayQuyetDinhTuDot == null || DNgayQuyetDinhDenDot == null || DNgayQuyetDinhTuDot > DNgayQuyetDinhDenDot)
            {
                MessageBoxHelper.Info(string.Format(Resources.VoucherBudgetAllocationHasNoData, _soQuyetDinhTuDotSelected.DisplayItem, _soQuyetDinhDenDotSelected.DisplayItem));
                return;
            }
            BackgroundWorkerHelper.Run((s, e) =>
            {
                IsLoading = true;
                List<ExportResult> results = new List<ExportResult>();
                string templateFileName;
                string fileNamePrefix;
                string fileNameWithoutExtension;

                _dmChuKy = _dmChuKyService.FindByCondition(x => x.IdType.Equals(_typeChuKy) && x.ITrangThai == StatusType.ACTIVE).FirstOrDefault();
                var listSelected = ListLns.Where(n => n.IsChecked).Select(n => n.Id).ToList();
                string lns = ListLns.Any() ? string.Join(StringUtils.COMMA, listSelected) : string.Empty;
                int donViTinh = GetDonViTinh();
                var listChungTuIds = Items.Where(x => x.DNgayQuyetDinh.Value.Date >= DNgayQuyetDinhTuDot.Value.Date && x.DNgayQuyetDinh.Value.Date <= DNgayQuyetDinhDenDot.Value.Date).Select(x => x.IIdDotNhan).ToList();
                List<NsDtChungTuCongKhaiQuery> lstChungTuBaoCao = _dtChungTuChiTietService.GetDataBaoCaoDanhMucCongKhai02Clone(_sessionInfo.YearOfWork, _sessionInfo.YearOfBudget, _sessionInfo.Budget, Convert.ToInt16(QuarterMonthSelected.ValueItem), lns, donViTinh, string.Join(",", listChungTuIds)).ToList();
                var listUnSelected = ListLns.Where(n => !n.IsChecked).Select(n => n.Id).ToList();

                List<NsDtChungTuCongKhaiQuery> lstData = new List<NsDtChungTuCongKhaiQuery>();
                GetListBaoCao(lstData, lstChungTuBaoCao);

                CalculateData(lstData);
                lstData = lstData.OrderBy(x => x.sMa).ToList();

                Dictionary<string, object> data = new Dictionary<string, object>();
                FormatNumber formatNumber = new FormatNumber(donViTinh, exportType);
                data.Add("FormatNumber", formatNumber);
                data.Add("H1", string.Empty);
                data.Add("Cap1", _cap1);
                data.Add("Cap2", _cap2);
                data.Add("Ngay", _ngay);
                data.Add("TieuDe1", TxtTitleFirst);
                data.Add("TieuDe2", TxtTitleSecond);
                data.Add("TieuDe3", TxtTitleThird);
                data.Add("Header1", CatUnitTypeSelected.DisplayItem);
                data.Add("DonViTinh", CatUnitTypeSelected.DisplayItem);
                data.Add("Items", lstData);
                AddChuKy(data);

                if (lstData.Count() > 0)
                {
                    data.Add("Count", 10000);
                    templateFileName = Path.Combine(ExportPrefix.PATH_TL_DT, ExportFileName.RPT_NS_DUTOAN_CONGKHAI_02CKNS_EXCEL);
                    //if (exportType == ExportType.EXCEL)
                    //{

                    //}
                    //else
                    //{
                    //    templateFileName = Path.Combine(ExportPrefix.PATH_TL_DT, ExportFileName.RPT_NS_DUTOAN_CONGKHAI_02CKNS);
                    //}
                }
                else
                {
                    templateFileName = Path.Combine(ExportPrefix.PATH_TL_DT, ExportFileName.RPT_NS_DUTOAN_CONGKHAI_02CKNS_To1_EMPTY);
                }

                fileNamePrefix = Path.GetFileNameWithoutExtension(templateFileName) + "_Tờ 1";
                fileNameWithoutExtension = StringUtils.CreateExportFileName(fileNamePrefix);
                var xlsFile = _exportService.Export<NsDtChungTuCongKhaiQuery, GenericReportHeader>(templateFileName, data);
                results.Add(new ExportResult("Biểu số 02/CKNS", fileNameWithoutExtension, null, xlsFile));
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

        private void GetListBaoCao(List<NsDtChungTuCongKhaiQuery> listData, List<NsDtChungTuCongKhaiQuery> lstChild)
        {
            List<NsDanhMucCongKhai> lstDanhMucCongKhai = _dmCongKhaiTaiChinhService.FindByCondition(n => n.iNamLamViec == _sessionService.Current.YearOfWork).ToList();
            List<NsDtChungTuCongKhaiQuery> lstChungTuParent = new List<NsDtChungTuCongKhaiQuery>();
            foreach (var item in lstChild)
            {
                listData.Add(item);
                AddListParent(item, listData, lstDanhMucCongKhai);
            }
        }

        private void AddListParent(NsDtChungTuCongKhaiQuery danhmuc, List<NsDtChungTuCongKhaiQuery> listData, List<NsDanhMucCongKhai> lstDanhMucCongKhai)
        {
            var parent = lstDanhMucCongKhai.Where(x => x.Id == danhmuc.Id_DanhMucCha).FirstOrDefault();
            if (parent != null)
            {
                if (!listData.Any(x => x.Id_DanhMuc == parent.Id))
                {
                    NsDtChungTuCongKhaiQuery dm = new NsDtChungTuCongKhaiQuery
                    {
                        Id_DanhMuc = parent.Id,
                        Id_DanhMucCha = parent.iID_DMCongKhai_Cha,
                        STT = parent.STT,
                        sMoTa = parent.sMoTa,
                        bHangCha = parent.bHangCha,
                        sMa = parent.sMa,
                        fTuChi = 0
                    };
                    listData.Add(dm);
                    AddListParent(dm, listData, lstDanhMucCongKhai);
                }
            }
        }

        public void ExportBaoCao_BieuSo06CKNS(ExportType exportType)
        {
            var DNgayQuyetDinhTuDot = _soQuyetDinhTuDotSelected.ValueItem != null ? Items.FirstOrDefault(x => x.IIdDotNhan.ToString().Equals(_soQuyetDinhTuDotSelected.ValueItem)).DNgayQuyetDinh : null;
            var DNgayQuyetDinhDenDot = _soQuyetDinhDenDotSelected.ValueItem != null ? Items.FirstOrDefault(x => x.IIdDotNhan.ToString().Equals(_soQuyetDinhDenDotSelected.ValueItem)).DNgayQuyetDinh : null;
            if (DNgayQuyetDinhTuDot == null || DNgayQuyetDinhDenDot == null || DNgayQuyetDinhTuDot > DNgayQuyetDinhDenDot)
            {
                MessageBoxHelper.Info(string.Format(Resources.VoucherBudgetAllocationHasNoData, _soQuyetDinhTuDotSelected.DisplayItem, _soQuyetDinhDenDotSelected.DisplayItem));
                return;
            }
            BackgroundWorkerHelper.Run((s, e) =>
            {
                IsLoading = true;
                List<ExportResult> results = new List<ExportResult>();
                string templateFileName;
                string fileNamePrefix;
                string fileNameWithoutExtension;

                _dmChuKy = _dmChuKyService.FindByCondition(x => x.IdType.Equals(_typeChuKy) && x.ITrangThai == StatusType.ACTIVE).FirstOrDefault();
                var listSelected = ListLns.Where(n => n.IsChecked).Select(n => n.Id).ToList();
                string lns = ListLns.Any() ? string.Join(StringUtils.COMMA, listSelected) : string.Empty;
                int donViTinh = GetDonViTinh();
                var listChungTuIds = Items.Where(x => x.DNgayQuyetDinh.Value.Date >= DNgayQuyetDinhTuDot.Value.Date && x.DNgayQuyetDinh.Value.Date <= DNgayQuyetDinhDenDot.Value.Date).Select(x => x.IIdDotNhan).ToList();
                List<NsDtChungTuCongKhaiQuery> lstChungTuBaoCao = _dtChungTuChiTietService.GetDataBaoCaoDanhMucCongKhai02Clone(_sessionInfo.YearOfWork, _sessionInfo.YearOfBudget, _sessionInfo.Budget, int.Parse(QuarterMonthSelected.ValueItem), lns, donViTinh, string.Join(",", listChungTuIds)).ToList();
                var listUnSelected = ListLns.Where(n => !n.IsChecked).Select(n => n.Id).ToList();

                lstChungTuBaoCao = lstChungTuBaoCao.Where(x => (x.Id_DanhMucCha != null && listSelected.Contains(x.Id_DanhMucCha.Value) || ((x.bHangCha || (!x.bHangCha && x.Id_DanhMucCha == null)) && listSelected.Contains(x.Id_DanhMuc)))).ToList();
                List<NsDtChungTuCongKhaiQuery> lstData = new List<NsDtChungTuCongKhaiQuery>();
                GetListBaoCao(lstData, lstChungTuBaoCao);

                CalculateData(lstData);
                lstData = lstData.OrderBy(x => x.sMa).ToList();
                //Export excel
                Dictionary<string, object> data = new Dictionary<string, object>();
                FormatNumber formatNumber = new FormatNumber(donViTinh, exportType);

                data.Add("FormatNumber", formatNumber);
                data.Add("H1", string.Empty);
                data.Add("Cap1", _cap1);
                data.Add("Cap2", _cap2);
                data.Add("Ngay", _ngay);
                data.Add("TieuDe1", TxtTitleFirst);
                data.Add("TieuDe2", TxtTitleSecond);
                data.Add("TieuDe3", TxtTitleThird);
                data.Add("Header1", CatUnitTypeSelected.DisplayItem);
                data.Add("DonViTinh", CatUnitTypeSelected.DisplayItem);
                data.Add("Items", lstData);
                AddChuKy(data);


                if (lstChungTuBaoCao.Any())
                {
                    data.Add("Count", 10000);
                    templateFileName = Path.Combine(ExportPrefix.PATH_TL_DT, ExportFileName.RPT_NS_DUTOAN_CONGKHAI_02CKNS_EXCEL);
                }
                else
                {
                    templateFileName = Path.Combine(ExportPrefix.PATH_TL_DT, ExportFileName.RPT_NS_DUTOAN_CONGKHAI_02CKNS_To1_EMPTY);
                }

                fileNamePrefix = Path.GetFileNameWithoutExtension(templateFileName) + "_Tờ 1";
                fileNameWithoutExtension = StringUtils.CreateExportFileName(fileNamePrefix);
                var xlsFile = _exportService.Export<NsDtChungTuCongKhaiQuery, GenericReportHeader>(templateFileName, data);
                results.Add(new ExportResult("Biểu số 02/CKNS", fileNameWithoutExtension, null, xlsFile));
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
        public string GetPath(string input)
        {
            if (PaperPrintTypeSelected.ValueItem == "2")
                input = input + "_Ngang";
            return Path.Combine(ExportPrefix.PATH_TL_DT, input + FileExtensionFormats.Xlsx);
        }

        public int GetDonViTinh()
        {
            if (CatUnitTypeSelected == null || string.IsNullOrEmpty(CatUnitTypeSelected.ValueItem))
                return 1;
            return int.Parse(CatUnitTypeSelected.ValueItem);
        }

        private void CalculateData(List<NsDtChungTuCongKhaiQuery> lstData)
        {
            foreach (var item in lstData.Where(x => !x.Id_DanhMucCha.HasValue))
            {
                CalculateParentNew(item, lstData);
            }
        }

        public static IEnumerable<List<T>> SplitList<T>(List<T> bigList, int nSize = 6)
        {
            for (int i = 0; i < bigList.Count; i += nSize)
            {
                yield return bigList.GetRange(i, Math.Min(nSize, bigList.Count - i));
            }
        }

        private bool CheckIsPhongBan(string maDonVi)
        {
            var listDonVi = _nSDonViService.FindAll(x => x.NamLamViec == _sessionService.Current.YearOfWork && x.ITrangThai == StatusType.ACTIVE);
            var donViIsPhongBan = listDonVi.FirstOrDefault(x => x.IIDMaDonVi == maDonVi && x.IsPhongBan.HasValue && x.IsPhongBan.Value);
            return donViIsPhongBan != null;
        }

        public void AddListParentDanhMuc(Guid? item, Dictionary<Guid, Guid> dictDM, List<NsDanhMucCongKhai> lstDanhMucCongKhai)
        {
            if (item == null) return;
            var parent = lstDanhMucCongKhai.FirstOrDefault(x => x.Id == (Guid)item);
            if (parent != null)
            {
                if (!dictDM.ContainsKey(parent.Id))
                {
                    dictDM.Add(parent.Id, parent.iID_DMCongKhai_Cha ?? Guid.Empty);
                }
                AddListParentDanhMuc(parent.iID_DMCongKhai_Cha, dictDM, lstDanhMucCongKhai);
            }
        }

        public void ExportBaoCao_BieuSo01CKNS(ExportType exportType)
        {
            var DNgayQuyetDinhTuDot = _soQuyetDinhTuDotSelected.ValueItem != null ? Items.FirstOrDefault(x => x.IIdDotNhan.ToString().Equals(_soQuyetDinhTuDotSelected.ValueItem)).DNgayQuyetDinh : null;
            var DNgayQuyetDinhDenDot = _soQuyetDinhDenDotSelected.ValueItem != null ? Items.FirstOrDefault(x => x.IIdDotNhan.ToString().Equals(_soQuyetDinhDenDotSelected.ValueItem)).DNgayQuyetDinh : null;
            if (DNgayQuyetDinhTuDot == null || DNgayQuyetDinhDenDot == null || DNgayQuyetDinhTuDot > DNgayQuyetDinhDenDot)
            {
                MessageBoxHelper.Info(string.Format(Resources.VoucherBudgetAllocationHasNoData, _soQuyetDinhTuDotSelected.DisplayItem, _soQuyetDinhDenDotSelected.DisplayItem));
                return;
            }
            BackgroundWorkerHelper.Run((s, e) =>
            {
                IsLoading = true;
                List<ExportResult> exportResults = new List<ExportResult>();

                _dmChuKy = _dmChuKyService.FindByCondition(x => x.IdType.Equals(_typeChuKy) && x.ITrangThai == StatusType.ACTIVE).FirstOrDefault();

                var dataDanhMucSelected = ListLns.Where(x => x.IsChecked).Select(n => n.Id);

                Dictionary<Guid, NsDanhMucCongKhai> dicDanhMuc = _dmCongKhaiTaiChinhService.FindByCondition(n => n.iNamLamViec == _sessionService.Current.YearOfWork).ToDictionary(n => n.Id, n => n);

                int donViTinh = GetDonViTinh();
                var listChungTuIds = Items.Where(x => x.DNgayQuyetDinh.Value.Date >= DNgayQuyetDinhTuDot.Value.Date && x.DNgayQuyetDinh.Value.Date <= DNgayQuyetDinhDenDot.Value.Date).Select(x => x.IIdDotNhan).ToList();
                var searchCondition = new EstimationVoucherDetailCriteria
                {
                    LNS = string.Join(StringUtils.COMMA, dataDanhMucSelected),
                    YearOfWork = _sessionService.Current.YearOfWork,
                    YearOfBudget = _sessionService.Current.YearOfBudget,
                    BudgetSource = _sessionService.Current.Budget,
                    IThangQuy = int.Parse(QuarterMonthSelected.ValueItem),
                    VoucherIds = string.Join(StringUtils.COMMA, listChungTuIds.Where(x => !string.IsNullOrEmpty(x)).ToList()),
                    DonViTinh = donViTinh
                };

                IEnumerable<NsDtChungTuChiTietCongKhaiQuery> listChungTuChiTiet = _dtChungTuChiTietService.FindDtChungTuChiTietCongKhaiClone(searchCondition);

                int pageSize = 6;
                var sttHeader = 1;
                var listDonVi = listChungTuChiTiet.Where(x => !string.IsNullOrEmpty(x.MaDonVi)).Select(x => x.MaDonVi).OrderBy(x => x).Distinct().ToList();
                //listChungTuChiTiet.Select(n =>
                //{
                //    n.SoPhanBo /= donViTinh;
                //    n.SoChuaPhanBo /= donViTinh;
                //    n.DuToanDuocGiao /= donViTinh;
                //    return n;
                //}).ToList();

                List<NsDanhMucCongKhai> lstDanhMucCongKhai = _dmCongKhaiTaiChinhService.FindByCondition(n => n.iNamLamViec == _sessionService.Current.YearOfWork).ToList();
                var dictDMCK = new Dictionary<Guid, Guid>();
                foreach (var item in listChungTuChiTiet)
                {
                    if (!dictDMCK.ContainsKey(item.ID_DMCongKhai))
                    {
                        dictDMCK.Add(item.ID_DMCongKhai, item.iID_DMCongKhai_Cha);
                    }
                    AddListParentDanhMuc(item.iID_DMCongKhai_Cha, dictDMCK, lstDanhMucCongKhai);
                }

                if (exportType == ExportType.EXCEL)
                {
                    pageSize = listDonVi.Where(n => !CheckIsPhongBan(n)).ToList().Count();

                }
                var listDonViSplits = SplitList(listDonVi.Where(n => !CheckIsPhongBan(n)).ToList(), pageSize).ToList();

                for (int i = 0; i < listDonViSplits.Count; i++)
                {
                    var headers = new List<HeaderReportDanhMucCongKhaiTaiChinh>();
                    var listData = new List<ReportDanhMucCongKhaiTaiChinhThuChi>();

                    if (listDonViSplits[i].Count < pageSize)
                    {
                        var countEmpty = pageSize - listDonViSplits[i].Count;
                        for (int j = 0; j < countEmpty; j++)
                        {
                            string emptyCb = string.Empty;
                            listDonViSplits[i].Add(emptyCb);
                        }
                    }

                    foreach (var dmck in dictDMCK.Select(n => n.Key))
                    {
                        var listChungTuChiTietDM = listChungTuChiTiet.Where(x => x.ID_DMCongKhai == dmck);

                        var giatri = new ReportDanhMucCongKhaiTaiChinhThuChi();
                        giatri.LstGiaTri = new List<NsDtChungTuChiTietCongKhaiQuery>();

                        giatri.DuToanDuocGiao = listChungTuChiTietDM.FirstOrDefault()?.DuToanDuocGiao ?? 0;
                        giatri.SoPhanBo = listChungTuChiTietDM.Sum(y => y.SoPhanBo);
                        giatri.SoChuaPhanBo = giatri.DuToanDuocGiao - giatri.SoPhanBo;
                        giatri.BanThan = listChungTuChiTietDM.Where(x => CheckIsPhongBan(x.MaDonVi)).Sum(y => y.SoPhanBo);

                        var itemChiTiet = listChungTuChiTiet.FirstOrDefault(x => x.ID_DMCongKhai != null && x.ID_DMCongKhai.Equals(dmck));
                        if (dicDanhMuc.ContainsKey(dmck))
                        {
                            giatri.STT = dicDanhMuc[dmck].STT;
                            giatri.sMa = dicDanhMuc[dmck].sMa;
                            giatri.NoiDung = dicDanhMuc[dmck].sMoTa;
                            giatri.bHangCha = dicDanhMuc[dmck].bHangCha;
                            giatri.Id = dicDanhMuc[dmck].Id;
                            giatri.ParentId = dicDanhMuc[dmck].iID_DMCongKhai_Cha;
                        }


                        foreach (var maDonVi in listDonViSplits[i])
                        {
                            var danhMuc = listChungTuChiTiet.FirstOrDefault(x => x.ID_DMCongKhai == dmck && x.MaDonVi == maDonVi);
                            giatri.LstGiaTri.Add(new NsDtChungTuChiTietCongKhaiQuery()
                            {
                                SoPhanBo = danhMuc?.SoPhanBo ?? 0
                            });
                        }
                        listData.Add(giatri);
                    }

                    // in tiêu đề theo từng trang
                    foreach (var maDonVi in listDonViSplits[i])
                    {
                        var header = new HeaderReportDanhMucCongKhaiTaiChinh();
                        header.ChiSoDanhMuc = maDonVi;
                        var tenDonVi = listChungTuChiTiet.FirstOrDefault(x => x.MaDonVi == maDonVi)?.TenDonVi ?? string.Empty;
                        header.TenDanhMuc = tenDonVi;
                        header.STT = sttHeader++;
                        headers.Add(header);
                    }

                    FormatNumber formatNumber = new FormatNumber(donViTinh, exportType);

                    listData = listData.OrderBy(n => n.sMa).ToList();

                    for (int index = listData.Count() - 1; index > 0; --index)
                    {
                        Guid? iIdparent = listData[index].ParentId;
                        if (!iIdparent.HasValue) continue;
                        var objParent = listData.FirstOrDefault(n => n.Id == iIdparent.Value);
                        if (objParent == null) continue;
                        objParent.BanThan += listData[index].BanThan;
                        objParent.SoChuaPhanBo += listData[index].SoChuaPhanBo;
                        objParent.SoPhanBo += listData[index].SoPhanBo;
                        objParent.DuToanDuocGiao += listData[index].DuToanDuocGiao;

                        var j = 0;
                        foreach (var item in listData[index].LstGiaTri)
                        {
                            if (objParent.LstGiaTri == null)
                                objParent.LstGiaTri.Add(new NsDtChungTuChiTietCongKhaiQuery());
                            objParent.LstGiaTri[j].SoPhanBo += item.SoPhanBo;
                            ++j;
                        }
                    }

                    var data = new Dictionary<string, object>
                    {
                        { "FormatNumber", formatNumber },
                        { "ListData", listData },
                        { "Headers", headers },
                        { "TieuDe1", TxtTitleFirst },
                        { "TieuDe2", TxtTitleSecond },
                        { "TieuDe3", TxtTitleThird },
                        { "Cap1", _cap1 },
                        { "Cap2", _cap2 },
                        { "Ngay", _ngay },
                        { "h1", string.Empty },
                        { "h2", string.Empty },
                        { "Header1", "Đơn vị tính: " + CatUnitTypeSelected.DisplayItem },
                        { "Count", 100000 },
                    };

                    AddChuKy(data);


                    if (i == 0)
                    {
                        string templateFileName = Path.Combine(ExportPrefix.PATH_TL_DT, ExportFileName.RPT_NS_DUTOAN_CONGKHAI_THUCHI_TO1);
                        string fileNamePrefix = ExportFileName.RPT_NS_DUTOAN_CONGKHAI_THUCHI_TO1.Split(".").First();

                        if (exportType == ExportType.EXCEL)
                        {
                            //Add List header1
                            List<HeaderReportDanhMucCongKhaiTaiChinh> lstHeader1 = new List<HeaderReportDanhMucCongKhaiTaiChinh>();
                            lstHeader1.Add(new HeaderReportDanhMucCongKhaiTaiChinh { STT = 1, TenDanhMuc = " Chi tiết trong các đơn vị" });
                            foreach (var lstDv in listDonVi)
                            {
                                HeaderReportDanhMucCongKhaiTaiChinh header1 = new HeaderReportDanhMucCongKhaiTaiChinh();
                                lstHeader1.Add(header1);
                            }
                            lstHeader1 = lstHeader1.Take(lstHeader1.Count() - 1).ToList();


                            int columnStart = 10;
                            var mergeRange = "";
                            var columnStartName = GetExcelColumnName(columnStart);
                            var columnEndName = GetExcelColumnName(listDonVi.Count() + columnStart);
                            mergeRange = columnStartName + "5" + ":" + columnEndName + "5";

                            data.Add("LstHeader1", lstHeader1);
                            data.Add("MergeRange", mergeRange);
                           // data.Add("Count", 10000);
                            templateFileName = Path.Combine(ExportPrefix.PATH_TL_DT, ExportFileName.RPT_NS_DUTOAN_CONGKHAI_THUCHI_EXCEL);
                            fileNamePrefix = ExportFileName.RPT_NS_DUTOAN_CONGKHAI_THUCHI_EXCEL.Split(".").First();
                        }

                        string fileNameWithoutExtension = StringUtils.CreateExportFileName(fileNamePrefix);
                        var xlsFile = _exportService.Export<ReportDanhMucCongKhaiTaiChinhThuChi, HeaderReportDanhMucCongKhaiTaiChinh>(templateFileName, data);
                        exportResults.Add(new ExportResult(fileNameWithoutExtension, fileNameWithoutExtension, null, xlsFile));
                    }
                    else
                    {
                        string templateFileName = Path.Combine(ExportPrefix.PATH_TL_DT, ExportFileName.RPT_NS_DUTOAN_CONGKHAI_THUCHI_TO2);
                        string fileNamePrefix = ExportFileName.RPT_NS_DUTOAN_CONGKHAI_THUCHI_TO2.Split(".").First();
                        string fileNameWithoutExtension = StringUtils.CreateExportFileName(fileNamePrefix);
                        var xlsFile = _exportService.Export<ReportDanhMucCongKhaiTaiChinhThuChi, HeaderReportDanhMucCongKhaiTaiChinh>(templateFileName, data);
                        exportResults.Add(new ExportResult(fileNameWithoutExtension, fileNameWithoutExtension, null, xlsFile));
                    }
                }
                if (listDonVi.Count == 0)
                {
                    var listData = new List<ReportDanhMucCongKhaiTaiChinhThuChi>();

                    foreach (var dmck in dictDMCK.Select(n => n.Key))
                    {
                        var giatri = new ReportDanhMucCongKhaiTaiChinhThuChi();
                        if (dicDanhMuc.ContainsKey(dmck))
                        {
                            giatri.STT = dicDanhMuc[dmck].STT;
                            giatri.sMa = dicDanhMuc[dmck].sMa;
                            giatri.NoiDung = dicDanhMuc[dmck].sMoTa;
                            giatri.bHangCha = dicDanhMuc[dmck].bHangCha;
                            giatri.Id = dicDanhMuc[dmck].Id;
                            giatri.ParentId = dicDanhMuc[dmck].iID_DMCongKhai_Cha;
                        }
                        listData.Add(giatri);
                    }

                    listData = listData.OrderBy(n => n.sMa).ToList();

                    var data = new Dictionary<string, object>()
                    {
                        { "ListData", listData },
                        { "TieuDe1", TxtTitleFirst },
                        { "TieuDe2", TxtTitleSecond },
                        { "TieuDe3", TxtTitleThird },
                        { "Cap1", _cap1 },
                        { "Cap2", _cap2 },
                        { "Ngay", _ngay },
                        { "h1", string.Empty },
                        { "h2", string.Empty },
                        { "Header1", "Đơn vị tính: " + CatUnitTypeSelected.DisplayItem },
                        { "Count", 100000 },

                    };
                    AddChuKy(data);
                    string templateFileName = Path.Combine(ExportPrefix.PATH_TL_DT, ExportFileName.RPT_NS_DUTOAN_CONGKHAI_THUCHI_TO_EMPTY);
                    string fileNamePrefix = ExportFileName.RPT_NS_DUTOAN_CONGKHAI_THUCHI_TO_EMPTY.Split(StringUtils.DOT).First();
                    string fileNameWithoutExtension = StringUtils.CreateExportFileName(fileNamePrefix);
                    var xlsFile = _exportService.Export<ReportDanhMucCongKhaiTaiChinhThuChi, HeaderReportDanhMucCongKhaiTaiChinh>(templateFileName, data);
                    exportResults.Add(new ExportResult(fileNameWithoutExtension, fileNameWithoutExtension, null, xlsFile));
                }
                e.Result = exportResults;
            }, (s, e) =>
            {
                if (e.Error == null)
                {
                    var result = (List<ExportResult>)e.Result;
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
        private double CalculateParentNew(NsDtChungTuCongKhaiQuery currentItem, List<NsDtChungTuCongKhaiQuery> lstData)
        {
            double fTong = currentItem.fTuChi ?? 0;
            if (lstData.Any(n => n.Id_DanhMucCha == currentItem.Id_DanhMuc))
            {
                fTong = 0;
                foreach (var item in lstData.Where(n => n.Id_DanhMucCha == currentItem.Id_DanhMuc))
                {
                    fTong += CalculateParentNew(item, lstData);
                }
            }
            currentItem.fTuChi = fTong;
            return fTong;
        }

        private void CalculateParent(NsDtChungTuCongKhaiQuery currentItem, NsDtChungTuCongKhaiQuery seftItem, List<NsDtChungTuCongKhaiQuery> lstData)
        {
            var parrentItem = lstData.Where(x => x.Id_DanhMuc == currentItem.Id_DanhMucCha).FirstOrDefault();
            if (parrentItem == null) return;
            if (parrentItem.fTuChi == null)
            {
                parrentItem.fTuChi = 0;
            }
            parrentItem.fTuChi += seftItem.fTuChi;
            lstData.Where(x => x.Id_DanhMuc == parrentItem.Id_DanhMuc).Select(x => { x.fTuChi = parrentItem.fTuChi; return x; }).ToList();
            CalculateParent(parrentItem, currentItem, lstData);
        }


        private void ExportMau_01QdCkns_02CknsBc(ExportType exportType)
        {
            try
            {
                BackgroundWorkerHelper.Run((s, e) =>
                {
                    IsLoading = true;

                    List<ExportResult> results = new List<ExportResult>();
                    string templateFileName = Path.Combine(ExportPrefix.PATH_TL_DT, ExportFileName.RPT_NS_DUTOAN_QD_CONGKHAINGANSACH);

                    List<DonVi> lstData = new List<DonVi>();
                    var lstDonVi = _nSDonViService.FindAll(n => n.NamLamViec == _sessionService.Current.YearOfWork && n.Loai == LoaiDonVi.NOI_BO);
                    if (lstDonVi != null) lstData = lstDonVi.ToList();

                    Dictionary<string, object> map = new Dictionary<string, object>();
                    map.Add("H1", string.Empty);
                    map.Add("Cap1", _cap1);
                    map.Add("Cap2", _cap2);
                    map.Add("Ngay", _ngay);
                    map.Add("DiaDiem", _diaDiem);
                    map.Add("Nam", _sessionService.Current.YearOfWork);
                    map.Add("Donvi_Count", lstData.Count);
                    map.Add("DonVi_DT", lstData);
                    map.Add("DonVi_NN", new List<DonVi>());
                    map.Add("ChuKy_93", string.Empty);
                    map.Add("ChuKy_94", string.Empty);
                    _dmChuKyService.GetConfigSign(_typeChuKy, ref map);

                    ExcelFile xlsFile = _exportService.Export<DonVi>(templateFileName, map);
                    string fileNamePrefix = string.Format("rptNS_DuToan_QdCongKhaiNganSach_{0}", DateTime.Now.ToString("ddMMyyyyHHmmssss"));
                    string fileNameWithoutExtension = StringUtils.CreateExportFileName(fileNamePrefix);
                    results.Add(new ExportResult("Biểu số 02/CKNS-BC", fileNameWithoutExtension, null, xlsFile));

                    templateFileName = Path.Combine(ExportPrefix.PATH_TL_DT, ExportFileName.RPT_NS_DUTOAN_QD_CONGKHAINGANSACH_To2);
                    xlsFile = _exportService.Export<DonVi>(templateFileName, map);
                    fileNamePrefix = string.Format("rptNS_DuToan_QdCongKhaiNganSach_To2_{0}", DateTime.Now.ToString("ddMMyyyyHHmmssss"));
                    fileNameWithoutExtension = StringUtils.CreateExportFileName(fileNamePrefix);
                    results.Add(new ExportResult("Biểu số 01/QĐ-CKNS", fileNameWithoutExtension, null, xlsFile));

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
    }
}