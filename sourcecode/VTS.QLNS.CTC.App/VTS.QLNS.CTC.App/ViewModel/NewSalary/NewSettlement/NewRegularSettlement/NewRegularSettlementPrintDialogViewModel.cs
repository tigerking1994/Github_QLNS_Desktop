using AutoMapper;
using log4net;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Web.UI.WebControls;
using System.Windows.Forms;
using VTS.QLNS.CTC.App.Command;
using VTS.QLNS.CTC.App.Helper;
using VTS.QLNS.CTC.App.Model;
using VTS.QLNS.CTC.App.Model.Control;
using VTS.QLNS.CTC.App.Model.Report;
using VTS.QLNS.CTC.App.Properties;
using VTS.QLNS.CTC.App.Service;
using VTS.QLNS.CTC.App.Service.Impl;
using VTS.QLNS.CTC.App.Service.UserFunction;
using VTS.QLNS.CTC.App.View.NewSalary.NewSettlement.NewRegularSettlement;
using VTS.QLNS.CTC.App.ViewModel.Budget.Settlement.Explanation;
using VTS.QLNS.CTC.App.ViewModel.Category;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Service;
using VTS.QLNS.CTC.Utility;
using VTS.QLNS.CTC.Utility.Enum;

namespace VTS.QLNS.CTC.App.ViewModel.NewSalary.NewSettlement.NewRegularSettlement
{
    public class NewRegularSettlementPrintDialogViewModel : ViewModelBase
    {
        private readonly ILog _logger;
        private readonly IExportService _exportService;
        private readonly INsDonViService _donViService;
        private readonly ISessionService _sessionService;
        private readonly ITlDmCanBoNq104Service _tlDmCanBoService;
        private readonly ITlDmDonViNq104Service _tlDmDonViService;
        private readonly ITlQtChungTuNq104Service _tlQtChungTuService;
        private readonly ITlQtChungTuChiTietNq104Service _tlQtChungTuChiTietService;
        private readonly ITlQtChungTuChiTietGiaiThichNq104Service _tlQtChungTuChiTietGiaiThichService;
        private readonly IMapper _mapper;
        private readonly IDanhMucService _danhMucService;
        private readonly IDmChuKyService _dmChuKyService;
        private readonly INsDonViService _nsDonViService;
        private SessionInfo _sessionInfo;
        private string _cap1;
        private string _diaDiem;
        private DmChuKy _dmChuKy;
        private string _typeChuKy = TypeChuKy.RPT_TL_QUYETTOAN_THUONGXUYEN_NEW;
        private TlQtChungTuChiTietGiaiThichNq104Model _modelGiaiThich;
        private List<TlQtChungTuChiTietNq104Model> _itemsChiTiet;

        private bool _bIsDetailView;
        public bool BIsDetailView
        {
            get => _bIsDetailView;
            set => SetProperty(ref _bIsDetailView, value);
        }

        private bool _bIsSummary;
        public bool BIsSummary
        {
            get => _bIsSummary;
            set
            {
                SetProperty(ref _bIsSummary, value);
                LoadDonViData();
            }
        }

        public override string Title => "In báo cáo quyết toán thường xuyên";
        public override string Description => "In báo cáo quyết toán thường xuyên theo chứng từ";
        public static string SCachTinhLuong = "CACH0,CACH5";
        public override Type ContentType => typeof(NewRegularSettlementPrintDialog);
        public bool IsExportEnable => _isData || _isDataInterpretation || _isVerbalExplanation || _isCover;
        public List<TlQtChungTuNq104Model> ItemsChungTu { get; set; }
        public TlQtChungTuNq104Model ChungTuModel { get; set; }

        public string MaDonVi { get; set; }
        public string TenDonVi { get; set; }

        private string _soChungTu;
        public string SoChungTu
        {
            get => _soChungTu;
            set => SetProperty(ref _soChungTu, value);
        }

        private DateTime? _dNgayChungTu;
        public DateTime? DNgayChungTu
        {
            get => _dNgayChungTu;
            set => SetProperty(ref _dNgayChungTu, value);
        }
        private int _nam;
        public int Nam
        {
            get => _nam;
            set => SetProperty(ref _nam, value);
        }

        private int _thang;
        public int Thang
        {
            get => _thang;
            set => SetProperty(ref _thang, value);
        }
        public bool InDetailView { get; set; }

        private bool _isOpenExportPopup;
        public bool IsOpenExportPopup
        {
            get => _isOpenExportPopup;
            set => SetProperty(ref _isOpenExportPopup, value);
        }

        private string _title1;
        public string Title1
        {
            get => _title1;
            set => SetProperty(ref _title1, value);
        }

        private string _title2;
        public string Title2
        {
            get => _title2;
            set => SetProperty(ref _title2, value);
        }

        private string _note;
        public string Note
        {
            get => _note;
            set => SetProperty(ref _note, value);
        }

        private bool _isCover;
        public bool IsCover
        {
            get => _isCover;
            set
            {
                SetProperty(ref _isCover, value);
                OnPropertyChanged(nameof(IsExportEnable));
            }
        }

        private bool _isData;
        public bool IsData
        {
            get => _isData;
            set
            {
                SetProperty(ref _isData, value);
                OnPropertyChanged(nameof(IsExportEnable));
            }
        }

        private bool _isDataInterpretation;
        public bool IsDataInterpretation
        {
            get => _isDataInterpretation;
            set
            {
                SetProperty(ref _isDataInterpretation, value);
                OnPropertyChanged(nameof(IsExportEnable));
            }
        }

        private bool _isVerbalExplanation;
        public bool IsVerbalExplanation
        {
            get => _isVerbalExplanation;
            set
            {
                SetProperty(ref _isVerbalExplanation, value);
                OnPropertyChanged(nameof(IsExportEnable));
            }
        }

        private TlQtChungTuChiTietGiaiThichNq104Model _tlRegularDataIntertation;

        public TlQtChungTuChiTietGiaiThichNq104Model TlRegularDataIntertation
        {
            get => _tlRegularDataIntertation;
            set => SetProperty(ref _tlRegularDataIntertation, value);
        }

        private List<ComboboxItem> _units;
        public List<ComboboxItem> Units
        {
            get => _units;
            set => SetProperty(ref _units, value);
        }

        private ComboboxItem _selectedUnit;
        public ComboboxItem SelectedUnit
        {
            get => _selectedUnit;
            set => SetProperty(ref _selectedUnit, value);
        }

        private ObservableCollection<TlDmDonViNq104Model> _donviItems;
        public ObservableCollection<TlDmDonViNq104Model> DonViItems
        {
            get => _donviItems;
            set => SetProperty(ref _donviItems, value);
        }


        private TlDmDonViNq104Model _donviSelected;
        public TlDmDonViNq104Model DonViSelected
        {
            get => _donviSelected;
            set
            {
                if (SetProperty(ref _donviSelected, value))
                {
                    if (_donviSelected != null)
                    {
                        MaDonVi = _donviSelected.MaDonVi;
                        TenDonVi = _donviSelected.TenDonVi;
                    }
                    LoadChungTuData();
                }
            }
        }

        private ObservableCollection<ComboboxItem> _monthItems;
        public ObservableCollection<ComboboxItem> MonthItems
        {
            get => _monthItems;
            set => SetProperty(ref _monthItems, value);
        }

        private ComboboxItem _monthSelected;
        public ComboboxItem MonthSelected
        {
            get => _monthSelected;
            set
            {
                SetProperty(ref _monthSelected, value);
                LoadDonViData();
                LoadChungTuData();
            }
        }

        private string _sNam;
        public string SNam
        {
            get => _sNam;
            set
            {
                SetProperty(ref _sNam, value);
                LoadDonViData();
            }
        }

        public NewRegularDataIntertationViewModel RegularDataIntertationViewModel { get; set; }
        public NewRegularSettlementVerbalExplanationViewModel VerbalExplanationViewModel { get; set; }
        public DmChuKyDialogViewModel DmChuKyDialogViewModel { get; set; }

        public RelayCommand ExportCommand { get; }
        public RelayCommand DataInterpretationCommand { get; }
        public RelayCommand VerbalExplanationCommand { get; }
        public RelayCommand ConfigSignCommand { get; }

        public NewRegularSettlementPrintDialogViewModel(
            ILog logger,
            IMapper mapper,
            IExportService exportService,
            INsDonViService donViService,
            ISessionService sessionService,
            ITlDmCanBoNq104Service tlDmCanBoService,
            IDanhMucService danhMucService,
            IDmChuKyService dmChuKyService,
            ITlDmDonViNq104Service tlDmDonViService,
            ITlQtChungTuNq104Service tlQtChungTuService,
            ITlQtChungTuChiTietNq104Service tlQtChungTuChiTietService,
            ITlQtChungTuChiTietGiaiThichNq104Service tlQtChungTuChiTietGiaiThichService,
            INsDonViService nsDonViService,
            NewRegularDataIntertationViewModel regularDataIntertationViewModel,
            NewRegularSettlementVerbalExplanationViewModel verbalExplanationViewModel,
            DmChuKyDialogViewModel dmChuKyDialogViewModel)
        {
            _exportService = exportService;
            _sessionService = sessionService;
            _donViService = donViService;
            _tlDmCanBoService = tlDmCanBoService;
            _tlDmDonViService = tlDmDonViService;
            _tlQtChungTuService = tlQtChungTuService;
            _tlQtChungTuChiTietService = tlQtChungTuChiTietService;
            _tlQtChungTuChiTietGiaiThichService = tlQtChungTuChiTietGiaiThichService;
            _nsDonViService = nsDonViService;
            _mapper = mapper;
            _danhMucService = danhMucService;
            _dmChuKyService = dmChuKyService;
            _logger = logger;

            RegularDataIntertationViewModel = regularDataIntertationViewModel;
            VerbalExplanationViewModel = verbalExplanationViewModel;
            DmChuKyDialogViewModel = dmChuKyDialogViewModel;

            DataInterpretationCommand = new RelayCommand(obj => OnOpenDataInterpretationDialog());
            VerbalExplanationCommand = new RelayCommand(obj => OnVerbalExplanation());
            ExportCommand = new RelayCommand(obj => OnExport((ExportType)obj));
            ConfigSignCommand = new RelayCommand(obj => OnConfigSign());
        }

        public override void Init()
        {
            base.Init();
            _sessionInfo = _sessionService.Current;
            LoadMonth();
            LoadDonViData();
            LoadDanhMuc();
            LoadTieuDe();
            LoadChungTuData();
            OnPropertyChanged(nameof(BIsDetailView));
        }

        private void LoadTieuDe()
        {
            _dmChuKy = _dmChuKyService.FindByCondition(x => x.IdType.Equals(_typeChuKy) && x.ITrangThai == StatusType.ACTIVE).FirstOrDefault();
            if (_dmChuKy == null)
                _dmChuKy = new DmChuKy();
            if (!string.IsNullOrEmpty(_dmChuKy.TieuDe1MoTa))
                Title1 = _dmChuKy.TieuDe1MoTa;
            if (!string.IsNullOrEmpty(_dmChuKy.TieuDe2MoTa))
                Title2 = _dmChuKy.TieuDe2MoTa;
        }

        private void LoadDanhMuc()
        {
            _units = new List<ComboboxItem>();
            var listDonViTinh = _danhMucService.FindByCondition(x => x.SType.Contains(TypeDanhMuc.DM_DONVITINH) && x.ITrangThai == StatusType.ACTIVE && x.INamLamViec == _sessionInfo.YearOfWork)
                .ToList();
            if (listDonViTinh.Count == 0)
                _units.Add(new ComboboxItem("Đồng", "1"));
            listDonViTinh = listDonViTinh.OrderBy(x => x.SGiaTri).ToList();
            foreach (var dvt in listDonViTinh)
            {
                ComboboxItem cb = new ComboboxItem();
                cb.DisplayItem = dvt.STen;
                cb.ValueItem = dvt.SGiaTri;
                cb.Type = dvt.SMoTa;
                _units.Add(new ComboboxItem(dvt.STen, dvt.SGiaTri));
            }
            OnPropertyChanged(nameof(Units));
            _selectedUnit = Units.ElementAt(0);

            var danhMucQuanLy = _danhMucService.FindByType(TypeDanhMuc.DM_CAUHINH, _sessionService.Current.YearOfWork).Where(n => n.IIDMaDanhMuc == MaDanhMuc.DV_QUANLY).FirstOrDefault();
            _cap1 = danhMucQuanLy == null ? string.Empty : danhMucQuanLy.SGiaTri;
            var danhMucDiaDiem = _danhMucService.FindByType(TypeDanhMuc.DM_CAUHINH, _sessionService.Current.YearOfWork).Where(n => n.IIDMaDanhMuc == MaDanhMuc.DIADIEM).FirstOrDefault();
            _diaDiem = danhMucDiaDiem == null ? string.Empty : danhMucDiaDiem.SGiaTri;
        }

        private void OnOpenDataInterpretationDialog()
        {
            if (MonthSelected == null) return;
            var lstChungTu = _tlQtChungTuService.FindAll(n => n.Nam == Nam && n.MaDonVi == MaDonVi);
            List<int> lstThang = new List<int>();
            if (int.Parse(MonthSelected.ValueItem) <= 12)
                lstThang.Add(int.Parse(MonthSelected.ValueItem));
            switch (int.Parse(MonthSelected.ValueItem))
            {
                case (int)DateTimeExtension.TimeConst.Types.QUY_1:
                    lstThang = new List<int>() { 1, 2, 3 };
                    break;
                case (int)DateTimeExtension.TimeConst.Types.QUY_2:
                    lstThang = new List<int>() { 4, 5, 6 };
                    break;
                case (int)DateTimeExtension.TimeConst.Types.QUY_3:
                    lstThang = new List<int>() { 7, 8, 9 };
                    break;
                case (int)DateTimeExtension.TimeConst.Types.QUY_4:
                    lstThang = new List<int>() { 10, 11, 12 };
                    break;
            }
            if (lstChungTu != null && int.Parse(MonthSelected.ValueItem) != 0)
            {
                lstChungTu = lstChungTu.Where(n => lstThang.Contains(n.Thang));
            }
            if (lstChungTu == null || lstChungTu.Count() == 0)
            {
                MessageBoxHelper.Error(Resources.AlertEmptyReport);
                return;
            }
            var results = _mapper.Map<List<TlQtChungTuNq104Model>>(lstChungTu);
            RegularDataIntertationViewModel.Nam = int.Parse(SNam);
            RegularDataIntertationViewModel.Thang = int.Parse(MonthSelected.ValueItem);
            if (results != null && results.Count != 0)
            {
                RegularDataIntertationViewModel.Model = results.First();
            }
            RegularDataIntertationViewModel.MaDonVi = DonViSelected.MaDonVi;
            RegularDataIntertationViewModel.BIsSummary = BIsSummary;
            RegularDataIntertationViewModel.ItemsChungTu = results;
            RegularDataIntertationViewModel.Init();
            RegularDataIntertationViewModel.ShowDialog();
        }

        private void OnVerbalExplanation()
        {
            var lstChungTu = _tlQtChungTuService.FindAll(n => n.Nam == Nam && n.MaDonVi == MaDonVi);
            List<int> lstThang = new List<int>();
            if (int.Parse(MonthSelected.ValueItem) <= 12)
                lstThang.Add(int.Parse(MonthSelected.ValueItem));
            switch (int.Parse(MonthSelected.ValueItem))
            {
                case (int)DateTimeExtension.TimeConst.Types.QUY_1:
                    lstThang = new List<int>() { 1, 2, 3 };
                    break;
                case (int)DateTimeExtension.TimeConst.Types.QUY_2:
                    lstThang = new List<int>() { 4, 5, 6 };
                    break;
                case (int)DateTimeExtension.TimeConst.Types.QUY_3:
                    lstThang = new List<int>() { 7, 8, 9 };
                    break;
                case (int)DateTimeExtension.TimeConst.Types.QUY_4:
                    lstThang = new List<int>() { 10, 11, 12 };
                    break;
            }
            if (lstChungTu != null && int.Parse(MonthSelected.ValueItem) != 0)
            {
                lstChungTu = lstChungTu.Where(n => lstThang.Contains(n.Thang));
            }
            if (lstChungTu == null || lstChungTu.Count() == 0)
            {
                MessageBoxHelper.Error(Resources.AlertEmptyReport);
                return;
            }
            VerbalExplanationViewModel.ChungTuModel = _mapper.Map<List<TlQtChungTuNq104Model>>(lstChungTu).First();
            VerbalExplanationViewModel.Init();
            VerbalExplanationViewModel.ShowDialog();
        }

        private void OnExport(ExportType exportType)
        {
            try
            {
                BackgroundWorkerHelper.Run((s, e) =>
                {
                    IsLoading = true;
                    LoadData();
                    if (ItemsChungTu == null || ItemsChungTu.Count == 0)
                    {
                        e.Result = new List<ExportResult>();
                        return;
                    }
                    List<ExportResult> results = new List<ExportResult>();
                    if (IsCover)
                    {
                        var dataResult = ExportCover();
                        if (dataResult != null)
                            results.Add(dataResult);
                    }
                    if (IsData)
                    {
                        var dataResult = ExportQtThuongXuyen(SalaryPrintType.BC_CHI_TIET_QUYET_TOAN_TX_SO_LIEU, exportType);
                        if (dataResult != null)
                            results.Add(dataResult);
                    }
                    if (IsDataInterpretation)
                    {
                        var interpretationResult = OnExportDataInterpretation();
                        if (interpretationResult != null)
                            results.Add(interpretationResult);
                    }
                    if (IsVerbalExplanation)
                    {
                        var verbalResult = OnExportVerbalExplaination(exportType);
                        if (verbalResult != null)
                            results.Add(verbalResult);
                    }
                    e.Result = results;
                }, (s, e) =>
                {
                    if (e.Error == null)
                    {
                        var result = (List<ExportResult>)e.Result;
                        if (result.Count() == 0)
                            MessageBoxHelper.Info(Resources.AlertEmptyReport);
                        else _exportService.Open(result, exportType);
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
                MessageBox.Show(Resources.ErrorExportReport, Resources.NotifiTitle, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private ExportResult ExportCover()
        {
            Dictionary<string, object> dicData = new Dictionary<string, object>();
            dicData.Add("Cap1", _danhMucService.FindDonViQuanLy(_sessionInfo.YearOfWork).ToUpper());
            dicData.Add("DonVi", TenDonVi);
            dicData.Add("DiaDiem", _danhMucService.FindByCode(MaDanhMuc.DIADIEM, _sessionService.Current.YearOfWork).SGiaTri);
            string fileNamePrefix = "";
            if (int.Parse(MonthSelected.ValueItem) == 0)
            {
                dicData.Add("THANGNAM1", string.Format("NĂM {0}", Nam));
                dicData.Add("ThangNam", string.Format("năm {0}", Nam));
                fileNamePrefix = string.Format("rpt_To_Bia_QTTX_Nam_{0}_{1}", Nam, TenDonVi);
            }
            else if (int.Parse(MonthSelected.ValueItem) <= 12)
            {
                dicData.Add("THANGNAM1", string.Format("THÁNG {0} NĂM {1}", MonthSelected.ValueItem, Nam));
                dicData.Add("ThangNam", string.Format("tháng {0} năm {1}", MonthSelected.ValueItem, Nam));
                fileNamePrefix = string.Format("rpt_To_Bia_QTTX_Thang_{0}_{1}_{2}", MonthSelected.ValueItem, Nam, TenDonVi);
            }
            else
            {
                dicData.Add("THANGNAM1", string.Format("QUÝ {0} NĂM {1}", (int.Parse(MonthSelected.ValueItem) - 20), Nam));
                dicData.Add("ThangNam", string.Format("quý {0} năm {1}", (int.Parse(MonthSelected.ValueItem) - 20), Nam));
                fileNamePrefix = string.Format("rpt_To_Bia_QTTX_Quy_{0}_{1}_{2}", (int.Parse(MonthSelected.ValueItem) - 20), Nam, TenDonVi);
            }

            string templateFileName = Path.Combine(ExportPrefix.PATH_TL_LUONG_NEW, ExportFileName.RPT_TL_TO_BIA_QTTX_NEW);
            string fileNameWithoutExtension = StringUtils.CreateExportFileName(fileNamePrefix);
            var xlsFile = _exportService.Export<TlQtChungTuChiTietNq104Model>(templateFileName, dicData);
            return new ExportResult(fileNameWithoutExtension, fileNameWithoutExtension, null, xlsFile);
        }

        public ExportResult ExportQtThuongXuyen(SalaryPrintType salaryPrintType = SalaryPrintType.BC_CHI_TIET_QUYET_TOAN_TX_SO_LIEU, ExportType exportType = ExportType.PDF)
        {
            int dvt = int.Parse(SelectedUnit.ValueItem);
            int iThang = int.Parse(MonthSelected.ValueItem);
            List<TlQtChungTuChiTietNq104Model> results = new List<TlQtChungTuChiTietNq104Model>();
            List<TlQtChungTuChiTietNq104Model> itemsChungTuChiTiet = FindChungTuChiTietByPrintType(salaryPrintType);
            if (string.IsNullOrEmpty(MaDonVi))
            {
                MessageBoxHelper.Error(Resources.MsgDonViEmpty);
                return null;
            }
            IEnumerable<TlDmDonViNq104> listDonVi = _tlDmDonViService.FindByDonViCon(MaDonVi);
            if (listDonVi.IsEmpty())
            {
                results = itemsChungTuChiTiet.ToList();
            }
            else
            {
                List<TlQtChungTuChiTietNq104Model> listDataDonViCon = new List<TlQtChungTuChiTietNq104Model>();
                foreach (var donVi in listDonVi)
                {
                    var tlQtChungTu = _tlQtChungTuService.FindAll().FirstOrDefault(x => x.MaDonVi == donVi.MaDonVi && x.Thang == iThang && x.Nam == Nam);
                    if (tlQtChungTu != null)
                    {
                        var listData = _tlQtChungTuChiTietService.FindByChungTuId(tlQtChungTu.Id);
                        var listDataItems = _mapper.Map<ObservableCollection<TlQtChungTuChiTietNq104Model>>(listData);
                        listDataDonViCon.AddRange(listDataItems);
                    }
                }
                var listDataParent = itemsChungTuChiTiet.Where(x => x.Ng != "").ToList();
                foreach (var item in listDataParent)
                {
                    List<TlQtChungTuChiTietNq104Model> donViCon = listDataDonViCon.Where(x => x.Lns == item.Lns && x.L == item.L && x.K == item.K && x.M == item.M && x.Tm == item.Tm && x.Ttm == item.Ttm && x.Ng == item.Ng).Select(x =>
                    {
                        x.IsParent = false;
                        x.MoTa = x.TenDonVi;
                        x.Lns = "";
                        x.L = "";
                        x.K = "";
                        x.M = "";
                        x.Tm = "";
                        x.Ng = "";
                        x.Ttm = "";
                        return x;
                    }).ToList();
                    int size = donViCon.Count();
                    int index = itemsChungTuChiTiet.IndexOf(item);
                    for (int i = 1; i <= size; i++)
                    {
                        itemsChungTuChiTiet.Insert(index + i, donViCon[i - 1]);
                    }
                }
                results = itemsChungTuChiTiet.ToList();
            }

            results = new List<TlQtChungTuChiTietNq104Model>(results.OrderBy(x => x.XauNoiMa));
            //results.Where(n => !n.BHangCha.GetValueOrDefault()).Select(n => { n.Lns = string.Empty; n.L = string.Empty; n.K = string.Empty; n.M = string.Empty; n.Tm = string.Empty; return n; }).ToList();
            //results.Where(n => n.BHangCha.GetValueOrDefault() && !string.IsNullOrEmpty(n.M)).Select(n => { n.L = string.Empty; n.K = string.Empty; n.Lns = string.Empty; return n; }).ToList();
            //results.Where(n => n.BHangCha.GetValueOrDefault() && !string.IsNullOrEmpty(n.Tm)).Select(n => { n.M = string.Empty; return n; }).ToList();

            foreach (var item in results.Where(x => !string.IsNullOrEmpty(x.L)).OrderByDescending(x => x.XauNoiMa))
            {
                var parent = results.Where(x => x.MlnsId == item.MlnsIdParent).LastOrDefault();
                if (parent != null && item.M != string.Empty)
                {
                    if (!string.IsNullOrEmpty(parent.M))
                        item.M = string.Empty;
                    if (!string.IsNullOrEmpty(parent.Tm))
                        item.Tm = string.Empty;
                    if (!string.IsNullOrEmpty(parent.Ttm))
                        item.Ttm = string.Empty;
                    if (!string.IsNullOrEmpty(parent.Ng))
                        item.Ng = string.Empty;
                    if (!string.IsNullOrEmpty(parent.Tng))
                        item.Tng = string.Empty;
                    if (!string.IsNullOrEmpty(parent.Tng1))
                        item.Tng1 = string.Empty;
                    if (!string.IsNullOrEmpty(parent.Tng2))
                        item.Tng2 = string.Empty;
                    if (!string.IsNullOrEmpty(parent.Tng3))
                        item.Tng3 = string.Empty;
                }
            }

            var items = results.Where(x => (x.DieuChinh != 0 && x.DieuChinh != null) || (x.TongLuyKe != 0 && x.TongLuyKe != null) || (x.DDuToan.HasValue && x.DDuToan != 0));
            // Lọc ra những thằng cha có tiền để tính tổng
            var itemsParent = results.Where(s => !string.IsNullOrEmpty(s.M) && string.IsNullOrEmpty(s.Tm) && string.IsNullOrEmpty(s.Ttm) && string.IsNullOrEmpty(s.Tng));

            double TongDieuChinh = (double)itemsParent.Sum(s => s.DieuChinh);
            double TongDuToan = (double)itemsParent.Sum(s => s.DDuToan);
            double TongLuyKe = (double)itemsParent.Sum(s => s.TongLuyKe);

            string tieuDe1 = "BÁO CÁO QUYẾT TOÁN LƯƠNG, PHỤ CẤP, TRỢ CẤP, TIỀN ĂN";
            string tieuDe2 = "Loại 010 - Khoản 011";
            if (salaryPrintType == SalaryPrintType.BC_QUYET_TOAN_LUONG_PHU_CAP)
            {
                tieuDe1 = "BÁO CÁO QUYẾT TOÁN LƯƠNG, PHỤ CẤP";
                tieuDe2 = "Loại 010 - Khoản 011 - Mục 6000 - 6100";
            }
            else if (salaryPrintType == SalaryPrintType.BC_QUYET_TOAN_TIEN_AN)
            {
                tieuDe1 = "BÁO CÁO QUYẾT TOÁN TIỀN ĂN";
                tieuDe2 = "Loại 010 - Khoản 011 - Mục 6400 - Tiểu mục 6401";
            }
            else if (salaryPrintType == SalaryPrintType.BC_QUYET_TOAN_BAO_HIEM)
            {
                tieuDe1 = "BÁO CÁO QUYẾT TOÁN BẢO HIỂM";
                tieuDe2 = "Loại 010 - Khoản 011 - Mục 6300";
            }
            else if (salaryPrintType == SalaryPrintType.BC_QUYET_TOAN_RA_QUAN)
            {
                tieuDe1 = "BÁO CÁO QUYẾT TOÁN CHI RA QUÂN, XUẤT NGŨ";
                tieuDe2 = "Loại 010 - Khoản 011 - Mục 8000";
            }

            FormatNumber formatNumber = new FormatNumber(1, exportType);
            string fileNamePrefix = "";
            var time = "";
            if (iThang == 0)
            {
                fileNamePrefix = string.Format("rpt_Quyet_Toan_Thuong_Xuyen_Nam_{0}_{1}", Nam, TenDonVi);
                time = string.Format("Năm {0}", Nam);
            }
            else if (iThang <= 12)
            {
                fileNamePrefix = string.Format("rpt_Quyet_Toan_Thuong_Xuyen_Thang_{0}_{1}_{2}", iThang, Nam, TenDonVi);
                time = string.Format("Tháng {0} Năm {1}", iThang, Nam);
            }
            else
            {
                fileNamePrefix = string.Format("rpt_Quyet_Toan_Thuong_Xuyen_Quy_{0}_{1}_{2}", (iThang - 20), Nam, TenDonVi);
                time = string.Format("Quý {0} Năm {1}", (iThang - 20), Nam);
            }
            if (items != null)
            {
                items = items.Select(n
                    =>
                {
                    if (n.SoNgay == 0) { n.SoNgay = null; }
                    if (n.SoNguoi == 0) { n.SoNguoi = null; }
                    return n;
                }).ToList();
            }

            items.ForAll(n =>
            {
                n.DDuToan /= dvt;
                n.DieuChinh /= dvt;
                n.TongLuyKe /= dvt;
            });
            Dictionary<string, object> dicData = new Dictionary<string, object>();
            dicData.Add("Cap1", _danhMucService.FindDonViQuanLy(_sessionInfo.YearOfWork).ToUpper());
            dicData.Add("Cap2", GetHeader2Report());
            dicData.Add("DonVi", TenDonVi);
            dicData.Add("DonViTinh", SelectedUnit.DisplayItem);
            dicData.Add("Items", items);
            dicData.Add("TieuDe1", tieuDe1);
            dicData.Add("TieuDe2", tieuDe2);
            dicData.Add("GhiChu", Note);
            dicData.Add("DiaDiem", _diaDiem);
            dicData.Add("Ngay", StringUtils.CreateDateTimeString());
            //dicData.Add("ChucDanh1", _dmChuKy != null ? _dmChuKy.ChucDanh1MoTa : string.Empty);
            //dicData.Add("ChucDanh2", _dmChuKy != null ? _dmChuKy.ChucDanh2MoTa : string.Empty);
            //dicData.Add("ChucDanh3", _dmChuKy != null ? _dmChuKy.ChucDanh3MoTa : string.Empty);
            //dicData.Add("ThuaLenh1", _dmChuKy != null ? _dmChuKy.ThuaLenh1MoTa : string.Empty);
            //dicData.Add("ThuaLenh2", _dmChuKy != null ? _dmChuKy.ThuaLenh2MoTa : string.Empty);
            //dicData.Add("ThuaLenh3", _dmChuKy != null ? _dmChuKy.ThuaLenh3MoTa : string.Empty);
            //dicData.Add("Ten1", _dmChuKy != null ? _dmChuKy.Ten1MoTa : string.Empty);
            //dicData.Add("Ten2", _dmChuKy != null ? _dmChuKy.Ten2MoTa : string.Empty);
            //dicData.Add("Ten3", _dmChuKy != null ? _dmChuKy.Ten3MoTa : string.Empty);
            dicData.Add("TongDuToan", TongDuToan);
            dicData.Add("TongLuyKe", TongLuyKe);
            dicData.Add("TongDieuChinh", TongDieuChinh);
            dicData.Add("FormatNumber", formatNumber);
            dicData.Add("Nam", time);
            dicData.Add("TienQuyetToan", StringUtils.NumberToText(TongDieuChinh));
            dicData.Add("DonViCha", _sessionService?.Current?.TenDonViReportHeader);
            AddChuKy(dicData, TypeChuKy.RPT_TL_QUYETTOAN_THUONGXUYEN_NEW);

            string templateFileName = Path.Combine(ExportPrefix.PATH_TL_LUONG_NEW, ExportFileName.RPT_TL_QT_THUONGXUYEN_NEW);
            string fileNameWithoutExtension = StringUtils.CreateExportFileName(fileNamePrefix);
            var xlsFile = _exportService.Export<TlQtChungTuChiTietNq104Model>(templateFileName, dicData);
            return new ExportResult(fileNameWithoutExtension, fileNameWithoutExtension, null, xlsFile);
        }

        public string GetHeader2Report()
        {
            DonVi donViParent = _donViService.FindByLoai(LoaiDonVi.ROOT, _sessionInfo.YearOfWork);
            return donViParent != null ? donViParent.TenDonVi.ToUpper() : string.Empty;
        }

        private void LoadChungTuData()
        {
            var lstChungTu = new List<TlQtChungTuNq104>();
            if (!SNam.IsEmpty() && MonthSelected != null)
                lstChungTu = _tlQtChungTuService.FindAll(n => n.Nam.ToString() == SNam && n.Thang.ToString() == MonthSelected.ValueItem).ToList();
            List<TlQtChungTuNq104> lstChungTuTheoDonVi = new List<TlQtChungTuNq104>();
            if (lstChungTu != null && lstChungTu.Any(n => !string.IsNullOrEmpty(n.MaDonVi)) && _donviSelected != null)
            {
                if (_bIsSummary)
                {
                    lstChungTuTheoDonVi = lstChungTu.Where(n => !string.IsNullOrEmpty(n.MaDonVi) && !n.STongHop.IsEmpty() && n.MaDonVi == _donviSelected.MaDonVi).ToList();
                }
                else
                {
                    lstChungTuTheoDonVi = lstChungTu.Where(n => !string.IsNullOrEmpty(n.MaDonVi) && n.STongHop.IsEmpty() && n.MaDonVi == _donviSelected.MaDonVi).ToList();
                }
                if (lstChungTuTheoDonVi.Count > 0)
                {
                    _soChungTu = lstChungTuTheoDonVi.FirstOrDefault().SoChungTu;
                    _dNgayChungTu = lstChungTuTheoDonVi.FirstOrDefault().NgayTao;
                }
                else
                {
                    _soChungTu = string.Empty;
                    _dNgayChungTu = null;
                }
            }
            else
            {
                _soChungTu = string.Empty;
                _dNgayChungTu = null;
            }
            OnPropertyChanged(nameof(SoChungTu));
            OnPropertyChanged(nameof(DNgayChungTu));
        }
        private void LoadDonViData()
        {
            _donviItems = new ObservableCollection<TlDmDonViNq104Model>();
            var predicate = PredicateBuilder.True<TlDmDonViNq104Model>();
            var lstChungTu = new List<TlQtChungTuNq104>();
            if (!SNam.IsEmpty() && MonthSelected != null)
                lstChungTu = _tlQtChungTuService.FindAll(n => n.Nam.ToString() == SNam && n.Thang == int.Parse(MonthSelected.ValueItem)).ToList();
            List<TlDmDonViNq104Model> lstDonVi = new List<TlDmDonViNq104Model>();
            if (lstChungTu != null && lstChungTu.Any(n => !string.IsNullOrEmpty(n.MaDonVi)))
            {
                if (_bIsSummary)
                {
                    lstDonVi = lstChungTu.Where(n => !string.IsNullOrEmpty(n.MaDonVi) && !n.STongHop.IsEmpty()).Select(n => new TlDmDonViNq104Model() { MaDonVi = n.MaDonVi, TenDonVi = n.TenDonVi }).ToList();
                }
                else
                {
                    lstDonVi = lstChungTu.Where(n => !string.IsNullOrEmpty(n.MaDonVi) && n.STongHop.IsEmpty()).Select(n => new TlDmDonViNq104Model() { MaDonVi = n.MaDonVi, TenDonVi = n.TenDonVi }).ToList();
                }

                lstDonVi = lstDonVi.GroupBy(x => x.MaDonVi).Select(x => x.First()).ToList();
                DonViItems = new ObservableCollection<TlDmDonViNq104Model>(_mapper.Map<List<TlDmDonViNq104Model>>(lstDonVi));
            }
            else
            {
                DonViItems = null;
            }

            //if (_bIsSummary)
            //{
            //    var yearOfWork = _sessionService.Current.YearOfWork;
            //    var predicate = PredicateBuilder.True<DonVi>();
            //    predicate = predicate.And(x => x.NamLamViec == yearOfWork);
            //    var listUnit = _donViService.FindByCondition(predicate).OrderBy(x => x.Loai).ThenBy(x => x.TenDonVi).ToList();
            //    if (listUnit != null)
            //        DonViItems = new ObservableCollection<TlDmDonViModel>(listUnit.Where(n=> lstDonVi.Contains(n.IIDMaDonVi)).Select(n => new TlDmDonViModel()
            //        {
            //            TenDonVi = n.TenDonVi,
            //            MaDonVi = n.IIDMaDonVi
            //        }));
            //}
            //else
            //{
            //    var data = _tlDmDonViService.FindAll().Where(n => lstDonVi.Contains(n.MaDonVi)).OrderBy(x => x.XauNoiMa);
            //    DonViItems = new ObservableCollection<TlDmDonViModel>(_mapper.Map<List<TlDmDonViModel>>(data));
            //}

            //var data = _tlDmDonViService.FindAll().Where(n => lstDonVi.Contains(n.MaDonVi)).OrderBy(x => x.XauNoiMa);

            if (DonViItems != null)
            {
                if (!string.IsNullOrEmpty(MaDonVi))
                {
                    DonViSelected = DonViItems.FirstOrDefault(n => n.MaDonVi == MaDonVi);
                }
                else
                {
                    DonViSelected = DonViItems.FirstOrDefault();
                }
                OnPropertyChanged(nameof(DonViSelected));
            }
            else
            {
                DonViSelected = null;
            }
            OnPropertyChanged(nameof(DonViItems));
        }

        private ExportResult OnExportDataInterpretation()
        {
            int iThang = int.Parse(MonthSelected.ValueItem);
            //RegularDataIntertationViewModel.Model = ChungTuModel.First();
            RegularDataIntertationViewModel.ItemsChungTu = ItemsChungTu;
            RegularDataIntertationViewModel.BIsSummary = BIsSummary;
            RegularDataIntertationViewModel.MaDonVi = MaDonVi;
            RegularDataIntertationViewModel.TenDonVi = TenDonVi;
            RegularDataIntertationViewModel.Thang = int.Parse(MonthSelected.ValueItem);
            RegularDataIntertationViewModel.Nam = Nam;
            RegularDataIntertationViewModel.Init();
            //RegularDataIntertationViewModel.OnSaveRegularDataIntertationViewModel();
            //LoadData();
            if (_modelGiaiThich == null) return null;

            Dictionary<string, object> data = new Dictionary<string, object>();
            data.Add("FLuongSiQuan", _modelGiaiThich.FLuongSiQuan);
            data.Add("FLuongQncn", _modelGiaiThich.FLuongQNCN);
            data.Add("FLuongCnvqp", _modelGiaiThich.FLuongCNVC);
            data.Add("FLuongHd", _modelGiaiThich.FLuongHDLD);
            data.Add("FPhuCapSiQuan", _modelGiaiThich.FPcSiQuan);
            data.Add("FPhuCapQncn", _modelGiaiThich.FPcQNCN);
            data.Add("FPhuCapCnvqp", _modelGiaiThich.FPcCNVC);
            data.Add("FPhuCapHd", _modelGiaiThich.FPcHDLD);

            data.Add("FLuongSiQuanTru", _modelGiaiThich.FLuongSiQuanTru);
            data.Add("FLuongQncnTru", _modelGiaiThich.FLuongQncnTru);
            data.Add("FLuongCnvqpTru", _modelGiaiThich.FLuongCnvqpTru);
            data.Add("FLuongHdTru", _modelGiaiThich.FLuongHdTru);
            data.Add("FPhuCapSiQuanTru", _modelGiaiThich.FPhuCapSiQuanTru);
            data.Add("FPhuCapQncnTru", _modelGiaiThich.FPhuCapQncnTru);
            data.Add("FPhuCapCnvqpTru", _modelGiaiThich.FPhuCapCnvqpTru);
            data.Add("FPhuCapHdTru", _modelGiaiThich.FPhuCapHdTru);

            data.Add("FLuongBhxhSiQuanTru", _modelGiaiThich.FLuongBhxhSiQuanTru);
            data.Add("FLuongBhxhQncnTru", _modelGiaiThich.FLuongBhxhQncnTru);
            data.Add("FLuongBhxhCnvqpTru", _modelGiaiThich.FLuongBhxhCnvqpTru);
            data.Add("FLuongBhxhHdTru", _modelGiaiThich.FLuongBhxhHdTru);

            data.Add("FPhuCapBhxhSiQuanTru", _modelGiaiThich.FPhuCapBhxhSiQuanTru);
            data.Add("FPhuCapBhxhQncnTru", _modelGiaiThich.FPhuCapBhxhQncnTru);
            data.Add("FPhuCapBhxhCnvqpTru", _modelGiaiThich.FPhuCapBhxhCnvqpTru);
            data.Add("FPhuCapBhxhHdTru", _modelGiaiThich.FPhuCapBhxhHdTru);

            data.Add("FLuongQtSiquan", _modelGiaiThich.FLuongQtSiquan);
            data.Add("FLuongQtQncn", _modelGiaiThich.FLuongQtQncn);
            data.Add("FLuongQtCnvc", _modelGiaiThich.FLuongQtCnvc);
            data.Add("FLuongQtHd", _modelGiaiThich.FLuongQtHd);

            data.Add("FPhuCapQtSiquan", _modelGiaiThich.FPhuCapQtSiquan);
            data.Add("FPhuCapQtQncn", _modelGiaiThich.FPhuCapQtQncn);
            data.Add("FPhuCapQtCnvc", _modelGiaiThich.FPhuCapQtCnvc);
            data.Add("FPhuCapQtHd", _modelGiaiThich.FPhuCapQtHd);

            data.Add("FNgayAn", _modelGiaiThich.FNgayAn);
            data.Add("FNgayAnCong", _modelGiaiThich.FNgayAnCong);
            data.Add("FNgayAnTru", _modelGiaiThich.FNgayAnTru);
            data.Add("FNgayAnQt", _modelGiaiThich.FNgayAnQt);

            data.Add("FKinhPhiLuongPcKhac", _modelGiaiThich.FKinhPhiLuongPcKhac);
            data.Add("FKinhPhiPhuCapHsqbs", _modelGiaiThich.FKinhPhiPhuCapHsqbs);
            data.Add("FKinhPhiAn", _modelGiaiThich.FKinhPhiAn);

            data.Add("IXuatNguSiQuan", _modelGiaiThich.IXuatNguSiQuan);
            data.Add("IXuatNguQncn", _modelGiaiThich.IXuatNguQncn);
            data.Add("IXuatNguKhac", _modelGiaiThich.IXuatNguKhac);
            data.Add("IXuatNguHsqbs", _modelGiaiThich.IXuatNguHsqbs);

            data.Add("IHuuSiQuan", _modelGiaiThich.IHuuSiQuan);
            data.Add("IHuuQncn", _modelGiaiThich.IHuuQncn);
            data.Add("IHuuKhac", _modelGiaiThich.IHuuKhac);
            data.Add("IHuuHsqbs", _modelGiaiThich.IHuuHsqbs);

            data.Add("IThoiViecSiQuan", _modelGiaiThich.IThoiViecSiQuan);
            data.Add("IThoiViecQncn", _modelGiaiThich.IThoiViecQncn);
            data.Add("IThoiViecKhac", _modelGiaiThich.IThoiViecKhac);
            data.Add("IThoiViecHsqbs", _modelGiaiThich.IThoiViecHsqbs);

            data.Add("FRaQuanSiQuanXuatNgu", _modelGiaiThich.XuatNguSQ);
            data.Add("FRaQuanQncnXuatNgu", _modelGiaiThich.XuatNguQNCN);
            data.Add("FRaQuanCnvqpXuatNgu", _modelGiaiThich.XuatNguCNVC);
            data.Add("FRaQuanHsqcsXuatNgu", _modelGiaiThich.XuatNguHSQ);
            data.Add("FRaQuanSiQuanHuu", _modelGiaiThich.HuuSQ);
            data.Add("FRaQuanQncnHuu", _modelGiaiThich.HuuQNCN);
            data.Add("FRaQuanCnvqpHuu", _modelGiaiThich.HuuCNVC);
            data.Add("FRaQuanHsqcsHuu", _modelGiaiThich.HuuHSQ);
            data.Add("FRaQuanSiQuanThoiViec", _modelGiaiThich.ThoiViecSQ);
            data.Add("FRaQuanQncnThoiViec", _modelGiaiThich.ThoiViecQNCN);
            data.Add("FRaQuanCnvqpThoiViec", _modelGiaiThich.ThoiViecCNVC);
            data.Add("FRaQuanHsqcsThoiViec", _modelGiaiThich.ThoiViecHSQ);
            data.Add("HEADER2", TenDonVi);

            string templateFileName = Path.Combine(ExportPrefix.PATH_TL_LUONG_NEW, ExportFileName.RPT_TL_BAOCAO_GIAITHICH_SOLIEU_NEW);
            var xlsFile = _exportService.Export<TlQtChungTuChiTietGiaiThichNq104Model>(templateFileName, data);
            string fileNamePrefix = "";
            if (iThang == 0)
            {
                fileNamePrefix = string.Format("rpt_GiaiThich_SoLieu_Nam_{0}_{1}", Nam, TenDonVi);
            }
            else if (iThang <= 12)
            {
                fileNamePrefix = string.Format("rpt_GiaiThich_SoLieu_Thang_{0}_{1}_{2}", iThang, Nam, TenDonVi);
            }
            else
            {
                fileNamePrefix = string.Format("rpt_GiaiThich_SoLieu_Quy_{0}_{1}_{2}", (iThang - 20), Nam, TenDonVi);
            }
            string fileNameWithoutExtension = StringUtils.CreateExportFileName(fileNamePrefix);
            return new ExportResult(string.Format("{0} - {1}", MaDonVi, TenDonVi), fileNameWithoutExtension, null, xlsFile);
        }

        /// <summary>
        /// Xuất tờ giải thích bằng lời
        /// </summary>
        private ExportResult OnExportVerbalExplaination(ExportType exportType)
        {
            if (_modelGiaiThich == null) return null;

            var tongQuyetToan = (double)_itemsChiTiet.Where(x => x.BHangCha != true && x.DieuChinh != null).Sum(x => x.DieuChinh);
            RptTLQuyetToanGiaiThichLoi rptGiaiThichLoi = new RptTLQuyetToanGiaiThichLoi();
            rptGiaiThichLoi.Tien = tongQuyetToan;
            rptGiaiThichLoi.TienTuChi = StringUtils.NumberToText(tongQuyetToan)?.Replace(".", "");
            rptGiaiThichLoi.DonVi = TenDonVi.Split(" - ").Last();
            rptGiaiThichLoi.MoTaTinhHinh = _modelGiaiThich.SMoTaTinhHinh;
            rptGiaiThichLoi.MoTaKienNghi = _modelGiaiThich.SMoTaKienNghi;

            _dmChuKy = _dmChuKyService.FindByCondition(x => x.IdType.Equals(_typeChuKy) && x.ITrangThai == StatusType.ACTIVE).FirstOrDefault();
            rptGiaiThichLoi.Ngay = StringUtils.CreateDateTimeString();
            rptGiaiThichLoi.ChucDanh1 = _dmChuKy != null ? _dmChuKy.ChucDanh1MoTa : string.Empty;
            rptGiaiThichLoi.ChucDanh2 = _dmChuKy != null ? _dmChuKy.ChucDanh2MoTa : string.Empty;
            rptGiaiThichLoi.ChucDanh3 = _dmChuKy != null ? _dmChuKy.ChucDanh3MoTa : string.Empty;
            rptGiaiThichLoi.ThuaLenh1 = _dmChuKy != null ? _dmChuKy.ThuaLenh1MoTa : string.Empty;
            rptGiaiThichLoi.ThuaLenh2 = _dmChuKy != null ? _dmChuKy.ThuaLenh2MoTa : string.Empty;
            rptGiaiThichLoi.ThuaLenh3 = _dmChuKy != null ? _dmChuKy.ThuaLenh3MoTa : string.Empty;
            rptGiaiThichLoi.Ten1 = _dmChuKy != null ? _dmChuKy.Ten1MoTa : string.Empty;
            rptGiaiThichLoi.Ten2 = _dmChuKy != null ? _dmChuKy.Ten2MoTa : string.Empty;
            rptGiaiThichLoi.Ten3 = _dmChuKy != null ? _dmChuKy.Ten3MoTa : string.Empty;
            rptGiaiThichLoi.ChucDanh4 = _dmChuKy != null ? _dmChuKy.ChucDanh4MoTa : string.Empty;
            rptGiaiThichLoi.ChucDanh5 = _dmChuKy != null ? _dmChuKy.ChucDanh5MoTa : string.Empty;
            rptGiaiThichLoi.ChucDanh6 = _dmChuKy != null ? _dmChuKy.ChucDanh6MoTa : string.Empty;
            rptGiaiThichLoi.ThuaLenh4 = _dmChuKy != null ? _dmChuKy.ThuaLenh4MoTa : string.Empty;
            rptGiaiThichLoi.ThuaLenh5 = _dmChuKy != null ? _dmChuKy.ThuaLenh5MoTa : string.Empty;
            rptGiaiThichLoi.ThuaLenh6 = _dmChuKy != null ? _dmChuKy.ThuaLenh6MoTa : string.Empty;
            rptGiaiThichLoi.Ten4 = _dmChuKy != null ? _dmChuKy.Ten4MoTa : string.Empty;
            rptGiaiThichLoi.Ten5 = _dmChuKy != null ? _dmChuKy.Ten5MoTa : string.Empty;
            rptGiaiThichLoi.Ten6 = _dmChuKy != null ? _dmChuKy.Ten6MoTa : string.Empty;

            Dictionary<string, object> data = new Dictionary<string, object>();
            int dvt = 1;
            FormatNumber formatNumber = new FormatNumber(dvt, exportType);
            data.Add("FormatNumber", formatNumber);
            foreach (var prop in rptGiaiThichLoi.GetType().GetProperties())
            {
                data.Add(prop.Name, prop.GetValue(rptGiaiThichLoi));
            }

            string templateFileName = Path.Combine(ExportPrefix.PATH_TL_LUONG_NEW, ExportFileName.RPT_TL_BAOCAO_GIAITHICH_LOI_NEW);
            string fileNamePrefix = ExportFileName.RPT_TL_BAOCAO_GIAITHICH_LOI_NEW.Split(".").First() + "_" + TenDonVi;
            string fileNameWithoutExtension = StringUtils.CreateExportFileName(fileNamePrefix);
            var xlsFile = _exportService.Export<ReportQtChungTuChiTietQuery>(templateFileName, data);
            return new ExportResult(fileNameWithoutExtension, fileNameWithoutExtension, null, xlsFile);
        }

        private void OnConfigSign()
        {
            DmChuKyModel chuKyModel = new DmChuKyModel();
            _dmChuKy = _dmChuKyService.FindByCondition(x => x.IdType.Equals(_typeChuKy) && x.ITrangThai == StatusType.ACTIVE).FirstOrDefault();
            if (_dmChuKy == null)
                chuKyModel.IdType = _typeChuKy;
            else
                chuKyModel = _mapper.Map<DmChuKyModel>(_dmChuKy);
            DmChuKyDialogViewModel.DmChuKyModel = chuKyModel;
            DmChuKyDialogViewModel.HasAddedSign4 = IsVerbalExplanation;
            DmChuKyDialogViewModel.HasAddedSign5 = IsVerbalExplanation;
            DmChuKyDialogViewModel.HasAddedSign6 = IsVerbalExplanation;
            DmChuKyDialogViewModel.SavedAction = obj => LoadTieuDe();
            DmChuKyDialogViewModel.Init();
            DmChuKyDialogViewModel.ShowDialog();
        }

        public override void LoadData(params object[] args)
        {
            string cachTL = string.Empty;
            List<int> lstThangSelected = new List<int>();
            int iThang = int.Parse(MonthSelected.ValueItem);
            _itemsChiTiet = new List<TlQtChungTuChiTietNq104Model>();
            List<TlQtChungTuChiTietNq104> lstLuyKeChiTiet = new List<TlQtChungTuChiTietNq104>();
            IEnumerable<TlQtChungTuNq104> lstChungTuLuyKe = null;
            IEnumerable<TlQtChungTuNq104> lstChungTu = _tlQtChungTuService.FindAll(n => n.Nam == Nam && n.MaDonVi == MaDonVi);
            if (lstChungTu != null)
            {
                switch (iThang)
                {
                    case 1:
                    case 2:
                    case 3:
                    case 4:
                    case 5:
                    case 6:
                    case 7:
                    case 8:
                    case 9:
                    case 10:
                    case 11:
                    case 12:
                        lstThangSelected.Add(iThang);
                        lstChungTuLuyKe = lstChungTu.Where(n => n.Thang <= (iThang - 1));
                        lstChungTu = lstChungTu.Where(n => n.Thang == iThang);
                        break;
                    case (int)DateTimeExtension.TimeConst.Types.QUY_1:
                        lstThangSelected = new List<int>() { 1, 2, 3 };
                        lstChungTu = lstChungTu.Where(n => n.Thang <= 3);
                        break;
                    case (int)DateTimeExtension.TimeConst.Types.QUY_2:
                        lstThangSelected = new List<int>() { 4, 5, 6 };
                        lstChungTuLuyKe = lstChungTu.Where(n => n.Thang <= 3);
                        lstChungTu = lstChungTu.Where(n => n.Thang >= 4 && n.Thang <= 6);
                        break;
                    case (int)DateTimeExtension.TimeConst.Types.QUY_3:
                        lstThangSelected = new List<int>() { 7, 8, 9 };
                        lstChungTuLuyKe = lstChungTu.Where(n => n.Thang <= 6);
                        lstChungTu = lstChungTu.Where(n => n.Thang >= 7 && n.Thang <= 9);
                        break;
                    case (int)DateTimeExtension.TimeConst.Types.QUY_4:
                        lstThangSelected = new List<int>() { 10, 11, 12 };
                        lstChungTuLuyKe = lstChungTu.Where(n => n.Thang <= 9);
                        lstChungTu = lstChungTu.Where(n => n.Thang >= 10);
                        break;
                    default:
                        lstThangSelected = new List<int>() { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12 };
                        break;
                }
                if (!BIsDetailView)
                    ItemsChungTu = _mapper.Map<List<TlQtChungTuNq104Model>>(lstChungTu);
            }
            if (ItemsChungTu == null || ItemsChungTu.Count == 0)
            {
                return;
            }
            if (ItemsChungTu != null)
            {
                var lstChungTuId = "";
                if (BIsSummary) lstChungTuId = string.Join(",", ItemsChungTu.Select(n => n.STongHop));
                else lstChungTuId = string.Join(",", ItemsChungTu.Select(n => n.Id.ToString()));
                if (lstChungTuId.IsEmpty())
                    return;

                var data = _tlQtChungTuChiTietService.GetDataChungTuChiTietNq104Export(string.Join(",", lstChungTuId), Nam, MaDonVi, BIsSummary);
                if (data != null)
                    _itemsChiTiet = _mapper.Map<List<TlQtChungTuChiTietNq104Model>>(data);
            }
            if (lstChungTuLuyKe != null)
            {
                var lstCtId = lstChungTuLuyKe.Select(x => x.Id).ToList();
                List<string> lstCachTinhLuong = SCachTinhLuong.Split(",").ToList();
                var lstCtChiTietLuyke = _tlQtChungTuChiTietService.FindAll(x => lstCtId.Contains(x.IdChungTu) && x.MaCachTl == String.Empty);
                if (lstCtChiTietLuyke != null)
                    lstLuyKeChiTiet = lstCtChiTietLuyke.ToList();
            }

            foreach (var item in _itemsChiTiet)
            {
                if (item.BHangCha != true)
                {
                    var lstCt = lstLuyKeChiTiet.Where(x => x.XauNoiMa.Equals(item.XauNoiMa));
                    if (lstCt != null)
                        item.TongLuyKe = lstCt.Sum(x => x.DieuChinh);
                }
            }

            CalculateData(_itemsChiTiet);

            _modelGiaiThich = new TlQtChungTuChiTietGiaiThichNq104Model();
            var dataGiaiThich = _tlQtChungTuChiTietGiaiThichService.FindByCondition(string.Join(",", lstThangSelected), Nam, MaDonVi);
            //if (dataGiaiThich != null)
            //{
            //    _modelGiaiThich = _mapper.Map<TlQtChungTuChiTietGiaiThichModel>(dataGiaiThich);
            //    CalculateSettlementSalary();
            //    CountRaQuanXuatNgu();
            //}
            if (dataGiaiThich != null)
            {
                _modelGiaiThich = _mapper.Map<TlQtChungTuChiTietGiaiThichNq104Model>(dataGiaiThich);
            }
            else
            {
                CalculateSettlementSalary();
            }
            CountRaQuanXuatNgu();
        }

        private void CountRaQuanXuatNgu()
        {
            var predicate = PredicateBuilder.True<TlDmCanBoNq104>();
            predicate = predicate.And(x => x.Nam == Nam);
            predicate = predicate.And(x => x.Thang == int.Parse(MonthSelected.ValueItem));
            predicate = predicate.And(x => MaDonVi.Equals(x.Parent));
            predicate = predicate.And(x => x.ITrangThai == 3);
            var listCanBo = _tlDmCanBoService.FindByCondition(predicate).ToList();
            List<string> maTangGiam = new List<string>() { "310", "320", "330" };
            foreach (var item in maTangGiam)
            {
                var listData = listCanBo.Where(x => item.Equals(x.MaTangGiam));
                // xuất ngũ
                _modelGiaiThich.XuatNguSQ = listData.Count(x => x.MaCb.StartsWith("1"));
                _modelGiaiThich.XuatNguQNCN = listData.Count(x => x.MaCb.StartsWith("2"));
                _modelGiaiThich.XuatNguCNVC = listData.Count(x => x.MaCb.StartsWith("4"));
                _modelGiaiThich.XuatNguHSQ = listData.Count(x => x.MaCb.StartsWith("0"));

                // về hưu
                _modelGiaiThich.HuuSQ = listData.Count(x => x.MaCb.StartsWith("1"));
                _modelGiaiThich.HuuQNCN = listData.Count(x => x.MaCb.StartsWith("2"));
                _modelGiaiThich.HuuCNVC = listData.Count(x => x.MaCb.StartsWith("4"));
                _modelGiaiThich.HuuHSQ = listData.Count(x => x.MaCb.StartsWith("0"));

                // thôi việc
                _modelGiaiThich.ThoiViecHSQ = listData.Count(x => x.MaCb.StartsWith("1"));
                _modelGiaiThich.ThoiViecQNCN = listData.Count(x => x.MaCb.StartsWith("2"));
                _modelGiaiThich.ThoiViecCNVC = listData.Count(x => x.MaCb.StartsWith("4"));
                _modelGiaiThich.ThoiViecHSQ = listData.Count(x => x.MaCb.StartsWith("0"));
            }
        }

        private void CalculateSettlementSalary()
        {
            string idChungTu = string.Empty; ;
            if (BIsSummary) idChungTu = string.Join(",", ItemsChungTu.Select(x => x.STongHop));
            else idChungTu = string.Join(",", ItemsChungTu.Select(x => x.Id.ToString()));
            var data = _tlQtChungTuChiTietService.GetDataGiaiThichBangSoNq104(idChungTu, Nam, MaDonVi, BIsSummary).ToList();
            var itemsChungTuChiTiet = _mapper.Map<ObservableCollection<TlQtChungTuChiTietNq104Model>>(data).ToList();
            //_modelGiaiThich.FLuongSiQuan = GetDataFromSumWage(new List<string> { "1010000-010-011-6000-6001-10-00" });
            //_modelGiaiThich.FPcSiQuan = GetDataFromSumWage(new List<string> { "1010000-010-011-6100-6101-10-00", "1010000-010-011-6100-6107-10-00",
            //    "1010000-010-011-6100-6124-10-00", "1010000-010-011-6100-6102-10-00", "1010000-010-011-6100-6103-10-00"});
            //_modelGiaiThich.FPcSiQuan = GetDataFromSumWage(_itemsChiTiet.Where(n => n.XauNoiMa.StartsWith("1010000-010-011-6100") && n.MaCb.Equals("1")).Select(n => n.XauNoiMa).ToList());
            //_modelGiaiThich.FLuongSiQuan = itemsChungTuChiTiet.Where(n => n.XauNoiMa.StartsWith(XAU_NOI_MA.TIEN_LUONG) && n.MaCb != null && n.MaCb.Equals(MA_CB.SQ)).Sum(n => (double)(n.DieuChinh ?? 0));
            //_modelGiaiThich.FPcSiQuan = itemsChungTuChiTiet.Where(n => n.XauNoiMa.StartsWith(XAU_NOI_MA.PHU_CAP_LUONG) && n.MaCb != null && n.MaCb.Equals(MA_CB.SQ)).Sum(n => (double)(n.DieuChinh ?? 0));

            //_modelGiaiThich.FLuongQNCN = GetDataFromSumWage(new List<string> { "1010000-010-011-6000-6001-20-00", "1010000-010-011-6000-6001-20-00-01" });
            //_modelGiaiThich.FPcQNCN = GetDataFromSumWage(new List<string> { "1010000-010-011-6100-6101-20-00", "1010000-010-011-6100-6107-20-00",
            //    "1010000-010-011-6100-6124-20-00", "1010000-010-011-6100-6102-20-00", "1010000-010-011-6100-6103-20-00"});
            //_modelGiaiThich.FPcQNCN = GetDataFromSumWage(_itemsChiTiet.Where(n => n.XauNoiMa.StartsWith("1010000-010-011-6100") && n.MaCb.Equals("2")).Select(n => n.XauNoiMa).ToList());
            //_modelGiaiThich.FLuongQNCN = itemsChungTuChiTiet.Where(n => n.XauNoiMa.StartsWith(XAU_NOI_MA.TIEN_LUONG) && n.MaCb != null && n.MaCb.Equals(MA_CB.QNCN)).Sum(n => (double)(n.DieuChinh ?? 0));
            //_modelGiaiThich.FPcQNCN = itemsChungTuChiTiet.Where(n => n.XauNoiMa.StartsWith(XAU_NOI_MA.PHU_CAP_LUONG) && n.MaCb != null && n.MaCb.Equals(MA_CB.QNCN)).Sum(n => (double)(n.DieuChinh ?? 0));

            //_modelGiaiThich.FLuongCNVC = GetDataFromSumWage(new List<string> { "1010000-010-011-6000-6001-30-00", "1010000-010-011-6000-6001-40-00", "1010000-010-011-6000-6001-70-00" });
            //_modelGiaiThich.FPcCNVC = GetDataFromSumWage(new List<string> { "1010000-010-011-6100-6101-30-00", "1010000-010-011-6100-6107-30-00",
            //    "1010000-010-011-6100-6124-30-00", "1010000-010-011-6100-6102-30-00", "1010000-010-011-6100-6103-30-00",
            //    "1010000-010-011-6100-6101-40-00", "1010000-010-011-6100-6107-40-00", "1010000-010-011-6100-6124-40-00", "1010000-010-011-6100-6102-40-00", "1010000-010-011-6100-6103-40-00",
            //    "1010000-010-011-6100-6101-70-00", "1010000-010-011-6100-6107-70-00", "1010000-010-011-6100-6124-70-00", "1010000-010-011-6100-6102-70-00", "1010000-010-011-6100-6103-70-00"});
            //_modelGiaiThich.FPcCNVC = GetDataFromSumWage(_itemsChiTiet.Where(n => n.XauNoiMa.StartsWith("1010000-010-011-6100") && n.MaCb.Equals("3.3")).Select(n => n.XauNoiMa).ToList());
            //_modelGiaiThich.FLuongCNVC = itemsChungTuChiTiet.Where(n => n.XauNoiMa.StartsWith(XAU_NOI_MA.TIEN_LUONG) && n.MaCb != null && n.MaCb.Equals(MA_CB.CNVC)).Sum(n => (double)(n.DieuChinh ?? 0));
            //_modelGiaiThich.FPcCNVC = itemsChungTuChiTiet.Where(n => n.XauNoiMa.StartsWith(XAU_NOI_MA.PHU_CAP_LUONG) && n.MaCb != null && n.MaCb.Equals(MA_CB.CNVC)).Sum(n => (double)(n.DieuChinh ?? 0));

            //_modelGiaiThich.FLuongHDLD = GetDataFromSumWage(new List<string> { "1010000-010-011-6000-6001-90-00" });
            //_modelGiaiThich.FPcHDLD = GetDataFromSumWage(_itemsChiTiet.Where(n => n.XauNoiMa.StartsWith("1010000-010-011-6100") && n.MaCb.Equals("4")).Select(n => n.XauNoiMa).ToList());
            //_modelGiaiThich.FLuongHDLD = itemsChungTuChiTiet.Where(n => n.XauNoiMa.StartsWith(XAU_NOI_MA.TIEN_LUONG) && n.MaCb != null && n.MaCb.Equals(MA_CB.LDHD)).Sum(n => (double)(n.DieuChinh ?? 0));
            //_modelGiaiThich.FPcHDLD = itemsChungTuChiTiet.Where(n => n.XauNoiMa.StartsWith(XAU_NOI_MA.PHU_CAP_LUONG) && n.MaCb != null && n.MaCb.Equals(MA_CB.LDHD)).Sum(n => (double)(n.DieuChinh ?? 0));

            _modelGiaiThich.FLuongSiQuan = itemsChungTuChiTiet.Where(n => n.XauNoiMa.StartsWith(XAU_NOI_MA.TIEN_LUONG) && n.MaCb != null && n.MaCb.Equals(MA_CB.SQ)).Sum(n => (double)(n.DieuChinh ?? 0));
            _modelGiaiThich.FPcSiQuan = itemsChungTuChiTiet.Where(n => n.XauNoiMa.StartsWith(XAU_NOI_MA.PHU_CAP_LUONG) && n.MaCb != null && n.MaCb.Equals(MA_CB.SQ)).Sum(n => (double)(n.DieuChinh ?? 0));

            _modelGiaiThich.FLuongQNCN = itemsChungTuChiTiet.Where(n => n.XauNoiMa.StartsWith(XAU_NOI_MA.TIEN_LUONG) && n.MaCb != null && n.MaCb.Equals(MA_CB.QNCN)).Sum(n => (double)(n.DieuChinh ?? 0));
            _modelGiaiThich.FPcQNCN = itemsChungTuChiTiet.Where(n => n.XauNoiMa.StartsWith(XAU_NOI_MA.PHU_CAP_LUONG) && n.MaCb != null && n.MaCb.Equals(MA_CB.QNCN)).Sum(n => (double)(n.DieuChinh ?? 0));

            _modelGiaiThich.FLuongCNVC = itemsChungTuChiTiet.Where(n => n.XauNoiMa.StartsWith(XAU_NOI_MA.TIEN_LUONG) && n.MaCb != null && IsCnvc(n)).Sum(n => (double)(n.DieuChinh ?? 0));
            _modelGiaiThich.FPcCNVC = itemsChungTuChiTiet.Where(n => n.XauNoiMa.StartsWith(XAU_NOI_MA.PHU_CAP_LUONG) && n.MaCb != null && IsCnvc(n)).Sum(n => (double)(n.DieuChinh ?? 0));

            _modelGiaiThich.FLuongHDLD = itemsChungTuChiTiet.Where(n => n.XauNoiMa.StartsWith(XAU_NOI_MA.TIEN_LUONG) && n.MaCb != null && IsLdhd(n)).Sum(n => (double)(n.DieuChinh ?? 0));
            _modelGiaiThich.FPcHDLD = itemsChungTuChiTiet.Where(n => n.XauNoiMa.StartsWith(XAU_NOI_MA.PHU_CAP_LUONG) && n.MaCb != null && IsLdhd(n)).Sum(n => (double)(n.DieuChinh ?? 0));
        }

        private bool IsCnvc(TlQtChungTuChiTietNq104Model model)
        {
            if (model.MaCbCha.IsEmpty() || IsLdhd(model)) return false;
            return model.MaCbCha.Equals(MA_CAP_BAC.CNVC) || model.MaCbCha.Equals(MA_CAP_BAC.CNQP) || model.MaCbCha.Equals(MA_CAP_BAC.CCQP) || model.MaCbCha.Equals(MA_CAP_BAC.LDHD);
        }

        private bool IsLdhd(TlQtChungTuChiTietNq104Model model)
        {
            if (model.MaCb.IsEmpty()) return false;
            var listMaCapBacLdhd = new List<string>() { "423", "425", "43" };
            return listMaCapBacLdhd.Contains(model.MaCb);
        }

        private double GetDataFromSumWage(List<string> lstMaNgach)
        {
            double luong = 0;
            foreach (var item in lstMaNgach)
            {
                var gt = _itemsChiTiet.FirstOrDefault(x => x.XauNoiMa.Equals(item));
                if (gt != null && gt.DieuChinh != null)
                {
                    luong += (double)(gt.DieuChinh ?? 0);
                }
                else
                {
                    luong += 0;
                }
            }
            return luong;
        }

        private void CalculateData(List<TlQtChungTuChiTietNq104Model> lstQtChungTuChiTiet)
        {

            lstQtChungTuChiTiet.Where(x => x.BHangCha.GetValueOrDefault(false) && _tlQtChungTuChiTietService.GetDataMucLucNG(x.XauNoiMa).ToList().Count == 0)
                .Select(x =>
                {
                    x.TongCong = 0;
                    x.DieuChinh = 0;
                    x.DDuToan = 0;
                    x.TongLuyKe = 0;
                    x.SoNguoi = 0;
                    x.SoNgay = 0;
                    return x;
                }).ToList();

            var temp = lstQtChungTuChiTiet.Where(x => !x.BHangCha.GetValueOrDefault(false)
            && (x.TongCong != null && x.TongCong != 0)
                || (x.DieuChinh != null && x.DieuChinh != 0)
                || (x.DDuToan != null && x.DDuToan != 0)
                || (x.TongLuyKe != null && x.TongLuyKe != 0));

            foreach (var item in temp)
            {
                CalculateParent(item.MlnsIdParent, item, lstQtChungTuChiTiet);
            }

            foreach (var item in temp)
            {
                if (item.DieuChinh == null)
                {
                    var listChild = lstQtChungTuChiTiet.Where(x => x.MlnsIdParent == item.MlnsId);
                    var model = lstQtChungTuChiTiet.FirstOrDefault(x => x.MlnsId == item.MlnsId);
                    decimal dieuchinh = 0;
                    foreach (var itemList in listChild)
                    {
                        dieuchinh += itemList.DieuChinh ?? 0;
                    }
                    model.DieuChinh = dieuchinh;
                }
                if (item.TongCong == null)
                {
                    var listChild = lstQtChungTuChiTiet.Where(x => x.MlnsIdParent == item.MlnsId);
                    var model = lstQtChungTuChiTiet.FirstOrDefault(x => x.MlnsId == item.MlnsId);
                    decimal TongCong = 0;
                    foreach (var itemList in listChild)
                    {
                        TongCong += itemList.TongCong ?? 0;
                    }
                    model.TongCong = TongCong;
                }
            }
        }
        private void CalculateParent(Guid? idParent, TlQtChungTuChiTietNq104Model item, List<TlQtChungTuChiTietNq104Model> lstQtChungTuChiTiet)
        {
            var model = lstQtChungTuChiTiet.FirstOrDefault(x => x.MlnsId == idParent);
            if (model == null) return;
            if (model.TongCong == null) model.TongCong = 0;
            if (model.DieuChinh == null) model.DieuChinh = 0;
            if (model.DDuToan == null) model.DDuToan = 0;
            if (model.TongLuyKe == null) model.TongLuyKe = 0;

            model.TongCong += item.TongCong ?? 0;
            model.DieuChinh += item.DieuChinh ?? 0;
            model.DDuToan += item.DDuToan ?? 0;
            model.TongLuyKe += item.TongLuyKe ?? 0;
            CalculateParent(model.MlnsIdParent, item, lstQtChungTuChiTiet);
        }
        private List<TlQtChungTuChiTietNq104Model> FindChungTuChiTietByPrintType(SalaryPrintType printType)
        {
            List<TlQtChungTuChiTietNq104Model> itemsChungTuChiTiet = new List<TlQtChungTuChiTietNq104Model>();
            switch (printType)
            {
                case SalaryPrintType.BC_CHI_TIET_QUYET_TOAN_TX_SO_LIEU:
                    itemsChungTuChiTiet = ObjectCopier.Clone(_itemsChiTiet).Where(s => !string.IsNullOrEmpty(s.M) || s.XauNoiMa.Equals("1010000")).ToList();
                    //CalculateData(itemsChungTuChiTiet);
                    return itemsChungTuChiTiet;

                case SalaryPrintType.BC_QUYET_TOAN_LUONG_PHU_CAP:
                    itemsChungTuChiTiet = ObjectCopier.Clone(_itemsChiTiet).Where(s => s.M == "6000" || s.M == "6100").ToList();
                    //CalculateData(itemsChungTuChiTiet);
                    return itemsChungTuChiTiet;

                case SalaryPrintType.BC_QUYET_TOAN_TIEN_AN:
                    itemsChungTuChiTiet = ObjectCopier.Clone(_itemsChiTiet).Where(s => s.M == "6400").ToList();
                    //CalculateData(itemsChungTuChiTiet);
                    return itemsChungTuChiTiet;

                case SalaryPrintType.BC_QUYET_TOAN_BAO_HIEM:
                    itemsChungTuChiTiet = ObjectCopier.Clone(_itemsChiTiet).Where(s => s.M == "6300").ToList();
                    //CalculateData(itemsChungTuChiTiet);
                    return itemsChungTuChiTiet;

                case SalaryPrintType.BC_QUYET_TOAN_RA_QUAN:
                    itemsChungTuChiTiet = ObjectCopier.Clone(_itemsChiTiet).Where(s => s.M == "8000").ToList();
                    //CalculateData(itemsChungTuChiTiet);
                    return itemsChungTuChiTiet;

                default:
                    return itemsChungTuChiTiet;
            }
        }

        private void LoadMonth()
        {
            List<ComboboxItem> lstData = new List<ComboboxItem>();
            lstData.Add(new ComboboxItem() { DisplayItem = DateTimeExtension.TimeConst.Names.CA_NAM, ValueItem = Convert.ToString((int)DateTimeExtension.TimeConst.Types.CA_NAM) });
            lstData.Add(new ComboboxItem() { DisplayItem = DateTimeExtension.TimeConst.Names.QUY_1, ValueItem = Convert.ToString((int)DateTimeExtension.TimeConst.Types.QUY_1) });
            lstData.Add(new ComboboxItem() { DisplayItem = DateTimeExtension.TimeConst.Names.QUY_2, ValueItem = Convert.ToString((int)DateTimeExtension.TimeConst.Types.QUY_2) });
            lstData.Add(new ComboboxItem() { DisplayItem = DateTimeExtension.TimeConst.Names.QUY_3, ValueItem = Convert.ToString((int)DateTimeExtension.TimeConst.Types.QUY_3) });
            lstData.Add(new ComboboxItem() { DisplayItem = DateTimeExtension.TimeConst.Names.QUY_4, ValueItem = Convert.ToString((int)DateTimeExtension.TimeConst.Types.QUY_4) });
            lstData.Add(new ComboboxItem() { DisplayItem = DateTimeExtension.TimeConst.Names.THANG_1, ValueItem = Convert.ToString((int)DateTimeExtension.TimeConst.Types.THANG_1) });
            lstData.Add(new ComboboxItem() { DisplayItem = DateTimeExtension.TimeConst.Names.THANG_2, ValueItem = Convert.ToString((int)DateTimeExtension.TimeConst.Types.THANG_2) });
            lstData.Add(new ComboboxItem() { DisplayItem = DateTimeExtension.TimeConst.Names.THANG_3, ValueItem = Convert.ToString((int)DateTimeExtension.TimeConst.Types.THANG_3) });
            lstData.Add(new ComboboxItem() { DisplayItem = DateTimeExtension.TimeConst.Names.THANG_4, ValueItem = Convert.ToString((int)DateTimeExtension.TimeConst.Types.THANG_4) });
            lstData.Add(new ComboboxItem() { DisplayItem = DateTimeExtension.TimeConst.Names.THANG_5, ValueItem = Convert.ToString((int)DateTimeExtension.TimeConst.Types.THANG_5) });
            lstData.Add(new ComboboxItem() { DisplayItem = DateTimeExtension.TimeConst.Names.THANG_6, ValueItem = Convert.ToString((int)DateTimeExtension.TimeConst.Types.THANG_6) });
            lstData.Add(new ComboboxItem() { DisplayItem = DateTimeExtension.TimeConst.Names.THANG_7, ValueItem = Convert.ToString((int)DateTimeExtension.TimeConst.Types.THANG_7) });
            lstData.Add(new ComboboxItem() { DisplayItem = DateTimeExtension.TimeConst.Names.THANG_8, ValueItem = Convert.ToString((int)DateTimeExtension.TimeConst.Types.THANG_8) });
            lstData.Add(new ComboboxItem() { DisplayItem = DateTimeExtension.TimeConst.Names.THANG_9, ValueItem = Convert.ToString((int)DateTimeExtension.TimeConst.Types.THANG_9) });
            lstData.Add(new ComboboxItem() { DisplayItem = DateTimeExtension.TimeConst.Names.THANG_10, ValueItem = Convert.ToString((int)DateTimeExtension.TimeConst.Types.THANG_10) });
            lstData.Add(new ComboboxItem() { DisplayItem = DateTimeExtension.TimeConst.Names.THANG_11, ValueItem = Convert.ToString((int)DateTimeExtension.TimeConst.Types.THANG_11) });
            lstData.Add(new ComboboxItem() { DisplayItem = DateTimeExtension.TimeConst.Names.THANG_12, ValueItem = Convert.ToString((int)DateTimeExtension.TimeConst.Types.THANG_12) });
            MonthItems = new ObservableCollection<ComboboxItem>(lstData);
            MonthSelected = MonthItems.FirstOrDefault(n => n.ValueItem == Thang.ToString());
            OnPropertyChanged(nameof(MonthItems));
            OnPropertyChanged(nameof(MonthSelected));
        }

        public void AddChuKy(Dictionary<string, object> data, string idType)
        {
            // add chữ ký
            var dmChyKy = _dmChuKyService.FindByCondition(x => x.IdType.Equals(idType) && x.ITrangThai == StatusType.ACTIVE).FirstOrDefault();
            if (dmChyKy == null) dmChyKy = new DmChuKy();

            data.Add("ThuaLenh1", dmChyKy.ThuaLenh1MoTa);
            data.Add("ChucDanh1", dmChyKy.ChucDanh1MoTa);
            data.Add("GhiChuKy1", "(Ký, họ tên)");
            data.Add("Ten1", dmChyKy.Ten1MoTa);

            data.Add("ThuaLenh2", dmChyKy.ThuaLenh2MoTa);
            data.Add("ChucDanh2", dmChyKy.ChucDanh2MoTa);
            data.Add("GhiChuKy2", "(Ký, họ tên)");
            data.Add("Ten2", dmChyKy.Ten2MoTa);

            data.Add("ThuaLenh3", dmChyKy.ThuaLenh3MoTa);
            data.Add("ChucDanh3", dmChyKy.ChucDanh3MoTa);
            data.Add("GhiChuKy3", "(Ký, họ tên, đóng dấu)");
            data.Add("Ten3", dmChyKy.Ten3MoTa);

            data.Add("ThuaLenh4", dmChyKy.ThuaLenh4MoTa);
            data.Add("ChucDanh4", dmChyKy.ChucDanh4MoTa);
            data.Add("GhiChuKy4", "(Ký, họ tên, đóng dấu)");
            data.Add("Ten4", dmChyKy.Ten4MoTa);
        }
    }
}
