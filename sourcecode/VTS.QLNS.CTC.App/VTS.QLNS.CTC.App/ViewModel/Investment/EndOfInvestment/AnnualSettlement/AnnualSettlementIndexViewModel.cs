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
using VTS.QLNS.CTC.App.View.Investment.EndOfInvestment.AnnualSettlement;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Service;
using VTS.QLNS.CTC.Utility;
using VTS.QLNS.CTC.Utility.Enum;

namespace VTS.QLNS.CTC.App.ViewModel.Investment.EndOfInvestment.AnnualSettlement
{
    public class AnnualSettlementIndexViewModel : GridViewModelBase<VdtQtBcquyetToanNienDoModel>
    {
        public override string FuncCode => NSFunctionCode.INVESTMENT_END_OF_INVESTMENT_ANNUAL_SETTLEMENT_INDEX;
        public override string GroupName => MenuItemContants.GROUP_ANNUAL_SETTLEMENT;
        public override string Name => "Đề nghị quyết toán niên độ ";
        public override string Description => "Danh sách quản lý đề nghị quyết toán niên độ ";
        public bool IsEdit => SelectedItem != null && SelectedItem.Id != Guid.Empty;
        public override Type ContentType => typeof(View.Investment.EndOfInvestment.AnnualSettlement.AnnualSettlementIndex);

        #region Private
        private static string[] _lstDonViExclude = new string[] { "0", "1" };
        private readonly IVdtQtBcQuyetToanNienDoService _service;
        private readonly ISessionService _sessionService;
        private readonly INsDonViService _nsDonViService;
        private readonly INsNguonNganSachService _nsNguonNganSachService;
        private ICollectionView _quyetToanNienDoView;
        private IMapper _mapper;
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

        private ObservableCollection<ComboboxItem> _drpNguonNganSach;
        public ObservableCollection<ComboboxItem> DrpNguonNganSach
        {
            get => _drpNguonNganSach;
            set => SetProperty(ref _drpNguonNganSach, value);
        }

        private ComboboxItem _drpNguonNganSachSelected;
        public ComboboxItem DrpNguonNganSachSelected
        {
            get => _drpNguonNganSachSelected;
            set
            {
                SetProperty(ref _drpNguonNganSachSelected, value);
                OnSearch();
            }
        }
        #endregion

        public AnnualSettlementDialogViewModel AnnualSettlementDialogViewModel { get; set; }
        public AnnualSettlementDetailViewModel AnnualSettlementDetailViewModel { get; set; }

        public AnnualSettlementIndexViewModel(
            AnnualSettlementDialogViewModel annualSettlementDialogViewModel,
            AnnualSettlementDetailViewModel annualSettlementDetailViewModel,
            IVdtQtBcQuyetToanNienDoService service,
            INsNguonNganSachService nsNguonNganSachService,
            ISessionService sessionService,
            INsDonViService nsDonViService,
            IMapper mapper)
        {
            AnnualSettlementDialogViewModel = annualSettlementDialogViewModel;
            AnnualSettlementDetailViewModel = annualSettlementDetailViewModel;
            _service = service;
            _nsNguonNganSachService = nsNguonNganSachService;
            _sessionService = sessionService;
            _nsDonViService = nsDonViService;
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
            List<VdtQtBcQuyetToanNienDoQuery> listChungTu = _service.GetDeNghiQuyetToanNienDoIndex();
            var lstItem = _mapper.Map<List<VdtQtBcquyetToanNienDoModel>>(listChungTu);
            lstItem = lstItem.Select(n => { n.IRowIndex = lstItem.IndexOf(n) + 1; return n; }).ToList();
            Items = _mapper.Map<ObservableCollection<VdtQtBcquyetToanNienDoModel>>(lstItem);
            _quyetToanNienDoView = CollectionViewSource.GetDefaultView(Items);
            _quyetToanNienDoView.Filter = VdtQtQuyetToanNienDoFilter;
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
            _quyetToanNienDoView.Refresh();
        }

        protected override void OnAdd()
        {
            AnnualSettlementDialogViewModel.Model = new VdtQtBcquyetToanNienDoModel();
            AnnualSettlementDialogViewModel.Init();
            AnnualSettlementDialogViewModel.SavedAction = obj =>
            {
                this.OnRefresh();
                OnOpenDisbursementPaymentDetail(_mapper.Map<VdtQtBcquyetToanNienDoModel>(obj));
            };
            var view = new AnnualSettlementDialog
            {
                DataContext = AnnualSettlementDialogViewModel
            };
            DialogHost.Show(view, "RootDialog");
        }

        protected override void OnUpdate()
        {
            AnnualSettlementDialogViewModel.Model = SelectedItem;
            AnnualSettlementDialogViewModel.Init();
            AnnualSettlementDialogViewModel.SavedAction = obj =>
            {
                this.OnRefresh();
                OnOpenDisbursementPaymentDetail(_mapper.Map<VdtQtBcquyetToanNienDoModel>(obj));
            };
            var view = new AnnualSettlementDialog
            {
                DataContext = AnnualSettlementDialogViewModel
            };
            DialogHost.Show(view, "RootDialog");
        }

        protected override void OnDelete()
        {
            base.OnDelete();
            StringBuilder messageBuilder = new StringBuilder();
            messageBuilder.AppendFormat(Resources.MsgConfirmDeleteQuyetToanNienDo);
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
            var data = (VdtQtBcquyetToanNienDoModel)obj;
            OnOpenDisbursementPaymentDetail(data);
        }
        #endregion

        #region Helper
        private void GetDonViQuanLy()
        {
            var cbxLoaiDonViData = _nsDonViService.FindByNamLamViec(_sessionService.Current.YearOfWork)
                .Where(n => _lstDonViExclude.Contains(n.Loai))
                .Select(n => new ComboboxItem() { ValueItem = n.IIDMaDonVi, DisplayItem = n.TenDonVi });
            _drpDonViQuanLy = new ObservableCollection<ComboboxItem>(cbxLoaiDonViData);
        }

        private void GetNguonNganSach()
        {
            var cbxNguonNganSachData = _nsNguonNganSachService.FindNguonNganSach()
                .OrderBy(n=>n.IIdMaNguonNganSach)
                .Select(n => new ComboboxItem() { ValueItem = n.IIdMaNguonNganSach.ToString(), DisplayItem = n.STen });
            _drpNguonNganSach = new ObservableCollection<ComboboxItem>(cbxNguonNganSachData);
        }

        private bool VdtQtQuyetToanNienDoFilter(object obj)
        {
            if (!(obj is VdtQtBcquyetToanNienDoModel temp)) return true;
            var bCondition = true;
            int iNamKeHoachParse = 0;
            if (!string.IsNullOrEmpty(iNamKeHoach) && int.TryParse(iNamKeHoach, out iNamKeHoachParse))
            {
                bCondition &= (temp.INamKeHoach == iNamKeHoachParse);
            }
            if (DNgayQuyetDinhFrom.HasValue)
            {
                bCondition &= (temp.DNgayDeNghi.HasValue && temp.DNgayDeNghi >= DNgayQuyetDinhFrom);
            }
            if (DNgayQuyetDinhTo.HasValue)
            {
                bCondition &= (temp.DNgayDeNghi.HasValue && temp.DNgayDeNghi <= DNgayQuyetDinhTo);
            }
            if (DrpDonViQuanLySelected != null)
            {
                bCondition &= (temp.IIDMaDonViQuanLy == DrpDonViQuanLySelected.ValueItem);
            }
            if (DrpNguonNganSachSelected != null)
            {
                bCondition &= (temp.IIDNguonVonID.ToString() == DrpNguonNganSachSelected.ValueItem);
            }
            return bCondition;
        }

        private void OnOpenDisbursementPaymentDetail(VdtQtBcquyetToanNienDoModel SelectedItem)
        {
            AnnualSettlementDetailViewModel.Model = SelectedItem;
            AnnualSettlementDetailViewModel.Init();
            var view = new AnnualSettlementDetail { DataContext = AnnualSettlementDetailViewModel };
            view.ShowDialog();
            LoadData();
        }

        private void DeleteEventHandler(NSDialogResult result)
        {
            if (result != NSDialogResult.Yes) return;
            _service.DeleteDeNghiQuyetToan(SelectedItem.Id);
            LoadData();
        }
        #endregion
    }
}
