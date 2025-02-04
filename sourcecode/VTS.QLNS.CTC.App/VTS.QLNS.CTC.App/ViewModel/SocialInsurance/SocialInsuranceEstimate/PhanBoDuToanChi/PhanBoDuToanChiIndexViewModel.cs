using AutoMapper;
using log4net;
using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Data;
using VTS.QLNS.CTC.App.Command;
using VTS.QLNS.CTC.App.Helper;
using VTS.QLNS.CTC.App.Model;
using VTS.QLNS.CTC.App.Model.Control;
using VTS.QLNS.CTC.App.Properties;
using VTS.QLNS.CTC.App.Service;
using VTS.QLNS.CTC.App.Service.Impl;
using VTS.QLNS.CTC.App.View.SocialInsurance.SocialInsuranceEstimate.PhanBoDuToanChi;
using VTS.QLNS.CTC.App.View.SocialInsurance.SocialInsuranceEstimate.PhanBoDuToanChi.PrintReport;
using VTS.QLNS.CTC.App.View.SocialInsurance.SocialInsuranceEstimate.Report;
using VTS.QLNS.CTC.App.ViewModel.SocialInsurance.SocialInsuranceEstimate.PhanBoDuToanChi.ExportReport;
using VTS.QLNS.CTC.App.ViewModel.SocialInsurance.SocialInsuranceEstimate.PhanBoDuToanChi.PrintReport;
using VTS.QLNS.CTC.App.ViewModel.SocialInsurance.SocialInsuranceEstimate.Report;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Service;
using VTS.QLNS.CTC.Core.Service.Impl;
using VTS.QLNS.CTC.Utility;
using VTS.QLNS.CTC.Utility.Enum;

namespace VTS.QLNS.CTC.App.ViewModel.SocialInsurance.SocialInsuranceEstimate.PhanBoDuToanChi
{
    public class PhanBoDuToanChiIndexViewModel : GridViewModelBase<BhPbdtcBHXHModel>
    {
        private readonly ILog _logger;
        private readonly IMapper _mapper;
        private readonly ISessionService _sessionService;
        private readonly IPbdtcBHXHService _pbdtcBHXHService;
        private readonly IPbdtcBHXHChiTietService _pbdtcBHXHChiTietService;
        private readonly IPbdtcMapBHXHService _pbdtcMapBHXHService;
        private readonly IBhDmMucLucNganSachService _bhDmMucLucNganSachService;
        private readonly IBhDtCtctKPQLService _bhDtCtctKPQLService;
        private readonly IExportService _exportService;
        private readonly INsDonViService _donViService;
        private readonly IDanhMucService _danhMucService;
        private readonly IVdtFtpRootService _ftpService;
        private readonly FtpStorageService _ftpStorageService;
        private readonly IBhDanhMucLoaiChiService _bhDanhMucLoaiChiService;
        private ICollectionView _dtChungTuView;
        private List<BhPbdtcBHXHChiTietModel> _listChungTuChiTiet;
        private SessionInfo _sessionInfo;
        private ICollectionView _bhDanhMucLoaiChiModelView;

        public override string FuncCode => NSFunctionCode.BUDGET_ESTIMATE_DIVISION;
        public override string GroupName => MenuItemContants.GROUP_CHI;
        public override string Name => "Phân bổ DT chi";
        public override string Description => "Danh sách đợt phân bổ dự toán chi";
        public override Type ContentType => typeof(PhanBoDuToanChiIndex);
        public override PackIconKind IconKind => PackIconKind.AxisArrow;
        public string ComboboxDisplayMemberPath => nameof(SelecteBhDanhMucLoaiChi.STenDanhMucLoaiChi);
        public bool IsEdit => SelectedItem != null && !SelectedItem.BIsKhoa;
        //public bool IsLock => SelectedItem != null && SelectedItem.bIsKhoa;
        private bool _isLock;
        public bool IsLock
        {
            get => _isLock;
            set => SetProperty(ref _isLock, value);
        }
        public bool IsEnableLockMultiple => Items.All(x => !x.Selected) ? false : Items.Any(x => x.Selected && !x.BIsKhoa);
        public bool IsEnableUnlockMultiple => Items.All(x => !x.Selected) ? false : Items.Any(x => x.Selected && x.BIsKhoa);
        //public bool IsEnableLock => SelectedItem != null;
        public bool IsEnableLock
        {
            get
            {
                var listItemSelected = Items.Where(n => n.Selected).ToList();
                var result = false;
                if (LockStatusSelected != null && !LockStatusSelected.ValueItem.Equals("0") && listItemSelected.Count > 0)
                {
                    result = true;
                }
                else
                {
                    var lstSelectedKhoa = listItemSelected.Where(x => x.BIsKhoa).ToList();
                    var lstSelectedMo = listItemSelected.Where(x => !x.BIsKhoa).ToList();
                    if (lstSelectedKhoa.Count() > 0 && lstSelectedMo.Count() > 0)
                    {
                        result = false;
                    }
                    else if (lstSelectedKhoa.Count() > 0)
                    {
                        IsLock = true;
                        result = true;
                    }
                    else if (lstSelectedMo.Count() > 0)
                    {
                        IsLock = false;
                        result = true;
                    }
                }
                return result;
            }
        }

        private BhDanhMucLoaiChiModel _selecteBhDanhMucLoaiChi;
        public BhDanhMucLoaiChiModel SelecteBhDanhMucLoaiChi
        {
            get => _selecteBhDanhMucLoaiChi;
            set
            {
                SetProperty(ref _selecteBhDanhMucLoaiChi, value);
                SearchData();
            }
        }

        private ObservableCollection<BhDanhMucLoaiChiModel> _bhDanhMucLoaiChiItems;
        public ObservableCollection<BhDanhMucLoaiChiModel> BhDanhMucLoaiChiItems
        {
            get => _bhDanhMucLoaiChiItems;
            set => SetProperty(ref _bhDanhMucLoaiChiItems, value);
        }

        public bool HasParentAgency { get; set; }

        public Dictionary<string, string> DictIdChungTu = new Dictionary<string, string>();

        private ObservableCollection<ComboboxItem> _cbxVoucherType;
        public ObservableCollection<ComboboxItem> CbxVoucherType
        {
            get => _cbxVoucherType;
            set => SetProperty(ref _cbxVoucherType, value);
        }

        private ObservableCollection<ComboboxItem> _lockStatus = new ObservableCollection<ComboboxItem>();

        public ObservableCollection<ComboboxItem> LockStatus
        {
            get => _lockStatus;
            set => SetProperty(ref _lockStatus, value);
        }

        private ComboboxItem _lockStatusSelected;

        public ComboboxItem LockStatusSelected
        {
            get => _lockStatusSelected;
            set
            {
                SetProperty(ref _lockStatusSelected, value);
                OnPropertyChanged(nameof(IsEnableLock));
                if (_lockStatusSelected != null && _lockStatusSelected.ValueItem.Equals("1"))
                {
                    IsLock = true;
                }
                else if (_lockStatusSelected != null && _lockStatusSelected.ValueItem.Equals("2"))
                {
                    IsLock = false;
                }
                if (_dtChungTuView != null)
                {
                    _dtChungTuView.Refresh();
                }
            }
        }

        public bool IsExportGridData
        {
            get
            {
                if (Items == null || !Items.Any())
                {
                    return false;
                }
                return Items.Any(item => item.Selected);
            }
        }

        private bool _isAllItemsSelected;
        public bool IsAllItemsSelected
        {
            get
            {
                if (Items == null || !Items.Any())
                {
                    return false;
                }
                return !Items.Any() ? false : Items.All(item => item.Selected);
            }
            set
            {
                SetProperty(ref _isAllItemsSelected, value);
                if (Items != null)
                {
                    Items.ForAll(c => c.Selected = _isAllItemsSelected);
                }
            }
        }

        private bool _isOpenPrintPopup;
        public bool IsOpenPrintPopup
        {
            get => _isOpenPrintPopup;
            set => SetProperty(ref _isOpenPrintPopup, value);
        }

        private BhPbdtcBHXHModel _selectedBhPhanBoDuToanChiModel;
        public BhPbdtcBHXHModel SelectedBhPhanBoDuToanChiModel
        {
            get => _selectedBhPhanBoDuToanChiModel;
            set
            {
                SetProperty(ref _selectedBhPhanBoDuToanChiModel, value);
            }
        }

        protected override void OnSelectedItemChanged()
        {
            base.OnSelectedItemChanged();
            OnPropertyChanged(nameof(IsEdit));
            //OnPropertyChanged(nameof(IsLock));
            OnPropertyChanged(nameof(IsEnableLock));
        }
        public PhanBoDuToanChiDialogViewModel PhanBoDuToanChiDialogViewModel { get; set; }
        public PhanBoDuToanChiDetailViewModel PhanBoDuToanChiDetailViewModel { get; set; }
        public ExportPhanBoDuToanChiViewModel ExportPhanBoDuToanChiViewModel { get; set; }
        public TongHopThuChiViewModel TongHopThuChiViewModel { get; set; }
        public PrintPhanBoTuToanChiTheoDonViViewModel PrintPhanBoTuToanChiTheoDonViViewModel { get; set; }
        public PrintPhuLucGiaoDuToanDuToanChiViewModel PrintPhuLucGiaoDuToanDuToanChiViewModel { get; set; }
        public PrintChiTietDuToanChiKPQLViewModel PrintChiTietDuToanChiKPQLViewModel { get; set; }
        public RelayCommand PrintReportCommand { get; }
        public RelayCommand PrintCommand { get; }
        public RelayCommand SelectionChangedCommand { get; }
        public RelayCommand ExportGridDataCommand { get; }
        public PhanBoDuToanChiIndexViewModel(
            IExportService exportService,
            INsDonViService donViService,
            IPbdtcBHXHService pbdtcBHXHService,
            IPbdtcBHXHChiTietService pbdtcBHXHChiTietService,
            IPbdtcMapBHXHService pbdtcMapBHXHService,
            IBhDmMucLucNganSachService bhDmMucLucNganSachService,
            ILog logger,
            IMapper mapper,
            PhanBoDuToanChiDialogViewModel phanBoDuToanChiDialogViewModel,
            PhanBoDuToanChiDetailViewModel phanBoDuToanChiDetailViewModel,
            ExportPhanBoDuToanChiViewModel exportPhanBoDuToanChiViewModel,
            TongHopThuChiViewModel tongHopThuChiViewModel,
            IBhDanhMucLoaiChiService bhDanhMucLoaiChiService,
            IDanhMucService danhMucService,
            PrintPhanBoTuToanChiTheoDonViViewModel printPhanBoTuToanChiTheoDonViViewModel,
            ISessionService sessionService,
            PrintPhuLucGiaoDuToanDuToanChiViewModel printPhuLucGiaoDuToanDuToanChiViewModel,
            IBhDtCtctKPQLService bhDtCtctKPQLService,
            PrintChiTietDuToanChiKPQLViewModel printChiTietDuToanChiKPQLViewModel)
        {
            _logger = logger;
            _mapper = mapper;
            _exportService = exportService;
            _sessionService = sessionService;
            _donViService = donViService;
            _pbdtcBHXHService = pbdtcBHXHService;
            _pbdtcBHXHChiTietService = pbdtcBHXHChiTietService;
            _pbdtcMapBHXHService = pbdtcMapBHXHService;
            _bhDmMucLucNganSachService = bhDmMucLucNganSachService;
            _bhDanhMucLoaiChiService = bhDanhMucLoaiChiService;
            _danhMucService = danhMucService;

            TongHopThuChiViewModel = tongHopThuChiViewModel;
            PhanBoDuToanChiDialogViewModel = phanBoDuToanChiDialogViewModel;
            PhanBoDuToanChiDetailViewModel = phanBoDuToanChiDetailViewModel;
            ExportPhanBoDuToanChiViewModel = exportPhanBoDuToanChiViewModel;
            PrintPhanBoTuToanChiTheoDonViViewModel = printPhanBoTuToanChiTheoDonViViewModel;

            SelectionChangedCommand = new RelayCommand(OnSelectedChange);
            ExportGridDataCommand = new RelayCommand(obj => OnExportDataDialog());
            PrintCommand = new RelayCommand(OnPrint);
            PrintReportCommand = new RelayCommand(OnPrintForAgency);
            PrintPhuLucGiaoDuToanDuToanChiViewModel = printPhuLucGiaoDuToanDuToanChiViewModel;
            _bhDtCtctKPQLService = bhDtCtctKPQLService;
            PrintChiTietDuToanChiKPQLViewModel = printChiTietDuToanChiKPQLViewModel;
        }

        private void LoadLockStatus()
        {
            var lockStatus = new List<ComboboxItem>
            {
                new ComboboxItem {DisplayItem = "Toàn bộ", ValueItem = "0"},
                new ComboboxItem {DisplayItem = "Đã khóa", ValueItem = "1"},
                new ComboboxItem {DisplayItem = "Chưa khóa", ValueItem = "2"},
            };

            LockStatus = new ObservableCollection<ComboboxItem>(lockStatus);
            LockStatusSelected = LockStatus.ElementAt(0);
        }

        public override void Init()
        {
            _sessionInfo = _sessionService.Current;
            LoadLockStatus();
            LoadDanhMucLoaiChi();
            LoadData();
        }

        private void LoadDanhMucLoaiChi()
        {
            var listDmLoaiChi = _bhDanhMucLoaiChiService.FindByNamLamViec(_sessionInfo.YearOfWork).ToList();
            BhDanhMucLoaiChiItems = _mapper.Map<ObservableCollection<BhDanhMucLoaiChiModel>>(listDmLoaiChi);
            _bhDanhMucLoaiChiModelView = CollectionViewSource.GetDefaultView(BhDanhMucLoaiChiItems);
            _bhDanhMucLoaiChiModelView.SortDescriptions.Add(new SortDescription(nameof(BhDanhMucLoaiChiModel.SMaLoaiChi),
                ListSortDirection.Ascending));
            _bhDanhMucLoaiChiModelView.SortDescriptions.Add(new SortDescription(nameof(BhDanhMucLoaiChiModel.STenDanhMucLoaiChi),
                ListSortDirection.Ascending));
        }

        private void LoadData()
        {
            var listChungTu = _pbdtcBHXHService.GetDanhSachPhanBoDuToanChi(_sessionService.Current.YearOfWork).OrderBy(x => x.DNgayChungTu).ToList();

            if (SelecteBhDanhMucLoaiChi != null)
            {
                listChungTu = listChungTu.Where(x => x.IIDLoaiDanhMucChi == SelecteBhDanhMucLoaiChi.Id).ToList();
            }

            Items = _mapper.Map<ObservableCollection<BhPbdtcBHXHModel>>(listChungTu);
            //Items.ForAll(x => x.ListSoChungTuDotNhan = string.Join(",", x.IIdDotNhan.Split(",").Select(e => DictIdChungTu.GetValueOrDefault(e, string.Empty))));

            _dtChungTuView = CollectionViewSource.GetDefaultView(Items);
            _dtChungTuView.Filter = VoucherFilter;

            foreach (var item in Items)
            {
                item.PropertyChanged += (sender, args) =>
                {
                    if (args.PropertyName == nameof(BhPbdtcBHXHModel.Selected))
                    {
                        OnPropertyChanged(nameof(IsExportGridData));
                        OnPropertyChanged(nameof(IsAllItemsSelected));
                        OnPropertyChanged(nameof(IsEnableLock));
                        OnPropertyChanged(nameof(IsEnableLockMultiple));
                        OnPropertyChanged(nameof(IsEnableUnlockMultiple));
                    }
                };
            }
        }

        public void SearchData()
        {
            LoadData();
        }

        protected override void OnDelete()
        {
            base.OnDelete();

            if (SelectedItem.SNguoiTao != _sessionService.Current.Principal)
            {
                System.Windows.MessageBox.Show(string.Format(Resources.MsgRoleDelete, SelectedItem.SNguoiTao), "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            StringBuilder messageBuilder = new StringBuilder();
            messageBuilder.AppendFormat(Resources.DeleteChungTu, SelectedItem.SSoChungTu, SelectedItem.DNgayQuyetDinh.HasValue ? SelectedItem.DNgayChungTu.Value.ToString("dd/MM/yyyy") : string.Empty);
            MessageBoxResult result = MessageBox.Show(messageBuilder.ToString(), Resources.ConfirmTitle, MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
                DeleteSelectedVoucher();
        }

        private void DeleteSelectedVoucher()
        {
            BhPbdtcBHXH chungtu = new BhPbdtcBHXH();
            _mapper.Map(SelectedBhPhanBoDuToanChiModel, chungtu);


            //Xóa chứng từ Map
            List<BhdtcnpbMapBHXH> chungtuMaps = new List<BhdtcnpbMapBHXH>();
            var predicate_map = PredicateBuilder.True<BhdtcnpbMapBHXH>();
            predicate_map = predicate_map.And(x => x.iID_BHDTC_PhanBo == chungtu.Id);
            chungtuMaps = _pbdtcMapBHXHService.FindByCondition(predicate_map).ToList();
            _pbdtcMapBHXHService.RemoveRange(chungtuMaps);

            //Xóa chứng từ chi tiét
            List<BhPbdtcBHXHChiTiet> chungtuChiTiets = new List<BhPbdtcBHXHChiTiet>();
            var predicate_chitiet = PredicateBuilder.True<BhPbdtcBHXHChiTiet>();
            predicate_chitiet = predicate_chitiet.And(x => x.IID_DTC_PhanBoDuToanChi == chungtu.Id);
            chungtuChiTiets = _pbdtcBHXHChiTietService.FindByCondition(predicate_chitiet).ToList();
            _pbdtcBHXHChiTietService.RemoveRange(chungtuChiTiets);

            var lstDtCtctKPQL = _bhDtCtctKPQLService.FindByCondition(chungtu.Id).ToList();
            _bhDtCtctKPQLService.RemoveRange(lstDtCtctKPQL);
            //Xóa chứng từ 
            _pbdtcBHXHService.Delete(chungtu);


            MessageBox.Show(Resources.MsgDeleteSuccess, Resources.NotifiTitle, MessageBoxButton.OK, MessageBoxImage.Information);
            OnRefresh();
        }

        private void OnSelectedChange(object obj)
        {
            SelectedBhPhanBoDuToanChiModel = (BhPbdtcBHXHModel)obj;

        }

        protected override void OnLockUnLock()
        {
            if (IsLock)
            {
                var listSoChungTu = string.Join(", ", Items.Where(n => n.Selected && n.BIsKhoa).Select(n => n.SSoChungTu));
                List<DonVi> userAgency = _donViService.FindByUser(_sessionInfo.Principal, _sessionInfo.YearOfWork, LoaiDonVi.ROOT);
                if (!userAgency.Any(x => x.Loai == LoaiDonVi.ROOT))
                {
                    MessageBox.Show(string.Format("Đồng chí không được mở khóa chứng từ {0} do không có quyền tổng hợp", listSoChungTu), Resources.NotifiTitle, MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }
            }
            else
            {
                var listSoChungTuInvalid = string.Join(", ", Items.Where(n => n.Selected && n.SNguoiTao != _sessionInfo.Principal && !n.BIsKhoa).Select(n => n.SSoChungTu));

                if (!string.IsNullOrEmpty(listSoChungTuInvalid))
                {
                    MessageBox.Show(string.Format("Đồng chí không có quyền kháo chứng từ {0} do không phải người tạo", listSoChungTuInvalid), Resources.NotifiTitle, MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

            }
            string message = IsLock ? Resources.UnlockChungTu : Resources.LockChungTu;
            string msgDone = IsLock ? Resources.MsgUnLockDone : Resources.MsgLockDone;
            MessageBoxResult result = MessageBox.Show(message, Resources.ConfirmTitle, MessageBoxButton.YesNo, MessageBoxImage.Question);

            var lstChungTuChon = Items.Where(n => n.Selected).ToList();
            if (_lockStatusSelected != null && _lockStatusSelected.ValueItem.Equals("1"))
            {
                lstChungTuChon = lstChungTuChon.Where(x => x.BIsKhoa).ToList();
            }
            else if (_lockStatusSelected != null && _lockStatusSelected.ValueItem.Equals("2"))
            {
                lstChungTuChon = lstChungTuChon.Where(x => !x.BIsKhoa).ToList();
            }

            if (result == MessageBoxResult.Yes)
            {
                foreach (var SelectedItemElement in lstChungTuChon)
                {
                    var rs = _pbdtcBHXHService.LockOrUnLock(SelectedItemElement.Id, !SelectedItemElement.BIsKhoa);
                    if (rs == DBContextSaveChangeState.SUCCESS)
                    {
                        SelectedItemElement.BIsKhoa = !SelectedItemElement.BIsKhoa;
                        OnPropertyChanged(nameof(IsLock));
                        OnPropertyChanged(nameof(IsEdit));
                    }
                }

                MessageBoxHelper.Info(msgDone);
                LockStatusSelected = LockStatus.ElementAt(0);
            }
        }


        private bool VoucherFilter(object obj)
        {
            bool result = true;
            var item = (BhPbdtcBHXHModel)obj;
            if (LockStatusSelected != null && LockStatusSelected.ValueItem.Equals("1"))
            {
                result = result && item.BIsKhoa;
            }

            if (LockStatusSelected != null && LockStatusSelected.ValueItem.Equals("2"))
            {
                result = result && !item.BIsKhoa;
            }


            return result;
        }

        protected override void OnRefresh()
        {
            LoadData();
        }

        protected override void OnAdd()
        {
            PhanBoDuToanChiDialogViewModel.Name = "Thêm mới chứng từ";
            PhanBoDuToanChiDialogViewModel.Description = "Tạo mới chứng từ nhận dự toán trên giao";
            //PhanBoDuToanChiDialogViewModel.IsEdit = false;
            PhanBoDuToanChiDialogViewModel.Model = new BhPbdtcBHXHModel();
            PhanBoDuToanChiDialogViewModel.Init();
            PhanBoDuToanChiDialogViewModel.SavedAction = obj =>
            {
                var pbdtcChungTu = (BhPbdtcBHXHModel)obj;
                this.LoadData();
                OpenDetailDialog(pbdtcChungTu);
            };
            var exportView = new PhanBoDuToanChiDialog() { DataContext = PhanBoDuToanChiDialogViewModel };
            DialogHost.Show(exportView, DemandCheckScreen.ROOT_DIALOG);
        }
        private void OpenDetailDialog(BhPbdtcBHXHModel bhPbdtcBHXHModel, params bool[] isNew)
        {
            PhanBoDuToanChiDetailViewModel.Model = bhPbdtcBHXHModel;
            PhanBoDuToanChiDetailViewModel.UpdateParentWindowEventHandler += RefreshAfterSaveData;
            PhanBoDuToanChiDetailViewModel.Init();
            PhanBoDuToanChiDetailViewModel.SavedAction = obj =>
            {
                OnRefresh();
            };
            var view = new PhanBoDuToanChiDetail() { DataContext = PhanBoDuToanChiDetailViewModel };

            view.ShowDialog();
        }
        protected override void OnUpdate()
        {
            //check quyền được chỉnh sửa
            if (SelectedItem.SNguoiTao != _sessionInfo.Principal)
            {
                MessageBox.Show(string.Format(Resources.MsgRoleUpdate, SelectedItem.SNguoiTao), Resources.NotifiTitle, MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            PhanBoDuToanChiDialogViewModel.Model = SelectedItem;
            PhanBoDuToanChiDialogViewModel.RefreshVoucherEvent += (object sender, EventArgs e) =>
            {
                OnRefresh();
            };
            PhanBoDuToanChiDialogViewModel.Init();
            PhanBoDuToanChiDialogViewModel.SavedAction = obj =>
            {
                this.OnRefresh();
            };
            var view = new PhanBoDuToanChiDialog { DataContext = PhanBoDuToanChiDialogViewModel };

            DialogHost.Show(view, SystemConstants.ROOT_DIALOG, null, null);
            PhanBoDuToanChiDialogViewModel.RefreshVoucherEvent -= (object sender, EventArgs e) =>
            {
                OnRefresh();
            };
        }

        protected override void OnSelectionDoubleClick(object obj)
        {
            base.OnSelectionDoubleClick(obj);
            OpenDetailDialog((BhPbdtcBHXHModel)obj);
        }

        private void OnOpenDivisionDetail(DtChungTuModel selectEstimation)
        {
        }

        private void RefreshAfterSaveData(object sender, EventArgs e)
        {

        }

        private void OnExportDataDialog()
        {
            //ExportPhanBoDuToanChiViewModel._listChungTuChiTiet = _listChungTuChiTiet;
            //ExportPhanBoDuToanChiViewModel.Items = Items;
            //ExportPhanBoDuToanChiViewModel.Init();
            //var addView = new ExportPhanBoDuToanChi() { DataContext = ExportPhanBoDuToanChiViewModel };
            //DialogHost.Show(addView, SettlementScreen.ROOT_DIALOG, null, null);


            BackgroundWorkerHelper.Run((s, e) =>
            {
                IsLoading = true;

                string chiTietToi = "NG";
                DanhMuc danhMucChiTietToi = _danhMucService.FindByType(TypeDanhMuc.DM_CAUHINH, _sessionService.Current.YearOfWork).Where(n => n.IIDMaDanhMuc == MaDanhMuc.MLNS_CHITIET_TOI).FirstOrDefault();
                if (danhMucChiTietToi != null)
                    chiTietToi = danhMucChiTietToi.SGiaTri;
                var itemDanhMuc = _danhMucService.FindByType(TypeDanhMuc.DM_CAUHINH, _sessionService.Current.YearOfWork).Where(n => n.IIDMaDanhMuc == MaDanhMuc.DV_QUANLY).FirstOrDefault();
                List<ExportResult> results = new List<ExportResult>();

                string templateFile = "rpt_BH_PhanBoDuToanChi.xlsx";

                var namLamViec = _sessionService.Current.YearOfWork;

                var predicate_danhmuc = PredicateBuilder.True<BhDmMucLucNganSach>();
                predicate_danhmuc = predicate_danhmuc.And(x => x.INamLamViec == namLamViec);


                List<DonVi> listNsDonVi = new List<DonVi>();
                listNsDonVi = _donViService.FindByUser(_sessionService.Current.Principal, _sessionService.Current.YearOfWork, string.Join(StringUtils.COMMA, new string[] { LoaiDonVi.NOI_BO, LoaiDonVi.ROOT })).ToList();

                if (listNsDonVi.Any(x => x.Loai == LoaiDonVi.ROOT))
                {
                    var predicate = PredicateBuilder.True<DonVi>();
                    predicate = predicate.And(x => x.NamLamViec == _sessionService.Current.YearOfWork);
                    predicate = predicate.And(x => x.Loai == SoChungTuType.EstimateDivision.ToString());
                    predicate = predicate.And(x => x.ITrangThai == NSEntityStatus.ACTIVED);

                    listNsDonVi = _donViService.FindByCondition(predicate).ToList();
                }
                var itemsExport = Items.Where(x => x.Selected);

                foreach (var item in itemsExport)
                {
                    var listMucLucNganSach = _bhDmMucLucNganSachService.FindByCondition(predicate_danhmuc).OrderBy(x => x.SXauNoiMa).ToList();

                    List<string> lstDonViPhanBo = item.SID_MaDonVi.Split(",").ToList();
                    var lstLNS = GetValueSLNS(item.SLNS);
                    listMucLucNganSach = listMucLucNganSach.Where(x => lstLNS.Contains(x.SLNS)).ToList();
                    foreach (var dv in lstDonViPhanBo)
                    {
                        var dataExportDetail = LoadDataExportDetail(item, dv);
                        //dataExportDetail = dataExportDetail.Where(x => string.IsNullOrEmpty(x.SM) || !string.IsNullOrEmpty(x.SDuToanChiTietToi)).ToList();
                        var donvi = listNsDonVi.Where(x => x.IIDMaDonVi == dv).FirstOrDefault();
                        //var lstDataPrent = dataExportDetail.Where(x => x.BHangCha).ToList();
                        //var lstDataChildbyDonVi = dataExportDetail.Where(x => x.IID_MaDonVi == dv && !x.BHangCha).ToList();

                        //lstDataPrent.AddRange(dataExportDetail);
                        List<BhPbdtcBHXHChiTietModel> lstData = new List<BhPbdtcBHXHChiTietModel>();
                        lstData = _mapper.Map(dataExportDetail, lstData);


                        CalculateData(lstData);

                        lstData = lstData.Where(x => (x.FTienTuChi ?? 0) != 0).OrderBy(x => x.SXauNoiMa).ToList();

                        var data = new Dictionary<string, object>();
                        data.Add("Cap1", itemDanhMuc != null ? itemDanhMuc.SGiaTri.ToUpper() : "");
                        data.Add("Cap2", _sessionService.Current.TenDonVi.ToUpper());
                        data.Add("TitleFirst", $"DỰ TOÁN CHI NGÂN SÁCH NĂM {_sessionService.Current.YearOfWork}");
                        data.Add("TitleSecond", $"(Kèm theo Quyết định số: {item.SSoQuyetDinh}, ngày: {DateUtils.Format(item.DNgayQuyetDinh)})");

                        data.Add("HeaderTenDonVi", $"Đơn vị: {donvi?.IIDMaDonVi.PadLeft(3, '0')}{StringUtils.DIVISION}{donvi?.TenDonVi}");
                        data.Add("TenDonVi", $"{donvi?.IIDMaDonVi.PadLeft(3, '0')}{StringUtils.DIVISION}{donvi?.TenDonVi}");
                        data.Add("SoChungTu", item.SSoChungTu);
                        data.Add("NgayChungTu", DateUtils.Format(item.DNgayChungTu));
                        data.Add("SoQuyetDinh", item.SSoQuyetDinh);
                        data.Add("NgayQuyetDinh", DateUtils.Format(item.DNgayQuyetDinh));
                        data.Add("MoTa", item.SMoTa);
                        data.Add("LoaiDuToan", VoucherType.BudgetTypeDict.GetValueOrDefault(item.ILoaiDotNhanPhanBo, string.Empty));
                        data.Add("LoaiChungTu", VoucherType.VoucherTypeDict.GetValueOrDefault(item.ILoaiChungTu, string.Empty));
                        data.Add("NguoiTao", item.SNguoiTao);
                        data.Add("NgayTao", DateUtils.Format(item.DNgayTao));

                        data.Add("Items", lstData);
                        data.Add("MLNS", listMucLucNganSach);

                        double? TotalTuChi = lstData?.Where(x => !string.IsNullOrEmpty(x.SDuToanChiTietToi)).Sum(x => x.FTienTuChi);
                        //double? TotalHienVat = lstData?.Where(x => !x.BHangCha).Sum(x => x.FTienHienVat);

                        data.Add("TotalTuChi", string.Format(StringUtils.FORMAT_ZERO, TotalTuChi));

                        List<int> hideColumns = new List<int>();
                        hideColumns.AddRange(ExportExcelHelper<BhPbdtcBHXHChiTietModel>.HideColumn(chiTietToi));

                        string templateFileName = Path.Combine(ExportPrefix.PATH_BH_DT_DTCPBCL, templateFile);
                        var xlsFile = _exportService.Export<BhPbdtcBHXHChiTietModel, BhDmMucLucNganSach>(templateFileName, data, hideColumns);
                        var nameRange = xlsFile.GetNamedRange(1);
                        nameRange.Comment = "Workbook";
                        xlsFile.SetNamedRange(nameRange);
                        xlsFile.SetNamedRange(new FlexCel.Core.TXlsNamedRange("__Area_Items__", 1, 0, "=1:2,A:B"));
                        xlsFile.SetCellValue(50, 50, "CheckSum");
                        xlsFile.SetRowHidden(50, true);
                        string fileNamePrefix = string.Format("{0}_{1}_{2}", item.SSoChungTu, item.SSoQuyetDinh, StringUtils.ConvertVN(donvi?.TenDonVi));
                        string fileNameWithoutExtension = StringUtils.CreateExportFileName(fileNamePrefix);
                        results.Add(new ExportResult(fileNameWithoutExtension, fileNameWithoutExtension, null, xlsFile));
                    }
                }

                e.Result = results;
            }, (s, e) =>
            {
                if (e.Error == null)
                {
                    var result = (List<ExportResult>)e.Result;
                    _exportService.Open(result, ExportType.EXCEL);
                }
                else
                {
                    _logger.Error(e.Error.Message);
                }
                IsLoading = false;
            });

        }

        private string GetValueSLNS(string SLNS)
        {
            var lstSLNS = SLNS.Split(",");
            string sLNS = SLNS;
            if (lstSLNS.Contains(LNSValue.LNS_9010001) || lstSLNS.Contains(LNSValue.LNS_9010002))
            {
                sLNS += "," + LNSValue.LNS_9_901;
            }

            if (lstSLNS.Contains(LNSValue.LNS_9010003) || lstSLNS.Contains(LNSValue.LNS_9010004)
                || lstSLNS.Contains(LNSValue.LNS_9010006) || lstSLNS.Contains(LNSValue.LNS_9010008)
                || lstSLNS.Contains(LNSValue.LNS_9010009) || lstSLNS.Contains(LNSValue.LNS_9010010))
            {
                sLNS += "," + LNSValue.LNS_9;
            }
            return sLNS;
        }


        private List<BhPbdtcBHXHChiTietQuery> LoadDataExportDetail(BhPbdtcBHXHModel item, string sMaDonVi)
        {
            string sLNS = GetValueSLNS(item.SLNS);
            var lstChungTuChiTiet = _pbdtcBHXHChiTietService.ExportExcelPhanBoDuToanChi(item.Id, sLNS, item.INamChungTu, sMaDonVi).ToList();
            lstChungTuChiTiet.ForEach(x =>
            {
                x.FTienTuChi = Math.Round(x.FTienTuChi ?? 0, 0, MidpointRounding.AwayFromZero);
            });
            return lstChungTuChiTiet;
        }

        private void CalculateData(List<BhPbdtcBHXHChiTietModel> listData)
        {
            listData.Where(x => x.BHangCha)
                .Select(x =>
                {
                    x.FTienHienVat = 0;
                    x.FTienTuChi = 0;
                    return x;
                }).ToList();

            var dictByMlns = listData.GroupBy(x => x.IID_MLNS).ToDictionary(x => x.Key, x => x.First());
            var temp = listData.Where(x => !x.BHangCha).ToList();
            foreach (var item in temp)
            {

                CalculateParent(item.IID_MLNS_Cha, item, dictByMlns);
            }
        }

        private void CalculateParent(Guid? idParent, BhPbdtcBHXHChiTietModel item, Dictionary<Guid?, BhPbdtcBHXHChiTietModel> dictByMlns)
        {
            if (idParent == null || !dictByMlns.ContainsKey(idParent))
            {
                return;
            }

            var model = dictByMlns[idParent];
            if (!model.IsRemainRow || model.Type != 2)
            {
                //Trước điều chỉnh
                model.FTienTuChiTruocDieuChinh = (model.FTienTuChiTruocDieuChinh == null ? 0 : model.FTienTuChiTruocDieuChinh) + (item.FTienTuChiTruocDieuChinh == null ? 0 : item.FTienTuChiTruocDieuChinh);
                //model.FTienHienVatTruocDieuChinh = (model.FTienHienVatTruocDieuChinh == null ? 0 : model.FTienHienVatTruocDieuChinh) + (item.FTienHienVatTruocDieuChinh == null ? 0 : item.FTienHienVatTruocDieuChinh);

                //model.FTienHienVat = (model.FTienHienVat == null ? 0 : model.FTienHienVat) + (item.FTienHienVat == null ? 0 : item.FTienHienVat);
                model.FTienTuChi = (model.FTienTuChi ?? 0) + (item.FTienTuChi ?? 0);

                //Sau điều chinh
                model.FTienTuChiSauDieuChinh = (model.FTienTuChiTruocDieuChinh ?? 0) + (model.FTienTuChi ?? 0);
                // model.FTienHienVatSauDieuChinh = (model.FTienHienVatTruocDieuChinh ?? 0) + (model.FTienHienVat ?? 0);

                CalculateParent(model.IID_MLNS_Cha, item, dictByMlns);
            }
        }

        private void OnPrint(object param)
        {
            var divisionPrintType = (SocialInsuranceDivisionEstimatePrintType)((int)param);
            TongHopThuChiViewModel.ReportNameTypeValue = (int)divisionPrintType;
            TongHopThuChiViewModel.ReportTypeValue = divisionPrintType;
            TongHopThuChiViewModel.Init();
            var view = new TongHopThuChi
            {
                DataContext = TongHopThuChiViewModel
            };
            DialogHost.Show(view, SystemConstants.ROOT_DIALOG, null, null);
        }

        private void OnPrintForAgency(object obj)
        {
            try
            {
                var printType = (SocialInsuranceDivisionEstimatePrintType)((int)obj);
                if ((int)printType == (int)SocialInsuranceDivisionEstimatePrintType.TARGET_AGENCY)
                {
                    PrintPhanBoTuToanChiTheoDonViViewModel.Init();
                    var view = new PrintPhanBoTuToanChiTheoDonVi
                    {
                        DataContext = PrintPhanBoTuToanChiTheoDonViViewModel
                    };

                    DialogHost.Show(view, SystemConstants.ROOT_DIALOG, null, null);
                }
                if ((int)printType == (int)SocialInsuranceDivisionEstimatePrintType.DELIVER)
                {
                    PrintPhuLucGiaoDuToanDuToanChiViewModel.Init();
                    var view = new PrintPhuLucGiaoDuToanDuToanChi
                    {
                        DataContext = PrintPhuLucGiaoDuToanDuToanChiViewModel
                    };
                    DialogHost.Show(view, SystemConstants.ROOT_DIALOG, null, null);
                }
                if ((int)printType == (int)SocialInsuranceDivisionEstimatePrintType.DU_TOAN_CHITIET_KPQL)
                {
                    PrintChiTietDuToanChiKPQLViewModel.Init();
                    var view = new PrintChiTietDuToanChiKPQL
                    {
                        DataContext = PrintChiTietDuToanChiKPQLViewModel
                    };
                    DialogHost.Show(view, SystemConstants.ROOT_DIALOG, null, null);
                }
            }
            catch (Exception ex)
            {

                _logger.Error(ex.Message, ex);
            }
        }
    }
}
