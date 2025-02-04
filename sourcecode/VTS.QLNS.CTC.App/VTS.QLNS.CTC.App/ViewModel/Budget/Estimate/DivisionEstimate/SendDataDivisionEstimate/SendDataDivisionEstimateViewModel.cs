using AutoMapper;
using log4net;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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

namespace VTS.QLNS.CTC.App.ViewModel.Budget.Estimate.DivisionEstimate.SendDataDivisionEstimate
{
    public class SendDataDivisionEstimateViewModel : DialogViewModelBase<DtChungTuModel>
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
        private readonly INsDtNhanPhanBoMapService _dtChungTuMapService;
        private readonly INsDtChungTuChiTietService _dtChungTuChiTietService;
        private readonly INsDtChungTuService _estimationService;

        private SessionInfo _sessionInfo;
        private readonly List<SettlementVoucherDetailModel> _settlementVoucherDetailExports;
        private List<NsMucLucNganSach> _mucLucNganSaches;
        public ObservableCollection<DtChungTuModel> Items;
        public List<DtChungTuChiTietModel> _listChungTuChiTiet;
        public bool IsSendHTTP;
        public string TitleButton => IsSendHTTP ? "Gửi dữ liệu HTTP" : "Gửi dữ liệu FTP";

        public ObservableCollection<SettlementVoucherModel> _settlementVouchers;
        public ObservableCollection<SettlementVoucherModel> _settlementVoucherSummaries;
        public ObservableCollection<SettlementVoucherModel> SettlementVoucherSummaries
        {
            get => _settlementVoucherSummaries;
            set => SetProperty(ref _settlementVoucherSummaries, value);
        }
        private string _tenBaoCao;
        public string TenBaoCao
        {
            get => _tenBaoCao;
            set => SetProperty(ref _tenBaoCao, value);
        }
        private ObservableCollection<ComboboxItem> _bTieuChiItems = new ObservableCollection<ComboboxItem>();
        public override Type ContentType => typeof(View.Budget.Settlement.RegularBudget.SendDataRegularBudget.SendDataRegularBudget);

        public List<UserAPIModel> _listChildAgency;
        public List<UserAPIModel> ListChildAgency
        {
            get => _listChildAgency;
            set => SetProperty(ref _listChildAgency, value);
        }

        private bool _selectAllAgency;
        public bool SelectAllAgency
        {
            get => ListChildAgency != null && ListChildAgency.Any() && ListChildAgency.All(item => item.IsChecked);
            set
            {
                SetProperty(ref _selectAllAgency, value);
                if (ListChildAgency != null)
                {
                    ListChildAgency.Select(c => { c.IsChecked = _selectAllAgency; return c; }).ToList();
                }
            }
        }

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

        public void LoadTenBaoCao()
        {
            DtChungTuModel itemsExport = Items.FirstOrDefault(x => x.Selected);
            _tenBaoCao = StringUtils.ConvertVN(string.Format("{0}_{1}", itemsExport.SSoChungTu, itemsExport.SSoQuyetDinh));
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

        public SendDataDivisionEstimateViewModel(ILog logger,
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
                                              INsDtNhanPhanBoMapService dtChungTuMapService,
                                              INsDtChungTuChiTietService dtChungTuChiTietService,
                                              INsDtChungTuService estimationService)
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
            _dtChungTuMapService = dtChungTuMapService;
            _dtChungTuChiTietService = dtChungTuChiTietService;
            _estimationService = estimationService;

            ExportCommand = new RelayCommand(obj => OnUpload());

        }

        public override void Init()
        {
            base.Init();
            LoadTieuChis();
            _sessionInfo = _sessionService.Current;
            _mucLucNganSaches = _mucLucNganSachService.FindAll(_sessionInfo.YearOfWork).ToList();
            LoadTenBaoCao();
            LoadData();
        }

        public void LoadData()
        {
            foreach (UserAPIModel item in ListChildAgency)
            {
                item.PropertyChanged += DetailModel_PropertyChanged;
            }
        }

        private void DetailModel_PropertyChanged(object sender, PropertyChangedEventArgs args)
        {
            try
            {
                UserAPIModel item = (UserAPIModel)sender;
                switch (args.PropertyName)
                {
                    case nameof(UserAPIModel.IsChecked):
                        if (ListChildAgency.Count(n => n.IsChecked) == ListChildAgency.Count)
                        {
                            SelectAllAgency = true;
                        }
                        else if (ListChildAgency.Count(n => !n.IsChecked) == ListChildAgency.Count)
                        {
                            SelectAllAgency = false;
                        }
                        break;
                }
                OnPropertyChanged(nameof(SelectAllAgency));
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        private async void OnUpload()
        {
            IsLoading = true;
            IEnumerable<string> departments = _phongBanService.FindByCondition(x => x.INamLamViec == _sessionInfo.YearOfWork).Select(n => n.IIDMaBQuanLy);
            try
            {
                if (!ListChildAgency.Any(n => n.IsChecked) || ListChildAgency.Where(n => n.IsChecked).Count() == 0)
                {
                    StringBuilder messageBuilder = new StringBuilder();
                    messageBuilder.AppendFormat("Vui lòng chọn ít nhất 1 đơn vị để phân bổ!");
                    MessageBox.Show(messageBuilder.ToString());
                    IsLoading = false;
                    return;
                }

                (int, string) token = await _hTTPUploadFileService.GetToken(IsSendHTTP);
                string salt = _cryptographyService.GetSalt();
                string tokenKey = Scramble(token.Item2 + salt);
                string chiTietToi = "NG";
                DanhMuc danhMucChiTietToi = _danhMucService.FindByType(TypeDanhMuc.DM_CAUHINH, _sessionService.Current.YearOfWork).Where(n => n.IIDMaDanhMuc == MaDanhMuc.MLNS_CHITIET_TOI).FirstOrDefault();
                if (danhMucChiTietToi != null)
                    chiTietToi = danhMucChiTietToi.SGiaTri;
                DanhMuc itemDanhMuc = _danhMucService.FindByType(TypeDanhMuc.DM_CAUHINH, _sessionService.Current.YearOfWork).Where(n => n.IIDMaDanhMuc == MaDanhMuc.DV_QUANLY).FirstOrDefault();
                List<ExportResult> results = new List<ExportResult>();

                string templateFileName = "rpt_DT_ChungTu.xlsx";

                int namLamViec = _sessionService.Current.YearOfWork;
                IEnumerable<NsMucLucNganSach> listNsMucLucNganSach = _mucLucNganSachService.FindAll(namLamViec);
                DtChungTuModel itemExport = Items.Where(x => x.Selected).FirstOrDefault();
                Dictionary<string, DonVi> dictDonVi = _donViService.FindByListIdDonVi(itemExport.SDsidMaDonVi, namLamViec)
                    .GroupBy(x => x.IIDMaDonVi)
                    .ToDictionary(x => x.Key, x => x.First());
                int count = 0;

                List<DtChungTuChiTietModel> dataExportDetail = LoadDataExportDetail(itemExport);
                if (!dataExportDetail.Any())
                {
                    IsLoading = false;
                    if (departments.Count() == count)
                    {
                        MessageBox.Show(new StringBuilder("Không có dữ liệu gửi").ToString());
                        return;
                    }
                }
                switch (BTieuChiSelected.ValueItem)
                {
                    case nameof(MLNSFiled.NG):
                        dataExportDetail = dataExportDetail.Where(x => string.IsNullOrEmpty(x.STng)).ToList();
                        dataExportDetail.Where(x => !string.IsNullOrEmpty(x.SNg)).Select(x => x.IsHangCha = false).ToList();
                        break;
                    case nameof(MLNSFiled.TNG):
                        dataExportDetail = dataExportDetail.Where(x => string.IsNullOrEmpty(x.STng1)).ToList();
                        dataExportDetail.Where(x => !string.IsNullOrEmpty(x.STng)).Select(x => x.IsHangCha = false).ToList();
                        break;
                    case nameof(MLNSFiled.TNG1):
                        dataExportDetail = dataExportDetail.Where(x => string.IsNullOrEmpty(x.STng2)).ToList();
                        dataExportDetail.Where(x => !string.IsNullOrEmpty(x.STng1)).Select(x => x.IsHangCha = false).ToList();
                        break;
                    case nameof(MLNSFiled.TNG2):
                        dataExportDetail = dataExportDetail.Where(x => string.IsNullOrEmpty(x.STng3)).ToList();
                        dataExportDetail.Where(x => !string.IsNullOrEmpty(x.STng2)).Select(x => x.IsHangCha = false).ToList();
                        break;
                }
                string[] listDonViChungTu = itemExport.SDsidMaDonVi.Split(",");
                IEnumerable<string> listDonVi = ListChildAgency.Where(x => x.IsChecked && listDonViChungTu.Contains(x.Code)).Select(x => x.Code);
                bool isNSSD = itemExport.ILoaiChungTu.HasValue && VoucherType.NSSD_Key.Equals(itemExport.ILoaiChungTu.ToString());
                StringBuilder errorMessageBuilder = new StringBuilder();

                List<NsMucLucNganSach> listMLNS = _mucLucNganSachService.FindByListLnsDonVi(itemExport.SDslns, _sessionService.Current.YearOfWork).ToList();
                DivisionColumnVisibility columnVisibility = new DivisionColumnVisibility();
                columnVisibility.IsDisplayTuChi = listMLNS.Any(x => x.BTuChi);
                columnVisibility.IsDisplayHienVat = listMLNS.Any(x => x.BHienVat);
                columnVisibility.IsDisplayDuPhong = listMLNS.Any(x => x.BDuPhong);
                columnVisibility.IsDisplayHangMua = listMLNS.Any(x => x.BHangMua);
                columnVisibility.IsDisplayHangNhap = listMLNS.Any(x => x.BHangNhap);
                columnVisibility.IsDisplayPhanCap = listMLNS.Any(x => x.BPhanCap);
                columnVisibility.IsDisplayTonKho = listMLNS.Any(x => x.BTonKho);
                string sNameUnit = string.Empty;
                List<FileUploadStreamModel> listAgencyUpload = new List<FileUploadStreamModel>();
                List<Guid> listIdMaDonViSend = new List<Guid>();

                foreach (string idDonVi in listDonVi)
                {
                    Dictionary<string, object> data = new Dictionary<string, object>();
                    string tenDonVi = dictDonVi.GetValueOrDefault(idDonVi, new DonVi()).TenDonVi;
                    string sStage = StringUtils.UCS2Convert(VoucherType.VoucherTypeDict.GetValueOrDefault(itemExport.ILoaiChungTu, string.Empty));

                    data.Add("Cap1", itemDanhMuc != null ? itemDanhMuc.SGiaTri.ToUpper() : "");
                    data.Add("Cap2", _sessionService.Current.TenDonVi.ToUpper());
                    data.Add("TitleFirst", $"DỰ TOÁN CHI NGÂN SÁCH NĂM {_sessionService.Current.YearOfWork}");
                    data.Add("TitleSecond", $"(Kèm theo Quyết định số: {itemExport.SSoQuyetDinh}, ngày: {DateUtils.Format(itemExport.DNgayQuyetDinh)})");

                    data.Add("HeaderTenDonVi", $"Đơn vị: {idDonVi.PadLeft(3, '0')}{StringUtils.DIVISION}{tenDonVi}");
                    data.Add("TenDonVi", $"{idDonVi.PadLeft(3, '0')}{StringUtils.DIVISION}{tenDonVi}");
                    data.Add("SoChungTu", itemExport.SSoChungTu);
                    data.Add("NgayChungTu", DateUtils.Format(itemExport.DNgayChungTu));
                    data.Add("SoQuyetDinh", itemExport.SSoQuyetDinh);
                    data.Add("NgayQuyetDinh", DateUtils.Format(itemExport.DNgayQuyetDinh));
                    data.Add("MoTa", itemExport.SMoTa);
                    data.Add("LoaiDuToan", VoucherType.BudgetTypeDict.GetValueOrDefault(itemExport.ILoaiDuToan, string.Empty));
                    data.Add("LoaiChungTu", VoucherType.VoucherTypeDict.GetValueOrDefault(itemExport.ILoaiChungTu, string.Empty));
                    data.Add("NguoiTao", itemExport.SNguoiTao);
                    data.Add("NgayTao", DateUtils.Format(itemExport.DNgayTao));

                    List<DtChungTuChiTietModel> listData = dataExportDetail.Where(x => x.IsHangCha || idDonVi.Equals(x.IIdMaDonVi)).ToList();
                    CalculateData(listData);
                    DtChungTuModel chungTu = CalculateTotal(listData);

                    List<DtChungTuChiTietModel> listDataExport = listData.Where(CheckIsHasData).ToList();
                    data.Add("Items", listDataExport);
                    data.Add("MLNS", listNsMucLucNganSach);

                    data.Add("TotalTuChi", string.Format(StringUtils.FORMAT_ZERO, chungTu.FTongTuChi));
                    data.Add("TotalHienVat", string.Format(StringUtils.FORMAT_ZERO, chungTu.FTongHienVat));
                    data.Add("TotalHangNhap", string.Format(StringUtils.FORMAT_ZERO, chungTu.FTongHangNhap));
                    data.Add("TotalHangMua", string.Format(StringUtils.FORMAT_ZERO, chungTu.FTongHangMua));
                    data.Add("TotalPhanCap", string.Format(StringUtils.FORMAT_ZERO, chungTu.FTongPhanCap));
                    data.Add("TotalDuPhong", string.Format(StringUtils.FORMAT_ZERO, chungTu.FTongDuPhong));
                    data.Add("TotalTonKho", string.Format(StringUtils.FORMAT_ZERO, chungTu.FTongTonKho));

                    List<int> hideColumns = new List<int>();
                    hideColumns.AddRange(ExportExcelHelper<DtChungTuChiTietModel>.HideColumn(chiTietToi));
                    hideColumns.AddRange(ExportExcelHelper<DtChungTuChiTietModel>.HideColumnDivision(columnVisibility));
                    FlexCel.Core.ExcelFile xlsFile = _exportService.Export<DtChungTuChiTietModel, NsMucLucNganSach>(templateFileName, data, hideColumns);
                    string fileNamePrefix = string.Format("{0}_{1}_{2}", itemExport.SSoChungTu, itemExport.SSoQuyetDinh, StringUtils.ConvertVN(tenDonVi));
                    string fileNameWithoutExtension = String.IsNullOrEmpty(TenBaoCao) ? StringUtils.ConvertVN(StringUtils.CreateExportFileName(fileNamePrefix)) : StringUtils.ConvertVN(TenBaoCao);

                    FileUploadStreamModel childData = new FileUploadStreamModel();
                    string fileNameChildWithoutExtension = fileNameWithoutExtension + "_" + idDonVi;
                    ExportResult Result = new ExportResult(fileNameChildWithoutExtension, fileNameChildWithoutExtension, null, xlsFile);

                    MemoryStream fileStream = new MemoryStream();
                    MemoryStream outputFileStream = new MemoryStream();
                    _exportService.Open(Result, ref fileStream);

                    await _cryptographyService.EncryptFile(fileStream, ref outputFileStream, tokenKey);
                    childData.File = outputFileStream;
                    childData.Name = fileNameChildWithoutExtension + FileExtensionFormats.Security;
                    listAgencyUpload.Add(childData);
                    listIdMaDonViSend.Add(ListChildAgency.Where(x => x.Code == idDonVi).FirstOrDefault().Id);
                }

                bool status = await _hTTPUploadFileService.UploadFileAsync(IsSendHTTP, new FileUploadStreamModel()
                {
                    //File = outputFileStream,
                    //Name = fileNameWithoutExtension + FileExtensionFormats.Security,
                    Description = "Chứng từ tổng hợp",
                    Module = NSFunctionCode.BUDGET,
                    ModuleName = "Ngân sách",
                    SubModule = itemExport.ILoaiChungTu == 1 ? NSFunctionCode.BUDGET_ESTIMATE_DIVISION : NSFunctionCode.BUDGET_ESTIMATE_DIVISION_GUARANTEE,
                    SubModuleName = "Phân bổ dự toán",
                    TokenKey = tokenKey,
                    YearOfWork = _sessionInfo.YearOfWork,
                    YearOfBudget = _sessionInfo.YearOfBudget,
                    SourceOfBudget = _sessionInfo.Budget,
                    Department = "",
                    Quarter = string.Empty,
                    IdChild = string.Join(",", listIdMaDonViSend),
                    listAgencyUpload = listAgencyUpload
                });
                if (!status)
                {
                    IsLoading = false;
                    MessageBox.Show(new StringBuilder("Gửi dữ liệu thất bại").ToString());
                    return;
                }
                else
                {
                    IsLoading = false;
                    MessageBox.Show(new StringBuilder("Gửi dữ liệu thành công").ToString());
                    return;
                }

            }
            catch (Exception ex)
            {
                IsLoading = false;
                MessageBox.Show(new StringBuilder("Gửi dữ liệu thất bại").ToString());
                _logger.Error(ex.Message, ex);
                return;
            }
        }

        private void CalculateData(List<DtChungTuChiTietModel> listData)
        {
            listData.Where(x => x.IsHangCha)
                .Select(x =>
                {
                    x.FTuChi = 0;
                    x.FHienVat = 0;
                    x.FHangNhap = 0;
                    x.FHangMua = 0;
                    x.FPhanCap = 0;
                    return x;
                }).ToList();

            foreach (DtChungTuChiTietModel item in listData.Where(x => x.IsEditable && (x.FTuChi != 0 || x.FHienVat != 0 || x.FHangNhap != 0 || x.FHangMua != 0 || x.FPhanCap != 0)))
            {
                CalculateParent(listData, item, item);
            }
        }

        private void CalculateParent(List<DtChungTuChiTietModel> listData, DtChungTuChiTietModel currentItem, DtChungTuChiTietModel seftItem)
        {
            DtChungTuChiTietModel parrentItem = listData.FirstOrDefault(x => x.IIdMlns == currentItem.IIdMlnsCha);
            if (parrentItem == null) return;
            parrentItem.FTuChi += seftItem.FTuChi;
            parrentItem.FHienVat += seftItem.FHienVat;
            parrentItem.FHangNhap += seftItem.FHangNhap;
            parrentItem.FHangMua += seftItem.FHangMua;
            parrentItem.FPhanCap += seftItem.FPhanCap;
            CalculateParent(listData, parrentItem, seftItem);
        }

        private DtChungTuModel CalculateTotal(List<DtChungTuChiTietModel> listData)
        {
            DtChungTuModel chungTu = new DtChungTuModel();

            List<DtChungTuChiTietModel> listChildren = listData.Where(x => x.IsEditable).ToList();
            foreach (DtChungTuChiTietModel item in listChildren)
            {
                chungTu.FTongTuChi += item.FTuChi;
                chungTu.FTongHienVat += item.FHienVat;
                chungTu.FTongHangNhap += item.FHangNhap;
                chungTu.FTongHangMua += item.FHangMua;
                chungTu.FTongPhanCap += item.FPhanCap;
            }

            return chungTu;
        }

        private List<DtChungTuChiTietModel> LoadDataExportDetail(DtChungTuModel item)
        {
            IEnumerable<string> listMlns = _mucLucNganSaches.Select(n => n.Lns);
            IEnumerable<string> listLns = item.SDslns.Split(StringUtils.COMMA).Where(x => listMlns.Contains(x));
            EstimationVoucherDetailCriteria searchCondition = new EstimationVoucherDetailCriteria
            {
                VoucherId = item.Id,
                LNS = string.Join(StringUtils.COMMA, listLns),
                YearOfWork = item.INamLamViec,
                YearOfBudget = item.INamNganSach,
                BudgetSource = item.IIdMaNguonNganSach,
                VoucherDate = item.DNgayChungTu,
                IdDotNhan = item.IIdDotNhan,
                SoChungTu = item.SSoChungTu
            };

            if (item.ILoaiDuToan.HasValue && BudgetType.ADJUSTED.Equals((BudgetType)item.ILoaiDuToan.Value))
            {
                IEnumerable<NsDtChungTu> listNhanPhanBo = LoadNhanPhanBo(item.Id.ToString());
                searchCondition.LNS = string.Join(",", listNhanPhanBo.Select(x => x.SDslns));
            }

            List<Core.Domain.Query.NsDtChungTuChiTietQuery> listChungTuChiTiet = _dtChungTuChiTietService.FindByCond(searchCondition, procedure: "sp_dt_export_phan_bo_du_toan_chi_tiet").ToList();
            _listChungTuChiTiet = _mapper.Map<List<DtChungTuChiTietModel>>(listChungTuChiTiet);
            return _listChungTuChiTiet;
        }

        private bool CheckIsHasData(DtChungTuChiTietModel chiTietModel)
        {
            return chiTietModel.FTuChi != 0 || chiTietModel.FHienVat != 0 || chiTietModel.FDuPhong != 0 ||
                   chiTietModel.FHangNhap != 0 || chiTietModel.FHangMua != 0 || chiTietModel.FPhanCap != 0;
        }

        private IEnumerable<NsDtChungTu> LoadNhanPhanBo(string idPhanBoDuToan)
        {
            List<NsDtNhanPhanBoMap> dtChungTuMapByIdPhanBoDuToan = _dtChungTuMapService.FindByIdPhanBoDuToan(idPhanBoDuToan).ToList();
            HashSet<string> listIdNhanPhanBo = dtChungTuMapByIdPhanBoDuToan.Select(e => e.IIdCtduToanNhan.ToString()).ToHashSet();

            List<NsDtChungTu> listNhanPhanBo = new List<NsDtChungTu>();
            if (dtChungTuMapByIdPhanBoDuToan.Count() > 0)
            {
                System.Linq.Expressions.Expression<Func<NsDtChungTu, bool>> predicate = PredicateBuilder.True<NsDtChungTu>();
                predicate = predicate.And(x => listIdNhanPhanBo.Contains(x.Id.ToString()));
                listNhanPhanBo = _estimationService.FindByCondition(predicate).OrderBy(x => x.DNgayChungTu).ToList();
            }
            return listNhanPhanBo;
        }

        private string Scramble(string s)
        {
            return new string(s.ToCharArray().OrderBy(x => Guid.NewGuid()).ToArray());
        }
    }
}
