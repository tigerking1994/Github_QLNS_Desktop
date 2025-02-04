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
using VTS.QLNS.CTC.Core.Domain.Criteria;
using VTS.QLNS.CTC.Core.Service;
using VTS.QLNS.CTC.Utility;
using VTS.QLNS.CTC.Utility.Enum;

namespace VTS.QLNS.CTC.App.ViewModel.Budget.DemandCheck.Demand.SendDataDemand
{
    public class SendDataDemandViewModel : GridViewModelBase<NsSktChungTuModel>
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
        private readonly ISktChungTuChiTietService _sktChungTuChiTietService;
        private readonly ISktMucLucService _SktMucLucService;
        private readonly ISktChungTuChiTietCanCuService _iSktChungTuChiTietCanCuService;
        private readonly ISktChungTuService _sktChungTuService;
        public delegate void DataChangedEventHandler(object sender, EventArgs e);
        public event DataChangedEventHandler ClosePopup;
        public bool IsSendHTTP;
        public string TitleButton => IsSendHTTP ? "Gửi dữ liệu HTTP" : "Gửi dữ liệu FTP";

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

        public ComboboxItem SelectedLoaiNganSach;
        public ObservableCollection<PlanBeginYearModel> DataPlanSummary;

        public string LoaiChungTu { get; set; }

        public string _cap1 { get; set; }
        public string HeaderThucHien { get; set; }
        public string HeaderChiTiet { get; set; }
        public PlanBeginYearModel SelectedPlan;
        private List<NsMucLucNganSach> _mucLucNganSaches;
        public NsSktChungTuModel _selectedNsSktChungTuModel;
        public NsSktChungTuModel SelectedNsSktChungTuModel
        {
            get => _selectedNsSktChungTuModel;
            set => SetProperty(ref _selectedNsSktChungTuModel, value);
        }

        public override Type ContentType => typeof(View.Budget.DemandCheck.Demand.SendDataDemand.SendDataDemand);

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

        public ImportTabIndex TabIndex;

        public RelayCommand ExportCommand { get; }
        public RelayCommand CloseWindowCommand { get; }

        public void LoadTenBaoCao()
        {
            NsSktChungTuModel itemImport = Items.Where(x => x.IIdMaDonVi == _selectedNsSktChungTuModel.IIdMaDonVi && x.ILoai == _selectedNsSktChungTuModel.ILoai && x.Selected).FirstOrDefault();
            _tenBaoCao = StringUtils.ConvertVN(string.Format("{0}_SNC_{1}", itemImport.STenDonVi, itemImport.SSoChungTu));
        }
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

        private bool IsDonViRoot(string iIDMaDonVi) => iIDMaDonVi == _sessionInfo.IdDonVi;

        public SendDataDemandViewModel(ILog logger,
                                              IMapper mapper,
                                              ISessionService sessionService,
                                              IDanhMucService danhMucService,
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
                                              ISktChungTuChiTietService sktChungTuChiTietService,
                                              ISktMucLucService sktMucLucService,
                                              ISktChungTuChiTietCanCuService sktChungTuChiTietCanCuService,
                                              ISktChungTuService sktChungTuService)
        {

            _logger = logger;
            _mapper = mapper;
            _sessionService = sessionService;
            _danhMucService = danhMucService;
            _mucLucNganSachService = mucLucNganSachService;
            _exportService = exportService;

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
            _sktChungTuChiTietService = sktChungTuChiTietService;
            _SktMucLucService = sktMucLucService;
            _iSktChungTuChiTietCanCuService = sktChungTuChiTietCanCuService;
            _sktChungTuService = sktChungTuService;
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
            List<NsSktChungTuModel> sktChungTuModelsSummary = Items.Where(x => x.IIdMaDonVi == _selectedNsSktChungTuModel.IIdMaDonVi && x.ILoai == _selectedNsSktChungTuModel.ILoai && x.Selected).ToList();
            IEnumerable<string> departments = _phongBanService.FindByCondition(x => x.INamLamViec == _sessionInfo.YearOfWork).Select(n => n.IIDMaBQuanLy);
            try
            {
                List<ExportResult> results = new List<ExportResult>();
                string templateFileName;
                string fileNamePrefix;
                string fileNameWithoutExtension;
                int yearOfWork = _sessionInfo.YearOfWork;
                System.Linq.Expressions.Expression<Func<NsCauHinhCanCu, bool>> predicateChcc = PredicateBuilder.True<NsCauHinhCanCu>();
                predicateChcc = predicateChcc.And(item => item.SModule == TypeModuleCanCu.DEMAND);
                predicateChcc = predicateChcc.And(item => item.INamLamViec == yearOfWork);
                IOrderedEnumerable<NsCauHinhCanCu> listCanCu = _iCauHinhCanCuService.FindByCondition(predicateChcc).OrderBy(n => n.INamCanCu);
                ObservableCollection<CauHinhCanCuModel> cauHinhCanCu = _mapper.Map<ObservableCollection<CauHinhCanCuModel>>(listCanCu);
                foreach (NsSktChungTuModel item in sktChungTuModelsSummary)
                {
                    if (LoaiBaoCaoSelected.ValueItem == LoaiBaoCaoUpload.TONG_HOP_VALUE.ToString())
                    {
                        string X1 = "", X2 = "", X3 = "", X4 = "", X5 = "";
                        int count = 0;
                        DonVi currentDonVi = GetNsDonViOfCurrentUser();
                        List<NsSktChungTuChiTietModel> sktChungTuChiTietModels = GetDemandVoucherDetail(item, "0");
                        System.Linq.Expressions.Expression<Func<NsSktMucLuc, bool>> predicate = PredicateBuilder.True<NsSktMucLuc>();
                        predicate = predicate.And(x => x.INamLamViec == yearOfWork);
                        predicate = predicate.And(x => x.ITrangThai == StatusType.ACTIVE);
                        List<NsSktMucLuc> sktMucLucs = _SktMucLucService.FindByCondition(predicate).ToList();
                        (int, string) token = await _hTTPUploadFileService.GetToken(IsSendHTTP);
                        string salt = _cryptographyService.GetSalt();
                        string tokenKey = Scramble(token.Item2 + salt);
                        IOrderedEnumerable<NsSktMucLuc> sktMucLucsOrder = from sktMucLuc in sktMucLucs
                                                                          orderby sktMucLuc.SKyHieu
                                                                          select sktMucLuc;
                        foreach (NsSktChungTuChiTietModel ct in sktChungTuChiTietModels)
                        {
                            NsSktMucLuc ml = sktMucLucsOrder.FirstOrDefault(x => x.IIDMLSKT.Equals(ct.IIdMlskt));
                            if (ml != null)
                            {
                                ct.Nganh = ml.SNg;
                                ct.NganhParent = ml.SNGCha;
                                ct.Stt = ml.SSTT;
                            }
                        }
                        foreach (CauHinhCanCuModel element in cauHinhCanCu)
                        {
                            System.Linq.Expressions.Expression<Func<NsSktChungTuChiTietCanCu, bool>> predicateCc = PredicateBuilder.True<NsSktChungTuChiTietCanCu>();
                            predicateCc = predicateCc.And(x => x.IiIdCtsoKiemTra.Equals(item.Id));
                            predicateCc = predicateCc.And(x => x.IIdCanCu.Equals(element.Id));
                            List<NsSktChungTuChiTietCanCu> lstCanCu = _iSktChungTuChiTietCanCuService.FindByCondition(predicateCc).ToList();

                            if (count == 0)
                            {
                                X1 = element.STenCot;
                            }
                            if (count == 1)
                            {
                                X2 = element.STenCot;
                            }
                            if (count == 2)
                            {
                                X3 = element.STenCot;
                            }
                            if (count == 3)
                            {
                                X4 = element.STenCot;
                            }
                            if (count == 4)
                            {
                                X5 = element.STenCot;
                            }


                            foreach (NsSktChungTuChiTietCanCu cc in lstCanCu)
                            {
                                // var mucLuc = Items.FirstOrDefault(x => x.IIdMlskt.Equals(cc.IIdMlskt));
                                NsSktChungTuChiTietModel mucLuc = sktChungTuChiTietModels.FirstOrDefault(x => x.SKyHieu.Equals(cc.SKyHieu));
                                if (mucLuc != null)
                                {
                                    if (count == 0)
                                    {
                                        // Lay so lieu
                                        mucLuc.X1.SoTien = cc.FTuChi;
                                        mucLuc.X1.SoTienMHHV = cc.FMuaHangCapHienVat;
                                        mucLuc.X1.SoTienDT = cc.FPhanCap;
                                    }

                                    if (count == 1)
                                    {
                                        // Lay so lieu
                                        mucLuc.X2.SoTien = cc.FTuChi;
                                        mucLuc.X2.SoTienMHHV = cc.FMuaHangCapHienVat;
                                        mucLuc.X2.SoTienDT = cc.FPhanCap;
                                    }

                                    if (count == 2)
                                    {
                                        // Lay so lieu
                                        mucLuc.X3.SoTien = cc.FTuChi;
                                        mucLuc.X3.SoTienMHHV = cc.FMuaHangCapHienVat;
                                        mucLuc.X3.SoTienDT = cc.FPhanCap;
                                    }

                                    if (count == 3)
                                    {
                                        // Lay so lieu
                                        mucLuc.X4.SoTien = cc.FTuChi;
                                        mucLuc.X4.SoTienMHHV = cc.FMuaHangCapHienVat;
                                        mucLuc.X4.SoTienDT = cc.FPhanCap;
                                    }

                                    if (count == 4)
                                    {
                                        // Lay so lieu
                                        mucLuc.X5.SoTien = cc.FTuChi;
                                        mucLuc.X5.SoTienMHHV = cc.FMuaHangCapHienVat;
                                        mucLuc.X5.SoTienDT = cc.FPhanCap;
                                    }
                                }
                            }
                            count++;

                        }
                        CalculateData(sktChungTuChiTietModels);
                        sktChungTuChiTietModels = sktChungTuChiTietModels.Where(item => item.X1.SoTien > 0 || item.X2.SoTien > 0 ||
                                                                                item.X3.SoTien > 0 || item.X4.SoTien > 0 || item.X5.SoTien > 0 ||
                                                                                item.FHuyDongTonKho > 0 || item.FTuChi > 0 || item.FMuaHangCapHienVat > 0 ||
                                                                                item.FPhanCap > 0 || item.FTonKhoDenNgay > 0 ||
                                                                                item.X1.SoTienMHHV > 0 || item.X1.SoTienDT > 0 ||
                                                                                item.X2.SoTienMHHV > 0 || item.X2.SoTienDT > 0 ||
                                                                                item.X3.SoTienMHHV > 0 || item.X3.SoTienDT > 0 ||
                                                                                item.X4.SoTienMHHV > 0 || item.X4.SoTienDT > 0 ||
                                                                                item.X5.SoTienMHHV > 0 || item.X5.SoTienDT > 0
                                                                                ).ToList();
                        //NSSD
                        double SumTotalHuyDong = sktChungTuChiTietModels.Where(item => item.IdParent == Guid.Empty).Sum(x => x.FHuyDongTonKho);
                        double SumTotalTuChi = sktChungTuChiTietModels.Where(item => item.IdParent == Guid.Empty).Sum(x => x.FTuChi);
                        double SumTotalTongCongNSSD = sktChungTuChiTietModels.Where(item => item.IdParent == Guid.Empty).Sum(x => x.TongHuyDongTuChi);
                        //NSBD
                        double SumTotalMuaHangHienVat = sktChungTuChiTietModels.Where(item => item.IdParent == Guid.Empty).Sum(x => x.FMuaHangCapHienVat);
                        double SumTotalDacThu = sktChungTuChiTietModels.Where(item => item.IdParent == Guid.Empty).Sum(x => x.FPhanCap);
                        double SumTotalTongCongNSBD = sktChungTuChiTietModels.Where(item => item.IdParent == Guid.Empty).Sum(x => x.TongMuaHangHienVatDacThu);

                        Dictionary<string, object> data = new Dictionary<string, object>();
                        List<int> columnHidden = new List<int>();
                        if (item.ILoaiChungTu == 1)
                        {
                            if (string.IsNullOrEmpty(X1))
                            {
                                columnHidden.Add(5);
                            }
                            if (string.IsNullOrEmpty(X2))
                            {
                                columnHidden.Add(6);
                            }
                            if (string.IsNullOrEmpty(X3))
                            {
                                columnHidden.Add(7);
                            }
                            if (string.IsNullOrEmpty(X4))
                            {
                                columnHidden.Add(8);
                            }
                            if (string.IsNullOrEmpty(X5))
                            {
                                columnHidden.Add(9);
                            }
                        }
                        else
                        {
                            if (string.IsNullOrEmpty(X1))
                            {
                                columnHidden.AddRange(new List<int> { 7, 8 });
                            }
                            if (string.IsNullOrEmpty(X2))
                            {
                                columnHidden.AddRange(new List<int> { 9, 10 });
                            }
                            if (string.IsNullOrEmpty(X3))
                            {
                                columnHidden.AddRange(new List<int> { 11, 12 });
                            }
                            if (string.IsNullOrEmpty(X4))
                            {
                                columnHidden.AddRange(new List<int> { 13, 14 });
                            }
                            if (string.IsNullOrEmpty(X5))
                            {
                                columnHidden.AddRange(new List<int> { 15, 16 });
                            }
                        }
                        data.Add("X1", X1);
                        data.Add("X2", X2);
                        data.Add("X3", X3);
                        data.Add("X4", X4);
                        data.Add("X5", X5);
                        data.Add("SoChungTu", item.SSoChungTu);
                        data.Add("TenDonVi", item.STenDonVi);
                        data.Add("IdDonVi", item.IIdMaDonVi);
                        data.Add("Cap1", currentDonVi.TenDonVi);
                        data.Add("TieuDe1", "BÁO CÁO CHI TIẾT SỐ NHU CẦU NGÂN SÁCH NĂM " + _sessionInfo.YearOfWork);
                        data.Add("h2", "Lữ đoàn X");
                        data.Add("h1", "Lữ đoàn X");
                        data.Add("LoaiChungTu", item.ILoaiChungTu == 1 ? VoucherType.NSSD_Value : VoucherType.NSBD_Value);
                        data.Add("MoTa", item.SMoTa);
                        data.Add("NgayChungTu", item.DNgayChungTu.GetValueOrDefault(DateTime.Now).ToString("dd/MM/yyyy"));
                        data.Add("NguoiTao", item.SNguoiTao);
                        data.Add("NgayTao", item.DNgayTao.GetValueOrDefault(new DateTime()).ToString("dd/MM/yyyy"));
                        data.Add("SumTotalHuyDong", SumTotalHuyDong);
                        data.Add("SumTotalTuChi", SumTotalTuChi);
                        data.Add("SumTotalMHHV", SumTotalMuaHangHienVat);
                        data.Add("SumTotalDT", SumTotalDacThu);
                        data.Add("SumTotalTongCongNSSD", SumTotalTongCongNSSD);
                        data.Add("SumTotalTongCongNSBD", SumTotalTongCongNSBD);
                        data.Add("SumTotalTonKho", sktChungTuChiTietModels.Where(item => item.IdParent == Guid.Empty).Sum(x => x.FTonKhoDenNgay));
                        data.Add("ListData", sktChungTuChiTietModels);
                        data.Add("SKTML", sktMucLucsOrder);
                        data.Add("Count", 10000);
                        data.Add("TonKhoDenNgay", "Tồn kho đến ngày 1/1/" + (_sessionInfo.YearOfWork - 1));
                        //templateFileName = Path.Combine(ExportPrefix.PATH_TL_SKT, ExportFileName.RPT_NS_SOKIEMTRA_CHUNGTU_TONGHOP_NSSD);
                        if (item.ILoaiChungTu == 1)
                        {
                            templateFileName = Path.Combine(ExportPrefix.PATH_TL_SKT, ExportFileName.RPT_NS_SOKIEMTRA_CHUNGTU_TONGHOP_NSSD);
                        }
                        else
                        {
                            templateFileName = Path.Combine(ExportPrefix.PATH_TL_SKT, ExportFileName.RPT_NS_SOKIEMTRA_CHUNGTU_TONGHOP_NSBD);
                        }
                        fileNamePrefix = item.SSoChungTu;
                        fileNameWithoutExtension = String.IsNullOrEmpty(TenBaoCao) ? StringUtils.ConvertVN(StringUtils.CreateExportFileName(fileNamePrefix)) : TenBaoCao;
                        FlexCel.Core.ExcelFile xlsFile = _exportService.HiddenExport<NsSktChungTuChiTietModel, NsSktMucLuc>(templateFileName, data, columnHidden);
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
                            SubModule = item.ILoaiChungTu == 1 ? NSFunctionCode.BUDGET_DEMANDCHECK_DEMAND : NSFunctionCode.BUDGET_DEMANDCHECK_DEMAND_GUARANTEE,
                            SubModuleName = "Nhập số nhu cầu đơn vị",
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
                        foreach (string department in departments)
                        {
                            string X1 = "", X2 = "", X3 = "", X4 = "", X5 = "";
                            int count = 0;
                            DonVi currentDonVi = GetNsDonViOfCurrentUser();
                            List<NsSktChungTuChiTietModel> sktChungTuChiTietModels = GetDemandVoucherDetail(item, department);
                            if (!sktChungTuChiTietModels.Any())
                            {
                                continue;
                            }
                            System.Linq.Expressions.Expression<Func<NsSktMucLuc, bool>> predicate = PredicateBuilder.True<NsSktMucLuc>();
                            predicate = predicate.And(x => x.INamLamViec == yearOfWork);
                            predicate = predicate.And(x => x.ITrangThai == StatusType.ACTIVE);
                            List<NsSktMucLuc> sktMucLucs = _SktMucLucService.FindByCondition(predicate).ToList();
                            (int, string) token = await _hTTPUploadFileService.GetToken(IsSendHTTP);
                            string salt = _cryptographyService.GetSalt();
                            string tokenKey = Scramble(token.Item2 + salt);
                            IOrderedEnumerable<NsSktMucLuc> sktMucLucsOrder = from sktMucLuc in sktMucLucs
                                                                              orderby sktMucLuc.SKyHieu
                                                                              select sktMucLuc;
                            foreach (NsSktChungTuChiTietModel ct in sktChungTuChiTietModels)
                            {
                                NsSktMucLuc ml = sktMucLucsOrder.FirstOrDefault(x => x.IIDMLSKT.Equals(ct.IIdMlskt));
                                if (ml != null)
                                {
                                    ct.Nganh = ml.SNg;
                                    ct.NganhParent = ml.SNGCha;
                                    ct.Stt = ml.SSTT;
                                }
                            }
                            foreach (CauHinhCanCuModel element in cauHinhCanCu)
                            {
                                System.Linq.Expressions.Expression<Func<NsSktChungTuChiTietCanCu, bool>> predicateCc = PredicateBuilder.True<NsSktChungTuChiTietCanCu>();
                                predicateCc = predicateCc.And(x => x.IiIdCtsoKiemTra.Equals(item.Id));
                                predicateCc = predicateCc.And(x => x.IIdCanCu.Equals(element.Id));
                                List<NsSktChungTuChiTietCanCu> lstCanCu = _iSktChungTuChiTietCanCuService.FindByCondition(predicateCc).ToList();

                                if (count == 0)
                                {
                                    X1 = element.STenCot;
                                }
                                if (count == 1)
                                {
                                    X2 = element.STenCot;
                                }
                                if (count == 2)
                                {
                                    X3 = element.STenCot;
                                }
                                if (count == 3)
                                {
                                    X4 = element.STenCot;
                                }
                                if (count == 4)
                                {
                                    X5 = element.STenCot;
                                }


                                foreach (NsSktChungTuChiTietCanCu cc in lstCanCu)
                                {
                                    // var mucLuc = Items.FirstOrDefault(x => x.IIdMlskt.Equals(cc.IIdMlskt));
                                    NsSktChungTuChiTietModel mucLuc = sktChungTuChiTietModels.FirstOrDefault(x => x.SKyHieu.Equals(cc.SKyHieu));
                                    if (mucLuc != null)
                                    {
                                        if (count == 0)
                                        {
                                            // Lay so lieu
                                            mucLuc.X1.SoTien = cc.FTuChi;
                                            mucLuc.X1.SoTienMHHV = cc.FMuaHangCapHienVat;
                                            mucLuc.X1.SoTienDT = cc.FPhanCap;
                                        }

                                        if (count == 1)
                                        {
                                            // Lay so lieu
                                            mucLuc.X2.SoTien = cc.FTuChi;
                                            mucLuc.X2.SoTienMHHV = cc.FMuaHangCapHienVat;
                                            mucLuc.X2.SoTienDT = cc.FPhanCap;
                                        }

                                        if (count == 2)
                                        {
                                            // Lay so lieu
                                            mucLuc.X3.SoTien = cc.FTuChi;
                                            mucLuc.X3.SoTienMHHV = cc.FMuaHangCapHienVat;
                                            mucLuc.X3.SoTienDT = cc.FPhanCap;
                                        }

                                        if (count == 3)
                                        {
                                            // Lay so lieu
                                            mucLuc.X4.SoTien = cc.FTuChi;
                                            mucLuc.X4.SoTienMHHV = cc.FMuaHangCapHienVat;
                                            mucLuc.X4.SoTienDT = cc.FPhanCap;
                                        }

                                        if (count == 4)
                                        {
                                            // Lay so lieu
                                            mucLuc.X5.SoTien = cc.FTuChi;
                                            mucLuc.X5.SoTienMHHV = cc.FMuaHangCapHienVat;
                                            mucLuc.X5.SoTienDT = cc.FPhanCap;
                                        }
                                    }
                                }
                                count++;

                            }
                            CalculateData(sktChungTuChiTietModels);
                            sktChungTuChiTietModels = sktChungTuChiTietModels.Where(item => item.X1.SoTien > 0 || item.X2.SoTien > 0 ||
                                                                                    item.X3.SoTien > 0 || item.X4.SoTien > 0 || item.X5.SoTien > 0 ||
                                                                                    item.FHuyDongTonKho > 0 || item.FTuChi > 0 || item.FMuaHangCapHienVat > 0 ||
                                                                                    item.FPhanCap > 0 || item.FTonKhoDenNgay > 0 ||
                                                                                    item.X1.SoTienMHHV > 0 || item.X1.SoTienDT > 0 ||
                                                                                    item.X2.SoTienMHHV > 0 || item.X2.SoTienDT > 0 ||
                                                                                    item.X3.SoTienMHHV > 0 || item.X3.SoTienDT > 0 ||
                                                                                    item.X4.SoTienMHHV > 0 || item.X4.SoTienDT > 0 ||
                                                                                    item.X5.SoTienMHHV > 0 || item.X5.SoTienDT > 0
                                                                                    ).ToList();
                            //NSSD
                            double SumTotalHuyDong = sktChungTuChiTietModels.Where(item => item.IdParent == Guid.Empty).Sum(x => x.FHuyDongTonKho);
                            double SumTotalTuChi = sktChungTuChiTietModels.Where(item => item.IdParent == Guid.Empty).Sum(x => x.FTuChi);
                            double SumTotalTongCongNSSD = sktChungTuChiTietModels.Where(item => item.IdParent == Guid.Empty).Sum(x => x.TongHuyDongTuChi);
                            //NSBD
                            double SumTotalMuaHangHienVat = sktChungTuChiTietModels.Where(item => item.IdParent == Guid.Empty).Sum(x => x.FMuaHangCapHienVat);
                            double SumTotalDacThu = sktChungTuChiTietModels.Where(item => item.IdParent == Guid.Empty).Sum(x => x.FPhanCap);
                            double SumTotalTongCongNSBD = sktChungTuChiTietModels.Where(item => item.IdParent == Guid.Empty).Sum(x => x.TongMuaHangHienVatDacThu);

                            Dictionary<string, object> data = new Dictionary<string, object>();
                            List<int> columnHidden = new List<int>();
                            if (item.ILoaiChungTu == 1)
                            {
                                if (string.IsNullOrEmpty(X1))
                                {
                                    columnHidden.Add(5);
                                }
                                if (string.IsNullOrEmpty(X2))
                                {
                                    columnHidden.Add(6);
                                }
                                if (string.IsNullOrEmpty(X3))
                                {
                                    columnHidden.Add(7);
                                }
                                if (string.IsNullOrEmpty(X4))
                                {
                                    columnHidden.Add(8);
                                }
                                if (string.IsNullOrEmpty(X5))
                                {
                                    columnHidden.Add(9);
                                }
                            }
                            else
                            {
                                if (string.IsNullOrEmpty(X1))
                                {
                                    columnHidden.AddRange(new List<int> { 7, 8 });
                                }
                                if (string.IsNullOrEmpty(X2))
                                {
                                    columnHidden.AddRange(new List<int> { 9, 10 });
                                }
                                if (string.IsNullOrEmpty(X3))
                                {
                                    columnHidden.AddRange(new List<int> { 11, 12 });
                                }
                                if (string.IsNullOrEmpty(X4))
                                {
                                    columnHidden.AddRange(new List<int> { 13, 14 });
                                }
                                if (string.IsNullOrEmpty(X5))
                                {
                                    columnHidden.AddRange(new List<int> { 15, 16 });
                                }
                            }
                            data.Add("X1", X1);
                            data.Add("X2", X2);
                            data.Add("X3", X3);
                            data.Add("X4", X4);
                            data.Add("X5", X5);
                            data.Add("SoChungTu", item.SSoChungTu);
                            data.Add("TenDonVi", item.STenDonVi);
                            data.Add("IdDonVi", item.IIdMaDonVi);
                            data.Add("Cap1", currentDonVi.TenDonVi);
                            data.Add("TieuDe1", "BÁO CÁO CHI TIẾT SỐ NHU CẦU NGÂN SÁCH NĂM " + _sessionInfo.YearOfWork);
                            data.Add("h2", "Lữ đoàn X");
                            data.Add("h1", "Lữ đoàn X");
                            data.Add("LoaiChungTu", item.ILoaiChungTu == 1 ? VoucherType.NSSD_Value : VoucherType.NSBD_Value);
                            data.Add("MoTa", item.SMoTa);
                            data.Add("NgayChungTu", item.DNgayChungTu.GetValueOrDefault(DateTime.Now).ToString("dd/MM/yyyy"));
                            data.Add("NguoiTao", item.SNguoiTao);
                            data.Add("NgayTao", item.DNgayTao.GetValueOrDefault(new DateTime()).ToString("dd/MM/yyyy"));
                            data.Add("SumTotalHuyDong", SumTotalHuyDong);
                            data.Add("SumTotalTuChi", SumTotalTuChi);
                            data.Add("SumTotalMHHV", SumTotalMuaHangHienVat);
                            data.Add("SumTotalDT", SumTotalDacThu);
                            data.Add("SumTotalTongCongNSSD", SumTotalTongCongNSSD);
                            data.Add("SumTotalTongCongNSBD", SumTotalTongCongNSBD);
                            data.Add("SumTotalTonKho", sktChungTuChiTietModels.Where(item => item.IdParent == Guid.Empty).Sum(x => x.FTonKhoDenNgay));
                            data.Add("ListData", sktChungTuChiTietModels);
                            data.Add("SKTML", sktMucLucsOrder);
                            data.Add("Count", 10000);
                            data.Add("TonKhoDenNgay", "Tồn kho đến ngày 1/1/" + (_sessionInfo.YearOfWork - 1));
                            //templateFileName = Path.Combine(ExportPrefix.PATH_TL_SKT, ExportFileName.RPT_NS_SOKIEMTRA_CHUNGTU_TONGHOP_NSSD);
                            if (item.ILoaiChungTu == 1)
                            {
                                templateFileName = Path.Combine(ExportPrefix.PATH_TL_SKT, ExportFileName.RPT_NS_SOKIEMTRA_CHUNGTU_TONGHOP_NSSD);
                            }
                            else
                            {
                                templateFileName = Path.Combine(ExportPrefix.PATH_TL_SKT, ExportFileName.RPT_NS_SOKIEMTRA_CHUNGTU_TONGHOP_NSBD);
                            }
                            fileNamePrefix = item.SSoChungTu;
                            fileNameWithoutExtension = string.Format("{0}_B{1}", String.IsNullOrEmpty(TenBaoCao) ? StringUtils.ConvertVN(StringUtils.CreateExportFileName(fileNamePrefix)) : TenBaoCao, department);
                            FlexCel.Core.ExcelFile xlsFile = _exportService.HiddenExport<NsSktChungTuChiTietModel, NsSktMucLuc>(templateFileName, data, columnHidden, null, true);
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
                                SubModule = item.ILoaiChungTu == 1 ? NSFunctionCode.BUDGET_DEMANDCHECK_DEMAND : NSFunctionCode.BUDGET_DEMANDCHECK_DEMAND_GUARANTEE,
                                SubModuleName = "Nhập số nhu cầu đơn vị",
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
                NsSktChungTu entity = _sktChungTuService.FindById(sktChungTuModelsSummary.FirstOrDefault().Id);
                entity.DNgaySua = DateTime.Now;
                entity.IsSent = true;
                _sktChungTuService.Update(entity);

                IsLoading = false;
                StringBuilder messageBuilder = new StringBuilder();
                messageBuilder.AppendFormat("Gửi dữ liệu thành công");
                MessageBox.Show(messageBuilder.ToString());
                return;
            }
            catch (Exception ex)
            {
                NsSktChungTu entityError = _sktChungTuService.FindById(sktChungTuModelsSummary.FirstOrDefault().Id);
                entityError.DNgaySua = DateTime.Now;
                entityError.IsSent = false;
                _sktChungTuService.Update(entityError);

                _logger.Error(ex.Message, ex);
                IsLoading = false;
                StringBuilder messageBuilder = new StringBuilder();
                messageBuilder.AppendFormat("Gửi dữ liệu thất bại");
                MessageBox.Show(messageBuilder.ToString());
                return;
            }
        }

        private string Scramble(string s)
        {
            return new string(s.ToCharArray().OrderBy(x => Guid.NewGuid()).ToArray());
        }

        private DonVi GetNsDonViOfCurrentUser()
        {
            int yearOfWork = _sessionInfo.YearOfWork;
            System.Linq.Expressions.Expression<Func<DonVi, bool>> predicate = PredicateBuilder.True<DonVi>();
            predicate = predicate.And(x => x.NamLamViec == yearOfWork && x.Loai == "0");
            DonVi nsDonViOfCurrentUser = _donViService.FindByCondition(predicate).FirstOrDefault();
            return nsDonViOfCurrentUser;
        }

        private List<NsSktChungTuChiTietModel> GetDemandVoucherDetail(NsSktChungTuModel nsSktChungTuModel, string bQuanLy)
        {
            int loaiChungTu = nsSktChungTuModel.ILoaiChungTu.GetValueOrDefault(-1);
            SktChungTuChiTietCriteria searchCondition = new SktChungTuChiTietCriteria();
            searchCondition.NamLamViec = _sessionInfo.YearOfWork;
            searchCondition.NamNganSach = _sessionInfo.YearOfBudget;
            searchCondition.NguonNganSach = _sessionInfo.Budget;
            searchCondition.ITrangThai = StatusType.ACTIVE;
            searchCondition.SktChungTuId = nsSktChungTuModel.Id;
            searchCondition.ILoai = nsSktChungTuModel.ILoai;
            searchCondition.IdDonVi = nsSktChungTuModel.IIdMaDonVi;
            searchCondition.CurrentIdDonVi = _sessionInfo.IdDonVi;
            searchCondition.LoaiChungTu = loaiChungTu;
            searchCondition.UserName = _sessionInfo.Principal;
            IEnumerable<NsSktChungTuChiTiet> temp = _sktChungTuChiTietService.FindByConditionForChildUnit(searchCondition);
            List<NsSktChungTuChiTietModel> lstChungTuChiTietModels = _mapper.Map<List<NsSktChungTuChiTietModel>>(temp);
            if (bQuanLy.Equals("0"))
            {
                CalculateData(lstChungTuChiTietModels);
                lstChungTuChiTietModels = lstChungTuChiTietModels.Where(item => item.FHuyDongTonKho > 0 || item.FTuChi > 0 || item.FMuaHangCapHienVat > 0 || item.FPhanCap > 0 || item.FTonKhoDenNgay > 0).ToList();
                return lstChungTuChiTietModels;
            }

            IEnumerable<Core.Domain.Query.SoKyHieuMucLucNganSachQuery> tempFilter = _sktChungTuChiTietService.FindSoKyHieus(_sessionService.Current.YearOfWork, bQuanLy, nsSktChungTuModel.ILoaiNguonNganSach ?? 0);
            List<SoKyHieuMucLucNganSachModel> soKyHieus = _mapper.Map<List<SoKyHieuMucLucNganSachModel>>(tempFilter);
            IEnumerable<SoKyHieuMucLucNganSachModel> tatCaSoKyHieus = _SktMucLucService.FindByCondition(x => x.INamLamViec == _sessionService.Current.YearOfWork).Select(n => new SoKyHieuMucLucNganSachModel
            {
                sSKT_KyHieu = n.SKyHieu
            });

            List<SoKyHieuMucLucNganSachModel> soKyHieuFilters = tatCaSoKyHieus.Where(n => soKyHieus.Any(m => m.sSKT_KyHieu.StartsWith(n.sSKT_KyHieu))).OrderBy(n => n.sSKT_KyHieu).ToList();

            IEnumerable<DonVi> tempDonVi = _donViService.FindAll().Where(n => n.NamLamViec == _sessionService.Current.YearOfWork);
            List<DonVi> donVis = _mapper.Map<List<DonVi>>(tempDonVi);


            List<NsSktChungTuChiTietModel> nsSktChungTuChiTietQuery = _mapper.Map<List<NsSktChungTuChiTietModel>>(
                                            from lstChungTuChiTietModel in lstChungTuChiTietModels
                                            join soKyHieuFilter in soKyHieuFilters on lstChungTuChiTietModel.SKyHieu equals soKyHieuFilter.sSKT_KyHieu
                                            join donVi in donVis on lstChungTuChiTietModel.IIdMaDonVi equals donVi.IIDMaDonVi
                                            select lstChungTuChiTietModel);
            CalculateData(nsSktChungTuChiTietQuery);
            nsSktChungTuChiTietQuery = nsSktChungTuChiTietQuery.Where(item => item.FHuyDongTonKho > 0 || item.FTuChi > 0 || item.FMuaHangCapHienVat > 0 || item.FPhanCap > 0 || item.FTonKhoDenNgay > 0).ToList();
            return nsSktChungTuChiTietQuery;
        }

        private void CalculateData(List<NsSktChungTuChiTietModel> lstSktChungTuChiTiet)
        {
            lstSktChungTuChiTiet.Where(x => x.IsHangCha)
                .Select(x =>
                {
                    x.FTuChi = 0;
                    x.FHuyDongTonKho = 0;
                    x.FMuaHangCapHienVat = 0;
                    x.FPhanCap = 0;
                    x.FTonKhoDenNgay = 0;
                    x.X1 = new NsSktChungTuChiTietModel.ChiTietCanCu();
                    x.X2 = new NsSktChungTuChiTietModel.ChiTietCanCu();
                    x.X3 = new NsSktChungTuChiTietModel.ChiTietCanCu();
                    x.X4 = new NsSktChungTuChiTietModel.ChiTietCanCu();
                    x.X5 = new NsSktChungTuChiTietModel.ChiTietCanCu();
                    return x;
                }).ToList();
            IEnumerable<NsSktChungTuChiTietModel> temp = lstSktChungTuChiTiet.Where(x => !x.IsHangCha && !x.IsDeleted);
            foreach (NsSktChungTuChiTietModel item in temp)
            {
                CalculateParent(item.IdParent, item, lstSktChungTuChiTiet);
            }

        }

        private void CalculateParent(Guid idParent, NsSktChungTuChiTietModel item, List<NsSktChungTuChiTietModel> lstSktChungTuChiTiet)
        {
            NsSktChungTuChiTietModel model = lstSktChungTuChiTiet.FirstOrDefault(x => x.IIdMlskt == idParent);
            if (model == null) return;
            model.FTuChi += item.FTuChi;
            model.FHuyDongTonKho += item.FHuyDongTonKho;
            model.FMuaHangCapHienVat += item.FMuaHangCapHienVat;
            model.FPhanCap += item.FPhanCap;
            model.FTonKhoDenNgay += item.FTonKhoDenNgay;
            model.X1.SoTien += item.X1.SoTien;
            model.X1.SoTienMHHV += item.X1.SoTienMHHV;
            model.X1.SoTienDT += item.X1.SoTienDT;
            model.X2.SoTien += item.X2.SoTien;
            model.X2.SoTienMHHV += item.X2.SoTienMHHV;
            model.X2.SoTienDT += item.X2.SoTienDT;
            model.X3.SoTien += item.X3.SoTien;
            model.X3.SoTienMHHV += item.X3.SoTienMHHV;
            model.X3.SoTienDT += item.X3.SoTienDT;
            model.X4.SoTien += item.X4.SoTien;
            model.X4.SoTienMHHV += item.X4.SoTienMHHV;
            model.X4.SoTienDT += item.X4.SoTienDT;
            model.X5.SoTien += item.X5.SoTien;
            model.X5.SoTienMHHV += item.X5.SoTienMHHV;
            model.X5.SoTienDT += item.X5.SoTienDT;
            CalculateParent(model.IdParent, item, lstSktChungTuChiTiet);
        }
    }
}
