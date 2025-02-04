using log4net;
using MaterialDesignThemes.Wpf;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.Configuration;
using Microsoft.SqlServer.Management.Common;
using Microsoft.SqlServer.Management.Smo;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Globalization;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using VTS.QLNS.CTC.App.Command;
using VTS.QLNS.CTC.App.Helper;
using VTS.QLNS.CTC.App.Model.Control;
using VTS.QLNS.CTC.App.Properties;
using VTS.QLNS.CTC.App.Service;
using VTS.QLNS.CTC.App.View.SystemAdmin.Utilities;
using VTS.QLNS.CTC.Core;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Service;
using VTS.QLNS.CTC.Utility;
using VTS.QLNS.CTC.Utility.Enum;
using static VTS.QLNS.CTC.Core.Extensions.MigrationBuilderExtension;
using ConnectionType = VTS.QLNS.CTC.Utility.ConnectionType;


namespace VTS.QLNS.CTC.App.ViewModel.SystemAdmin.Utilities
{
    public class ClearDataRedundancyViewModel : ViewModelBase
    {
        public override string Name => "Tính toán lại dữ liệu";
        public override string Description => "Tính toán lại dữ liệu";
        public override string Title => "Tính toán lại dữ liệu";
        public override Type ContentType => typeof(ClearDataRedundancy);
        public const string BatchTerminator = "\r\nGO\r\n";

        public override PackIconKind IconKind => PackIconKind.DatabaseCogOutline;

        private readonly IAppVersionService _appVersionService;
        private readonly IMigrationDataService _iMigrationDataService;
        private readonly IErrorDatabaseLogService _errorDatabaseLogService;
        private readonly ISessionService _sessionService;
        private readonly IDatabaseService _databaseService;
        private readonly IDanhMucService _dmService;
        private readonly IConfiguration _configuration;
        private readonly string _logPath;
        private readonly ILog _logger;

        private bool _isProcessReport;
        public bool IsProcessReport
        {
            get => _isProcessReport;
            set => SetProperty(ref _isProcessReport, value);
        }

        private int _progressValue;
        public int ProgressValue
        {
            get => _progressValue;
            set => SetProperty(ref _progressValue, value);
        }

        public int CountCompareDatabase { get; set; } = 0;

        public RelayCommand UploadFileCommand { get; }
        public RelayCommand RestoreMLSKTCommand { get; }
        public RelayCommand ClearDataRedundancyCommand { get; set; }
        public RelayCommand ShowErrorDataLogCommand { get; set; }
        public RelayCommand ConfigBudgetCategoryCommand { get; set; }
        public RelayCommand CopyDoiTuongCommand { get; set; }
        public RelayCommand GenerateScriptCommand { get; set; }
        public RelayCommand CompareDatabaseCommand { get; set; }
        public RelayCommand RestoreMLNSThuNopCommand { get; set; }

        public ClearDataRedundancyDialogViewModel ClearDataRedundancyDialogViewModel { get; }


        private ObservableCollection<ComboboxItem> _years;
        public ObservableCollection<ComboboxItem> Years
        {
            get => _years;
            set => SetProperty(ref _years, value);
        }

        private ObservableCollection<ComboboxItem> _restoreYears;
        public ObservableCollection<ComboboxItem> RestoreYears
        {
            get => _restoreYears;
            set => SetProperty(ref _restoreYears, value);
        }

        private int? _restoreYear;
        public int? RestoreYear
        {
            get => _restoreYear;
            set => SetProperty(ref _restoreYear, value);
        }

        private int? _sourceYear;
        public int? SourceYear
        {
            get => _sourceYear;
            set => SetProperty(ref _sourceYear, value);
        }

        private ObservableCollection<ComboboxItem> _modules;
        public ObservableCollection<ComboboxItem> Modules
        {
            get => _modules;
            set => SetProperty(ref _modules, value);
        }

        private string _selectedModule;
        public string SelectedModule
        {
            get => _selectedModule;
            set => SetProperty(ref _selectedModule, value);
        }

        private bool _isLoading1;
        public bool IsLoading1
        {
            get => _isLoading1;
            set => SetProperty(ref _isLoading1, value);
        }

        private bool _isLoading2;
        public bool IsLoading2
        {
            get => _isLoading2;
            set => SetProperty(ref _isLoading2, value);
        }

        private bool _isLoading3;
        public bool IsLoading3
        {
            get => _isLoading3;
            set => SetProperty(ref _isLoading3, value);
        }

        private bool _isLoading4;
        public bool IsLoading4
        {
            get => _isLoading4;
            set => SetProperty(ref _isLoading4, value);
        }

        private bool _isLoading5;
        public bool IsLoading5
        {
            get => _isLoading5;
            set => SetProperty(ref _isLoading5, value);
        }

        private bool _isShowCompareDatabase;
        public bool IsShowCompareDatabase
        {
            get => _isShowCompareDatabase;
            set => SetProperty(ref _isShowCompareDatabase, value);
        }

        public ClearDataRedundancyViewModel(
            ILog logger,
            IConfiguration configuration,
            IMigrationDataService iMigrationDataService,
            ISessionService sessionService,
            IErrorDatabaseLogService errorDatabaseLogService,
            IDanhMucService dmService,
            IAppVersionService appVersionService,
            IDatabaseService databaseService,
            ClearDataRedundancyDialogViewModel clearDataRedundancyDialogViewModel)
        {
            ClearDataRedundancyCommand = new RelayCommand(obj => OnClearDataRedundancy());
            ShowErrorDataLogCommand = new RelayCommand(obj => OnShowErrorDataLog());
            ConfigBudgetCategoryCommand = new RelayCommand(obj => OnConfigBudgetCategory());
            CopyDoiTuongCommand = new RelayCommand(obj => OnCopyDoiTuong());
            GenerateScriptCommand = new RelayCommand(obj => OnGenerateScript());
            CompareDatabaseCommand = new RelayCommand(obj => CompareDatabase());
            UploadFileCommand = new RelayCommand(obj => OnUploadFile());
            RestoreMLSKTCommand = new RelayCommand(obj => OnRestoreMLSKT());
            RestoreMLNSThuNopCommand = new RelayCommand(obj => OnRestoreMLNSThuNop());

            _logger = logger;
            _iMigrationDataService = iMigrationDataService;
            _errorDatabaseLogService = errorDatabaseLogService;
            _appVersionService = appVersionService;
            _configuration = configuration;
            _sessionService = sessionService;
            _dmService = dmService;
            _databaseService = databaseService;
            ClearDataRedundancyDialogViewModel = clearDataRedundancyDialogViewModel;
        }

        private void OnGenerateScript()
        {
            switch (SelectedModule)
            {
                case "Budget":
                    Backup("NS_");
                    break;
                case "Investment":
                    Backup("VDT_");
                    break;
                case "Forex":
                    Backup("NH_");
                    break;
                case "Salary":
                    Backup("TL_");
                    break;
                case "NewSalary":
                    Backup("TL_", "NQ104");
                    break;
                case "SocialInsurance":
                    Backup("BH_");
                    break;
                case "System":
                    Backup("HT_");
                    break;
                default:
                    break;
            }
        }

        private void OnShowErrorDataLog()
        {
            IEnumerable<ErrorDatabaseLog> items = _errorDatabaseLogService.FindAllByToday();
            if (!items.Any())
            {
                new NSMessageBoxViewModel("CSDL không có lỗi, nhấn so sánh CSDL để kiểm tra lại!").ShowDialogHost();
            }
            else
            {
                ClearDataRedundancyDialogViewModel.ErrorDatabaseLogs = new ObservableCollection<ErrorDatabaseLog>(items);
                ClearDataRedundancyDialogViewModel.ShowDialogHost();
            }
        }

        // Tính năng đang phát triển
        private void OnClearDataRedundancy()
        {
            BackgroundWorkerHelper.Run((s, e) =>
            {
                IsLoading1 = true;
                _iMigrationDataService.ClearDataRedundancy();
            }, (s, e) =>
            {
                if (e.Error == null)
                {
                    NSMessageBoxViewModel messageBox = new NSMessageBoxViewModel(Resources.SuccessClearDataRedundancy);
                    DialogHost.Show(messageBox.Content, "RootDialog");
                    //Helper.MessageBoxHelper.Info(Resources.SuccessClearDataRedundancy);
                }
                else
                {
                    MessageBoxHelper.Error(Resources.ErrorManipulation);
                }
                IsLoading1 = false;
            });
        }

        private void OnUploadFile()
        {
            try
            {
                OpenFileDialog openFileDialog = new OpenFileDialog();
                openFileDialog.Title = string.Format("Chọn tệp mã dữ liệu");
                openFileDialog.RestoreDirectory = true;
                openFileDialog.DefaultExt = ".zip";
                if (openFileDialog.ShowDialog() != DialogResult.OK)
                    return;
                string filePath = openFileDialog.FileName;
                //_fileName = openFileDialog.SafeFileName;
                using (ZipArchive zipArchive = ZipFile.OpenRead(filePath))
                {
                    zipArchive.ExtractToDirectory(Path.GetDirectoryName(filePath));
                    string textScript = File.ReadAllText(Path.Combine(Path.GetDirectoryName(filePath), zipArchive.Entries[0].FullName));
                    string[] splitter = new string[] { "\r\nGO\r\n", "GO" };
                    string[] commandTexts = textScript.Split(splitter,
                      StringSplitOptions.RemoveEmptyEntries);

                    ConnectionType connectionType = _configuration.GetSection("DbSettings:ConnectionType").Value == ConnectionType.SqlServer.ToString() ? ConnectionType.SqlServer : ConnectionType.LocalDb;
                    string connectionString = _configuration.GetConnectionString(connectionType.ToString());

                    foreach (string commandText in commandTexts)
                    {
                        _databaseService.ExecuteNonQuery(commandText, connectionString);
                        //_result = _databaseService.ExecuteQuery(Document.Text, _connectionString);
                    }
                }
            }
            catch (Exception)
            {
                //_logger.Error(ex.Message, ex);
            }
        }


        private void OnConfigBudgetCategory()
        {
            BackgroundWorkerHelper.Run((s, e) =>
            {
                IsLoading2 = true;
                _iMigrationDataService.ConfigBudgetCategory();
            }, (s, e) =>
            {
                IsLoading2 = false;
                if (e.Error is null)
                {
                    new NSMessageBoxViewModel(Resources.SuccessConfigBudgetCategory).ShowDialogHost();
                }
                else
                {
                    new NSMessageBoxViewModel(Resources.ErrorManipulation, "Lỗi", NSMessageBoxButtons.OK, PackIconKind.Error, null).ShowDialogHost();
                }
            });
        }

        public void CompareDatabase()
        {
            if (IsProcessReport) return;
            ProgressValue = 0;
            _errorDatabaseLogService.RemoveByToday();
            RemoveLDF();

            BackgroundWorkerHelper.Run((s, e) =>
            {
                IsProcessReport = true;
                CountCompareDatabase = 0;
                string script = "";
                string standardMDFPath = @"|DataDirectory|AppData\Standard_MDF\QLNS_TIEUCHUAN.mdf";
                string localDbConnectionString = $"Data Source=(LocalDB)\\v11.0;Database=QLNS_TIEUCHUAN;AttachDbFilename={standardMDFPath};Integrated Security=True;Connect Timeout=10;MultipleActiveResultSets=True;";
                ServerConnection standardServerConnection = new ServerConnection();
                standardServerConnection.ConnectionString = localDbConnectionString;

                Action<DbContextOptionsBuilder> configureDbContext = options =>
                {
                    options.UseSqlServer(localDbConnectionString, sqlServerOptions => sqlServerOptions.CommandTimeout(6000));
                    options.EnableSensitiveDataLogging();
                    options.ConfigureWarnings(x => x.Ignore(RelationalEventId.AmbientTransactionWarning));
                };

                try
                {
                    if (_iMigrationDataService.DetachDatabase(@"(LocalDB)\v11.0", "QLNS_TIEUCHUAN"))
                    {
                        standardServerConnection.Connect();
                        Server server = new Server(standardServerConnection);
                        Database standardDB = server.Databases["QLNS_TIEUCHUAN"];
                        Database currentDB = server.Databases[_sessionService.Current.DbName.Substring(6)];
                        int index = 0;


                        var contextFactory = new ApplicationDbContextFactory(configureDbContext);
                        using var context = contextFactory.CreateDbContext();
                        context.Database.Migrate();

                        List<Tuple<string, string>> tables = _iMigrationDataService.GetListTableName(@"(LocalDB)\v11.0", "QLNS_TIEUCHUAN").ToList();
                        List<Tuple<string, string>> storedProcedures = _iMigrationDataService.GetListStoredProcedure(@"(LocalDB)\v11.0", "QLNS_TIEUCHUAN").ToList();
                        int count = tables.Count + storedProcedures.Count;

                        foreach (Tuple<string, string> table in tables)
                        {
                            (s as BackgroundWorker).ReportProgress((index++) * 100 / count, null);
                            Table table1 = standardDB.Tables[table.Item1, table.Item2];
                            Table table2 = currentDB.Tables[table1.Name, table1.Schema];
                            if (table1 != null && table2 == null)
                            {
                                CountCompareDatabase++;
                                //_logger.Warn($"Table {table1.Name} exists in DB1 but not in DB2");
                                _errorDatabaseLogService.Add(new ErrorDatabaseLog
                                {
                                    Object = $"Bảng {table1.Name}",
                                    Reason = "Không tồn tại",
                                    Description = ""
                                });
                                _logger.Warn(@$"Table {table1.Name} not exists");

                                ScriptingOptions options = new ScriptingOptions
                                {
                                    ScriptForCreateDrop = true,
                                    ScriptData = false, // Set to true if you want to script data as well
                                    ScriptSchema = true, // Set to true to script the schema
                                    IncludeIfNotExists = true,
                                    IncludeHeaders = true,
                                    FileName = string.Empty,
                                    AppendToFile = false,
                                    ScriptBatchTerminator = true
                                };
                                StringCollection script1 = table1.Script(options);

                                script += string.Join(BatchTerminator, script1.Cast<string>()) + BatchTerminator;
                                continue;
                            }
                            else
                            {
                                CompareTableColumns(table1, table2, (tableName, columnName, columnType) =>
                                {
                                    string script1 = $@"
                                    if not exists (
                                        select * from sys.columns
                                        where name = '{columnName}'
                                        and object_id = object_id('{tableName}'))
                                    begin
                                        alter table {tableName} add {columnName} {columnType};
                                    end
                                ";
                                    script += script1 + BatchTerminator;
                                });
                            }
                        }

                        foreach (Tuple<string, string> storedProcedure in storedProcedures)
                        {
                            (s as BackgroundWorker).ReportProgress((index++) * 100 / count, null);
                            StoredProcedure sp1 = standardDB.StoredProcedures[storedProcedure.Item1, storedProcedure.Item2];
                            StoredProcedure sp2 = currentDB.StoredProcedures[sp1.Name, sp1.Schema];
                            if (sp1 != null && sp2 == null)
                            {
                                //_logger.Warn($"Stored procedure {sp1.Name} exists in DB1 but not in DB2");
                                CountCompareDatabase++;
                                _logger.Warn(@$"Stored procedure {sp1.Name} not exists");
                                _errorDatabaseLogService.Add(new ErrorDatabaseLog
                                {
                                    Object = $"Thủ tục {sp1.Name}",
                                    Reason = "không tồn tại",
                                    Description = $""
                                });
                                ScriptingOptions options = new ScriptingOptions
                                {
                                    ScriptForCreateDrop = true,
                                    ScriptData = false, // Set to true if you want to script data as well
                                    ScriptSchema = true, // Set to true to script the schema
                                    IncludeIfNotExists = true,
                                    IncludeHeaders = true,
                                    EnforceScriptingOptions = true,
                                    FileName = string.Empty,
                                    AppendToFile = false,
                                    ScriptBatchTerminator = true
                                };
                                StringCollection script1 = sp1.Script(options);

                                script += string.Join(BatchTerminator, script1.Cast<string>()) + BatchTerminator;
                                continue;
                            }
                            else
                            {
                                CompareProcedureText(sp1.TextBody, sp2.TextBody, sp1.Name, () =>
                                {
                                    ScriptingOptions options = new ScriptingOptions
                                    {
                                        ScriptForCreateDrop = true,
                                        ScriptData = false, // Set to true if you want to script data as well
                                        ScriptSchema = true, // Set to true to script the schema
                                        IncludeIfNotExists = true,
                                        IncludeHeaders = true,
                                        EnforceScriptingOptions = true,
                                        FileName = string.Empty,
                                        AppendToFile = false,
                                        ScriptBatchTerminator = true
                                    };
                                    StringCollection script1 = sp1.Script(options);

                                    script += string.Join(BatchTerminator, script1.Cast<string>()) + BatchTerminator;
                                });
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    _logger.Error(ex.Message, ex);
                    IsProcessReport = false;
                    throw ex;
                }
                finally
                {
                    if (standardServerConnection.IsOpen)
                    {
                        standardServerConnection.Disconnect();
                    }
                    DropDatabase("QLNS_TIEUCHUAN", script);
                    RemoveLDF();
                }
            }, (s, e) =>
            {
                IsProcessReport = false;
                if (e.Error is null)
                {
                    if (CountCompareDatabase == 0)
                    {
                        new NSMessageBoxViewModel("Cơ sở dữ liệu không có lỗi").ShowDialogHost();
                    }
                    else
                    {
                        new NSMessageBoxViewModel($"Cơ sở dữ liệu có {CountCompareDatabase} lỗi xảy ra. Vui lòng kiểm tra, lấy file log và liên hệ bộ phận chăm sóc khách hàng").ShowDialogHost();
                    }
                }
                else
                {
                    new NSMessageBoxViewModel($"Có lỗi xảy ra trong quá trình so sánh CSDL", "Lỗi", NSMessageBoxButtons.OK, PackIconKind.Error, null).ShowDialogHost();
                }
            }, (s, e) =>
            {
                ProgressValue = e.ProgressPercentage;
            });
        }


        public void CompareDatabaseOrigin()
        {
            if (IsProcessReport) return;
            ProgressValue = 0;
            _errorDatabaseLogService.RemoveByToday();
            RemoveLDF();

            BackgroundWorkerHelper.Run((s, e) =>
            {
                IsProcessReport = true;
                CountCompareDatabase = 0;
                string script = "";
                string standardMDFPath = @"|DataDirectory|AppData\Standard_MDF\QLNS_TIEUCHUAN.mdf";
                string localDbConnectionString = $"Data Source=(LocalDB)\\v11.0;Database=QLNS_TIEUCHUAN;AttachDbFilename={standardMDFPath};Integrated Security=True;Connect Timeout=10;MultipleActiveResultSets=True;";
                ServerConnection standardServerConnection = new ServerConnection();
                standardServerConnection.ConnectionString = localDbConnectionString;


                try
                {
                    if (_iMigrationDataService.DetachDatabase(@"(LocalDB)\v11.0", "QLNS_TIEUCHUAN"))
                    {
                        standardServerConnection.Connect();
                        Server server = new Server(standardServerConnection);
                        Database standardDB = server.Databases["QLNS_TIEUCHUAN"];
                        Database currentDB = server.Databases[_sessionService.Current.DbName.Substring(6)];
                        int index = 0;

                        List<Table> tableStandard = standardDB.Tables.OfType<Table>().Where(x => !x.IsSystemObject).ToList();
                        List<StoredProcedure> storeStandard = standardDB.StoredProcedures.OfType<StoredProcedure>().Where(x => !x.IsSystemObject).ToList();
                        int count = tableStandard.Count + storeStandard.Count;

                        foreach (Table table1 in tableStandard)
                        {
                            (s as BackgroundWorker).ReportProgress((index++) * 100 / count, null);
                            Table table2 = currentDB.Tables[table1.Name, table1.Schema];
                            if (table2 == null)
                            {
                                CountCompareDatabase++;
                                //_logger.Warn($"Table {table1.Name} exists in DB1 but not in DB2");
                                _errorDatabaseLogService.Add(new ErrorDatabaseLog
                                {
                                    Object = $"Bảng {table1.Name}",
                                    Reason = "Không tồn tại",
                                    Description = ""
                                });
                                _logger.Warn(@$"Table {table1.Name} not exists");

                                ScriptingOptions options = new ScriptingOptions
                                {
                                    ScriptForCreateDrop = true,
                                    ScriptData = false, // Set to true if you want to script data as well
                                    ScriptSchema = true, // Set to true to script the schema
                                    IncludeIfNotExists = true,
                                    IncludeHeaders = true,
                                    FileName = string.Empty,
                                    AppendToFile = false,
                                    ScriptBatchTerminator = true
                                };
                                StringCollection script1 = table1.Script(options);

                                script += string.Join(BatchTerminator, script1.Cast<string>()) + BatchTerminator;
                                continue;
                            }
                            else
                            {
                                CompareTableColumns(table1, table2, (tableName, columnName, columnType) =>
                                {
                                    string script1 = $@"
                                    if not exists (
                                        select * from sys.columns
                                        where name = '{columnName}'
                                        and object_id = object_id('{tableName}'))
                                    begin
                                        alter table {tableName} add {columnName} {columnType};
                                    end
                                ";
                                    script += script1 + BatchTerminator;
                                });
                            }
                        }

                        foreach (StoredProcedure sp1 in storeStandard)
                        {
                            if (!sp1.IsSystemObject)
                            {
                                (s as BackgroundWorker).ReportProgress((index++) * 100 / count, null);
                                StoredProcedure sp2 = currentDB.StoredProcedures[sp1.Name, sp1.Schema];
                                if (sp2 == null)
                                {
                                    //_logger.Warn($"Stored procedure {sp1.Name} exists in DB1 but not in DB2");
                                    CountCompareDatabase++;
                                    _logger.Warn(@$"Stored procedure {sp1.Name} not exists");
                                    _errorDatabaseLogService.Add(new ErrorDatabaseLog
                                    {
                                        Object = $"Thủ tục {sp1.Name}",
                                        Reason = "không tồn tại",
                                        Description = $""
                                    });
                                    ScriptingOptions options = new ScriptingOptions
                                    {
                                        ScriptForCreateDrop = true,
                                        ScriptData = false, // Set to true if you want to script data as well
                                        ScriptSchema = true, // Set to true to script the schema
                                        IncludeIfNotExists = true,
                                        IncludeHeaders = true,
                                        EnforceScriptingOptions = true,
                                        FileName = string.Empty,
                                        AppendToFile = false,
                                        ScriptBatchTerminator = true
                                    };
                                    StringCollection script1 = sp1.Script(options);

                                    script += string.Join(BatchTerminator, script1.Cast<string>()) + BatchTerminator;
                                    continue;
                                }
                                else
                                {
                                    CompareProcedureText(sp1.TextBody, sp2.TextBody, sp1.Name, () =>
                                    {
                                        ScriptingOptions options = new ScriptingOptions
                                        {
                                            ScriptForCreateDrop = true,
                                            ScriptData = false, // Set to true if you want to script data as well
                                            ScriptSchema = true, // Set to true to script the schema
                                            IncludeIfNotExists = true,
                                            IncludeHeaders = true,
                                            EnforceScriptingOptions = true,
                                            FileName = string.Empty,
                                            AppendToFile = false,
                                            ScriptBatchTerminator = true
                                        };
                                        StringCollection script1 = sp1.Script(options);

                                        script += string.Join(BatchTerminator, script1.Cast<string>()) + BatchTerminator;
                                    });
                                }

                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    _logger.Error(ex.Message, ex);
                    IsProcessReport = false;
                }
                finally
                {
                    if (standardServerConnection.IsOpen)
                    {
                        standardServerConnection.Disconnect();
                    }
                    DropDatabase("QLNS_TIEUCHUAN", script);
                    RemoveLDF();
                }
            }, (s, e) =>
            {
                if (e.Error is null)
                {
                    IsProcessReport = false;
                    if (CountCompareDatabase == 0)
                    {
                        new NSMessageBoxViewModel("Cơ sở dữ liệu không có lỗi").ShowDialogHost();
                    }
                    else
                    {
                        new NSMessageBoxViewModel($"Cơ sở dữ liệu có {CountCompareDatabase} lỗi xảy ra. Vui lòng kiểm tra, lấy file log và liên hệ bộ phận chăm sóc khách hàng", "Lỗi", NSMessageBoxButtons.OK, PackIconKind.Error, null).ShowDialogHost();
                    }
                }
            }, (s, e) =>
            {
                ProgressValue = e.ProgressPercentage;
            });
        }

        private void RemoveLDF()
        {
            if (File.Exists(@"AppData\Standard_MDF\QLNS_TIEUCHUAN_log.ldf"))
            {
                File.Delete(@"AppData\Standard_MDF\QLNS_TIEUCHUAN_log.ldf");
            }
        }

        public void DropDatabase(string databaseName, string script)
        {
            if (_iMigrationDataService.DetachDatabase(@"(LocalDB)\v11.0", databaseName))
            {
                using (SqlConnection conn = new SqlConnection())
                {
                    ConnectionType _connectionType = _configuration.GetSection("DbSettings:ConnectionType").Value == ConnectionType.SqlServer.ToString() ? ConnectionType.SqlServer : ConnectionType.LocalDb;
                    conn.ConnectionString = _configuration.GetConnectionString(_connectionType.ToString());
                    conn.Open();
                    List<MigrationStatement> ms = new List<MigrationStatement>();
                    StatementBatch(ms, script);
                    using (SqlCommand queryCmd = new SqlCommand())
                    {
                        queryCmd.Connection = conn;
                        foreach (MigrationStatement item in ms)
                        {
                            if (!string.IsNullOrEmpty(item.Sql))
                            {
                                queryCmd.CommandText = item.Sql;
                                queryCmd.ExecuteNonQuery();
                            }
                        }
                    }
                }
            }
        }

        private void StatementBatch(List<MigrationStatement> ms, string sqlBatch, bool suppressTransaction = false)
        {
            string BatchTerminator = "GO";

            // Handle backslash utility statement (see http://technet.microsoft.com/en-us/library/dd207007.aspx)
            sqlBatch = Regex.Replace(sqlBatch, @"\\(\r\n|\r|\n)", "");

            // Handle batch splitting utility statement (see http://technet.microsoft.com/en-us/library/ms188037.aspx)
            string[] batches = Regex.Split(sqlBatch,
                String.Format(CultureInfo.InvariantCulture, @"^\s*({0}[ \t]+[0-9]+|{0})(?:\s+|$)", BatchTerminator),
                RegexOptions.IgnoreCase | RegexOptions.Multiline);

            for (int i = 0; i < batches.Length; ++i)
            {
                // Skip batches that merely contain the batch terminator
                if (batches[i].StartsWith(BatchTerminator, StringComparison.OrdinalIgnoreCase) ||
                    (i == batches.Length - 1 && string.IsNullOrWhiteSpace(batches[i])))
                {
                    continue;
                }

                // Include batch terminator if the next element is a batch terminator
                if (batches.Length > i + 1 &&
                    batches[i + 1].StartsWith(BatchTerminator, StringComparison.OrdinalIgnoreCase))
                {
                    int repeatCount = 1;

                    // Handle count parameter on the batch splitting utility statement
                    if (!batches[i + 1].ToLower().Equals(BatchTerminator.ToLower()))
                    {
                        repeatCount = int.Parse(Regex.Match(batches[i + 1], @"([0-9]+)").Value, CultureInfo.InvariantCulture);
                    }

                    for (int j = 0; j < repeatCount; ++j)
                        ms.Add(new MigrationStatement { Sql = batches[i], SuppressTransaction = suppressTransaction, BatchTerminator = BatchTerminator });
                }
                else
                {
                    ms.Add(new MigrationStatement { Sql = batches[i], SuppressTransaction = suppressTransaction });
                }
            }
        }

        private void CompareProcedureText(string text1, string text2, string procedureName, Action action)
        {
            if (text1.Trim() != text2.Trim())
            {
                //_logger.Warn($"Stored procedure {procedureName} has different definitions in DB1 and DB2");
                CountCompareDatabase++;
                action();
                _errorDatabaseLogService.Add(new ErrorDatabaseLog
                {
                    Object = $"Thủ tục {procedureName}",
                    Reason = "Sai nội dung",
                    Description = ""
                });
                _logger.Warn(@$"Stored procedure {procedureName} is wrong");
            }
        }

        private void CompareTableColumns(Table table1, Table table2, Action<string, string, string> action)
        {
            foreach (Column column1 in table1.Columns)
            {
                Column column2 = table2.Columns[column1.Name];
                if (column2 == null)
                {
                    CountCompareDatabase++;
                    action(table1.Name, column1.Name, column1.DataType.Name);
                    //_logger.Warn($"Column {column1.Name} exists in {table1.Name} in DB1 but not in DB2");
                    _errorDatabaseLogService.Add(new ErrorDatabaseLog
                    {
                        Object = $"Bảng {table1.Name}",
                        Reason = "Thiếu cột",
                        Description = $"{column1.Name}"
                    });
                    _logger.Warn(@$"Table {table1.Name} not exists {column1.Name}");
                }
            }
        }

        public void Backup(string module, string post = "")
        {
            string currentPath = AppDomain.CurrentDomain.BaseDirectory;
            string pathFolder = Path.Combine(currentPath, "scripts");
            if (!Directory.Exists(pathFolder))
            {
                Directory.CreateDirectory(pathFolder);
            }

            using FolderBrowserDialog folder = new FolderBrowserDialog();
            DialogResult result = folder.ShowDialog();

            string fileName = $"{module}_{Guid.NewGuid().ToString()}.sql";
            string fileZip = $"{module}_{Guid.NewGuid().ToString()}.zip";

            if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(folder.SelectedPath))
            {
                BackgroundWorkerHelper.Run((e, s) =>
                {
                    IsLoading = true;
                    StringBuilder sb = new StringBuilder();
                    Server srv = new Server(new ServerConnection("(LocalDB)\\v11.0"));
                    Database dbs = srv.Databases[_sessionService.Current.DbName.Substring(6)];

                    ScriptingOptions options = new ScriptingOptions();
                    options.ScriptForCreateDrop = true;
                    options.ScriptData = true;
                    options.ScriptSchema = true;
                    options.FileName = Path.Combine(folder.SelectedPath, fileName);
                    options.EnforceScriptingOptions = true;
                    options.IncludeHeaders = true;
                    options.AppendToFile = true;
                    options.Indexes = true;
                    options.WithDependencies = true;

                    for (int i = 0; i < dbs.Tables.Count; i++)
                    {
                        if (!string.IsNullOrEmpty(post))
                        {
                            if (dbs.Tables[i].Name.StartsWith(module) && dbs.Tables[i].Name.EndsWith(post))
                            {
                                dbs.Tables[i].EnumScript(options);
                            }
                        }
                        else
                        {
                            if (dbs.Tables[i].Name.StartsWith(module))
                            {
                                dbs.Tables[i].EnumScript(options);
                            }
                        }
                    }
                }, (e, s) =>
                {
                    if (s.Error is null)
                    {
                        using (ZipArchive zip = ZipFile.Open(Path.Combine(folder.SelectedPath, fileZip), ZipArchiveMode.Create))
                        {
                            zip.CreateEntryFromFile(Path.Combine(folder.SelectedPath, fileName), fileName);
                        }
                        File.Delete(Path.Combine(folder.SelectedPath, fileName));

                        IOExtensions.OpenFolder(folder.SelectedPath);
                        IsLoading = false;
                    }
                });
            }
        }

        private void OnCopyDoiTuong()
        {
            BackgroundWorkerHelper.Run((s, e) =>
            {
                IsLoading3 = true;
                _iMigrationDataService.CopyDoiTuong(_sessionService.Current.YearOfWork, (int)SourceYear);
            }, (s, e) =>
            {
                IsLoading3 = false;
                if (e.Error is null)
                {
                    new NSMessageBoxViewModel(Resources.SuccessCopyDoiTuong).ShowDialogHost();
                }
                else
                {
                    new NSMessageBoxViewModel(Resources.ErrorManipulation, "Lỗi", NSMessageBoxButtons.OK, PackIconKind.Error, null).ShowDialogHost();
                }
            });
        }

        private void OnRestoreMLNSThuNop()
        {
            new NSMessageBoxViewModel($"Phục hồi MLNS Thu nộp năm 2025 (đầu 8) sẽ mất hết các mục lục cũ đầu 8 năm 2025", "Xác nhận", NSMessageBoxButtons.YesNo, OnRestoreMLNSThuNopHandler).ShowDialogHost();
        }
        private void OnRestoreMLSKT()
        {
            System.Windows.Application.Current.Dispatcher.Invoke(delegate
            {
                new NSMessageBoxViewModel($"Phục hồi mục lục SKT, MAP_MLSKT năm {RestoreYear} sẽ mất hết các mục lục cũ", "Xác nhận", NSMessageBoxButtons.YesNo, OnRestoreMLSKTHandler).ShowDialogHost();
            });
        }

        private void OnRemoveSKTChiTietHandler(NSDialogResult result)
        {
            if (result != NSDialogResult.Yes) return;
            _iMigrationDataService.RemoveWrongSKTChiTiet((int)RestoreYear);
            _iMigrationDataService.RestoreMLSKT((int)RestoreYear);
            new NSMessageBoxViewModel($"Xóa thành công dữ liệu chi tiết SNC, SKT sai mục lục tiêu chuẩn", "Thông báo", NSMessageBoxButtons.OK, PackIconKind.Done, null).ShowDialogHost();
        }

        private void OnRestoreMLSKTHandler(NSDialogResult result)
        {
            if (result != NSDialogResult.Yes) return;
            BackgroundWorkerHelper.Run((s, e) =>
            {
                IsLoading4 = true;
                e.Result = _iMigrationDataService.RestoreMLSKT((int)RestoreYear);
            }, (s, e) =>
            {
                IsLoading4 = false;
                if (e.Error is null)
                {
                    if (e.Result is string result)
                    {
                        switch (result)
                        {
                            case "DONE":
                                new NSMessageBoxViewModel(string.Format(Resources.SuccessRestoreMLSKT, RestoreYear)).ShowDialogHost();
                                break;
                            default:
                                new NSMessageBoxViewModel(string.Format("Xóa dữ liệu chi tiết SNC, SKT có mục lục sai: {0}", result), "Lỗi", NSMessageBoxButtons.YesNo, PackIconKind.Error, OnRemoveSKTChiTietHandler).ShowDialogHost();
                                break;
                        }
                    }
                }
                else
                {
                    new NSMessageBoxViewModel(Resources.ErrorManipulation, "Lỗi", NSMessageBoxButtons.OK, PackIconKind.Error, null).ShowDialogHost();
                }
            });
        }

        private void OnRestoreMLNSThuNopHandler(NSDialogResult result)
        {
            if (result != NSDialogResult.Yes) return;
            BackgroundWorkerHelper.Run((s, e) =>
            {
                IsLoading5 = true;
                _iMigrationDataService.RestoreMLNSThuNop();
            }, (s, e) =>
            {
                IsLoading5 = false;
                if (e.Error is null)
                {
                    new NSMessageBoxViewModel("Phục hồi MLNS Thu nộp năm 2025 thành công").ShowDialogHost();
                }
                else if (e.Error is Exception exception && exception.Message.Contains("duplicate key"))
                {
                    new NSMessageBoxViewModel("Một số MLNS bị trùng khóa chính", "Lỗi", NSMessageBoxButtons.OK, PackIconKind.Error, null).ShowDialogHost();
                }
                else
                {
                    new NSMessageBoxViewModel(Resources.ErrorManipulation, "Lỗi", NSMessageBoxButtons.OK, PackIconKind.Error, null).ShowDialogHost();
                }
            });
        }

        public override void Init()
        {
            try
            {
                var appVersion = Assembly.GetExecutingAssembly().GetName().Version.ToString();
                var dbVersion = _appVersionService.GetDbInfo();

                IsShowCompareDatabase = !string.IsNullOrEmpty(appVersion) && !string.IsNullOrEmpty(dbVersion.Version) && dbVersion.Version.CompareTo(appVersion) == 0;

                Years = new ObservableCollection<ComboboxItem>();
                RestoreYears = new ObservableCollection<ComboboxItem>
                {
                    new ComboboxItem("2024", "2024"),
                    new ComboboxItem("2025", "2025")
                };
                IEnumerable<DanhMuc> danhMucNamLV = _dmService.FindByType("NS_NamLamViec").OrderBy(c => c.INamLamViec).ToList();
                foreach (DanhMuc namLV in danhMucNamLV)
                {
                    Years.Add(new ComboboxItem { DisplayItem = namLV.SGiaTri, ValueItem = namLV.SGiaTri });
                }
                SourceYear = int.Parse(Years.FirstOrDefault().ValueItem);
                RestoreYear = int.Parse(RestoreYears.FirstOrDefault().ValueItem);

                Modules = new ObservableCollection<ComboboxItem>()
                {
                    new ComboboxItem("Ngân sách", "Budget"),
                    new ComboboxItem("Vốn đầu tư", "Investment"),
                    new ComboboxItem("Ngoại hối", "Forex"),
                    new ComboboxItem("Lương", "Salary"),
                    new ComboboxItem("Lương NQ104", "NewSalary"),
                    new ComboboxItem("Bảo hiểm xã hội", "SocialInsurance"),
                    new ComboboxItem("Hệ thống", "System"),
                };

                SelectedModule = Modules.FirstOrDefault().ValueItem;
            }
            catch (Exception)
            {

            }
        }
    }
}
