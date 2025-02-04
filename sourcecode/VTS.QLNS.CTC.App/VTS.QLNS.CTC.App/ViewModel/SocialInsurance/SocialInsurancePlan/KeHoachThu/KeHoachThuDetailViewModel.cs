using AutoMapper;
using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Web.UI.WebControls;
using System.Windows;
using System.Windows.Data;
using VTS.QLNS.CTC.App.Command;
using VTS.QLNS.CTC.App.Helper;
using VTS.QLNS.CTC.App.Model;
using VTS.QLNS.CTC.App.Model.Control;
using VTS.QLNS.CTC.App.Properties;
using VTS.QLNS.CTC.App.Service;
using VTS.QLNS.CTC.App.Service.Impl;
using VTS.QLNS.CTC.App.View.SocialInsurance.SocialInsurancePlan.KeHoachThu;
using VTS.QLNS.CTC.App.View.SocialInsurance.SocialInsurancePlan.KeHoachThu.ImportKhtBHXH;
using VTS.QLNS.CTC.App.View.SocialInsurance.SocialInsurancePlan.KeHoachThu.PrintReport;
using VTS.QLNS.CTC.App.ViewModel.SocialInsurance.SocialInsurancePlan.KeHoachThu.ImportKhtBHXH;
using VTS.QLNS.CTC.App.ViewModel.SocialInsurance.SocialInsurancePlan.KeHoachThu.PrintReport;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Criteria;
using VTS.QLNS.CTC.Core.Domain.Query.Shared;
using VTS.QLNS.CTC.Core.Service;
using VTS.QLNS.CTC.Utility;
using VTS.QLNS.CTC.Utility.Enum;

namespace VTS.QLNS.CTC.App.ViewModel.SocialInsurance.SocialInsurancePlan.KeHoachThu
{
    public class KeHoachThuDetailViewModel : DetailViewModelBase<BhKhtBHXHModel, BhKhtBHXHChiTietModel>
    {
        private readonly ISessionService _sessionService;
        private readonly IKhtBHXHChiTietService _khtBHXHChiTietService;
        private readonly IKhtBHXHService _khtBHXHService;
        private readonly ISysAuditLogService _log;
        private SessionInfo _sessionInfo;
        private readonly IMapper _mapper;
        private readonly INsDonViService _nsDonViService;
        private string xnmConcatenation = "";
        public delegate void DataChangedEventHandler(object sender, EventArgs e);
        public event DataChangedEventHandler UpdateParentWindowEventHandler;
        private ICollection<BhKhtBHXHChiTietModel> _filterResult = new HashSet<BhKhtBHXHChiTietModel>();
        private ICollectionView _khtBHXHChiTietModelsView { get; set; }
        private ICollectionView _searchPopupView { get; set; }
        public List<BhKhtBHXHModel> ListIdsKhtBHXHSummary { get; set; }
        public List<TlDsBangLuongKeHoachModel> ListItemsGetSalary { get; set; }
        public List<TlBangLuongKeHoachExportQuery> DataImport { get; set; }
        public string SLuongKeHoach { get; set; }
        public ObservableCollection<CheckBoxItem> ListDonVi { get; set; }
        private BhKhtBHXHModel _ctTongHop;
        public BhKhtBHXHModel CtTongHop
        {
            get => _ctTongHop;
            set => SetProperty(ref _ctTongHop, value);
        }

        private string _sNoiDungSearch;
        public string SNoiDungSearch
        {
            get => _sNoiDungSearch;
            set => SetProperty(ref _sNoiDungSearch, value);
        }

        private ObservableCollection<BhKhtBHXHChiTietModel> _dataPopupSearchItems;
        public ObservableCollection<BhKhtBHXHChiTietModel> DataPopupSearchItems
        {
            get => _dataPopupSearchItems;
            set => SetProperty(ref _dataPopupSearchItems, value);
        }

        private BhKhtBHXHChiTietModel _selectedPopupItem;
        public BhKhtBHXHChiTietModel SelectedPopupItem
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

        private ObservableCollection<BhKhtBHXHChiTietModel> _dataSearch;
        public ObservableCollection<BhKhtBHXHChiTietModel> DataSearch
        {
            get => _dataSearch;
            set => SetProperty(ref _dataSearch, value);
        }

        private string _popupSearchText;
        public string PopupSearchText
        {
            set
            {
                SetProperty(ref _popupSearchText, value);
                _searchPopupView?.Refresh();
            }
            get => _popupSearchText;
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

        private bool _isShowColumnUnit;
        public bool IsShowColumnUnit
        {
            get => _isShowColumnUnit;
            set => SetProperty(ref _isShowColumnUnit, value);
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
                if (SetProperty(ref _selectedTypeShowAgencyKHT, value))
                {
                    if (_selectedTypeShowAgencyKHT != null && _selectedTypeShowAgencyKHT.ValueItem == TypeDisplay.CHITIET_DONVI)
                        _isShowColumnUnit = true;
                    else _isShowColumnUnit = false;
                    LoadData();
                    OnPropertyChanged(nameof(IsShowColumnUnit));
                    OnPropertyChanged(nameof(IsShowAgencyFilter));
                }
            }
        }
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
                BeForeRefresh();
                _khtBHXHChiTietModelsView.Refresh();
                CalculateData();
            }
        }
        public bool IsVoucherSummary { get; set; }
        public bool IsShowAgencyFilter => IsVoucherSummary && _selectedTypeShowAgencyKHT != null && _selectedTypeShowAgencyKHT.ValueItem == TypeDisplay.CHITIET_DONVI;
        public bool IsFilterDonVi => _viewSummarySelected != null &&
                                     _viewSummarySelected.ValueItem.Equals(TypeViewSummary.Detail.ToString());
        public bool IsAnotherUserCreate { get; set; }
        public bool IsEnabledDelete => !IsLock && SelectedItem != null;
        public bool IsDeleteAll => !IsLock && Items.Any(item => !item.IsModified);
        public PrintReportKhtBhxhViewModel PrintReportKhtBhxhViewModel { get; }
        public GetSalaryDataViewModel GetSalaryDataViewModel { get; set; }
        public ImportGetSalaryDataViewModel ImportGetSalaryDataViewModel { get; set; }
        public int NamLamViec { get; set; }
        public RelayCommand SearchCommand { get; }
        public RelayCommand ClearSearchCommand { get; }
        public new RelayCommand SaveCommand { get; }
        public new RelayCommand CloseCommand { get; }
        public RelayCommand PrintCommand { get; }
        public RelayCommand GetSalaryCommand { get; }
        public RelayCommand CanCuCommand { get; }
        public RelayCommand LaySummaryCanCuCommand { get; }
        public RelayCommand ImportSalaryDataCommand { get; }
        public RelayCommand SearchTextCommand { get; }

        public Visibility QSBQNamVisible { get; set; }
        public Visibility LuongChinhVisible { get; set; }
        public Visibility PCChucVuVisible { get; set; }
        public Visibility PCTNNgheVisible { get; set; }
        public Visibility PCTNVKVisible { get; set; }
        public Visibility NghiOmVisible { get; set; }

        public Visibility CongLuongVisible { get; set; }
        public Visibility NLDDongBHXHVisible { get; set; }
        public Visibility NSDDongBHXHVisible { get; set; }
        public Visibility CongBHXHVisible { get; set; }
        public Visibility NLDDongBHYTVisible { get; set; }
        public Visibility NSDDongBHYTVisible { get; set; }
        public Visibility CongBHYTVisible { get; set; }
        public Visibility NLDDongBHTNVisible { get; set; }
        public Visibility NSDDongBHTNVisible { get; set; }
        public Visibility CongBHTNVisible { get; set; }

        public bool IsReadOnlyCongLuong { get; set; }
        public bool IsReadOnlyCongBHXH { get; set; }
        public bool IsReadOnlyCongBHYT { get; set; }
        public bool IsReadOnlyCongBHTN { get; set; }
        public bool IsInit { get; set; }

        public DateTime DtNow => DateTime.Now;

        public KeHoachThuDetailViewModel(
            IKhtBHXHChiTietService khtBHXHChiTietService,
            IKhtBHXHService khtBHXHService,
            ISessionService sessionService,
            IMapper mapper,
            ISysAuditLogService log,
            PrintReportKhtBhxhViewModel printReportKhtBhxhViewModel,
            GetSalaryDataViewModel salaryDataViewModel,
            ImportGetSalaryDataViewModel importGetSalaryDataView,
            INsDonViService nsDonViService)
        {
            _khtBHXHChiTietService = khtBHXHChiTietService;
            _khtBHXHService = khtBHXHService;
            _sessionService = sessionService;
            _log = log;
            _mapper = mapper;
            PrintReportKhtBhxhViewModel = printReportKhtBhxhViewModel;
            GetSalaryDataViewModel = salaryDataViewModel;
            ImportGetSalaryDataViewModel = importGetSalaryDataView;
            _nsDonViService = nsDonViService;

            ClearSearchCommand = new RelayCommand(OnClearSearch);
            SaveCommand = new RelayCommand(o => OnSave());
            CloseCommand = new RelayCommand(OnClose);
            PrintCommand = new RelayCommand(OnPrint);
            //GetSalaryCommand = new RelayCommand(obj => GetPlanSalary());
            GetSalaryCommand = new RelayCommand(obj => GetSalaryData());
            ImportSalaryDataCommand = new RelayCommand(obj => ImportSalaryData());
            SearchTextCommand = new RelayCommand(obj => SearchTextFilter());
        }

        public override void Init()
        {
            base.Init();
            _sessionInfo = _sessionService.Current;
            NamLamViec = _sessionService.Current.YearOfWork;
            _selectedTypeShowAgencyKHT = null;
            _isShowColumnUnit = false;
            if (Model != null)
            {
                IsLock = Model.BIsKhoa;
                IsAnotherUserCreate = Model.SNguoiTao != _sessionInfo.Principal;
            }
            if (!string.IsNullOrEmpty(Model.STongHop))
            {
                LoadComboboxTypeShow();
            }
            IsInit = true;
            DataSearch = new ObservableCollection<BhKhtBHXHChiTietModel>();
            LoadData();
            IsInit = false;
            ListItemsGetSalary = new List<TlDsBangLuongKeHoachModel>();
            OnClearSearch(false);
        }

        private void LoadComboboxTypeShow()
        {
            TypeShowAgencyKHT = new ObservableCollection<ComboboxItem>();
            TypeShowAgencyKHT.Add(new ComboboxItem { ValueItem = TypeDisplay.TONG_DONVI, DisplayItem = TypeDisplay.TONG_DONVI });
            TypeShowAgencyKHT.Add(new ComboboxItem { ValueItem = TypeDisplay.CHITIET_DONVI, DisplayItem = TypeDisplay.CHITIET_DONVI });
            _selectedTypeShowAgencyKHT = TypeShowAgencyKHT.FirstOrDefault();
            OnPropertyChanged(nameof(SelectedTypeShowAgencyKHT));
        }

        private void OnClearSearch(object obj)
        {
            SNoiDungSearch = string.Empty;
            if (!(obj is bool temp))
            {
                _khtBHXHChiTietModelsView.Refresh();
            }
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
            Func<BhKhtBHXHChiTietModel, bool> isAdd = x => x.IsModified && !x.IsDeleted && x.IsAdd && !x.IsHangCha;
            Func<BhKhtBHXHChiTietModel, bool> isUpdate = x => x.IsModified && !x.IsDeleted && !x.IsAdd && !x.IsHangCha;
            Func<BhKhtBHXHChiTietModel, bool> isDelete = x => x.IsDeleted && !x.IsHangCha;

            var detailsAdd = Items.Where(isAdd).ToList();
            var detailsUpdate = Items.Where(isUpdate).ToList();
            var detailsDelete = Items.Where(isDelete).ToList();

            //thêm mới chứng từ chi tiết
            if (detailsAdd.Count > 0)
            {
                var addItems = new List<BhKhtBHXHChiTiet>();
                _mapper.Map(detailsAdd, addItems);
                foreach (var item in addItems)
                {
                    item.FLuongChinh = Math.Round(item.FLuongChinh.GetValueOrDefault());
                    item.FNghiOm = Math.Round(item.FNghiOm.GetValueOrDefault());
                    item.FPCTNNghe = Math.Round(item.FPCTNNghe.GetValueOrDefault());
                    item.FPCTNVuotKhung = Math.Round(item.FPCTNVuotKhung.GetValueOrDefault());
                    item.FPhuCapChucVu = Math.Round(item.FPhuCapChucVu.GetValueOrDefault());
                    item.FHSBL = Math.Round(item.FHSBL.GetValueOrDefault());
                    item.FThuBHXHNguoiLaoDong = Math.Round(item.FThuBHXHNguoiLaoDong.GetValueOrDefault());
                    item.FThuBHXHNguoiSuDungLaoDong = Math.Round(item.FThuBHXHNguoiSuDungLaoDong.GetValueOrDefault());
                    item.FTongThuBHXH = Math.Round(item.FTongThuBHXH.GetValueOrDefault());
                    item.FThuBHYTNguoiLaoDong = Math.Round(item.FThuBHYTNguoiLaoDong.GetValueOrDefault());
                    item.FThuBHYTNguoiSuDungLaoDong = Math.Round(item.FThuBHYTNguoiSuDungLaoDong.GetValueOrDefault());
                    item.FTongThuBHYT = Math.Round(item.FTongThuBHYT.GetValueOrDefault());
                    item.FThuBHTNNguoiLaoDong = Math.Round(item.FThuBHTNNguoiLaoDong.GetValueOrDefault());
                    item.FThuBHTNNguoiSuDungLaoDong = Math.Round(item.FThuBHTNNguoiSuDungLaoDong.GetValueOrDefault());
                    item.FTongThuBHTN = Math.Round(item.FTongThuBHTN.GetValueOrDefault());
                    item.FTongCong = Math.Round(item.FTongCong.GetValueOrDefault());
                    item.SNguoiTao = _sessionInfo.Principal;
                    item.DNgayTao = DateTime.Now;
                }
                _khtBHXHChiTietService.AddRange(addItems);

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
                    var khtBHXHChiTiet = _khtBHXHChiTietService.FindById(updateItem.Id);
                    _mapper.Map(updateItem, khtBHXHChiTiet);

                    khtBHXHChiTiet.SNguoiSua = _sessionInfo.Principal;
                    khtBHXHChiTiet.DNgaySua = DateTime.Now;
                    khtBHXHChiTiet.FLuongChinh = Math.Round(khtBHXHChiTiet.FLuongChinh.GetValueOrDefault());
                    khtBHXHChiTiet.FNghiOm = Math.Round(khtBHXHChiTiet.FNghiOm.GetValueOrDefault());
                    khtBHXHChiTiet.FPCTNNghe = Math.Round(khtBHXHChiTiet.FPCTNNghe.GetValueOrDefault());
                    khtBHXHChiTiet.FPCTNVuotKhung = Math.Round(khtBHXHChiTiet.FPCTNVuotKhung.GetValueOrDefault());
                    khtBHXHChiTiet.FPhuCapChucVu = Math.Round(khtBHXHChiTiet.FPhuCapChucVu.GetValueOrDefault());
                    khtBHXHChiTiet.FHSBL = Math.Round(khtBHXHChiTiet.FHSBL.GetValueOrDefault());
                    khtBHXHChiTiet.FThuBHXHNguoiLaoDong = Math.Round(khtBHXHChiTiet.FThuBHXHNguoiLaoDong.GetValueOrDefault());
                    khtBHXHChiTiet.FThuBHXHNguoiSuDungLaoDong = Math.Round(khtBHXHChiTiet.FThuBHXHNguoiSuDungLaoDong.GetValueOrDefault());
                    khtBHXHChiTiet.FTongThuBHXH = Math.Round(khtBHXHChiTiet.FTongThuBHXH.GetValueOrDefault());
                    khtBHXHChiTiet.FThuBHYTNguoiLaoDong = Math.Round(khtBHXHChiTiet.FThuBHYTNguoiLaoDong.GetValueOrDefault());
                    khtBHXHChiTiet.FThuBHYTNguoiSuDungLaoDong = Math.Round(khtBHXHChiTiet.FThuBHYTNguoiSuDungLaoDong.GetValueOrDefault());
                    khtBHXHChiTiet.FTongThuBHYT = Math.Round(khtBHXHChiTiet.FTongThuBHYT.GetValueOrDefault());
                    khtBHXHChiTiet.FThuBHTNNguoiLaoDong = Math.Round(khtBHXHChiTiet.FThuBHTNNguoiLaoDong.GetValueOrDefault());
                    khtBHXHChiTiet.FThuBHTNNguoiSuDungLaoDong = Math.Round(khtBHXHChiTiet.FThuBHTNNguoiSuDungLaoDong.GetValueOrDefault());
                    khtBHXHChiTiet.FTongThuBHTN = Math.Round(khtBHXHChiTiet.FTongThuBHTN.GetValueOrDefault());
                    khtBHXHChiTiet.FTongCong = Math.Round(khtBHXHChiTiet.FTongCong.GetValueOrDefault());

                    _khtBHXHChiTietService.Update(khtBHXHChiTiet);
                    updateItem.IsModified = false;
                }
            }
            //cập nhật tổng cộng chứng từ
            var bhKhtChungTu = _khtBHXHService.FindById(Model.Id);
            bhKhtChungTu.IQSBQNam = Model.IQSBQNam;
            bhKhtChungTu.FLuongChinh = Model.FLuongChinh;
            bhKhtChungTu.FPhuCapChucVu = Model.FPhuCapChucVu;
            bhKhtChungTu.FPCTNNghe = Model.FPCTNNghe;
            bhKhtChungTu.FPCTNVuotKhung = Model.FPCTNVuotKhung;
            bhKhtChungTu.FNghiOm = Model.FNghiOm;
            bhKhtChungTu.FHSBL = Model.FHSBL;
            bhKhtChungTu.FTongQTLN = Model.FTongQTLN;
            bhKhtChungTu.FThuBHXHNLDDong = Model.FThuBHXHNLDDong;
            bhKhtChungTu.FThuBHXHNSDDong = Model.FThuBHXHNSDDong;
            bhKhtChungTu.FThuBHXH = Model.FThuBHXH;
            bhKhtChungTu.FThuBHYTNLDDong = Model.FThuBHYTNLDDong;
            bhKhtChungTu.FThuBHYTNSDDong = Model.FThuBHYTNSDDong;
            bhKhtChungTu.FTongBHYT = Model.FTongBHYT;
            bhKhtChungTu.FThuBHTNNLDDong = Model.FThuBHTNNLDDong;
            bhKhtChungTu.FThuBHTNNSDDong = Model.FThuBHTNNSDDong;
            bhKhtChungTu.FThuBHTN = Model.FThuBHTN;
            bhKhtChungTu.FTong = Model.FTong;
            bhKhtChungTu.SBangLuongKeHoach = SLuongKeHoach;
            _khtBHXHService.Update(bhKhtChungTu);
            Model.SBangLuongKeHoach = bhKhtChungTu.SBangLuongKeHoach;
            OnRefresh();
            _log.WriteLog(Resources.ApplicationName, "Số nhu cầu - chứng từ chi tiết", (int)TypeExecute.Adjust, DtNow, TransactionStatus.Success, _sessionService.Current.Principal);
            OnPropertyChanged(nameof(IsSaveData));
            OnPropertyChanged(nameof(IsDeleteAll));
            OnPropertyChanged(nameof(Model));
            UpdateParentWindowEventHandler?.Invoke(Model, new EventArgs());
            var message = Resources.MsgSaveDone;
            MessageBoxHelper.Info(message);
        }

        protected override void OnRefresh()
        {
            IsInit = true;
            LoadData();
            IsInit = false;
        }

        private void LoadAgencies(string agencyIds)
        {
            var listDonVi = _nsDonViService.FindByListIdDonVi(agencyIds, _sessionInfo.YearOfWork);
            _agencies = _mapper.Map<List<ComboboxItem>>(listDonVi);
            OnPropertyChanged(nameof(Agencies));
        }

        public override void LoadData(params object[] args)
        {
            SLuongKeHoach = Model.SBangLuongKeHoach;
            IsReadOnlyCongLuong = IsReadOnlyCongBHYT = IsReadOnlyCongBHTN = IsReadOnlyCongBHXH = true;
            List<BhKhtBHXHChiTiet> temp = new List<BhKhtBHXHChiTiet>();
            KhtBHXHChiTietCriteria searchCondition = new KhtBHXHChiTietCriteria();
            searchCondition.NamLamViec = _sessionInfo.YearOfWork;
            searchCondition.IdDonVi = Model.IID_MaDonVi;
            searchCondition.khtBhxhId = Model.Id;
            if (_selectedDonVi != null)
            {
                searchCondition.IdDonViFilter = _selectedDonVi.IIDMaDonVi;
                searchCondition.IdDonVi = _selectedDonVi.IIDMaDonVi;
            }
            if (IsVoucherSummary && SelectedTypeShowAgencyKHT != null && SelectedTypeShowAgencyKHT.ValueItem == TypeDisplay.CHITIET_DONVI)
            {
                var voucherNos = Model.STongHop.Split(",").ToList();
                List<BhKhtBHXH> listChungTu = _khtBHXHService.FindByAggregateVoucher(voucherNos, _sessionInfo.YearOfWork).ToList();
                List<BhKhtBHXHChiTiet> listChungTuChiTietParent = new List<BhKhtBHXHChiTiet>();
                List<BhKhtBHXHChiTiet> listChungTuChiTietChildren = new List<BhKhtBHXHChiTiet>();
                foreach (var chungTu in listChungTu)
                {
                    searchCondition.khtBhxhId = chungTu.Id;
                    searchCondition.MaDonVi = chungTu.IID_MaDonVi;
                    var listQuery = _khtBHXHChiTietService.FindBhKhtBHXHChiTietByCondition(searchCondition).ToList();
                    listChungTuChiTietParent.AddRange(listQuery.Where(x => x.IsHangCha));
                    listChungTuChiTietChildren.AddRange(listQuery.Where(x => !x.IsHangCha));
                }
                var listXauNoiMa = listChungTuChiTietChildren.Select(x => x.SXauNoiMa).Distinct().ToList();
                listChungTuChiTietParent = listChungTuChiTietParent.Where(x => listXauNoiMa.Any(y => y.Contains(x.SXauNoiMa))).GroupBy(x => x.SXauNoiMa).Select(x => x.First()).Distinct().ToList();
                temp.AddRange(listChungTuChiTietParent);
                temp.AddRange(listChungTuChiTietChildren);
                temp = temp.OrderBy(x => x.SXauNoiMa).ThenBy(x => x.IIdMaDonVi).ToList();
                string agencyIds = string.Join(",", listChungTu.Select(x => x.IID_MaDonVi));
                LoadAgencies(agencyIds);
            }
            else
            {
                searchCondition.khtBhxhId = Model.Id;
                searchCondition.MaDonVi = Model.IID_MaDonVi;
                temp = _khtBHXHChiTietService.FindBhKhtBHXHChiTietByCondition(searchCondition).ToList();
            }

            var existBhChiTiet = _khtBHXHChiTietService.ExistBHXHChiTiet(Model.Id);
            foreach (var item in temp)
            {
                item.IsAuToFillTuChi = !existBhChiTiet;
            }
            Items = _mapper.Map<ObservableCollection<BhKhtBHXHChiTietModel>>(temp);
            DataPopupSearchItems = _mapper.Map<ObservableCollection<BhKhtBHXHChiTietModel>>(temp);
            _khtBHXHChiTietModelsView = CollectionViewSource.GetDefaultView(Items);
            _khtBHXHChiTietModelsView.Filter = ItemsViewFilter;
            foreach (var khtBhxhChiTietModel in Items)
            {
                var entityItem = temp.FirstOrDefault(x => x.Id == khtBhxhChiTietModel.Id);
                khtBhxhChiTietModel.IsFilter = true;
                if (!khtBhxhChiTietModel.IsHangCha)
                {
                    khtBhxhChiTietModel.PropertyChanged += (sender, args) =>
                    {
                        if (args.PropertyName == nameof(BhKhtBHXHChiTietModel.IQSBQNam))
                        {
                            CalculatePlanData(khtBhxhChiTietModel, entityItem);
                        }
                        if (args.PropertyName == nameof(BhKhtBHXHChiTietModel.FLuongChinh) || args.PropertyName == nameof(BhKhtBHXHChiTietModel.FPhuCapChucVu)
                            || args.PropertyName == nameof(BhKhtBHXHChiTietModel.FPCTNNghe) || args.PropertyName == nameof(BhKhtBHXHChiTietModel.FPCTNVuotKhung)
                            || args.PropertyName == nameof(BhKhtBHXHChiTietModel.FNghiOm) || args.PropertyName == nameof(BhKhtBHXHChiTietModel.FHSBL)
                            || args.PropertyName == nameof(BhKhtBHXHChiTietModel.IQSBQNam))
                        {
                            BhKhtBHXHChiTietModel item = (BhKhtBHXHChiTietModel)sender;
                            item.IsModified = true;
                            CalculateData();
                            khtBhxhChiTietModel.IsModified = true;
                            OnPropertyChanged(nameof(IsSaveData));
                        }
                    };
                }
            }
            CalculateData();
        }

        private bool ItemsViewFilter(object obj)
        {
            bool result = true;
            var item = (BhKhtBHXHChiTietModel)obj;
            result = VoucherDetailFilter(item);
            if (!result && item.IsHangCha)
            {
                result = xnmConcatenation.Contains(item.SXauNoiMa);
            }
            if (result)
                item.IsFilter = result;
            if (!string.IsNullOrEmpty(SNoiDungSearch))
                result = DataSearch.Any(x => x.Id.Equals(item.Id));
            return result;
        }

        private bool VoucherDetailFilter(object obj)
        {
            bool result = true;
            var item = (BhKhtBHXHChiTietModel)obj;
            if (IsShowAgencyFilter && SelectedAgency != null)
                result = result && item.IIdMaDonVi == _selectedAgency.ValueItem;
            item.IsFilter = result;
            return result;
        }

        private void BeForeRefresh()
        {
            _filterResult = Items.Where(item => VoucherDetailFilter(item)).Where(item => !item.IsHangCha).ToList();
            xnmConcatenation = string.Join(";", _filterResult.Select(i => i.SXauNoiMa).ToHashSet());
        }

        private void CalculatePlanData(BhKhtBHXHChiTietModel model, BhKhtBHXHChiTiet item)
        {
            if (model.SXauNoiMa == BhxhMLNS.HSQ_BS_DU_TOAN || model.SXauNoiMa == BhxhMLNS.HSQ_BS_HACH_TOAN)
            {
                var fLuongChinhCal = model.IQSBQNam.GetValueOrDefault() * 12 * (double)model.DHeSoLCS.GetValueOrDefault();
                model.FLuongChinh = fLuongChinhCal;
            }
        }

        private void CalculateParent(Guid idParent, BhKhtBHXHChiTietModel item, Dictionary<Guid, BhKhtBHXHChiTietModel> dictByMlns)
        {
            if (!dictByMlns.ContainsKey(idParent))
            {
                return;
            }

            var model = dictByMlns[idParent];
            model.IQSBQNam += item.IQSBQNam.GetValueOrDefault();
            model.FLuongChinh += item.FLuongChinh.GetValueOrDefault();
            model.FPhuCapChucVu += item.FPhuCapChucVu.GetValueOrDefault();
            model.FPCTNNghe += item.FPCTNNghe.GetValueOrDefault();
            model.FPCTNVuotKhung += item.FPCTNVuotKhung.GetValueOrDefault();
            model.FNghiOm += item.FNghiOm.GetValueOrDefault();
            model.FHSBL += item.FHSBL.GetValueOrDefault();
            model.FThuBHXHNguoiLaoDong += item.FThuBHXHNguoiLaoDong.GetValueOrDefault();
            model.FThuBHXHNguoiSuDungLaoDong += item.FThuBHXHNguoiSuDungLaoDong.GetValueOrDefault();
            model.FThuBHYTNguoiLaoDong += item.FThuBHYTNguoiLaoDong.GetValueOrDefault();
            model.FThuBHYTNguoiSuDungLaoDong += item.FThuBHYTNguoiSuDungLaoDong.GetValueOrDefault();
            model.FThuBHTNNguoiLaoDong += item.FThuBHTNNguoiLaoDong.GetValueOrDefault();
            model.FThuBHTNNguoiSuDungLaoDong += item.FThuBHTNNguoiSuDungLaoDong.GetValueOrDefault();

            CalculateParent(model.IdParent, item, dictByMlns);
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
                    x.FThuBHXHNguoiLaoDong = 0;
                    x.FThuBHXHNguoiSuDungLaoDong = 0;
                    x.FThuBHYTNguoiLaoDong = 0;
                    x.FThuBHYTNguoiSuDungLaoDong = 0;
                    x.FThuBHTNNguoiLaoDong = 0;
                    x.FThuBHTNNguoiSuDungLaoDong = 0;
                });

            var temp = Items.Where(x => !x.IsHangCha && !x.IsDeleted && x.IsFilter).ToList();
            var dictByMlns = Items.GroupBy(x => x.IIDMucLucNganSach).ToDictionary(x => x.Key, x => x.First());
            foreach (var item in temp)
            {
                CalculateParent(item.IdParent, item, dictByMlns);
            }

            UpdateTotal();
        }
        private void UpdateTotal()
        {
            Model.FThuBHXHNLDDong = 0;
            Model.FThuBHXHNSDDong = 0;
            Model.FThuBHXH = 0;
            Model.FThuBHYTNLDDong = 0;
            Model.FThuBHYTNSDDong = 0;
            Model.FTongBHYT = 0;
            Model.FThuBHTNNLDDong = 0;
            Model.FThuBHTNNSDDong = 0;
            Model.FThuBHTN = 0;
            Model.FTong = 0;
            Model.IQSBQNam = 0;
            Model.FLuongChinh = 0;
            Model.FPhuCapChucVu = 0;
            Model.FPCTNNghe = 0;
            Model.FPCTNVuotKhung = 0;
            Model.FNghiOm = 0;
            Model.FHSBL = 0;
            Model.FTongQTLN = 0;

            var roots = Items.Where(t => !t.IsHangCha).ToList();
            foreach (var item in roots)
            {
                Model.FThuBHXHNLDDong += Math.Round(item.FThuBHXHNguoiLaoDong.GetValueOrDefault());
                Model.FThuBHXHNSDDong += Math.Round(item.FThuBHXHNguoiSuDungLaoDong.GetValueOrDefault());
                Model.FThuBHXH += Math.Round(item.FTongThuBHXH.GetValueOrDefault());
                Model.FThuBHYTNLDDong += Math.Round(item.FThuBHYTNguoiLaoDong.GetValueOrDefault());
                Model.FThuBHYTNSDDong += Math.Round(item.FThuBHYTNguoiSuDungLaoDong.GetValueOrDefault());
                Model.FTongBHYT += Math.Round(item.FTongThuBHYT.GetValueOrDefault());
                Model.FThuBHTNNLDDong += Math.Round(item.FThuBHTNNguoiLaoDong.GetValueOrDefault());
                Model.FThuBHTNNSDDong += Math.Round(item.FThuBHTNNguoiSuDungLaoDong.GetValueOrDefault());
                Model.FThuBHTN += Math.Round(item.FTongThuBHTN.GetValueOrDefault());
                Model.FTong += Math.Round(item.FTongCong.GetValueOrDefault());
                Model.IQSBQNam += item.IQSBQNam.GetValueOrDefault();
                Model.FLuongChinh += Math.Round(item.FLuongChinh.GetValueOrDefault());
                Model.FPhuCapChucVu += Math.Round(item.FPhuCapChucVu.GetValueOrDefault());
                Model.FPCTNNghe += Math.Round(item.FPCTNNghe.GetValueOrDefault());
                Model.FPCTNVuotKhung += Math.Round(item.FPCTNVuotKhung.GetValueOrDefault());
                Model.FNghiOm += Math.Round(item.FNghiOm.GetValueOrDefault());
                Model.FHSBL += Math.Round(item.FHSBL.GetValueOrDefault());
                Model.FTongQTLN += Math.Round(item.FTongQuyTienLuongNam.GetValueOrDefault());
            }
        }
        protected override void OnSelectedItemChanged()
        {
            base.OnSelectedItemChanged();
            OnPropertyChanged(nameof(IsEnabledDelete));
        }

        private void OnPrint(object param)
        {
            var bhxhCheckPrintType = (BHXHCheckPrintType)((int)param);
            object content;
            PrintReportKhtBhxhViewModel.BHXHCheckPrintType = bhxhCheckPrintType;
            if (Items != null && Items.Count > 0)
            {
                PrintReportKhtBhxhViewModel.ReportNameTypeValue = (int)BHXHCheckPrintType.KE_HOACH_THU_BHXH_BHYT_BHTN;
                PrintReportKhtBhxhViewModel.DonViChaChungTu = GetParentUnit();
                PrintReportKhtBhxhViewModel.DonViChungTu = Model.STenDonVi;
                PrintReportKhtBhxhViewModel.KhtBhxhId = Model.Id;
                PrintReportKhtBhxhViewModel.MaDonViChungTu = Model.IID_MaDonVi;
                PrintReportKhtBhxhViewModel.ListDonVi = ListDonVi;
            }
            PrintReportKhtBhxhViewModel.Init();
            content = new PrintKhtBHXH
            {
                DataContext = PrintReportKhtBhxhViewModel
            };
            if (content != null)
            {
                DialogHost.Show(content, SystemConstants.DETAIL_DIALOG, null, null);
            }
        }
        private string GetParentUnit()
        {
            string sParent = NSConstants.BO_QUOC_PHONG;
            DonVi donvi = _nsDonViService.FindByIdDonVi(Model.IID_MaDonVi, _sessionService.Current.YearOfWork);
            if (!"0".Equals(donvi?.Loai))
            {
                DonVi donViCapTren = _nsDonViService.FindByLoai("0", _sessionService.Current.YearOfWork);
                sParent = donViCapTren?.TenDonVi;
            }
            return sParent;
        }
        //private void GetPlanSalary()
        //{
        //    var planSalary = _khtBHXHChiTietService.GetPlanSalary(_sessionService.Current.YearOfWork, BhxhMLNS.LUONG_CHINH, BhxhMLNS.PHU_CAP_CHUC_VU, BhxhMLNS.PHU_CAP_TNN, BhxhMLNS.PHU_CAP_TNVK);
        //    var quanSoBinhQuan = _khtBHXHChiTietService.GetQuanSoBinhQuan(_sessionService.Current.YearOfWork);
        //    var itemFilter = Items.Where(x => !string.IsNullOrEmpty(x.SMaPhuCap));
        //    foreach (var item in itemFilter)
        //    {
        //        var lstMaPhuCap = item.SMaPhuCap.Split(",").ToList();
        //        item.FLuongChinh = planSalary.Where(x => x.XauNoiMa == item.SXauNoiMa && x.MaPhuCap == BhxhMLNS.LUONG_CHINH).Select(y => y.GiaTri).FirstOrDefault();
        //        item.FPhuCapChucVu = planSalary.Where(x => x.XauNoiMa == item.SXauNoiMa && x.MaPhuCap == BhxhMLNS.PHU_CAP_CHUC_VU).Select(y => y.GiaTri).FirstOrDefault();
        //        item.FPCTNNghe = planSalary.Where(x => x.XauNoiMa == item.SXauNoiMa && x.MaPhuCap == BhxhMLNS.PHU_CAP_TNN).Select(y => y.GiaTri).FirstOrDefault();
        //        item.FPCTNVuotKhung = planSalary.Where(x => x.XauNoiMa == item.SXauNoiMa && x.MaPhuCap == BhxhMLNS.PHU_CAP_TNVK).Select(y => y.GiaTri).FirstOrDefault();
        //        item.IQSBQNam = quanSoBinhQuan.FirstOrDefault(x => x.XauNoiMa == item.SXauNoiMa).QSBQ;
        //    }
        //}

        private void GetSalaryData()
        {
            GetSalaryDataViewModel.ListItems = ListItemsGetSalary;
            GetSalaryDataViewModel.SLuongKeHoach = SLuongKeHoach;
            GetSalaryDataViewModel.SavedAction = obj =>
            {
                BhGetSalaryDataModel dataSalary = (BhGetSalaryDataModel)obj;
                if (dataSalary != null)
                {
                    ListItemsGetSalary = dataSalary.Items.ToList();
                    SLuongKeHoach = string.Join(StringUtils.COMMA, ListItemsGetSalary.Select(x => x.Id));
                    var itemFilter = Items.Where(x => !string.IsNullOrEmpty(x.SMaPhuCap));
                    itemFilter.Select(item =>
                    {
                        item.FLuongChinh = dataSalary.ItemsPlanSalary.Any(x => x.XauNoiMa.Equals(item.SXauNoiMa) && x.MaPhuCap == BhxhMLNS.LUONG_CHINH) ? dataSalary.ItemsPlanSalary.FirstOrDefault(x => x.XauNoiMa.Equals(item.SXauNoiMa) && x.MaPhuCap == BhxhMLNS.LUONG_CHINH).GiaTri : 0;
                        item.FPhuCapChucVu = dataSalary.ItemsPlanSalary.Any(x => x.XauNoiMa.Equals(item.SXauNoiMa) && x.MaPhuCap == BhxhMLNS.PHU_CAP_CHUC_VU) ? dataSalary.ItemsPlanSalary.FirstOrDefault(x => x.XauNoiMa.Equals(item.SXauNoiMa) && x.MaPhuCap == BhxhMLNS.PHU_CAP_CHUC_VU).GiaTri : 0;
                        item.FPCTNNghe = dataSalary.ItemsPlanSalary.Any(x => x.XauNoiMa.Equals(item.SXauNoiMa) && x.MaPhuCap == BhxhMLNS.PHU_CAP_TNN) ? dataSalary.ItemsPlanSalary.FirstOrDefault(x => x.XauNoiMa.Equals(item.SXauNoiMa) && x.MaPhuCap == BhxhMLNS.PHU_CAP_TNN).GiaTri : 0;
                        item.FPCTNVuotKhung = dataSalary.ItemsPlanSalary.Any(x => x.XauNoiMa.Equals(item.SXauNoiMa) && x.MaPhuCap == BhxhMLNS.PHU_CAP_TNVK) ? dataSalary.ItemsPlanSalary.FirstOrDefault(x => x.XauNoiMa.Equals(item.SXauNoiMa) && x.MaPhuCap == BhxhMLNS.PHU_CAP_TNVK).GiaTri : 0;
                        item.IQSBQNam = dataSalary.ItemArmy.Any(x => x.XauNoiMa.Equals(item.SXauNoiMa)) ? dataSalary.ItemArmy.FirstOrDefault(x => x.XauNoiMa.Equals(item.SXauNoiMa)).QSBQ : 0;
                        item.IsModified = (item.FLuongChinh.GetValueOrDefault() != 0 || item.FPhuCapChucVu.GetValueOrDefault() != 0 || item.FPCTNNghe.GetValueOrDefault() != 0 || item.FPCTNVuotKhung.GetValueOrDefault() != 0 || item.IQSBQNam.GetValueOrDefault() != 0) ? true : false;
                        return item;
                    }).ToList();
                }
                else
                {
                    SLuongKeHoach = string.Empty;
                }
            };
            object content = new GetSalaryData
            {
                DataContext = GetSalaryDataViewModel
            };
            GetSalaryDataViewModel.Init();
            DialogHost.Show(content, SystemConstants.DETAIL_DIALOG, null, null);

        }

        private void ImportSalaryData()
        {
            ImportGetSalaryDataViewModel.Model = Model;
            ImportGetSalaryDataViewModel.SavedAction = obj =>
            {
                List<TlBangLuongKeHoachExportQuery> data = (List<TlBangLuongKeHoachExportQuery>)obj;
                if (!data.IsEmpty())
                {
                    var itemFilter = Items.Where(x => !string.IsNullOrEmpty(x.SMaPhuCap));
                    TlBangLuongKeHoachExportQuery dataMap;
                    itemFilter.Select(item =>
                    {
                        if (data.Any(x => x.SXauNoiMa.Equals(item.SXauNoiMa)))
                        {
                            dataMap = data.FirstOrDefault(x => x.SXauNoiMa.Equals(item.SXauNoiMa));
                            item.FLuongChinh = dataMap.LHT_TT;
                            item.FPhuCapChucVu = dataMap.PCCV_TT;
                            item.FPCTNNghe = dataMap.PCTN_TT;
                            item.FPCTNVuotKhung = dataMap.PCTNVK_TT;
                            item.IQSBQNam = (int?)dataMap.QSBQ;
                        }
                        return item;
                    }).ToList();
                }
            };
            object content = new ImportGetSalaryData
            {
                DataContext = ImportGetSalaryDataViewModel
            };
            ImportGetSalaryDataViewModel.Init();
            ImportGetSalaryDataViewModel.Show();
        }

        private void SearchTextFilter()
        {
            if (!string.IsNullOrEmpty(SNoiDungSearch))
            {
                List<string> lstResult = new List<string>();
                List<string> lstParents = new List<string>();
                List<BhKhtBHXHChiTietModel> results = new List<BhKhtBHXHChiTietModel>();

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
                DataSearch = new ObservableCollection<BhKhtBHXHChiTietModel>(results);
            }
            else
            {
                DataSearch = new ObservableCollection<BhKhtBHXHChiTietModel>();
            }
            _khtBHXHChiTietModelsView.Refresh();
        }

        private List<BhKhtBHXHChiTietModel> GetDataParent(List<string> lstInput)
        {
            List<BhKhtBHXHChiTietModel> result = new List<BhKhtBHXHChiTietModel>();
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

        private void GetListChild(List<BhKhtBHXHChiTietModel> lstInput, List<BhKhtBHXHChiTietModel> results)
        {
            var itemChild = Items.Where(x => lstInput.Select(x => x.IIDMucLucNganSach).Distinct().Contains(x.IdParent)).ToList();
            if (!itemChild.IsEmpty())
            {
                results.AddRange(itemChild);
                foreach (var item in itemChild.Where(x => Items.Select(y => y.IdParent).Distinct().Contains(x.IIDMucLucNganSach)))
                {
                    GetListChild(new List<BhKhtBHXHChiTietModel>() { item }, results);
                }
            }
        }
    }
}
