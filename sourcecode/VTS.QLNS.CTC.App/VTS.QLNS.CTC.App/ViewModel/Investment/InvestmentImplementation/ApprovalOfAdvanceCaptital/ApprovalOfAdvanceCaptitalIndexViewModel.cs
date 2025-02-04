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
using VTS.QLNS.CTC.App.View.Investment.InvestmentImplementation.ApprovalOfAdvanceCaptital;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Service;
using VTS.QLNS.CTC.Utility;
using VTS.QLNS.CTC.Utility.Enum;

namespace VTS.QLNS.CTC.App.ViewModel.Investment.InvestmentImplementation.ApprovalOfAdvanceCaptital
{
    public class ApprovalOfAdvanceCaptitalIndexViewModel : GridViewModelBase<VdtTtDeNghiThanhToanUngModel>
    {
        public override string FuncCode => NSFunctionCode.INVESTMENT_IMPLEMENTATION_APPROVED_OF_ADVANCED_CAPITAL_INDEX;
        public override string GroupName => MenuItemContants.GROUP_ALLOCATION;
        public override string Name => "Quản lý cấp phát cấp ứng ngoài chỉ tiêu";
        public override string Description => "Danh sách cấp phát cấp ứng ngoài chỉ tiêu";
        public bool IsEdit => SelectedItem != null && SelectedItem.Id != Guid.Empty;
        public override Type ContentType => typeof(View.Investment.InvestmentImplementation.ApprovalOfAdvanceCaptital.ApprovalOfAdvanceCaptitalIndex);

        #region Private
        private static string[] _lstDonViExclude = new string[] { "0", "1" };
        private readonly IVdtTtDeNghiThanhToanUngService _deNghiThanhToanUngService;
        private readonly ISessionService _sessionService;
        private readonly INsDonViService _nsDonViService;
        private ICollectionView _deNghiThanhToanView;
        private IMapper _mapper;
        #endregion

        #region declare RelayCommand
        public RelayCommand SearchCommand { get; }
        public RelayCommand ResetFilterCommand { get; }
        #endregion

        #region Componer
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

        #endregion

        public ApprovalOfAdvanceCaptitalDialogViewModel ApprovalOfAdvanceCaptitalDialogViewModel { get; set; }
        public ApprovalOfAdvanceCaptitalDetailViewModel ApprovalOfAdvanceCaptitalDetailViewModel { get; set; }

        public ApprovalOfAdvanceCaptitalIndexViewModel(
            ApprovalOfAdvanceCaptitalDialogViewModel approvalOfAdvanceCaptitalDialogViewModel,
            ApprovalOfAdvanceCaptitalDetailViewModel approvalOfAdvanceCaptitalDetailViewModel,
            IVdtTtDeNghiThanhToanUngService deNghiThanhToanUngService,
            INsDonViService nsDonViService,
            ISessionService sessionService,
            IMapper mapper)
        {
            _deNghiThanhToanUngService = deNghiThanhToanUngService;
            _nsDonViService = nsDonViService;
            _sessionService = sessionService;
            _mapper = mapper;

            ApprovalOfAdvanceCaptitalDialogViewModel = approvalOfAdvanceCaptitalDialogViewModel;
            ApprovalOfAdvanceCaptitalDialogViewModel.ParentPage = this;
            ApprovalOfAdvanceCaptitalDetailViewModel = approvalOfAdvanceCaptitalDetailViewModel;
            ApprovalOfAdvanceCaptitalDetailViewModel.ParentPage = this;
            
            SearchCommand = new RelayCommand(obj => OnSearch());
            ResetFilterCommand = new RelayCommand(obj => onResetFilter());
        }

        #region RelayCommand Event
        public override void Init()
        {
            MarginRequirement = new System.Windows.Thickness(10);
            GetDonViQuanLy();
            LoadData();
        }

        public override void LoadData(params object[] args)
        {
            List<VdtTtDeNghiThanhToanUngQuery> listChungTu = _deNghiThanhToanUngService.GetDeNghiThanhToanUngIndex().ToList();
            var lstItem = _mapper.Map<List<VdtTtDeNghiThanhToanUngModel>>(listChungTu);
            lstItem = lstItem.Select(n => { n.iRowIndex = lstItem.IndexOf(n) + 1; return n; }).ToList();
            Items = _mapper.Map<ObservableCollection<VdtTtDeNghiThanhToanUngModel>>(lstItem);
            _deNghiThanhToanView = CollectionViewSource.GetDefaultView(Items);
            _deNghiThanhToanView.Filter = VdtTtDeNghiThanhToanUngFilter;
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
            ApprovalOfAdvanceCaptitalDialogViewModel.Model = new VdtTtDeNghiThanhToanUngModel();
            ApprovalOfAdvanceCaptitalDialogViewModel.Init();
            ApprovalOfAdvanceCaptitalDialogViewModel.SavedAction = obj =>
            {
                this.OnRefresh();
                OnOpenApprovalOfAdvanceCaptitalDetail(_mapper.Map<VdtTtDeNghiThanhToanUngModel>(obj));
            };
            var view = new ApprovalOfAdvanceCaptitalDialog
            {
                DataContext = ApprovalOfAdvanceCaptitalDialogViewModel
            };
            DialogHost.Show(view, "RootDialog");
        }

        protected override void OnUpdate()
        {
            ApprovalOfAdvanceCaptitalDialogViewModel.Model = SelectedItem;
            ApprovalOfAdvanceCaptitalDialogViewModel.Model.IsEdit = true;
            ApprovalOfAdvanceCaptitalDialogViewModel.Init();
            ApprovalOfAdvanceCaptitalDialogViewModel.SavedAction = obj =>
            {
                this.OnRefresh();
                OnOpenApprovalOfAdvanceCaptitalDetail(_mapper.Map<VdtTtDeNghiThanhToanUngModel>(obj));
            };
            var view = new ApprovalOfAdvanceCaptitalDialog
            {
                DataContext = ApprovalOfAdvanceCaptitalDialogViewModel
            };
            DialogHost.Show(view, "RootDialog");
        }

        protected override void OnDelete()
        {
            base.OnDelete();
            StringBuilder messageBuilder = new StringBuilder();
            messageBuilder.AppendFormat(Resources.MsgConfirmDeletePhanBoVon);
            var messageBox = new NSMessageBoxViewModel(messageBuilder.ToString(), "Xác nhận", NSMessageBoxButtons.YesNo, DeleteEventHandler);
            DialogHost.Show(messageBox.Content, "RootDialog");
        }

        private void onResetFilter()
        {
            DNgayQuyetDinhFrom = null;
            DNgayQuyetDinhTo = null;
            DrpDonViQuanLySelected = null;
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
            var data = (VdtTtDeNghiThanhToanUngModel)obj;
            OnOpenApprovalOfAdvanceCaptitalDetail(data);
        }
        #endregion

        #region Helper
        private void GetDonViQuanLy()
        {
            var cbxLoaiDonViData = _nsDonViService.FindByNamLamViec(_sessionService.Current.YearOfWork)
                .Select(n => new ComboboxItem() { ValueItem = n.IIDMaDonVi, DisplayItem = string.Format("{0}-{1}", n.IIDMaDonVi, n.TenDonVi) });
            _drpDonViQuanLy = new ObservableCollection<ComboboxItem>(cbxLoaiDonViData);
        }

        private bool VdtTtDeNghiThanhToanUngFilter(object obj)
        {
            if (!(obj is VdtTtDeNghiThanhToanUngModel temp)) return true;
            var bCondition = true;
            if (DNgayQuyetDinhFrom.HasValue)
            {
                bCondition &= (temp.dNgayDeNghi.HasValue && temp.dNgayDeNghi >= DNgayQuyetDinhFrom);
            }
            if (DNgayQuyetDinhTo.HasValue)
            {
                bCondition &= (temp.dNgayDeNghi.HasValue && temp.dNgayDeNghi <= DNgayQuyetDinhTo);
            }
            if (DrpDonViQuanLySelected != null)
            {
                bCondition &= (temp.iID_MaDonViQuanLy == DrpDonViQuanLySelected.ValueItem);
            }
            return bCondition;
        }

        private void OnOpenApprovalOfAdvanceCaptitalDetail(VdtTtDeNghiThanhToanUngModel SelectedItem)
        {
            ApprovalOfAdvanceCaptitalDetailViewModel.Model = SelectedItem;
            ApprovalOfAdvanceCaptitalDetailViewModel.Init();
            var view = new ApprovalOfAdvanceCaptitalDetail { DataContext = ApprovalOfAdvanceCaptitalDetailViewModel };
            //view.Owner = System.Windows.Application.Current.MainWindow;
            view.ShowDialog();
            LoadData();
        }

        private void DeleteEventHandler(NSDialogResult result)
        {
            if (result != NSDialogResult.Yes) return;
            _deNghiThanhToanUngService.DeleteDeNghiThanhToanUng(_mapper.Map<VdtTtDeNghiThanhToanUng>(SelectedItem), _sessionService.Current.Principal);
            LoadData();
        }
        #endregion
    }
}
