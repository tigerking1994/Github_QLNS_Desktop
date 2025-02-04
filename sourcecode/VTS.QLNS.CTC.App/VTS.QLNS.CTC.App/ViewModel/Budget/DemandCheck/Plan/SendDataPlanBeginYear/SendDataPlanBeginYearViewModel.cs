using AutoMapper;
using log4net;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;
using VTS.QLNS.CTC.App.Command;
using VTS.QLNS.CTC.App.Helper;
using VTS.QLNS.CTC.App.Model;
using VTS.QLNS.CTC.App.Model.Control;
using VTS.QLNS.CTC.App.Service;
using VTS.QLNS.CTC.App.Service.Impl;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Service;
using VTS.QLNS.CTC.Utility;
using VTS.QLNS.CTC.Utility.Enum;

namespace VTS.QLNS.CTC.App.ViewModel.Budget.DemandCheck.Plan.SendDataPlanBeginYear
{
    public class SendDataPlanBeginYearViewModel : ViewModelBase
    {
        private readonly ILog _logger;
        private readonly IMapper _mapper;
        private SessionInfo _sessionInfo;
        private readonly ISessionService _sessionService;
        private readonly IDanhMucService _danhMucService;
        private readonly IExportService _exportService;
        private readonly INsMucLucNganSachService _mucLucNganSachService;
        private readonly ISoLieuChiTietPhanCapService _soLieuChiTietPhanCapService;
        private readonly ISktSoLieuService _sktSoLieuService;
        private readonly ISktSoLieuChiTietCanCuDataService _sktSoLieuChiTietCanCuDataService;
        private readonly ICauHinhCanCuService _iCauHinhCanCuService;
        private readonly IVdtFtpRootService _ftpService;
        private readonly FtpStorageService _ftpStorageService;
        private readonly INsPhongBanService _phongBanService;
        private readonly IHTTPUploadFileService _hTTPUploadFileService;
        private readonly ICryptographyService _cryptographyService;
        private readonly INsDonViService _donViService;
        private readonly ISktSoLieuChungTuService _sktChungTuService;

        public PlanBeginYearModel SelectedPlanSummary;
        public List<SktSoLieuChiTietMLNSModel> DataPlanBeginYearExport;
        public ComboboxItem SelectedLoaiNganSach;
        public ObservableCollection<PlanBeginYearModel> DataPlanSummary;

        public string LoaiChungTu { get; set; }

        public string _cap1 { get; set; }
        public string HeaderThucHien { get; set; }
        public string HeaderChiTiet { get; set; }
        public PlanBeginYearModel SelectedPlan;
        private List<NsMucLucNganSach> _mucLucNganSaches;

        public delegate void DataChangedEventHandler(object sender, EventArgs e);
        public event DataChangedEventHandler ClosePopup;
        public bool IsSendHTTP;
        public string TitleButton => IsSendHTTP ? "Gửi dữ liệu HTTP" : "Gửi dữ liệu FTP";


        public override Type ContentType => typeof(View.Budget.DemandCheck.Plan.SendDataPlanBeginYear.SendDataPlanBeginYear);

        private string _tenBaoCao;
        public string TenBaoCao
        {
            get => _tenBaoCao;
            set => SetProperty(ref _tenBaoCao, value);
        }

        private ObservableCollection<ComboboxItem> _bTieuChiItems = new ObservableCollection<ComboboxItem>();

        public ObservableCollection<ComboboxItem> BTieuChiItems
        {
            get => _bTieuChiItems;
            set => SetProperty(ref _bTieuChiItems, value);
        }

        private ComboboxItem _bTieuChiSelected;
        public ComboboxItem BTieuChiSelected
        {
            get => _bTieuChiSelected;
            set
            {
                SetProperty(ref _bTieuChiSelected, value);
            }
        }

        private ObservableCollection<ComboboxItem> _loaiBaoCao;
        public ObservableCollection<ComboboxItem> LoaiBaoCao
        {
            get => _loaiBaoCao;
            set => SetProperty(ref _loaiBaoCao, value);
        }

        private ComboboxItem _loaiBaoCaoSelected;
        public ComboboxItem LoaiBaoCaoSelected
        {
            get => _loaiBaoCaoSelected;
            set
            {
                SetProperty(ref _loaiBaoCaoSelected, value);
            }
        }

        public ImportTabIndex TabIndex;

        public RelayCommand ExportCommand { get; }
        public RelayCommand CloseWindowCommand { get; }

        public void LoadTieuChis()
        {

            List<DanhMuc> danhMucCauHinh = _danhMucService.FindByType(TypeDanhMuc.DM_CAUHINH, _sessionService.Current.YearOfWork).ToList();
            if (danhMucCauHinh.Count > 0)
            {
                DanhMuc danhMucMLNS = _danhMucService.FindByType(TypeDanhMuc.DM_CAUHINH, _sessionService.Current.YearOfWork).Where(n => n.IIDMaDanhMuc == MaDanhMuc.MLNS_CHITIET_TOI).FirstOrDefault();
                string chiTietToi = danhMucMLNS == null ? string.Empty : danhMucMLNS.SGiaTri;
                BTieuChiItems = new ObservableCollection<ComboboxItem>(DynamicMLNS.CreateMLNSReportSetting(chiTietToi, false));
                _bTieuChiSelected = BTieuChiItems != null ? BTieuChiItems[0] : null;
            }

        }

        public void LoadLoaiBaoCaoGui()
        {
            LoaiBaoCao = new ObservableCollection<ComboboxItem>();
            LoaiBaoCao.Add(new ComboboxItem { ValueItem = LoaiBaoCaoUpload.TONG_HOP_VALUE.ToString(), DisplayItem = LoaiBaoCaoUpload.TONG_HOP });
            LoaiBaoCao.Add(new ComboboxItem { ValueItem = LoaiBaoCaoUpload.B_QUAN_LY_VALUE.ToString(), DisplayItem = LoaiBaoCaoUpload.B_QUAN_LY });
            _loaiBaoCaoSelected = LoaiBaoCao != null ? LoaiBaoCao[0] : null;
            OnPropertyChanged(nameof(LoaiBaoCaoSelected));
        }

        public void LoadTenBaoCao()
        {
            _tenBaoCao = StringUtils.ConvertVN(string.Format("{0}_DTDN_{1}", SelectedPlanSummary.TenDonVi, SelectedPlanSummary.SSoChungTu));
        }

        private bool IsDonViRoot(string iIDMaDonVi) => iIDMaDonVi == _sessionInfo.IdDonVi;

        public SendDataPlanBeginYearViewModel(ILog logger,
                                              IMapper mapper,
                                              ISessionService sessionService,
                                              IDanhMucService danhMucService,
                                              INsDonViService donViService,
                                              INsQtChungTuChiTietService chungTuChiTietService,
                                              INsMucLucNganSachService mucLucNganSachService,
                                              IExportService exportService,
                                              ISoLieuChiTietPhanCapService soLieuChiTietPhanCapService,
                                              ISktSoLieuService sktSoLieuService,
                                              ISktSoLieuChiTietCanCuDataService sktSoLieuChiTietCanCuDataService,
                                              ICauHinhCanCuService iCauHinhCanCuService,
                                              IVdtFtpRootService ftpService,
                                              FtpStorageService ftpStorageService,
                                              INsPhongBanService nsPhongBanService,
                                              IHTTPUploadFileService hTTPUploadFileService,
                                              ICryptographyService cryptographyService,
                                              INsDonViService nsDonViService,
                                              ISktSoLieuChungTuService sktChungTuService)
        {

            _logger = logger;
            _mapper = mapper;
            _sessionService = sessionService;
            _danhMucService = danhMucService;
            _mucLucNganSachService = mucLucNganSachService;
            _exportService = exportService;
            _sktChungTuService = sktChungTuService;

            ExportCommand = new RelayCommand(obj => OnUpload());
            CloseWindowCommand = new RelayCommand(obj => OnCloseWindow());
            _soLieuChiTietPhanCapService = soLieuChiTietPhanCapService;
            _sktSoLieuService = sktSoLieuService;
            _sktSoLieuChiTietCanCuDataService = sktSoLieuChiTietCanCuDataService;
            _iCauHinhCanCuService = iCauHinhCanCuService;
            _ftpService = ftpService;
            _ftpStorageService = ftpStorageService;
            _phongBanService = nsPhongBanService;
            _hTTPUploadFileService = hTTPUploadFileService;
            _cryptographyService = cryptographyService;
            _donViService = nsDonViService;
        }

        public override void Init()
        {
            base.Init();
            LoadTieuChis();
            LoadLoaiBaoCaoGui();
            _sessionInfo = _sessionService.Current;
            _mucLucNganSaches = _mucLucNganSachService.FindAll(_sessionInfo.YearOfWork).ToList();
            LoadTenBaoCao();
        }

        private void OnCloseWindow()
        {
            DataChangedEventHandler handler = ClosePopup;
            if (handler != null)
            {
                handler(this, new EventArgs());
            }
        }

        private async void OnUpload()
        {

            IsLoading = true;
            IEnumerable<string> departments = _phongBanService.FindByCondition(x => x.INamLamViec == _sessionInfo.YearOfWork).Select(n => n.IIDMaBQuanLy);
            try
            {
                (int, string) token = await _hTTPUploadFileService.GetToken(IsSendHTTP);
                string salt = _cryptographyService.GetSalt();
                string tokenKey = Scramble(token.Item2 + salt);
                int namLamViec = _sessionService.Current.YearOfWork;
                int count = 0;

                List<NsMucLucNganSach> mucLucNganSaches = _mucLucNganSachService.FindAll(namLamViec).ToList();
                List<string> lstHeader = new List<string>() { string.Empty, string.Empty, string.Empty, string.Empty, string.Empty };
                List<bool> lstCheckData = new List<bool>() { false, false, false, false, false };

                if (LoaiBaoCaoSelected.ValueItem == LoaiBaoCaoUpload.TONG_HOP_VALUE.ToString())
                {
                    GetDataPlanBeginYearExportAggregate(ref lstHeader, ref lstCheckData, SelectedPlanSummary);
                    if (!DataPlanBeginYearExport.Any())
                    {
                        count++;
                    }

                    DataPlanBeginYearExport = new List<SktSoLieuChiTietMLNSModel>(DataPlanBeginYearExport.Where(n => n.ChiTiet != 0 || n.UocThucHien != 0 || n.QuyetToan != 0 || n.DuToan != 0
                    || n.HangNhap != 0 || n.HangMua != 0 || n.PhanCap != 0 || n.ChuaPhanCap != 0
                    || n.FTuChi1 != 0 || n.FTuChi2 != 0 || n.FTuChi3 != 0 || n.FTuChi4 != 0 || n.FTuChi5 != 0
                    || n.FMHHV1 != 0 || n.FMHHV2 != 0 || n.FMHHV3 != 0 || n.FMHHV4 != 0 || n.FMHHV5 != 0
                    || n.FPhanCap1 != 0 || n.FPhanCap2 != 0 || n.FPhanCap3 != 0 || n.FPhanCap4 != 0 || n.FPhanCap5 != 0
                    ).ToList());

                    List<int> columnHidden = new List<int>();
                    switch (BTieuChiSelected.ValueItem)
                    {
                        case nameof(MLNSFiled.NG):
                            DataPlanBeginYearExport = DataPlanBeginYearExport.Where(x => string.IsNullOrEmpty(x.TNG)).ToList();
                            DataPlanBeginYearExport.Where(x => !string.IsNullOrEmpty(x.NG)).ForAll(x => { x.BHangChaDuToan = false; });
                            columnHidden.Add(8);
                            columnHidden.Add(9);
                            columnHidden.Add(10);
                            columnHidden.Add(11);
                            break;
                        case nameof(MLNSFiled.TNG):
                            DataPlanBeginYearExport = DataPlanBeginYearExport.Where(x => string.IsNullOrEmpty(x.TNG1)).ToList();
                            DataPlanBeginYearExport.Where(x => !string.IsNullOrEmpty(x.TNG)).Select(x => x.IsHangCha = false).ToList();
                            columnHidden.Add(9);
                            columnHidden.Add(10);
                            columnHidden.Add(11);
                            break;
                        case nameof(MLNSFiled.TNG1):
                            DataPlanBeginYearExport = DataPlanBeginYearExport.Where(x => string.IsNullOrEmpty(x.TNG2)).ToList();
                            DataPlanBeginYearExport.Where(x => !string.IsNullOrEmpty(x.TNG1)).Select(x => x.IsHangCha = false).ToList();
                            columnHidden.Add(10);
                            columnHidden.Add(11);
                            break;
                        case nameof(MLNSFiled.TNG2):
                            DataPlanBeginYearExport = DataPlanBeginYearExport.Where(x => string.IsNullOrEmpty(x.TNG3)).ToList();
                            DataPlanBeginYearExport.Where(x => !string.IsNullOrEmpty(x.TNG2)).Select(x => x.IsHangCha = false).ToList();
                            columnHidden.Add(11);
                            break;
                    }

                    Dictionary<string, object> data = new Dictionary<string, object>();
                    data.Add("Header1", _cap1 != null ? _cap1.ToUpper() : "");
                    data.Add("Header2", _sessionService.Current.TenDonVi.ToUpper());

                    data.Add("Title1", lstHeader[0]);
                    data.Add("Title2", lstHeader[1]);
                    data.Add("Title3", lstHeader[2]);
                    data.Add("Title4", lstHeader[3]);
                    data.Add("Title5", lstHeader[4]);

                    data.Add("HeaderThucHien", HeaderThucHien);
                    data.Add("HeaderChiTiet", HeaderChiTiet);
                    data.Add("Items", DataPlanBeginYearExport);
                    data.Add("MLNS", mucLucNganSaches);
                    data.Add("NamLamViec", _sessionService.Current.YearOfWork);
                    data.Add("ThoiGian", string.Format("TP.Hà Nội, ngày {0} tháng {1} năm {2}", DateTime.Now.Day, DateTime.Now.Month, DateTime.Now.Year));

                    PlanBeginYearModel item = DataPlanSummary.FirstOrDefault(x => x.Selected);
                    string tenNganSach;
                    string templateFileName;
                    string section = SelectedLoaiNganSach.ValueItem;
                    if (SelectedLoaiNganSach != null && SelectedLoaiNganSach.ValueItem == VoucherType.NSSD_Key)
                    {
                        double tongTien = (DataPlanBeginYearExport != null && DataPlanBeginYearExport.Count > 0) ? DataPlanBeginYearExport.Where(n => !n.IsHangCha).Select(n => n.ChiTiet).Sum() : 0;
                        data.Add("TongTienBangChu", StringUtils.NumberToText(tongTien, true));

                        tenNganSach = "NSSD_";
                        templateFileName = Path.Combine(ExportPrefix.PATH_TL_DTDN, ExportFileName.EPT_NS_DUTOANDAUNAM_CHUNGTU_TONGHOP_NSSD);
                    }
                    else
                    {
                        double tongTien = 0;
                        if (DataPlanBeginYearExport != null && DataPlanBeginYearExport.Count > 0)
                        {
                            tongTien += DataPlanBeginYearExport.Where(n => !n.IsHangCha).Select(n => n.HangNhap).Sum();
                            tongTien += DataPlanBeginYearExport.Where(n => !n.IsHangCha).Select(n => n.HangMua).Sum();
                            tongTien += DataPlanBeginYearExport.Where(n => !n.IsHangCha).Select(n => n.PhanCap).Sum();
                            tongTien += DataPlanBeginYearExport.Where(n => !n.IsHangCha).Select(n => n.ChuaPhanCap).Sum();
                        }
                        data.Add("TongTienBangChu", StringUtils.NumberToText(tongTien, true));

                        List<SoLieuChiTietPhanCapQuery> dataPhanCap = _soLieuChiTietPhanCapService.GetSoLieuChiTietPhanCapDonVi0_1(_sessionService.Current.YearOfWork, SelectedPlanSummary.Id).ToList();
                        ObservableCollection<SktSoLieuPhanCapModel> dataPhanCapExport = _mapper.Map<ObservableCollection<SktSoLieuPhanCapModel>>(dataPhanCap);
                        double tongTienPhanCap = (dataPhanCap != null && dataPhanCap.Count > 0) ? dataPhanCap.Where(n => !n.bHangCha).Select(n => n.TuChi).Sum() : 0;
                        data.Add("TongTienBangChuPhanCap", StringUtils.NumberToText(tongTienPhanCap, true));

                        data.Add("ItemPhanCaps", dataPhanCapExport);
                        tenNganSach = "NSBD_";
                        templateFileName = Path.Combine(ExportPrefix.PATH_TL_DTDN, ExportFileName.EPT_NS_DUTOANDAUNAM_CHUNGTU_TONGHOP_NSBD);
                    }
                    string fileNamePrefix = string.Format("rptDuToan_DauNam_ChungTu_TongHop_{0}", tenNganSach);
                    string fileNameWithoutExtension = String.IsNullOrEmpty(TenBaoCao) ? StringUtils.ConvertVN(StringUtils.CreateExportFileName(fileNamePrefix)) : TenBaoCao;
                    if (SelectedLoaiNganSach != null && SelectedLoaiNganSach.ValueItem == VoucherType.NSSD_Key)
                    {
                        int startIndex = 13;
                        for (int i = 0; i < 5; i++)
                        {
                            if (!lstCheckData[i])
                            {
                                columnHidden.Add(startIndex);
                            }
                            startIndex += 1;
                        }
                        FlexCel.Core.ExcelFile xlsFile = _exportService.Export<SktSoLieuChiTietMLNSModel, NsMucLucNganSach>(templateFileName, data, columnHidden);
                        FlexCel.Core.TXlsNamedRange nameRange = xlsFile.GetNamedRange(1);
                        nameRange.Comment = "Workbook";
                        xlsFile.SetNamedRange(nameRange);
                        xlsFile.SetNamedRange(new FlexCel.Core.TXlsNamedRange("__Area_Items__", 1, 0, "=1:2,A:B"));
                        xlsFile.SetCellValue(50, 50, "CheckSum");
                        xlsFile.SetRowHidden(50, true);
                        ExportResult Result = new ExportResult(fileNameWithoutExtension, fileNameWithoutExtension, null, xlsFile);
                        MemoryStream fileStream = new MemoryStream();
                        MemoryStream outputFileStream = new MemoryStream();
                        _exportService.Open(Result, ref fileStream);
                        await _cryptographyService.EncryptFile(fileStream, ref outputFileStream, tokenKey);
                        await _hTTPUploadFileService.UploadFile(IsSendHTTP, new FileUploadStreamModel()
                        {
                            File = outputFileStream,
                            Name = fileNameWithoutExtension + FileExtensionFormats.Security,
                            Description = "Chứng từ tổng hợp",
                            Module = NSFunctionCode.BUDGET,
                            ModuleName = "Số nhu cầu - Kiểm tra",
                            SubModule = SelectedLoaiNganSach.ValueItem == VoucherType.NSSD_Key ? NSFunctionCode.BUDGET_USE_DEMANDCHECK_PLAN_BEGIN_YEAR : NSFunctionCode.BUDGET_GUARANTEE_DEMANDCHECK_PLAN_BEGIN_YEAR,
                            SubModuleName = "Lập dự toán đầu năm",
                            TokenKey = tokenKey,
                            YearOfWork = _sessionInfo.YearOfWork,
                            YearOfBudget = _sessionInfo.YearOfBudget,
                            SourceOfBudget = _sessionInfo.Budget,
                            Department = "",
                            Quarter = ""
                        });
                    }
                    else
                    {
                        int startIndex = 13;
                        for (int i = 0; i < 5; i++)
                        {
                            if (!lstCheckData[i])
                            {
                                columnHidden.AddRange(new List<int> { startIndex, startIndex + 1 });
                            }
                            startIndex += 2;
                        }

                        FlexCel.Core.ExcelFile xlsFile = _exportService.Export<SktSoLieuChiTietMLNSModel, NsMucLucNganSach, SktSoLieuPhanCapModel>(templateFileName, data, columnHidden);
                        FlexCel.Core.TXlsNamedRange nameRange = xlsFile.GetNamedRange(1);
                        nameRange.Comment = "Workbook";
                        xlsFile.SetNamedRange(nameRange);
                        xlsFile.SetNamedRange(new FlexCel.Core.TXlsNamedRange("__Area_Items__", 1, 0, "=1:2,A:B"));
                        xlsFile.SetCellValue(50, 50, "CheckSum");
                        xlsFile.SetRowHidden(50, true);
                        ExportResult Result = new ExportResult(fileNameWithoutExtension, fileNameWithoutExtension, null, xlsFile);
                        MemoryStream fileStream = new MemoryStream();
                        MemoryStream outputFileStream = new MemoryStream();
                        _exportService.Open(Result, ref fileStream);
                        await _cryptographyService.EncryptFile(fileStream, ref outputFileStream, tokenKey);
                        await _hTTPUploadFileService.UploadFile(IsSendHTTP, new FileUploadStreamModel()
                        {
                            File = outputFileStream,
                            Name = fileNameWithoutExtension + FileExtensionFormats.Security,
                            Description = "Chứng từ tổng hợp",
                            Module = NSFunctionCode.BUDGET,
                            ModuleName = "Số nhu cầu - Kiểm tra",
                            SubModule = SelectedLoaiNganSach.ValueItem == VoucherType.NSSD_Key ? NSFunctionCode.BUDGET_USE_DEMANDCHECK_PLAN_BEGIN_YEAR : NSFunctionCode.BUDGET_GUARANTEE_DEMANDCHECK_PLAN_BEGIN_YEAR,
                            SubModuleName = "Lập dự toán đầu năm",
                            TokenKey = tokenKey,
                            YearOfWork = _sessionInfo.YearOfWork,
                            YearOfBudget = _sessionInfo.YearOfBudget,
                            SourceOfBudget = _sessionInfo.Budget,
                            Department = "",
                            Quarter = ""
                        });
                    }
                }
                else
                {
                    foreach (string department in departments)
                    {
                        GetDataPlanBeginYearExportByDeparment(ref lstHeader, ref lstCheckData, SelectedPlanSummary, department);
                        if (!DataPlanBeginYearExport.Any())
                        {
                            count++;
                            continue;
                        }

                        DataPlanBeginYearExport = new List<SktSoLieuChiTietMLNSModel>(DataPlanBeginYearExport.Where(n => n.ChiTiet != 0 || n.UocThucHien != 0 || n.QuyetToan != 0 || n.DuToan != 0
                        || n.HangNhap != 0 || n.HangMua != 0 || n.PhanCap != 0 || n.ChuaPhanCap != 0
                        || n.FTuChi1 != 0 || n.FTuChi2 != 0 || n.FTuChi3 != 0 || n.FTuChi4 != 0 || n.FTuChi5 != 0
                        || n.FMHHV1 != 0 || n.FMHHV2 != 0 || n.FMHHV3 != 0 || n.FMHHV4 != 0 || n.FMHHV5 != 0
                        || n.FPhanCap1 != 0 || n.FPhanCap2 != 0 || n.FPhanCap3 != 0 || n.FPhanCap4 != 0 || n.FPhanCap5 != 0
                        ).ToList());

                        List<int> columnHidden = new List<int>();
                        switch (BTieuChiSelected.ValueItem)
                        {
                            case nameof(MLNSFiled.NG):
                                DataPlanBeginYearExport = DataPlanBeginYearExport.Where(x => string.IsNullOrEmpty(x.TNG)).ToList();
                                DataPlanBeginYearExport.Where(x => !string.IsNullOrEmpty(x.NG)).ForAll(x => { x.BHangChaDuToan = false; });
                                columnHidden.Add(8);
                                columnHidden.Add(9);
                                columnHidden.Add(10);
                                columnHidden.Add(11);
                                break;
                            case nameof(MLNSFiled.TNG):
                                DataPlanBeginYearExport = DataPlanBeginYearExport.Where(x => string.IsNullOrEmpty(x.TNG1)).ToList();
                                DataPlanBeginYearExport.Where(x => !string.IsNullOrEmpty(x.TNG)).Select(x => x.IsHangCha = false).ToList();
                                columnHidden.Add(9);
                                columnHidden.Add(10);
                                columnHidden.Add(11);
                                break;
                            case nameof(MLNSFiled.TNG1):
                                DataPlanBeginYearExport = DataPlanBeginYearExport.Where(x => string.IsNullOrEmpty(x.TNG2)).ToList();
                                DataPlanBeginYearExport.Where(x => !string.IsNullOrEmpty(x.TNG1)).Select(x => x.IsHangCha = false).ToList();
                                columnHidden.Add(10);
                                columnHidden.Add(11);
                                break;
                            case nameof(MLNSFiled.TNG2):
                                DataPlanBeginYearExport = DataPlanBeginYearExport.Where(x => string.IsNullOrEmpty(x.TNG3)).ToList();
                                DataPlanBeginYearExport.Where(x => !string.IsNullOrEmpty(x.TNG2)).Select(x => x.IsHangCha = false).ToList();
                                columnHidden.Add(11);
                                break;
                        }

                        Dictionary<string, object> data = new Dictionary<string, object>();
                        data.Add("Header1", _cap1 != null ? _cap1.ToUpper() : "");
                        data.Add("Header2", _sessionService.Current.TenDonVi.ToUpper());

                        data.Add("Title1", lstHeader[0]);
                        data.Add("Title2", lstHeader[1]);
                        data.Add("Title3", lstHeader[2]);
                        data.Add("Title4", lstHeader[3]);
                        data.Add("Title5", lstHeader[4]);

                        data.Add("HeaderThucHien", HeaderThucHien);
                        data.Add("HeaderChiTiet", HeaderChiTiet);
                        data.Add("Items", DataPlanBeginYearExport);
                        data.Add("MLNS", mucLucNganSaches);
                        data.Add("NamLamViec", _sessionService.Current.YearOfWork);
                        data.Add("ThoiGian", string.Format("TP.Hà Nội, ngày {0} tháng {1} năm {2}", DateTime.Now.Day, DateTime.Now.Month, DateTime.Now.Year));

                        PlanBeginYearModel item = DataPlanSummary.FirstOrDefault(x => x.Selected);
                        string tenNganSach;
                        string templateFileName;
                        string section = SelectedLoaiNganSach.ValueItem;
                        if (SelectedLoaiNganSach != null && SelectedLoaiNganSach.ValueItem == VoucherType.NSSD_Key)
                        {
                            double tongTien = (DataPlanBeginYearExport != null && DataPlanBeginYearExport.Count > 0) ? DataPlanBeginYearExport.Where(n => !n.IsHangCha).Select(n => n.ChiTiet).Sum() : 0;
                            data.Add("TongTienBangChu", StringUtils.NumberToText(tongTien, true));

                            tenNganSach = "NSSD_";
                            templateFileName = Path.Combine(ExportPrefix.PATH_TL_DTDN, ExportFileName.EPT_NS_DUTOANDAUNAM_CHUNGTU_TONGHOP_NSSD);
                        }
                        else
                        {
                            double tongTien = 0;
                            if (DataPlanBeginYearExport != null && DataPlanBeginYearExport.Count > 0)
                            {
                                tongTien += DataPlanBeginYearExport.Where(n => !n.IsHangCha).Select(n => n.HangNhap).Sum();
                                tongTien += DataPlanBeginYearExport.Where(n => !n.IsHangCha).Select(n => n.HangMua).Sum();
                                tongTien += DataPlanBeginYearExport.Where(n => !n.IsHangCha).Select(n => n.PhanCap).Sum();
                                tongTien += DataPlanBeginYearExport.Where(n => !n.IsHangCha).Select(n => n.ChuaPhanCap).Sum();
                            }
                            data.Add("TongTienBangChu", StringUtils.NumberToText(tongTien, true));

                            List<SoLieuChiTietPhanCapQuery> dataPhanCap = _soLieuChiTietPhanCapService.GetSoLieuChiTietPhanCapDonVi0_1(_sessionService.Current.YearOfWork, SelectedPlanSummary.Id).ToList();
                            ObservableCollection<SktSoLieuPhanCapModel> dataPhanCapExport = _mapper.Map<ObservableCollection<SktSoLieuPhanCapModel>>(dataPhanCap);
                            double tongTienPhanCap = (dataPhanCap != null && dataPhanCap.Count > 0) ? dataPhanCap.Where(n => !n.bHangCha).Select(n => n.TuChi).Sum() : 0;
                            data.Add("TongTienBangChuPhanCap", StringUtils.NumberToText(tongTienPhanCap, true));
                            //if (dataPhanCapExport == null || !dataPhanCapExport.Any())
                            //{
                            //    MessageBoxHelper.Warning(string.Format("Không có dữ liệu phân cấp"));
                            //    return;
                            //}

                            data.Add("ItemPhanCaps", dataPhanCapExport);
                            tenNganSach = "NSBD_";
                            templateFileName = Path.Combine(ExportPrefix.PATH_TL_DTDN, ExportFileName.EPT_NS_DUTOANDAUNAM_CHUNGTU_TONGHOP_NSBD);
                        }
                        string fileNamePrefix = string.Format("rptDuToan_DauNam_ChungTu_TongHop_{0}", tenNganSach);
                        string fileNameWithoutExtension = string.Format("{0}_B{1}", String.IsNullOrEmpty(TenBaoCao) ? StringUtils.ConvertVN(StringUtils.CreateExportFileName(fileNamePrefix)) : TenBaoCao, department);
                        if (SelectedLoaiNganSach != null && SelectedLoaiNganSach.ValueItem == VoucherType.NSSD_Key)
                        {
                            int startIndex = 13;
                            for (int i = 0; i < 5; i++)
                            {
                                if (!lstCheckData[i])
                                {
                                    columnHidden.Add(startIndex);
                                }
                                startIndex += 1;
                            }
                            FlexCel.Core.ExcelFile xlsFile = _exportService.Export<SktSoLieuChiTietMLNSModel, NsMucLucNganSach>(templateFileName, data, columnHidden);
                            FlexCel.Core.TXlsNamedRange nameRange = xlsFile.GetNamedRange(1);
                            nameRange.Comment = "Workbook";
                            xlsFile.SetNamedRange(nameRange);
                            xlsFile.SetNamedRange(new FlexCel.Core.TXlsNamedRange("__Area_Items__", 1, 0, "=1:2,A:B"));
                            xlsFile.SetCellValue(50, 50, "CheckSum");
                            xlsFile.SetRowHidden(50, true);
                            ExportResult Result = new ExportResult(fileNameWithoutExtension, fileNameWithoutExtension, null, xlsFile);
                            MemoryStream fileStream = new MemoryStream();
                            MemoryStream outputFileStream = new MemoryStream();
                            _exportService.Open(Result, ref fileStream);
                            await _cryptographyService.EncryptFile(fileStream, ref outputFileStream, tokenKey);
                            await _hTTPUploadFileService.UploadFile(IsSendHTTP, new FileUploadStreamModel()
                            {
                                File = outputFileStream,
                                Name = fileNameWithoutExtension + FileExtensionFormats.Security,
                                Description = "Chứng từ tổng hợp",
                                Module = NSFunctionCode.BUDGET,
                                ModuleName = "Số nhu cầu - Kiểm tra",
                                SubModule = SelectedLoaiNganSach.ValueItem == VoucherType.NSSD_Key ? NSFunctionCode.BUDGET_USE_DEMANDCHECK_PLAN_BEGIN_YEAR : NSFunctionCode.BUDGET_GUARANTEE_DEMANDCHECK_PLAN_BEGIN_YEAR,
                                SubModuleName = "Lập dự toán đầu năm",
                                TokenKey = tokenKey,
                                YearOfWork = _sessionInfo.YearOfWork,
                                YearOfBudget = _sessionInfo.YearOfBudget,
                                SourceOfBudget = _sessionInfo.Budget,
                                Department = department,
                                Quarter = ""
                            });
                        }
                        else
                        {
                            int startIndex = 13;
                            for (int i = 0; i < 5; i++)
                            {
                                if (!lstCheckData[i])
                                {
                                    columnHidden.AddRange(new List<int> { startIndex, startIndex + 1 });
                                }
                                startIndex += 2;
                            }

                            FlexCel.Core.ExcelFile xlsFile = _exportService.Export<SktSoLieuChiTietMLNSModel, NsMucLucNganSach, SktSoLieuPhanCapModel>(templateFileName, data, columnHidden);
                            FlexCel.Core.TXlsNamedRange nameRange = xlsFile.GetNamedRange(1);
                            nameRange.Comment = "Workbook";
                            xlsFile.SetNamedRange(nameRange);
                            xlsFile.SetNamedRange(new FlexCel.Core.TXlsNamedRange("__Area_Items__", 1, 0, "=1:2,A:B"));
                            xlsFile.SetCellValue(50, 50, "CheckSum");
                            xlsFile.SetRowHidden(50, true);
                            ExportResult Result = new ExportResult(fileNameWithoutExtension, fileNameWithoutExtension, null, xlsFile);
                            MemoryStream fileStream = new MemoryStream();
                            MemoryStream outputFileStream = new MemoryStream();
                            _exportService.Open(Result, ref fileStream);
                            await _cryptographyService.EncryptFile(fileStream, ref outputFileStream, tokenKey);
                            await _hTTPUploadFileService.UploadFile(IsSendHTTP, new FileUploadStreamModel()
                            {
                                File = outputFileStream,
                                Name = fileNameWithoutExtension + FileExtensionFormats.Security,
                                Description = "Chứng từ tổng hợp",
                                Module = NSFunctionCode.BUDGET,
                                ModuleName = "Số nhu cầu - Kiểm tra",
                                SubModule = SelectedLoaiNganSach.ValueItem == VoucherType.NSSD_Key ? NSFunctionCode.BUDGET_USE_DEMANDCHECK_PLAN_BEGIN_YEAR : NSFunctionCode.BUDGET_GUARANTEE_DEMANDCHECK_PLAN_BEGIN_YEAR,
                                SubModuleName = "Lập dự toán đầu năm",
                                TokenKey = tokenKey,
                                YearOfWork = _sessionInfo.YearOfWork,
                                YearOfBudget = _sessionInfo.YearOfBudget,
                                SourceOfBudget = _sessionInfo.Budget,
                                Department = department,
                                Quarter = ""
                            });
                        }
                    }
                }
                if (departments.Count() == count && LoaiBaoCaoSelected.ValueItem == LoaiBaoCaoUpload.B_QUAN_LY_VALUE.ToString())
                {
                    NsDtdauNamChungTu entityError = _sktChungTuService.Find(SelectedPlanSummary.Id);
                    entityError.DNgaySua = DateTime.Now;
                    entityError.IsSent = false;
                    _sktChungTuService.Update(entityError);
                    IsLoading = false;
                    MessageBox.Show(new StringBuilder("Không có dữ liệu gửi").ToString());
                    return;
                }

                NsDtdauNamChungTu entity = _sktChungTuService.Find(SelectedPlanSummary.Id);
                entity.DNgaySua = DateTime.Now;
                entity.IsSent = true;
                _sktChungTuService.Update(entity);

                IsLoading = false;
                MessageBox.Show(new StringBuilder("Gửi dữ liệu thành công").ToString());
                return;

            }
            catch (Exception ex)
            {
                NsDtdauNamChungTu entityError = _sktChungTuService.Find(SelectedPlanSummary.Id);
                entityError.DNgaySua = DateTime.Now;
                entityError.IsSent = false;
                _sktChungTuService.Update(entityError);

                IsLoading = false;
                MessageBox.Show(new StringBuilder("Gửi dữ liệu thất bại").ToString());
                _logger.Error(ex.Message, ex);
                return;
            }

        }

        private string Scramble(string s)
        {
            return new string(s.ToCharArray().OrderBy(x => Guid.NewGuid()).ToArray());
        }

        public void GetDataPlanBeginYearExportByDeparment(ref List<string> lstHeader, ref List<bool> lstCheckData, PlanBeginYearModel item, string department)
        {
            IEnumerable<string> listMlns = _mucLucNganSaches.Where(n => n.IdPhongBan == department).Select(n => n.Lns);
            IEnumerable<string> listLnsChild = item.DsLNS.Split(StringUtils.COMMA).Where(x => listMlns.Contains(x));
            DataPlanBeginYearExport = new List<SktSoLieuChiTietMLNSModel>();

            if (listLnsChild.Any())
            {
                List<string> listLns = new List<string>();

                foreach (string sLns in listLnsChild)
                {
                    listLns.AddRange(StringUtils.SplitXauNoiMaParent(sLns));
                }
                List<SktSoLieuChiTietMlnsQuery> dataPlanExport = _sktSoLieuService.FindByConditionDonVi0(_sessionService.Current.YearOfWork, _sessionService.Current.YearOfBudget,
                _sessionService.Current.Budget, 0, 0, SelectedPlanSummary.Id_DonVi, LoaiChungTu, SelectedPlanSummary.Id.ToString(), string.Join(StringUtils.COMMA, listLns)).ToList();
                ObservableCollection<SktSoLieuChiTietMLNSModel> data = _mapper.Map<ObservableCollection<SktSoLieuChiTietMLNSModel>>(dataPlanExport);
                LoadDataCanCuExport(ref data, ref lstHeader, ref lstCheckData);
                DataPlanBeginYearExport = new List<SktSoLieuChiTietMLNSModel>(data.ToList());
            }

        }

        public void GetDataPlanBeginYearExportAggregate(ref List<string> lstHeader, ref List<bool> lstCheckData, PlanBeginYearModel item)
        {
            IEnumerable<string> listMlns = _mucLucNganSaches.Select(n => n.Lns);
            IEnumerable<string> listLnsChild = item.DsLNS.Split(StringUtils.COMMA).Where(x => listMlns.Contains(x));
            DataPlanBeginYearExport = new List<SktSoLieuChiTietMLNSModel>();

            if (listLnsChild.Any())
            {
                List<string> listLns = new List<string>();

                foreach (string sLns in listLnsChild)
                {
                    listLns.AddRange(StringUtils.SplitXauNoiMaParent(sLns));
                }
                List<SktSoLieuChiTietMlnsQuery> dataPlanExport = _sktSoLieuService.FindByConditionDonVi0(_sessionService.Current.YearOfWork, _sessionService.Current.YearOfBudget,
                _sessionService.Current.Budget, 0, 0, SelectedPlanSummary.Id_DonVi, LoaiChungTu, SelectedPlanSummary.Id.ToString(), string.Join(StringUtils.COMMA, listLns)).ToList();
                ObservableCollection<SktSoLieuChiTietMLNSModel> data = _mapper.Map<ObservableCollection<SktSoLieuChiTietMLNSModel>>(dataPlanExport);
                LoadDataCanCuExport(ref data, ref lstHeader, ref lstCheckData);
                DataPlanBeginYearExport = new List<SktSoLieuChiTietMLNSModel>(data.ToList());
            }

        }

        private void LoadDataCanCuExport(ref ObservableCollection<SktSoLieuChiTietMLNSModel> data, ref List<string> lstHeader, ref List<bool> lstCheckData)
        {
            string loaiChungTu = LoaiChungTu;
            int yearOfWork = _sessionService.Current.YearOfWork;
            System.Linq.Expressions.Expression<Func<NsCauHinhCanCu, bool>> predicate = PredicateBuilder.True<NsCauHinhCanCu>();
            predicate = predicate.And(item => item.SModule == TypeModuleCanCu.PLAN_BEGIN_YEAR);
            predicate = predicate.And(item => item.INamLamViec == yearOfWork);
            IOrderedEnumerable<NsCauHinhCanCu> listCanCu = _iCauHinhCanCuService.FindByCondition(predicate).OrderBy(n => n.INamCanCu);
            if (listCanCu == null) return;
            int DuToanNamTruocIndex = -1;
            int count = 0;
            foreach (NsCauHinhCanCu item in listCanCu)
            {
                if (item.IIDMaChucNang.Equals(TypeCanCu.ESTIMATE) && item.INamCanCu == yearOfWork - 1)
                {
                    DuToanNamTruocIndex = count;
                }
                System.Linq.Expressions.Expression<Func<NsDtdauNamChungTuChiTietCanCu, bool>> predicateCc = PredicateBuilder.True<NsDtdauNamChungTuChiTietCanCu>();
                predicateCc = predicateCc.And(x => x.IIdMaDonVi.Equals(SelectedPlanSummary.Id_DonVi));
                predicateCc = predicateCc.And(x => x.IIdCanCu.HasValue && x.IIdCanCu.Equals(item.Id));
                predicateCc = predicateCc.And(x => x.IID_CTDTDauNam == SelectedPlanSummary.Id);
                //predicateCc = predicateCc.And(x => x.LoaiChungTu == loaiChungTu);
                IEnumerable<NsDtdauNamChungTuChiTietCanCu> lstCanCu = _sktSoLieuChiTietCanCuDataService.FindByCondition(predicateCc);
                lstCheckData[count] = true;
                lstHeader[count] = item.STenCot;
                foreach (NsDtdauNamChungTuChiTietCanCu cc in lstCanCu)
                {
                    SktSoLieuChiTietMLNSModel mucLuc = data.FirstOrDefault(x => x.XauNoiMa == cc.SXauNoiMa);
                    if (mucLuc != null)
                    {

                        switch (item.IIDMaChucNang)
                        {
                            case TypeCanCu.SETTLEMENT:
                                mucLuc.FTuChi1 = cc.FTuChi;
                                mucLuc.FPhanCap1 = cc.FPhanCap;
                                mucLuc.FMHHV1 = cc.FHangNhap + cc.FHangMua;
                                mucLuc.HangMua1 = cc.FHangMua;
                                mucLuc.HangNhap1 = cc.FHangNhap;
                                break;
                            case TypeCanCu.ESTIMATE:
                                mucLuc.FTuChi2 = cc.FTuChi;
                                mucLuc.FPhanCap2 = cc.FPhanCap;
                                mucLuc.FMHHV2 = cc.FHangNhap + cc.FHangMua;
                                mucLuc.HangMua2 = cc.FHangMua;
                                mucLuc.HangNhap2 = cc.FHangNhap;
                                break;
                            case TypeCanCu.ALLOCATION:
                                mucLuc.FTuChi3 = cc.FTuChi;
                                mucLuc.FPhanCap3 = cc.FPhanCap;
                                mucLuc.FMHHV3 = cc.FHangNhap + cc.FHangMua;
                                mucLuc.HangMua3 = cc.FHangMua;
                                mucLuc.HangNhap3 = cc.FHangNhap;
                                break;
                            case TypeCanCu.DEMAND:
                                mucLuc.FTuChi4 = cc.FTuChi;
                                mucLuc.FPhanCap4 = cc.FPhanCap;
                                mucLuc.FMHHV4 = cc.FHangNhap + cc.FHangMua;
                                mucLuc.HangMua4 = cc.FHangMua;
                                mucLuc.HangNhap4 = cc.FHangNhap;
                                break;
                            case TypeCanCu.CHECK_NUMBER:
                                mucLuc.FTuChi5 = cc.FTuChi;
                                mucLuc.FPhanCap5 = cc.FPhanCap;
                                mucLuc.FMHHV5 = cc.FHangNhap + cc.FHangMua;
                                mucLuc.HangMua5 = cc.FHangMua;
                                mucLuc.HangNhap5 = cc.FHangNhap;
                                break;
                        }

                    }
                }
                count++;
            }
            CalculateData(ref data);
        }

        private void CalculateData(ref ObservableCollection<SktSoLieuChiTietMLNSModel> data)
        {
            data.Where(x => x.IsHangCha)
                .Select(x =>
                {
                    x.ChiTiet = 0; x.UocThucHien = 0; x.HangMua = 0; x.HangNhap = 0; x.PhanCap = 0; x.ChuaPhanCap = 0;
                    x.FTuChi1 = 0; x.FTuChi2 = 0; x.FTuChi3 = 0; x.FTuChi4 = 0; x.FTuChi5 = 0;
                    return x;
                }).ToList();
            foreach (SktSoLieuChiTietMLNSModel item in data.Where(x => !x.IsHangCha &&
            (x.ChiTiet != 0 || x.UocThucHien != 0 || x.HangMua != 0 || x.HangNhap != 0 || x.PhanCap != 0 || x.ChuaPhanCap != 0
             || x.FTuChi1 != 0 || x.FTuChi2 != 0 || x.FTuChi3 != 0 || x.FTuChi4 != 0 || x.FTuChi5 != 0)))
            {
                CalculateParent(ref data, item, item);
            }
        }

        private void CalculateParent(ref ObservableCollection<SktSoLieuChiTietMLNSModel> data, SktSoLieuChiTietMLNSModel currentItem, SktSoLieuChiTietMLNSModel selfItem)
        {
            SktSoLieuChiTietMLNSModel parentItem = data.FirstOrDefault(x => x.MlnsId == currentItem.MlnsIdParent);
            if (parentItem == null) return;
            parentItem.ChiTiet += selfItem.ChiTiet;
            parentItem.UocThucHien += selfItem.UocThucHien;
            parentItem.HangNhap += selfItem.HangNhap;
            parentItem.HangMua += selfItem.HangMua;
            parentItem.PhanCap += selfItem.PhanCap;
            parentItem.ChuaPhanCap += selfItem.ChuaPhanCap;
            parentItem.FTuChi1 += selfItem.FTuChi1;
            parentItem.FTuChi2 += selfItem.FTuChi2;
            parentItem.FTuChi3 += selfItem.FTuChi3;
            parentItem.FTuChi4 += selfItem.FTuChi4;
            parentItem.FTuChi5 += selfItem.FTuChi5;
            CalculateParent(ref data, parentItem, selfItem);
        }

    }
}
