using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows;
using VTS.QLNS.CTC.App.Command;
using VTS.QLNS.CTC.App.View.Shared.Settings;
using VTS.QLNS.CTC.App.ViewModel.Shared.Settings;

namespace VTS.QLNS.CTC.App.ViewModel.Shared
{
    public class SettingsConfigViewModel : ViewModelBase
    {
        private SettingsDonViSuDungViewModel _settingsDonViSuDungViewModel;
        private SettingsDonViConViewModel _settingsDonViConViewModel;

        public override string Name => "KHỞI TẠO DỮ LIỆU";
        public override string Description => "Khởi tạo dữ liệu";
        public override PackIconKind IconKind => PackIconKind.Cogs;
        public override Type ContentType => typeof(SettingsConfig);

        private RelayCommand _saveAndNextPageCommand;
        public RelayCommand SaveAndNextPageCommand 
        {
            get => _saveAndNextPageCommand;
            set => SetProperty(ref _saveAndNextPageCommand, value);
        }

        public RelayCommand PreviousPageCommand { get; set; }
        public RelayCommand SkipCommand { get; }

        private RelayCommand _saveAndClosePageCommand;
        public RelayCommand SaveAndCloseCommand 
        {
            get => _saveAndClosePageCommand;
            set => SetProperty(ref _saveAndClosePageCommand, value);
        }

        private RelayCommand _saveCommand;
        public RelayCommand SaveCommand 
        {
            get => _saveCommand;
            set => SetProperty(ref _saveCommand, value);
        }

        public int NamLamViec { get; set; }

        public SettingsConfigViewModel(SettingsDonViSuDungViewModel settingsDonViSuDungViewModel, 
            SettingsDonViConViewModel settingsDonViConViewModel)
        {
            _settingsDonViSuDungViewModel = settingsDonViSuDungViewModel;
            _settingsDonViConViewModel = settingsDonViConViewModel;
            _settingsDonViSuDungViewModel.ParentPage = this;
            _settingsDonViConViewModel.ParentPage = this;

            PreviousPageCommand = new RelayCommand(obj => PreviousPage(), CanGoPrevious);
            SkipCommand = new RelayCommand(obj => NextPage(), canGoNext);
        }

        private bool canGoNext(object arg)
        {
            int currentIndex = Documentation.IndexOf(DocumentationSelectedItem);
            return currentIndex < Documentation.Count - 1;
        }

        private bool CanGoPrevious(object arg)
        {
            int currentIndex = Documentation.IndexOf(DocumentationSelectedItem);
            return currentIndex > 0;
        }

        public override void Init()
        {
            MarginRequirement = new System.Windows.Thickness(0);

            Documentation = new ObservableCollection<ViewModelBase>()
            {
                _settingsDonViSuDungViewModel,
                _settingsDonViConViewModel,
            };
            DocumentationSelectedItem = Documentation.FirstOrDefault();
        }

        public void NextPage()
        {
            int currentIndex = Documentation.IndexOf(DocumentationSelectedItem);
            DocumentationSelectedItem = Documentation[++currentIndex];
        }

        public void PreviousPage()
        {
            int currentIndex = Documentation.IndexOf(DocumentationSelectedItem);
            DocumentationSelectedItem = Documentation[--currentIndex];
        }

        public override void OnClose(object obj)
        {
            var wd = obj as Window;
            wd.Close();
        }

    }
}
