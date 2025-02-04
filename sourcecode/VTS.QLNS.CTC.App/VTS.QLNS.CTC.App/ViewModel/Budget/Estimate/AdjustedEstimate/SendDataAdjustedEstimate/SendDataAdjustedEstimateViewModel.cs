using AutoMapper;
using ControlzEx.Standard;
using log4net;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using VTS.QLNS.CTC.App.Command;
using VTS.QLNS.CTC.App.Helper;
using VTS.QLNS.CTC.App.Model;
using VTS.QLNS.CTC.App.Model.Control;
using VTS.QLNS.CTC.App.Model.Report;
using VTS.QLNS.CTC.App.Service;
using VTS.QLNS.CTC.App.Service.Impl;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Criteria;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Service;
using VTS.QLNS.CTC.Utility;
using VTS.QLNS.CTC.Utility.Enum;

namespace VTS.QLNS.CTC.App.ViewModel.Budget.Estimate.AdjustedEstimate.SendDataAdjustedEstimate
{
    public class SendDataAdjustedEstimateViewModel : GridViewModelBase<DcChungTuModel>
    {
        private readonly ILog _logger;
        private readonly IMapper _mapper;
        private readonly ISessionService _sessionService;
        private readonly IDanhMucService _danhMucService;
        private readonly INsDcChungTuChiTietService _chungTuChiTietService;
        private readonly INsMucLucNganSachService _mucLucNganSachService;
        private readonly IExportService _exportService;
        private readonly INsPhongBanService _phongBanService;
        private readonly IHTTPUploadFileService _hTTPUploadFileService;
        private readonly ICryptographyService _cryptographyService;
        private readonly INsDcChungTuService _chungTuService;
        public bool IsSendHTTP;
        public string TitleButton => IsSendHTTP ? "Gửi dữ liệu HTTP" : "Gửi dữ liệu FTP";

        private SessionInfo _sessionInfo;
        private List<NsMucLucNganSach> _mucLucNganSaches;
        public delegate void DataChangedEventHandler(object sender, EventArgs e);
        public event DataChangedEventHandler ClosePopup;
        private List<DcChungTuChiTietModel> _adjustedEstimateDetailExports;

        private string _tenBaoCao;
        public string TenBaoCao
        {
            get => _tenBaoCao;
            set => SetProperty(ref _tenBaoCao, value);
        }

        public ObservableCollection<DcChungTuModel> _adjustedEstimateSummaries;
        public ObservableCollection<DcChungTuModel> AdjustedEstimateSummaries
        {
            get => _adjustedEstimateSummaries;
            set => SetProperty(ref _adjustedEstimateSummaries, value);
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
        private bool IsDonViRoot(string iIDMaDonVi) => iIDMaDonVi == _sessionInfo.IdDonVi;
        public SendDataAdjustedEstimateViewModel(ILog logger,
                                              IMapper mapper,
                                              ISessionService sessionService,
                                              IDanhMucService danhMucService,
                                              INsDcChungTuChiTietService chungTuChiTietService,
                                              INsMucLucNganSachService mucLucNganSachService,
                                              IExportService exportService,
                                              INsPhongBanService nsPhongBanService,
                                              IHTTPUploadFileService hTTPUploadFileService,
                                              ICryptographyService cryptographyService,
                                              INsDcChungTuService iNsDcChungTuService)
        {

            _logger = logger;
            _mapper = mapper;
            _sessionService = sessionService;
            _danhMucService = danhMucService;
            _chungTuChiTietService = chungTuChiTietService;
            _mucLucNganSachService = mucLucNganSachService;
            _exportService = exportService;
            _phongBanService = nsPhongBanService;
            _hTTPUploadFileService = hTTPUploadFileService;
            _cryptographyService = cryptographyService;
            _chungTuService = iNsDcChungTuService;

            ExportCommand = new RelayCommand(async obj => await OnUpload());
            CloseWindowCommand = new RelayCommand(obj => OnCloseWindow());

        }

        public void LoadTenBaoCao()
        {
            DcChungTuModel itemImport = _adjustedEstimateSummaries.Where(x => x.Selected && IsDonViRoot(x.IIdMaDonVi)).FirstOrDefault();
            _tenBaoCao = StringUtils.ConvertVN(string.Format("{0}_SNC_{1}", itemImport.STenDonVi, itemImport.SSoChungTu));
        }


        private void LoadTieuChis()
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
        public override void Init()
        {
            base.Init();
            LoadTieuChis();
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

        private async Task OnUpload()
        {
            IsLoading = true;
            List<DcChungTuModel> settlementVouchers = AdjustedEstimateSummaries.Where(x => x.Selected && IsDonViRoot(x.IIdMaDonVi)).ToList();
            IEnumerable<string> departments = _phongBanService.FindByCondition(x => x.INamLamViec == _sessionInfo.YearOfWork).Select(n => n.IIDMaBQuanLy);
            try
            {
                string templateFileName = Path.Combine(ExportPrefix.PATH_TL_DT, ExportFileName.RPT_NS_DUTOAN_DIEUCHINH_TONGHOP);
                string fileNamePrefix;
                string fileNameWithoutExtension;
                (int, string) token = await _hTTPUploadFileService.GetToken(IsSendHTTP);
                string salt = _cryptographyService.GetSalt();
                string tokenKey = Scramble(token.Item2 + salt);
                int count = 0;
                string donVi1 = _danhMucService.FindDonViQuanLy(_sessionService.Current.YearOfWork).ToUpper();
                string donVi2 = _sessionInfo.TenDonVi;

                foreach (DcChungTuModel item in settlementVouchers)
                {
                    foreach (string department in departments)
                    {
                        var lstExportData = GetAdjustedEstimateDetailSummary(item, department);
                        if (!lstExportData.Any())
                        {
                            count++;
                            continue;
                        }
                        CalculateData(lstExportData);
                        lstExportData = lstExportData.Where(x => x.FDuToanNganSachNam != 0
                                                            || x.FDuToanChuyenNamSau != 0
                                                            || x.FDuKienQtDauNam != 0
                                                            || x.FDuKienQtCuoiNam != 0).OrderBy(x => x.SXauNoiMa).ToList();
                        List<NsMucLucNganSach> mucLucNganSaches = _mucLucNganSachService.FindAll(_sessionInfo.YearOfWork).ToList();

                        switch (BTieuChiSelected.ValueItem)
                        {
                            case nameof(MLNSFiled.NG):
                                lstExportData = lstExportData.Where(x => string.IsNullOrEmpty(x.STng)).ToList();
                                lstExportData.Where(x => !string.IsNullOrEmpty(x.SNg)).Select(x => x.IsHangCha = false).ToList();
                                break;
                            case nameof(MLNSFiled.TNG):
                                lstExportData = lstExportData.Where(x => string.IsNullOrEmpty(x.STng1)).ToList();
                                lstExportData.Where(x => !string.IsNullOrEmpty(x.STng)).Select(x => x.IsHangCha = false).ToList();
                                break;
                            case nameof(MLNSFiled.TNG1):
                                lstExportData = lstExportData.Where(x => string.IsNullOrEmpty(x.STng2)).ToList();
                                lstExportData.Where(x => !string.IsNullOrEmpty(x.STng1)).Select(x => x.IsHangCha = false).ToList();
                                break;
                            case nameof(MLNSFiled.TNG2):
                                lstExportData = lstExportData.Where(x => string.IsNullOrEmpty(x.STng3)).ToList();
                                lstExportData.Where(x => !string.IsNullOrEmpty(x.STng2)).Select(x => x.IsHangCha = false).ToList();
                                break;
                        }

                        RptDuToanDieuChinhTongHop ctTongHop = new RptDuToanDieuChinhTongHop
                        {
                            DonVi1 = donVi1,
                            DonVi2 = donVi2,
                            TieuDe1 = "Chứng từ chi tiết",
                            TieuDe2 = "Điều chỉnh số liệu dự toán",
                            ThoiGian = string.Format("Ngày chứng từ: {0}", item.DNgayChungTu.Value.ToString("dd/MM/yyyy")),
                            Items = lstExportData.OrderBy(x => x.SXauNoiMa).ToList(),
                            Count = lstExportData.Count,
                            MLNS = _mucLucNganSaches
                        };
                        if (item.ILoaiDuKien == (int)EstimateSettlementType.SIX_MONTH)
                        {
                            ctTongHop.QtDauNam = 6;
                            ctTongHop.QtCuoiNam = 6;
                        }
                        else
                        {
                            ctTongHop.QtDauNam = 9;
                            ctTongHop.QtCuoiNam = 3;
                        }
                        Dictionary<string, object> data = new Dictionary<string, object>();
                        foreach (System.Reflection.PropertyInfo prop in ctTongHop.GetType().GetProperties())
                        {
                            data.Add(prop.Name, prop.GetValue(ctTongHop));
                        }

                        fileNamePrefix = item.SSoChungTu;
                        fileNameWithoutExtension = StringUtils.CreateExportFileName(fileNamePrefix);
                        FlexCel.Core.ExcelFile xlsFile = _exportService.Export<DcChungTuChiTietModel, NsMucLucNganSach>(templateFileName, data);
                        FlexCel.Core.TXlsNamedRange nameRange = xlsFile.GetNamedRange(1);
                        nameRange.Comment = "Workbook";
                        xlsFile.SetNamedRange(nameRange);
                        xlsFile.SetNamedRange(new FlexCel.Core.TXlsNamedRange("__Area_Items__", 1, 0, "=1:2,A:B"));
                        xlsFile.SetCellValue(50, 50, "CheckSum");
                        xlsFile.SetRowHidden(50, true);
                        ExportResult results = new ExportResult(fileNameWithoutExtension, fileNameWithoutExtension, null, xlsFile);

                        MemoryStream fileStream = new MemoryStream();
                        MemoryStream outputFileStream = new MemoryStream();
                        _exportService.Open(results, ref fileStream);

                        await _cryptographyService.EncryptFile(fileStream, ref outputFileStream, tokenKey);
                        await _hTTPUploadFileService.UploadFile(IsSendHTTP, new FileUploadStreamModel()
                        {
                            File = outputFileStream,
                            Name = fileNameWithoutExtension + FileExtensionFormats.Security,
                            Description = "Chứng từ tổng hợp",
                            Module = NSFunctionCode.BUDGET,
                            ModuleName = "Ngân sách",
                            SubModule = NSFunctionCode.BUDGET_ESTIMATE_ADJUSTED_ESTIMATE,
                            SubModuleName = "Điều chỉnh dự toán",
                            TokenKey = tokenKey,
                            YearOfWork = _sessionInfo.YearOfWork,
                            YearOfBudget = _sessionInfo.YearOfBudget,
                            SourceOfBudget = _sessionInfo.Budget,
                            Department = department,
                            Quarter = ""
                        });
                    }

                    if (departments.Count() == count)
                    {
                        List<NsDcChungTu> listCTError = _chungTuService.FindByCondition(n => settlementVouchers.Select(x => x.Id).Contains(n.Id)).ToList();
                        foreach (NsDcChungTu i in listCTError)
                        {
                            i.IsSent = false;
                            i.DNgaySua = DateTime.Now;
                        }
                        _chungTuService.UpdateRange(listCTError);
                        IsLoading = false;
                        MessageBox.Show(new StringBuilder("Không có dữ liệu gửi").ToString());
                        return;
                    }

                    List<NsDcChungTu> listCTUpdate = _chungTuService.FindByCondition(n => settlementVouchers.Select(x => x.Id).Contains(n.Id)).ToList();
                    foreach (NsDcChungTu i in listCTUpdate)
                    {
                        i.IsSent = true;
                        i.DNgaySua = DateTime.Now;
                    }
                    _chungTuService.UpdateRange(listCTUpdate);

                    IsLoading = false;
                    MessageBox.Show(new StringBuilder("Gửi dữ liệu thành công").ToString());

                }
                return;
            }
            catch (Exception ex)
            {               
                IsLoading = false;
                MessageBox.Show(new StringBuilder("Gửi dữ liệu thất bại").ToString());
                _logger.Error(ex.Message, ex);
                return;
            }
        }

        private List<DcChungTuChiTietModel> GetAdjustedEstimateDetailSummary(DcChungTuModel model, string department)
        {
            IEnumerable<string> listMlns = _mucLucNganSaches.Where(n => n.IdPhongBan == department).Select(n => n.Lns);
            var slns = listMlns.Where(x => model.SDslns.Split(',').ToList().Contains(x.ToString())).ToList();
            EstimationVoucherDetailCriteria searchCondition = new EstimationVoucherDetailCriteria
            {
                VoucherId = model.Id,
                LNS = string.Join(",", slns),
                YearOfWork = model.INamLamViec,
                YearOfBudget = model.INamNganSach,
                BudgetSource = model.IIdMaNguonNganSach,
                LoaiDuKien = model.ILoaiDuKien,
                LoaiChungTu = model.ILoaiChungTu,
                VoucherDate = model.DNgayChungTu,
                UserName = _sessionInfo.Principal
            };

            searchCondition.IdDonVi = _sessionInfo.IdDonVi;

            IEnumerable<NsDcChungTuChiTietQuery> listChungTuChiTiet = _chungTuChiTietService.FindByConditionTongSo(searchCondition);
            return _mapper.Map<List<DcChungTuChiTietModel>>(listChungTuChiTiet);
        }

        private void CalculateData(List<DcChungTuChiTietModel> lstExportVouchers)
        {
            lstExportVouchers.Where(x => x.IsHangCha)
                .Select(x =>
                {
                    x.FDuToanNganSachNam = 0;
                    x.FDuToanChuyenNamSau = 0;
                    x.FDuKienQtDauNam = 0;
                    x.FDuKienQtCuoiNam = 0;
                    return x;
                }).ToList();

            foreach (DcChungTuChiTietModel item in lstExportVouchers.Where(x => x.FDuToanNganSachNam != 0
                            || x.FDuToanChuyenNamSau != 0
                            || x.FDuKienQtDauNam != 0
                            || x.FDuKienQtCuoiNam != 0))
            {
                CalculateParent(item, item, lstExportVouchers);
            }
        }

        private void CalculateParent(DcChungTuChiTietModel currentItem, DcChungTuChiTietModel seftItem, List<DcChungTuChiTietModel> lstExportVouchers)
        {
            DcChungTuChiTietModel parrentItem = lstExportVouchers.Where(x => x.IIdMlns == currentItem.IIdMlnsCha).FirstOrDefault();
            if (parrentItem == null) return;
            parrentItem.FDuToanNganSachNam += seftItem.FDuToanNganSachNam;
            parrentItem.FDuToanChuyenNamSau += seftItem.FDuToanChuyenNamSau;
            parrentItem.FDuKienQtDauNam += seftItem.FDuKienQtDauNam;
            parrentItem.FDuKienQtCuoiNam += seftItem.FDuKienQtCuoiNam;
            CalculateParent(parrentItem, seftItem, lstExportVouchers);
        }

        private string Scramble(string s)
        {
            return new string(s.ToCharArray().OrderBy(x => Guid.NewGuid()).ToArray());
        }
    }
}
