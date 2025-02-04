using System;
using System.Collections.Generic;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Criteria;
using VTS.QLNS.CTC.Core.Domain.Query;

namespace VTS.QLNS.CTC.Core.Repository
{
    public interface INsQtChungTuChiTietRepository : IRepository<NsQtChungTuChiTiet>
    {
        IEnumerable<QtChungTuChiTietQuery> FindByCondition(SettlementVoucherDetailSearch searchCondition);
        IEnumerable<NsQtChungTuChiTiet> FindBySummaryReport(SettlementVoucherDetailSearch searchCondition);
        void AddAggregateVoucherDetail(SettlementVoucherDetailCriteria creation);
        IEnumerable<ReportQtChungTuChiTietQuery> FindForReportQuarterlySummary(ReportSettlementCriteria condition);
        IEnumerable<ReportQtChungTuChiTietQuery> FindForReportQuarterlySummary2Year(ReportSettlementCriteria condition);
        IEnumerable<ReportQtChungTuChiTietQuery> FindForSettlementVoucherReport(SettlementVoucherDetailSearch searchCondition);
        IEnumerable<ReportQtThongTriLNSQuery> FindForSettlementLNSReport(SettlementVoucherDetailSearch searchCondition);
        IEnumerable<ReportQtThongTriLNSQuery> FindForSettlementLNSSoChungTu(SettlementVoucherDetailSearch searchCondition);
        IEnumerable<ReportQtThongTriDonViQuery> FindForSettlementAgencyReport(int yearOfWork, int yearOfBudget, int budgetSource, string agencyId, string quarterMonth, string LNS, int dvt, bool IsInTongHop, int iKhoi);
        IEnumerable<ReportQtDuToanQuyetToanQuery> FindForEstimateSettlementReport(ReportSettlementCriteria condition);
        IEnumerable<ReportQtDuToanQuyetToanQuery> FindForEstimateSettlementReportClone(ReportSettlementCriteria condition);
        IEnumerable<ReportQtDuToanQuyetToanThangQuery> FindForEstimateSettlementMonthReport(ReportSettlementCriteria condition);
        IEnumerable<ReportQtDuToanQuyetToanThangQuery> FindForEstimateSettlementMonthReportClone(ReportSettlementCriteria condition);
        IEnumerable<ReportQtDuToanQuyetToanQuyQuery> FindForEstimateSettlementQuarterReport(ReportSettlementCriteria condition);
        IEnumerable<ReportQtDuToanQuyetToanQuyQuery> FindForEstimateSettlementQuarterReportClone(ReportSettlementCriteria condition);
        IEnumerable<ReportQtDuToanQuyetToanTongThangQuery> FindForEstimateSettlementSummaryMonthReport(ReportSettlementCriteria condition);
        IEnumerable<ReportQtTongHopNamQuery> FindForSummaryYearSettlementReport(ReportSettlementCriteria searchCondition);
        IEnumerable<ReportQtTongHopNamQuery> FindForSummaryYearSettlementReportUpdate(ReportSettlementCriteria searchCondition);
        List<ReportQtNhanQuyetToanKinhPhiQuery> FindForReceiveSettlementReport(ReportSettlementCriteria searchCondition);

        void DeleteByVoucherId(Guid voucherId);
        void DeleteByListVoucherId(List<Guid> listVoucherId);
        NsQtChungTuChiTiet FindByChungTuIdAndMlnsId(Guid voucherId, string mlnsId);
        IEnumerable<ReportQuyetToanTongHopDonViQuery> FindForSummaryAgencyReport(ReportSettlementCriteria searchCondition);
        IEnumerable<FindApprovalSettlementYearQuery> FindApprovalSettlementYear(int yearOfWork, string yearOfBuget, string listLns, string listUnit, int dataType, int budgetSource, int unit, string printType);
        IEnumerable<string> GetLnsHasData(List<Guid> chungTuIds);
        void UpdateMonth(Guid voucherId, int month, int monthType, string userName);
        IEnumerable<PrintYearSettlementQuery> FindSettlementYear(int yearOfWork, string yearOfBuget, string listLns, string listUnit, int dataType, int budgetSource, int unit, string printType);
        List<NsQtChungTuChiTiet> FindByParentId(List<Guid> iIds);
        IEnumerable<NsQuyetToanChiTietCongKhaiQuery> FindQTChungTuChiTietCongKhai(EstimationVoucherDetailCriteria criteria);

    }
}
