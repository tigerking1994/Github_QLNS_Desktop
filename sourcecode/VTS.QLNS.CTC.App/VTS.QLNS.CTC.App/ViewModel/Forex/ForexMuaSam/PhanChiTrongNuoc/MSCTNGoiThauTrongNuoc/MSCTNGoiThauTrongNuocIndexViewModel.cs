using AutoMapper;
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
using VTS.QLNS.CTC.App.View.Forex.ForexMuaSam.PhanChiTrongNuoc.MSCTNGoiThauTrongNuoc;
using VTS.QLNS.CTC.Core.Service;
using VTS.QLNS.CTC.Utility;

namespace VTS.QLNS.CTC.App.ViewModel.Forex.ForexMuaSam.PhanChiTrongNuoc.MSCTNGoiThauTrongNuoc
{
    public class MSCTNGoiThauTrongNuocIndexViewModel : GridViewModelBase<NhDaGoiThauModel>
    {
        private readonly IMapper _mapper;
        private readonly ISessionService _sessionService;
        private readonly ILog _logger;
        private readonly INhDaDuAnService _duanService;
        private ICollectionView _itemsCollectionView;
        private readonly INhDaGoiThauService _nhDaGoiThauService;
        private readonly INsDonViService _nsDonViService;
        private readonly INhDmNhiemVuChiService _nhDmNhiemVuChiService;

        public override string Name => "Gói thầu trong nước";
        public override string Title => "Quản lý Gói thầu trong nước";
        public override string Description => "Danh sách Gói thầu trong nước";
        public override Type ContentType => typeof(MSCTNGoiThauTrongNuocIndex);
        public bool IsLock => SelectedItem != null;
        public bool IsEditable => SelectedItem != null && SelectedItem.BActive == true && !SelectedItem.BIsKhoa;
        public int ILoai { get; set; }
        public int IThuocMenu { get; set; }
        public bool IsShowDuAn { get; set; }
        public MSCTNGoiThauTrongNuocDialogViewModel ForexDomesticBiddingPackageDialogViewModel { get; }

        private NhDaGoiThauModel _itemsFilter;
        public NhDaGoiThauModel ItemsFilter
        {
            get => _itemsFilter;
            set => SetProperty(ref _itemsFilter, value);
        }
        //private ObservableCollection<NhDmNhiemVuChiModel> _itemsChuongTrinh;
        //public ObservableCollection<NhDmNhiemVuChiModel> ItemsChuongTrinh
        //{
        //    get => _itemsChuongTrinh;
        //    set => SetProperty(ref _itemsChuongTrinh, value);
        //}
        //private NhDmNhiemVuChiModel _selectedChuongTrinh;
        //public NhDmNhiemVuChiModel SelectedChuongTrinh
        //{
        //    get => _selectedChuongTrinh;
        //    set => SetProperty(ref _selectedChuongTrinh, value);
        //}
        private ComboboxItem _selectedDuAn;
        public ComboboxItem SelectedDuAn
        {
            get => _selectedDuAn;
            set => SetProperty(ref _selectedDuAn, value);
        }

        private ObservableCollection<DonViModel> _itemsDonVi;
        public ObservableCollection<DonViModel> ItemsDonVi
        {
            get => _itemsDonVi;
            set => SetProperty(ref _itemsDonVi, value);
        }

        private DonViModel _selectedDonVi;
        public DonViModel SelectedDonVi
        {
            get => _selectedDonVi;
            set
            {
                if (SetProperty(ref _selectedDonVi, value))
                {
                    LoadDuAn();
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

        private ObservableCollection<ComboboxItem> _itemsDuAn;
        public ObservableCollection<ComboboxItem> ItemsDuAn
        {
            get => _itemsDuAn;
            set => SetProperty(ref _itemsDuAn, value);
        }
        public RelayCommand SearchCommand { get; }

        public MSCTNGoiThauTrongNuocIndexViewModel(
            IMapper mapper,
            ISessionService sessionService,
            ILog logger,
            INhDaDuAnService duanService,
            INhDaGoiThauService nhDaGoiThauService,
            INhDmNhiemVuChiService nhDmNhiemVuChiService,
            MSCTNGoiThauTrongNuocDialogViewModel forexDomesticBiddingPackageDialogViewModel,
            INsDonViService nsDonViService)
        {
            _mapper = mapper;
            _sessionService = sessionService;
            _duanService = duanService;
            _logger = logger;
            _nhDaGoiThauService = nhDaGoiThauService;
            _nsDonViService = nsDonViService;
            _nhDmNhiemVuChiService = nhDmNhiemVuChiService;

            SearchCommand = new RelayCommand(obj => _itemsCollectionView.Refresh());
            ForexDomesticBiddingPackageDialogViewModel = forexDomesticBiddingPackageDialogViewModel;
            LockUnLockCommand = new RelayCommand(o => OnLockUnLock(), obj => IsLock);
            UpdateCommand = new RelayCommand(o => OnUpdate(), obj => IsEditable);
            DeleteCommand = new RelayCommand(o => OnDelete(), obj => IsEditable);
        }

        public override void Init()
        {
            base.Init();
            IThuocMenu = 3;
            LoadDefault();
            LoadDonVi();
            LoadDuAn();
            LoadData();
        }

        private void LoadDefault()
        {
            ItemsFilter = new NhDaGoiThauModel();
        }

        private void LoadDonVi()
        {
            int year = _sessionService.Current.YearOfWork;
            var data = _nsDonViService.FindByCondition(x => x.NamLamViec == year);
            _itemsDonVi = _mapper.Map<ObservableCollection<DonViModel>>(data);
            OnPropertyChanged(nameof(ItemsDonVi));
        }
        private void LoadDuAn()
        {
            try
            {
                var lstDuAn = _duanService.FindAll();
                if (lstDuAn == null) return;
                ItemsDuAn = new ObservableCollection<ComboboxItem>(lstDuAn.Select(n => new ComboboxItem() { ValueItem = n.Id.ToString(), DisplayItem = n.STenDuAn }));
                OnPropertyChanged(nameof(ItemsDuAn));
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }


        public override void LoadData(params object[] args)
        {
            try
            {
                BackgroundWorkerHelper.Run((s, e) =>
                {
                    IsLoading = true;
                    Items = new ObservableCollection<NhDaGoiThauModel>();
                    e.Result = _nhDaGoiThauService.GetAllGoiThauTrongNuoc(ILoai, IThuocMenu).OrderByDescending(x => x.DNgayTao);
                   // e.Result= _nhDaGoiThauService.GetAllGoiThauTrongNuocByILoai(ILoai, IThuocMenu).OrderByDescending(x => x.DNgayTao);                 

                }, (s, e) =>
                {
                    if (e.Error == null)
                    {
                        Items = _mapper.Map<ObservableCollection<NhDaGoiThauModel>>(e.Result);
                        if (Items != null && Items.Count > 0)
                        {
                            SelectedItem = Items.FirstOrDefault();
                        }
                        _itemsCollectionView = CollectionViewSource.GetDefaultView(Items);
                        _itemsCollectionView.Filter = Items_Filte;
                        LoadChuongTrinh();
                    }
                });
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        private bool Items_Filte(object obj)
        {
            if (obj is NhDaGoiThauModel item)
            {
                bool result = true;
                if (ItemsFilter != null)
                {
                    if (!string.IsNullOrEmpty(ItemsFilter.SSoQuyetDinh) && !string.IsNullOrEmpty(item.SSoQuyetDinh))
                    {
                        result &= item.SSoQuyetDinh.Contains(ItemsFilter.SSoQuyetDinh, StringComparison.OrdinalIgnoreCase);
                    }
                    if (!string.IsNullOrEmpty(ItemsFilter.STenGoiThau) && !string.IsNullOrEmpty(item.STenGoiThau))
                    {
                        result &= item.STenGoiThau.Contains(ItemsFilter.STenGoiThau, StringComparison.OrdinalIgnoreCase);
                    }
                    if (!string.IsNullOrEmpty(ItemsFilter.SMoTa) && !string.IsNullOrEmpty(item.SMoTa))
                    {
                        result &= item.SMoTa.Contains(ItemsFilter.SMoTa, StringComparison.OrdinalIgnoreCase);
                    }
                    if (SelectedDonVi!=null)
                    {
                        result &= item.IID_DonViQuanLyID == SelectedDonVi.Id;
                    }
                    if (SelectedDuAn != null)
                    {
                        result &= item.IIdDuAnId == Guid.Parse(SelectedDuAn.ValueItem);
                    }
                    if (!string.IsNullOrEmpty(ItemsFilter.STenChuongTrinh) && !string.IsNullOrEmpty(item.STenChuongTrinh))
                    {
                        result &= item.STenChuongTrinh.Contains(ItemsFilter.STenChuongTrinh, StringComparison.OrdinalIgnoreCase);
                    }
                    if (ItemsFilter.DNgayQuyetDinh != null)
                    {
                        result &= ItemsFilter.DNgayQuyetDinh.Value.ToString("dd/MM/yyyy").Equals(item.DNgayQuyetDinhString);
                    }
                    if (SelectedChuongTrinh != null)
                    {
                        result &= item.IIdKHTTNhiemVuChiId == SelectedChuongTrinh.Id;
                    }
                }
                return result;
            }
            return false;
        }

        protected override void OnUpdate()
        {
            ForexDomesticBiddingPackageDialogViewModel.Model = SelectedItem;
            ForexDomesticBiddingPackageDialogViewModel.BIsReadOnly = false;
            ForexDomesticBiddingPackageDialogViewModel.BIsReadOnlyButtonAdd = false;
            ForexDomesticBiddingPackageDialogViewModel.IsDetail = false;
            ForexDomesticBiddingPackageDialogViewModel.ILoai = ILoai;
            ForexDomesticBiddingPackageDialogViewModel.IThuocMenu = IThuocMenu;
            ForexDomesticBiddingPackageDialogViewModel.IsShowDuAn = true;
            ForexDomesticBiddingPackageDialogViewModel.SavedAction = obj => OnRefresh();
            ForexDomesticBiddingPackageDialogViewModel.Init();
            ForexDomesticBiddingPackageDialogViewModel.ShowDialog();
        } 
        protected override void OnAdd()
        {
            ForexDomesticBiddingPackageDialogViewModel.Model = new NhDaGoiThauModel();
            ForexDomesticBiddingPackageDialogViewModel.BIsReadOnly = false;
            ForexDomesticBiddingPackageDialogViewModel.BIsReadOnlyButtonAdd = false;
            ForexDomesticBiddingPackageDialogViewModel.IsDetail = false;
            ForexDomesticBiddingPackageDialogViewModel.ILoai = ILoai;
            ForexDomesticBiddingPackageDialogViewModel.IThuocMenu = IThuocMenu;
            ForexDomesticBiddingPackageDialogViewModel.IsShowDuAn = true;
            ForexDomesticBiddingPackageDialogViewModel.SavedAction = obj => OnRefresh();
            ForexDomesticBiddingPackageDialogViewModel.Init();
            ForexDomesticBiddingPackageDialogViewModel.ShowDialog();
        }

        protected override void OnSelectionDoubleClick(object obj)
        {
            ForexDomesticBiddingPackageDialogViewModel.Model = SelectedItem;
            ForexDomesticBiddingPackageDialogViewModel.BIsReadOnly = true;
            ForexDomesticBiddingPackageDialogViewModel.BIsReadOnlyButtonAdd = true;
            ForexDomesticBiddingPackageDialogViewModel.IsDetail = false;
            ForexDomesticBiddingPackageDialogViewModel.ILoai = ILoai;
            ForexDomesticBiddingPackageDialogViewModel.IThuocMenu = IThuocMenu;
            ForexDomesticBiddingPackageDialogViewModel.IsShowDuAn = true;
            ForexDomesticBiddingPackageDialogViewModel.Init();
            ForexDomesticBiddingPackageDialogViewModel.ShowDialog();
        }

        protected override void OnDelete()
        {
            if (SelectedItem != null)
            {
                DialogResult dialogResult = System.Windows.Forms.MessageBox.Show(string.Format(Resources.MsgOnDeleteGoiThau), Resources.ConfirmTitle, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dialogResult == DialogResult.Yes)
                {
                    _nhDaGoiThauService.Delete(SelectedItem.Id);
                    OnRefresh();
                }
            }
        }

        protected override void OnLockUnLock()
        {
            string message = IsLock ? Resources.UnlockChungTu : Resources.LockChungTu;
            var result = System.Windows.MessageBox.Show(message, Resources.ConfirmTitle, MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                _nhDaGoiThauService.LockOrUnlock(SelectedItem.Id, !SelectedItem.BIsKhoa);
                OnRefresh();
            }
        }

        protected override void OnRefresh()
        {
            LoadData();
        }

        private void LoadChuongTrinh()
        {
            try
            {
                if (Items == null) return;
                ItemsChuongTrinh = new ObservableCollection<ComboboxItem>(
                                   Items.GroupBy(g => g.IIdKHTTNhiemVuChiId)
                                        .Select(n => new ComboboxItem()
                                        { ValueItem = n.First().Id.ToString(), Id = n.First().IIdKHTTNhiemVuChiId != null ? n.First().IIdKHTTNhiemVuChiId.Value : Guid.Empty, DisplayItem = n.First().STenChuongTrinh }));

                OnPropertyChanged(nameof(ItemsChuongTrinh));
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }
    }
}

