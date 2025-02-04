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
using System.Windows;
using System.Windows.Data;
using VTS.QLNS.CTC.App.Command;
using VTS.QLNS.CTC.App.Helper;
using VTS.QLNS.CTC.App.Model;
using VTS.QLNS.CTC.App.Model.Control;
using VTS.QLNS.CTC.App.Model.Report;
using VTS.QLNS.CTC.App.Properties;
using VTS.QLNS.CTC.App.Service;
using VTS.QLNS.CTC.App.Service.Impl;
using VTS.QLNS.CTC.App.Service.UserFunction;
using VTS.QLNS.CTC.App.ViewModel.Category;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Criteria;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Service;
using VTS.QLNS.CTC.Core.Service.Impl;
using VTS.QLNS.CTC.Utility;
using VTS.QLNS.CTC.Utility.Enum;

namespace VTS.QLNS.CTC.App.ViewModel.Budget.Estimate.Division.PrintReport
{
    public class PrintReportSynthesisAgencyViewModel : ReportViewModelBase<DtChungTuModel, DtChungTuChiTietModel, DuToanChiTieuTongHop>
    {
        private readonly ISessionService _sessionService;
        private readonly IMapper _mapper;
        private ICollectionView _coverSheetView;
        private ICollectionView _lnsView;
        private readonly INsDtChungTuService _dtChungTuService;
        private readonly INsNguoiDungLnsService _nsNguoiDungLNSService;
        private readonly INsDtChungTuChiTietService _dtChungTuChiTietService;
        private readonly IDanhMucService _danhMucService;
        private readonly IDmChuKyService _dmChuKyService;
        private readonly INsMucLucNganSachService _nsMucLucNganSachService;
        private readonly INsDtNhanPhanBoMapService _iNsDtNhanPhanBoMapService;
        private readonly INsNguoiDungDonViService _nsNguoiDungDonViService;
        private readonly INsPhongBanService _iNsPhongBanService;
        private readonly ILog _logger;
        private DmChuKyDialogViewModel DmChuKyDialogViewModel;
        private DmChuKy _dmChuKy;
        private string _diaDiem;
        private string _cap1;
        private List<DtChungTuModel> _listChungTuReport;
        private List<DtChungTuChiTietModel> _listChungTuChiTietReport;
        private HashSet<string> _listDonVi;
        private Dictionary<int, List<string>> _dictAgencyByPageSize;
        private const int PAGE_SIZE = 7;
        public bool IsChecking { get; set; }
        public bool Flag { get; set; } = false;
        public override string Name
        {
            get => DivisionEstimateScreen.DivisionEstimatePrintTypeName[(int)DivisionEstimatePrintType.SYNTHESIS_BUDGET_AGENCY];
        }

        public override string Title
        {
            get => DivisionEstimateScreen.DivisionEstimatePrintTypeTitle[(int)DivisionEstimatePrintType.SYNTHESIS_BUDGET_AGENCY];
        }

        public override string Description
        {
            get => DivisionEstimateScreen.DivisionEstimatePrintTypeDescription[(int)DivisionEstimatePrintType.SYNTHESIS_BUDGET_AGENCY];
        }

        public override Type ContentType => typeof(View.Budget.Estimate.Division.PrintReport.PrintReportSynthesisAgency);

        private bool _isExportData;
        public bool IsExportData
        {
            get => true;
            set => SetProperty(ref _isExportData, value);
        }

        public bool InMotToChecked { get; set; }


        public IEnumerable<DtChungTuModel> DtChungTuModelNhanPhanBos { get; set; }
        // start handle chon dot
        private ObservableCollection<ComboboxItem> _dataDot = new ObservableCollection<ComboboxItem>();
        public ObservableCollection<ComboboxItem> DataDot
        {
            get => _dataDot;
            set => SetProperty(ref _dataDot, value);
        }

        private ComboboxItem _dataDotSelected;
        public ComboboxItem DataDotSelected
        {
            get => _dataDotSelected;
            set
            {
                SetProperty(ref _dataDotSelected, value);
                if (_dataDotSelected != null)
                {
                    LoadVoucherType();
                    GetListChungTuReport();
                    LoadCoverSheet();
                    LoadLNS();
                }
            }
        }

        private ObservableCollection<ComboboxItem> _cbxVoucherType;
        public ObservableCollection<ComboboxItem> CbxVoucherType
        {
            get => _cbxVoucherType;
            set => SetProperty(ref _cbxVoucherType, value);
        }

        private ComboboxItem _cbxVoucherTypeSelected;
        public ComboboxItem CbxVoucherTypeSelected
        {
            get => _cbxVoucherTypeSelected;
            set => SetProperty(ref _cbxVoucherTypeSelected, value);
        }

        private ObservableCollection<ComboboxItem> _cbxSpecialized;
        public ObservableCollection<ComboboxItem> CbxSpecialized
        {
            get => _cbxSpecialized;
            set => SetProperty(ref _cbxSpecialized, value);
        }

        //chọn ngành
        private ComboboxItem _cbxSpecializedSelected;
        public ComboboxItem CbxSpecializedSelected
        {
            get => _cbxSpecializedSelected;
            set
            {
                SetProperty(ref _cbxSpecializedSelected, value);
                LoadCoverSheet();
            }
        }


        //B quản lý
        private ObservableCollection<ComboboxItem> _cbxBQuanLy;
        public ObservableCollection<ComboboxItem> CbxBQuanLy
        {
            get => _cbxBQuanLy;
            set => SetProperty(ref _cbxBQuanLy, value);
        }

        private ComboboxItem _cbxBQuanLySelected;
        public ComboboxItem CbxBQuanLySelected
        {
            get => _cbxBQuanLySelected;
            set
            {
                SetProperty(ref _cbxBQuanLySelected, value);
                LoadLNS();
            }
        }

        private Visibility _inMotToCheckedVisibility;
        public Visibility InMotToCheckedVisibility
        {
            get => _inMotToCheckedVisibility;
            set
            {
                SetProperty(ref _inMotToCheckedVisibility, value);
            }
        }

        //chọn lũy kế
        private bool _checkPrintAccumulation;
        public bool CheckPrintAccumulation
        {
            get => _checkPrintAccumulation;
            set
            {
                SetProperty(ref _checkPrintAccumulation, value);
                GetListChungTuReport();
                LoadCoverSheet();
                LoadLNS();
                LoadTieuDe();
                if (_checkPrintAccumulation)
                {
                    CheckPrintBudgetDivisionCurrent = false;
                }
            }
        }

        private bool _checkPrintBudgetDivisionCurrent;
        public bool CheckPrintBudgetDivisionCurrent
        {
            get => _checkPrintBudgetDivisionCurrent;
            set
            {
                SetProperty(ref _checkPrintBudgetDivisionCurrent, value);
                if (_checkPrintBudgetDivisionCurrent)
                {
                    CheckPrintAccumulation = false;
                }
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

        private bool _checkPrintSignatureLastPage;
        public bool CheckPrintSignatureLastPage
        {
            get => _checkPrintSignatureLastPage;
            set => SetProperty(ref _checkPrintSignatureLastPage, value);
        }

        private ObservableCollection<NsMuclucNgansachModel> _listLNS = new ObservableCollection<NsMuclucNgansachModel>();
        public ObservableCollection<NsMuclucNgansachModel> ListLNS
        {
            get => _listLNS;
            set => SetProperty(ref _listLNS, value);
        }

        private List<string> _lstDonViByUser = new List<string>();
        public List<string> LstDonViByUser
        {
            get => _lstDonViByUser;
            set => SetProperty(ref _lstDonViByUser, value);
        }

        private string _labelSelectedCountLNS;
        public string LabelSelectedCountLNS
        {
            get => $"CHỌN LNS ({ListLNS.Count(item => item.IsSelected)}/{ListLNS.Count})";
            set => SetProperty(ref _labelSelectedCountLNS, value);
        }

        private bool _selectAllLNS;
        public bool SelectAllLNS
        {
            get => ListLNS.Any() && ListLNS.All(item => item.IsSelected);
            set
            {
                SetProperty(ref _selectAllLNS, value);
                Flag = true;
                foreach (var item in ListLNS)
                {
                    item.IsSelected = _selectAllLNS;
                }
                Flag = false;
                LoadCoverSheet();
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


        // start handle cover sheet
        private List<CheckBoxItem> _listCoverSheet = new List<CheckBoxItem>();
        public List<CheckBoxItem> ListCoverSheet
        {
            get => _listCoverSheet;
            set => SetProperty(ref _listCoverSheet, value);
        }

        private string _labelSelectedCountCoverSheet;
        public string LabelSelectedCountCoverSheet
        {
            get => $"CHỌN TỜ ({ListCoverSheet.Count(item => item.IsChecked)}/{ListCoverSheet.Count})";
            set => SetProperty(ref _labelSelectedCountCoverSheet, value);
        }

        private bool _selectAllCoverSheet;
        public bool SelectAllCoverSheet
        {
            get => ListCoverSheet.Any() && ListCoverSheet.All(item => item.IsChecked);
            set
            {
                SetProperty(ref _selectAllCoverSheet, value);
                foreach (var item in ListCoverSheet) item.IsChecked = _selectAllCoverSheet;
            }
        }

        private string _searchCoverSheet;
        public string SearchCoverSheet
        {
            get => _searchCoverSheet;
            set
            {
                if (SetProperty(ref _searchCoverSheet, value))
                {
                    _coverSheetView.Refresh();
                }
            }
        }

        public RelayCommand ConfigSignCommand { get; }

        public PrintReportSynthesisAgencyViewModel(
            IMapper mapper,
            INsDtChungTuService dtChungTuService,
            ISessionService sessionService,
            INsDtChungTuChiTietService dtChungTuChiTietService,
            IDanhMucService danhMucService,
            IExportService exportService,
            INsNguoiDungDonViService nsNguoiDungDonViService,
            IDmChuKyService dmChuKyService,
            INsDtNhanPhanBoMapService iNsDtNhanPhanBoMapService,
            INsNguoiDungLnsService nsNguoiDungLNSService,
            INsMucLucNganSachService nsMucLucNganSachService,
            ILog logger,
            INsPhongBanService nsPhongBanService,
            DmChuKyDialogViewModel dmChuKyDialogViewModel) : base(exportService, danhMucService, sessionService)
        {
            _sessionService = sessionService;
            _mapper = mapper;
            _dtChungTuService = dtChungTuService;
            _dtChungTuChiTietService = dtChungTuChiTietService;
            _danhMucService = danhMucService;
            _dmChuKyService = dmChuKyService;
            _nsMucLucNganSachService = nsMucLucNganSachService;
            _iNsDtNhanPhanBoMapService = iNsDtNhanPhanBoMapService;
            _nsNguoiDungDonViService = nsNguoiDungDonViService;
            DmChuKyDialogViewModel = dmChuKyDialogViewModel;
            _nsNguoiDungLNSService = nsNguoiDungLNSService;
            _logger = logger;
            _iNsPhongBanService = nsPhongBanService;
            ConfigSignCommand = new RelayCommand(obj => OnConfigSign());
        }

        public override void Init()
        {
            IsChecking = true;
            if (Model == null)
            {
                Model = new DtChungTuModel();
            }
            LoadCatUnitTypes();
            LoadDataDot();
            if (!Models.Any())
            {
                return;
            }
            InitReportDefaultDate();
            LoadVoucherType();
            LoadSpecialized();
            LoadTieuDe();
            LoadCoverSheet();
            LoadDanhMuc();
            LoadBQuanLy();
            GetQuanLyDonViCha();
        }

        private void LoadTieuDe()
        {
            try
            {
                _dmChuKy = _dmChuKyService.FindByCondition(x => x.IdType.Equals(TypeChuKy.RPT_NS_DUTOAN_TONGHOP_PHANBO) && x.ITrangThai == StatusType.ACTIVE).FirstOrDefault();
                if (_dmChuKy == null)
                    _dmChuKy = new DmChuKy();
                if (!string.IsNullOrEmpty(_dmChuKy.TieuDe1MoTa))
                    TxtTitleFirst = _dmChuKy.TieuDe1MoTa;
                if (!string.IsNullOrEmpty(_dmChuKy.TieuDe2MoTa))
                    TxtTitleSecond = _dmChuKy.TieuDe2MoTa;
                if (!string.IsNullOrEmpty(_dmChuKy.TieuDe3MoTa))
                    TxtTitleThird = _dmChuKy.TieuDe3MoTa;
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        private void LoadDanhMuc()
        {
            string chiTietToi = "NG";
            var danhMucCauHinh = _danhMucService.FindByType(TypeDanhMuc.DM_CAUHINH, _sessionService.Current.YearOfWork).ToList();
            if (danhMucCauHinh.Count > 0)
            {
                var danhMucQuanLy = danhMucCauHinh.Where(n => n.IIDMaDanhMuc == MaDanhMuc.DV_QUANLY).FirstOrDefault();
                _cap1 = danhMucQuanLy == null ? string.Empty : danhMucQuanLy.SGiaTri;

                var danhMucDiaDiem = danhMucCauHinh.Where(n => n.IIDMaDanhMuc == MaDanhMuc.DIADIEM).FirstOrDefault();
                _diaDiem = danhMucDiaDiem == null ? string.Empty : danhMucDiaDiem.SGiaTri;

                var danhMucMLNS = danhMucCauHinh.Where(n => n.IIDMaDanhMuc == MaDanhMuc.MLNS_CHITIET_TOI).FirstOrDefault();
                chiTietToi = danhMucMLNS == null ? chiTietToi : danhMucMLNS.SGiaTri;
            }
            PrintTypeMLNS = new ObservableCollection<ComboboxItem>(DynamicMLNS.CreateMLNSReportSetting(chiTietToi));
            SelectedPrintTypeMLNS = PrintTypeMLNS.First();
        }

        private void LoadDataDot()
        {
            DataDot = new ObservableCollection<ComboboxItem>();
            var predicate = CreatePredicateChungTuIndex();
            var listChungTu = _dtChungTuService.FindByCondition(predicate).ToList();
            Models = _mapper.Map<ObservableCollection<DtChungTuModel>>(listChungTu);
            List<string> lstSoQuyetDinh = new List<string>();
            List<DateTime> lstNgayChungTu = new List<DateTime>();
            foreach (DtChungTuModel item in Models)
            {
                if (!string.IsNullOrEmpty(item.SSoQuyetDinh))
                {
                    if (!lstSoQuyetDinh.Contains(item.SSoQuyetDinh))
                    {
                        lstSoQuyetDinh.Add(item.SSoQuyetDinh);
                        var lstDotBySoQuyetDinh = Models.Where(x => !string.IsNullOrEmpty(x.SSoQuyetDinh) && x.SSoQuyetDinh.Equals(item.SSoQuyetDinh));

                        var firstLst = lstDotBySoQuyetDinh.FirstOrDefault();
                        string mota = firstLst.DNgayQuyetDinh.HasValue
                                ? firstLst.DNgayQuyetDinh.Value.ToString("dd/MM/yyyy")
                                : string.Empty;
                        mota += StringUtils.SPACE + firstLst.SMoTa;
                        DataDot.Add(new ComboboxItem
                        {
                            ValueItem = item.SSoQuyetDinh,
                            DisplayItem = string.Format("{0}\n{1}", item.SSoQuyetDinh, mota),
                            HiddenValue = "1"
                        });
                    }
                }
                else
                {
                    var ngayChungTu = item.DNgayChungTu.GetValueOrDefault().Date;
                    if (!lstNgayChungTu.Contains(ngayChungTu))
                    {
                        lstNgayChungTu.Add(item.DNgayChungTu.GetValueOrDefault().Date);
                        var lstDotByNgayChungTu = Models.Where(x => x.DNgayChungTu.GetValueOrDefault().Date.Equals(ngayChungTu) && string.IsNullOrEmpty(x.SSoQuyetDinh));
                        /*
                        string mota = "";
                        foreach (var dt in lstDotByNgayChungTu)
                        {
                            if (!string.IsNullOrEmpty(mota))
                            {
                                mota += "\n";
                            }
                            mota += (dt.DNgayChungTu.HasValue
                                ? dt.DNgayChungTu.Value.ToString("dd/MM/yyyy")
                                : string.Empty) + " " + dt.SMoTa;
                        }
                        */
                        var firstLst = lstDotByNgayChungTu.FirstOrDefault();
                        string mota = firstLst.DNgayChungTu.HasValue ? firstLst.DNgayChungTu.Value.ToString("dd/MM/yyyy") : string.Empty;
                        mota += StringUtils.SPACE + firstLst.SMoTa;
                        var lstSoChungTuMota = string.Join(",", lstDotByNgayChungTu.Select(x => x.SSoChungTu));
                        DataDot.Add(new ComboboxItem
                        {
                            ValueItem = ngayChungTu.ToString("dd/MM/yyyy"),
                            DisplayItem = string.Format("{0}\n{1}", lstSoChungTuMota, mota),
                            HiddenValue = "2"
                        });
                    }
                }
            }

            var ordered = DataDot.OrderByDescending(c => DateTime.Parse(c.DisplayItem.Split('\n')[1].Split(" ")[0])).ToList();
            DataDot = new ObservableCollection<ComboboxItem>(ordered);
            if (DataDot != null && DataDot.Count > 0)
            {
                DataDotSelected = DataDot.Where(x => x.ValueItem.Equals(Model.SSoQuyetDinh) || x.ValueItem.Equals(Model.SSoChungTu)).Select(x => x).DefaultIfEmpty(DataDot.ElementAt(0)).FirstOrDefault();
            }
        }

        private Expression<Func<NsDtChungTu, bool>> CreatePredicateChungTuIndex()
        {
            var predicate = PredicateBuilder.True<NsDtChungTu>();
            predicate = predicate.And(x => x.INamLamViec == _sessionService.Current.YearOfWork);
            predicate = predicate.And(x => x.IIdMaNguonNganSach == _sessionService.Current.Budget);
            predicate = predicate.And(x => x.INamNganSach == _sessionService.Current.YearOfBudget);
            predicate = predicate.And(x => x.ILoai == SoChungTuType.EstimateDivision);
            predicate = predicate.And(x => !string.IsNullOrEmpty(x.IIdDotNhan));
            return predicate;
        }

        private void LoadVoucherType()
        {
            var cbxVoucher = new List<ComboboxItem>
            {
                new ComboboxItem {DisplayItem = "Tự chi", ValueItem = "1"},
                new ComboboxItem {DisplayItem = "Hiện vật", ValueItem = "2"},
                new ComboboxItem {DisplayItem = "Tổng hợp Tự chi hiện vật", ValueItem = "3"},
                new ComboboxItem {DisplayItem = "Hàng Nhập", ValueItem = "4"},
                new ComboboxItem {DisplayItem = "Hàng Mua", ValueItem = "5"},
                new ComboboxItem {DisplayItem = "Phân cấp", ValueItem = "6"},
                new ComboboxItem {DisplayItem = "Tổng hợp Hàng nhâp, hàng mua, phân cấp", ValueItem = "7"}
            };

            CbxVoucherType = new ObservableCollection<ComboboxItem>(cbxVoucher);
            CbxVoucherTypeSelected = CbxVoucherType.ElementAt(0);
        }

        public override void OnPrint(object obj)
        {
            if (InMotToChecked)
            {
                Export(obj, ExportType.PDF_ONE_PAPER);
            }
            else
            {
                Export(obj, ExportType.BROWSER);
            }
        }


        private void SetCheckChildren(ObservableCollection<NsMuclucNgansachModel> items, NsMuclucNgansachModel item)
        {
            foreach (var e in items)
            {
                if (e.MlnsIdParent == item.MlnsId)
                {
                    e.IsSelected = item.IsSelected;
                    SetCheckChildren(items, e);
                }
            }
        }

        private void SetCheckParent(ObservableCollection<NsMuclucNgansachModel> items, NsMuclucNgansachModel item)
        {
            foreach (var e in items)
            {
                if (e.MlnsId == item.MlnsIdParent)
                {
                    e.IsSelected = items.Where(x => x.MlnsIdParent == item.MlnsIdParent).All(x => x.IsSelected);
                    SetCheckParent(items, e);
                }
            }
        }

        private List<string> GetListLNSByUser()
        {
            var predicate = PredicateBuilder.True<NsNguoiDungLns>();
            predicate = predicate.And(x => x.INamLamViec == _sessionService.Current.YearOfWork);
            predicate = predicate.And(x => x.SMaNguoiDung == _sessionService.Current.Principal);
            List<NsNguoiDungLns> listNguoiDungDonVi = _nsNguoiDungLNSService.FindAll(predicate).ToList();
            return listNguoiDungDonVi.Select(x => x.SLns).ToList();
        }

        private void LoadLNS()
        {
            var yearOfWork = _sessionService.Current.YearOfWork;
            List<string> listLNSNguoiDung = GetListLNSByUser();
            var listLNS = _dtChungTuChiTietService.GetLnsHasData(_listChungTuReport.Select(n => n.Id).ToList()).ToList();

            if (CheckPrintAccumulation)
            {
                var listLNSNhan = GetTotalBudgetAvailable().Where(x => x.HasData).Select(x => x.SLns).Distinct().ToList();
                listLNS = listLNS.Union(listLNSNhan).ToList();
            }

            var listNsMucLucNganSach = _nsMucLucNganSachService.FindByListLnsDonVi(string.Join(",", listLNS), yearOfWork).ToList();
            if (CbxBQuanLySelected != null)
            {
                if (!string.IsNullOrEmpty(CbxBQuanLySelected.ValueItem) && CbxBQuanLySelected.ValueItem != StringUtils.ZERO)
                {
                    listNsMucLucNganSach = listNsMucLucNganSach.Where(x => x.IdPhongBan == CbxBQuanLySelected.ValueItem).ToList();
                }
            }
            var listAllMLNS = _nsMucLucNganSachService.FindByCondition(x => x.NamLamViec == yearOfWork && string.IsNullOrEmpty(x.L) && string.IsNullOrEmpty(x.K)).ToList();
            var listAll = listAllMLNS.Where(x => listNsMucLucNganSach.Any(y => y.XauNoiMa.StartsWith(x.XauNoiMa))).OrderBy(x => x.XauNoiMa).ToList();
            listAll = listAll.Where(x => listLNSNguoiDung.Contains(x.Lns)).ToList();

            ListLNS = _mapper.Map<ObservableCollection<NsMuclucNgansachModel>>(listAll);
            _lnsView = CollectionViewSource.GetDefaultView(ListLNS);
            _lnsView.Filter = obj => string.IsNullOrWhiteSpace(_searchLNS)
                                     || (obj is NsMuclucNgansachModel item && item.LNSDisplay.Contains(_searchLNS.Trim(), StringComparison.OrdinalIgnoreCase));

            OnPropertyChanged(nameof(SelectAllLNS));
            OnPropertyChanged(nameof(LabelSelectedCountLNS));
            OnPropertyChanged(nameof(IsExportData));
            if (_listLNS != null && _listLNS.Count > 0)
            {
                foreach (var model in _listLNS)
                {
                    model.PropertyChanged += (sender, args) =>
                    {
                        if (args.PropertyName == nameof(NsMuclucNgansachModel.IsSelected) && !Flag)
                        {
                            Flag = true;
                            SetCheckChildren(ListLNS, model);
                            SetCheckParent(ListLNS, model);
                            //ListLNS.Where(x => x.MlnsIdParent == model.MlnsId).Select(x => x.IsSelected = model.IsSelected).ToList();
                            Flag = false;
                            LoadCoverSheet();
                            //foreach (var item in _listLNS)
                            //{
                            //    if (item.MlnsIdParent == model.MlnsId)
                            //    {
                            //        Flag = true;
                            //        IsChecking = false;
                            //        item.IsSelected = model.IsSelected;
                            //    }

                            //    IsChecking = !_listLNS.Any(item => item.MlnsIdParent == model.MlnsId);
                            //    Flag = false;
                            //}

                            //if (IsChecking && !Flag)
                            //{
                            //    LoadCoverSheet();
                            //}
                            OnPropertyChanged(nameof(SelectAllLNS));
                            OnPropertyChanged(nameof(LabelSelectedCountLNS));
                            OnPropertyChanged(nameof(IsExportData));
                        }
                    };
                }
            }
        }

        private void LoadDataChiTiet()
        {
            _listChungTuChiTietReport = GetTotalBudgetUsed("rpt_du_toan_chi_tieu_tong_hop_used_so_quyet_dinh");
            if (_cbxSpecializedSelected != null)
            {
                _listChungTuChiTietReport = _listChungTuChiTietReport.Where(x => _cbxSpecializedSelected.ValueItem.Split(StringUtils.COMMA).Contains(x.SNg)).ToList();
            }
        }

        private void LoadCoverSheet()
        {
            LoadDataChiTiet();
            _listDonVi = _listChungTuChiTietReport.Where(x => !string.IsNullOrEmpty(x.STenDonVi) && x.HasData && LstDonViByUser.Contains(x.IIdMaDonVi)).OrderBy(x => x.IIdMaDonVi).Select(x => x.STenDonVi).ToHashSet();

            _dictAgencyByPageSize = _listDonVi.Select((x, i) => new { Group = i / PAGE_SIZE, Value = x })
               .GroupBy(item => item.Group, g => g.Value)
               .ToDictionary(x => x.Key, x => x.ToList());
            ListCoverSheet = new List<CheckBoxItem>();
            for (int i = 0; i < _dictAgencyByPageSize.Count; i++)
            {
                ListCoverSheet.Add(new CheckBoxItem
                {
                    DisplayItem = string.Format("Tờ {0}", i + 1),
                    ValueItem = (i + 1).ToString()
                });
            }

            OnPropertyChanged(nameof(LabelSelectedCountCoverSheet));
            OnPropertyChanged(nameof(SelectAllCoverSheet));
            OnPropertyChanged(nameof(IsExportData));
            // Filter
            _coverSheetView = CollectionViewSource.GetDefaultView(ListCoverSheet);
            _coverSheetView.Filter = obj => string.IsNullOrWhiteSpace(_searchCoverSheet)
                                            || (obj is CheckBoxItem item && item.DisplayItem!.ToLower().Contains(_searchCoverSheet!.Trim().ToLower(), StringComparison.OrdinalIgnoreCase));

            foreach (var model in ListCoverSheet)
            {
                model.PropertyChanged += (sender, args) =>
                {
                    var objChange = (CheckBoxItem)sender;
                    if (objChange.ValueItem.Equals("2"))
                    {
                        ListCoverSheet.First(x => x.ValueItem.Equals("1")).IsChecked = true;
                    }

                    OnPropertyChanged(nameof(LabelSelectedCountCoverSheet));
                    OnPropertyChanged(nameof(SelectAllCoverSheet));
                    OnPropertyChanged(nameof(IsExportData));
                };
            }
        }

        private void LoadSpecialized()
        {
            var yearOfWork = _sessionService.Current.YearOfWork;
            var listDanhMuc = _danhMucService.FindByType(EstimationReport.DU_TOAN_THEO_NGANH, yearOfWork).ToList();
            CbxSpecialized = _mapper.Map<ObservableCollection<ComboboxItem>>(listDanhMuc);
        }

        private void LoadBQuanLy()
        {
            _cbxBQuanLy = new ObservableCollection<ComboboxItem>();
            var predicate = PredicateBuilder.True<DmBQuanLy>();
            predicate = predicate.And(x => x.INamLamViec == _sessionService.Current.YearOfWork);

            List<DmBQuanLy> data = _iNsPhongBanService.FindByCondition(predicate).ToList();
            _cbxBQuanLy = _mapper.Map<ObservableCollection<ComboboxItem>>(data);
            _cbxBQuanLy.Insert(0, new ComboboxItem { DisplayItem = "Tất cả", ValueItem = "0" });
            _cbxBQuanLySelected = _cbxBQuanLy.FirstOrDefault();
        }

        public override string GetFileTemplate(string strPageNumber = "")
        {
            if (CheckPrintAccumulation)
            {
                return Path.Combine(ExportPrefix.PATH_TL_DT, $"{ExportFileName.RPT_NS_DUTOAN_CHITIEU_TONGHOP_DONVI}{strPageNumber}{StringUtils.EXCEL_EXTENSION}");
            }
            else
            {
                return Path.Combine(ExportPrefix.PATH_TL_DT, $"{ExportFileName.RPT_NS_DUTOAN_CHITIEU_TONGHOP_DONVI}{strPageNumber}_TheoDot{StringUtils.EXCEL_EXTENSION}");
            }
        }

        public string GetFileTemplateOnePaper(string strPageNumber = "")
        {
            if (CheckPrintAccumulation)
            {
                return Path.Combine(ExportPrefix.PATH_TL_DT, $"{ExportFileName.RPT_NS_DUTOAN_CHITIEU_TONGHOP_DONVI_ONEPAPER}{strPageNumber}{StringUtils.EXCEL_EXTENSION}");
            }
            else
            {
                return Path.Combine(ExportPrefix.PATH_TL_DT, $"{ExportFileName.RPT_NS_DUTOAN_CHITIEU_TONGHOP_DONVI_ONEPAPER}{strPageNumber}_TheoDot{StringUtils.EXCEL_EXTENSION}");
            }
        }

        private void GetQuanLyDonViCha()
        {
            var predicate = PredicateBuilder.True<NguoiDungDonVi>();
            predicate = predicate.And(x => x.IIDMaNguoiDung.Equals(_sessionService.Current.Principal));
            predicate = predicate.And(x => x.INamLamViec == _sessionService.Current.YearOfWork);
            predicate = predicate.And(x => x.ITrangThai == StatusType.ACTIVE);
            var nsDungDonVis = _nsNguoiDungDonViService.FindAll(predicate).ToList();
            LstDonViByUser = nsDungDonVis.Select(x => x.IIdMaDonVi).ToList();
        }

        public override IEnumerable<DtChungTuChiTietModel> GetData()
        {
            LoadDataChiTiet();
            var ctFirst = _listChungTuReport.FirstOrDefault();
            //var lns = ctFirst.SDslns.Split(StringUtils.COMMA).Where(n => ListLNS.Where(m => m.IsSelected).Select(m => m.Lns).Contains(n))
            var sDsln = string.Join(",", _listChungTuReport.Select(s => s.SDslns));
            var lns = sDsln.Split(StringUtils.COMMA).Where(n => ListLNS.Where(m => m.IsSelected).Select(m => m.Lns).Contains(n)).Distinct();
            int.TryParse(CatUnitTypeSelected.ValueItem, out int donViTinh);
            var searchCondition = new EstimationVoucherDetailCriteria
            {
                VoucherId = Guid.Empty,
                ChungTuId = string.Join(StringUtils.COMMA, _listChungTuReport.Select(x => x.Id)),
                LNS = string.Join(StringUtils.COMMA, lns),
                YearOfWork = ctFirst.INamLamViec,
                YearOfBudget = ctFirst.INamNganSach,
                BudgetSource = ctFirst.IIdMaNguonNganSach,
                VoucherDate = ctFirst.DNgayChungTu,
                IdDotNhan = ctFirst.IIdDotNhan,
                DonViTinh = donViTinh
            };
            string procedure = "rpt_du_toan_chi_tieu_tong_hop";
            if (ctFirst.ILoaiDuToan == (int)BudgetType.ADJUSTED)
            {
                procedure = "rpt_du_toan_chi_tieu_tong_hop_dieuchinh";
            }
            var listChungTuChiTiet = _dtChungTuChiTietService.FindByCond(searchCondition, procedure: procedure);
            if (_cbxSpecializedSelected != null)
            {
                listChungTuChiTiet = listChungTuChiTiet.Where(x => x.BHangCha || _cbxSpecializedSelected.ValueItem.Split(StringUtils.COMMA).Contains(x.SNg));
            }

            if (listChungTuChiTiet.All(x => x.BHangCha))
            {
                listChungTuChiTiet = new List<NsDtChungTuChiTietQuery>();
            }

            var listChungTuChiTietModel = _mapper.Map<List<DtChungTuChiTietModel>>(listChungTuChiTiet);
            return listChungTuChiTietModel;
        }

        public override List<Tuple<string, string, Dictionary<string, object>>> ConvertDataExport(IEnumerable<DtChungTuChiTietModel> listData, string extension)
        {
            _dmChuKy = _dmChuKyService.FindByCondition(x => x.IdType.Equals(TypeChuKy.RPT_NS_DUTOAN_TONGHOP_PHANBO) && x.ITrangThai == StatusType.ACTIVE).FirstOrDefault();
            var idDonViLogin = _sessionService.Current.IdDonVi;
            var dataGroupByMlnsId = listData.GroupBy(x => x.IIdMlns.ToString()).ToDictionary(x => x.Key, x => x.ToList());
            // get total budget
            var listDetailAvailable = GetTotalBudgetAvailableLNS();
            var totalBudgetEstimateDivisionMapByCategoryId = listDetailAvailable
                .GroupBy(budget => budget.IIdMlns.ToString())
                .ToDictionary(g => g.Key, g => g.ToList());

            if (CheckPrintAccumulation)
            {
                totalBudgetEstimateDivisionMapByCategoryId.ForAll(item =>
                {
                    if (!dataGroupByMlnsId.ContainsKey(item.Key))
                    {
                        dataGroupByMlnsId.Add(item.Key, item.Value);
                    }
                });
            }

            var usedBudgetEstimateDivisionMapByCategoryId = _listChungTuChiTietReport.Where(x => !string.IsNullOrEmpty(x.IIdMaDonVi) && LstDonViByUser.Contains(x.IIdMaDonVi))
                                                                                     .GroupBy(budget => budget.IIdMlns.ToString())
                                                                                     .ToDictionary(g => g.Key, g => g.GroupBy(e => e.STenDonVi).ToDictionary(e => e.Key, e => e.ToList()));
            var pageSize = 7;
            var listResult = new List<Tuple<string, string, Dictionary<string, object>>>();

            var catUnitTypeStr = "Đơn vị tính: " + CatUnitTypeSelected.DisplayItem + " Tờ số: {0}";
            var catUnitType = Convert.ToInt32(CatUnitTypeSelected.ValueItem);
            foreach (var dictByPage in _dictAgencyByPageSize)
            {
                if (!ListCoverSheet.Where(x => x.IsChecked).Any(x => x.ValueItem == (dictByPage.Key + 1).ToString()))
                {
                    continue;
                }
                var data = new Dictionary<string, object>();
                FormatNumber formatNumber = new FormatNumber(catUnitType, StringUtils.PDF_EXTENSION == extension ? ExportType.PDF : ExportType.EXCEL);
                data.Add("FormatNumber", formatNumber);
                /*
                data.Add("Cap1", dictByPage.Key > 0 ? string.Empty : _cap1);
                data.Add("Cap2", dictByPage.Key > 0 ? string.Empty : _sessionService.Current.TenDonVi);
                data.Add("TitleFirst", dictByPage.Key > 0 ? string.Empty : TxtTitleFirst);
                data.Add("TitleSecond", dictByPage.Key > 0 ? string.Empty : TxtTitleSecond);
                data.Add("TitleThird", dictByPage.Key > 0 ? string.Empty : TxtTitleThird);
                */
                data.Add("Cap1", _cap1);
                data.Add("Cap2", _sessionService.Current.TenDonVi);
                data.Add("TitleFirst", TxtTitleFirst);
                data.Add("TitleSecond", TxtTitleSecond);
                data.Add("TitleThird", TxtTitleThird);
                data.Add("header1", (_cbxSpecializedSelected != null ? $"Ngành: {_cbxSpecializedSelected.DisplayItem}" : string.Empty));
                data.Add("CatUnitType", string.Format(catUnitTypeStr, dictByPage.Key + 1));

                data.Add("DiaDiem", _diaDiem);
                data.Add("Ngay", DateUtils.FormatDateReport(ReportDate));

                data.Add("ThuaLenh1", (_dmChuKy != null ? _dmChuKy.ThuaLenh1MoTa : string.Empty));
                data.Add("ThuaLenh2", (_dmChuKy != null ? _dmChuKy.ThuaLenh2MoTa : string.Empty));
                data.Add("ThuaLenh3", (_dmChuKy != null ? _dmChuKy.ThuaLenh3MoTa : string.Empty));

                data.Add("ChucDanh1", (_dmChuKy != null ? _dmChuKy.ChucDanh1MoTa : string.Empty));
                data.Add("ChucDanh2", (_dmChuKy != null ? _dmChuKy.ChucDanh2MoTa : string.Empty));
                data.Add("ChucDanh3", (_dmChuKy != null ? _dmChuKy.ChucDanh3MoTa : string.Empty));

                data.Add("Ten1", (_dmChuKy != null ? _dmChuKy.Ten1MoTa : string.Empty));
                data.Add("Ten2", (_dmChuKy != null ? _dmChuKy.Ten2MoTa : string.Empty));
                data.Add("Ten3", (_dmChuKy != null ? _dmChuKy.Ten3MoTa : string.Empty));

                var dictByIndexCol = dictByPage.Value
                    .Select((value, index) => new { PairNum = index, value })
                    .GroupBy(pair => pair.PairNum)
                    .ToDictionary(x => x.Key, x => x.First().value);

                var dataExport = new List<DuToanChiTieuTongHop>();
                foreach (var item in dataGroupByMlnsId)
                {
                    var dictDonViByDonVi = usedBudgetEstimateDivisionMapByCategoryId.GetValueOrDefault(item.Key, new Dictionary<string, List<DtChungTuChiTietModel>>());
                    var dictValue = new Dictionary<int, List<DtChungTuChiTietModel>>();
                    for (var i = 0; i < pageSize; i++)
                    {
                        var mlnsByCol = dictByIndexCol.GetValueOrDefault(i, string.Empty);
                        if (mlnsByCol == null)
                        {
                            mlnsByCol = string.Empty;
                        }
                        dictValue.Add(i, dictDonViByDonVi.GetValueOrDefault(mlnsByCol, new List<DtChungTuChiTietModel>()));
                    }

                    var duToanChiTieuTongHop = new DuToanChiTieuTongHop();
                    dataExport.Add(duToanChiTieuTongHop);

                    duToanChiTieuTongHop.Model = item.Value.ElementAt(0);

                    CalculateDataExportDynamicCol(item.Key, catUnitType, duToanChiTieuTongHop, dictValue, dictDonViByDonVi, totalBudgetEstimateDivisionMapByCategoryId);
                }

                var listFilterExport = CalculateData(dataExport);
                var dictTotalByIndexCol = CalculateTotal(listFilterExport);
                for (var i = 0; i < pageSize; i++)
                {
                    data.Add($"MoTa{i + 1}", dictByIndexCol.GetValueOrDefault(i, string.Empty));
                    data.Add($"Total{i + 1}", dictTotalByIndexCol.GetValueOrDefault(i, 0.0));
                }

                data.Add("TotalDuToan", dictTotalByIndexCol.GetValueOrDefault(-1, 0.0));
                data.Add("TotalPhanBo", dictTotalByIndexCol.GetValueOrDefault(-2, 0.0));
                data.Add("TotalConLai", dictTotalByIndexCol.GetValueOrDefault(-3, 0.0));

                foreach (var item in listFilterExport.Where(x => !string.IsNullOrEmpty(x.Model.SL)).OrderByDescending(x => x.Model.SXauNoiMa))
                {
                    var parent = listFilterExport.Where(x => x.Model.IIdMlns == item.Model.IIdMlnsCha).LastOrDefault();
                    if (parent != null && item.Model.SM != string.Empty)
                    {
                        if (!string.IsNullOrEmpty(parent.Model.SL) && !string.IsNullOrEmpty(parent.Model.SK))
                        {
                            item.Model.SL = string.Empty;
                            item.Model.SK = string.Empty;
                            item.Model.SLns = string.Empty;
                        }
                        if (!string.IsNullOrEmpty(parent.Model.SM))
                            item.Model.SM = string.Empty;
                        if (!string.IsNullOrEmpty(parent.Model.STm))
                            item.Model.STm = string.Empty;
                        if (!string.IsNullOrEmpty(parent.Model.STtm))
                            item.Model.STtm = string.Empty;
                        if (!string.IsNullOrEmpty(parent.Model.SNg))
                            item.Model.SNg = string.Empty;
                        if (!string.IsNullOrEmpty(parent.Model.STng))
                            item.Model.STng = string.Empty;
                        if (!string.IsNullOrEmpty(parent.Model.STng1))
                            item.Model.STng1 = string.Empty;
                        if (!string.IsNullOrEmpty(parent.Model.STng2))
                            item.Model.STng2 = string.Empty;
                        if (!string.IsNullOrEmpty(parent.Model.STng3))
                            item.Model.STng3 = string.Empty;
                    }
                }

                var listFilterExportClone = ObjectCopier.Clone(listFilterExport);
                switch (SelectedPrintTypeMLNS.ValueItem)
                {
                    case nameof(MLNSFiled.NG):
                        listFilterExportClone = listFilterExportClone.Where(x => string.IsNullOrEmpty(x.Model.STng)).ToList();
                        listFilterExportClone.Where(x => !string.IsNullOrEmpty(x.Model.SNg)).Select(x => x.Model.IsHangCha = false).ToList();
                        break;
                    case nameof(MLNSFiled.TNG):
                        listFilterExportClone = listFilterExportClone.Where(x => string.IsNullOrEmpty(x.Model.STng1)).ToList();
                        listFilterExportClone.Where(x => !string.IsNullOrEmpty(x.Model.STng)).Select(x => x.Model.IsHangCha = false).ToList();
                        break;
                    case nameof(MLNSFiled.TNG1):
                        listFilterExportClone = listFilterExportClone.Where(x => string.IsNullOrEmpty(x.Model.STng2)).ToList();
                        listFilterExportClone.Where(x => !string.IsNullOrEmpty(x.Model.STng1)).Select(x => x.Model.IsHangCha = false).ToList();
                        break;
                    case nameof(MLNSFiled.TNG2):
                        listFilterExportClone = listFilterExportClone.Where(x => string.IsNullOrEmpty(x.Model.STng3)).ToList();
                        listFilterExportClone.Where(x => !string.IsNullOrEmpty(x.Model.STng2)).Select(x => x.Model.IsHangCha = false).ToList();
                        break;
                }

                data.Add("Items", listFilterExportClone.OrderBy(x => x.Model.SXauNoiMa));

                var strPageNumber = dictByPage.Key > 0 ? "_To2" : "_To1";
                var outputFileName = $"{ExportFileName.RPT_NS_DUTOAN_CHITIEU_TONGHOP_DONVI}_{idDonViLogin}_{DateTime.Now.Millisecond}{strPageNumber}{extension}";
                var templateFileName = GetFileTemplate(strPageNumber);
                listResult.Add(Tuple.Create(templateFileName, outputFileName, data));
            }

            return listResult;
        }


        public List<Tuple<string, string, Dictionary<string, object>>> ConvertDataExportExecl(IEnumerable<DtChungTuChiTietModel> listData, string extension)
        {
            _dmChuKy = _dmChuKyService.FindByCondition(x => x.IdType.Equals(TypeChuKy.RPT_NS_DUTOAN_TONGHOP_PHANBO) && x.ITrangThai == StatusType.ACTIVE).FirstOrDefault();
            var idDonViLogin = _sessionService.Current.IdDonVi;
            var dataGroupByMlnsId = listData.GroupBy(x => x.IIdMlns.ToString()).ToDictionary(x => x.Key, x => x.ToList());
            // get total budget
            var listDetailAvailable = GetTotalBudgetAvailableLNS();
            var totalBudgetEstimateDivisionMapByCategoryId = listDetailAvailable
                .GroupBy(budget => budget.IIdMlns.ToString())
                .ToDictionary(g => g.Key, g => g.ToList());

            if (CheckPrintAccumulation)
            {
                totalBudgetEstimateDivisionMapByCategoryId.ForAll(item =>
                {
                    if (!dataGroupByMlnsId.ContainsKey(item.Key))
                    {
                        dataGroupByMlnsId.Add(item.Key, item.Value);
                    }
                });
            }

            var usedBudgetEstimateDivisionMapByCategoryId = _listChungTuChiTietReport.Where(x => !string.IsNullOrEmpty(x.IIdMaDonVi))
                .GroupBy(budget => budget.IIdMlns.ToString())
                .ToDictionary(g => g.Key, g => g.GroupBy(e => e.STenDonVi).ToDictionary(e => e.Key, e => e.ToList()));

            _listDonVi = _listChungTuChiTietReport.OrderBy(x => x.IIdMaDonVi).Select(x => x.STenDonVi).ToHashSet();
            var pageSize = _listDonVi.Count();
            var dicAgencyByPageExcel = _listDonVi.Select((x, i) => new { Group = i / pageSize, Value = x })
             .GroupBy(item => item.Group, g => g.Value)
             .ToDictionary(x => x.Key, x => x.ToList());

            var listResult = new List<Tuple<string, string, Dictionary<string, object>>>();

            var catUnitTypeStr = "Đơn vị tính: " + CatUnitTypeSelected.DisplayItem + " Tờ số: {0}";
            var catUnitType = Convert.ToInt32(CatUnitTypeSelected.ValueItem);

            foreach (var dictByPage in dicAgencyByPageExcel)
            {
                if (!ListCoverSheet.Where(x => x.IsChecked).Any(x => x.ValueItem == (dictByPage.Key + 1).ToString()))
                {
                    continue;
                }
                var data = new Dictionary<string, object>();
                FormatNumber formatNumber = new FormatNumber(catUnitType, StringUtils.PDF_EXTENSION == extension ? ExportType.PDF : ExportType.EXCEL);
                data.Add("FormatNumber", formatNumber);
                data.Add("Cap1", dictByPage.Key > 0 ? string.Empty : _cap1);
                data.Add("Cap2", dictByPage.Key > 0 ? string.Empty : _sessionService.Current.TenDonVi);
                data.Add("TitleFirst", dictByPage.Key > 0 ? string.Empty : TxtTitleFirst);
                data.Add("TitleSecond", dictByPage.Key > 0 ? string.Empty : TxtTitleSecond);
                data.Add("TitleThird", dictByPage.Key > 0 ? string.Empty : TxtTitleThird);
                data.Add("header1", dictByPage.Key > 0 ? string.Empty : (_cbxSpecializedSelected != null ? $"Ngành: {_cbxSpecializedSelected.DisplayItem}" : string.Empty));
                data.Add("CatUnitType", string.Format(catUnitTypeStr, dictByPage.Key + 1));

                data.Add("DiaDiem", dictByPage.Key > 0 ? string.Empty : _diaDiem);
                data.Add("Ngay", dictByPage.Key > 0 ? string.Empty : DateUtils.FormatDateReport(ReportDate));

                data.Add("ThuaLenh1", dictByPage.Key > 0 ? string.Empty : (_dmChuKy != null ? _dmChuKy.ThuaLenh1MoTa : string.Empty));
                data.Add("ThuaLenh2", dictByPage.Key > 0 ? string.Empty : (_dmChuKy != null ? _dmChuKy.ThuaLenh2MoTa : string.Empty));
                data.Add("ThuaLenh3", dictByPage.Key > 0 ? string.Empty : (_dmChuKy != null ? _dmChuKy.ThuaLenh3MoTa : string.Empty));

                data.Add("ChucDanh1", dictByPage.Key > 0 ? string.Empty : (_dmChuKy != null ? _dmChuKy.ChucDanh1MoTa : string.Empty));
                data.Add("ChucDanh2", dictByPage.Key > 0 ? string.Empty : (_dmChuKy != null ? _dmChuKy.ChucDanh2MoTa : string.Empty));
                data.Add("ChucDanh3", dictByPage.Key > 0 ? string.Empty : (_dmChuKy != null ? _dmChuKy.ChucDanh3MoTa : string.Empty));

                data.Add("Ten1", dictByPage.Key > 0 ? string.Empty : (_dmChuKy != null ? _dmChuKy.Ten1MoTa : string.Empty));
                data.Add("Ten2", dictByPage.Key > 0 ? string.Empty : (_dmChuKy != null ? _dmChuKy.Ten2MoTa : string.Empty));
                data.Add("Ten3", dictByPage.Key > 0 ? string.Empty : (_dmChuKy != null ? _dmChuKy.Ten3MoTa : string.Empty));

                var dictByIndexCol = dictByPage.Value
                    .Select((value, index) => new { PairNum = index, value })
                    .GroupBy(pair => pair.PairNum)
                    .ToDictionary(x => x.Key, x => x.First().value);

                var dataExport = new List<DuToanChiTieuTongHop>();
                foreach (var item in dataGroupByMlnsId)
                {
                    var dictDonViByDonVi = usedBudgetEstimateDivisionMapByCategoryId.GetValueOrDefault(item.Key, new Dictionary<string, List<DtChungTuChiTietModel>>());
                    var dictValue = new Dictionary<int, List<DtChungTuChiTietModel>>();
                    for (var i = 0; i < pageSize; i++)
                    {
                        var mlnsByCol = dictByIndexCol.GetValueOrDefault(i, string.Empty);
                        if (mlnsByCol == null)
                        {
                            mlnsByCol = string.Empty;
                        }
                        dictValue.Add(i, dictDonViByDonVi.GetValueOrDefault(mlnsByCol, new List<DtChungTuChiTietModel>()));
                    }

                    var duToanChiTieuTongHop = new DuToanChiTieuTongHop();
                    dataExport.Add(duToanChiTieuTongHop);

                    duToanChiTieuTongHop.Model = item.Value.ElementAt(0);

                    CalculateDataExportDynamicCol(item.Key, catUnitType, duToanChiTieuTongHop, dictValue, dictDonViByDonVi, totalBudgetEstimateDivisionMapByCategoryId);

                    duToanChiTieuTongHop.ValDuToan = totalBudgetEstimateDivisionMapByCategoryId.GetValueOrDefault(item.Key, new List<DtChungTuChiTietModel>()).Sum(GetDataByType);
                    duToanChiTieuTongHop.ValPhanBo = dictDonViByDonVi.Values.SelectMany(x => x).Sum(GetDataByType);
                    duToanChiTieuTongHop.ValConLai = duToanChiTieuTongHop.ValDuToan - duToanChiTieuTongHop.ValPhanBo;

                    duToanChiTieuTongHop.LstGiaTri = new List<DuToanChiTieuTongHopColDymamic>();

                    for (int m = 0; m < pageSize; m++)
                    {
                        duToanChiTieuTongHop.LstGiaTri.Add(new DuToanChiTieuTongHopColDymamic
                        {
                            fTuChi = dictValue.GetValueOrDefault(m, new List<DtChungTuChiTietModel>()).Sum(GetDataByType),
                            sMoTa = dictByIndexCol.GetValueOrDefault(m, string.Empty)
                        });
                    }
                }

                var listFilterExport = CalculateData(dataExport);
                var dictTotalByIndexCol = CalculateTotal(listFilterExport);

                //Tính giá trị cha 
                for (int index = dataExport.Count() - 1; index > 0; --index)
                {
                    Guid? iIdparent = dataExport[index].Model.IIdMlnsCha;
                    if (!iIdparent.HasValue) continue;
                    var objParent = dataExport.FirstOrDefault(n => n.Model.IIdMlns == iIdparent.Value);
                    if (objParent == null) continue;
                    var j = 0;
                    foreach (var item in dataExport[index].LstGiaTri)
                    {
                        if (objParent.LstGiaTri == null)
                            objParent.LstGiaTri.Add(new DuToanChiTieuTongHopColDymamic());
                        objParent.LstGiaTri[j].fTuChi += item.fTuChi;
                        ++j;
                    }
                }

                //Add header
                List<DuToanChiTieuTongHopColDymamic> lstHeader1 = new List<DuToanChiTieuTongHopColDymamic>();
                lstHeader1.Add(new DuToanChiTieuTongHopColDymamic { STT = 1, sMoTa = "Trong đó" });
                for (var i = 0; i < pageSize; i++)
                {
                    DuToanChiTieuTongHopColDymamic headerCol = new DuToanChiTieuTongHopColDymamic();
                    lstHeader1.Add(headerCol);
                }

                List<DuToanChiTieuTongHopColDymamic> lstHeader = new List<DuToanChiTieuTongHopColDymamic>();
                for (var i = 0; i < pageSize; i++)
                {
                    DuToanChiTieuTongHopColDymamic headerCol = new DuToanChiTieuTongHopColDymamic();
                    headerCol.sMoTa = dictByIndexCol.GetValueOrDefault(i, string.Empty);
                    lstHeader.Add(headerCol);
                }
                //Mergecell 1
                int columnStart = 18;
                var mergeRange = "";
                var columnStartName = GetExcelColumnName(columnStart);
                var columnEndName = GetExcelColumnName(pageSize + columnStart - 1);

                //Add total 
                List<DuToanChiTieuTongHopColDymamic> lstTotal = new List<DuToanChiTieuTongHopColDymamic>();

                for (int k = 0; k < _listDonVi.Count(); k++)
                {
                    DuToanChiTieuTongHopColDymamic colTotal = new DuToanChiTieuTongHopColDymamic();
                    colTotal.fTuChi = listFilterExport.Where(x => !x.Model.IsHangCha).Sum(x => x.LstGiaTri[k].fTuChi);
                    lstTotal.Add(colTotal);
                }
                //If pagesize < 7
                if (pageSize < 7)
                {
                    columnEndName = GetExcelColumnName(6 + columnStart);
                    for (int i = 0; i < 7 - pageSize; i++)
                    {
                        DuToanChiTieuTongHopColDymamic col = new DuToanChiTieuTongHopColDymamic();
                        lstHeader1.Add(col);
                        lstHeader.Add(col);
                        lstTotal.Add(col);
                        foreach (var item in dataExport)
                        {
                            item.LstGiaTri.Add(col);
                        }
                    }
                }
                lstHeader1 = lstHeader1.Take(lstHeader1.Count() - 1).ToList();
                mergeRange = columnStartName + "9" + ":" + columnEndName + "9";

                data.Add("LstHeader1", lstHeader1);
                data.Add("LstHeader", lstHeader);
                data.Add("LstToTal", lstTotal);
                data.Add("MergeRange", mergeRange);
                data.Add("TotalDuToan", dictTotalByIndexCol.GetValueOrDefault(-1, 0.0));
                data.Add("TotalPhanBo", dictTotalByIndexCol.GetValueOrDefault(-2, 0.0));
                data.Add("TotalConLai", dictTotalByIndexCol.GetValueOrDefault(-3, 0.0));

                switch (SelectedPrintTypeMLNS.ValueItem)
                {
                    case nameof(MLNSFiled.NG):
                        listFilterExport = listFilterExport.Where(x => string.IsNullOrEmpty(x.Model.STng)).ToList();
                        listFilterExport.Where(x => !string.IsNullOrEmpty(x.Model.SNg)).Select(x => x.Model.IsHangCha = false).ToList();
                        break;
                    case nameof(MLNSFiled.TNG):
                        listFilterExport = listFilterExport.Where(x => string.IsNullOrEmpty(x.Model.STng1)).ToList();
                        listFilterExport.Where(x => !string.IsNullOrEmpty(x.Model.STng)).Select(x => x.Model.IsHangCha = false).ToList();
                        break;
                    case nameof(MLNSFiled.TNG1):
                        listFilterExport = listFilterExport.Where(x => string.IsNullOrEmpty(x.Model.STng2)).ToList();
                        listFilterExport.Where(x => !string.IsNullOrEmpty(x.Model.STng1)).Select(x => x.Model.IsHangCha = false).ToList();
                        break;
                    case nameof(MLNSFiled.TNG2):
                        listFilterExport = listFilterExport.Where(x => string.IsNullOrEmpty(x.Model.STng3)).ToList();
                        listFilterExport.Where(x => !string.IsNullOrEmpty(x.Model.STng2)).Select(x => x.Model.IsHangCha = false).ToList();
                        break;
                }

                listFilterExport.Where(n => n.Model.IIdMlnsCha == null).Select(n => n.Model.IIdMlnsCha = Guid.Empty).ToList();

                foreach (var item in listFilterExport.Where(x => !string.IsNullOrEmpty(x.Model.SL)).OrderByDescending(x => x.Model.SXauNoiMa))
                {
                    var parent = listFilterExport.Where(x => x.Model.IIdMlns == item.Model.IIdMlnsCha).LastOrDefault();
                    if (parent != null && item.Model.SM != string.Empty)
                    {
                        if (!string.IsNullOrEmpty(parent.Model.SL) && !string.IsNullOrEmpty(parent.Model.SL))
                        {
                            item.Model.SL = string.Empty;
                            item.Model.SK = string.Empty;
                            item.Model.SLns = string.Empty;
                        }

                        if (!string.IsNullOrEmpty(parent.Model.SM))
                            item.Model.SM = string.Empty;
                        if (!string.IsNullOrEmpty(parent.Model.STm))
                            item.Model.STm = string.Empty;
                        if (!string.IsNullOrEmpty(parent.Model.STtm))
                            item.Model.STtm = string.Empty;
                        if (!string.IsNullOrEmpty(parent.Model.SNg))
                            item.Model.SNg = string.Empty;
                        if (!string.IsNullOrEmpty(parent.Model.STng))
                            item.Model.STng = string.Empty;
                        if (!string.IsNullOrEmpty(parent.Model.STng1))
                            item.Model.STng1 = string.Empty;
                        if (!string.IsNullOrEmpty(parent.Model.STng2))
                            item.Model.STng2 = string.Empty;
                        if (!string.IsNullOrEmpty(parent.Model.STng3))
                            item.Model.STng3 = string.Empty;
                    }
                }
                data.Add("Items", listFilterExport.OrderBy(x => x.Model.SXauNoiMa));
                data.Add("Count", 10000);

                var strPageNumber = dictByPage.Key > 0 ? "_To2" : "_To1";
                var outputFileName = $"{ExportFileName.RPT_NS_DUTOAN_CHITIEU_TONGHOP_DONVI_EXCEL}_{idDonViLogin}_{DateTime.Now.Millisecond}{strPageNumber}{extension}";
                if (CheckPrintAccumulation)
                {
                    var templateFileName = Path.Combine(ExportPrefix.PATH_TL_DT, $"{ExportFileName.RPT_NS_DUTOAN_CHITIEU_TONGHOP_DONVI_EXCEL}OnePage{StringUtils.EXCEL_EXTENSION}");
                    listResult.Add(Tuple.Create(templateFileName, outputFileName, data));
                }
                else
                {
                    var typeExcel = StringUtils.PDF_EXTENSION == extension ? ExportType.PDF : ExportType.EXCEL;
                    if (typeExcel == ExportType.EXCEL && InMotToChecked)
                    {
                        var templateFileName = Path.Combine(ExportPrefix.PATH_TL_DT, $"{ExportFileName.RPT_NS_DUTOAN_CHITIEU_TONGHOP_DONVI_EXCEL}_TheoDotOnePage{StringUtils.EXCEL_EXTENSION}");
                        listResult.Add(Tuple.Create(templateFileName, outputFileName, data));
                    }
                    else
                    {
                        var templateFileName = Path.Combine(ExportPrefix.PATH_TL_DT, $"{ExportFileName.RPT_NS_DUTOAN_CHITIEU_TONGHOP_DONVI_EXCEL}_TheoDot{StringUtils.EXCEL_EXTENSION}");
                        listResult.Add(Tuple.Create(templateFileName, outputFileName, data));
                    }

                }

            }

            return listResult;
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

        public List<Tuple<string, string, Dictionary<string, object>>> ConvertDataExportOnePaper(IEnumerable<DtChungTuChiTietModel> listData, string extension)
        {
            _dmChuKy = _dmChuKyService.FindByCondition(x => x.IdType.Equals(TypeChuKy.RPT_NS_DUTOAN_TONGHOP_PHANBO) && x.ITrangThai == StatusType.ACTIVE).FirstOrDefault();
            var idDonViLogin = _sessionService.Current.IdDonVi;
            var dataGroupByMlnsId = listData.GroupBy(x => x.IIdMlns.ToString()).ToDictionary(x => x.Key, x => x.ToList());
            // get total budget
            var listDetailAvailable = GetTotalBudgetAvailableLNS();
            var totalBudgetEstimateDivisionMapByCategoryId = listDetailAvailable
                .GroupBy(budget => budget.IIdMlns.ToString())
                .ToDictionary(g => g.Key, g => g.ToList());

            var usedBudgetEstimateDivisionMapByCategoryId = _listChungTuChiTietReport.Where(x => !string.IsNullOrEmpty(x.IIdMaDonVi))
                .GroupBy(budget => budget.IIdMlns.ToString())
                .ToDictionary(g => g.Key, g => g.GroupBy(e => e.STenDonVi).ToDictionary(e => e.Key, e => e.ToList()));

            var pageSize = 7;
            var listResult = new List<Tuple<string, string, Dictionary<string, object>>>();

            var catUnitTypeStr = "Đơn vị tính: " + CatUnitTypeSelected.DisplayItem + " Tờ số: {0}";
            var catUnitType = Convert.ToInt32(CatUnitTypeSelected.ValueItem);

            foreach (var dictByPage in _dictAgencyByPageSize)
            {
                if (!ListCoverSheet.Where(x => x.IsChecked).Any(x => x.ValueItem == (dictByPage.Key + 1).ToString()))
                {
                    continue;
                }
                var data = new Dictionary<string, object>();
                FormatNumber formatNumber = new FormatNumber(catUnitType, StringUtils.PDF_EXTENSION == extension ? ExportType.PDF : ExportType.EXCEL);
                data.Add("FormatNumber", formatNumber);
                data.Add("Cap1", dictByPage.Key > 0 ? string.Empty : _cap1);
                data.Add("Cap2", dictByPage.Key > 0 ? string.Empty : _sessionService.Current.TenDonVi);
                data.Add("TitleFirst", dictByPage.Key > 0 ? string.Empty : TxtTitleFirst);
                data.Add("TitleSecond", dictByPage.Key > 0 ? string.Empty : TxtTitleSecond);
                data.Add("TitleThird", dictByPage.Key > 0 ? string.Empty : TxtTitleThird);
                data.Add("header1", dictByPage.Key > 0 ? string.Empty : (_cbxSpecializedSelected != null ? $"Ngành: {_cbxSpecializedSelected.DisplayItem}" : string.Empty));
                data.Add("CatUnitType", string.Format(catUnitTypeStr, dictByPage.Key + 1));

                data.Add("DiaDiem", dictByPage.Key > 0 ? string.Empty : _diaDiem);
                data.Add("Ngay", dictByPage.Key > 0 ? string.Empty : DateUtils.FormatDateReport(ReportDate));

                data.Add("ThuaLenh1", dictByPage.Key > 0 ? string.Empty : (_dmChuKy != null ? _dmChuKy.ThuaLenh1MoTa : string.Empty));
                data.Add("ThuaLenh2", dictByPage.Key > 0 ? string.Empty : (_dmChuKy != null ? _dmChuKy.ThuaLenh2MoTa : string.Empty));
                data.Add("ThuaLenh3", dictByPage.Key > 0 ? string.Empty : (_dmChuKy != null ? _dmChuKy.ThuaLenh3MoTa : string.Empty));

                data.Add("ChucDanh1", dictByPage.Key > 0 ? string.Empty : (_dmChuKy != null ? _dmChuKy.ChucDanh1MoTa : string.Empty));
                data.Add("ChucDanh2", dictByPage.Key > 0 ? string.Empty : (_dmChuKy != null ? _dmChuKy.ChucDanh2MoTa : string.Empty));
                data.Add("ChucDanh3", dictByPage.Key > 0 ? string.Empty : (_dmChuKy != null ? _dmChuKy.ChucDanh3MoTa : string.Empty));

                data.Add("Ten1", dictByPage.Key > 0 ? string.Empty : (_dmChuKy != null ? _dmChuKy.Ten1MoTa : string.Empty));
                data.Add("Ten2", dictByPage.Key > 0 ? string.Empty : (_dmChuKy != null ? _dmChuKy.Ten2MoTa : string.Empty));
                data.Add("Ten3", dictByPage.Key > 0 ? string.Empty : (_dmChuKy != null ? _dmChuKy.Ten3MoTa : string.Empty));

                var dictByIndexCol = dictByPage.Value
                    .Select((value, index) => new { PairNum = index, value })
                    .GroupBy(pair => pair.PairNum)
                    .ToDictionary(x => x.Key, x => x.First().value);

                var dataExport = new List<DuToanChiTieuTongHop>();
                foreach (var item in dataGroupByMlnsId)
                {
                    var dictDonViByDonVi = usedBudgetEstimateDivisionMapByCategoryId.GetValueOrDefault(item.Key, new Dictionary<string, List<DtChungTuChiTietModel>>());
                    var dictValue = new Dictionary<int, List<DtChungTuChiTietModel>>();
                    for (var i = 0; i < pageSize; i++)
                    {
                        var mlnsByCol = dictByIndexCol.GetValueOrDefault(i, string.Empty);
                        if (mlnsByCol == null)
                        {
                            mlnsByCol = string.Empty;
                        }
                        dictValue.Add(i, dictDonViByDonVi.GetValueOrDefault(mlnsByCol, new List<DtChungTuChiTietModel>()));
                    }

                    var duToanChiTieuTongHop = new DuToanChiTieuTongHop();
                    dataExport.Add(duToanChiTieuTongHop);

                    duToanChiTieuTongHop.Model = item.Value.ElementAt(0);

                    CalculateDataExportDynamicCol(item.Key, catUnitType, duToanChiTieuTongHop, dictValue, dictDonViByDonVi, totalBudgetEstimateDivisionMapByCategoryId);
                }

                var listFilterExport = CalculateData(dataExport);
                var dictTotalByIndexCol = CalculateTotal(listFilterExport);
                for (var i = 0; i < pageSize; i++)
                {
                    data.Add($"MoTa{i + 1}", dictByIndexCol.GetValueOrDefault(i, string.Empty));
                    data.Add($"Total{i + 1}", dictTotalByIndexCol.GetValueOrDefault(i, 0.0));
                }

                data.Add("TotalDuToan", dictTotalByIndexCol.GetValueOrDefault(-1, 0.0));
                data.Add("TotalPhanBo", dictTotalByIndexCol.GetValueOrDefault(-2, 0.0));
                data.Add("TotalConLai", dictTotalByIndexCol.GetValueOrDefault(-3, 0.0));

                switch (SelectedPrintTypeMLNS.ValueItem)
                {
                    case nameof(MLNSFiled.NG):
                        listFilterExport = listFilterExport.Where(x => string.IsNullOrEmpty(x.Model.STng)).ToList();
                        listFilterExport.Where(x => !string.IsNullOrEmpty(x.Model.SNg)).Select(x => x.Model.IsHangCha = false).ToList();
                        break;
                    case nameof(MLNSFiled.TNG):
                        listFilterExport = listFilterExport.Where(x => string.IsNullOrEmpty(x.Model.STng1)).ToList();
                        listFilterExport.Where(x => !string.IsNullOrEmpty(x.Model.STng)).Select(x => x.Model.IsHangCha = false).ToList();
                        break;
                    case nameof(MLNSFiled.TNG1):
                        listFilterExport = listFilterExport.Where(x => string.IsNullOrEmpty(x.Model.STng2)).ToList();
                        listFilterExport.Where(x => !string.IsNullOrEmpty(x.Model.STng1)).Select(x => x.Model.IsHangCha = false).ToList();
                        break;
                    case nameof(MLNSFiled.TNG2):
                        listFilterExport = listFilterExport.Where(x => string.IsNullOrEmpty(x.Model.STng3)).ToList();
                        listFilterExport.Where(x => !string.IsNullOrEmpty(x.Model.STng2)).Select(x => x.Model.IsHangCha = false).ToList();
                        break;
                }
                foreach (var item in listFilterExport.Where(x => !string.IsNullOrEmpty(x.Model.SL)).OrderByDescending(x => x.Model.SXauNoiMa))
                {
                    var parent = listFilterExport.Where(x => x.Model.IIdMlns == item.Model.IIdMlnsCha).LastOrDefault();
                    if (parent != null && item.Model.SM != string.Empty)
                    {
                        if (!string.IsNullOrEmpty(parent.Model.SL) && !string.IsNullOrEmpty(parent.Model.SK))
                        {
                            item.Model.SL = string.Empty;
                            item.Model.SK = string.Empty;
                            item.Model.SLns = string.Empty;
                        }

                        if (!string.IsNullOrEmpty(parent.Model.SM))
                            item.Model.SM = string.Empty;
                        if (!string.IsNullOrEmpty(parent.Model.STm))
                            item.Model.STm = string.Empty;
                        if (!string.IsNullOrEmpty(parent.Model.STtm))
                            item.Model.STtm = string.Empty;
                        if (!string.IsNullOrEmpty(parent.Model.SNg))
                            item.Model.SNg = string.Empty;
                        if (!string.IsNullOrEmpty(parent.Model.STng))
                            item.Model.STng = string.Empty;
                        if (!string.IsNullOrEmpty(parent.Model.STng1))
                            item.Model.STng1 = string.Empty;
                        if (!string.IsNullOrEmpty(parent.Model.STng2))
                            item.Model.STng2 = string.Empty;
                        if (!string.IsNullOrEmpty(parent.Model.STng3))
                            item.Model.STng3 = string.Empty;
                    }
                }
                data.Add("Items", listFilterExport.OrderBy(x => x.Model.SXauNoiMa));

                var strPageNumber = dictByPage.Key > 0 ? "_To2" : "_To1";
                var outputFileName = $"{ExportFileName.RPT_NS_DUTOAN_CHITIEU_TONGHOP_DONVI}_{idDonViLogin}_{DateTime.Now.Millisecond}{strPageNumber}{extension}";
                var templateFileName = GetFileTemplateOnePaper(strPageNumber);
                listResult.Add(Tuple.Create(templateFileName, outputFileName, data));
            }

            return listResult;
        }

        public override void Export(object obj, ExportType type)
        {
            BackgroundWorkerHelper.Run((s, e) =>
            {
                IsLoading = true;
                List<ExportResult> results = new List<ExportResult>();
                List<Tuple<string, string, Dictionary<string, object>>> dataExport = new List<Tuple<string, string, Dictionary<string, object>>>();
                switch (type)
                {
                    case ExportType.EXCEL:
                        dataExport = ConvertDataExportExecl(GetData(), StringUtils.EXCEL_EXTENSION);
                        break;
                    case ExportType.PDF:
                        dataExport = ConvertDataExport(GetData(), StringUtils.PDF_EXTENSION);
                        break;
                    case ExportType.WORD:
                        break;
                    case ExportType.BROWSER:
                        dataExport = ConvertDataExport(GetData(), StringUtils.PDF_EXTENSION);
                        break;
                    case ExportType.PDF_ONE_PAPER:
                        dataExport = ConvertDataExportOnePaper(GetData(), StringUtils.PDF_EXTENSION);
                        break;
                    case ExportType.SIGNATURE:
                        break;
                }
                foreach (var item in dataExport)
                {
                    List<int> hideColumns = new List<int>();
                    if (SelectedPrintTypeMLNS != null)
                        hideColumns = ExportExcelHelper<DuToanChiTieuTongHop>.HideColumn(SelectedPrintTypeMLNS.ValueItem).Select(x => x + 1).ToList();
                    var xlsFile = _exportService.Export<DuToanChiTieuTongHop, DuToanChiTieuTongHopColDymamic>(item.Item1, item.Item3, hideColumns, true);
                    string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(item.Item2);
                    results.Add(new ExportResult(fileNameWithoutExtension, fileNameWithoutExtension, null, xlsFile));
                }
                e.Result = results;
            },
            (s, e) =>
            {
                if (e.Error == null && InMotToChecked && type.Equals(ExportType.PDF_ONE_PAPER))
                {
                    var result = (List<ExportResult>)e.Result;
                    if (result.Count() == 0)
                        MessageBoxHelper.Info(Resources.AlertEmptyReport);
                    else _exportService.Open(result, ExportType.PDF_ONE_PAPER);
                }
                else if (e.Error == null && (type.Equals(ExportType.EXCEL) || type.Equals(ExportType.PDF) || type.Equals(ExportType.BROWSER)))
                {
                    var result = (List<ExportResult>)e.Result;
                    if (result.Count() == 0)
                        MessageBoxHelper.Info(Resources.AlertEmptyReport);
                    else _exportService.Open(result, type.Equals(ExportType.EXCEL) ? ExportType.EXCEL : ExportType.PDF);
                }
                IsLoading = false;
            });
            //HandleAfterExport();
        }

        public override void HandleAfterExport()
        {
            DialogHost.CloseDialogCommand.Execute(null, null);
        }

        private double GetDataByType(DtChungTuChiTietModel chungTuChiTiet)
        {
            if (_cbxVoucherTypeSelected == null)
            {
                return 0.0;
            }

            switch (_cbxVoucherTypeSelected.ValueItem)
            {
                case VoucherType.NSSD_Key:
                    return chungTuChiTiet.FTuChi;
                case VoucherType.NSBD_Key:
                    return chungTuChiTiet.FHienVat;
                case "3":
                    return chungTuChiTiet.FTuChi + chungTuChiTiet.FHienVat;
                case "4":
                    return chungTuChiTiet.FHangNhap;
                case "5":
                    return chungTuChiTiet.FHangMua;
                case "6":
                    return chungTuChiTiet.FPhanCap;
                case "7":
                    return chungTuChiTiet.FHangNhap + chungTuChiTiet.FHangMua + chungTuChiTiet.FPhanCap;
            }

            return 0.0;
        }

        private void CalculateDataExportDynamicCol(
            string mlnsId,
            int catUnitType,
            DuToanChiTieuTongHop duToanChiTieuTongHop,
            Dictionary<int, List<DtChungTuChiTietModel>> dictValue,
            Dictionary<string, List<DtChungTuChiTietModel>> dictDonViByDonVi,
            Dictionary<string, List<DtChungTuChiTietModel>> totalBudgetEstimateDivisionMapByCategoryId)
        {

            duToanChiTieuTongHop.ValDuToan = totalBudgetEstimateDivisionMapByCategoryId.GetValueOrDefault(mlnsId, new List<DtChungTuChiTietModel>()).Sum(GetDataByType);
            duToanChiTieuTongHop.ValPhanBo = dictDonViByDonVi.Values.SelectMany(x => x).Sum(GetDataByType);
            duToanChiTieuTongHop.ValConLai = duToanChiTieuTongHop.ValDuToan - duToanChiTieuTongHop.ValPhanBo;

            duToanChiTieuTongHop.Val1 = dictValue.GetValueOrDefault(0, new List<DtChungTuChiTietModel>()).Sum(GetDataByType);
            duToanChiTieuTongHop.Val2 = dictValue.GetValueOrDefault(1, new List<DtChungTuChiTietModel>()).Sum(GetDataByType);
            duToanChiTieuTongHop.Val3 = dictValue.GetValueOrDefault(2, new List<DtChungTuChiTietModel>()).Sum(GetDataByType);
            duToanChiTieuTongHop.Val4 = dictValue.GetValueOrDefault(3, new List<DtChungTuChiTietModel>()).Sum(GetDataByType);
            duToanChiTieuTongHop.Val5 = dictValue.GetValueOrDefault(4, new List<DtChungTuChiTietModel>()).Sum(GetDataByType);
            duToanChiTieuTongHop.Val6 = dictValue.GetValueOrDefault(5, new List<DtChungTuChiTietModel>()).Sum(GetDataByType);
            duToanChiTieuTongHop.Val7 = dictValue.GetValueOrDefault(6, new List<DtChungTuChiTietModel>()).Sum(GetDataByType);
            duToanChiTieuTongHop.Val8 = dictValue.GetValueOrDefault(7, new List<DtChungTuChiTietModel>()).Sum(GetDataByType);
        }

        private List<DtChungTuChiTietModel> GetTotalBudgetAvailable()
        {
            var ctFirst = _listChungTuReport.FirstOrDefault();
            DateTime voucherDate = DateTime.Now;
            int voucherIndex = 0;
            if (!CheckPrintAccumulation)
            {
                if (_listChungTuReport.Any(x => x.DNgayQuyetDinh.HasValue))
                    voucherDate = _listChungTuReport.Where(x => x.DNgayQuyetDinh.HasValue).Min(x => x.DNgayQuyetDinh.Value);
                else if (_listChungTuReport.Any(x => x.DNgayChungTu.HasValue))
                    voucherDate = _listChungTuReport.Where(x => x.DNgayChungTu.HasValue).Min(x => x.DNgayChungTu.Value);
                voucherIndex = _listChungTuReport.Min(x => x.ISoChungTuIndex.Value);
            }
            else
            {
                if (_listChungTuReport.Any(x => x.DNgayQuyetDinh.HasValue))
                    voucherDate = _listChungTuReport.Where(x => x.DNgayQuyetDinh.HasValue).Max(x => x.DNgayQuyetDinh.Value);
                else if (_listChungTuReport.Any(x => x.DNgayChungTu.HasValue))
                    voucherDate = _listChungTuReport.Where(x => x.DNgayChungTu.HasValue).Max(x => x.DNgayChungTu.Value);
            }
            var budgetEstimateDivisionCondition = new EstimationVoucherDetailCriteria
            {
                ChungTuId = string.Join(",", _listChungTuReport.Select(x => x.Id)),
                LNS = string.Join(",", _listChungTuReport.Select(x => x.SDslns)),
                YearOfWork = ctFirst.INamLamViec,
                YearOfBudget = ctFirst.INamNganSach,
                BudgetSource = ctFirst.IIdMaNguonNganSach,
                VoucherDate = voucherDate,
                VoucherIndex = voucherIndex,
                IsLuyKe = CheckPrintAccumulation
            };

            var listDetail = _dtChungTuChiTietService.FindBudgetEstimateDivisionBySoQuyetDinh(budgetEstimateDivisionCondition).ToList();
            var listDetailModel = _mapper.Map<List<DtChungTuChiTietModel>>(listDetail);
            return listDetailModel;
        }

        private List<DtChungTuChiTietModel> GetTotalBudgetAvailableLNS()
        {
            var ctFirst = _listChungTuReport.FirstOrDefault();
            DateTime voucherDate = DateTime.Now;
            int voucherIndex = 0;
            if (!CheckPrintAccumulation)
            {
                if (_listChungTuReport.Any(x => x.DNgayQuyetDinh.HasValue))
                    voucherDate = _listChungTuReport.Where(x => x.DNgayQuyetDinh.HasValue).Min(x => x.DNgayQuyetDinh.Value);
                else if (_listChungTuReport.Any(x => x.DNgayChungTu.HasValue))
                    voucherDate = _listChungTuReport.Where(x => x.DNgayChungTu.HasValue).Min(x => x.DNgayChungTu.Value);
                voucherIndex = _listChungTuReport.Min(x => x.ISoChungTuIndex.Value);
            }
            else
            {
                if (_listChungTuReport.Any(x => x.DNgayQuyetDinh.HasValue))
                    voucherDate = _listChungTuReport.Where(x => x.DNgayQuyetDinh.HasValue).Max(x => x.DNgayQuyetDinh.Value);
                else if (_listChungTuReport.Any(x => x.DNgayChungTu.HasValue))
                    voucherDate = _listChungTuReport.Where(x => x.DNgayChungTu.HasValue).Max(x => x.DNgayChungTu.Value);
            }
            var budgetEstimateDivisionCondition = new EstimationVoucherDetailCriteria
            {
                ChungTuId = string.Join(",", _listChungTuReport.Select(x => x.Id)),
                LNS = string.Join(",", ListLNS.Where(m => m.IsSelected).Select(m => m.Lns)),
                YearOfWork = ctFirst.INamLamViec,
                YearOfBudget = ctFirst.INamNganSach,
                BudgetSource = ctFirst.IIdMaNguonNganSach,
                VoucherDate = voucherDate,
                VoucherIndex = voucherIndex,
                IsLuyKe = CheckPrintAccumulation,
                DonViTinh = int.Parse(CatUnitTypeSelected.ValueItem)
            };

            var listDetail = _dtChungTuChiTietService.FindBudgetEstimateDivisionBySoQuyetDinhLNS(budgetEstimateDivisionCondition).ToList();
            var listDetailModel = _mapper.Map<List<DtChungTuChiTietModel>>(listDetail);
            return listDetailModel;
        }

        private List<DtChungTuChiTietModel> GetTotalBudgetUsed(string procedure)
        {
            var ctFirst = _listChungTuReport.FirstOrDefault();
            var sDsln = string.Join(",", _listChungTuReport.Select(s => s.SDslns));
            if (ctFirst.ILoaiDuToan == (int)BudgetType.ADJUSTED)
            {
                procedure = "rpt_du_toan_chi_tieu_tong_hop_dieuchinh";
            }
            var lns = sDsln.Split(StringUtils.COMMA).Where(n => ListLNS.Where(m => m.IsSelected).Select(m => m.Lns).Contains(n)).Distinct();
            var searchCondition = new EstimationVoucherDetailCriteria
            {
                ChungTuId = string.Join(",", _listChungTuReport.Select(x => x.Id)),
                LNS = string.Join(StringUtils.COMMA, lns),
                YearOfWork = ctFirst.INamLamViec,
                YearOfBudget = ctFirst.INamNganSach,
                BudgetSource = ctFirst.IIdMaNguonNganSach,
                VoucherDate = ctFirst.DNgayChungTu,
                IdDotNhan = ctFirst.IIdDotNhan,
                SoChungTu = ctFirst.SSoChungTu,
                DonViTinh = int.Parse(CatUnitTypeSelected.ValueItem)
            };

            var listDetail = _dtChungTuChiTietService.FindByCond(searchCondition, procedure).ToList();
            var listDetailModel = _mapper.Map<List<DtChungTuChiTietModel>>(listDetail);
            return listDetailModel;
        }

        private List<T> CalculateData<T>(List<T> listData) where T : DuToanChiTieuTongHop
        {
            listData.Where(x => x.Model.IsHangCha)
                .Select(x =>
                {
                    x.ValDuToan = 0;
                    x.ValPhanBo = 0;
                    x.ValConLai = 0;
                    return x;
                }).ToList();

            foreach (var item in listData.Where(x => x.Model.IsEditable && (x.ValDuToan != 0 || x.ValPhanBo != 0 ||
                x.ValConLai != 0 || x.Val != 0 || x.Val1 != 0 || x.Val2 != 0 || x.Val3 != 0 || x.Val4 != 0 ||
                x.Val5 != 0 || x.Val6 != 0 || x.Val7 != 0 || x.Val8 != 0)))
            {
                CalculateParent(listData, item, item);
            }

            var listFilterResult = listData.Where(x => x.ValDuToan != 0 || x.ValPhanBo != 0 || x.ValDuToan != 0 || x.ValPhanBo != 0 ||
                x.ValConLai != 0 || x.Val != 0 || x.Val1 != 0 || x.Val2 != 0 || x.Val3 != 0 || x.Val4 != 0 ||
                x.Val5 != 0 || x.Val6 != 0 || x.Val7 != 0 || x.Val8 != 0).ToList();
            return listFilterResult;
        }

        private void CalculateParent<T>(List<T> listData, T currentItem, T seftItem) where T : DuToanChiTieuTongHop
        {
            var parrentItem = listData.FirstOrDefault(x => x.Model.IIdMlns == currentItem.Model.IIdMlnsCha);
            if (parrentItem == null) return;
            parrentItem.ValDuToan += seftItem.ValDuToan;
            parrentItem.ValPhanBo += seftItem.ValPhanBo;
            parrentItem.ValConLai += seftItem.ValConLai;
            parrentItem.Val += seftItem.Val;
            parrentItem.Val1 += seftItem.Val1;
            parrentItem.Val2 += seftItem.Val2;
            parrentItem.Val3 += seftItem.Val3;
            parrentItem.Val4 += seftItem.Val4;
            parrentItem.Val5 += seftItem.Val5;
            parrentItem.Val6 += seftItem.Val6;
            parrentItem.Val7 += seftItem.Val7;
            parrentItem.Val8 += seftItem.Val8;
            CalculateParent(listData, parrentItem, seftItem);
        }

        private Dictionary<int, double> CalculateTotal<T>(List<T> listData) where T : DuToanChiTieuTongHop
        {
            var listChildren = listData.Where(x => x.Model.IsEditable).ToList();
            Dictionary<int, double> dictVal = new Dictionary<int, double>();
            dictVal.Add(-1, listChildren.Sum(x => x.ValDuToan));
            dictVal.Add(-2, listChildren.Sum(x => x.ValPhanBo));
            dictVal.Add(-3, listChildren.Sum(x => x.ValConLai));

            dictVal.Add(0, listChildren.Sum(x => x.Val1));
            dictVal.Add(1, listChildren.Sum(x => x.Val2));
            dictVal.Add(2, listChildren.Sum(x => x.Val3));
            dictVal.Add(3, listChildren.Sum(x => x.Val4));
            dictVal.Add(4, listChildren.Sum(x => x.Val5));
            dictVal.Add(5, listChildren.Sum(x => x.Val6));
            dictVal.Add(6, listChildren.Sum(x => x.Val7));
            dictVal.Add(7, listChildren.Sum(x => x.Val8));
            return dictVal;
        }

        private void GetListChungTuReport()
        {
            _listChungTuReport = new List<DtChungTuModel>();
            List<DtChungTuModel> lstChungTu = new List<DtChungTuModel>();
            DateTime ngayLuyKe = DateTime.MinValue;
            if (DataDotSelected == null) return;
            if (DataDotSelected.HiddenValue.Equals("1"))
            {
                lstChungTu = Models.Where(n => !string.IsNullOrEmpty(n.SSoQuyetDinh) && n.SSoQuyetDinh.Equals(DataDotSelected.ValueItem)).ToList();
                if (lstChungTu.Count > 0)
                {
                    ngayLuyKe = lstChungTu.FirstOrDefault().DNgayQuyetDinh.GetValueOrDefault().Date;
                }
            }
            else
            {
                var ngayChungTu = DataDotSelected.ValueItem;
                lstChungTu = Models.Where(n => string.IsNullOrEmpty(n.SSoQuyetDinh) && n.DNgayChungTu.HasValue && n.DNgayChungTu.Value.ToString("dd/MM/yyyy").Equals(ngayChungTu)).ToList();
                if (lstChungTu.Count > 0)
                {
                    ngayLuyKe = lstChungTu.FirstOrDefault().DNgayChungTu.GetValueOrDefault().Date;
                }
            }
            _listChungTuReport.AddRange(lstChungTu);
            // ds chung tu luy ke
            if (CheckPrintAccumulation)
            {
                var predicate = PredicateBuilder.True<NsDtChungTu>();
                predicate = predicate.And(x => x.INamLamViec == _sessionService.Current.YearOfWork);
                predicate = predicate.And(x => x.INamNganSach == _sessionService.Current.YearOfBudget);
                predicate = predicate.And(x => x.IIdMaNguonNganSach == _sessionService.Current.Budget);
                predicate = predicate.And(x => x.ILoai == SoChungTuType.EstimateDivision);
                predicate = predicate.And(x => (x.DNgayQuyetDinh == null && x.DNgayChungTu != null && x.DNgayChungTu.Value.Date <= ngayLuyKe) || (x.DNgayQuyetDinh != null && x.DNgayQuyetDinh.Value.Date <= ngayLuyKe));
                var lstCtLuyKe = _dtChungTuService.FindByCondition(predicate).ToList();
                if (lstCtLuyKe.Count > 0)
                {
                    _listChungTuReport.AddRange(_mapper.Map<List<DtChungTuModel>>(lstCtLuyKe));
                }
            }
        }

        private void OnConfigSign()
        {
            DmChuKyModel chuKyModel = new DmChuKyModel();
            _dmChuKy = _dmChuKyService.FindByCondition(x => x.IdType.Equals(TypeChuKy.RPT_NS_DUTOAN_TONGHOP_PHANBO) && x.ITrangThai == StatusType.ACTIVE).FirstOrDefault();
            if (_dmChuKy == null)
                chuKyModel.IdType = TypeChuKy.RPT_NS_DUTOAN_TONGHOP_PHANBO;
            else
                chuKyModel = _mapper.Map<DmChuKyModel>(_dmChuKy);
            DmChuKyDialogViewModel.DmChuKyModel = chuKyModel;
            DmChuKyDialogViewModel.SavedAction = obj =>
            {
                LoadTieuDe();
            };
            DmChuKyDialogViewModel.Init();
            DmChuKyDialogViewModel.ShowDialog();
        }
    }
}
