using MaterialDesignThemes.Wpf;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using VTS.QLNS.CTC.App.ViewModel.SystemAdmin.SysLog;
using VTS.QLNS.CTC.App.ViewModel.SystemAdmin.UserManagement;
using VTS.QLNS.CTC.App.ViewModel.SystemAdmin.Utilities;
using VTS.QLNS.CTC.App.ViewModel.SystemAdmin.BackupRestore;
using VTS.QLNS.CTC.Utility;
using VTS.QLNS.CTC.App.ViewModel.SystemAdmin.AppVersion;

namespace VTS.QLNS.CTC.App.ViewModel.SystemAdmin
{
    public class SystemAdminViewModel : ViewModelBase
    {
        public override string FuncCode => NSFunctionCode.SYSTEM;
        public override string Name => "HỆ THỐNG";
        public override Type ContentType => typeof(View.SystemAdmin.SystemAdmin);
        public override PackIconKind IconKind => PackIconKind.Cogs;
        
        public UserManagementViewModel UserManagementViewModel { get; }
        public SysLogManagementViewModel SysLogManagementViewModel { get; }
        public BackupRestoreViewModel BackupDataViewModel { get; }
        public UtilitiesViewModel UtilitiesViewModel { get; }
        public AppVersionManagementViewModel AppVersionManagementViewModel { get; }

        public SystemAdminViewModel(UserManagementViewModel userManagementViewModel,
            SysLogManagementViewModel sysLogManagementViewModel,
            BackupRestoreViewModel backupDataViewModel,
            UtilitiesViewModel utilitiesViewModel,
            AppVersionManagementViewModel appVersionManagementViewModel)
        {
            UserManagementViewModel = userManagementViewModel;
            SysLogManagementViewModel = sysLogManagementViewModel;
            BackupDataViewModel = backupDataViewModel;
            UtilitiesViewModel = utilitiesViewModel;
            AppVersionManagementViewModel = appVersionManagementViewModel;

            UserManagementViewModel.ParentPage = this;
            SysLogManagementViewModel.ParentPage = this;
            BackupDataViewModel.ParentPage = this;
            UtilitiesViewModel.ParentPage = this;
        }

        public override void Init()
        {
            MarginRequirement = new System.Windows.Thickness(0);

            Documentation = new ObservableCollection<ViewModelBase>()
            {
                UserManagementViewModel,
                SysLogManagementViewModel,
                BackupDataViewModel,
                UtilitiesViewModel,
                AppVersionManagementViewModel,
            };
            DocumentationSelectedItem = Documentation.FirstOrDefault(x => x.IsAuthorized);
        }
    }
}
