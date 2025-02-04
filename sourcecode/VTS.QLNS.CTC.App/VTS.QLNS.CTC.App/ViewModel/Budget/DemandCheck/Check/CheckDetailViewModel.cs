using AutoMapper;
using log4net;
using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Data;
using System.Windows.Forms;
using VTS.QLNS.CTC.App.Command;
using VTS.QLNS.CTC.App.Helper;
using VTS.QLNS.CTC.App.Model;
using VTS.QLNS.CTC.App.Model.Control;
using VTS.QLNS.CTC.App.Properties;
using VTS.QLNS.CTC.App.Service;
using VTS.QLNS.CTC.App.Service.Impl;
using VTS.QLNS.CTC.App.View.Budget.DemandCheck.Check;
using VTS.QLNS.CTC.App.View.Budget.DemandCheck.Check.PrintReport;
using VTS.QLNS.CTC.App.View.Budget.DemandCheck.Distribution;
using VTS.QLNS.CTC.App.View.Budget.DemandCheck.FunctionMap.PrintReport;
using VTS.QLNS.CTC.App.ViewModel.Budget.DemandCheck.Check.ImportCheck;
using VTS.QLNS.CTC.App.ViewModel.Budget.DemandCheck.Check.PrintReport;
using VTS.QLNS.CTC.App.ViewModel.Budget.DemandCheck.Distribution;
using VTS.QLNS.CTC.App.ViewModel.Budget.DemandCheck.PrintReport;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Criteria;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Service;
using VTS.QLNS.CTC.Utility;
using VTS.QLNS.CTC.Utility.Enum;

namespace VTS.QLNS.CTC.App.ViewModel.Budget.DemandCheck.Check
{
    public class CheckDetailViewModel : DetailViewModelBase<NsSktChungTuModel, NsSktChungTuChiTietModel>
    {
        private readonly ISessionService _sessionService;
        private readonly ISktChungTuChiTietService _sktChungTuChiTietService;
        private readonly ISktChungTuService _sktChungTuService;
        private readonly ISktMucLucService _sktMucLucService;
        private readonly IDanhMucService _iDanhMucService;
        private readonly INsMucLucNganSachService _iNsMucLucNganSachService;
        private readonly ISktNganhThamDinhService _sktThamDinhService;
        private readonly ISktNganhThamDinhChiTietService _sktThamDinhChiTietService;
        private readonly INsSktNganhThamDinhChiTietSktService _iSktNganhThamDinhChiTietSktService;
        private readonly ISysAuditLogService _log;
        private readonly IMapper _mapper;
        private ICollectionView _sktChungTuChiTietModelsView;
        private ICollectionView _searchPopupView;
        private SessionInfo _sessionInfo;
        private readonly ILog _logger;

        private Dictionary<Guid, NsSktChungTuChiTietModel> _dicItems;
        private Dictionary<Guid, CanCuDuToanNamTruocSoKiemTraQuery> _dicDataCanDuToan;
        public delegate void DataChangedEventHandler(object sender, EventArgs e);
        public event DataChangedEventHandler UpdateParentWindowEventHandler;

        private NsSktChungTuChiTietModel _nsSktChungTuChiTietSearchModel;
        public NsSktChungTuChiTietModel NsSktChungTuChiTietSearchModel
        {
            get => _nsSktChungTuChiTietSearchModel;
            set => SetProperty(ref _nsSktChungTuChiTietSearchModel, value);
        }

        private ObservableCollection<SktMucLucModel> _sktMucLucModelItems;
        public ObservableCollection<SktMucLucModel> SktMucLucModelItems
        {
            get => _sktMucLucModelItems;
            set => SetProperty(ref _sktMucLucModelItems, value);
        }

        private Dictionary<Guid, List<NsSktChungTuChiTietModel>> DicChungTu { get; set; }
        private Dictionary<Guid, List<NsSktChungTuChiTietModel>> DicChungTu2 { get; set; }

        private bool _isLock;
        public bool IsLock
        {
            get => _isLock;
            set => SetProperty(ref _isLock, value);
        }

        public Visibility ShowColNSBD { get; set; }
        public Visibility ShowColNSSD { get; set; }

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

        private SktMucLucModel _selectedPopupItem;
        public SktMucLucModel SelectedPopupItem
        {
            get => _selectedPopupItem;
            set
            {
                SetProperty(ref _selectedPopupItem, value);
                NsSktChungTuChiTietSearchModel.SKyHieu = _selectedPopupItem?.SKyHieu;
                OnPropertyChanged(nameof(NsSktChungTuChiTietSearchModel));
                IsPopupOpen = false;
            }
        }

        private double _fTongHuyDongTonKho;
        public double FTongHuyDongTonKho
        {
            get => _fTongHuyDongTonKho;
            set => SetProperty(ref _fTongHuyDongTonKho, value);
        }

        public bool IsSaveData => Items.Any(item => item.IsModified || item.IsDeleted);
        public bool IsDeleteAll => !IsLock && Items.Any(item => !item.IsModified && item.HasData);

        private ComboboxItem _selectedDataState;
        public ComboboxItem SelectedDataState
        {
            get => _selectedDataState;
            set
            {
                SetProperty(ref _selectedDataState, value);
                OnSearch();
            }
        }

        private ObservableCollection<ComboboxItem> _dataStateItems;
        public ObservableCollection<ComboboxItem> DataStateItems
        {
            get => _dataStateItems;
            set => SetProperty(ref _dataStateItems, value);
        }

        private DanhMucNganhModel _selectedNNganhModel;
        public DanhMucNganhModel SelectedNNganhModel
        {
            get => _selectedNNganhModel;
            set
            {
                SetProperty(ref _selectedNNganhModel, value);
                OnSearch();
            }
        }

        private ObservableCollection<NsSktNganhThamDinhChiTietSktModel> _itemsThamDinhSkt;
        public ObservableCollection<NsSktNganhThamDinhChiTietSktModel> ItemsThamDinhSkt
        {
            get => _itemsThamDinhSkt;
            set
            {
                if (SetProperty(ref _itemsThamDinhSkt, value))
                {
                }
            }
        }

        public string MainHeader => "SỐ KIỂM TRA - CHỨNG TỪ CHI TIẾT";
        public string SoNhuCau => "Số nhu cầu năm " + (_sessionInfo.YearOfWork);
        public string SoNhuCauMHHV => "Số nhu cầu năm " + (_sessionInfo.YearOfWork);
        public string SoNhuCauDT => "Số nhu cầu đặc thù năm " + (_sessionInfo.YearOfWork);
        public string SoKiemTraNamTruoc => "Số kiểm tra năm " + (_sessionInfo.YearOfWork - 1);
        public string DuToan => "Dự toán năm " + (_sessionInfo.YearOfWork - 1);
        public string DuToanMHCHV => "Dự toán năm " + (_sessionInfo.YearOfWork - 1);
        public string DuToanDT => "Dự toán đặc thù năm " + (_sessionInfo.YearOfWork - 1);

        private bool _isPopupOpen;
        public bool IsPopupOpen
        {
            get => _isPopupOpen;
            set
            {
                SetProperty(ref _isPopupOpen, value);
            }
        }
        public DateTime DtNow => DateTime.Now;
        public int NamLamViec { get; set; }
        public bool IsInit { get; set; } = false;

        public override Type ContentType => typeof(CheckDetail);

        public RelayCommand SearchCommand { get; }
        public RelayCommand ClearSearchCommand { get; }
        public RelayCommand PrintCommand { get; }
        public RelayCommand GetDataNganhThamDinh { get; }
        public RelayCommand CopySoNhuCau { get; }
        public RelayCommand ImportDataCommand { get; }
        public RelayCommand CopySoDuToan { get; }
        public DistributionDetailViewModel DistributionDetailViewModel { get; }
        public PrintReportReceiveTheCheckNumberViewModel PrintReportReceiveTheCheckNumberViewModel { get; }
        public CheckExpertiseDetailViewModel CheckExpertiseDetailViewModel { get; }
        public PrintReportDemandOrgViewModel PrintReportDemandOrgViewModel { get; set; }
        public ImportCheckDetailViewModel ImportCheckDetailViewModel { get; }

        public CheckDetailViewModel(
            ISktChungTuChiTietService sktChungTuChiTietService,
            ISktChungTuService sktChungTuService,
            ISktMucLucService sktMucLucService,
            IDanhMucService iDanhMucService,
            INsMucLucNganSachService iMucLucNganSachService,
            ISktNganhThamDinhService sktThamDinhService,
            ISktNganhThamDinhChiTietService sktThamDinhChiTietService,
            INsSktNganhThamDinhChiTietSktService iSktNganhThamDinhChiTietSktService,
            ISysAuditLogService log,
            ILog logger,
            IMapper mapper,
            ISessionService sessionService,
            DistributionDetailViewModel distributionDetailViewModel,
            CheckExpertiseDetailViewModel checkExpertiseDetailViewModel,
            PrintReportReceiveTheCheckNumberViewModel printReportReceiveTheCheckNumberViewModel,
            PrintReportDemandOrgViewModel printReportDemandOrgViewModel,
            ImportCheckDetailViewModel importCheckDetailViewModel)
        {
            _sktChungTuChiTietService = sktChungTuChiTietService;
            _sktChungTuService = sktChungTuService;
            _sktMucLucService = sktMucLucService;
            _iDanhMucService = iDanhMucService;
            _iNsMucLucNganSachService = iMucLucNganSachService;
            _sktThamDinhService = sktThamDinhService;
            _sktThamDinhChiTietService = sktThamDinhChiTietService;
            _iSktNganhThamDinhChiTietSktService = iSktNganhThamDinhChiTietSktService;
            _log = log;
            _logger = logger;
            _mapper = mapper;
            _sessionService = sessionService;
            NsSktChungTuChiTietSearchModel = new NsSktChungTuChiTietModel();

            DistributionDetailViewModel = distributionDetailViewModel;
            CheckExpertiseDetailViewModel = checkExpertiseDetailViewModel;
            PrintReportReceiveTheCheckNumberViewModel = printReportReceiveTheCheckNumberViewModel;
            PrintReportDemandOrgViewModel = printReportDemandOrgViewModel;
            ImportCheckDetailViewModel = importCheckDetailViewModel;

            ImportCheckDetailViewModel.ParentPage = this;

            SearchCommand = new RelayCommand(o => OnSearch());
            ClearSearchCommand = new RelayCommand(o => OnClearSearch());
            PrintCommand = new RelayCommand(OnPrint);
            GetDataNganhThamDinh = new RelayCommand(obj => LayDuLieuNganhThamDinh());
            CopySoNhuCau = new RelayCommand(obj => CopySoNhuCauSangTuChi());
            ImportDataCommand = new RelayCommand(obj => OnImportData());
            CopySoDuToan = new RelayCommand(obj => OnCopyEstimateData());
        }

        public override void Init()
        {
            base.Init();
            OnClearSearch();
            _sessionInfo = _sessionService.Current;
            NamLamViec = _sessionService.Current.YearOfWork;
            if (Model != null) IsLock = Model.BKhoa;
            LoadDataState();
            LoadData();
            LoadPopupData();
            LoadDataChiTietThongBaoDv();
            ImportCheckDetailViewModel.UpdateParentWindowEventHandler += SelfRefresh;

        }

        private void OnClearSearch()
        {
            NsSktChungTuChiTietSearchModel = new NsSktChungTuChiTietModel();
            if (_sktChungTuChiTietModelsView != null)
            {
                _sktChungTuChiTietModelsView.Refresh();
            }
        }

        private void OnSearch()
        {
            _sktChungTuChiTietModelsView?.Refresh();
            if (Items != null)
            {
                CalculateData();
                UpdateTongTangGiam();
            }
            _sktChungTuChiTietModelsView?.Refresh();
        }

        /// <summary>
        ///     Mở màn hình in
        /// </summary>
        /// <param name="param"></param>
        private void OnPrint(object param)
        {
            var dialogType = (int)param;
            switch (dialogType)
            {
                case (int)DemandCheckPrintType.THE_REPORT_RECEIVES_THE_CHECK_NUMBER:
                    PrintReportReceiveTheCheckNumberViewModel._demandCheckPrintType =
                        DemandCheckPrintType.THE_REPORT_RECEIVES_THE_CHECK_NUMBER;
                    PrintReportReceiveTheCheckNumberViewModel.Init();
                    var view1 = new PrintReportReceiveTheCheckNumber
                    {
                        DataContext = PrintReportReceiveTheCheckNumberViewModel
                    };
                    DialogHost.Show(view1, DemandCheckScreen.DETAIL_DIALOG, null, null);
                    break;
                case (int)DemandCheckPrintType.SUMMARY_REPORT_OF_TEST_NUMBER_ALLOCATION:
                    var view2 = new PrintCommunicateSettlementLNS();
                    //show the dialog
                    DialogHost.Show(view2, DemandCheckScreen.DETAIL_DIALOG, null, null);
                    break;
                case (int)DemandCheckPrintType.REPORT_CHI_TIET_NHAN_SO_KIEM_TRA_THEO_NGANH:
                    var demandCheckPrintType = (DemandCheckPrintType)(int)param;
                    object content;
                    PrintReportDemandOrgViewModel.DemandCheckPrintType = demandCheckPrintType;
                    PrintReportDemandOrgViewModel.Init();
                    content = new PrintReportDemandOrg
                    {
                        DataContext = PrintReportDemandOrgViewModel
                    };
                    DialogHost.Show(content, DemandCheckScreen.DETAIL_DIALOG, null, null);
                    break;
            }
        }

        public override void OnClose(object o)
        {
            ((Window)o).Close();
            UpdateParentWindowEventHandler?.Invoke(Model, new EventArgs());
        }

        protected override void OnDelete()
        {
            if (SelectedItem == null) return;
            SelectedItem.IsDeleted = !SelectedItem.IsDeleted;
            CalculateData();
            OnPropertyChanged(nameof(IsSaveData));
        }

        protected override void OnDeleteAll()
        {
            base.OnDeleteAll();
            var result = MessageBoxHelper.Confirm(Resources.DeleteAllChungTuChiTiet);
            if (result == MessageBoxResult.No)
                return;
            else if (result == MessageBoxResult.Yes)
            {
                var lstItemFilter = Items.Where(x => x.IsFilter
                                                     && !x.IsHangCha
                                                     && (x.FTuChi != 0 || x.FTuChiDeNghi != 0 || x.FHuyDongTonKho != 0 || x.FMuaHangCapHienVat != 0
                                                         || x.FPhanCap != 0 || x.FTonKhoDenNgay != 0)).ToList();
                //xóa chứng từ chi tiết
                DeleteChiTiet(lstItemFilter);
                LoadData();
                UpdateDemandVoucherTotal();
                MessageBoxHelper.Info(Resources.MsgDeleteSuccess);
                OnPropertyChanged(nameof(IsDeleteAll));
            }
        }

        protected override void OnSelectionDoubleClick(object obj)
        {
            base.OnSelectionDoubleClick(obj);
            //OpenDetailDialog();
            OpenExpertiseDetail();
        }

        public void OpenExpertiseDetail()
        {
            try
            {
                if (Model.ILoaiChungTu.GetValueOrDefault().Equals(int.Parse(VoucherType.NSSD_Key)) || SelectedItem == null || SelectedItem != null && SelectedItem.IsHangCha)
                {
                    return;
                }
                var lstItemsThamDinh = ItemsThamDinhSkt != null ? new ObservableCollection<NsSktNganhThamDinhChiTietSktModel>(ItemsThamDinhSkt.Where(x => x.IIdMucLuc.Equals(SelectedItem.IIdMlskt))) : null;
                CheckExpertiseDetailViewModel.Model = new ExpertiseModel();
                CheckExpertiseDetailViewModel.DataThamDinh = lstItemsThamDinh;
                CheckExpertiseDetailViewModel.IdMucLucSeleted = SelectedItem.IIdMlskt;
                CheckExpertiseDetailViewModel.PhanLoai = 2;

                var view = new CheckExpertiseDetail
                {
                    DataContext = CheckExpertiseDetailViewModel
                };
                view.SavedAction = obj =>
                {
                    var data = (List<NsSktNganhThamDinhChiTietSktModel>)obj;
                    var idMlskt = data.First().IIdMucLuc;
                    if (Items.Any())
                    {
                        var it = Items.FirstOrDefault(x => x.IIdMlskt.Equals(idMlskt));
                        if (it != null)
                        {
                            it.FThongBaoDonVi = data.Sum(x => x.FTuChi.GetValueOrDefault());
                            it.IsModified = true;
                        }
                    }
                    AddItemToItemsThamDinh(data.ToList());
                    CalculateData();
                    view.Close();
                };
                CheckExpertiseDetailViewModel.CheckExpertiseDetail = view;
                CheckExpertiseDetailViewModel.Init();
                view.ShowDialog();
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        private void OpenDetailDialog()
        {
            /// don vi cha
            var idChungTu = Guid.Empty;
            if (SelectedItem != null && SelectedItem.IsHangCha)
            {
                return;
            }

            var ctChiTiet = _sktChungTuChiTietService.FindByCondition(item => item.Id.Equals(SelectedItem.Id)).FirstOrDefault();
            if (ctChiTiet == null || ctChiTiet.FTuChi == 0
                && ctChiTiet.FHuyDongTonKho == 0
                && ctChiTiet.FMuaHangCapHienVat == 0
                && ctChiTiet.FPhanCap == 0)
            {
                return;
            }

            if (SelectedItem != null)
            {
                idChungTu = SelectedItem.IIdCtsoKiemTra;
            }
            var predicate = PredicateBuilder.True<NsSktChungTu>().And(x => x.Id.Equals(idChungTu));
            NsSktChungTu nsSktChungTu = _sktChungTuService.FindByCondition(predicate).FirstOrDefault();
            NsSktChungTuModel nsSktChungTuModel = _mapper.Map<NsSktChungTuModel>(nsSktChungTu);
            if (nsSktChungTu != null)
            {
                DistributionDetailViewModel.Model = nsSktChungTuModel.Clone();
                DistributionDetailViewModel.ModelFromCheck = SelectedItem.Clone();
                DistributionDetailViewModel.ShowColNSBD = nsSktChungTuModel.ILoaiChungTu.Equals(int.Parse(VoucherType.NSBD_Key)) ? Visibility.Visible : Visibility.Collapsed;
                DistributionDetailViewModel.ShowColNSSD = nsSktChungTuModel.ILoaiChungTu.Equals(int.Parse(VoucherType.NSSD_Key)) ? Visibility.Visible : Visibility.Collapsed;
                DistributionDetailViewModel.Init();
                DistributionDetailViewModel.UpdateParentWindowEventHandler += SelfRefresh;
                var view = new DistributionDetail() { DataContext = DistributionDetailViewModel };
                view.ShowDialog();
            }
        }

        private void SelfRefresh(object sender, EventArgs e)
        {
            OnRefresh();
        }

        public override void OnSave()
        {
            Func<NsSktChungTuChiTietModel, bool> isAdd = x => x.IsModified && !x.IsDeleted && x.IsAdd && !x.IsHangCha;
            Func<NsSktChungTuChiTietModel, bool> isUpdate = x => x.IsModified && !x.IsDeleted && !x.IsAdd && !x.IsHangCha;
            Func<NsSktChungTuChiTietModel, bool> isDelete = x => x.IsDeleted && !x.IsHangCha;

            var detailsAdd = Items.Where(isAdd).ToList();
            var detailsUpdate = Items.Where(isUpdate).ToList();
            var detailsDelete = Items.Where(isDelete).ToList();

            //save số liệu chi tiết thông tri thẩm định
            //SaveThongTriThamDinh();

            //update số liệu chứng từ
            UpdateDemandVoucherTotal();

            //thêm mới chứng từ chi tiết
            if (detailsAdd.Count > 0)
            {
                var addItems = new List<NsSktChungTuChiTiet>();
                _mapper.Map(detailsAdd, addItems);
                _sktChungTuChiTietService.AddRange(addItems);
                _log.WriteLog(Resources.ApplicationName, "Chi tiết số kiểm tra", (int)TypeExecute.Insert, DtNow, TransactionStatus.Success, _sessionService.Current.Principal);
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
                    var sktChungTuChiTiet = _sktChungTuChiTietService.FindById(updateItem.Id);
                    if (sktChungTuChiTiet != null)
                    {
                        _mapper.Map(updateItem, sktChungTuChiTiet);
                        if (IsUpdateToDelete(sktChungTuChiTiet))
                        {
                            detailsDelete.Add(updateItem);
                        }
                        else
                        {
                            _sktChungTuChiTietService.Update(sktChungTuChiTiet);
                        }
                    }
                    _log.WriteLog(Resources.ApplicationName, "Chi tiết số kiểm tra", (int)TypeExecute.Update, DtNow, TransactionStatus.Success, _sessionService.Current.Principal);
                    updateItem.IsModified = false;
                }
            }

            //xóa chứng từ chi tiết
            if (detailsDelete.Count > 0)
            {
                foreach (var item in detailsDelete)
                {
                    var deleteItem = _sktChungTuChiTietService.FindById(item.Id);
                    if (deleteItem != null)
                    {
                        _mapper.Map(item, deleteItem);
                        _sktChungTuChiTietService.Delete(deleteItem);
                        _log.WriteLog(Resources.ApplicationName, "Chi tiết số kiểm tra", (int)TypeExecute.Delete, DtNow, TransactionStatus.Success, _sessionService.Current.Principal);
                    }
                    item.FTuChi = 0;
                    item.FHuyDongTonKho = 0;
                    item.FMuaHangCapHienVat = 0;
                    item.FPhanCap = 0;
                    item.IsModified = false;
                    item.IsDeleted = false;
                }
            }

            OnPropertyChanged(nameof(IsSaveData));
            OnPropertyChanged(nameof(IsDeleteAll));
            var message = Resources.MsgSaveDone;
            var messageBox = new NSMessageBoxViewModel(message);
            DialogHost.Show(messageBox.Content, DemandCheckScreen.DETAIL_DIALOG);
        }

        public void UpdateDemandVoucherTotal()
        {
            if (Model != null)
            {
                var chungTuUpdate = _sktChungTuService.FindById(Model.Id);
                if (chungTuUpdate != null)
                {
                    chungTuUpdate.FTongTuChi =
                        Items.Where(item => !item.IsHangCha && !item.IsDeleted).Sum(item => item.FTuChi);
                    chungTuUpdate.FTongPhanCap =
                        Items.Where(item => !item.IsHangCha && !item.IsDeleted).Sum(item => item.FPhanCap);
                    chungTuUpdate.FTongMuaHangCapHienVat =
                        Items.Where(item => !item.IsHangCha && !item.IsDeleted).Sum(item => item.FMuaHangCapHienVat);
                    _sktChungTuService.Update(chungTuUpdate);
                    _log.WriteLog(Resources.ApplicationName, "Chi tiết số kiểm tra", (int)TypeExecute.Update, DtNow, TransactionStatus.Success, _sessionService.Current.Principal);
                }
            }
        }

        public void SaveThongTriThamDinh()
        {
            if (ItemsThamDinhSkt != null && ItemsThamDinhSkt.Count > 0)
            {
                List<NsSktNganhThamDinhChiTietSkt> lstEntities =
                    _mapper.Map<List<NsSktNganhThamDinhChiTietSkt>>(ItemsThamDinhSkt.Where(x => x.IsModified));
                foreach (var it in lstEntities)
                {
                    if (it.Id.Equals(Guid.Empty))
                    {
                        _iSktNganhThamDinhChiTietSktService.Add(it);
                    }
                    else
                    {
                        _iSktNganhThamDinhChiTietSktService.Update(it);
                    }
                }
            }
        }

        protected override void OnLockUnLock()
        {
            var message = string.Empty;
            message = IsLock ? Resources.UnlockChungTu : Resources.LockChungTu;
            MessageBoxResult dialogResult = MessageBoxHelper.Confirm(message);
            if (dialogResult == MessageBoxResult.Yes)
            {
                LockOrUnLockDetail();
            }
        }

        private void LockOrUnLockDetail()
        {
            string msgDone = IsLock ? Resources.MsgUnLockDone : Resources.MsgLockDone;
            IsLock = !IsLock;
            _sktChungTuService.LockOrUnlock(Model.Id, IsLock);
            MessageBoxHelper.Info(msgDone);
            OnPropertyChanged(nameof(IsDeleteAll));
            _log.WriteLog(Resources.ApplicationName, "Chi tiết số kiểm tra", (int)TypeExecute.Update, DtNow, TransactionStatus.Success, _sessionService.Current.Principal);
            UpdateParentWindowEventHandler?.Invoke(Model, new EventArgs());
        }

        protected override void OnRefresh()
        {
            LoadData();
        }

        private void LoadDataState()
        {
            DataStateItems = new ObservableCollection<ComboboxItem>
            {
                new ComboboxItem()
                {
                    ValueItem = DataStateValue.HIEN_THI_TAT_CA.ToString(),
                    DisplayItem = DataStateName.HIEN_THI_TAT_CA,
                },
                new ComboboxItem()
                {
                    ValueItem = DataStateValue.DA_NHAP_SKT.ToString(), DisplayItem = DataStateName.DA_NHAP_SKT,
                }
            };
            SelectedDataState = DataStateItems[0];
        }

        public override void LoadData(params object[] args)
        {
            var loaiChungTu = Model.ILoaiChungTu;
            SktChungTuChiTietCriteria searchCondition = new SktChungTuChiTietCriteria();
            searchCondition.NamLamViec = _sessionInfo.YearOfWork;
            searchCondition.NamNganSach = _sessionInfo.YearOfBudget;
            searchCondition.NguonNganSach = _sessionInfo.Budget;
            searchCondition.ITrangThai = StatusType.ACTIVE;
            searchCondition.SktChungTuId = Model.Id;
            searchCondition.ILoai = Model.ILoai;
            searchCondition.ILoaiNguonNganSach = Model.ILoaiNguonNganSach.GetValueOrDefault(1);
            searchCondition.IdDonVi = Model.IIdMaDonVi;
            searchCondition.CurrentIdDonVi = _sessionInfo.IdDonVi;
            searchCondition.LoaiChungTu = Model.ILoaiChungTu.GetValueOrDefault(-1);
            searchCondition.UserName = _sessionInfo.Principal;
            var temp = _sktChungTuChiTietService.FindByConditionForChildUnit(searchCondition).OrderBy(t => t.SSttbc).ToList();
            Items = _mapper.Map<ObservableCollection<NsSktChungTuChiTietModel>>(temp);
            _sktChungTuChiTietModelsView = CollectionViewSource.GetDefaultView(Items);
            //them can cu du toan
            AddCanCuDuToanNamTruoc();
            foreach (var sktChungTuChiTietModel in Items)
            {
                sktChungTuChiTietModel.ILoaiChungTu = Model.ILoaiChungTu;
                sktChungTuChiTietModel.IsFilter = true;
                if (!sktChungTuChiTietModel.IsHangCha)
                {
                    sktChungTuChiTietModel.PropertyChanged += ((sender, args) =>
                    {
                        if (args.PropertyName == nameof(SelectedItem.FHuyDongTonKho) ||
                            args.PropertyName == nameof(SelectedItem.FTuChi) ||
                            args.PropertyName == nameof(SelectedItem.SoNhuCau) ||
                            args.PropertyName == nameof(SelectedItem.FMuaHangCapHienVat) ||
                            args.PropertyName == nameof(SelectedItem.FPhanCap))
                        {
                            NsSktChungTuChiTietModel item = (NsSktChungTuChiTietModel)sender;
                            item.IsModified = true;
                            if (!IsInit)
                            {
                                CalculateData();
                                UpdateTongTangGiam();
                            }
                            OnPropertyChanged(nameof(IsSaveData));
                        }
                    });
                }
            }
            CalculateData();
            UpdateTongTangGiam();
            _sktChungTuChiTietModelsView.Filter = SktChungTuChiTietFilter;
            DicChungTu = Items.GroupBy(n => n.IdParent).ToDictionary(n => n.Key, n => n.ToList());
            DicChungTu2 = new Dictionary<Guid, List<NsSktChungTuChiTietModel>>();
            Items.ForAll(x =>
            {
                if (x.IsHangCha)
                    DicChungTu2.Add(x.IIdMlskt, FindListChild(x));
            });
        }

        private void CalculateData()
        {
            Items.Where(x => x.IsHangCha)
                .Select(x =>
                {
                    x.FTuChi = 0;
                    x.FHuyDongTonKho = 0;
                    x.FMuaHangCapHienVat = 0;
                    x.FPhanCap = 0;
                    x.SoNhuCau = 0;
                    x.SoNhuCauMHHV = 0;
                    x.SoNhuCauDT = 0;
                    x.SoKiemTra = 0;
                    x.SoKiemTraMHHV = 0;
                    x.SoKiemTraDT = 0;
                    x.DuToan = 0;
                    x.DuToanMHCHV = 0;
                    x.DuToanDT = 0;
                    x.FThongBaoDonVi = 0;
                    return x;
                }).ToList();
            _dicItems = Items.ToDictionary(key => key.IIdMlskt, value => value);
            var temp = Items.Where(x => !x.IsHangCha && !x.IsDeleted && x.IsFilter);
            foreach (var item in temp)
            {
                CalculateParent(item.IdParent, item);
            }

            UpdateTotal();
        }

        private void CalculateParent(Guid idParent, NsSktChungTuChiTietModel item)
        {
            //var model = Items.FirstOrDefault(x => x.IIdMlskt == idParent);
            var model = _dicItems.ContainsKey(idParent) ? _dicItems[idParent] : null;
            if (model == null) return;
            model.FTuChi += item.FTuChi;
            model.FHuyDongTonKho += item.FHuyDongTonKho;
            model.FMuaHangCapHienVat += item.FMuaHangCapHienVat;
            model.FPhanCap += item.FPhanCap;
            model.SoNhuCau += item.SoNhuCau;
            model.SoNhuCauMHHV += item.SoNhuCauMHHV;
            model.SoNhuCauDT += item.SoNhuCauDT;
            model.SoKiemTra += item.SoKiemTra;
            model.SoKiemTraMHHV += item.SoKiemTraMHHV;
            model.SoKiemTraDT += item.SoKiemTraDT;
            model.DuToan += item.DuToan;
            model.DuToanMHCHV += item.DuToanMHCHV;
            model.DuToanDT += item.DuToanDT;
            model.FThongBaoDonVi += item.FThongBaoDonVi;
            CalculateParent(model.IdParent, item);
        }

        private void CalculateDataCanCuDuToanNamTruoc(List<CanCuDuToanNamTruocSoKiemTraQuery> items)
        {
            items.Where(x => x.BHangCha.GetValueOrDefault()).ToList();
            var temp = items.Where(x => !x.BHangCha.GetValueOrDefault());
            _dicDataCanDuToan = items.ToDictionary(key => key.IdMlns, value => value);
            foreach (var item in temp)
            {
                CalculateParentCanCuDuToanNamTruoc(item.IdMlnsCha, items, item);
            }

            UpdateTotal();
        }

        private void CalculateParentCanCuDuToanNamTruoc(Guid? idParent, List<CanCuDuToanNamTruocSoKiemTraQuery> listData, CanCuDuToanNamTruocSoKiemTraQuery item)
        {
            //var model = listData.FirstOrDefault(x => x.IdMlns == idParent.GetValueOrDefault());
            var model = _dicDataCanDuToan.ContainsKey(idParent ?? Guid.NewGuid()) ? _dicDataCanDuToan[idParent.Value] : null;
            if (model == null) return;
            model.TuChi += item.TuChi;
            model.HangNhap += item.HangNhap;
            model.HangMua += item.HangMua;
            model.PhanCap += item.PhanCap;
            model.MuaHangHienVat += item.MuaHangHienVat;
            model.DacThu += item.DacThu;
            CalculateParentCanCuDuToanNamTruoc(model.IdMlnsCha, listData, item);
        }

        private void AddCanCuDuToanNamTruoc()
        {
            var loaiChungTu = Model.ILoaiChungTu;
            List<CanCuDuToanNamTruocSoKiemTraQuery> lstCanCu = _sktChungTuChiTietService
                .FindCanCuSoKiemTra(loaiChungTu.GetValueOrDefault(), Model.IIdMaDonVi, Model.INamLamViec - 1, _sessionInfo.YearOfBudget, _sessionInfo.Budget).ToList();
            List<Guid> lstIdMucLuc = lstCanCu.Select(x => x.IdMlns).Distinct().ToList();
            List<NsMucLucNganSach> sktMucLucs = FindListParentMucLucByChildNamTruoc(lstIdMucLuc);
            foreach (var mlc in sktMucLucs)
            {
                if (!lstIdMucLuc.Contains(mlc.MlnsId))
                {
                    CanCuDuToanNamTruocSoKiemTraQuery mlCha = new CanCuDuToanNamTruocSoKiemTraQuery();
                    mlCha.IdMlns = mlc.MlnsId;
                    mlCha.IdMlnsCha = mlc.MlnsIdParent;
                    mlCha.SXauNoiMa = mlc.XauNoiMa;
                    mlCha.BHangCha = mlc.BHangCha;
                    lstCanCu.Add(mlCha);
                }
            }
            CalculateDataCanCuDuToanNamTruoc(lstCanCu);
            List<NsMlsktMlns> lstMap = new List<NsMlsktMlns>();
            lstMap = _sktMucLucService.FindAllMapMlsktMlns(_sessionInfo.YearOfWork - 1).ToList();
            foreach (var it in lstCanCu)
            {
                var itSkt = lstMap.FirstOrDefault(x => x.SNsXauNoiMa.Equals(it.SXauNoiMa));
                if (itSkt != null)
                {
                    it.KyHieu = itSkt.SSktKyHieu;
                }
            }
            foreach (var cc in lstCanCu)
            {
                var mucLuc = Items.FirstOrDefault(item => item.SKyHieuCu == cc.KyHieu);
                if (mucLuc != null)
                {
                    if (loaiChungTu.GetValueOrDefault(0).Equals(int.Parse(VoucherType.NSSD_Key)))
                    {
                        mucLuc.DuToan += cc.TuChi;
                        mucLuc.DuToanMHCHV += 0;
                        mucLuc.DuToanDT += 0;
                    }
                    else
                    {
                        mucLuc.DuToan += 0;
                        mucLuc.DuToanMHCHV += cc.HangNhap + cc.HangMua + cc.TuChi;
                        mucLuc.DuToanDT += cc.PhanCap + cc.DuPhong;
                    }
                }
            }
        }

        public List<NsMucLucNganSach> FindListParentMucLucByChildNamTruoc(List<Guid> listIdMucLuc)
        {
            var yearOfWork = _sessionInfo.YearOfWork - 1;
            var listMucLuc = _iNsMucLucNganSachService
                .FindByCondition(x => listIdMucLuc.Contains(x.MlnsId) && x.NamLamViec == yearOfWork).ToList();
            List<Guid> listIdMlskt = new List<Guid>();
            List<NsMucLucNganSach> sktMucLucs = new List<NsMucLucNganSach>();
            if (listMucLuc.Count > 0)
            {
                listIdMlskt = listMucLuc.Select(item => item.MlnsId).ToList();
                sktMucLucs = listMucLuc;
                while (true)
                {
                    var listIdParent = listMucLuc.Where(x => !listIdMlskt.Contains(x.MlnsIdParent.GetValueOrDefault())).Select(x => x.MlnsIdParent).ToList();
                    var listParent1 = _iNsMucLucNganSachService.FindByCondition(x => listIdParent.Contains(x.MlnsId) && x.NamLamViec == yearOfWork).ToList();
                    if (listParent1.Count > 0)
                    {
                        var lstId = listParent1.Select(item => item.MlnsId).ToList();
                        listIdMlskt.AddRange(lstId);
                        sktMucLucs.AddRange(listParent1);
                    }
                    else
                    {
                        break;
                    }
                }
            }
            sktMucLucs = sktMucLucs.GroupBy(x => x.MlnsId).Select(x => x.First()).OrderBy(x => x.XauNoiMa).ToList();
            return sktMucLucs;
        }

        private bool PopupFilter(object obj)
        {
            var temp = (SktMucLucModel)obj;
            var condition = true;
            if (!string.IsNullOrEmpty(PopupSearchText))
                condition = condition && (temp.SSTT.ToLower().Contains(PopupSearchText.Trim().ToLower())
                                          || temp.SMoTa.ToLower().Contains(PopupSearchText.Trim().ToLower())
                                          || temp.SKyHieu.ToLower().Contains(PopupSearchText.Trim().ToLower()));
            return condition;
        }

        private bool SktChungTuChiTietFilter(object obj)
        {
            var temp = (NsSktChungTuChiTietModel)obj;

            var condition = true;
            if (DicChungTu2 != null)
            {
                if (DicChungTu2.ContainsKey(temp.IIdMlskt))
                {
                    var listChild = DicChungTu2[temp.IIdMlskt];
                    foreach (var child in listChild)
                    {
                        if (CheckChungTuChiTietCanView(child))
                        {
                            temp.IsFilter = condition;
                            return true;
                        }
                    }
                }
            }
            condition = CheckChungTuChiTietCanView(temp);

            temp.IsFilter = condition;

            return condition;
        }

        private List<NsSktChungTuChiTietModel> FindListChild(NsSktChungTuChiTietModel item)
        {
            var listResult = new List<NsSktChungTuChiTietModel>();
            if (DicChungTu == null) return listResult;
            if (DicChungTu.ContainsKey(item.IIdMlskt))
            {
                var currentListChild = DicChungTu[item.IIdMlskt];
                if (currentListChild != null) listResult.AddRange(DicChungTu[item.IIdMlskt]);

                while (currentListChild != null && currentListChild.Count != 0)
                {
                    var listParent = currentListChild;
                    currentListChild = new List<NsSktChungTuChiTietModel>();
                    foreach (var parent in listParent)
                    {
                        if (DicChungTu.ContainsKey(parent.IIdMlskt))
                            currentListChild.AddRange(DicChungTu[parent.IIdMlskt]);
                    }
                    if (currentListChild != null) listResult.AddRange(currentListChild);
                }
            }
            return listResult;
        }


        private bool CheckChungTuChiTietCanView(NsSktChungTuChiTietModel temp)
        {

            var condition = true;
            if (!string.IsNullOrEmpty(NsSktChungTuChiTietSearchModel.SKyHieu))
            {
                Regex rg = new Regex(string.Format("^{0}", NsSktChungTuChiTietSearchModel.SKyHieu.ToLower()));
                condition = condition && rg.IsMatch(temp.SKyHieu.ToLower());
            }

            if (SelectedDataState != null)
            {
                var dataStateSelectedValue = int.Parse(SelectedDataState.ValueItem);
                switch (dataStateSelectedValue)
                {
                    case (int)DataStateValue.HIEN_THI_TAT_CA:
                        break;
                    case (int)DataStateValue.CO_SO_LIEU:
                        condition = condition && (temp.FTuChi > 0 || temp.FHuyDongTonKho > 0 || temp.FMuaHangCapHienVat > 0
                                                  || temp.FPhanCap > 0 || temp.DuToan > 0 || temp.DuToanDT > 0 || temp.DuToanMHCHV > 0
                                                  || temp.SoKiemTra > 0 || temp.SoKiemTraMHHV > 0 || temp.SoKiemTraDT > 0
                                                  || temp.SoNhuCau > 0 || temp.SoNhuCauDT > 0 || temp.SoNhuCauMHHV > 0) || temp.FThongBaoDonVi > 0;
                        break;
                    case (int)DataStateValue.DA_NHAP_SKT:
                        condition = condition && (temp.FTuChi > 0 || temp.FMuaHangCapHienVat > 0 || temp.FPhanCap > 0);
                        break;
                }
            }

            if (SelectedNNganhModel != null)
            {
                if (!String.IsNullOrEmpty(SelectedNNganhModel.SGiaTri))
                {
                    string[] values = SelectedNNganhModel.SGiaTri.Split(",");
                    List<NsSktChungTuChiTietModel> listParent = new List<NsSktChungTuChiTietModel>();
                    IsChild(temp, values, listParent);
                    condition = condition && (values.Contains(temp.Nganh) || listParent.Exists(item => values.Contains(item.Nganh)));
                }
            }
            return condition;
        }

        private void IsChild(NsSktChungTuChiTietModel parent, string[] values, List<NsSktChungTuChiTietModel> listParent)
        {
            foreach (var item in Items)
            {
                if (!listParent.Contains(item) && item.IdParent.Equals(parent.IIdMlskt))
                {
                    listParent.Add(item);
                    IsChild(item, values, listParent);
                }
            }
        }

        private void LoadPopupData()
        {
            var predicate = PredicateBuilder.True<NsSktMucLuc>();
            predicate = predicate.And(x => x.INamLamViec == _sessionInfo.YearOfWork);
            predicate = predicate.And(x => x.ITrangThai == StatusType.ACTIVE);
            predicate = predicate.And(x => true.Equals(x.BHangCha));
            var temp = _sktMucLucService.FindByCondition(predicate).OrderBy(x => x.SKyHieu).ToList();
            SktMucLucModelItems = _mapper.Map<ObservableCollection<SktMucLucModel>>(temp);
            _searchPopupView = CollectionViewSource.GetDefaultView(SktMucLucModelItems);
            _searchPopupView.Filter = PopupFilter;
        }

        private void UpdateTotal()
        {
            var listChildren = Items.Where(x => !x.IsHangCha && !x.IsDeleted && x.IsFilter).ToList();
            Model.TongHuyDong = listChildren.Sum(x => x.FHuyDongTonKho);
            Model.FTongTuChi = listChildren.Sum(x => x.FTuChi);
            Model.FTongMuaHangCapHienVat = listChildren.Sum(x => x.FMuaHangCapHienVat);
            Model.FTongPhanCap = listChildren.Sum(x => x.FPhanCap);
            Model.TongSoNhuCau = listChildren.Sum(x => x.SoNhuCau);
            Model.TongSoNhuCauMHHV = listChildren.Sum(x => x.SoNhuCauMHHV);
            Model.TongSoNhuCauDT = listChildren.Sum(x => x.SoNhuCauDT);
            Model.TongSoKiemTra = listChildren.Sum(x => x.SoKiemTra);
            Model.TongSoKiemTraMHHV = listChildren.Sum(x => x.SoKiemTraMHHV);
            Model.TongSoKiemTraDT = listChildren.Sum(x => x.SoKiemTraDT);
            Model.TongCanCuDuToan = listChildren.Sum(x => x.DuToan);
            Model.TongCanCuDuToanMHCHV = listChildren.Sum(x => x.DuToanMHCHV);
            Model.TongCanCuDuToanDT = listChildren.Sum(x => x.DuToanDT);
            Model.FTongThongBaoDonVi = listChildren.Sum(x => x.FThongBaoDonVi);
            FTongHuyDongTonKho = listChildren.Sum(x => x.FHuyDongTonKho);

            OnPropertyChanged(nameof(FTongHuyDongTonKho));
        }

        private void UpdateTongTangGiam()
        {
            Model.TongTangSnc = 0;
            Model.TongGiamSnc = 0;
            Model.TongTangDT = 0;
            Model.TongGiamDT = 0;
            var listChildren = Items.Where(x => !x.IsHangCha && !x.IsDeleted && x.IsFilter).ToList();

            Model.TongTangSnc = listChildren.Sum(item => item.TangSKT > 0 ? item.TangSKT : 0);
            Model.TongGiamSnc = listChildren.Sum(item => item.GiamSKT > 0 ? item.GiamSKT : 0);
            Model.TongTangDT = listChildren.Sum(item => item.TangDT > 0 ? item.TangDT : 0);
            Model.TongGiamDT = listChildren.Sum(item => item.GiamDT > 0 ? item.GiamDT : 0);
            //foreach (var item in listChildren)
            //{
            //    //Model.TongTangSnc += item.TangSNC > 0 ? item.TangSNC : 0;
            //    //Model.TongGiamSnc += item.GiamSNC > 0 ? item.GiamSNC : 0;                
            //    Model.TongTangSnc += item.TangSKT > 0 ? item.TangSKT : 0;
            //    Model.TongGiamSnc += item.GiamSKT > 0 ? item.GiamSKT : 0;
            //    Model.TongTangDT += item.TangDT > 0 ? item.TangDT : 0;
            //    Model.TongGiamDT += item.GiamDT > 0 ? item.GiamDT : 0;
            //}
        }

        public void DeleteChiTiet(List<NsSktChungTuChiTietModel> lstItems)
        {
            if (lstItems.Count > 0)
            {
                foreach (var item in lstItems)
                {
                    var deleteItem = _sktChungTuChiTietService.FindById(item.Id);
                    if (deleteItem != null)
                    {
                        _mapper.Map(item, deleteItem);
                        _sktChungTuChiTietService.Delete(deleteItem);
                    }
                    item.FTuChi = 0;
                    item.FTuChiDeNghi = 0;
                    item.FHuyDongTonKho = 0;
                    item.FMuaHangCapHienVat = 0;
                    item.FPhanCap = 0;
                    item.FTonKhoDenNgay = 0;
                    item.SMoTa = string.Empty;
                    item.IsModified = false;
                    item.IsDeleted = false;
                }
            }
        }

        public void CopySoNhuCauSangTuChi()
        {
            IsInit = true;
            if (Model.ILoaiChungTu.ToString() == VoucherType.NSSD_Key)
            {
                Items.Where(x => !x.IsHangCha && !x.IsDeleted && x.IsFilter).ForAll(x =>
                {
                    x.FTuChi = x.SoNhuCau;
                });
            }
            else
            {
                Items.Where(x => !x.IsHangCha && !x.IsDeleted && x.IsFilter).ForAll(x =>
                {
                    x.FMuaHangCapHienVat = x.SoNhuCauMHHV;
                    x.FPhanCap = x.SoNhuCauDT;
                });
            }

            CalculateData();
            UpdateTongTangGiam();
            IsInit = false;
        }

        public void LayDuLieuNganhThamDinh()
        {
            DialogResult dialogLock = System.Windows.Forms.MessageBox.Show(string.Format(Resources.MsgTransferThamDinh), Resources.ConfirmTitle, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dialogLock != DialogResult.Yes)
            {
                return;
            }

            var lstDataThamDinh = _mapper.Map<List<NsSktNganhThamDinhChiTietSktModel>>(GetChungTuThamDinhChiTiets());
            AddItemToItemsThamDinh(lstDataThamDinh);

            foreach (var it in Items)
            {
                double giaTri = ItemsThamDinhSkt.Where(x => x.IIdMucLuc.Equals(it.IIdMlskt)).Sum(x => x.FTuChi.GetValueOrDefault());
                if (!it.IsHangCha && !it.FThongBaoDonVi.Equals(giaTri))
                {
                    it.FThongBaoDonVi = giaTri;
                    it.IsModified = true;
                    OnPropertyChanged(nameof(IsSaveData));
                }
            }

            CalculateData();
        }

        public List<NsSktNganhThamDinhChiTiet> GetChungTuThamDinhChiTiets()
        {
            var lstCtThamDinh = GetChungTuThamDinh();
            var lstIdCtThamDinh = lstCtThamDinh.Select(x => x.Id).ToList();
            var predicate = PredicateBuilder.True<NsSktNganhThamDinhChiTiet>();
            predicate = predicate.And(x => lstIdCtThamDinh.Contains(x.IIdCtnganhThamDinh));
            var results = _sktThamDinhChiTietService.FindByCondition(predicate).ToList();
            return results;
        }

        public List<ExpertiseModel> GetChungTuThamDinh()
        {
            int loai = LoaiNganhThamDinh.CTNTD;
            int loaiNganSach = int.Parse(VoucherType.NSSD_Key);
            IEnumerable<ThDChungTuQuery> data = _sktThamDinhService.FindByNamLamViec(_sessionService.Current.YearOfWork,
                _sessionService.Current.YearOfBudget, _sessionService.Current.Budget, _sessionService.Current.Principal, loai, loaiNganSach);
            List<ExpertiseModel> results;
            results = _mapper.Map<List<ExpertiseModel>>(data);
            return results;
        }

        public void LoadDataChiTietThongBaoDv()
        {
            var predicate = PredicateBuilder.True<NsSktNganhThamDinhChiTietSkt>();
            predicate = predicate.And(x => x.INamLamViec == _sessionInfo.YearOfWork);
            predicate = predicate.And(x => x.INamNganSach == _sessionInfo.YearOfBudget);
            predicate = predicate.And(x => x.IIdMaNguonNganSach == _sessionInfo.Budget);
            var data = _iSktNganhThamDinhChiTietSktService.FindByCondition(predicate);
            ItemsThamDinhSkt = _mapper.Map<ObservableCollection<NsSktNganhThamDinhChiTietSktModel>>(data);
        }

        public void AddItemToItemsThamDinh(List<NsSktNganhThamDinhChiTietSktModel> dataAdd)
        {
            if (ItemsThamDinhSkt != null)
            {
                if (dataAdd.Any())
                {
                    foreach (var it in dataAdd)
                    {
                        var itTd = ItemsThamDinhSkt.FirstOrDefault(x =>
                            x.IIdMucLuc.Equals(it.IIdMucLuc) && x.IIdMaDonVi.Equals(it.IIdMaDonVi));
                        if (itTd != null)
                        {
                            itTd.FTuChi = it.FTuChi;
                            itTd.SGhiChu = it.SGhiChu;
                            itTd.IsModified = true;
                        }
                        else
                        {
                            it.Id = new Guid();
                            it.IsModified = true;
                            ItemsThamDinhSkt.Add(it);
                        }
                    }
                }
                else
                {
                    ItemsThamDinhSkt = new ObservableCollection<NsSktNganhThamDinhChiTietSktModel>();
                }

            }
            else
            {
                ItemsThamDinhSkt = new ObservableCollection<NsSktNganhThamDinhChiTietSktModel>(dataAdd);
                foreach (var td in ItemsThamDinhSkt)
                {
                    td.Id = Guid.Empty;
                    td.IsModified = true;
                }
            }
            OnPropertyChanged(nameof(IsSaveData));
        }

        private bool IsUpdateToDelete(NsSktChungTuChiTiet entity)
        {
            //TODO: verify requirement
            return entity.FTuChi == 0 && entity.FTonKhoDenNgay == 0 && entity.FTonKhoDenNgay == 0 && entity.FHuyDongTonKho == 0;
        }

        private void OnImportData()
        {
            ImportCheckDetailViewModel.Model = Model;
            ImportCheckDetailViewModel.Init();
            ImportCheckDetailViewModel.SavedAction = obj =>
            {
                this.LoadData();
                OpenDetailDialog((NsSktChungTuModel)obj);
            };

            ImportCheckDetailViewModel.ShowDialog();
        }

        private void OpenDetailDialog(NsSktChungTuModel itemDetail)
        {
            //this.Model = itemDetail;
            this.Init();
            var view = new CheckDetail { DataContext = this };
            view.ShowDialog();
        }

        public void OnCopyEstimateData()
        {
            IsInit = true;
            if (Model.ILoaiChungTu.ToString() == VoucherType.NSSD_Key)
            {
                Items.Where(x => !x.IsHangCha && !x.IsDeleted && x.IsFilter).ForAll(x =>
                {
                    x.FTuChi = x.DuToan;
                });
            }
            else
            {
                Items.Where(x => !x.IsHangCha && !x.IsDeleted && x.IsFilter).ForAll(x =>
                {
                    x.FMuaHangCapHienVat = x.DuToanMHCHV;
                    x.FPhanCap = x.DuToanDT;
                });
            }

            CalculateData();
            UpdateTongTangGiam();
            IsInit = false;
        }
    }
}