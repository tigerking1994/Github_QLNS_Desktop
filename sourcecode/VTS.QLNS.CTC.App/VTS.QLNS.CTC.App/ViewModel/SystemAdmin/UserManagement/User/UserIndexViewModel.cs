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
using VTS.QLNS.CTC.App.Helper;
using VTS.QLNS.CTC.App.Model;
using VTS.QLNS.CTC.App.Properties;
using VTS.QLNS.CTC.App.Service;
using VTS.QLNS.CTC.App.View.SystemAdmin.UserManagement.User;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Service;
using VTS.QLNS.CTC.Utility;
using VTS.QLNS.CTC.Utility.Enum;

namespace VTS.QLNS.CTC.App.ViewModel.SystemAdmin.UserManagement.User
{
    public class UserIndexViewModel : GridViewModelBase<HTNguoiDungModel>
    {
        private readonly IUserService _userService;
        private readonly IExportService _exportService;
        private readonly IMapper _mapper;
        private readonly INsDonViService _nsDonViService;
        private ICollectionView _dataCollectionView;
        private readonly ISessionService _sessionService;

        public override string FuncCode => NSFunctionCode.SYSTEM_USER_MANAGEMENT_USER_INDEX;
        public override string Name => "Quản lý người dùng";
        public override string Description => "Danh sách người dùng";
        public override Type ContentType => typeof(UserIndex);
        public override PackIconKind IconKind => PackIconKind.User;

        private bool _isOpenLockPopup;
        public bool IsOpenLockPopup
        {
            get => _isOpenLockPopup;
            set => SetProperty(ref _isOpenLockPopup, value);
        }

        private string _userName;
        public string UserName
        {
            get => _userName;
            set => SetProperty(ref _userName, value);
        }

        public bool IsEdit => SelectedItem != null;

        public bool IsLock => SelectedItem != null && !SelectedItem.BKichHoat;

        public UserDialogViewModel UserDialogViewModel { get; set; }

        public RelayCommand LockUnlockCommand { get; set; }
        public RelayCommand UnlockCommand { get; set; }
        public RelayCommand SearchCommand { get; set; }
        public RelayCommand ResetPasswordCommand { get; set; }

        public UserIndexViewModel(
            UserDialogViewModel userDialogViewModel,
            IUserService userService,
            IMapper mapper,
            IExportService exportService,
            INsDonViService nsDonViService,
            ISessionService sessionService)
        {
            _userService = userService;
            _exportService = exportService;
            _nsDonViService = nsDonViService;
            _mapper = mapper;
            _sessionService = sessionService;
            UserDialogViewModel = userDialogViewModel;
            LockUnlockCommand = new RelayCommand(obj => OnLockUnlock());
            SearchCommand = new RelayCommand(obj => OnSearch());
            ResetPasswordCommand = new RelayCommand(obj => OnResetPassword());
        }

        private void OnSearch()
        {
            _dataCollectionView.Refresh();
        }

        private void ClosingEventHandler(object sender, DialogClosingEventArgs eventArgs)
        {
            Console.WriteLine(eventArgs.ToString());
        }

        public override void Init()
        {
            base.Init();
            MarginRequirement = new System.Windows.Thickness(10);
            LoadUsers();
        }

        private void LoadUsers()
        {
            // Load data
            ObservableCollection<HtNguoiDung> userEntities = new ObservableCollection<HtNguoiDung>(_userService.FindAll(_sessionService.Current.YearOfWork));
            Items = _mapper.Map<ObservableCollection<HtNguoiDung>, ObservableCollection<HTNguoiDungModel>>(userEntities);
            _dataCollectionView = CollectionViewSource.GetDefaultView(Items);
            _dataCollectionView.Filter = ItemsViewFilter;
            if (Items != null && Items.Count > 0)
            {
                SelectedItem = Items.FirstOrDefault();
            }
            foreach (HTNguoiDungModel model in Items)
            {
                model.PropertyChanged += (sender, args) =>
                {
                    if (args.PropertyName == nameof(HTNguoiDungModel.IsSelected))
                    {
                        OnPropertyChanged(nameof(IsAllItemsSelected));
                        OnPropertyChanged(nameof(IsEdit));
                    }
                };
            }
            OnPropertyChanged(nameof(Items));
            OnPropertyChanged(nameof(IsAllItemsSelected));
            OnPropertyChanged(nameof(IsEdit));
        }

        private bool ItemsViewFilter(object obj)
        {
            HTNguoiDungModel user = (HTNguoiDungModel)obj;
            if (!string.IsNullOrEmpty(UserName))
                return user.FullName.ToLower().Contains(UserName.ToLower().Trim()) || user.STaiKhoan.ToLower().Contains(UserName.ToLower().Trim());
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
                    //OnPropertyChanged();
                }
            }
        }

        /// <summary>
        /// Action when checkbox select all is selected
        /// </summary>
        /// <param name="select">true/false</param>
        /// <param name="models">items source of data grid</param>
        private static void SelectAll(bool select, IEnumerable<HTNguoiDungModel> models)
        {
            foreach (HTNguoiDungModel model in models)
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
                _userService.Delete(SelectedItem.Id);
                LoadUsers();
            }
        }

        protected override void OnUpdate()
        {
            UserDialogViewModel.ParentPage = this;
            UserDialogViewModel.IsDisabledUserLoginField = true;
            UserDialogViewModel.IsVisiblePasswordField = Visibility.Collapsed;
            UserDialogViewModel.IsEnablePasswordField = false;
            UserDialogViewModel.Model = SelectedItem;
            UserDialogViewModel.Init();

            UserDialog updateUserDialog = new UserDialog()
            {
                DataContext = UserDialogViewModel
            };
            System.Threading.Tasks.Task<object> dialog = DialogHost.Show(updateUserDialog, "RootDialog", ClosingEventHandler);
        }

        protected override void OnAdd()
        {
            UserDialogViewModel.ParentPage = this;
            UserDialogViewModel.IsDisabledUserLoginField = false;
            UserDialogViewModel.IsVisiblePasswordField = Visibility.Visible;
            UserDialogViewModel.IsEnablePasswordField = true;
            UserDialogViewModel.Model = new HTNguoiDungModel();
            UserDialogViewModel.Init();

            UserDialog userDialog = new UserDialog()
            {
                DataContext = UserDialogViewModel
            };
            System.Threading.Tasks.Task<object> dialog = DialogHost.Show(userDialog, "RootDialog", ClosingEventHandler);
        }

        private void OnLockUnlock()
        {
            NSMessageBoxViewModel messeageBox = new NSMessageBoxViewModel(
                SelectedItem.BKichHoat ? Resources.ConfirmLockUsers : Resources.ConfirmUnlockUsers,
                Resources.ConfirmMsg,
                NSMessageBoxButtons.YesNo, LockOrUnLockResultHanlder);
            DialogHost.Show(messeageBox.Content, "RootDialog");
        }

        private void LockOrUnLockResultHanlder(NSDialogResult obj)
        {
            if (obj == NSDialogResult.Yes)
            {
                _userService.LockOrUnLock(SelectedItem.Id, !SelectedItem.BKichHoat);
                LoadUsers();
            }
        }

        protected override void OnSelectedItemChanged()
        {
            OnPropertyChanged(nameof(IsEdit));
            OnPropertyChanged(nameof(IsLock));
        }

        protected override void OnRefresh()
        {
            LoadUsers();
        }

        private void OnResetPassword()
        {
            MessageBoxResult rs = MessageBoxHelper.Confirm("Bạn có muốn reset mật khẩu mặc định của người dùng này?");
            if (rs.Equals(MessageBoxResult.Yes))
            {
                try
                {
                    _userService.ResetPassword(SelectedItem.STaiKhoan);
                    MessageBoxHelper.Info("Reset mật khẩu thành công");
                }
                catch (Exception)
                {
                    MessageBoxHelper.Info("Có lỗi xảy ra");
                }
            }
        }
    }
}
