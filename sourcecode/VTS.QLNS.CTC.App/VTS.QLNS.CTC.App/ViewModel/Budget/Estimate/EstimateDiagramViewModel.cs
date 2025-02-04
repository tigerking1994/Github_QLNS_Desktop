using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VTS.QLNS.CTC.App.Command;
using VTS.QLNS.CTC.App.Service;
using VTS.QLNS.CTC.Utility;

namespace VTS.QLNS.CTC.App.ViewModel.Budget.Estimate
{
    public class EstimateDiagramViewModel : ViewModelBase
    {
        private ISessionService _sessionService;

        public override string GroupName => MenuItemContants.GROUP_FUNCTION;
        public override string Name => "Sơ đồ chức năng";
        public override Type ContentType => typeof(View.Budget.Estimate.EstimateDiagram);
        public override PackIconKind IconKind => PackIconKind.Sitemap;

        public RelayCommand ShowDivisionIndexCommand { get; }
        public RelayCommand ShowDivisionEstimateIndexCommand { get; }

        public string TieuDe
        {
            get => "CHỈ TIÊU NGÂN SÁCH NĂM " + _sessionService.Current.YearOfWork.ToString();
        }

        public EstimateDiagramViewModel(ISessionService sessionService)
        {
            _sessionService = sessionService;
            ShowDivisionIndexCommand = new RelayCommand(obj =>
            {
                var settlement = (EstimateViewModel)this.ParentPage;
                settlement.DocumentationSelectedItem = settlement.Documentation.ElementAtOrDefault(1);
            });
            ShowDivisionEstimateIndexCommand = new RelayCommand(obj =>
            {
                var settlement = (EstimateViewModel)this.ParentPage;
                settlement.DocumentationSelectedItem = settlement.Documentation.ElementAtOrDefault(2);
            });
        }
    }
}
