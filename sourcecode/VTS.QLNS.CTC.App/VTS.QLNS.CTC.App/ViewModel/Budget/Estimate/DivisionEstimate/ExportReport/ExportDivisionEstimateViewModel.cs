using AutoMapper;
using log4net;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
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

namespace VTS.QLNS.CTC.App.ViewModel.Budget.Estimate.DivisionEstimate.ExportReport
{
    public class ExportDivisionEstimateViewModel : DialogViewModelBase<DtChungTuModel>
    {
        private readonly ILog _logger;
        private readonly IMapper _mapper;
        private SessionInfo _sessionInfo;
        private readonly ISessionService _sessionService;
        private readonly IDanhMucService _danhMucService;
        private readonly INsDonViService _donViService;
        private readonly INsQtChungTuChiTietService _chungTuChiTietService;
        private readonly INsMucLucNganSachService _mucLucNganSachService;
        private readonly IExportService _exportService;
        private readonly INsDtNhanPhanBoMapService _dtChungTuMapService;
        private readonly INsDtChungTuChiTietService _dtChungTuChiTietService;
        private readonly INsDtChungTuService _estimationService;

        private List<NsMucLucNganSach> _mucLucNganSachs;
        public ObservableCollection<DtChungTuModel> Items;
        public override Type ContentType => typeof(View.Budget.Estimate.DivisionEstimate.ExportReport.ExportDivisionEstimate);
        private ObservableCollection<ComboboxItem> _bTieuChiItems = new ObservableCollection<ComboboxItem>();
        public ObservableCollection<ComboboxItem> BTieuChiItems
        {
            get => _bTieuChiItems;
            set => SetProperty(ref _bTieuChiItems, value);
        }

        private bool _isProcessReport;
        public bool IsProcessReport
        {
            get => _isProcessReport;
            set => SetProperty(ref _isProcessReport, value);
        }

        private int _progressValue;
        public int ProgressValue
        {
            get => _progressValue;
            set => SetProperty(ref _progressValue, value);
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

        public List<DtChungTuChiTietModel> _listChungTuChiTiet;

        public RelayCommand ExportCommand { get; }

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

        private bool IsDonViRoot(string iIDMaDonVi) => iIDMaDonVi == _sessionInfo.IdDonVi;

        public ExportDivisionEstimateViewModel(ILog logger,
                                               IMapper mapper,
                                               ISessionService sessionService,
                                               IDanhMucService danhMucService,
                                               INsDonViService donViService,
                                               INsQtChungTuChiTietService chungTuChiTietService,
                                               INsMucLucNganSachService mucLucNganSachService,
                                               IExportService exportService,
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

            ExportCommand = new RelayCommand(obj => OnExportGridData());
            _dtChungTuMapService = dtChungTuMapService;
            _dtChungTuChiTietService = dtChungTuChiTietService;
            _estimationService = estimationService;
        }

        public override void Init()
        {
            base.Init();
            LoadTieuChis();
            _sessionInfo = _sessionService.Current;
            _mucLucNganSachs = _mucLucNganSachService.FindAll(_sessionInfo.YearOfWork).ToList();

        }

        private void OnExportGridData()
        {
            BackgroundWorkerHelper.Run((s, e) =>
            {
                IsProcessReport = true;

                string chiTietToi = "NG";
                DanhMuc danhMucChiTietToi = _danhMucService.FindByType(TypeDanhMuc.DM_CAUHINH, _sessionService.Current.YearOfWork).Where(n => n.IIDMaDanhMuc == MaDanhMuc.MLNS_CHITIET_TOI).FirstOrDefault();
                if (danhMucChiTietToi != null)
                    chiTietToi = danhMucChiTietToi.SGiaTri;
                DanhMuc itemDanhMuc = _danhMucService.FindByType(TypeDanhMuc.DM_CAUHINH, _sessionService.Current.YearOfWork).Where(n => n.IIDMaDanhMuc == MaDanhMuc.DV_QUANLY).FirstOrDefault();
                List<ExportResult> results = new List<ExportResult>();

                string templateFileName = "rpt_DT_ChungTu.xlsx";

                int namLamViec = _sessionService.Current.YearOfWork;
                IEnumerable<NsMucLucNganSach> listNsMucLucNganSach = _mucLucNganSachService.FindAll(namLamViec);
                IEnumerable<DtChungTuModel> itemsExport = Items.Where(x => x.Selected);
                Dictionary<string, DonVi> dictDonVi = _donViService.FindByListIdDonVi(string.Join(",", itemsExport.Select(x => x.SDsidMaDonVi)), namLamViec)
                    .GroupBy(x => x.IIDMaDonVi)
                    .ToDictionary(x => x.Key, x => x.First());

                int count = itemsExport.Select(x => x.SDsidMaDonVi.Split(",")).SelectMany(x => x).Count();
                int index = 0;
                foreach (DtChungTuModel item in itemsExport)
                {
                    List<DtChungTuChiTietModel> dataExportDetail = LoadDataExportDetail(item);
                    List<DtChungTuChiTietModel> dataParentGroups = dataExportDetail.Where(x => x.IsHangCha).ToList();
                    List<DtChungTuChiTietModel> dataChildGroups = dataExportDetail.Where(x => !x.IsHangCha).GroupBy(g => new { g.IIdMlns, g.IIdMlnsCha, g.SXauNoiMa, g.SLns, g.SL, g.SK, g.SM, g.STm, g.STtm, g.SNg, g.STng, g.STng1, g.STng2, g.STng3, g.SMoTa, g.IsHangCha }).Select(x => new DtChungTuChiTietModel
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
                        FTonKho = x.Sum(x => x.FTonKho),
                        FTuChi = x.Sum(x => x.FTuChi),
                        FRutKBNN = x.Sum(x => x.FRutKBNN),
                        FTuChiTruocDieuChinh = x.Sum(x => x.FTuChiTruocDieuChinh),
                        FTuChiSauDieuChinh = x.Sum(x => x.FTuChiSauDieuChinh),
                        FHienVat = x.Sum(x => x.FHienVat),
                        FHienVatTruocDieuChinh = x.Sum(x => x.FHienVatTruocDieuChinh),
                        FHienVatSauDieuChinh = x.Sum(x => x.FHienVatSauDieuChinh),
                        HienVatDaCap = x.Sum(x => x.HienVatDaCap),
                        TuChiDaCap = x.Sum(x => x.TuChiDaCap),
                        FDuPhong = x.Sum(x => x.FDuPhong),
                        FHangMua = x.Sum(x => x.FHangMua),
                        FHangMuaSauDieuChinh = x.Sum(x => x.FHangMuaSauDieuChinh),
                        FHangMuaTruocDieuChinh = x.Sum(x => x.FHangMuaTruocDieuChinh),
                        FHangNhap = x.Sum(x => x.FHangNhap),
                        FHangNhapTruocDieuChinh = x.Sum(x => x.FHangNhapTruocDieuChinh),
                        FHangNhapSauDieuChinh = x.Sum(x => x.FHangNhapSauDieuChinh),
                        FPhanCap = x.Sum(x => x.FPhanCap),
                        FPhanCapTruocDieuChinh = x.Sum(x => x.FPhanCapTruocDieuChinh),
                        FPhanCapSauDieuChinh = x.Sum(x => x.FPhanCapSauDieuChinh),
                        SGhiChu = x.First().SGhiChu,
                        IIdMaDonVi = x.First().IIdMaDonVi,
                        STenDonVi = x.First().STenDonVi,
                        SChiTietToi = x.First().SChiTietToi,
                    }).ToList();
                    List<DtChungTuChiTietModel> dataGroups = dataParentGroups;
                    dataGroups.AddRange(dataChildGroups);
                    dataExportDetail = dataGroups.OrderBy(s => s.SXauNoiMa).ToList();
                    var listDonVi = item.SDsidMaDonVi.Split(",");
                    bool isNSSD = item.ILoaiChungTu.HasValue && VoucherType.NSSD_Key.Equals(item.ILoaiChungTu.ToString());

                    List<NsMucLucNganSach> listMLNS = _mucLucNganSachService.FindByListLnsDonVi(item.SDslns, _sessionService.Current.YearOfWork).ToList();
                    DivisionColumnVisibility columnVisibility = new DivisionColumnVisibility();
                    columnVisibility.IsDisplayTuChi = listMLNS.Any(x => x.BTuChi);
                    columnVisibility.IsDisplayHienVat = listMLNS.Any(x => x.BHienVat);
                    columnVisibility.IsDisplayDuPhong = listMLNS.Any(x => x.BDuPhong);
                    columnVisibility.IsDisplayHangMua = listMLNS.Any(x => x.BHangMua);
                    columnVisibility.IsDisplayHangNhap = listMLNS.Any(x => x.BHangNhap);
                    columnVisibility.IsDisplayPhanCap = listMLNS.Any(x => x.BPhanCap);
                    columnVisibility.IsDisplayTonKho = listMLNS.Any(x => x.BTonKho);

                    foreach (string idDonVi in listDonVi)
                    {
                        Dictionary<string, object> data = new Dictionary<string, object>();
                        string tenDonVi = dictDonVi.GetValueOrDefault(idDonVi, new DonVi()).TenDonVi;

                        data.Add("Cap1", itemDanhMuc != null ? itemDanhMuc.SGiaTri.ToUpper() : "");
                        data.Add("Cap2", _sessionService.Current.TenDonVi.ToUpper());
                        data.Add("TitleFirst", $"DỰ TOÁN CHI NGÂN SÁCH NĂM {_sessionService.Current.YearOfWork}");
                        data.Add("TitleSecond", $"(Kèm theo Quyết định số: {item.SSoQuyetDinh}, ngày: {DateUtils.Format(item.DNgayQuyetDinh)})");

                        data.Add("HeaderTenDonVi", $"Đơn vị: {idDonVi.PadLeft(3, '0')}{StringUtils.DIVISION}{tenDonVi}");
                        data.Add("TenDonVi", $"{idDonVi.PadLeft(3, '0')}{StringUtils.DIVISION}{tenDonVi}");
                        data.Add("SoChungTu", item.SSoChungTu);
                        data.Add("NgayChungTu", DateUtils.Format(item.DNgayChungTu));
                        data.Add("SoQuyetDinh", item.SSoQuyetDinh);
                        data.Add("NgayQuyetDinh", DateUtils.Format(item.DNgayQuyetDinh));
                        data.Add("MoTa", item.SMoTa);
                        data.Add("LoaiDuToan", VoucherType.BudgetTypeDict.GetValueOrDefault(item.ILoaiDuToan, string.Empty));
                        data.Add("LoaiChungTu", VoucherType.VoucherTypeDict.GetValueOrDefault(item.ILoaiChungTu, string.Empty));
                        data.Add("NguoiTao", item.SNguoiTao);
                        data.Add("NgayTao", DateUtils.Format(item.DNgayTao));

                        List<DtChungTuChiTietModel> listData = dataExportDetail.Where(x => x.IsHangCha || idDonVi.Equals(x.IIdMaDonVi)).ToList();
                        CalculateData(listData);
                        DtChungTuModel chungTu = CalculateTotal(listData);

                        List<DtChungTuChiTietModel> listDataExport = listData.Where(CheckIsHasData).ToList();
                        switch (BTieuChiSelected.ValueItem)
                        {
                            case nameof(MLNSFiled.NG):
                                listDataExport = listDataExport.Where(x => string.IsNullOrEmpty(x.STng)).ToList();
                                listDataExport.Where(x => !string.IsNullOrEmpty(x.SNg)).Select(x => x.IsHangCha = false).ToList();
                                break;
                            case nameof(MLNSFiled.TNG):
                                listDataExport = listDataExport.Where(x => string.IsNullOrEmpty(x.STng1)).ToList();
                                listDataExport.Where(x => !string.IsNullOrEmpty(x.STng)).Select(x => x.IsHangCha = false).ToList();
                                break;
                            case nameof(MLNSFiled.TNG1):
                                listDataExport = listDataExport.Where(x => string.IsNullOrEmpty(x.STng2)).ToList();
                                listDataExport.Where(x => !string.IsNullOrEmpty(x.STng1)).Select(x => x.IsHangCha = false).ToList();
                                break;
                            case nameof(MLNSFiled.TNG2):
                                listDataExport = listDataExport.Where(x => string.IsNullOrEmpty(x.STng3)).ToList();
                                listDataExport.Where(x => !string.IsNullOrEmpty(x.STng2)).Select(x => x.IsHangCha = false).ToList();
                                break;
                        }

                        if (listDataExport.Where(CheckIsHasData).Count() == 0) continue;

                        data.Add("Items", listDataExport);
                        data.Add("MLNS", listNsMucLucNganSach);

                        data.Add("TotalTuChi", string.Format(StringUtils.FORMAT_ZERO, chungTu.FTongTuChi));
                        data.Add("TotalRutKBNN", string.Format(StringUtils.FORMAT_ZERO, chungTu.FTongRutKBNN));
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
                        FlexCel.Core.TXlsNamedRange nameRange = xlsFile.GetNamedRange(1);
                        nameRange.Comment = "Workbook";
                        xlsFile.SetNamedRange(nameRange);
                        xlsFile.SetNamedRange(new FlexCel.Core.TXlsNamedRange("__Area_Items__", 1, 0, "=1:2,A:B"));
                        xlsFile.SetCellValue(50, 50, "CheckSum");
                        xlsFile.SetRowHidden(50, true);
                        string fileNamePrefix = string.Format("{0}_{1}_{2}", item.SSoChungTu, item.SSoQuyetDinh, StringUtils.ConvertVN(tenDonVi));
                        string fileNameWithoutExtension = StringUtils.CreateExportFileName(fileNamePrefix);
                        results.Add(new ExportResult(fileNameWithoutExtension, fileNameWithoutExtension, null, xlsFile));
                        (s as BackgroundWorker).ReportProgress((index++) * 100 / count, null);
                    }
                }
                e.Result = results;
            }, (s, e) =>
            {
                IsProcessReport = false;
                if (e.Error == null)
                {
                    List<ExportResult> result = (List<ExportResult>)e.Result;
                    _exportService.Open(result, ExportType.EXCEL);
                }
                else
                {
                    _logger.Error(e.Error.Message);
                }
            }, (s, e) =>
            {
                ProgressValue = e.ProgressPercentage;
            });
        }

        private List<DtChungTuChiTietModel> LoadDataExportDetail(DtChungTuModel item)
        {
            EstimationVoucherDetailCriteria searchCondition = new EstimationVoucherDetailCriteria
            {
                VoucherId = item.Id,
                LNS = item.SDslns,
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

        private void CalculateData(List<DtChungTuChiTietModel> listData)
        {
            listData.Where(x => x.IsHangCha)
                .Select(x =>
                {
                    x.FTuChi = 0;
                    x.FRutKBNN = 0;
                    x.FHienVat = 0;
                    x.FHangNhap = 0;
                    x.FHangMua = 0;
                    x.FPhanCap = 0;
                    return x;
                }).ToList();

            foreach (DtChungTuChiTietModel item in listData.Where(x => x.IsEditable && (x.FRutKBNN != 0 || x.FTuChi != 0 || x.FHienVat != 0 || x.FHangNhap != 0 || x.FHangMua != 0 || x.FPhanCap != 0)))
            {
                CalculateParent(listData, item, item);
            }
        }

        private void CalculateParent(List<DtChungTuChiTietModel> listData, DtChungTuChiTietModel currentItem, DtChungTuChiTietModel seftItem)
        {
            DtChungTuChiTietModel parrentItem = listData.FirstOrDefault(x => x.IIdMlns == currentItem.IIdMlnsCha);
            if (parrentItem == null) return;
            parrentItem.FTuChi += seftItem.FTuChi;
            parrentItem.FRutKBNN += seftItem.FRutKBNN;
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
                chungTu.FTongRutKBNN += item.FRutKBNN;
                chungTu.FTongHienVat += item.FHienVat;
                chungTu.FTongHangNhap += item.FHangNhap;
                chungTu.FTongHangMua += item.FHangMua;
                chungTu.FTongPhanCap += item.FPhanCap;
            }

            return chungTu;
        }

        private bool CheckIsHasData(DtChungTuChiTietModel chiTietModel)
        {
            return chiTietModel.FTuChi != 0 || 
                   chiTietModel.FRutKBNN != 0 ||
                   chiTietModel.FHienVat != 0 || chiTietModel.FDuPhong != 0 ||
                   chiTietModel.FHangNhap != 0 || chiTietModel.FHangMua != 0 || chiTietModel.FPhanCap != 0;
        }
    }
}
