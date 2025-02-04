using AutoMapper;
using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Data;
using VTS.QLNS.CTC.App.Command;
using VTS.QLNS.CTC.App.Model;
using VTS.QLNS.CTC.App.Properties;
using VTS.QLNS.CTC.App.View.SystemAdmin.UserManagement.Group;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Service;
using VTS.QLNS.CTC.Utility;
using VTS.QLNS.CTC.Utility.Enum;

namespace VTS.QLNS.CTC.App.ViewModel.SystemAdmin.UserManagement.Group
{
    public class GroupIndexViewModel : GridViewModelBase<HTNhomModel>
    {
        private readonly IGroupService _groupService;
        private readonly IMapper _mapper;
        private ICollectionView _dataCollectionView;

        public override string FuncCode => NSFunctionCode.SYSTEM_USER_MANAGEMENT_GROUP_INDEX;
        public override string Name => "Quản lý nhóm người dùng";
        public override string Description => "Danh sách nhóm người dùng";
        public override Type ContentType => typeof(GroupIndex);
        public override PackIconKind IconKind => PackIconKind.AccountGroup;

        private bool _isOpenLockPopup;
        public bool IsOpenLockPopup
        {
            get => _isOpenLockPopup;
            set => SetProperty(ref _isOpenLockPopup, value);
        }

        private string _groupName;
        public string GroupName
        {
            get => _groupName;
            set => SetProperty(ref _groupName, value);
        }

        public bool IsEdit => SelectedItem != null;

        public bool IsLock => SelectedItem != null && !SelectedItem.BKichHoat;

        public GroupDialogViewModel GroupDialogViewModel { get; set; }

        public RelayCommand ShowAddNewGroupDialogCommand { get; set; }
        public RelayCommand ShowUpdateGroupDialogCommand { get; set; }
        public RelayCommand LockUnlockCommand { get; set; }
        public RelayCommand SearchCommand { get; set; }

        public GroupIndexViewModel(IGroupService groupService, IMapper mapper, GroupDialogViewModel groupDialogViewModel)
        {
            GroupDialogViewModel = groupDialogViewModel;
            _groupService = groupService;
            _mapper = mapper;
            ShowUpdateGroupDialogCommand = new RelayCommand(obj => OnShowUpdateGroupDialog());
            ShowAddNewGroupDialogCommand = new RelayCommand(obj => OnShowAddNewGroupDialog());
            LockUnlockCommand = new RelayCommand(obj => OnLockUnlock());
            SearchCommand = new RelayCommand(obj => OnSearch());
        }

        private void OnSearch()
        {
            _dataCollectionView.Refresh();
        }

        private void ClosingEventHandler(object sender, DialogClosingEventArgs eventArgs)
        {
        }

        public override void Init()
        {
            base.Init();
            MarginRequirement = new System.Windows.Thickness(10);
            SelectedItem = null;
            LoadGroups();
        }

        private void LoadGroups()
        {
            ObservableCollection<HtNhom> groupEntities = new ObservableCollection<HtNhom>(_groupService.LoadEagerGroups(a => true));
            Items = _mapper.Map<ObservableCollection<HtNhom>, ObservableCollection<HTNhomModel>>(groupEntities);
            _dataCollectionView = CollectionViewSource.GetDefaultView(Items);
            _dataCollectionView.Filter = ItemsViewFilter;
            if (Items != null && Items.Count > 0)
            {
                SelectedItem = Items.FirstOrDefault();
            }
            OnPropertyChanged(nameof(Items));
        }

        private bool ItemsViewFilter(object obj)
        {
            HTNhomModel group = (HTNhomModel)obj;
            if (!string.IsNullOrEmpty(GroupName))
                return group.STenNhom.ToLower().Contains(GroupName.ToLower().Trim());
            return true;
        }

        public bool? IsAllItemsSelected
        {
            get
            {
                List<bool> selected = Items.Select(item => item.IsSelected).Distinct().ToList();
                return selected.Count == 1 ? selected.Single() : (bool?)null;
            }
            set
            {
                if (value.HasValue)
                {
                    SelectAll(value.Value, Items);
                    OnPropertyChanged(nameof(IsAllItemsSelected));
                }
            }
        }

        /// <summary>
        /// Action when checkbox select all is selected
        /// </summary>
        /// <param name="select">true/false</param>
        /// <param name="models">items source of data grid</param>
        private static void SelectAll(bool select, IEnumerable<HTNhomModel> models)
        {
            foreach (HTNhomModel model in models)
            {
                model.IsSelected = select;
            }
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
                _groupService.Delete(SelectedItem.Id);
                LoadGroups();
            }
        }

        private void OnShowUpdateGroupDialog()
        {
            if (SelectedItem == null)
            {
                MessageBox.Show("Vui lòng chọn bản ghi để cập nhật");
                return;
            }

            GroupDialogViewModel.GroupIndexViewModel = this;
            GroupDialogViewModel.Model = SelectedItem;
            GroupDialogViewModel.Init();

            GroupDialog updateGroupDialog = new GroupDialog()
            {
                DataContext = GroupDialogViewModel
            };
            System.Threading.Tasks.Task<object> dialog = DialogHost.Show(updateGroupDialog, "RootDialog", ClosingEventHandler);
        }

        private void OnShowAddNewGroupDialog()
        {
            GroupDialogViewModel.GroupIndexViewModel = this;
            GroupDialogViewModel.Model = new HTNhomModel();
            GroupDialogViewModel.Init();

            GroupDialog addGroupDialog = new GroupDialog()
            {
                DataContext = GroupDialogViewModel
            };
            System.Threading.Tasks.Task<object> dialog = DialogHost.Show(addGroupDialog, "RootDialog", ClosingEventHandler);
        }

        private void OnLockUnlock()
        {
            MessageBoxResult confirmResult = MessageBox.Show(SelectedItem.BKichHoat ? Resources.ConfirmLockGroups : Resources.ConfirmUnLockGroups,
                Resources.ConfirmMsg,
                MessageBoxButton.YesNo,
                MessageBoxImage.Question);

            if (confirmResult == MessageBoxResult.Yes)
            {
                _groupService.LockOrUnLock(SelectedItem.Id, !SelectedItem.BKichHoat);
                LoadGroups();
            }
        }

        protected override void OnRefresh()
        {
            LoadGroups();
        }
    }
}
