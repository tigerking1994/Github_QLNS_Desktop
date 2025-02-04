using AutoMapper;
using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows;
using System.Windows.Data;
using VTS.QLNS.CTC.App.Command;
using VTS.QLNS.CTC.App.Service;
using VTS.QLNS.CTC.App.Model;
using VTS.QLNS.CTC.App.Properties;
using VTS.QLNS.CTC.App.View;
using VTS.QLNS.CTC.App.View.SystemAdmin.UserManagement.User;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Service;
using VTS.QLNS.CTC.Utility.Enum;
using VTS.QLNS.CTC.Core.Service.Impl;
using VTS.QLNS.CTC.Utility;

namespace VTS.QLNS.CTC.App.ViewModel.SystemAdmin.UserManagement.User
{
    public class UserLnsIndexViewModel : ViewModelBase
    {
        private IUserService _userService;
        private IExportService _exportService;
        private IMapper _mapper;
        private INsDonViService _nsDonViService;
        private ICollectionView _dataCollectionView;
        private ISessionService _sessionService;
        private ICauHinhMLNSService _cauHinhMLNSService;
        private IServiceProvider _serviceProvider;

        public override string FuncCode => NSFunctionCode.SYSTEM_USER_MANAGEMENT_USER_LNS_INDEX;
        public override string Name => "Quản lý người dùng loại ngân sách";
        public override string Description => "Danh sách người dùng loại ngân sách";
        public override Type ContentType => typeof(UserLnsIndex);
        public override PackIconKind IconKind => PackIconKind.User;

        private ObservableCollection<HTNguoiDungModel> _users;
        public ObservableCollection<HTNguoiDungModel> Users
        {
            get => _users;
            set => SetProperty(ref _users, value);
        }

        private HTNguoiDungModel _selectedUser;
        public HTNguoiDungModel SelectedUser
        {
            get => _selectedUser;
            set
            {
                SetProperty(ref _selectedUser, value);
                OnPropertyChanged(nameof(IsEdit));
                OnPropertyChanged(nameof(IsLock));
            }
        }

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

        public bool IsEdit => SelectedUser != null;

        public bool IsLock => SelectedUser != null && !SelectedUser.BKichHoat;

        public RelayCommand RefreshCommand { get; set; }
        public RelayCommand SearchCommand { get; set; }
        public RelayCommand UpdateLnsCommand { get; set; }

        public UserLnsIndexViewModel(
            UserDialogViewModel userDialogViewModel,
            IUserService userService,
            IMapper mapper,
            IExportService exportService,
            INsDonViService nsDonViService,
            ISessionService sessionService,
            ICauHinhMLNSService cauHinhMLNSService)
        {
            _userService = userService;
            _exportService = exportService;
            _nsDonViService = nsDonViService;
            _cauHinhMLNSService = cauHinhMLNSService;
            _mapper = mapper;
            _sessionService = sessionService;
            RefreshCommand = new RelayCommand(obj => LoadUsers());
            SearchCommand = new RelayCommand(obj => OnSearch());
            UpdateLnsCommand = new RelayCommand(obj => OnUpdateLns());
        }

        private void OnUpdateLns()
        {
            if (SelectedUser == null)
            {
                return;
            }
            List<string> selectedLns = SelectedUser.NsNguoiDungLnsModels.Select(lns => lns.SLns).ToList();
            GenericControlCustomViewModel<CauHinhUserMLNSModel, Core.Domain.NsMucLucNganSach, CauHinhMLNSService> genericControlCustomViewModel = new GenericControlCustomViewModel<CauHinhUserMLNSModel, Core.Domain.NsMucLucNganSach, CauHinhMLNSService>((CauHinhMLNSService)_cauHinhMLNSService, _mapper, _sessionService, _serviceProvider);
            genericControlCustomViewModel.IsDialog = true;
            genericControlCustomViewModel.Description = "Loại ngân sách";
            genericControlCustomViewModel.Title = "Loại ngân sách";
            genericControlCustomViewModel.IsMultipleSelect = true;

            GenericControlCustomWindowViewModel genericControlCustomWindowViewModel = new GenericControlCustomWindowViewModel(genericControlCustomViewModel);
            VTS.QLNS.CTC.App.View.Shared.GenericControlCustomWindow GenericControlCustomWindow = new VTS.QLNS.CTC.App.View.Shared.GenericControlCustomWindow
            {
                DataContext = genericControlCustomWindowViewModel
            };

            foreach (CauHinhUserMLNSModel cauHinhMLNSModel in genericControlCustomViewModel.Items)
            {
                cauHinhMLNSModel.IsSelected = selectedLns.Contains(cauHinhMLNSModel.Lns);
            }

            genericControlCustomViewModel.GenericControlCustomWindow = GenericControlCustomWindow;
            genericControlCustomViewModel.GenericControlCustomWindow.SavedAction += obj =>
            {
                IEnumerable<CauHinhUserMLNSModel> cauHinhMLNSModels = obj as IEnumerable<CauHinhUserMLNSModel>;
                List<NsMucLucNganSach> nsMucLucNganSaches = _mapper.Map<List<NsMucLucNganSach>>(cauHinhMLNSModels.ToList());
                _userService.SaveUserLns(SelectedUser.STaiKhoan, nsMucLucNganSaches, _sessionService.Current.YearOfWork);
                LoadUsers();
                GenericControlCustomWindow.Close();
                MessageBox.Show("Lưu dữ liệu thành công.", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
            };
            GenericControlCustomWindow.Show();
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
            ObservableCollection<HtNguoiDung> userEntities = new ObservableCollection<HtNguoiDung>(_userService.FindAllUsersWithLns(t => true, _sessionService.Current.YearOfWork));
            Users = _mapper.Map<ObservableCollection<HtNguoiDung>, ObservableCollection<HTNguoiDungModel>>(userEntities);
            _dataCollectionView = CollectionViewSource.GetDefaultView(Users);
            _dataCollectionView.Filter = ItemsViewFilter;
            if (Users != null && Users.Count > 0)
            {
                SelectedUser = Users.FirstOrDefault();
            }
            foreach (var model in Users)
            {
                model.PropertyChanged += (sender, args) =>
                {
                    if (args.PropertyName == nameof(HTNguoiDungModel.IsSelected))
                    {
                        OnPropertyChanged(nameof(IsEdit));
                    }
                };
            }
            OnPropertyChanged(nameof(Users));
            OnPropertyChanged(nameof(IsEdit));
        }

        private bool ItemsViewFilter(object obj)
        {
            var user = (HTNguoiDungModel)obj;
            if (!string.IsNullOrEmpty(UserName))
                return user.FullName.ToLower().Contains(UserName.ToLower().Trim()) || user.STaiKhoan.ToLower().Contains(UserName.ToLower().Trim());
            return true;
        }

    }
}
