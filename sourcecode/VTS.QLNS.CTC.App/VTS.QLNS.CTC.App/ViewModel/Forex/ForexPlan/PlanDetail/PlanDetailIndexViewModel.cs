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
using VTS.QLNS.CTC.App.ViewModel.Investment.MediumTermPlan.PlanSuggestions;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Service;
using VTS.QLNS.CTC.Core.Service.Impl;
using VTS.QLNS.CTC.Utility;
using VTS.QLNS.CTC.Utility.Enum;
using VTS.QLNS.CTC.Core.Domain;
using System.Collections.ObjectModel;
using VTS.QLNS.CTC.App.ViewModel.Investment.MediumTermPlan.PlanManager;
using VTS.QLNS.CTC.App.Model.Control;
using System.Windows;
using VTS.QLNS.CTC.App.Properties;
using VTS.QLNS.CTC.App.ViewModel.Investment.MediumTermPlan.QLDuAn;
using System.Text;
using System.Windows.Forms;

namespace VTS.QLNS.CTC.App.ViewModel.Forex.ForexPlan.PlanDetail
{
    public class PlanDetailIndexViewModel : GridViewModelBase<NhKhChiTietModel>
    {
        private static string[] lstDonViExclude = new string[] { "0", "1" };

        private readonly ILog _logger;
        private readonly IMapper _mapper;
        private readonly ISessionService _sessionService;
        private readonly INhKhChiTietService _nhKhChiTietService;
        private readonly INhKhChiTietHopDongService _nhKhChiTietHopDongService;

        private ICollectionView _nhKhChiTietView;
        public override string Name => "Kế hoạch chi tiết";
        public override string Description => "Kế hoạch chi tiết Bộ Quốc phòng phê duyệt ";
        public override Type ContentType => typeof(View.Forex.ForexPlan.PlanDetail.PlanDetailIndex);
        public bool IsEdit => SelectedItem != null && SelectedItem.BIsActive && !SelectedItem.BIsKhoa;
        public bool IsLock => SelectedItem != null && SelectedItem.BIsKhoa;

        private string _searchSoKeHoachTongTheText;
        public string SearchSoKeHoachTongTheText
        {
            get => _searchSoKeHoachTongTheText;
            set => SetProperty(ref _searchSoKeHoachTongTheText, value);
        }

        private string _searchSoKeHoachChiTietText;
        public string SearchSoKeHoachChiTietText
        {
            get => _searchSoKeHoachChiTietText;
            set => SetProperty(ref _searchSoKeHoachChiTietText, value);
        }

        private DateTime? _dNgayKeHoach;
        public DateTime? DNgayKeHoach
        {
            get => _dNgayKeHoach;
            set
            {
                SetProperty(ref _dNgayKeHoach, value);
                if (_nhKhChiTietView != null) _nhKhChiTietView.Refresh();
            }
        }

        private string _MoTaChiTiet;
        public string MoTaChiTiet
        {
            get => _MoTaChiTiet;
            set => SetProperty(ref _MoTaChiTiet, value);
        }

        public bool? IsAllItemsSelected
        {
            get
            {
                if (Items != null)
                {
                    var selected = Items.Select(item => item.Selected).Distinct().ToList();
                    return selected.Count == 1 ? selected.Single() : (bool?)null;
                }
                return false;
            }
            set
            {
                if (value.HasValue)
                {
                    SelectAll(value.Value, Items);
                    OnPropertyChanged();
                }
            }
        }

        #region RelayCommand
        public RelayCommand SearchCommand { get; set; }
        public RelayCommand ResetFilterCommand { get; set; }
        public RelayCommand FixDataCommand { get; set; }
        public RelayCommand ExportCommand { get; set; }
        public RelayCommand ExportKHTHDXCommand { get; set; }
        public RelayCommand ImportDataCommand { get; }
        public RelayCommand PrintReportCommand { get; }
        public RelayCommand ViewAttachmentCommand { get; }
        public RelayCommand AggregateCommand { get; set; }
        #endregion RelayCommand

        #region View
        public PlanDetailDialogViewModel PlanDetailDialogViewModel { get; set; }

        #endregion

        public PlanDetailIndexViewModel(
            ILog logger,
            IMapper mapper,
            ISessionService sessionService,
            INhKhChiTietService nhKhChiTietService,
            INhKhChiTietHopDongService nhKhChiTietHopDongService,
            PlanDetailDialogViewModel planDetailDialogViewModel)
        {
            _logger = logger;
            _mapper = mapper;
            _sessionService = sessionService;
            _nhKhChiTietService = nhKhChiTietService;
            _nhKhChiTietHopDongService = nhKhChiTietHopDongService;

            PlanDetailDialogViewModel = planDetailDialogViewModel;
            PlanDetailDialogViewModel.ParentPage = this;

            SearchCommand = new RelayCommand(obj => _nhKhChiTietView.Refresh());
            ResetFilterCommand = new RelayCommand(obj => OnResetFilter());
            FixDataCommand = new RelayCommand(obj => OnFixData());
        }

        public override void Init()
        {
            LoadData();
        }

        #region RelayCommand
        public override void LoadData(params object[] args)
        {
            try
            {
                List<NhKhChiTietQuery> lstNhKhChiTiet = _nhKhChiTietService.FindByCondition(_sessionService.Current.YearOfWork).ToList();
                Items = _mapper.Map<ObservableCollection<NhKhChiTietModel>>(lstNhKhChiTiet);

                _nhKhChiTietView = CollectionViewSource.GetDefaultView(Items);
                _nhKhChiTietView.Filter = ListSettlementVoucherFilter;

                if (Items != null && Items.Count > 0)
                    SelectedItem = Items.FirstOrDefault();
                OnPropertyChanged(nameof(Items));

            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        #endregion
        private bool ListSettlementVoucherFilter(object obj)
        {
            bool result = true;
            var item = (NhKhChiTietModel)obj;

            if (!string.IsNullOrEmpty(SearchSoKeHoachTongTheText))
            {
                result &= !string.IsNullOrEmpty(item.SSoKeHoachTongTheBQP) && item.SSoKeHoachTongTheBQP.ToLower().Contains(SearchSoKeHoachTongTheText.ToLower());
            }
            if (!string.IsNullOrEmpty(SearchSoKeHoachChiTietText))
            {
                result &= !string.IsNullOrEmpty(item.SSoKeHoach) && item.SSoKeHoach.ToLower().Contains(SearchSoKeHoachChiTietText.ToLower());
            }

            if (DNgayKeHoach != null)
            {
                result &= item.DNgayKeHoach.HasValue && item.DNgayKeHoach.Value.ToString("yyyy-MM-dd").ToLower().Contains(DNgayKeHoach.Value.ToString("yyyy-MM-dd"));
            }

            if (!string.IsNullOrEmpty(MoTaChiTiet))
            {
                result &= !string.IsNullOrEmpty(item.SMoTaChiTiet) && item.SMoTaChiTiet.ToLower().Contains(MoTaChiTiet.ToLower());
            }

            return result;
        }

        private static void SelectAll(bool select, IEnumerable<NhKhChiTietModel> models)
        {
            foreach (var model in models)
            {
                model.Selected = select;
            }
        }
        private void OnResetFilter()
        {
            SearchSoKeHoachTongTheText = string.Empty;
            SearchSoKeHoachChiTietText = string.Empty;
            MoTaChiTiet = string.Empty;
            DNgayKeHoach = null;
            if (_nhKhChiTietView != null) _nhKhChiTietView.Refresh();
        }
        protected override void OnSelectedItemChanged()
        {
            OnPropertyChanged(nameof(IsEdit));
            OnPropertyChanged(nameof(IsLock));
        }
        protected override void OnRefresh()
        {
            this.LoadData();
            OnPropertyChanged(nameof(Items));
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
            _nhKhChiTietService.LockOrUnlock(SelectedItem.Id, !SelectedItem.BIsKhoa);
            SelectedItem.BIsKhoa = !SelectedItem.BIsKhoa;
            OnPropertyChanged(nameof(IsLock));
            OnPropertyChanged(nameof(IsEdit));
            OnRefresh();
        }
        protected override void OnAdd()
        {
            PlanDetailDialogViewModel.Model = new NhKhChiTietModel();
            PlanDetailDialogViewModel.IsDieuChinh = false;
            PlanDetailDialogViewModel.IsReadOnly = false;
            PlanDetailDialogViewModel.Init();
            PlanDetailDialogViewModel.SavedAction = obj => this.OnRefresh();
            PlanDetailDialogViewModel.ShowDialog();
        }

        protected override void OnUpdate()
        {
            if (SelectedItem != null)
            {
                PlanDetailDialogViewModel.Model = SelectedItem;
                PlanDetailDialogViewModel.IsDieuChinh = false;
                PlanDetailDialogViewModel.IsReadOnly = false;
                PlanDetailDialogViewModel.Init();
                PlanDetailDialogViewModel.SavedAction = obj => this.OnRefresh();
                PlanDetailDialogViewModel.ShowDialog();
            }
        }

        private void OnFixData()
        {
            if (SelectedItem != null)
            {
                PlanDetailDialogViewModel.Model = SelectedItem;
                PlanDetailDialogViewModel.IsDieuChinh = true;
                PlanDetailDialogViewModel.IsReadOnly = false;
                PlanDetailDialogViewModel.Init();
                PlanDetailDialogViewModel.SavedAction = obj =>
                {
                    this.OnRefresh();
                };
                PlanDetailDialogViewModel.ShowDialog();
            }
        }

        protected override void OnSelectionDoubleClick(object obj)
        {
            if (SelectedItem != null)
            {
                switch (SelectedItem.BIsKhoa)
                {
                    case true:
                        // view detail
                        PlanDetailDialogViewModel.Model = SelectedItem;
                        PlanDetailDialogViewModel.IsReadOnly = true;
                        PlanDetailDialogViewModel.Init();
                        PlanDetailDialogViewModel.ShowDialog();
                        break;
                    case false:
                        // update
                        PlanDetailDialogViewModel.Model = SelectedItem;
                        PlanDetailDialogViewModel.IsReadOnly = false;
                        PlanDetailDialogViewModel.Init();
                        PlanDetailDialogViewModel.SavedAction = obj => this.OnRefresh();
                        PlanDetailDialogViewModel.ShowDialog();
                        break;
                }
            }
        }

        protected override void OnDelete()
        {
            try
            {
                var messageBox = new NSMessageBoxViewModel("Bạn có chắc chắn muốn xóa?", "Xác nhận", NSMessageBoxButtons.YesNo, DeleteEventHandler);
                DialogHost.Show(messageBox.Content, "RootDialog");
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        private void DeleteEventHandler(NSDialogResult result)
        {
            if (result != NSDialogResult.Yes) return;
            _nhKhChiTietHopDongService.DeleteByIdKhChiTiet(SelectedItem.Id);
            _nhKhChiTietService.Delete(SelectedItem.Id);
            // Nếu là xóa bản ghi điều chỉnh thì bản ghi cha sẽ được update bactive = 1
            if (SelectedItem.IIdParentAdjustId.HasValue)
            {
                NhKhChiTiet chiTietParent = _nhKhChiTietService.FindById(SelectedItem.IIdParentAdjustId.Value);
                if (chiTietParent != null)
                {
                    chiTietParent.BIsActive = true;
                    _nhKhChiTietService.Update(chiTietParent);
                }
            }
            OnRefresh();
            OnPropertyChanged(nameof(Items));
        }

    }
}
