using MaterialDesignThemes.Wpf;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using VTS.QLNS.CTC.App.Command;
using VTS.QLNS.CTC.App.Model;
using System.ComponentModel;
using VTS.QLNS.CTC.App.ViewModel.Budget.Allocation.Report;
using VTS.QLNS.CTC.Utility;
using VTS.QLNS.CTC.App.View.Budget.Allocation.Report;

namespace VTS.QLNS.CTC.App.ViewModel.Budget.Allocation
{
    public class AllocationReportViewModel : ViewModelBase
    {
        public override string GroupName => MenuItemContants.GROUP_REPORT;
        public override string Name => "TH Số chỉ tiêu - Cấp phát";
        public override Type ContentType => typeof(View.Budget.Allocation.Report.AllocationReport);
        public override PackIconKind IconKind => PackIconKind.FileDocument;
        public delegate void DataChangedEventHandler(object sender, EventArgs e);
        public event DataChangedEventHandler ClosePopup;

        public AllocationReportCompareViewModel AllocationReportCompareViewModel { get; }

        public AllocationReportViewModel(AllocationReportCompareViewModel allocationReportCompareViewModel)
        {
            AllocationReportCompareViewModel = allocationReportCompareViewModel;
        }

        public override void Init()
        {
            var item = DocumentationSelectedItem;
            AllocationReportCompareViewModel.Init();
            AllocationReportCompare view = new AllocationReportCompare()
            {
                DataContext = AllocationReportCompareViewModel
            };
            var result = DialogHost.Show(view, "RootDialog", null, RefeshAfterClose);
        }

        private void RefeshAfterClose(object sender, DialogClosingEventArgs eventArgs)
        {
            DataChangedEventHandler handler = ClosePopup;
            if (handler != null)
            {
                handler(this, new EventArgs());
            }
        }
    }
}
