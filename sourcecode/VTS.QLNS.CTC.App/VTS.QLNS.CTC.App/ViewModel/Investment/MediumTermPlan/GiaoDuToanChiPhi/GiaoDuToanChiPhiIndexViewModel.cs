using AutoMapper;
using MaterialDesignThemes.Wpf;
using log4net;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows;
using System.Windows.Data;
using System.Windows.Forms;
using VTS.QLNS.CTC.App.Command;
using VTS.QLNS.CTC.App.Helper;
using VTS.QLNS.CTC.App.Model;
using VTS.QLNS.CTC.App.Model.Control;
using VTS.QLNS.CTC.App.Properties;
using VTS.QLNS.CTC.App.Service;
using VTS.QLNS.CTC.App.View.Investment.MediumTermPlan.GiaoDuToanChiPhi;
using VTS.QLNS.CTC.App.ViewModel.Shared;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Service;
using VTS.QLNS.CTC.Utility;
using VTS.QLNS.CTC.Utility.Enum;

namespace VTS.QLNS.CTC.App.ViewModel.Investment.MediumTermPlan.GiaoDuToanChiPhi
{
    public class GiaoDuToanChiPhiIndexViewModel : GridViewModelBase<VdtKhvPhanBoVonChiPhiModel>
    {
        private static string[] lstDonViExclude = new string[] { "0", "1" };
        private readonly ILog _logger;
        private readonly IMapper _mapper;
        private readonly ISessionService _sessionService;
        private readonly INsDonViService _nsDonViService;
        private readonly IVdtDaChuTruongDauTuService _vdtDaChuTruongDauTuService;
        private readonly IVdtKhvPhanBoVonChiPhiService _vdtKhvPhanBoVonChiPhiService;
        private readonly IApproveProjectService _approveProjectService;
        private readonly IProjectManagerService _projectManagerService;
        private readonly INsNguonNganSachService _nsNguonNganSachService;
        private readonly IAttachmentService _iAttachmentService;
        private ICollectionView _itemsCollectionView;

        public override string FuncCode => NSFunctionCode.INVESTMENT_STANDARD_GIAO_DU_TOAN_CHI_PHI_INDEX;
        //public override string GroupName => MenuItemContants.GROUP_CAPITAL_PLAN_OF_YEAR;
        public override string GroupName => MenuItemContants.GROUP_PROJECT_MANAGEMENT;
        //public override string Name => "Quản lý giao dự toán chi phí";
        public override string Name => "Giao dự toán chi phí";
        public override string Title => "Giao dự toán chi phí";
        public override string Description => "Danh sách giao dự toán chi phí";
        public override Type ContentType => typeof(GiaoDuToanChiPhiIndex);
        public bool IsEditable => SelectedItem != null && SelectedItem.BActive && !SelectedItem.BKhoa;
        public bool IsLock => SelectedItem != null && SelectedItem.BKhoa;

        private string _iNamKeHoach;
        public string iNamKeHoach
        {
            get => _iNamKeHoach;
            set => SetProperty(ref _iNamKeHoach, value);
        }
        private DateTime? _dNgayQuyetDinh;
        public DateTime? DNgayQuyetDinh
        {
            get => _dNgayQuyetDinh;
            set
            {
                SetProperty(ref _dNgayQuyetDinh, value);
            }
        }
        private string _sSoQuyetDinh;
        public string SSoQuyetDinh
        {
            get => _sSoQuyetDinh;
            set => SetProperty(ref _sSoQuyetDinh, value);
        }
        private ObservableCollection<ComboboxItem> _drpDonViQuanLy;
        public ObservableCollection<ComboboxItem> DrpDonViQuanLy
        {
            get => _drpDonViQuanLy;
            set => SetProperty(ref _drpDonViQuanLy, value);
        }
        private ComboboxItem _drpDonViQuanLySelected;
        public ComboboxItem DrpDonViQuanLySelected
        {
            get => _drpDonViQuanLySelected;
            set
            {
                SetProperty(ref _drpDonViQuanLySelected, value);
            }
        }
        private ObservableCollection<ComboboxItem> _drpNguonVon;
        public ObservableCollection<ComboboxItem> DrpNguonVon
        {
            get => _drpNguonVon;
            set => SetProperty(ref _drpNguonVon, value);
        }
        private ComboboxItem _drpNguonVonSelected;
        public ComboboxItem DrpNguonVonSelected
        {
            get => _drpNguonVonSelected;
            set
            {
                SetProperty(ref _drpNguonVonSelected, value);
            }
        }
        private ObservableCollection<CheckBoxItem> _itemsDonVi;
        public ObservableCollection<CheckBoxItem> ItemsDonVi
        {
            get => _itemsDonVi;
            set => SetProperty(ref _itemsDonVi, value);
        }
        private string _selectedDonVi;
        public string SelectedDonVi
        {
            get => _selectedDonVi;
            set
            {
                SetProperty(ref _selectedDonVi, value);
            }
        }

        public RelayCommand DieuChinhCommand { get; }
        public RelayCommand SearchCommand { get; }
        public RelayCommand RemoveFilterCommand { get; }
        public RelayCommand ViewAttachmentCommand { get; }
        public AttachmentViewModel AttachmentViewModel { get; set; }
        public GiaoDuToanChiPhiDialogViewModel GiaoDuToanChiPhiDialogViewModel { get; set; }
        public GiaoDuToanChiPhiDetailViewModel GiaoDuToanChiPhiDetailViewModel { get; set; }

        public GiaoDuToanChiPhiIndexViewModel(
            GiaoDuToanChiPhiDialogViewModel giaoDuToanChiPhiDialogViewModel,
            GiaoDuToanChiPhiDetailViewModel giaoDuToanChiPhiDetailViewModel,
            ILog logger,
            IMapper mapper,
            ISessionService sessionService,
            INsDonViService nsDonViService,
            IVdtDaChuTruongDauTuService vdtDaChuTruongDauTuService,
            IVdtKhvPhanBoVonChiPhiService vdtKhvPhanBoVonChiPhiService,
            IApproveProjectService approveProjectService,
            IProjectManagerService projectManagerService,
            IAttachmentService iAttachmentService,
            INsNguonNganSachService nsNguonNganSachService,
            AttachmentViewModel attachmentViewModel)
        {
            _logger = logger;
            _mapper = mapper;
            _sessionService = sessionService;
            _nsDonViService = nsDonViService;
            GiaoDuToanChiPhiDialogViewModel = giaoDuToanChiPhiDialogViewModel;
            GiaoDuToanChiPhiDialogViewModel.ParentPage = this;
            GiaoDuToanChiPhiDetailViewModel = giaoDuToanChiPhiDetailViewModel;
            GiaoDuToanChiPhiDetailViewModel.ParentPage = this;
            _vdtDaChuTruongDauTuService = vdtDaChuTruongDauTuService;
            _vdtKhvPhanBoVonChiPhiService = vdtKhvPhanBoVonChiPhiService;
            _approveProjectService = approveProjectService;
            _projectManagerService = projectManagerService;
            _iAttachmentService = iAttachmentService;
            _nsNguonNganSachService = nsNguonNganSachService;

            SearchCommand = new RelayCommand(obj => _itemsCollectionView.Refresh());
            RemoveFilterCommand = new RelayCommand(obj => OnRemoveFilter());
            UpdateCommand = new RelayCommand(o => OnUpdate(), obj => IsEditable);
            DieuChinhCommand = new RelayCommand(o => OnDieuChinh(), obj => IsEditable);

            AttachmentViewModel = attachmentViewModel;
        }

        public override void Init()
        {
            GetDonViQuanLy();
            GetNguonVon();
            LoadData();
        }

        private void LoadData()
        {
            BackgroundWorkerHelper.Run((s, e) =>
            {
                IsLoading = true;

                // Main process
                Items = new ObservableCollection<VdtKhvPhanBoVonChiPhiModel>();
                e.Result = _vdtKhvPhanBoVonChiPhiService.FindGiaoDuToanIndex().OrderByDescending(n => n.DNgayQuyetDinh);
            }, (s, e) =>
            {
                if (e.Error == null)
                {
                    Items = _mapper.Map<ObservableCollection<Model.VdtKhvPhanBoVonChiPhiModel>>(e.Result);
                    // Process when run completed. e.Result
                    if (Items != null && Items.Count > 0)
                    {
                        SelectedItem = Items.FirstOrDefault();
                    }
                    _itemsCollectionView = CollectionViewSource.GetDefaultView(Items);
                    _itemsCollectionView.Filter = Items_Filter;
                }
                else
                {
                    _logger.Error(e.Error.Message);
                }
                IsLoading = false;
            });
        }

        private bool Items_Filter(object obj)
        {
            try
            {
                if (!(obj is VdtKhvPhanBoVonChiPhiModel temp)) return true;
                var bCondition = true;
                int iNamKeHoachParse = 0;
                if (!string.IsNullOrEmpty(iNamKeHoach) && int.TryParse(iNamKeHoach, out iNamKeHoachParse))
                {
                    bCondition &= (temp.INamKeHoach.HasValue && temp.INamKeHoach.Value == iNamKeHoachParse);
                }
                if (DNgayQuyetDinh.HasValue)
                {
                    bCondition &= (temp.DNgayQuyetDinh.HasValue && temp.DNgayQuyetDinh.ToShortDateString() == DNgayQuyetDinh.Value.ToShortDateString());
                }
                if (!string.IsNullOrEmpty(_sSoQuyetDinh))
                {
                    bCondition &= !string.IsNullOrEmpty(temp.SSoQuyetDinh) && (temp.SSoQuyetDinh.ToLower().Contains(_sSoQuyetDinh.ToLower()));
                }
                if (DrpDonViQuanLySelected != null)
                {
                    bCondition &= !string.IsNullOrEmpty(temp.IIdMaDonVi) && (temp.IIdMaDonVi == DrpDonViQuanLySelected.ValueItem);
                }
                if (_drpNguonVonSelected != null)
                {
                    bCondition &= (temp.IIdLoaiNguonVonId == Int32.Parse(_drpNguonVonSelected.ValueItem));
                }
                return bCondition;
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
                return true;
            }
        }

        private void GetNguonVon()
        {
            List<NsNguonNganSach> lstNguonNganSach = _nsNguonNganSachService.FindNguonNganSach().ToList();
            _drpNguonVon = _mapper.Map<ObservableCollection<ComboboxItem>>(lstNguonNganSach);
        }
        private void GetDonViQuanLy()
        {
            try
            {
                var cbxLoaiDonViData = _nsDonViService.FindByNamLamViec(_sessionService.Current.YearOfWork)
                .Where(n => lstDonViExclude.Contains(n.Loai)).ToList();

                _drpDonViQuanLy = _mapper.Map<ObservableCollection<ComboboxItem>>(cbxLoaiDonViData);
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        protected override void OnDelete()
        {
            if (SelectedItem == null || !IsEditable)
            {
                return;
            }
            if (SelectedItem.BKhoa)
            {
                return;
            }

            string msgConfirm = string.Format(Resources.ConfirmDeleteUsers);
            if (MessageBoxHelper.Confirm(msgConfirm) == MessageBoxResult.Yes)
            {
                // Nếu là xóa bản ghi điều chỉnh thì bản ghi gốc sẽ được update bactive = 1
                if (SelectedItem.IIdParentId.HasValue)
                {
                    VdtKhvPhanBoVonChiPhi chiphiParent = _vdtKhvPhanBoVonChiPhiService.FindById(SelectedItem.IIdParentId.Value);
                    if (chiphiParent != null)
                    {
                        chiphiParent.BActive = true;
                        _vdtKhvPhanBoVonChiPhiService.Update(chiphiParent);
                    }
                }
                _vdtKhvPhanBoVonChiPhiService.Delete(_mapper.Map<VdtKhvPhanBoVonChiPhi>(SelectedItem));
                OnRefresh();
            }
        }

        public bool CheckChuTruongExistQDDauTu(Guid duanId, Guid iIdChuTruongId)
        {
            VdtDaQddauTu qdDauTu = _approveProjectService.FindByDuAnId(duanId);
            if (qdDauTu != null && qdDauTu.IIdChuTruongDauTuId == iIdChuTruongId)
            {
                return true;
            }
            return false;
        }


        protected override void OnAdd()
        {
            GiaoDuToanChiPhiDialogViewModel.IsDieuChinh = false;
            GiaoDuToanChiPhiDialogViewModel.IsInsert = true;
            GiaoDuToanChiPhiDialogViewModel.Model = new VdtKhvPhanBoVonChiPhiModel();
            GiaoDuToanChiPhiDialogViewModel.Init();
            GiaoDuToanChiPhiDialogViewModel.SavedAction = obj =>
            {
                this.OnRefresh();
                OnOpenGiaoDuToanChiPhiDetailViewModel(_mapper.Map<VdtKhvPhanBoVonChiPhiModel>(obj));
            };
            var view = new GiaoDuToanChiPhiDialog
            {
                DataContext = GiaoDuToanChiPhiDialogViewModel
            };
            DialogHost.Show(view, "RootDialog");
        }

        private void OnOpenGiaoDuToanChiPhiDetailViewModel(VdtKhvPhanBoVonChiPhiModel SelectedItem, bool bIsDetail = false)
        {
            GiaoDuToanChiPhiDetailViewModel.BIsDetail = bIsDetail;
            GiaoDuToanChiPhiDetailViewModel.Model = SelectedItem;
            GiaoDuToanChiPhiDetailViewModel.Init();
            var view = new GiaoDuToanChiPhiDetail { DataContext = GiaoDuToanChiPhiDetailViewModel };
            view.ShowDialog();
            LoadData();
        }

        protected override void OnUpdate()
        {
            GiaoDuToanChiPhiDialogViewModel.IsDieuChinh = false;
            GiaoDuToanChiPhiDialogViewModel.IsInsert = false;
            GiaoDuToanChiPhiDialogViewModel.Model = SelectedItem;
            GiaoDuToanChiPhiDialogViewModel.Init();
            GiaoDuToanChiPhiDialogViewModel.SavedAction = obj =>
            {
                this.OnRefresh();
                OnOpenGiaoDuToanChiPhiDetailViewModel(_mapper.Map<VdtKhvPhanBoVonChiPhiModel>(obj));
            };
            var view = new GiaoDuToanChiPhiDialog
            {
                DataContext = GiaoDuToanChiPhiDialogViewModel
            };
            DialogHost.Show(view, "RootDialog");
        }

        private void OnDieuChinh()
        {
            if (SelectedItem != null)
            {
                if (SelectedItem.BKhoa)
                {
                    return;
                }
                GiaoDuToanChiPhiDialogViewModel.IsInsert = false;
                GiaoDuToanChiPhiDialogViewModel.IsDieuChinh = true;
                GiaoDuToanChiPhiDialogViewModel.Model = SelectedItem;
                GiaoDuToanChiPhiDialogViewModel.Init();
                GiaoDuToanChiPhiDialogViewModel.SavedAction = obj =>
                {
                    this.OnRefresh();
                    OnOpenGiaoDuToanChiPhiDetailViewModel(_mapper.Map<VdtKhvPhanBoVonChiPhiModel>(obj));
                };
                var view = new GiaoDuToanChiPhiDialog
                {
                    DataContext = GiaoDuToanChiPhiDialogViewModel
                };
                DialogHost.Show(view, "RootDialog");
            }
        }

        private bool CheckDuAnQuyetToanHoanThanh()
        {
            bool isQuyetToan = _projectManagerService.CheckDuAnQuyetToanHoanThanh(SelectedItem.IIdDuAnId.Value);
            return isQuyetToan;
        }

        protected override void OnRefresh()
        {
            LoadData();
        }

        private void OnRemoveFilter()
        {
            iNamKeHoach = null;
            DNgayQuyetDinh = null;
            DrpDonViQuanLySelected = null;
            DrpNguonVonSelected = null;
            SSoQuyetDinh = string.Empty;

            OnPropertyChanged(nameof(iNamKeHoach));
            OnPropertyChanged(nameof(DNgayQuyetDinh));
            OnPropertyChanged(nameof(DrpDonViQuanLySelected));
            OnPropertyChanged(nameof(DrpNguonVonSelected));
            _itemsCollectionView.Refresh();
        }

        protected override void OnSelectionDoubleClick(object obj)
        {
            base.OnSelectionDoubleClick(obj);
            OnOpenGiaoDuToanChiPhiDetailViewModel(_mapper.Map<VdtKhvPhanBoVonChiPhiModel>(obj), true);
        }

        private void OnViewAttachment(object obj)
        {
            if (base.SelectedItem != null)
            {
                AttachmentViewModel.ModuleType = AttachmentEnum.Type.VDT_CHUTRUONG_DAUTU;
                AttachmentViewModel.ObjectId = base.SelectedItem.Id;
                AttachmentViewModel.Init();
                AttachmentViewModel.ShowDialogHost();
            }
        }

        protected override void OnSelectedItemChanged()
        {
            base.OnSelectedItemChanged();
            OnPropertyChanged(nameof(IsLock));
            OnPropertyChanged(nameof(IsEditable));
        }

        public override void Dispose()
        {
            base.Dispose();

            // Clear data
            _itemsCollectionView = null;
            Items = new ObservableCollection<VdtKhvPhanBoVonChiPhiModel>();
            ItemsDonVi = new ObservableCollection<CheckBoxItem>();
        }

        protected override void OnLockUnLock()
        {
            try
            {
                string message = IsLock ? Resources.UnlockChungTu : Resources.LockChungTu;
                if (MessageBoxHelper.Confirm(message) == MessageBoxResult.Yes)
                    LockConfirmEventHandler();
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        private void LockConfirmEventHandler()
        {
            _vdtKhvPhanBoVonChiPhiService.LogItem(SelectedItem.Id, _sessionService.Current.Principal);
            SelectedItem.BKhoa = !SelectedItem.BKhoa;
            OnPropertyChanged(nameof(IsLock));
            OnPropertyChanged(nameof(IsEditable));
            OnPropertyChanged(nameof(Items));
        }
    }
}
