using MaterialDesignThemes.Wpf;
using System;
using System.Linq;
using VTS.QLNS.CTC.App.Command;
using VTS.QLNS.CTC.App.View.Budget.DemandCheck.FunctionMap;
using VTS.QLNS.CTC.App.View.Budget.DemandCheck.FunctionMap.PrintReport;
using VTS.QLNS.CTC.App.ViewModel.Budget.DemandCheck.PrintReport;
using VTS.QLNS.CTC.Utility.Enum;

namespace VTS.QLNS.CTC.App.ViewModel.Budget.DemandCheck
{
    public class DemandCheckFunctionMapViewModel : ViewModelBase
    {
        public override PackIconKind IconKind => PackIconKind.Sitemap;
        public override string Name => "Sơ đồ chức năng";
        public override Type ContentType => typeof(DemandCheckFunctionMap);
        
        public RelayCommand CancelCommand { get; }
        public RelayCommand CheckCommand { get; }
        public RelayCommand DemandCommand { get; }
        public RelayCommand AllocationCommand { get; }
        public RelayCommand PrintCommand { get; }
        public RelayCommand PrintActionCommand { get; }

        public PrintReportDemandOrgViewModel PrintReportDemandOrgViewModel { get;}

        private bool _isOpenPrintPopup;
        public bool IsOpenPrintPopup
        {
            get => _isOpenPrintPopup;
            set => SetProperty(ref _isOpenPrintPopup, value);
        }

        public DemandCheckFunctionMapViewModel(PrintReportDemandOrgViewModel printReportDemandOrgViewModel)
        {
            PrintReportDemandOrgViewModel = printReportDemandOrgViewModel;

            PrintCommand = new RelayCommand(obj => IsOpenPrintPopup = true);
            PrintActionCommand = new RelayCommand(obj => OpenPrintDialog(obj));

            CancelCommand = new RelayCommand(obj =>
            {
                this.ParentPage.ParentPage.CurrentPage = null;
            });
            DemandCommand = new RelayCommand(obj =>
            {
                var demandCheck = (DemandCheckViewModel)this.ParentPage;
                demandCheck.DocumentationSelectedItem = demandCheck.Documentation.ElementAtOrDefault(1);
            });
            CheckCommand = new RelayCommand(obj =>
            {
                var demandCheck = (DemandCheckViewModel)this.ParentPage;
                demandCheck.DocumentationSelectedItem = demandCheck.Documentation.ElementAtOrDefault(2);
            });
            AllocationCommand = new RelayCommand(obj =>
            {
                var demandCheck = (DemandCheckViewModel)this.ParentPage;
                demandCheck.DocumentationSelectedItem = demandCheck.Documentation.ElementAtOrDefault(3);
            });
        }


        /// <summary>
        /// open screen print
        /// </summary>
        /// <param name="param"></param>
        public void OpenPrintDialog(object param)
        {
            var demandCheckPrintType = (DemandCheckPrintType)((int)param);
            object content;
            switch (demandCheckPrintType)
            {
                case DemandCheckPrintType.REPORT_ORG_DEMAND_DETAIL_NUMBER:
                case DemandCheckPrintType.REPORT_SYNTHESIS_ORG_DEMAND_DETAIL_NUMBER:
                    PrintReportDemandOrgViewModel.DemandCheckPrintType = demandCheckPrintType;
                    PrintReportDemandOrgViewModel.Init();
                    content = new PrintReportDemandOrg
                    {
                        DataContext = PrintReportDemandOrgViewModel
                    };
                    break;
                default:
                    content = null;
                    break;
            }

            if (content != null)
            {
                DialogHost.Show(content, DivisionScreen.ROOT_DIALOG, null, null);
            }
        }

    }
}