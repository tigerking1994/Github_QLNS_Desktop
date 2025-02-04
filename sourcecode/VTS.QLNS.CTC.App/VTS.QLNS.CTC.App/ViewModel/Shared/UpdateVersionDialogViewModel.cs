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
    public class UpdateVersionDialogViewModel : ViewModelBase
    {
        public override Type ContentType => typeof(UpdateVersionDialog);
        public PackIconKind ActionIcon => PackIconKind.Update;

        private string _messageContent;
        public string MessageContent
        {
            get => _messageContent;
            set => SetProperty(ref _messageContent, value);
        }

        public RelayCommand YesCommand { get; }
        public RelayCommand NoCommand { get; }

        private Action _updateAction { get; }

        public UpdateVersionDialogViewModel(string messageContent, Action updateAction)
        {
            IsLoading = false;
            MessageContent = messageContent;
            _updateAction = updateAction;
            YesCommand = new RelayCommand((obj) => OnUpdate(), (obj) => !IsLoading);
            NoCommand = new RelayCommand(obj => DialogHost.CloseDialogCommand.Execute(null, null), (obj) => !IsLoading);
        }

        private void OnUpdate()
        {
            BackgroundWorkerHelper.Run((s, e) =>
            {
                IsLoading = true;
                _updateAction();
            },
            (s, e) =>
            {
                IsLoading = false;
                Application.Current.Shutdown();
            });
        }
    }
}
