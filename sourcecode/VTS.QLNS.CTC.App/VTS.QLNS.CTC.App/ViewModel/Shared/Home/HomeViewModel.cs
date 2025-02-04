using AutoMapper;
using log4net;
using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using VTS.QLNS.CTC.App.Command;
using VTS.QLNS.CTC.App.Model.Control;
using VTS.QLNS.CTC.App.Service;
using VTS.QLNS.CTC.App.View.Shared.Home;
using VTS.QLNS.CTC.App.ViewModel.Shared;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Service;
using VTS.QLNS.CTC.Utility;

namespace VTS.QLNS.CTC.App.ViewModel
{
    public class HomeViewModel : ViewModelBase
    {
        private readonly ISessionService _sessionService;
        private readonly IDanhMucService _dmService;
        private readonly INsNguonNganSachService _nsNguonNganSachService;
        private readonly IMapper _mapper;
        private readonly INsDonViService _nsDonViService;
        private readonly IUserService _userService;
        private readonly ILog _logger;

        public override string Name => "TRANG CHỦ";
        public override Type ContentType => typeof(Home);
        public override PackIconKind IconKind => PackIconKind.Home;
        public UpdateDataYearOfWorkViewModel _updateDataYearOfWorkViewModel;
        public SettingsConfigViewModel _settingsConfigViewModel;

        public ObservableCollection<ComboboxItem> NamNganSach { get; set; }
        public ObservableCollection<ComboboxItem> NguonNganSach { get; set; }
        public ObservableCollection<ComboboxItem> Months { get; set; }
        public ObservableCollection<ComboboxItem> Years { get; set; }

        private ComboboxItem _selectedNamNganSach;
        public ComboboxItem SelectedNamNganSach
        {
            get => _selectedNamNganSach;
            set
            {
                SetProperty(ref _selectedNamNganSach, value);
                if (_selectedNamNganSach != null && _sessionService != null && _sessionService.Current != null)
                {
                    _sessionService.Current.YearOfBudget = int.Parse(_selectedNamNganSach.ValueItem);
                }
            }
        }

        private int? _selectedNguonNganSach;
        public int? SelectedNguonNganSach
        {
            get => _selectedNguonNganSach;
            set
            {
                SetProperty(ref _selectedNguonNganSach, value);
                if (_selectedNguonNganSach != null && _sessionService != null && _sessionService.Current != null)
                {
                    _sessionService.Current.Budget = _selectedNguonNganSach.Value;
                }
            }
        }

        private int? _month;
        public int? Month
        {
            get => _month;
            set
            {
                SetProperty(ref _month, value);
                if (_month != null && _sessionService != null && _sessionService.Current != null)
                {
                    _sessionService.Current.Month = _month.Value;
                }
            }
        }

        private int? _year;
        public int? Year
        {
            get => _year;
            set
            {
                SetProperty(ref _year, value);
                if (_year != null && _sessionService != null && _sessionService.Current != null)
                {
                    _sessionService.Current.YearOfWork = _year.Value;
                    ReloadDonViQuanly();
                }
            }
        }

        public bool IsAfterLogIn { get; set; }

        public RelayCommand OpenUpdateDataYearOfWorkDialogCommand { get; }
        public RelayCommand CopyDataCommand { get; }

        public HomeViewModel(ISessionService sessionService,
            IDanhMucService danhMucService,
            IMapper mapper,
            ILog logger,
            UpdateDataYearOfWorkViewModel updateDataYearOfWorkViewModel,
            SettingsConfigViewModel settingsConfigViewModel,
            INsNguonNganSachService nsNguonNganSachService,
            INsDonViService nsDonViService,
            IUserService userService)
        {
            _sessionService = sessionService;
            _dmService = danhMucService;
            _userService = userService;
            _mapper = mapper;
            _logger = logger;
            _updateDataYearOfWorkViewModel = updateDataYearOfWorkViewModel;
            _settingsConfigViewModel = settingsConfigViewModel;
            _nsNguonNganSachService = nsNguonNganSachService;
            _nsDonViService = nsDonViService;

            //OpenUpdateDataYearOfWorkDialogCommand = new RelayCommand(obj => OnOpenUpdateDataYearOfWorkDialog());
            OpenUpdateDataYearOfWorkDialogCommand = new RelayCommand(obj => InitMasterData(), o => Year.HasValue);
            CopyDataCommand = new RelayCommand(obj => OnOpenUpdateDataYearOfWorkDialog());
        }

        public override void Init()
        {
            base.Init();
            NamNganSach = new ObservableCollection<ComboboxItem>();
            IEnumerable<DanhMuc> danhMucNamNS = _dmService.FindByType("NS_NamNganSach").OrderBy(n => n.STen).Where(x => x.IIDMaDanhMuc != "1" && x.IIDMaDanhMuc != "4").ToList();
            foreach (DanhMuc namNS in danhMucNamNS)
            {
                NamNganSach.Add(new ComboboxItem { DisplayItem = namNS.STen, ValueItem = namNS.IIDMaDanhMuc });
            }

            SelectedNamNganSach = NamNganSach.FirstOrDefault(x => x.ValueItem == "2");
            // SelectedNamNganSach = _sessionService?.Current?.YearOfBudget != 0 ?
            // _sessionService?.Current?.YearOfBudget : (NamNganSach.Count == 0 ? 0 : int.Parse(NamNganSach.FirstOrDefault(n => n.ValueItem.Equals("2")).ValueItem));

            NguonNganSach = new ObservableCollection<ComboboxItem>();
            IEnumerable<DanhMuc> danhMucNguonNS = _dmService.FindByType("NS_NguonNganSach").ToList();
            // var danhMucNguonNS = _nsNguonNganSachService.FindNguonNganSach().Where(n => n.ITrangThai == 1);
            foreach (DanhMuc nguonNS in danhMucNguonNS)
            {
                NguonNganSach.Add(new ComboboxItem { DisplayItem = nguonNS.STen, ValueItem = nguonNS.IIDMaDanhMuc.ToString() });
            }
            SelectedNguonNganSach = _sessionService?.Current?.Budget != 0 ?
                _sessionService?.Current?.Budget : (NguonNganSach.Count == 0 ? 0 : int.Parse(NguonNganSach.FirstOrDefault().ValueItem));

            Months = new ObservableCollection<ComboboxItem>();
            for (int i = 1; i <= 12; i++)
            {
                Months.Add(new ComboboxItem { DisplayItem = i.ToString(), ValueItem = i.ToString() });
            }
            Month = _sessionService?.Current?.Month != 0 ? _sessionService?.Current?.Month : DateTime.Now.Month;

            Years = new ObservableCollection<ComboboxItem>();
            IEnumerable<DanhMuc> danhMucNamLV = _dmService.FindByType("NS_NamLamViec").OrderBy(c => c.INamLamViec).ToList();
            foreach (DanhMuc namLV in danhMucNamLV)
            {
                Years.Add(new ComboboxItem { DisplayItem = namLV.SGiaTri, ValueItem = namLV.SGiaTri });
            }

            if (_sessionService?.Current?.YearOfWork != null
                && Years.Any(x => x.ValueItem == _sessionService.Current.YearOfWork.ToString()))
            {
                Year = _sessionService.Current.YearOfWork;
            }
            else
            {
                Year = int.TryParse(Years.LastOrDefault().ValueItem, out int value) ? value : 0;
            }

            if (_nsDonViService.CountDonVi() == 0 && IsAfterLogIn)
            {
                InitMasterData();
                IsAfterLogIn = false;
            }
        }

        private void OnOpenUpdateDataYearOfWorkDialog()
        {
            /*if (!Year.HasValue || Years.Count == 0)
                return;*/
            if (Year.HasValue)
            {
                _updateDataYearOfWorkViewModel.DestinationYear = Year.Value;
            }
            else
            {
                _updateDataYearOfWorkViewModel.DestinationYear = null;
            }
            _updateDataYearOfWorkViewModel.Years = new ObservableCollection<ComboboxItem>(Years.ToList());
            _updateDataYearOfWorkViewModel.ParentPage = this;
            _updateDataYearOfWorkViewModel.SourceYear = int.Parse(_updateDataYearOfWorkViewModel.Years.FirstOrDefault().ValueItem);
            _updateDataYearOfWorkViewModel.Init();

            UpdateDataYearOfWorkView UpdateDataYearOfWorkViewDialog = new UpdateDataYearOfWorkView()
            {
                DataContext = _updateDataYearOfWorkViewModel
            };
            System.Threading.Tasks.Task<object> dialog = DialogHost.Show(UpdateDataYearOfWorkViewDialog, "RootDialog", OnClose);
        }

        private void OnClose(object sender, DialogClosingEventArgs eventArgs)
        {
            int? selectedYear = Year;
            Years.Clear();
            IEnumerable<DanhMuc> danhMucNamLV = _dmService.FindByType("NS_NamLamViec").ToList();
            foreach (DanhMuc namLV in danhMucNamLV)
            {
                Years.Add(new ComboboxItem { DisplayItem = namLV.SGiaTri, ValueItem = namLV.SGiaTri });
            }
            Year = selectedYear;
        }

        private void ReloadDonViQuanly()
        {
            try
            {
                string idsDonViQuanLy = _sessionService.Current.IdsDonViQuanLy;
                int namLamViec = _sessionService.Current.YearOfWork;
                DonVi donVi = _nsDonViService.FindByLoai(LoaiDonVi.ROOT, namLamViec);
                if (donVi != null)
                {
                    _sessionService.Current.TenDonVi = donVi.TenDonVi;
                    _sessionService.Current.IdDonVi = donVi.IIDMaDonVi;
                }

                HtNguoiDung sysUser = _userService.FindByPredicate(u => _sessionService.Current.Principal.Equals(u.STaiKhoan), namLamViec);
                List<string> maDonVis = sysUser.NsNguoiDungDonVis.Select(t => t.IIdMaDonVi).ToList();
                List<string> maPhanHos = sysUser.TlNguoiDungPhanHos.Select(t => t.IIdMaDonVi).ToList();
                _sessionService.Current.DonViQuanLy = _nsDonViService.FindByCondition(x => maDonVis.Contains(x.IIDMaDonVi) && namLamViec == x.NamLamViec).ToList();
                _sessionService.Current.IdsDonViQuanLy = string.Join(",", maDonVis);
                _sessionService.Current.IdsPhanHoQuanLy = string.Join(",", maPhanHos);
            }
            catch (Exception ex) {
                _logger.Error($"Lỗi CSDL phần hệ thống: {ex.Message}");
            }
        }

        private void InitMasterData()
        {
            if (!Year.HasValue)
            {
                return;
            }
            _settingsConfigViewModel.NamLamViec = Year.Value;
            _settingsConfigViewModel.Init();
            _settingsConfigViewModel.ShowDialog();
        }
    }
}
