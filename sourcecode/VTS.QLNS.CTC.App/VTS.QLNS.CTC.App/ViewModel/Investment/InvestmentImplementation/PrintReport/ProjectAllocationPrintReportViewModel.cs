using AutoMapper;
using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows.Data;
using System.Windows.Forms;
using VTS.QLNS.CTC.App.Command;
using VTS.QLNS.CTC.App.Service;
using VTS.QLNS.CTC.App.Model.Control;
using VTS.QLNS.CTC.App.Model.Report;
using VTS.QLNS.CTC.App.Properties;
using VTS.QLNS.CTC.App.Service;
using VTS.QLNS.CTC.App.View.Investment.InvestmentImplementation.PrintReport;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Service;
using VTS.QLNS.CTC.Core.Domain.Criteria;
using VTS.QLNS.CTC.Utility;
using VTS.QLNS.CTC.App.Helper;
using VTS.QLNS.CTC.App.Service.Impl;
using log4net;
using VTS.QLNS.CTC.Utility.Enum;
using VTS.QLNS.CTC.App.Service.UserFunction;

namespace VTS.QLNS.CTC.App.ViewModel.Investment.InvestmentImplementation.PrintReport
{
    public class ProjectAllocationPrintReportViewModel : GridViewModelBase<RptChiTieuCapPhatDuAn>
    {
        private static string[] lstDonViExclude = new string[] { "0", "1" };

        private readonly ILog _logger;
        private readonly IMapper _mapper;
        private readonly ISessionService _sessionService;
        private readonly INsDonViService _nsDonViService;
        private readonly INsNguonNganSachService _iNsNguonNganSachService;
        private readonly IProjectManagerService _projectManagerService;
        private readonly IExportService _exportService;
        private ICollectionView _projectInformationPrintReportView;
        private ReportChiTieuCapPhatDuAnCriteria _searchCondition;

        public override string FuncCode => NSFunctionCode.INVESTMENT_IMPLEMENTATION_PROJECT_ALLOCATION_PRINT_REPORT;
        public override string GroupName => MenuItemContants.GROUP_REPORT;
        public override string Name => "Báo cáo theo dõi chỉ tiêu,cấp phát dự án";
        public override string Description => "Theo dõi chỉ tiêu-cấp phát dự án, công trình";
        public override Type ContentType => typeof(ProjectAllocationPrintReport);
        public override PackIconKind IconKind => PackIconKind.ClipboardFileOutline;

        private ComboboxItem _cbxLoaiDonViSelected;
        public ComboboxItem CbxLoaiDonViSelected
        {
            get => _cbxLoaiDonViSelected;
            set
            {
                SetProperty(ref _cbxLoaiDonViSelected, value);
            }
        }

        private ObservableCollection<ComboboxItem> _dataDonVi;
        public ObservableCollection<ComboboxItem> DataDonVi
        {
            get => _dataDonVi;
            set => SetProperty(ref _dataDonVi, value);
        }

        private string _searchText;
        public string SearchText
        {
            get => _searchText;
            set => SetProperty(ref _searchText, value);
        }

        private int _yearOfOrigination;
        public int YearOfOrigination
        {
            get => _yearOfOrigination;
            set => SetProperty(ref _yearOfOrigination, value);
        }
        private ComboboxItem _cbxNguonVonSelected;
        public ComboboxItem CbxNguonVonSelected
        {
            get => _cbxNguonVonSelected;
            set
            {
                SetProperty(ref _cbxNguonVonSelected, value);
            }
        }
        private ObservableCollection<ComboboxItem> _cbxNguonVon;
        public ObservableCollection<ComboboxItem> CbxNguonVon
        {
            get => _cbxNguonVon;
            set => SetProperty(ref _cbxNguonVon, value);
        }
        public RelayCommand SearchCommand { get; }
        public RelayCommand PrintBrowserCommand { get; }

        public ProjectAllocationPrintReportViewModel(
            IMapper mapper,
            ILog logger,
            ISessionService sessionService,
            INsDonViService nsDonViService,
            IProjectManagerService projectManagerService,
            INsNguonNganSachService iNsNguonNganSachService,
            IExportService exportService)
        {
            _mapper = mapper;
            _logger = logger;
            _sessionService = sessionService;
            _nsDonViService = nsDonViService;
            _projectManagerService = projectManagerService;
            _iNsNguonNganSachService = iNsNguonNganSachService;
            _exportService = exportService;

            PrintBrowserCommand = new RelayCommand(obj => OnPrint(ExportType.PDF));
            SearchCommand = new RelayCommand(obj => LoadData());
        }

        public override void Init()
        {
            LoadDataDonVi();
            LoadComboBoxNguonNganSach();
            LoadData();
        }
        private void LoadComboBoxNguonNganSach()
        {
            _cbxNguonVon = new ObservableCollection<ComboboxItem>(new List<ComboboxItem>());
            var data = _iNsNguonNganSachService.FindNguonNganSach()
                .Select(n => new ComboboxItem() { ValueItem = (n.IIdMaNguonNganSach ?? 0).ToString(), DisplayItem = n.STen });
            _cbxNguonVon = new ObservableCollection<ComboboxItem>(data);
            OnPropertyChanged(nameof(CbxNguonVon));
        }
        private void ResetConditionSearch()
        {
            _searchCondition = new ReportChiTieuCapPhatDuAnCriteria()
            {
                IdMaDonViQuanLy = _cbxLoaiDonViSelected is null ? null : _nsDonViService.FindByIdDonVi(_cbxLoaiDonViSelected.ValueItem, _sessionService.Current.YearOfWork).IIDMaDonVi,
                NamThucHien = _yearOfOrigination != 0 ? _yearOfOrigination : _sessionService.Current.YearOfWork,
                NguonVonId = _cbxNguonVonSelected is null ? 0 : int.Parse(_cbxNguonVonSelected.ValueItem),
            };
        }

        public override void LoadData(params object[] args)
        {
            ResetConditionSearch();
            List<ReportChiTieuCapPhatDuAnQuery> lstDuAn = _projectManagerService.FindByConditionProjectAllocationReport(_searchCondition).ToList();
            if (lstDuAn != null && lstDuAn.Count > 0)
            {
                var itemSummary = new ReportChiTieuCapPhatDuAnQuery()
                {
                    SDuAnCongTrinh = "TỔNG SỐ",
                    TMDT_NSQP = lstDuAn.Sum(c => c.TMDT_NSQP),
                    TMDT_NSNN = lstDuAn.Sum(c => c.TMDT_NSNN),
                    TMDT_Khac = lstDuAn.Sum(c => c.TMDT_Khac),
                    Tong_TMDT = lstDuAn.Sum(c => c.Tong_TMDT),
                    LuyKeVonNamTruoc = lstDuAn.Sum(c => c.LuyKeVonNamTruoc),
                    ChiTieuNganSachNam = lstDuAn.Sum(c => c.ChiTieuNganSachNam),
                    ThanhToan = lstDuAn.Sum(c => c.ThanhToan),
                    TamUng = lstDuAn.Sum(c => c.TamUng),
                    ThuUng = lstDuAn.Sum(c => c.ThuUng),
                    KeHoachUngNgoaiChiTieu = lstDuAn.Sum(c => c.KeHoachUngNgoaiChiTieu),
                    CapUngNgoaiChiTieu = lstDuAn.Sum(c => c.CapUngNgoaiChiTieu),
                    ThuUngXDCB = lstDuAn.Sum(c => c.ThuUngXDCB),
                    SoUngConPhaiThu = lstDuAn.Sum(c => c.SoUngConPhaiThu),
                    ChiTieuConLaiChuaCap = lstDuAn.Sum(c => c.ChiTieuConLaiChuaCap),
                    SoVonConBoTriTiep = lstDuAn.Sum(c => c.SoVonConBoTriTiep),
                };

                lstDuAn.Insert(0, itemSummary);
            }
            Items = _mapper.Map<ObservableCollection<RptChiTieuCapPhatDuAn>>(lstDuAn);
            _projectInformationPrintReportView = CollectionViewSource.GetDefaultView(Items);
            _projectInformationPrintReportView.Filter = ListSettlementVoucherFilter;

        }

        private void CaculateData()
        {
            var listChildren = Items.Where(x => !(bool)x.IsHangCha).ToList();
            if (listChildren != null && listChildren.Count > 0)
            {
                foreach (var item in listChildren)
                {
                    FindParent(item);
                }
            }
        }

        private void FindParent(RptChiTieuCapPhatDuAn item)
        {
            var parentItemCapPheDuyet = Items.Where(x => (bool)x.IsHangCha && x.MaThuTu == item.MaThuTu).FirstOrDefault();
            if (parentItemCapPheDuyet == null) return;
            var parentItemLoaiCongTrinh = Items.Where(x => (bool)x.IsHangCha && x.MaThuTu == null && x.CT == item.CT).FirstOrDefault();
            CalculateParent(item, parentItemCapPheDuyet);

            if (parentItemLoaiCongTrinh != null)
            {
                CalculateParent(item, parentItemLoaiCongTrinh);
            }

        }

        private void CalculateParent(RptChiTieuCapPhatDuAn child, RptChiTieuCapPhatDuAn parent)
        {
            parent.TMDT_NSQP += child.TMDT_NSQP;
            parent.TMDT_NSNN += child.TMDT_NSNN;
            parent.TMDT_Khac += child.TMDT_Khac;
            parent.Tong_TMDT += child.Tong_TMDT;
            parent.LuyKeVonNamTruoc += child.LuyKeVonNamTruoc;
            parent.ChiTieuNganSachNam += child.ChiTieuNganSachNam;
            parent.ThanhToan += child.ThanhToan;
            parent.TamUng += child.TamUng;
            parent.ThuUng += child.ThuUng;
            parent.KeHoachUngNgoaiChiTieu += child.KeHoachUngNgoaiChiTieu;
            parent.CapUngNgoaiChiTieu += child.CapUngNgoaiChiTieu;
            parent.ThuUngXDCB += child.ThuUngXDCB;
            parent.SoUngConPhaiThu += child.SoUngConPhaiThu;
            parent.ChiTieuConLaiChuaCap += child.ChiTieuConLaiChuaCap;
            parent.SoVonConBoTriTiep += child.SoVonConBoTriTiep;
        }


        private bool ListSettlementVoucherFilter(object obj)
        {
            bool result = true;
            var item = (RptChiTieuCapPhatDuAn)obj;

            if (!string.IsNullOrEmpty(SearchText))
                result = result && item.SDuAnCongTrinh.ToLower().Contains(_searchText.ToLower());

            return result;
        }

        private void LoadDataDonVi()
        {
            _yearOfOrigination = _sessionService.Current.YearOfWork;
            var lstDonVi = _nsDonViService.FindByNamLamViec(_sessionService.Current.YearOfWork).ToList();
            _dataDonVi = _mapper.Map<ObservableCollection<ComboboxItem>>(lstDonVi);
        }

        private void OnPrint(ExportType exportType)
        {
            try
            {
                BackgroundWorkerHelper.Run((s, e) =>
                {
                    IsLoading = true;

                    List<RptChiTieuCapPhatDuAn> lstReport = Items.ToList();
                    Dictionary<string, object> data = new Dictionary<string, object>();
                    FormatNumber formatNumber = new FormatNumber(1, exportType);
                    data.Add("FormatNumber", formatNumber);
                    data.Add("Items", lstReport);
                    data.Add("Cap1", "BỘ QUỐC PHÒNG");
                    data.Add("Cap2", "CỤC TÀI CHÍNH");
                    data.Add("Header1", string.Format("THEO DÕI CHỈ TIÊU - CẤP PHÁT DỰ ÁN, CÔNG TRÌNH {0}", _yearOfOrigination != 0 ? _yearOfOrigination : _sessionService.Current.YearOfWork));
                    data.Add("Header2", string.Format("Đơn vị: {0}", CbxLoaiDonViSelected.DisplayItem));
                    string templateFileName = "TheoDoiChiTieuCapPhat.xls";
                    string fileNamePrefix = "TheoDoiChiTieuCapPhat";
                    string fileNameWithoutExtension = StringUtils.CreateExportFileName(fileNamePrefix);
                    var xlsFile = _exportService.Export<RptChiTieuCapPhatDuAn>(templateFileName, data);
                    e.Result = new ExportResult(fileNameWithoutExtension, fileNameWithoutExtension, null, xlsFile);
                }, (s, e) =>
                {
                    if (e.Error == null)
                    {
                        var result = (ExportResult)e.Result;
                        if (result != null)
                        {
                            _exportService.Open(result, exportType);
                        }
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
    }
}
