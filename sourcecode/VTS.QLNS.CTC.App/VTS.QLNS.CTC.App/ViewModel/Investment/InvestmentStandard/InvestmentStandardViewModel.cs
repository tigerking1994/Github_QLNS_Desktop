using MaterialDesignThemes.Wpf;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using VTS.QLNS.CTC.App.ViewModel.Investment.InvestmentStandard.YearPlan;
//using VTS.QLNS.CTC.App.ViewModel.Investment.MediumTermPlan.ChuTruongDauTu;
using VTS.QLNS.CTC.App.ViewModel.Investment.InvestmentStandard.VonNamDonVi;
using VTS.QLNS.CTC.App.ViewModel.Investment.InvestmentStandard.AdvanceCapitalApproved;
using VTS.QLNS.CTC.App.ViewModel.Investment.InvestmentStandard.KeHoachVonNamDuocDuyet;
//using VTS.QLNS.CTC.App.ViewModel.Investment.MediumTermPlan.GiaoDuToanChiPhi;
using VTS.QLNS.CTC.Utility;
using VTS.QLNS.CTC.App.ViewModel.Investment.InvestmentStandard.KeHoachChiQuy;
using VTS.QLNS.CTC.App.ViewModel.Investment.InvestmentStandard.KeHoachVonUngDeXuat;

namespace VTS.QLNS.CTC.App.ViewModel.Investment.InvestmentStandard
{
    public class InvestmentStandardViewModel : ViewModelBase
    {
        public override string FuncCode => NSFunctionCode.INVESTMENT_STANDARD;
        //public override string Name => "CHUẨN BỊ ĐẦU TƯ";
        public override string Name => "KẾ HOẠCH VỐN";
        public override string Description => "Kế hoạch vốn";
        public override Type ContentType => typeof(View.Investment.InvestmentStandard.InvestmentStandard);
        public override PackIconKind IconKind => PackIconKind.Money;

       // public ChuTruongDauTuIndexViewModel ChuTruongDauTuIndexViewModel { get; }
        public YearPlanUnitIndexViewModel VonNamDonViIndexViewModel { get; }
        public KHVNamDuocDuyetIndexViewModel KhvNamDuocDuyetIndexViewModel { get; }
        public YearPlanIndexViewModel YearPlanIndexViewModel { get; }

        public KeHoachChiQuyIndexViewModel KeHoachChiQuyIndexViewModel_ { get; }

        public AdvanceCapitalApprovedIndexViewModel AdvanceCapitalApprovedIndexViewModel { get; }
        public KeHoachVonUngDeXuatIndexViewModel KeHoachVonUngDeXuatIndexViewModel { get; }
        //public GiaoDuToanChiPhiIndexViewModel GiaoDuToanChiPhiIndexViewModel { get; }

        public InvestmentStandardViewModel(
            YearPlanUnitIndexViewModel vonNamDonViIndexViewModel,
            KHVNamDuocDuyetIndexViewModel khvNamDuocDuyetIndexViewModel,
            YearPlanIndexViewModel yearPlanIndexViewModel,
            AdvanceCapitalApprovedIndexViewModel advanceCapitalApprovedIndexViewModel,
            KeHoachChiQuyIndexViewModel khHoachChiQuyIndexViewModel_,
            KeHoachVonUngDeXuatIndexViewModel keHoachVonUngDeXuatIndexViewModel
            //ChuTruongDauTuIndexViewModel chuTruongDauTuIndexViewModel,
            //GiaoDuToanChiPhiIndexViewModel giaoDuToanChiPhiIndexViewModel
            )
        {
            //ChuTruongDauTuIndexViewModel = chuTruongDauTuIndexViewModel;
            VonNamDonViIndexViewModel = vonNamDonViIndexViewModel;
            KhvNamDuocDuyetIndexViewModel = khvNamDuocDuyetIndexViewModel;
            YearPlanIndexViewModel = yearPlanIndexViewModel;
            KeHoachChiQuyIndexViewModel_ = khHoachChiQuyIndexViewModel_;
            AdvanceCapitalApprovedIndexViewModel = advanceCapitalApprovedIndexViewModel;
            KeHoachVonUngDeXuatIndexViewModel = keHoachVonUngDeXuatIndexViewModel;
            //GiaoDuToanChiPhiIndexViewModel = giaoDuToanChiPhiIndexViewModel;

            //ChuTruongDauTuIndexViewModel.ParentPage = this;
            YearPlanIndexViewModel.ParentPage = this;
            KhvNamDuocDuyetIndexViewModel.ParentPage = this;
            VonNamDonViIndexViewModel.ParentPage = this;
            AdvanceCapitalApprovedIndexViewModel.ParentPage = this;
            KeHoachVonUngDeXuatIndexViewModel.ParentPage = this;
            //GiaoDuToanChiPhiIndexViewModel.ParentPage = this;
        }

        public override void Init()
        {
            MarginRequirement = new System.Windows.Thickness();
            Documentation = new ObservableCollection<ViewModelBase>() {
                //ChuTruongDauTuIndexViewModel,
                VonNamDonViIndexViewModel,
                KhvNamDuocDuyetIndexViewModel,
                YearPlanIndexViewModel,
                KeHoachChiQuyIndexViewModel_,
                KeHoachVonUngDeXuatIndexViewModel,
                AdvanceCapitalApprovedIndexViewModel
                //GiaoDuToanChiPhiIndexViewModel
            };
            DocumentationSelectedItem = Documentation.FirstOrDefault(x => x.IsAuthorized);
        }

        public override void Dispose()
        {
            base.Dispose();
        }
    }
}
