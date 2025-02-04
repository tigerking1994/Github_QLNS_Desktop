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
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Service;
using VTS.QLNS.CTC.Utility.Enum;
using VTS.QLNS.CTC.Utility;
using VTS.QLNS.CTC.App.Model.Control;
using VTS.QLNS.CTC.App.Service.UserFunction;
using VTS.QLNS.CTC.Core.Service.Impl;
using System.IO;
using VTS.QLNS.CTC.Core.Domain.Criteria;
using VTS.QLNS.CTC.Core.Domain.Query;

namespace VTS.QLNS.CTC.App.ViewModel.Budget.Allocation.ExportAllocation
{
    public class ExportAllocationViewModel : ViewModelBase
    {
        private readonly ILog _logger;
        private readonly IMapper _mapper;
        private SessionInfo _sessionInfo;
        private readonly ISessionService _sessionService;
        private IDanhMucService _danhMucService;
        private IExportService _exportService;
        private readonly INsMucLucNganSachService _mucLucNganSachService;
        private readonly ISoLieuChiTietPhanCapService _soLieuChiTietPhanCapService;
        private readonly ISktSoLieuService _sktSoLieuService;
        private readonly ISktSoLieuChiTietCanCuDataService _sktSoLieuChiTietCanCuDataService;
        private readonly ICauHinhCanCuService _iCauHinhCanCuService;
        private readonly INsDonViService _nsDonViService;
        private readonly ICpChungTuChiTietService _cpChungTuChiTietService;
        
        public ObservableCollection<AllocationModel> DataAllocation;


        public override Type ContentType => typeof(View.Budget.Allocation.ExportAllocation.ExportAllocation);

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
        private List<DonVi> _listDonVi;
        public RelayCommand ExportCommand { get; }

        public void LoadTieuChis()
        {

            var danhMucCauHinh = _danhMucService.FindByType(TypeDanhMuc.DM_CAUHINH, _sessionService.Current.YearOfWork).ToList();
            if (danhMucCauHinh.Count > 0)
            {
                var danhMucMLNS = _danhMucService.FindByType(TypeDanhMuc.DM_CAUHINH, _sessionService.Current.YearOfWork).Where(n => n.IIDMaDanhMuc == MaDanhMuc.MLNS_CHITIET_TOI).FirstOrDefault();
                string chiTietToi = danhMucMLNS == null ? string.Empty : danhMucMLNS.SGiaTri;
                BTieuChiItems = new ObservableCollection<ComboboxItem>(DynamicMLNS.CreateMLNSReportSetting(chiTietToi, false));
                _bTieuChiSelected = BTieuChiItems != null ? BTieuChiItems[0] : null;
            }

        }

        public ExportAllocationViewModel(ILog logger,
                                         IMapper mapper,
                                         ISessionService sessionService,
                                         IDanhMucService danhMucService,
                                         INsDonViService nsDonViService,
                                         INsQtChungTuChiTietService chungTuChiTietService,
                                         INsMucLucNganSachService mucLucNganSachService,
                                         IExportService exportService,
                                         ISoLieuChiTietPhanCapService soLieuChiTietPhanCapService,
                                         ISktSoLieuService sktSoLieuService,
                                         ISktSoLieuChiTietCanCuDataService sktSoLieuChiTietCanCuDataService,
                                         ICauHinhCanCuService iCauHinhCanCuService,
                                         ICpChungTuChiTietService cpChungTuChiTietService)
        {

            _logger = logger;
            _mapper = mapper;
            _sessionService = sessionService;
            _danhMucService = danhMucService;
            _mucLucNganSachService = mucLucNganSachService;
            _exportService = exportService;
            _nsDonViService = nsDonViService;

            ExportCommand = new RelayCommand(obj => OnExportData());
            _soLieuChiTietPhanCapService = soLieuChiTietPhanCapService;
            _sktSoLieuService = sktSoLieuService;
            _sktSoLieuChiTietCanCuDataService = sktSoLieuChiTietCanCuDataService;
            _iCauHinhCanCuService = iCauHinhCanCuService;
            _cpChungTuChiTietService = cpChungTuChiTietService;
        }

        public override void Init()
        {
            base.Init();
            LoadTieuChis();
            _sessionInfo = _sessionService.Current;

        }

        public void OnExportData()
        {
            try
            {
                BackgroundWorkerHelper.Run((s, e) =>
                {
                    IsLoading = true;

                    List<ExportResult> results = new List<ExportResult>();
                    string templateFileName = Path.Combine(ExportPrefix.PATH_TL_CP, ExportFileName.RPT_CAPPHAT_CHUNGTU_EXPORT);
                    string fileNamePrefix;
                    string fileNameWithoutExtension;

                    List<AllocationModel> listExport = DataAllocation.Where(x => x.Selected).ToList();
                    foreach (AllocationModel item in listExport)
                    {
                        List<string> listIdDonVi = item.IdDonVi.Split(",").ToList();
                        foreach (string idDonVi in listIdDonVi)
                        {
                            DonVi donViChild = _nsDonViService.FindByIdDonVi(idDonVi, _sessionInfo.YearOfWork);
                            DonVi donViParent = _nsDonViService.FindByLoai(LoaiDonVi.ROOT, _sessionInfo.YearOfWork);
                            List<int> hideColumn = new List<int>();
                            if (idDonVi == _sessionInfo.IdDonVi)
                            {
                                hideColumn.Add(9);
                            }


                            List<AllocationDetailModel> listAllocationDetail = GetDetailDataExport(item, idDonVi);
                            switch (BTieuChiSelected.ValueItem)
                            {
                                case nameof(MLNSFiled.NG):
                                    listAllocationDetail = listAllocationDetail.Where(x => string.IsNullOrEmpty(x.Tng)).ToList();
                                    listAllocationDetail.Where(x => !string.IsNullOrEmpty(x.Ng)).Select(x => x.IsHangCha = false).ToList();
                                    break;
                                case nameof(MLNSFiled.TNG):
                                    listAllocationDetail = listAllocationDetail.Where(x => string.IsNullOrEmpty(x.TNG1)).ToList();
                                    listAllocationDetail.Where(x => !string.IsNullOrEmpty(x.Tng)).Select(x => x.IsHangCha = false).ToList();
                                    break;
                                case nameof(MLNSFiled.TNG1):
                                    listAllocationDetail = listAllocationDetail.Where(x => string.IsNullOrEmpty(x.TNG2)).ToList();
                                    listAllocationDetail.Where(x => !string.IsNullOrEmpty(x.TNG1)).Select(x => x.IsHangCha = false).ToList();
                                    break;
                                case nameof(MLNSFiled.TNG2):
                                    listAllocationDetail = listAllocationDetail.Where(x => string.IsNullOrEmpty(x.TNG3)).ToList();
                                    listAllocationDetail.Where(x => !string.IsNullOrEmpty(x.TNG2)).Select(x => x.IsHangCha = false).ToList();
                                    break;
                            }

                            int namLamViec = _sessionInfo.YearOfWork;
                            List<NsMucLucNganSach> mucLucNganSaches = _mucLucNganSachService.FindAll(namLamViec).ToList();
                            Dictionary<string, object> data = new Dictionary<string, object>();
                            FormatNumber formatNumber = new FormatNumber(1, ExportType.EXCEL);
                            data.Add("FormatNumber", formatNumber);
                            data.Add("Header2", donViChild != null ? donViChild.TenDonVi : string.Empty);
                            data.Add("Header1", donViParent != null ? donViParent.TenDonVi : string.Empty);
                            data.Add("TieuDe1", "Chứng từ cấp phát");
                            data.Add("TieuDe2", string.Format("Số chứng từ: {0}", item.SoChungTu));
                            data.Add("ThoiGian", string.Format("Ngày chứng từ: {0}", item.NgayChungTu.HasValue ? item.NgayChungTu.Value.ToString("dd/MM/yyyy") : string.Empty));
                            data.Add("NamLamViec", _sessionInfo.YearOfWork);
                            data.Add("Items", listAllocationDetail);
                            data.Add("MLNS", mucLucNganSaches);
                            double tongTien = (listAllocationDetail != null && listAllocationDetail.Count > 0) ? listAllocationDetail.Where(n => !n.IsHangCha).Select(n => n.TuChi).Sum() : 0;
                            data.Add("TongTienBangChu", StringUtils.NumberToText(tongTien, true));

                            fileNamePrefix = item.SoChungTu;
                            fileNameWithoutExtension = StringUtils.CreateExportFileName(fileNamePrefix);
                            var xlsFile = _exportService.Export<AllocationDetailModel, NsMucLucNganSach>(templateFileName, data, hideColumn);
                            results.Add(new ExportResult(fileNameWithoutExtension, fileNameWithoutExtension, null, xlsFile));
                        }
                    }
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

        public List<AllocationDetailModel> GetDetailDataExport(AllocationModel itemAllocation, string idDonVi)
        {
            AllocationDetailCriteria searchConditon = new AllocationDetailCriteria
            {
                VoucherId = itemAllocation.Id.ToString(),
                LNS = itemAllocation.Lns,
                YearOfWork = _sessionInfo.YearOfWork,
                YearOfBudget = _sessionInfo.YearOfBudget,
                Type = itemAllocation.ILoai,
                BudgetSource = itemAllocation.NguonNganSach.HasValue ? itemAllocation.NguonNganSach.Value : 0,
                AgencyId = idDonVi,
                VoucherDate = itemAllocation.NgayChungTu
            };
            searchConditon.PhanCap = DynamicMLNS.FormatLevel(itemAllocation.ITypeMoTa);
            searchConditon.UserName = _sessionInfo.Principal;
            List<CpChungTuChiTietQuery> data = _cpChungTuChiTietService.FindChungTuChiTietByCondition(searchConditon, false).ToList();
            ProcessSummaryDetailData(ref data);
            List<AllocationDetailModel> listAllocationDetail = _mapper.Map<List<Model.AllocationDetailModel>>(data);
            CalculateData(ref listAllocationDetail);
            listAllocationDetail = listAllocationDetail.Where(x => (x.HienVat != 0 || x.DuToan != 0 || x.DaCap != 0 || x.ConLai != 0 || x.TuChi != 0 || x.DeNghiDonVi != 0)).ToList();
            FormatDetailExport(itemAllocation, ref listAllocationDetail);
            return listAllocationDetail;
        }

        private void ProcessSummaryDetailData(ref List<CpChungTuChiTietQuery> input)
        {
            List<NsMucLucNganSach> dataMLNS = GetChiTietToiMLNS();
            foreach (var mlns in dataMLNS)
            {
                switch (mlns.SCPChiTietToi)
                {
                    case "NG":
                        input.Where(n => n.Lns.Trim() == mlns.XauNoiMa.Trim() && n.XauNoiMa.Trim().Length <= 25).Select(n => { n.BHangCha = true; return n; }).ToList();
                        input.Where(n => n.Lns.Trim() == mlns.XauNoiMa.Trim() && n.XauNoiMa.Trim().Length > 25).Select(n => { n.BHangCha = false; return n; }).ToList();
                        break;
                    case "TM":
                        input.Where(n => n.Lns.Trim() == mlns.XauNoiMa.Trim() && n.XauNoiMa.Trim().Length <= 20).Select(n => { n.BHangCha = true; return n; }).ToList();
                        input.Where(n => n.Lns.Trim() == mlns.XauNoiMa.Trim() && n.XauNoiMa.Trim().Length > 20).Select(n => { n.BHangCha = false; return n; }).ToList();
                        break;
                    case "M":
                        input.Where(n => n.Lns.Trim() == mlns.XauNoiMa.Trim() && n.XauNoiMa.Trim().Length <= 15).Select(n => { n.BHangCha = true; return n; }).ToList();
                        input.Where(n => n.Lns.Trim() == mlns.XauNoiMa.Trim() && n.XauNoiMa.Trim().Length > 15).Select(n => { n.BHangCha = false; return n; }).ToList();
                        break;
                    default:
                        break;
                }
            }
        }

        private List<NsMucLucNganSach> GetChiTietToiMLNS()
        {
            var predicate = PredicateBuilder.True<NsMucLucNganSach>();
            predicate = predicate.And(x => x.NamLamViec == _sessionInfo.YearOfWork);
            predicate = predicate.And(x => x.SCPChiTietToi != null && x.XauNoiMa.Length == 7);
            List<NsMucLucNganSach> data = _mucLucNganSachService.FindByCondition(predicate).ToList();
            return data;
        }

        private void CalculateData(ref List<AllocationDetailModel> data)
        {
            data.Where(x => x.IsHangCha)
                .Select(x => { x.TuChi = 0; x.HienVat = 0; x.DaCap = 0; x.DuToan = 0; x.DeNghiDonVi = 0; return x; }).ToList();
            foreach (var item in data.Where(x => !x.IsHangCha && !x.IsDeleted && (x.HienVat != 0 || x.DuToan != 0 || x.DaCap != 0 || x.ConLai != 0 || x.TuChi != 0 || x.DeNghiDonVi != 0)))
            {
                CalculateParent(ref data, item, item);
            }
        }

        private void CalculateParent(ref List<AllocationDetailModel> data, AllocationDetailModel currentItem, AllocationDetailModel selfItem)
        {
            var parentItem = data.Where(x => x.MlnsId == currentItem.MlnsIdParent).FirstOrDefault();
            if (parentItem == null) return;
            parentItem.TuChi += selfItem.TuChi;
            parentItem.HienVat += selfItem.HienVat;
            parentItem.DuToan += selfItem.DuToan;
            parentItem.DaCap += selfItem.DaCap;
            parentItem.DeNghiDonVi += selfItem.DeNghiDonVi;
            CalculateParent(ref data, parentItem, selfItem);
        }

        private void FormatDetailExport(AllocationModel itemAllocation, ref List<AllocationDetailModel> listAllocationDetail)
        {
            List<CpChungTuChiTietQuery> result = new List<CpChungTuChiTietQuery>();
            string chiTietToi = DynamicMLNS.FormatLevel(itemAllocation.ITypeMoTa);
            switch (chiTietToi)
            {
                case "NG":
                    listAllocationDetail = listAllocationDetail.Where(n =>
                    (!string.IsNullOrEmpty(n.Ng) && string.IsNullOrEmpty(n.Tng)
                                                    && string.IsNullOrEmpty(n.TNG1) && string.IsNullOrEmpty(n.TNG2) && string.IsNullOrEmpty(n.TNG3))
                    || (string.IsNullOrEmpty(n.Ng) && !string.IsNullOrEmpty(n.Ttm))
                    || string.IsNullOrEmpty(n.L) || string.IsNullOrEmpty(n.K) || string.IsNullOrEmpty(n.M)
                    || string.IsNullOrEmpty(n.Tm) || string.IsNullOrEmpty(n.Ttm)).ToList();
                    listAllocationDetail.Where(n => !string.IsNullOrEmpty(n.Ng)).Select(n => { n.IsHangCha = false; return n; }).ToList();
                    listAllocationDetail.Where(n => string.IsNullOrEmpty(n.Ng)).Select(n => { n.IsHangCha = true; return n; }).ToList();
                    break;
                case "M":
                    listAllocationDetail = listAllocationDetail.Where(n =>
                    (!string.IsNullOrEmpty(n.M)
                    && string.IsNullOrEmpty(n.Tm) && string.IsNullOrEmpty(n.Ttm) && string.IsNullOrEmpty(n.Ng)
                    && string.IsNullOrEmpty(n.Tng) && string.IsNullOrEmpty(n.TNG1) && string.IsNullOrEmpty(n.TNG2) && string.IsNullOrEmpty(n.TNG3))
                    || string.IsNullOrEmpty(n.L) || string.IsNullOrEmpty(n.K) || string.IsNullOrEmpty(n.M)).ToList();
                    listAllocationDetail.Where(n => !string.IsNullOrEmpty(n.M)).Select(n => { n.IsHangCha = false; return n; }).ToList();
                    listAllocationDetail.Where(n => string.IsNullOrEmpty(n.M)).Select(n => { n.IsHangCha = true; return n; }).ToList();
                    break;
                case "TM":
                    listAllocationDetail = listAllocationDetail.Where(n =>
                    (!string.IsNullOrEmpty(n.Tm)
                    && string.IsNullOrEmpty(n.Ttm) && string.IsNullOrEmpty(n.Ng)
                    && string.IsNullOrEmpty(n.Tng) && string.IsNullOrEmpty(n.TNG1) && string.IsNullOrEmpty(n.TNG2) && string.IsNullOrEmpty(n.TNG3))
                    || string.IsNullOrEmpty(n.L) || string.IsNullOrEmpty(n.K) || string.IsNullOrEmpty(n.M)
                    || string.IsNullOrEmpty(n.Tm)).ToList();
                    listAllocationDetail.Where(n => !string.IsNullOrEmpty(n.Tm)).Select(n => { n.IsHangCha = false; return n; }).ToList();
                    listAllocationDetail.Where(n => string.IsNullOrEmpty(n.Tm)).Select(n => { n.IsHangCha = true; return n; }).ToList();
                    break;
                default:
                    break;
            }
            listAllocationDetail = listAllocationDetail.OrderBy(x => x.XauNoiMa).ToList();
        }

    }
}
