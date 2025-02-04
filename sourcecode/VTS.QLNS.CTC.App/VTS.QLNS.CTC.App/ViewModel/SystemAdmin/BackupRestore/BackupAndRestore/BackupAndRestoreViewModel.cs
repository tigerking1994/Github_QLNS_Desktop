using log4net;
using MaterialDesignThemes.Wpf;
using Microsoft.Extensions.Configuration;
using System;
using System.Data.SqlClient;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;
using VTS.QLNS.CTC.App.Command;
using VTS.QLNS.CTC.App.Helper;
using VTS.QLNS.CTC.App.Properties;
using VTS.QLNS.CTC.App.View.SystemAdmin.BackupRestore.BackupAndRestore;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Service;
using VTS.QLNS.CTC.Utility;
using VTS.QLNS.CTC.Utility.Enum;
using MessageBox = System.Windows.Forms.MessageBox;

namespace VTS.QLNS.CTC.App.ViewModel.SystemAdmin.BackupRestore.BackupAndRestore
{
    public class BackupAndRestoreViewModel : ViewModelBase
    {
        private readonly IConfiguration _configuration;
        private readonly ILog _logger;
        private readonly IDatabaseService _databaseService;
        private readonly IDanhMucService _danhMucService;
        private string _connectionString;
        private SqlConnectionStringBuilder _connectionStringBuilder;
        private ConnectionType _connectionType;

        public override string FuncCode => NSFunctionCode.SYSTEM_BACKUP_RESTORE_SCHEDULE_INDEX;
        public override string Name => "Sao lưu và phục hồi";
        public override string Description => "Sao lưu và phục hồi dữ liệu";
        public override Type ContentType => typeof(BackupAndRestoreIndex);
        public override PackIconKind IconKind => PackIconKind.CalendarMonth;

        private bool _isSetAuto;
        public bool IsSetAuto
        {
            get => _isSetAuto;
            set => SetProperty(ref _isSetAuto, value);
        }

        private bool _isRestore;
        public bool IsRestore
        {
            get => _isRestore;
            set => SetProperty(ref _isRestore, value);
        }

        private bool _isBackup;
        public bool IsBackup
        {
            get => _isBackup;
            set => SetProperty(ref _isBackup, value);
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

        private string _databaseInformation;
        public string DatabaseInformation
        {
            get => _databaseInformation;
            set => SetProperty(ref _databaseInformation, value);
        }

        public RelayCommand SelectFolderCommand { get; }
        public RelayCommand BackupCommand { get; }
        public RelayCommand RestoreCommand { get; }

        public BackupAndRestoreViewModel(IConfiguration configuration, IDanhMucService danhMucService, IDatabaseService databaseService, ILog log)
        {
            _configuration = configuration;
            _danhMucService = danhMucService;
            _databaseService = databaseService;
            _logger = log;

            SelectFolderCommand = new RelayCommand(obj => OnSelectFolder());
            BackupCommand = new RelayCommand(obj => OnBackup());
            RestoreCommand = new RelayCommand(obj => OnRestore());
        }

        public override void Init()
        {
            base.Init();
            LoadDatabaseInformation();
        }

        private void LoadDatabaseInformation()
        {
            _connectionType = _configuration.GetSection("DbSettings:ConnectionType").Value == ConnectionType.SqlServer.ToString() ? ConnectionType.SqlServer : ConnectionType.LocalDb;
            _connectionString = _configuration.GetConnectionString(_connectionType.ToString());
            _connectionStringBuilder = new SqlConnectionStringBuilder(_connectionString);
            _databaseInformation = string.Format("- Kiểu CSDL: {0} {1}- Server: {2} {3}- Database: {4} {5}",
                _connectionType.ToString(), Environment.NewLine,
                _connectionStringBuilder.DataSource, Environment.NewLine,
                _connectionStringBuilder.InitialCatalog, Environment.NewLine);
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

        private void OnBackup()
        {
            string fileName = _databaseService.GetBackupFilename(_connectionString, _connectionType.ToString());
            using SaveFileDialog dialog = new SaveFileDialog();
            dialog.FileName = fileName;
            dialog.Title = "Lưu file sao lưu dữ liệu";
            dialog.InitialDirectory = BackupFolder;
            var result = dialog.ShowDialog();
            if (result == DialogResult.OK)
            {
                string filePath = dialog.FileName;
                BackgroundWorkerHelper.Run((s, e) =>
                {
                    IsBackup = true;
                    if (_connectionType == ConnectionType.SqlServer)
                    {
                        _databaseService.BackupSqlServer(_connectionString, fileName, filePath);
                    }
                    else
                    {
                        _databaseService.BackupLocal(_connectionString, fileName, filePath);
                    }
                }, (s, e) =>
                {
                    IsBackup = false;
                    if (e.Error is null)
                    {
                        new NSMessageBoxViewModel(Resources.MsgBackupSuccess, Resources.NotifiTitle, NSMessageBoxButtons.OK, PackIconKind.Done, null).ShowDialogHost();
                    } else
                    {
                        new NSMessageBoxViewModel(Resources.MsgBackupError, Resources.NotifiTitle, NSMessageBoxButtons.OK, PackIconKind.Error, null).ShowDialogHost();
                    }
                });
            }
        }

        private void OnRestore()
        {
            new NSMessageBoxViewModel(Resources.MsgBeforeRestore, Resources.NotifiTitle, NSMessageBoxButtons.OK, PackIconKind.Warning, null).ShowDialogHost();
            using var openFileDialog = new OpenFileDialog
            {
                Title = "Chọn file sao lưu",
                RestoreDirectory = true,
                DefaultExt = StringUtils.ZIP_EXTENSION
            };
            if (openFileDialog.ShowDialog() != DialogResult.OK)
            {
                return;
            }

            string filePath = openFileDialog.FileName;

            var builder = new SqlConnectionStringBuilder(_connectionString);
            string mdfFileName = string.IsNullOrEmpty(builder.AttachDBFilename) ? builder.InitialCatalog.Split("\\").Last<string>() : builder.AttachDBFilename.Split("\\").Last();
            string dbFileName = string.IsNullOrEmpty(builder.AttachDBFilename) ? builder.InitialCatalog : builder.AttachDBFilename;

            BackgroundWorkerHelper.Run((s, e) =>
            {
                IsRestore = true;
                e.Result = _databaseService.RestoreLocal(_connectionString, filePath, mdfFileName);
            }, (s, e) =>
            {
                IsRestore = false;
                if (e.Error is null && e.Result is bool result)
                {
                    if (result)
                    {
                        new NSMessageBoxViewModel(Resources.MsgRestoreDatabaseSuccess, Resources.NotifiTitle, NSMessageBoxButtons.OK, PackIconKind.Done, null).ShowDialogHost();
                    } else
                    {
                        new NSMessageBoxViewModel(Resources.MsgRestoreDatabaseError, Resources.NotifiTitle, NSMessageBoxButtons.OK, PackIconKind.Error, null).ShowDialogHost();
                    }
                }
            });
        }
    }
}
