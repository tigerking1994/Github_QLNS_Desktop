using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Text;
using VTS.QLNS.CTC.App.View.SystemAdmin.Utilities;
using VTS.QLNS.CTC.Utility;

namespace VTS.QLNS.CTC.App.ViewModel.SystemAdmin.Utilities.Tool
{
    public class ManageDataViewModel : ViewModelBase
    {
        public override string FuncCode => NSFunctionCode.SYSTEM_UTILITIES_MANAGE_DATA;
        public override string Name => "Quản lý dữ liệu";
        public override string Description => "Công cụ quản trị dữ liệu";
        public override string Title => "Công cụ quản trị dữ liệu";
        public override Type ContentType => typeof(ManageData);
        public override PackIconKind IconKind => PackIconKind.DatabaseCogOutline;
    }
}
