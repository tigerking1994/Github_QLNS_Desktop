using AutoMapper;
using log4net;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using VTS.QLNS.CTC.App.Command;
using VTS.QLNS.CTC.App.Helper;
using VTS.QLNS.CTC.App.Model;
using VTS.QLNS.CTC.App.Model.Control;
using VTS.QLNS.CTC.App.Service;
using VTS.QLNS.CTC.App.Service.Impl;
using VTS.QLNS.CTC.App.View.SocialInsurance.SocialInsuranceEstimate.PhanBoDuToanChi.ExportReport;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Repository;
using VTS.QLNS.CTC.Core.Service;
using VTS.QLNS.CTC.Utility;
using VTS.QLNS.CTC.Utility.Enum;

namespace VTS.QLNS.CTC.App.ViewModel.SocialInsurance.SocialInsuranceEstimate.PhanBoDuToanChi.ExportReport
{
    public class ExportPhanBoDuToanChiViewModel : DialogViewModelBase<BhPbdtcBHXHModel>
    {
        private readonly ILog _logger;
        private readonly IMapper _mapper;
        private SessionInfo _sessionInfo;
        private readonly ISessionService _sessionService;
        private readonly INsDonViService _donViService;
        private readonly IExportService _exportService;
        private readonly IDanhMucService _danhMucService;
        private readonly IBhDmMucLucNganSachService _bhDmMucLucNganSachService;
        private readonly IPbdtcBHXHService _pbdtcBHXHService;
        private readonly IPbdtcBHXHChiTietRepository _pbdtcBHXHChiTietRepository;

        public ObservableCollection<BhPbdtcBHXHModel> Items;
        public override Type ContentType => typeof(ExportPhanBoDuToanChi);
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

        public ExportPhanBoDuToanChiViewModel(ILog logger,
                                               IMapper mapper,
                                               ISessionService sessionService,
                                               IDanhMucService danhMucService,
                                               INsDonViService donViService,
                                               IExportService exportService,
                                               IBhDmMucLucNganSachService bhDmMucLucNganSachService,
                                               IPbdtcBHXHService pbdtcBHXHService,
                                               IPbdtcBHXHChiTietRepository pbdtcBHXHChiTietRepository)
        {

            _logger = logger;
            _mapper = mapper;
            _sessionService = sessionService;
            _donViService = donViService;
            _danhMucService = danhMucService;
            _exportService = exportService;
            _bhDmMucLucNganSachService = bhDmMucLucNganSachService;
            _pbdtcBHXHService = pbdtcBHXHService;
            _pbdtcBHXHChiTietRepository = pbdtcBHXHChiTietRepository;

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

                string templateFile = "rpt_BH_PhanBoDuToanChi.xlsx";

                int namLamViec = _sessionService.Current.YearOfWork;

                System.Linq.Expressions.Expression<Func<BhDmMucLucNganSach, bool>> predicate_danhmuc = PredicateBuilder.True<BhDmMucLucNganSach>();
                predicate_danhmuc = predicate_danhmuc.And(x => x.INamLamViec == namLamViec);

                List<BhDmMucLucNganSach> listMucLucNganSach = _bhDmMucLucNganSachService.FindByCondition(predicate_danhmuc).Where(x => x.SLNS.StartsWith("901")).OrderBy(x => x.SXauNoiMa).ToList();
                List<DonVi> listNsDonVi = new List<DonVi>();
                listNsDonVi = _donViService.FindByUser(_sessionService.Current.Principal, _sessionService.Current.YearOfWork, string.Join(StringUtils.COMMA, new string[] { LoaiDonVi.NOI_BO, LoaiDonVi.ROOT })).ToList();

                if (listNsDonVi.Any(x => x.Loai == LoaiDonVi.ROOT))
                {
                    System.Linq.Expressions.Expression<Func<DonVi, bool>> predicate = PredicateBuilder.True<DonVi>();
                    predicate = predicate.And(x => x.NamLamViec == _sessionService.Current.YearOfWork);
                    predicate = predicate.And(x => x.Loai == SoChungTuType.EstimateDivision.ToString());
                    predicate = predicate.And(x => x.ITrangThai == NSEntityStatus.ACTIVED);

                    listNsDonVi = _donViService.FindByCondition(predicate).ToList();
                }
                IEnumerable<BhPbdtcBHXHModel> itemsExport = Items.Where(x => x.Selected);

                foreach (BhPbdtcBHXHModel item in itemsExport)
                {

                    List<string> lstDonViPhanBo = item.SID_MaDonVi.Split(",").ToList();

                    foreach (string dv in lstDonViPhanBo)
                    {
                        List<BhPbdtcBHXHChiTietQuery> dataExportDetail = LoadDataExportDetail(item, dv);
                        dataExportDetail = dataExportDetail.Where(x => string.IsNullOrEmpty(x.SM) || !string.IsNullOrEmpty(x.SDuToanChiTietToi)).ToList();
                        DonVi donvi = listNsDonVi.Where(x => x.IIDMaDonVi == dv).FirstOrDefault();
                        List<BhPbdtcBHXHChiTietQuery> lstDataPrent = dataExportDetail.Where(x => x.BHangCha).ToList();
                        List<BhPbdtcBHXHChiTietQuery> lstDataChildbyDonVi = dataExportDetail.Where(x => x.IID_MaDonVi == dv && !x.BHangCha).ToList();

                        lstDataPrent.AddRange(lstDataChildbyDonVi);
                        List<BhPbdtcBHXHChiTietModel> lstData = new List<BhPbdtcBHXHChiTietModel>();
                        lstData = _mapper.Map(lstDataPrent, lstData);

                        if (item.SLNS == LNSValue.LNS_9010001_9010002 || item.SLNS == LNSValue.LNS_901_9010001_9010002)
                        {
                            lstData.ForEach(x =>
                            {
                                if (x.SDuToanChiTietToi == BHXHMLNSChiToi.DuToanChiToi)
                                {
                                    x.BHangCha = false;
                                    x.IsHangCha = false;
                                }
                            });

                            CalculateData(lstData);
                        }

                        lstData = lstData.Where(x => (x.FTienTuChi ?? 0) != 0).OrderBy(x => x.SXauNoiMa).ToList();

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
                        data.Add("LoaiDuToan", VoucherType.BudgetTypeDict.GetValueOrDefault(item.ILoaiDotNhanPhanBo, string.Empty));
                        data.Add("LoaiChungTu", VoucherType.VoucherTypeDict.GetValueOrDefault(item.ILoaiChungTu, string.Empty));
                        data.Add("NguoiTao", item.SNguoiTao);
                        data.Add("NgayTao", DateUtils.Format(item.DNgayTao));


                        data.Add("Items", lstData);
                        data.Add("MLNS", listMucLucNganSach);

                        double? TotalTuChi = lstData?.Where(x => !string.IsNullOrEmpty(x.SDuToanChiTietToi)).Sum(x => x.FTienTuChi);
                        //double? TotalHienVat = lstData?.Where(x => !x.BHangCha).Sum(x => x.FTienHienVat);

                        data.Add("TotalTuChi", string.Format(StringUtils.FORMAT_ZERO, TotalTuChi));

                        List<int> hideColumns = new List<int>();
                        hideColumns.AddRange(ExportExcelHelper<BhPbdtcBHXHChiTietModel>.HideColumn(chiTietToi));

                        string templateFileName = Path.Combine(ExportPrefix.PATH_BH_DT_DTCPBCL, templateFile);
                        FlexCel.Core.ExcelFile xlsFile = _exportService.Export<BhPbdtcBHXHChiTietModel, BhDmMucLucNganSach>(templateFileName, data, hideColumns);
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

        private List<BhPbdtcBHXHChiTietQuery> LoadDataExportDetail(BhPbdtcBHXHModel item, string sMaDonVi)
        {
            List<BhPbdtcBHXHChiTietQuery> lstChungTuChiTiet = _pbdtcBHXHChiTietRepository.ExportExcelPhanBoDuToanChi(item.Id, item.SLNS, item.INamChungTu, sMaDonVi).ToList();
            lstChungTuChiTiet.ForEach(x =>
            {
                x.FTienTuChi = Math.Round(x.FTienTuChi ?? 0, 0, MidpointRounding.AwayFromZero);
            });
            return lstChungTuChiTiet;
        }


        private IEnumerable<NsDtChungTu> LoadNhanPhanBo(string idPhanBoDuToan)
        {

            return null;
        }

        private void CalculateData(List<BhPbdtcBHXHChiTietModel> listData)
        {
            listData.Where(x => x.IsHangCha)
                .Select(x =>
                {
                    x.FTienHienVat = 0;
                    x.FTienTuChi = 0;
                    return x;
                }).ToList();

            foreach (BhPbdtcBHXHChiTietModel item in listData.Where(x => x.IsEditable && (x.FTienHienVat != 0 || x.FTienTuChi != 0)))
            {
                CalculateParent(listData, item, item);
            }
        }

        private void CalculateParent(List<BhPbdtcBHXHChiTietModel> listData, BhPbdtcBHXHChiTietModel currentItem, BhPbdtcBHXHChiTietModel seftItem)
        {
            BhPbdtcBHXHChiTietModel parrentItem = listData.FirstOrDefault(x => x.IID_MLNS == currentItem.IID_MLNS_Cha);
            if (parrentItem == null) return;
            parrentItem.FTienHienVat = (parrentItem.FTienHienVat ?? 0) + (seftItem.FTienHienVat ?? 0);
            parrentItem.FTienTuChi = (parrentItem.FTienTuChi ?? 0) + (seftItem.FTienTuChi ?? 0);

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

        private bool CheckIsHasData(DtChungTuChiTietModel chiTietModel)
        {
            return chiTietModel.FTuChi != 0 || chiTietModel.FHienVat != 0 || chiTietModel.FDuPhong != 0 ||
                   chiTietModel.FHangNhap != 0 || chiTietModel.FHangMua != 0 || chiTietModel.FPhanCap != 0;
        }
    }
}
