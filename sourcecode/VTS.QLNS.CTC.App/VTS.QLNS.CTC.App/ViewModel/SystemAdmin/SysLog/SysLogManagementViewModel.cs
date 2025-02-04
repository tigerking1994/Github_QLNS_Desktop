using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using VTS.QLNS.CTC.Utility;

namespace VTS.QLNS.CTC.App.ViewModel.SystemAdmin.SysLog
{
    public class SysLogManagementViewModel : ViewModelBase
    {
        public override string FuncCode => NSFunctionCode.SYSTEM_SYSLOG;
        public override string Name => "NHẬT KÝ DỮ LIỆU";
        public override string Description => "NHẬT KÝ DỮ LIỆU";
        public override string Title => "NHẬT KÝ DỮ LIỆU";
        public override Type ContentType => typeof(View.SystemAdmin.SysLog.SysLogManagement);
        public override PackIconKind IconKind => PackIconKind.Account;

        public SysLogIndexViewModel SysLogIndexViewModel { get; set; }
        public SysLogManagementViewModel(SysLogIndexViewModel sysLogIndexViewModel)
        {
            SysLogIndexViewModel = sysLogIndexViewModel;
        }

        public override void Init()
        {
            MarginRequirement = new System.Windows.Thickness(0);

            Documentation = new ObservableCollection<ViewModelBase>()
            {
               SysLogIndexViewModel
            };
            DocumentationSelectedItem = Documentation.FirstOrDefault(x => x.IsAuthorized);
        }
    }
}
