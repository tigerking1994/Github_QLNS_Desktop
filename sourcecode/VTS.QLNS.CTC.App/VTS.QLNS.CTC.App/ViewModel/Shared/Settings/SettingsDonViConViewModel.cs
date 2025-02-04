using AutoMapper;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Data;
using VTS.QLNS.CTC.App.Command;
using VTS.QLNS.CTC.App.Model;
using VTS.QLNS.CTC.App.Model.Control;
using VTS.QLNS.CTC.App.Properties;
using VTS.QLNS.CTC.App.Service;
using VTS.QLNS.CTC.App.View.Shared.Settings;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Service;

namespace VTS.QLNS.CTC.App.ViewModel.Shared.Settings
{
    public class SettingsDonViConViewModel : GridViewModelBase<DonViModel>
    {
        public override Type ContentType => typeof(SettingsDonViCon);
        public override string Name => "CẤU HÌNH ĐƠN VỊ CON";
        public override string Description => "Cấu hình đơn vị con";

        private INsDonViService _nsDonViService;
        private IMapper _mapper;
        private ISessionService _sessionService;

        public ICollectionView _dataCollectionView;

        private DonViModel _filterModel;
        public DonViModel FilterModel
        {
            get => _filterModel;
            set => SetProperty(ref _filterModel, value);
        }

        public RelayCommand LoadExistCommand { get; }

        public ObservableCollection<ComboboxItem> LoaiDonVi
        {
            get => new ObservableCollection<ComboboxItem>
            {
                new ComboboxItem { DisplayItem = "Nội bộ", ValueItem = "1" },
                new ComboboxItem { DisplayItem = "Toàn quân", ValueItem = "2" }
            };
        }

        public int NamLamViec { get; set; }

        public SettingsDonViConViewModel(INsDonViService nsDonViService,
            IMapper mapper,
            ISessionService sessionService)
        {
            _nsDonViService = nsDonViService;
            _mapper = mapper;
            _sessionService = sessionService;
            LoadExistCommand = new RelayCommand(obj => ReloadData());
        }

        public override void Init()
        {
            FilterModel = new DonViModel();
            SettingsConfigViewModel settingsConfigViewModel = ParentPage as SettingsConfigViewModel;
            NamLamViec = settingsConfigViewModel.NamLamViec;
            LoadData();
            settingsConfigViewModel.SaveAndNextPageCommand = new RelayCommand(obj => { }, obj => false);
            settingsConfigViewModel.SaveAndCloseCommand = new RelayCommand(obj => SaveAndClose(obj), obj => true);
            settingsConfigViewModel.SaveCommand = new RelayCommand(obj =>
            {
                OnSaveData();
                LoadData();
            }, obj => true);
            settingsConfigViewModel.Name = this.Name;
        }

        private void LoadData()
        {
            IEnumerable<DonVi> data = _nsDonViService.FindDonViConByNamLamViec(NamLamViec);
            Items = _mapper.Map<ObservableCollection<DonViModel>>(data);
            foreach (var item in Items)
            {
                item.PropertyChanged += Item_PropertyChanged;
            }
            _dataCollectionView = CollectionViewSource.GetDefaultView(Items);
            OnPropertyChanged(nameof(Items));
        }

        private void Item_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            DonViModel dv = sender as DonViModel;
            dv.IsModified = true;
        }

        private void ReloadData()
        {
            var result = MessageBox.Show("Dữ liệu đã nhập sẽ mất, Đồng chí có chắc chắn muốn tải dữ liệu?", Resources.ConfirmTitle, MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                LoadData();
            }
        }

        private void SaveAndClose(object obj)
        {
            if (OnSaveData())
            {
                Window wd = obj as Window;
                wd.Close();
            }
        }

        public void SaveDonViCon()
        {
            ObservableCollection<DonVi> savedData = _mapper.Map<ObservableCollection<DonVi>>(Items.Where(i => i.IsModified));
            _nsDonViService.SaveAllDonViCon(savedData, NamLamViec);
            _nsDonViService.CopyDataToDonViThucHienDuAn(NamLamViec);
        }

        public bool OnSaveData()
        {
            var result = MessageBox.Show("Đồng chí có chắc chắn muốn lưu dữ liệu?", Resources.ConfirmTitle, MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                if (ValidateDuplicateMaDonVi())
                {
                    MessageBox.Show("Mã đơn vị không được trùng khớp", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Question);
                    return false;
                }
                SaveDonViCon();
                return true;
            }
            return false;
        }

        private bool ValidateDuplicateMaDonVi()
        {
            var dvsd = _nsDonViService.FindCurrentDonViSuDungByNamLamViec(NamLamViec);
            // check posible duplicate madonvi
            var lstDonvi = _mapper.Map<ObservableCollection<DonVi>>(Items);
            lstDonvi.Add(dvsd);
            var duplicate = lstDonvi.GroupBy(t => t.IIDMaDonVi).Any(x => x.Skip(1).Any());
            return duplicate;
        }

        protected override void OnAdd()
        {
            DonViModel newItem = new DonViModel();
            newItem.NamLamViec = NamLamViec;
            newItem.Loai = VTS.QLNS.CTC.Utility.LoaiDonVi.NOI_BO;
            newItem.IsModified = true;
            newItem.IsPhongBan = false;
            Items.Add(newItem);
        }

        protected override void OnDelete()
        {
            if (SelectedItem == null)
                return;
            SelectedItem.IsDeleted = !SelectedItem.IsDeleted;
        }
    }
}
