using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Criteria;
using VTS.QLNS.CTC.Core.Domain.Query;

namespace VTS.QLNS.CTC.Core.Service
{
    public interface INsQtChungTuChiTietService
    {
        List<QtChungTuChiTietQuery> FindByCondition(SettlementVoucherDetailSearch searchCondition);
        List<NsQtChungTuChiTiet> FindBySummaryReport(SettlementVoucherDetailSearch searchCondition);
        IEnumerable<NsQtChungTuChiTiet> FindByCondition(Expression<Func<NsQtChungTuChiTiet, bool>> predicate);
        NsQtChungTuChiTiet FindById(Guid id);
        void AddRange(List<NsQtChungTuChiTiet> voucherDetails);
        void RemoveRange(List<NsQtChungTuChiTiet> entities);
        void Update(NsQtChungTuChiTiet voucherDetail);
        int Delete(Guid id);
        void AddAggregateVoucherDetail(SettlementVoucherDetailCriteria creation);
        List<ReportQtChungTuChiTietQuery> FindForReportQuarterlySummary(ReportSettlementCriteria searchCondition);
        List<ReportQtChungTuChiTietQuery> FindForReportQuarterlySummary2Year(ReportSettlementCriteria searchCondition);
        List<ReportQtChungTuChiTietQuery> FindForSettlementVoucherReport(SettlementVoucherDetailSearch searchCondition);
        List<ReportQtThongTriLNSQuery> FindForSettlementLNSReport(SettlementVoucherDetailSearch searchCondition);
        List<ReportQtThongTriLNSQuery> FindForSettlementLNSSoChungTu(SettlementVoucherDetailSearch searchCondition);
        List<ReportQtThongTriDonViQuery> FindForSettlementAgencyReport(int yearOfWork, int yearOfBudget, int budgetSource, string agencyId, string quarterMonth, string LNS, int dvt, bool IsInTongHop, int iKhoi);
        List<ReportQtDuToanQuyetToanQuery> FindForEstimateSettlementReport(ReportSettlementCriteria searchCondition);
        List<ReportQtDuToanQuyetToanQuery> FindForEstimateSettlementReportClone(ReportSettlementCriteria searchCondition);
        List<ReportQtDuToanQuyetToanThangQuery> FindForEstimateSettlementMonthReport(ReportSettlementCriteria searchCondition);
        List<ReportQtDuToanQuyetToanThangQuery> FindForEstimateSettlementMonthReportClone(ReportSettlementCriteria searchCondition);
        List<ReportQtDuToanQuyetToanQuyQuery> FindForEstimateSettlementQuarterReport(ReportSettlementCriteria searchCondition);
        List<ReportQtDuToanQuyetToanQuyQuery> FindForEstimateSettlementQuarterReportClone(ReportSettlementCriteria searchCondition);
        List<ReportQtDuToanQuyetToanTongThangQuery> FindForEstimateSettlementSummaryMonthReport(ReportSettlementCriteria searchCondition);
        List<ReportQtTongHopNamQuery> FindForSummaryYearSettlementReport(ReportSettlementCriteria searchCondition);
        List<ReportQtTongHopNamQuery> FindForSummaryYearSettlementReportUpdate(ReportSettlementCriteria searchCondition);
        List<ReportQtNhanQuyetToanKinhPhiQuery> FindForReceiveSettlementReport(ReportSettlementCriteria searchCondition);
        void DeleteByVoucherId(Guid voucherId);
        void DeleteByListVoucherId(List<Guid> listVoucherId);
        void UpdateRange(List<NsQtChungTuChiTiet> voucherDetails);
        NsQtChungTuChiTiet FindByChungTuIdAndMlnsId(Guid voucherId, string mlnsId);
        List<ReportQuyetToanTongHopDonViQuery> FindForSummaryAgencyReport(ReportSettlementCriteria searchCondition);
        IEnumerable<string> GetLnsHasData(List<Guid> chungTuIds);
        void UpdateMonth(Guid voucherId, int month, int monthType, string userName);
        IEnumerable<FindApprovalSettlementYearQuery> FindApprovalSettlementYear(int yearOfWork, string yearOfBuget, string listLns, string listUnit, int dataType, int budgetSource, int unit, string printType);
        IEnumerable<PrintYearSettlementQuery> FindSettlementYear(int yearOfWork, string yearOfBudget, string listLns, string listUnit, int dataType, int budgetSource, int unit, string printType);
        IEnumerable<NsQuyetToanChiTietCongKhaiQuery> FindQTChungTuChiTietCongKhai(EstimationVoucherDetailCriteria searchCondition);
        void BulkInsertNsQtChungTuChiTiet(List<NsQtChungTuChiTiet> lstData);
    }
}
