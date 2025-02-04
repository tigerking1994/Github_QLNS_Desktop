using log4net;
using MaterialDesignThemes.Wpf;
using System;
using System.Collections.ObjectModel;
using VTS.QLNS.CTC.App.Model;
using VTS.QLNS.CTC.App.Service;
using VTS.QLNS.CTC.App.View;
using VTS.QLNS.CTC.Core.Service;

namespace VTS.QLNS.CTC.App.ViewModel
{
    public class ReleaseNoteViewModel : ViewModelBase
    {
        public override string Name => "Các cập nhật";
        public override string Description => "Thông tin chi tiết phiên bản";
        public override string Title => "What's new";
        public override Type ContentType => typeof(ReleaseNote);
        public override PackIconKind IconKind => PackIconKind.DatabaseCogOutline;
        private readonly IMigrationDataService _iMigrationDataService;
        private readonly IErrorDatabaseLogService _errorDatabaseLogService;
        private readonly ISessionService _sessionService;
        private readonly IDatabaseService _databaseService;
        private readonly IDanhMucService _dmService;
        private readonly string _logPath;
        private readonly ILog _logger;

        private ObservableCollection<AppRelease> _textLines;
        public ObservableCollection<AppRelease> TextLines
        {
            get => _textLines;
            set => SetProperty(ref _textLines, value);
        }

        public ReleaseNoteViewModel(
            ILog logger,
            IMigrationDataService iMigrationDataService,
            ISessionService sessionService,
            IErrorDatabaseLogService errorDatabaseLogService,
            IDatabaseService databaseService)
        {
            _logger = logger;
            _errorDatabaseLogService = errorDatabaseLogService;
            _sessionService = sessionService;
            _databaseService = databaseService;
        }

        public override void Init()
        {
            TextLines = new ObservableCollection<AppRelease>();
        }
    }
}
