using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using VTS.QLNS.CTC.App.ViewModel.Investment.Initialization.InitializationProcess;
using VTS.QLNS.CTC.App.ViewModel.Investment.Initialization.InitializationProject;
//using VTS.QLNS.CTC.App.ViewModel.Investment.MediumTermPlan.TKTCVaTongDuToan;
using VTS.QLNS.CTC.Utility;

namespace VTS.QLNS.CTC.App.ViewModel.Investment.Initialization
{
    public class InitializationViewModel : ViewModelBase
    {
        public override string FuncCode => NSFunctionCode.INVESTMENT_INITIALIZATION;
        public override string Name => "KHỞI TẠO THÔNG TIN DỰ ÁN";
        public override string Description => "Khởi tạo thông tin dự án";
        public override Type ContentType => typeof(View.Investment.Initialization.Initialization);
        public override PackIconKind IconKind => PackIconKind.Projector;

        public InitializationProjectIndexViewModel InitializationProjectViewModel { get; }
        public InitializationProcessIndexViewModel InitializationProcessIndexViewModel { get; }

        public InitializationViewModel(InitializationProjectIndexViewModel initializationProjectViewModel, InitializationProcessIndexViewModel initializationProcessIndexViewModel)
        {
            InitializationProjectViewModel = initializationProjectViewModel;
            InitializationProcessIndexViewModel = initializationProcessIndexViewModel;
            InitializationProjectViewModel.ParentPage = this;
            InitializationProcessIndexViewModel.ParentPage = this;
        }

        public override void Init()
        {
            MarginRequirement = new System.Windows.Thickness();
            Documentation = new ObservableCollection<ViewModelBase>()
            {
               InitializationProcessIndexViewModel
            };
            DocumentationSelectedItem = InitializationProcessIndexViewModel;
        }
    }
}
