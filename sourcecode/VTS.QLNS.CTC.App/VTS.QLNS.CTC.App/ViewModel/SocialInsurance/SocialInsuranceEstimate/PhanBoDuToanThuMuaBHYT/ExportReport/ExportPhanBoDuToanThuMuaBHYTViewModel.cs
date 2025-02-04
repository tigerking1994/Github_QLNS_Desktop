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
using VTS.QLNS.CTC.App.View.SocialInsurance.SocialInsuranceEstimate.PhanBoDuToanThuMuaBHYT.ExportReport;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Service;
using VTS.QLNS.CTC.Utility;
using VTS.QLNS.CTC.Utility.Enum;

namespace VTS.QLNS.CTC.App.ViewModel.SocialInsurance.SocialInsuranceEstimate.PhanBoDuToanThuMuaBHYT.ExportReport
{
    public class ExportPhanBoDuToanThuMuaBHYTViewModel : DialogViewModelBase<BhPbdttmBHYTModel>
    {
        private readonly ILog _logger;
        private readonly IMapper _mapper;
        private SessionInfo _sessionInfo;
        private readonly ISessionService _sessionService;
        private readonly INsDonViService _donViService;
        private readonly IExportService _exportService;
        private readonly IDanhMucService _danhMucService;

        private readonly IBhDmMucLucNganSachService _bhDmMucLucNganSachService;
        private readonly IPbdttmBHYTChiTietService _pbdttmBHYTChiTietService;
        private readonly IPbdttmBHYTService _pbdttmBHYTService;

        public ObservableCollection<BhPbdttmBHYTModel> Items;
        public override Type ContentType => typeof(ExportPhanBoDuToanThuMuaBHYT);
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

        public List<BhPbdtcBHXHChiTietModel> _listChungTuChiTiet;

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

        public ExportPhanBoDuToanThuMuaBHYTViewModel(ILog logger,
                                               IMapper mapper,
                                               ISessionService sessionService,
                                               IDanhMucService danhMucService,
                                               INsDonViService donViService,
                                               IExportService exportService,
                                               IBhDmMucLucNganSachService bhDmMucLucNganSachService,
                                               IPbdttmBHYTService pbdttmBHYTService,
                                               IPbdttmBHYTChiTietService pbdttmBHYTChiTietService)
        {

            _logger = logger;
            _mapper = mapper;
            _sessionService = sessionService;
            _donViService = donViService;
            _danhMucService = danhMucService;
            _exportService = exportService;
            _bhDmMucLucNganSachService = bhDmMucLucNganSachService;
            _pbdttmBHYTService = pbdttmBHYTService;
            _pbdttmBHYTChiTietService = pbdttmBHYTChiTietService;

            ExportCommand = new RelayCommand(obj => OnExportGridData());
        }

        public override void Init()
        {
            base.Init();
            LoadTieuChis();
            _sessionInfo = _sessionService.Current;
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

                string templateFileName = "rpt_BH_PhanBoDuToanThuMuaBHYT.xlsx";

                int namLamViec = _sessionService.Current.YearOfWork;

                System.Linq.Expressions.Expression<Func<BhDmMucLucNganSach, bool>> predicate_danhmuc = PredicateBuilder.True<BhDmMucLucNganSach>();
                predicate_danhmuc = predicate_danhmuc.And(x => x.INamLamViec == namLamViec);

                List<BhDmMucLucNganSach> listMucLucNganSach = _bhDmMucLucNganSachService.FindByCondition(predicate_danhmuc).Where(x => x.SLNS.StartsWith("903")).OrderBy(x => x.SXauNoiMa).ToList();

                List<DonVi> lstDonVi = _donViService.FindAll().Where(x => x.NamLamViec == namLamViec).ToList();

                IEnumerable<BhPbdttmBHYTModel> itemsExport = Items.Where(x => x.Selected);
                Dictionary<string, DonVi> dictDonVi = _donViService.FindByListIdDonVi(string.Join(",", itemsExport.Select(x => x.SDS_IDMaDonVi)), namLamViec)
                   .GroupBy(x => x.IIDMaDonVi)
                   .ToDictionary(x => x.Key, x => x.First());


                foreach (BhPbdttmBHYTModel item in itemsExport)
                {
                    List<BhPbdttmBHYTChiTietQuery> dataExportDetail = LoadDataExportDetail(item);

                    List<string> lstDonViPhanBo = item.SDS_IDMaDonVi.Split(",").ToList();

                    foreach (string dv in lstDonViPhanBo)
                    {
                        DonVi donvi = lstDonVi.Where(x => x.IIDMaDonVi == dv).FirstOrDefault();
                        List<BhPbdttmBHYTChiTietQuery> lstDataPrent = dataExportDetail.Where(x => x.BHangCha).ToList();
                        List<BhPbdttmBHYTChiTietQuery> lstDataChildbyDonVi = dataExportDetail.Where(x => !x.BHangCha && x.IID_MaDonVi == dv).ToList();

                        lstDataPrent.AddRange(lstDataChildbyDonVi);
                        List<BhPbdttmBHYTChiTietModel> lstData = new List<BhPbdttmBHYTChiTietModel>();
                        lstData = _mapper.Map(lstDataPrent, lstData);
                        CalculateData(lstData);
                        lstData = lstData.Where(x => x.FDuToan > 0).OrderBy(x => x.SXauNoiMa).ToList();

                        Dictionary<string, object> data = new Dictionary<string, object>();
                        data.Add("Cap1", itemDanhMuc != null ? itemDanhMuc.SGiaTri.ToUpper() : "");
                        data.Add("Cap2", _sessionService.Current.TenDonVi.ToUpper());
                        data.Add("TitleFirst", $"DỰ TOÁN CHI NGÂN SÁCH NĂM {_sessionService.Current.YearOfWork}");
                        data.Add("TitleSecond", $"(Kèm theo Quyết định số: {item.SSoQuyetDinh}, ngày: {DateUtils.Format(item.DNgayQuyetDinh)})");

                        data.Add("HeaderTenDonVi", $"Đơn vị: {donvi?.IIDMaDonVi.PadLeft(3, '0')}{StringUtils.DIVISION}{donvi?.TenDonVi}");
                        data.Add("TenDonVi", $"{donvi?.IIDMaDonVi.PadLeft(3, '0')}{StringUtils.DIVISION}{donvi?.TenDonVi}");
                        data.Add("SoChungTu", item.SSoChungTu);
                        data.Add("NgayChungTu", DateUtils.Format(item.DNgayChungTu));
                        data.Add("SoQuyetDinh", item.SSoQuyetDinh);
                        data.Add("NgayQuyetDinh", DateUtils.Format(item.DNgayQuyetDinh));
                        data.Add("MoTa", item.SMoTa);
                        data.Add("LoaiDuToan", VoucherType.BudgetTypeDict.GetValueOrDefault(item.ILoaiDuToan, string.Empty));
                        data.Add("NguoiTao", item.SNguoiTao);
                        data.Add("NgayTao", DateUtils.Format(item.DNgayTao));


                        data.Add("Items", lstData);
                        data.Add("MLNS", listMucLucNganSach);

                        double? TotalDuToan = lstData?.Where(x => !x.BHangCha).Sum(x => x.FDuToan);


                        data.Add("TotalDuToan", string.Format(StringUtils.FORMAT_ZERO, TotalDuToan));

                        List<int> hideColumns = new List<int>();
                        hideColumns.AddRange(ExportExcelHelper<BhPbdttmBHYTChiTietModel>.HideColumn(chiTietToi));


                        FlexCel.Core.ExcelFile xlsFile = _exportService.Export<BhPbdttmBHYTChiTietModel, BhDmMucLucNganSach>(templateFileName, data, hideColumns);
                        FlexCel.Core.TXlsNamedRange nameRange = xlsFile.GetNamedRange(1);
                        nameRange.Comment = "Workbook";
                        xlsFile.SetNamedRange(nameRange);
                        xlsFile.SetNamedRange(new FlexCel.Core.TXlsNamedRange("__Area_Items__", 1, 0, "=1:2,A:B"));
                        xlsFile.SetCellValue(50, 50, "CheckSum");
                        xlsFile.SetRowHidden(50, true);
                        string fileNamePrefix = string.Format("{0}_{1}_{2}", item.SSoChungTu, item.SSoQuyetDinh, StringUtils.ConvertVN(donvi?.TenDonVi));
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

        private List<BhPbdttmBHYTChiTietQuery> LoadDataExportDetail(BhPbdttmBHYTModel item)
        {
            List<BhPbdttmBHYTChiTietQuery> lstChungTuChiTiet = _pbdttmBHYTChiTietService.ExportExcelPhanBoDuToanChi(item.Id, item.SDSLNS, item.INamLamViec).ToList();
            return lstChungTuChiTiet;
        }


        private void CalculateData(List<BhPbdttmBHYTChiTietModel> listData)
        {
            listData.Where(x => x.IsHangCha)
                .Select(x =>
                {
                    x.FDuToan = 0;
                    return x;
                }).ToList();

            foreach (BhPbdttmBHYTChiTietModel item in listData.Where(x => !x.BHangCha && (x.FDuToan != 0)))
            {
                CalculateParent(listData, item, item);
            }
        }

        private void CalculateParent(List<BhPbdttmBHYTChiTietModel> listData, BhPbdttmBHYTChiTietModel currentItem, BhPbdttmBHYTChiTietModel seftItem)
        {
            BhPbdttmBHYTChiTietModel parrentItem = listData.FirstOrDefault(x => x.IID_MLNS == currentItem.IID_MLNS_Cha);
            if (parrentItem == null) return;
            parrentItem.FDuToan = (parrentItem.FDuToan ?? 0) + (seftItem.FDuToan ?? 0);

            CalculateParent(listData, parrentItem, seftItem);
        }
        //private DtChungTuModel CalculateTotal(List<DtChungTuChiTietModel> listData)
        //{
        //    DtChungTuModel chungTu = new DtChungTuModel();

        //    var listChildren = listData.Where(x => x.IsEditable).ToList();
        //    foreach (var item in listChildren)
        //    {
        //        chungTu.FTongTuChi += item.FTuChi;
        //        chungTu.FTongHienVat += item.FHienVat;
        //        chungTu.FTongHangNhap += item.FHangNhap;
        //        chungTu.FTongHangMua += item.FHangMua;
        //        chungTu.FTongPhanCap += item.FPhanCap;
        //    }

        //    return chungTu;
        //}

        //private bool CheckIsHasData(DtChungTuChiTietModel chiTietModel)
        //{
        //    return chiTietModel.FTuChi != 0 || chiTietModel.FHienVat != 0 || chiTietModel.FDuPhong != 0 ||
        //           chiTietModel.FHangNhap != 0 || chiTietModel.FHangMua != 0 || chiTietModel.FPhanCap != 0;
        //}
    }
}
