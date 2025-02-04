using log4net;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.ObjectModel;
using System.Data.SqlClient;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using VTS.QLNS.CTC.App.Command;
using VTS.QLNS.CTC.App.Helper;
using VTS.QLNS.CTC.App.Model.Control;
using VTS.QLNS.CTC.App.Model.Setting;
using VTS.QLNS.CTC.App.Properties;
using VTS.QLNS.CTC.Core.Service;
using VTS.QLNS.CTC.Utility;

namespace VTS.QLNS.CTC.App.ViewModel.Setting
{
    public class ConnectDatabaseViewModel : ViewModelBase
    {
        private readonly IWritableOptions<ConnectionStrings> _writableConnectionString;
        private readonly IWritableOptions<DbSettings> _writableDbSetting;
        private readonly IConfiguration _configuration;
        private readonly IMigrationDataService _migrationDataService;
        private readonly IDatabaseService _databaseService;
        private readonly ILog _loger;

        public delegate void DataChangedEventHandler(object sender, EventArgs e);
        public event DataChangedEventHandler UpdateConnectionString;

        public string ConnectionString;
        public string SelectedFilePath;
        public string SelectedDbName;

        private bool isCheckConnection = false;
        private bool statusConnection = false;

        private bool _isSelectedDbFile;
        public bool IsSelectedDbFile
        {
            get => _isSelectedDbFile;
            set
            {
                SetProperty(ref _isSelectedDbFile, value);
                OnPropertyChanged(nameof(_isSelectedDbFile));
            }
        }

        private ConnectionType _connectType;
        public ConnectionType ConnectType
        {
            get => _connectType;
            set
            {
                SetProperty(ref _connectType, value);
                IsSelectedDbFile = _connectType == ConnectionType.SqlServer;

                OnPropertyChanged(nameof(IsLocalConnect));
            }
        }

        private DatabaseConfigurationModel _dbConfigServer;
        public DatabaseConfigurationModel DbConfigServer
        {
            get => _dbConfigServer;
            set => SetProperty(ref _dbConfigServer, value);
        }

        private DatabaseConfigurationModel _dbConfigLocal;
        public DatabaseConfigurationModel DbConfigLocal
        {
            get => _dbConfigLocal;
            set => SetProperty(ref _dbConfigLocal, value);
        }

        public bool IsLocalConnect => _connectType == ConnectionType.LocalDb;

        private ObservableCollection<ComboboxItem> _localDbTypes;
        public ObservableCollection<ComboboxItem> LocalDbTypes
        {
            get => _localDbTypes;
            set => SetProperty(ref _localDbTypes, value);
        }

        private ComboboxItem _localDbType;
        public ComboboxItem LocalDbType
        {
            get => _localDbType;
            set => SetProperty(ref _localDbType, value);
        }

        public RelayCommand CheckConnectionCommand { get; }
        public RelayCommand RefreshCommand { get; }
        public RelayCommand ChooseDBFileCommand { get; }

        public ConnectDatabaseViewModel(
            //IWritableOptions<ConnectionStrings> writableConnectionString,
            //IWritableOptions<DbSettings> writableDbSetting,
            IConfiguration configuration,
            ILog log,
            IMigrationDataService migrationDataService,
            IDatabaseService databaseService)
        {
            //_writableConnectionString = writableConnectionString;
            //_writableDbSetting = writableDbSetting;
            _configuration = configuration;
            _migrationDataService = migrationDataService;
            _databaseService = databaseService;
            _loger = log;


            CheckConnectionCommand = new RelayCommand(obj => OnCheckConnection(), obj => IsEnableCheckConnection(obj));
            RefreshCommand = new RelayCommand(obj =>
            {
                if (ConnectType == ConnectionType.SqlServer)
                    DbConfigServer = new DatabaseConfigurationModel();
                else DbConfigLocal = new DatabaseConfigurationModel();
            });
            SaveCommand = new RelayCommand(obj => OnSave(obj), obj => IsEnableCheckConnection(obj));
            ChooseDBFileCommand = new RelayCommand(obj => OnChooseDBFile(obj));
        }

        private bool IsEnableCheckConnection(object obj)
        {
            if (ConnectType == ConnectionType.SqlServer)
            {
                return !string.IsNullOrWhiteSpace(DbConfigServer.Server)
                    && !string.IsNullOrWhiteSpace(DbConfigServer.DbName)
                    && !string.IsNullOrWhiteSpace(DbConfigServer.UserId)
                    && !string.IsNullOrWhiteSpace(DbConfigServer.Password);
            }

            return !string.IsNullOrWhiteSpace(DbConfigLocal.Server)
                    && !string.IsNullOrWhiteSpace(DbConfigLocal.DbPath);

        }

        private void OnChooseDBFile(object obj)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog()
            {
                Filter = "mdf files (*.mdf)|*.mdf",
                Title = "Chọn DB mẫu"
            };
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                SelectedFilePath = openFileDialog.FileName;
                //SelectedDbName = string.Concat(NSConstants.QLNS_DB_NAME, "_", DateTime.Now.ToString("yyyyMMddhhmmss"));
                //DbConfigLocal.DbName = SelectedDbName;
                //DbConfigLocal.DbPath = string.Concat("|DataDirectory|AppData\\", SelectedDbName, ".mdf");
                SelectedDbName = SelectedFilePath.Split('\\').Last().Replace(".mdf", "").Replace(".MDF", "");
                DbConfigLocal.DbName = SelectedDbName;
                DbConfigLocal.DbPath = string.Concat("|DataDirectory|AppData\\", SelectedDbName, ".mdf");
                IsSelectedDbFile = true;
            }
        }

        public override void Init()
        {
            base.Init();

            LocalDbTypes = new ObservableCollection<ComboboxItem>
            {
                new ComboboxItem { ValueItem = "(LocalDb)\\v11.0", DisplayItem = "(LocalDb)\\v11.0"},
                new ComboboxItem { ValueItem = "(LocalDb)\\MSSQLLocalDB", DisplayItem = "(LocalDb)\\MSSQLLocalDB"},
            };
            string connectionString = _configuration.GetConnectionString(ConnectionType.LocalDb.ToString());
            SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder(connectionString);

            LocalDbType = LocalDbTypes.FirstOrDefault(x => x.ValueItem == builder.DataSource) ?? LocalDbTypes.ElementAt(0);

            _isSelectedDbFile = false;
            LoadServerConnectionString();
            LoadLocalConnectionString();
        }

        private void LoadServerConnectionString()
        {
            string connectionString = _configuration.GetConnectionString(ConnectionType.SqlServer.ToString());
            SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder(connectionString);
            _dbConfigServer = new DatabaseConfigurationModel
            {
                Server = builder.DataSource,
                DbName = builder.InitialCatalog,
                UserId = builder.UserID,
                Password = builder.Password,
                ConnectionString = connectionString
            };
        }

        private void LoadLocalConnectionString()
        {
            string connectionString = _configuration.GetConnectionString(ConnectionType.LocalDb.ToString());
            SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder(connectionString);
            _dbConfigLocal = new DatabaseConfigurationModel
            {
                Server = builder.DataSource,
                DbPath = string.IsNullOrEmpty(builder.AttachDBFilename) ? _migrationDataService.GetPhysicalMDF(builder.InitialCatalog) : builder.AttachDBFilename,
                ConnectionString = connectionString
            };
        }

        private void OnCheckConnection()
        {
            isCheckConnection = true;
            if (CheckConnection())
            {
                statusConnection = true;
                MessageBox.Show(Resources.MsgConnectDbSuccess, Resources.NotifiTitle, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                statusConnection = false;
                MessageBox.Show(Resources.MsgConnectDbFail, Resources.NotifiTitle, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private bool CheckConnection()
        {
            try
            {
                SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();
                if (ConnectType == ConnectionType.SqlServer)
                {
                    builder.DataSource = DbConfigServer.Server;
                    builder.InitialCatalog = DbConfigServer.DbName;
                    builder.UserID = DbConfigServer.UserId;
                    builder.Password = DbConfigServer.Password;
                }
                else
                {
                    builder.DataSource = DbConfigLocal.Server;
                    builder.AttachDBFilename = string.Concat("|DataDirectory|AppData\\", SelectedDbName, ".mdf");
                    builder.IntegratedSecurity = true;
                    string currentLocation = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
                    string dbFolder = Path.Combine(currentLocation, "AppData");

                    if (!string.IsNullOrEmpty(SelectedFilePath))
                    {
                        string connectionString = _configuration.GetConnectionString(ConnectionType.LocalDb.ToString());
                        string schema = "";
                        string schemaName = "";
                        SqlConnection connection = new SqlConnection(connectionString);
                        using (connection)
                        {
                            try
                            {
                                connection.Open();

                                using (SqlCommand command = new SqlCommand())
                                {
                                    // get current database name
                                    command.CommandText = "select db_name() as [current database]";
                                    command.CommandType = System.Data.CommandType.Text;
                                    command.Connection = connection;
                                    schema = (string)command.ExecuteScalar();
                                    schemaName = schema.Split('\\').Last<string>().Replace(".mdf", "").Replace(".MDF", "");

                                    // get physical mdf path 
                                    command.CommandType = System.Data.CommandType.Text;
                                    command.Connection = connection;
                                    command.CommandText = "select physical_name from sys.database_files where type = 0";
                                    string currentDbPath = (string)command.ExecuteScalar();
                                    string currentLogPath = currentDbPath.Replace(".mdf", "_log.ldf");

                                    // set offline database
                                    command.CommandText = "USE[master]; ALTER DATABASE[" + schema + "] SET OFFLINE WITH ROLLBACK IMMEDIATE";
                                    command.CommandType = System.Data.CommandType.Text;
                                    command.Connection = connection;
                                    command.CommandTimeout = 900;
                                    command.ExecuteNonQuery();

                                    // zip physical mdf file
                                    using (ZipArchive zip = ZipFile.Open(string.Concat(dbFolder, "\\", schemaName, "_", DateTime.Now.ToString("yyyyMMddhhmmss"), ".zip"), ZipArchiveMode.Create))
                                    {
                                        zip.CreateEntryFromFile(currentDbPath, schemaName + ".mdf");
                                    }

                                    // drop current database
                                    command.CommandText = "USE[master]; DROP DATABASE[" + schema + "]";
                                    command.CommandType = System.Data.CommandType.Text;
                                    command.Connection = connection;
                                    command.CommandTimeout = 900;
                                    command.ExecuteNonQuery();
                                    if (File.Exists(currentDbPath)) File.Delete(currentDbPath);
                                    if (File.Exists(currentLogPath)) File.Delete(currentLogPath);

                                    // drop  database if exists
                                    command.CommandText = "USE[master]; IF EXISTS(select * from sys.databases where name='" + SelectedDbName + "') DROP DATABASE[" + SelectedDbName + "]";
                                    command.CommandType = System.Data.CommandType.Text;
                                    command.Connection = connection;
                                    command.CommandTimeout = 900;
                                    command.ExecuteNonQuery();
                                }
                            }
                            catch (Exception e)
                            {
                                _loger.Error(e);
                            }
                            finally
                            {
                                // create new database from selectedDbName
                                string dbPath2 = Path.Combine(dbFolder, SelectedDbName + ".mdf");
                                if (!SelectedFilePath.Equals(dbPath2))
                                {
                                    if (File.Exists(dbPath2)) File.Delete(dbPath2);
                                    if (File.Exists(dbPath2.Replace(".mdf", "_log.ldf"))) File.Delete(dbPath2.Replace(".mdf", "_log.ldf"));
                                    File.Copy(SelectedFilePath, dbPath2);
                                }
                            }

                            //string dbPath = Path.Combine(dbFolder, SelectedDbName + ".mdf");
                            //if (!File.Exists(dbPath)) File.Copy(SelectedFilePath, dbPath);
                        }
                    }
                }

                return _databaseService.CheckConnection(builder.ConnectionString);
            }
            catch (Exception e)
            {
                _loger.Error(e);
                return false;
            }
        }

        public override void OnSave(object obj)
        {
            if (!isCheckConnection) statusConnection = CheckConnection();
            if (statusConnection)
            {
                WriteLogChangeDb();
                SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();
                if (ConnectType == ConnectionType.SqlServer)
                {
                    builder.DataSource = DbConfigServer.Server;
                    builder.InitialCatalog = DbConfigServer.DbName;
                    builder.UserID = DbConfigServer.UserId;
                    builder.Password = DbConfigServer.Password;
                }
                else
                {
                    builder.DataSource = DbConfigLocal.Server;
                    builder.AttachDBFilename = string.Concat("|DataDirectory|AppData\\", SelectedDbName, ".mdf");
                    builder.IntegratedSecurity = true;
                }
                /*_writableDbSetting.Update(opt => opt.ConnectionType = ConnectType.ToString());
                _writableConnectionString.Update(opt =>
                {
                    if (ConnectType == ConnectionType.SqlServer)
                    {
                        DbConfigServer.ConnectionString = builder.ConnectionString;
                        opt.SqlServer = DbConfigServer.ConnectionString;
                    }
                    else
                    {
                        DbConfigLocal.ConnectionString = builder.ConnectionString;
                        opt.LocalDb = DbConfigLocal.ConnectionString;
                    }
                });*/
                ConnectionStrings connectionStrings = new ConnectionStrings
                {
                    SqlServer = "Server=Localhost;",
                    LocalDb = "Server=(LocalDB)\\v11.0;Database={0};Trusted_Connection=True;AttachDbFileName={1}",
                };
                DbSettings dbSettings = new DbSettings
                {
                    ConnectionType = ConnectType.ToString()
                };
                if (ConnectType == ConnectionType.SqlServer)
                {
                    DbConfigServer.ConnectionString = builder.ConnectionString;
                }
                else
                {
                    DbConfigLocal.ConnectionString = builder.ConnectionString;
                }
                connectionStrings.SqlServer = DbConfigServer.ConnectionString;

                if (!string.IsNullOrEmpty(SelectedDbName))
                {
                    connectionStrings.LocalDb = string.Format(connectionStrings.LocalDb, SelectedDbName, DbConfigLocal.DbPath);
                }
                else
                {
                    connectionStrings.LocalDb = builder.ConnectionString;
                }
                string currentLocation = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
                string physicalPath = Path.Combine(currentLocation, "AppData", "_configs", "dbconfig.json");
                object jsonObject = new { ConnectionStrings = connectionStrings, DbSettings = dbSettings };
                string json = JsonConvert.SerializeObject(jsonObject, Formatting.Indented);
                File.WriteAllText(physicalPath, json);

                ConnectionString = ConnectType == ConnectionType.SqlServer ? DbConfigServer.ConnectionString : DbConfigLocal.ConnectionString;
                MessageBox.Show(Resources.AlertChangeConnectionString, Resources.NotifiTitle, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                System.Windows.Window window = obj as System.Windows.Window;
                window.Close();
            }
            else MessageBox.Show(Resources.MsgConnectDbFail, Resources.NotifiTitle, MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        public override void OnClose(object obj)
        {
            DataChangedEventHandler handler = UpdateConnectionString;
            if (handler != null)
            {
                handler(ConnectionString, new EventArgs());
            }
            System.Windows.Window window = obj as System.Windows.Window;
            window.Close();
        }

        private void WriteLogChangeDb()
        {
            SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder(_configuration.GetConnectionString("LocalDb"));
            if (builder != null)
            {
                IOExtensions.WriteLogChangeDatabase(builder.InitialCatalog, SelectedDbName);
            }
        }
    }
}
