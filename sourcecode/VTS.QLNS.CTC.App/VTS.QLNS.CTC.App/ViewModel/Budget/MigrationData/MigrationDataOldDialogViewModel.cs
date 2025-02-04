using ControlzEx.Standard;
using log4net;
using MaterialDesignThemes.Wpf;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Configuration;
using Microsoft.SqlServer.Management.Common;
using Microsoft.SqlServer.Management.Smo;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Globalization;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net.Configuration;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using VTS.QLNS.CTC.App.Command;
using VTS.QLNS.CTC.App.Helper;
using VTS.QLNS.CTC.App.Model;
using VTS.QLNS.CTC.App.Model.Control;
using VTS.QLNS.CTC.App.Properties;
using VTS.QLNS.CTC.App.Service;
using VTS.QLNS.CTC.App.View.Budget.MigrationData;
using VTS.QLNS.CTC.App.View.SystemAdmin.Utilities;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Service;
using VTS.QLNS.CTC.Core.Service.Impl;
using VTS.QLNS.CTC.Utility;
using static Microsoft.SqlServer.Management.Sdk.Sfc.FilterNodeConstant;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ToolTip;
using static VTS.QLNS.CTC.Core.Extensions.MigrationBuilderExtension;
using ConnectionType = VTS.QLNS.CTC.Utility.ConnectionType;


namespace VTS.QLNS.CTC.App.ViewModel.SystemAdmin.Utilities
{
    public class MigrationDataOldDialogViewModel : ViewModelBase
    {
        public override string Name => "Tính toán lại dữ liệu";
        public override string Description => "Tính toán lại dữ liệu";
        public override string Title => "Tính toán lại dữ liệu";
        public override Type ContentType => typeof(MigrationDataOldDialog);
        public override PackIconKind IconKind => PackIconKind.DatabaseCogOutline;
        private readonly IMigrationDataService _iMigrationDataService;
        private readonly IErrorDatabaseLogService _errorDatabaseLogService;
        private readonly ISessionService _sessionService;
        private readonly IDatabaseService _databaseService;
        private readonly IDanhMucService _dmService;
        private readonly IConfiguration _configuration;
        private readonly string _logPath;
        private readonly ILog _logger;

        private ObservableCollection<HtTableMigrateModel> _errorDatabaseLogs;
        public ObservableCollection<HtTableMigrateModel> ErrorDatabaseLogs
        {
            get => _errorDatabaseLogs;
            set => SetProperty(ref _errorDatabaseLogs, value);
        }

        public MigrationDataOldDialogViewModel(
            ILog logger,
            IConfiguration configuration,
            IMigrationDataService iMigrationDataService,
            ISessionService sessionService,
            IErrorDatabaseLogService errorDatabaseLogService,
            IDatabaseService databaseService)
        {
            _logger = logger;
            _errorDatabaseLogService = errorDatabaseLogService;
            _configuration = configuration;
            _sessionService = sessionService;
            _databaseService = databaseService;
        }

        public override void Init()
        {
        }
    }
}
