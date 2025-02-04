
using AutoMapper;
using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
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
using VTS.QLNS.CTC.App.View.Budget.DemandCheck.CanCu;
using VTS.QLNS.CTC.App.View.Budget.DemandCheck.Check;
using VTS.QLNS.CTC.App.View.Budget.DemandCheck.Check.PrintReport;
using VTS.QLNS.CTC.App.View.Budget.DemandCheck.FunctionMap.PrintReport;
using VTS.QLNS.CTC.App.ViewModel.Budget.DemandCheck.PrintReport;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Criteria;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Service;
using VTS.QLNS.CTC.Utility;
using VTS.QLNS.CTC.Utility.Enum;

namespace VTS.QLNS.CTC.App.ViewModel.Budget.DemandCheck.Demand3Y
{
    public class Demand3YDetailViewModel : DetailViewModelBase<NsSktChungTuModel, NsNc3YChungTuChiTietModel>
    {
        private readonly ISessionService _sessionService;
        private readonly INc3YChungTuChiTietService _nc3YChungTuChiTietService;
        private readonly ISktChungTuService _sktChungTuService;
        private readonly ISktMucLucService _sktMucLucService;
        private readonly IDanhMucService _iDanhMucService;
        private readonly ICauHinhCanCuService _iCauHinhCanCuService;
        private readonly INsMucLucNganSachService _iNsMucLucNganSachService;
        private readonly INsDonViService _iNsDonViService;
        private readonly INsDtChungTuService _iDtChungTuService;
        private readonly ISysAuditLogService _log;
        private SessionInfo _sessionInfo;
        private readonly IMapper _mapper;

        public delegate void DataChangedEventHandler(object sender, EventArgs e);
        public event DataChangedEventHandler UpdateParentWindowEventHandler;

        private ICollectionView _nc3YChungTuChiTietModelsView { get; set; }
        private ICollectionView _searchPopupView { get; set; }

        private NsSktChungTuModel _ctTongHop;
        public NsSktChungTuModel CtTongHop
        {
            get => _ctTongHop;
            set => SetProperty(ref _ctTongHop, value);
        }

        private NsNc3YChungTuChiTietModel _nsNc3YChungTuChiTietSearchModel;
        public NsNc3YChungTuChiTietModel NsNc3YChungTuChiTietSearchModel
        {
            get => _nsNc3YChungTuChiTietSearchModel;
            set => SetProperty(ref _nsNc3YChungTuChiTietSearchModel, value);
        }

        private ObservableCollection<SktMucLucModel> _sktMucLucModelItems;
        public ObservableCollection<SktMucLucModel> SktMucLucModelItems
        {
            get => _sktMucLucModelItems;
            set => SetProperty(ref _sktMucLucModelItems, value);
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

        public Visibility ShowColNSBD { get; set; }
        public Visibility ShowColNSSD { get; set; }

        public string Header1 => $"Năm {_sessionInfo.YearOfWork-1}";
        public string Header2 => $"Năm {_sessionInfo.YearOfWork}";
        public string Header3 => $"So sánh với UTH năm {_sessionInfo.YearOfWork-1} (%)";
        public string Header4 => $"Năm {_sessionInfo.YearOfWork+1}";
        public string Header5 => $"So sánh nhu cầu năm {_sessionInfo.YearOfWork} (%)";
        public string Header6 => $"Năm {_sessionInfo.YearOfWork+2}";
        public string Header7 => $"So sánh nhu cầu năm {_sessionInfo.YearOfWork + 1} (%)";

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
                NsNc3YChungTuChiTietSearchModel.SKyHieu = _selectedPopupItem?.SKyHieu;
                OnPropertyChanged(nameof(NsNc3YChungTuChiTietSearchModel));
                IsPopupOpen = false;
                OnSearch();
            }
        }

        public bool IsSaveData => Items.Any(item => item.IsModified || item.IsDeleted || item.IsUpdateCanCu || LstCanCu != null);

        private ComboboxItem _selectedDataState;
        public ComboboxItem SelectedDataState
        {
            get => _selectedDataState;
            set
            {
                SetProperty(ref _selectedDataState, value);
                if (_selectedDataState != null)
                {
                    OnSearch();
                }
            }
        }

        public int IndexDataState { get; set; }

        private ObservableCollection<ComboboxItem> _dataStateItems;
        public ObservableCollection<ComboboxItem> DataStateItems
        {
            get => _dataStateItems;
            set => SetProperty(ref _dataStateItems, value);
        }

        public string ComboboxDisplayMemberPathDanhMucNganh => nameof(DanhMucNganhModel.STen);

        private ObservableCollection<DanhMucNganhModel> _nNganhModelItems;
        public ObservableCollection<DanhMucNganhModel> NNganhModelItems
        {
            get => _nNganhModelItems;
            set => SetProperty(ref _nNganhModelItems, value);
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

        private bool _isPopupOpen;
        public bool IsPopupOpen
        {
            get => _isPopupOpen;
            set => SetProperty(ref _isPopupOpen, value);
        }

        private ObservableCollection<CauHinhCanCuModel> _lstCanCu;
        public ObservableCollection<CauHinhCanCuModel> LstCanCu
        {
            get => _lstCanCu;
            set => SetProperty(ref _lstCanCu, value);
        }

        private ObservableCollection<CauHinhCanCuModel> _lstCanCuInit;
        public ObservableCollection<CauHinhCanCuModel> LstCanCuInit
        {
            get => _lstCanCuInit;
            set => SetProperty(ref _lstCanCuInit, value);
        }

        private ObservableCollection<ComboboxItem> _viewSummary = new ObservableCollection<ComboboxItem>();

        public ObservableCollection<ComboboxItem> ViewSummary
        {
            get => _viewSummary;
            set => SetProperty(ref _viewSummary, value);
        }

        private ComboboxItem _viewSummarySelected;

        public ComboboxItem ViewSummarySelected
        {
            get => _viewSummarySelected;
            set
            {
                SetProperty(ref _viewSummarySelected, value);
                _selectedDonVi = null;
                LoadData();
                LoadDonViFilter();
                OnPropertyChanged(nameof(IsFilterDonVi));
            }
        }

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

        public bool IsFilterDonVi => _viewSummarySelected != null &&
                                     _viewSummarySelected.ValueItem.Equals(TypeViewSummary.Detail.ToString());
        public bool IsChungTuTongHop => Model != null && !string.IsNullOrEmpty(Model.SDssoChungTuTongHop);
        public bool IsAnotherUserCreate { get; set; }
        public bool IsEnabledDelete => !IsLock && SelectedItem != null && SelectedItem.HasData;
        public bool IsDeleteAll => !IsLock && Items.Any(item => !item.IsModified && item.HasData) && !IsVoucherSummary;

        public int NamLamViec { get; set; }

        public override Type ContentType => typeof(CheckDetail);
        public RelayCommand SearchCommand { get; }
        public RelayCommand ClearSearchCommand { get; }
        public new RelayCommand SaveCommand { get; }
        public new RelayCommand CloseCommand { get; }
        public RelayCommand PrintCommand { get; }
        public RelayCommand CanCuCommand { get; }
        public RelayCommand LaySummaryCanCuCommand { get; }
        public PrintReportDemandOrgViewModel PrintReportDemandOrgViewModel { get; }
        public TongHopCanCuViewModel TongHopCanCuViewModel { get; }

        public bool IsInit { get; set; }

        public DateTime DtNow => DateTime.Now;

        public Demand3YDetailViewModel(
            INc3YChungTuChiTietService nc3YChungTuChiTietService,
            ISktChungTuService sktChungTuService,
            ISktMucLucService sktMucLucService,
            ICauHinhCanCuService iCauHinhCanCuService,
            INsMucLucNganSachService iNsMucLucNganSachService,
            ISessionService sessionService,
            IMapper mapper,
            PrintReportDemandOrgViewModel printReportDemandOrgViewModel,
            TongHopCanCuViewModel tongHopCanCuViewModel,
            IDanhMucService iDanhMucService,
            INsDonViService iNsDonViService,
            INsDtChungTuService iDtChungTuService,
            ISysAuditLogService log)
        {
            _nc3YChungTuChiTietService = nc3YChungTuChiTietService;
            _sktChungTuService = sktChungTuService;
            _sktMucLucService = sktMucLucService;
            _iCauHinhCanCuService = iCauHinhCanCuService;
            _iNsMucLucNganSachService = iNsMucLucNganSachService;
            _iNsDonViService = iNsDonViService;
            _iDtChungTuService = iDtChungTuService;
            _sessionService = sessionService;
            _log = log;
            _mapper = mapper;
            _iDanhMucService = iDanhMucService;
            NsNc3YChungTuChiTietSearchModel = new NsNc3YChungTuChiTietModel();
            PrintReportDemandOrgViewModel = printReportDemandOrgViewModel;
            TongHopCanCuViewModel = tongHopCanCuViewModel;

            SearchCommand = new RelayCommand(o => OnSearch());
            ClearSearchCommand = new RelayCommand(OnClearSearch);
            PrintCommand = new RelayCommand(obj => OpenPrintDialog(obj));
            SaveCommand = new RelayCommand(o => OnSave());
            CloseCommand = new RelayCommand(OnClose);
            PrintCommand = new RelayCommand(obj => OpenPrintDialog(obj));
        }

        public override void Init()
        {
            base.Init();
            _sessionInfo = _sessionService.Current;
            NamLamViec = _sessionService.Current.YearOfWork;
            if (Model != null)
            {
                IsLock = Model.BKhoa;
                IsAnotherUserCreate = Model.SNguoiTao != _sessionInfo.Principal;
            }
            IsInit = true;
            LoadPopupData();
            LoadDataState();
            LoadDataTypeViewSummary();
            LoadData();
            //LoadCanCu();
            IsInit = false;
        }

        private void OnClearSearch(object obj)
        {
            NsNc3YChungTuChiTietSearchModel = new NsNc3YChungTuChiTietModel();
            _nc3YChungTuChiTietModelsView.Refresh();
        }

        private void OnSearch()
        {
            _nc3YChungTuChiTietModelsView?.Refresh();
            if (Items != null)
            {
                CalculateData();
            }
            _nc3YChungTuChiTietModelsView?.Refresh();
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
            CalculateData();
            OnPropertyChanged(nameof(IsSaveData));
            OnPropertyChanged(nameof(IsDeleteAll));
        }

        protected override void OnDeleteAll()
        {
            base.OnDeleteAll();
            var result = MessageBoxHelper.Confirm(Resources.DeleteAllChungTuChiTiet);
            if (result == MessageBoxResult.No)
                return;
            if (result == MessageBoxResult.Yes)
            {
                var lstItemFilter = Items.Where(x => x.IsFilter
                                                     && !x.IsHangCha
                                                     && (x.FDuToan != 0 || x.FUocTH != 0 || x.FNCNam1 != 0 || x.FNCNam2 != 0
                                                                       || x.FNCNam3 != 0)).ToList();
                //xóa chứng từ chi tiết
                DeleteChiTiet(lstItemFilter);                             
                LoadData();
                UpdateDemandVoucherTotal();
                MessageBoxHelper.Info(Resources.MsgDeleteSuccess);
                OnPropertyChanged(nameof(IsDeleteAll));
            }
        }

        public override void OnSave()
        {
            if (!IsSaveData)
            {
                return;
            }
            Func<NsNc3YChungTuChiTietModel, bool> isAdd = x => x.IsModified && !x.IsDeleted && x.IsAdd && !x.IsHangCha;
            Func<NsNc3YChungTuChiTietModel, bool> isUpdate = x => x.IsModified && !x.IsDeleted && !x.IsAdd && !x.IsHangCha;
            bool isDelete(NsNc3YChungTuChiTietModel x) => x.IsDeleted && !x.IsHangCha;
            Func<NsNc3YChungTuChiTietModel, bool> isUpdateCanCu = x => !x.IsHangCha ;

            var detailsAdd = Items.Where(isAdd).ToList();
            var detailsUpdate = Items.Where(isUpdate).ToList();
            var detailsDelete = Items.Where(isDelete).ToList();
            var detailsCanCu = Items.Where(isUpdateCanCu).ToList();

            // update số liệu chứng từ
            UpdateDemandVoucherTotal();

            //thêm mới chứng từ chi tiết
            if (detailsAdd.Count > 0)
            {
                var addItems = new List<NsNc3YChungTuChiTiet>();
                _mapper.Map(detailsAdd, addItems);
                _nc3YChungTuChiTietService.AddRange(addItems);
                //Items.Where(isAdd).Select(x =>
                //{
                //    x.IsModified = false;
                //    x.IsAdd = false;
                //    return x;
                //}).ToList();
            }

            //cập nhật chứng từ chi tiết
            if (detailsUpdate.Count > 0)
            {
                foreach (var updateItem in detailsUpdate)
                {
                    if (IsUpdateToDelete(updateItem))
                    {
                        detailsDelete.Add(updateItem);
                    }
                    else
                    {
                        var nc3YChungTuChiTiet = _nc3YChungTuChiTietService.FindById(updateItem.Id);
                        _mapper.Map(updateItem, nc3YChungTuChiTiet);
                        _nc3YChungTuChiTietService.Update(nc3YChungTuChiTiet);
                        updateItem.IsModified = false;
                    }
                }
            }

            //xóa chứng từ chi tiết
            if (detailsDelete.Count > 0)
            {
                foreach (var item in detailsDelete)
                {
                    var deleteItem = _nc3YChungTuChiTietService.FindById(item.Id);
                    if (deleteItem != null)
                    {
                        _mapper.Map(item, deleteItem);
                        _nc3YChungTuChiTietService.Delete(deleteItem);
                        _log.WriteLog(Resources.ApplicationName, "Số nhu cầu - chứng từ chi tiết", (int)TypeExecute.Delete, DtNow, TransactionStatus.Success, _sessionService.Current.Principal);
                    }

                    if (!item.IsDeleted)
                    {
                        continue;
                    }
                    var predicateCT = PredicateBuilder.True<NsSktChungTuChiTietCanCu>();
                    predicateCT = predicateCT.And(x => x.IiIdCtsoKiemTra == Model.Id);
                    predicateCT = predicateCT.And(x => x.IIdMlskt == item.IIdMlskt);
                    predicateCT = predicateCT.And(x => x.INamLamViec == _sessionInfo.YearOfWork);                    
                    item.FDuToan = 0;
                    item.FUocTH = 0;
                    item.FNCNam1 = 0;
                    item.FNCNam2 = 0;
                    item.FNCNam3 = 0;
                    item.SMoTa = string.Empty;
                    item.IsModified = false;
                    item.IsDeleted = false;
                }
            }           

            _log.WriteLog(Resources.ApplicationName, "Số nhu cầu - chứng từ chi tiết", (int)TypeExecute.Adjust, DtNow, TransactionStatus.Success, _sessionService.Current.Principal);
            LoadData();
            OnPropertyChanged(nameof(IsSaveData));
            OnPropertyChanged(nameof(IsDeleteAll));
            var message = Resources.MsgSaveDone;
            var messageBox = new NSMessageBoxViewModel(message);
            UpdateParentWindowEventHandler?.Invoke(Model, new EventArgs());
            DialogHost.Show(messageBox.Content, DemandCheckScreen.DETAIL_DIALOG);
        }

        public void UpdateDemandVoucherTotal()
        {
            if (Model != null)
            {
                var sktChungTuUpdate = _sktChungTuService.FindById(Model.Id);
                if (sktChungTuUpdate != null)
                {
                    sktChungTuUpdate.FTongTuChi = Items.Where(item => !item.IsHangCha && !item.IsDeleted)
                        .Sum(item => item.FNCNam1);
                    sktChungTuUpdate.FTongPhanCap = Items.Where(item => !item.IsHangCha && !item.IsDeleted)
                        .Sum(item => item.FNCNam2);
                    sktChungTuUpdate.FTongMuaHangCapHienVat = Items.Where(item => !item.IsHangCha && !item.IsDeleted)
                        .Sum(item => item.FNCNam3);
                    _sktChungTuService.Update(sktChungTuUpdate);
                }
            }
        }

        protected override void OnLockUnLock()
        {
            if (IsLock)
            {
                List<DonVi> userAgency = _iNsDonViService.FindByUser(_sessionInfo.Principal, _sessionInfo.YearOfWork, LoaiDonVi.ROOT);
                if (userAgency.All(x => x.Loai != LoaiDonVi.ROOT))
                {
                    MessageBoxHelper.Warning(Resources.MsgRoleUnlock);
                    return;
                }
            }
            else
            {
                if (Model.SNguoiTao != _sessionInfo.Principal)
                {
                    MessageBoxHelper.Warning(string.Format(Resources.MsgRoleLock, Model.SNguoiTao));
                    return;
                }
            }
            string message = IsLock ? Resources.UnlockChungTu : Resources.LockChungTu;
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
            _log.WriteLog(Resources.ApplicationName, "Số nhu cầu - chứng từ chi tiết", (int)TypeExecute.Update, DtNow, TransactionStatus.Success, _sessionService.Current.Principal);
            UpdateParentWindowEventHandler?.Invoke(Model, new EventArgs());
        }

        protected override void OnRefresh()
        {
            IsInit = true;
            LoadData();
            //LoadCanCu();
            IsInit = false;
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
            _selectedDataState = DataStateItems[0];
        }

        private void LoadDataTypeViewSummary()
        {
            ViewSummary = new ObservableCollection<ComboboxItem>
            {
                new ComboboxItem()
                {
                    ValueItem = TypeViewSummary.Summary.ToString(),
                    DisplayItem = TypeViewSummaryName.Summary,
                },
                new ComboboxItem()
                {
                    ValueItem = TypeViewSummary.Detail.ToString(),
                    DisplayItem = TypeViewSummaryName.Detail,
                }
            };
            _viewSummarySelected = ViewSummary[0];
        }

        public bool IsVoucherSummary { get; set; }

        public override void LoadData(params object[] args)
        {
            Nc3YChungTuChiTietCriteria searchCondition = new Nc3YChungTuChiTietCriteria();
            searchCondition.NamLamViec = _sessionInfo.YearOfWork;
            searchCondition.NamNganSach = _sessionInfo.YearOfBudget;
            searchCondition.NguonNganSach = _sessionInfo.Budget;
            searchCondition.ITrangThai = StatusType.ACTIVE;
            searchCondition.SktChungTuId = Model.Id;
            searchCondition.ILoai = Model.ILoai;
            searchCondition.ILoaiNguonNganSach = Model.ILoaiNguonNganSach.GetValueOrDefault(1);
            searchCondition.IdDonVi = Model.IIdMaDonVi;
            searchCondition.CurrentIdDonVi = _sessionInfo.IdDonVi;
            searchCondition.UserName = _sessionInfo.Principal;
            searchCondition.LoaiChungTu = Model.ILoaiChungTu.GetValueOrDefault();
            searchCondition.IsViewDetailSummary = _viewSummarySelected != null ? int.Parse(_viewSummarySelected.ValueItem) : 0;
            if (_selectedDonVi != null)
            {
                searchCondition.IdDonViFilter = _selectedDonVi.IIDMaDonVi;
                searchCondition.IdDonVi = _selectedDonVi.IIDMaDonVi;
                var predicateCtDv = PredicateBuilder.True<NsSktChungTu>();
                predicateCtDv = predicateCtDv.And(x => x.INamLamViec == _sessionInfo.YearOfWork);
                predicateCtDv = predicateCtDv.And(x => x.INamNganSach == _sessionInfo.YearOfBudget);
                predicateCtDv = predicateCtDv.And(x => x.IIdMaNguonNganSach == _sessionInfo.Budget);
                predicateCtDv = predicateCtDv.And(x => x.ILoai == Model.ILoai);
                predicateCtDv = predicateCtDv.And(x => x.ILoaiChungTu == Model.ILoaiChungTu.GetValueOrDefault());
                predicateCtDv = predicateCtDv.And(x => x.IIdMaDonVi.Equals(_selectedDonVi.IIDMaDonVi));
                predicateCtDv = predicateCtDv.And(x => Model.SDssoChungTuTongHop.Contains(x.SSoChungTu));
                var ctDonVi = _sktChungTuService.FindByCondition(predicateCtDv).FirstOrDefault();
                if (ctDonVi != null)
                {
                    searchCondition.SktChungTuId = ctDonVi.Id;
                }
            }

            var temp = _nc3YChungTuChiTietService.FindByConditionForChildUnit(searchCondition).ToList();
            var existChiTiet = _nc3YChungTuChiTietService.ExistChungTuChiTiet(Model.Id);
            
            if (IsInit && !existChiTiet)
            {
                foreach (var item in temp.Where(t => t.FDuToan + t.FUocTH + t.FNCNam1 + t.FNCNam2 + t.FNCNam3 != 0))
                {
                    item.Id = Guid.Empty;
                    item.IsAdd = true;
                    item.IsModified = true;
                    OnPropertyChanged(nameof(IsSaveData));
                }
            }

            foreach (var item in temp)
            {
                item.IsAuToFillTuChi = !existChiTiet;
            }
            Items = _mapper.Map<ObservableCollection<NsNc3YChungTuChiTietModel>>(temp);
            if (IsChungTuTongHop && searchCondition.IsViewDetailSummary == 1)
            {
                List<NsNc3YChungTuChiTietModel> ChiTietChungTus = Items.Select(t => t.IIdCtsoKiemTraChild).Distinct().Select(t => new NsNc3YChungTuChiTietModel { Id = t }).ToList();
                foreach (var item in ChiTietChungTus)
                {
                    var data = Items.Where(t => t.IIdCtsoKiemTraChild.Equals(item.Id));
                }
            }
            else
            {
                //LoadCanCu();
            }
            _nc3YChungTuChiTietModelsView = CollectionViewSource.GetDefaultView(Items);
            foreach (var nc3YChungTuChiTietModel in Items)
            {
                nc3YChungTuChiTietModel.ILoaiChungTu = Model.ILoaiChungTu;
                nc3YChungTuChiTietModel.IsFilter = true;
                if (!nc3YChungTuChiTietModel.IsHangCha)
                {
                    nc3YChungTuChiTietModel.PropertyChanged += (sender, args) =>
                    {
                        if ((args.PropertyName == nameof(SelectedItem.FDuToan) ||
                            args.PropertyName == nameof(SelectedItem.FUocTH) ||
                            args.PropertyName == nameof(SelectedItem.FNCNam1) ||
                            args.PropertyName == nameof(SelectedItem.FNCNam2) ||
                            args.PropertyName == nameof(SelectedItem.FNCNam3) ||
                            args.PropertyName == nameof(SelectedItem.SGhiChu)) && !IsInit)
                        {

                            NsNc3YChungTuChiTietModel item = (NsNc3YChungTuChiTietModel)sender;
                            item.IsModified = true;
                            IsInit = true;
                            CalculateData();
                            IsInit = false;
                            OnPropertyChanged(nameof(IsSaveData));
                        }
                    };
                }
            }
            CalculateData();
            _nc3YChungTuChiTietModelsView.Filter = SktChungTuChiTietFilter;
        }

        private void CalculateData()
        {
            Items.Where(x => x.IsHangCha)
                .ForAll(x =>
                {
                    x.FDuToan = 0;
                    x.FUocTH = 0;
                    x.FNCNam1 = 0;
                    x.FNCNam2 = 0;
                    x.FNCNam3 = 0;
                });
            var temp = Items.Where(x => !x.IsHangCha && !x.IsDeleted && x.IsFilter).ToList();
            var dictByMlns = Items.GroupBy(x => x.IIdMlskt).ToDictionary(x => x.Key, x => x.First());
            foreach (var item in temp)
            {
                CalculateParent(item.IdParent, item, dictByMlns);
            }

            UpdateTotal(temp);
        }

        private void CalculateParent(Guid idParent, NsNc3YChungTuChiTietModel item, Dictionary<Guid, NsNc3YChungTuChiTietModel> dictByMlns)
        {
            if (!dictByMlns.ContainsKey(idParent))
            {
                return;
            }

            var model = dictByMlns[idParent];
            model.FDuToan += item.FDuToan;
            model.FUocTH += item.FUocTH;
            model.FNCNam1 += item.FNCNam1;
            model.FNCNam2 += item.FNCNam2;
            model.FNCNam3 += item.FNCNam3;
            
            CalculateParent(model.IdParent, item, dictByMlns);
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
            var temp = (NsNc3YChungTuChiTietModel)obj;
            var condition = true;
            if (!string.IsNullOrEmpty(NsNc3YChungTuChiTietSearchModel.SKyHieu))
                condition = condition && temp.SKyHieu.ToLower()
                    .StartsWith(NsNc3YChungTuChiTietSearchModel.SKyHieu.Trim().ToLower());
            if (SelectedDataState != null)
            {
                var DataStateSelectedValue = int.Parse(SelectedDataState.ValueItem);
                switch (DataStateSelectedValue)
                {
                    case (int)DataStateValue.HIEN_THI_TAT_CA:
                        break;
                    case (int)DataStateValue.CO_SO_LIEU_DT_QT_SKT:
                        condition = condition && (temp.FDuToan > 0 || temp.FUocTH > 0 || temp.FNCNam1 > 0 || temp.FNCNam2 > 0 || temp.FNCNam3 > 0 );
                        break;
                    case (int)DataStateValue.DA_NHAP_SKT:
                        condition = condition && (temp.FNCNam1 > 0 || temp.FNCNam2 > 0 || temp.FNCNam3 > 0);
                        break;
                }
            }

            if (SelectedNNganhModel != null)
            {
                if (!String.IsNullOrEmpty(SelectedNNganhModel.SGiaTri))
                {
                    string[] values = SelectedNNganhModel.SGiaTri.Split(",");
                    List<NsNc3YChungTuChiTietModel> listParent = new List<NsNc3YChungTuChiTietModel>();
                    IsChild(temp, values, listParent);
                    condition = condition && (values.Contains(temp.Nganh) || listParent.Exists(item => values.Contains(item.Nganh)));
                }
            }

            temp.IsFilter = condition;
            return condition;
        }

        private void IsChild(NsNc3YChungTuChiTietModel parent, string[] values, List<NsNc3YChungTuChiTietModel> listParent)
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
            NsNc3YChungTuChiTietSearchModel = new NsNc3YChungTuChiTietModel();
        }

        private void UpdateTotal(List<NsNc3YChungTuChiTietModel> listChildren)
        {
            Model.TongDuToan = 0;
            Model.TongUocThucHien = 0;
            Model.FTongTuChi = 0;
            Model.FTongTuChiDeNghi = 0;
            Model.FTongPhanCap = 0;
            Model.TongTangSnc = 0;
            var roots = Items.Where(t => t.IdParent.Equals(Guid.Empty)).ToList();
            foreach (var item in roots)
            {
                Model.TongDuToan += item.FDuToan;
                Model.TongUocThucHien += item.FUocTH;
                Model.FTongTuChi += item.FNCNam1;
                Model.FTongTuChiDeNghi += item.FNCNam2;
                Model.FTongPhanCap += item.FNCNam3;
                Model.TongTangSnc += item.FNCNam1 + item.FNCNam2 + item.FNCNam3;
            }
        }        

        /// <summary>
        /// open screen print
        /// </summary>
        /// <param name="param"></param>
        public void OpenPrintDialog(object param)
        {
            var demandCheckPrintType = (DemandCheckPrintType)(int)param;
            object content;
            switch (demandCheckPrintType)
            {
                case DemandCheckPrintType.REPORT_ORG_DEMAND3Y_DETAIL_NUMBER:
                    PrintReportDemandOrgViewModel.DemandCheckPrintType = demandCheckPrintType;
                    if (Items != null && Items.Count > 0)
                    {
                        var listDonViCheckBox = Items.Select(item => new CheckBoxItem
                        {
                            ValueItem = item.IIdMaDonVi,
                            DisplayItem = string.Join("-", item.IIdMaDonVi, item.STenDonVi)
                        }).OrderBy(item => item.ValueItem);
                        PrintReportDemandOrgViewModel.ListDonVi = new ObservableCollection<CheckBoxItem>(listDonViCheckBox);
                    }
                    PrintReportDemandOrgViewModel.Init();
                    content = new PrintReportDemandOrg
                    {
                        DataContext = PrintReportDemandOrgViewModel
                    };
                    break;
                case DemandCheckPrintType.REPORT_DEMAND3Y_NUMBER_SUMMARY:
                    PrintReportDemandOrgViewModel.DemandCheckPrintType = demandCheckPrintType;
                    if (Items != null && Items.Count > 0)
                        PrintReportDemandOrgViewModel.Init();
                    content = new PrintReportDemandOrg
                    {
                        DataContext = PrintReportDemandOrgViewModel
                    };
                    break;
                default:
                    content = null;
                    break;
            }

            if (content != null)
            {
                DialogHost.Show(content, DemandCheckScreen.DETAIL_DIALOG, null, null);
            }
        }        

        protected override void OnSelectedItemChanged()
        {
            base.OnSelectedItemChanged();
            OnPropertyChanged(nameof(IsEnabledDelete));
        }

        public void LoadDonViFilter()
        {
            if (Items != null && Items.Count > 0 && _viewSummarySelected != null && _viewSummarySelected.ValueItem.Equals(TypeViewSummary.Detail.ToString()))
            {
                var lstIdDonVi = Items.Where(x => !x.IsHangCha).Select(x => x.IIdMaDonVi).Distinct().ToList();
                var predicate = PredicateBuilder.True<DonVi>();
                predicate = predicate.And(x => x.NamLamViec == _sessionInfo.YearOfWork);
                predicate = predicate.And(x => x.ITrangThai == StatusType.ACTIVE);
                predicate = predicate.And(x => lstIdDonVi.Contains(x.IIDMaDonVi));
                var lstDonVi = _iNsDonViService.FindByCondition(predicate);
                DonViItems = _mapper.Map<ObservableCollection<DonViModel>>(lstDonVi);
                _selectedDonVi = null;
            }
        }

        public void DeleteChiTiet(List<NsNc3YChungTuChiTietModel> lstItems)
        {
            if (lstItems.Count > 0)
            {
                foreach (var item in lstItems)
                {
                    var deleteItem = _nc3YChungTuChiTietService.FindById(item.Id);
                    if (deleteItem != null)
                    {
                        _mapper.Map(item, deleteItem);
                        _nc3YChungTuChiTietService.Delete(deleteItem);
                    }
                    item.FDuToan = 0;
                    item.FUocTH = 0;
                    item.FNCNam1 = 0;
                    item.FNCNam2 = 0;
                    item.FNCNam3 = 0;
                    item.SMoTa = string.Empty;
                    item.IsModified = false;
                    item.IsDeleted = false;
                }
            }
        }

        private void CalculateData(IEnumerable<NsNc3YChungTuChiTietModel> Items)
        {
            Items.Where(x => x.IsHangCha)
                .ForAll(x =>
                {
                    x.FDuToan = 0;
                    x.FUocTH = 0;
                    x.FNCNam1 = 0;
                    x.FNCNam2 = 0;
                    x.FNCNam3 = 0;
                });
            var temp = Items.Where(x => !x.IsHangCha && !x.IsDeleted && x.IsFilter).ToList();
            var dictByMlns = Items.GroupBy(x => x.IIdMlskt).ToDictionary(x => x.Key, x => x.First());
            foreach (var item in temp)
            {
                CalculateParent(item.IdParent, item, dictByMlns);
            }

            UpdateTotal(temp);
        }

        private bool IsUpdateToDelete(NsNc3YChungTuChiTietModel item)
        {
            return item.FDuToan == 0 && item.FUocTH == 0
                && item.FNCNam1 == 0 && item.FNCNam2 == 0
                && item.FNCNam3 == 0;
        }
    }
}