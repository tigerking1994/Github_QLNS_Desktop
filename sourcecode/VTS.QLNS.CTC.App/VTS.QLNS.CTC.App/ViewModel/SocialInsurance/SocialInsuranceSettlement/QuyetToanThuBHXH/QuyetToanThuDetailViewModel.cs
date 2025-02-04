using AutoMapper;
using ControlzEx.Standard;
using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using VTS.QLNS.CTC.App.Command;
using VTS.QLNS.CTC.App.Helper;
using VTS.QLNS.CTC.App.Model;
using VTS.QLNS.CTC.App.Model.Control;
using VTS.QLNS.CTC.App.Properties;
using VTS.QLNS.CTC.App.Service;
using VTS.QLNS.CTC.App.Service.Impl;
using VTS.QLNS.CTC.App.View.SocialInsurance.SocialInsuranceSettlement.QuyetToanThuBHXH.Explanation;
using VTS.QLNS.CTC.App.ViewModel.SocialInsurance.SocialInsuranceSettlement.QuyetToanThuBHXH.Explanation;
using VTS.QLNS.CTC.App.ViewModel.SocialInsurance.SocialInsuranceSettlement.QuyetToanThuBHXH.PrintReport;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Criteria;
using VTS.QLNS.CTC.Core.Service;
using VTS.QLNS.CTC.Utility;
using VTS.QLNS.CTC.Utility.Enum;

namespace VTS.QLNS.CTC.App.ViewModel.SocialInsurance.SocialInsuranceSettlement.QuyetToanThuBHXH
{
    public class QuyetToanThuDetailViewModel : DetailViewModelBase<BhQttBHXHModel, BhQttBHXHChiTietModel>
    {
        private readonly ISessionService _sessionService;
        private readonly IQttBHXHChiTietService _chungTuChiTietService;
        private readonly IBhDmMucLucNganSachService _bhDmMucLucNganSachService;
        private readonly INsQtChungTuChiTietService _nsQtChungTuChiTietService;
        private readonly IQttBHXHService _chungTuService;
        private readonly ISysAuditLogService _log;
        private SessionInfo _sessionInfo;
        private readonly IMapper _mapper;
        private readonly IBhDmCauHinhThamSoService _bhDmCauHinhThamSoService;
        private readonly INsDonViService _donViService;

        private bool _isInBudget = default;
        public bool IsInBudget
        {
            get => _isInBudget;
            set => SetProperty(ref _isInBudget, value);
        }

        public delegate void DataChangedEventHandler(object sender, EventArgs e);
        public event DataChangedEventHandler UpdateParentWindowEventHandler;
        private QuyetToanThuGiaiThichBangLoiViewModel QuyetToanThuGiaiThichBangLoiViewModel;
        private QuyetToanThuGiaiThichSoLieuViewModel QuyetToanThuGiaiThichSoLieuViewModel;
        public PrintThongTriQuyetToanViewModel PrintThongTriQuyetToanViewModel { get;}
        private ICollectionView _qttBHXHChiTietModelsView { get; set; }
        private ICollectionView _searchPopupView { get; set; }
        public List<BhQttBHXHModel> ListIdsVoucherSummary { get; set; }
        private BhQttBHXHModel _ctTongHop;
        public BhQttBHXHModel CtTongHop
        {
            get => _ctTongHop;
            set => SetProperty(ref _ctTongHop, value);
        }

        private string _searchText;
        public string SearchText
        {
            get => _searchText;
            set
            {
                SetProperty(ref _searchText, value);
                if (!string.IsNullOrEmpty(_searchText))
                {
                    SearchDataParent();
                }
            }
        }

        private string _selectedMonths;
        public string SelectedMonths
        {
            get => _selectedMonths;
            set
            {
                SetProperty(ref _selectedMonths, value);
            }
        }

        private ObservableCollection<ComboboxItem> _typeShowAgencyKHT;
        public ObservableCollection<ComboboxItem> TypeShowAgencyKHT
        {
            get => _typeShowAgencyKHT;
            set => SetProperty(ref _typeShowAgencyKHT, value);
        }

        private ComboboxItem _selectedTypeShowAgencyKHT;
        public ComboboxItem SelectedTypeShowAgencyKHT
        {
            get => _selectedTypeShowAgencyKHT;
            set
            {
                if (SetProperty(ref _selectedTypeShowAgencyKHT, value) && IsVoucherSummary)
                {
                    LoadData();
                    OnPropertyChanged(nameof(IsShowAgencyFilter));
                }
            }
        }

        private bool _isLock;
        public bool IsLock
        {
            get => _isLock;
            set
            {
                SetProperty(ref _isLock, value);
                OnPropertyChanged(nameof(IsEnabledDelete));
            }
        }

        public bool IsSaveData => Items.Any(item => item.IsModified || item.IsDeleted)
                                || Items.Any(x => !x.IsHangCha);


        private bool _isPopupOpen;
        public bool IsPopupOpen
        {
            get => _isPopupOpen;
            set => SetProperty(ref _isPopupOpen, value);
        }

        private ObservableCollection<ComboboxItem> _viewSummary = new ObservableCollection<ComboboxItem>();

        public ObservableCollection<ComboboxItem> ViewSummary
        {
            get => _viewSummary;
            set => SetProperty(ref _viewSummary, value);
        }

        private ComboboxItem _viewSummarySelected;

        private ObservableCollection<DonViModel> _donViItems;
        public ObservableCollection<DonViModel> DonViItems
        {
            get => _donViItems;
            set => SetProperty(ref _donViItems, value);
        }

        private DonViModel _selectedDonVi;
        public DonViModel SelectedDonVi
        {
            get => _selectedDonVi;
            set
            {
                SetProperty(ref _selectedDonVi, value);
                LoadData();
            }
        }

        private string _sNoiDungSearch;
        public string SNoiDungSearch
        {
            get => _sNoiDungSearch;
            set => SetProperty(ref _sNoiDungSearch, value);
        }

        private ObservableCollection<BhQttBHXHChiTietModel> _dataPopupSearchItems;
        public ObservableCollection<BhQttBHXHChiTietModel> DataPopupSearchItems
        {
            get => _dataPopupSearchItems;
            set => SetProperty(ref _dataPopupSearchItems, value);
        }

        private BhQttBHXHChiTietModel _selectedPopupItem;
        public BhQttBHXHChiTietModel SelectedPopupItem
        {
            get => _selectedPopupItem;
            set
            {
                SetProperty(ref _selectedPopupItem, value);
                SNoiDungSearch = _selectedPopupItem?.STenBhMLNS;
                OnPropertyChanged(nameof(SNoiDungSearch));
                IsPopupOpen = false;
            }
        }

        private ObservableCollection<BhQttBHXHChiTietModel> _dataSearch;
        public ObservableCollection<BhQttBHXHChiTietModel> DataSearch
        {
            get => _dataSearch;
            set => SetProperty(ref _dataSearch, value);
        }

        public bool IsFilterDonVi => _viewSummarySelected != null &&
                                     _viewSummarySelected.ValueItem.Equals(TypeViewSummary.Detail.ToString());

        public bool IsVoucherSummary { get; set; }
        //public bool IsShowAgencyFilter => IsVoucherSummary && _selectedTypeShowAgencyKHT != null && _selectedTypeShowAgencyKHT.ValueItem == TypeDisplay.CHITIET_DONVI;
        public bool IsShowAgencyFilter => (CheckParentUnit() && Model.ILoaiTongHop == 1) || (IsVoucherSummary && _selectedTypeShowAgencyKHT != null && _selectedTypeShowAgencyKHT.ValueItem == TypeDisplay.CHITIET_DONVI);
        public bool IsVisibilityGetSalaryBudget => !IsVoucherSummary;
        public bool IsIsEnableGetSalaryBudget => !Model.BIsKhoa;
        public bool IsShowNumberExplanation => Model.IQuyNamLoai == 2 ? true : false;

        private List<ComboboxItem> _agencies;
        public List<ComboboxItem> Agencies
        {
            get => _agencies;
            set => SetProperty(ref _agencies, value);
        }

        private ComboboxItem _selectedAgency;
        public ComboboxItem SelectedAgency
        {
            get => _selectedAgency;
            set
            {
                SetProperty(ref _selectedAgency, value);
                _qttBHXHChiTietModelsView.Refresh();
                CalculateData();
            }
        }

        public bool IsAnotherUserCreate { get; set; }
        public bool IsEnabledDelete => !IsLock && SelectedItem != null;
        public bool IsDeleteAll => !IsLock && Items.Any(item => !item.IsModified);
        public int NamLamViec { get; set; }
        public RelayCommand SearchCommand { get; }
        public RelayCommand ClearSearchCommand { get; }
        public new RelayCommand SaveCommand { get; }
        public new RelayCommand CloseCommand { get; }
        public RelayCommand PrintCommand { get; }
        public RelayCommand CanCuCommand { get; }
        public RelayCommand LaySummaryCanCuCommand { get; }
        public RelayCommand GiaiThichBangLoiCommand { get; }
        public RelayCommand GiaiThichSoLieuCommand { get; }
        public RelayCommand GetBaseSalaryCommand { get; }
        public RelayCommand GetBaseBudgetCommand { get; }
        public bool IsInit { get; set; }

        public DateTime DtNow => DateTime.Now;

        public QuyetToanThuDetailViewModel(
            IQttBHXHChiTietService qttBHXHChiTietService,
            IQttBHXHService qttBHXHService,
            ISessionService sessionService,
            IMapper mapper,
            ISysAuditLogService log,
            IBhDmMucLucNganSachService bhDmMucLucNganSachService,
            INsQtChungTuChiTietService nsQtChungTuChiTietService,
            INsDonViService nsDonViService,
            QuyetToanThuGiaiThichSoLieuViewModel quyetToanThuGiaiThichSoLieuViewModel,
            QuyetToanThuGiaiThichBangLoiViewModel quyetToanThuGiaiThichBangLoiViewModel,
            IBhDmCauHinhThamSoService bhDmCauHinhThamSoService,
            PrintThongTriQuyetToanViewModel printThongTriQuyetToanViewModel,
            INsDonViService donViService
            )
        {
            _chungTuChiTietService = qttBHXHChiTietService;
            _chungTuService = qttBHXHService;
            _sessionService = sessionService;
            _bhDmMucLucNganSachService = bhDmMucLucNganSachService;
            _nsQtChungTuChiTietService = nsQtChungTuChiTietService;
            _bhDmCauHinhThamSoService = bhDmCauHinhThamSoService;
            _log = log;
            _mapper = mapper;
            QuyetToanThuGiaiThichBangLoiViewModel = quyetToanThuGiaiThichBangLoiViewModel;
            QuyetToanThuGiaiThichSoLieuViewModel = quyetToanThuGiaiThichSoLieuViewModel;
            PrintThongTriQuyetToanViewModel = printThongTriQuyetToanViewModel;
            _donViService = donViService;
            ClearSearchCommand = new RelayCommand(OnClearSearch);
            SaveCommand = new RelayCommand(o => OnSave());
            SearchCommand = new RelayCommand(obj => OnSearch(obj));
            CloseCommand = new RelayCommand(OnClose);
            PrintCommand = new RelayCommand(OnPrint);
            GiaiThichBangLoiCommand = new RelayCommand(obj => OnOpenVerbalExplanationDialog());
            GiaiThichSoLieuCommand = new RelayCommand(obj => OnOpenDataInterpretationDialog());
            RefreshCommand = new RelayCommand(_ => OnRefresh());
            GetBaseSalaryCommand = new RelayCommand(obj => OnGetBaseSalary());
            GetBaseBudgetCommand = new RelayCommand(obj => OnGetBaseBudget());
        }

        public override void Init()
        {
            base.Init();
            _sessionInfo = _sessionService.Current;
            NamLamViec = _sessionService.Current.YearOfWork;
            if (Model != null)
            {
                IsLock = Model.BIsKhoa;
                IsAnotherUserCreate = Model.SNguoiTao != _sessionInfo.Principal;
            }
            IsInit = true;
            LoadComboboxTypeShow();
            LoadData();
            IsInit = false;
            LoadDefault();
        }

        private void LoadComboboxTypeShow()
        {
            TypeShowAgencyKHT = new ObservableCollection<ComboboxItem>();
            TypeShowAgencyKHT.Add(new ComboboxItem { ValueItem = TypeDisplay.TONG_DONVI, DisplayItem = TypeDisplay.TONG_DONVI });
            TypeShowAgencyKHT.Add(new ComboboxItem { ValueItem = TypeDisplay.CHITIET_DONVI, DisplayItem = TypeDisplay.CHITIET_DONVI });
            _selectedTypeShowAgencyKHT = TypeShowAgencyKHT.FirstOrDefault();
            OnPropertyChanged(nameof(SelectedTypeShowAgencyKHT));
        }

        public override void OnClose(object o)
        {
            ((Window)o).Close();
            UpdateParentWindowEventHandler?.Invoke(Model, new EventArgs());
        }

        protected override void OnDelete()
        {
            if (SelectedItem == null || IsLock) return;
            if (Model.SNguoiTao != _sessionInfo.Principal)
            {
                MessageBoxHelper.Warning(string.Format(Resources.MsgRoleDelete, Model.SNguoiTao));
                return;
            }
            SelectedItem.IsDeleted = !SelectedItem.IsDeleted;
            OnPropertyChanged(nameof(IsSaveData));
            OnPropertyChanged(nameof(IsDeleteAll));
        }

        public override void OnSave()
        {
            if (!IsSaveData)
            {
                return;
            }
            Func<BhQttBHXHChiTietModel, bool> isAdd = x => x.IsModified && !x.IsDeleted && x.IsAdd && !x.IsHangCha;
            Func<BhQttBHXHChiTietModel, bool> isUpdate = x => x.IsModified && !x.IsDeleted && !x.IsAdd && !x.IsHangCha;
            Func<BhQttBHXHChiTietModel, bool> isDelete = x => x.IsDeleted && !x.IsHangCha;

            var detailsAdd = Items.Where(isAdd).ToList();
            var detailsUpdate = Items.Where(isUpdate).ToList();
            var detailsDelete = Items.Where(isDelete).ToList();

            //thêm mới chứng từ chi tiết
            if (detailsAdd.Count > 0)
            {
                var addItems = new List<BhQttBHXHChiTiet>();
                _mapper.Map(detailsAdd, addItems);
                foreach (var item in addItems)
                {
                    item.FLuongChinh = Math.Round(item.FLuongChinh.GetValueOrDefault(), MidpointRounding.AwayFromZero);
                    item.FPhuCapChucVu = Math.Round(item.FPhuCapChucVu.GetValueOrDefault(), MidpointRounding.AwayFromZero);
                    item.FPCTNNghe = Math.Round(item.FPCTNNghe.GetValueOrDefault(), MidpointRounding.AwayFromZero);
                    item.FPCTNVuotKhung = Math.Round(item.FPCTNVuotKhung.GetValueOrDefault(), MidpointRounding.AwayFromZero);
                    item.FNghiOm = Math.Round(item.FNghiOm.GetValueOrDefault(), MidpointRounding.AwayFromZero);
                    item.FHSBL = Math.Round(item.FHSBL.GetValueOrDefault(), MidpointRounding.AwayFromZero);
                    item.FDuToan = Math.Round(item.FDuToan.GetValueOrDefault(), MidpointRounding.AwayFromZero);
                    item.FDaQuyetToan = Math.Round(item.FDaQuyetToan.GetValueOrDefault(), MidpointRounding.AwayFromZero);
                    item.FConLai = Math.Round(item.FConLai.GetValueOrDefault(), MidpointRounding.AwayFromZero);
                    item.FThuBHXHNLD = Math.Round(item.FThuBHXHNLD.GetValueOrDefault(), MidpointRounding.AwayFromZero);
                    item.FThuBHXHNSD = Math.Round(item.FThuBHXHNSD.GetValueOrDefault(), MidpointRounding.AwayFromZero);
                    item.FThuBHYTNLD = Math.Round(item.FThuBHYTNLD.GetValueOrDefault(), MidpointRounding.AwayFromZero);
                    item.FThuBHYTNSD = Math.Round(item.FThuBHYTNSD.GetValueOrDefault(), MidpointRounding.AwayFromZero);
                    item.FThuBHTNNLD = Math.Round(item.FThuBHTNNLD.GetValueOrDefault(), MidpointRounding.AwayFromZero);
                    item.FThuBHTNNSD = Math.Round(item.FThuBHTNNSD.GetValueOrDefault(), MidpointRounding.AwayFromZero);
                    item.FTongSoPhaiThuBHXH = Math.Round(item.FTongSoPhaiThuBHXH.GetValueOrDefault(), MidpointRounding.AwayFromZero);
                    item.FTongSoPhaiThuBHYT = Math.Round(item.FTongSoPhaiThuBHYT.GetValueOrDefault(), MidpointRounding.AwayFromZero);
                    item.FTongSoPhaiThuBHTN = Math.Round(item.FTongSoPhaiThuBHTN.GetValueOrDefault(), MidpointRounding.AwayFromZero);
                    item.FTongCong = Math.Round(item.FTongCong.GetValueOrDefault());
                }

                _chungTuChiTietService.AddRange(addItems);
                Items.Where(isAdd).Select(x =>
                {
                    x.IsModified = false;
                    x.IsAdd = false;
                    return x;
                }).ToList();
            }

            //cập nhật chứng từ chi tiết
            if (detailsUpdate.Count > 0)
            {
                foreach (var updateItem in detailsUpdate)
                {
                    var chungTuChiTiet = _chungTuChiTietService.FindById(updateItem.Id);
                    _mapper.Map(updateItem, chungTuChiTiet);

                    chungTuChiTiet.FLuongChinh = Math.Round(chungTuChiTiet.FLuongChinh.GetValueOrDefault(), MidpointRounding.AwayFromZero);
                    chungTuChiTiet.FPhuCapChucVu = Math.Round(chungTuChiTiet.FPhuCapChucVu.GetValueOrDefault(), MidpointRounding.AwayFromZero);
                    chungTuChiTiet.FPCTNNghe = Math.Round(chungTuChiTiet.FPCTNNghe.GetValueOrDefault(), MidpointRounding.AwayFromZero);
                    chungTuChiTiet.FPCTNVuotKhung = Math.Round(chungTuChiTiet.FPCTNVuotKhung.GetValueOrDefault(), MidpointRounding.AwayFromZero);
                    chungTuChiTiet.FNghiOm = Math.Round(chungTuChiTiet.FNghiOm.GetValueOrDefault(), MidpointRounding.AwayFromZero);
                    chungTuChiTiet.FHSBL = Math.Round(chungTuChiTiet.FHSBL.GetValueOrDefault(), MidpointRounding.AwayFromZero);
                    chungTuChiTiet.FDuToan = Math.Round(chungTuChiTiet.FDuToan.GetValueOrDefault(), MidpointRounding.AwayFromZero);
                    chungTuChiTiet.FDaQuyetToan = Math.Round(chungTuChiTiet.FDaQuyetToan.GetValueOrDefault(), MidpointRounding.AwayFromZero);
                    chungTuChiTiet.FConLai = Math.Round(chungTuChiTiet.FConLai.GetValueOrDefault(), MidpointRounding.AwayFromZero);
                    chungTuChiTiet.FThuBHXHNLD = Math.Round(chungTuChiTiet.FThuBHXHNLD.GetValueOrDefault(), MidpointRounding.AwayFromZero);
                    chungTuChiTiet.FThuBHXHNSD = Math.Round(chungTuChiTiet.FThuBHXHNSD.GetValueOrDefault(), MidpointRounding.AwayFromZero);
                    chungTuChiTiet.FThuBHYTNLD = Math.Round(chungTuChiTiet.FThuBHYTNLD.GetValueOrDefault(), MidpointRounding.AwayFromZero);
                    chungTuChiTiet.FThuBHYTNSD = Math.Round(chungTuChiTiet.FThuBHYTNSD.GetValueOrDefault(), MidpointRounding.AwayFromZero);
                    chungTuChiTiet.FThuBHTNNLD = Math.Round(chungTuChiTiet.FThuBHTNNLD.GetValueOrDefault(), MidpointRounding.AwayFromZero);
                    chungTuChiTiet.FThuBHTNNSD = Math.Round(chungTuChiTiet.FThuBHTNNSD.GetValueOrDefault(), MidpointRounding.AwayFromZero);
                    chungTuChiTiet.FTongSoPhaiThuBHXH = Math.Round(chungTuChiTiet.FTongSoPhaiThuBHXH.GetValueOrDefault(), MidpointRounding.AwayFromZero);
                    chungTuChiTiet.FTongSoPhaiThuBHYT = Math.Round(chungTuChiTiet.FTongSoPhaiThuBHYT.GetValueOrDefault(), MidpointRounding.AwayFromZero);
                    chungTuChiTiet.FTongSoPhaiThuBHTN = Math.Round(chungTuChiTiet.FTongSoPhaiThuBHTN.GetValueOrDefault(), MidpointRounding.AwayFromZero);
                    chungTuChiTiet.FTongCong = Math.Round(chungTuChiTiet.FTongCong.GetValueOrDefault());

                    _chungTuChiTietService.Update(chungTuChiTiet);
                    updateItem.IsModified = false;
                }
            }
            //cập nhật tổng cộng chứng từ
            var qttChungTu = _chungTuService.FindById(Model.Id);
            qttChungTu.IQSBQNam = Model.IQSBQNam;
            qttChungTu.FLuongChinh = Model.FLuongChinh;
            qttChungTu.FPhuCapChucVu = Model.FPhuCapChucVu;
            qttChungTu.FPCTNNghe = Model.FPCTNNghe;
            qttChungTu.FPCTNVuotKhung = Model.FPCTNVuotKhung;
            qttChungTu.FNghiOm = Model.FNghiOm;
            qttChungTu.FHSBL = Model.FHSBL;
            qttChungTu.FTongQuyTienLuongNam = Model.FTongQuyTienLuongNam;
            qttChungTu.FDuToan = Model.FDuToan;
            qttChungTu.FDaQuyetToan = Model.FDaQuyetToan;
            qttChungTu.FConLai = Model.FConLai;
            qttChungTu.FThuBHXHNLD = Model.FThuBHXHNLD;
            qttChungTu.FThuBHXHNSD = Model.FThuBHXHNSD;
            qttChungTu.FTongSoPhaiThuBHXH = Model.FTongSoPhaiThuBHXH;
            qttChungTu.FThuBHYTNLD = Model.FThuBHYTNLD;
            qttChungTu.FThuBHYTNSD = Model.FThuBHYTNSD;
            qttChungTu.FTongSoPhaiThuBHYT = Model.FTongSoPhaiThuBHYT;
            qttChungTu.FThuBHTNNLD = Model.FThuBHTNNLD;
            qttChungTu.FThuBHTNNSD = Model.FThuBHTNNSD;
            qttChungTu.FTongSoPhaiThuBHTN = Model.FTongSoPhaiThuBHTN;
            qttChungTu.FTongCong = Model.FTongCong;

            _chungTuService.Update(qttChungTu);
            OnRefresh();
            _log.WriteLog(Resources.ApplicationName, "Quyết toán thu BHXH - chứng từ chi tiết", (int)TypeExecute.Adjust, DtNow, TransactionStatus.Success, _sessionService.Current.Principal);
            OnPropertyChanged(nameof(IsSaveData));
            OnPropertyChanged(nameof(IsDeleteAll));
            UpdateParentWindowEventHandler?.Invoke(Model, new EventArgs());
            var message = Resources.MsgSaveDone;
            MessageBoxHelper.Info(message);
        }
        private void SearchDataParent()
        {
            if (_qttBHXHChiTietModelsView != null)
            {
                _qttBHXHChiTietModelsView.Refresh();
            }
        }
        protected override void OnRefresh()
        {
            IsInit = true;
            SearchText = string.Empty;
            LoadData();
            IsInit = false;
        }

        private void LoadAgencies(string agencyIds)
        {
            var listDonVi = _donViService.FindByListIdDonVi(agencyIds, _sessionInfo.YearOfWork);
            _agencies = _mapper.Map<List<ComboboxItem>>(listDonVi);
            OnPropertyChanged(nameof(Agencies));
        }

        public override void LoadData(params object[] args)
        {
            BhQttBHXHChiTietCriteria searchCondition = new BhQttBHXHChiTietCriteria();
            LoadMonthOfQuarter();
            searchCondition.INamLamViec = _sessionInfo.YearOfWork;
            searchCondition.IIDMaDonVi = Model.IIDMaDonVi;
            searchCondition.IdQttBhxh = Model.Id;
            searchCondition.SLns = Model.sDSMLNS;
            searchCondition.IQuyNamLoai = Model.IQuyNamLoai;
            searchCondition.Months = SelectedMonths;
            searchCondition.ILoai = Model.ILoai;
            searchCondition.IIDMaDonViCha = Model.IIDMaDonVi;

            if (Model.IQuyNamLoai == 2)
            {
                searchCondition.IsNam = true;
            }

            searchCondition.IsDonViCha = CheckParentUnit();
            if (_selectedDonVi != null)
            {
                searchCondition.IdDonViFilter = _selectedDonVi.IIDMaDonVi;
                searchCondition.IIDMaDonVi = _selectedDonVi.IIDMaDonVi;
            }

            var temp = new List<BhQttBHXHChiTiet>();
            if (IsVoucherSummary && SelectedTypeShowAgencyKHT != null && SelectedTypeShowAgencyKHT.ValueItem == TypeDisplay.CHITIET_DONVI)
            {
                var voucherNos = Model.STongHop.Split(",").ToList();
                List<BhQttBHXH> listChungTu = _chungTuService.FindByCondition(x => x.INamLamViec == _sessionInfo.YearOfWork && voucherNos.Contains(x.SSoChungTu) && x.BDaTongHop.GetValueOrDefault(false)).ToList();
                //string agencyIds = string.Join(",", listChungTu.Select(x => x.IIDMaDonVi));
                //var listDonVi = _donViService.FindByListIdDonVi(agencyIds, _sessionInfo.YearOfWork);

                List<BhQttBHXHChiTiet> listChungTuChiTietParent = new List<BhQttBHXHChiTiet>();
                List<BhQttBHXHChiTiet> listChungTuChiTietChildren = new List<BhQttBHXHChiTiet>();
                foreach (var chungTu in listChungTu)
                {
                    searchCondition.IdQttBhxh = chungTu.Id;
                    searchCondition.IIDMaDonVi = chungTu.IIDMaDonVi;
                    searchCondition.IsDonViCha = false;
                    var listQuery = _chungTuChiTietService.FindVoucherDetailByCondition(searchCondition).ToList();
                    //listQuery.Where(x => !x.IsHangCha).Select(x => x.STenDonVi = listDonVi.FirstOrDefault(y => y.IIDMaDonVi == chungTu.IIDMaDonVi).TenDonVi).ToList();
                    listChungTuChiTietParent.AddRange(listQuery.Where(x => x.IsHangCha));
                    listChungTuChiTietChildren.AddRange(listQuery.Where(x => !x.IsHangCha));
                }

                string agencyIds = string.Join(",", listChungTuChiTietChildren.Select(x => x.IIDMaDonVi).Distinct());
                var listDonVi = _donViService.FindByListIdDonVi(agencyIds, _sessionInfo.YearOfWork);

                var listXauNoiMa = listChungTuChiTietChildren.Select(x => x.SXauNoiMa).Distinct().ToList();
                listChungTuChiTietParent = listChungTuChiTietParent.Where(x => listXauNoiMa.Any(y => y.Contains(x.SXauNoiMa))).GroupBy(x => x.SXauNoiMa).Select(x => x.First()).Distinct().ToList();
                listChungTuChiTietChildren.Where(x => !string.IsNullOrEmpty(x.IIDMaDonVi) && !x.IsHangCha).Select(x => x.STenDonVi = listDonVi.FirstOrDefault(y => y.IIDMaDonVi == x.IIDMaDonVi).TenDonVi).ToList();
                temp.AddRange(listChungTuChiTietParent);
                temp.AddRange(listChungTuChiTietChildren);
                temp = temp.OrderBy(x => x.SXauNoiMa).ThenBy(x => x.IIDMaDonVi).ToList();

                //LoadAgencies(agencyIds);
                _agencies = _mapper.Map<List<ComboboxItem>>(listDonVi);
                OnPropertyChanged(nameof(Agencies));
            }
            else
            {
                if (CheckParentUnit() && !IsVoucherSummary && Model.ILoai == (int)SettlementReportTypeNum.Detail)
                {
                    var selectedMonth = Model.IQuyNam.ToString();
                    var listDonVi = _donViService.FindByUserCreateVoucher(_sessionInfo.Principal, _sessionInfo.YearOfWork, LoaiDonVi.NOI_BO);
                    var lstCurrentUnit = _chungTuChiTietService.FindListDonViExistSettlement(Model.Id, _sessionInfo.YearOfWork, _sessionService.Current.Principal, int.Parse(selectedMonth), Model.IQuyNamLoai);
                    listDonVi = listDonVi.Where(y => !lstCurrentUnit.Contains(y.IIDMaDonVi)).ToList();

                    List<BhQttBHXHChiTiet> listChungTuChiTietParent = new List<BhQttBHXHChiTiet>();
                    List<BhQttBHXHChiTiet> listChungTuChiTietChildren = new List<BhQttBHXHChiTiet>();
                    foreach (var idDonVi in listDonVi)
                    {
                        searchCondition.IdQttBhxh = Model.Id;
                        searchCondition.IIDMaDonVi = idDonVi.IIDMaDonVi;
                        searchCondition.IsDonViCha = false;
                        var listQuery = _chungTuChiTietService.FindVoucherDetailByCondition(searchCondition).ToList();
                        listQuery.Where(x => !x.IsHangCha).Select(x => x.STenDonVi = listDonVi.FirstOrDefault(y => y.IIDMaDonVi == idDonVi.IIDMaDonVi).TenDonVi).ToList();
                        listChungTuChiTietParent.AddRange(listQuery.Where(x => x.IsHangCha));
                        listChungTuChiTietChildren.AddRange(listQuery.Where(x => !x.IsHangCha && ((idDonVi.Khoi != "1" && x.SXauNoiMa.StartsWith("9020001")) || (idDonVi.Khoi == "1" && x.SXauNoiMa.StartsWith("9020002")))));
                    }
                    var listXauNoiMa = listChungTuChiTietChildren.Select(x => x.SXauNoiMa).Distinct().ToList();
                    listChungTuChiTietParent = listChungTuChiTietParent.Where(x => listXauNoiMa.Any(y => y.Contains(x.SXauNoiMa))).GroupBy(x => x.SXauNoiMa).Select(x => x.First()).Distinct().ToList();
                    temp.AddRange(listChungTuChiTietParent);
                    temp.AddRange(listChungTuChiTietChildren);
                    temp = temp.OrderBy(x => x.SXauNoiMa).ThenBy(x => x.IIDMaDonVi).ToList();

                    _agencies = _mapper.Map<List<ComboboxItem>>(listDonVi);
                    OnPropertyChanged(nameof(Agencies));
                }
                else
                {
                    temp = _chungTuChiTietService.FindVoucherDetailByCondition(searchCondition).ToList();
                }
            }

            var existBhChiTiet = _chungTuChiTietService.ExistVoucherDetail(Model.Id);
            Items = _mapper.Map<ObservableCollection<BhQttBHXHChiTietModel>>(temp);
            DataPopupSearchItems = _mapper.Map<ObservableCollection<BhQttBHXHChiTietModel>>(temp);
            _qttBHXHChiTietModelsView = CollectionViewSource.GetDefaultView(Items);
            _qttBHXHChiTietModelsView.Filter = BHQttBHXHModelsFilter;
            foreach (var qttItem in Items)
            {
                qttItem.IsFilter = true;
                if (!qttItem.IsHangCha)
                {
                    //if (qttItem.SXauNoiMa == BhxhMLNS.HSQ_BS_HACH_TOAN || qttItem.SXauNoiMa == BhxhMLNS.HSQ_BS_DU_TOAN)
                    //{
                    //    qttItem.IsCancel = true;
                    //}
                    qttItem.PropertyChanged += (sender, args) =>
                    {
                        if (args.PropertyName == nameof(BhKhtBHXHChiTietModel.IQSBQNam))
                        {
                            if (qttItem.SXauNoiMa == BhxhMLNS.HSQ_BS_HACH_TOAN || qttItem.SXauNoiMa == BhxhMLNS.HSQ_BS_DU_TOAN)
                            {
                                double? lcs = 1;
                                var cauHinhLcs = _bhDmCauHinhThamSoService.FindByCondition(x => x.SMa == "LCS" && x.INamLamViec == _sessionService.Current.YearOfWork && x.BTrangThai == true).FirstOrDefault();
                                if (cauHinhLcs != null)
                                {
                                    lcs = cauHinhLcs.fGiaTri ?? 1;
                                    if (Model.IQuyNamLoai == 0)
                                    {
                                        qttItem.FLuongChinh = qttItem.IQSBQNam * lcs;
                                    }
                                    else if (Model.IQuyNamLoai == 1)
                                    {
                                        qttItem.FLuongChinh = qttItem.IQSBQNam * lcs * 3;
                                    }
                                    else if (Model.IQuyNamLoai == 2)
                                    {
                                        qttItem.FLuongChinh = qttItem.IQSBQNam * lcs * 12;
                                    }
                                }
                            }
                        }
                        if (args.PropertyName == nameof(BhKhtBHXHChiTietModel.FLuongChinh) || args.PropertyName == nameof(BhKhtBHXHChiTietModel.FPhuCapChucVu)
                            || args.PropertyName == nameof(BhKhtBHXHChiTietModel.FPCTNNghe) || args.PropertyName == nameof(BhKhtBHXHChiTietModel.FPCTNVuotKhung)
                            || args.PropertyName == nameof(BhKhtBHXHChiTietModel.FNghiOm) || args.PropertyName == nameof(BhKhtBHXHChiTietModel.FHSBL) 
                            || args.PropertyName == nameof(BhKhtBHXHChiTietModel.IQSBQNam) || args.PropertyName == nameof(BhKhtBHXHChiTietModel.SMoTa))
                        {
                            BhQttBHXHChiTietModel item = (BhQttBHXHChiTietModel)sender;
                            item.IsModified = true;
                            CalculateData();
                            qttItem.IsModified = true;
                            OnPropertyChanged(nameof(IsSaveData));
                        }
                    };
                }
            }
            CalculateData();
        }

        private bool BHQttBHXHModelsFilter(object obj)
        {
            bool result = true;
            if (!(obj is BhQttBHXHChiTietModel temp)) return result;
            var item = (BhQttBHXHChiTietModel)obj;
            if (!string.IsNullOrEmpty(SNoiDungSearch))
            {
                result = DataSearch.Any(x => x.IIDMLNS.Equals(item.IIDMLNS));
            }
            if (IsShowAgencyFilter)
            {
                if (SelectedAgency != null)
                {
                    result = item.IsHangCha || (!item.IsHangCha && item.IIDMaDonVi.Equals(SelectedAgency.ValueItem));
                }
            }           

            return result;
        }

        private void CalculateParent(Guid idParent, BhQttBHXHChiTietModel item, Dictionary<Guid, BhQttBHXHChiTietModel> dictByMlns)
        {
            if (!dictByMlns.ContainsKey(idParent))
            {
                return;
            }

            var model = dictByMlns[idParent];
            model.IQSBQNam += item.IQSBQNam.GetValueOrDefault();
            model.FLuongChinh += Math.Round(item.FLuongChinh.GetValueOrDefault(), MidpointRounding.AwayFromZero);
            model.FPhuCapChucVu += Math.Round(item.FPhuCapChucVu.GetValueOrDefault(), MidpointRounding.AwayFromZero);
            model.FPCTNNghe += Math.Round(item.FPCTNNghe.GetValueOrDefault(), MidpointRounding.AwayFromZero);
            model.FPCTNVuotKhung += Math.Round(item.FPCTNVuotKhung.GetValueOrDefault(), MidpointRounding.AwayFromZero);
            model.FNghiOm += Math.Round(item.FNghiOm.GetValueOrDefault(), MidpointRounding.AwayFromZero);
            model.FHSBL += Math.Round(item.FHSBL.GetValueOrDefault(), MidpointRounding.AwayFromZero);
            model.FDuToan += Math.Round(item.FDuToan.GetValueOrDefault(), MidpointRounding.AwayFromZero);
            model.FDaQuyetToan += Math.Round(item.FDaQuyetToan.GetValueOrDefault(), MidpointRounding.AwayFromZero);
            model.FThuBHXHNLD += Math.Round(item.FThuBHXHNLD.GetValueOrDefault(), MidpointRounding.AwayFromZero);
            model.FThuBHXHNSD += Math.Round(item.FThuBHXHNSD.GetValueOrDefault(), MidpointRounding.AwayFromZero);
            model.FThuBHYTNLD += Math.Round(item.FThuBHYTNLD.GetValueOrDefault(), MidpointRounding.AwayFromZero);
            model.FThuBHYTNSD += Math.Round(item.FThuBHYTNSD.GetValueOrDefault(), MidpointRounding.AwayFromZero);
            model.FThuBHTNNLD += Math.Round(item.FThuBHTNNLD.GetValueOrDefault(), MidpointRounding.AwayFromZero);
            model.FThuBHTNNSD += Math.Round(item.FThuBHTNNSD.GetValueOrDefault(), MidpointRounding.AwayFromZero);

            CalculateParent(model.IIDMLNSCha, item, dictByMlns);
        }
        private void CalculateData()
        {
            Items.Where(x => x.IsHangCha)
                .ForAll(x =>
                {
                    x.IQSBQNam = 0;
                    x.FLuongChinh = 0;
                    x.FPhuCapChucVu = 0;
                    x.FPCTNNghe = 0;
                    x.FPCTNVuotKhung = 0;
                    x.FNghiOm = 0;
                    x.FHSBL = 0;
                    x.FDuToan = 0;
                    x.FDaQuyetToan = 0;
                    x.FThuBHXHNLD = 0;
                    x.FThuBHXHNSD = 0;
                    x.FThuBHYTNLD = 0;
                    x.FThuBHYTNSD = 0;
                    x.FThuBHTNNLD = 0;
                    x.FThuBHTNNSD = 0;
                });

            var temp = Items.Where(x => !x.IsHangCha && !x.IsDeleted && x.IsFilter).ToList();
            var dictByMlns = Items.GroupBy(x => x.IIDMLNS).ToDictionary(x => x.Key, x => x.First());
            foreach (var item in temp)
            {
                CalculateParent(item.IIDMLNSCha, item, dictByMlns);
            }

            UpdateTotal();
        }
        private void UpdateTotal()
        {
            Model.IQSBQNam = 0;
            Model.FLuongChinh = 0;
            Model.FPhuCapChucVu = 0;
            Model.FPCTNNghe = 0;
            Model.FPCTNVuotKhung = 0;
            Model.FNghiOm = 0;
            Model.FHSBL = 0;
            Model.FTongQuyTienLuongNam = 0;
            Model.FDuToan = 0;
            Model.FDaQuyetToan = 0;
            Model.FConLai = 0;
            Model.FThuBHXHNLD = 0;
            Model.FThuBHXHNSD = 0;
            Model.FTongSoPhaiThuBHXH = 0;
            Model.FThuBHYTNLD = 0;
            Model.FThuBHYTNSD = 0;
            Model.FTongSoPhaiThuBHYT = 0;
            Model.FThuBHTNNLD = 0;
            Model.FThuBHTNNSD = 0;
            Model.FTongSoPhaiThuBHTN = 0;
            Model.FTongCong = 0;

            //var roots = Items.Where(t => t.IIDMLNSCha.Equals(Guid.Empty)).ToList();

            var leaf = Items.Where(x => !x.IsHangCha).ToList();

            foreach (var item in leaf)
            {
                Model.IQSBQNam += item.IQSBQNam.GetValueOrDefault();
                Model.FLuongChinh += Math.Round(item.FLuongChinh.GetValueOrDefault(), MidpointRounding.AwayFromZero);
                Model.FPhuCapChucVu += Math.Round(item.FPhuCapChucVu.GetValueOrDefault(), MidpointRounding.AwayFromZero);
                Model.FPCTNNghe += Math.Round(item.FPCTNNghe.GetValueOrDefault(), MidpointRounding.AwayFromZero);
                Model.FPCTNVuotKhung += Math.Round(item.FPCTNVuotKhung.GetValueOrDefault(), MidpointRounding.AwayFromZero);
                Model.FNghiOm += Math.Round(item.FNghiOm.GetValueOrDefault(), MidpointRounding.AwayFromZero);
                Model.FHSBL += Math.Round(item.FHSBL.GetValueOrDefault(), MidpointRounding.AwayFromZero);
                Model.FTongQuyTienLuongNam += Math.Round(item.FTongQuyTienLuongNam.GetValueOrDefault(), MidpointRounding.AwayFromZero);
                Model.FDuToan += Math.Round(item.FDuToan.GetValueOrDefault(), MidpointRounding.AwayFromZero);
                Model.FDaQuyetToan += Math.Round(item.FDaQuyetToan.GetValueOrDefault(), MidpointRounding.AwayFromZero);
                Model.FConLai += Math.Round(item.FConLai.GetValueOrDefault(), MidpointRounding.AwayFromZero);
                Model.FThuBHXHNLD += Math.Round(item.FThuBHXHNLD.GetValueOrDefault(), MidpointRounding.AwayFromZero);
                Model.FThuBHXHNSD += Math.Round(item.FThuBHXHNSD.GetValueOrDefault(), MidpointRounding.AwayFromZero);
                Model.FTongSoPhaiThuBHXH += Math.Round(item.FTongSoPhaiThuBHXH.GetValueOrDefault(), MidpointRounding.AwayFromZero);
                Model.FThuBHYTNLD += Math.Round(item.FThuBHYTNLD.GetValueOrDefault(), MidpointRounding.AwayFromZero);
                Model.FThuBHYTNSD += Math.Round(item.FThuBHYTNSD.GetValueOrDefault(), MidpointRounding.AwayFromZero);
                Model.FTongSoPhaiThuBHYT += Math.Round(item.FTongSoPhaiThuBHYT.GetValueOrDefault(), MidpointRounding.AwayFromZero);
                Model.FThuBHTNNLD += Math.Round(item.FThuBHTNNLD.GetValueOrDefault(), MidpointRounding.AwayFromZero);
                Model.FThuBHTNNSD += Math.Round(item.FThuBHTNNSD.GetValueOrDefault(), MidpointRounding.AwayFromZero);
                Model.FTongSoPhaiThuBHTN += Math.Round(item.FTongSoPhaiThuBHTN.GetValueOrDefault(), MidpointRounding.AwayFromZero);
                Model.FTongCong += Math.Round(item.FTongCong.GetValueOrDefault(), MidpointRounding.AwayFromZero);
            }
        }
        protected override void OnSelectedItemChanged()
        {
            base.OnSelectedItemChanged();
            OnPropertyChanged(nameof(IsEnabledDelete));
        }

        private void OnPrint(object param)
        {
            PrintThongTriQuyetToanViewModel.Model = Model;
            PrintThongTriQuyetToanViewModel.Init();
            PrintThongTriQuyetToanViewModel.ShowDialogHost("DetailDialog");
        }

        private void OnOpenVerbalExplanationDialog()
        {
            QuyetToanThuGiaiThichBangLoiViewModel.SettlementVoucher = Model;
            QuyetToanThuGiaiThichBangLoiViewModel.Init();
            var view = new GiaiThichBangLoi { DataContext = QuyetToanThuGiaiThichBangLoiViewModel };
            view.ShowDialog();
        }

        private void OnOpenDataInterpretationDialog()
        {
            QuyetToanThuGiaiThichSoLieuViewModel.SettlementVoucher = Model;
            QuyetToanThuGiaiThichSoLieuViewModel.Init();
            var view = new GiaiThichSoLieu { DataContext = QuyetToanThuGiaiThichSoLieuViewModel };
            view.ShowDialog();
        }

        private bool CheckParentUnit()
        {
            int yearOfWork = _sessionInfo.YearOfWork;
            return _donViService.IsDonViCha(Model.IIDMaDonVi, yearOfWork);
        }

        #region Search

        private void OnSearch(object obj)
        {
            SearchTextFilter();
        }

        private void LoadDefault()
        {
            SNoiDungSearch = string.Empty;
            DataSearch = new ObservableCollection<BhQttBHXHChiTietModel>();
        }

        private void OnClearSearch(object obj)
        {
            LoadDefault();
            _qttBHXHChiTietModelsView.Refresh();
        }

        private void SearchTextFilter()
        {
            if (!string.IsNullOrEmpty(SNoiDungSearch))
            {
                List<string> lstResult = new List<string>();
                List<string> lstParents = new List<string>();
                List<BhQttBHXHChiTietModel> results = new List<BhQttBHXHChiTietModel>();

                List<string> lstSXaNoiMaChildSearch = Items.Where(x => x.STenBhMLNS.ToLower().Contains(SNoiDungSearch.ToLower()) && !x.IsHangCha).Select(s => s.SXauNoiMa).Distinct().ToList();
                List<string> lstSXaNoiMaParentSearch = Items.Where(x => x.STenBhMLNS.ToLower().Contains(SNoiDungSearch.ToLower()) && x.IsHangCha).Select(s => s.SXauNoiMa).Distinct().ToList();
                if (!lstSXaNoiMaChildSearch.IsEmpty())
                {
                    lstParents.AddRange(StringUtils.GetListKyHieuParent(lstSXaNoiMaChildSearch));
                    if (lstParents.Any(x => x.Count() >= 3))
                    {
                        lstParents.Add(lstParents.FirstOrDefault(x => x.Count() >= 3).Substring(0, 1));
                        lstParents.Add(lstParents.FirstOrDefault(x => x.Count() >= 3).Substring(0, 3));
                    }
                    results = Items.Where(x => lstParents.Contains(x.SXauNoiMa)).ToList();
                }
                if (!lstSXaNoiMaParentSearch.IsEmpty())
                {
                    if (results.IsEmpty())
                        results = GetDataParent(lstSXaNoiMaParentSearch);
                    else
                        results.AddRange(GetDataParent(lstSXaNoiMaParentSearch.Where(x => !lstParents.Contains(x)).ToList()));
                }
                DataSearch = new ObservableCollection<BhQttBHXHChiTietModel>(results);
            }
            else
            {
                DataSearch = new ObservableCollection<BhQttBHXHChiTietModel>();
            }
            _qttBHXHChiTietModelsView.Refresh();
        }

        private List<BhQttBHXHChiTietModel> GetDataParent(List<string> lstInput)
        {
            List<BhQttBHXHChiTietModel> result = new List<BhQttBHXHChiTietModel>();
            List<string> lstParent = StringUtils.GetListKyHieuParent(lstInput);
            if (!lstParent.IsEmpty() && lstParent.Any(x => x.Count() >= 3))
            {
                lstParent.Add(lstParent.FirstOrDefault(x => x.Count() >= 3).Substring(0, 1));
                lstParent.Add(lstParent.FirstOrDefault(x => x.Count() >= 3).Substring(0, 3));
            }
            var lstData = Items.Where(x => lstParent.Contains(x.SXauNoiMa)).ToList();
            result.AddRange(lstData);
            GetListChild(lstData.Where(x => lstInput.Contains(x.SXauNoiMa)).ToList(), result);
            return result;
        }

        private void GetListChild(List<BhQttBHXHChiTietModel> lstInput, List<BhQttBHXHChiTietModel> results)
        {
            var itemChild = Items.Where(x => lstInput.Select(x => x.IIDMLNS).Distinct().Contains(x.IIDMLNSCha)).ToList();
            if (!itemChild.IsEmpty())
            {
                results.AddRange(itemChild);
                foreach (var item in itemChild.Where(x => Items.Select(y => y.IIDMLNSCha).Distinct().Contains(x.IIDMLNS)))
                {
                    GetListChild(new List<BhQttBHXHChiTietModel>() { item }, results);
                }
            }
        }

        #endregion

        private void LoadMonthOfQuarter()
        {
            switch (Model.IQuyNam)
            {
                case 3:
                    SelectedMonths = MonthOfQuarter.QUY1;
                    break;
                case 6:
                    SelectedMonths = MonthOfQuarter.QUY2;
                    break;
                case 9:
                    SelectedMonths = MonthOfQuarter.QUY3;
                    break;
                case 12:
                    SelectedMonths = MonthOfQuarter.QUY4;
                    break;
            }
        }

        private void OnGetBaseSalary()
        {
            var selectedMonth = (Model.IQuyNamLoai == 0 || Model.IQuyNamLoai == 2) ? Model.IQuyNam.ToString() : SelectedMonths;
            var salaryData = _chungTuChiTietService.GetDataLuongCanCu(Model.IIDMaDonVi, _sessionInfo.YearOfWork, selectedMonth, Model.IQuyNamLoai);
            if (salaryData is object && salaryData.Any())
            {
                var itemFilter = Items.Where(x => !x.IsHangCha && !string.IsNullOrEmpty(x.SXauNoiMa));
                if (salaryData.FirstOrDefault().LoaiKhoi == "1")
                {
                    itemFilter = itemFilter.Where(x => x.SXauNoiMa.StartsWith(BhxhMLNS.KHOI_HACH_TOAN));
                }    
                else
                {
                    itemFilter = itemFilter.Where(x => x.SXauNoiMa.StartsWith(BhxhMLNS.KHOI_DU_TOAN));
                }

                Parallel.ForEach(itemFilter, item =>
                {
                    item.IQSBQNam = salaryData.Where(x => !string.IsNullOrEmpty(item.SNsLuongChinh) && item.SNsLuongChinh.Contains(x.SXauNoiMa)).Sum(x => x.IQSBQNam.GetValueOrDefault());
                    if (item.SXauNoiMa != BhxhMLNS.HSQ_BS_DU_TOAN && item.SXauNoiMa != BhxhMLNS.HSQ_BS_HACH_TOAN)
                    {
                        item.FLuongChinh = (item.SXauNoiMa.StartsWith(BhxhMLNS.PHUNHAN_PHUQUAN_DU_TOAN) || item.SXauNoiMa.StartsWith(BhxhMLNS.TUYVIEN_QP_DU_TOAN) || item.SXauNoiMa.StartsWith(BhxhMLNS.PHUNHAN_PHUQUAN_HACH_TOAN)) ? salaryData.Where(x => !string.IsNullOrEmpty(item.SNsLuongChinh) && item.SNsLuongChinh.Contains(x.SXauNoiMa)).Sum(x => x.FGiaTriLuongChinh.GetValueOrDefault()) * item.FHeSoLayQuyLuong : salaryData.Where(x => !string.IsNullOrEmpty(item.SNsLuongChinh) && item.SNsLuongChinh.Contains(x.SXauNoiMa)).Sum(x => x.FGiaTriLuongChinh.GetValueOrDefault());
                    }
                    item.FPhuCapChucVu = (item.SXauNoiMa.StartsWith(BhxhMLNS.PHUNHAN_PHUQUAN_DU_TOAN) || item.SXauNoiMa.StartsWith(BhxhMLNS.TUYVIEN_QP_DU_TOAN) || item.SXauNoiMa.StartsWith(BhxhMLNS.PHUNHAN_PHUQUAN_HACH_TOAN)) ? salaryData.Where(x => !string.IsNullOrEmpty(item.SNsPCCV) && item.SNsPCCV.Contains(x.SXauNoiMa)).Sum(x => x.FGiaTriPCCV.GetValueOrDefault()) * item.FHeSoLayQuyLuong : salaryData.Where(x => !string.IsNullOrEmpty(item.SNsPCCV) && item.SNsPCCV.Contains(x.SXauNoiMa)).Sum(x => x.FGiaTriPCCV.GetValueOrDefault());
                    item.FPCTNNghe = (item.SXauNoiMa.StartsWith(BhxhMLNS.PHUNHAN_PHUQUAN_DU_TOAN) || item.SXauNoiMa.StartsWith(BhxhMLNS.TUYVIEN_QP_DU_TOAN) || item.SXauNoiMa.StartsWith(BhxhMLNS.PHUNHAN_PHUQUAN_HACH_TOAN)) ? salaryData.Where(x => !string.IsNullOrEmpty(item.SNsPCTN) && item.SNsPCTN.Contains(x.SXauNoiMa)).Sum(x => x.FGiaTriPCTN.GetValueOrDefault()) * item.FHeSoLayQuyLuong : salaryData.Where(x => !string.IsNullOrEmpty(item.SNsPCTN) && item.SNsPCTN.Contains(x.SXauNoiMa)).Sum(x => x.FGiaTriPCTN.GetValueOrDefault());
                    item.FPCTNVuotKhung = (item.SXauNoiMa.StartsWith(BhxhMLNS.PHUNHAN_PHUQUAN_DU_TOAN) || item.SXauNoiMa.StartsWith(BhxhMLNS.TUYVIEN_QP_DU_TOAN) || item.SXauNoiMa.StartsWith(BhxhMLNS.PHUNHAN_PHUQUAN_HACH_TOAN)) ? salaryData.Where(x => !string.IsNullOrEmpty(item.SNsPCTNVK) && item.SNsPCTNVK.Contains(x.SXauNoiMa)).Sum(x => x.FGiaTriPCTNVK.GetValueOrDefault()) * item.FHeSoLayQuyLuong : salaryData.Where(x => !string.IsNullOrEmpty(item.SNsPCTNVK) && item.SNsPCTNVK.Contains(x.SXauNoiMa)).Sum(x => x.FGiaTriPCTNVK.GetValueOrDefault());
                    item.FHSBL = (item.SXauNoiMa.StartsWith(BhxhMLNS.PHUNHAN_PHUQUAN_DU_TOAN) || item.SXauNoiMa.StartsWith(BhxhMLNS.TUYVIEN_QP_DU_TOAN) || item.SXauNoiMa.StartsWith(BhxhMLNS.PHUNHAN_PHUQUAN_HACH_TOAN)) ? salaryData.Where(x => !string.IsNullOrEmpty(item.SNsHSBL) && item.SNsHSBL.Contains(x.SXauNoiMa)).Sum(x => x.FGiaTriHSBL.GetValueOrDefault()) * item.FHeSoLayQuyLuong : salaryData.Where(x => !string.IsNullOrEmpty(item.SNsHSBL) && item.SNsHSBL.Contains(x.SXauNoiMa)).Sum(x => x.FGiaTriHSBL.GetValueOrDefault());
                    item.FNghiOm = (item.SXauNoiMa.StartsWith(BhxhMLNS.PHUNHAN_PHUQUAN_DU_TOAN) || item.SXauNoiMa.StartsWith(BhxhMLNS.TUYVIEN_QP_DU_TOAN) || item.SXauNoiMa.StartsWith(BhxhMLNS.PHUNHAN_PHUQUAN_HACH_TOAN)) ? salaryData.Where(x => x.SMaCapBac.Equals(item.SMaCapBac)).Select(x => x.FNghiOm.GetValueOrDefault()).FirstOrDefault() * item.FHeSoLayQuyLuong : salaryData.Where(x => x.SMaCapBac.Equals(item.SMaCapBac)).Select(x => x.FNghiOm.GetValueOrDefault()).FirstOrDefault();
                });
                CalculateData();
            }
            else
            {
                MessageBoxHelper.Info(Resources.MsgBaseSalaryData);
            }
        }

        private void OnGetBaseBudget()
        {
            var selectedMonth = Model.IQuyNam.ToString();
            var donviData = _donViService.FindByListIdDonVi(Model.IIDMaDonVi, _sessionInfo.YearOfWork).ToList();
            //if (CheckParentUnit())
            //{
            //    donviData = _donViService.FindByUserCreateVoucher(_sessionInfo.Principal, _sessionInfo.YearOfWork, LoaiDonVi.NOI_BO).ToList();
            //}
            var lstCurrentUnit = _chungTuChiTietService.FindListDonViExistSettlement(Model.Id, _sessionInfo.YearOfWork, _sessionService.Current.Principal, int.Parse(selectedMonth), Model.IQuyNamLoai);
            donviData = donviData.Where(y => !lstCurrentUnit.Contains(y.IIDMaDonVi)).ToList();
            var mlnsBHXH = _bhDmMucLucNganSachService.FindByCondition(x => x.INamLamViec == _sessionInfo.YearOfWork && (x.SXauNoiMa.StartsWith("9020002") || x.SXauNoiMa.StartsWith("9020001"))
                                        && (!string.IsNullOrEmpty(x.SNS_LuongChinh) || !string.IsNullOrEmpty(x.SNS_PCCV) || !string.IsNullOrEmpty(x.SNS_PCTN) || !string.IsNullOrEmpty(x.SNS_PCTNVK) || !string.IsNullOrEmpty(x.SNS_HSBL)));
            
            var budgetDatas = _chungTuChiTietService.FindDetailsQT(string.Join(",", donviData.Select(x => x.IIDMaDonVi)), _sessionInfo.YearOfWork, _sessionInfo.YearOfBudget, _sessionInfo.Budget, int.Parse(selectedMonth), Model.IQuyNamLoai);
            var bDstring = budgetDatas.Select(x => { string xauNoiMaDonVi = string.Join(StringUtils.DELIMITER, x.SXauNoiMa, x.IIDMaDonVi); return xauNoiMaDonVi; }).Distinct().ToList();

            if (budgetDatas is object && budgetDatas.Any())
            {
                var itemsfilter = Items.Where(x => !x.IsHangCha && bDstring.Contains(string.Join(StringUtils.DELIMITER, x.SXauNoiMa, x.IIDMaDonVi)));
                Parallel.ForEach(itemsfilter, item =>
                {
                    item.IQSBQNam = budgetDatas.Where(x => x.IIDMaDonVi == item.IIDMaDonVi && x.SXauNoiMa == item.SXauNoiMa).Sum(x => x.IQSBQNam);
                    if (item.SXauNoiMa != BhxhMLNS.HSQ_BS_DU_TOAN && item.SXauNoiMa != BhxhMLNS.HSQ_BS_HACH_TOAN)
                    {
                        item.FLuongChinh = (item.SXauNoiMa.StartsWith(BhxhMLNS.PHUNHAN_PHUQUAN_DU_TOAN) || item.SXauNoiMa.StartsWith(BhxhMLNS.TUYVIEN_QP_DU_TOAN) || item.SXauNoiMa.StartsWith(BhxhMLNS.PHUNHAN_PHUQUAN_HACH_TOAN)) ? budgetDatas.Where(x => x.IIDMaDonVi == item.IIDMaDonVi && x.SXauNoiMa == item.SXauNoiMa).Sum(x => x.FLuongChinh).GetValueOrDefault() * item.FHeSoLayQuyLuong : budgetDatas.Where(x => x.IIDMaDonVi == item.IIDMaDonVi && x.SXauNoiMa == item.SXauNoiMa).Sum(x => x.FLuongChinh).GetValueOrDefault();
                    }
                    item.FPhuCapChucVu = (item.SXauNoiMa.StartsWith(BhxhMLNS.PHUNHAN_PHUQUAN_DU_TOAN) || item.SXauNoiMa.StartsWith(BhxhMLNS.TUYVIEN_QP_DU_TOAN) || item.SXauNoiMa.StartsWith(BhxhMLNS.PHUNHAN_PHUQUAN_HACH_TOAN)) ? budgetDatas.Where(x => x.IIDMaDonVi == item.IIDMaDonVi && x.SXauNoiMa == item.SXauNoiMa).Sum(x => x.FPhuCapChucVu).GetValueOrDefault() * item.FHeSoLayQuyLuong : budgetDatas.Where(x => x.IIDMaDonVi == item.IIDMaDonVi && x.SXauNoiMa == item.SXauNoiMa).Sum(x => x.FPhuCapChucVu).GetValueOrDefault();
                    item.FPCTNNghe = (item.SXauNoiMa.StartsWith(BhxhMLNS.PHUNHAN_PHUQUAN_DU_TOAN) || item.SXauNoiMa.StartsWith(BhxhMLNS.TUYVIEN_QP_DU_TOAN) || item.SXauNoiMa.StartsWith(BhxhMLNS.PHUNHAN_PHUQUAN_HACH_TOAN)) ? budgetDatas.Where(x => x.IIDMaDonVi == item.IIDMaDonVi && x.SXauNoiMa == item.SXauNoiMa).Sum(x => x.FPCTNNghe).GetValueOrDefault() * item.FHeSoLayQuyLuong : budgetDatas.Where(x => x.IIDMaDonVi == item.IIDMaDonVi && x.SXauNoiMa == item.SXauNoiMa).Sum(x => x.FPCTNNghe).GetValueOrDefault();
                    item.FPCTNVuotKhung = (item.SXauNoiMa.StartsWith(BhxhMLNS.PHUNHAN_PHUQUAN_DU_TOAN) || item.SXauNoiMa.StartsWith(BhxhMLNS.TUYVIEN_QP_DU_TOAN) || item.SXauNoiMa.StartsWith(BhxhMLNS.PHUNHAN_PHUQUAN_HACH_TOAN)) ? budgetDatas.Where(x => x.IIDMaDonVi == item.IIDMaDonVi && x.SXauNoiMa == item.SXauNoiMa).Sum(x => x.FPCTNVuotKhung).GetValueOrDefault() * item.FHeSoLayQuyLuong : budgetDatas.Where(x => x.IIDMaDonVi == item.IIDMaDonVi && x.SXauNoiMa == item.SXauNoiMa).Sum(x => x.FPCTNVuotKhung).GetValueOrDefault();
                    item.FHSBL = (item.SXauNoiMa.StartsWith(BhxhMLNS.PHUNHAN_PHUQUAN_DU_TOAN) || item.SXauNoiMa.StartsWith(BhxhMLNS.TUYVIEN_QP_DU_TOAN) || item.SXauNoiMa.StartsWith(BhxhMLNS.PHUNHAN_PHUQUAN_HACH_TOAN)) ? budgetDatas.Where(x => x.IIDMaDonVi == item.IIDMaDonVi && x.SXauNoiMa == item.SXauNoiMa).Sum(x => x.FHSBL).GetValueOrDefault() * item.FHeSoLayQuyLuong : budgetDatas.Where(x => x.IIDMaDonVi == item.IIDMaDonVi && x.SXauNoiMa == item.SXauNoiMa).Sum(x => x.FHSBL).GetValueOrDefault();
                });
                CalculateData();
            }
            else
            {
                MessageBoxHelper.Info("Chưa có dữ liệu");
            }
        }
    }
}
