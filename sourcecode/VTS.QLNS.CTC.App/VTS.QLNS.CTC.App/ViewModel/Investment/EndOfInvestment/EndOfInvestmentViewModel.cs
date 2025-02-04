using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using VTS.QLNS.CTC.App.ViewModel.Investment.EndOfInvestment.AnnualSettlement;
using VTS.QLNS.CTC.App.ViewModel.Investment.EndOfInvestment.ApproveSettlementDone;
using VTS.QLNS.CTC.App.ViewModel.Investment.EndOfInvestment.RequestSettlement;
using VTS.QLNS.CTC.App.ViewModel.Investment.EndOfInvestment.ThongTriQuyetToan;
using VTS.QLNS.CTC.App.ViewModel.Investment.EndOfInvestment.QuyetToanVDT;
using VTS.QLNS.CTC.Utility;

namespace VTS.QLNS.CTC.App.ViewModel.Investment.EndOfInvestment
{
    public class EndOfInvestmentViewModel: ViewModelBase
    {
        public override string FuncCode => NSFunctionCode.INVESTMENT_END_OF_INVESTMENT;
        //public override string Name => "KẾT THÚC ĐẦU TƯ";//QUYẾT TOÁN
        public override string Name => "QUYẾT TOÁN";//QUYẾT TOÁN
        public override string Description => "Quyết toán";
        public override Type ContentType => typeof(View.Investment.EndOfInvestment.EndOfInvestment);
        public override PackIconKind IconKind => PackIconKind.Money;
        
        public RequestSettlementIndexViewModel RequestSettlementIndexViewModel { get; }
        public ApproveSettlementDoneIndexViewModel ApproveSettlementDoneIndexViewModel { get; }
        public ReportSumarySettlementViewModel ReportSumarySettlementViewModel { get; }
        public QuyetToanVDTIndexViewModel QuyetToanVDTIndexViewModel { get; }
        public ThongTriQuyetToanIndexViewModel ThongTriQuyetToanIndexViewModel { get; set; }

        public EndOfInvestmentViewModel(
            RequestSettlementIndexViewModel requestSettlementIndexViewModel,
            ReportSumarySettlementViewModel reportSumarySettlementViewModel,
            ApproveSettlementDoneIndexViewModel approveSettlementDoneIndexViewModel,
            QuyetToanVDTIndexViewModel quyetToanVDTIndexViewModel,
            ThongTriQuyetToanIndexViewModel thongTriQuyetToanIndexViewModel
            )
        {
            RequestSettlementIndexViewModel = requestSettlementIndexViewModel;
            ApproveSettlementDoneIndexViewModel = approveSettlementDoneIndexViewModel;
            ReportSumarySettlementViewModel = reportSumarySettlementViewModel;
            QuyetToanVDTIndexViewModel = quyetToanVDTIndexViewModel;
            ThongTriQuyetToanIndexViewModel = thongTriQuyetToanIndexViewModel;




            RequestSettlementIndexViewModel.ParentPage = this;
            ApproveSettlementDoneIndexViewModel.ParentPage = this;
            ReportSumarySettlementViewModel.ParentPage = this;
            QuyetToanVDTIndexViewModel.ParentPage = this;
            ThongTriQuyetToanIndexViewModel.ParentPage = this;

        }

        public override void Init()
        {
            MarginRequirement = new System.Windows.Thickness();
            Documentation = new ObservableCollection<ViewModelBase>()
            {
                QuyetToanVDTIndexViewModel,
                ThongTriQuyetToanIndexViewModel,

                RequestSettlementIndexViewModel,
                ApproveSettlementDoneIndexViewModel,
                ReportSumarySettlementViewModel
            };
            DocumentationSelectedItem = QuyetToanVDTIndexViewModel;
        }
    }
}
