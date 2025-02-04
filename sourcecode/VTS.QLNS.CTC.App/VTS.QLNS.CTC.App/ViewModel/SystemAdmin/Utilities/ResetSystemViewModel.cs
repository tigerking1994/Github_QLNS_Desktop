using log4net;
using MaterialDesignThemes.Wpf;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VTS.QLNS.CTC.App.ViewModel.Shared;
using VTS.QLNS.CTC.Utility;

namespace VTS.QLNS.CTC.App.ViewModel.SystemAdmin.Utilities
{
    public class ResetSystemViewModel : ViewModelBase
    {
        private readonly IConfiguration _configuration;
        private readonly ILog _logger;
        public override string FuncCode => NSFunctionCode.SYSTEM_UTILITIES_QUERY_DATA;
        public override string Name => "Reset hệ thống";
        public override string Description => "Reset hệ thống";
        public override string Title => "Reset hệ thống";
        public override PackIconKind IconKind => PackIconKind.DatabaseSearchOutline;

        public ResetSystemViewModel(IConfiguration configuration, ILog log)
        {
            _configuration = configuration;
            _logger = log;
        }

        public override void Init()
        {
            base.Init();
            string messageUpdate = string.Format("{0}\n{1}", "Ứng dụng sẽ bị tắt và phải khởi động lại.", "Bạn có chắc chắn muốn reset ứng dụng?");
            var updateVersionDialogViewModel = new ResetMigrationViewModel(messageUpdate, ResetMigration, _configuration);
            updateVersionDialogViewModel.ShowDialogHost();
        }
        private void ResetMigration()
        {
            try
            {
                ConnectionType _connectionType = _configuration.GetSection("DbSettings:ConnectionType").Value == ConnectionType.SqlServer.ToString() ? ConnectionType.SqlServer : ConnectionType.LocalDb;
                string connectionString = _configuration.GetConnectionString(_connectionType.ToString());
                SqlConnection.ClearAllPools();
                var builder = new SqlConnectionStringBuilder(connectionString);
                string fullPath = builder.AttachDBFilename;
                string schema = builder.InitialCatalog.IsEmpty() ? builder.AttachDBFilename : builder.InitialCatalog;
                string strSqlDropDB = " USE [master]; DROP DATABASE [" + schema + "];";
                if (_connectionType == ConnectionType.LocalDb)
                {
                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        connection.Open();
                        using (SqlCommand command = new SqlCommand())
                        {
                            command.CommandText = strSqlDropDB;
                            command.CommandType = System.Data.CommandType.Text;
                            command.Connection = connection;
                            command.ExecuteNonQuery();
                        }
                        connection.Close();
                    }

                    if (File.Exists(fullPath))
                    {
                        File.Delete(fullPath);
                    }
                }
            }
            catch (Exception e)
            {
                _logger.Error(e);
            }
        }

    }
}
