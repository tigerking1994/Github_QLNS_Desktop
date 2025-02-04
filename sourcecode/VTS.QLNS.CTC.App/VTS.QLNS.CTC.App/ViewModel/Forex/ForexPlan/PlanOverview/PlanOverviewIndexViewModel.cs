using AutoMapper;
using log4net;
using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using VTS.QLNS.CTC.App.Command;
using VTS.QLNS.CTC.App.Model;
using VTS.QLNS.CTC.App.Properties;
using VTS.QLNS.CTC.App.Service;
using VTS.QLNS.CTC.App.View.Forex.ForexPlan.PlanOverview;
using VTS.QLNS.CTC.App.ViewModel.Shared;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Service;
using VTS.QLNS.CTC.Utility;
using VTS.QLNS.CTC.Utility.Enum;

namespace VTS.QLNS.CTC.App.ViewModel.Forex.ForexPlan.PlanOverview
{
    public class PlanOverviewIndexViewModel : GridViewModelBase<NhKhTongTheModel>
    {

        private readonly INhKhTongTheService _nhKhTongTheService;
        private readonly INhKhTongTheNhiemVuChiService _nhKhTongTheNhiemVuChiService;
        private readonly INhDmNhiemVuChiService _nhDmNhiemVuChiService;
        private readonly ILog _logger;
        private readonly ISessionService _sessionService;
        private readonly IMapper _mapper;

        //private ICollectionView _dataIndexFilter;
        public override string Name => "Kế hoạch tổng thể";
        public override string Description => "Kế hoạch tổng thể của Thủ tướng Chính phủ và Bộ quốc phòng phê duyệt";
        public override Type ContentType => typeof(PlanOverviewIndex);

        private string _sMoTaChiTietKhttcp;
        public string SMoTaChiTietKhttcp
        {
            get => _sMoTaChiTietKhttcp;
            set => SetProperty(ref _sMoTaChiTietKhttcp, value);
        }

        private ObservableCollection<NhKhTongTheModel> ItemsClone;
        private ObservableCollection<NhKhTongTheModel> ListNvcChiTietClone;
        public ObservableCollection<NhKhTongTheModel> ListNvcChiTiet;
        public IEnumerable<NhKhTongTheQuery> data;

        public bool IsEdit => SelectedItem != null && !SelectedItem.BIsKhoa && SelectedItem.BIsActive;
        public bool IsAddChild => SelectedItem != null && SelectedItem.IGiaiDoanTu.HasValue && SelectedItem.IGiaiDoanDen.HasValue;
        public bool IsLock => SelectedItem != null && SelectedItem.BIsKhoa;
        //public string SPlanOverviewType { get; set; }
        private NhKhTongTheFilterModel _nhKhTongTheFilter;
        public NhKhTongTheFilterModel NhKhTongTheFilter
        {
            get => _nhKhTongTheFilter;
            set => SetProperty(ref _nhKhTongTheFilter, value);
        }

        public RelayCommand ShowAddNewPlanOverviewDialogCommand { get; set; }
        public RelayCommand ShowAddChildNewPlanOverviewDialogCommand { get; set; }
        public RelayCommand ShowUpdatePlanOverviewDialogCommand { get; set; }
        public RelayCommand AdjustPlanOverviewCommand { get; set; }
        public RelayCommand LockUnlockCommand { get; set; }
        public RelayCommand UnlockCommand { get; set; }
        public RelayCommand SearchCommand { get; set; }
        public RelayCommand ResetFilterCommand { get; set; }
        public RelayCommand ViewAttachmentCommand { get; set; }

        public PlanOverviewStableDialogViewModel PlanOverviewStableDialogViewModel { get; set; }
        public PlanOverviewStageDialogViewModel PlanOverviewStageDialogViewModel { get; set; }
        public AttachmentViewModel AttachmentViewModel { get; set; }

        public PlanOverviewIndexViewModel(
            PlanOverviewStableDialogViewModel planOverviewStableDialogViewModel,
            PlanOverviewStageDialogViewModel planOverviewStageDialogViewModel,
            AttachmentViewModel attachmentViewModel,
            INhKhTongTheService nhKhTongTheService,
            INhKhTongTheNhiemVuChiService nhKhTongTheNhiemVuChiService,
            INhDmNhiemVuChiService nhDmNhiemVuChiService,
            ISessionService sessionService,
            IMapper mapper,
            ILog logger)
        {
            PlanOverviewStableDialogViewModel = planOverviewStableDialogViewModel;
            PlanOverviewStageDialogViewModel = planOverviewStageDialogViewModel;
            AttachmentViewModel = attachmentViewModel;
            _nhKhTongTheService = nhKhTongTheService;
            _nhKhTongTheNhiemVuChiService = nhKhTongTheNhiemVuChiService;
            _nhDmNhiemVuChiService = nhDmNhiemVuChiService;
            _sessionService = sessionService;
            _mapper = mapper;
            _logger = logger;
            ShowUpdatePlanOverviewDialogCommand = new RelayCommand(obj => OnUpdate());
            ShowAddNewPlanOverviewDialogCommand = new RelayCommand(obj => OnAdd());
            ShowAddChildNewPlanOverviewDialogCommand = new RelayCommand(obj => OnAddGiaiDoanCon(), obj => IsAddChild);
            AdjustPlanOverviewCommand = new RelayCommand(obj => OnAdjust());
            LockUnlockCommand = new RelayCommand(obj => OnLockUnlock());
            SearchCommand = new RelayCommand(obj => OnSearch());
            ResetFilterCommand = new RelayCommand(obj => OnRemoveFilter());
            ViewAttachmentCommand = new RelayCommand(obj => OnViewAttachment(), obj => base.SelectedItem != null && base.SelectedItem.TotalFiles > 0);
        }

        public override void Init()
        {
            MarginRequirement = new Thickness(10);
            NhKhTongTheFilter = new NhKhTongTheFilterModel();
            SelectedItem = null;
            //SPlanOverviewType = "Theo giai đoạn";
            LoadData();
        }

        private void LoadData()
        {
            try
            {
                data = _nhKhTongTheService.FindAllOverview();
                ListNvcChiTiet = _mapper.Map<ObservableCollection<NhKhTongTheModel>>(data.Where(x => x.IdParentTongThe != null).OrderByDescending(x => x.DNgayTao));
                ObservableCollection<NhKhTongTheModel> lstTemp = _mapper.Map<ObservableCollection<NhKhTongTheModel>>(data.Where(x => x.IdParentTongThe == null));
                Items.Clear();
                foreach (NhKhTongTheModel kh in lstTemp)
                {
                    //NhKhTongTheModel item = lstTemp.FirstOrDefault(x => x.SSoKeHoachTtcp == kh.SSoKeHoachTtcp);
                    Items.Add(kh);
                }
                if (Items != null && Items.Count > 0)
                {
                    foreach (NhKhTongTheModel item in Items)
                    {
                        item.IsShowChildren = false;
                        item.HasChildren = true;
                        item.IsHangCha = true;
                        item.PropertyChanged += PlanOverview_PropertyChanged;
                    }
                    SelectedItem = Items.FirstOrDefault();
                }

                //_dataIndexFilter = CollectionViewSource.GetDefaultView(Items);
                //_dataIndexFilter.Filter = DataFilter;
                OnPropertyChanged(nameof(Items));
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        private void PlanOverview_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName.Equals(nameof(NhKhTongTheModel.IsShowChildren)))
            {
                NhKhTongTheModel model = sender as NhKhTongTheModel;
                if (model.IsShowChildren)
                {
                    OnExpand();
                }
                else
                {
                    OnCollapse();
                }
            }
        }

        private void OnExpand()
        {
            SelectedItem.PropertyChanged -= PlanOverview_PropertyChanged;
            int currentIndex = Items.IndexOf(SelectedItem);
            SelectedItem.IsShowChildren = true;
            IEnumerable<NhKhTongTheModel> children = new List<NhKhTongTheModel>(ListNvcChiTiet.Where(t => SelectedItem.SSoKeHoachTtcp.Equals(t.SSoKeHoachTtcp)));
            foreach (NhKhTongTheModel item in children)
            {
                item.AncestorIds = new HashSet<Guid>();
                item.AncestorIds.Add(SelectedItem.Id);
                Items.Insert(++currentIndex, item);
            }
            SelectedItem.PropertyChanged += PlanOverview_PropertyChanged;
        }

        private void OnCollapse()
        {
            SelectedItem.IsShowChildren = false;
            Items = new ObservableCollection<NhKhTongTheModel>(Items.Where(t => t.AncestorIds == null || !t.AncestorIds.Contains(SelectedItem.Id)));
        }

        private bool DataFilter(object obj)
        {
            bool result = true;
            NhKhTongTheModel item = obj as NhKhTongTheModel;
            if (item != null)
            {
                //if (!string.IsNullOrEmpty(NhKhTongTheFilter.SNam))
                //{
                //    result &= !string.IsNullOrEmpty(item.SNam) && item.SNam.ToLower().Contains(NhKhTongTheFilter.SNam.ToLower());
                //}
                if (!string.IsNullOrWhiteSpace(NhKhTongTheFilter.SSoKeHoachTtcp))
                {
                    result &= !string.IsNullOrWhiteSpace(item.SSoKeHoachTtcp) && item.SSoKeHoachTtcp.ToLower().Contains(NhKhTongTheFilter.SSoKeHoachTtcp.ToLower());
                }
                if (NhKhTongTheFilter.DNgayBanHanhTtcp != null && NhKhTongTheFilter.DNgayBanHanhTtcp.HasValue)
                {
                    result &= item.DNgayKeHoachTtcp.HasValue && (item.DNgayKeHoachTtcp.Value == NhKhTongTheFilter.DNgayBanHanhTtcp.Value);
                }
                if (!string.IsNullOrWhiteSpace(NhKhTongTheFilter.SMoTaKeHoachTtcp))
                {
                    result &= !string.IsNullOrWhiteSpace(item.SMoTaChiTietKhttcp) && item.SMoTaChiTietKhttcp.ToLower().Contains(NhKhTongTheFilter.SMoTaKeHoachTtcp.ToLower());
                }
                if (!string.IsNullOrWhiteSpace(NhKhTongTheFilter.SSoKeHoachBqp))
                {
                    result &= !string.IsNullOrWhiteSpace(item.SSoKeHoachBqp) && item.SSoKeHoachBqp.ToLower().Contains(NhKhTongTheFilter.SSoKeHoachBqp.ToLower());
                }
                if (NhKhTongTheFilter.DNgayBanHanhBqp != null && NhKhTongTheFilter.DNgayBanHanhBqp.HasValue)
                {
                    result &= item.DNgayKeHoachBqp.HasValue && (item.DNgayKeHoachBqp.Value == NhKhTongTheFilter.DNgayBanHanhBqp.Value);
                }
                if (!string.IsNullOrWhiteSpace(NhKhTongTheFilter.SMoTaKeHoachBqp))
                {
                    result &= !string.IsNullOrWhiteSpace(item.SSoKeHoachBqp) && item.SSoKeHoachBqp.ToLower().Contains(NhKhTongTheFilter.SMoTaKeHoachBqp.ToLower());
                }
                if (NhKhTongTheFilter.IGiaiDoanTu_TTCP != null && NhKhTongTheFilter.IGiaiDoanTu_TTCP.HasValue)
                {
                    result &= item.IGiaiDoanTu_TTCP != null && item.IGiaiDoanTu_TTCP.HasValue && NhKhTongTheFilter.IGiaiDoanTu_TTCP.Value <= item.IGiaiDoanTu_TTCP.Value;
                }
                if (NhKhTongTheFilter.IGiaiDoanDen_TTCP != null && NhKhTongTheFilter.IGiaiDoanDen_TTCP.HasValue)
                {
                    result &= item.IGiaiDoanDen_TTCP != null && item.IGiaiDoanDen_TTCP.HasValue && NhKhTongTheFilter.IGiaiDoanDen_TTCP.Value >= item.IGiaiDoanDen_TTCP.Value;
                }
                //if (NhKhTongTheFilter.IGiaiDoanTu_BQP != null && NhKhTongTheFilter.IGiaiDoanTu_BQP.HasValue)
                //{
                //    result &= item.IGiaiDoanTu_BQP != null && item.IGiaiDoanTu_BQP.HasValue && NhKhTongTheFilter.IGiaiDoanTu_BQP.Value <= item.IGiaiDoanTu_BQP.Value;
                //}
                //if (NhKhTongTheFilter.IGiaiDoanDen_BQP != null && NhKhTongTheFilter.IGiaiDoanDen_BQP.HasValue)
                //{
                //    result &= item.IGiaiDoanDen_BQP != null && item.IGiaiDoanDen_BQP.HasValue && NhKhTongTheFilter.IGiaiDoanDen_BQP.Value >= item.IGiaiDoanDen_BQP.Value;
                //}
            }
            return result;
        }

        private void OnRemoveFilter()
        {
            NhKhTongTheFilter.SSoKeHoachTtcp = string.Empty;
            NhKhTongTheFilter.DNgayBanHanhTtcp = null;
            NhKhTongTheFilter.SSoKeHoachTtcp = string.Empty;
            NhKhTongTheFilter.SSoKeHoachBqp = string.Empty;
            NhKhTongTheFilter.DNgayBanHanhBqp = null;
            NhKhTongTheFilter.SSoKeHoachBqp = string.Empty;
            NhKhTongTheFilter.IGiaiDoanTu_TTCP = null;
            NhKhTongTheFilter.IGiaiDoanDen_TTCP = null;
            //NhKhTongTheFilter.IGiaiDoanTu_BQP = null;
            //NhKhTongTheFilter.IGiaiDoanDen_BQP = null;
            LoadData();
        }

        private void OnSearch()
        {
            //_dataIndexFilter.Refresh();
            ItemsClone = _mapper.Map<ObservableCollection<NhKhTongTheModel>>(data.Where(x => x.IdParentTongThe == null));
            ListNvcChiTietClone = _mapper.Map<ObservableCollection<NhKhTongTheModel>>(data.Where(x => x.IdParentTongThe != null));

            ListNvcChiTiet = _mapper.Map<ObservableCollection<NhKhTongTheModel>>(ListNvcChiTietClone.Where(x => DataFilter(x)));
            Items = _mapper.Map<ObservableCollection<NhKhTongTheModel>>(ItemsClone.Where(x => ListNvcChiTiet.Any(y => y.SSoKeHoachTtcp.Equals(x.SSoKeHoachTtcp))));
            if (Items != null && Items.Count > 0)
            {
                foreach (NhKhTongTheModel item in Items)
                {
                    item.HasChildren = true;
                    item.IsHangCha = true;
                    item.PropertyChanged += PlanOverview_PropertyChanged;
                }
                SelectedItem = Items.FirstOrDefault();
            }
            OnPropertyChanged(nameof(Items));
        }

        public bool? IsAllItemsSelected
        {
            get
            {
                if (Items != null)
                {
                    List<bool> selected = Items.Select(item => item.IsSelected).Distinct().ToList();
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

        private static void SelectAll(bool select, IEnumerable<NhKhTongTheModel> models)
        {
            foreach (NhKhTongTheModel model in models)
            {
                model.IsSelected = select;
            }
        }

        protected override void OnSelectedItemChanged()
        {
            OnPropertyChanged(nameof(IsEdit));
            OnPropertyChanged(nameof(IsLock));
        }

        protected override void OnDelete()
        {
            NSMessageBoxViewModel messeageBox = new NSMessageBoxViewModel("Bạn có chắc chắn muốn xóa", "Xác nhận", NSMessageBoxButtons.YesNo, ActionHanlder);
            DialogHost.Show(messeageBox.Content, "RootDialog");
        }

        private void ActionHanlder(NSDialogResult result)
        {
            if (result == NSDialogResult.Yes)
            {
                IEnumerable<NhKhTongTheNhiemVuChi> nhKhTongTheNhiemVuChis = _nhKhTongTheNhiemVuChiService.FindAllByKhTongTheId(SelectedItem.Id);
                _nhKhTongTheNhiemVuChiService.RemoveRange(nhKhTongTheNhiemVuChis);
                foreach (NhKhTongTheNhiemVuChi item in nhKhTongTheNhiemVuChis)
                {
                    _nhDmNhiemVuChiService.Delete(item.IIdNhiemVuChiId);
                }
                _nhKhTongTheService.Delete(SelectedItem.Id);
                // Nếu là xóa bản ghi điều chỉnh thì bản ghi cha sẽ được update bactive = 1
                if (SelectedItem.IIdParentAdjustId.HasValue)
                {
                    NhKhTongThe parent = _nhKhTongTheService.Find(SelectedItem.IIdParentAdjustId.Value);
                    if (parent != null)
                    {
                        parent.BIsActive = true;
                        _nhKhTongTheService.Update(parent);
                    }
                }
                LoadData();
            }
        }

        protected override void OnAdd()
        {
            //if (SPlanOverviewType != null)
            //{
            //bool isGiaiDoan = SPlanOverviewType.Contains("Theo giai đoạn");

            PlanOverviewStageDialogViewModel.Model = new NhKhTongTheModel();
            //PlanOverviewStageDialogViewModel.Model.ILoai = isGiaiDoan ? Loai_KHTT.GIAIDOAN : Loai_KHTT.NAM;
            PlanOverviewStageDialogViewModel.Model.ILoai = Loai_KHTT.GIAIDOAN;
            PlanOverviewStageDialogViewModel.IsDieuChinh = false;
            PlanOverviewStageDialogViewModel.BIsReadOnly = false;
            PlanOverviewStageDialogViewModel.IsAddChild = false;
            //PlanOverviewStageDialogViewModel.IsViewGiaiDoan = isGiaiDoan;
            PlanOverviewStageDialogViewModel.IsViewGiaiDoan = true;
            PlanOverviewStageDialogViewModel.Init();
            PlanOverviewStageDialogViewModel.SavedAction = obj => this.OnRefresh();
            PlanOverviewStageDialogViewModel.ShowDialog();
            //}
        }

        private void OnAddGiaiDoanCon()
        {
            PlanOverviewStageDialogViewModel.Model = SelectedItem;
            PlanOverviewStageDialogViewModel.IsDieuChinh = false;
            PlanOverviewStageDialogViewModel.BIsReadOnly = false;
            PlanOverviewStageDialogViewModel.IsAddChild = true;
            PlanOverviewStageDialogViewModel.IsViewGiaiDoan = true;
            PlanOverviewStageDialogViewModel.Init();
            PlanOverviewStageDialogViewModel.SavedAction = obj => this.OnRefresh();
            PlanOverviewStageDialogViewModel.ShowDialog();
        }

        private void OnLockUnlock()
        {
            MessageBoxResult confirmResult = MessageBox.Show(!SelectedItem.BIsKhoa ? Resources.ConfirmLockGroups : Resources.ConfirmUnLockGroups,
            Resources.ConfirmMsg,
            MessageBoxButton.YesNo,
            MessageBoxImage.Question);
            foreach (NhKhTongTheModel item in Items)
            {
                if (item.IsSelected)
                {
                    if (confirmResult == MessageBoxResult.Yes)
                    {
                        _nhKhTongTheService.LockOrUnLock(item.Id, !item.BIsKhoa);
                    }
                }
            }
            LoadData();
        }

        protected override void OnUpdate()
        {
            if (SelectedItem != null)
            {
                //bool isGiaiDoan = SelectedItem.ILoai == Loai_KHTT.GIAIDOAN;
                PlanOverviewStageDialogViewModel.Model = SelectedItem;
                PlanOverviewStageDialogViewModel.IsDieuChinh = false;
                PlanOverviewStageDialogViewModel.BIsReadOnly = false;
                PlanOverviewStageDialogViewModel.IsAddChild = false;
                //PlanOverviewStageDialogViewModel.IsViewGiaiDoan = isGiaiDoan;
                PlanOverviewStageDialogViewModel.IsViewGiaiDoan = true;
                PlanOverviewStageDialogViewModel.Init();
                PlanOverviewStageDialogViewModel.SavedAction = obj => this.OnRefresh();
                PlanOverviewStageDialogViewModel.ShowDialog();
            }
        }

        protected void OnAdjust()
        {
            if (SelectedItem != null)
            {
                //bool isGiaiDoan = SelectedItem.ILoai == Loai_KHTT.GIAIDOAN;
                PlanOverviewStageDialogViewModel.Model = SelectedItem;
                //PlanOverviewStageDialogViewModel.Model.ILoai = isGiaiDoan ? Loai_KHTT.GIAIDOAN : Loai_KHTT.NAM;
                PlanOverviewStageDialogViewModel.Model.ILoai = Loai_KHTT.GIAIDOAN;
                PlanOverviewStageDialogViewModel.IdDieuChinh = SelectedItem.Id;
                PlanOverviewStageDialogViewModel.IsDieuChinh = true;
                PlanOverviewStageDialogViewModel.BIsReadOnly = false;
                PlanOverviewStageDialogViewModel.IsAddChild = false;
                //PlanOverviewStageDialogViewModel.IsViewGiaiDoan = isGiaiDoan;
                PlanOverviewStageDialogViewModel.IsViewGiaiDoan = true;
                PlanOverviewStageDialogViewModel.Init();
                PlanOverviewStageDialogViewModel.SavedAction = obj => this.OnRefresh();
                PlanOverviewStageDialogViewModel.ShowDialog();
            }
        }

        protected override void OnRefresh()
        {
            LoadData();
        }

        protected override void OnSelectionDoubleClick(object obj)
        {
            if (SelectedItem != null)
            {
                if (SelectedItem.IdParentTongThe != null)
                {
                    //bool isGiaiDoan = SelectedItem.ILoai == Loai_KHTT.GIAIDOAN;
                    PlanOverviewStageDialogViewModel.Model = SelectedItem;
                    PlanOverviewStageDialogViewModel.BIsReadOnly = true;
                    //PlanOverviewStageDialogViewModel.IsViewGiaiDoan = isGiaiDoan;
                    PlanOverviewStageDialogViewModel.IsViewGiaiDoan = true;
                    PlanOverviewStageDialogViewModel.Init();
                    PlanOverviewStageDialogViewModel.ShowDialog();
                }
            }
        }

        private void OnViewAttachment()
        {
            if (base.SelectedItem != null)
            {
                AttachmentViewModel.ModuleType = AttachmentEnum.Type.NH_KH_TONGTHE;
                AttachmentViewModel.ObjectId = base.SelectedItem.Id;
                AttachmentViewModel.Init();
                AttachmentViewModel.ShowDialogHost();
            }
        }
    }
}

