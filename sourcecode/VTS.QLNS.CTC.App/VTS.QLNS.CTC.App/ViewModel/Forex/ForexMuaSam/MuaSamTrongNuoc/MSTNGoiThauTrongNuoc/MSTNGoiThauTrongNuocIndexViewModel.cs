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
using VTS.QLNS.CTC.App.View.Forex.ForexMuaSam.MuaSamTrongNuoc.MSTNGoiThauTrongNuoc;
using VTS.QLNS.CTC.Core.Service;
using VTS.QLNS.CTC.Utility;

namespace VTS.QLNS.CTC.App.ViewModel.Forex.ForexMuaSam.MuaSamTrongNuoc.MSTNGoiThauTrongNuoc
{
    public class MSTNGoiThauTrongNuocIndexViewModel : GridViewModelBase<NhDaGoiThauModel>
    {
        private readonly IMapper _mapper;
        private readonly ISessionService _sessionService;
        private readonly ILog _logger;
        private ICollectionView _itemsCollectionView;
        private readonly INhDaGoiThauService _nhDaGoiThauService;
        private readonly INsDonViService _nsDonViService;
        private readonly INhDmNhiemVuChiService _nhDmNhiemVuChiService;

        public override string Name => "Gói thầu trong nước";
        public override string Title => "Quản lý Gói thầu trong nước";
        public override string Description => "Danh sách Gói thầu trong nước";
        public override Type ContentType => typeof(MSTNGoiThauTrongNuocIndex);
        public bool IsLock => SelectedItem != null;
        public bool IsEditable => SelectedItem != null && SelectedItem.BActive == true && !SelectedItem.BIsKhoa;
        public int ILoai { get; set; }
        public int IThuocMenu { get; set; }
        public bool IsShowDuAn { get; set; }
        public MSTNGoiThauTrongNuocDialogViewModel ForexDomesticBiddingPackageDialogViewModel { get; }

        private NhDaGoiThauModel _itemsFilter;
        public NhDaGoiThauModel ItemsFilter
        {
            get => _itemsFilter;
            set => SetProperty(ref _itemsFilter, value);
        }
        private ObservableCollection<NhDmNhiemVuChiModel> _itemsChuongTrinh;
        public ObservableCollection<NhDmNhiemVuChiModel> ItemsChuongTrinh
        {
            get => _itemsChuongTrinh;
            set => SetProperty(ref _itemsChuongTrinh, value);
        }
        private NhDmNhiemVuChiModel _selectedChuongTrinh;
        public NhDmNhiemVuChiModel SelectedChuongTrinh
        {
            get => _selectedChuongTrinh;
            set => SetProperty(ref _selectedChuongTrinh, value);
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
                        LoadChuongTrinh();
                    }
                }
            }

        public RelayCommand SearchCommand { get; }

        public MSTNGoiThauTrongNuocIndexViewModel(
            IMapper mapper,
            ISessionService sessionService,
            ILog logger,
            INhDaGoiThauService nhDaGoiThauService,
            INhDmNhiemVuChiService nhDmNhiemVuChiService,
            MSTNGoiThauTrongNuocDialogViewModel forexDomesticBiddingPackageDialogViewModel,
            INsDonViService nsDonViService)
        {
            _mapper = mapper;
            _sessionService = sessionService;
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
            LoadDefault();
            LoadDonVi();
            LoadData();
            LoadChuongTrinh();
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
        private void LoadChuongTrinh()
        {
            try
            {
                var lstChuongTrinh = _nhDmNhiemVuChiService.FindAllFillter(SelectedDonVi != null ? SelectedDonVi.Id : Guid.Empty);
                if (lstChuongTrinh == null) return;
                ItemsChuongTrinh = _mapper.Map<ObservableCollection<NhDmNhiemVuChiModel>>(lstChuongTrinh);
                if (ItemsChuongTrinh.Count !=0)
                {
                    SelectedChuongTrinh = ItemsChuongTrinh.FirstOrDefault(x => x.Id == ItemsFilter.IIdKHTTNhiemVuChiId);;
                }
                OnPropertyChanged(nameof(ItemsChuongTrinh));
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
                    if (!string.IsNullOrEmpty(ItemsFilter.STenNhiemVuChi) && !string.IsNullOrEmpty(item.STenNhiemVuChi))
                    {
                        result &= item.STenNhiemVuChi.Contains(ItemsFilter.STenNhiemVuChi, StringComparison.OrdinalIgnoreCase);
                    }
                    if (!string.IsNullOrEmpty(ItemsFilter.STenChuongTrinh) && !string.IsNullOrEmpty(item.STenChuongTrinh))
                    {
                        result &= item.STenChuongTrinh.Contains(ItemsFilter.STenChuongTrinh, StringComparison.OrdinalIgnoreCase);
                    }
                    if (ItemsFilter.DNgayQuyetDinh != null)
                    {
                        result &= ItemsFilter.DNgayQuyetDinh.Value.ToString("dd/MM/yyyy").Equals(item.DNgayQuyetDinhString);
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
            ForexDomesticBiddingPackageDialogViewModel.IsShowDuAn = false;
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
            ForexDomesticBiddingPackageDialogViewModel.IsShowDuAn = false;
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
            ForexDomesticBiddingPackageDialogViewModel.IsShowDuAn = false;
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
    }
}
