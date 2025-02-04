using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using VTS.QLNS.CTC.App.View.SystemAdmin.AppVersion;

namespace VTS.QLNS.CTC.App.ViewModel.SystemAdmin.AppVersion
{
    public class AppVersionManagementViewModel : ViewModelBase
    {
        public override string Name => "CẬP NHẬT PHIÊN BẢN";
        public override string Description => "CẬP NHẬT PHIÊN BẢN";
        public override string Title => "CẬP NHẬT PHIÊN BẢN";
        public override Type ContentType => typeof(AppVersionManagementView);

        public AppVersionIndexViewModel AppVersionIndexViewModel;

        public AppVersionManagementViewModel(AppVersionIndexViewModel appVersionIndexViewModel)
        {
            AppVersionIndexViewModel = appVersionIndexViewModel;
        }

        public override void Init()
        {
            MarginRequirement = new System.Windows.Thickness(0);
            Documentation = new ObservableCollection<ViewModelBase>()
            {
               AppVersionIndexViewModel,
            };
            DocumentationSelectedItem = Documentation.FirstOrDefault(x => x.IsAuthorized);
        }
    }
}
