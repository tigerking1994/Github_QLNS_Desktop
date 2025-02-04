using AutoMapper;
using log4net;
using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Windows.Data;
using VTS.QLNS.CTC.App.Command;
using VTS.QLNS.CTC.App.Helper;
using VTS.QLNS.CTC.App.Model;
using VTS.QLNS.CTC.App.Model.Control;
using VTS.QLNS.CTC.App.Model.Report;
using VTS.QLNS.CTC.App.Service;
using VTS.QLNS.CTC.App.Service.Impl;
using VTS.QLNS.CTC.App.Service.UserFunction;
using VTS.QLNS.CTC.App.ViewModel.Category;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Criteria;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Service;
using VTS.QLNS.CTC.Utility;
using VTS.QLNS.CTC.Utility.Enum;

namespace VTS.QLNS.CTC.App.ViewModel.Budget.Estimate.Division.PrintReport
{
    public class PrintReportPublicFinanceViewModel : ReportViewModelBase<DtChungTuModel, DtChungTuChiTietModel, PrintPublicFinanceQuery>
    {
        private readonly ISessionService _sessionService;
        private readonly IMapper _mapper;
        private ILog _logger;
        private ICollectionView _lnsView;
        private readonly INsDtChungTuService _dtChungTuService;
        private readonly INsDtChungTuChiTietService _dtChungTuChiTietService;
        private readonly INsMucLucNganSachService _nsMucLucNganSachService;
        private readonly IDmChuKyService _dmChuKyService;
        private readonly IDmCongKhaiTaiChinhService _dmCongKhaiTaiChinhService;
        private readonly IDanhMucService _danhMucService;
        private DmChuKyDialogViewModel DmChuKyDialogViewModel;
        private DmChuKy _dmChuKy;
        private string _cap1;
        private string _diaDiem;

        public override string Name
        {
            get => DivisionEstimateScreen.DivisionEstimatePrintTypeName[(int)DivisionEstimatePrintType.PUBLIC_FINANCE];
        }

        public override string Title
        {
            get => DivisionEstimateScreen.DivisionEstimatePrintTypeTitle[(int)DivisionEstimatePrintType.PUBLIC_FINANCE];
        }

        public override string Description
        {
            get => DivisionEstimateScreen.DivisionEstimatePrintTypeDescription[(int)DivisionEstimatePrintType.PUBLIC_FINANCE];
        }

        public override Type ContentType => typeof(View.Budget.Estimate.Division.PrintReport.PrintReportPublicFinance);

        private bool _isExportData;
        public bool IsExportData
        {
            get => ListPublic.Any() && ListPublic.Any(x => x.IsSelected);
            set => SetProperty(ref _isExportData, value);
        }

        private ObservableCollection<ComboboxItem> _DataDonViTinh;
        public ObservableCollection<ComboboxItem> DataDonViTinh
        {
            get => _DataDonViTinh;
            set => SetProperty(ref _DataDonViTinh, value);
        }

        private ComboboxItem _selectedDonViTinh;
        public ComboboxItem SelectedDonViTinh
        {
            get => _selectedDonViTinh;
            set => SetProperty(ref _selectedDonViTinh, value);
        }

        // start handle chon dot
        private ObservableCollection<ComboboxManyItem> _dataDot = new ObservableCollection<ComboboxManyItem>();
        public ObservableCollection<ComboboxManyItem> DataDot
        {
            get => _dataDot;
            set => SetProperty(ref _dataDot, value);
        }

        private ComboboxManyItem _dataDotSelected;
        public ComboboxManyItem DataDotSelected
        {
            get => _dataDotSelected;
            set
            {
                SetProperty(ref _dataDotSelected, value);
                if (_dataDotSelected != null)
                {
                    LoadPublicFinances();
                }
            }
        }

        private bool _checkPrintAccumulation;
        public bool CheckPrintAccumulation
        {
            get => _checkPrintAccumulation;
            set
            {
                SetProperty(ref _checkPrintAccumulation, value);
                LoadPublicFinances();
            }
        }

        private string _txtTitleFirst;
        public string TxtTitleFirst
        {
            get => _txtTitleFirst;
            set => SetProperty(ref _txtTitleFirst, value);
        }

        private string _txtTitleSecond;
        public string TxtTitleSecond
        {
            get => _txtTitleSecond;
            set => SetProperty(ref _txtTitleSecond, value);
        }

        private string _txtTitleThird;
        public string TxtTitleThird
        {
            get => _txtTitleThird;
            set => SetProperty(ref _txtTitleThird, value);
        }

        private bool _checkPrintTNG;
        public bool CheckPrintTNG
        {
            get => _checkPrintTNG;
            set => SetProperty(ref _checkPrintTNG, value);
        }

        // start handle cover sheet
        private ObservableCollection<NsDanhMucCongKhaiCustomModel> _listPublic = new ObservableCollection<NsDanhMucCongKhaiCustomModel>();
        public ObservableCollection<NsDanhMucCongKhaiCustomModel> ListPublic
        {
            get => _listPublic;
            set => SetProperty(ref _listPublic, value);
        }

        private string _labelSelectedCountLNS;
        public string LabelSelectedCountLNS
        {
            get => $"CHỌN DM ({ListPublic.Count(item => item.IsSelected)}/{ListPublic.Count})";
            set => SetProperty(ref _labelSelectedCountLNS, value);
        }

        private bool _selectAllFinance;
        public bool SelectAllFinance
        {
            get => ListPublic.Any() && ListPublic.All(item => item.IsSelected);
            set
            {
                SetProperty(ref _selectAllFinance, value);
                foreach (var item in ListPublic) item.IsSelected = _selectAllFinance;
            }
        }

        private string _searchLNS;
        public string SearchLNS
        {
            get => _searchLNS;
            set
            {
                if (SetProperty(ref _searchLNS, value))
                {
                    _lnsView.Refresh();
                }
            }
        }

        public RelayCommand ConfigSignCommand { get; }
        public RelayCommand ExportExcelCommand { get; }
        public RelayCommand ExportPDFCommand { get; }
        public RelayCommand PrintCommand { get; }

        public PrintReportPublicFinanceViewModel(
            IMapper mapper,
            ILog logger,
            INsDtChungTuService dtChungTuService,
            ISessionService sessionService,
            INsDtChungTuChiTietService dtChungTuChiTietService,
            INsMucLucNganSachService nsMucLucNganSachService,
            IDanhMucService danhMucService,
            IExportService exportService,
            IDmChuKyService dmChuKyService,
            IDmCongKhaiTaiChinhService dmCongKhaiTaiChinhService,
            DmChuKyDialogViewModel dmChuKyDialogViewModel) : base(exportService, danhMucService, sessionService)
        {
            _sessionService = sessionService;
            _mapper = mapper;
            _logger = logger;
            _dtChungTuService = dtChungTuService;
            _dtChungTuChiTietService = dtChungTuChiTietService;
            _nsMucLucNganSachService = nsMucLucNganSachService;
            _dmChuKyService = dmChuKyService;
            _dmCongKhaiTaiChinhService = dmCongKhaiTaiChinhService;
            _danhMucService = danhMucService;
            DmChuKyDialogViewModel = dmChuKyDialogViewModel;
            ConfigSignCommand = new RelayCommand(obj => OnConfigSign());
            PrintCommand = new RelayCommand(obj => OnExportFile(ExportType.PDF));
            ExportExcelCommand = new RelayCommand(obj => OnExportFile(ExportType.EXCEL));
            ExportPDFCommand = new RelayCommand(obj => OnExportFile(ExportType.PDF));
        }

        public override void Init()
        {
            if (Model == null)
            {
                Model = new DtChungTuModel();
            }

            LoadDataDot();
            if (!Models.Any())
            {
                return;
            }
            var danhMucDiaDiem = _danhMucService.FindByType(TypeDanhMuc.DM_CAUHINH, _sessionService.Current.YearOfWork).Where(n => n.IIDMaDanhMuc == MaDanhMuc.DIADIEM).FirstOrDefault();
            _diaDiem = danhMucDiaDiem == null ? string.Empty : danhMucDiaDiem.SGiaTri;
            InitReportDefaultDate();
            LoadTieuDe();
            LoadDonViTinh();
            LoadPaperPrintTypes();
            LoadCatUnitTypes();
            LoadPublicFinances();
            LoadDanhMuc();
        }

        private void LoadTieuDe()
        {
            _dmChuKy = _dmChuKyService.FindByCondition(x => x.IdType.Equals(TypeChuKy.RPT_NS_PHANBODUTOAN_CONGKHAITAICHINH) && x.ITrangThai == StatusType.ACTIVE).FirstOrDefault();
            if (_dmChuKy == null)
                _dmChuKy = new DmChuKy();
            if (!string.IsNullOrEmpty(_dmChuKy.TieuDe1MoTa))
                TxtTitleFirst = _dmChuKy.TieuDe1MoTa;
            if (!string.IsNullOrEmpty(_dmChuKy.TieuDe2MoTa))
                TxtTitleSecond = _dmChuKy.TieuDe2MoTa;
            if (!string.IsNullOrEmpty(_dmChuKy.TieuDe3MoTa))
            {
                TxtTitleThird = _dmChuKy.TieuDe3MoTa;
            }
        }

        private void LoadDanhMuc()
        {
            var danhMucQuanLy = _danhMucService.FindByType(TypeDanhMuc.DM_CAUHINH, _sessionService.Current.YearOfWork).Where(n => n.IIDMaDanhMuc == MaDanhMuc.DV_QUANLY).FirstOrDefault();
            _cap1 = danhMucQuanLy == null ? string.Empty : danhMucQuanLy.SGiaTri;
            var danhMucDiaDiem = _danhMucService.FindByType(TypeDanhMuc.DM_CAUHINH, _sessionService.Current.YearOfWork).Where(n => n.IIDMaDanhMuc == MaDanhMuc.DIADIEM).FirstOrDefault();
            _diaDiem = danhMucDiaDiem == null ? string.Empty : danhMucDiaDiem.SGiaTri;
        }

        private void LoadDataDot()
        {
            if (Models == null || !Models.Any())
            {
                var predicate = CreatePredicateChungTuIndex();
                var listDtChungTu = _dtChungTuService.FindByCondition(predicate).OrderBy(x => x.DNgayChungTu).ToList();
                Models = _mapper.Map<ObservableCollection<DtChungTuModel>>(listDtChungTu);
            }

            DataDot = _mapper.Map<ObservableCollection<ComboboxManyItem>>(Models);
            var ordered = DataDot.OrderByDescending(c => DateTime.Parse(c.DisplayItem2)).ToList();
            DataDot = new ObservableCollection<ComboboxManyItem>(ordered);
            if (DataDot != null && DataDot.Count() > 0)
                _dataDotSelected = DataDot.FirstOrDefault();
        }

        private Expression<Func<NsDtChungTu, bool>> CreatePredicateChungTuIndex()
        {
            var predicate = PredicateBuilder.True<NsDtChungTu>();
            predicate = predicate.And(x => x.INamLamViec == _sessionService.Current.YearOfWork);
            predicate = predicate.And(x => x.IIdMaNguonNganSach == _sessionService.Current.Budget);
            predicate = predicate.And(x => x.INamNganSach == _sessionService.Current.YearOfBudget);
            predicate = predicate.And(x => x.ILoai == SoChungTuType.EstimateDivision);
            predicate = predicate.And(x => !string.IsNullOrEmpty(x.IIdDotNhan));
            if (Model != null && Model.ILoaiChungTu.HasValue)
            {
                predicate = predicate.And(x => x.ILoaiChungTu.HasValue && x.ILoaiChungTu.Equals(Model.ILoaiChungTu));
            }
            return predicate;
        }

        private void OrderByTreeDanhMucCongKhai(List<NsDanhMucCongKhaiCustomModel> lstDanhMucModel)
        {
            foreach (var item in lstDanhMucModel.Where(x => x.iID_DMCongKhai_Cha == null))
            {
                AddListTreeChilDanhMucCongKhai(item, lstDanhMucModel);

            }
        }

        private void AddListTreeChilDanhMucCongKhai(NsDanhMucCongKhaiCustomModel danhmuc, List<NsDanhMucCongKhaiCustomModel> lstDanhMucModel)
        {
            if (lstDanhMucModel.Any(n => n.iID_DMCongKhai_Cha == danhmuc.Id))
            {
                foreach (var item in lstDanhMucModel.Where(n => n.iID_DMCongKhai_Cha == danhmuc.Id))
                {

                    item.sSpace = danhmuc.sSpace + StringUtils.SPACE_DIVISION;
                    item.sMoTa = item.sSpace + item.sMoTa;
                    AddListTreeChilDanhMucCongKhai(item, lstDanhMucModel);
                }
            }
        }

        private void LoadPublicFinances()
        {
            var yearOfWork = _sessionService.Current.YearOfWork;
            var dtChungTuSelected = Models.First(x => x.Id.Equals(_dataDotSelected.Id));

            var dmCongKhais = _dmCongKhaiTaiChinhService.FindByCondition(c => c.iNamLamViec == yearOfWork);

            var dataDotSelectedModel = Models.First(x => x.Id.Equals(_dataDotSelected.Id));
            var chungTuId = dataDotSelectedModel.Id.ToString();
            var dataDanhMucSelected = ListPublic.Where(x => x.IsSelected).Select(x => x.sMoTa).ToList();
            if (_checkPrintAccumulation)
            {
                var chungTuIdAccumulation = string.Join(StringUtils.COMMA, Models.Where(x => x.DNgayChungTu.HasValue && x.DNgayChungTu <= DateUtils.EndOfDay(dataDotSelectedModel.DNgayChungTu.Value))
                    .Select(x => x.Id.ToString()));
                chungTuId = string.Join(StringUtils.COMMA, chungTuId, chungTuIdAccumulation);
            }
            var listIDDMCongKhai = dmCongKhais.Select(x => x.Id).ToList();

            int donViTinh = GetDonViTinh();

            var searchCondition = new EstimationVoucherDetailCriteria
            {
                VoucherId = Guid.Empty,
                ChungTuId = chungTuId,
                LNS = string.Join(StringUtils.COMMA, listIDDMCongKhai),
                YearOfWork = _sessionService.Current.YearOfWork,
                YearOfBudget = _sessionService.Current.YearOfBudget,
                BudgetSource = _sessionService.Current.Budget,
                VoucherDate = dataDotSelectedModel.DNgayChungTu.HasValue ? dataDotSelectedModel.DNgayChungTu : DateTime.Now,
                SoChungTu = string.IsNullOrEmpty(dataDotSelectedModel.SSoChungTu) ? string.Empty : dataDotSelectedModel.SSoChungTu,
                dvt = donViTinh
            };
            /*
            Func<NsDtChungTuChiTietQuery, bool> hasData = entityInput => dataDotSelectedModel.ILoaiChungTu.HasValue && dataDotSelectedModel.ILoaiChungTu.Value.ToString().Equals(VoucherType.NSSD_Key)
                ? entityInput.FTuChi != 0 || entityInput.FHienVat != 0
                : entityInput.FHangNhap != 0 || entityInput.FHangMua != 0 || entityInput.FPhanCap != 0;

            var listChungTuChiTiet = _dtChungTuChiTietService.FindReportCongKhaiTaiChinh(searchCondition).Where(x => x.BHangCha || hasData.Invoke(x)).ToList();
            dmCongKhais = dmCongKhais.Where(x => listChungTuChiTiet.Any(y => y.SMoTa == x.sMoTa));
            */
            List<NsDanhMucCongKhaiCustomModel> lstDanhMucModel = new List<NsDanhMucCongKhaiCustomModel>();
            lstDanhMucModel = dmCongKhais.Select(d => new NsDanhMucCongKhaiCustomModel
            {
                Id = d.Id,
                sMoTa = String.IsNullOrEmpty(d.STT) ? d.sMoTa : (d.STT + StringUtils.DOT_SPLIT + d.sMoTa),
                iID_DMCongKhai_Cha = d.iID_DMCongKhai_Cha,
                iNamLamViec = d.iNamLamViec,
                bHangCha = d.bHangCha,
                STT = d.STT,
                sMa = d.sMa,
                sMaCha = d.sMaCha
            }).ToList();

            OrderByTreeDanhMucCongKhai(lstDanhMucModel);
            lstDanhMucModel = lstDanhMucModel.OrderBy(x => x.sMa).ToList();

            ListPublic = _mapper.Map<ObservableCollection<NsDanhMucCongKhaiCustomModel>>(lstDanhMucModel);

            _lnsView = CollectionViewSource.GetDefaultView(ListPublic);
            _lnsView.Filter = obj => string.IsNullOrWhiteSpace(_searchLNS)
                                     || (obj is DmCongKhaiTaiChinhModel item && item.sMoTa.Contains(_searchLNS.Trim(), StringComparison.OrdinalIgnoreCase));

            OnPropertyChanged(nameof(SelectAllFinance));
            OnPropertyChanged(nameof(LabelSelectedCountLNS));
            OnPropertyChanged(nameof(IsExportData));

            if (_listPublic != null && _listPublic.Count > 0)
            {
                foreach (var model in _listPublic)
                {
                    model.PropertyChanged += (sender, args) =>
                    {
                        if (args.PropertyName == nameof(NsMuclucNgansachModel.IsSelected))
                        {
                            OnPropertyChanged(nameof(SelectAllFinance));
                            OnPropertyChanged(nameof(LabelSelectedCountLNS));
                            OnPropertyChanged(nameof(IsExportData));
                        }
                    };
                }
            }

        }

        public override IEnumerable<DtChungTuChiTietModel> GetData()
        {
            var dataDotSelectedModel = Models.First(x => x.Id.Equals(_dataDotSelected.Id));
            var chungTuId = dataDotSelectedModel.Id.ToString();
            if (_checkPrintAccumulation)
            {
                var chungTuIdAccumulation = string.Join(StringUtils.COMMA, Models.Where(x => x.DNgayChungTu.HasValue && x.DNgayChungTu <= DateUtils.EndOfDay(dataDotSelectedModel.DNgayChungTu.Value))
                    .Select(x => x.Id.ToString()));
                chungTuId = string.Join(StringUtils.COMMA, chungTuId, chungTuIdAccumulation);
            }

            //var listDanhMucCongKhaiMLNS = _dmCongKhaiTaiChinhService.GetAllMlns(ListPublic.Select(x => x.Id));
            //var listMLNS = _nsMucLucNganSachService.FindByCondition(n => n.NamLamViec == _sessionService.Current.YearOfWork);

            var listIDDMCongKhai = ListPublic.Select(x => x.Id).ToList();

            //var listLNS = from danhMucCongKhaiMLNS in listDanhMucCongKhaiMLNS
            //              join mLNS in listMLNS
            //              on danhMucCongKhaiMLNS.sNS_XauNoiMa equals mLNS.XauNoiMa
            //              select mLNS.Lns;

            var searchCondition = new EstimationVoucherDetailCriteria
            {
                VoucherId = Guid.Empty,
                ChungTuId = chungTuId,
                LNS = string.Join(StringUtils.COMMA, listIDDMCongKhai),
                YearOfWork = _sessionService.Current.YearOfWork,
                YearOfBudget = _sessionService.Current.YearOfBudget,
                BudgetSource = _sessionService.Current.Budget,
                VoucherDate = dataDotSelectedModel.DNgayChungTu.HasValue ? dataDotSelectedModel.DNgayChungTu : DateTime.Now,
                SoChungTu = string.IsNullOrEmpty(dataDotSelectedModel.SSoChungTu) ? string.Empty : dataDotSelectedModel.SSoChungTu
            };

            Func<NsDtChungTuChiTietQuery, bool> hasData = entityInput => dataDotSelectedModel.ILoaiChungTu.HasValue && dataDotSelectedModel.ILoaiChungTu.Value.ToString().Equals(VoucherType.NSSD_Key)
                ? entityInput.FTuChi != 0 || entityInput.FHienVat != 0
                : entityInput.FHangNhap != 0 || entityInput.FHangMua != 0 || entityInput.FPhanCap != 0;

            var listChungTuChiTiet = _dtChungTuChiTietService.FindByCond(searchCondition, procedure: "rpt_du_toan_chi_tieu_LNS_1").Where(x => x.BHangCha || hasData.Invoke(x)).ToList();
            var listChungTuChiTietModel = _mapper.Map<List<DtChungTuChiTietModel>>(listChungTuChiTiet
                .GroupBy(n => new { n.IIdMaDonVi, n.STenDonVi })
                .Select(m => new DtChungTuChiTietModel { IIdMaDonVi = m.Key.IIdMaDonVi, STenDonVi = m.Key.STenDonVi, FTuChi = m.Sum(x => x.FTuChi), SMoTa = string.Join(",", m.Select(x => x.SMoTa)) }));
            return listChungTuChiTietModel;
        }

        public override void HandleAfterExport()
        {
            DialogHost.CloseDialogCommand.Execute(null, null);
        }

        private double GetDataByType(DtChungTuChiTietModel chungTuChiTiet)
        {
            if (_dataDotSelected == null)
            {
                return 0.0;
            }

            var dataDotModel = Models.FirstOrDefault(x => x.Id.Equals(_dataDotSelected.Id));
            if (dataDotModel == null || !dataDotModel.ILoaiChungTu.HasValue)
            {
                return 0.0;
            }

            if (VoucherType.NSSD_Key.Equals(dataDotModel.ILoaiChungTu.ToString()))
            {
                return chungTuChiTiet.FTuChi + chungTuChiTiet.FHienVat;
            }
            else if (VoucherType.NSBD_Key.Equals(dataDotModel.ILoaiChungTu.ToString()))
            {
                return chungTuChiTiet.FHangNhap + chungTuChiTiet.FHangMua + chungTuChiTiet.FPhanCap;
            }

            return 0.0;
        }

        private void LoadDonViTinh()
        {
            DataDonViTinh = new ObservableCollection<ComboboxItem>();
            List<DanhMuc> listDonViTinh = _danhMucService.FindByCondition(x => x.SType.Contains(TypeDanhMuc.DM_DONVITINH) && x.ITrangThai == StatusType.ACTIVE
                                && x.INamLamViec == _sessionService.Current.YearOfWork).OrderBy(n => n.SGiaTri).ToList();
            if (listDonViTinh == null || listDonViTinh.Count <= 0)
            {
                DataDonViTinh.Add(new ComboboxItem { ValueItem = DonViTinh.DONG_VALUE, DisplayItem = DonViTinh.DONG });
            }
            foreach (var dvt in listDonViTinh)
            {
                DataDonViTinh.Add(new ComboboxItem { ValueItem = dvt.SGiaTri.ToString(), DisplayItem = dvt.STen });
            }
            SelectedDonViTinh = DataDonViTinh.FirstOrDefault();
        }

        public override List<Tuple<string, string, Dictionary<string, object>>> ConvertDataExport(IEnumerable<DtChungTuChiTietModel> dataExport, string extension)
        {
            var listResult = new List<Tuple<string, string, Dictionary<string, object>>>();
            var yearOfWork = _sessionService.Current.YearOfWork;
            var yearOfBudget = _sessionService.Current.YearOfBudget;
            var budgetSource = _sessionService.Current.Budget;

            int pageSize = 6;

            var listDonVi = dataExport.Select(x => x.IIdMaDonVi).Distinct();
            var listData = new List<DtChungTuChiTietModel>();
            foreach (var dv in listDonVi)
            {
                var giatri = new DtChungTuChiTietModel();
                giatri.STenDonVi = dv;
                listData.Add(giatri);
            }

            var data = new Dictionary<string, object>
            {
                { "ListData", listData },
                { "TitleFirst", TxtTitleFirst },
                { "TitleSecond", TxtTitleSecond },
                { "TitleThird", TxtTitleThird },
                { "Cap1", "" },
                { "Cap2", "" },
                { "TongCongBangChu", "" }
            };
            //List<RptDuToanCongKhai> results = new List<RptDuToanCongKhai>();

            string templateFileName = string.Empty;
            templateFileName = Path.Combine(ExportPrefix.PATH_TL_DT, $"{ExportFileName.RPT_NS_DUTOAN_CONGKHAI_TO1}{StringUtils.EXCEL_EXTENSION}");
            string fileNameWithoutExtension = StringUtils.CreateExportFileName(ExportFileName.RPT_NS_DUTOAN_CONGKHAI_TO1);

            listResult.Add(Tuple.Create(templateFileName, fileNameWithoutExtension, data));
            return listResult;
        }

        public static IEnumerable<List<T>> SplitList<T>(List<T> bigList, int nSize = 6)
        {
            for (int i = 0; i < bigList.Count; i += nSize)
            {
                yield return bigList.GetRange(i, Math.Min(nSize, bigList.Count - i));
            }
        }
        private int GetDonViTinh()
        {
            if (SelectedDonViTinh == null || string.IsNullOrEmpty(SelectedDonViTinh.ValueItem))
                return 1;
            return int.Parse(SelectedDonViTinh.ValueItem);
        }

        private void OnExportFile(ExportType exportType)
        {
            BackgroundWorkerHelper.Run((s, e) =>
            {
                IsLoading = true;
                List<ExportResult> exportResults = new List<ExportResult>();
                var dataDotSelectedModel = Models.First(x => x.Id.Equals(_dataDotSelected.Id));
                var chungTuId = dataDotSelectedModel.Id.ToString();
                var dataDanhMucSelected = ListPublic.Where(x => x.IsSelected).Select(x => x.sMa).ToList();

                if (_checkPrintAccumulation)
                {
                    var chungTuIdAccumulation = string.Join(StringUtils.COMMA, Models.Where(x => x.DNgayChungTu.HasValue && x.DNgayChungTu <= DateUtils.EndOfDay(dataDotSelectedModel.DNgayChungTu.Value))
                        .Select(x => x.Id.ToString()));
                    chungTuId = string.Join(StringUtils.COMMA, chungTuId, chungTuIdAccumulation);
                }
                var listIDDMCongKhai = ListPublic.Select(x => x.Id).ToList();
                int donViTinh = GetDonViTinh();

                var searchCondition = new EstimationVoucherDetailCriteria
                {
                    VoucherId = Guid.Empty,
                    ChungTuId = chungTuId,
                    LNS = string.Join(StringUtils.COMMA, listIDDMCongKhai),
                    YearOfWork = _sessionService.Current.YearOfWork,
                    YearOfBudget = _sessionService.Current.YearOfBudget,
                    BudgetSource = _sessionService.Current.Budget,
                    VoucherDate = dataDotSelectedModel.DNgayChungTu.HasValue ? dataDotSelectedModel.DNgayChungTu : DateTime.Now,
                    SoChungTu = string.IsNullOrEmpty(dataDotSelectedModel.SSoChungTu) ? string.Empty : dataDotSelectedModel.SSoChungTu,
                    dvt = donViTinh
                };

                Func<NsDtChungTuChiTietQuery, bool> hasData = entityInput => dataDotSelectedModel.ILoaiChungTu.HasValue && dataDotSelectedModel.ILoaiChungTu.Value.ToString().Equals(VoucherType.NSSD_Key)
                    ? entityInput.FTuChi != 0 || entityInput.FHienVat != 0
                    : entityInput.FHangNhap != 0 || entityInput.FHangMua != 0 || entityInput.FPhanCap != 0;

                var listChungTuChiTiet = _dtChungTuChiTietService.FindReportCongKhaiTaiChinh(searchCondition).Where(x => x.BHangCha || hasData.Invoke(x)).ToList();
                var listChungTuChiTietModel = _mapper.Map<List<DtChungTuChiTietModel>>(listChungTuChiTiet
                    .Select(m => new DtChungTuChiTietModel
                    {
                        IIdMaDonVi = m.IIdMaDonVi,
                        STenDonVi = m.STenDonVi,
                        FTuChi = m.FTuChi,
                        SMoTa = m.SMoTa,
                        IID_DMCongKhai = m.IID_DMCongKhai,
                        IID_DMCongKhai_Cha = m.IID_DMCongKhai_Cha,
                        SMa = m.SMa
                    }));

                int pageSize = 6;
                var stt = 1;
                var listDonVi = listChungTuChiTietModel.OrderBy(x => x.IIdMaDonVi).Select(x => x.IIdMaDonVi).Distinct();
                var listDanhMuc = listChungTuChiTietModel.Where(x => dataDanhMucSelected.Any(y => y == x.SMa)).OrderBy(z => z.SMa).Select(x => new Tuple<Guid?, Guid?, string>(x.IID_DMCongKhai, x.IID_DMCongKhai_Cha, x.SMoTa)).Distinct().ToList();

                if (exportType == ExportType.EXCEL)
                {
                    pageSize = listDanhMuc.Count();
                }

                var listDanhMucSplits = SplitList(listDanhMuc, pageSize).ToList();

                for (int i = 0; i < listDanhMucSplits.Count; i++)
                {
                    var danhMucSplitMoTa = listDanhMucSplits[i].Select(n => n.Item3);
                    var danhMucSplitID = listDanhMucSplits[i].Select(n => n.Item2);
                    var listDataChild = listDanhMucSplits[i].Select(n => n.Item1);
                    //var listDataChild = ListPublic.Where(n => listDanhMucSplits[i].Contains(n.sMoTa.Substring(n.sMoTa.IndexOf(StringUtils.DOT) + 2))).Distinct().ToList();
                    var listDataParent = ListPublic.Where(n => danhMucSplitID.Contains(n.Id)).OrderBy(n => n.sMa).ToList();

                    var listData = new List<ReportDanhMucCongKhaiTaiChinh>();
                    if (listDanhMucSplits[i].Count < pageSize)
                    {
                        var countEmpty = pageSize - listDanhMucSplits[i].Count;
                        for (int j = 0; j < countEmpty; j++)
                        {
                            string emptyCb = string.Empty;
                            listDanhMucSplits[i].Add(new Tuple<Guid?, Guid?, string>(Guid.Empty, Guid.Empty, string.Empty));
                        }
                    }
                    foreach (var dv in listDonVi)
                    {
                        var giatri = new ReportDanhMucCongKhaiTaiChinh();

                        giatri.TongCong = listChungTuChiTietModel.Where(x => x.IIdMaDonVi.Equals(dv)).Sum(x => x.FTuChi);
                        giatri.TenDonVi = listChungTuChiTietModel.FirstOrDefault(x => x.IIdMaDonVi.Equals(dv))?.STenDonVi ?? string.Empty;
                        giatri.NoiDung = string.Format("{0} - {1}", dv, giatri.TenDonVi);
                        giatri.LstGiaTri = new List<NsDtChungTuChiTiet>();

                        foreach (var dm in listDanhMucSplits[i])
                        {
                            var kq = new NsDtChungTuChiTiet();
                            var danhmuc = listChungTuChiTietModel.FirstOrDefault(x => x.IID_DMCongKhai == dm.Item1 && x.SMoTa == dm.Item3 && x.IIdMaDonVi == dv);
                            kq.FTuChi = danhmuc?.FTuChi ?? 0;
                            giatri.LstGiaTri.Add(kq);
                        }

                        if (giatri.LstGiaTri.Where(n => n.FTuChi != 0).Count() > 0)
                        {
                            giatri.STT = stt++;
                            listData.Add(giatri);
                        }
                    }

                    List<HeaderReportDanhMucCongKhaiTaiChinh> headers = new List<HeaderReportDanhMucCongKhaiTaiChinh>();
                    List<HeaderReportDanhMucCongKhaiTaiChinh> headersParents = new List<HeaderReportDanhMucCongKhaiTaiChinh>();

                    int columnStarts = 5;
                    int STT = 1;
                    foreach (var item in listDataParent)
                    {
                        var listChild = listDanhMucSplits[i].Where(x => x.Item2 == item.Id).ToList();
                        var columnStartName = GetExcelColumnName(columnStarts);
                        var columnEndName = GetExcelColumnName(columnStarts + listChild.Count() - 1);

                        headersParents.Add(new HeaderReportDanhMucCongKhaiTaiChinh()
                        {
                            MergeRange = columnStartName + "6" + ":" + columnEndName + "6",
                            ChiSoDanhMuc = item.sMoTa.Substring(item.sMoTa.IndexOf(StringUtils.DOT) + 2),
                            STT = 1,
                        });
                        for (int j = 0; j < listChild.Count() - 1; j++)
                        {
                            headersParents.Add(new HeaderReportDanhMucCongKhaiTaiChinh()
                            {
                                //MergeRange = GetExcelColumnName(columnStarts + listChild.Count() - 2) + "6" + ":" + GetExcelColumnName(columnStarts + listChild.Count() - 2) + "6",
                                ChiSoDanhMuc = string.Empty,
                                STT = 0,
                            });
                        }
                        //columnStarts += listChild.Count() - 1;
                    }

                    foreach (var item in listDanhMucSplits[i])
                    {
                        var header = new HeaderReportDanhMucCongKhaiTaiChinh();
                        header.TenDanhMuc = item.Item3;
                        headers.Add(header);
                        /*
                        header.ChiSoDanhMuc = listLookupParent[listLookupChild[item].First() ?? Guid.Empty].First();
                        header.MergeRange = columnStartName + "8" + ":" + columnEndName + "8";
                        */
                    }

                    List<ReportDanhMucCongKhaiTaiChinh> resultsTotal = new List<ReportDanhMucCongKhaiTaiChinh>();
                    ReportDanhMucCongKhaiTaiChinh total = new ReportDanhMucCongKhaiTaiChinh();
                    total.LstTong = new List<NsDtChungTuChiTiet>();
                    total.TongCong = listChungTuChiTietModel.Sum(x => x.FTuChi);

                    foreach (var dm in listDanhMucSplits[i])
                    {
                        NsDtChungTuChiTiet giaTri = new NsDtChungTuChiTiet();
                        giaTri.FTuChi = listChungTuChiTietModel.Where(x => x.SMoTa == dm.Item3 && x.IID_DMCongKhai == dm.Item1)?.Sum(x => x.FTuChi) ?? 0;
                        total.LstTong.Add(giaTri);
                    }

                    resultsTotal.Add(total);
                    var tongSoTien = resultsTotal.FirstOrDefault();

                    var data = new Dictionary<string, object>();

                    FormatNumber formatNumber = new FormatNumber(donViTinh, exportType);
                    data.Add("FormatNumber", formatNumber);
                    data.Add("TieuDe1", TxtTitleFirst);
                    data.Add("TieuDe2", TxtTitleSecond);
                    data.Add("TieuDe3", TxtTitleThird);
                    data.Add("Cap1", "");
                    data.Add("Cap2", "");
                    data.Add("h1", "");
                    data.Add("h2", "");
                    data.Add("Header1", "Đơn vị tính: " + SelectedDonViTinh.DisplayItem);
                    data.Add("TongCongBangChu", tongSoTien != null ? StringUtils.NumberToText(tongSoTien.TongCong * donViTinh) : "");
                    AddChuKy(data);


                    if (i == 0)
                    {
                        string templateFileName = Path.Combine(ExportPrefix.PATH_TL_DT, ExportFileName.RPT_NS_DUTOAN_CONGKHAI_TO1);
                        string fileNamePrefix = ExportFileName.RPT_NS_DUTOAN_CONGKHAI_TO1.Split(".").First();
                        string fileNameWithoutExtension = StringUtils.CreateExportFileName(fileNamePrefix);
                        var mergeRange = "";
                        if (exportType == ExportType.EXCEL)
                        {
                            List<HeaderReportDanhMucCongKhaiTaiChinh> lstHeader1 = new List<HeaderReportDanhMucCongKhaiTaiChinh>();
                            lstHeader1.Add(new HeaderReportDanhMucCongKhaiTaiChinh { STT = 1, TenDanhMuc = " Trong đó" });
                            foreach (var lstDm in listDanhMuc)
                            {
                                HeaderReportDanhMucCongKhaiTaiChinh header1 = new HeaderReportDanhMucCongKhaiTaiChinh();
                                lstHeader1.Add(header1);
                            }
                            lstHeader1 = lstHeader1.Take(lstHeader1.Count() - 1).ToList();

                            int columnStart = 5;
                            var columnStartName = GetExcelColumnName(columnStart);
                            var columnEndName = GetExcelColumnName(listDanhMuc.Count() + columnStart - 1);
                            if (pageSize < 6)
                            {
                                columnEndName = GetExcelColumnName(6 + columnStart - 1);
                            }

                            mergeRange = columnStartName + "5" + ":" + columnEndName + "5";
                            if (pageSize < 6)
                            {
                                for (int m = 0; m < 6 - pageSize; m++)
                                {
                                    HeaderReportDanhMucCongKhaiTaiChinh col = new HeaderReportDanhMucCongKhaiTaiChinh();
                                    lstHeader1.Add(col);
                                    headers.Add(col);

                                    foreach (var da in listData)
                                    {
                                        NsDtChungTuChiTiet ct = new NsDtChungTuChiTiet();
                                        da.LstGiaTri.Add(ct);
                                    }
                                    foreach (var tt in resultsTotal)
                                    {
                                        tt.LstTong.Add(new NsDtChungTuChiTiet());
                                    }
                                }

                            }
                            data.Add("LstHeader1", lstHeader1);
                            data.Add("MergeRange", mergeRange);
                            templateFileName = Path.Combine(ExportPrefix.PATH_TL_DT, ExportFileName.RPT_NS_DUTOAN_CONGKHAI_THUCHI_TO1_EXCEL);
                            fileNamePrefix = ExportFileName.RPT_NS_DUTOAN_CONGKHAI_THUCHI_TO1_EXCEL.Split(".").First();
                        }

                        data.Add("ListData", listData);
                        data.Add("ListTotal", resultsTotal);
                        data.Add("Headers", headers);
                        data.Add("HeadersParents", headersParents);

                        var xlsFile = _exportService.Export<ReportDanhMucCongKhaiTaiChinh, HeaderReportDanhMucCongKhaiTaiChinh>(templateFileName, data);
                        exportResults.Add(new ExportResult(fileNameWithoutExtension, fileNameWithoutExtension, null, xlsFile));
                    }
                    else
                    {
                        data.Add("ListData", listData);
                        data.Add("ListTotal", resultsTotal);
                        data.Add("Headers", headers);
                        data.Add("HeadersParents", headersParents);
                        string templateFileName = Path.Combine(ExportPrefix.PATH_TL_DT, ExportFileName.RPT_NS_DUTOAN_CONGKHAI_TO2);
                        string fileNamePrefix = ExportFileName.RPT_NS_DUTOAN_CONGKHAI_TO2.Split(".").First();
                        string fileNameWithoutExtension = StringUtils.CreateExportFileName(fileNamePrefix);
                        var xlsFile = _exportService.Export<ReportDanhMucCongKhaiTaiChinh, HeaderReportDanhMucCongKhaiTaiChinh>(templateFileName, data, new List<int> { 4 });
                        exportResults.Add(new ExportResult(fileNameWithoutExtension, fileNameWithoutExtension, null, xlsFile));
                    }
                }
                if (listDanhMuc.Count == 0)
                {
                    var data = new Dictionary<string, object>();
                    data.Add("ListData", new List<ReportDanhMucCongKhaiTaiChinh>());
                    data.Add("ListTotal", new List<ReportDanhMucCongKhaiTaiChinh>());
                    data.Add("Headers", new List<HeaderReportDanhMucCongKhaiTaiChinh>());
                    data.Add("TieuDe1", TxtTitleFirst);
                    data.Add("TieuDe2", TxtTitleSecond);
                    data.Add("TieuDe3", TxtTitleThird);
                    data.Add("Cap1", "");
                    data.Add("Cap2", "");
                    data.Add("h1", "");
                    data.Add("h2", "");
                    data.Add("TongCongBangChu", "");
                    AddChuKy(data);
                    string templateFileName = Path.Combine(ExportPrefix.PATH_TL_DT, ExportFileName.RPT_NS_DUTOAN_CONGKHAI_TO1_EMPTY);
                    string fileNamePrefix = ExportFileName.RPT_NS_DUTOAN_CONGKHAI_TO1_EMPTY.Split(".").First();
                    string fileNameWithoutExtension = StringUtils.CreateExportFileName(fileNamePrefix);
                    var xlsFile = _exportService.Export<ReportDanhMucCongKhaiTaiChinh, HeaderReportDanhMucCongKhaiTaiChinh>(templateFileName, data);
                    exportResults.Add(new ExportResult(fileNameWithoutExtension, fileNameWithoutExtension, null, xlsFile));
                }
                e.Result = exportResults;
            }, (s, e) =>
                {
                    if (e.Error == null)
                    {
                        var result = (List<ExportResult>)e.Result;
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


        private string GetExcelColumnName(int columnNumber)
        {
            string columnName = "";

            while (columnNumber > 0)
            {
                int modulo = (columnNumber - 1) % 26;
                columnName = Convert.ToChar('A' + modulo) + columnName;
                columnNumber = (columnNumber - modulo) / 26;
            }
            return columnName;
        }
        private void AddChuKy(Dictionary<string, object> data)
        {
            data.Add("Diadiem", string.Format("{0}, ngày {1} tháng {2} năm {3}", _diaDiem, DateTime.Now.Day, DateTime.Now.Month, DateTime.Now.Year));
            data.Add("ChucDanh1", _dmChuKy != null ? _dmChuKy.ChucDanh1MoTa : string.Empty);
            data.Add("ChucDanh2", _dmChuKy != null ? _dmChuKy.ChucDanh2MoTa : string.Empty);
            data.Add("ChucDanh3", _dmChuKy != null ? _dmChuKy.ChucDanh3MoTa : string.Empty);
            data.Add("ThuaLenh1", _dmChuKy != null ? _dmChuKy.ThuaLenh1MoTa : string.Empty);
            data.Add("ThuaLenh2", _dmChuKy != null ? _dmChuKy.ThuaLenh2MoTa : string.Empty);
            data.Add("ThuaLenh3", _dmChuKy != null ? _dmChuKy.ThuaLenh3MoTa : string.Empty);
            data.Add("Ten1", _dmChuKy != null ? _dmChuKy.Ten1MoTa : string.Empty);
            data.Add("Ten2", _dmChuKy != null ? _dmChuKy.Ten2MoTa : string.Empty);
            data.Add("Ten3", _dmChuKy != null ? _dmChuKy.Ten3MoTa : string.Empty);
        }

        private void CalculateDataExportDynamicCol(DuToanChiTieuLNS duToanChiTieuLns, Dictionary<int, List<DtChungTuChiTietModel>> dictValue, int catUnitType)
        {
            duToanChiTieuLns.Val1 = dictValue.GetValueOrDefault(0, new List<DtChungTuChiTietModel>()).Sum(GetDataByType) / catUnitType;
            duToanChiTieuLns.Val2 = dictValue.GetValueOrDefault(1, new List<DtChungTuChiTietModel>()).Sum(GetDataByType) / catUnitType;
            duToanChiTieuLns.Val3 = dictValue.GetValueOrDefault(2, new List<DtChungTuChiTietModel>()).Sum(GetDataByType) / catUnitType;
            duToanChiTieuLns.Val4 = dictValue.GetValueOrDefault(3, new List<DtChungTuChiTietModel>()).Sum(GetDataByType) / catUnitType;
            duToanChiTieuLns.Val5 = dictValue.GetValueOrDefault(4, new List<DtChungTuChiTietModel>()).Sum(GetDataByType) / catUnitType;
            duToanChiTieuLns.Val6 = dictValue.GetValueOrDefault(5, new List<DtChungTuChiTietModel>()).Sum(GetDataByType) / catUnitType;
            duToanChiTieuLns.Val7 = dictValue.GetValueOrDefault(6, new List<DtChungTuChiTietModel>()).Sum(GetDataByType) / catUnitType;
            duToanChiTieuLns.Val8 = dictValue.GetValueOrDefault(7, new List<DtChungTuChiTietModel>()).Sum(GetDataByType) / catUnitType;
            duToanChiTieuLns.Val9 = dictValue.GetValueOrDefault(8, new List<DtChungTuChiTietModel>()).Sum(GetDataByType) / catUnitType;
            duToanChiTieuLns.Val10 = dictValue.GetValueOrDefault(9, new List<DtChungTuChiTietModel>()).Sum(GetDataByType) / catUnitType;
        }

        private void OnConfigSign()
        {
            DmChuKyModel chuKyModel = new DmChuKyModel();
            _dmChuKy = _dmChuKyService.FindByCondition(x => x.IdType.Equals(TypeChuKy.RPT_NS_PHANBODUTOAN_CONGKHAITAICHINH) && x.ITrangThai == StatusType.ACTIVE).FirstOrDefault();
            if (_dmChuKy == null)
                chuKyModel.IdType = TypeChuKy.RPT_NS_PHANBODUTOAN_CONGKHAITAICHINH;
            else
                chuKyModel = _mapper.Map<DmChuKyModel>(_dmChuKy);
            DmChuKyDialogViewModel.DmChuKyModel = chuKyModel;
            DmChuKyDialogViewModel.SavedAction = obj => LoadTieuDe();
            DmChuKyDialogViewModel.Init();
            DmChuKyDialogViewModel.ShowDialog();
        }
    }
}
