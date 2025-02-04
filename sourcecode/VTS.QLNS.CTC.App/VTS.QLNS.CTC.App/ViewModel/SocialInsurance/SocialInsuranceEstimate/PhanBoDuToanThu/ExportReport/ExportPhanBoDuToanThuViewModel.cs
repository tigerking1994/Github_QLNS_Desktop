using AutoMapper;
using log4net;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace VTS.QLNS.CTC.App.ViewModel.SocialInsurance.SocialInsuranceEstimate.PhanBoDuToanThu.ExportReport
{
    public class ExportPhanBoDuToanThuViewModel : DialogViewModelBase<BhDtPhanBoChungTuModel>
    {
        private readonly ILog _logger;
        private readonly IMapper _mapper;
        private SessionInfo _sessionInfo;
        private readonly ISessionService _sessionService;
        private readonly INsDonViService _donViService;
        private readonly IBhDmMucLucNganSachService _mucLucNganSachService;
        private readonly IExportService _exportService;
        private readonly IBhDttNhanPhanBoMapService _dtChungTuMapService;
        private readonly IDttBHXHPhanBoChiTietService _dtChungTuChiTietService;
        private readonly IDttBHXHPhanBoService _estimationService;
        private readonly IDanhMucService _danhMucService;

        private List<BhDmMucLucNganSach> _mucLucNganSachs;
        public ObservableCollection<BhDtPhanBoChungTuModel> Items;
        public override Type ContentType => typeof(View.SocialInsurance.SocialInsuranceEstimate.PhanBoDuToanThu.ExportReport.ExportPhanBoDuToanThu);
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

        public List<BhDtPhanBoChungTuChiTietModel> _listChungTuChiTiet;

        public RelayCommand ExportCommand { get; }

        private bool IsDonViRoot(string iIDMaDonVi) => iIDMaDonVi == _sessionInfo.IdDonVi;

        public ExportPhanBoDuToanThuViewModel(ILog logger,
                                               IMapper mapper,
                                               ISessionService sessionService,
                                               INsDonViService donViService,
                                               IBhDmMucLucNganSachService mucLucNganSachService,
                                               IExportService exportService,
                                               IBhDttNhanPhanBoMapService dtChungTuMapService,
                                               IDttBHXHPhanBoChiTietService dtChungTuChiTietService,
                                               IDttBHXHPhanBoService estimationService,
                                               IDanhMucService danhMucService)
        {

            _logger = logger;
            _mapper = mapper;
            _sessionService = sessionService;
            _donViService = donViService;
            _mucLucNganSachService = mucLucNganSachService;
            _exportService = exportService;
            _danhMucService = danhMucService;

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
            _mucLucNganSachs = _mucLucNganSachService.FindAllByYear(_sessionInfo.YearOfWork).ToList();

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

        private void OnExportGridData()
        {
            BackgroundWorkerHelper.Run((s, e) =>
            {
                IsLoading = true;
                string chiTietToi = "NG";
                DanhMuc danhMucChiTietToi = _danhMucService.FindByType(TypeDanhMuc.DM_CAUHINH, _sessionService.Current.YearOfWork).Where(n => n.IIDMaDanhMuc == MaDanhMuc.MLNS_CHITIET_TOI).FirstOrDefault();
                if (danhMucChiTietToi != null)
                    chiTietToi = danhMucChiTietToi.SGiaTri;
                DanhMuc itemDanhMuc = _danhMucService.FindByType(TypeDanhMuc.DM_CAUHINH, _sessionService.Current.YearOfWork).Where(n => n.IIDMaDanhMuc == MaDanhMuc.DV_QUANLY).FirstOrDefault();
                List<ExportResult> results = new List<ExportResult>();

                string templateFileName = ExportFileName.EXPORT_BH_DTT_BHXH_CHUNGTU_CHITIET;

                int namLamViec = _sessionService.Current.YearOfWork;
                IEnumerable<BhDmMucLucNganSach> listBhDmMucLucNganSach = _mucLucNganSachService.FindAllByYear(namLamViec);
                IEnumerable<BhDtPhanBoChungTuModel> itemsExport = Items.Where(x => x.Selected);
                Dictionary<string, DonVi> dictDonVi = _donViService.FindByListIdDonVi(string.Join(",", itemsExport.Select(x => x.SDsidMaDonVi)), namLamViec)
                    .GroupBy(x => x.IIDMaDonVi)
                    .ToDictionary(x => x.Key, x => x.First());
                foreach (BhDtPhanBoChungTuModel item in itemsExport)
                {
                    List<BhDtPhanBoChungTuChiTietModel> dataExportDetail = LoadDataExportDetail(item);
                    List<BhDtPhanBoChungTuChiTietModel> dataParentGroups = dataExportDetail.Where(x => x.IsHangCha).ToList();
                    List<BhDtPhanBoChungTuChiTietModel> dataChildGroups = dataExportDetail.Where(x => !x.IsHangCha).GroupBy(g => new { g.IIdMlns, g.IIdMlnsCha, g.SXauNoiMa, g.SLns, g.SL, g.SK, g.SM, g.STm, g.STtm, g.SNg, g.STng, g.STng1, g.STng2, g.STng3, g.SMoTa, g.IsHangCha }).Select(x => new BhDtPhanBoChungTuChiTietModel
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

                        FBHXHNLD = x.Sum(x => x.FBHXHNLD),
                        FBHXHNLDTruocDieuChinh = x.Sum(x => x.FBHXHNLDTruocDieuChinh),
                        FBHXHNLDSauDieuChinh = x.Sum(x => x.FBHXHNLDSauDieuChinh),
                        FBHXHNSD = x.Sum(x => x.FBHXHNSD),
                        FBHXHNSDTruocDieuChinh = x.Sum(x => x.FBHXHNSDTruocDieuChinh),
                        FBHXHNSDSauDieuChinh = x.Sum(x => x.FBHXHNSDSauDieuChinh),
                        FThuBHXH = x.Sum(x => x.FBHXHNSD),
                        FThuBHXHTruocDieuChinh = x.Sum(x => x.FThuBHXHTruocDieuChinh),
                        FThuBHXHSauDieuChinh = x.Sum(x => x.FThuBHXHSauDieuChinh),

                        FBHYTNLD = x.Sum(x => x.FBHYTNLD),
                        FBHYTNLDTruocDieuChinh = x.Sum(x => x.FBHYTNLDTruocDieuChinh),
                        FBHYTNLDSauDieuChinh = x.Sum(x => x.FBHYTNLDSauDieuChinh),
                        FBHYTNSD = x.Sum(x => x.FBHYTNSD),
                        FBHYTNSDTruocDieuChinh = x.Sum(x => x.FBHYTNSDTruocDieuChinh),
                        FBHYTNSDSauDieuChinh = x.Sum(x => x.FBHYTNSDSauDieuChinh),
                        FThuBHYT = x.Sum(x => x.FThuBHYT),
                        FThuBHYTTruocDieuChinh = x.Sum(x => x.FThuBHYTTruocDieuChinh),
                        FThuBHYTSauDieuChinh = x.Sum(x => x.FThuBHYTSauDieuChinh),

                        FBHTNNLD = x.Sum(x => x.FBHTNNLD),
                        FBHTNNLDTruocDieuChinh = x.Sum(x => x.FBHTNNLDTruocDieuChinh),
                        FBHTNNLDSauDieuChinh = x.Sum(x => x.FBHTNNLDSauDieuChinh),
                        FBHTNNSD = x.Sum(x => x.FBHTNNSD),
                        FBHTNNSDTruocDieuChinh = x.Sum(x => x.FBHTNNSDTruocDieuChinh),
                        FBHTNNSDSauDieuChinh = x.Sum(x => x.FBHTNNSDSauDieuChinh),
                        FThuBHTN = x.Sum(x => x.FThuBHTN),
                        FThuBHTNTruocDieuChinh = x.Sum(x => x.FThuBHTNTruocDieuChinh),
                        FThuBHTNSauDieuChinh = x.Sum(x => x.FThuBHTNSauDieuChinh),

                        SGhiChu = x.First().SGhiChu,
                        IIdMaDonVi = x.First().IIdMaDonVi,
                        STenDonVi = x.First().STenDonVi,
                        SChiTietToi = x.First().SChiTietToi,
                    }).ToList();
                    List<BhDtPhanBoChungTuChiTietModel> dataGroups = dataParentGroups;
                    dataGroups.AddRange(dataChildGroups);
                    dataExportDetail = dataGroups.OrderBy(s => s.SXauNoiMa).ToList();
                    string[] listDonVi = item.SDsidMaDonVi.Split(",");

                    List<BhDmMucLucNganSach> listMLNS = _mucLucNganSachService.FindByListLnsDonVi(item.SDslns, _sessionService.Current.YearOfWork).ToList();

                    foreach (string idDonVi in listDonVi)
                    {
                        Dictionary<string, object> data = new Dictionary<string, object>();

                        string tenDonVi = dictDonVi.GetValueOrDefault(idDonVi, new DonVi()).TenDonVi;

                        data.Add("Cap1", itemDanhMuc != null ? itemDanhMuc.SGiaTri.ToUpper() : "");
                        data.Add("Cap2", _sessionService.Current.TenDonVi.ToUpper());
                        data.Add("TitleFirst", $"DỰ TOÁN THU BHXH NĂM {_sessionService.Current.YearOfWork}");
                        data.Add("TitleSecond", $"(Kèm theo Quyết định số: {item.SSoQuyetDinh}, ngày: {DateUtils.Format(item.DNgayQuyetDinh)})");

                        data.Add("HeaderTenDonVi", $"Đơn vị: {idDonVi.PadLeft(3, '0')}{StringUtils.DIVISION}{tenDonVi}");
                        data.Add("TenDonVi", $"{idDonVi.PadLeft(3, '0')}{StringUtils.DIVISION}{tenDonVi}");
                        data.Add("SoChungTu", item.SSoChungTu);
                        data.Add("NgayChungTu", DateUtils.Format(item.DNgayChungTu));
                        data.Add("SoQuyetDinh", item.SSoQuyetDinh);
                        data.Add("NgayQuyetDinh", DateUtils.Format(item.DNgayQuyetDinh));
                        data.Add("MoTa", item.SMoTa);
                        data.Add("LoaiDuToan", VoucherType.BudgetTypeDict.GetValueOrDefault(item.ILoaiDuToan, string.Empty));
                        data.Add("NguoiTao", item.SNguoiTao);
                        data.Add("NgayTao", DateUtils.Format(item.DNgayTao));

                        List<BhDtPhanBoChungTuChiTietModel> listData = dataExportDetail.Where(x => x.IsHangCha || idDonVi.Equals(x.IIdMaDonVi)).ToList();
                        CalculateData(listData);
                        BhDtPhanBoChungTuModel chungTu = CalculateTotal(listData);

                        List<BhDtPhanBoChungTuChiTietModel> listDataExport = listData.Where(CheckIsHasData).ToList();
                        switch (BTieuChiSelected.ValueItem)
                        {
                            case nameof(MLNSFiled.NG):
                                listDataExport = listDataExport.Where(x => string.IsNullOrEmpty(x.STng)).ToList();
                                listDataExport.Where(x => !string.IsNullOrEmpty(x.SNg)).Select(x => x.BHangCha = false).ToList();
                                break;
                            case nameof(MLNSFiled.TNG):
                                listDataExport = listDataExport.Where(x => string.IsNullOrEmpty(x.STng1)).ToList();
                                listDataExport.Where(x => !string.IsNullOrEmpty(x.STng)).Select(x => x.BHangCha = false).ToList();
                                break;
                            case nameof(MLNSFiled.TNG1):
                                listDataExport = listDataExport.Where(x => string.IsNullOrEmpty(x.STng2)).ToList();
                                listDataExport.Where(x => !string.IsNullOrEmpty(x.STng1)).Select(x => x.BHangCha = false).ToList();
                                break;
                            case nameof(MLNSFiled.TNG2):
                                listDataExport = listDataExport.Where(x => string.IsNullOrEmpty(x.STng3)).ToList();
                                listDataExport.Where(x => !string.IsNullOrEmpty(x.STng2)).Select(x => x.BHangCha = false).ToList();
                                break;
                        }

                        data.Add("Items", listDataExport);
                        data.Add("MLNS", listBhDmMucLucNganSach);

                        data.Add("TotalBHXHNLD", string.Format(StringUtils.FORMAT_ZERO, chungTu.FThuBHXHNLD));
                        data.Add("TotalBHYTNLD", string.Format(StringUtils.FORMAT_ZERO, chungTu.FThuBHYTNLD));
                        data.Add("TotalBHTNNLD", string.Format(StringUtils.FORMAT_ZERO, chungTu.FThuBHTNNLD));
                        data.Add("TotalBHXHNSD", string.Format(StringUtils.FORMAT_ZERO, chungTu.FThuBHXHNSD));
                        data.Add("TotalBHYTNSD", string.Format(StringUtils.FORMAT_ZERO, chungTu.FThuBHYTNSD));
                        data.Add("TotalBHTNNSD", string.Format(StringUtils.FORMAT_ZERO, chungTu.FThuBHTNNSD));
                        data.Add("TotalBHXH", string.Format(StringUtils.FORMAT_ZERO, chungTu.FTongBHXH));
                        data.Add("TotalBHYT", string.Format(StringUtils.FORMAT_ZERO, chungTu.FTongBHYT));
                        data.Add("TotalBHTN", string.Format(StringUtils.FORMAT_ZERO, chungTu.FTongBHTN));
                        data.Add("Total", string.Format(StringUtils.FORMAT_ZERO, chungTu.FTongDuToan));

                        List<int> hideColumns = new List<int>();
                        hideColumns.AddRange(ExportExcelHelper<BhDtPhanBoChungTuChiTietModel>.HideColumn(chiTietToi));
                        FlexCel.Core.ExcelFile xlsFile = _exportService.Export<BhDtPhanBoChungTuChiTietModel, BhDmMucLucNganSach>(templateFileName, data);
                        FlexCel.Core.TXlsNamedRange nameRange = xlsFile.GetNamedRange(1);
                        nameRange.Comment = "Workbook";
                        xlsFile.SetNamedRange(nameRange);
                        xlsFile.SetNamedRange(new FlexCel.Core.TXlsNamedRange("__Area_Items__", 1, 0, "=1:2,A:B"));
                        xlsFile.SetCellValue(50, 50, "CheckSum");
                        xlsFile.SetRowHidden(50, true);
                        tenDonVi = tenDonVi ?? "DonVi";
                        string fileNamePrefix = string.Format("{0}_{1}_{2}", item.SSoChungTu, item.SSoQuyetDinh, StringUtils.ConvertVN(tenDonVi));
                        string fileNameWithoutExtension = StringUtils.CreateExportFileName(fileNamePrefix);
                        results.Add(new ExportResult(fileNameWithoutExtension, fileNameWithoutExtension, null, xlsFile));
                    }
                }
                e.Result = results;
            }, (s, e) =>
            {
                if (e.Error == null)
                {
                    List<ExportResult> result = (List<ExportResult>)e.Result;
                    _exportService.Open(result, ExportType.EXCEL);
                }
                else
                {
                    _logger.Error(e.Error.Message);
                }
                IsLoading = false;
            });
        }

        private List<BhDtPhanBoChungTuChiTietModel> LoadDataExportDetail(BhDtPhanBoChungTuModel item)
        {
            DuToanThuChungTuChiTietCriteria searchCondition = new DuToanThuChungTuChiTietCriteria
            {
                VoucherId = item.Id,
                LNS = item.SDslns,
                YearOfWork = item.INamLamViec,
                VoucherDate = item.DNgayChungTu,
                IdDotNhan = item.IIdDotNhan,
                SoChungTu = item.SSoChungTu
            };

            if (item.ILoaiDuToan.HasValue && EstimateTypeNum.ADJUSTED.Equals((EstimateTypeNum)item.ILoaiDuToan.Value))
            {
                IEnumerable<BhDtPhanBoChungTu> listNhanPhanBo = LoadNhanPhanBo(item.Id.ToString());
                searchCondition.LNS = string.Join(",", listNhanPhanBo.Select(x => x.SDslns));
            }

            List<Core.Domain.Query.BhDtPhanBoChungTuChiTietQuery> listChungTuChiTiet = _dtChungTuChiTietService.FindByCondition(searchCondition).ToList();
            _listChungTuChiTiet = _mapper.Map<List<BhDtPhanBoChungTuChiTietModel>>(listChungTuChiTiet);
            return _listChungTuChiTiet;
        }


        private IEnumerable<BhDtPhanBoChungTu> LoadNhanPhanBo(string idPhanBoDuToan)
        {
            List<BhDttNhanPhanBoMap> dtChungTuMapByIdPhanBoDuToan = _dtChungTuMapService.FindByIdPhanBoDuToan(idPhanBoDuToan).ToList();
            HashSet<string> listIdNhanPhanBo = dtChungTuMapByIdPhanBoDuToan.Select(e => e.IIdCtduToanNhan.ToString()).ToHashSet();

            List<BhDtPhanBoChungTu> listNhanPhanBo = new List<BhDtPhanBoChungTu>();
            if (dtChungTuMapByIdPhanBoDuToan.Count() > 0)
            {
                System.Linq.Expressions.Expression<Func<BhDtPhanBoChungTu, bool>> predicate = PredicateBuilder.True<BhDtPhanBoChungTu>();
                predicate = predicate.And(x => listIdNhanPhanBo.Contains(x.Id.ToString()));
                listNhanPhanBo = _estimationService.FindByCondition(predicate).OrderBy(x => x.DNgayChungTu).ToList();
            }
            return listNhanPhanBo;
        }

        private void CalculateData(List<BhDtPhanBoChungTuChiTietModel> listData)
        {
            listData.Where(x => x.IsHangCha)
                .Select(x =>
                {
                    x.FBHXHNLD = 0;
                    x.FBHXHNSD = 0;
                    x.FBHYTNLD = 0;
                    x.FBHYTNSD = 0;
                    x.FBHTNNLD = 0;
                    x.FBHTNNSD = 0;
                    return x;
                }).ToList();

            foreach (BhDtPhanBoChungTuChiTietModel item in listData.Where(x => x.IsEditable && (x.HasDataChild)))
            {
                CalculateParent(listData, item, item);
            }
        }

        private void CalculateParent(List<BhDtPhanBoChungTuChiTietModel> listData, BhDtPhanBoChungTuChiTietModel currentItem, BhDtPhanBoChungTuChiTietModel seftItem)
        {
            BhDtPhanBoChungTuChiTietModel parrentItem = listData.FirstOrDefault(x => x.IIdMlns == currentItem.IIdMlnsCha);
            if (parrentItem == null) return;
            parrentItem.FBHXHNLD += seftItem.FBHXHNLD;
            parrentItem.FBHXHNSD += seftItem.FBHXHNSD;
            parrentItem.FBHYTNLD += seftItem.FBHYTNLD;
            parrentItem.FBHYTNSD += seftItem.FBHYTNSD;
            parrentItem.FBHTNNLD += seftItem.FBHTNNLD;
            parrentItem.FBHTNNSD += seftItem.FBHTNNSD;
            CalculateParent(listData, parrentItem, seftItem);
        }

        private BhDtPhanBoChungTuModel CalculateTotal(List<BhDtPhanBoChungTuChiTietModel> listData)
        {
            BhDtPhanBoChungTuModel chungTu = new BhDtPhanBoChungTuModel();
            chungTu.FThuBHXHNLD = 0;
            chungTu.FThuBHXHNSD = 0;
            chungTu.FTongBHXH = 0;
            chungTu.FThuBHYTNLD = 0;
            chungTu.FThuBHYTNSD = 0;
            chungTu.FTongBHYT = 0;
            chungTu.FThuBHTNNLD = 0;
            chungTu.FThuBHTNNSD = 0;
            chungTu.FTongBHTN = 0;
            List<BhDtPhanBoChungTuChiTietModel> listChildren = listData.Where(x => x.IsEditable).ToList();
            foreach (BhDtPhanBoChungTuChiTietModel item in listChildren)
            {
                chungTu.FThuBHXHNLD += item.FBHXHNLD;
                chungTu.FThuBHXHNSD += item.FBHXHNSD;
                chungTu.FTongBHXH += item.FThuBHXH;
                chungTu.FThuBHYTNLD += item.FBHYTNLD;
                chungTu.FThuBHYTNSD += item.FBHYTNSD;
                chungTu.FTongBHYT += item.FThuBHYT;
                chungTu.FThuBHTNNLD += item.FBHTNNLD;
                chungTu.FThuBHTNNSD += item.FBHTNNSD;
                chungTu.FTongBHTN += item.FThuBHTN;
            }

            return chungTu;
        }

        private bool CheckIsHasData(BhDtPhanBoChungTuChiTietModel chiTietModel)
        {
            return chiTietModel.HasDataChild;
        }
    }
}
