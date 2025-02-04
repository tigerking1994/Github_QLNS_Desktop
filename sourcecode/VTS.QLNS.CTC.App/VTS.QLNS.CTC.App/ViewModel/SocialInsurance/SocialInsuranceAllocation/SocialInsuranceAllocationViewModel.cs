using MaterialDesignThemes.Wpf;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using VTS.QLNS.CTC.App.ViewModel.SocialInsurance.SocialInsuranceAllocation.CapPhatBoSung;
using VTS.QLNS.CTC.App.ViewModel.SocialInsurance.SocialInsuranceAllocation.ChungTuCapPhat;
using VTS.QLNS.CTC.App.ViewModel.SocialInsurance.SocialInsuranceAllocation.Report;
using VTS.QLNS.CTC.App.ViewModel.SocialInsurance.SocialInsuranceSettlement.QuyetToanCapKinhPhiKCBBHYT;

namespace VTS.QLNS.CTC.App.ViewModel.SocialInsurance.SocialInsuranceAllocation
{
    public class SocialInsuranceAllocationViewModel : ViewModelBase
    {
        public override string Name => "CẤP PHÁT";
        public override string Description => "Cấp phát";
        public override PackIconKind IconKind => PackIconKind.LayersTriple;
        public override Type ContentType => typeof(View.SocialInsurance.SocialInsuranceAllocation.SocialInsuranceAllocation);
        public CapPhatBoSungIndexViewModel CapPhatBoSungIndexViewModel { get; }
        public QuyetToanCapKinhPhiKcbIndexViewModel QuyetToanCapKinhPhiKcbIndexViewModel { get; }

        public CapPhatTamUngKCBBHYTIndexViewModel CapPhatTamUngKCBBHYTIndexViewModel { get; set; }
        public ChungTuCapPhatIndexViewModel ChungTuCapPhatIndexViewModel { get; set; }
        public SocialInsuranceAllocationReportIndexViewModel SocialInsuranceAllocationReportIndexViewModel { get; set; }
        public SocialInsuranceAllocationViewModel(
            ChungTuCapPhatIndexViewModel chungTuCapPhatIndexViewModel,
            CapPhatBoSungIndexViewModel capPhatBoSungIndexViewModel,
            QuyetToanCapKinhPhiKcbIndexViewModel quyetToanCapKinhPhiKcbIndexViewModel,
            CapPhatTamUngKCBBHYTIndexViewModel capPhatTamUngKCBBHYTIndexViewModel,
            SocialInsuranceAllocationReportIndexViewModel socialInsuranceAllocationReportIndexViewModel)
        {
            ChungTuCapPhatIndexViewModel = chungTuCapPhatIndexViewModel;
            CapPhatBoSungIndexViewModel = capPhatBoSungIndexViewModel;
            QuyetToanCapKinhPhiKcbIndexViewModel = quyetToanCapKinhPhiKcbIndexViewModel;
            CapPhatTamUngKCBBHYTIndexViewModel = capPhatTamUngKCBBHYTIndexViewModel;
            SocialInsuranceAllocationReportIndexViewModel = socialInsuranceAllocationReportIndexViewModel;

            ChungTuCapPhatIndexViewModel.ParentPage = this;
            CapPhatTamUngKCBBHYTIndexViewModel.ParentPage = this;
            CapPhatBoSungIndexViewModel.ParentPage = this;
            QuyetToanCapKinhPhiKcbIndexViewModel.ParentPage = this;
            SocialInsuranceAllocationReportIndexViewModel.ParentPage = this;
        }

        public override void Init()
        {
            MarginRequirement = new System.Windows.Thickness();
            Documentation = new ObservableCollection<ViewModelBase>()
            {
                ChungTuCapPhatIndexViewModel,
                CapPhatTamUngKCBBHYTIndexViewModel,
                CapPhatBoSungIndexViewModel,
                QuyetToanCapKinhPhiKcbIndexViewModel,
                SocialInsuranceAllocationReportIndexViewModel
            };
            DocumentationSelectedItem = Documentation.FirstOrDefault(x => x.IsAuthorized);
        }
    }
}
