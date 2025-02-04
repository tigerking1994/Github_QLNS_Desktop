using AutoMapper;
using FlexCel.Core;
using log4net;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Windows;
using System.Text;
using System.Threading.Tasks;
using VTS.QLNS.CTC.App.Command;
using VTS.QLNS.CTC.App.Helper;
using VTS.QLNS.CTC.App.Model;
using VTS.QLNS.CTC.App.Model.Control;
using VTS.QLNS.CTC.App.Service;
using VTS.QLNS.CTC.App.Service.Impl;
using VTS.QLNS.CTC.App.Service.UserFunction;
using VTS.QLNS.CTC.App.View.Forex.ForexSettlement.QuyetToanNienDo.PrintDialog;
using VTS.QLNS.CTC.App.ViewModel.Category;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Service;
using VTS.QLNS.CTC.Utility;
using VTS.QLNS.CTC.Utility.Enum;

namespace VTS.QLNS.CTC.App.ViewModel.Forex.ForexSettlement.QuyetToanNienDo.PrintDialog
{
    public class QuyetToanNienDoPrintDialogViewModel : DialogViewModelBase<NhQtQuyetToanNienDoModel>
    {
        private readonly ILog _logger;
        private readonly IMapper _mapper;
        private readonly ISessionService _sessionService;
        private readonly IDmChuKyService _dmChuKyService;
        private readonly IDanhMucService _danhMucService;
        private readonly INhQtQuyetToanNienDoService _service;
        private readonly INhQtQuyetToanNienDoChiTietService _serviceChiTiet;
        private readonly IExportService _exportService;
        private SessionInfo _sessionInfo;
        private readonly string _typeChuky = TypeChuKy.RPT_NH_DENGHI_QUYETTOAN_NIENDO;
        private readonly string TITLE_FIRST_DEFAULT_VALUE = "BÁO CÁO QUYẾT TOÁN KINH PHÍ NGUỒN QUỸ DỰ TRỮ NGOẠI HỐI NĂM";
        private readonly string TITLE_SECOND_DEFAULT_VALUE = "";
        private DmChuKy _dmChuKy;
        public override string Title => "Báo cáo Đề nghị quyết toán niên độ";
        public override string Description => "Báo cáo Đề nghị thanh toán niên độ";
        public override Type ContentType => typeof(QuyetToanNienDoPrintDialog);

        private string _txtTitleFirst;
        public string TxtTitleFirst
        {
            get => _txtTitleFirst;
            set
            {
                SetProperty(ref _txtTitleFirst, value);
            }
        }

        private string _txtTitleSecond;
        public string TxtTitleSecond
        {
            get => _txtTitleSecond;
            set => SetProperty(ref _txtTitleSecond, value);
        }

        //private ObservableCollection<ComboboxItem> _itemsLoaiBaoCao = new ObservableCollection<ComboboxItem>();
        //public ObservableCollection<ComboboxItem> ItemsLoaiBaoCao
        //{
        //    get => _itemsLoaiBaoCao;
        //    set => SetProperty(ref _itemsLoaiBaoCao, value);
        //}

        //private ComboboxItem _selectedLoaiBaoCao;
        //public ComboboxItem SelectedLoaiBaoCao
        //{
        //    get => _selectedLoaiBaoCao;
        //    set => SetProperty(ref _selectedLoaiBaoCao, value);
        //}

        private ObservableCollection<ComboboxItem> _itemsDonViTinhUSD = new ObservableCollection<ComboboxItem>();
        public ObservableCollection<ComboboxItem> ItemsDonViTinhUSD
        {
            get => _itemsDonViTinhUSD;
            set => SetProperty(ref _itemsDonViTinhUSD, value);
        }

        private ComboboxItem _selectedDonViTinhUSD;
        public ComboboxItem SelectedDonViTinhUSD
        {
            get => _selectedDonViTinhUSD;
            set => SetProperty(ref _selectedDonViTinhUSD, value);
        }

        private ObservableCollection<ComboboxItem> _itemsDonViTinhVND = new ObservableCollection<ComboboxItem>();
        public ObservableCollection<ComboboxItem> ItemsDonViTinhVND
        {
            get => _itemsDonViTinhVND;
            set => SetProperty(ref _itemsDonViTinhVND, value);
        }

        private ComboboxItem _selectedDonViTinhVND;
        public ComboboxItem SelectedDonViTinhVND
        {
            get => _selectedDonViTinhVND;
            set => SetProperty(ref _selectedDonViTinhVND, value);
        }

        private DmChuKyDialogViewModel DmChuKyDialogViewModel { get; }

        public RelayCommand PrintCommand { get; }
        public RelayCommand ConfigSignCommand { get; }

        public QuyetToanNienDoPrintDialogViewModel(
            ILog logger,
            IMapper mapper,
            ISessionService sessionService,
            INhQtQuyetToanNienDoChiTietService serviceChiTiet,
            IDmChuKyService dmChuKyService,
            IDanhMucService danhMucService,
            IExportService exportService,
            INhQtQuyetToanNienDoService service,
            DmChuKyDialogViewModel dmChuKyDialogViewModel)
        {
            _logger = logger;
            _mapper = mapper;
            _sessionService = sessionService;
            _danhMucService = danhMucService;
            _dmChuKyService = dmChuKyService;
            _exportService = exportService;
            _service = service;
            _serviceChiTiet = serviceChiTiet;

            DmChuKyDialogViewModel = dmChuKyDialogViewModel;

            PrintCommand = new RelayCommand(obj => OnPrint(obj));
            ConfigSignCommand = new RelayCommand(obj => OnConfigSign());
        }

        public override void Init()
        {
            LoadTitleFirst();
            LoadDonViTinh();
            //LoadLoaiBaoCao();
            LoadData();
        }
        private void LoadDonViTinh()
        {
            _itemsDonViTinhUSD = new ObservableCollection<ComboboxItem>();
            _itemsDonViTinhVND = new ObservableCollection<ComboboxItem>();
            var predicate = PredicateBuilder.True<DanhMuc>();
            predicate = predicate.And(x => x.SType.Contains(TypeDanhMuc.DM_DONVITINH));
            predicate = predicate.And(x => x.ITrangThai == StatusType.ACTIVE);
            predicate = predicate.And(x => x.INamLamViec == _sessionService.Current.YearOfWork);

            List<DanhMuc> dataVND = _danhMucService.FindByCondition(predicate).OrderBy(x => x.SGiaTri).ToList();
            
            _itemsDonViTinhVND = _mapper.Map<ObservableCollection<ComboboxItem>>(dataVND);
            if (dataVND.Count == 0)
            {
                _itemsDonViTinhVND.Insert(0, new ComboboxItem { ValueItem = DonViTinh.DONG_VALUE, DisplayItem = DonViTinh.DONG });
            }

            _itemsDonViTinhUSD.Insert(0, new ComboboxItem { ValueItem = "1", DisplayItem = "USD" });
            _itemsDonViTinhUSD.Insert(1, new ComboboxItem { ValueItem = "1000", DisplayItem = "Nghìn USD" });
            _itemsDonViTinhUSD.Insert(2, new ComboboxItem { ValueItem = "1000000", DisplayItem = "Triệu USD" });
            _itemsDonViTinhUSD.Insert(2, new ComboboxItem { ValueItem = "1000000000", DisplayItem = "Tỉ USD" });

            SelectedDonViTinhUSD = _itemsDonViTinhUSD.FirstOrDefault();
            SelectedDonViTinhVND = _itemsDonViTinhVND.FirstOrDefault();
            OnPropertyChanged(nameof(ItemsDonViTinhUSD));
            OnPropertyChanged(nameof(ItemsDonViTinhVND));
        }

        private void LoadTitleFirst()
        {
            TxtTitleFirst = TITLE_FIRST_DEFAULT_VALUE;
            TxtTitleSecond = TITLE_SECOND_DEFAULT_VALUE;
        }

        //private void LoadLoaiBaoCao()
        //{
        //    _itemsLoaiBaoCao = new ObservableCollection<ComboboxItem>();
        //    _itemsLoaiBaoCao.Add(new ComboboxItem() { ValueItem = "QUY", DisplayItem = "Quý" });
        //    _itemsLoaiBaoCao.Add(new ComboboxItem() { ValueItem = "NAM", DisplayItem = "Năm" });
        //    _itemsLoaiBaoCao.Add(new ComboboxItem() { ValueItem = "GIAI_DOAN", DisplayItem = "Giai đoạn" });

        //    SelectedLoaiBaoCao = _itemsLoaiBaoCao.FirstOrDefault(x => x.ValueItem.Equals("NAM"));
        //    OnPropertyChanged(nameof(ItemsLoaiBaoCao));
        //}

        public override void LoadData(params object[] args)
        {
        }

        private void OnPrint(object obj)
        {
            try
            {
                BackgroundWorkerHelper.Run((s, e) =>
                {
                    IsLoading = true;

                    List<ExportResult> results = new List<ExportResult>();
                    FormatNumber formatNumber = new FormatNumber(1, ExportType.PDF);
                    _dmChuKy = _dmChuKyService.FindByCondition(x => x.IdType.Equals(TypeChuKy.RPT_NH_DENGHI_QUYETTOAN_NIENDO) && x.ITrangThai == StatusType.ACTIVE).FirstOrDefault();
                    Dictionary<string, object> report = new Dictionary<string, object>();
                    report.Add("DonViCapTren", _sessionService.Current.TenDonViTrucThuocReportHeader);
                    report.Add("DonVi", _sessionService.Current.TenDonViReportHeader);
                    report.Add("C", "010");
                    report.Add("L", "010");
                    report.Add("K", "010");
                    report.Add("Quy", "2");
                    report.Add("Nam", Model.INamKeHoach);
                    report.Add("FormatNumber", formatNumber);
                    report.Add("DonViUSD", SelectedDonViTinhUSD.DisplayItem);
                    report.Add("DonViVND", SelectedDonViTinhVND.DisplayItem);
                    report.Add("TenBaoCao", _dmChuKy != null ? (_dmChuKy.TieuDe1MoTa != null ? _dmChuKy.TieuDe1MoTa.ToUpper() : "") : string.Empty);
                    report.Add("ThuaLenh1", _dmChuKy != null ? _dmChuKy.ThuaLenh1MoTa : string.Empty);
                    report.Add("ThuaLenh2", _dmChuKy != null ? _dmChuKy.ThuaLenh2MoTa : string.Empty);
                    report.Add("ThuaLenh3", _dmChuKy != null ? _dmChuKy.ThuaLenh3MoTa : string.Empty);
                    report.Add("ChucDanh1", _dmChuKy != null ? _dmChuKy.ChucDanh1MoTa : string.Empty);
                    report.Add("ChucDanh2", _dmChuKy != null ? _dmChuKy.ChucDanh2MoTa : string.Empty);
                    report.Add("ChucDanh3", _dmChuKy != null ? _dmChuKy.ChucDanh3MoTa : string.Empty);
                    report.Add("Ten1", _dmChuKy != null ? _dmChuKy.Ten1MoTa : string.Empty);
                    report.Add("Ten2", _dmChuKy != null ? _dmChuKy.Ten2MoTa : string.Empty);
                    report.Add("Ten3", _dmChuKy != null ? _dmChuKy.Ten3MoTa : string.Empty);
                    string templateFileName;
                    string fileNamePrefix;
                    string fileNameWithoutExtension;
                    ExcelFile xlsFile;
                    //switch (SelectedLoaiBaoCao.ValueItem)
                    //{
                    //    case "QUY":
                    //        var dataQuy = _service.ReportQuy(Model.Id);
                    //        report.Add("Items", dataQuy);

                    //        templateFileName = Path.Combine(ExportPrefix.PATH_NH_QT, ExportFileName.RPT_NH_QUYETTOAN_QUYETTOAN_NIENDO_QUY);
                    //        fileNamePrefix = string.Format("rptNgoaiHoiQuyetToanNienDo_Quy_{0}", Model.SSoDeNghi);
                    //        fileNameWithoutExtension = StringUtils.CreateExportFileName(fileNamePrefix);
                    //        xlsFile = _exportService.Export<ReportNhQtQuyetToanNienDoQuyQuery>(templateFileName, report);
                    //        results.Add(new ExportResult(fileNameWithoutExtension, fileNameWithoutExtension, null, xlsFile));
                    //        break;
                    //    case "NAM":
                    //        var dataNam = _serviceChiTiet.getListQTNDDetailChiTiet(Model.Id, Model.IIdDonViId, Model.INamKeHoach);
                    //        report.Add("Items", dataNam);

                    //        templateFileName = Path.Combine(ExportPrefix.PATH_NH_QT, ExportFileName.RPT_NH_QUYETTOAN_QUYETTOAN_NIENDO_NAM);
                    //        fileNamePrefix = string.Format("rptNgoaiHoiQuyetToanNienDo_Nam_{0}", Model.SSoDeNghi);
                    //        fileNameWithoutExtension = StringUtils.CreateExportFileName(fileNamePrefix);
                    //        xlsFile = _exportService.Export<NhQTQuyetToanNienDoChiTietQuery>(templateFileName, report);
                    //        results.Add(new ExportResult(fileNameWithoutExtension, fileNameWithoutExtension, null, xlsFile));
                    //        break;
                    //    case "GIAI_DOAN":
                    //        templateFileName = Path.Combine(ExportPrefix.PATH_NH_QT, ExportFileName.RPT_NH_QUYETTOAN_QUYETTOAN_NIENDO_GIAIDOAN);
                    //        break;
                    //    default:
                    //        templateFileName = Path.Combine(ExportPrefix.PATH_NH_QT, ExportFileName.RPT_NH_QUYETTOAN_QUYETTOAN_NIENDO_NAM);
                    //        break;
                    //}
                    var dataNam = _serviceChiTiet.getListQTNDDetailChiTiet(Model.Id, Model.IIdDonViId, Model.INamKeHoach, int.Parse(SelectedDonViTinhUSD.ValueItem), int.Parse(SelectedDonViTinhUSD.ValueItem));
                    report.Add("Items", dataNam);

                    templateFileName = Path.Combine(ExportPrefix.PATH_NH_QT, ExportFileName.RPT_NH_QUYETTOAN_QUYETTOAN_NIENDO_NAM);
                    fileNamePrefix = string.Format("rptNgoaiHoiQuyetToanNienDo_Nam_{0}", Model.SSoDeNghi);
                    fileNameWithoutExtension = StringUtils.CreateExportFileName(fileNamePrefix);
                    xlsFile = _exportService.Export<NhQTQuyetToanNienDoChiTietQuery>(templateFileName, report);
                    results.Add(new ExportResult(fileNameWithoutExtension, fileNameWithoutExtension, null, xlsFile));

                    e.Result = results;
                }, (s, e) =>
                {
                    if (e.Error == null)
                    {
                        var result = (List<ExportResult>)e.Result;
                        _exportService.Open(result, ExportType.PDF);
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

        private void OnConfigSign()
        {
            DmChuKyModel chuKyModel = new DmChuKyModel();
            DmChuKy _dmChuKy = _dmChuKyService.FindByCondition(x => x.IdType.Equals(_typeChuky) && x.ITrangThai == StatusType.ACTIVE).FirstOrDefault();
            if (_dmChuKy == null)
                chuKyModel.IdType = _typeChuky;
            else
                chuKyModel = _mapper.Map<DmChuKyModel>(_dmChuKy);
            DmChuKyDialogViewModel.DmChuKyModel = chuKyModel;
            DmChuKyDialogViewModel.SavedAction = obj =>
            {
                LoadTitleFirst();
                DmChuKyModel chuKy = (DmChuKyModel)obj;
                if (chuKy != null)
                {
                    if (!string.IsNullOrEmpty(chuKy.TieuDe1MoTa))
                        TxtTitleFirst = chuKy.TieuDe1MoTa;
                    if (!string.IsNullOrEmpty(chuKy.TieuDe2MoTa))
                        TxtTitleSecond = chuKy.TieuDe2MoTa;
                }
            };
            DmChuKyDialogViewModel.Init();
            DmChuKyDialogViewModel.ShowDialog();
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
