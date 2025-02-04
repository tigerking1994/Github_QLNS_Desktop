using MaterialDesignThemes.Wpf;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using VTS.QLNS.CTC.App.View.SystemAdmin.BackupRestore;
using VTS.QLNS.CTC.App.ViewModel.SystemAdmin.BackupRestore.BackupAndRestore;
using VTS.QLNS.CTC.App.ViewModel.SystemAdmin.BackupRestore.File;
using VTS.QLNS.CTC.App.ViewModel.SystemAdmin.BackupRestore.Schedule;
using VTS.QLNS.CTC.Utility;

namespace VTS.QLNS.CTC.App.ViewModel.SystemAdmin.BackupRestore
{
    public class BackupRestoreViewModel : ViewModelBase
    {
        public override string FuncCode => NSFunctionCode.SYSTEM_BACKUP_RESTORE;
        public override string Name => "SAO LƯU, PHỤC HỒI DỮ LIỆU";
        public override string Description => "Sao lưu, phục hồi dữ liệu";
        public override string Title => "Sao lưu, phục hồi dữ liệu";
        public override Type ContentType => typeof(BackupDataIndex);
        public override PackIconKind IconKind => PackIconKind.BackupRestore;

        public ScheduleIndexViewModel ScheduleIndexViewModel { get; }
        public BackupFileIndexViewModel BackupFileIndexViewModel { get; }
        public BackupAndRestoreViewModel BackupAndRestoreViewModel { get; }

        public BackupRestoreViewModel(ScheduleIndexViewModel scheduleIndexViewModel,
            BackupFileIndexViewModel backupFileIndexViewModel,
            BackupAndRestoreViewModel backupAndRestoreViewModel)
        {
            ScheduleIndexViewModel = scheduleIndexViewModel;
            BackupFileIndexViewModel = backupFileIndexViewModel;
            BackupAndRestoreViewModel = backupAndRestoreViewModel;

            ScheduleIndexViewModel.ParentPage = this;
            BackupFileIndexViewModel.ParentPage = this;
            backupAndRestoreViewModel.ParentPage = this;    
        }

        public override void Init()
        {
            MarginRequirement = new System.Windows.Thickness(0);

            Documentation = new ObservableCollection<ViewModelBase>()
            {
               ScheduleIndexViewModel,
               BackupFileIndexViewModel,
               BackupAndRestoreViewModel
            };
            DocumentationSelectedItem = Documentation.FirstOrDefault(x => x.IsAuthorized); 
        }
    }
}
