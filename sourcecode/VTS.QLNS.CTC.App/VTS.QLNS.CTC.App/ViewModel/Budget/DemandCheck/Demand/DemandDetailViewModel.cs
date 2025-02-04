
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

namespace VTS.QLNS.CTC.App.ViewModel.Budget.DemandCheck.Demand
{
    public class DemandDetailViewModel : DetailViewModelBase<NsSktChungTuModel, NsSktChungTuChiTietModel>
    {
        private readonly ISessionService _sessionService;
        private readonly ISktChungTuChiTietService _sktChungTuChiTietService;
        private readonly ISktChungTuService _sktChungTuService;
        private readonly ISktMucLucService _sktMucLucService;
        private readonly IDanhMucService _iDanhMucService;
        private readonly ICauHinhCanCuService _iCauHinhCanCuService;
        private readonly ISktChungTuChiTietCanCuService _iSktChungTuChiTietCanCuService;
        private readonly ISktChungTuChiTietCanCuChungTuService _iSktChungTuChiTietCanCuChungTuService;
        private readonly INsMucLucNganSachService _iNsMucLucNganSachService;
        private readonly INsDonViService _iNsDonViService;
        private readonly INsDtChungTuService _iDtChungTuService;
        private readonly ISysAuditLogService _log;
        private SessionInfo _sessionInfo;
        private readonly IMapper _mapper;

        public delegate void DataChangedEventHandler(object sender, EventArgs e);
        public event DataChangedEventHandler UpdateParentWindowEventHandler;

        private ICollectionView _sktChungTuChiTietModelsView { get; set; }
        private ICollectionView _searchPopupView { get; set; }

        private NsSktChungTuModel _ctTongHop;
        public NsSktChungTuModel CtTongHop
        {
            get => _ctTongHop;
            set => SetProperty(ref _ctTongHop, value);
        }

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
                OnSearch();
            }
        }

        public bool IsSaveData => Items.Any(item => item.IsModified || item.IsDeleted || item.IsUpdateCanCu || LstCanCu != null)
                                || Items.Any(x => !x.IsHangCha && (x.X1.HasData || x.X2.HasData || x.X3.HasData || x.X4.HasData || x.X5.HasData));

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
        public bool IsEnabledDelete => !IsLock && SelectedItem != null && (SelectedItem.HasData || SelectedItem.X1.HasData || SelectedItem.X2.HasData || SelectedItem.X3.HasData || SelectedItem.X4.HasData || SelectedItem.X5.HasData);
        public bool IsDeleteAll => !IsLock && Items.Any(item => !item.IsModified && (item.HasData || (!item.IsHangCha && (item.X1.HasData || item.X2.HasData || item.X3.HasData || item.X4.HasData || item.X5.HasData)))) && !IsVoucherSummary;

        public string TC1 { get; set; }
        public string TC2 { get; set; }
        public string TC3 { get; set; }
        public string TC4 { get; set; }
        public string TC5 { get; set; }
        public string MHHV1 { get; set; }
        public string DT1 { get; set; }
        public string MHHV2 { get; set; }
        public string DT2 { get; set; }
        public string MHHV3 { get; set; }
        public string DT3 { get; set; }
        public string MHHV4 { get; set; }
        public string DT4 { get; set; }
        public string MHHV5 { get; set; }
        public string DT5 { get; set; }
        public string CanCuBaoDam1 { get; set; }
        public string CanCuBaoDam2 { get; set; }
        public string CanCuBaoDam3 { get; set; }
        public string CanCuBaoDam4 { get; set; }
        public string CanCuBaoDam5 { get; set; }
        public string TonKhoDenNgay => Model.ILoaiChungTu == int.Parse(VoucherType.NSSD_Key)
            ? "Tồn kho đến ngày 01/01/" + (_sessionInfo.YearOfWork - 1)
            : "Giá trị hàng hóa tồn kho 01/01/" + (_sessionInfo.YearOfWork - 1);
        public string KhungNganSachDuocDuyet => $"Khung ngân sách được duyệt năm {_sessionInfo.YearOfWork}";
        public string SoNganhPhanCap => $"Số ngành đã phân cấp theo khung ngân sách năm {_sessionInfo.YearOfWork}";
        public string SoNhuCauNam => $"Nhu cầu ngân sách năm {_sessionInfo.YearOfWork}";
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

        public Visibility TC1Visible { get; set; }
        public Visibility TC2Visible { get; set; }
        public Visibility TC3Visible { get; set; }
        public Visibility TC4Visible { get; set; }
        public Visibility TC5Visible { get; set; }

        public Visibility MHHV1Visible { get; set; }
        public Visibility DT1Visible { get; set; }
        public Visibility MHHV2Visible { get; set; }
        public Visibility DT2Visible { get; set; }
        public Visibility MHHV3Visible { get; set; }
        public Visibility DT3Visible { get; set; }
        public Visibility MHHV4Visible { get; set; }
        public Visibility DT4Visible { get; set; }
        public Visibility MHHV5Visible { get; set; }
        public Visibility DT5Visible { get; set; }

        public bool IsReadOnlyX1 { get; set; }
        public bool IsReadOnlyX2 { get; set; }
        public bool IsReadOnlyX3 { get; set; }
        public bool IsReadOnlyX4 { get; set; }
        public bool IsReadOnlyX5 { get; set; }

        public bool IsInit { get; set; }

        public DateTime DtNow => DateTime.Now;

        public DemandDetailViewModel(
            ISktChungTuChiTietService sktChungTuChiTietService,
            ISktChungTuService sktChungTuService,
            ISktMucLucService sktMucLucService,
            ICauHinhCanCuService iCauHinhCanCuService,
            ISktChungTuChiTietCanCuService iSktChungTuChiTietCanCuService,
            ISktChungTuChiTietCanCuChungTuService iChungTuChiTietCanCuChungTuService,
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
            _sktChungTuChiTietService = sktChungTuChiTietService;
            _sktChungTuService = sktChungTuService;
            _sktMucLucService = sktMucLucService;
            _iCauHinhCanCuService = iCauHinhCanCuService;
            _iSktChungTuChiTietCanCuService = iSktChungTuChiTietCanCuService;
            _iSktChungTuChiTietCanCuChungTuService = iChungTuChiTietCanCuChungTuService;
            _iNsMucLucNganSachService = iNsMucLucNganSachService;
            _iNsDonViService = iNsDonViService;
            _iDtChungTuService = iDtChungTuService;
            _sessionService = sessionService;
            _log = log;
            _mapper = mapper;
            _iDanhMucService = iDanhMucService;
            NsSktChungTuChiTietSearchModel = new NsSktChungTuChiTietModel();
            PrintReportDemandOrgViewModel = printReportDemandOrgViewModel;
            TongHopCanCuViewModel = tongHopCanCuViewModel;

            SearchCommand = new RelayCommand(o => OnSearch());
            ClearSearchCommand = new RelayCommand(OnClearSearch);
            SaveCommand = new RelayCommand(o => OnSave());
            CloseCommand = new RelayCommand(OnClose);
            PrintCommand = new RelayCommand(obj => OpenPrintDialog(obj));
            CanCuCommand = new RelayCommand(obj => OnCanCu());
            LaySummaryCanCuCommand = new RelayCommand(obj => LaySummaryCanCu());
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
            IsInit = false;
        }

        private void OnClearSearch(object obj)
        {
            NsSktChungTuChiTietSearchModel = new NsSktChungTuChiTietModel();
            _sktChungTuChiTietModelsView.Refresh();
        }

        private void OnSearch()
        {
            _sktChungTuChiTietModelsView?.Refresh();
            if (Items != null)
            {
                CalculateData();
            }
            _sktChungTuChiTietModelsView?.Refresh();
        }

        public override void OnClose(object o)
        {
            ((Window)o).Close();
            UpdateParentWindowEventHandler?.Invoke(Model, new EventArgs());
        }

        protected override void OnDelete()
        {
            if (SelectedItem == null || IsLock)
                return;
            if (Model.SNguoiTao != _sessionInfo.Principal)
            {
                MessageBoxHelper.Warning(string.Format(Resources.MsgRoleDelete, Model.SNguoiTao));
                return;
            }
            SelectedItem.IsModified = true;
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
                                                     && (x.FTuChi != 0 || x.FTuChiDeNghi != 0 || x.FHuyDongTonKho != 0 || x.FMuaHangCapHienVat != 0
                                                                       || x.FPhanCap != 0 || x.FTonKhoDenNgay != 0)).ToList();
                //xóa chứng từ chi tiết
                DeleteChiTiet(lstItemFilter);
                var predicateCT = PredicateBuilder.True<NsSktChungTuChiTietCanCu>();
                predicateCT = predicateCT.And(x => x.IiIdCtsoKiemTra == Model.Id);
                predicateCT = predicateCT.And(x => x.INamLamViec == _sessionInfo.YearOfWork);
                var sktcanCanCus = _iSktChungTuChiTietCanCuService.FindByCondition(predicateCT).ToList();
                _iSktChungTuChiTietCanCuService.RemoveRange(sktcanCanCus);
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

            var detailsAdd = Items.Where(x => x.IsModified && !x.IsDeleted && x.IsAdd && !x.IsHangCha).ToList();
            var detailsUpdate = Items.Where(x => x.IsModified && !x.IsDeleted && x.HasData && !x.IsAdd && !x.IsHangCha).ToList();
            var detailsDelete = Items.Where(x => x.IsModified && (x.IsDeleted || !x.HasData) && !x.IsAdd && !x.IsHangCha).ToList();
            var detailsCanCu = Items.Where(x => !x.IsHangCha && (x.X1.HasData || x.X2.HasData || x.X3.HasData || x.X4.HasData || x.X5.HasData)).ToList();

            // update số liệu chứng từ
            UpdateDemandVoucherTotal();

            //thêm mới chứng từ chi tiết
            if (detailsAdd.Count > 0)
            {
                var addItems = new List<NsSktChungTuChiTiet>();
                _mapper.Map(detailsAdd, addItems);
                _sktChungTuChiTietService.AddRange(addItems);
                detailsAdd.Select(x =>
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
                    _mapper.Map(updateItem, sktChungTuChiTiet);
                    _sktChungTuChiTietService.Update(sktChungTuChiTiet);
                    updateItem.IsModified = false;
                }
            }

            //xóa chứng từ chi tiết
            if (detailsDelete.Count > 0)
            {
                var predicateCT = PredicateBuilder.True<NsSktChungTuChiTietCanCu>();
                predicateCT = predicateCT.And(x => x.IiIdCtsoKiemTra == Model.Id);
                predicateCT = predicateCT.And(x => detailsDelete.Select(x => x.SKyHieu).Contains(x.SKyHieu));
                predicateCT = predicateCT.And(x => x.INamLamViec == _sessionInfo.YearOfWork);
                var sktcanCanCus = _iSktChungTuChiTietCanCuService.FindByCondition(predicateCT).ToList();
                _iSktChungTuChiTietCanCuService.RemoveRange(sktcanCanCus);

                foreach (var item in detailsDelete)
                {
                    var deleteItem = _sktChungTuChiTietService.FindById(item.Id);
                    if (deleteItem != null)
                    {
                        _mapper.Map(item, deleteItem);
                        _sktChungTuChiTietService.Delete(deleteItem);
                        _log.WriteLog(Resources.ApplicationName, "Số nhu cầu - chứng từ chi tiết", (int)TypeExecute.Delete, DtNow, TransactionStatus.Success, _sessionService.Current.Principal);
                    }

                    if (!item.IsDeleted)
                    {
                        item.SMoTa = string.Empty;
                        item.IsAdd = true;
                        item.IsModified = false;
                        continue;
                    }

                    item.FTuChi = 0;
                    item.FTuChiDeNghi = 0;
                    item.FHuyDongTonKho = 0;
                    item.FMuaHangCapHienVat = 0;
                    item.FPhanCap = 0;
                    item.FTonKhoDenNgay = 0;
                    item.X1 = new NsSktChungTuChiTietModel.ChiTietCanCu();
                    item.X2 = new NsSktChungTuChiTietModel.ChiTietCanCu();
                    item.X3 = new NsSktChungTuChiTietModel.ChiTietCanCu();
                    item.X4 = new NsSktChungTuChiTietModel.ChiTietCanCu();
                    item.X5 = new NsSktChungTuChiTietModel.ChiTietCanCu();
                    item.SMoTa = string.Empty;
                    item.IsAdd = true;
                    item.IsModified = false;
                    item.IsDeleted = false;
                }
            }

            // update can cu chứng từ chi tiết
            if (LstCanCu != null)
            {
                List<NsSktChungTuChungTuCanCu> listCanCuChungTus = new List<NsSktChungTuChungTuCanCu>();
                List<NsSktChungTuChiTietCanCu> listCanCus = new List<NsSktChungTuChiTietCanCu>();

                //xoa can cu chung tu 
                var predicate = PredicateBuilder.True<NsSktChungTuChungTuCanCu>();
                predicate = predicate.And(x => x.IIdCtSoKiemTra == Model.Id);
                var sktcanCanCuChungTus = _iSktChungTuChiTietCanCuChungTuService.FindByCondition(predicate).ToList();
                _iSktChungTuChiTietCanCuChungTuService.RemoveRange(sktcanCanCuChungTus);

                var count = 0;
                foreach (var cc in LstCanCu)
                {
                    //xoa can cu chung tu chi tiet
                    var predicateCT = PredicateBuilder.True<NsSktChungTuChiTietCanCu>();
                    predicateCT = predicateCT.And(x => x.IiIdCtsoKiemTra == Model.Id);
                    predicateCT = predicateCT.And(x => x.IIdCanCu == cc.Id);
                    //predicateCT = predicateCT.And(x => x.SKyHieu == item.SKyHieu);
                    predicateCT = predicateCT.And(x => x.INamLamViec == _sessionInfo.YearOfWork);
                    var sktcanCanCus = _iSktChungTuChiTietCanCuService.FindByCondition(predicateCT).ToList();
                    _iSktChungTuChiTietCanCuService.RemoveRange(sktcanCanCus);

                    if (cc.LstIdChungTuCanCu != null && cc.LstIdChungTuCanCu.Count > 0)
                    {
                        if (cc.IThietLap != 3)
                        {
                            foreach (var idChungTu in cc.LstIdChungTuCanCu)
                            {
                                NsSktChungTuChungTuCanCu canCu = new NsSktChungTuChungTuCanCu();
                                canCu.IIdCtSoKiemTra = Model.Id;
                                canCu.IIdCtCanCu = idChungTu;
                                canCu.IIdCanCu = cc.Id;
                                listCanCuChungTus.Add(canCu);
                            }
                        }
                        else
                        {
                            NsSktChungTuChungTuCanCu canCu = new NsSktChungTuChungTuCanCu();
                            canCu.IIdCtSoKiemTra = Model.Id;
                            canCu.IIdCtCanCu = cc.IdChungTuCanCuLuyKe;
                            canCu.IIdCanCu = cc.Id;
                            listCanCuChungTus.Add(canCu);
                        }
                    }

                    foreach (var item in detailsCanCu)
                    {
                        NsSktChungTuChiTietCanCu canCuChungTu = new NsSktChungTuChiTietCanCu();
                        canCuChungTu.IiIdCtsoKiemTra = Model.Id;
                        canCuChungTu.IIdMlskt = item.IIdMlskt;
                        canCuChungTu.IIdCanCu = cc.Id;
                        canCuChungTu.INamLamViec = _sessionInfo.YearOfWork;
                        canCuChungTu.SKyHieu = item.SKyHieu;
                        if (item.X1 != null && count == 0)
                        {
                            canCuChungTu.FTuChi = item.X1.SoTien;
                            canCuChungTu.FMuaHangCapHienVat = item.X1.SoTienMHHV;
                            canCuChungTu.FPhanCap = item.X1.SoTienDT;
                        }

                        if (item.X2 != null && count == 1)
                        {
                            canCuChungTu.FTuChi = item.X2.SoTien;
                            canCuChungTu.FMuaHangCapHienVat = item.X2.SoTienMHHV;
                            canCuChungTu.FPhanCap = item.X2.SoTienDT;
                        }

                        if (item.X3 != null && count == 2)
                        {
                            canCuChungTu.FTuChi = item.X3.SoTien;
                            canCuChungTu.FMuaHangCapHienVat = item.X3.SoTienMHHV;
                            canCuChungTu.FPhanCap = item.X3.SoTienDT;
                        }

                        if (item.X4 != null && count == 3)
                        {
                            canCuChungTu.FTuChi = item.X4.SoTien;
                            canCuChungTu.FMuaHangCapHienVat = item.X4.SoTienMHHV;
                            canCuChungTu.FPhanCap = item.X4.SoTienDT;
                        }

                        if (item.X5 != null && count == 4)
                        {
                            canCuChungTu.FTuChi = item.X5.SoTien;
                            canCuChungTu.FMuaHangCapHienVat = item.X5.SoTienMHHV;
                            canCuChungTu.FPhanCap = item.X5.SoTienDT;
                        }

                        listCanCus.Add(canCuChungTu);
                    }

                    count++;
                }

                _iSktChungTuChiTietCanCuChungTuService.AddRange(listCanCuChungTus);
                _iSktChungTuChiTietCanCuService.AddRange(listCanCus);
            }
            else if (_lstCanCuInit != null && _lstCanCuInit.Count > 0)
            {
                List<NsSktChungTuChiTietCanCu> listCanCus = new List<NsSktChungTuChiTietCanCu>();
                var count = 0;
                foreach (var cc in _lstCanCuInit)
                {
                    //xoa can cu chung tu chi tiet
                    var predicateCT = PredicateBuilder.True<NsSktChungTuChiTietCanCu>();
                    predicateCT = predicateCT.And(x => x.IiIdCtsoKiemTra == Model.Id);
                    predicateCT = predicateCT.And(x => x.IIdCanCu == cc.Id);
                    //predicateCT = predicateCT.And(x => x.SKyHieu == item.SKyHieu);
                    predicateCT = predicateCT.And(x => x.INamLamViec == _sessionInfo.YearOfWork);
                    var sktcanCanCus = _iSktChungTuChiTietCanCuService.FindByCondition(predicateCT).ToList();
                    _iSktChungTuChiTietCanCuService.RemoveRange(sktcanCanCus);
                    foreach (var item in detailsCanCu)
                    {
                        NsSktChungTuChiTietCanCu canCuChungTu = new NsSktChungTuChiTietCanCu();
                        canCuChungTu.IiIdCtsoKiemTra = Model.Id;
                        canCuChungTu.IIdMlskt = item.IIdMlskt;
                        canCuChungTu.IIdCanCu = cc.Id;
                        canCuChungTu.INamLamViec = _sessionInfo.YearOfWork;
                        canCuChungTu.SKyHieu = item.SKyHieu;
                        if (item.X1 != null && count == 0)
                        {
                            canCuChungTu.FTuChi = item.X1.SoTien;
                            canCuChungTu.FMuaHangCapHienVat = item.X1.SoTienMHHV;
                            canCuChungTu.FPhanCap = item.X1.SoTienDT;
                        }

                        if (item.X2 != null && count == 1)
                        {
                            canCuChungTu.FTuChi = item.X2.SoTien;
                            canCuChungTu.FMuaHangCapHienVat = item.X2.SoTienMHHV;
                            canCuChungTu.FPhanCap = item.X2.SoTienDT;
                        }

                        if (item.X3 != null && count == 2)
                        {
                            canCuChungTu.FTuChi = item.X3.SoTien;
                            canCuChungTu.FMuaHangCapHienVat = item.X3.SoTienMHHV;
                            canCuChungTu.FPhanCap = item.X3.SoTienDT;
                        }

                        if (item.X4 != null && count == 3)
                        {
                            canCuChungTu.FTuChi = item.X4.SoTien;
                            canCuChungTu.FMuaHangCapHienVat = item.X4.SoTienMHHV;
                            canCuChungTu.FPhanCap = item.X4.SoTienDT;
                        }

                        if (item.X5 != null && count == 4)
                        {
                            canCuChungTu.FTuChi = item.X5.SoTien;
                            canCuChungTu.FMuaHangCapHienVat = item.X5.SoTienMHHV;
                            canCuChungTu.FPhanCap = item.X5.SoTienDT;
                        }

                        listCanCus.Add(canCuChungTu);
                    }

                    count++;
                }

                _iSktChungTuChiTietCanCuService.AddRange(listCanCus);
            }

            _log.WriteLog(Resources.ApplicationName, "Số nhu cầu - chứng từ chi tiết", (int)TypeExecute.Adjust, DtNow, TransactionStatus.Success, _sessionService.Current.Principal);
            OnPropertyChanged(nameof(IsSaveData));
            OnPropertyChanged(nameof(IsDeleteAll));
            UpdateParentWindowEventHandler?.Invoke(Model, new EventArgs());
            new NSMessageBoxViewModel(Resources.MsgSaveDone).ShowDialogHost(DemandCheckScreen.DETAIL_DIALOG);
        }

        public void UpdateDemandVoucherTotal()
        {
            if (Model != null)
            {
                var sktChungTuUpdate = _sktChungTuService.FindById(Model.Id);
                if (sktChungTuUpdate != null)
                {
                    sktChungTuUpdate.FTongTuChi = Items.Where(item => !item.IsHangCha && !item.IsDeleted)
                        .Sum(item => item.FTuChi);
                    sktChungTuUpdate.FTongPhanCap = Items.Where(item => !item.IsHangCha && !item.IsDeleted)
                        .Sum(item => item.FPhanCap);
                    sktChungTuUpdate.FTongMuaHangCapHienVat = Items.Where(item => !item.IsHangCha && !item.IsDeleted)
                        .Sum(item => item.FMuaHangCapHienVat);
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
            var temp = _sktChungTuChiTietService.FindByConditionForChildUnit(searchCondition).OrderBy(t => t.SSttbc).ToList();
            var existChiTiet = _sktChungTuChiTietService.ExistChungTuChiTiet(Model.Id);
            foreach (var item in temp)
            {
                item.IsAuToFillTuChi = !existChiTiet;
            }
            Items = _mapper.Map<ObservableCollection<NsSktChungTuChiTietModel>>(temp);

            // nếu chọn hiển thị chi tiết thì lấy căn cứ của từng chứng từ con
            if (IsChungTuTongHop && searchCondition.IsViewDetailSummary == 1)
            {
                var idChungTuCon = Items.Select(t => t.IIdCtsoKiemTraChild).Distinct().ToList();
                foreach (var item in idChungTuCon)
                {
                    var data = Items.Where(t => t.IIdCtsoKiemTraChild == item);
                    LoadCanCu(item, data);
                }
            }
            else
            {
                LoadCanCu();
            }
            _sktChungTuChiTietModelsView = CollectionViewSource.GetDefaultView(Items);
            foreach (var sktChungTuChiTietModel in Items)
            {
                sktChungTuChiTietModel.ILoaiChungTu = Model.ILoaiChungTu;
                sktChungTuChiTietModel.IsFilter = true;
                if (!sktChungTuChiTietModel.IsHangCha)
                {
                    sktChungTuChiTietModel.PropertyChanged += (sender, args) =>
                    {
                        if ((args.PropertyName == nameof(SelectedItem.FHuyDongTonKho) ||
                            args.PropertyName == nameof(SelectedItem.FTuChi) ||
                            args.PropertyName == nameof(SelectedItem.FTuChiDeNghi) ||
                            args.PropertyName == nameof(SelectedItem.FTonKhoDenNgay) ||
                            args.PropertyName == nameof(SelectedItem.FKhungNganSachDuocDuyet) ||
                            args.PropertyName == nameof(SelectedItem.FSoNganhPhanCap) ||
                            args.PropertyName == nameof(SelectedItem.FMuaHangCapHienVat) ||
                            args.PropertyName == nameof(SelectedItem.FPhanCap) ||
                            args.PropertyName == nameof(SelectedItem.SGhiChu)) && !IsInit)
                        {
                            NsSktChungTuChiTietModel item = (NsSktChungTuChiTietModel)sender;
                            item.IsModified = true;
                            IsInit = true;
                            CalculateData();
                            IsInit = false;
                            OnPropertyChanged(nameof(IsSaveData));
                        }
                    };

                    sktChungTuChiTietModel.X1.PropertyChanged += (sender, args) =>
                    {
                        if (!IsInit)
                        {
                            if (SelectedItem != null)
                            {
                                SelectedItem.IsUpdateCanCu = true;
                                SelectedItem.IsModified = true;
                            }
                            IsInit = true;
                            CalculateData();
                            IsInit = false;
                            OnPropertyChanged(nameof(IsSaveData));
                        }
                    };
                    sktChungTuChiTietModel.X2.PropertyChanged += (sender, args) =>
                    {
                        if (!IsInit)
                        {
                            if (SelectedItem != null)
                            {
                                SelectedItem.IsUpdateCanCu = true;
                                SelectedItem.IsModified = true;
                            }
                            IsInit = true;
                            CalculateData();
                            IsInit = false;
                            OnPropertyChanged(nameof(IsSaveData));
                        }
                    };
                    sktChungTuChiTietModel.X3.PropertyChanged += (sender, args) =>
                    {
                        if (!IsInit)
                        {
                            if (SelectedItem != null)
                            {
                                SelectedItem.IsUpdateCanCu = true;
                                SelectedItem.IsModified = true;
                            }
                            IsInit = true;
                            CalculateData();
                            IsInit = false;
                            OnPropertyChanged(nameof(IsSaveData));
                        }
                    };
                    sktChungTuChiTietModel.X4.PropertyChanged += (sender, args) =>
                    {
                        if (!IsInit)
                        {
                            if (SelectedItem != null)
                            {
                                SelectedItem.IsUpdateCanCu = true;
                                SelectedItem.IsModified = true;
                            }
                            IsInit = true;
                            CalculateData();
                            IsInit = false;
                            OnPropertyChanged(nameof(IsSaveData));
                        }
                    };
                    sktChungTuChiTietModel.X5.PropertyChanged += (sender, args) =>
                    {
                        if (!IsInit)
                        {
                            if (SelectedItem != null)
                            {
                                SelectedItem.IsUpdateCanCu = true;
                                SelectedItem.IsModified = true;
                            }
                            IsInit = true;
                            CalculateData();
                            IsInit = false;
                            OnPropertyChanged(nameof(IsSaveData));
                        }
                    };
                }
            }
            CalculateData();
            _sktChungTuChiTietModelsView.Filter = SktChungTuChiTietFilter;
        }

        private void CalculateData()
        {
            Items.Where(x => x.IsHangCha)
                .ForAll(x =>
                {
                    x.FTuChi = 0;
                    x.FTuChiDeNghi = 0;
                    x.FHuyDongTonKho = 0;
                    x.FTonKhoDenNgay = 0;
                    x.FMuaHangCapHienVat = 0;
                    x.FKhungNganSachDuocDuyet = 0;
                    x.FSoNganhPhanCap = 0;
                    x.FPhanCap = 0;
                    x.X1 = new NsSktChungTuChiTietModel.ChiTietCanCu();
                    x.X2 = new NsSktChungTuChiTietModel.ChiTietCanCu();
                    x.X3 = new NsSktChungTuChiTietModel.ChiTietCanCu();
                    x.X4 = new NsSktChungTuChiTietModel.ChiTietCanCu();
                    x.X5 = new NsSktChungTuChiTietModel.ChiTietCanCu();
                });
            var temp = Items.Where(x => !x.IsHangCha && !x.IsDeleted && x.IsFilter).ToList();
            var dictByMlns = Items.GroupBy(x => x.IIdMlskt).ToDictionary(x => x.Key, x => x.First());
            foreach (var item in temp)
            {
                CalculateParent(item.IdParent, item, dictByMlns);
            }

            UpdateTotal(temp);
        }

        private void CalculateParent(Guid idParent, NsSktChungTuChiTietModel item, Dictionary<Guid, NsSktChungTuChiTietModel> dictByMlns)
        {
            if (!dictByMlns.ContainsKey(idParent))
            {
                return;
            }

            var model = dictByMlns[idParent];
            model.FTuChi += item.FTuChi;
            model.FTuChiDeNghi += item.FTuChiDeNghi;
            model.FHuyDongTonKho += item.FHuyDongTonKho;
            model.FTonKhoDenNgay += item.FTonKhoDenNgay;
            model.FKhungNganSachDuocDuyet += item.FKhungNganSachDuocDuyet;
            model.FSoNganhPhanCap += item.FSoNganhPhanCap;
            model.FMuaHangCapHienVat += item.FMuaHangCapHienVat;
            model.FPhanCap += item.FPhanCap;
            model.X1.SoTien += item.X1.SoTien;
            model.X1.SoTienMHHV += item.X1.SoTienMHHV;
            model.X1.SoTienDT += item.X1.SoTienDT;
            model.X2.SoTien += item.X2.SoTien;
            model.X2.SoTienMHHV += item.X2.SoTienMHHV;
            model.X2.SoTienDT += item.X2.SoTienDT;
            model.X3.SoTien += item.X3.SoTien;
            model.X3.SoTienMHHV += item.X3.SoTienMHHV;
            model.X3.SoTienDT += item.X3.SoTienDT;
            model.X4.SoTien += item.X4.SoTien;
            model.X4.SoTienMHHV += item.X4.SoTienMHHV;
            model.X4.SoTienDT += item.X4.SoTienDT;
            model.X5.SoTien += item.X5.SoTien;
            model.X5.SoTienMHHV += item.X5.SoTienMHHV;
            model.X5.SoTienDT += item.X5.SoTienDT;
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
            var temp = (NsSktChungTuChiTietModel)obj;
            var condition = true;
            if (!string.IsNullOrEmpty(NsSktChungTuChiTietSearchModel.SKyHieu))
                condition = condition && temp.SKyHieu.ToLower()
                    .StartsWith(NsSktChungTuChiTietSearchModel.SKyHieu.Trim().ToLower());
            if (SelectedDataState != null)
            {
                var DataStateSelectedValue = int.Parse(SelectedDataState.ValueItem);
                switch (DataStateSelectedValue)
                {
                    case (int)DataStateValue.HIEN_THI_TAT_CA:
                        break;
                    case (int)DataStateValue.CO_SO_LIEU_DT_QT_SKT:
                        condition = condition && (temp.FTuChi > 0 || temp.FTuChiDeNghi > 0 || temp.FHuyDongTonKho > 0 || temp.FTonKhoDenNgay > 0 || temp.FMuaHangCapHienVat > 0 || temp.FPhanCap > 0);
                        break;
                    case (int)DataStateValue.DA_NHAP_SKT:
                        condition = condition && (temp.FTuChi > 0 || temp.FTuChiDeNghi > 0 || temp.FHuyDongTonKho > 0 || temp.FTonKhoDenNgay > 0 || temp.FMuaHangCapHienVat > 0 || temp.FPhanCap > 0);
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

            temp.IsFilter = condition;
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
            NsSktChungTuChiTietSearchModel = new NsSktChungTuChiTietModel();
        }

        private void UpdateTotal(List<NsSktChungTuChiTietModel> listChildren)
        {

            Model.TongHuyDong = 0;
            Model.FTongTuChi = 0;
            Model.FTongTuChiDeNghi = 0;
            Model.FTongTonKhoDenNgay = 0;
            Model.FTongMuaHangCapHienVat = 0;
            Model.FTongPhanCap = 0;
            Model.FTongKhungNganSachDuocDuyet = 0;
            Model.FTongSoNganhPhanCap = 0;
            Model.FTongTongSo = 0;
            Model.TongTangSnc = 0;
            Model.X1 = new NsSktChungTuModel.ChiTietCanCuTong();
            Model.X2 = new NsSktChungTuModel.ChiTietCanCuTong();
            Model.X3 = new NsSktChungTuModel.ChiTietCanCuTong();
            Model.X4 = new NsSktChungTuModel.ChiTietCanCuTong();
            Model.X5 = new NsSktChungTuModel.ChiTietCanCuTong();
            var roots = Items.Where(t => t.IdParent.Equals(Guid.Empty)).ToList();
            foreach (var item in roots)
            {
                Model.TongHuyDong += item.FHuyDongTonKho;
                Model.FTongTuChi += item.FTuChi;
                Model.FTongTuChiDeNghi += item.FTuChiDeNghi;
                Model.FTongTonKhoDenNgay += item.FTonKhoDenNgay;
                Model.FTongMuaHangCapHienVat += item.FMuaHangCapHienVat;
                Model.FTongPhanCap += item.FPhanCap;
                Model.FTongKhungNganSachDuocDuyet += item.FKhungNganSachDuocDuyet;
                Model.FTongSoNganhPhanCap += item.FSoNganhPhanCap;
                Model.FTongTongSo += item.FTongSo;
                Model.TongTangSnc += item.TongHuyDongTuChi;
                Model.X1.SoTienTong += item.X1.SoTien;
                Model.X1.SoTienMHHVTong += item.X1.SoTienMHHV;
                Model.X1.SoTienDTTong += item.X1.SoTienDT;
                Model.X2.SoTienTong += item.X2.SoTien;
                Model.X2.SoTienMHHVTong += item.X2.SoTienMHHV;
                Model.X2.SoTienDTTong += item.X2.SoTienDT;
                Model.X3.SoTienTong += item.X3.SoTien;
                Model.X3.SoTienMHHVTong += item.X3.SoTienMHHV;
                Model.X3.SoTienDTTong += item.X3.SoTienDT;
                Model.X4.SoTienTong += item.X4.SoTien;
                Model.X4.SoTienMHHVTong += item.X4.SoTienMHHV;
                Model.X4.SoTienDTTong += item.X4.SoTienDT;
                Model.X5.SoTienTong += item.X5.SoTien;
                Model.X5.SoTienMHHVTong += item.X5.SoTienMHHV;
                Model.X5.SoTienDTTong += item.X5.SoTienDT;
            }
        }

        private void LoadCanCu()
        {
            TC1Visible = Visibility.Collapsed;
            TC2Visible = Visibility.Collapsed;
            TC3Visible = Visibility.Collapsed;
            TC4Visible = Visibility.Collapsed;
            TC5Visible = Visibility.Collapsed;

            MHHV1Visible = Visibility.Collapsed;
            DT1Visible = Visibility.Collapsed;
            MHHV2Visible = Visibility.Collapsed;
            DT2Visible = Visibility.Collapsed;
            MHHV3Visible = Visibility.Collapsed;
            DT3Visible = Visibility.Collapsed;
            MHHV4Visible = Visibility.Collapsed;
            DT4Visible = Visibility.Collapsed;
            MHHV5Visible = Visibility.Collapsed;
            DT5Visible = Visibility.Collapsed;

            IsReadOnlyX1 = true;
            IsReadOnlyX2 = true;
            IsReadOnlyX3 = true;
            IsReadOnlyX4 = true;
            IsReadOnlyX5 = true;

            var loaiChungTu = Model.ILoaiChungTu;
            int yearOfWork = _sessionInfo.YearOfWork;
            var predicate = PredicateBuilder.True<NsCauHinhCanCu>();
            predicate = predicate.And(item => item.SModule == TypeModuleCanCu.DEMAND);
            predicate = predicate.And(item => item.INamLamViec == yearOfWork);
            var listCanCu = _iCauHinhCanCuService.FindByCondition(predicate);
            var cauHinhCanCu = _mapper.Map<ObservableCollection<CauHinhCanCuModel>>(listCanCu);
            _lstCanCuInit = cauHinhCanCu;
            int count = 0;
            var predicate1 = PredicateBuilder.True<NsSktChungTuChiTiet>();
            predicate1 = predicate1.And(x => x.IIdCtsoKiemTra == Model.Id);
            var listChungTuChiTiet = _sktChungTuChiTietService.FindByCondition(predicate1);

            foreach (var item in cauHinhCanCu)
            {
                var predicateCc = PredicateBuilder.True<NsSktChungTuChiTietCanCu>();
                predicateCc = predicateCc.And(x => x.IiIdCtsoKiemTra.Equals(Model.Id));
                predicateCc = predicateCc.And(x => x.IIdCanCu.Equals(item.Id));
                var lstCanCu = _iSktChungTuChiTietCanCuService.FindByCondition(predicateCc).ToList();

                if (listChungTuChiTiet.Count() == 0)
                {
                    if (!lstCanCu.Any() && (item.IIDMaChucNang.Equals(TypeCanCu.DEMAND)
                                            || item.IIDMaChucNang.Equals(TypeCanCu.CHECK_NUMBER)
                                            || item.IIDMaChucNang.Equals(TypeCanCu.ESTIMATE)))
                    {
                        if (item.IIDMaChucNang.Equals(TypeCanCu.ESTIMATE))
                        {
                            LoadCanCuDuToanDefault(item, count);
                        }
                        else
                        {
                            LoadCanCuDefault(item, count);
                        }
                    }
                }
                else
                {
                    foreach (var cc in lstCanCu)
                    {
                        // var mucLuc = Items.FirstOrDefault(x => x.IIdMlskt.Equals(cc.IIdMlskt));
                        var mucLuc = Items.FirstOrDefault(x => !string.IsNullOrEmpty(x.SKyHieu) && x.SKyHieu.Equals(cc.SKyHieu));
                        if (mucLuc != null)
                        {
                            if (count == 0)
                            {
                                // Lay so lieu
                                mucLuc.X1.SoTien = cc.FTuChi;
                                mucLuc.X1.SoTienMHHV = cc.FMuaHangCapHienVat;
                                mucLuc.X1.SoTienDT = cc.FPhanCap;
                            }

                            if (count == 1)
                            {
                                // Lay so lieu
                                mucLuc.X2.SoTien = cc.FTuChi;
                                mucLuc.X2.SoTienMHHV = cc.FMuaHangCapHienVat;
                                mucLuc.X2.SoTienDT = cc.FPhanCap;
                            }

                            if (count == 2)
                            {
                                // Lay so lieu
                                mucLuc.X3.SoTien = cc.FTuChi;
                                mucLuc.X3.SoTienMHHV = cc.FMuaHangCapHienVat;
                                mucLuc.X3.SoTienDT = cc.FPhanCap;
                            }

                            if (count == 3)
                            {
                                // Lay so lieu
                                mucLuc.X4.SoTien = cc.FTuChi;
                                mucLuc.X4.SoTienMHHV = cc.FMuaHangCapHienVat;
                                mucLuc.X4.SoTienDT = cc.FPhanCap;
                            }

                            if (count == 4)
                            {
                                // Lay so lieu
                                mucLuc.X5.SoTien = cc.FTuChi;
                                mucLuc.X5.SoTienMHHV = cc.FMuaHangCapHienVat;
                                mucLuc.X5.SoTienDT = cc.FPhanCap;
                            }
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
                    if (loaiChungTu == int.Parse(VoucherType.NSSD_Key))
                    {
                        TC1Visible = Visibility.Visible;
                        TC1 = cauHinhCanCu[i] != null ? cauHinhCanCu[i].STenCot : "";
                    }
                    else
                    {
                        MHHV1Visible = Visibility.Visible;
                        MHHV1 = cauHinhCanCu[i] != null ? cauHinhCanCu[i].STenCot + " mua hàng cấp hiện vật " : "";
                        DT1Visible = Visibility.Visible;
                        DT1 = cauHinhCanCu[i] != null ? cauHinhCanCu[i].STenCot + " đặc thù " : "";
                        var iidMaChucNang = cauHinhCanCu[i] != null ? cauHinhCanCu[i].IIDMaChucNang : "-1";
                        CanCuBaoDam1 = VoucherType.TypeCanCuDict.GetValueOrDefault(iidMaChucNang, string.Empty);
                    }

                    if (true.Equals(cauHinhCanCu[i].BChinhSua))
                    {
                        IsReadOnlyX1 = false;
                    }
                }
                if (i == 1)
                {
                    if (loaiChungTu == int.Parse(VoucherType.NSSD_Key))
                    {
                        TC2Visible = Visibility.Visible;
                        TC2 = cauHinhCanCu[i] != null ? cauHinhCanCu[i].STenCot : "";
                    }
                    else
                    {
                        MHHV2Visible = Visibility.Visible;
                        MHHV2 = cauHinhCanCu[i] != null ? cauHinhCanCu[i].STenCot + " mua hàng cấp hiện vật " : "";
                        DT2Visible = Visibility.Visible;
                        DT2 = cauHinhCanCu[i] != null ? cauHinhCanCu[i].STenCot + " đặc thù " : "";
                        var iidMaChucNang = cauHinhCanCu[i] != null ? cauHinhCanCu[i].IIDMaChucNang : "-1";
                        CanCuBaoDam2 = VoucherType.TypeCanCuDict.GetValueOrDefault(iidMaChucNang, string.Empty);
                    }
                    if (true.Equals(cauHinhCanCu[i].BChinhSua))
                    {
                        IsReadOnlyX2 = false;
                    }
                }
                if (i == 2)
                {
                    if (loaiChungTu == int.Parse(VoucherType.NSSD_Key))
                    {
                        TC3Visible = Visibility.Visible;
                        TC3 = cauHinhCanCu[i] != null ? cauHinhCanCu[i].STenCot : "";
                    }
                    else
                    {
                        MHHV3Visible = Visibility.Visible;
                        MHHV3 = cauHinhCanCu[i] != null ? cauHinhCanCu[i].STenCot + " mua hàng cấp hiện vật " : "";
                        DT3Visible = Visibility.Visible;
                        DT3 = cauHinhCanCu[i] != null ? cauHinhCanCu[i].STenCot + " đặc thù " : "";
                        var iidMaChucNang = cauHinhCanCu[i] != null ? cauHinhCanCu[i].IIDMaChucNang : "-1";
                        CanCuBaoDam3 = VoucherType.TypeCanCuDict.GetValueOrDefault(iidMaChucNang, string.Empty);
                    }
                    if (true.Equals(cauHinhCanCu[i].BChinhSua))
                    {
                        IsReadOnlyX3 = false;
                    }
                }
                if (i == 3)
                {
                    if (loaiChungTu == int.Parse(VoucherType.NSSD_Key))
                    {
                        TC4Visible = Visibility.Visible;
                        TC4 = cauHinhCanCu[i] != null ? cauHinhCanCu[i].STenCot : "";
                    }
                    else
                    {
                        MHHV4Visible = Visibility.Visible;
                        MHHV4 = cauHinhCanCu[i] != null ? cauHinhCanCu[i].STenCot + " mua hàng cấp hiện vật " : "";
                        DT4Visible = Visibility.Visible;
                        DT4 = cauHinhCanCu[i] != null ? cauHinhCanCu[i].STenCot + " đặc thù " : "";
                        var iidMaChucNang = cauHinhCanCu[i] != null ? cauHinhCanCu[i].IIDMaChucNang : "-1";
                        CanCuBaoDam4 = VoucherType.TypeCanCuDict.GetValueOrDefault(iidMaChucNang, string.Empty);
                    }
                    if (true.Equals(cauHinhCanCu[i].BChinhSua))
                    {
                        IsReadOnlyX4 = false;
                    }
                }
                if (i == 4)
                {
                    if (loaiChungTu == int.Parse(VoucherType.NSSD_Key))
                    {
                        TC5Visible = Visibility.Visible;
                        TC5 = cauHinhCanCu[i] != null ? cauHinhCanCu[i].STenCot : "";
                    }
                    else
                    {
                        MHHV5Visible = Visibility.Visible;
                        MHHV5 = cauHinhCanCu[i] != null ? cauHinhCanCu[i].STenCot + " mua hàng cấp hiện vật " : "";
                        DT5Visible = Visibility.Visible;
                        DT5 = cauHinhCanCu[i] != null ? cauHinhCanCu[i].STenCot + " đặc thù " : "";
                        var iidMaChucNang = cauHinhCanCu[i] != null ? cauHinhCanCu[i].IIDMaChucNang : "-1";
                        CanCuBaoDam5 = VoucherType.TypeCanCuDict.GetValueOrDefault(iidMaChucNang, string.Empty);
                    }
                    if (true.Equals(cauHinhCanCu[i].BChinhSua))
                    {
                        IsReadOnlyX5 = false;
                    }
                }
            }
        }

        private void LoadCanCuDefault(CauHinhCanCuModel cancu, int count)
        {
            var pre = PredicateBuilder.True<NsSktChungTu>();
            pre = pre.And(x => x.INamLamViec == cancu.INamCanCu);
            pre = pre.And(x => x.INamNganSach == _sessionService.Current.YearOfBudget);
            pre = pre.And(x => x.IIdMaNguonNganSach == _sessionService.Current.Budget);
            pre = pre.And(x => x.ILoaiChungTu == Model.ILoaiChungTu);
            pre = pre.And(x => x.ILoaiNguonNganSach == Model.ILoaiNguonNganSach);

            var chungTu = _sktChungTuService.FindByCondition(pre).Select(x => x.Id).ToList();

            var predicate = PredicateBuilder.True<NsSktChungTuChiTiet>();
            predicate = predicate.And(x => x.INamLamViec == cancu.INamCanCu);
            predicate = predicate.And(x => x.INamNganSach == _sessionService.Current.YearOfBudget);
            predicate = predicate.And(x => x.IIdMaNguonNganSach == _sessionService.Current.Budget);
            predicate = predicate.And(x => x.ILoaiChungTu == Model.ILoaiChungTu);
            predicate = predicate.And(x => x.IIdMaDonVi.Equals(Model.IIdMaDonVi));
            predicate = predicate.And(x => chungTu.Contains(x.IIdCtsoKiemTra));


            if (cancu.IIDMaChucNang.Equals(TypeCanCu.DEMAND))
            {
                predicate = predicate.And(x => x.ILoai == 0);
                predicate = predicate.And(x => chungTu.Contains(x.IIdCtsoKiemTra));
            }
            if (cancu.IIDMaChucNang.Equals(TypeCanCu.CHECK_NUMBER))
            {
                predicate = predicate.And(x => x.ILoai == 3 || x.ILoai == 4 || x.ILoai == 2);
            }

            var lstChiTiet = _sktChungTuChiTietService.FindByCondition(predicate).GroupBy(n => n.SKyHieu).Select(m => new NsSktChungTuChiTiet
            {
                FHuyDongTonKho = m.Sum(x => x.FHuyDongTonKho),
                FTuChi = m.Sum(x => x.FTuChi),
                FTuChiDeNghi = m.Sum(x => x.FTuChiDeNghi),
                FHienVat = m.Sum(x => x.FHienVat),
                FPhanCap = m.Sum(x => x.FPhanCap),
                FTonKhoDenNgay = m.Sum(x => x.FTonKhoDenNgay),
                FMuaHangCapHienVat = m.Sum(x => x.FMuaHangCapHienVat),
                FThongBaoDonVi = m.Sum(x => x.FThongBaoDonVi),
                SKyHieu = m.FirstOrDefault().SKyHieu
            }).ToList();
            foreach (var cc in lstChiTiet)
            {
                var mucLuc = Items.FirstOrDefault(x => !x.SKyHieuCu.IsEmpty() && x.SKyHieuCu.Equals(cc.SKyHieu));
                if (mucLuc != null)
                {
                    if (count == 0)
                    {
                        // Lay so lieu
                        mucLuc.X1.SoTien = cc.FTuChi;
                        mucLuc.X1.SoTienMHHV = cc.FMuaHangCapHienVat;
                        mucLuc.X1.SoTienDT = cc.FPhanCap;
                    }

                    if (count == 1)
                    {
                        // Lay so lieu
                        mucLuc.X2.SoTien = cc.FTuChi;
                        mucLuc.X2.SoTienMHHV = cc.FMuaHangCapHienVat;
                        mucLuc.X2.SoTienDT = cc.FPhanCap;
                    }

                    if (count == 2)
                    {
                        // Lay so lieu
                        mucLuc.X3.SoTien = cc.FTuChi;
                        mucLuc.X3.SoTienMHHV = cc.FMuaHangCapHienVat;
                        mucLuc.X3.SoTienDT = cc.FPhanCap;
                    }

                    if (count == 3)
                    {
                        // Lay so lieu
                        mucLuc.X4.SoTien = cc.FTuChi;
                        mucLuc.X4.SoTienMHHV = cc.FMuaHangCapHienVat;
                        mucLuc.X4.SoTienDT = cc.FPhanCap;
                    }

                    if (count == 4)
                    {
                        // Lay so lieu
                        mucLuc.X5.SoTien = cc.FTuChi;
                        mucLuc.X5.SoTienMHHV = cc.FMuaHangCapHienVat;
                        mucLuc.X5.SoTienDT = cc.FPhanCap;
                    }
                }
            }
        }

        private void LoadCanCuDuToanDefault(CauHinhCanCuModel cancu, int count)
        {
            var idDonVi = Model.IIdMaDonVi;
            var loaiChungTu = Model.ILoaiChungTu;
            var namChungTu = cancu.INamCanCu;
            var predicate = PredicateBuilder.True<NsDtChungTu>();
            predicate = predicate.And(item => item.INamLamViec == namChungTu);
            predicate = predicate.And(item => item.INamNganSach == _sessionService.Current.YearOfBudget);
            predicate = predicate.And(item => item.IIdMaNguonNganSach == _sessionService.Current.Budget);
            predicate = predicate.And(item => item.ILoaiChungTu == loaiChungTu);
            predicate = predicate.And(item => item.ILoaiDuToan == (int)BudgetType.YEAR);
            predicate = predicate.And(item => true.Equals(item.BKhoa));

            if (idDonVi.Equals(_sessionService.Current.IdDonVi))
            {
                predicate = predicate.And(item => item.ILoai == SoChungTuType.ReceiveEstimate);
            }
            else
            {
                predicate = predicate.And(item => item.ILoai == SoChungTuType.EstimateDivision);
                predicate = predicate.And(item => item.SDsidMaDonVi.Contains(idDonVi));
            }

            var listCTCanCu = _iDtChungTuService.FindByCondition(predicate).ToList();
            if (!listCTCanCu.Any())
            {
                return;
            }
            var llstIdCt = string.Join(",", listCTCanCu.Select(x => x.Id));
            var lstChiTiet = GetCanCuDuToanQtCp(llstIdCt, "-1", idDonVi, cancu.IIDMaChucNang, namChungTu.GetValueOrDefault(), Model.ILoaiNguonNganSach);
            foreach (var cc in lstChiTiet)
            {
                var mucLuc = Items.FirstOrDefault(x => x.SKyHieu == cc.KyHieu);
                if (mucLuc != null)
                {
                    if (count == 0)
                    {
                        // Lay so lieu
                        mucLuc.X1.SoTien = cc.TuChi;
                        mucLuc.X1.SoTienMHHV = cc.HangNhap + cc.HangMua;
                        mucLuc.X1.SoTienDT = cc.PhanCap;
                    }

                    if (count == 1)
                    {
                        // Lay so lieu
                        mucLuc.X2.SoTien = cc.TuChi;
                        mucLuc.X2.SoTienMHHV = cc.HangNhap + cc.HangMua;
                        mucLuc.X2.SoTienDT = cc.PhanCap;
                    }

                    if (count == 2)
                    {
                        // Lay so lieu
                        mucLuc.X3.SoTien = cc.TuChi;
                        mucLuc.X3.SoTienMHHV = cc.HangNhap + cc.HangMua;
                        mucLuc.X3.SoTienDT = cc.PhanCap;
                    }

                    if (count == 3)
                    {
                        // Lay so lieu
                        mucLuc.X4.SoTien = cc.TuChi;
                        mucLuc.X4.SoTienMHHV = cc.HangNhap + cc.HangMua;
                        mucLuc.X4.SoTienDT = cc.PhanCap;
                    }

                    if (count == 4)
                    {
                        // Lay so lieu
                        mucLuc.X5.SoTien = cc.TuChi;
                        mucLuc.X5.SoTienMHHV = cc.HangNhap + cc.HangMua;
                        mucLuc.X5.SoTienDT = cc.PhanCap;
                    }
                }
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
                case DemandCheckPrintType.REPORT_ORG_DEMAND_DETAIL_NUMBER:
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
                case DemandCheckPrintType.REPORT_DEMAND_NUMBER_SUMMARY:
                    PrintReportDemandOrgViewModel.DemandCheckPrintType = demandCheckPrintType;
                    if (Items != null && Items.Count > 0)
                        PrintReportDemandOrgViewModel.Init();
                    content = new PrintReportDemandOrg
                    {
                        DataContext = PrintReportDemandOrgViewModel
                    };
                    break;
                case DemandCheckPrintType.SUMMARY_REPORT_OF_TEST_NUMBER_ALLOCATION:
                    content = new PrintCommunicateSettlementLNS();
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

        private void OnCanCu()
        {
            TongHopCanCuViewModel.NsSktChungTuModel = Model;
            TongHopCanCuViewModel.CauHinhCanCuTemp = LstCanCu;
            TongHopCanCuViewModel.ListIdNhomNganhSelected = null;
            TongHopCanCuViewModel.ListIdMlnsSelected = null;
            TongHopCanCuViewModel.Init();
            TongHopCanCuViewModel.SavedAction = obj =>
            {
                IsInit = true;
                //LoadData();
                LoadCanCu((ObservableCollection<CauHinhCanCuModel>)obj);
                IsInit = false;
                _sktChungTuChiTietModelsView.Refresh();
            };
            var view = new TongHopCanCu()
            {
                DataContext = TongHopCanCuViewModel
            };
            DialogHost.Show(view, DemandCheckScreen.DETAIL_DIALOG, null, null);
        }

        private void LoadCanCu(ObservableCollection<CauHinhCanCuModel> obj)
        {
            LstCanCu = obj;
            int i = 0;
            foreach (var cc in obj)
            {
                var lstIdChungTu = (cc.LstIdChungTuCanCu != null && cc.LstIdChungTuCanCu.Count > 0) ? string.Join(",", cc.LstIdChungTuCanCu)
                    : Guid.Empty.ToString();
                var idDonVi = Model.IIdMaDonVi;
                var loaiCanCu = cc.IIDMaChucNang;
                var namLamViec = cc.INamCanCu.GetValueOrDefault();
                var nhomNganh = cc.NganhSelected;
                string listIdMucLuc = "-1";
                if (VoucherType.NSBD_Key.Equals(Model.ILoaiChungTu.GetValueOrDefault().ToString()))
                {
                    listIdMucLuc = "1";
                }
                if (!string.IsNullOrEmpty(nhomNganh))
                {
                    listIdMucLuc = string.Join(",", _sktChungTuChiTietService.FindMucLucSKTTheoNganh(nhomNganh, 1, _sessionInfo.YearOfWork).Select(item => item.IIdMlskt).ToList());
                }
                List<CanCuSoNhuCauQuery> lstCanCu = new List<CanCuSoNhuCauQuery>();
                if (TypeCanCu.DEMAND.Equals(cc.IIDMaChucNang) || TypeCanCu.CHECK_NUMBER.Equals(cc.IIDMaChucNang))
                {
                    lstCanCu = _sktChungTuChiTietService
                        .FindCanCuSoNhuCau(lstIdChungTu, listIdMucLuc, idDonVi, loaiCanCu, namLamViec).ToList();
                    if (i == 0)
                    {
                        foreach (var item in lstCanCu)
                        {
                            var mucLuc = Items.FirstOrDefault(x => x.SKyHieuCu == item.KyHieu);
                            if (mucLuc != null)
                            {
                                mucLuc.IsUpdateCanCu = true;

                                // Lay so lieu
                                mucLuc.X1.SoTien = item.TuChi;
                                mucLuc.X1.SoTienHN = item.HangNhap;
                                mucLuc.X1.SoTienHM = item.HangMua;
                                mucLuc.X1.SoTienPC = item.PhanCap;
                                mucLuc.X1.Loai = cc.IIDMaChucNang;
                                mucLuc.X1.IdCanCu = cc.Id;
                                if (TypeCanCu.ESTIMATE.Equals(cc.IIDMaChucNang))
                                {
                                    mucLuc.X1.SoTienMHHV = item.HangNhap + item.HangMua;
                                    mucLuc.X1.SoTienDT = item.PhanCap;
                                }

                                if (TypeCanCu.SETTLEMENT.Equals(cc.IIDMaChucNang) || TypeCanCu.ALLOCATION.Equals(cc.IIDMaChucNang))
                                {
                                    mucLuc.X1.SoTienMHHV = item.TuChi;
                                    mucLuc.X1.SoTienDT = 0;
                                }

                                if (TypeCanCu.DEMAND.Equals(cc.IIDMaChucNang) || TypeCanCu.CHECK_NUMBER.Equals(cc.IIDMaChucNang))
                                {
                                    mucLuc.X1.SoTienMHHV = item.MuaHangHienVat;
                                    mucLuc.X1.SoTienDT = item.DacThu;
                                }
                            }
                        }
                    }

                    if (i == 1)
                    {
                        foreach (var item in lstCanCu)
                        {
                            var mucLuc = Items.FirstOrDefault(x => x.SKyHieuCu.Equals(item.KyHieu));
                            if (mucLuc != null)
                            {
                                mucLuc.IsUpdateCanCu = true;

                                // Lay so lieu
                                mucLuc.X2.SoTien = item.TuChi;
                                mucLuc.X2.SoTienHN = item.HangNhap;
                                mucLuc.X2.SoTienHM = item.HangMua;
                                mucLuc.X2.SoTienPC = item.PhanCap;
                                mucLuc.X2.Loai = cc.IIDMaChucNang;
                                mucLuc.X2.IdCanCu = cc.Id;
                                if (TypeCanCu.ESTIMATE.Equals(cc.IIDMaChucNang))
                                {
                                    mucLuc.X2.SoTienMHHV = item.HangNhap + item.HangMua;
                                    mucLuc.X2.SoTienDT = item.PhanCap;
                                }

                                if (TypeCanCu.SETTLEMENT.Equals(cc.IIDMaChucNang) || TypeCanCu.ALLOCATION.Equals(cc.IIDMaChucNang))
                                {
                                    mucLuc.X2.SoTienMHHV = item.TuChi;
                                    mucLuc.X2.SoTienDT = 0;
                                }

                                if (TypeCanCu.DEMAND.Equals(cc.IIDMaChucNang) || TypeCanCu.CHECK_NUMBER.Equals(cc.IIDMaChucNang))
                                {
                                    mucLuc.X2.SoTienMHHV = item.MuaHangHienVat;
                                    mucLuc.X2.SoTienDT = item.DacThu;
                                }
                            }
                        }
                    }

                    if (i == 2)
                    {
                        foreach (var item in lstCanCu)
                        {
                            var mucLuc = Items.FirstOrDefault(x => x.SKyHieuCu.Equals(item.KyHieu));
                            if (mucLuc != null)
                            {
                                mucLuc.IsUpdateCanCu = true;

                                // Lay so lieu
                                mucLuc.X3.SoTien = item.TuChi;
                                mucLuc.X3.SoTienHN = item.HangNhap;
                                mucLuc.X3.SoTienHM = item.HangMua;
                                mucLuc.X3.SoTienPC = item.PhanCap;
                                mucLuc.X3.Loai = cc.IIDMaChucNang;
                                mucLuc.X3.IdCanCu = cc.Id;

                                if (TypeCanCu.ESTIMATE.Equals(cc.IIDMaChucNang))
                                {
                                    mucLuc.X3.SoTienMHHV = item.HangNhap + item.HangMua;
                                    mucLuc.X3.SoTienDT = item.PhanCap;
                                }

                                if (TypeCanCu.SETTLEMENT.Equals(cc.IIDMaChucNang) || TypeCanCu.ALLOCATION.Equals(cc.IIDMaChucNang))
                                {
                                    mucLuc.X3.SoTienMHHV = item.TuChi;
                                    mucLuc.X3.SoTienDT = 0;
                                }

                                if (TypeCanCu.DEMAND.Equals(cc.IIDMaChucNang) || TypeCanCu.CHECK_NUMBER.Equals(cc.IIDMaChucNang))
                                {
                                    mucLuc.X3.SoTienMHHV = item.MuaHangHienVat;
                                    mucLuc.X3.SoTienDT = item.DacThu;
                                }
                            }
                        }
                    }

                    if (i == 3)
                    {
                        foreach (var item in lstCanCu)
                        {
                            var mucLuc = Items.FirstOrDefault(x => x.SKyHieuCu.Equals(item.KyHieu));
                            if (mucLuc != null)
                            {
                                mucLuc.IsUpdateCanCu = true;

                                // Lay so lieu
                                mucLuc.X4.SoTien = item.TuChi;
                                mucLuc.X4.SoTienHN = item.HangNhap;
                                mucLuc.X4.SoTienHM = item.HangMua;
                                mucLuc.X4.SoTienPC = item.PhanCap;
                                mucLuc.X4.Loai = cc.IIDMaChucNang;
                                mucLuc.X4.IdCanCu = cc.Id;

                                if (TypeCanCu.ESTIMATE.Equals(cc.IIDMaChucNang))
                                {
                                    mucLuc.X4.SoTienMHHV = item.HangNhap + item.HangMua;
                                    mucLuc.X4.SoTienDT = item.PhanCap;
                                }

                                if (TypeCanCu.SETTLEMENT.Equals(cc.IIDMaChucNang) || TypeCanCu.ALLOCATION.Equals(cc.IIDMaChucNang))
                                {
                                    mucLuc.X4.SoTienMHHV = item.TuChi;
                                    mucLuc.X4.SoTienDT = 0;
                                }

                                if (TypeCanCu.DEMAND.Equals(cc.IIDMaChucNang) || TypeCanCu.CHECK_NUMBER.Equals(cc.IIDMaChucNang))
                                {
                                    mucLuc.X4.SoTienMHHV = item.MuaHangHienVat;
                                    mucLuc.X4.SoTienDT = item.DacThu;
                                }
                            }
                        }
                    }

                    if (i == 4)
                    {
                        foreach (var item in lstCanCu)
                        {
                            var mucLuc = Items.FirstOrDefault(x => x.SKyHieuCu.Equals(item.KyHieu));
                            if (mucLuc != null)
                            {
                                mucLuc.IsUpdateCanCu = true;

                                // Lay so lieu
                                mucLuc.X5.SoTien = item.TuChi;
                                mucLuc.X5.SoTienHN = item.HangNhap;
                                mucLuc.X5.SoTienHM = item.HangMua;
                                mucLuc.X5.SoTienPC = item.PhanCap;
                                mucLuc.X5.Loai = cc.IIDMaChucNang;
                                mucLuc.X5.IdCanCu = cc.Id;

                                if (TypeCanCu.ESTIMATE.Equals(cc.IIDMaChucNang))
                                {
                                    mucLuc.X5.SoTienMHHV = item.HangNhap + item.HangMua;
                                    mucLuc.X5.SoTienDT = item.PhanCap;
                                }

                                if (TypeCanCu.SETTLEMENT.Equals(cc.IIDMaChucNang) || TypeCanCu.ALLOCATION.Equals(cc.IIDMaChucNang))
                                {
                                    mucLuc.X5.SoTienMHHV = item.TuChi;
                                    mucLuc.X5.SoTienDT = 0;
                                }

                                if (TypeCanCu.DEMAND.Equals(cc.IIDMaChucNang) || TypeCanCu.CHECK_NUMBER.Equals(cc.IIDMaChucNang))
                                {
                                    mucLuc.X5.SoTienMHHV = item.MuaHangHienVat;
                                    mucLuc.X5.SoTienDT = item.DacThu;
                                }
                            }
                        }
                    }

                    i++;
                }
                else
                {
                    lstCanCu = GetCanCuDuToanQtCp(lstIdChungTu, listIdMucLuc, idDonVi, loaiCanCu, namLamViec, Model.ILoaiNguonNganSach);
                    if (i == 0)
                    {
                        foreach (var item in lstCanCu)
                        {
                            var mucLuc = Items.FirstOrDefault(x => x.SKyHieu.Equals(item.KyHieu));
                            if (mucLuc != null)
                            {
                                mucLuc.IsUpdateCanCu = true;

                                // Lay so lieu
                                mucLuc.X1.SoTien = item.TuChi;
                                mucLuc.X1.SoTienHN = item.HangNhap;
                                mucLuc.X1.SoTienHM = item.HangMua;
                                mucLuc.X1.SoTienPC = item.PhanCap;
                                mucLuc.X1.Loai = cc.IIDMaChucNang;
                                mucLuc.X1.IdCanCu = cc.Id;
                                if (TypeCanCu.ESTIMATE.Equals(cc.IIDMaChucNang))
                                {
                                    mucLuc.X1.SoTienMHHV = item.HangNhap + item.HangMua;
                                    mucLuc.X1.SoTienDT = item.PhanCap;
                                }

                                if (TypeCanCu.SETTLEMENT.Equals(cc.IIDMaChucNang) || TypeCanCu.ALLOCATION.Equals(cc.IIDMaChucNang))
                                {
                                    mucLuc.X1.SoTienMHHV = item.TuChi;
                                    mucLuc.X1.SoTienDT = 0;
                                }

                                if (TypeCanCu.DEMAND.Equals(cc.IIDMaChucNang) || TypeCanCu.CHECK_NUMBER.Equals(cc.IIDMaChucNang))
                                {
                                    mucLuc.X1.SoTienMHHV = item.MuaHangHienVat;
                                    mucLuc.X1.SoTienDT = item.DacThu;
                                }
                            }
                        }
                    }

                    if (i == 1)
                    {
                        foreach (var item in lstCanCu)
                        {
                            var mucLuc = Items.FirstOrDefault(x => x.SKyHieu.Equals(item.KyHieu));
                            if (mucLuc != null)
                            {
                                mucLuc.IsUpdateCanCu = true;

                                // Lay so lieu
                                mucLuc.X2.SoTien = item.TuChi;
                                mucLuc.X2.SoTienHN = item.HangNhap;
                                mucLuc.X2.SoTienHM = item.HangMua;
                                mucLuc.X2.SoTienPC = item.PhanCap;
                                mucLuc.X2.Loai = cc.IIDMaChucNang;
                                mucLuc.X2.IdCanCu = cc.Id;
                                if (TypeCanCu.ESTIMATE.Equals(cc.IIDMaChucNang))
                                {
                                    mucLuc.X2.SoTienMHHV = item.HangNhap + item.HangMua;
                                    mucLuc.X2.SoTienDT = item.PhanCap;
                                }

                                if (TypeCanCu.SETTLEMENT.Equals(cc.IIDMaChucNang) || TypeCanCu.ALLOCATION.Equals(cc.IIDMaChucNang))
                                {
                                    mucLuc.X2.SoTienMHHV = item.TuChi;
                                    mucLuc.X2.SoTienDT = 0;
                                }

                                if (TypeCanCu.DEMAND.Equals(cc.IIDMaChucNang) || TypeCanCu.CHECK_NUMBER.Equals(cc.IIDMaChucNang))
                                {
                                    mucLuc.X2.SoTienMHHV = item.MuaHangHienVat;
                                    mucLuc.X2.SoTienDT = item.DacThu;
                                }
                            }
                        }
                    }

                    if (i == 2)
                    {
                        foreach (var item in lstCanCu)
                        {
                            var mucLuc = Items.FirstOrDefault(x => x.SKyHieu.Equals(item.KyHieu));
                            if (mucLuc != null)
                            {
                                mucLuc.IsUpdateCanCu = true;

                                // Lay so lieu
                                mucLuc.X3.SoTien = item.TuChi;
                                mucLuc.X3.SoTienHN = item.HangNhap;
                                mucLuc.X3.SoTienHM = item.HangMua;
                                mucLuc.X3.SoTienPC = item.PhanCap;
                                mucLuc.X3.Loai = cc.IIDMaChucNang;
                                mucLuc.X3.IdCanCu = cc.Id;

                                if (TypeCanCu.ESTIMATE.Equals(cc.IIDMaChucNang))
                                {
                                    mucLuc.X3.SoTienMHHV = item.HangNhap + item.HangMua;
                                    mucLuc.X3.SoTienDT = item.PhanCap;
                                }

                                if (TypeCanCu.SETTLEMENT.Equals(cc.IIDMaChucNang) || TypeCanCu.ALLOCATION.Equals(cc.IIDMaChucNang))
                                {
                                    mucLuc.X3.SoTienMHHV = item.TuChi;
                                    mucLuc.X3.SoTienDT = 0;
                                }

                                if (TypeCanCu.DEMAND.Equals(cc.IIDMaChucNang) || TypeCanCu.CHECK_NUMBER.Equals(cc.IIDMaChucNang))
                                {
                                    mucLuc.X3.SoTienMHHV = item.MuaHangHienVat;
                                    mucLuc.X3.SoTienDT = item.DacThu;
                                }
                            }
                        }
                    }

                    if (i == 3)
                    {
                        foreach (var item in lstCanCu)
                        {
                            var mucLuc = Items.FirstOrDefault(x => x.SKyHieu.Equals(item.KyHieu));
                            if (mucLuc != null)
                            {
                                mucLuc.IsUpdateCanCu = true;

                                // Lay so lieu
                                mucLuc.X4.SoTien = item.TuChi;
                                mucLuc.X4.SoTienHN = item.HangNhap;
                                mucLuc.X4.SoTienHM = item.HangMua;
                                mucLuc.X4.SoTienPC = item.PhanCap;
                                mucLuc.X4.Loai = cc.IIDMaChucNang;
                                mucLuc.X4.IdCanCu = cc.Id;

                                if (TypeCanCu.ESTIMATE.Equals(cc.IIDMaChucNang))
                                {
                                    mucLuc.X4.SoTienMHHV = item.HangNhap + item.HangMua;
                                    mucLuc.X4.SoTienDT = item.PhanCap;
                                }

                                if (TypeCanCu.SETTLEMENT.Equals(cc.IIDMaChucNang) || TypeCanCu.ALLOCATION.Equals(cc.IIDMaChucNang))
                                {
                                    mucLuc.X4.SoTienMHHV = item.TuChi;
                                    mucLuc.X4.SoTienDT = 0;
                                }

                                if (TypeCanCu.DEMAND.Equals(cc.IIDMaChucNang) || TypeCanCu.CHECK_NUMBER.Equals(cc.IIDMaChucNang))
                                {
                                    mucLuc.X4.SoTienMHHV = item.MuaHangHienVat;
                                    mucLuc.X4.SoTienDT = item.DacThu;
                                }
                            }
                        }
                    }

                    if (i == 4)
                    {
                        foreach (var item in lstCanCu)
                        {
                            var mucLuc = Items.FirstOrDefault(x => x.SKyHieu.Equals(item.KyHieu));
                            if (mucLuc != null)
                            {
                                mucLuc.IsUpdateCanCu = true;

                                // Lay so lieu
                                mucLuc.X5.SoTien = item.TuChi;
                                mucLuc.X5.SoTienHN = item.HangNhap;
                                mucLuc.X5.SoTienHM = item.HangMua;
                                mucLuc.X5.SoTienPC = item.PhanCap;
                                mucLuc.X5.Loai = cc.IIDMaChucNang;
                                mucLuc.X5.IdCanCu = cc.Id;

                                if (TypeCanCu.ESTIMATE.Equals(cc.IIDMaChucNang))
                                {
                                    mucLuc.X5.SoTienMHHV = item.HangNhap + item.HangMua;
                                    mucLuc.X5.SoTienDT = item.PhanCap;
                                }

                                if (TypeCanCu.SETTLEMENT.Equals(cc.IIDMaChucNang) || TypeCanCu.ALLOCATION.Equals(cc.IIDMaChucNang))
                                {
                                    mucLuc.X5.SoTienMHHV = item.TuChi;
                                    mucLuc.X5.SoTienDT = 0;
                                }

                                if (TypeCanCu.DEMAND.Equals(cc.IIDMaChucNang) || TypeCanCu.CHECK_NUMBER.Equals(cc.IIDMaChucNang))
                                {
                                    mucLuc.X5.SoTienMHHV = item.MuaHangHienVat;
                                    mucLuc.X5.SoTienDT = item.DacThu;
                                }
                            }
                        }
                    }

                    i++;
                }


            }

            CalculateData();
            OnPropertyChanged(nameof(IsSaveData));
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

        private List<CanCuSoNhuCauQuery> GetCanCuDuToanQtCp(string lstChungTu, string lstIdMucLuc, string idDonVi, string loaiCanCu,
            int namLamViec, int? typeLoaiNNS)
        {
            List<CanCuDuToanQtCpSoNhuCauQuery> lstCanCu = _sktChungTuChiTietService
                .FindCanCuDuToanSoNhuCau(lstChungTu, lstIdMucLuc, idDonVi, loaiCanCu, namLamViec).ToList();
            List<Guid> lstIdMlns = lstCanCu.Select(x => x.IdMlns).Distinct().ToList();
            List<NsMucLucNganSach> sktMucLucs = FindListParentMucLucByChildNamTruoc(lstIdMlns, namLamViec);
            foreach (var mlc in sktMucLucs)
            {
                if (!lstIdMlns.Contains(mlc.MlnsId))
                {
                    CanCuDuToanQtCpSoNhuCauQuery mlCha = new CanCuDuToanQtCpSoNhuCauQuery();
                    mlCha.IdMlns = mlc.MlnsId;
                    mlCha.IdMlnsCha = mlc.MlnsIdParent;
                    mlCha.SXauNoiMa = mlc.XauNoiMa;
                    mlCha.BHangCha = mlc.BHangCha;
                    lstCanCu.Add(mlCha);
                }
            }
            CalculateDataCanCuDuToanNamTruoc(lstCanCu);
            var lstXauNoiMa = lstCanCu.Select(x => x.SXauNoiMa).Distinct().ToList();
            List<NsMlsktMlns> lstMap = new List<NsMlsktMlns>();
            //lstMap = _sktMucLucService.FindByConditionMlsktMlns(x => x.INamLamViec == namLamViec && lstXauNoiMa.Contains(x.SNsXauNoiMa)).ToList();
            // map du lieu mlskt_mlns theo nam lam viec hien tai
            lstMap = _sktMucLucService.FindByConditionMlsktMlns(x => x.INamLamViec == _sessionInfo.YearOfWork && lstXauNoiMa.Contains(x.SNsXauNoiMa)).ToList();
            foreach (var it in lstCanCu)
            {
                var itSkt = lstMap.FirstOrDefault(x => x.SNsXauNoiMa.Equals(it.SXauNoiMa));
                if (itSkt != null)
                {
                    it.KyHieu = itSkt.SSktKyHieu;
                }
            }

            var lstKyHieu = lstCanCu.Select(x => x.KyHieu).Distinct().ToList();
            List<CanCuDuToanQtCpSoNhuCauQuery> results = new List<CanCuDuToanQtCpSoNhuCauQuery>();
            foreach (var kh in lstKyHieu)
            {
                var lstData = typeLoaiNNS switch
                {
                    TypeLoaiNNS.BENH_VIEN => lstCanCu.Where(x => x.KyHieu != null && x.KyHieu.Equals(kh) && new List<string> { "1010100", "1020900", "1020902" }.Any(y => x.SXauNoiMa.StartsWith(y))).ToList(),
                    TypeLoaiNNS.DOANH_NGHIEP => lstCanCu.Where(x => x.KyHieu != null && x.KyHieu.Equals(kh) && new List<string> { "1010400", "1020600", "105" }.Any(y => x.SXauNoiMa.StartsWith(y))).ToList(),
                    _ => lstCanCu.Where(x => x.KyHieu != null && x.KyHieu.Equals(kh) && !new List<string> { "1010100", "1020900", "1020902", "1010400", "1020600", "105" }.Any(y => x.SXauNoiMa.StartsWith(y))).ToList()
                };
                if (lstData.Any())
                {
                    CanCuDuToanQtCpSoNhuCauQuery rs = new CanCuDuToanQtCpSoNhuCauQuery();
                    rs.KyHieu = kh;
                    rs.TuChi = lstData.Sum(x => x.TuChi);
                    rs.HangNhap = lstData.Sum(x => x.HangNhap);
                    rs.HangMua = lstData.Sum(x => x.HangMua);
                    rs.PhanCap = lstData.Sum(x => x.PhanCap);
                    rs.MuaHangHienVat = lstData.Sum(x => x.MuaHangHienVat);
                    rs.DacThu = lstData.Sum(x => x.DacThu);
                    results.Add(rs);
                }
            }
            return _mapper.Map<List<CanCuSoNhuCauQuery>>(results);
        }

        public List<NsMucLucNganSach> FindListParentMucLucByChildNamTruoc(List<Guid> listIdMucLuc, int namLamViec)
        {
            var yearOfWork = namLamViec;
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

        private void CalculateDataCanCuDuToanNamTruoc(List<CanCuDuToanQtCpSoNhuCauQuery> items)
        {
            items.Where(x => x.BHangCha.GetValueOrDefault()).ToList();
            var temp = items.Where(x => !x.BHangCha.GetValueOrDefault());
            foreach (var item in temp)
            {
                CalculateParentCanCuDuToanNamTruoc(item.IdMlnsCha, items, item);
            }
        }

        private void CalculateParentCanCuDuToanNamTruoc(Guid? idParent, List<CanCuDuToanQtCpSoNhuCauQuery> listData, CanCuDuToanQtCpSoNhuCauQuery item)
        {
            var model = listData.FirstOrDefault(x => x.IdMlns == idParent.GetValueOrDefault());
            if (model == null)
                return;
            model.TuChi += item.TuChi;
            model.HangNhap += item.HangNhap;
            model.HangMua += item.HangMua;
            model.PhanCap += item.PhanCap;
            model.MuaHangHienVat += item.MuaHangHienVat;
            model.DacThu += item.DacThu;
            CalculateParentCanCuDuToanNamTruoc(model.IdMlnsCha, listData, item);
        }

        private void LaySummaryCanCu()
        {
            SktChungTuChiTietCriteria searchCondition = new SktChungTuChiTietCriteria();
            searchCondition.NamLamViec = _sessionInfo.YearOfWork;
            searchCondition.NamNganSach = _sessionInfo.YearOfBudget;
            searchCondition.NguonNganSach = _sessionInfo.Budget;
            searchCondition.ITrangThai = StatusType.ACTIVE;
            searchCondition.SktChungTuId = Model.Id;
            searchCondition.ILoai = Model.ILoai;
            searchCondition.IdDonVi = Model.IIdMaDonVi;
            searchCondition.CurrentIdDonVi = _sessionInfo.IdDonVi;
            searchCondition.UserName = _sessionInfo.Principal;
            searchCondition.LoaiChungTu = Model.ILoaiChungTu.GetValueOrDefault();
            searchCondition.IsViewDetailSummary = 1;

            var temp = _sktChungTuChiTietService.FindByConditionForChildUnit(searchCondition);
            var AllItems = _mapper.Map<ObservableCollection<NsSktChungTuChiTietModel>>(temp);

            var idChungTuCon = AllItems.Select(t => t.IIdCtsoKiemTraChild).Distinct().ToList();
            foreach (var item in idChungTuCon)
            {
                var data = Items.Where(t => t.IIdCtsoKiemTraChild == item);
                LoadCanCu(item, data);
            }

            foreach (var sktChungTuChiTietModel in Items)
            {
                var data = AllItems.Where(t => t.SKyHieu.Equals(sktChungTuChiTietModel.SKyHieu));
                sktChungTuChiTietModel.X1.SoTien = data.Select(t => t.X1).Sum(t => t.SoTien);
                sktChungTuChiTietModel.X1.SoTienDT = data.Select(t => t.X1).Sum(t => t.SoTienDT);
                sktChungTuChiTietModel.X1.SoTienHM = data.Select(t => t.X1).Sum(t => t.SoTienHM);
                sktChungTuChiTietModel.X1.SoTienHN = data.Select(t => t.X1).Sum(t => t.SoTienHN);
                sktChungTuChiTietModel.X1.SoTienMHHV = data.Select(t => t.X1).Sum(t => t.SoTienMHHV);
                sktChungTuChiTietModel.X1.SoTienPC = data.Select(t => t.X1).Sum(t => t.SoTienPC);
                sktChungTuChiTietModel.X2.SoTien = data.Select(t => t.X2).Sum(t => t.SoTien);
                sktChungTuChiTietModel.X2.SoTienDT = data.Select(t => t.X2).Sum(t => t.SoTienDT);
                sktChungTuChiTietModel.X2.SoTienHM = data.Select(t => t.X2).Sum(t => t.SoTienHM);
                sktChungTuChiTietModel.X2.SoTienHN = data.Select(t => t.X2).Sum(t => t.SoTienHN);
                sktChungTuChiTietModel.X2.SoTienMHHV = data.Select(t => t.X2).Sum(t => t.SoTienMHHV);
                sktChungTuChiTietModel.X2.SoTienPC = data.Select(t => t.X2).Sum(t => t.SoTienPC);
                sktChungTuChiTietModel.X3.SoTien = data.Select(t => t.X3).Sum(t => t.SoTien);
                sktChungTuChiTietModel.X3.SoTienDT = data.Select(t => t.X3).Sum(t => t.SoTienDT);
                sktChungTuChiTietModel.X3.SoTienHM = data.Select(t => t.X3).Sum(t => t.SoTienHM);
                sktChungTuChiTietModel.X3.SoTienHN = data.Select(t => t.X3).Sum(t => t.SoTienHN);
                sktChungTuChiTietModel.X3.SoTienMHHV = data.Select(t => t.X3).Sum(t => t.SoTienMHHV);
                sktChungTuChiTietModel.X3.SoTienPC = data.Select(t => t.X3).Sum(t => t.SoTienPC);
                sktChungTuChiTietModel.X4.SoTien = data.Select(t => t.X4).Sum(t => t.SoTien);
                sktChungTuChiTietModel.X4.SoTienDT = data.Select(t => t.X4).Sum(t => t.SoTienDT);
                sktChungTuChiTietModel.X4.SoTienHM = data.Select(t => t.X4).Sum(t => t.SoTienHM);
                sktChungTuChiTietModel.X4.SoTienHN = data.Select(t => t.X4).Sum(t => t.SoTienHN);
                sktChungTuChiTietModel.X4.SoTienMHHV = data.Select(t => t.X4).Sum(t => t.SoTienMHHV);
                sktChungTuChiTietModel.X4.SoTienPC = data.Select(t => t.X4).Sum(t => t.SoTienPC);
                sktChungTuChiTietModel.X5.SoTien = data.Select(t => t.X5).Sum(t => t.SoTien);
                sktChungTuChiTietModel.X5.SoTienDT = data.Select(t => t.X5).Sum(t => t.SoTienDT);
                sktChungTuChiTietModel.X5.SoTienHM = data.Select(t => t.X5).Sum(t => t.SoTienHM);
                sktChungTuChiTietModel.X5.SoTienHN = data.Select(t => t.X5).Sum(t => t.SoTienHN);
                sktChungTuChiTietModel.X5.SoTienMHHV = data.Select(t => t.X5).Sum(t => t.SoTienMHHV);
                sktChungTuChiTietModel.X5.SoTienPC = data.Select(t => t.X5).Sum(t => t.SoTienPC);
            }
            CalculateData(AllItems);
        }

        private void CalculateData(IEnumerable<NsSktChungTuChiTietModel> Items)
        {
            Items.Where(x => x.IsHangCha)
                .ForAll(x =>
                {
                    x.FTuChi = 0;
                    x.FTuChiDeNghi = 0;
                    x.FHuyDongTonKho = 0;
                    x.FTonKhoDenNgay = 0;
                    x.FMuaHangCapHienVat = 0;
                    x.FPhanCap = 0;
                    x.X1 = new NsSktChungTuChiTietModel.ChiTietCanCu();
                    x.X2 = new NsSktChungTuChiTietModel.ChiTietCanCu();
                    x.X3 = new NsSktChungTuChiTietModel.ChiTietCanCu();
                    x.X4 = new NsSktChungTuChiTietModel.ChiTietCanCu();
                    x.X5 = new NsSktChungTuChiTietModel.ChiTietCanCu();
                });
            var temp = Items.Where(x => !x.IsHangCha && !x.IsDeleted && x.IsFilter).ToList();
            var dictByMlns = Items.GroupBy(x => x.IIdMlskt).ToDictionary(x => x.Key, x => x.First());
            foreach (var item in temp)
            {
                CalculateParent(item.IdParent, item, dictByMlns);
            }
            Items.OrderBy(t => t.SSTTBC);
            UpdateTotal(temp);
        }

        private void LoadCanCu(Guid idChungTu, IEnumerable<NsSktChungTuChiTietModel> items)
        {
            var loaiChungTu = Model.ILoaiChungTu;
            int yearOfWork = _sessionInfo.YearOfWork;
            var predicate = PredicateBuilder.True<NsCauHinhCanCu>();
            predicate = predicate.And(item => item.SModule == TypeModuleCanCu.DEMAND);
            predicate = predicate.And(item => item.INamLamViec == yearOfWork);
            var listCanCu = _iCauHinhCanCuService.FindByCondition(predicate);
            var cauHinhCanCu = _mapper.Map<ObservableCollection<CauHinhCanCuModel>>(listCanCu);

            int count = 0;
            var predicate1 = PredicateBuilder.True<NsSktChungTuChiTiet>();
            predicate1 = predicate1.And(x => x.IIdCtsoKiemTra == idChungTu);
            var listChungTuChiTiet = _sktChungTuChiTietService.FindByCondition(predicate1);

            foreach (var item in cauHinhCanCu)
            {
                var predicateCc = PredicateBuilder.True<NsSktChungTuChiTietCanCu>();
                predicateCc = predicateCc.And(x => x.IiIdCtsoKiemTra == idChungTu);
                predicateCc = predicateCc.And(x => x.IIdCanCu.Equals(item.Id));
                var lstCanCu = _iSktChungTuChiTietCanCuService.FindByCondition(predicateCc).ToList();
                if (listChungTuChiTiet.Count() == 0)
                {
                    if (!lstCanCu.Any() && (item.IIDMaChucNang.Equals(TypeCanCu.DEMAND)
                                            || item.IIDMaChucNang.Equals(TypeCanCu.CHECK_NUMBER)
                                            || item.IIDMaChucNang.Equals(TypeCanCu.ESTIMATE)))
                    {
                        if (item.IIDMaChucNang.Equals(TypeCanCu.ESTIMATE))
                        {
                            LoadCanCuDuToanDefault(item, count);
                        }
                        else
                        {
                            LoadCanCuDefault(item, count);
                        }
                    }
                }
                foreach (var cc in lstCanCu)
                {
                    var mucLuc = items.FirstOrDefault(x => x.SKyHieu == cc.SKyHieu && x.IIdCtsoKiemTraChild == idChungTu);
                    if (mucLuc != null)
                    {
                        if (count == 0)
                        {
                            // Lay so lieu
                            mucLuc.X1.SoTien = cc.FTuChi;
                            mucLuc.X1.SoTienMHHV = cc.FMuaHangCapHienVat;
                            mucLuc.X1.SoTienDT = cc.FPhanCap;
                        }

                        if (count == 1)
                        {
                            // Lay so lieu
                            mucLuc.X2.SoTien = cc.FTuChi;
                            mucLuc.X2.SoTienMHHV = cc.FMuaHangCapHienVat;
                            mucLuc.X2.SoTienDT = cc.FPhanCap;
                        }

                        if (count == 2)
                        {
                            // Lay so lieu
                            mucLuc.X3.SoTien = cc.FTuChi;
                            mucLuc.X3.SoTienMHHV = cc.FMuaHangCapHienVat;
                            mucLuc.X3.SoTienDT = cc.FPhanCap;
                        }

                        if (count == 3)
                        {
                            // Lay so lieu
                            mucLuc.X4.SoTien = cc.FTuChi;
                            mucLuc.X4.SoTienMHHV = cc.FMuaHangCapHienVat;
                            mucLuc.X4.SoTienDT = cc.FPhanCap;
                        }

                        if (count == 4)
                        {
                            // Lay so lieu
                            mucLuc.X5.SoTien = cc.FTuChi;
                            mucLuc.X5.SoTienMHHV = cc.FMuaHangCapHienVat;
                            mucLuc.X5.SoTienDT = cc.FPhanCap;
                        }
                    }
                }
                count++;

            }
        }
    }
}