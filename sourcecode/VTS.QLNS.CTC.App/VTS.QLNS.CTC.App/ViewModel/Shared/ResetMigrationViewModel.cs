using MaterialDesignThemes.Wpf;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using VTS.QLNS.CTC.App.Command;
using VTS.QLNS.CTC.App.Component;
using VTS.QLNS.CTC.App.Helper;
using VTS.QLNS.CTC.Utility;

namespace VTS.QLNS.CTC.App.ViewModel.Shared
{
    public class ResetMigrationViewModel : ViewModelBase
    {
        private readonly IConfiguration _configuration;
        private ConnectionType _connectionType;
        public override Type ContentType => typeof(ResetMigrationDialog);
        public PackIconKind ActionIcon => PackIconKind.RollerSkate;
        
        private string _messageContent;
        public string MessageContent
        {
            get => _messageContent;
            set => SetProperty(ref _messageContent, value);
        }

        public RelayCommand YesCommand { get; }
        public RelayCommand NoCommand { get; }

        private Action _resetAction { get; }

        public ResetMigrationViewModel(string messageContent, Action resetAction, IConfiguration configuration)
        {
            _configuration = configuration;
            _connectionType = _configuration.GetSection("DbSettings:ConnectionType").Value == ConnectionType.SqlServer.ToString() ? ConnectionType.SqlServer : ConnectionType.LocalDb;
            IsLoading = false;
            MessageContent = messageContent;
            _resetAction = resetAction;
            YesCommand = new RelayCommand((obj) => OnReset(), (obj) => !IsLoading);
            NoCommand = new RelayCommand(obj => DialogHost.CloseDialogCommand.Execute(null, null), (obj) => !IsLoading);
        }

        private void OnReset()
        {
            BackgroundWorkerHelper.Run((s, e) =>
            {
                IsLoading = true;
                _resetAction();
            },
            (s, e) =>
            {
                IsLoading = false;
                DialogHost.CloseDialogCommand.Execute(null, null);
                if (e.Error != null)
                {
                    MessageContent = "Có lỗi xảy ra !";
                    MessageBoxHelper.Info(MessageContent);
                }
                else
                {
                    if (_connectionType == ConnectionType.LocalDb)
                    {
                        MessageContent = "Reset thành công.";
                        MessageBoxHelper.Info(MessageContent);
                        Application.Current.Shutdown();
                    }
                    else
                    {
                        MessageContent = "Đang chọn Database SQLServer.\nChỉ Reset ứng dụng với Local Database";
                        MessageBoxHelper.Info(MessageContent);
                    }
                }
            });
        }
    }
}
