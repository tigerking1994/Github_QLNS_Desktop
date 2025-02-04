using AutoMapper;
using log4net;
using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Data;
using VTS.QLNS.CTC.App.Command;
using VTS.QLNS.CTC.App.Helper;
using VTS.QLNS.CTC.App.Model;
using VTS.QLNS.CTC.App.Model.Control;
using VTS.QLNS.CTC.App.Properties;
using VTS.QLNS.CTC.App.Service;
using VTS.QLNS.CTC.App.Service.Impl;
using VTS.QLNS.CTC.App.View.SocialInsurance.SocialInsuranceSettlement.ThamDinhQuyetToan;
using VTS.QLNS.CTC.App.View.SocialInsurance.SocialInsuranceSettlement.ThamDinhQuyetToan.PrintReport;
using VTS.QLNS.CTC.App.ViewModel.SocialInsurance.SocialInsuranceSettlement.QuyetToanChiNamBHXH.PritnReport;
using VTS.QLNS.CTC.App.ViewModel.SocialInsurance.SocialInsuranceSettlement.QuyetToanThuBHXH.PrintReport;
using VTS.QLNS.CTC.App.ViewModel.SocialInsurance.SocialInsuranceSettlement.ThamDinhQuyetToan.PrintReport;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Service;
using VTS.QLNS.CTC.Utility;
using VTS.QLNS.CTC.Utility.Enum;

namespace VTS.QLNS.CTC.App.ViewModel.SocialInsurance.SocialInsuranceSettlement.ThamDinhQuyetToan
{
    public class ThamDinhQuyetToanDetailViewModel : StandardDetailViewModelBase<BhThamDinhQuyetToanModel, BhThamDinhQuyetToanChiTietModel>
    {
        private readonly IBhDmMucLucNganSachService _bhDmMucLucNganSachService;
        private readonly IBhDmThamDinhQuyetToanService _bhDmThamDinhQuyetToanService;
        private readonly IBhThamDinhQuyetToanService _bhThamDinhQuyetToanService;
        private readonly IBhThamDinhQuyetToanChiTietService _bhThamDinhQuyetToanChiTietService;
        private ICollectionView _itemsView;
        private ICollection<BhThamDinhQuyetToanChiTietModel> _filterResult = new HashSet<BhThamDinhQuyetToanChiTietModel>();
        private SessionInfo _sessionInfo;
        private bool _isCapPhatToanDonVi;

        public delegate void DataChangedEventHandler(object sender, EventArgs e);
        public event DataChangedEventHandler UpdateParentWindowEventHandler;
        public override Type ContentType => typeof(ThamDinhQuyetToanDetail);
        public override PackIconKind IconKind => PackIconKind.FileDocumentBoxMultiple;
        public bool IsSaveData;
        public bool IsDelete => _selectedTypeShowAgency != null && _selectedTypeShowAgency.ValueItem == TypeDisplay.TONG_DONVI ? false : (SelectedItem != null ? true : false);
        public bool IsDeleteAll => Items.Any(item => !item.IsModified);
        public bool IsReadOnlyGrid => (IsShowTypeAgency && SelectedTypeShowAgency != null && SelectedTypeShowAgency.ValueItem == TypeDisplay.TONG_DONVI) ? true : false;
        public bool IsTongHop => !string.IsNullOrEmpty(Model.STongHop);
        public Visibility VisibleColAgency => (IsShowTypeAgency && SelectedTypeShowAgency != null && SelectedTypeShowAgency.ValueItem == TypeDisplay.TONG_DONVI) ?
            Visibility.Collapsed : Visibility.Visible;

        public Visibility VisibleVoucherNo => !string.IsNullOrEmpty(Model.STongHop) && VisibleColAgency == Visibility.Visible ? Visibility.Visible : Visibility.Collapsed;

        public bool ReadOnlyCapPhat => IsShowTypeAgency && SelectedTypeShowAgency != null && SelectedTypeShowAgency.ValueItem == TypeDisplay.TONG_DONVI ? true : false;
        public bool ReadOnlyDeNghi => IsTongHop ? true : false;
        public bool IsEditByRole => Model.SNguoiTao == _sessionInfo.Principal;

        private ObservableCollection<BhThamDinhQuyetToanChiTietModel> _lastYearItems;
        public ObservableCollection<BhThamDinhQuyetToanChiTietModel> LastYearItems
        {
            get => _lastYearItems;
            set
            {
                SetProperty(ref _lastYearItems, value);
            }
        }

        //Xác định lần đầu tiên tạo mới
        public bool IsCreate;

        public int NamLamViec { get; set; }

        private bool _isSummaryVoucher;
        public bool IsSummaryVoucher
        {
            get => _isSummaryVoucher;
            set
            {
                SetProperty(ref _isSummaryVoucher, value);
                OnPropertyChanged(nameof(ReadOnlyCapPhat));
                OnPropertyChanged(nameof(ReadOnlyDeNghi));
            }
        }

        private bool _isOpenRefresh;
        public bool IsOpenRefresh
        {
            get => _isOpenRefresh;
            set => SetProperty(ref _isOpenRefresh, value);
        }
        private string _selectedLNS;
        public string SelectedLNS
        {
            get => _selectedLNS;
            set => SetProperty(ref _selectedLNS, value);
        }

        private bool _isOpenLnsPopup;
        public bool IsOpenLnsPopup
        {
            get => _isOpenLnsPopup;
            set
            {
                SetProperty(ref _isOpenLnsPopup, value);
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
                if (_selectedAgency != null)
                {
                    LoadData();
                }
            }
        }

        private string _searchLNS;
        public string SearchLNS
        {
            get => _searchLNS;
            set
            {
                SetProperty(ref _searchLNS, value);
            }
        }

        public Visibility ShowTotal => Items.Count > 0 ? Visibility.Visible : Visibility.Hidden;


        private bool _isShowTypeAgency;
        public bool IsShowTypeAgency
        {
            get => _isShowTypeAgency;
            set => SetProperty(ref _isShowTypeAgency, value);
        }

        private ObservableCollection<ComboboxItem> _typeShowAgency;
        public ObservableCollection<ComboboxItem> TypeShowAgency
        {
            get => _typeShowAgency;
            set => SetProperty(ref _typeShowAgency, value);
        }

        private ComboboxItem _selectedTypeShowAgency;
        public ComboboxItem SelectedTypeShowAgency
        {
            get => _selectedTypeShowAgency;
            set
            {
                if (SetProperty(ref _selectedTypeShowAgency, value))
                {
                    OnPropertyChanged(nameof(ReadOnlyCapPhat));
                    OnPropertyChanged(nameof(ReadOnlyDeNghi));
                    LoadData();
                }
            }
        }

        private string _sNoiDungSearch;
        public string SNoiDungSearch
        {
            get => _sNoiDungSearch;
            set => SetProperty(ref _sNoiDungSearch, value);
        }

        private ObservableCollection<BhThamDinhQuyetToanChiTietModel> _dataPopupSearchItems;
        public ObservableCollection<BhThamDinhQuyetToanChiTietModel> DataPopupSearchItems
        {
            get => _dataPopupSearchItems;
            set => SetProperty(ref _dataPopupSearchItems, value);
        }

        private BhThamDinhQuyetToanChiTietModel _selectedPopupItem;
        public BhThamDinhQuyetToanChiTietModel SelectedPopupItem
        {
            get => _selectedPopupItem;
            set
            {
                SetProperty(ref _selectedPopupItem, value);
                SNoiDungSearch = _selectedPopupItem?.SNoiDung;
                OnPropertyChanged(nameof(SNoiDungSearch));
                IsPopupOpen = false;
            }
        }

        private bool _isPopupOpen;
        public bool IsPopupOpen
        {
            get => _isPopupOpen;
            set => SetProperty(ref _isPopupOpen, value);
        }

        private ObservableCollection<BhThamDinhQuyetToanChiTietModel> _dataSearch;
        public ObservableCollection<BhThamDinhQuyetToanChiTietModel> DataSearch
        {
            get => _dataSearch;
            set => SetProperty(ref _dataSearch, value);
        }

        private ObservableCollection<ComboboxItem> _typeDisplays;
        public ObservableCollection<ComboboxItem> TypeDisplays
        {
            get => _typeDisplays;
            set => SetProperty(ref _typeDisplays, value);
        }

        private string _typeDisplaysselected;
        public string TypeDisplaysSelected
        {
            get => _typeDisplaysselected;
            set
            {
                if (SetProperty(ref _typeDisplaysselected, value) && _itemsView != null && _typeDisplaysselected != TypeDisplay.TAT_CA)
                {

                    if (_typeDisplaysselected != TypeDisplay.SO_LIEU_CHENH_LECH)
                    {
                        OnRefresh();
                        CalculateFilterData();
                        _itemsView.Refresh();
                    }
                    else
                    {
                        //OnRefresh();
                        CalculateFilterData();
                        _itemsView.Refresh();
                    }
                }

                if (_typeDisplaysselected == null || _typeDisplaysselected == TypeDisplay.TAT_CA)
                {
                    OnRefresh();
                    _itemsView.Refresh();
                }
            }
        }

        public bool IsShowAgencyFilter => IsTongHop && _selectedTypeShowAgency?.ValueItem == TypeDisplay.CHITIET_DONVI;

        public RelayCommand PrintCommand { get; }
        public RelayCommand AutoFillDataCommand { get; }
        public new RelayCommand CloseCommand { get; }
        public RelayCommand ClearSearchCommand { get; }
        public RelayCommand SearchCommand { get; }
        public RelayCommand CopyCommand { get; }
        public RelayCommand ExplainCommand { get; }
        public ThamDinhQuyetToanDetailDialogViewModel ThamDinhQuyetToanDetailDialogViewModel { get; }
        public PrintBaoCaoQuyetToanThuViewModel PrintBaoCaoQuyetToanThuViewModel { get; }
        public PrintChiTieuKinhPhiViewModel PrintChiTieuKinhPhiViewModel { get; }
        public PrintQuyetToanChiNamBHXHViewModel PrintQuyetToanChiNamBHXHViewModel { get; }
        public PrintThamDinhTongHopThuChiViewModel PrintThamDinhTongHopThuChiViewModel { get; }
        public PrintThamDinhQuyetToanViewModel PrintThamDinhQuyetToanViewModel { get; }
        public PrintBaoCaoQuyetToanChiKinhPhiQuanLyViewModel PrintBaoCaoQuyetToanChiKinhPhiQuanLyViewModel { get; }

        public ThamDinhQuyetToanDetailViewModel(ICpChungTuService cpChungTuService,
            IMapper mapper,
            ISessionService sessionService,
            INsDonViService nsDonViService,
            IDanhMucService danhMucService,
            ILog logger,
            IBhThamDinhQuyetToanService bhThamDinhQuyetToanService,
            IBhThamDinhQuyetToanChiTietService bhThamDinhQuyetToanChiTietService,
            IBhDmThamDinhQuyetToanService bhDmThamDinhQuyetToanService,
            IBhDmMucLucNganSachService bhDmMucLucNganSachService,
            PrintQuyetToanChiNamBHXHViewModel printQuyetToanChiNamBHXHViewModel,
            PrintThamDinhQuyetToanViewModel printThamDinhQuyetToanViewModel,
            PrintBaoCaoQuyetToanThuViewModel printBaoCaoQuyetToanThuViewModel,
            PrintChiTieuKinhPhiViewModel printChiTieuKinhPhiViewModel,
            PrintThamDinhTongHopThuChiViewModel printThamDinhTongHopThuChiViewModel,
            PrintBaoCaoQuyetToanChiKinhPhiQuanLyViewModel printBaoCaoQuyetToanChiKinhPhiQuanLyViewModel,
            ThamDinhQuyetToanDetailDialogViewModel thamDinhQuyetToanDetailDialogViewModel
            ) : base(sessionService, mapper, logger, nsDonViService, danhMucService)
        {
            _bhThamDinhQuyetToanService = bhThamDinhQuyetToanService;
            _bhThamDinhQuyetToanChiTietService = bhThamDinhQuyetToanChiTietService;
            _bhDmThamDinhQuyetToanService = bhDmThamDinhQuyetToanService;
            _bhDmMucLucNganSachService = bhDmMucLucNganSachService;
            PrintBaoCaoQuyetToanThuViewModel = printBaoCaoQuyetToanThuViewModel;
            PrintChiTieuKinhPhiViewModel = printChiTieuKinhPhiViewModel;
            PrintThamDinhTongHopThuChiViewModel = printThamDinhTongHopThuChiViewModel;
            PrintThamDinhQuyetToanViewModel = printThamDinhQuyetToanViewModel;
            PrintBaoCaoQuyetToanChiKinhPhiQuanLyViewModel = printBaoCaoQuyetToanChiKinhPhiQuanLyViewModel;
            PrintQuyetToanChiNamBHXHViewModel = printQuyetToanChiNamBHXHViewModel;
            ThamDinhQuyetToanDetailDialogViewModel = thamDinhQuyetToanDetailDialogViewModel;
            SaveCommand = new RelayCommand(obj => OnSaveData());
            RefreshCommand = new RelayCommand(obj => OnRefreshAllData());
            CloseCommand = new RelayCommand(obj => OnClose(obj));
            PrintCommand = new RelayCommand(obj => OnPrintDetal(obj));
            ClearSearchCommand = new RelayCommand(OnClearSearch);
            SearchCommand = new RelayCommand(OnSearch);
            CopyCommand = new RelayCommand(obj => OnCopy());
            ExplainCommand = new RelayCommand(obj => OnExplainDialog());
        }

        public override void Init()
        {
            try
            {
                MarginRequirement = new System.Windows.Thickness(10);
                _sessionInfo = _sessionService.Current;
                NamLamViec = _sessionService.Current.YearOfWork;
                IsSummaryVoucher = false;
                IsShowTypeAgency = false;
                _selectedAgency = null;
                LoadComboboxTypeShow();
                if (!string.IsNullOrEmpty(Model.STongHop))
                {
                    IsShowTypeAgency = true;
                    IsSummaryVoucher = true;
                    if (!IsEditByRole)
                        MessageBoxHelper.Info(string.Format(Resources.AlertRoleEditDetail, Model.SNguoiTao));
                    OnPropertyChanged(nameof(ReadOnlyCapPhat));
                    OnPropertyChanged(nameof(ReadOnlyDeNghi));
                    OnPropertyChanged(nameof(IsDeleteAll));
                    OnPropertyChanged(nameof(IsShowTypeAgency));
                    OnPropertyChanged(nameof(VisibleColAgency));
                    OnPropertyChanged(nameof(VisibleVoucherNo));
                }
                LoadTypeDisplay();
                LoadData();
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        private void LoadTypeDisplay()
        {
            TypeDisplays = new ObservableCollection<ComboboxItem>();
            TypeDisplays.Add(new ComboboxItem { ValueItem = TypeDisplay.TAT_CA, DisplayItem = "Tất cả" });
            TypeDisplays.Add(new ComboboxItem { ValueItem = TypeDisplay.SO_LIEU_BAO_CAO, DisplayItem = "Số liệu báo cáo" });
            TypeDisplays.Add(new ComboboxItem { ValueItem = TypeDisplay.SO_LIEU_THAM_DINH, DisplayItem = "Số liệu thẩm định" });
            TypeDisplays.Add(new ComboboxItem { ValueItem = TypeDisplay.SO_LIEU_CHENH_LECH, DisplayItem = "Số liệu chênh lệch" });
            TypeDisplays.Add(new ComboboxItem { ValueItem = TypeDisplay.SO_LIEU_BC_TD, DisplayItem = "Số liệu báo cáo và thẩm định" });
            TypeDisplaysSelected = TypeDisplay.TAT_CA;
        }

        private bool ChungTuChiTietItemsViewFilter(BhThamDinhQuyetToanChiTietModel item)
        {
            bool result = true;

            if (!string.IsNullOrEmpty(TypeDisplaysSelected))
            {
                if (TypeDisplaysSelected == TypeDisplay.SO_LIEU_BAO_CAO)
                    result = result && (item.HasReportData || item.IMa == 1 || item.IMa == 2);
                else if (TypeDisplaysSelected == TypeDisplay.SO_LIEU_THAM_DINH)
                    result = result && (item.HasEvaluationData || item.IMa == 1 || item.IMa == 2);
                else if (TypeDisplaysSelected == TypeDisplay.SO_LIEU_CHENH_LECH)
                    result = result && (item.HasDiffData || item.IMa == 1 || item.IMa == 2);
                else if (TypeDisplaysSelected == TypeDisplay.SO_LIEU_BC_TD)
                    result = result && (item.HasReportNEvaluateDate || item.IMa == 1 || item.IMa == 2);
            }

            item.IsFilter = result;
            return result;
        }

        protected override void OnSelectedItemChanged()
        {
            base.OnSelectedItemChanged();
            OnPropertyChanged(nameof(IsDelete));
        }

        private void OnPrintDetal(object param)
        {
            int dialogType = (int)param;
            switch (dialogType)
            {
                //case (int)BhQuyeToanChiNamType.PRINT_BAOCAOQUYETTOANCHIBHXH:
                //case (int)BhQuyeToanChiNamType.PRINT_QUYETTOANCHIBHXH:
                //    PrintQuyetToanChiNamBHXHViewModel.SettlementTypeValue = dialogType;
                //    PrintQuyetToanChiNamBHXHViewModel.Init();
                //    var view1 = new PrintQuyetToanChiNam
                //    {
                //        DataContext = PrintQuyetToanChiNamBHXHViewModel
                //    };
                //    DialogHost.Show(view1, SystemConstants.DETAIL_DIALOG, null, null);
                //    break;
                case (int)BhThamDinhQuyetToanType.PRINT_BAO_CAO_QUYET_TOAN_THU_BHXH_BHYT_BHTN:
                    PrintBaoCaoQuyetToanThuViewModel.SettlementTypeValue = dialogType;
                    PrintBaoCaoQuyetToanThuViewModel.IsTypeBHXH = true;
                    PrintBaoCaoQuyetToanThuViewModel.IsTypeBHYT = false;
                    PrintBaoCaoQuyetToanThuViewModel.Init();
                    PrintBaoCaoQuyetToanThuViewModel.ShowDialogHost("DetailDialog");
                    break;
                case (int)BhThamDinhQuyetToanType.PRINT_BAO_CAO_QUYET_TOAN_THU_BHYT_THAN_NHAN:
                    PrintBaoCaoQuyetToanThuViewModel.SettlementTypeValue = dialogType;
                    PrintBaoCaoQuyetToanThuViewModel.IsTypeBHXH = false;
                    PrintBaoCaoQuyetToanThuViewModel.IsTypeBHYT = true;
                    PrintBaoCaoQuyetToanThuViewModel.Init();
                    PrintBaoCaoQuyetToanThuViewModel.ShowDialogHost("DetailDialog");
                    break;
                case (int)BhThamDinhQuyetToanType.PRINT_BAO_CAO_CHI_TIEU_QUYET_TOAN_KINH_PHI_MUA_SAM_TTBYT:
                case (int)BhThamDinhQuyetToanType.PRINT_BAO_CAO_CHI_TIEU_QUYET_TOAN_KINH_PHI_KCB_HSSV_NLD:
                case (int)BhThamDinhQuyetToanType.PRINT_BAO_CAO_CHI_TIEU_QUYET_TOAN_KINH_PHI_KCB_QUAN_Y_DON_VI:
                case (int)BhThamDinhQuyetToanType.PRINT_BAO_CAO_QUYET_TOAN_CHI_CHE_DO_BHXH:
                    PrintChiTieuKinhPhiViewModel.SettlementTypeValue = dialogType;
                    PrintChiTieuKinhPhiViewModel.Init();
                    PrintChiTieuKinhPhiViewModel.ShowDialogHost("DetailDialog");
                    break;
                case (int)BhThamDinhQuyetToanType.PRINT_THONG_BAO_PHE_DUYET_QUYET_TOAN_NAM_TONG_HOP_THU_CHI:
                case (int)BhThamDinhQuyetToanType.PRINT_BAO_CAO_TONG_HOP_QUYET_TOAN_THU_CHI_BHXH_BHYT_BHTN:
                case (int)BhThamDinhQuyetToanType.PRINT_BAO_CAO_DU_TOAN_KINH_PHI_BHXH_BHYT_BHTN_CHUYEN_NAM_SAU:
                    PrintThamDinhTongHopThuChiViewModel.SettlementTypeValue = dialogType;
                    PrintThamDinhTongHopThuChiViewModel.Init();
                    DialogHost.Show(new PrintThamDinhTongHopThuChi() { DataContext = PrintThamDinhTongHopThuChiViewModel }, SettlementScreen.DETAIL_DIALOG, null, null);
                    break;
                case (int)BhThamDinhQuyetToanType.PRINT_BAO_CAO_CHI_TIEU_QUYET_TOAN_KINH_PHI_QUAN_LY_BHXH_BHYT:
                case (int)BhThamDinhQuyetToanType.PRINT_BAO_CAO_CHI_TIEU_QUYET_TOAN_KINH_PHI_KCB_TRUONG_SA_DK:
                    PrintBaoCaoQuyetToanChiKinhPhiQuanLyViewModel.SettlementTypeValue = dialogType;
                    PrintBaoCaoQuyetToanChiKinhPhiQuanLyViewModel.Init();
                    var view1 = new PrintBaoCaoQuyetToanChiKinhPhiQuanLy
                    {
                        DataContext = PrintBaoCaoQuyetToanChiKinhPhiQuanLyViewModel
                    };
                    DialogHost.Show(view1, SettlementScreen.DETAIL_DIALOG, null, null);
                    break;
                case (int)BhThamDinhQuyetToanType.PRINT_BAO_CAO_THAM_DINH_QUYET_TOAN_THU_CHI:
                case (int)BhThamDinhQuyetToanType.PRINT_CAN_CU_TRICH_QUY_BHXH_SANG_DONG_BHYT:
                    PrintThamDinhQuyetToanViewModel.SettlementTypeValue = dialogType;
                    PrintThamDinhQuyetToanViewModel.Init();
                    PrintThamDinhQuyetToanViewModel.ShowDialogHost("DetailDialog");
                    break;
                default:
                    PrintThamDinhQuyetToanViewModel.SettlementTypeValue = dialogType;
                    PrintThamDinhQuyetToanViewModel.Init();
                    PrintThamDinhQuyetToanViewModel.ShowDialogHost("DetailDialog");
                    break;
            }
        }

        private void LoadComboboxTypeShow()
        {
            TypeShowAgency = new ObservableCollection<ComboboxItem>();
            TypeShowAgency.Add(new ComboboxItem { ValueItem = TypeDisplay.TONG_DONVI, DisplayItem = TypeDisplay.TONG_DONVI });
            TypeShowAgency.Add(new ComboboxItem { ValueItem = TypeDisplay.CHITIET_DONVI, DisplayItem = TypeDisplay.CHITIET_DONVI });
            _selectedTypeShowAgency = TypeShowAgency.FirstOrDefault();
            OnPropertyChanged(nameof(SelectedTypeShowAgency));
        }

        public override void LoadData(params object[] args)
        {
            if (IsTongHop && SelectedTypeShowAgency?.ValueItem == TypeDisplay.CHITIET_DONVI && _selectedAgency == null)
            {
                var voucherNos = Model.STongHop.Split(",").ToList();
                var listChungTu = _bhThamDinhQuyetToanService.FindAll(x => x.INamLamViec == _sessionInfo.YearOfWork && voucherNos.Contains(x.SSoChungTu) && x.BDaTongHop);
                string agencyIds = string.Join(",", listChungTu.Select(x => x.IID_MaDonVi));
                var listDonVi = _donViService.FindByListIdDonVi(agencyIds, _sessionInfo.YearOfWork);

                List<BhThamDinhQuyetToanChiTietModel> listChungTuChiTietParent = new List<BhThamDinhQuyetToanChiTietModel>();
                List<BhThamDinhQuyetToanChiTietModel> listChungTuChiTietChildren = new List<BhThamDinhQuyetToanChiTietModel>();
                List<BhThamDinhQuyetToanChiTietModel> temp = new List<BhThamDinhQuyetToanChiTietModel>();
                foreach (var chungTu in listChungTu)
                {
                    var listQuery = _bhThamDinhQuyetToanChiTietService.FindAll(chungTu.Id, YearOfWork, chungTu.IID_MaDonVi).OrderBy(x => x.ISTT);
                    var listModel = _mapper.Map<ObservableCollection<BhThamDinhQuyetToanChiTietModel>>(listQuery);
                    var lstIMas = listModel.Select(x => x.IMaCha).Distinct();
                    listModel.Where(x => lstIMas.Contains(x.IMa)).Select(x => x.IsHangCha = true).ToList();
                    listModel.Where(x => (x.IKieuChu == 3 || x.IKieuChu == 2) && !x.IsHangCha).Select(x => x.STenDonVi = listDonVi.FirstOrDefault(x => x.IIDMaDonVi == chungTu.IID_MaDonVi).TenDonVi).ToList();

                    listChungTuChiTietParent.AddRange(listModel.Where(x => x.IsHangCha || x.IKieuChu == 1));
                    listChungTuChiTietChildren.AddRange(listModel.Where(x => !x.IsHangCha && x.IKieuChu != 1));
                }

                listChungTuChiTietParent = listChungTuChiTietParent.GroupBy(x => x.IMa).Select(x => x.First()).Distinct().ToList();
                temp.AddRange(listChungTuChiTietParent);
                temp.AddRange(listChungTuChiTietChildren);
                temp = temp.OrderBy(x => x.SXauNoiMa).ThenBy(x => x.IID_MaDonVi).ToList();
                Items = _mapper.Map<ObservableCollection<BhThamDinhQuyetToanChiTietModel>>(temp.OrderBy(x => x.ISTT));

                //LoadAgencies(agencyIds);
                _agencies = _mapper.Map<List<ComboboxItem>>(listDonVi);
                OnPropertyChanged(nameof(Agencies));
                OnPropertyChanged(nameof(IsShowAgencyFilter));
            }
            else
            {
                IEnumerable<BhThamDinhQuyetToanChiTietQuery> data = null;
                IEnumerable<BhThamDinhQuyetToanChiTiet> lastYeardata = null;
                if (_selectedAgency != null && SelectedTypeShowAgency.ValueItem == TypeDisplay.CHITIET_DONVI)
                {
                    string sMaDonVi = _selectedAgency.ValueItem;
                    Guid idChungTu = Guid.Empty;
                    var predicateCtDv = PredicateBuilder.True<BhThamDinhQuyetToan>();
                    predicateCtDv = predicateCtDv.And(x => x.INamLamViec == Model.INamLamViec);
                    predicateCtDv = predicateCtDv.And(x => x.IID_MaDonVi == sMaDonVi);

                    var ctDonVi = _bhThamDinhQuyetToanService.FindAll(predicateCtDv).FirstOrDefault();
                    if (ctDonVi != null)
                    {
                        idChungTu = ctDonVi.Id;
                    }
                    data = _bhThamDinhQuyetToanChiTietService.FindAll(idChungTu, YearOfWork, sMaDonVi);
                    lastYeardata = _bhThamDinhQuyetToanChiTietService.FindAllOfLastYear(YearOfWork - 1, sMaDonVi).OrderBy(x => x.IMa);
                }
                else
                {
                    data = _bhThamDinhQuyetToanChiTietService.FindAll(Model.Id, YearOfWork, Model.IID_MaDonVi);
                    lastYeardata = _bhThamDinhQuyetToanChiTietService.FindAllOfLastYear(YearOfWork - 1, Model.IID_MaDonVi).OrderBy(x => x.IMa);
                }

                Items = _mapper.Map<ObservableCollection<BhThamDinhQuyetToanChiTietModel>>(data.OrderBy(x => x.ISTT));
                LastYearItems = _mapper.Map<ObservableCollection<BhThamDinhQuyetToanChiTietModel>>(lastYeardata);
            }


            //DataPopupSearchItems = _mapper.Map<ObservableCollection<BhThamDinhQuyetToanChiTietModel>>(Items);
            //var lstIMa = DataPopupSearchItems.Select(x => x.IMaCha).Distinct();
            //DataPopupSearchItems.Where(x => lstIMa.Contains(x.IMa)).Select(x => x.IsHangCha = true).ToList();

            _itemsView = CollectionViewSource.GetDefaultView(Items);
            _itemsView.Filter = ItemsViewFilter;
            _itemsView.SortDescriptions.Add(new SortDescription(nameof(BhThamDinhQuyetToanChiTietModel.ISTT),
                ListSortDirection.Ascending));
            _itemsView?.Refresh();
            CalculateData();
            CalculateReportData();
            foreach (var item in Items)
            {
                item.IsFilter = true;
                if (!item.IsHangCha)
                {
                    item.PropertyChanged += (sender, args) =>
                    {
                        if (args.PropertyName == nameof(SelectedItem.FSoBaoCao) || args.PropertyName == nameof(SelectedItem.FSoThamDinh)
                        || args.PropertyName == nameof(SelectedItem.FQuanNhan) || args.PropertyName == nameof(SelectedItem.FCNVLDHD)
                        || args.PropertyName == nameof(SelectedItem.SGhiChu))
                        {
                            BhThamDinhQuyetToanChiTietModel item = (BhThamDinhQuyetToanChiTietModel)sender;
                            item.IsModified = true;
                            CalculateData();
                            CalculateReportData();
                        }
                        IsSaveData = true;
                        OnPropertyChanged(nameof(IsSaveData));
                        //OnPropertyChanged(nameof(IsOpenPrintPopup));
                    };
                }
            }
            // Các màn đến ngày thẩm định.. chỉ là title ko nhập dữ liệu
            List<int> lstIMaTitle = new List<int> { 59, 62, 83, 86, 123, 126, 143, 146 };
            Items.Where(x => lstIMaTitle.Contains(x.IMa)).Select(x => x.IsTitle = true).ToList();
        }

        private void OnSaveData()
        {
            if (!IsSaveData)
            {
                return;
            }
            var lstDataAdd = Items.Where(x => !x.IsHangCha && x.Id == Guid.Empty && x.IsModified).ToList();
            var lstDataUpdate = Items.Where(x => !x.IsHangCha && x.Id != Guid.Empty && x.IsModified && !x.IsDeleted).ToList();
            var lstDataDelete = Items.Where(x => !x.IsHangCha && x.IsDeleted && x.IsModified && x.Id != Guid.Empty).ToList();

            var addItemList = new List<BhThamDinhQuyetToanChiTiet>();
            if (lstDataAdd.Count() > 0)
            {
                _mapper.Map(lstDataAdd, addItemList);
                addItemList.Select(x => { x.Id = Guid.NewGuid(); x.IID_BH_TDQT_ChungTu = Model.Id; x.IID_MaDonVi = Model.IID_MaDonVi; return x; }).ToList();
                _bhThamDinhQuyetToanChiTietService.AddRange(addItemList);
                Items.Where(x => !x.IsHangCha && x.IsModified).Select(x => { x.IsModified = false; return x; }).ToList();
            }
            if (lstDataUpdate.Count() > 0)
            {
                _mapper.Map(lstDataUpdate, addItemList);
                addItemList.Select(x => { x.IID_BH_TDQT_ChungTu = Model.Id; x.IID_MaDonVi = Model.IID_MaDonVi; return x; }).ToList();
                _bhThamDinhQuyetToanChiTietService.UpdateRange(addItemList);
                Items.Where(x => !x.IsHangCha && x.IsModified).Select(x => { x.IsModified = false; return x; }).ToList();
            }

            if (lstDataDelete.Count() > 0)
            {
                _mapper.Map(lstDataDelete, addItemList);
                _bhThamDinhQuyetToanChiTietService.RemoveRange(addItemList);
                Items.Where(x => !x.IsHangCha && x.IsModified).Select(x => { x.IsModified = false; x.IsDeleted = false; return x; }).ToList();
            }

            //Update quyết toán chi nam BHXH
            var chungtu = _bhThamDinhQuyetToanService.Find(Model.Id);
            if (chungtu != null)
            {
                chungtu.FSoThamDinh = Model.FSoThamDinh;
                chungtu.FSoBaoCao = Model.FSoBaoCao;
                chungtu.FQuanNhan = Model.FQuanNhan;
                chungtu.FCNVLDHD = Model.FCNVLDHD;
                _bhThamDinhQuyetToanService.Update(chungtu);
            }

            IsSaveData = false;
            LoadData();
            MessageBoxHelper.Info(Resources.MsgSaveDone);
            UpdateParentWindowEventHandler?.Invoke(Model, new EventArgs());
        }

        protected override void OnRefresh()
        {
            IsOpenRefresh = !IsOpenRefresh;
            LoadData();
        }

        private void OnCopy()
        {
            if (Model.IsLocked) return;
            foreach (var item in Items)
            {
                if (item.FSoBaoCao != 0)
                {
                    item.FSoThamDinh = item.FSoBaoCao;
                }
            }
            CalculateData();
        }

        private void OnExplainDialog()
        {
            ThamDinhQuyetToanDetailDialogViewModel.Model = Model;
            ThamDinhQuyetToanDetailDialogViewModel.Init();
            var view1 = new ThamDinhQuyetToanDetailDialog
            {
                DataContext = ThamDinhQuyetToanDetailDialogViewModel
            };
            DialogHost.Show(view1, SystemConstants.DETAIL_DIALOG, null, null);
        }

        private void OnRefreshAllData()
        {
            try
            {
                if (IsSaveData)
                {
                    var result = MessageBoxHelper.ConfirmCancel(Resources.ConfirmReloadData);
                    if (result == MessageBoxResult.Cancel)
                        return;
                    else if (result == MessageBoxResult.Yes)
                        OnSaveData();
                }
                IsCreate = false;
                LoadData();
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        public override void OnClose(object o)
        {
            ((Window)o).Close();
            UpdateParentWindowEventHandler?.Invoke(Model, new EventArgs());
        }

        private void CalculateData()
        {
            var dict = Items.Select(x => x.IMaCha).Distinct();
            Items.Where(x => dict.Contains(x.IMa)).Select(x => x.IsHangCha = true).ToList();
            Items.Where(x => x.IsHangCha)
                .ForAll(x =>
                {
                    x.FSoBaoCao = 0;
                    x.FSoThamDinh = 0;
                    x.FQuanNhan = 0;
                    x.FCNVLDHD = 0;
                });

            var temp = Items.Where(x => !x.IsHangCha && !x.IsDeleted && x.IsFilter).ToList();
            Model.FSoBaoCao = temp.Sum(x => x.FSoBaoCao);
            Model.FSoThamDinh = temp.Sum(x => x.FSoThamDinh);
            Model.FQuanNhan = temp.Sum(x => x.FQuanNhan);
            Model.FCNVLDHD = temp.Sum(x => x.FCNVLDHD);
            CalculateParent(Items);

            Items.Where(x => x.IsHangCha)
            .ForAll(x =>
            {
                x.IID_MaDonVi = string.Empty;
                x.STenDonVi = string.Empty;
            });
        }

        private void CalculateParent(IEnumerable<BhThamDinhQuyetToanChiTietModel> listChild)
        {
            /*if (listChild == null || !listChild.Any()) return;
            var temp = listChild.GroupBy(x => x.IMaCha);
            var listParent = temp.Select(x =>
            {
                var parent = Items.FirstOrDefault(y => !y.IsDeleted && y.IsFilter && y.IMa == x.Key);
                if (parent == null) return null;
                parent.FSoBaoCao += x.Sum(x => x.FSoBaoCao);
                parent.FSoThamDinh += x.Sum(x => x.FSoThamDinh);
                parent.FQuanNhan += x.Sum(x => x.FQuanNhan);
                parent.FCNVLDHD += x.Sum(x => x.FCNVLDHD);
                return parent;
            }).Where(x => x != null).ToList();

            CalculateParent(listParent);*/
            foreach (var item in listChild.OrderByDescending(x => x.IMaCha))
            {
                var parent = Items.FirstOrDefault(y => !y.IsDeleted && y.IsFilter && y.IMa == item.IMaCha);
                if (parent != null)
                {
                    parent.FSoBaoCao += item.FSoBaoCao;
                    parent.FSoThamDinh += item.FSoThamDinh;
                    parent.FQuanNhan += item.FQuanNhan;
                    parent.FCNVLDHD += item.FCNVLDHD;
                };
            }
        }

        #region Search


        private bool ItemsViewFilter(object obj)
        {
            if (!(obj is BhThamDinhQuyetToanChiTietModel temp)) return true;
            bool result = true;
            var item = (BhThamDinhQuyetToanChiTietModel)obj;
            result = ChungTuChiTietItemsViewFilter(item);
            if (!string.IsNullOrEmpty(SNoiDungSearch))
            {
                result = DataSearch.Any(x => x.IMa.Equals(item.IMa));
            }
            return result;
        }

        private void OnSearch(object obj)
        {
            SearchTextFilter();
        }

        private void OnClearSearch(object obj)
        {
            SNoiDungSearch = string.Empty;
            if (!(obj is bool temp))
            {
                _itemsView.Refresh();
            }
        }

        private void SearchTextFilter()
        {
            if (!string.IsNullOrEmpty(SNoiDungSearch))
            {
                List<int> lstResult = new List<int>();
                List<int> lstParents = new List<int>();
                List<BhThamDinhQuyetToanChiTietModel> results = new List<BhThamDinhQuyetToanChiTietModel>();

                List<int> lstSXaNoiMaChildSearch = Items.Where(x => x.SNoiDung.ToLower().Contains(SNoiDungSearch.ToLower()) && !x.IsHangCha).Select(s => s.IMa).Distinct().ToList();
                List<int> lstSXaNoiMaParentSearch = Items.Where(x => x.SNoiDung.ToLower().Contains(SNoiDungSearch.ToLower()) && x.IsHangCha).Select(s => s.IMa).Distinct().ToList();
                List<int> lstPrarentNoSearch = Items.Where(x => x.SNoiDung.ToLower().Contains(SNoiDungSearch.ToLower()) && x.IMaCha == -1).Select(s => s.IMa).Distinct().ToList();
                if (!lstPrarentNoSearch.IsEmpty())
                {
                    lstResult.AddRange(lstPrarentNoSearch);

                }
                if (!lstSXaNoiMaChildSearch.IsEmpty())
                {
                    lstResult.AddRange(lstSXaNoiMaChildSearch);
                    GetListParent(lstSXaNoiMaChildSearch, lstResult);
                }
                if (!lstSXaNoiMaParentSearch.IsEmpty())
                {
                    lstResult.AddRange(lstSXaNoiMaParentSearch);
                    GetListChild(lstSXaNoiMaParentSearch, lstResult);

                }
                if (!lstResult.IsEmpty())
                {
                    results = Items.Where(x => lstResult.Distinct().Contains(x.IMa)).OrderBy(o => o.IMa).ToList();
                }
                DataSearch = new ObservableCollection<BhThamDinhQuyetToanChiTietModel>(results);
            }
            else
            {
                DataSearch = new ObservableCollection<BhThamDinhQuyetToanChiTietModel>();
            }
            _itemsView.Refresh();
        }

        private void GetListChild(List<int> lstInput, List<int> lstResult)
        {
            var itemChild = Items.Where(x => lstInput.Contains(x.IMaCha)).Select(s => s.IMa).Distinct().ToList();
            if (!itemChild.IsEmpty())
            {
                lstResult.AddRange(itemChild);
                foreach (var item in itemChild)
                {
                    GetListChild(new List<int>() { item }, lstResult);
                }
            }
        }

        private void GetListParent(List<int> lstInput, List<int> lstResult)
        {
            var itemParent = Items.Where(x => lstInput.Contains(x.IMa)).Select(x => x.IMaCha).Distinct().ToList();
            if (!itemParent.IsEmpty())
            {
                lstResult.AddRange(itemParent);

                foreach (var item in itemParent)
                {
                    GetListParent(new List<int>() { item }, lstResult);
                }
            }

        }

        #endregion

        private void CalculateFilterData()
        {
            var dict = Items.Select(x => x.IMaCha).Distinct();
            Items.Where(x => dict.Contains(x.IMa)).Select(x => x.IsHangCha = true).ToList();
            Items.Where(x => x.IsHangCha)
                .ForAll(x =>
                {
                    x.FSoBaoCao = 0;
                    x.FSoThamDinh = 0;
                    x.FQuanNhan = 0;
                    x.FCNVLDHD = 0;
                });
            List<BhThamDinhQuyetToanChiTietModel> temp = new List<BhThamDinhQuyetToanChiTietModel>();
            if (!string.IsNullOrEmpty(TypeDisplaysSelected))
            {
                if (TypeDisplaysSelected == TypeDisplay.SO_LIEU_BAO_CAO)
                    temp = Items.Where(x => !x.IsHangCha && !x.IsDeleted && x.IsFilter && x.HasReportData).ToList();
                else if (TypeDisplaysSelected == TypeDisplay.SO_LIEU_THAM_DINH)
                    temp = Items.Where(x => !x.IsHangCha && !x.IsDeleted && x.IsFilter && x.HasEvaluationData).ToList();
                else if (TypeDisplaysSelected == TypeDisplay.SO_LIEU_CHENH_LECH)
                    temp = Items.Where(x => !x.IsHangCha && !x.IsDeleted && x.IsFilter && x.HasDiffData).ToList();
                else if (TypeDisplaysSelected == TypeDisplay.SO_LIEU_BC_TD)
                    temp = Items.Where(x => !x.IsHangCha && !x.IsDeleted && x.IsFilter && x.HasReportNEvaluateDate).ToList();
            }
            var dictByMlns = Items.GroupBy(x => x.IMa).ToDictionary(x => x.Key, x => x.First());
            foreach (var item in temp)
            {
                CalculateFilterParent(item.IMaCha, item, dictByMlns);
            }
        }

        private void CalculateFilterParent(int iMaCha, BhThamDinhQuyetToanChiTietModel item, Dictionary<int, BhThamDinhQuyetToanChiTietModel> dictByMlns)
        {
            if (!dictByMlns.ContainsKey(iMaCha))
            {
                return;
            }

            var model = dictByMlns[iMaCha];
            model.FSoBaoCao += item.FSoBaoCao;
            model.FSoThamDinh += item.FSoThamDinh;
            model.FQuanNhan += item.FQuanNhan;
            model.FCNVLDHD += item.FCNVLDHD;

            CalculateFilterParent(model.IMaCha, item, dictByMlns);
        }

        private void CalculateReportData()
        {
            foreach (var item in Items)
            {
                switch (item.IMa)
                {
                    case 61:
                        var fSoPhaiNop = Items.FirstOrDefault(x => x.IMa == 3).FSoBaoCao;
                        item.FSoBaoCao = fSoPhaiNop != 0 ? Math.Round((Items.FirstOrDefault(x => x.IMa == 60).FSoBaoCao / fSoPhaiNop) * 100, 2, MidpointRounding.ToEven) : 0;
                        break;
                    case 64:
                        var fSoPhaiNop64 = Items.FirstOrDefault(x => x.IMa == 3).FSoBaoCao;
                        item.FSoBaoCao = fSoPhaiNop64 != 0 ? Math.Round((Items.FirstOrDefault(x => x.IMa == 63).FSoBaoCao / fSoPhaiNop64) * 100, 2, MidpointRounding.ToEven) : 0;
                        break;
                    case 65:
                        item.FSoBaoCao = Math.Round((Items.FirstOrDefault(x => x.IMa == 63).FSoBaoCao - Items.FirstOrDefault(x => x.IMa == 3).FSoBaoCao), 2, MidpointRounding.ToEven);
                        break;
                    case 85:
                        var fSoPhaiNop85 = Items.FirstOrDefault(x => x.IMa == 67).FSoBaoCao;
                        item.FSoBaoCao = fSoPhaiNop85 != 0 ? Math.Round((Items.FirstOrDefault(x => x.IMa == 84).FSoBaoCao / fSoPhaiNop85) * 100, 2, MidpointRounding.ToEven) : 0;
                        break;
                    case 88:
                        var fSoPhaiNop88 = Items.FirstOrDefault(x => x.IMa == 67).FSoBaoCao;
                        item.FSoBaoCao = fSoPhaiNop88 != 0 ? Math.Round((Items.FirstOrDefault(x => x.IMa == 87).FSoBaoCao / fSoPhaiNop88) * 100, 2, MidpointRounding.ToEven) : 0;
                        break;
                    case 89:
                        item.FSoBaoCao = Math.Round((Items.FirstOrDefault(x => x.IMa == 87).FSoBaoCao - Items.FirstOrDefault(x => x.IMa == 67).FSoBaoCao), 2, MidpointRounding.ToEven);
                        break;
                    case 125:
                        var fSoPhaiNop125 = Items.FirstOrDefault(x => x.IMa == 91).FSoBaoCao;
                        item.FSoBaoCao = fSoPhaiNop125 != 0 ? Math.Round((Items.FirstOrDefault(x => x.IMa == 124).FSoBaoCao / fSoPhaiNop125) * 100, 2, MidpointRounding.ToEven) : 0;
                        break;
                    case 128:
                        var fSoPhaiNop128 = Items.FirstOrDefault(x => x.IMa == 91).FSoBaoCao;
                        item.FSoBaoCao = fSoPhaiNop128 != 0 ? Math.Round((Items.FirstOrDefault(x => x.IMa == 127).FSoBaoCao / fSoPhaiNop128) * 100, 2, MidpointRounding.ToEven) : 0;
                        break;
                    case 129:
                        item.FSoBaoCao = Math.Round((Items.FirstOrDefault(x => x.IMa == 127).FSoBaoCao - Items.FirstOrDefault(x => x.IMa == 91).FSoBaoCao), 2, MidpointRounding.ToEven);
                        break;
                    case 145:
                        var fSoPhaiNop145 = Items.FirstOrDefault(x => x.IMa == 131).FSoBaoCao;
                        item.FSoBaoCao = fSoPhaiNop145 != 0 ? Math.Round((Items.FirstOrDefault(x => x.IMa == 144).FSoBaoCao / fSoPhaiNop145) * 100, 2, MidpointRounding.ToEven) : 0;
                        break;
                    case 148:
                        var fSoPhaiNop148 = Items.FirstOrDefault(x => x.IMa == 131).FSoBaoCao;
                        item.FSoBaoCao = fSoPhaiNop148 != 0 ? Math.Round((Items.FirstOrDefault(x => x.IMa == 147).FSoBaoCao / fSoPhaiNop148) * 100, 2, MidpointRounding.ToEven) : 0;
                        break;
                    case 149:
                        item.FSoBaoCao = Math.Round((Items.FirstOrDefault(x => x.IMa == 147).FSoBaoCao - Items.FirstOrDefault(x => x.IMa == 131).FSoBaoCao), 2, MidpointRounding.ToEven);
                        break;
                    case 153:
                        item.FSoBaoCao = Math.Round((Items.FirstOrDefault(x => x.IMa == 151).FSoBaoCao - Items.FirstOrDefault(x => x.IMa == 152).FSoBaoCao), 2, MidpointRounding.ToEven);
                        break;
                    case 157:
                        item.FSoBaoCao = Math.Round((Items.FirstOrDefault(x => x.IMa == 155).FSoBaoCao - Items.FirstOrDefault(x => x.IMa == 156).FSoBaoCao), 2, MidpointRounding.ToEven);
                        break;
                    case 161:
                        item.FSoBaoCao = Math.Round((Items.FirstOrDefault(x => x.IMa == 159).FSoBaoCao - Items.FirstOrDefault(x => x.IMa == 160).FSoBaoCao), 2, MidpointRounding.ToEven);
                        break;
                    case 165:
                        item.FSoBaoCao = Math.Round((Items.FirstOrDefault(x => x.IMa == 163).FSoBaoCao - Items.FirstOrDefault(x => x.IMa == 164).FSoBaoCao), 2, MidpointRounding.ToEven);
                        break;
                    case 169:
                        item.FSoBaoCao = Math.Round((Items.FirstOrDefault(x => x.IMa == 167).FSoBaoCao - Items.FirstOrDefault(x => x.IMa == 168).FSoBaoCao), 2, MidpointRounding.ToEven);
                        break;
                    case 173:
                        item.FSoBaoCao = Math.Round((Items.FirstOrDefault(x => x.IMa == 171).FSoBaoCao - Items.FirstOrDefault(x => x.IMa == 172).FSoBaoCao), 2, MidpointRounding.ToEven);
                        break;
                    case 202:
                        item.FSoBaoCao = Math.Round((Items.FirstOrDefault(x => x.IMa == 180).FSoBaoCao - Items.FirstOrDefault(x => x.IMa == 181).FSoBaoCao), 2, MidpointRounding.ToEven);
                        break;
                    case 207:
                        if (LastYearItems.Any() && item.FSoBaoCao == 0)
                            item.FSoBaoCao = Math.Round(((LastYearItems.FirstOrDefault(x => x.IMa == 207)?.FSoThamDinh ?? 0) + (LastYearItems.FirstOrDefault(x => x.IMa == 208)?.FSoThamDinh ?? 0) - (LastYearItems.FirstOrDefault(x => x.IMa == 209)?.FSoThamDinh ?? 0)), 2, MidpointRounding.ToEven) > 0 ? Math.Round(((LastYearItems.FirstOrDefault(x => x.IMa == 207)?.FSoThamDinh ?? 0) + (LastYearItems.FirstOrDefault(x => x.IMa == 208)?.FSoThamDinh ?? 0) - (LastYearItems.FirstOrDefault(x => x.IMa == 209)?.FSoThamDinh ?? 0)), 2, MidpointRounding.ToEven) : 0;
                        break;
                    case 210:
                        item.FSoBaoCao = Math.Round((Items.FirstOrDefault(x => x.IMa == 206).FSoBaoCao - Items.FirstOrDefault(x => x.IMa == 209).FSoBaoCao), 2, MidpointRounding.ToEven);
                        break;
                    case 213:
                        if (LastYearItems.Any() && item.FSoBaoCao == 0)
                            item.FSoBaoCao = Math.Round(((LastYearItems.FirstOrDefault(x => x.IMa == 213)?.FSoThamDinh ?? 0) + (LastYearItems.FirstOrDefault(x => x.IMa == 214)?.FSoThamDinh ?? 0) - (LastYearItems.FirstOrDefault(x => x.IMa == 215)?.FSoThamDinh ?? 0)), 2, MidpointRounding.ToEven) > 0 ? Math.Round(((LastYearItems.FirstOrDefault(x => x.IMa == 213)?.FSoThamDinh ?? 0) + (LastYearItems.FirstOrDefault(x => x.IMa == 214)?.FSoThamDinh ?? 0) - (LastYearItems.FirstOrDefault(x => x.IMa == 215)?.FSoThamDinh ?? 0)), 2, MidpointRounding.ToEven) : 0;
                        break;
                    case 216:
                        item.FSoBaoCao = Math.Round((Items.FirstOrDefault(x => x.IMa == 212).FSoBaoCao - Items.FirstOrDefault(x => x.IMa == 215).FSoBaoCao), 2, MidpointRounding.ToEven);
                        break;
                    case 220:
                        if (LastYearItems.Any() && item.FSoBaoCao == 0)
                            item.FSoBaoCao = Math.Round(((LastYearItems.FirstOrDefault(x => x.IMa == 220)?.FSoBaoCao ?? 0) + (LastYearItems.FirstOrDefault(x => x.IMa == 221)?.FSoBaoCao ?? 0) - (LastYearItems.FirstOrDefault(x => x.IMa == 223)?.FSoBaoCao ?? 0)), 2, MidpointRounding.ToEven);
                        break;
                    case 224:
                        item.FSoBaoCao = Math.Round((Items.FirstOrDefault(x => x.IMa == 222).FSoBaoCao - Items.FirstOrDefault(x => x.IMa == 223).FSoBaoCao), 2, MidpointRounding.ToEven);
                        break;
                    case 225:
                        item.FSoBaoCao = Math.Round((Items.FirstOrDefault(x => x.IMa == 219).FSoBaoCao - Items.FirstOrDefault(x => x.IMa == 223).FSoBaoCao), 2, MidpointRounding.ToEven);
                        break;
                    case 232:
                        item.FSoBaoCao = Math.Round((Items.FirstOrDefault(x => x.IMa == 230).FSoBaoCao - Items.FirstOrDefault(x => x.IMa == 231).FSoBaoCao), 2, MidpointRounding.ToEven);
                        break;
                    case 239:
                        item.FSoBaoCao = Math.Round((Items.FirstOrDefault(x => x.IMa == 237).FSoBaoCao - Items.FirstOrDefault(x => x.IMa == 238).FSoBaoCao), 2, MidpointRounding.ToEven);
                        break;
                    case 240:
                        item.FSoBaoCao = Math.Round((Items.FirstOrDefault(x => x.IMa == 234).FSoBaoCao - Items.FirstOrDefault(x => x.IMa == 238).FSoBaoCao), 2, MidpointRounding.ToEven);
                        break;
                    case 244:
                        item.FSoBaoCao = Math.Round((Items.FirstOrDefault(x => x.IMa == 242).FSoBaoCao - Items.FirstOrDefault(x => x.IMa == 243).FSoBaoCao), 2, MidpointRounding.ToEven);
                        break;
                    case 253:
                        item.FSoBaoCao = Math.Round((Items.FirstOrDefault(x => x.IMa == 249).FSoBaoCao - Items.FirstOrDefault(x => x.IMa == 252).FSoBaoCao), 2, MidpointRounding.ToEven);
                        break;
                    case 254:
                        item.FSoBaoCao = Math.Round((Items.FirstOrDefault(x => x.IMa == 246).FSoBaoCao - Items.FirstOrDefault(x => x.IMa == 252).FSoBaoCao), 2, MidpointRounding.ToEven);
                        break;
                    default:
                        break;
                }

                if (!item.HasData)
                    item.IsModified = false;
            }
        }
    }
}
