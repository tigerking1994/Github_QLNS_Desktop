using AutoMapper;
using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VTS.QLNS.CTC.Utility;
using System.Collections.ObjectModel;
using VTS.QLNS.CTC.App.ViewModel.Salary.SalaryUtilities.CleanupSalaryData;

namespace VTS.QLNS.CTC.App.ViewModel.Salary.SalaryUtilities
{
    public class SalaryUtilitiesViewModel : ViewModelBase
    {
        public override string FuncCode => NSFunctionCode.SALARY_CHUYEN_DOI_DU_LIEU;
        public override string Name => "TIỆN ÍCH";
        public override string Description => "Tiện ích";
        public override Type ContentType => typeof(View.Salary.SalaryUtilities.SalaryUtilities);
        public override PackIconKind IconKind => PackIconKind.PuzzleOutline;

        public CleanupSalaryDataViewModel CleanupSalaryDataViewModel { get; }

        public SalaryUtilitiesViewModel(
            CleanupSalaryDataViewModel cleanupSalaryDataViewModel)
        {
            CleanupSalaryDataViewModel = cleanupSalaryDataViewModel;
        }

        public override void Init()
        {
            MarginRequirement = new System.Windows.Thickness(0);
            InitDocument();
        }

        public void InitDocument()
        {
            Documentation = new ObservableCollection<ViewModelBase>()
            {
                CleanupSalaryDataViewModel
            };
            DocumentationSelectedItem = Documentation.FirstOrDefault(x => x.IsAuthorized);
        }
    }
}
