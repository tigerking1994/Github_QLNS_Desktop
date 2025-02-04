using AutoMapper;
using MaterialDesignThemes.Wpf;
using log4net;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Forms;
using VTS.QLNS.CTC.App.Command;
using VTS.QLNS.CTC.App.Helper;
using VTS.QLNS.CTC.App.Model;
using VTS.QLNS.CTC.App.Model.Control;
using VTS.QLNS.CTC.App.Properties;
using VTS.QLNS.CTC.App.Service;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Service;
using VTS.QLNS.CTC.Utility;
using VTS.QLNS.CTC.Utility.Enum;

namespace VTS.QLNS.CTC.App.ViewModel.Forex.ForexMuaSam.MuaSamTrongNuoc.MSTNKeHoachDatHang
{
    public class MSTNKeHoachDatHangIndexViewModel : GridViewModelBase<NhMstnKeHoachDatHangModel>
    {
        #region Private
        private readonly INhMstnKeHoachDatHangService _service;
        private readonly INsDonViService _dvService;
        private readonly ISessionService _sessionService;
        private readonly INhDmNhiemVuChiService _nhDmNhiemVuChiService;
        private readonly INhDaHopDongService _nhDaHopDongService;
        private readonly IMapper _mapper;
        private readonly ILog _logger;
        private ICollectionView _khDatHangView;
        #endregion

        #region Public
        public override string Name => "Kế hoạch đặt hàng";
        public override string Title => "Quản lý Kế hoạch đặt hàng được duyệt";
        public override string Description => "Danh sách Kế hoạch đặt hàng được duyệt";
        public override Type ContentType => typeof(View.Forex.ForexMuaSam.MuaSamTrongNuoc.MSTNKeHoachDatHang.MSTNKeHoachDatHangIndex);
        public bool IsEdit => SelectedItem != null && SelectedItem.BIsActive;
        public bool IsEnableLock => SelectedItem != null;
        //public bool IsLock => SelectedItem != null && SelectedItem.BIsKhoa;
        public int IThuocMenu { get; set; }
        public int ILoai { get; set; }
        public bool IsShowDuAn { get; set; }
        public bool IsEditable => SelectedItem != null && SelectedItem.BIsActive;
        #endregion

        #region Items
        private string _sSoQuyetDinh;
        public string SSoQuyetDinh
        {
            get => _sSoQuyetDinh;
            set => SetProperty(ref _sSoQuyetDinh, value);
        }
        private NhMstnKeHoachDatHangModel _itemsFilter;
        public NhMstnKeHoachDatHangModel ItemsFilter
        {
            get => _itemsFilter;
            set => SetProperty(ref _itemsFilter, value);
        }
        private string _sTenChuongTrinh;
        public string STenChuongTrinh
        {
            get => _sTenChuongTrinh;
            set => SetProperty(ref _sTenChuongTrinh, value);
        }

        private DateTime? _dNgayQuyetDinhTu;
        public DateTime? DNgayQuyetDinhTu
        {
            get => _dNgayQuyetDinhTu;
            set => SetProperty(ref _dNgayQuyetDinhTu, value);
        }

        private DateTime? _dNgayQuyetDinhDen;
        public DateTime? DNgayQuyetDinhDen
        {
            get => _dNgayQuyetDinhDen;
            set => SetProperty(ref _dNgayQuyetDinhDen, value);
        }

        private string _sMoTa;
        public string SMoTa
        {
            get => _sMoTa;
            set => SetProperty(ref _sMoTa, value);
        }

        private ObservableCollection<ComboboxItem> _itemsDonVi;
        public ObservableCollection<ComboboxItem> ItemsDonVi
        {
            get => _itemsDonVi;
            set => SetProperty(ref _itemsDonVi, value);
        }

        private ComboboxItem _selectedDonVi;
        public ComboboxItem SelectedDonVi
        {
            get => _selectedDonVi;
            set
            {
                if (SetProperty(ref _selectedDonVi, value))
                {
                    LoadChuongTrinh();
                }
            }
        }

        private ObservableCollection<ComboboxItem> _itemsChuongTrinh;
        public ObservableCollection<ComboboxItem> ItemsChuongTrinh
        {
            get => _itemsChuongTrinh;
            set => SetProperty(ref _itemsChuongTrinh, value);
        }

        private ComboboxItem _selectedChuongTrinh;
        public ComboboxItem SelectedChuongTrinh
        {
            get => _selectedChuongTrinh;
            set => SetProperty(ref _selectedChuongTrinh, value);
        }
        #endregion

        #region declare RelayCommand
        public RelayCommand SearchCommand { get; }
        public RelayCommand ResetFilterCommand { get; }
        public RelayCommand ViewAttachmentCommand { get; }
        #endregion

        public MSTNKeHoachDatHangDialogViewModel NHKeHoachDatHangDialogViewModel { get; }

        public MSTNKeHoachDatHangIndexViewModel(
            MSTNKeHoachDatHangDialogViewModel nHKeHoachDatHangDialogViewModel,
            INhMstnKeHoachDatHangService service,
            INhDmNhiemVuChiService nhDmNhiemVuChiService,
            INsDonViService dvService,
            INhDaHopDongService nhDaHopDongService,
            ISessionService sessionService,
            IMapper mapper,
            ILog logger)
        {
            NHKeHoachDatHangDialogViewModel = nHKeHoachDatHangDialogViewModel;
            NHKeHoachDatHangDialogViewModel.ParentPage = this;

            _service = service;
            _nhDmNhiemVuChiService = nhDmNhiemVuChiService;
            _dvService = dvService;
            _nhDaHopDongService = nhDaHopDongService;
            _sessionService = sessionService;
            _mapper = mapper;
            _logger = logger;

            SearchCommand = new RelayCommand(obj => OnSearch());
            ResetFilterCommand = new RelayCommand(obj => onResetFilter());

            UpdateCommand = new RelayCommand(o => OnUpdate(), obj => IsEditable);
            DieuChinhCommand = new RelayCommand(o => OnDieuChinh(), obj => IsEditable);
            DeleteCommand = new RelayCommand(o => OnDelete(), obj => SelectedItem != null && IsEditable && CheckDelete());
        }

        #region Event
        public override void Init()
        {
            ItemsFilter = new NhMstnKeHoachDatHangModel();
            MarginRequirement = new Thickness(10);
            LoadDonViQuanLy();
            LoadChuongTrinh();
            LoadData();
        }

        public override void LoadData(params object[] args)
        {
            try
            {
                BackgroundWorkerHelper.Run((s, e) =>
                {
                    IsLoading = true;

                    // Main process
                    Items = new ObservableCollection<NhMstnKeHoachDatHangModel>();
                    e.Result = _service.GetAllMstnKeHoachDatHangIndex();
                }, (s, e) =>
                {
                    if (e.Error == null)
                    {
                        Items = _mapper.Map<ObservableCollection<NhMstnKeHoachDatHangModel>>(e.Result);
                        if (Items != null && Items.Count > 0)
                        {
                            SelectedItem = Items.FirstOrDefault();
                        }
                        _khDatHangView = CollectionViewSource.GetDefaultView(Items);
                        _khDatHangView.Filter = OnFilter;
                    }
                    else
                    {
                        _logger.Error(e.Error.Message);
                    }
                    IsLoading = false;
                });
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        protected override void OnAdd()
        {
            NHKeHoachDatHangDialogViewModel.Model = new NhMstnKeHoachDatHangModel();
            NHKeHoachDatHangDialogViewModel.BIsReadOnly = false;
            NHKeHoachDatHangDialogViewModel.IsDieuChinh = false;
            NHKeHoachDatHangDialogViewModel.Init();
            NHKeHoachDatHangDialogViewModel.SavedAction = obj => OnRefresh();
            NHKeHoachDatHangDialogViewModel.ShowDialog();
        }

        protected override void OnUpdate()
        {
            NHKeHoachDatHangDialogViewModel.Model = SelectedItem;
            NHKeHoachDatHangDialogViewModel.BIsReadOnly = false;
            NHKeHoachDatHangDialogViewModel.IsDieuChinh = false;
            NHKeHoachDatHangDialogViewModel.Init();
            NHKeHoachDatHangDialogViewModel.SavedAction = obj => OnRefresh();
            NHKeHoachDatHangDialogViewModel.ShowDialog();
        }
        protected override void OnDieuChinh()
        {
            NHKeHoachDatHangDialogViewModel.Model = SelectedItem;
            NHKeHoachDatHangDialogViewModel.BIsReadOnly = false;
            NHKeHoachDatHangDialogViewModel.IsDieuChinh = true;
            NHKeHoachDatHangDialogViewModel.Init();
            NHKeHoachDatHangDialogViewModel.SavedAction = obj => OnRefresh();
            NHKeHoachDatHangDialogViewModel.ShowDialog();
        }

        protected override void OnSelectionDoubleClick(object obj)
        {
            NHKeHoachDatHangDialogViewModel.Model = SelectedItem;
            NHKeHoachDatHangDialogViewModel.BIsReadOnly = true;
            NHKeHoachDatHangDialogViewModel.IsDieuChinh = false;
            NHKeHoachDatHangDialogViewModel.Init();
            NHKeHoachDatHangDialogViewModel.SavedAction = obj => OnRefresh();
            NHKeHoachDatHangDialogViewModel.ShowDialog();
        }

        protected override void OnDelete()
        {
            string msgConfirm = string.Format(Resources.ConfirmDeleteUsers);
            if (MessageBoxHelper.Confirm(msgConfirm) == MessageBoxResult.Yes)
            {
                _service.Delete(SelectedItem.Id);
                OnRefresh();
            }
        }

        protected override void OnRefresh()
        {
            base.OnRefresh();
            LoadData();
        }

        public void OnSearch()
        {
            _khDatHangView.Refresh();
        }

        private void onResetFilter()
        {
            SSoQuyetDinh = null;
            DNgayQuyetDinhTu = null;
            DNgayQuyetDinhDen = null;
            SMoTa = null;
            SelectedDonVi = null;
            SelectedChuongTrinh = null;
            STenChuongTrinh = null;
            OnPropertyChanged(nameof(SSoQuyetDinh));
            OnPropertyChanged(nameof(DNgayQuyetDinhTu));
            OnPropertyChanged(nameof(DNgayQuyetDinhDen));
            OnPropertyChanged(nameof(SelectedDonVi));
            OnPropertyChanged(nameof(SelectedChuongTrinh));
            OnSearch();
        }

        protected override void OnSelectedItemChanged()
        {
            base.OnSelectedItemChanged();
            OnPropertyChanged(nameof(IsEnableLock));
            OnPropertyChanged(nameof(IsEdit));
        }
        #endregion

        #region Helper
        private bool OnFilter(object obj)
        {
            if (!(obj is NhMstnKeHoachDatHangModel item)) return true;
            var bCondition = true;
            if (!string.IsNullOrEmpty(SSoQuyetDinh))
            {
                if (!string.IsNullOrEmpty(item.SSoQuyetDinh))
                {
                    bCondition &= item.SSoQuyetDinh.Contains(SSoQuyetDinh, StringComparison.OrdinalIgnoreCase);
                }
                else bCondition = false;
            }
            if (SelectedDonVi != null)
            {
                bCondition &= item.IIdDonViQuanLy == SelectedDonVi.Id;
            }
            if (SelectedChuongTrinh != null)
            {
                bCondition &= item.IIdKHTTNhiemVuChiId == SelectedChuongTrinh.Id;
            }
            if (DNgayQuyetDinhTu != null)
            {
                bCondition &= DNgayQuyetDinhTu <= item.DNgayQuyetDinh;
            }
            if (DNgayQuyetDinhDen != null)
            {
                bCondition &= DNgayQuyetDinhDen >= item.DNgayQuyetDinh;
            }
            return bCondition;
        }
        private void LoadDonViQuanLy()
        {
            try
            {
                var lstDonVi = _dvService.FindByNamLamViec(_sessionService.Current.YearOfWork);
                if (lstDonVi == null) return;
                ItemsDonVi = new ObservableCollection<ComboboxItem>(lstDonVi.Select(n => new ComboboxItem() { ValueItem = n.IIDMaDonVi, Id = n.Id, DisplayItem = n.TenDonVi }));
                OnPropertyChanged(nameof(ItemsDonVi));
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        private void LoadChuongTrinh()
        {
            try
            {
                var lstChuongTrinh = _nhDmNhiemVuChiService.FindAllFillter(SelectedDonVi != null ? SelectedDonVi.Id : Guid.Empty);
                if (lstChuongTrinh == null) return;
                ItemsChuongTrinh = new ObservableCollection<ComboboxItem>(lstChuongTrinh.Select(n => new ComboboxItem() { ValueItem = n.Id.ToString(), Id = n.IIdKHTTNhiemVuChiId != null ? n.IIdKHTTNhiemVuChiId.Value : Guid.Empty, DisplayItem = n.STenNhiemVuChi }));

                OnPropertyChanged(nameof(ItemsChuongTrinh));
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        private bool CheckDelete()
        {
            var itemDAHopDong = _nhDaHopDongService.FindByCondition(x => x.IIdKeHoachDatHangId == SelectedItem.Id).ToList();
            return itemDAHopDong == null || itemDAHopDong.Count == 0;
        }
        #endregion
    }
}
