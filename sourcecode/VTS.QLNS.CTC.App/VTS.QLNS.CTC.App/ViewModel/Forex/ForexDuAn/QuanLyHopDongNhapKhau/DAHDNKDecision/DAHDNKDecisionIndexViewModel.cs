using AutoMapper;
using MaterialDesignThemes.Wpf;
using log4net;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Data;
using VTS.QLNS.CTC.App.Command;
using VTS.QLNS.CTC.App.Model;
using VTS.QLNS.CTC.App.Service;
using VTS.QLNS.CTC.Core.Service;
using VTS.QLNS.CTC.Utility;
using VTS.QLNS.CTC.Utility.Enum;
using VTS.QLNS.CTC.Core.Domain;
using System.Collections.ObjectModel;
using VTS.QLNS.CTC.App.Model.Control;
using System.Windows;
using VTS.QLNS.CTC.App.Properties;
using System.Windows.Forms;
using VTS.QLNS.CTC.App.Helper;
using VTS.QLNS.CTC.App.View.Forex.ForexDuAn.QuanLyHopDongNhapKhau.DAHDNKDecision;

namespace VTS.QLNS.CTC.App.ViewModel.Forex.ForexDuAn.QuanLyHopDongNhapKhau.DAHDNKDecision
{
    public class DAHDNKDecisionIndexViewModel : GridViewModelBase<NhQuyetDinhDamPhamModel>
    {
        private readonly ILog _logger;
        private readonly IMapper _mapper;
        private readonly ISessionService _sessionService;
        private readonly INhHdnkCacQuyetDinhService _service;
        private readonly INsDonViService _nsDonViService;
        private readonly INhDaGoiThauService _nhDaGoiThauService;
        private ICollectionView _itemsCollectionView;

        public override string Name => "Quyết định chi tiết";
        public override string Title => "Quyết định chi tiết";
        public override string Description => "Danh sách quyết định chi tiết";
        public override Type ContentType => typeof(DAHDNKDecisionIndex);
        public override PackIconKind IconKind => PackIconKind.BagChecked;
        public override string FuncCode => NSFunctionCode.FOREX_DECISION_APPROVING_NEGOTIATION_RESULTS;
        public bool IsEdit => SelectedItem != null && SelectedItem.BIsActive && !SelectedItem.BIsKhoa;
        public bool IsLock => SelectedItem != null && SelectedItem.BIsKhoa;
        public bool IsEnableLock => SelectedItem != null;
        public int ILoai { get; set; }

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
            set => SetProperty(ref _selectedDonVi, value);
        }

        private string _searchSoQuyetDinh;
        public string SearchSoQuyetDinh
        {
            get => _searchSoQuyetDinh;
            set => SetProperty(ref _searchSoQuyetDinh, value);
        }

        private DateTime? _searchNgayQuyetDinh;
        public DateTime? SearchNgayQuyetDinh
        {
            get => _searchNgayQuyetDinh;
            set
            {
                SetProperty(ref _searchNgayQuyetDinh, value);
                if (_itemsCollectionView != null) _itemsCollectionView.Refresh();
            }
        }

        private string _searchTenChuongTrinh;
        public string SearchTenChuongTrinh
        {
            get => _searchTenChuongTrinh;
            set => SetProperty(ref _searchTenChuongTrinh, value);
        }

        private string _searchPhuongAnNK;
        public string SearchPhuongAnNK
        {
            get => _searchPhuongAnNK;
            set => SetProperty(ref _searchPhuongAnNK, value);
        }

        private ObservableCollection<ComboboxItem> _itemsLoaiQuyetDinh;
        public ObservableCollection<ComboboxItem> ItemsLoaiQuyetDinh
        {
            get => _itemsLoaiQuyetDinh;
            set => SetProperty(ref _itemsLoaiQuyetDinh, value);
        }

        private ComboboxItem _selectedLoaiQuyetDinh;
        public ComboboxItem SelectedLoaiQuyetDinh
        {
            get => _selectedLoaiQuyetDinh;
            set
            {
                SetProperty(ref _selectedLoaiQuyetDinh, value);
                if (_itemsCollectionView != null) _itemsCollectionView.Refresh();
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

        private ComboboxItem _selectedDuAn;
        public ComboboxItem SelectedDuAn
        {
            get => _selectedDuAn;
            set => SetProperty(ref _selectedDuAn, value);
        }

        public RelayCommand DieuChinhCommand { get; }
        public RelayCommand SearchCommand { get; }
        public RelayCommand RemoveFilterCommand { get; }
        public DAHDNKDecisionDialogViewModel DAHDNKDecisionDialogViewModel { get; set; }

        public DAHDNKDecisionIndexViewModel(
            ILog logger,
            IMapper mapper,
            ISessionService sessionService,
            INhHdnkCacQuyetDinhService nhHdnkCacQuyetDinhService,
            INsDonViService nsDonViService,
            INhDaGoiThauService nhDaGoiThauService,
            DAHDNKDecisionDialogViewModel DAHDNKdecisionDialogViewModel)
        {
            _logger = logger;
            _mapper = mapper;
            _sessionService = sessionService;
            _service = nhHdnkCacQuyetDinhService;
            _nsDonViService = nsDonViService;
            _nhDaGoiThauService = nhDaGoiThauService;

            SearchCommand = new RelayCommand(obj => _itemsCollectionView.Refresh());
            RemoveFilterCommand = new RelayCommand(obj => OnRemoveFilter());
            UpdateCommand = new RelayCommand(o => OnUpdate(), obj => IsEdit);
            DieuChinhCommand = new RelayCommand(o => OnDieuChinh(), obj => IsEdit);

            DAHDNKDecisionDialogViewModel = DAHDNKdecisionDialogViewModel;
        }

        public override void Init()
        {
            LoadLoaiQuyetDinh();
            LoadDonVi();
            LoadData();
        }

        private void LoadDonVi()
        {
            _itemsDonVi = new ObservableCollection<DonViModel>();
            var data = _nsDonViService.FindByNamLamViec(_sessionService.Current.YearOfWork);
            _itemsDonVi = _mapper.Map<ObservableCollection<DonViModel>>(data);
            OnPropertyChanged(nameof(ItemsDonVi));
        }

        private void LoadData()
        {
            BackgroundWorkerHelper.Run((s, e) =>
            {
                IsLoading = true;

                // Main process
                Items = new ObservableCollection<NhQuyetDinhDamPhamModel>();
                e.Result = _service.FindByCondition(ILoai);
            }, (s, e) =>
            {
                if (e.Error == null)
                {
                    Items = _mapper.Map<ObservableCollection<NhQuyetDinhDamPhamModel>>(e.Result);
                    // Process when run completed. e.Result
                    if (Items != null && Items.Count > 0)
                    {
                        SelectedItem = Items.FirstOrDefault();
                    }
                    _itemsCollectionView = CollectionViewSource.GetDefaultView(Items);
                    _itemsCollectionView.Filter = Items_Filter;
                    LoadChuongTrinh();
                    LoadDuAn();
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
            bool result = true;
            var item = (NhQuyetDinhDamPhamModel)obj;

            if (SelectedLoaiQuyetDinh != null)
            {
                result &= result && item.ILoaiQuyetDinh == int.Parse(SelectedLoaiQuyetDinh.ValueItem);
            }
            if (!string.IsNullOrEmpty(SearchSoQuyetDinh))
            {
                result &= !string.IsNullOrEmpty(item.SSoQuyetDinh) && item.SSoQuyetDinh.ToLower().Contains(SearchSoQuyetDinh.ToLower());
            }
            if (SearchNgayQuyetDinh != null)
            {
                result &= result && item.DNgayQuyetDinh.HasValue && item.DNgayQuyetDinh.Value.ToString("yyyy-MM-dd").ToLower().Contains(SearchNgayQuyetDinh.Value.ToString("yyyy-MM-dd"));
            }
            if (SelectedDonVi != null)
            {
                result &= result && item.IIdDonViQuanLy.Equals(SelectedDonVi.Id);
            }
            if (!string.IsNullOrEmpty(SearchTenChuongTrinh))
            {
                result &= !string.IsNullOrEmpty(item.STenChuongTrinh) && item.STenChuongTrinh.ToLower().Contains(SearchTenChuongTrinh.ToLower());
            }
            if (!string.IsNullOrEmpty(SearchPhuongAnNK))
            {
                result &= !string.IsNullOrEmpty(item.SPhuongAnNhapKhau) && item.SPhuongAnNhapKhau.ToLower().Contains(SearchPhuongAnNK.ToLower());
            }
            if (SelectedDuAn != null)
            {
                result &= item.IIdDuAnId.HasValue && item.IIdDuAnId.Value.Equals(SelectedDuAn.Id);
            }
            if (SelectedChuongTrinh != null)
            {
                result &= item.IIdKhTongTheNhiemVuChiId.HasValue && item.IIdKhTongTheNhiemVuChiId.Value.Equals(SelectedChuongTrinh.Id);
            }
            return result;
        }

        private void LoadLoaiQuyetDinh()
        {
            ItemsLoaiQuyetDinh = new ObservableCollection<ComboboxItem>(new[]
            {
                new ComboboxItem(LoaiQuyetDinhEnum.Get((int)LoaiQuyetDinhEnum.Type.DAM_PHAM), ((int)LoaiQuyetDinhEnum.Type.DAM_PHAM).ToString()),
                new ComboboxItem(LoaiQuyetDinhEnum.Get((int)LoaiQuyetDinhEnum.Type.CHI_TRONG_NUOC), ((int)LoaiQuyetDinhEnum.Type.CHI_TRONG_NUOC).ToString()),
                new ComboboxItem(LoaiQuyetDinhEnum.Get((int)LoaiQuyetDinhEnum.Type.CHI_DOAN_RA), ((int)LoaiQuyetDinhEnum.Type.CHI_DOAN_RA).ToString()),
            });
        }

        protected override void OnDelete()
        {
            if (SelectedItem == null) return;

            string msgConfirm = string.Format(Resources.ConfirmDeleteUsers);
            DialogResult dialogResult = System.Windows.Forms.MessageBox.Show(msgConfirm, Resources.ConfirmTitle, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dialogResult == DialogResult.Yes)
            {
                // Nếu là xóa bản ghi điều chỉnh thì bản ghi gốc sẽ được update bactive = 1
                if (SelectedItem.IIdParentAdjustId.HasValue)
                {
                    NhHdnkCacQuyetDinh quyetDinhParent = _service.FindById(SelectedItem.IIdParentAdjustId.Value);
                    if (quyetDinhParent != null)
                    {
                        quyetDinhParent.BIsActive = true;
                        _service.Update(quyetDinhParent);
                    }
                }
                _service.Delete(SelectedItem.Id);
                var listGoiThau = _nhDaGoiThauService.FindAll(s => s.IIdQuyetDinhChiTietId == SelectedItem.Id);
                if (listGoiThau.Any())
                {
                    listGoiThau.ForAll(s =>
                    {
                        //KhaiPD update 05/12/2022
                        //s.FGiaQuyetDinhChiTietUsd = null;
                        //s.FGiaQuyetDinhChiTietVnd = null;
                        //s.FGiaQuyetDinhChiTietEur = null;
                        //s.FGiaQuyetDinhChiTietNgoaiTeKhac = null;
                        s.IIdQuyetDinhChiTietId = null;
                        s.IIdParentAdjustId = null;
                    });
                    _nhDaGoiThauService.UpdateRange(listGoiThau);
                }
                OnRefresh();
            }
        }

        protected override void OnAdd()
        {
            DAHDNKDecisionDialogViewModel.IsDieuChinh = false;
            DAHDNKDecisionDialogViewModel.BIsReadOnly = false;
            DAHDNKDecisionDialogViewModel.Model = new NhQuyetDinhDamPhamModel();
            DAHDNKDecisionDialogViewModel.ILoai = this.ILoai;
            DAHDNKDecisionDialogViewModel.Init();
            DAHDNKDecisionDialogViewModel.SavedAction = obj => this.OnRefresh();
            DAHDNKDecisionDialogViewModel.ShowDialog();
        }

        protected override void OnUpdate()
        {
            if (SelectedItem != null)
            {
                DAHDNKDecisionDialogViewModel.IsDieuChinh = false;
                DAHDNKDecisionDialogViewModel.BIsReadOnly = false;
                DAHDNKDecisionDialogViewModel.Model = SelectedItem;
                DAHDNKDecisionDialogViewModel.ILoai = this.ILoai;
                DAHDNKDecisionDialogViewModel.Init();
                DAHDNKDecisionDialogViewModel.SavedAction = obj => this.OnRefresh();
                DAHDNKDecisionDialogViewModel.ShowDialog();
            }
        }

        protected void OnViewDetail()
        {
            if (SelectedItem != null)
            {
                DAHDNKDecisionDialogViewModel.BIsReadOnly = true;
                DAHDNKDecisionDialogViewModel.IsDieuChinh = false;
                DAHDNKDecisionDialogViewModel.Model = SelectedItem;
                DAHDNKDecisionDialogViewModel.ILoai = this.ILoai;
                DAHDNKDecisionDialogViewModel.Init();
                DAHDNKDecisionDialogViewModel.SavedAction = obj => this.OnRefresh();
                DAHDNKDecisionDialogViewModel.ShowDialog();
            }
        }

        private void OnDieuChinh()
        {
            //MessageBoxHelper.Info("Chức năng đang trong quá trình confirm, xin thử lại sau!");
            if (SelectedItem != null)
            {
                DAHDNKDecisionDialogViewModel.IsDieuChinh = true;
                DAHDNKDecisionDialogViewModel.IdDieuChinh = SelectedItem.Id;
                DAHDNKDecisionDialogViewModel.BIsReadOnly = false;
                DAHDNKDecisionDialogViewModel.ILoai = this.ILoai;

                DAHDNKDecisionDialogViewModel.Model = SelectedItem;
                DAHDNKDecisionDialogViewModel.Init();
                DAHDNKDecisionDialogViewModel.SavedAction = obj => this.OnRefresh();
                DAHDNKDecisionDialogViewModel.ShowDialog();
            }
        }

        protected override void OnRefresh()
        {
            LoadData();
        }

        private void OnRemoveFilter()
        {
            SelectedLoaiQuyetDinh = null;
            SearchSoQuyetDinh = string.Empty;
            SearchNgayQuyetDinh = null;
            SelectedDonVi = null;
            SearchTenChuongTrinh = string.Empty;
            SearchPhuongAnNK = string.Empty;
            if (_itemsCollectionView != null) _itemsCollectionView.Refresh();
        }

        protected override void OnSelectionDoubleClick(object obj)
        {
            if (SelectedItem != null)
            {
                OnViewDetail();
            }
        }

        protected override void OnSelectedItemChanged()
        {
            OnPropertyChanged(nameof(IsEnableLock));
            OnPropertyChanged(nameof(IsEdit));
            OnPropertyChanged(nameof(IsLock));
            base.OnSelectedItemChanged();
        }

        protected override void OnLockUnLock()
        {
            string message = IsLock ? Resources.UnlockChungTu : Resources.LockChungTu;
            var result = System.Windows.MessageBox.Show(message, Resources.ConfirmTitle, MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
                LockConfirmEventHandler();

        }

        private void LockConfirmEventHandler()
        {
            // call service to lock , unlock item in DB and reload data table !
            _service.LockOrUnlock(SelectedItem.Id, !SelectedItem.BIsKhoa);
            SelectedItem.BIsKhoa = !SelectedItem.BIsKhoa;
            OnPropertyChanged(nameof(IsLock));
            OnPropertyChanged(nameof(IsEdit));
            OnRefresh();

        }

        public override void Dispose()
        {
            base.Dispose();

            // Clear data
            _itemsCollectionView = null;
            Items.Clear();
        }

        private void LoadChuongTrinh()
        {
            try
            {
                if (Items == null) return;
                ItemsChuongTrinh = new ObservableCollection<ComboboxItem>(Items.GroupBy(g => g.IIdKhTongTheNhiemVuChiId).Select(n => new ComboboxItem() { ValueItem = n.First().Id.ToString(), Id = n.First().IIdKhTongTheNhiemVuChiId != null ? n.First().IIdKhTongTheNhiemVuChiId.Value : Guid.Empty, DisplayItem = n.First().STenChuongTrinh }));

                OnPropertyChanged(nameof(ItemsChuongTrinh));
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        private void LoadDuAn()
        {
            try
            {
                if (Items == null) return;
                ItemsDuAn = new ObservableCollection<ComboboxItem>(Items.GroupBy(g => g.IIdDuAnId).Select(n => new ComboboxItem() { ValueItem = n.First().Id.ToString(), Id = n.First().IIdDuAnId != null ? n.First().IIdDuAnId.Value : Guid.Empty, DisplayItem = n.First().STenDuAn }));
                OnPropertyChanged(nameof(ItemsDuAn));
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }
    }
}
