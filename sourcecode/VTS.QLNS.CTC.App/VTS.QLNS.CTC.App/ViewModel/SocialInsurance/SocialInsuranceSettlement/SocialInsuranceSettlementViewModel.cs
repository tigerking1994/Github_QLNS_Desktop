using MaterialDesignThemes.Wpf;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using VTS.QLNS.CTC.App.ViewModel.SocialInsurance.SocialInsuranceSettlement.QuyetToanCapKinhPhiKCBBHYT;
using VTS.QLNS.CTC.App.ViewModel.SocialInsurance.SocialInsuranceSettlement.QuyetToanChiKinhPhiQuanLy;
using VTS.QLNS.CTC.App.ViewModel.SocialInsurance.SocialInsuranceSettlement.QuyetToanChiNamBHXH;
using VTS.QLNS.CTC.App.ViewModel.SocialInsurance.SocialInsuranceSettlement.QuyetToanChiNamKCB;
using VTS.QLNS.CTC.App.ViewModel.SocialInsurance.SocialInsuranceSettlement.QuyetToanChiQuyBHXH;
using VTS.QLNS.CTC.App.ViewModel.SocialInsurance.SocialInsuranceSettlement.QuyetToanChiQuyKCB;
using VTS.QLNS.CTC.App.ViewModel.SocialInsurance.SocialInsuranceSettlement.QuyetToanChiQuyKinhPhiKhac;
using VTS.QLNS.CTC.App.ViewModel.SocialInsurance.SocialInsuranceSettlement.QuyetToanNamChiKinhPhiKhac;
using VTS.QLNS.CTC.App.ViewModel.SocialInsurance.SocialInsuranceSettlement.QuyetToanNamChiKinhPhiQuanLy;
using VTS.QLNS.CTC.App.ViewModel.SocialInsurance.SocialInsuranceSettlement.QuyetToanThuBHXH;
using VTS.QLNS.CTC.App.ViewModel.SocialInsurance.SocialInsuranceSettlement.QuyetToanThuMuaBHYT;
using VTS.QLNS.CTC.App.ViewModel.SocialInsurance.SocialInsuranceSettlement.Report;
using VTS.QLNS.CTC.App.ViewModel.SocialInsurance.SocialInsuranceSettlement.ThamDinhQuyetToan;

namespace VTS.QLNS.CTC.App.ViewModel.SocialInsurance.SocialInsuranceSettlement
{
    public class SocialInsuranceSettlementViewModel : ViewModelBase
    {
        public override string Name => "QUYẾT TOÁN";
        public override string Description => "Quyêt toán";
        public override Type ContentType => typeof(View.SocialInsurance.SocialInsuranceSettlement.SocialInsuranceSettlement);
        public override PackIconKind IconKind => PackIconKind.Dollar;
        //public override string FuncCode => NSFunctionCode.SOCIAL_INSURANCE_LAP_KE_HOACH_CHI;

        public QuyetToanChiQuyBHXHIndexViewModel QuyetToanChiQuyBHXHIndexViewModel { get; }
        public QuyetToanChiNamBHXHIndexViewModel QuyetToanChiNamBHXHIndexViewModel { get; }
        public QuyetToanChiKinhPhiQuanLyIndexViewModel QuyetToanChiKinhPhiQuanLyIndexViewModel { get; set; }
        public QuyetToanThuIndexViewModel QuyetToanThuIndexViewModel { get; }
        public QuyetToanNamChiKinhPhiQuanLyIndexViewModel QuyetToanNamChiKinhPhiQuanLyIndexViewModel { get; }
        public QuyetToanThuMuaIndexViewModel QuyetToanThuMuaIndexViewModel { get; }
        public QuyetToanChiQuyKinhPhiKhacIndexViewModel QuyetToanChiQuyKinhPhiKhacIndexViewModel { get; }
        //public QuyetToanCapKinhPhiKcbIndexViewModel QuyetToanCapKinhPhiKcbIndexViewModel { get; }

        public QuyetToanNamChiKinhPhiKhacIndexViewModel QuyetToanNamChiKinhPhiKhacIndexViewModel { get; }
        public ThamDinhQuyetToanIndexViewModel ThamDinhQuyetToanIndexViewModel { get; }
        public QuyetToanChiQuyKCBIndexViewModel QuyetToanChiQuyKCBIndexViewModel { get; }
        public QuyetToanChiNamKCBIndexViewModel QuyetToanChiNamKCBIndexViewModel { get; }
        public SocailInsuranceSettlementReportIndexViewModel SocailInsuranceSettlementReportIndexViewModel { get; }
        public SocailInsuranceSettlementReportQuarterIndexViewModel SocailInsuranceSettlementReportQuarterIndexViewModel { get; }
        public SocailInsuranceSettlementReportYearIndexViewModel SocailInsuranceSettlementReportYearIndexViewModel { get; }
        public SocialInsuranceSettlementReportGeneralIndexViewModel SocialInsuranceSettlementReportGeneralIndexViewModel { get; }

        public SocialInsuranceSettlementViewModel(
            QuyetToanChiNamBHXHIndexViewModel quyetToanChiNamBHXHIndexViewModel,
            QuyetToanThuIndexViewModel quyetToanThuIndexViewModel,
            QuyetToanChiKinhPhiQuanLyIndexViewModel quyetToanChiKinhPhiQuanLyIndexViewModel,
            QuyetToanChiQuyBHXHIndexViewModel quyetToanChiQuyBHXHIndexViewModel,
            QuyetToanNamChiKinhPhiQuanLyIndexViewModel quyetToanNamChiKinhPhiQuanLyIndexViewModel,
            QuyetToanThuMuaIndexViewModel quyetToanThuMuaIndexViewModel,
            QuyetToanNamChiKinhPhiKhacIndexViewModel quyetToanNamChiKinhPhiKhacIndexViewModel,
            QuyetToanChiQuyKCBIndexViewModel quyetToanChiQuyKCBIndexViewModel,
            QuyetToanChiNamKCBIndexViewModel quyetToanChiNamKCBIndexViewModel,
            QuyetToanChiQuyKinhPhiKhacIndexViewModel quyetToanChiQuyKinhPhiKhacIndexViewModel,
            QuyetToanCapKinhPhiKcbIndexViewModel quyetToanCapKinhPhiKcbIndexViewModel,
            ThamDinhQuyetToanIndexViewModel thamDinhQuyetToanIndexViewModel,
            SocailInsuranceSettlementReportIndexViewModel socailInsuranceSettlementReportIndexViewModel,
            SocailInsuranceSettlementReportQuarterIndexViewModel socailInsuranceSettlementReportQuarterIndexViewModel,
            SocailInsuranceSettlementReportYearIndexViewModel socailInsuranceSettlementReportYearIndexViewModel,
            SocialInsuranceSettlementReportGeneralIndexViewModel socialInsuranceSettlementReportGeneralIndexViewModel
            )
        {
            QuyetToanChiNamBHXHIndexViewModel = quyetToanChiNamBHXHIndexViewModel;
            QuyetToanThuIndexViewModel = quyetToanThuIndexViewModel;
            QuyetToanThuMuaIndexViewModel = quyetToanThuMuaIndexViewModel;
            QuyetToanChiKinhPhiQuanLyIndexViewModel = quyetToanChiKinhPhiQuanLyIndexViewModel;
            QuyetToanChiQuyBHXHIndexViewModel = quyetToanChiQuyBHXHIndexViewModel;
            QuyetToanNamChiKinhPhiQuanLyIndexViewModel = quyetToanNamChiKinhPhiQuanLyIndexViewModel;
            QuyetToanChiQuyKCBIndexViewModel = quyetToanChiQuyKCBIndexViewModel;
            QuyetToanChiQuyKinhPhiKhacIndexViewModel = quyetToanChiQuyKinhPhiKhacIndexViewModel;
            QuyetToanNamChiKinhPhiKhacIndexViewModel = quyetToanNamChiKinhPhiKhacIndexViewModel;
            QuyetToanChiNamKCBIndexViewModel = quyetToanChiNamKCBIndexViewModel;
            //QuyetToanCapKinhPhiKcbIndexViewModel = quyetToanCapKinhPhiKcbIndexViewModel;
            ThamDinhQuyetToanIndexViewModel = thamDinhQuyetToanIndexViewModel;
            SocailInsuranceSettlementReportIndexViewModel = socailInsuranceSettlementReportIndexViewModel;
            SocailInsuranceSettlementReportQuarterIndexViewModel = socailInsuranceSettlementReportQuarterIndexViewModel;
            SocailInsuranceSettlementReportYearIndexViewModel = socailInsuranceSettlementReportYearIndexViewModel;
            SocialInsuranceSettlementReportGeneralIndexViewModel = socialInsuranceSettlementReportGeneralIndexViewModel;

            ThamDinhQuyetToanIndexViewModel.ParentPage = this;
            QuyetToanChiQuyKinhPhiKhacIndexViewModel.ParentPage = this;
            QuyetToanThuIndexViewModel.ParentPage = this;
            QuyetToanThuMuaIndexViewModel.ParentPage = this;
            QuyetToanChiKinhPhiQuanLyIndexViewModel.ParentPage = this;
            QuyetToanChiNamBHXHIndexViewModel.ParentPage = this;
            QuyetToanChiQuyBHXHIndexViewModel.ParentPage = this;
            QuyetToanNamChiKinhPhiQuanLyIndexViewModel.ParentPage = this;
            QuyetToanNamChiKinhPhiKhacIndexViewModel.ParentPage = this;
            QuyetToanChiQuyKCBIndexViewModel.ParentPage = this;
            QuyetToanChiNamKCBIndexViewModel.ParentPage = this;
            //QuyetToanCapKinhPhiKcbIndexViewModel.ParentPage = this;
            SocailInsuranceSettlementReportIndexViewModel.ParentPage = this;
            SocailInsuranceSettlementReportQuarterIndexViewModel.ParentPage = this;
            SocailInsuranceSettlementReportYearIndexViewModel.ParentPage = this;
            SocialInsuranceSettlementReportGeneralIndexViewModel.ParentPage = this;
        }

        public override void Init()
        {
            MarginRequirement = new System.Windows.Thickness();
            Documentation = new ObservableCollection<ViewModelBase>()
            {
                QuyetToanThuIndexViewModel,
                QuyetToanThuMuaIndexViewModel,

                QuyetToanChiQuyBHXHIndexViewModel,
                QuyetToanChiKinhPhiQuanLyIndexViewModel,
                QuyetToanChiQuyKCBIndexViewModel,
                QuyetToanChiQuyKinhPhiKhacIndexViewModel,

                QuyetToanChiNamBHXHIndexViewModel,
                QuyetToanNamChiKinhPhiQuanLyIndexViewModel,
                QuyetToanChiNamKCBIndexViewModel,
                QuyetToanNamChiKinhPhiKhacIndexViewModel,
                //QuyetToanCapKinhPhiKcbIndexViewModel,
                ThamDinhQuyetToanIndexViewModel,

                SocailInsuranceSettlementReportIndexViewModel,
                SocailInsuranceSettlementReportQuarterIndexViewModel,
                SocailInsuranceSettlementReportYearIndexViewModel,
                SocialInsuranceSettlementReportGeneralIndexViewModel
            };
            DocumentationSelectedItem = Documentation.FirstOrDefault(x => x.IsAuthorized);
        }
    }
}
