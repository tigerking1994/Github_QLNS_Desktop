using log4net;
using MaterialDesignThemes.Wpf;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Windows;
using System.Windows.Forms;
using VTS.QLNS.CTC.App.Command;
using VTS.QLNS.CTC.App.Model.Control;
using VTS.QLNS.CTC.App.Properties;
using VTS.QLNS.CTC.App.View.SystemAdmin.BackupRestore.Schedule;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Service;
using VTS.QLNS.CTC.Utility;

namespace VTS.QLNS.CTC.App.ViewModel.SystemAdmin.BackupRestore.Schedule
{
    public class ScheduleIndexViewModel : ViewModelBase
    {
        private readonly IConfiguration _configuration;
        private readonly ILog _loger;
        private readonly IDatabaseService _databaseService;
        private readonly IDanhMucService _danhMucService;
        private string _connectionString;
        private SqlConnectionStringBuilder _connectionStringBuilder;
        private ConnectionType _connectionType;

        public override string FuncCode => NSFunctionCode.SYSTEM_BACKUP_RESTORE_SCHEDULE_INDEX;
        public override string Name => "Lịch sao lưu";
        public override string Description => "Đặt lịch tự động sao lưu dữ liệu. Nếu sử dụng CSDL trên máy chủ thì phải thực hiện trên máy chủ";
        public override Type ContentType => typeof(ScheduleIndex);
        public override PackIconKind IconKind => PackIconKind.CalendarMonth;

        private bool _isSetAuto;
        public bool IsSetAuto
        {
            get => _isSetAuto;
            set => SetProperty(ref _isSetAuto, value);
        }

        private string _backupFolder;
        public string BackupFolder
        {
            get => _backupFolder;
            set => SetProperty(ref _backupFolder, value);
        }

        private ScheduleType _scheduleType;
        public ScheduleType ScheduleType
        {
            get => _scheduleType;
            set
            {
                SetProperty(ref _scheduleType, value);
                OnPropertyChanged(nameof(IsDay));
                OnPropertyChanged(nameof(IsWeek));
                OnPropertyChanged(nameof(IsMonth));
            }
        }

        public bool IsDay => ScheduleType == ScheduleType.Day;

        public bool IsWeek => ScheduleType == ScheduleType.Week;

        public bool IsMonth => ScheduleType == ScheduleType.Month;

        private DayType _dayType;
        public DayType DayType
        {
            get => _dayType;
            set
            {
                SetProperty(ref _dayType, value);
                OnPropertyChanged(nameof(IsDaily));
            }
        }

        public bool IsDaily => DayType == DayType.Daily;

        private List<ComboboxItem> _hours;
        public List<ComboboxItem> Hours
        {
            get => _hours;
            set => SetProperty(ref _hours, value);
        }

        private ComboboxItem _hourSelected;
        public ComboboxItem HourSelected
        {
            get => _hourSelected;
            set => SetProperty(ref _hourSelected, value);
        }

        private string _hoursStr;
        public string HoursStr
        {
            get => _hoursStr;
            set => SetProperty(ref _hoursStr, value);
        }

        private List<CheckBoxItem> _weekDays;
        public List<CheckBoxItem> WeekDays
        {
            get => _weekDays;
            set => SetProperty(ref _weekDays, value);
        }

        public string WeekDayValue => string.Join(",", _weekDays.Where(x => x.IsChecked).Select(x => x.DisplayItem));

        private List<CheckBoxItem> _months;
        public List<CheckBoxItem> Months
        {
            get => _months;
            set => SetProperty(ref _months, value);
        }

        public string MonthValue => string.Join(",", _months.Where(x => x.IsChecked).Select(x => x.DisplayItem));

        private List<CheckBoxItem> _dates;
        public List<CheckBoxItem> Dates
        {
            get => _dates;
            set => SetProperty(ref _dates, value);
        }

        public string DateValue => string.Join(",", _dates.Where(x => x.IsChecked).Select(x => x.DisplayItem));

        private List<ComboboxItem> _backupDateDelete;
        public List<ComboboxItem> BackupDateDelete
        {
            get => _backupDateDelete;
            set => SetProperty(ref _backupDateDelete, value);
        }

        private ComboboxItem _backupDateDeleteSelected;
        public ComboboxItem BackupDateDeleteSelected
        {
            get => _backupDateDeleteSelected;
            set
            {
                SetProperty(ref _backupDateDeleteSelected, value);
                OnPropertyChanged(nameof(BackupDateDeleteStr));
            }
        }

        public string BackupDateDeleteStr => string.Format("(Hệ thống sẽ tự động xóa files đã sao lưu trước {0} ngày)", _backupDateDeleteSelected.ValueItem);

        private string _databaseInformation;
        public string DatabaseInformation
        {
            get => _databaseInformation;
            set => SetProperty(ref _databaseInformation, value);
        }

        public RelayCommand SelectFolderCommand { get; }
        public RelayCommand BackupCommand { get; }
        public RelayCommand SaveCommand { get; }

        public ScheduleIndexViewModel(IConfiguration configuration, IDanhMucService danhMucService, IDatabaseService databaseService, ILog log)
        {
            _configuration = configuration;
            _danhMucService = danhMucService;
            _databaseService = databaseService;
            _loger = log;

            SelectFolderCommand = new RelayCommand(obj => OnSelectFolder());
            BackupCommand = new RelayCommand(obj => OnBackup());
            SaveCommand = new RelayCommand(obj => OnSaveConfig());
        }

        public override void Init()
        {
            base.Init();
            LoadHours();
            LoadWeekDays();
            LoadMonths();
            LoadDates();
            LoadBackupDateDelete();
            LoadDatabaseInformation();
            CheckHasConfig();
        }

        private void LoadDatabaseInformation()
        {
            _connectionType = _configuration.GetSection("DbSettings:ConnectionType").Value == ConnectionType.SqlServer.ToString() ? ConnectionType.SqlServer : ConnectionType.LocalDb;
            _connectionString = _configuration.GetConnectionString(_connectionType.ToString());
            _connectionStringBuilder = new SqlConnectionStringBuilder(_connectionString);
            _databaseInformation = string.Format("- Kiểu csdl: {0} {1}- Server: {2} {3}- Database: {4} {5}- Kiểu sao lưu: Phải sao lưu trực tiếp trên máy chủ",
                _connectionType.ToString(), Environment.NewLine, 
                _connectionStringBuilder.DataSource, Environment.NewLine, 
                _connectionType == ConnectionType.SqlServer ? _connectionStringBuilder.InitialCatalog : _connectionStringBuilder.AttachDBFilename, Environment.NewLine);
        }

        private void OnSelectFolder()
        {
            var dialog = new FolderBrowserDialog();
            DialogResult result = dialog.ShowDialog();
            if (result == DialogResult.OK)
            {
                BackupFolder = dialog.SelectedPath;
                var folderBackupConfig = new DanhMuc()
                {
                    SType = "DM_CauHinh",
                    IIDMaDanhMuc = DMCauHinh.BACKUP_009.ToString(),
                    STen = "Thư mục sao lưu dữ liệu",
                    SGiaTri = BackupFolder,
                    SMoTa = "Thư mục sao lưu dữ liệu"
                };
                var predicate = PredicateBuilder.True<DanhMuc>();
                predicate = predicate.And(x => x.SType == "DM_CauHinh" && x.IIDMaDanhMuc == DMCauHinh.BACKUP_009.ToString());
                bool isExist = _danhMucService.FindByCondition(predicate).ToList().Count > 0;
                if (!isExist)
                    _danhMucService.Add(folderBackupConfig);
                else _danhMucService.Update(folderBackupConfig);
            }
        }

        private void LoadHours()
        {
            _hours = new List<ComboboxItem>();
            for (int i = 0; i < 24; i++)
            {
                _hours.Add(new ComboboxItem(i.ToString(), i.ToString()));
            }
        }

        private void LoadWeekDays()
        {
            _weekDays = new List<CheckBoxItem>();
            for (int i = 2; i < 8; i++)
            {
                _weekDays.Add(new CheckBoxItem { DisplayItem = "Thứ " + i, ValueItem = i.ToString() });
            }
            _weekDays.Add(new CheckBoxItem { DisplayItem = "Chủ nhật", ValueItem = "8" });
            foreach (var item in _weekDays)
            {
                item.PropertyChanged += (sender, args) =>
                {
                    OnPropertyChanged(nameof(WeekDayValue));
                };
            }
        }

        private void LoadMonths()
        {
            _months = new List<CheckBoxItem>();
            for (int i = 1; i < 13; i++)
            {
                _months.Add(new CheckBoxItem { DisplayItem = "Tháng " + i, ValueItem = i.ToString() });
            }
            foreach (var item in _months)
            {
                item.PropertyChanged += (sender, args) =>
                {
                    OnPropertyChanged(nameof(MonthValue));
                };
            }
        }
        private void LoadDates()
        {
            _dates = new List<CheckBoxItem>();
            for (int i = 1; i < 32; i++)
            {
                _dates.Add(new CheckBoxItem { DisplayItem = i.ToString(), ValueItem = i.ToString() });
            }
            foreach (var item in _dates)
            {
                item.PropertyChanged += (sender, args) =>
                {
                    OnPropertyChanged(nameof(DateValue));
                };
            }
        }

        private void LoadBackupDateDelete()
        {
            _backupDateDelete = new List<ComboboxItem>
            {
                new ComboboxItem("5", "5"),
                new ComboboxItem("15", "15"),
                new ComboboxItem("20", "20"),
                new ComboboxItem("30", "30"),
                new ComboboxItem("60", "60"),
            };
        }

        private void OnBackup()
        {
            if (string.IsNullOrEmpty(BackupFolder))
            {
                System.Windows.MessageBox.Show(Resources.AlertSelectBackupFolder, Resources.NotifiTitle, MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            string fileName = _databaseService.GetBackupFilename(_connectionString, _connectionType.ToString());
            SaveFileDialog dialog = new SaveFileDialog();
            dialog.FileName = fileName;
            dialog.Title = "Lưu file sao lưu dữ liệu";
            dialog.InitialDirectory = BackupFolder;
            var result = dialog.ShowDialog();
            if (result == DialogResult.OK)
            {
                string filePath = dialog.FileName;
                try
                {
                    if (_connectionType == ConnectionType.SqlServer)
                        _databaseService.BackupSqlServer(_connectionString, fileName, filePath);
                    else _databaseService.BackupLocal(_connectionString, fileName, filePath);
                    System.Windows.MessageBox.Show(Resources.MsgBackupSuccess, Resources.NotifiTitle, MessageBoxButton.OK, MessageBoxImage.Information);
                }
                catch (Exception ex)
                {
                    _loger.Error(ex);
                    System.Windows.MessageBox.Show(Resources.MsgBackupError, Resources.NotifiTitle, MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
        }

        private void OnSaveConfig()
        {
            List<DanhMuc> danhMucList = new List<DanhMuc>()
            {
                new DanhMuc()
                {
                    SType = "DM_CauHinh",
                    IIDMaDanhMuc = DMCauHinh.BACKUP_001.ToString(),
                    STen = "Sao lưu tự động hay không",
                    SGiaTri = IsSetAuto ? "1" : "0",
                    SMoTa = "0: Không, 1: Tự động"
                },
                new DanhMuc()
                {
                    SType = "DM_CauHinh",
                    IIDMaDanhMuc = DMCauHinh.BACKUP_002.ToString(),
                    STen = "Sao lưu tự động theo ngày, tuần, tháng",
                    SGiaTri = ((int)ScheduleType).ToString(),
                    SMoTa = "1: Ngày, 2: Tuần, 3: Tháng"
                },
                new DanhMuc()
                {
                    SType = "DM_CauHinh",
                    IIDMaDanhMuc = DMCauHinh.BACKUP_003.ToString(),
                    STen = "Sao lưu tự động trong ngày, hàng ngày",
                    SGiaTri = ((int)DayType).ToString(),
                    SMoTa = "1: Hàng ngày, 2: Giờ"
                },
                new DanhMuc()
                {
                    SType = "DM_CauHinh",
                    IIDMaDanhMuc = DMCauHinh.BACKUP_004.ToString(),
                    STen = "Giờ sao lưu hàng ngày",
                    SGiaTri = HourSelected != null ? HourSelected.ValueItem : string.Empty,
                    SMoTa = "Giờ hàng ngày sẽ tự động sao lưu dữ liệu"
                },
                new DanhMuc()
                {
                    SType = "DM_CauHinh",
                    IIDMaDanhMuc = DMCauHinh.BACKUP_005.ToString(),
                    STen = "Giờ sao lưu trong ngày",
                    SGiaTri = HoursStr,
                    SMoTa = "Giờ trong ngày sẽ tự động sao lưu dữ liệu"
                },
                new DanhMuc()
                {
                    SType = "DM_CauHinh",
                    IIDMaDanhMuc = DMCauHinh.BACKUP_006.ToString(),
                    STen = "Ngày sao lưu trong tuần",
                    SGiaTri = string.Join(",", WeekDays.Where(x => x.IsChecked).Select(x => x.ValueItem)),
                    SMoTa = "Ngày trong tuần tự động sao lưu dữ liệu"
                },
                new DanhMuc()
                {
                    SType = "DM_CauHinh",
                    IIDMaDanhMuc = DMCauHinh.BACKUP_007.ToString(),
                    STen = "Tháng sao lưu dữ liệu",
                    SGiaTri = string.Join(",", Months.Where(x => x.IsChecked).Select(x => x.ValueItem)),
                    SMoTa = "Tháng tự động sao lưu dữ liệu"
                },
                new DanhMuc()
                {
                    SType = "DM_CauHinh",
                    IIDMaDanhMuc = DMCauHinh.BACKUP_008.ToString(),
                    STen = "Ngày trong tháng sao lưu dữ liệu",
                    SGiaTri = string.Join(",", Dates.Where(x => x.IsChecked).Select(x => x.ValueItem)),
                    SMoTa = "Ngày trong tháng tự động sao lưu dữ liệu"
                },
                new DanhMuc()
                {
                    SType = "DM_CauHinh",
                    IIDMaDanhMuc = DMCauHinh.BACKUP_009.ToString(),
                    STen = "Thư mục sao lưu dữ liệu",
                    SGiaTri = BackupFolder,
                    SMoTa = "Thư mục sao lưu dữ liệu"
                },
                new DanhMuc()
                {
                    SType = "DM_CauHinh",
                    IIDMaDanhMuc = DMCauHinh.BACKUP_010.ToString(),
                    STen = "Xóa dữ liệu",
                    SGiaTri = BackupDateDeleteSelected.ValueItem,
                    SMoTa = "Hệ thống sẽ tự động xóa file sao lưu trước"
                }
            };
            foreach (var item in danhMucList)
            {
                var predicate = PredicateBuilder.True<DanhMuc>();
                predicate = predicate.And(x => x.SType == "DM_CauHinh" && x.IIDMaDanhMuc == item.IIDMaDanhMuc);
                bool isExist = _danhMucService.FindByCondition(predicate).ToList().Count > 0;
                if (!isExist)
                    _danhMucService.Add(item);
                else _danhMucService.Update(item);
            }
            System.Windows.MessageBox.Show(Resources.MsgSaveDone, Resources.NotifiTitle, MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void CheckHasConfig()
        {
            var predicate = PredicateBuilder.True<DanhMuc>();
            predicate = predicate.And(x => x.SType == "DM_CauHinh" && x.IIDMaDanhMuc.Contains("BACKUP"));
            List<DanhMuc> danhMucs = _danhMucService.FindByCondition(predicate).ToList();
            if (danhMucs.Count == 0)
            {
                _scheduleType = ScheduleType.Day;
                _dayType = DayType.Daily;
                _backupDateDeleteSelected = _backupDateDelete.First();
            }
            else
            {
                _isSetAuto = danhMucs.Where(x => x.IIDMaDanhMuc == DMCauHinh.BACKUP_001.ToString()).First().SGiaTri == "1";
                _scheduleType = (ScheduleType)Enum.Parse(typeof(ScheduleType), danhMucs.Where(x => x.IIDMaDanhMuc == DMCauHinh.BACKUP_002.ToString()).First().SGiaTri);
                _dayType = (DayType)Enum.Parse(typeof(DayType), danhMucs.Where(x => x.IIDMaDanhMuc == DMCauHinh.BACKUP_003.ToString()).First().SGiaTri);
                _hourSelected = _hours.Where(x => x.ValueItem == danhMucs.Where(x => x.IIDMaDanhMuc == DMCauHinh.BACKUP_004.ToString()).First().SGiaTri).FirstOrDefault();
                _hoursStr = danhMucs.Where(x => x.IIDMaDanhMuc == DMCauHinh.BACKUP_005.ToString()).First().SGiaTri;
                _weekDays.Where(x => danhMucs.Where(x => x.IIDMaDanhMuc == DMCauHinh.BACKUP_006.ToString()).First().SGiaTri.Contains(x.ValueItem)).ToList().ForEach(x => x.IsChecked = true);
                _months.Where(x => danhMucs.Where(x => x.IIDMaDanhMuc == DMCauHinh.BACKUP_007.ToString()).First().SGiaTri.Contains(x.ValueItem)).ToList().ForEach(x => x.IsChecked = true);
                _dates.Where(x => danhMucs.Where(x => x.IIDMaDanhMuc == DMCauHinh.BACKUP_008.ToString()).First().SGiaTri.Contains(x.ValueItem)).ToList().ForEach(x => x.IsChecked = true);
                _backupFolder = danhMucs.Where(x => x.IIDMaDanhMuc == DMCauHinh.BACKUP_009.ToString()).First().SGiaTri;
                _backupDateDeleteSelected = _backupDateDelete.Where(x => x.ValueItem == danhMucs.Where(x => x.IIDMaDanhMuc == DMCauHinh.BACKUP_010.ToString()).First().SGiaTri).First();
            }
        }
    }
}
