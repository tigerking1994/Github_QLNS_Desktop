using AutoMapper;
using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows.Data;
using VTS.QLNS.CTC.App.Command;
using VTS.QLNS.CTC.App.Model;
using VTS.QLNS.CTC.App.Model.Control;
using VTS.QLNS.CTC.App.Properties;
using VTS.QLNS.CTC.App.Service;
using VTS.QLNS.CTC.App.View.Investment.InvestmentImplementation.ThanhToanKhoBac;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Service;
using VTS.QLNS.CTC.Utility;
using VTS.QLNS.CTC.Utility.Enum;

namespace VTS.QLNS.CTC.App.ViewModel.Investment.InvestmentImplementation.ThanhToanKhoBac
{
    public class ThanhToanKhoBacIndexViewModel : GridViewModelBase<ThanhToanQuaKhoBacModel>
    {
        public override string FuncCode => NSFunctionCode.INVESTMENT_IMPLEMENTATION_THANH_TOAN_KHO_BAC_INDEX;
        //public override string GroupName => MenuItemContants.GROUP_ALLOCATION;
        public override string Name => "Thanh toán qua kho bạc";
        public override string Description => "Danh sách thông tin thanh toán qua kho bạc";
        public bool IsEdit => SelectedItem != null && SelectedItem.Id != Guid.Empty;
        public override Type ContentType => typeof(View.Investment.InvestmentImplementation.ThanhToanKhoBac.ThanhToanKhoBacIndex);
        public ThanhToanKhoBacDialogViewModel ThanhToanKhoBacDialogViewModel { get; set; }
        public ThanhToanKhoBacDetailViewModel ThanhToanKhoBacDetailViewModel { get; set; }

        #region Private
        private static string[] _lstDonViExclude = new string[] { "0", "1" };
        private readonly IVdtTtThanhToanQuaKhoBacService _thanhToanService;
        private readonly INsNguonNganSachService _nguonVonService;
        private readonly ISessionService _sessionService;
        private readonly INsDonViService _nsDonViService;
        private readonly ITongHopNguonNSDauTuService _tonghopService;
        private IMapper _mapper;
        private ICollectionView _deNghiThanhToanView;
        #endregion

        #region declare RelayCommand
        public RelayCommand SearchCommand { get; }
        public RelayCommand ResetFilterCommand { get; }
        #endregion

        #region Componer
        private string _iNamKeHoach;
        public string iNamKeHoach
        {
            get => _iNamKeHoach;
            set
            {
                SetProperty(ref _iNamKeHoach, value);
                OnSearch();
            }
        }

        private DateTime? _dNgayQuyetDinhFrom;
        public DateTime? DNgayQuyetDinhFrom
        {
            get => _dNgayQuyetDinhFrom;
            set
            {
                SetProperty(ref _dNgayQuyetDinhFrom, value);
                OnSearch();
            }
        }

        private DateTime? _dNgayQuyetDinhTo;
        public DateTime? DNgayQuyetDinhTo
        {
            get => _dNgayQuyetDinhTo;
            set
            {
                SetProperty(ref _dNgayQuyetDinhTo, value);
                OnSearch();
            }
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
                OnSearch();
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
                OnSearch();
            }
        }
        #endregion

        public ThanhToanKhoBacIndexViewModel(
            ThanhToanKhoBacDialogViewModel thanhToanKhoBacDialogViewModel,
            ThanhToanKhoBacDetailViewModel thanhToanKhoBacDetailViewModel,
            IVdtTtThanhToanQuaKhoBacService thanhToanService,
            ISessionService sessionService,
            INsNguonNganSachService nguonVonService,
            INsDonViService nsDonViService,
            ITongHopNguonNSDauTuService tonghopService,
            IMapper mapper)
        {
            ThanhToanKhoBacDialogViewModel = thanhToanKhoBacDialogViewModel;
            ThanhToanKhoBacDialogViewModel.ParentPage = this;
            ThanhToanKhoBacDetailViewModel = thanhToanKhoBacDetailViewModel;
            ThanhToanKhoBacDetailViewModel.ParentPage = this;

            _thanhToanService = thanhToanService;
            _sessionService = sessionService;
            _nsDonViService = nsDonViService;
            _nguonVonService = nguonVonService;
            _tonghopService = tonghopService;
            _mapper = mapper;

            SearchCommand = new RelayCommand(obj => OnSearch());
            ResetFilterCommand = new RelayCommand(obj => onResetFilter());
        }

        #region RelayCommand Event
        public override void Init()
        {
            MarginRequirement = new System.Windows.Thickness(10);
            GetDonViQuanLy();
            GetNguonNganSach();
            LoadData();
        }

        public override void LoadData(params object[] args)
        {
            List<VdtTtThanhToanQuaKhoBacQuery> listChungTu = _thanhToanService.GetDataIndex().ToList();
            var lstItem = _mapper.Map<List<ThanhToanQuaKhoBacModel>>(listChungTu);
            lstItem = lstItem.Select(n => { n.iRowIndex = lstItem.IndexOf(n) + 1; return n; }).ToList();
            Items = _mapper.Map<ObservableCollection<ThanhToanQuaKhoBacModel>>(lstItem);
            _deNghiThanhToanView = CollectionViewSource.GetDefaultView(Items);
            _deNghiThanhToanView.Filter = VdtTtDeNghiThanhToanFilter;
            if (Items != null && Items.Count > 0)
            {
                SelectedItem = Items.FirstOrDefault();
            }
        }

        protected override void OnRefresh()
        {
            base.OnRefresh();
            LoadData();
        }

        public void OnSearch()
        {
            _deNghiThanhToanView.Refresh();
        }

        protected override void OnAdd()
        {
            ThanhToanKhoBacDialogViewModel.Model = new ThanhToanQuaKhoBacModel();
            ThanhToanKhoBacDialogViewModel.Init();
            ThanhToanKhoBacDialogViewModel.SavedAction = obj =>
            {
                this.OnRefresh();
                OnOpenDisbursementPaymentDetail(_mapper.Map<ThanhToanQuaKhoBacModel>(obj));
            };
            var view = new ThanhToanKhoBacDialog
            {
                DataContext = ThanhToanKhoBacDialogViewModel
            };
            DialogHost.Show(view, "RootDialog");
        }

        protected override void OnUpdate()
        {
            ThanhToanKhoBacDialogViewModel.Model = SelectedItem;
            ThanhToanKhoBacDialogViewModel.Model.IsEdit = true;
            ThanhToanKhoBacDialogViewModel.Init();
            ThanhToanKhoBacDialogViewModel.SavedAction = obj =>
            {
                this.OnRefresh();
                OnOpenDisbursementPaymentDetail(_mapper.Map<ThanhToanQuaKhoBacModel>(obj));
            };
            var view = new ThanhToanKhoBacDialog
            {
                DataContext = ThanhToanKhoBacDialogViewModel
            };
            DialogHost.Show(view, "RootDialog");
        }

        protected override void OnDelete()
        {
            base.OnDelete();
            StringBuilder messageBuilder = new StringBuilder();
            messageBuilder.AppendFormat(Resources.MsgConfirmDeleteCapPhatThanhToan);
            var messageBox = new NSMessageBoxViewModel(messageBuilder.ToString(), "Xác nhận", NSMessageBoxButtons.YesNo, DeleteEventHandler);
            DialogHost.Show(messageBox.Content, "RootDialog");
        }

        private void onResetFilter()
        {
            iNamKeHoach = null;
            DNgayQuyetDinhFrom = null;
            DNgayQuyetDinhTo = null;
            DrpDonViQuanLySelected = null;
            OnPropertyChanged(nameof(iNamKeHoach));
            OnPropertyChanged(nameof(DNgayQuyetDinhFrom));
            OnPropertyChanged(nameof(DNgayQuyetDinhTo));
            OnPropertyChanged(nameof(DrpDonViQuanLySelected));
            OnSearch();
        }

        protected override void OnSelectedItemChanged()
        {
            base.OnSelectedItemChanged();
            OnPropertyChanged(nameof(IsEdit));
        }

        protected override void OnSelectionDoubleClick(object obj)
        {
            base.OnSelectionDoubleClick(obj);
            var data = (ThanhToanQuaKhoBacModel)obj;
            OnOpenDisbursementPaymentDetail(data);
        }
        #endregion

        #region Helper
        private void GetDonViQuanLy()
        {
            var cbxLoaiDonViData = _nsDonViService.FindByNamLamViec(_sessionService.Current.YearOfWork)
                .Select(n => new ComboboxItem() { ValueItem = n.IIDMaDonVi, DisplayItem = n.TenDonVi });
            _drpDonViQuanLy = new ObservableCollection<ComboboxItem>(cbxLoaiDonViData);
        }

        private void GetNguonNganSach()
        {
            var data = _nguonVonService.FindNguonNganSach().Where(n => n.IIdMaNguonNganSach != 1)
                .Select(n => new ComboboxItem() { ValueItem = n.IIdMaNguonNganSach.Value.ToString(), DisplayItem = n.STen });
            _drpNguonVon = new ObservableCollection<ComboboxItem>(data);
        }

        private bool VdtTtDeNghiThanhToanFilter(object obj)
        {
            if (!(obj is ThanhToanQuaKhoBacModel temp)) return true;
            var bCondition = true;
            int iNamKeHoachParse = 0;
            if (!string.IsNullOrEmpty(iNamKeHoach) && int.TryParse(iNamKeHoach, out iNamKeHoachParse))
            {
                bCondition &= (temp.iNamKeHoach == iNamKeHoachParse);
            }
            if (DNgayQuyetDinhFrom.HasValue)
            {
                bCondition &= (temp.dNgayThanhToan.HasValue && temp.dNgayThanhToan >= DNgayQuyetDinhFrom);
            }
            if (DNgayQuyetDinhTo.HasValue)
            {
                bCondition &= (temp.dNgayThanhToan.HasValue && temp.dNgayThanhToan <= DNgayQuyetDinhTo);
            }
            if (DrpDonViQuanLySelected != null)
            {
                bCondition &= (temp.iId_MaDonViQuanLyID == DrpDonViQuanLySelected.ValueItem);
            }
            return bCondition;
        }

        private void OnOpenDisbursementPaymentDetail(ThanhToanQuaKhoBacModel SelectedItem)
        {
            ThanhToanKhoBacDetailViewModel.Model = SelectedItem;
            ThanhToanKhoBacDetailViewModel.Init();
            var view = new ThanhToanKhoBacDetail { DataContext = ThanhToanKhoBacDetailViewModel };
            //view.Owner = System.Windows.Application.Current.MainWindow;
            view.ShowDialog();
            LoadData();
        }

        private void DeleteEventHandler(NSDialogResult result)
        {
            if (result != NSDialogResult.Yes) return;
            _thanhToanService.DeleteThanhToanKhoBac(SelectedItem.Id);
            LoadData();
        }
        #endregion
    }
}
