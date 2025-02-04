using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VTS.QLNS.CTC.App.Command;
using VTS.QLNS.CTC.Utility;

namespace VTS.QLNS.CTC.App.ViewModel.Budget.Settlement.Diagram
{
    public class SettlementDiagramViewModel : ViewModelBase
    {
        public override string GroupName => MenuItemContants.GROUP_FUNCTION;
        public override string Name => "Sơ đồ chức năng";
        public override Type ContentType => typeof(View.Budget.Settlement.Diagram.SettlementDiagram);
        public override PackIconKind IconKind => PackIconKind.Sitemap;

        public RelayCommand CancelCommand { get; }
        public RelayCommand RegularBudgetCommand { get; }
        public RelayCommand DefenseBudgetCommand { get; }
        public RelayCommand StateBudgetCommand { get; }

        public SettlementDiagramViewModel()
        {
            CancelCommand = new RelayCommand(obj => {
                this.ParentPage.ParentPage.CurrentPage = null;
            });
            RegularBudgetCommand = new RelayCommand(obj =>
            {
                var settlement = (SettlementViewModel)this.ParentPage;
                settlement.DocumentationSelectedItem = settlement.Documentation.ElementAtOrDefault(1);
            });
            DefenseBudgetCommand = new RelayCommand(obj =>
            {
                var settlement = (SettlementViewModel)this.ParentPage;
                settlement.DocumentationSelectedItem = settlement.Documentation.ElementAtOrDefault(2);
            });
            StateBudgetCommand = new RelayCommand(obj =>
            {
                var settlement = (SettlementViewModel)this.ParentPage;
                settlement.DocumentationSelectedItem = settlement.Documentation.ElementAtOrDefault(3);
            });
        }
    }
}
