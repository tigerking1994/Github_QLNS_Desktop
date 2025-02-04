using AutoMapper;
using log4net;
using MaterialDesignThemes.Wpf;
using Microsoft.Extensions.Configuration;
using Microsoft.SqlServer.Management.Smo;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Windows;
using System.Windows.Data;
using System.Windows.Forms;
using VTS.QLNS.CTC.App.Command;
using VTS.QLNS.CTC.App.Extensions;
using VTS.QLNS.CTC.App.Helper;
using VTS.QLNS.CTC.App.Model;
using VTS.QLNS.CTC.App.Properties;
using VTS.QLNS.CTC.App.Service;
using VTS.QLNS.CTC.App.View.Budget.MigrationData;
using VTS.QLNS.CTC.App.ViewModel.Shared;
using VTS.QLNS.CTC.App.ViewModel.SystemAdmin.Utilities;
using VTS.QLNS.CTC.Core.Domain.Criteria;
using VTS.QLNS.CTC.Core.Repository;
using VTS.QLNS.CTC.Core.Service;
using VTS.QLNS.CTC.Utility;
using VTS.QLNS.CTC.Utility.Enum;

namespace VTS.QLNS.CTC.App.ViewModel.Budget.MigrationData
{
    public class MigrationDataOldViewModel : GridViewModelBase<HtTableMigrateModel>
    {
        private readonly IMigrationDataService _migrationDataService;
        private readonly IDatabaseService _databaseService;
        private readonly IConfiguration _configuration;
        private readonly IHtTableMigrateService _tableMigrateService;
        private readonly ISktSoLieuChungTuRepository _sktSoLieuChungTuRepository;
        private readonly ISktMucLucRepository _sktMucLucRepository;
        private readonly ISktChungTuRepository _sktChungTuRepository;
        private readonly IMapper _mapper;
        private readonly IDanhMucService _dmService;
        private readonly ICloneDataYearOfWork _cloneDataYearOfWorkService;
        private readonly ISessionService _sessionService;
        private readonly IDmChuKyService _dmChuKyService;
        private readonly IDanhMucService _danhMucService;
        private readonly ILog _logger;
        private readonly string _connectionString;
        private readonly SqlConnectionStringBuilder _connectionStringBuilder;
        private readonly ConnectionType _connectionType;
        public override string Name => "Tích hợp dữ liệu CSDL cũ";
        public override string Description => "Tích hợp dữ liệu CSDL cũ";
        public override Type ContentType => typeof(MigrationDataOld);
        public override PackIconKind IconKind => PackIconKind.Adjust;

        private ICollectionView _itemsCollectionView;

        public MigrationDataOldDialogViewModel MigrationDataOldDialogViewModel { get; }

        private bool _isCheckedAll = false;
        public bool IsCheckedAll
        {
            get => Items.Where(x => x.SourceRowCount != -1).All(x => x.IsChecked);
            set
            {
                SetProperty(ref _isCheckedAll, value);
                IsProcess = true;
                Items.Where(x => x.SourceRowCount != -1).ForAll(x => x.IsChecked = value);
                IsProcess = false;
            }
        }

        public string FilePath { get; set; }
        public string MdfPath { get; set; }

        public enum TypeOfFile
        {
            ZipMdfFile,
            ZipBakFile,
            MdfFile,
            BakFile
        }
        public TypeOfFile FileType { get; set; }

        public DatabaseInfo DatabaseInfo { get; set; }

        public RelayCommand OpenDialogFileCommand { get; set; }
        public RelayCommand CancelProgressCommand { get; set; }
        public RelayCommand ManualUpdateCommand { get; set; }
        public RelayCommand RestoreStandardCommand { get; set; }

        private bool _isUnziping;
        public bool IsUnziping
        {
            get => _isUnziping;
            set => SetProperty(ref _isUnziping, value);
        }

        private bool _isProcess;
        public bool IsProcess
        {
            get => _isProcess;
            set => SetProperty(ref _isProcess, value);
        }

        private bool _isProcessReport;
        public bool IsProcessReport
        {
            get => _isProcessReport;
            set => SetProperty(ref _isProcessReport, value);
        }
        public bool IsCancel { get; set; }

        private int _progressValue;
        public int ProgressValue
        {
            get => _progressValue;
            set => SetProperty(ref _progressValue, value);
        }

        public MigrationDataOldViewModel(
            IConfiguration configuration,
            IMigrationDataService migrationDataService,
            IHtTableMigrateService tableMigrateService,
            ISktSoLieuChungTuRepository sktSoLieuChungTuRepository,
            IMapper mapper,
            IDanhMucService danhMucService,
            ILog logger,
            ICloneDataYearOfWork cloneDataYearOfWork,
            ISktMucLucRepository sktMucLucRepository,
            ISktChungTuRepository sktChungTuRepository,
            ISessionService sessionService,
            IDanhMucService danhmucService,
            IDatabaseService databaseService,
            IDmChuKyService dmChuKyService,
            MigrationDataOldDialogViewModel migrationDataOldDialogViewModel)
        {
            _configuration = configuration;
            _databaseService = databaseService;
            _migrationDataService = migrationDataService;
            _tableMigrateService = tableMigrateService;
            _sktSoLieuChungTuRepository = sktSoLieuChungTuRepository;
            _dmService = danhMucService;
            _cloneDataYearOfWorkService = cloneDataYearOfWork;
            _sktMucLucRepository = sktMucLucRepository;
            _sktChungTuRepository = sktChungTuRepository;
            _sessionService = sessionService;
            _dmChuKyService = dmChuKyService;
            _danhMucService = danhMucService;
            _mapper = mapper;
            _logger = logger;
            MigrationDataOldDialogViewModel = migrationDataOldDialogViewModel;

            OpenDialogFileCommand = new RelayCommand(obj => OnUploadFile(), obj => !IsLoading || !IsUnziping);
            RestoreStandardCommand = new RelayCommand(obj => OnRestoreStandard(), obj => !IsLoading);
            CancelProgressCommand = new RelayCommand(obj => OnCancelProgress(), obj => !IsLoading);
            ManualUpdateCommand = new RelayCommand(obj => OnManualUpdate(), obj => !IsLoading);

            _connectionType = _configuration.GetSection("DbSettings:ConnectionType").Value == ConnectionType.SqlServer.ToString() ? ConnectionType.SqlServer : ConnectionType.LocalDb;
            _connectionString = _configuration.GetConnectionString(_connectionType.ToString());
            _connectionStringBuilder = new SqlConnectionStringBuilder(_connectionString);
        }

        public override void Init()
        {
            IsProcess = false;
            DatabaseInfo = new DatabaseInfo();
            Items = new ObservableCollection<HtTableMigrateModel>();
        }



        private void LoadFullData()
        {
            try
            {
                DatabaseInfo = _migrationDataService.AttachDatabase(DatabaseInfo?.Name ?? "TEMP_DATABASE", MdfPath);
                List<HtTableMigrateModel> destinationTables = _mapper.Map<List<HtTableMigrateModel>>(_migrationDataService.GetListTable("(LocalDB)\\v11.0", DatabaseInfo?.Name ?? "TEMP_DATABASE"));
                List<HtTableMigrateModel> sourceTables = _mapper.Map<List<HtTableMigrateModel>>(_migrationDataService.GetListTable());
                List<HtTableMigrateModel> trackChangeTables = _mapper.Map<List<HtTableMigrateModel>>(_tableMigrateService.FindAll());
                IEnumerable<HtTableMigrateModel> items = from dest in destinationTables
                                                         join source in sourceTables on dest.Object equals source.Object into ps
                                                         from p in ps.DefaultIfEmpty()
                                                         join track in trackChangeTables on dest.Object equals track.Object into trackps
                                                         from trackp in trackps.DefaultIfEmpty()
                                                         where dest.DestinationRowCount > 0 && !new string[] { "__EFMigrationsHistory", "HT_TableMigrate", "HT_ErrorDatabaseLog" }.Contains(dest.Object)
                                                         select new HtTableMigrateModel
                                                         {
                                                             Object = dest.Object,
                                                             DestinationRowCount = dest.DestinationRowCount,
                                                             SourceRowCount = p is null ? -1 : p.SourceRowCount,
                                                             MigrateFrequency = trackp is null ? 0 : trackp.MigrateFrequency,
                                                             Description = trackp is null ? "" : trackp.Description,
                                                             IsMigrated = trackp?.IsMigrated ?? false,
                                                         };
                Items = new ObservableCollection<HtTableMigrateModel>(items);
                _itemsCollectionView = CollectionViewSource.GetDefaultView(Items);
                foreach (HtTableMigrateModel item in Items)
                {
                    item.PropertyChanged += (object sender, PropertyChangedEventArgs e) =>
                    {
                        if (e.PropertyName == nameof(HtTableMigrateModel.IsChecked) && !IsProcess)
                        {
                            OnPropertyChanged(nameof(IsCheckedAll));
                        }
                    };
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message);
            }
            finally
            {
                DropDatabase();
            }
        }

        private void OnCancelProgress()
        {
            IsCancel = true;
        }

        public void LoadData(bool isShowMessage = true)
        {
            BackgroundWorkerHelper.Run((e, s) =>
            {
                IsLoading = true;
                LoadFullData();
            }, (e, s) =>
            {
                IsLoading = false;
                if (s.Error is null)
                {
                    if (isShowMessage) new NSMessageBoxViewModel("Phục hồi dữ liệu các bảng được chọn sẽ xóa hoàn toàn dữ liệu cũ của bảng đó").ShowDialogHost();
                }
                else
                {
                    new NSMessageBoxViewModel("Có lỗi xảy ra", "Lỗi", NSMessageBoxButtons.OK, null).ShowDialogHost();
                }
            });
        }

        private void ManualUpdateVersion()
        {
            try
            {
                string applicationPath = IOExtensions.ApplicationPath;
                string appVersionFolder = _configuration.GetSection(ConfigHelper.APPVERSION_LOCATION).Value;
                string appVersionPath = Path.Combine(applicationPath, appVersionFolder, DateTime.Now.ToStringTimeStamp());

                if (!Directory.Exists(appVersionPath))
                {
                    Directory.CreateDirectory(appVersionPath);
                }

                using (Ionic.Zip.ZipFile zipFile = new Ionic.Zip.ZipFile(FilePath))
                {
                    zipFile.ExtractAll(appVersionPath, Ionic.Zip.ExtractExistingFileAction.OverwriteSilently);
                }
                string path = _configuration.GetSection(ConfigHelper.CONFIG_REPORT_BHXH_SETTING_PATH).Value;
                ConfigHelper.UpdateSetting(path, new UpdateSetting { IsOverrideDatabase = true });
                LauchUpdate(applicationPath, appVersionPath);
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
                new NSMessageBoxViewModel(Resources.FileTypeInvalid, "Cảnh báo", NSMessageBoxButtons.OK, PackIconKind.Information, null).ShowDialogHost();
            }
        }


        private void LauchUpdate(string applicationPath, string appVersionPath)
        {
            // Start update process
            string updateExecuteFileName = _configuration.GetSection(ConfigHelper.APPVERSION_UPDATE_EXECUTE_FILENAME).Value;
            string updateExecutePath = Path.Combine(appVersionPath, updateExecuteFileName);
            ProcessStartInfo process = new ProcessStartInfo();
            process.FileName = updateExecutePath;
            process.Arguments = "\"" + Path.Combine(applicationPath, "VTS.QLNS.CTC.App.exe") + "\"" + $" {Assembly.GetExecutingAssembly().GetName().Version.ToString()}";
            process.WorkingDirectory = appVersionPath;
            Process.Start(process);
            // Kill current process
            Process.GetCurrentProcess().Kill();
        }

        private void OnManualUpdate()
        {
            using OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Title = "Chọn tệp";
            openFileDialog.RestoreDirectory = true;
            openFileDialog.DefaultExt = ".zip";
            if (openFileDialog.ShowDialog() != DialogResult.OK)
            {
                return;
            }
            FilePath = openFileDialog.FileName;
            string extension = Path.GetExtension(FilePath);
            if (!extension.EndsWith("zip"))
            {
                new NSMessageBoxViewModel(Resources.FileTypeInvalid, "Cảnh báo", NSMessageBoxButtons.OK, PackIconKind.Information, null).ShowDialogHost();
                return;
            }
            new UpdateVersionDialogViewModel("Bạn có muốn cập nhật ứng dụng?\n", ManualUpdateVersion).ShowDialogHost();
        }

        private void OnBackup()
        {
            try
            {
                DialogHost.CloseDialogCommand.Execute(null, null);
                string fileName = _databaseService.GetBackupFilename(_connectionString, _connectionType.ToString());
                using SaveFileDialog dialog = new SaveFileDialog();
                dialog.FileName = fileName;
                dialog.Title = "Lưu tệp tin sao lưu CSDL";
                var result = dialog.ShowDialog();
                if (result == DialogResult.OK)
                {
                    IsLoading = true;
                    string filePath = dialog.FileName;
                    BackgroundWorkerHelper.Run((s, e) =>
                    {
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
                        IsLoading = false;
                        if (e.Error is null)
                        {
                            new NSMessageBoxViewModel(Resources.MsgBackupSuccess, "Thông báo", NSMessageBoxButtons.OK, PackIconKind.Done, (result) =>
                            {
                                if (result == NSDialogResult.OK) OnRestore();
                            }).ShowDialogHost();
                        }
                        else
                        {
                            new NSMessageBoxViewModel(Resources.MsgBackupError, "Lỗi", NSMessageBoxButtons.OK, PackIconKind.Error, null).ShowDialogHost();
                        }
                    });
                }
            }
            catch (Exception ex) { }
            
        }

        private void OnRestoreStandard()
        {
            new NSMessageBoxViewModel("Sao lưu CSDL trước khi phục hồi?\n", "Cảnh báo", NSMessageBoxButtons.YesNoCancel, PackIconKind.Warning, OnRestore).ShowDialogHost();
        }

        private void OnRestore(NSDialogResult dialog)
        {
            if (dialog == NSDialogResult.Cancel) return;
            if (dialog == NSDialogResult.No)
            {
                OnRestore();
            } else
            {
                OnBackup();
            }

        }

        private void OnRestore()
        {
            SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder(_connectionString);
            string mdfFileName = string.IsNullOrEmpty(builder.InitialCatalog) ? builder.AttachDBFilename.Split("\\").Last() : builder.InitialCatalog.Split("\\").Last();
            BackgroundWorkerHelper.Run((e, s) =>
            {
                IsLoading = true;
                string path = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), @"AppData\Standard_MDF\QLNS_TIEUCHUAN.zip");
                s.Result = _databaseService.RestoreLocal(_connectionString, path, "");
            }, (e, s) =>
            {
                IsLoading = false;
                if (s.Error is null && (bool)s.Result)
                {
                    new NSMessageBoxViewModel("Phục hồi dữ liệu tiêu chuẩn thành công, khởi động lại phần mềm và phục hồi dữ liệu", "Thành công", NSMessageBoxButtons.OK, PackIconKind.DoneAll, (result) =>
                    {
                        if (result is NSDialogResult.OK)
                        {
                            System.Windows.Application.Current.Shutdown();
                        }
                    }).ShowDialogHost();
                }
                else
                {
                    new NSMessageBoxViewModel("Phục hồi dữ liệu thất bại", "Lỗi", NSMessageBoxButtons.OK, PackIconKind.Error, null).ShowDialogHost();
                }
            });
        }

        private void ClearTempDatabase()
        {
            string appDataLocation = $"{Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "AppData")}";
            string[] files = Directory.GetFiles(appDataLocation, "*.mdf", SearchOption.AllDirectories);
            foreach (string file in files)
            {
                if (Path.GetFileName(file).ToUpper().StartsWith("TEMP_DATABASE") && File.Exists(file))
                {
                    try
                    {
                        File.Delete(file);
                    }
                    catch (Exception ex)
                    {
                        _logger.Error(ex.Message, ex);
                    }
                }
            }
        }

        private void OnUploadFile()
        {
            Items = new ObservableCollection<HtTableMigrateModel>();
            ClearTempDatabase();
            using OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Title = string.Format("Chọn file");
            openFileDialog.RestoreDirectory = true;
            openFileDialog.DefaultExt = ".mdf";
            openFileDialog.Filter = "Database Files|*.mdf;*.bak;*.zip";
            if (openFileDialog.ShowDialog() != DialogResult.OK)
                return;
            FilePath = openFileDialog.FileName;
            string extension = Path.GetExtension(FilePath).ToLower();

            if (extension == ".zip")
            {
                string fullName = "";
                IsUnziping = true;
                try
                {
                    string dirPath = Path.GetDirectoryName(FilePath);
                    BackgroundWorkerHelper.Run((e, s) =>
                    {
                        IsUnziping = true;
                        using ZipArchive zipArchive = ZipFile.OpenRead(FilePath);
                        zipArchive.ExtractToDirectory(dirPath, true);
                        s.Result = zipArchive.Entries[0].FullName;

                    }, (e, s) =>
                    {
                        IsUnziping = false;
                        if (s.Error is null)
                        {
                            string name = s.Result as string;
                            fullName = Path.Combine(dirPath, name);
                            string ext = Path.GetExtension(fullName).ToLower();
                            if (string.IsNullOrEmpty(ext) || ext == ".bak")
                            {
                                FileType = TypeOfFile.ZipBakFile;
                                DatabaseInfo = _migrationDataService.RestoreLocal(fullName, "TEMP_DATABASE");
                                MdfPath = DatabaseInfo.MdfFilePath;
                            }
                            else if (name.ToLower().EndsWith(".mdf"))
                            {
                                FileType = TypeOfFile.ZipMdfFile;
                                MdfPath = fullName;
                            }
                            LoadData();
                        }
                        else
                        {
                            IsLoading = false;
                        }
                    });

                }
                catch (Exception ex)
                {
                    _logger.Error(ex.Message, ex);
                }
                finally
                {
                    if (FileType == TypeOfFile.ZipBakFile && File.Exists(fullName))
                    {
                        File.Delete(fullName);
                    }
                }
            }
            else if (extension == ".mdf")
            {
                FileType = TypeOfFile.MdfFile;
                MdfPath = FilePath;
                LoadData();
            }
            else if (extension == ".bak")
            {
                FileType = TypeOfFile.BakFile;
                DatabaseInfo = _migrationDataService.RestoreLocal(FilePath, "TEMP_DATABASE");
                MdfPath = DatabaseInfo.MdfFilePath;
                LoadData();
            }
            else
            {
                new NSMessageBoxViewModel("Chọn sai loại tệp tin", "Lỗi", NSMessageBoxButtons.OK, null).ShowDialogHost();
                return;
            }
        }

        private void DropDatabase()
        {
            if (_migrationDataService.DropDatabase(DatabaseInfo?.Name ?? "TEMP_DATABASE"))
            {
                if (!string.IsNullOrEmpty(DatabaseInfo?.LogFilePath) && File.Exists(DatabaseInfo.LogFilePath)) File.Delete(DatabaseInfo.LogFilePath);
                if (!string.IsNullOrEmpty(MdfPath) && File.Exists(MdfPath.Replace(".mdf", "_log.ldf"))) File.Delete(MdfPath.Replace(".mdf", "_log.ldf"));
            }
        }

        public override void OnSave()
        {
            if (IsProcessReport) return;
            List<HtTableMigrateModel> items = Items.Where(x => x.IsChecked).ToList();
            if (!items.Any())
            {
                new NSMessageBoxViewModel("Chưa có bảng nào được chọn").ShowDialogHost();
                return;
            }

            DatabaseInfo = _migrationDataService.AttachDatabase(DatabaseInfo.Name, MdfPath);
            IsProcessReport = true;
            int index = 0;
            int count = items.Count;
            BackgroundWorkerHelper.Run((s, e) =>
            {
                BlockingCollection<string> listAddForeignKey = new BlockingCollection<string>();

                items.ForAll(item =>
                {
                    if (item.Object.StartsWith("HT"))
                    {
                        string temp = _migrationDataService.GenerateAddForeignKey(item.Object);
                        if (!string.IsNullOrEmpty(temp)) listAddForeignKey.Add(temp);
                    }
                    _migrationDataService.MigrateTable(DatabaseInfo.Name, item.Object);
                    (s as BackgroundWorker).ReportProgress((Interlocked.Increment(ref index)) * 100 / count, null);
                });

                //Parallel.ForEach(Items.AsParallel(), new ParallelOptions { MaxDegreeOfParallelism = 10 }, item =>
                //{
                //    (s as BackgroundWorker).ReportProgress((Interlocked.Increment(ref index)) * 100 / count, null);
                //    string temp = _migrationDataService.GenerateAddForeignKey(item.Object);
                //    if (!string.IsNullOrEmpty(temp)) listAddForeignKey.Add(temp);
                //    _migrationDataService.MigrateTable("TEMP_DATABASE", item.Object);
                //});
                e.Result = listAddForeignKey;
            }, (s, e) =>
            {
                IsProcessReport = false;
                if (e.Error is null)
                {
                    try
                    {
                        _migrationDataService.ApplyScript(e.Result as BlockingCollection<string>);
                        _migrationDataService.ApplyScript(items.Select(x => $@"alter table {x.Object} with check check constraint all"));
                    }
                    catch (Exception ex) {
                        new NSMessageBoxViewModel(ex.Message, "Lỗi", NSMessageBoxButtons.OK, PackIconKind.Error, null).ShowDialogHost();
                    }
                }
                new NSMessageBoxViewModel("Phục hồi thành công").ShowDialogHost();
                LoadData(false);
            }, (s, e) =>
            {
                ProgressValue = e.ProgressPercentage;
            });


        }

    }
}
