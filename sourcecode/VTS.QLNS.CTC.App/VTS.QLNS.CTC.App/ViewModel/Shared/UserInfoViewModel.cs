using AutoMapper;
using MaterialDesignThemes.Wpf;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;
using VTS.QLNS.CTC.App.Command;
using VTS.QLNS.CTC.App.Model;
using VTS.QLNS.CTC.App.Service;
using VTS.QLNS.CTC.App.Service.Impl;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Service;
using VTS.QLNS.CTC.Utility;
using VTS.QLNS.CTC.Utility.Exceptions;

namespace VTS.QLNS.CTC.App.ViewModel.UserInfo
{
    public class UserInfoViewModel : ViewModelBase
    {
        private IUserService _userService;
        private IAuthenticationService _authenticationService;
        private ISessionService _sessionService;
        private IMapper _mapper;
        private string _imageDirectory = ImageConst.IMAGE_DIRECTORY;
        private string _imageType = ImageConst.IMAGE_TYPE;
        private IConfiguration _configuration;

        public override string Name => "Thông tin người dùng";
        public override string Description => "Thông tin người dùng";

        public HTNguoiDungModel User { get; set; } = new HTNguoiDungModel();
        public SessionInfo AuthenticationToken => _sessionService.Current;

        public string CurrentPassword { get; set; }
        public string NewPassword { get; set; }
        public string ConfirmPassword { get; set; }

        public RelayCommand ChangeAvatarCommand { get; set; }
        public RelayCommand CurrentPasswordChangedCommand { get; set; }
        public RelayCommand NewPasswordChangedCommand { get; set; }
        public RelayCommand ConfirmPasswordChangedCommand { get; set; }

        public Visibility IsVisibleDefaultAvatar
        {
            get
            {
                return IsVisibleAvatar == Visibility.Collapsed ? Visibility.Visible : Visibility.Collapsed;
            }
        }

        public Visibility IsVisibleAvatar
        {
            get
            {
                if (!string.IsNullOrEmpty(User.SDuongDanAnh) && File.Exists(User.SDuongDanAnh))
                {
                    string extension = Path.GetExtension(User.SDuongDanAnh);
                    if (_imageType.Contains(extension.ToUpper()))
                    {
                        return Visibility.Visible;
                    }
                }
                return Visibility.Collapsed;
            }
        }

        public UserInfoViewModel(IUserService userService, IAuthenticationService authenticationService, ISessionService sessionService, IMapper mapper, IConfiguration configuration)
        {
            _userService = userService;
            _sessionService = sessionService;
            _authenticationService = authenticationService;
            _mapper = mapper;
            _configuration = configuration;
            SaveCommand = new RelayCommand(obj => OnSave(obj));
            ChangeAvatarCommand = new RelayCommand(obj => OnChangeAvatar());
            CurrentPasswordChangedCommand = new RelayCommand(obj => OnChangePassword(obj, "CurrentPassword"));
            NewPasswordChangedCommand = new RelayCommand(obj => OnChangePassword(obj, "NewPassword"));
            ConfirmPasswordChangedCommand = new RelayCommand(obj => OnChangePassword(obj, "ConfirmPassword"));
            _imageDirectory = _configuration.GetSection("ImgFolder").Value;
            if (string.IsNullOrEmpty(_imageDirectory))
            {
                _imageDirectory = ImageConst.IMAGE_DIRECTORY;
            }
            _imageType = _configuration.GetSection("ImgType").Value;
            if (string.IsNullOrEmpty(_imageType))
            {
                _imageType = ImageConst.IMAGE_TYPE;
            }
            Init();
        }

        public override void Init()
        {
            base.Init();
            CurrentPassword = "";
            NewPassword = "";
            ConfirmPassword = "";
            HtNguoiDung user = _userService.FindByLogin(_sessionService.Current.Principal);
            User = _mapper.Map<HTNguoiDungModel>(user);
            OnPropertyChanged("IsVisibleAvatar");
            OnPropertyChanged("IsVisibleDefaultAvatar");
        }

        private void OnChangeAvatar()
        {
            string filename = "";
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Filter = _imageType
            };
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string time = DateTime.Now.ToString("ddmmyyhhmmss");
                filename = openFileDialog.FileName;
                string copyFileName = Path.GetFileNameWithoutExtension(filename) + "_" + _sessionService.Current.Principal + "_" + time + Path.GetExtension(filename);
                System.IO.Directory.CreateDirectory(_imageDirectory);
                File.Copy(filename, Path.Combine(_imageDirectory, copyFileName), true);
                User.SDuongDanAnh = Path.GetFullPath(Path.Combine(_imageDirectory, copyFileName));
                OnPropertyChanged("IsVisibleAvatar");
                OnPropertyChanged("IsVisibleDefaultAvatar");
            }
        }

        private void OnChangePassword(object sender, string type)
        {
            PasswordBox passwordBox = sender as PasswordBox;
            string password = passwordBox.Password;
            switch (type)
            {
                case "CurrentPassword":
                    CurrentPassword = password;
                    break;
                case "NewPassword":
                    NewPassword = password;
                    break;
                case "ConfirmPassword":
                    ConfirmPassword = password;
                    break;
                default:
                    break;
            }
        }

        public override void OnSave(object obj)
        {
            int type = int.Parse(obj.ToString());
            if (type == 0)
            {
                HtNguoiDung u = _userService.FindByLogin(AuthenticationToken.Principal);
                u.SDuongDanAnh = User.SDuongDanAnh;
                _userService.Update(u);
                _sessionService.Current.ImageUrl = User.SDuongDanAnh;
                DialogHost.CloseDialogCommand.Execute(null, null);
            }
            else
            {
                // validate password
                try
                {
                    if (string.IsNullOrEmpty(CurrentPassword) || string.IsNullOrEmpty(NewPassword) || string.IsNullOrEmpty(ConfirmPassword))
                    {
                        System.Windows.Forms.MessageBox.Show("Vui lòng điền đầy đủ thông tin các trường.", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                    if (!NewPassword.Equals(ConfirmPassword))
                    {
                        System.Windows.Forms.MessageBox.Show("Xác nhận mật khẩu không đúng.", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                    if (CurrentPassword.Equals(NewPassword))
                    {
                        System.Windows.Forms.MessageBox.Show("Mật khẩu mới không được trùng với mật khẩu hiện tại.", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                    HtNguoiDung currentUser = _authenticationService.Login(User.STaiKhoan, CurrentPassword);
                    HtNguoiDung u = _userService.FindByLogin(_sessionService.Current.Principal);
                    string passwordHash = _userService.CalculateHash(NewPassword, u.STaiKhoan);
                    u.SMatKhau = passwordHash;
                    _userService.Update(u);
                    DialogHost.CloseDialogCommand.Execute(null, null);
                }
                catch (InvalidPasswordException)
                {
                    System.Windows.Forms.MessageBox.Show("Mật khẩu hiện tại không chính xác.", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
            }
        }
    }
}
