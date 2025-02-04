using AutoMapper;
using Microsoft.Extensions.Configuration;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;
using System.Linq;
using VTS.QLNS.CTC.App.Command;
using VTS.QLNS.CTC.App.Helper;
using VTS.QLNS.CTC.App.Properties;
using VTS.QLNS.CTC.App.Service;
using VTS.QLNS.CTC.App.Service.Impl;
using VTS.QLNS.CTC.App.View;
using VTS.QLNS.CTC.App.View.Shared;
using VTS.QLNS.CTC.App.ViewModel.Setting;
using VTS.QLNS.CTC.App.ViewModel.Shared;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Service;
using VTS.QLNS.CTC.Utility;
using VTS.QLNS.CTC.Utility.Exceptions;

namespace VTS.QLNS.CTC.App.ViewModel
{
    public class LoginViewModel : ViewModelBase
    {
        private ISessionService _sessionService;
        private MainWindowViewModel _mainWindowViewModel;
        private IAuthenticationService _authenticationService;
        private IMapper _mapper;
        private ConnectDatabaseViewModel _connectDatabaseViewModel;
        private IConfiguration _configuration;
        private string _currentConnectionString;
        private ConnectionType _currentConnectionType;
        private INsDonViService _nsDonViService;

        public override Type ContentType => typeof(Login);

        private string _errMsg;
        public string ErrMsg
        {
            get => _errMsg;
            set
            {
                SetProperty(ref _errMsg, value);
                OnPropertyChanged(nameof(IsError));
            }
        }

        private string _userName;
        public string UserName
        {
            get => _userName;
            set => SetProperty(ref _userName, value);
        }

        public bool IsError => !string.IsNullOrEmpty(_errMsg) ? true : false;

        public RelayCommand LoginCommand { get; set; }
        public RelayCommand ConnectDbCommand { get; }
        public RelayCommand ManualUpdateCommand { get; }

        public LoginViewModel(ISessionService sessionService,
            IAuthenticationService authenticationService,
            IMapper mapper,
            MainWindowViewModel mainWindowViewModel,
            ConnectDatabaseViewModel connectDatabaseViewModel,
            IConfiguration configuration,
            INsDonViService nsDonViService)
        {
            _mainWindowViewModel = mainWindowViewModel;
            _sessionService = sessionService;
            _authenticationService = authenticationService;
            _mapper = mapper;
            _connectDatabaseViewModel = connectDatabaseViewModel;
            _configuration = configuration;
            _nsDonViService = nsDonViService;

            LoginCommand = new RelayCommand(obj => Login(obj));
            ConnectDbCommand = new RelayCommand(obj => OnOpenConnectDbWindow());
        }

        private void Login(object parameter)
        {
            var passwordBox = parameter as PasswordBox;
            var password = passwordBox.Password;
            if (String.IsNullOrEmpty(UserName) || String.IsNullOrEmpty(password))
            {
                ErrMsg = Resources.ErrorLoginEmptyInfo;
                return;
            }

            try
            {
                HtNguoiDung currentUser = _authenticationService.Login(UserName, password);
                
                if (currentUser != null)
                {
                    _sessionService.Current = new SessionInfo()
                    {
                        Principal = currentUser.STaiKhoan,
                        FirstName = currentUser.STen,
                        LastName = currentUser.SHo,
                        Email = currentUser.SEmail,
                        Authorities = currentUser.Authorities,
                        FuncAuthorities = currentUser.FuncAuthorities,
                        SysGroupUsers = currentUser.SysGroupUsers,
                        Budget = 1,
                        YearOfBudget = 2,
                        YearOfWork = DateTime.Now.Year,
                        ImageUrl = currentUser.SDuongDanAnh,
                        IdsPhanHoQuanLy = string.Join(",", currentUser.TlNguoiDungPhanHos.Select(n => n.IIdMaDonVi))
                    };

                    Window window = new MainWindow();
                    window.DataContext = _mainWindowViewModel;
                    window.Show();
                    ApplicationHelper.CloseAllButThis(typeof(MainWindow));
                    _mainWindowViewModel.Init();
                    _mainWindowViewModel.LoginViewModel = this;
                }
            }
            catch (InputPasswordExceedLimitException)
            {
                ErrMsg = Resources.ErrorInputPasswordExceedLimit;
            }
            catch (NoUserException)
            {
                ErrMsg = Resources.ErrorNoUserDatabase;
            }
            catch (InvalidPasswordException)
            {
                ErrMsg = Resources.ErrorLoginInvalidPassword;
            }
            catch (UserLockedException)
            {
                ErrMsg = Resources.ErrorUserLocked;
            }
        }

        public override void Init()
        {
            base.Init();
            ErrMsg = string.Empty;
            UserName = String.Empty;
            _connectDatabaseViewModel.UpdateConnectionString += CheckConnectionString;

            string connectionType = _configuration.GetSection("DbSettings:ConnectionType").Value;
            _currentConnectionType = connectionType == ConnectionType.SqlServer.ToString() ? ConnectionType.SqlServer : ConnectionType.LocalDb;
            _currentConnectionString = _configuration.GetConnectionString(connectionType);

        }

        private void OnOpenConnectDbWindow()
        {
            _connectDatabaseViewModel.ConnectionString = _currentConnectionString;
            _connectDatabaseViewModel.ConnectType = _currentConnectionType;
            _connectDatabaseViewModel.Init();

            ConnectDatabase view = new ConnectDatabase { DataContext = _connectDatabaseViewModel };
            view.ShowDialog();
        }

        private void CheckConnectionString(object sender, EventArgs e)
        {
            if (_currentConnectionString != (string)sender)
            {
                var result = System.Windows.Forms.MessageBox.Show(Resources.AlertChangeConnectionString, Resources.NotifiTitle, MessageBoxButtons.OK, MessageBoxIcon.Information);
                if (result == DialogResult.OK)
                    System.Windows.Application.Current.Shutdown();
            }
        }
    }
}
