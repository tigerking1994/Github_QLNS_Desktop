using MaterialDesignThemes.Wpf;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using VTS.QLNS.CTC.App.ViewModel.Budget.MigrationData;
using VTS.QLNS.CTC.App.ViewModel.SystemAdmin.Utilities.Tool;
using VTS.QLNS.CTC.Utility;

namespace VTS.QLNS.CTC.App.ViewModel.SystemAdmin.Utilities
{
    public class UtilitiesViewModel : ViewModelBase
    {
        public override string FuncCode => NSFunctionCode.SYSTEM_UTILITIES;
        public override string Name => "TIỆN ÍCH";
        public override Type ContentType => typeof(View.SystemAdmin.Utilities.Utilities);
        public override PackIconKind IconKind => PackIconKind.PuzzleOutline;

        public ManageDataViewModel ManageDataViewModel { get; }
        public QueryDataViewModel QueryDataViewModel { get; }
        public GetLogFileViewModel GetLogFileViewModel { get; }
        public ClearDataRedundancyViewModel ClearDataRedundancyViewModel { get; }
        public MigrationDataIndexViewModel MigrationDataIndexViewModel { get; }
        public MigrationDataOldViewModel MigrationDataOldViewModel { get; }
        public ResetSystemViewModel ResetSystemViewModel { get; }

        public UtilitiesViewModel(ManageDataViewModel manageDataViewModel, QueryDataViewModel queryDataViewModel, GetLogFileViewModel getLogFileViewModel,
            MigrationDataIndexViewModel migrationDataIndexViewModel, ClearDataRedundancyViewModel clearDataRedundancyViewModel,
            MigrationDataOldViewModel migrationDataOldViewModel,
            ResetSystemViewModel resetSystemViewModel)
        {
            ManageDataViewModel = manageDataViewModel;
            QueryDataViewModel = queryDataViewModel;
            GetLogFileViewModel = getLogFileViewModel;
            MigrationDataIndexViewModel = migrationDataIndexViewModel;
            MigrationDataOldViewModel = migrationDataOldViewModel;
            ClearDataRedundancyViewModel = clearDataRedundancyViewModel;
            ResetSystemViewModel = resetSystemViewModel;

            ManageDataViewModel.ParentPage = this;
            QueryDataViewModel.ParentPage = this;
            GetLogFileViewModel.ParentPage = this;
            MigrationDataIndexViewModel.ParentPage = this;
            MigrationDataOldViewModel.ParentPage = this;
            ClearDataRedundancyViewModel.ParentPage = this;
            ResetSystemViewModel.ParentPage = this;
        }

        public override void Init()
        {
            base.Init();
            MarginRequirement = new System.Windows.Thickness(0);

            Documentation = new ObservableCollection<ViewModelBase>()
            {
                QueryDataViewModel,
                ManageDataViewModel,
                GetLogFileViewModel,
                MigrationDataIndexViewModel,
                MigrationDataOldViewModel,
                ClearDataRedundancyViewModel,
                ResetSystemViewModel
            };
            DocumentationSelectedItem = Documentation.FirstOrDefault(x => x.IsAuthorized);
        }
    }
}
