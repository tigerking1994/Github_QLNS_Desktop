using AutoMapper;
using log4net;
using MaterialDesignThemes.Wpf;
using Microsoft.SqlServer.Management.Common;
using System;
using System.Data.Common;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using VTS.QLNS.CTC.App.Command;
using VTS.QLNS.CTC.App.Helper;
using VTS.QLNS.CTC.App.Properties;
using VTS.QLNS.CTC.App.Service;
using VTS.QLNS.CTC.App.Service.Impl;
using VTS.QLNS.CTC.Core.Service;
using VTS.QLNS.CTC.Utility;
using VTS.QLNS.CTC.Utility.Enum;
using Microsoft.Extensions.Configuration;
using System.Windows;
using ConnectionType = VTS.QLNS.CTC.Utility.ConnectionType;
using System.Collections.Generic;
using VTS.QLNS.CTC.App.Component;

namespace VTS.QLNS.CTC.App.ViewModel.Salary.SalaryUtilities.CleanupSalaryData
{
    public class CleanupSalaryDataViewModel : ViewModelBase
    {
        private readonly ILog _logger;
        private readonly ISessionService _sessionService;
        private SessionInfo _sessionInfo;
        private readonly IDatabaseService _databaseService;
        private ITlBangLuongThangService _service;
        private readonly IConfiguration _configuration;
        private SqlConnectionStringBuilder _connectionStringBuilder;
        private ConnectionType _connectionType;
        private string _connectionString;

        public override string FuncCode => NSFunctionCode.SALARY_CHUYEN_DOI_DU_LIEU;
        public override string Name => "Dọn dẹp dữ liệu";
        public override Type ContentType => typeof(View.Salary.SalaryUtilities.CleanupSalaryData.CleanupSalaryData);
        public override PackIconKind IconKind => PackIconKind.Broom;
        public override string Title => "Dọn dẹp dữ liệu đối tượng hưởng lương, bảng lương tháng";
        public override string Description => "Dọn dẹp dữ liệu đối tượng hưởng lương, bảng lương tháng";

        private string _databaseInformation;
        public string DatabaseInformation
        {
            get => _databaseInformation;
            set => SetProperty(ref _databaseInformation, value);
        }

        private string _sCleanupBtn;
        public string SCleanupBtn
        {
            get => _sCleanupBtn;
            set => SetProperty(ref _sCleanupBtn, value);
        }

        private bool _isBackup;
        public bool IsBackup
        {
            get => _isBackup;
            set
            {
                SetProperty(ref _isBackup, value);
                if (_isBackup)
                    SCleanupBtn = "Sao lưu và dọn dẹp dữ liệu";
                else
                    SCleanupBtn = "Dọn dẹp dữ liệu";
            }
        }

        private bool _isProcessReport;
        public bool IsProcessReport
        {
            get => _isProcessReport;
            set => SetProperty(ref _isProcessReport, value);
        }

        public RelayCommand CleanDataCommand { get; }

        public CleanupSalaryDataViewModel(ILog logger,
            ISessionService sessionService,
            ITlBangLuongThangService service,
            IDatabaseService databaseService,
            IConfiguration configuration)
        {
            _logger = logger;
            _sessionService = sessionService;
            _service = service;
            _databaseService = databaseService;
            _configuration = configuration;

            CleanDataCommand = new RelayCommand(o => CleanupData());
        }

        public override void Init()
        {
            base.Init();
            IsBackup = true;
            _sessionInfo = _sessionService.Current;
            LoadDatabaseInformation();
        }

        private void CleanupData()
        {
            var messageBuilder = new StringBuilder();
            if (!IsBackup)
            {
                MessageBoxHelper.Warning(Resources.MsgCheckBackupData);
                var messageBox = new NSMessageBoxViewModel(Resources.MsgConfirmCleanSalaryData, "Xác nhận", NSMessageBoxButtons.YesNo, BackUpSalaryDataHandler);
                DialogHost.Show(messageBox.Content, "RootDialog");
            }
            else
            {
                var messageBox = new NSMessageBoxViewModel(Resources.MsgConfirmBackupCleanSalaryData, "Xác nhận", NSMessageBoxButtons.YesNo, OnBackup);
                DialogHost.Show(messageBox.Content, "RootDialog");
            }
        }

        private void BackUpSalaryDataHandler(NSDialogResult result)
        {
            try
            {
                if (result != NSDialogResult.Yes) return;
                BackgroundWorkerHelper.Run((s, e) =>
                {
                    IsProcessReport = true;
                    _service.CleanupData();
                }, (s, e) =>
                {
                    if (e.Error == null)
                    {
                        System.Windows.MessageBox.Show(Resources.MsgCleanSalaryDone, "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                    else
                    {
                        _logger.Error(e.Error.Message);
                        Helper.MessageBoxHelper.Error(Resources.ErrorManipulation);
                    }
                    IsLoading = false;
                    IsProcessReport = false;
                });
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        private void LoadDatabaseInformation()
        {
            _connectionType = _configuration.GetSection("DbSettings:ConnectionType").Value == ConnectionType.SqlServer.ToString() ? ConnectionType.SqlServer : ConnectionType.LocalDb;
            _connectionString = _configuration.GetConnectionString(_connectionType.ToString());
            _connectionStringBuilder = new SqlConnectionStringBuilder(_connectionString);
            _databaseInformation = string.Format("- Kiểu csdl: {0} {1}- Server: {2} {3}- Database: {4} {5}",
                _connectionType.ToString(), Environment.NewLine,
            _connectionStringBuilder.DataSource, Environment.NewLine,
            _connectionType == ConnectionType.SqlServer ? _connectionStringBuilder.InitialCatalog : _connectionStringBuilder.AttachDBFilename, Environment.NewLine);
        }

        private void OnBackup(NSDialogResult result)
        {
            if (result != NSDialogResult.Yes) return;
            DialogHost.Close("RootDialog");
            string fileName = _databaseService.GetBackupFilename(_connectionString, _connectionType.ToString());
            SaveFileDialog dialog = new SaveFileDialog();
            dialog.FileName = fileName;
            dialog.Title = "Lưu file sao lưu dữ liệu";
            var resultDialog = dialog.ShowDialog();
            if (resultDialog == DialogResult.OK)
            {
                string filePath = dialog.FileName;
                try
                {
                    BackgroundWorkerHelper.Run((s, e) =>
                    {
                        IsProcessReport = true;
                        if (_connectionType == ConnectionType.SqlServer)
                            _databaseService.BackupSqlServer(_connectionString, fileName, filePath);
                        else _databaseService.BackupLocal(_connectionString, fileName, filePath);
                        //System.Windows.MessageBox.Show(Resources.MsgBackupSuccess, Resources.NotifiTitle, MessageBoxButton.OK, MessageBoxImage.Information);
                        _service.CleanupData();
                    }, (s, e) =>
                    {
                        if (e.Error == null)
                        {
                            System.Windows.MessageBox.Show(Resources.MsgCleanSalaryDone, "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                        }
                        else
                        {
                            _logger.Error(e.Error.Message);
                            Helper.MessageBoxHelper.Error(Resources.ErrorManipulation);
                        }
                        IsLoading = false;
                        IsProcessReport = false;
                    });
                }
                catch (Exception ex)
                {
                    _logger.Error(ex);
                    System.Windows.MessageBox.Show(Resources.MsgBackupError, Resources.NotifiTitle, MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
        }
    }
}
