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
using VTS.QLNS.CTC.Core.Domain.Criteria;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Service;
using VTS.QLNS.CTC.Utility.Enum;
using VTS.QLNS.CTC.Utility;
using VTS.QLNS.CTC.App.Model.Control;
using ControlzEx.Standard;
using VTS.QLNS.CTC.App.Model.Report;
using VTS.QLNS.CTC.Core.Domain.Query;
using System.IO;
using System.ComponentModel;
using VTS.QLNS.CTC.App.Properties;

namespace VTS.QLNS.CTC.App.ViewModel.Budget.Estimate.AdjustedEstimate.ExportReport
{
    public class ExportAdjustedEstimateViewModel : DialogViewModelBase<DcChungTuModel>
    {
        private readonly ILog _logger;
        private readonly IMapper _mapper;
        private SessionInfo _sessionInfo;
        private readonly ISessionService _sessionService;
        private readonly IDanhMucService _danhMucService;
        private readonly INsDonViService _donViService;
        private readonly INsDcChungTuChiTietService _chungTuChiTietService;
        private readonly INsMucLucNganSachService _mucLucNganSachService;
        private readonly IExportService _exportService;

        private List<NsMucLucNganSach> _mucLucNganSachs;
        public ObservableCollection<DtChungTuModel> Items;
        public override Type ContentType => typeof(View.Budget.Estimate.AdjustedEstimate.ExportReport.ExportAdjustedEstimate);
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

        private Dictionary<string, object> _donViItems;

        public Dictionary<string, object> DonViItems
        {
            get => _donViItems;
            set => SetProperty(ref _donViItems, value);
        }

        private Dictionary<string, object> _selectedDonViItems;
        public Dictionary<string, object> SelectedDonViItems
        {
            get => _selectedDonViItems;
            set
            {
                SetProperty(ref _selectedDonViItems, value);
            }
        }

        private ComboboxItem _exportEstimateSettlementTypeSelected;
        public ComboboxItem ExportEstimateSettlementTypeSelected
        {
            get => _exportEstimateSettlementTypeSelected;
            set
            {
                SetProperty(ref _exportEstimateSettlementTypeSelected, value);
                if (_exportEstimateSettlementTypeSelected != null)
                    GetDataDonVi();
            }
        }

        private ObservableCollection<ComboboxItem> _exportEstimateSettlementTypes;
        public ObservableCollection<ComboboxItem> ExportEstimateSettlementTypes
        {
            get => _exportEstimateSettlementTypes;
            set => SetProperty(ref _exportEstimateSettlementTypes, value);
        }

        public List<DcChungTuModel> ListChungTu { get; set; }
        public List<DcChungTuModel> ListChungTuTH { get; set; }
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

        public ExportAdjustedEstimateViewModel(ILog logger,
                                               IMapper mapper,
                                               ISessionService sessionService,
                                               IDanhMucService danhMucService,
                                               INsDonViService donViService,
                                               INsDcChungTuChiTietService chungTuChiTietService,
                                               INsMucLucNganSachService mucLucNganSachService,
                                               IExportService exportService)
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
        }

        public override void Init()
        {
            base.Init();
            LoadTieuChis();
            //GetDataDonVi();
            LoadEstimateSettlementType();
            _sessionInfo = _sessionService.Current;
            _mucLucNganSachs = _mucLucNganSachService.FindAll(_sessionInfo.YearOfWork).ToList();
            if (SelectedDonViItems != null)
                SelectedDonViItems.Clear();
        }

        private void OnExportGridData()
        {
            if (SelectedDonViItems == null || SelectedDonViItems.Count() == 0)
            {
                MessageBoxHelper.Warning(Resources.MsgCheckDonVi);
                return;
            }
            BackgroundWorkerHelper.Run((s, e) =>
            {
                IsProcessReport = true;
                var lstExportVouchers = ListChungTu.Where(x => SelectedDonViItems.ContainsValue(x.IIdMaDonVi) && x.ILoaiDuKien == int.Parse(_exportEstimateSettlementTypeSelected.ValueItem)).ToList();
                string donVi1 = _danhMucService.FindDonViQuanLy(_sessionService.Current.YearOfWork).ToUpper();
                string donVi2 = _sessionInfo.TenDonVi;
                List<ExportResult> results = new List<ExportResult>();
                string templateFileName = Path.Combine(ExportPrefix.PATH_TL_DT, ExportFileName.RPT_NS_DUTOAN_DIEUCHINH_TONGHOP);
                string fileNamePrefix;
                string fileNameWithoutExtension;
                int count = lstExportVouchers.Select(x => x.IIdMaDonVi.Split(",")).SelectMany(x => x).Count();
                int index = 0;
                foreach (DcChungTuModel item in lstExportVouchers)
                {
                    (s as BackgroundWorker).ReportProgress((index++) * 100 / count, null);
                    var lstExportData = GetAdjustedEstimateDetailSummary(item);
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
                        MLNS = mucLucNganSaches
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

                    fileNamePrefix = item.SSoChungTu + "_" + item.STenDonVi;
                    fileNameWithoutExtension = StringUtils.CreateExportFileName(fileNamePrefix);
                    var chiTietToi = _bTieuChiSelected != null ? ConvertChiTietToi(_bTieuChiSelected.ValueItem) : "Ngành";
                    List<int> hideColumns = ExportExcelHelper<DuToanChiTieuToBia>.HideColumnFullNameMLNS(chiTietToi);
                    if (hideColumns.Any())
                    {
                        for (int i = 0; i < hideColumns.Count; i++)
                        {
                            hideColumns[i] += 1;
                        }
                    }
                    FlexCel.Core.ExcelFile xlsFile = _exportService.Export<DcChungTuChiTietModel, NsMucLucNganSach>(templateFileName, data, hideColumns);
                    FlexCel.Core.TXlsNamedRange nameRange = xlsFile.GetNamedRange(1);
                    nameRange.Comment = "Workbook";
                    xlsFile.SetNamedRange(nameRange);
                    xlsFile.SetNamedRange(new FlexCel.Core.TXlsNamedRange("__Area_Items__", 1, 0, "=1:2,A:B"));
                    xlsFile.SetCellValue(50, 50, "CheckSum");
                    xlsFile.SetRowHidden(50, true);
                    results.Add(new ExportResult(fileNameWithoutExtension, fileNameWithoutExtension, null, xlsFile));
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
                IsProcessReport = false;
            }, (s, e) =>
            {
                ProgressValue = e.ProgressPercentage;
            });
        }

        private void GetDataDonVi()
        {
            var idDonVis = ListChungTu.Where(x => x.ILoaiDuKien == int.Parse(_exportEstimateSettlementTypeSelected.ValueItem)).Select(x => x.IIdMaDonVi.Split(",")).SelectMany(x => x).Distinct();
            var listDonVi = _donViService.FindByListIdDonVi(idDonVis, _sessionService.Current.YearOfWork).ToList();
            DonViItems = listDonVi.ToDictionary(x => $"{x.IIDMaDonVi} - {x.TenDonVi}", x => x.IIDMaDonVi as object);
        }

        private List<DcChungTuChiTietModel> GetAdjustedEstimateDetailSummary(DcChungTuModel model)
        {
            EstimationVoucherDetailCriteria searchCondition = new EstimationVoucherDetailCriteria
            {
                VoucherId = model.Id,
                LNS = model.SDslns,
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

        private void LoadEstimateSettlementType()
        {
            var cbxVoucher = new List<ComboboxItem>
            {
                new ComboboxItem {DisplayItem = VoucherType.EstimateSettlementTypeName[EstimateSettlementType.SIX_MONTH], ValueItem = ((int)EstimateSettlementType.SIX_MONTH).ToString()},
                new ComboboxItem {DisplayItem = VoucherType.EstimateSettlementTypeName[EstimateSettlementType.NINE_MONTH], ValueItem = ((int)EstimateSettlementType.NINE_MONTH).ToString()}
            };
            ExportEstimateSettlementTypes = new ObservableCollection<ComboboxItem>(cbxVoucher);
            _exportEstimateSettlementTypeSelected = ExportEstimateSettlementTypes.ElementAt(0);
            if (_exportEstimateSettlementTypeSelected != null)
                GetDataDonVi();
        }

        private string ConvertChiTietToi(string maCiTietToi)
        {
            string chiTietToi = maCiTietToi;
            switch (maCiTietToi.ToUpper())
            {
                case "NG":
                    chiTietToi = "Ngành";
                    break;
                case "TNG":
                    chiTietToi = "Tiểu ngành";
                    break;
                case "TNG1":
                    chiTietToi = "Tiểu ngành 1";
                    break;
                case "TNG2":
                    chiTietToi = "Tiểu ngành 2";
                    break;
                case "TNG3":
                    chiTietToi = "Tiểu ngành 3";
                    break;
            }
            return chiTietToi;
        }
    }
}
