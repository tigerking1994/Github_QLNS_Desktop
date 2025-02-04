using MaterialDesignThemes.Wpf;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using VTS.QLNS.CTC.Utility;

namespace VTS.QLNS.CTC.App.ViewModel.Category
{
    public class CategoryViewModel : ViewModelBase
    {

        public override string FuncCode => NSFunctionCode.CATEGORY;
        public override string Name => "DANH MỤC";
        public override Type ContentType => typeof(View.Category.Category);
        public override PackIconKind IconKind => PackIconKind.ViewList;

        public CategoryGeneralViewModel CategoryGeneralViewModel { get; }
        public CategoryBudgetViewModel CategoryBudgetViewModel { get; }
        //public CategoryBudgetSourceViewModel CategoryBudgetSourceViewModel { get; }
        public CategoryForexViewModel CategoryForexViewModel { get; }
        public CategoryInvestmentViewModel CategoryInvestmentViewModel { get; }

        public CategorySalaryViewModel CategorySalaryViewModel { get; }
        public CategorySocialInsuranceViewModel CategorySocialInsuranceViewModel { get; }

        public CategoryPublicFinanceViewModel CategoryPublicFinanceViewModel { get; }

        public CategoryViewModel(CategoryGeneralViewModel categoryGeneralViewModel,
            CategoryBudgetViewModel categoryBudgetViewModel,
            //CategoryBudgetSourceViewModel categoryBudgetSourceViewModel,
            CategoryForexViewModel categoryForexViewModel,
            CategoryInvestmentViewModel categoryInvestmentViewModel,
            CategorySalaryViewModel categorySalaryViewModel,
            CategorySocialInsuranceViewModel categorySocialInsuranceViewModel,
            CategoryPublicFinanceViewModel categoryPublicFinanceViewModel
            )
        {
            CategoryGeneralViewModel = categoryGeneralViewModel;
            CategoryBudgetViewModel = categoryBudgetViewModel;
            //CategoryBudgetSourceViewModel = categoryBudgetSourceViewModel;
            CategoryForexViewModel = categoryForexViewModel;
            CategoryInvestmentViewModel = categoryInvestmentViewModel;
            CategorySalaryViewModel = categorySalaryViewModel;
            CategorySocialInsuranceViewModel = categorySocialInsuranceViewModel;
            CategoryPublicFinanceViewModel = categoryPublicFinanceViewModel;

            CategoryGeneralViewModel.ParentPage = this;
            CategoryBudgetViewModel.ParentPage = this;
            //CategoryBudgetSourceViewModel.ParentPage = this;
            CategoryForexViewModel.ParentPage = this;
            CategoryInvestmentViewModel.ParentPage = this;
            CategorySalaryViewModel.ParentPage = this;
            CategorySocialInsuranceViewModel.ParentPage = this;
            CategoryPublicFinanceViewModel.ParentPage = this;
        }

        public override void Init()
        {
            base.Init();
            MarginRequirement = new System.Windows.Thickness(0);

            Documentation = new ObservableCollection<ViewModelBase>()
            {
                CategoryGeneralViewModel,
                CategoryBudgetViewModel,
                //CategoryBudgetSourceViewModel,
                CategoryForexViewModel,
                CategoryInvestmentViewModel,
                CategorySalaryViewModel,
                CategorySocialInsuranceViewModel,
                //CategoryPublicFinanceViewModel
            };
            DocumentationSelectedItem = Documentation.FirstOrDefault(x => x.IsAuthorized);
        }
    }
}
