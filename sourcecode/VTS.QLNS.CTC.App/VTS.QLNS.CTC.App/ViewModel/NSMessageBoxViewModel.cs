using MaterialDesignThemes.Wpf;
using System;
using System.Windows;
using VTS.QLNS.CTC.App.Command;
using VTS.QLNS.CTC.App.Component;
using VTS.QLNS.CTC.Utility.Enum;

namespace VTS.QLNS.CTC.App.ViewModel
{
    public class NSMessageBoxViewModel : ViewModelBase
    {
        public override Type ContentType => typeof(NSMessageBox);
        public virtual PackIconKind ActionIcon { get; }

        private string _caption;
        public string Caption
        {
            get => _caption;
            set => SetProperty(ref _caption, value);
        }

        private string _messageContent;
        public string MessageContent
        {
            get => _messageContent;
            set => SetProperty(ref _messageContent, value);
        }

        private NSMessageBoxButtons _buttons;
        public NSMessageBoxButtons Buttons
        {
            get => _buttons;
            set
            {
                SetProperty(ref _buttons, value);
                OnPropertyChanged(nameof(YesVisibility));
                OnPropertyChanged(nameof(NoVisibility));
                OnPropertyChanged(nameof(OKVisibility));
                OnPropertyChanged(nameof(CancelVisibility));
            }
        }

        public Visibility YesVisibility
        {
            get
            {
                if (_buttons == NSMessageBoxButtons.YesNo || _buttons == NSMessageBoxButtons.YesNoCancel)
                {
                    return Visibility.Visible;
                }
                return Visibility.Collapsed;
            }
        }

        public Visibility NoVisibility
        {
            get
            {
                if (_buttons == NSMessageBoxButtons.YesNo || _buttons == NSMessageBoxButtons.YesNoCancel)
                {
                    return Visibility.Visible;
                }
                return Visibility.Collapsed;
            }
        }

        public Visibility OKVisibility
        {
            get
            {
                if (_buttons == NSMessageBoxButtons.OK || _buttons == NSMessageBoxButtons.OKCancel)
                {
                    return Visibility.Visible;
                }
                return Visibility.Collapsed;
            }
        }

        public Visibility CancelVisibility
        {
            get
            {
                if (_buttons == NSMessageBoxButtons.OKCancel || _buttons == NSMessageBoxButtons.YesNoCancel)
                {
                    return Visibility.Visible;
                }
                return Visibility.Collapsed;
            }
        }

        public RelayCommand YesCommand { get; set; }
        public RelayCommand NoCommand { get; set; }
        public RelayCommand OKCommand { get; set; }
        public new RelayCommand CancelCommand { get; set; }

        public NSMessageBoxViewModel(string messageContent)
            : this(messageContent, "Thông báo", NSMessageBoxButtons.OK, PackIconKind.CheckboxesMarkedCircle, null)
        {
        }

        public NSMessageBoxViewModel(string messageContent, Action<NSDialogResult> action)
            : this(messageContent, "Thông báo", NSMessageBoxButtons.OK, PackIconKind.CheckboxesMarkedCircle, action)
        {
        }

        public NSMessageBoxViewModel(string messageContent, string caption, Action<NSDialogResult> action)
            : this(messageContent, caption, NSMessageBoxButtons.OK, PackIconKind.CheckboxesMarkedCircle, action)
        {
        }

        public NSMessageBoxViewModel(string messageContent, string caption, NSMessageBoxButtons buttons, Action<NSDialogResult> action)
            : this(messageContent, caption, buttons, PackIconKind.HelpRhombus, action)
        {
        }

        public NSMessageBoxViewModel(string messageContent, string caption, NSMessageBoxButtons buttons, PackIconKind icon, Action<NSDialogResult> action)
        {
            Buttons = buttons;
            MessageContent = messageContent;
            Caption = caption;
            ActionIcon = icon;
            YesCommand = new RelayCommand(obj =>
            {
                action?.Invoke(NSDialogResult.Yes);
                DialogHost.CloseDialogCommand.Execute(null, null);
            });
            NoCommand = new RelayCommand(obj =>
            {
                action?.Invoke(NSDialogResult.No);
                DialogHost.CloseDialogCommand.Execute(null, null);
            });
            OKCommand = new RelayCommand(obj =>
            {
                action?.Invoke(NSDialogResult.OK);
                DialogHost.CloseDialogCommand.Execute(null, null);
            });
            CancelCommand = new RelayCommand(obj =>
            {
                action?.Invoke(NSDialogResult.Cancel);
                DialogHost.CloseDialogCommand.Execute(null, null);
            });
        }

        public NSMessageBoxViewModel(string messageContent, string caption, NSMessageBoxButtons buttons, PackIconKind icon, Action<NSDialogResult> action, Action func)
            : this(messageContent, caption, buttons, icon, action)
        {
            Buttons = buttons;
            MessageContent = messageContent;
            Caption = caption;
            ActionIcon = icon;
            YesCommand = new RelayCommand(obj =>
            {
                DialogHost.CloseDialogCommand.Execute(null, null);
                func?.Invoke();
                action?.Invoke(NSDialogResult.Yes);
            });
            NoCommand = new RelayCommand(obj =>
            {
                DialogHost.CloseDialogCommand.Execute(null, null);
                func?.Invoke();
                action?.Invoke(NSDialogResult.No);
            });
            OKCommand = new RelayCommand(obj =>
            {
                DialogHost.CloseDialogCommand.Execute(null, null);
                action?.Invoke(NSDialogResult.OK);
            });
            CancelCommand = new RelayCommand(obj =>
            {
                DialogHost.CloseDialogCommand.Execute(null, null);
                action?.Invoke(NSDialogResult.Cancel);
            });
        }
    }
}
