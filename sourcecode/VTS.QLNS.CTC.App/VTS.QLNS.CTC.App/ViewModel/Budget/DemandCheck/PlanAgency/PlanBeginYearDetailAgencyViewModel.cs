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
using VTS.QLNS.CTC.App.View.Budget.DemandCheck.CanCu;
using VTS.QLNS.CTC.App.View.Budget.DemandCheck.Plan;
using VTS.QLNS.CTC.App.ViewModel.Budget.DemandCheck.Plan;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Service;
using VTS.QLNS.CTC.Utility;
using VTS.QLNS.CTC.Utility.Enum;
using static VTS.QLNS.CTC.App.Model.PlanBeginYearModel;

namespace VTS.QLNS.CTC.App.ViewModel.Budget.DemandCheck.PlanAgency
{
    public class PlanBeginYearDetailAgencyViewModel : DetailViewModelBase<PlanBeginYearModel, SktSoLieuChiTietMLNSModel>
    {
        private readonly ISktSoLieuService _sktSoLieuService;
        private readonly ISktSoLieuChiTietCanCuDataService _sktSoLieuChiTietCanCuDataService;
        private readonly ISktSoLieuChiTietCanCuService _sktSoLieuChiTietCanCuService;
        private readonly ISktMucLucService _sktMucLucService;
        private readonly ISktChungTuChiTietService _sktChungTuChiTietService;
        private readonly ICauHinhCanCuService _iCauHinhCanCuService;
        private readonly ILog _logger;
        private readonly ISessionService _sessionService;
        private readonly ISktSoLieuChungTuService _sktChungTuService;
        private readonly ISktChungTuService _iSktChungTuService;
        private readonly INsNguoiDungLnsService _nsNguoiDungLNSService;
        private readonly IMapper _mapper;
        private ICollectionView _dataDetailFilter;
        private ICollectionView _dataMuclucFilter;
        private IDanhMucService _danhMucService;
        private INsDonViService _nsDonViService;
        private INsMucLucNganSachService _nsMucLucNganSachService;
        private List<SktSoLieuChiTietMlnsQuery> _dataDetailPlanBeginYear;
        private List<SktSoLieuChiTietMlnsQuery> _dataDetailPlanBeginYearDynamic;

        private int _countLoop;

        public delegate void DataChangedEventHandler(object sender, EventArgs e);
        public event DataChangedEventHandler ClosePopup;
        public event DataChangedEventHandler RefeshIndexWindow;
        public bool LoadComboboxDone = false;

        public override Type ContentType => typeof(View.Budget.DemandCheck.PlanAgency.PlanBeginYearDetailAgency);
        public override PackIconKind IconKind => PackIconKind.FileDocumentBoxMultiple;
        public bool IsSaveData => (Items != null && Items.Any(item => item.IsModified || item.IsDeleted || item.IsUpdateCanCu || (LstCanCu != null && LstCanCu.Where(n => !n.IsSaved).Count() > 0)) && !IsReadOnlyTable);
        public bool IsEnableButtonDelete => SelectedItem != null && !SelectedItem.IsHangCha && !IsReadOnlyTable;
        public bool IsEnableButtonCanCu => !IsReadOnlyTable;
        public bool IsDonViC4Only = false;
        public PrintReportChiTietDuToanDonViViewModel PrintReportChiTietDuToanDonViViewModel { get; }
        public PlanBeginYearDetailChildViewModel PlanBeginYearDetailChildViewModel { get; }
        public PrintReportCompareDemandCheckViewModel PrintReportCompareDemandCheckViewModel { get; }
        public TongHopCanCuDuToanDauNamViewModel TongHopCanCuDuToanDauNamViewModel { get; }

        public string HeaderQuyetToan => "Quyết toán " + (_sessionService.Current.YearOfWork - 2);
        public string HeaderDuToan => "Dự toán " + (_sessionService.Current.YearOfWork - 1);
        public string HeaderThucHien => "Ước t.hiện " + (_sessionService.Current.YearOfWork - 1);
        public string HeaderChiTiet => "Chi tiết " + _sessionService.Current.YearOfWork;
        public string HeaderMucMuc => "SỐ KIỂM TRA " + _sessionService.Current.YearOfWork;
        public bool IsDeleteAll => Items.Any(item => !item.IsModified && item.HasData) && !IsReadOnlyTable;

        private ObservableCollection<SktMucLucDuToanDauNamModel> _dataMucLuc;
        public ObservableCollection<SktMucLucDuToanDauNamModel> DataMucLuc
        {
            get => _dataMucLuc;
            set => SetProperty(ref _dataMucLuc, value);
        }

        private SktMucLucDuToanDauNamModel _selectedMucLuc;
        public SktMucLucDuToanDauNamModel SelectedMucLuc
        {
            get => _selectedMucLuc;
            set
            {
                SetProperty(ref _selectedMucLuc, value);
                if (SetProperty(ref _selectedMucLuc, value) && _dataDetailFilter != null && LoadComboboxDone)
                {
                    GetListParentFilter();
                    OnRefeshFilter();
                }
            }
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
                if (SetProperty(ref _selectedTypeShowAgency, value) && _selectedTypeShowAgency != null)
                {
                    LoadData();
                    OnRefeshFilter();
                }
            }
        }

        private ObservableCollection<ComboboxItem> _typeDisplays;
        public ObservableCollection<ComboboxItem> TypeDisplays
        {
            get => _typeDisplays;
            set => SetProperty(ref _typeDisplays, value);
        }

        private string _selectedTypeDisplays;
        public string SelectedTypeDisplays
        {
            get => _selectedTypeDisplays;
            set
            {
                if (SetProperty(ref _selectedTypeDisplays, value) && _dataDetailFilter != null && LoadComboboxDone)
                {
                    GetListParentFilter();
                    OnRefeshFilter();
                }
            }
        }

        private ObservableCollection<ComboboxItem> _typeDisplaysMucLuc;
        public ObservableCollection<ComboboxItem> TypeDisplaysMucLuc
        {
            get => _typeDisplaysMucLuc;
            set => SetProperty(ref _typeDisplaysMucLuc, value);
        }

        private string _selectedTypeDisplaysMucLuc;
        public string SelectedTypeDisplaysMucLuc
        {
            get => _selectedTypeDisplaysMucLuc;
            set
            {
                if (SetProperty(ref _selectedTypeDisplaysMucLuc, value) && _dataMuclucFilter != null)
                {
                    if (_dataMuclucFilter != null && _selectedTypeDisplaysMucLuc != null)
                        _dataMuclucFilter.Refresh();
                    if (DataMucLuc != null && DataMucLuc.Count > 0)
                    {
                        SelectedMucLuc = DataMucLuc.FirstOrDefault();
                    }
                }
            }
        }

        private bool _isReadOnlyTable;
        public bool IsReadOnlyTable
        {
            get => _isReadOnlyTable;
            set => SetProperty(ref _isReadOnlyTable, value);
        }

        private bool _isShowTypeAgency;
        public bool IsShowTypeAgency
        {
            get => _isShowTypeAgency;
            set => SetProperty(ref _isShowTypeAgency, value);
        }

        private bool _isReadOnlyData;
        public bool IsReadOnlyData
        {
            get => _isReadOnlyData;
            set => SetProperty(ref _isReadOnlyData, value);
        }

        private bool _isGetDonVi0Lock;
        public bool IsGetDonVi0Lock
        {
            get => _isGetDonVi0Lock;
            set => SetProperty(ref _isGetDonVi0Lock, value);
        }

        private bool _isLock;
        public bool IsLock
        {
            get => _isLock;
            set => SetProperty(ref _isLock, value);
        }

        private ObservableCollection<ComboboxItem> _loaiNganSach;
        public ObservableCollection<ComboboxItem> LoaiNganSach
        {
            get => _loaiNganSach;
            set => SetProperty(ref _loaiNganSach, value);
        }

        private ComboboxItem _selectedLoaiNganSach;
        public ComboboxItem SelectedLoaiNganSach
        {
            get => _selectedLoaiNganSach;
            set
            {
                if (SetProperty(ref _selectedLoaiNganSach, value) && _dataDetailFilter != null
                    && LoaiChungTu != null && LoaiChungTu == VoucherType.NSBD_Key && LoadComboboxDone)
                {
                    GetListParentFilter();
                    OnRefeshFilter();
                }
            }
        }

        private string _loaiChungTu;
        public string LoaiChungTu
        {
            get => _loaiChungTu;
            set => SetProperty(ref _loaiChungTu, value);
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

        private List<NguoiDungDonVi> _listNguoiDungDonVi;
        public List<NguoiDungDonVi> ListNguoiDungDonVi
        {
            get => _listNguoiDungDonVi;
            set => SetProperty(ref _listNguoiDungDonVi, value);
        }

        public Visibility VisibleNganSachSuDung => LoaiChungTu == VoucherType.NSSD_Key ? Visibility.Visible : Visibility.Collapsed;
        public Visibility VisibleNganSachDamBao => LoaiChungTu == VoucherType.NSBD_Key ? Visibility.Visible : Visibility.Collapsed;

        public List<SktSoLieuChiTietMLNSModel> ListParentFilter;
        public List<SktSoLieuChiTietMLNSModel> ListChildFilter;
        public List<SktMucLucDuToanDauNamModel> ListMucLucSelected;


        public Visibility VisibilityAgencyRoot { get; set; }
        public Visibility TC1Visible { get; set; }
        public Visibility TC2Visible { get; set; }
        public Visibility TC3Visible { get; set; }
        public Visibility TC4Visible { get; set; }
        public Visibility TC5Visible { get; set; }

        public Visibility HangNhap1Visible { get; set; }
        public Visibility HangMua1Visible { get; set; }
        public Visibility PhanCap1Visible { get; set; }

        public Visibility HangNhap2Visible { get; set; }
        public Visibility HangMua2Visible { get; set; }
        public Visibility PhanCap2Visible { get; set; }

        public Visibility HangNhap3Visible { get; set; }
        public Visibility HangMua3Visible { get; set; }
        public Visibility PhanCap3Visible { get; set; }

        public Visibility HangNhap4Visible { get; set; }
        public Visibility HangMua4Visible { get; set; }
        public Visibility PhanCap4Visible { get; set; }

        public Visibility HangNhap5Visible { get; set; }
        public Visibility HangMua5Visible { get; set; }
        public Visibility PhanCap5Visible { get; set; }

        public bool IsReadOnlyX1 { get; set; }
        public bool IsReadOnlyX2 { get; set; }
        public bool IsReadOnlyX3 { get; set; }
        public bool IsReadOnlyX4 { get; set; }
        public bool IsReadOnlyX5 { get; set; }

        public string TC1 { get; set; }
        public string TC2 { get; set; }
        public string TC3 { get; set; }
        public string TC4 { get; set; }
        public string TC5 { get; set; }

        public string HangNhap1 { get; set; }
        public string HangMua1 { get; set; }
        public string PhanCap1 { get; set; }

        public string HangNhap2 { get; set; }
        public string HangMua2 { get; set; }
        public string PhanCap2 { get; set; }

        public string HangNhap3 { get; set; }
        public string HangMua3 { get; set; }
        public string PhanCap3 { get; set; }

        public string HangNhap4 { get; set; }
        public string HangMua4 { get; set; }
        public string PhanCap4 { get; set; }

        public string HangNhap5 { get; set; }
        public string HangMua5 { get; set; }
        public string PhanCap5 { get; set; }
        public bool IsInit { get; set; }

        public RelayCommand SaveDataCommand { get; }
        public RelayCommand SelectionChangedMucLucCommand { get; }
        public RelayCommand RefreshMucLucDataCommand { get; set; }
        public RelayCommand CloseWindowCommand { get; }
        public RelayCommand ClosingWindowCommand { get; }
        public RelayCommand ShowPopupReportChiTietCommand { get; }

        public RelayCommand ShowPopupChildCommand { get; }
        public RelayCommand ShowPopupReportCompareCommand { get; }
        public RelayCommand CanCuCommand { get; }

        public PlanBeginYearDetailAgencyViewModel(ISktSoLieuService sktSoLieuService,
           ISktMucLucService sktMucLucService,
           ISktSoLieuChungTuService sktChungTuService,
           IMapper mapper,
           ISessionService sessionService,
           IDanhMucService danhMucService,
           INsDonViService nsDonViService,
           INsMucLucNganSachService nsMucLucNganSachService,
           ICauHinhCanCuService iCauHinhCanCuService,
           ISktSoLieuChiTietCanCuDataService sktSoLieuChiTietCanCuDataService,
           ISktChungTuChiTietService sktChungTuChiTietService,
           ISktSoLieuChiTietCanCuService sktSoLieuChiTietCanCuService,
           ISktChungTuService iSktChungTuService,
           INsNguoiDungLnsService nsNguoiDungLNSService,
           ILog logger,
           PrintReportChiTietDuToanDonViViewModel printReportChiTietDuToanDonViViewModel,
           PrintReportCompareDemandCheckViewModel printReportCompareDemandCheckViewModel,
           TongHopCanCuDuToanDauNamViewModel tongHopCanCuDuToanDauNamViewModel,
           PlanBeginYearDetailChildViewModel planBeginYearDetailChildViewModel) : base(danhMucService, sessionService)
        {
            _mapper = mapper;
            _logger = logger;
            _sessionService = sessionService;
            _sktSoLieuService = sktSoLieuService;
            _sktMucLucService = sktMucLucService;
            _danhMucService = danhMucService;
            _nsDonViService = nsDonViService;
            _nsMucLucNganSachService = nsMucLucNganSachService;
            _iCauHinhCanCuService = iCauHinhCanCuService;
            _sktSoLieuChiTietCanCuDataService = sktSoLieuChiTietCanCuDataService;
            _sktChungTuChiTietService = sktChungTuChiTietService;
            _sktSoLieuChiTietCanCuService = sktSoLieuChiTietCanCuService;
            _sktChungTuService = sktChungTuService;
            _iSktChungTuService = iSktChungTuService;
            _nsNguoiDungLNSService = nsNguoiDungLNSService;

            SaveDataCommand = new RelayCommand(obj => OnSaveData());
            SelectionChangedMucLucCommand = new RelayCommand(obj => OnSelectionChangedMucLuc());
            RefreshMucLucDataCommand = new RelayCommand(obj => OnRefreshMucLucData());
            CloseWindowCommand = new RelayCommand(obj => OnCloseWindow());
            ClosingWindowCommand = new RelayCommand(obj => OnRefeshIndexWindow());
            ShowPopupReportChiTietCommand = new RelayCommand(obj => OnShowPopupReportChiTiet());
            ShowPopupChildCommand = new RelayCommand(o => OnShowPopupChild());
            ShowPopupReportCompareCommand = new RelayCommand(obj => OnShowPopupReportCompare());
            PrintReportChiTietDuToanDonViViewModel = printReportChiTietDuToanDonViViewModel;
            PlanBeginYearDetailChildViewModel = planBeginYearDetailChildViewModel;
            PrintReportCompareDemandCheckViewModel = printReportCompareDemandCheckViewModel;
            TongHopCanCuDuToanDauNamViewModel = tongHopCanCuDuToanDauNamViewModel;
            CanCuCommand = new RelayCommand(obj => OnCanCu());
        }

        protected override void OnDeleteAll()
        {
            base.OnDeleteAll();
            var result = MessageBox.Show(Resources.DeleteAllChungTuChiTiet, Resources.ConfirmTitle, MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.No)
                return;
            else if (result == MessageBoxResult.Yes)
            {
                Guid chungTuId = AddChungTu();
                _sktSoLieuService.DeleteByVoucherId(chungTuId);
                _sktChungTuService.UpdateTotalChungTu(Model.Id_DonVi, Model.Loai,
                    int.Parse(LoaiChungTu), _sessionService.Current.YearOfWork, _sessionService.Current.YearOfBudget, Model.ILoaiNguonNganSach ?? 0, _sessionService.Current.Budget);
                LoadData();
                MessageBox.Show(Resources.MsgDeleteSuccess, Resources.NotifiTitle, MessageBoxButton.OK, MessageBoxImage.Information);
                OnPropertyChanged(nameof(IsDeleteAll));
            }
        }

        private void OnRefreshMucLucData()
        {
            if (IsSaveData)
            {
                var message = Resources.MsgConfirmEdit;
                var messageBox = new NSMessageBoxViewModel(message, Resources.ConfirmTitle, NSMessageBoxButtons.YesNoCancel, RefeshMucLucHandler);
                DialogHost.Show(messageBox.Content, "PlanBeginYearDetailDialog");
            }
            else
            {
                IsInit = true;
                LoadDataMucLuc();
                LoadData();
                IsInit = false;
            }
        }

        private void OnCanCu()
        {
            TongHopCanCuDuToanDauNamViewModel.SktChungTuModel = Model;
            TongHopCanCuDuToanDauNamViewModel.CauHinhCanCuTemp = LstCanCu;
            TongHopCanCuDuToanDauNamViewModel.ListIdNhomNganhSelected = null;
            TongHopCanCuDuToanDauNamViewModel.ListIdMlnsSelected = null;
            TongHopCanCuDuToanDauNamViewModel.LoaiChungTu = LoaiChungTu;
            TongHopCanCuDuToanDauNamViewModel.Init();
            TongHopCanCuDuToanDauNamViewModel.SavedAction = obj =>
            {
                LoadData();
                LoadCanCu((ObservableCollection<CauHinhCanCuModel>)obj);
            };
            var view = new TongHopCanCu()
            {
                DataContext = TongHopCanCuDuToanDauNamViewModel
            };
            DialogHost.Show(view, "PlanBeginYearDetailDialog", null, null);
        }

        private void RefeshMucLucHandler(NSDialogResult result)
        {
            if (result == NSDialogResult.Cancel) return;
            if (result == NSDialogResult.Yes)
            {
                OnSaveData();
            }
            LoadDataMucLuc();
            LoadData();
        }

        protected override void OnSelectedItemChanged()
        {
            OnPropertyChanged(nameof(IsEnableButtonDelete));
        }

        protected override void OnRefresh()
        {
            if (IsSaveData)
            {
                var message = Resources.MsgConfirmEdit;
                var messageBox = new NSMessageBoxViewModel(message, Resources.ConfirmTitle, NSMessageBoxButtons.YesNoCancel, RefeshDataHandler);
                DialogHost.Show(messageBox.Content, "PlanBeginYearDetailDialog");
            }
            else
            {
                IsInit = true;
                LoadData();
                IsInit = false;
            }
        }

        private void RefeshDataHandler(NSDialogResult result)
        {
            if (result == NSDialogResult.Cancel) return;
            if (result == NSDialogResult.Yes)
            {
                OnSaveData();
            }
            LoadData();
        }

        private void OnSaveData()
        {
            try
            {
                List<SktSoLieuChiTietMLNSModel> listAdd = Items.Where(x => x.IsModified && (x.IdDb == Guid.Empty || x.IdDb == null) && !x.IsDeleted).ToList();
                List<SktSoLieuChiTietMLNSModel> listUpdate = Items.Where(x => x.IsModified && x.IdDb != Guid.Empty && x.IdDb != null && !x.IsDeleted).ToList();
                List<SktSoLieuChiTietMLNSModel> listDelete = Items.Where(x => x.IsDeleted && x.IdDb != Guid.Empty && x.IdDb != null).ToList();

                //chung tu
                Guid chungTuId = AddChungTu();

                if (listAdd.Count > 0)
                {
                    List<NsDtdauNamChungTuChiTiet> listChiTiet = new List<NsDtdauNamChungTuChiTiet>();
                    listChiTiet = _mapper.Map<List<NsDtdauNamChungTuChiTiet>>(listAdd);
                    listChiTiet = listChiTiet.Select(x =>
                    {
                        x.ILoaiChungTu = LoaiChungTu;
                        x.INamNganSach = _sessionService.Current.YearOfBudget;
                        x.IIdMaNguonNganSach = _sessionService.Current.Budget;
                        x.INamLamViec = _sessionService.Current.YearOfWork;
                        x.ILoai = 3;
                        x.DNgayTao = DateTime.Now;
                        x.SNguoiTao = _sessionService.Current.Principal;
                        x.IIdMaDonVi = Model.Id_DonVi;
                        x.STenDonVi = Model.TenDonVi;
                        x.IIdCtdtdauNam = chungTuId;
                        return x;
                    }).ToList();
                    _sktSoLieuService.AddRange(listChiTiet);
                    listAdd.Select(n => { n.IdDb = n.Id; return n; }).ToList();
                }

                if (listUpdate.Count > 0)
                {
                    foreach (var item in listUpdate)
                    {
                        item.IsModified = false;
                        NsDtdauNamChungTuChiTiet solieuChiTiet = _sktSoLieuService.Find(item.Id);
                        if (solieuChiTiet != null)
                        {
                            solieuChiTiet.FUocThucHien = item.UocThucHien;
                            solieuChiTiet.FTuChi = item.ChiTiet;
                            solieuChiTiet.FHangNhap = item.HangNhap;
                            solieuChiTiet.FHangMua = item.HangMua;
                            solieuChiTiet.FPhanCap = item.PhanCap;
                            solieuChiTiet.FChuaPhanCap = item.ChuaPhanCap;
                            solieuChiTiet.DNgaySua = DateTime.Now;
                            solieuChiTiet.SNguoiSua = _sessionService.Current.Principal;
                            _sktSoLieuService.Update(solieuChiTiet);
                        }
                    }
                }

                if (listDelete.Count > 0)
                {
                    foreach (var item in listDelete)
                    {
                        _sktSoLieuService.Delete(item.Id);
                    }
                }

                //can cu
                SaveCanCu();

                //update tong
                _sktChungTuService.UpdateTotalChungTu(Model.Id_DonVi, Model.Loai, int.Parse(LoaiChungTu),
                    _sessionService.Current.YearOfWork, _sessionService.Current.YearOfBudget, Model.ILoaiNguonNganSach ?? 0, _sessionService.Current.Budget);

                OnPropertyChanged(nameof(IsSaveData));
                var message = Resources.MsgSaveDone;
                var messageBox = new NSMessageBoxViewModel(message, Resources.NotifiTitle, NSMessageBoxButtons.OK, null);
                DialogHost.Show(messageBox.Content, "PlanBeginYearDetailDialog");
                ReFreshDataItems();
                OnPropertyChanged(nameof(IsDeleteAll));
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        private Guid AddChungTu()
        {
            var predicate = PredicateBuilder.True<NsDtdauNamChungTu>();
            predicate = predicate.And(x => x.INamLamViec == _sessionService.Current.YearOfWork);
            predicate = predicate.And(x => x.IIdMaDonVi == Model.Id_DonVi);
            predicate = predicate.And(x => x.ILoaiChungTu == int.Parse(LoaiChungTu));
            predicate = predicate.And(x => x.INamNganSach == _sessionService.Current.YearOfBudget);
            predicate = predicate.And(x => x.IIdMaNguonNganSach == _sessionService.Current.Budget);

            NsDtdauNamChungTu chungTu = _sktChungTuService.FindByCondition(predicate).FirstOrDefault();
            if (chungTu == null)
            {
                int soChungTuIndex = _sktChungTuService.GetSoChungTuIndexByCondition(_sessionService.Current.YearOfWork,
                   _sessionService.Current.Budget, _sessionService.Current.YearOfBudget, int.Parse(LoaiChungTu));
                NsDtdauNamChungTu entity = new NsDtdauNamChungTu();
                entity.SSoChungTu = "DTDN-" + soChungTuIndex.ToString("D3");
                entity.IIdMaDonVi = Model.Id_DonVi;
                entity.INamLamViec = _sessionService.Current.YearOfWork;
                entity.DNgayTao = DateTime.Now;
                entity.SNguoiTao = _sessionService.Current.Principal;
                entity.BKhoa = false;
                entity.ILoaiChungTu = int.Parse(LoaiChungTu);
                entity.INamNganSach = _sessionService.Current.YearOfBudget;
                entity.IIdMaNguonNganSach = _sessionService.Current.Budget;
                _sktChungTuService.Add(entity);
                return entity.Id;
            }
            else
            {
                return chungTu.Id;
            }
        }

        public void SaveCanCu()
        {
            List<SktSoLieuChiTietMLNSModel> detailsCanCu = Items.Where(x => !x.IsDeleted && !x.IsHangCha && x.IsUpdateCanCu).ToList();
            if (LstCanCu != null)
            {
                List<NsDtdauNamChungTuChungTuCanCu> listCanCuChungTus = new List<NsDtdauNamChungTuChungTuCanCu>();
                List<NsDtdauNamChungTuChiTietCanCu> listCanCuChiTiet = new List<NsDtdauNamChungTuChiTietCanCu>();

                //xoa can cu chung tu 
                var predicate = PredicateBuilder.True<NsDtdauNamChungTuChungTuCanCu>();
                predicate = predicate.And(x => x.IIdMaDonVi == Model.Id_DonVi);
                predicate = predicate.And(x => x.INamLamViec == _sessionService.Current.YearOfWork);
                predicate = predicate.And(x => x.ILoaiChungTu == int.Parse(LoaiChungTu));
                var sktcanCanCuChungTus = _sktSoLieuChiTietCanCuService.FindByCondition(predicate).ToList();
                //_sktSoLieuChiTietCanCuService.RemoveRange(sktcanCanCuChungTus);
                foreach (var item in sktcanCanCuChungTus)
                {
                    _sktSoLieuChiTietCanCuService.Delete(item.Id);
                }
                var count = 0;
                foreach (var cc in LstCanCu)
                {
                    //xoa can cu chung tu chi tiet
                    var predicateCT = PredicateBuilder.True<NsDtdauNamChungTuChiTietCanCu>();
                    predicateCT = predicateCT.And(x => x.IIdCanCu == cc.Id);
                    predicateCT = predicateCT.And(x => x.IIdMaDonVi == Model.Id_DonVi);
                    predicateCT = predicateCT.And(x => x.ILoaiChungTu == int.Parse(LoaiChungTu));
                    var sktcanCanCus = _sktSoLieuChiTietCanCuDataService.FindByCondition(predicateCT).ToList();

                    foreach (var item in sktcanCanCus)
                    {
                        _sktSoLieuChiTietCanCuDataService.Delete(item.Id);
                    }

                    if (cc.LstIdChungTuCanCu != null && cc.LstIdChungTuCanCu.Count > 0)
                    {
                        if (cc.IThietLap != 3)
                        {
                            foreach (var idChungTu in cc.LstIdChungTuCanCu)
                            {
                                NsDtdauNamChungTuChungTuCanCu canCuChungTu = new NsDtdauNamChungTuChungTuCanCu();
                                canCuChungTu.IIdCtcanCu = idChungTu;
                                canCuChungTu.IIdCanCu = cc.Id;
                                canCuChungTu.ILoaiChungTu = int.Parse(LoaiChungTu);
                                canCuChungTu.IIdMaDonVi = Model.Id_DonVi;
                                canCuChungTu.INamLamViec = _sessionService.Current.YearOfWork;
                                listCanCuChungTus.Add(canCuChungTu);
                            }
                        }
                        else
                        {
                            NsDtdauNamChungTuChungTuCanCu canCuChungTu = new NsDtdauNamChungTuChungTuCanCu();
                            canCuChungTu.IIdCtcanCu = cc.IdChungTuCanCuLuyKe;
                            canCuChungTu.IIdCanCu = cc.Id;
                            canCuChungTu.ILoaiChungTu = int.Parse(LoaiChungTu);
                            canCuChungTu.IIdMaDonVi = Model.Id_DonVi;
                            canCuChungTu.INamLamViec = _sessionService.Current.YearOfWork;
                            listCanCuChungTus.Add(canCuChungTu);
                        }

                        foreach (var item in detailsCanCu)
                        {
                            NsDtdauNamChungTuChiTietCanCu canCuChungTu = new NsDtdauNamChungTuChiTietCanCu();
                            canCuChungTu = _mapper.Map<NsDtdauNamChungTuChiTietCanCu>(item);
                            canCuChungTu.Id = Guid.Empty;
                            canCuChungTu.IIdCanCu = cc.Id;
                            canCuChungTu.ILoaiChungTu = int.Parse(LoaiChungTu);
                            canCuChungTu.INamNganSach = _sessionService.Current.YearOfBudget;
                            canCuChungTu.IIdMaNguonNganSach = _sessionService.Current.Budget;
                            canCuChungTu.INamLamViec = _sessionService.Current.YearOfWork;
                            //canCuChungTu.ILoai = 3;
                            canCuChungTu.DNgayTao = DateTime.Now;
                            canCuChungTu.SNguoiTao = _sessionService.Current.Principal;
                            canCuChungTu.IIdMaDonVi = Model.Id_DonVi;
                            canCuChungTu.STenDonVi = Model.TenDonVi;
                            canCuChungTu.FTuChi = 0;
                            canCuChungTu.FHienVat = 0;
                            canCuChungTu.FHangNhap = 0;
                            canCuChungTu.FHangMua = 0;
                            canCuChungTu.FPhanCap = 0;
                            canCuChungTu.FChuaPhanCap = 0;

                            if (item.X1 != null && count == 0)
                            {
                                canCuChungTu.FTuChi = item.X1.TuChi;
                                canCuChungTu.FHangNhap = item.X1.HangNhap;
                                canCuChungTu.FHangMua = item.X1.HangMua;
                                canCuChungTu.FPhanCap = item.X1.PhanCap;
                            }
                            if (item.X2 != null && count == 1)
                            {
                                canCuChungTu.FTuChi = item.X2.TuChi;
                                canCuChungTu.FHangNhap = item.X2.HangNhap;
                                canCuChungTu.FHangMua = item.X2.HangMua;
                                canCuChungTu.FPhanCap = item.X2.PhanCap;
                            }
                            if (item.X3 != null && count == 2)
                            {
                                canCuChungTu.FTuChi = item.X3.TuChi;
                                canCuChungTu.FHangNhap = item.X3.HangNhap;
                                canCuChungTu.FHangMua = item.X3.HangMua;
                                canCuChungTu.FPhanCap = item.X3.PhanCap;
                            }
                            if (item.X4 != null && count == 3)
                            {
                                canCuChungTu.FTuChi = item.X4.TuChi;
                                canCuChungTu.FHangNhap = item.X4.HangNhap;
                                canCuChungTu.FHangMua = item.X4.HangMua;
                                canCuChungTu.FPhanCap = item.X4.PhanCap;
                            }
                            if (item.X5 != null && count == 4)
                            {
                                canCuChungTu.FTuChi = item.X5.TuChi;
                                canCuChungTu.FHangNhap = item.X5.HangNhap;
                                canCuChungTu.FHangMua = item.X5.HangMua;
                                canCuChungTu.FPhanCap = item.X5.PhanCap;
                            }

                            listCanCuChiTiet.Add(canCuChungTu);
                        }
                    }
                    count++;
                }
                LstCanCu.Select(n => { n.IsSaved = true; return n; }).ToList();
                _sktSoLieuChiTietCanCuService.AddRange(listCanCuChungTus);
                _sktSoLieuChiTietCanCuDataService.AddRange(listCanCuChiTiet);
            }
            else if (_lstCanCuInit != null && _lstCanCuInit.Count > 0)
            {
                List<NsDtdauNamChungTuChiTietCanCu> listCanCus = new List<NsDtdauNamChungTuChiTietCanCu>();
                var count = 0;
                foreach (var cc in _lstCanCuInit)
                {
                    foreach (var item in detailsCanCu)
                    {
                        var predicateCT = PredicateBuilder.True<NsDtdauNamChungTuChiTietCanCu>();
                        predicateCT = predicateCT.And(x => x.IIdCanCu == cc.Id);
                        predicateCT = predicateCT.And(x => x.IIdMaDonVi == Model.Id_DonVi);
                        predicateCT = predicateCT.And(x => x.ILoaiChungTu == int.Parse(LoaiChungTu));
                        predicateCT = predicateCT.And(x => x.SXauNoiMa == item.XauNoiMa);

                        var sktcanCanCus = _sktSoLieuChiTietCanCuDataService.FindByCondition(predicateCT).ToList();
                        //_sktSoLieuChiTietCanCuDataService.RemoveRange(sktcanCanCus);
                        foreach (var itemDelete in sktcanCanCus)
                        {
                            _sktSoLieuChiTietCanCuDataService.Delete(itemDelete.Id);
                        }

                        NsDtdauNamChungTuChiTietCanCu canCuChungTu = new NsDtdauNamChungTuChiTietCanCu();
                        canCuChungTu = _mapper.Map<NsDtdauNamChungTuChiTietCanCu>(item);
                        canCuChungTu.Id = Guid.Empty;
                        canCuChungTu.IIdCanCu = cc.Id;
                        canCuChungTu.ILoaiChungTu = int.Parse(LoaiChungTu);
                        canCuChungTu.INamNganSach = _sessionService.Current.YearOfBudget;
                        canCuChungTu.IIdMaNguonNganSach = _sessionService.Current.Budget;
                        canCuChungTu.INamLamViec = _sessionService.Current.YearOfWork;
                        //canCuChungTu.ILoai = 3;
                        canCuChungTu.DNgayTao = DateTime.Now;
                        canCuChungTu.SNguoiTao = _sessionService.Current.Principal;
                        canCuChungTu.IIdMaDonVi = Model.Id_DonVi;
                        canCuChungTu.STenDonVi = Model.TenDonVi;
                        canCuChungTu.FTuChi = 0;
                        canCuChungTu.FHienVat = 0;
                        canCuChungTu.FHangNhap = 0;
                        canCuChungTu.FHangMua = 0;
                        canCuChungTu.FPhanCap = 0;
                        canCuChungTu.FChuaPhanCap = 0;

                        if (item.X1 != null && count == 0)
                        {
                            canCuChungTu.FTuChi = item.X1.TuChi;
                            canCuChungTu.FHangNhap = item.X1.HangNhap;
                            canCuChungTu.FHangMua = item.X1.HangMua;
                            canCuChungTu.FPhanCap = item.X1.PhanCap;
                        }
                        if (item.X2 != null && count == 1)
                        {
                            canCuChungTu.FTuChi = item.X2.TuChi;
                            canCuChungTu.FHangNhap = item.X2.HangNhap;
                            canCuChungTu.FHangMua = item.X2.HangMua;
                            canCuChungTu.FPhanCap = item.X2.PhanCap;
                        }
                        if (item.X3 != null && count == 2)
                        {
                            canCuChungTu.FTuChi = item.X3.TuChi;
                            canCuChungTu.FHangNhap = item.X3.HangNhap;
                            canCuChungTu.FHangMua = item.X3.HangMua;
                            canCuChungTu.FPhanCap = item.X3.PhanCap;
                        }
                        if (item.X4 != null && count == 3)
                        {
                            canCuChungTu.FTuChi = item.X4.TuChi;
                            canCuChungTu.FHangNhap = item.X4.HangNhap;
                            canCuChungTu.FHangMua = item.X4.HangMua;
                            canCuChungTu.FPhanCap = item.X4.PhanCap;
                        }
                        if (item.X5 != null && count == 4)
                        {
                            canCuChungTu.FTuChi = item.X5.TuChi;
                            canCuChungTu.FHangNhap = item.X5.HangNhap;
                            canCuChungTu.FHangMua = item.X5.HangMua;
                            canCuChungTu.FPhanCap = item.X5.PhanCap;
                        }

                        listCanCus.Add(canCuChungTu);
                    }

                    count++;
                }
                _sktSoLieuChiTietCanCuDataService.AddRange(listCanCus);
            }
            if (detailsCanCu != null && detailsCanCu.Count > 0)
            {
                detailsCanCu.Select(n => { n.IsUpdateCanCu = false; return n; }).ToList();
            }
            if (Items != null && Items.Count > 0)
            {
                Items.Where(n => n.IsHangCha).Select(n => { n.IsUpdateCanCu = false; return n; }).ToList();
            }
        }

        private void ReFreshDataItems()
        {
            Items.Where(x => x.IsModified).Select(x =>
            {
                x.IsModified = false;
                return x;
            }).ToList();
            Items.Where(x => x.IsDeleted).Select(x =>
            {
                x.ChiTiet = 0;
                x.UocThucHien = 0;
                x.HangMua = 0;
                x.HangNhap = 0;
                x.PhanCap = 0;
                x.ChuaPhanCap = 0;
                x.IdDb = null;
                x.IsModified = false;
                x.IsDeleted = false;
                return x;
            }).ToList();
            OnPropertyChanged(nameof(Items));
            OnPropertyChanged(nameof(IsSaveData));
        }

        protected override void OnDelete()
        {
            if (Items != null && Items.Count > 0 && SelectedItem != null && !SelectedItem.IsHangCha && !IsReadOnlyTable)
            {
                SelectedItem.IsDeleted = !SelectedItem.IsDeleted;
                CalculateData();
                OnPropertyChanged(nameof(IsSaveData));
            }
        }

        private void CreateRow(int indexRow)
        {
            SktSoLieuChiTietMLNSModel itemNewRow = new SktSoLieuChiTietMLNSModel();
            itemNewRow.MlnsId = SelectedItem.MlnsId;
            itemNewRow.MlnsIdParent = SelectedItem.MlnsIdParent;
            itemNewRow.XauNoiMa = SelectedItem.XauNoiMa;
            itemNewRow.LNS = SelectedItem.LNS;
            itemNewRow.L = SelectedItem.L;
            itemNewRow.K = SelectedItem.K;
            itemNewRow.M = SelectedItem.M;
            itemNewRow.TM = SelectedItem.TM;
            itemNewRow.TTM = SelectedItem.TTM;
            itemNewRow.NG = SelectedItem.NG;
            itemNewRow.TNG = SelectedItem.TNG;
            itemNewRow.MoTa = SelectedItem.MoTa;
            itemNewRow.Chuong = SelectedItem.Chuong;
            itemNewRow.IsHangCha = false;
            itemNewRow.IdDonVi = Model.Id_DonVi;
            itemNewRow.TenDonVi = Model.TenDonVi;
            itemNewRow.SKT_KyHieu = SelectedItem.SKT_KyHieu;
            itemNewRow.IsModified = true;
            itemNewRow.PropertyChanged += DetailModel_PropertyChanged;
            Items.Insert(indexRow, itemNewRow);
            OnPropertyChanged(nameof(Items));
            OnPropertyChanged(nameof(IsSaveData));
        }

        private List<NsNguoiDungLns> GetListLNSByUser()
        {
            var predicate = PredicateBuilder.True<NsNguoiDungLns>();
            predicate = predicate.And(x => x.INamLamViec == _sessionService.Current.YearOfWork);
            predicate = predicate.And(x => x.SMaNguoiDung == _sessionService.Current.Principal);
            List<NsNguoiDungLns> listNguoiDungDonVi = _nsNguoiDungLNSService.FindAll(predicate).ToList();
            return listNguoiDungDonVi;
        }

        private void FilterDetailByLNSUser(ref List<SktSoLieuChiTietMLNSModel> detail)
        {
            List<SktSoLieuChiTietMLNSModel> temp = new List<SktSoLieuChiTietMLNSModel>(detail);
            DonVi donvi0 = _nsDonViService.FindByLoai(LoaiDonVi.ROOT, _sessionService.Current.YearOfWork);
            List<NsNguoiDungLns> listLNSUser = GetListLNSByUser();
            listLNSUser = listLNSUser.Where(n => temp.Where(x => !x.IsHangCha).Select(x => x.LNS).ToList().Contains(n.SLns)).ToList();
            List<string> listParentLNS = StringUtils.GetListXauNoiMaParent(listLNSUser.Select(n => n.SLns).ToList());
            if (ListNguoiDungDonVi.Select(n => n.IIdMaDonVi).ToList().Contains(donvi0.IIDMaDonVi))
            {
                detail.Where(n => !listParentLNS.Contains(n.LNS)).Select(n => { n.IsReadonlyByLNSUser = true; return n; }).ToList();
            }
            else
            {
                detail = detail.Where(n => listParentLNS.Contains(n.LNS)).ToList();
            }
        }

        public void LoadData()
        {
            if (Model == null)
                return;
            int loai;
            int typeGet;
            if (Model.Loai == LoaiDonVi.ROOT)
            {
                typeGet = 0;
                loai = 0;
            }
            else
            {
                typeGet = 1;
                loai = 3;
            }
            _dataDetailPlanBeginYear = new List<SktSoLieuChiTietMlnsQuery>();
            if (IsGetDonVi0Lock && IsDonViC4Only)
            {
                if (IsShowTypeAgency && SelectedTypeShowAgency != null && SelectedTypeShowAgency.ValueItem == TypeDisplay.CHITIET_DONVI)
                {
                    //if (IsLock)
                    //{
                    //    _dataDetailPlanBeginYear = _sktSoLieuService.FindByConditionDonVi0ChiTietDonVi(_sessionService.Current.YearOfWork, _sessionService.Current.YearOfBudget,
                    //        _sessionService.Current.Budget, 0, typeGet, Model.Id_DonVi, LoaiChungTu).ToList();
                    //}
                    //else
                    //{
                    //    _dataDetailPlanBeginYear = _sktSoLieuService.FindByConditionDonVi0ChiTietDonVi(_sessionService.Current.YearOfWork, _sessionService.Current.YearOfBudget,
                    //        _sessionService.Current.Budget, 1, typeGet, Model.Id_DonVi, LoaiChungTu).ToList();
                    //}
                }
                else
                {
                    //if (IsLock)
                    //{
                    //    _dataDetailPlanBeginYear = _sktSoLieuService.FindByConditionDonVi0(_sessionService.Current.YearOfWork, _sessionService.Current.YearOfBudget,
                    //        _sessionService.Current.Budget, 0, typeGet, Model.Id_DonVi, LoaiChungTu).ToList();
                    //}
                    //else
                    //{
                    //    _dataDetailPlanBeginYear = _sktSoLieuService.FindByConditionDonVi0(_sessionService.Current.YearOfWork, _sessionService.Current.YearOfBudget,
                    //        _sessionService.Current.Budget, 1, typeGet, Model.Id_DonVi, LoaiChungTu).ToList();
                    //}
                }
            }
            else
            {
                _dataDetailPlanBeginYear = _sktSoLieuService.FindByCondition(_sessionService.Current.YearOfWork, _sessionService.Current.YearOfBudget,
                _sessionService.Current.Budget, loai, typeGet, Model.Id_DonVi, LoaiChungTu).ToList();
            }

            if (LoaiChungTu == VoucherType.NSBD_Key)
            {
                List<DanhMuc> listDanhMuc = _danhMucService.FindByType("NS_Nganh", _sessionService.Current.YearOfWork).Where(n => n.SGiaTri == Model.Id_DonVi).ToList();
                if (listDanhMuc != null && listDanhMuc.Count > 0)
                {
                    List<SktSoLieuChiTietMlnsQuery> listChild = _dataDetailPlanBeginYear.Where(n => listDanhMuc.Select(m => m.IIDMaDanhMuc).ToList().Contains(n.NG)).ToList();
                    List<string> listParentXauNoiMa = StringUtils.GetListXauNoiMaParent(listChild.Select(n => n.XauNoiMa).ToList());
                    _dataDetailPlanBeginYear = _dataDetailPlanBeginYear.Where(n => listParentXauNoiMa.Contains(n.XauNoiMa)).ToList();
                }
            }

            //Check map
            List<SktSoLieuChiTietMlnsQuery> listHasMap = _dataDetailPlanBeginYear.Where(n => !string.IsNullOrEmpty(n.SKT_KyHieu)).ToList();
            List<string> listXauNoiMaMap = new List<string>();
            listXauNoiMaMap.AddRange(StringUtils.GetListXauNoiMaParent(listHasMap.Select(n => n.XauNoiMa).ToList()));
            _dataDetailPlanBeginYear = _dataDetailPlanBeginYear.Where(n => listXauNoiMaMap.Contains(n.XauNoiMa)).ToList();

            //ProcessDynamicMLNS();
            List<SktSoLieuChiTietMLNSModel> listCheckLNS = _mapper.Map<List<Model.SktSoLieuChiTietMLNSModel>>(_dataDetailPlanBeginYear);
            FilterDetailByLNSUser(ref listCheckLNS);
            Items = new ObservableCollection<SktSoLieuChiTietMLNSModel>(listCheckLNS);
            _dataDetailFilter = CollectionViewSource.GetDefaultView(Items);
            _dataDetailFilter.Filter = DetailFilter;
            if (Items != null && Items.Count > 0)
            {
                SelectedItem = Items.FirstOrDefault();
            }
            foreach (SktSoLieuChiTietMLNSModel model in Items)
            {
                if (!model.IsHangCha)
                {
                    model.PropertyChanged += DetailModel_PropertyChanged;

                    model.X1.PropertyChanged += (sender, args) =>
                    {
                        if (!IsInit)
                        {
                            SelectedItem.IsUpdateCanCu = true;
                            SelectedItem.IsModified = true;
                            CalculateData();
                            OnPropertyChanged(nameof(IsSaveData));
                        }
                    };

                    model.X2.PropertyChanged += (sender, args) =>
                    {
                        if (!IsInit)
                        {
                            SelectedItem.IsUpdateCanCu = true;
                            SelectedItem.IsModified = true;
                            CalculateData();
                            OnPropertyChanged(nameof(IsSaveData));
                        }
                    };

                    model.X3.PropertyChanged += (sender, args) =>
                    {
                        if (!IsInit)
                        {
                            SelectedItem.IsUpdateCanCu = true;
                            SelectedItem.IsModified = true;
                            CalculateData();
                            OnPropertyChanged(nameof(IsSaveData));
                        }
                    };

                    model.X4.PropertyChanged += (sender, args) =>
                    {
                        if (!IsInit)
                        {
                            SelectedItem.IsUpdateCanCu = true;
                            SelectedItem.IsModified = true;
                            CalculateData();
                            OnPropertyChanged(nameof(IsSaveData));
                        }
                    };

                    model.X5.PropertyChanged += (sender, args) =>
                    {
                        if (!IsInit)
                        {
                            SelectedItem.IsUpdateCanCu = true;
                            SelectedItem.IsModified = true;
                            CalculateData();
                            OnPropertyChanged(nameof(IsSaveData));
                        }
                    };
                }
            }
            CalculateData();
            OnPropertyChanged(nameof(Items));
            OnPropertyChanged(nameof(IsSaveData));
            OnPropertyChanged(nameof(IsDeleteAll));
        }

        private void ProcessDynamicMLNS()
        {
            _dataDetailPlanBeginYearDynamic = new List<SktSoLieuChiTietMlnsQuery>();

            List<string> chiTietTois = _dataDetailPlanBeginYear.Where(x => !string.IsNullOrEmpty(x.ChiTietToi)).Select(x => x.ChiTietToi).ToList();
            ColumnDisplay = DynamicMLNS.SettingColumnVisibility(chiTietTois);

            foreach (var item in _dataDetailPlanBeginYear.Where(x => string.IsNullOrEmpty(x.L)))
            {
                if (!string.IsNullOrEmpty(item.ChiTietToi))
                    _dataDetailPlanBeginYearDynamic.AddRange(GetChildItems(_dataDetailPlanBeginYear.Where(x => x.LNS == item.LNS).ToList(), item));
                else _dataDetailPlanBeginYearDynamic.Add(item);
            }
            _dataDetailPlanBeginYearDynamic = _dataDetailPlanBeginYearDynamic.OrderBy(x => x.XauNoiMa).ToList();
        }

        public List<SktSoLieuChiTietMlnsQuery> GetChildItems(List<SktSoLieuChiTietMlnsQuery> data, SktSoLieuChiTietMlnsQuery root)
        {
            List<SktSoLieuChiTietMlnsQuery> childrens = new List<SktSoLieuChiTietMlnsQuery>();
            switch (root.ChiTietToi)
            {
                case nameof(MLNSFiled.NG):
                    childrens.AddRange(data.Where(x => !string.IsNullOrEmpty(x.NG) && string.IsNullOrEmpty(x.TNG)).Select(x => { x.BHangCha = false; return x; }).ToList());
                    childrens.AddRange(data.Where(x => string.IsNullOrEmpty(x.NG) && string.IsNullOrEmpty(x.TNG)).Select(x => { x.BHangCha = true; return x; }).ToList());
                    break;
                case nameof(MLNSFiled.TNG):
                    childrens.AddRange(data.Where(x => !string.IsNullOrEmpty(x.TNG) && string.IsNullOrEmpty(x.TNG1)).Select(x => { x.BHangCha = false; return x; }).ToList());
                    childrens.AddRange(data.Where(x => string.IsNullOrEmpty(x.TNG) && string.IsNullOrEmpty(x.TNG1)).Select(x => { x.BHangCha = true; return x; }).ToList());
                    break;
                case nameof(MLNSFiled.TNG1):
                    childrens.AddRange(data.Where(x => !string.IsNullOrEmpty(x.TNG1) && string.IsNullOrEmpty(x.TNG2)).Select(x => { x.BHangCha = false; return x; }).ToList());
                    childrens.AddRange(data.Where(x => string.IsNullOrEmpty(x.TNG1) && string.IsNullOrEmpty(x.TNG2)).Select(x => { x.BHangCha = true; return x; }).ToList());
                    break;
                case nameof(MLNSFiled.TNG2):
                    childrens.AddRange(data.Where(x => !string.IsNullOrEmpty(x.TNG2) && string.IsNullOrEmpty(x.TNG3)).Select(x => { x.BHangCha = false; return x; }).ToList());
                    childrens.AddRange(data.Where(x => string.IsNullOrEmpty(x.TNG2) && string.IsNullOrEmpty(x.TNG3)).Select(x => { x.BHangCha = true; return x; }).ToList());
                    break;
                case nameof(MLNSFiled.TNG3):
                    childrens.AddRange(data.Where(x => !string.IsNullOrEmpty(x.TNG3)).Select(x => { x.BHangCha = false; return x; }).ToList());
                    childrens.AddRange(data.Where(x => string.IsNullOrEmpty(x.TNG3)).Select(x => { x.BHangCha = true; return x; }).ToList());
                    break;
            }
            return childrens;
        }

        private void OnRefeshFilter()
        {
            CalculateDataFilter();
            _dataDetailFilter.Refresh();
            CalculateData();
        }

        private bool DetailFilter(object obj)
        {
            bool result = true;
            var item = (SktSoLieuChiTietMLNSModel)obj;
            if (!string.IsNullOrEmpty(SelectedTypeDisplays))
            {
                if (SelectedTypeDisplays == TypeDisplay.CO_SO_LIEU)
                    result = result && ((item.ChiTiet != 0 || item.UocThucHien != 0
                        || item.HangNhap != 0 || item.HangMua != 0 || item.PhanCap != 0 || item.ChuaPhanCap != 0
                        || item.X1.TuChi != 0 || item.X1.HangNhap != 0 || item.X1.HangMua != 0 || item.X1.PhanCap != 0
                        || item.X2.TuChi != 0 || item.X2.HangNhap != 0 || item.X2.HangMua != 0 || item.X2.PhanCap != 0
                        || item.X3.TuChi != 0 || item.X3.HangNhap != 0 || item.X3.HangMua != 0 || item.X3.PhanCap != 0
                        || item.X4.TuChi != 0 || item.X4.HangNhap != 0 || item.X4.HangMua != 0 || item.X4.PhanCap != 0
                        || item.X5.TuChi != 0 || item.X5.HangNhap != 0 || item.X5.HangMua != 0 || item.X5.PhanCap != 0
                        || (item.IsModified && (item.IdDb == Guid.Empty || item.IdDb == null))));
                if (SelectedTypeDisplays == TypeDisplay.DA_NHAP_SO_DU_TOAN)
                    result = result && (item.DuToan != 0 || (item.IsModified && (item.IdDb == Guid.Empty || item.IdDb == null)));
                if (!string.IsNullOrEmpty(SelectedMucLuc.Stt))
                {
                    result = result && (item.SKT_KyHieu == SelectedMucLuc.KyHieu
                        || (ListParentFilter.Contains(item) && item.IsHangCha && ListChildFilter.Where(n => !n.IsHangCha).Any(n => n.SKT_KyHieu == SelectedMucLuc.KyHieu))
                        || ListMucLucSelected.Any(n => n.KyHieu == item.SKT_KyHieu
                        || (ListParentFilter.Contains(item) && item.IsHangCha)
                        )
                        );
                }
                if (SelectedLoaiNganSach != null && LoaiChungTu != null && LoaiChungTu == VoucherType.NSBD_Key)
                {
                    result = result && (((SelectedLoaiNganSach.HiddenValue.Contains(item.NG) && !string.IsNullOrEmpty(item.NG))
                        || string.IsNullOrEmpty(SelectedLoaiNganSach.HiddenValue)) || ListParentFilter.Any(n => n.XauNoiMa == item.XauNoiMa)
                        );
                }
            }
            item.IsFilter = result;
            return result;
        }

        public void GetListParentFilter()
        {
            ListParentFilter = new List<SktSoLieuChiTietMLNSModel>();
            ListChildFilter = new List<SktSoLieuChiTietMLNSModel>();
            ListMucLucSelected = new List<SktMucLucDuToanDauNamModel>();
            GetDataListMucLucSelected();
            ListChildFilter = Items.Where(item =>
                                    (((item.ChiTiet != 0 || item.UocThucHien != 0 || (item.IsModified && (item.IdDb == Guid.Empty || item.IdDb == null))) && SelectedTypeDisplays == TypeDisplay.CO_SO_LIEU)
                                    || ((item.DuToan != 0 || (item.IsModified && (item.IdDb == Guid.Empty || item.IdDb == null))) && SelectedTypeDisplays == TypeDisplay.DA_NHAP_SO_DU_TOAN)
                                    || SelectedTypeDisplays == TypeDisplay.HIEN_THI_TAT_CA
                                    )
                                && (SelectedMucLuc.KyHieu == item.SKT_KyHieu || (SelectedMucLuc.IsHangCha && ListMucLucSelected.Any(n => n.KyHieu == item.SKT_KyHieu)) || string.IsNullOrEmpty(SelectedMucLuc.Stt))
                                && (
                                    (SelectedLoaiNganSach != null && LoaiChungTu != null && LoaiChungTu == VoucherType.NSBD_Key
                                            && (SelectedLoaiNganSach.HiddenValue.Contains(item.NG) || string.IsNullOrEmpty(SelectedLoaiNganSach.HiddenValue)) && !string.IsNullOrEmpty(item.NG)
                                    )
                                    ||
                                    (LoaiChungTu != null && LoaiChungTu == VoucherType.NSSD_Key)
                                )
              ).ToList();
            if (SelectedLoaiNganSach != null)
            {
                if (ListChildFilter != null && ListChildFilter.Count > 0)
                {
                    List<string> listLNSChild = StringUtils.GetListXauNoiMaParent(ListChildFilter.Select(n => n.XauNoiMa).ToList());
                    ListParentFilter = Items.Where(n => n.IsHangCha
                        && listLNSChild.Any(x => x == n.XauNoiMa)).ToList();
                }
            }
        }

        public void GetDataListMucLucSelected()
        {
            try
            {
                _countLoop = 0;
                if (SelectedMucLuc != null)
                    ListMucLucSelected.AddRange(GetItemChildMucLucSelected(SelectedMucLuc));
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
                MessageBox.Show(ex.Message, "Thông báo", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public List<SktMucLucDuToanDauNamModel> GetItemChildMucLucSelected(SktMucLucDuToanDauNamModel parent)
        {
            _countLoop++;
            if (_countLoop > DataMucLuc.Count)
            {
                throw new Exception("Có lỗi xảy ra do Mục lục số kiểm tra chưa đúng. Vui lòng kiểm tra lại");
            }
            List<SktMucLucDuToanDauNamModel> listChild = DataMucLuc.Where(n => n.IdParent == parent.IdMucLuc).ToList();
            foreach (SktMucLucDuToanDauNamModel item in listChild)
            {
                ListMucLucSelected.AddRange(GetItemChildMucLucSelected(item));
            }
            return (listChild != null && listChild.Count > 0) ? listChild : new List<SktMucLucDuToanDauNamModel>();
        }

        public void LoadDataMucLuc()
        {
            if (Model == null)
                return;
            int loai;
            if (Model.Loai == LoaiDonVi.ROOT)
                loai = 3;
            else
                loai = 4;
            List<string> listKyHieu = new List<string>();
            if (LoaiChungTu == VoucherType.NSBD_Key)
            {
                List<DanhMuc> listDanhMuc = _danhMucService.FindByType("NS_Nganh", _sessionService.Current.YearOfWork).Where(n => n.SGiaTri == Model.Id_DonVi).ToList();
                if (listDanhMuc != null && listDanhMuc.Count > 0)
                {
                    List<NsSktMucLuc> listMucLuc = _sktMucLucService.FindByNganh(_sessionService.Current.YearOfWork, listDanhMuc.Select(n => n.IIDMaDanhMuc).ToList()).ToList();
                    listKyHieu = StringUtils.GetListKyHieuParent(listMucLuc.Select(n => n.SKyHieu).ToList());
                }
            }
            IEnumerable<SktMucLucQuery> data = _sktMucLucService.FindByCondition(_sessionService.Current.YearOfWork, loai, Model.Id_DonVi, int.Parse(LoaiChungTu)).ToList();
            if (LoaiChungTu == VoucherType.NSBD_Key && Model.Loai != LoaiDonVi.ROOT)
            {
                data = data.Where(n => listKyHieu.Contains(n.KyHieu)).ToList();
            }
            //Ngu them
            var ctSkt = _iSktChungTuService.FindByCondition(x => x.INamLamViec == _sessionService.Current.YearOfWork
                                                                 && x.ILoaiChungTu == int.Parse(LoaiChungTu)
                                                                 && x.ILoai == DemandCheckType.CHECK
                                                                 && x.BKhoa).ToList();
            if (ctSkt == null || ctSkt.Count <= 0)
            {
                data.Select(n =>
                {
                    n.TuChi = 0;
                    n.MuaHangHienVat = 0;
                    n.DacThu = 0;
                    return n;
                }).ToList();
            }
            //end
            _dataMucLuc = _mapper.Map<ObservableCollection<Model.SktMucLucDuToanDauNamModel>>(data);
            _dataMucLuc.Insert(0, new SktMucLucDuToanDauNamModel { Stt = string.Empty, MoTa = "TỔNG CỘNG", BHangCha = true, TuChi = 0 });
            _dataMuclucFilter = CollectionViewSource.GetDefaultView(DataMucLuc);
            _dataMuclucFilter.Filter = MucLucFilter;
            if (DataMucLuc != null && DataMucLuc.Count > 0)
            {
                SelectedMucLuc = DataMucLuc.FirstOrDefault();
            }
            CalculateDataMucLuc();
            CalculateTotalMucLuc();
            OnPropertyChanged(nameof(DataMucLuc));
            _dataMuclucFilter.Refresh();
        }

        private void CalculateMucLuc()
        {
            OnPropertyChanged(nameof(Items));
            foreach (SktMucLucDuToanDauNamModel item in _dataMucLuc)
            {
                item.TongSo = Items != null ? Items.Where(n => n.SKT_KyHieu == item.KyHieu && !n.IsHangCha
                    && !string.IsNullOrEmpty(n.SKT_KyHieu) && !string.IsNullOrEmpty(item.KyHieu)).Sum(n => n.ChiTiet) : 0;

                double tongSoHangMua = Items != null ? Items.Where(n => n.SKT_KyHieu == item.KyHieu && !n.IsHangCha
                    && !string.IsNullOrEmpty(n.SKT_KyHieu) && !string.IsNullOrEmpty(item.KyHieu)).Sum(n => n.HangMua) : 0;
                double tongSoHangNhap = Items != null ? Items.Where(n => n.SKT_KyHieu == item.KyHieu && !n.IsHangCha
                    && !string.IsNullOrEmpty(n.SKT_KyHieu) && !string.IsNullOrEmpty(item.KyHieu)).Sum(n => n.HangNhap) : 0;
                item.TongSoHang = tongSoHangMua + tongSoHangNhap;

                item.TongDacThu = Items != null ? Items.Where(n => n.SKT_KyHieu == item.KyHieu && !n.IsHangCha
                    && !string.IsNullOrEmpty(n.SKT_KyHieu) && !string.IsNullOrEmpty(item.KyHieu)).Sum(n => n.PhanCap) : 0;
            }
            CalculateDataMucLuc();
            OnPropertyChanged(nameof(DataMucLuc));
        }

        private bool MucLucFilter(object obj)
        {
            bool result = true;
            var item = (SktMucLucDuToanDauNamModel)obj;
            if (!string.IsNullOrEmpty(SelectedTypeDisplaysMucLuc))
            {
                if (LoaiChungTu == VoucherType.NSSD_Key)
                {
                    if (SelectedTypeDisplaysMucLuc == TypeDisplay.DA_NHAP_SKT)
                        result = result && (item.TuChi != 0 || item.TongSo != 0 || item.ConLai != 0 || item.ConLaiHang != 0 || item.ConLaiDacThu != 0 || (string.IsNullOrEmpty(item.Stt) && item.MoTa == "TỔNG CỘNG"));
                }
                if (LoaiChungTu == VoucherType.NSBD_Key)
                {
                    if (SelectedTypeDisplaysMucLuc == TypeDisplay.DA_NHAP_SKT)
                        result = result && (item.HangNhap != 0 || item.HangMua != 0 || item.PhanCap != 0 || (string.IsNullOrEmpty(item.Stt) && item.MoTa == "TỔNG CỘNG"));
                }
            }
            return result;
        }

        private void CalculateData()
        {
            Items.Where(x => x.IsHangCha)
                .Select(x =>
                {
                    x.ChiTiet = 0; x.UocThucHien = 0; x.HangMua = 0; x.HangNhap = 0; x.PhanCap = 0; x.ChuaPhanCap = 0;
                    x.X1 = new SktSoLieuChiTietCanCuDetail();
                    x.X2 = new SktSoLieuChiTietCanCuDetail();
                    x.X3 = new SktSoLieuChiTietCanCuDetail();
                    x.X4 = new SktSoLieuChiTietCanCuDetail();
                    x.X5 = new SktSoLieuChiTietCanCuDetail();
                    return x;
                }).ToList();
            foreach (var item in Items.Where(x => x.IsFilter && !x.IsHangCha && !x.IsDeleted &&
            (x.ChiTiet != 0 || x.UocThucHien != 0 || x.HangMua != 0 || x.HangNhap != 0 || x.PhanCap != 0 || x.ChuaPhanCap != 0 || x.IsUpdateCanCu
            || x.X1.TuChi != 0 || x.X1.HangNhap != 0 || x.X1.HangMua != 0 || x.X1.PhanCap != 0
            || x.X2.TuChi != 0 || x.X2.HangNhap != 0 || x.X2.HangMua != 0 || x.X2.PhanCap != 0
            || x.X3.TuChi != 0 || x.X3.HangNhap != 0 || x.X3.HangMua != 0 || x.X3.PhanCap != 0
            || x.X4.TuChi != 0 || x.X4.HangNhap != 0 || x.X4.HangMua != 0 || x.X4.PhanCap != 0
            || x.X5.TuChi != 0 || x.X5.HangNhap != 0 || x.X5.HangMua != 0 || x.X5.PhanCap != 0
            )
            ))
            {
                CalculateParent(item, item);
            }
            CalculateTotal();
        }

        private void CalculateDataFilter()
        {
            Items.Where(x => x.IsHangCha)
                .Select(x => { x.ChiTiet = 0; x.UocThucHien = 0; x.HangMua = 0; x.HangNhap = 0; x.PhanCap = 0; return x; }).ToList();
            foreach (var item in Items.Where(x => !x.IsHangCha && !x.IsDeleted && (x.ChiTiet != 0 || x.UocThucHien != 0 || x.HangMua != 0 || x.HangNhap != 0 || x.PhanCap != 0 || x.ChuaPhanCap != 0 || x.IsUpdateCanCu
            || x.X1.TuChi != 0 || x.X1.HangNhap != 0 || x.X1.HangMua != 0 || x.X1.PhanCap != 0
            || x.X2.TuChi != 0 || x.X2.HangNhap != 0 || x.X2.HangMua != 0 || x.X2.PhanCap != 0
            || x.X3.TuChi != 0 || x.X3.HangNhap != 0 || x.X3.HangMua != 0 || x.X3.PhanCap != 0
            || x.X4.TuChi != 0 || x.X4.HangNhap != 0 || x.X4.HangMua != 0 || x.X4.PhanCap != 0
            || x.X5.TuChi != 0 || x.X5.HangNhap != 0 || x.X5.HangMua != 0 || x.X5.PhanCap != 0
            )
            ))
            {
                CalculateParent(item, item);
            }
            CalculateTotal();
        }

        private void CalculateParent(SktSoLieuChiTietMLNSModel currentItem, SktSoLieuChiTietMLNSModel selfItem)
        {
            var parentItem = Items.Where(x => x.MlnsId == currentItem.MlnsIdParent).FirstOrDefault();
            if (parentItem == null) return;
            parentItem.ChiTiet += selfItem.ChiTiet;
            parentItem.UocThucHien += selfItem.UocThucHien;
            parentItem.HangNhap += selfItem.HangNhap;
            parentItem.HangMua += selfItem.HangMua;
            parentItem.PhanCap += selfItem.PhanCap;
            parentItem.ChuaPhanCap += selfItem.ChuaPhanCap;

            parentItem.X1.TuChi += selfItem.X1.TuChi;
            parentItem.X1.HangNhap += selfItem.X1.HangNhap;
            parentItem.X1.HangMua += selfItem.X1.HangMua;
            parentItem.X1.PhanCap += selfItem.X1.PhanCap;

            parentItem.X2.TuChi += selfItem.X2.TuChi;
            parentItem.X2.HangNhap += selfItem.X2.HangNhap;
            parentItem.X2.HangMua += selfItem.X2.HangMua;
            parentItem.X2.PhanCap += selfItem.X2.PhanCap;

            parentItem.X3.TuChi += selfItem.X3.TuChi;
            parentItem.X3.HangNhap += selfItem.X3.HangNhap;
            parentItem.X3.HangMua += selfItem.X3.HangMua;
            parentItem.X3.PhanCap += selfItem.X3.PhanCap;

            parentItem.X4.TuChi += selfItem.X4.TuChi;
            parentItem.X4.HangNhap += selfItem.X4.HangNhap;
            parentItem.X4.HangMua += selfItem.X4.HangMua;
            parentItem.X4.PhanCap += selfItem.X4.PhanCap;

            parentItem.X5.TuChi += selfItem.X5.TuChi;
            parentItem.X5.HangNhap += selfItem.X5.HangNhap;
            parentItem.X5.HangMua += selfItem.X5.HangMua;
            parentItem.X5.PhanCap += selfItem.X5.PhanCap;
            CalculateParent(parentItem, selfItem);
        }

        private void CalculateDataMucLuc()
        {
            _dataMucLuc.Where(x => x.IsHangCha)
                .Select(x =>
                {
                    x.ConLai = 0; x.ConLaiHang = 0; x.ConLaiDacThu = 0; x.TuChi = 0;
                    x.TongSo = 0; x.HangNhap = 0; x.HangMua = 0; x.PhanCap = 0; x.MuaHangHienVat = 0; x.DacThu = 0; return x;
                }).ToList();
            foreach (var item in _dataMucLuc.Where(x => !x.IsHangCha && !x.IsDeleted && (x.TuChi != 0 || x.TongSo != 0 || x.ConLai != 0 || x.ConLaiHang != 0 || x.ConLaiDacThu != 0 ||
                                                                x.HangNhap != 0 || x.HangMua != 0 || x.PhanCap != 0 || x.MuaHangHienVat != 0 || x.DacThu != 0)))
            {
                CalculateParentMucLuc(item, item);
            }
            CalculateTotal();
            CalculateTotalMucLuc();
        }

        private void CalculateParentMucLuc(SktMucLucDuToanDauNamModel currentItem, SktMucLucDuToanDauNamModel selfItem)
        {
            var parentItem = _dataMucLuc.Where(x => x.IdMucLuc == currentItem.IdParent).FirstOrDefault();
            if (parentItem == null) return;
            parentItem.TuChi += selfItem.TuChi;
            parentItem.ConLai += selfItem.ConLai;

            parentItem.TongSo += selfItem.TongSo;
            parentItem.TongSoHang += selfItem.TongSoHang;
            parentItem.TongDacThu += selfItem.TongDacThu;
            parentItem.HangNhap += selfItem.HangNhap;
            parentItem.HangMua += selfItem.HangMua;
            parentItem.PhanCap += selfItem.PhanCap;

            parentItem.MuaHangHienVat += selfItem.MuaHangHienVat;
            parentItem.DacThu += selfItem.DacThu;
            parentItem.ConLaiHang += selfItem.ConLaiHang;
            parentItem.ConLaiDacThu += selfItem.ConLaiDacThu;
            CalculateParentMucLuc(parentItem, selfItem);
        }

        private void DetailModel_PropertyChanged(object sender, PropertyChangedEventArgs args)
        {
            if (args.PropertyName == nameof(SktSoLieuChiTietMLNSModel.ChiTiet) || args.PropertyName == nameof(SktSoLieuChiTietMLNSModel.UocThucHien)
                || args.PropertyName == nameof(SktSoLieuChiTietMLNSModel.HangNhap) || args.PropertyName == nameof(SktSoLieuChiTietMLNSModel.HangMua)
                || args.PropertyName == nameof(SktSoLieuChiTietMLNSModel.PhanCap) || args.PropertyName == nameof(SktSoLieuChiTietMLNSModel.ChuaPhanCap))
            {
                SktSoLieuChiTietMLNSModel item = Items.Where(x => x.Id == ((SktSoLieuChiTietMLNSModel)sender).Id).First();
                item.IsModified = true;
                if (args.PropertyName == nameof(SktSoLieuChiTietMLNSModel.ChiTiet) || args.PropertyName == nameof(SktSoLieuChiTietMLNSModel.UocThucHien)
                    || args.PropertyName == nameof(SktSoLieuChiTietMLNSModel.HangNhap) || args.PropertyName == nameof(SktSoLieuChiTietMLNSModel.HangMua)
                    || args.PropertyName == nameof(SktSoLieuChiTietMLNSModel.PhanCap) || args.PropertyName == nameof(SktSoLieuChiTietMLNSModel.ChuaPhanCap))
                {
                    CalculateData();
                    CalculateMucLuc();
                }
                OnPropertyChanged(nameof(IsSaveData));
                OnPropertyChanged(nameof(IsDeleteAll));
            }
        }

        public void LoadCombobox()
        {
            TypeDisplaysMucLuc = new ObservableCollection<ComboboxItem>();
            TypeDisplaysMucLuc.Add(new ComboboxItem { ValueItem = TypeDisplay.DA_NHAP_SKT, DisplayItem = TypeDisplay.DA_NHAP_SKT });
            TypeDisplaysMucLuc.Add(new ComboboxItem { ValueItem = TypeDisplay.HIEN_THI_TAT_CA, DisplayItem = TypeDisplay.HIEN_THI_TAT_CA });
            SelectedTypeDisplaysMucLuc = TypeDisplay.HIEN_THI_TAT_CA;

            TypeDisplays = new ObservableCollection<ComboboxItem>();
            TypeDisplays.Add(new ComboboxItem { ValueItem = TypeDisplay.CO_SO_LIEU, DisplayItem = TypeDisplay.CO_SO_LIEU });
            TypeDisplays.Add(new ComboboxItem { ValueItem = TypeDisplay.DA_NHAP_SO_DU_TOAN, DisplayItem = TypeDisplay.DA_NHAP_SO_DU_TOAN });
            TypeDisplays.Add(new ComboboxItem { ValueItem = TypeDisplay.HIEN_THI_TAT_CA, DisplayItem = TypeDisplay.HIEN_THI_TAT_CA });
            SelectedTypeDisplays = TypeDisplay.HIEN_THI_TAT_CA;

            TypeShowAgency = new ObservableCollection<ComboboxItem>();
            TypeShowAgency.Add(new ComboboxItem { ValueItem = TypeDisplay.TONG_DONVI, DisplayItem = TypeDisplay.TONG_DONVI });
            TypeShowAgency.Add(new ComboboxItem { ValueItem = TypeDisplay.CHITIET_DONVI, DisplayItem = TypeDisplay.CHITIET_DONVI });
            _selectedTypeShowAgency = TypeShowAgency.FirstOrDefault();

            List<DanhMuc> listDanhMuc = _danhMucService.FindByType("NS_Nganh_Nganh", _sessionService.Current.YearOfWork).OrderBy(n => n.STen).ToList();
            LoaiNganSach = new ObservableCollection<ComboboxItem>();
            LoaiNganSach.Add(new ComboboxItem { ValueItem = string.Empty, DisplayItem = "Nhóm ngành", HiddenValue = string.Empty });

            foreach (DanhMuc item in listDanhMuc)
            {
                LoaiNganSach.Add(new ComboboxItem { ValueItem = item.IIDMaDanhMuc, DisplayItem = item.STen, HiddenValue = item.SGiaTri });
            }
            SelectedLoaiNganSach = LoaiNganSach.FirstOrDefault();
        }

        public bool CheckCanEditTableAuthority()
        {
            List<string> funcAuthority = _sessionService.Current.FuncAuthorities["BUDGET_DEMANDCHECK_PLAN_DETAIL_SAVE"];
            List<string> authorities = _sessionService.Current.Authorities;
            if (funcAuthority == null || funcAuthority.Count == 0 || authorities == null || authorities.Count == 0)
            {
                return false;
            }
            if (funcAuthority.Any(x => authorities.Any(y => y == x)))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public override void Init()
        {
            try
            {
                base.Init();
                LoadComboboxDone = false;
                IsInit = true;
                Items = new ObservableCollection<Model.SktSoLieuChiTietMLNSModel>();
                List<DonVi> listDonVi = _nsDonViService.FindByLoai(_sessionService.Current.YearOfWork, LoaiDonVi.NOI_BO).ToList();
                IsDonViC4Only = (listDonVi != null && listDonVi.Count > 0);

                IsShowTypeAgency = false;
                VisibilityAgencyRoot = Visibility.Collapsed;
                DonVi donvi0 = _nsDonViService.FindByLoai(LoaiDonVi.ROOT, _sessionService.Current.YearOfWork);
                if (donvi0 != null)
                {
                    if (Model.Id_DonVi == donvi0.IIDMaDonVi)
                    {
                        IsShowTypeAgency = true;
                        VisibilityAgencyRoot = Visibility.Visible;
                    }
                }

                LoadCombobox();
                LoadDataMucLuc();
                LoadComboboxDone = true;
                LoadData();
                CalculateMucLuc();
                LoadCanCu();
                //IsReadOnlyTable = Model.Loai == LoaiDonVi.ROOT && IsDonViC4Only;
                //IsReadOnlyTable = IsReadOnlyData;

                if (Model.Loai == LoaiDonVi.ROOT)
                {
                    IsReadOnlyTable = (Model.Loai == LoaiDonVi.ROOT && IsDonViC4Only) || (!CheckCanEditTableAuthority());
                }
                else
                {
                    //List<NsDtdauNamChungTuChiTiet> listChiTiet = _sktSoLieuService.FindDataDonViLoai0ByCondition(_sessionService.Current.YearOfBudget, LoaiChungTu, Model.Id_DonVi).ToList();
                    //if (listChiTiet == null || listChiTiet.Count == 0)
                    //{
                    IsReadOnlyTable = !(ListNguoiDungDonVi != null && ListNguoiDungDonVi.Count > 0 && ListNguoiDungDonVi.Select(n => n.IIdMaDonVi).Contains(Model.Id_DonVi)
                                        && !IsReadOnlyData && CheckCanEditTableAuthority());
                    //}
                    //else
                    //{
                    //    IsReadOnlyTable = !(listChiTiet.Any(n => n.SNguoiTao == _sessionService.Current.Principal) && !IsReadOnlyData);
                    //}
                }



                OnPropertyChanged(nameof(IsReadOnlyTable));
                OnPropertyChanged(nameof(IsShowTypeAgency));
                OnPropertyChanged(nameof(IsEnableButtonCanCu));
                OnPropertyChanged(nameof(VisibleNganSachSuDung));
                OnPropertyChanged(nameof(VisibleNganSachDamBao));
                OnPropertyChanged(nameof(IsDeleteAll));
                OnPropertyChanged(nameof(VisibilityAgencyRoot));
                IsInit = false;
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        private void LoadCanCu(ObservableCollection<CauHinhCanCuModel> obj)
        {
            LstCanCu = obj;
            int i = 0;
            foreach (var cc in obj)
            {
                var lstIdChungTu = (cc.LstIdChungTuCanCu != null && cc.LstIdChungTuCanCu.Count > 0) ? string.Join(",", cc.LstIdChungTuCanCu)
                    : Guid.Empty.ToString();
                var idDonVi = Model.Id_DonVi;
                int loaiCanCu = 0;

                switch (cc.IIDMaChucNang)
                {
                    case "BUDGET_ESTIMATE":
                        loaiCanCu = 1;
                        break;
                    case "BUDGET_SETTLEMENT":
                        loaiCanCu = 2;
                        break;
                    case "BUDGET_ALLOCATION":
                        loaiCanCu = 3;
                        break;
                    default:
                        break;
                }

                var namLamViec = cc.INamCanCu.GetValueOrDefault();
                var idLns = cc.IdLns;
                var nhomNganh = cc.NganhSelected;
                string listIdMucLuc = "-1";
                if (!String.IsNullOrEmpty(idLns))
                {
                    listIdMucLuc = string.Join(",", _nsMucLucNganSachService.FindChildMlnsByParent(idLns,
                        _sessionService.Current.YearOfWork).Select(item => item.MlnsId).ToList());
                }
                if (!String.IsNullOrEmpty(nhomNganh))
                {
                    listIdMucLuc = string.Join(",", _sktChungTuChiTietService.FindMucLucSKTTheoNganh(nhomNganh, 1, _sessionService.Current.YearOfWork).Select(item => item.IIdMlskt).ToList());
                }
                List<DuToanDauNamCanCuQuery> lstCanCu = _sktSoLieuChiTietCanCuDataService.FindCanCuSoNhuCau(lstIdChungTu, listIdMucLuc, idDonVi, loaiCanCu, namLamViec).ToList();

                if (i == 0)
                {
                    foreach (var item in lstCanCu)
                    {
                        var mucLuc = Items.FirstOrDefault(x => x.XauNoiMa.Equals(item.XauNoiMa));
                        if (mucLuc != null)
                        {
                            mucLuc.IsUpdateCanCu = true;

                            // Lay so lieu
                            mucLuc.X1.TuChi = item.TuChi;
                            mucLuc.X1.HangNhap = item.HangNhap;
                            mucLuc.X1.HangMua = item.HangMua;
                            mucLuc.X1.PhanCap = item.PhanCap;
                            mucLuc.X1.Loai = cc.IIDMaChucNang;
                            mucLuc.X1.IdCanCu = cc.Id;
                        }

                        if (mucLuc != null && LoaiChungTu == VoucherType.NSBD_Key && (loaiCanCu == 2 || loaiCanCu == 3))
                        {
                            if (item.XauNoiMa.StartsWith(LNSValue.LNS_1040200))
                            {
                                mucLuc.X1.HangMua = 0;
                                mucLuc.X1.PhanCap = 0;
                            }
                            else if (item.XauNoiMa.StartsWith(LNSValue.LNS_1040300))
                            {
                                mucLuc.X1.HangNhap = 0;
                                mucLuc.X1.PhanCap = 0;
                            }
                        }
                    }
                }

                if (i == 1)
                {
                    foreach (var item in lstCanCu)
                    {
                        var mucLuc = Items.FirstOrDefault(x => x.XauNoiMa.Equals(item.XauNoiMa));
                        if (mucLuc != null)
                        {
                            mucLuc.IsUpdateCanCu = true;

                            // Lay so lieu
                            mucLuc.X2.TuChi = item.TuChi;
                            mucLuc.X2.HangNhap = item.HangNhap;
                            mucLuc.X2.HangMua = item.HangMua;
                            mucLuc.X2.PhanCap = item.PhanCap;
                            mucLuc.X2.Loai = cc.IIDMaChucNang;
                            mucLuc.X2.IdCanCu = cc.Id;
                        }

                        if (mucLuc != null && LoaiChungTu == VoucherType.NSBD_Key && (loaiCanCu == 2 || loaiCanCu == 3))
                        {
                            if (item.XauNoiMa.StartsWith(LNSValue.LNS_1040200))
                            {
                                mucLuc.X2.HangMua = 0;
                                mucLuc.X2.PhanCap = 0;
                            }
                            else if (item.XauNoiMa.StartsWith(LNSValue.LNS_1040300))
                            {
                                mucLuc.X2.HangNhap = 0;
                                mucLuc.X2.PhanCap = 0;
                            }
                        }
                    }
                }

                if (i == 2)
                {
                    foreach (var item in lstCanCu)
                    {
                        var mucLuc = Items.FirstOrDefault(x => x.XauNoiMa.Equals(item.XauNoiMa));
                        if (mucLuc != null)
                        {
                            mucLuc.IsUpdateCanCu = true;

                            // Lay so lieu
                            mucLuc.X3.TuChi = item.TuChi;
                            mucLuc.X3.HangNhap = item.HangNhap;
                            mucLuc.X3.HangMua = item.HangMua;
                            mucLuc.X3.PhanCap = item.PhanCap;
                            mucLuc.X3.Loai = cc.IIDMaChucNang;
                            mucLuc.X3.IdCanCu = cc.Id;
                        }

                        if (mucLuc != null && LoaiChungTu == VoucherType.NSBD_Key && (loaiCanCu == 2 || loaiCanCu == 3))
                        {
                            if (item.XauNoiMa.StartsWith(LNSValue.LNS_1040200))
                            {
                                mucLuc.X3.HangMua = 0;
                                mucLuc.X3.PhanCap = 0;
                            }
                            else if (item.XauNoiMa.StartsWith(LNSValue.LNS_1040300))
                            {
                                mucLuc.X3.HangNhap = 0;
                                mucLuc.X3.PhanCap = 0;
                            }
                        }
                    }
                }

                if (i == 3)
                {
                    foreach (var item in lstCanCu)
                    {
                        var mucLuc = Items.FirstOrDefault(x => x.XauNoiMa.Equals(item.XauNoiMa));
                        if (mucLuc != null)
                        {
                            mucLuc.IsUpdateCanCu = true;

                            // Lay so lieu
                            mucLuc.X4.TuChi = item.TuChi;
                            mucLuc.X4.HangNhap = item.HangNhap;
                            mucLuc.X4.HangMua = item.HangMua;
                            mucLuc.X4.PhanCap = item.PhanCap;
                            mucLuc.X4.Loai = cc.IIDMaChucNang;
                            mucLuc.X4.IdCanCu = cc.Id;
                        }

                        if (mucLuc != null && LoaiChungTu == VoucherType.NSBD_Key && (loaiCanCu == 2 || loaiCanCu == 3))
                        {
                            if (item.XauNoiMa.StartsWith(LNSValue.LNS_1040200))
                            {
                                mucLuc.X4.HangMua = 0;
                                mucLuc.X4.PhanCap = 0;
                            }
                            else if (item.XauNoiMa.StartsWith(LNSValue.LNS_1040300))
                            {
                                mucLuc.X4.HangNhap = 0;
                                mucLuc.X4.PhanCap = 0;
                            }
                        }
                    }
                }

                if (i == 4)
                {
                    foreach (var item in lstCanCu)
                    {
                        var mucLuc = Items.FirstOrDefault(x => x.XauNoiMa.Equals(item.XauNoiMa));
                        if (mucLuc != null)
                        {
                            mucLuc.IsUpdateCanCu = true;

                            // Lay so lieu
                            mucLuc.X5.TuChi = item.TuChi;
                            mucLuc.X5.HangNhap = item.HangNhap;
                            mucLuc.X5.HangMua = item.HangMua;
                            mucLuc.X5.PhanCap = item.PhanCap;
                            mucLuc.X5.Loai = cc.IIDMaChucNang;
                            mucLuc.X5.IdCanCu = cc.Id;
                        }

                        if (mucLuc != null && LoaiChungTu == VoucherType.NSBD_Key && (loaiCanCu == 2 || loaiCanCu == 3))
                        {
                            if (item.XauNoiMa.StartsWith(LNSValue.LNS_1040200))
                            {
                                mucLuc.X5.HangMua = 0;
                                mucLuc.X5.PhanCap = 0;
                            }
                            else if (item.XauNoiMa.StartsWith(LNSValue.LNS_1040300))
                            {
                                mucLuc.X5.HangNhap = 0;
                                mucLuc.X5.PhanCap = 0;
                            }
                        }
                    }
                }
                i++;
            }
            CalculateData();
            OnPropertyChanged(nameof(IsSaveData));
        }

        private void LoadCanCu()
        {
            TC1Visible = Visibility.Collapsed;
            TC2Visible = Visibility.Collapsed;
            TC3Visible = Visibility.Collapsed;
            TC4Visible = Visibility.Collapsed;
            TC5Visible = Visibility.Collapsed;

            HangMua1Visible = Visibility.Collapsed;
            HangNhap1Visible = Visibility.Collapsed;
            PhanCap1Visible = Visibility.Collapsed;

            HangMua2Visible = Visibility.Collapsed;
            HangNhap2Visible = Visibility.Collapsed;
            PhanCap2Visible = Visibility.Collapsed;

            HangMua3Visible = Visibility.Collapsed;
            HangNhap3Visible = Visibility.Collapsed;
            PhanCap3Visible = Visibility.Collapsed;

            HangMua4Visible = Visibility.Collapsed;
            HangNhap4Visible = Visibility.Collapsed;
            PhanCap4Visible = Visibility.Collapsed;

            HangMua5Visible = Visibility.Collapsed;
            HangNhap5Visible = Visibility.Collapsed;
            PhanCap5Visible = Visibility.Collapsed;

            IsReadOnlyX1 = true;
            IsReadOnlyX2 = true;
            IsReadOnlyX3 = true;
            IsReadOnlyX4 = true;
            IsReadOnlyX5 = true;

            var loaiChungTu = LoaiChungTu;
            int yearOfWork = _sessionService.Current.YearOfWork;
            var predicate = PredicateBuilder.True<NsCauHinhCanCu>();
            predicate = predicate.And(item => item.SModule == TypeModuleCanCu.PLAN_BEGIN_YEAR);
            predicate = predicate.And(item => item.INamLamViec == yearOfWork);
            var listCanCu = _iCauHinhCanCuService.FindByCondition(predicate).OrderBy(n => n.INamCanCu);
            var cauHinhCanCu = _mapper.Map<ObservableCollection<CauHinhCanCuModel>>(listCanCu);
            _lstCanCuInit = cauHinhCanCu;
            int count = 0;
            foreach (var item in cauHinhCanCu)
            {
                var predicateCc = PredicateBuilder.True<NsDtdauNamChungTuChiTietCanCu>();
                predicateCc = predicateCc.And(x => x.IIdMaDonVi.Equals(Model.Id_DonVi));
                predicateCc = predicateCc.And(x => x.IIdCanCu.HasValue && x.IIdCanCu.Equals(item.Id));
                //predicateCc = predicateCc.And(x => x.LoaiChungTu == loaiChungTu);
                var lstCanCu = _sktSoLieuChiTietCanCuDataService.FindByCondition(predicateCc);
                foreach (var cc in lstCanCu)
                {
                    var mucLuc = Items.FirstOrDefault(x => x.XauNoiMa == cc.SXauNoiMa);
                    if (mucLuc != null)
                    {
                        if (count == 0)
                        {
                            // Lay so lieu
                            mucLuc.X1.TuChi = cc.FTuChi;
                            mucLuc.X1.HangNhap = cc.FHangNhap;
                            mucLuc.X1.HangMua = cc.FHangMua;
                            mucLuc.X1.PhanCap = cc.FPhanCap;
                            mucLuc.X1.ChuaPhanCap = cc.FChuaPhanCap;
                        }

                        if (count == 1)
                        {
                            // Lay so lieu
                            mucLuc.X2.TuChi = cc.FTuChi;
                            mucLuc.X2.HangNhap = cc.FHangNhap;
                            mucLuc.X2.HangMua = cc.FHangMua;
                            mucLuc.X2.PhanCap = cc.FPhanCap;
                            mucLuc.X2.ChuaPhanCap = cc.FChuaPhanCap;
                        }

                        if (count == 2)
                        {
                            // Lay so lieu
                            mucLuc.X3.TuChi = cc.FTuChi;
                            mucLuc.X3.HangNhap = cc.FHangNhap;
                            mucLuc.X3.HangMua = cc.FHangMua;
                            mucLuc.X3.PhanCap = cc.FPhanCap;
                            mucLuc.X3.ChuaPhanCap = cc.FChuaPhanCap;
                        }

                        if (count == 3)
                        {
                            // Lay so lieu
                            mucLuc.X4.TuChi = cc.FTuChi;
                            mucLuc.X4.HangNhap = cc.FHangNhap;
                            mucLuc.X4.HangMua = cc.FHangMua;
                            mucLuc.X4.PhanCap = cc.FPhanCap;
                            mucLuc.X4.ChuaPhanCap = cc.FChuaPhanCap;
                        }

                        if (count == 4)
                        {
                            // Lay so lieu
                            mucLuc.X5.TuChi = cc.FTuChi;
                            mucLuc.X5.HangNhap = cc.FHangNhap;
                            mucLuc.X5.HangMua = cc.FHangMua;
                            mucLuc.X5.PhanCap = cc.FPhanCap;
                            mucLuc.X5.ChuaPhanCap = cc.FChuaPhanCap;
                        }
                    }
                }
                count++;
            }
            CalculateData();
            for (int i = 0; i < cauHinhCanCu.Count; i++)
            {
                if (i == 0)
                {
                    if (loaiChungTu == VoucherType.NSSD_Key)
                    {
                        TC1Visible = Visibility.Visible;
                        TC1 = cauHinhCanCu[i] != null ? cauHinhCanCu[i].STenCot + " tự chi" : string.Empty;
                    }
                    else
                    {
                        HangNhap1Visible = Visibility.Visible;
                        HangNhap1 = cauHinhCanCu[i] != null ? cauHinhCanCu[i].STenCot + " hàng nhập" : string.Empty;

                        HangMua1Visible = Visibility.Visible;
                        HangMua1 = cauHinhCanCu[i] != null ? cauHinhCanCu[i].STenCot + " hàng mua" : string.Empty;

                        PhanCap1Visible = Visibility.Visible;
                        PhanCap1 = cauHinhCanCu[i] != null ? cauHinhCanCu[i].STenCot + " phân cấp" : string.Empty;
                    }
                    if (true.Equals(cauHinhCanCu[i].BChinhSua))
                    {
                        IsReadOnlyX1 = false;
                    }
                }
                if (i == 1)
                {
                    if (loaiChungTu == VoucherType.NSSD_Key)
                    {
                        TC2Visible = Visibility.Visible;
                        TC2 = cauHinhCanCu[i] != null ? cauHinhCanCu[i].STenCot + " tự chi" : string.Empty;
                    }
                    else
                    {
                        HangNhap2Visible = Visibility.Visible;
                        HangNhap2 = cauHinhCanCu[i] != null ? cauHinhCanCu[i].STenCot + " hàng nhập" : string.Empty;

                        HangMua2Visible = Visibility.Visible;
                        HangMua2 = cauHinhCanCu[i] != null ? cauHinhCanCu[i].STenCot + " hàng mua" : string.Empty;

                        PhanCap2Visible = Visibility.Visible;
                        PhanCap2 = cauHinhCanCu[i] != null ? cauHinhCanCu[i].STenCot + " phân cấp" : string.Empty;
                    }
                    if (true.Equals(cauHinhCanCu[i].BChinhSua))
                    {
                        IsReadOnlyX2 = false;
                    }
                }
                if (i == 2)
                {
                    if (loaiChungTu == VoucherType.NSSD_Key)
                    {
                        TC3Visible = Visibility.Visible;
                        TC3 = cauHinhCanCu[i] != null ? cauHinhCanCu[i].STenCot + " tự chi" : string.Empty;
                    }
                    else
                    {
                        HangNhap3Visible = Visibility.Visible;
                        HangNhap3 = cauHinhCanCu[i] != null ? cauHinhCanCu[i].STenCot + " hàng nhập" : string.Empty;

                        HangMua3Visible = Visibility.Visible;
                        HangMua3 = cauHinhCanCu[i] != null ? cauHinhCanCu[i].STenCot + " hàng mua" : string.Empty;

                        PhanCap3Visible = Visibility.Visible;
                        PhanCap3 = cauHinhCanCu[i] != null ? cauHinhCanCu[i].STenCot + " phân cấp" : string.Empty;
                    }
                    if (true.Equals(cauHinhCanCu[i].BChinhSua))
                    {
                        IsReadOnlyX3 = false;
                    }
                }
                if (i == 3)
                {
                    if (loaiChungTu == VoucherType.NSSD_Key)
                    {
                        TC4Visible = Visibility.Visible;
                        TC4 = cauHinhCanCu[i] != null ? cauHinhCanCu[i].STenCot + " tự chi" : string.Empty;
                    }
                    else
                    {
                        HangNhap4Visible = Visibility.Visible;
                        HangNhap4 = cauHinhCanCu[i] != null ? cauHinhCanCu[i].STenCot + " hàng nhập" : string.Empty;

                        HangMua4Visible = Visibility.Visible;
                        HangMua4 = cauHinhCanCu[i] != null ? cauHinhCanCu[i].STenCot + " hàng mua" : string.Empty;

                        PhanCap4Visible = Visibility.Visible;
                        PhanCap4 = cauHinhCanCu[i] != null ? cauHinhCanCu[i].STenCot + " phân cấp" : string.Empty;
                    }
                    if (true.Equals(cauHinhCanCu[i].BChinhSua))
                    {
                        IsReadOnlyX4 = false;
                    }
                }
                if (i == 4)
                {
                    if (loaiChungTu == VoucherType.NSSD_Key)
                    {
                        TC5Visible = Visibility.Visible;
                        TC5 = cauHinhCanCu[i] != null ? cauHinhCanCu[i].STenCot + " tự chi" : string.Empty;
                    }
                    else
                    {
                        HangNhap5Visible = Visibility.Visible;
                        HangNhap5 = cauHinhCanCu[i] != null ? cauHinhCanCu[i].STenCot + " hàng nhập" : string.Empty;

                        HangMua5Visible = Visibility.Visible;
                        HangMua5 = cauHinhCanCu[i] != null ? cauHinhCanCu[i].STenCot + " hàng mua" : string.Empty;

                        PhanCap5Visible = Visibility.Visible;
                        PhanCap5 = cauHinhCanCu[i] != null ? cauHinhCanCu[i].STenCot + " phân cấp" : string.Empty;
                    }
                    if (true.Equals(cauHinhCanCu[i].BChinhSua))
                    {
                        IsReadOnlyX5 = false;
                    }
                }
            }
        }

        public void OnSelectionChangedMucLuc()
        {
            try
            {
                if (SelectedMucLuc != null && LoadComboboxDone)
                {
                    CalculateDataFilter();
                    GetListParentFilter();
                    _dataDetailFilter = CollectionViewSource.GetDefaultView(Items);
                    _dataDetailFilter.Filter = DetailFilter;
                    CalculateData();
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        private void CalculateTotal()
        {
            Model.TotalChiTiet = 0;
            Model.TotalUocThucHien = 0;
            Model.TotalQuyetToan = 0;
            Model.TotalDuToan = 0;
            Model.TotalHangNhap = 0;
            Model.TotalHangMua = 0;
            Model.TotalPhanCap = 0;
            Model.TotalChuaPhanCap = 0;

            Model.X1 = new ChiTietDuToanDauNamCanCuTong();
            Model.X2 = new ChiTietDuToanDauNamCanCuTong();
            Model.X3 = new ChiTietDuToanDauNamCanCuTong();
            Model.X4 = new ChiTietDuToanDauNamCanCuTong();
            Model.X5 = new ChiTietDuToanDauNamCanCuTong();

            List<SktSoLieuChiTietMLNSModel> listChildren = Items.Where(x => x.IsFilter && !x.IsHangCha && !x.IsDeleted).ToList();
            foreach (SktSoLieuChiTietMLNSModel item in listChildren)
            {
                Model.TotalChiTiet += item.ChiTiet;
                Model.TotalUocThucHien += item.UocThucHien;
                Model.TotalHangNhap += item.HangNhap;
                Model.TotalHangMua += item.HangMua;
                Model.TotalPhanCap += item.PhanCap;
                Model.TotalChuaPhanCap += item.ChuaPhanCap;

                Model.X1.TongTuChi += item.X1.TuChi;
                Model.X1.TongHangNhap += item.X1.HangNhap;
                Model.X1.TongHangMua += item.X1.HangMua;
                Model.X1.TongPhanCap += item.X1.PhanCap;

                Model.X2.TongTuChi += item.X2.TuChi;
                Model.X2.TongHangNhap += item.X2.HangNhap;
                Model.X2.TongHangMua += item.X2.HangMua;
                Model.X2.TongPhanCap += item.X2.PhanCap;

                Model.X3.TongTuChi += item.X3.TuChi;
                Model.X3.TongHangNhap += item.X3.HangNhap;
                Model.X3.TongHangMua += item.X3.HangMua;
                Model.X3.TongPhanCap += item.X3.PhanCap;

                Model.X4.TongTuChi += item.X4.TuChi;
                Model.X4.TongHangNhap += item.X4.HangNhap;
                Model.X4.TongHangMua += item.X4.HangMua;
                Model.X4.TongPhanCap += item.X4.PhanCap;

                Model.X5.TongTuChi += item.X5.TuChi;
                Model.X5.TongHangNhap += item.X5.HangNhap;
                Model.X5.TongHangMua += item.X5.HangMua;
                Model.X5.TongPhanCap += item.X5.PhanCap;
            }
        }

        private void CalculateTotalMucLuc()
        {
            Model.TotalMucLuc = 0;
            Model.TotalMucLucConLai = 0;
            Model.TotalMucLucConLaiHang = 0;
            Model.TotalMucLucConLaiDacThu = 0;
            Model.TotalMucLucHangNhap = 0;
            Model.TotalMucLucHangMua = 0;
            Model.TotalMucLucPhanCap = 0;
            Model.TotalMucLucMuaHangHienVat = 0;
            Model.TotalMucLucDacThu = 0;
            List<SktMucLucDuToanDauNamModel> listChildren = _dataMucLuc.Where(x => !x.IsHangCha && !x.IsDeleted).ToList();
            foreach (SktMucLucDuToanDauNamModel item in listChildren)
            {
                Model.TotalMucLuc += item.TuChi;
                Model.TotalMucLucConLai += item.ConLai;
                Model.TotalMucLucConLaiHang += item.ConLaiHang;
                Model.TotalMucLucConLaiDacThu += item.ConLaiDacThu;
                Model.TotalMucLucHangNhap += item.HangNhap;
                Model.TotalMucLucHangMua += item.HangMua;
                Model.TotalMucLucPhanCap += item.PhanCap;

                Model.TotalMucLucMuaHangHienVat += item.MuaHangHienVat;
                Model.TotalMucLucDacThu += item.DacThu;
            }
        }

        private void OnCloseWindow()
        {
            DataChangedEventHandler handler = ClosePopup;
            if (handler != null)
            {
                handler(this, new EventArgs());
            }
        }

        public void OnRefeshIndexWindow()
        {
            DataChangedEventHandler handler = RefeshIndexWindow;
            if (handler != null)
            {
                handler(this, new EventArgs());
            }
        }

        private void OnShowPopupReportChiTiet()
        {
            PrintReportChiTietDuToanDonViViewModel.LoaiChungTu = LoaiChungTu;
            PrintReportChiTietDuToanDonViViewModel.Init();
            VTS.QLNS.CTC.App.View.Budget.DemandCheck.Plan.PrintReportChiTietDuToanDonVi view = new View.Budget.DemandCheck.Plan.PrintReportChiTietDuToanDonVi()
            {
                DataContext = PrintReportChiTietDuToanDonViViewModel
            };
            var result = DialogHost.Show(view, "PlanBeginYearDetailDialog", null, null);
        }

        public void OnShowPopupChild()
        {
            if (LoaiChungTu == VoucherType.NSSD_Key || SelectedItem.IsHangCha ||
                SelectedItem.IsDeleted || SelectedItem.IdDb == null || SelectedItem.IdDb == Guid.Empty || SelectedItem.PhanCap == 0)
                return;
            //base.OnSelectionDoubleClick(obj);
            OpenDetailDialog();
        }

        private void OpenDetailDialog()
        {
            if (SelectedItem == null)
                return;
            List<NsMucLucNganSach> mucluc = _nsMucLucNganSachService.FindAll(_sessionService.Current.YearOfWork).ToList();
            if (mucluc.Where(n => n.XauNoiMa == SelectedItem.XauNoiMa.Replace("1040100", "1020100")).ToList().Count == 0)
                return;
            PlanBeginYearDetailChildViewModel.Model = Model;
            PlanBeginYearDetailChildViewModel.XauNoiMa = SelectedItem.XauNoiMa.Replace("1040100", "1020100");
            PlanBeginYearDetailChildViewModel.ListXauNoiMa = string.Join(",", StringUtils.SplitXauNoiMaParent(SelectedItem.XauNoiMa.Replace("1040100", "1020100")));
            PlanBeginYearDetailChildViewModel.IdChiTiet = SelectedItem.IdDb.ToString();
            PlanBeginYearDetailChildViewModel.TotalGlobal = SelectedItem.PhanCap;
            PlanBeginYearDetailChildViewModel.Id_DonVi = Model.Id_DonVi;
            PlanBeginYearDetailChildViewModel.TenDonVi = Model.TenDonVi;
            PlanBeginYearDetailChildViewModel.IsReadOnlyTable = IsReadOnlyTable;
            PlanBeginYearDetailChildViewModel.Init();
            var view = new PlanBeginYearDetailChild() { DataContext = PlanBeginYearDetailChildViewModel };
            view.ShowDialog();
        }

        private void OnShowPopupReportCompare()
        {
            try
            {
                PrintReportCompareDemandCheckViewModel.LoaiChungTu = LoaiChungTu;
                PrintReportCompareDemandCheckViewModel.Init();
                VTS.QLNS.CTC.App.View.Budget.DemandCheck.Plan.PrintReportCompareDemandCheck view = new View.Budget.DemandCheck.Plan.PrintReportCompareDemandCheck()
                {
                    DataContext = PrintReportCompareDemandCheckViewModel
                };
                var result = DialogHost.Show(view, "PlanBeginYearDetailDialog", null, null);
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }
    }
}

