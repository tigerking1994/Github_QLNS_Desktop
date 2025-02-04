using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using VTS.QLNS.CTC.App.Command;
using VTS.QLNS.CTC.App.Component;
using VTS.QLNS.CTC.App.Helper;

namespace VTS.QLNS.CTC.App.ViewModel.Shared
{
    public class BackupDialogViewModel : ViewModelBase
    {
        public override Type ContentType => typeof(BackupVersionDialog);
        public PackIconKind ActionIcon => PackIconKind.Update;

        private string _messageContent;
        public string MessageContent
        {
            get => _messageContent;
            set => SetProperty(ref _messageContent, value);
        }

        public RelayCommand YesCommand { get; }
        public RelayCommand NoCommand { get; }

        private Action _backupAction { get; }

        public BackupDialogViewModel(string messageContent, Action backupAction)
        {
            IsLoading = false;
            MessageContent = messageContent;
            _backupAction = backupAction;
            YesCommand = new RelayCommand((obj) => OnBackup(), (obj) => !IsLoading);
            NoCommand = new RelayCommand(obj => DialogHost.CloseDialogCommand.Execute(null, null), (obj) => !IsLoading);
        }

        private void OnBackup()
        {
            BackgroundWorkerHelper.Run((s, e) =>
            {
                IsLoading = true;
                _backupAction();
            },
            (s, e) =>
            {
                IsLoading = false;
                DialogHost.CloseDialogCommand.Execute(null, null);
                if (e.Error != null)
                {
                    MessageContent = "Có lỗi xảy ra !";
                }
                else
                {
                    MessageContent = "Hoàn thành backup dữ liệu !";
                }
                MessageBoxHelper.Info(MessageContent);
            });
        }
    }
}
