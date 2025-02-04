using ICSharpCode.AvalonEdit.Document;
using MaterialDesignThemes.Wpf;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Windows.Forms;
using VTS.QLNS.CTC.App.Command;
using VTS.QLNS.CTC.App.Service;
using VTS.QLNS.CTC.App.Properties;
using VTS.QLNS.CTC.App.View.SystemAdmin.Utilities;
using VTS.QLNS.CTC.Core.Service;
using VTS.QLNS.CTC.Utility;
using VTS.QLNS.CTC.App.Service.Impl;
using FlexCel.Core;
using System.Reflection;
using System.Linq;
using VTS.QLNS.CTC.App.Model;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Data;

namespace VTS.QLNS.CTC.App.ViewModel.SystemAdmin.Utilities.Tool
{
    public class QueryDataViewModel : ViewModelBase
    {
        private readonly IExportService _exportService;
        private readonly IConfiguration _configuration;
        private readonly IDatabaseService _databaseService;
        private ICollectionView _tableCollection;
        private string _filePath;
        private string _connectionString;

        public override string FuncCode => NSFunctionCode.SYSTEM_UTILITIES_QUERY_DATA;
        public override string Name => "Truy vấn dữ liệu";
        public override string Description => "Công cụ truy vấn dữ liệu";
        public override string Title => "Công cụ truy vấn dữ liệu";
        public override Type ContentType => typeof(QueryData);
        public override PackIconKind IconKind => PackIconKind.DatabaseSearchOutline;

        private string _sqlQuerySelected;
        public string SqlQuerySelected
        {
            get => _sqlQuerySelected;
            set => SetProperty(ref _sqlQuerySelected, value);
        }

        private string _notification;
        public string Notification
        {
            get => _notification;
            set => SetProperty(ref _notification, value);
        }

        private DataTable _result;
        public DataTable Result
        {
            get => _result;
            set => SetProperty(ref _result, value);
        }

        public bool HasData => _result != null && _result.Rows.Count > 0;

        private QueryToolSelection _tabSelection;
        public QueryToolSelection TabSelection
        {
            get => _tabSelection;
            set => SetProperty(ref _tabSelection, value);
        }

        public string _currentRecord;
        public string CurrentRecord
        {
            get => _currentRecord;
            set => SetProperty(ref _currentRecord, value);
        }

        public int TotalRecord => _result.Rows.Count;

        public bool IsExport => _result.Rows.Count > 0;

        public TextDocument Document { get; set; }

        private IEnumerable<string> _suggestionWords;
        public IEnumerable<string> SuggestionWords
        {
            get => _suggestionWords;
            set => SetProperty(ref _suggestionWords, value);
        }

        private ObservableCollection<DatabaseTableModel> _tables;
        public ObservableCollection<DatabaseTableModel> Tables
        {
            get => _tables;
            set => SetProperty(ref _tables, value);
        }

        private string _searchTableText;
        public string SearchTableText
        {
            get => _searchTableText;
            set
            {
                if (SetProperty(ref _searchTableText, value))
                    _tableCollection.Refresh();
            }
        }

        public RelayCommand OpenSqlCommand { get; }
        public RelayCommand SaveSqlCommand { get; }
        public RelayCommand QueryCommand { get; }
        public RelayCommand SelectionCommand { get; }
        public RelayCommand ExportCommand { get; }
        public RelayCommand CommentCommand { get; }

        public QueryDataViewModel(IConfiguration configuration, IExportService exportService, IDatabaseService databaseService)
        {
            _configuration = configuration;
            _exportService = exportService;
            _databaseService = databaseService;

            OpenSqlCommand = new RelayCommand(obj => OnOpenSql());
            SaveSqlCommand = new RelayCommand(obj => OnSaveSql());
            QueryCommand = new RelayCommand(obj => OnQuery());
            SelectionCommand = new RelayCommand(obj => OnSelection(obj));
            ExportCommand = new RelayCommand(obj => OnExport());
        }

        public override void Init()
        {
            base.Init();
            ConnectionType connectionType = _configuration.GetSection("DbSettings:ConnectionType").Value == ConnectionType.SqlServer.ToString() ? ConnectionType.SqlServer : ConnectionType.LocalDb;
            _connectionString = _configuration.GetConnectionString(connectionType.ToString());
            _result = new DataTable();
            _sqlQuerySelected = string.Empty;
            _notification = string.Empty;
            _currentRecord = "0/0";
            Document = new TextDocument();
            LoadTables();
            LoadSuggestionWords();
        }

        private void LoadTables()
        {
            string sql = "SELECT TABLE_NAME FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_TYPE = 'BASE TABLE' ORDER BY TABLE_NAME";
            DataTable tables = _databaseService.ExecuteQuery(sql, _connectionString);
            List<DatabaseTableModel> listTable = new List<DatabaseTableModel>();
            foreach (DataRow row in tables.Rows)
            {
                listTable.Add(new DatabaseTableModel
                {
                    Icon = PackIconKind.Table,
                    TableName = row["TABLE_NAME"].ToString()
                });
            }
            _tables = new ObservableCollection<DatabaseTableModel>(listTable);
            OnPropertyChanged(nameof(Tables));
            _tableCollection = CollectionViewSource.GetDefaultView(Tables);
            _tableCollection.Filter = DatabaseTableFilter;
        }

        private bool DatabaseTableFilter(object obj)
        {
            bool result = true;
            var item = (DatabaseTableModel)obj;
            if (!string.IsNullOrWhiteSpace(_searchTableText))
                result = item.TableName.ToLower().Contains(_searchTableText!.ToLower());
            item.IsFilter = result;
            return result;
        }

        private void LoadSuggestionWords()
        {
            string keyWordsPath = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "AppData\\_configs\\code_keywords_sql.txt");
            SuggestionWords = ((IEnumerable<string>)File.ReadAllLines(keyWordsPath)).Where(x => !string.IsNullOrWhiteSpace(x));
        }

        private void OnOpenSql()
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Title = string.Format("Chọn file .txt, .sql");
            dialog.RestoreDirectory = true;
            dialog.Filter = "sql files (*.sql)|*.sql|txt files (*.txt)|*.txt";
            if (dialog.ShowDialog() != DialogResult.OK)
                return;
            _filePath = dialog.FileName;
            Document.Text = File.ReadAllText(_filePath);
        }

        private void OnSaveSql()
        {
            if (string.IsNullOrEmpty(_filePath))
            {
                SaveFileDialog dialog = new SaveFileDialog();
                dialog.FileName = _databaseService.CreateSqlFileName();
                if (dialog.ShowDialog() != DialogResult.OK)
                    return;
                File.WriteAllText(dialog.FileName, Document.Text);
                _filePath = dialog.FileName;
            }
            else File.WriteAllText(_filePath, Document.Text);
        }

        private void OnQuery()
        {
            try
            {
                _result = _databaseService.ExecuteQuery(Document.Text, _connectionString);
                _notification = "Truy vấn thành công";
                _tabSelection = QueryToolSelection.Result;
            }
            catch
            {
                _notification = "Truy vấn thất bại";
                _tabSelection = QueryToolSelection.Notification;
            }

            OnPropertyChanged(nameof(Result));
            OnPropertyChanged(nameof(HasData));
            OnPropertyChanged(nameof(Notification));
            OnPropertyChanged(nameof(TabSelection));
            OnPropertyChanged(nameof(TotalRecord));

            _currentRecord = string.Format("{0}/{1}", 0, TotalRecord);
            OnPropertyChanged(nameof(CurrentRecord));
            OnPropertyChanged(nameof(IsExport));
        }

        private void OnSelection(object obj)
        {
            DataRow row = ((DataRowView)obj).Row;
            int index = _result.Rows.IndexOf(row);
            _currentRecord = string.Format("{0}/{1}", index + 1, TotalRecord);
            OnPropertyChanged(nameof(CurrentRecord));
        }

        private void OnExport()
        {
            try
            {
                ExcelFile file = _exportService.Export("export_table.xlsx", _result);
                ExportResult exportResult = new ExportResult("data", "data", "data.xlsx", file);
                _exportService.Open(exportResult, Utility.Enum.ExportType.EXCEL);
            }
            catch
            {
                MessageBox.Show(Resources.MsgExportDataTableError, Resources.NotifiTitle, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
        }
    }
}
