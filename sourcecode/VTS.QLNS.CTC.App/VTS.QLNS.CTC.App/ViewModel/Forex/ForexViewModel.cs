using MaterialDesignThemes.Wpf;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using VTS.QLNS.CTC.App.ViewModel.Category;
using VTS.QLNS.CTC.App.ViewModel.Forex.ForexAllocation;
using VTS.QLNS.CTC.App.ViewModel.Forex.ForexPlan;
using VTS.QLNS.CTC.App.ViewModel.Forex.ForexSettlement;
using VTS.QLNS.CTC.Utility;
using VTS.QLNS.CTC.App.ViewModel.Forex.ForexKhoiTaoCapPhat;
using VTS.QLNS.CTC.App.ViewModel.Forex.ForexMuaSam;
using VTS.QLNS.CTC.App.ViewModel.Forex.ForexDuAn;

namespace VTS.QLNS.CTC.App.ViewModel.Forex
{
    public class ForexViewModel : ViewModelBase
    {
        public override string Name => "QUẢN LÝ NGOẠI HỐI";
        public override Type ContentType => typeof(View.Forex.Forex);
        public override PackIconKind IconKind => PackIconKind.BagChecked;
        public override string FuncCode => NSFunctionCode.FOREX;
        public ForexPlanViewModel ForexPlanViewModel { get; }
        public ForexAllocationViewModel ForexAllocationViewModel { get; }
        public ForexSettlementViewModel ForexSettlementViewModel { get; }
        public ForexMuaSamViewModel ForexMuaSamViewModel { get; }
        public ForexDuAnViewModel ForexDuAnViewModel { get; }
        public CategoryForexViewModel CategoryForexViewModel { get; }
        public ForexKhoiTaoCapPhatViewModel ForexKhoiTaoCapPhatViewModel { get; }

        public ForexViewModel(
            ForexPlanViewModel forexPlanViewModel,
            ForexAllocationViewModel forexAllocationViewModel,
            ForexSettlementViewModel forexSettlementViewModel,
            CategoryForexViewModel categoryForexViewModel,
            ForexMuaSamViewModel forexMuaSamViewModel,
            ForexDuAnViewModel forexDuAnViewModel,
            ForexKhoiTaoCapPhatViewModel forexKhoiTaoCapPhatViewModel
            )
        {
            ForexPlanViewModel = forexPlanViewModel;
            ForexAllocationViewModel = forexAllocationViewModel;
            ForexSettlementViewModel = forexSettlementViewModel;
            CategoryForexViewModel = categoryForexViewModel;
            ForexKhoiTaoCapPhatViewModel = forexKhoiTaoCapPhatViewModel;
            ForexMuaSamViewModel = forexMuaSamViewModel;
            ForexDuAnViewModel = forexDuAnViewModel;

            ForexPlanViewModel.ParentPage = this;
            ForexAllocationViewModel.ParentPage = this;
            ForexSettlementViewModel.ParentPage = this;
            CategoryForexViewModel.ParentPage = this;
            ForexKhoiTaoCapPhatViewModel.ParentPage = this;
            ForexMuaSamViewModel.ParentPage = this;
            ForexDuAnViewModel.ParentPage = this;
        }

        public override void Init()
        {
            MarginRequirement = new System.Windows.Thickness(0);
            Documentation = new ObservableCollection<ViewModelBase>() {
                ForexPlanViewModel, // Kế hoạch
                //ForexPrepareToInvestViewModel, // Chuẩn bị đầu tư
                //ForexForeignTradeViewModel, // Ngoại thương
                ForexMuaSamViewModel, // Mua sắm
                //DomesticViewModel, // Trong nước
                ForexDuAnViewModel, // Dự án
                ForexAllocationViewModel, // Cấp phát
                ForexSettlementViewModel, // Quyết toán
                CategoryForexViewModel, // Danh mục
                ForexKhoiTaoCapPhatViewModel //Khởi tạo cấp phát
            };
            DocumentationSelectedItem = Documentation.First();
        }
    }
}
