using AutoMapper;
using log4net;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VTS.QLNS.CTC.App.Command;
using VTS.QLNS.CTC.App.Helper;
using VTS.QLNS.CTC.App.Model;
using VTS.QLNS.CTC.App.Service.Impl;
using VTS.QLNS.CTC.App.Service;
using VTS.QLNS.CTC.Core.Service;
using VTS.QLNS.CTC.Utility.Enum;
using VTS.QLNS.CTC.Utility;
using VTS.QLNS.CTC.App.View.NewSalary.NewSalaryManagement.NewInsuranceSalaryMonthTable.NewExport;
using VTS.QLNS.CTC.App.Model.Control;
using System.IO;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Service.Impl;

namespace VTS.QLNS.CTC.App.ViewModel.NewSalary.NewSalaryManagement.NewInsuranceSalaryMonthTable.NewExport
{
    public class NewInsuranceSalaryExportViewModel : DialogViewModelBase<TlBangLuongThangBHXHNq104Model>
    {
        private readonly ILog _logger;
        private readonly IMapper _mapper;
        private SessionInfo _sessionInfo;
        private readonly ISessionService _sessionService;
        private INsDonViService _donViService;
        private IExportService _exportService;
        private readonly ITlBangLuongThangBHXHNq104Service _luongThangBHXHService;
        private ITlDmCheDoBHXHService _iTlDmCheDoBHXHService;
        private readonly ITlDmCanBoNq104Service _tlDmCanboService;
        private readonly ITlCanBoCheDoBHXHService _tlCanBoCheDoBHXHService;
        private readonly TlDmCapBacService _tlDmCapBacService;

        private ITlDmDonViService _tlDmDonViService;
        private IDanhMucService _danhMucService;
        public override Type ContentType => typeof(NewInsuranceSalaryExport);

        private List<ComboboxItem> _cbxQuarters;
        public List<ComboboxItem> CbxQuarters
        {
            get => _cbxQuarters;
            set => SetProperty(ref _cbxQuarters, value);
        }
        private ComboboxItem _selectedQuarter;
        public ComboboxItem SelectedQuarter
        {
            get => _selectedQuarter;
            set
            {
                SetProperty(ref _selectedQuarter, value);
                IsSaveData = true;
            }
        }
        private string _selectedMonths;
        public string SelectedMonths
        {
            get => _selectedMonths;
            set
            {
                SetProperty(ref _selectedMonths, value);
            }
        }
        private string _quarterHint;
        public string QuarterHint
        {
            get => _quarterHint;
            set => SetProperty(ref _quarterHint, value);
        }
        private bool _isSaveData;
        public bool IsSaveData
        {
            get => _isSaveData;
            set => SetProperty(ref _isSaveData, value);
        }
        public RelayCommand ExportCommand { get; }

        public NewInsuranceSalaryExportViewModel(ILog logger,
            IMapper mapper,
            ISessionService sessionService,
            INsDonViService donViService,
            ITlDmCheDoBHXHService iTlDmCheDoBHXHService,
            ITlBangLuongThangBHXHNq104Service luongThangBHXHService,
            ITlDmCanBoNq104Service iTlDmCanBoService,
            IExportService exportService,
            IDanhMucService danhMucService,
            ITlDmDonViService tlDmDonViService,
            ITlCanBoCheDoBHXHService tlCanBoCheDoBHXHService,
            TlDmCapBacService tlDmCapBacService)
        {

            _logger = logger;
            _mapper = mapper;
            _sessionService = sessionService;
            _donViService = donViService;
            _iTlDmCheDoBHXHService = iTlDmCheDoBHXHService;
            _luongThangBHXHService = luongThangBHXHService;
            _tlDmCanboService = iTlDmCanBoService;
            _exportService = exportService;
            _danhMucService = danhMucService;
            _tlDmDonViService = tlDmDonViService;
            _tlCanBoCheDoBHXHService = tlCanBoCheDoBHXHService;
            _tlDmCapBacService = tlDmCapBacService;

            ExportCommand = new RelayCommand(obj => OnExport());
        }

        public override void Init()
        {
            base.Init();
            _sessionInfo = _sessionService.Current;
            IsSaveData = false;
            LoadQuarters();
        }

        private void LoadQuarters()
        {
            _cbxQuarters = new List<ComboboxItem>();
            _cbxQuarters.Add(new ComboboxItem("Quý I", "1"));
            _cbxQuarters.Add(new ComboboxItem("Quý II", "2"));
            _cbxQuarters.Add(new ComboboxItem("Quý III", "3"));
            _cbxQuarters.Add(new ComboboxItem("Quý IV", "4"));
        }

        private void OnExport()
        {
            try
            {
                BackgroundWorkerHelper.Run((s, e) =>
                {
                    IsLoading = true;
                    LoadMonthOfQuarter();
                    List<ExportResult> results = new List<ExportResult>();
                    string templateFileName = Path.Combine(ExportPrefix.PATH_TL_LUONG_NEW, ExportFileName.RPT_TL_LUONG_THANG_BHXH_IMPORT_NEW);
                    string fileNamePrefix;
                    string fileNameWithoutExtension;
                    var lstCheDos = _iTlDmCheDoBHXHService.GetAllCheDoBHXH();
                    var lstCheDosModel = _mapper.Map<ObservableCollection<TlDmCheDoBHXHModel>>(lstCheDos).ToList();
                    ExportChiTietBangLuongBHXHNq104Model lstHeader = new ExportChiTietBangLuongBHXHNq104Model();
                    lstHeader.LstCheDo = lstCheDosModel;
                    List<ExportChiTietBangLuongBHXHNq104Model> lstHeaderItems = new List<ExportChiTietBangLuongBHXHNq104Model>();
                    lstHeaderItems.Add(lstHeader);
                    var bangLuongThang = _luongThangBHXHService.ExportBangLuongBHXH(_sessionInfo.YearOfWork, SelectedMonths).ToList();
                    var bangLuongThangModel = _mapper.Map<List<TlBangLuongThangBHXHNq104Model>>(bangLuongThang);
                    ExportChiTietBangLuongBHXHNq104Model bangLuongDoc = new ExportChiTietBangLuongBHXHNq104Model();
                    bangLuongDoc.ListGiaTriDoc = bangLuongThangModel;
                    var lstCanBo = bangLuongThangModel.Select(item => item.MaHieuCanBo).Distinct().ToList();
                    List<ExportChiTietBangLuongBHXHNq104Model> bangLuongViewItems = new List<ExportChiTietBangLuongBHXHNq104Model>();
                    int i = 1;
                    foreach (var maHieuCanBo in lstCanBo)
                    {
                        ExportChiTietBangLuongBHXHNq104Model itemRpt = new ExportChiTietBangLuongBHXHNq104Model();
                        itemRpt.iStt = i++;
                        var canBo = _tlDmCanboService.FindByMaHieuCanbo(maHieuCanBo);
                        var capBac = _tlDmCapBacService.FindByMaCapBac(canBo.MaCb);
                        foreach (var item in bangLuongThangModel)
                        {
                            itemRpt.Nam = item.Nam.ToString();
                            itemRpt.TenCbo = canBo?.TenCanBo ?? "";
                            itemRpt.MaHieuCanBo = canBo?.MaHieuCanBo ?? "";
                            itemRpt.TenDonVi = canBo?.TenDonVi ?? "";
                            itemRpt.MaCb = capBac?.MaCb ?? "";
                            itemRpt.TenCapBac = capBac?.Note ?? "";
                            itemRpt.ListGiaTriCheDo = new List<TlBangLuongThangBHXHNq104Model>();
                            foreach (var pc in lstCheDosModel)
                            {
                                TlBangLuongThangBHXHNq104Model giaTri = new TlBangLuongThangBHXHNq104Model();
                                giaTri.MaCheDo = pc.SMaCheDo;
                                var cheDo = bangLuongThangModel.FirstOrDefault(item => item.MaHieuCanBo == maHieuCanBo && item.MaCheDo == pc.SMaCheDo);
                                giaTri.GiaTri = cheDo?.GiaTri ?? 0;
                                itemRpt.ListGiaTriCheDo.Add(giaTri);
                            }
                        }
                        bangLuongViewItems.Add(itemRpt);
                    }

                    // Data to import QTC BHXH
                    var importQTCBHXHItems = _luongThangBHXHService.ExportDataQTCBHXH(_sessionInfo.YearOfWork, SelectedMonths).ToList();
                    // Data Can Bo Che Do
                    var dataCanBoCheDos = _tlCanBoCheDoBHXHService.ExportCanBoCheDo(_sessionInfo.YearOfWork, SelectedMonths).ToList();
                    // Data Che Do BHXH
                    var dataCheDoBHXHs = _iTlDmCheDoBHXHService.GetCheDoBHXHMapping();
                    Dictionary<string, object> data = new Dictionary<string, object>();
                    data.Add("LstHeaderItems", lstHeaderItems);
                    data.Add("BangLuongViewItems", bangLuongViewItems);
                    data.Add("ImportQTCBHXHItems", importQTCBHXHItems);
                    data.Add("CanBoCheDoItems", dataCanBoCheDos);
                    data.Add("CheDoBHXHItems", dataCheDoBHXHs);
                    fileNamePrefix = string.Format("Bang_Luong_BHXH_Chi_Tiet_Quy_{0}_{1}", SelectedQuarter.ValueItem != null ? SelectedQuarter.ValueItem : "", DateTime.Now.Year);
                    fileNameWithoutExtension = StringUtils.CreateExportFileName(fileNamePrefix);
                    var xlsFile = _exportService.Export<ExportChiTietBangLuongBHXHNq104Model, TlBangLuongThangBHXHNq104Query, TlCanBoCheDoBHXHQuery, TlDmCheDoBHXHQuery>(templateFileName, data);
                    results.Add(new ExportResult(fileNameWithoutExtension, fileNameWithoutExtension, null, xlsFile));

                    e.Result = results;
                }, (s, e) =>
                {
                    if (e.Error == null)
                    {
                        var result = (List<ExportResult>)e.Result;
                        _exportService.Open(result, ExportType.EXCEL);
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

        private void LoadMonthOfQuarter()
        {
            switch (SelectedQuarter.ValueItem)
            {
                case Quarter.QUY1:
                    SelectedMonths = MonthOfQuarter.QUY1;
                    break;
                case Quarter.QUY2:
                    SelectedMonths = MonthOfQuarter.QUY2;
                    break;
                case Quarter.QUY3:
                    SelectedMonths = MonthOfQuarter.QUY3;
                    break;
                case Quarter.QUY4:
                    SelectedMonths = MonthOfQuarter.QUY4;
                    break;
            }
        }
    }
}

