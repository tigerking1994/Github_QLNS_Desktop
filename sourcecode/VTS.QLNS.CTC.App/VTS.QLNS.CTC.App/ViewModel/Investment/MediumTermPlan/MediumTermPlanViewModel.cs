using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using VTS.QLNS.CTC.App.ViewModel.Investment.MediumTermPlan.PlanManager;
using VTS.QLNS.CTC.App.ViewModel.Investment.MediumTermPlan.PlanSuggestions;
using VTS.QLNS.CTC.App.ViewModel.Investment.MediumTermPlan.ChuTruongDauTu;
using VTS.QLNS.CTC.App.ViewModel.Investment.MediumTermPlan.PheDuyetDuAn;
using VTS.QLNS.CTC.App.ViewModel.Investment.MediumTermPlan.TKTCVaTongDuToan;
using VTS.QLNS.CTC.App.ViewModel.Investment.MediumTermPlan.GoiThau;
using VTS.QLNS.CTC.App.ViewModel.Investment.MediumTermPlan.ContractInfo;
using VTS.QLNS.CTC.App.ViewModel.Investment.MediumTermPlan.QLDuAn;
using VTS.QLNS.CTC.App.ViewModel.Investment.MediumTermPlan.GiaoDuToanChiPhi;
using VTS.QLNS.CTC.App.ViewModel.Investment.MediumTermPlan.KHLuaChonNhaThau;



using VTS.QLNS.CTC.Utility;

namespace VTS.QLNS.CTC.App.ViewModel.Investment.MediumTermPlan
{
    public class MediumTermPlanViewModel : ViewModelBase
    {
        //public override string Name => "KẾ HOẠCH TRUNG HẠN"; --THÔNG TIN DỰ ÁN
        public override string Name => "THÔNG TIN DỰ ÁN"; 
        public override string Description => "Thông tin dự án";
        public override Type ContentType => typeof(View.Investment.MediumTermPlan.MediumTermPlan);
        public override PackIconKind IconKind => PackIconKind.Money;
        public override string FuncCode => NSFunctionCode.INVESTMENT_MEDIUM_TERM_PLAN;

        public PlanManagerApprovedIndexViewModel PlanManagerApprovedIndexViewModel { get; }
        public PlanSuggestionsIndexViewModel PlanSuggestionsIndexViewModel { get; }
        public ChuTruongDauTuIndexViewModel ChuTruongDauTuIndexViewModel_ { get; }
        public PheDuyetDuAnIndexViewModel PheDuyetDuAnIndexViewModel_ { get; }
        public TKTCVaTongDuToanIndexViewModel TKTCVaTongDuToanIndexViewModel_ { get; }
        public KHLuaChonNhaThauIndexViewModel KHLuaChonNhaThauIndexViewModel_ { get; }
        public GoiThauIndexViewModel GoiThauIndexViewModel_ { get; }
        public ContractInfoIndexViewModel ContractInfoIndexViewModel_ { get; }
        public QLDuAnIndexViewModel QLDuAnIndexViewModel_ { get; }
        public GiaoDuToanChiPhiIndexViewModel GiaoDuToanChiPhiIndexViewModel_ { get; }





        public MediumTermPlanViewModel(PlanManagerApprovedIndexViewModel planManagerApprovedIndexViewModel,
            PlanSuggestionsIndexViewModel planSuggestionsIndexViewModel,
            ChuTruongDauTuIndexViewModel chuTruongDauTuIndexViewModel,
            PheDuyetDuAnIndexViewModel pheDuyetDuAnIndexViewModel,
            TKTCVaTongDuToanIndexViewModel tktcVaTongDuToanIndexViewModel,
            KHLuaChonNhaThauIndexViewModel khLuaChonNhaThauIndexViewModel,
            GoiThauIndexViewModel goiThauIndexViewModel,
            ContractInfoIndexViewModel contractInfoIndexViewModel,
            QLDuAnIndexViewModel qlDuAnIndexViewModel,
            GiaoDuToanChiPhiIndexViewModel giaoDuToanChiPhiIndexViewModel




            )
        {
            PlanSuggestionsIndexViewModel = planSuggestionsIndexViewModel;
            PlanManagerApprovedIndexViewModel = planManagerApprovedIndexViewModel;
            ChuTruongDauTuIndexViewModel_ = chuTruongDauTuIndexViewModel;
            PheDuyetDuAnIndexViewModel_ = pheDuyetDuAnIndexViewModel;
            TKTCVaTongDuToanIndexViewModel_ = tktcVaTongDuToanIndexViewModel;
            KHLuaChonNhaThauIndexViewModel_ = khLuaChonNhaThauIndexViewModel;
            GoiThauIndexViewModel_ = goiThauIndexViewModel;
            ContractInfoIndexViewModel_ = contractInfoIndexViewModel;
            QLDuAnIndexViewModel_ = qlDuAnIndexViewModel;
            GiaoDuToanChiPhiIndexViewModel_ = giaoDuToanChiPhiIndexViewModel;



        }

        public override void Init()
        {
            MarginRequirement = new System.Windows.Thickness();
            Documentation = new ObservableCollection<ViewModelBase>() {
                  PlanSuggestionsIndexViewModel,
                  PlanManagerApprovedIndexViewModel,
                  ChuTruongDauTuIndexViewModel_,
                  PheDuyetDuAnIndexViewModel_,
                  TKTCVaTongDuToanIndexViewModel_,
                  KHLuaChonNhaThauIndexViewModel_,
                  GoiThauIndexViewModel_,
                  ContractInfoIndexViewModel_,
                  QLDuAnIndexViewModel_,
                  GiaoDuToanChiPhiIndexViewModel_
            };
            DocumentationSelectedItem = Documentation.FirstOrDefault(x => x.IsAuthorized);
        }
    }
}
