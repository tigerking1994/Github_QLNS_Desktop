using MaterialDesignThemes.Wpf;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using VTS.QLNS.CTC.App.Command;
using VTS.QLNS.CTC.App.Model;
using VTS.QLNS.CTC.App.Properties;
using VTS.QLNS.CTC.App.View.SystemAdmin.BackupRestore.File;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Service;
using VTS.QLNS.CTC.Utility;

namespace VTS.QLNS.CTC.App.ViewModel.SystemAdmin.BackupRestore.File
{
    public class BackupFileIndexViewModel : ViewModelBase
    {
        private readonly IConfiguration _configuration;
        private readonly IDanhMucService _danhMucService;
        private readonly IDatabaseService _databaseService;
        private string _backupFolder;
        private ConnectionType _connectionType;
        private string _connectionString;

        public override string FuncCode => NSFunctionCode.SYSTEM_BACKUP_RESTORE_FILE_INDEX;
        public override string Name => "File sao lưu";
        public override string Description => "File sao lưu";
        public override Type ContentType => typeof(BackupFileIndex);
        public override PackIconKind IconKind => PackIconKind.FileRestore;

        private ObservableCollection<BackupFileModel> _backupFiles;
        public ObservableCollection<BackupFileModel> BackupFiles
        {
            get => _backupFiles;
            set => SetProperty(ref _backupFiles, value);
        }

        private bool _isAllItemsSelected;
        public bool IsAllItemsSelected
        {
            get => _backupFiles != null && _backupFiles.All(x => x.Selected) && _backupFiles.Count() > 0;
            set
            {
                SetProperty(ref _isAllItemsSelected, value);
                _backupFiles.ToList().ForEach(x => x.Selected = _isAllItemsSelected);
            }
        }

        public bool IsDelete => _backupFiles != null && _backupFiles.Where(x => x.Selected).Count() > 0;
        public bool IsRestore => _backupFiles != null && _backupFiles.Where(x => x.Selected).Count() == 1;

        public RelayCommand ReloadCommand { get; }
        public RelayCommand OpenFolderCommand { get; }
        public RelayCommand DeleteCommand { get; }
        public RelayCommand RestoreCommand { get; }

        public BackupFileIndexViewModel(IDanhMucService danhMucService, IConfiguration configuration, IDatabaseService databaseService)
        {
            _danhMucService = danhMucService;
            _configuration = configuration;
            _databaseService = databaseService;

            ReloadCommand = new RelayCommand(obj => OnReload());
            OpenFolderCommand = new RelayCommand(obj => OnOpenFolder());
            DeleteCommand = new RelayCommand(obj => OnDelete());
            RestoreCommand = new RelayCommand(obj => OnRestore());
        }

        public override void Init()
        {
            base.Init();
            _connectionType = _configuration.GetSection("DbSettings:ConnectionType").Value == ConnectionType.SqlServer.ToString() ? ConnectionType.SqlServer : ConnectionType.LocalDb;
            _connectionString = _configuration.GetConnectionString(_connectionType.ToString());
            LoadData();
        }

        private void LoadData()
        {
            DanhMuc danhMucBackupFolder = _danhMucService.FindByCode(DMCauHinh.BACKUP_009.ToString());
            if (danhMucBackupFolder != null)
            {
                _backupFolder = danhMucBackupFolder.SGiaTri;
                GetBackupFiles();
            }
        }

        private void GetBackupFiles()
        {
            if (!Directory.Exists(_backupFolder)) return;
            string[] files = Directory.GetFiles(_backupFolder, "*.zip");
            List<BackupFileModel> listBackupFile = new List<BackupFileModel>();
            foreach (var file in files)
            {
                listBackupFile.Add(CreateFromFilename(file));
            }
            _backupFiles = new ObservableCollection<BackupFileModel>(listBackupFile);
            foreach (var item in _backupFiles)
            {
                item.PropertyChanged += (sender, args) =>
                {
                    OnPropertyChanged(nameof(IsAllItemsSelected));
                    OnPropertyChanged(nameof(IsDelete));
                    OnPropertyChanged(nameof(IsRestore));
                };
            }
        }

        private void OnReload()
        {
            LoadData();
            OnPropertyChanged(nameof(BackupFiles));
            OnPropertyChanged(nameof(IsAllItemsSelected));
            OnPropertyChanged(nameof(IsDelete));
            OnPropertyChanged(nameof(IsRestore));
        }

        private void OnOpenFolder()
        {
            //var psi = new ProcessStartInfo()
            //{
            //    FileName = _backupFolder,
            //    UseShellExecute = true
            //};
            //Process.Start(psi);

            //string fileName = _databaseService.GetBackupFilename(_connectionString, _connectionType.ToString());
            OpenFileDialog dialog = new OpenFileDialog();
            if (dialog.ShowDialog() == DialogResult.OK)
                System.IO.File.Copy(dialog.FileName, _backupFolder + "\\" + dialog.SafeFileName);

        }

        private void OnDelete()
        {
            var result = MessageBox.Show(Resources.MsgConfirmDeleteBackupFile, Resources.NotifiTitle, MessageBoxButtons.YesNo, MessageBoxIcon.Information);
            if (result == DialogResult.Yes)
            {
                List<string> deleteFiles = _backupFiles.Where(x => x.Selected).Select(x => x.FilePath).ToList();
                foreach (var file in deleteFiles)
                {
                    if (System.IO.File.Exists(file))
                        System.IO.File.Delete(file);
                }
                MessageBox.Show(string.Format(Resources.MsgDeleteBackupFileSuccess, deleteFiles.Count), Resources.NotifiTitle, MessageBoxButtons.OK, MessageBoxIcon.Information);
                OnReload();
            }
        }

        private void OnRestore()
        {
            BackupFileModel backupFile = _backupFiles.Where(x => x.Selected).FirstOrDefault();
            string message = string.Format(Resources.MsgConfirmRestoreDatabase, Environment.NewLine, Environment.NewLine, backupFile.DatabaseName, Environment.NewLine, backupFile.DatabaseType, Environment.NewLine, backupFile.DateCreated, Environment.NewLine, backupFile.FilePath);
            var result = MessageBox.Show(message, "Xác nhận phục hồi cơ sở dữ liệu", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
            if (result == DialogResult.Yes)
            {
                //if (_connectionType == ConnectionType.SqlServer)
                //    RestoreSqlServer(backupFile);
                //else
                RestoreLocal(backupFile);
            }

        }

        private void RestoreSqlServer(BackupFileModel backupFile)
        {
            try
            {
                //using (ZipArchive zipArchive = Ionic.Zip.ZipFile.OpenRead(backupFile.FilePath))
                //{
                //    zipArchive.ExtractToDirectory(_backupFolder);
                //    string filePath = backupFile.FilePath.Replace("zip", "bak");
                //    _databaseService.RestoreSqlServer(_connectionString, backupFile.DatabaseName, filePath);
                //    System.IO.File.Delete(filePath);
                //    MessageBox.Show(Resources.MsgRestoreDatabaseSuccess, Resources.NotifiTitle, MessageBoxButtons.OK, MessageBoxIcon.Information);
                //}
            }
            catch
            {
                MessageBox.Show(Resources.MsgRestoreDatabaseError, Resources.NotifiTitle, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void RestoreLocal(BackupFileModel backupFile)
        {
            var builder = new SqlConnectionStringBuilder(_connectionString);
            string mdfFileName = string.IsNullOrEmpty(builder.AttachDBFilename) ? builder.InitialCatalog.Split("\\").Last<string>() : builder.AttachDBFilename.Split("\\").Last();
            string dbFileName = string.IsNullOrEmpty(builder.AttachDBFilename) ? builder.InitialCatalog : builder.AttachDBFilename;
            
            var isSuccess = _databaseService.RestoreLocal(_connectionString, backupFile.FilePath, mdfFileName);
            if (isSuccess)
                MessageBox.Show(Resources.MsgRestoreDatabaseSuccess, Resources.NotifiTitle, MessageBoxButtons.OK, MessageBoxIcon.Information);
            else
                MessageBox.Show(Resources.MsgRestoreDatabaseError, Resources.NotifiTitle, MessageBoxButtons.OK, MessageBoxIcon.Warning);
           
        }

        private BackupFileModel CreateFromFilename(string fileName)
        {
            FileInfo fileInfo = new FileInfo(fileName);
            string name = fileInfo.Name;
            List<string> list = name.Replace(".zip", "").Split("___").ToList();
            BackupFileModel backupFile = new BackupFileModel()
            {
                DatabaseName = list[0],
                DatabaseType = "", //list[1],
                AppVersion = "",// list[2],
                DateCreated = fileInfo.CreationTime,
                FileName = name,
                FilePath = fileInfo.FullName,
                FileSize = BytesToString(fileInfo.Length)
            };
            return backupFile;
        }

        private static string BytesToString(long byteCount)
        {
            string[] strArray = new string[7]
            {
                "B",
                "KB",
                "MB",
                "GB",
                "TB",
                "PB",
                "EB"
            };
            if (byteCount == 0L)
                return "0" + strArray[0];
            long num1 = Math.Abs(byteCount);
            int int32 = Convert.ToInt32(Math.Floor(Math.Log((double)num1, 1024.0)));
            double num2 = Math.Round((double)num1 / Math.Pow(1024.0, (double)int32), 3);
            return string.Format("{0} {1}", (object)((double)Math.Sign(byteCount) * num2), (object)strArray[int32]);
        }
    }
}
