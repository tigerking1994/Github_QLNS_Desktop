using AutoMapper;
using MaterialDesignThemes.Wpf;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Data;
using VTS.QLNS.CTC.App.Command;
using VTS.QLNS.CTC.App.Model;
using VTS.QLNS.CTC.App.Properties;
using VTS.QLNS.CTC.App.View.SystemAdmin.AppVersion;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Service;
using VTS.QLNS.CTC.Utility.Enum;

namespace VTS.QLNS.CTC.App.ViewModel.SystemAdmin.AppVersion
{
    public class AppVersionIndexViewModel : GridViewModelBase<AppVersionModel>
    {
        private readonly IAppVersionService _appVersionService;
        private readonly IMapper _mapper;
        private ICollectionView _itemsCollectionView;

        public override string Name => "Cập nhật phiên bản";
        public override string Description => "Cập nhật phiên bản";
        public override string Title => "Cập nhật phiên bản";
        public override Type ContentType => typeof(AppVersionIndex);
        public override PackIconKind IconKind => PackIconKind.Update;
        public bool IsApply => base.SelectedItem != null && base.SelectedItem.Status == 0;

        public AppVersionDialogViewModel AppVersionDialogViewModel { get; set; }

        public RelayCommand ApplyCommand { get; set; }

        public AppVersionIndexViewModel(IAppVersionService appVersionService,
            AppVersionDialogViewModel appVersionDialogViewModel,
            IMapper mapper)
        {
            _appVersionService = appVersionService;
            _mapper = mapper;

            AppVersionDialogViewModel = appVersionDialogViewModel;
            AppVersionDialogViewModel.ParentPage = this;

            ApplyCommand = new RelayCommand(obj => OnApplyVersion());
        }

        protected override void OnSelectedItemChanged()
        {
            base.OnSelectedItemChanged();
            OnPropertyChanged(nameof(IsApply));
        }

        public override void Init()
        {
            base.Init();
            OnRefresh();
        }

        protected override void OnRefresh()
        {
            ObservableCollection<AppVersionQuery> appVersionQuerys = new ObservableCollection<AppVersionQuery>(_appVersionService.FindAll());
            Items = _mapper.Map<ObservableCollection<AppVersionModel>>(appVersionQuerys);
            _itemsCollectionView = CollectionViewSource.GetDefaultView(Items);
            OnPropertyChanged(nameof(Items));
        }

        public void OnApplyVersion()
        {
            if (SelectedItem == null || SelectedItem.Status != 0)
            {
                return;
            }
            AppVersionQuery selectedEntity = _mapper.Map<AppVersionQuery>(SelectedItem);
            ObservableCollection<AppVersionQuery> appVersionQueries = _mapper.Map<ObservableCollection<AppVersionQuery>>(Items);
            _appVersionService.ApplyVersion(selectedEntity, appVersionQueries);
            OnRefresh();
        }

        protected override void OnUpdate()
        {
            if (SelectedItem == null)
            {
                return;
            }
            AppVersionDialogViewModel.Model = SelectedItem;
            AppVersionDialogViewModel.Init();
            AppVersionDialogViewModel.SavedAction = obj => OnRefresh();
            AppVersionDialogViewModel.ShowDialogHost();
        }

        protected override void OnAdd()
        {
            AppVersionDialogViewModel.Model = new AppVersionModel();
            AppVersionDialogViewModel.Init();
            AppVersionDialogViewModel.SavedAction = obj => OnRefresh();
            AppVersionDialogViewModel.ShowDialogHost();
        }

        protected override bool CanDelete(object obj)
        {
            return SelectedItem != null;
        }

        protected override void OnDelete()
        {
            MessageBoxResult dialogResult = MessageBox.Show(Resources.MsgDeleteRecord, Resources.ConfirmTitle, MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (dialogResult == MessageBoxResult.Yes)
            {
                _appVersionService.DeleteVersion(SelectedItem.Id);
                OnRefresh();
                MessageBox.Show(string.Format(Resources.MsgDeleteSuccess), "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }
    }
}
