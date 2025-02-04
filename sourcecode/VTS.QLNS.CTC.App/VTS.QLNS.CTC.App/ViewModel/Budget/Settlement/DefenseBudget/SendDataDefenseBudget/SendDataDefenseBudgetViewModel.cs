using AutoMapper;
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

namespace VTS.QLNS.CTC.App.ViewModel.Budget.Settlement.DefenseBudget.SendDataDefenseBudget
{
    public class SendDataDefenseBudgetViewModel : DialogViewModelBase<SettlementVoucherModel>
    {
        private readonly ILog _logger;
        private readonly IMapper _mapper;
        private readonly ISessionService _sessionService;
        private readonly IDanhMucService _danhMucService;
        private readonly INsDonViService _donViService;
        private readonly INsQtChungTuChiTietService _chungTuChiTietService;
        private readonly INsMucLucNganSachService _mucLucNganSachService;
        private readonly IExportService _exportService;
        private readonly INsPhongBanService _phongBanService;
        private readonly IHTTPUploadFileService _hTTPUploadFileService;
        private readonly ICryptographyService _cryptographyService;
        private readonly INsQtChungTuService _chungTuService;
        public bool IsSendHTTP;
        public string TitleButton => IsSendHTTP ? "Gửi dữ liệu HTTP" : "Gửi dữ liệu FTP";

        public delegate void DataChangedEventHandler(object sender, EventArgs e);
        public event DataChangedEventHandler ClosePopup;
        private SessionInfo _sessionInfo;
        private List<SettlementVoucherDetailModel> _settlementVoucherDetailExports;
        private List<NsMucLucNganSach> _mucLucNganSaches;
        public ObservableCollection<SettlementVoucherModel> _settlementVouchers;
        public ObservableCollection<SettlementVoucherModel> _settlementVoucherSummaries;
        public ObservableCollection<SettlementVoucherModel> SettlementVoucherSummaries
        {
            get => _settlementVoucherSummaries;
            set => SetProperty(ref _settlementVoucherSummaries, value);
        }
        private ObservableCollection<ComboboxItem> _bTieuChiItems = new ObservableCollection<ComboboxItem>();
        public override Type ContentType => typeof(View.Budget.Settlement.DefenseBudget.SendDataDefenseBudget.SendDataDefenseBudget);
        public ObservableCollection<ComboboxItem> BTieuChiItems
        {
            get => _bTieuChiItems;
            set => SetProperty(ref _bTieuChiItems, value);
        }

        private string _tenBaoCao;
        public string TenBaoCao
        {
            get => _tenBaoCao;
            set => SetProperty(ref _tenBaoCao, value);
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
            List<SettlementVoucherModel> items = _settlementVoucherSummaries.Where(x => x.Selected && IsDonViRoot(x.IIdMaDonVi)).ToList();
            if (items.Count > 1)
            {
                string sThangQuy = "";
                if (items.FirstOrDefault().IThangQuy >= 1 && items.FirstOrDefault().IThangQuy <= 3)
                {
                    sThangQuy = "Quy 1";
                }
                else if (items.FirstOrDefault().IThangQuy >= 4 && items.FirstOrDefault().IThangQuy <= 6)
                {
                    sThangQuy = "Quy 2";
                }
                else if (items.FirstOrDefault().IThangQuy >= 7 && items.FirstOrDefault().IThangQuy <= 9)
                {
                    sThangQuy = "Quy 3";
                }
                else if (items.FirstOrDefault().IThangQuy >= 10 && items.FirstOrDefault().IThangQuy <= 12)
                {
                    sThangQuy = "Quy 4";
                }
                else
                {
                    sThangQuy = "";
                }
                _tenBaoCao = StringUtils.ConvertVN(string.Format("{0}_QTNSQP_{1}_{2}", items.FirstOrDefault().STenDonVi, items.FirstOrDefault().SSoChungTu, sThangQuy));
            }
            else
            {
                string sThangQuy = (items.FirstOrDefault().IThangQuyLoai == (int)QuarterMonth.MONTH) ? string.Format("Thang {0}", items.FirstOrDefault().IThangQuy.ToString()) : string.Format("Quy {0}", (items.FirstOrDefault().IThangQuy / 3).ToString());
                _tenBaoCao = StringUtils.ConvertVN(string.Format("{0}_QTNSQP_{1}_{2}", items.FirstOrDefault().STenDonVi, items.FirstOrDefault().SSoChungTu, sThangQuy));
            }

            //var settlementVoucher = _settlementVoucherSummaries.FirstOrDefault(x => x.Selected && IsDonViRoot(x.IIdMaDonVi));
            //var sThangQuy = (settlementVoucher.IThangQuyLoai == (int)QuarterMonth.MONTH) ? string.Format("Thang {0}", settlementVoucher.IThangQuy.ToString()) : string.Format("Quy {0}", (settlementVoucher.IThangQuy / 3).ToString());
            //_tenBaoCao = StringUtils.ConvertVN(string.Format("{0}_QTNSQP_{1}_{2}", settlementVoucher.STenDonVi, settlementVoucher.SSoChungTu, sThangQuy));
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

        private bool IsDonViRoot(string iIDMaDonVi) => iIDMaDonVi == _sessionInfo.IdDonVi;

        public SendDataDefenseBudgetViewModel(ILog logger,
                                              IMapper mapper,
                                              ISessionService sessionService,
                                              IDanhMucService danhMucService,
                                              INsDonViService donViService,
                                              INsQtChungTuChiTietService chungTuChiTietService,
                                              INsMucLucNganSachService mucLucNganSachService,
                                              IExportService exportService,
                                              INsPhongBanService nsPhongBanService,
                                              IHTTPUploadFileService hTTPUploadFileService,
                                              ICryptographyService cryptographyService,
                                              INsQtChungTuService chungTuService)
        {

            _logger = logger;
            _mapper = mapper;
            _sessionService = sessionService;
            _danhMucService = danhMucService;
            _donViService = donViService;
            _chungTuChiTietService = chungTuChiTietService;
            _mucLucNganSachService = mucLucNganSachService;
            _exportService = exportService;
            _phongBanService = nsPhongBanService;
            _hTTPUploadFileService = hTTPUploadFileService;
            _cryptographyService = cryptographyService;
            _chungTuService = chungTuService;

            ExportCommand = new RelayCommand(async obj => await OnUpload());
            CloseWindowCommand = new RelayCommand(obj => OnCloseWindow());
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
            IEnumerable<string> departments = _phongBanService.FindByCondition(x => x.INamLamViec == _sessionInfo.YearOfWork).Select(n => n.IIDMaBQuanLy);
            List<SettlementVoucherModel> settlementVouchers = _settlementVoucherSummaries.Where(x => x.Selected && !string.IsNullOrEmpty(x.STongHop)).ToList();
            try
            {
                string templateFileName = Path.Combine(ExportPrefix.PATH_TL_QUYETTOAN, ExportFileName.RPT_NS_QUYETTOAN_CHUNGTU_TONGHOP);
                string fileNamePrefix;
                string fileNameWithoutExtension;

                string donVi1 = _danhMucService.FindDonViQuanLy(_sessionService.Current.YearOfWork).ToUpper();
                string donVi2 = _sessionInfo.TenDonVi;
                //var item = settlementVouchers.FirstOrDefault(x => x.Selected);
                (int, string) token = await _hTTPUploadFileService.GetToken(IsSendHTTP);
                string salt = _cryptographyService.GetSalt();
                string tokenKey = Scramble(token.Item2 + salt);
                int count = 0;
                string quarterUpload = "";
                foreach (string department in departments)
                {
                    _settlementVoucherDetailExports = GetSettlementVoucherDetailDepartment(settlementVouchers, department);
                    if (!_settlementVoucherDetailExports.Any())
                    {
                        count++;
                        continue;
                    }
                    if (settlementVouchers.Count() > 1)
                    {
                        List<SettlementVoucherDetailModel> dataParentGroups = _settlementVoucherDetailExports.Where(x => x.BHangCha).GroupBy(g => new { g.SXauNoiMa, g.SMoTa }).Select(s => s.First()).ToList();
                        IEnumerable<SettlementVoucherDetailModel> dataChildGroups = _settlementVoucherDetailExports.Where(x => !x.BHangCha).GroupBy(g => new { g.IIdMlns, g.IIdMlnsCha, g.SXauNoiMa, g.SLns, g.SL, g.SK, g.SM, g.STm, g.STtm, g.SNg, g.STng, g.STng1, g.STng2, g.STng3, g.SMoTa, g.IsHangCha }).Select(x => new SettlementVoucherDetailModel
                        {
                            IIdMlns = x.Key.IIdMlns,
                            IIdMlnsCha = x.Key.IIdMlnsCha,
                            SXauNoiMa = x.Key.SXauNoiMa,
                            SLns = x.Key.SLns,
                            SL = x.Key.SL,
                            SK = x.Key.SK,
                            SM = x.Key.SM,
                            STm = x.Key.STm,
                            STtm = x.Key.STtm,
                            SNg = x.Key.SNg,
                            STng = x.Key.STng,
                            STng1 = x.Key.STng1,
                            STng2 = x.Key.STng2,
                            STng3 = x.Key.STng3,
                            SMoTa = x.Key.SMoTa,
                            FDuToan = x.Sum(x => x.FDuToan),
                            FDaQuyetToan = x.Sum(x => x.FDaQuyetToan),
                            FSoNgay = x.Sum(x => x.FSoNgay),
                            FSoNguoi = x.Sum(x => x.FSoNguoi),
                            FSoLuot = x.Sum(x => x.FSoLuot),
                            FTuChiDeNghi = x.Sum(x => x.FTuChiDeNghi),
                            FTuChiPheDuyet = x.Sum(x => x.FTuChiPheDuyet),
                            FDuToanOrigin = x.Sum(x => x.FDuToanOrigin),
                            SGhiChu = x.First().SGhiChu,
                            IIdMaDonVi = x.First().IIdMaDonVi,
                            STenDonVi = x.First().STenDonVi,
                            IThangQuy = x.First().IThangQuy,
                            SChiTietToi = x.First().SChiTietToi,

                        });
                        List<SettlementVoucherDetailModel> dataGroups = dataParentGroups;
                        dataGroups.AddRange(dataChildGroups);
                        _settlementVoucherDetailExports = dataGroups.OrderBy(s => s.SXauNoiMa).ToList();
                    }
                    CalculateData();
                    _settlementVoucherDetailExports = _settlementVoucherDetailExports.Where(x => x.FDuToan != 0 || x.FDaQuyetToan != 0
                                                                    || x.FTuChiDeNghi != 0 || x.FTuChiPheDuyet != 0).OrderBy(x => x.SXauNoiMa).ToList();
                    _settlementVoucherDetailExports.ForEach(x => x.FTuChiDeNghi = x.FTuChiPheDuyet);

                    switch (BTieuChiSelected.ValueItem)
                    {
                        case nameof(MLNSFiled.NG):
                            _settlementVoucherDetailExports = _settlementVoucherDetailExports.Where(x => string.IsNullOrEmpty(x.STng)).ToList();
                            _settlementVoucherDetailExports.Where(x => !string.IsNullOrEmpty(x.SNg)).Select(x => x.IsHangCha = false).ToList();
                            break;
                        case nameof(MLNSFiled.TNG):
                            _settlementVoucherDetailExports = _settlementVoucherDetailExports.Where(x => string.IsNullOrEmpty(x.STng1)).ToList();
                            _settlementVoucherDetailExports.Where(x => !string.IsNullOrEmpty(x.STng)).Select(x => x.IsHangCha = false).ToList();
                            break;
                        case nameof(MLNSFiled.TNG1):
                            _settlementVoucherDetailExports = _settlementVoucherDetailExports.Where(x => string.IsNullOrEmpty(x.STng2)).ToList();
                            _settlementVoucherDetailExports.Where(x => !string.IsNullOrEmpty(x.STng1)).Select(x => x.IsHangCha = false).ToList();
                            break;
                        case nameof(MLNSFiled.TNG2):
                            _settlementVoucherDetailExports = _settlementVoucherDetailExports.Where(x => string.IsNullOrEmpty(x.STng3)).ToList();
                            _settlementVoucherDetailExports.Where(x => !string.IsNullOrEmpty(x.STng2)).Select(x => x.IsHangCha = false).ToList();
                            break;
                    }
                    RptQuyetToanChungTuTongHop ctTongHop = new RptQuyetToanChungTuTongHop
                    {
                        DonVi1 = donVi1,
                        DonVi2 = donVi2,
                        TieuDe1 = "Chứng từ tổng hợp",
                        TieuDe2 = "Quyết toán ngân sách quốc phòng",
                        ThoiGian = string.Format("Ngày chứng từ: {0}", settlementVouchers.FirstOrDefault().DNgayChungTu.ToString("dd/MM/yyyy")),
                        Items = _settlementVoucherDetailExports,
                        MLNS = _mucLucNganSaches
                    };
                    Dictionary<string, object> data = new Dictionary<string, object>();
                    foreach (System.Reflection.PropertyInfo prop in ctTongHop.GetType().GetProperties())
                    {
                        data.Add(prop.Name, prop.GetValue(ctTongHop));
                    }

                    fileNamePrefix = $"{settlementVouchers.FirstOrDefault().SSoChungTu}_{StringUtils.ConvertVN(settlementVouchers.FirstOrDefault().STenDonVi)}";
                    fileNameWithoutExtension = string.Format("{0}_B{1}", String.IsNullOrEmpty(TenBaoCao) ? StringUtils.ConvertVN(StringUtils.CreateExportFileName(fileNamePrefix)) : TenBaoCao, department);
                    FlexCel.Core.ExcelFile xlsFile = _exportService.Export<SettlementVoucherDetailModel, NsMucLucNganSach>(templateFileName, data);
                    ExportResult Result = new ExportResult(fileNameWithoutExtension, fileNameWithoutExtension, null, xlsFile);

                    //var sStage = "thang-" + item.IThangQuy;
                    string sMaDonVi = $"{settlementVouchers.FirstOrDefault().IIdMaDonVi}-{StringUtils.UCS2Convert(settlementVouchers.FirstOrDefault().STenDonVi)}";

                    MemoryStream fileStream = new MemoryStream();
                    MemoryStream outputFileStream = new MemoryStream();
                    _exportService.Open(Result, ref fileStream);

                    if (settlementVouchers.Count > 1)
                    {
                        quarterUpload = GetQuarter(settlementVouchers.FirstOrDefault().IThangQuy);
                    }
                    else
                    {
                        quarterUpload = (settlementVouchers.FirstOrDefault().IThangQuyLoai == (int)QuarterMonth.MONTH) ? settlementVouchers.FirstOrDefault().IThangQuy.ToString() : string.Join(",", CreateThangFromQuy(settlementVouchers.FirstOrDefault().IThangQuy));
                    }

                    await _cryptographyService.EncryptFile(fileStream, ref outputFileStream, tokenKey);
                    await _hTTPUploadFileService.UploadFile(IsSendHTTP, new FileUploadStreamModel()
                    {
                        File = outputFileStream,
                        Name = fileNameWithoutExtension + FileExtensionFormats.Security,
                        Description = "Chứng từ tổng hợp",
                        Module = NSFunctionCode.BUDGET,
                        ModuleName = "Ngân sách",
                        SubModule = NSFunctionCode.BUDGET_SETTLEMENT_DEFENSE_BUDGET,
                        SubModuleName = "Quyết toán ngân sách quốc phòng",
                        TokenKey = tokenKey,
                        YearOfWork = _sessionInfo.YearOfWork,
                        YearOfBudget = _sessionInfo.YearOfBudget,
                        SourceOfBudget = _sessionInfo.Budget,
                        Department = department,
                        Quarter = quarterUpload
                    });
                }

                if (departments.Count() == count)
                {
                    List<NsQtChungTu> listCTError = _chungTuService.FindByCondition(n => settlementVouchers.Select(x => x.Id).Contains(n.Id)).ToList();
                    foreach (NsQtChungTu item in listCTError)
                    {
                        item.IsSent = false;
                        item.DNgaySua = DateTime.Now;
                    }
                    _chungTuService.UpdateRange(listCTError);
                    IsLoading = false;
                    MessageBox.Show(new StringBuilder("Không có dữ liệu gửi").ToString());
                    return;
                }

                List<NsQtChungTu> listCTUpdate = _chungTuService.FindByCondition(n => settlementVouchers.Select(x => x.Id).Contains(n.Id)).ToList();
                foreach (NsQtChungTu item in listCTUpdate)
                {
                    item.IsSent = true;
                    item.DNgaySua = DateTime.Now;
                }
                _chungTuService.UpdateRange(listCTUpdate);
                IsLoading = false;
                MessageBox.Show(new StringBuilder("Gửi dữ liệu thành công").ToString());
                return;
            }
            catch (Exception ex)
            {
                List<NsQtChungTu> listCTError = _chungTuService.FindByCondition(n => settlementVouchers.Select(x => x.Id).Contains(n.Id)).ToList();
                foreach (NsQtChungTu item in listCTError)
                {
                    item.IsSent = false;
                    item.DNgaySua = DateTime.Now;
                }
                _chungTuService.UpdateRange(listCTError);
                IsLoading = false;

                if (ex is System.Configuration.ConfigurationErrorsException)
                {
                    MessageBox.Show(new StringBuilder("Cấu hình thông tin chưa đúng").ToString());
                }
                MessageBox.Show(new StringBuilder("Gửi dữ liệu thất bại").ToString());
                _logger.Error(ex.Message, ex);
                return;
            }
        }

        private string GetQuarter(int thang)
        {
            if (thang >= 1 && thang <= 3)
            {
                return "1,2,3";
            }
            else if (thang >= 4 && thang <= 6)
            {
                return "4,5,6";
            }
            else if (thang >= 7 && thang <= 9)
            {
                return "7,8,9";
            }
            else if (thang >= 10 && thang <= 12)
            {
                return "10,11,12";
            }
            else
            {
                return "";
            }
        }

        private IEnumerable<int> CreateThangFromQuy(int quy)
        {
            int i = 3;
            while (i > 0)
            {
                yield return quy + 1 - (i--);
            }
        }


        private string Scramble(string s)
        {
            return new string(s.ToCharArray().OrderBy(x => Guid.NewGuid()).ToArray());
        }

        //private List<SettlementVoucherDetailModel> GetSettlementVoucherDetailDepartment(SettlementVoucherModel settlementVoucher, string department)
        //{
        //    var listMlns = _mucLucNganSaches.Where(n => n.IdPhongBan == department).Select(n => n.Lns);
        //    var listLns = settlementVoucher.SDslns.Split(StringUtils.COMMA).Where(x => listMlns.Contains(x));
        //    SettlementVoucherDetailSearch searchCondition = new SettlementVoucherDetailSearch
        //    {
        //        VoucherId = settlementVoucher.Id,
        //        LNS = string.Join(",", listLns),
        //        YearOfWork = _sessionService.Current.YearOfWork,
        //        YearOfBudget = _sessionService.Current.YearOfBudget,
        //        Type = settlementVoucher.SLoai,
        //        BudgetSource = 1,
        //        AgencyId = settlementVoucher.IIdMaDonVi,
        //        VoucherDate = settlementVoucher.DNgayChungTu,
        //        UserName = _sessionInfo.Principal,
        //        QuarterMonth = settlementVoucher.IThangQuy.ToString(),
        //    };
        //    var listChungTuChiTiet = _chungTuChiTietService.FindByCondition(searchCondition);
        //    return _mapper.Map<List<SettlementVoucherDetailModel>>(listChungTuChiTiet);
        //}

        private List<SettlementVoucherDetailModel> GetSettlementVoucherDetailDepartment(List<SettlementVoucherModel> listSettlementVoucher, string department)
        {
            IEnumerable<string> listMlns = _mucLucNganSaches.Where(n => n.IdPhongBan == department).Select(n => n.Lns);
            IEnumerable<string> listLns = listSettlementVoucher.Select(x => x.SDslns).SelectMany(x => x.Split(StringUtils.COMMA)).Where(x => listMlns.Contains(x)).Distinct();
            IEnumerable<int> listQuarterMonth = listSettlementVoucher.Select(x => x.IThangQuy);
            List<QtChungTuChiTietQuery> listChungTuChiTiet = new List<QtChungTuChiTietQuery>();
            foreach (SettlementVoucherModel item in listSettlementVoucher)
            {
                SettlementVoucherDetailSearch searchCondition = new SettlementVoucherDetailSearch
                {
                    VoucherId = item.Id,
                    LNS = string.Join(",", listLns),
                    YearOfWork = _sessionInfo.YearOfWork,
                    YearOfBudget = _sessionInfo.YearOfBudget,
                    Type = listSettlementVoucher.FirstOrDefault().SLoai,
                    BudgetSource = _sessionInfo.Budget,
                    AgencyId = item.IIdMaDonVi,
                    VoucherDate = item.DNgayChungTu,
                    UserName = _sessionInfo.Principal,
                    QuarterMonth = listQuarterMonth.Max().ToString(),
                };
                List<QtChungTuChiTietQuery> listChungTuChiTietItem = _chungTuChiTietService.FindByCondition(searchCondition);

                if (listSettlementVoucher.Count() > 1)
                {
                    List<QtChungTuChiTietQuery> listRemove = listChungTuChiTietItem.Where(w => !w.BHangCha && !(w.IThangQuy >= listQuarterMonth.Min() && w.IThangQuy <= listQuarterMonth.Max() && w.IIdMaDonVi == item.IIdMaDonVi)).ToList();
                    listChungTuChiTietItem = listChungTuChiTietItem.Except(listRemove).ToList();
                    listChungTuChiTiet.AddRange(listChungTuChiTietItem);
                }
                else
                {
                    List<QtChungTuChiTietQuery> listRemove = listChungTuChiTietItem.Where(w => !w.BHangCha && w.IThangQuy != listQuarterMonth.Max()).ToList();
                    listChungTuChiTietItem = listChungTuChiTietItem.Except(listRemove).ToList();
                    listChungTuChiTiet.AddRange(listChungTuChiTietItem);
                }
            }
            return _mapper.Map<List<SettlementVoucherDetailModel>>(listChungTuChiTiet);
        }

        private void CalculateData()
        {
            // Reset value parrent
            _settlementVoucherDetailExports.Where(x => x.IsHangCha)
                .Select(x => { x.FDuToan = x.FDuToanOrigin != 0 ? x.FDuToanOrigin : 0; x.FDaQuyetToan = 0; x.FTuChiDeNghi = 0; x.FTuChiPheDuyet = 0; return x; }).ToList();
            // Caculate value child
            foreach (SettlementVoucherDetailModel item in _settlementVoucherDetailExports.Where(x => x.FDuToanOrigin != 0 || (x.IsEditable && (x.FDaQuyetToan != 0 || x.FTuChiPheDuyet != 0 || x.FTuChiDeNghi != 0))))
            {
                CalculateParent(item, item);
            }
        }

        private void CalculateParent(SettlementVoucherDetailModel currentItem, SettlementVoucherDetailModel selfItem)
        {
            SettlementVoucherDetailModel parentItem = _settlementVoucherDetailExports.Where(x => x.IIdMlns == currentItem.IIdMlnsCha).FirstOrDefault();
            if (parentItem == null) return;
            if (selfItem.FDuToanOrigin != 0)
                parentItem.FDuToan += selfItem.FDuToan;
            if (parentItem.FDuToan != 0 && currentItem.FDuToan == 0)
            {
                currentItem.IsCalculateConLai = false;
                OnPropertyChanged(nameof(currentItem.FConLai));
            }
            parentItem.FDaQuyetToan += selfItem.FDaQuyetToan;
            parentItem.FTuChiDeNghi += selfItem.FTuChiDeNghi;
            parentItem.FTuChiPheDuyet += selfItem.FTuChiPheDuyet;
            CalculateParent(parentItem, selfItem);
        }

    }
}
