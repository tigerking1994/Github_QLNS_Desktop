using AutoMapper;
using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using VTS.QLNS.CTC.App.Command;
using VTS.QLNS.CTC.App.Model;
using VTS.QLNS.CTC.App.Properties;
using VTS.QLNS.CTC.App.Service;
using VTS.QLNS.CTC.App.View.Shared.Settings;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Service;
using VTS.QLNS.CTC.Utility;
using VTS.QLNS.CTC.App.Model.Control;

namespace VTS.QLNS.CTC.App.ViewModel.Shared.Settings
{
    public class SettingsDonViSuDungViewModel : DialogViewModelBase<DonViModel>
    {
        private INsDonViService _nsDonViService;
        private IMapper _mapper;
        private ISessionService _sessionService;

        public override Type ContentType => typeof(SettingDonViSuDung);
        public override string Name => "CẤU HÌNH ĐƠN VỊ SỬ DỤNG";
        public override string Description => "Cấu hình đơn vị sử dụng";
        public ObservableCollection<ComboboxItem> NamLamViecs { get; set; }

        public RelayCommand LoadExistCommand { get; }

        private int _settingNamLamViec;
        public int SettingNamLamViec
        {
            get => _settingNamLamViec;
            set
            {
                SetProperty(ref _settingNamLamViec, value);
                LoadExistData();
                SettingsConfigViewModel settingsConfigViewModel = ParentPage as SettingsConfigViewModel;
                settingsConfigViewModel.NamLamViec = value;
            }
        }

        public SettingsDonViSuDungViewModel(INsDonViService nsDonViService,
            ISessionService sessionService,
            IMapper mapper)
        {
            _mapper = mapper;
            _nsDonViService = nsDonViService;
            _sessionService = sessionService;
            LoadExistCommand = new RelayCommand(obj => ReloadData());
        }

        public override void Init()
        {            
            SettingsConfigViewModel settingsConfigViewModel = ParentPage as SettingsConfigViewModel;
            settingsConfigViewModel.SaveAndNextPageCommand = new RelayCommand(obj => OnSaveAndGoNext(settingsConfigViewModel), CanSave);
            settingsConfigViewModel.SaveAndCloseCommand = new RelayCommand(obj => OnSaveAndClose(obj), CanSave);
            settingsConfigViewModel.SaveCommand = new RelayCommand(obj => SaveData(), CanSave);
            settingsConfigViewModel.Name = this.Name;
            int currentYear = DateTime.Now.Year;
            NamLamViecs = new ObservableCollection<ComboboxItem>();
            for(int i = currentYear - 10; i < currentYear + 10; i++)
            {
                NamLamViecs.Add(new ComboboxItem
                {
                    DisplayItem = i.ToString(),
                    ValueItem = i.ToString()
                });
            }
            SettingNamLamViec = settingsConfigViewModel.NamLamViec;
        }

        private bool CanSave(object arg)
        {
            return !string.IsNullOrWhiteSpace(Model.IIDMaDonVi) && !string.IsNullOrWhiteSpace(Model.TenDonVi) && Model.iCapDonVi.HasValue && SettingNamLamViec != null;
        }

        private void OnSaveAndClose(object obj)
        {
            if (SaveData())
            {
                var wd = obj as Window;
                wd.Close();
            }
        }

        public bool SaveData()
        {
            if (!ValidateDVSD())
            {
                MessageBox.Show("Trường thông tin mã đơn vị và tên đơn vị không được bỏ trống", Resources.ConfirmTitle, MessageBoxButton.YesNo, MessageBoxImage.Question);
                return false;
            }
            var result = MessageBox.Show("Đồng chí có chắc chắn lưu dữ liệu?", Resources.ConfirmTitle, MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                DonVi checkExistDonVi = _nsDonViService.FindByMaDonViAndNamLamViec(Model.IIDMaDonVi, SettingNamLamViec);
                if (checkExistDonVi != null)
                {
                    string confirmMessage = "Đơn vị với mã {0} đã tồn tại, đồng chí có chắc chắn muốn lưu lại không?";
                    var confirmOverride = MessageBox.Show(string.Format(confirmMessage, Model.IIDMaDonVi), Resources.ConfirmTitle, MessageBoxButton.YesNo, MessageBoxImage.Question);
                    if (confirmOverride == MessageBoxResult.Yes)
                    {
                        SaveDonViSuDung();
                        return true;
                    }
                }
                else
                {
                    SaveDonViSuDung();
                    return true;
                }
            }
            return false;
        }

        private bool ValidateDVSD()
        {
            return !string.IsNullOrWhiteSpace(Model.IIDMaDonVi) && !string.IsNullOrWhiteSpace(Model.TenDonVi);
        }

        public void OnSaveAndGoNext(SettingsConfigViewModel parent)
        {
            if (SaveData())
            {
                parent.NextPage();
            }
        }

        private void SaveDonViSuDung()
        {
            DonVi donVi = _mapper.Map<DonVi>(Model);
            donVi.Loai = LoaiDonVi.ROOT;
            donVi.NamLamViec = SettingNamLamViec;
            _nsDonViService.SaveDonViSuDung(donVi, SettingNamLamViec);
        }

        private void ReloadData()
        {
            var result = MessageBox.Show("Dữ liệu đã nhập sẽ mất, Đồng chí có chắc chắn muốn tải dữ liệu?", Resources.ConfirmTitle, MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                LoadExistData();
            }
        }

        private void LoadExistData()
        {
            DonVi donvi = _nsDonViService.FindCurrentDonViSuDungByNamLamViec(SettingNamLamViec);
            if (donvi != null)
            {
                Model = _mapper.Map<DonViModel>(donvi);
            }
            else
            {
                Model = new DonViModel();
                Model.IsPhongBan = false;
            }
        }
    }
}
