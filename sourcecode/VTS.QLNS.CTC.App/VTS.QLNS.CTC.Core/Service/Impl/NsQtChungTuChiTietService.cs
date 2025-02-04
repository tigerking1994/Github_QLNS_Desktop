using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Criteria;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Repository;

namespace VTS.QLNS.CTC.Core.Service.Impl
{
    public class NsQtChungTuChiTietService : INsQtChungTuChiTietService
    {
        private INsQtChungTuChiTietRepository _chungTuChiTietRepository;

        public NsQtChungTuChiTietService(INsQtChungTuChiTietRepository chungTuChiTietRepository)
        {
            _chungTuChiTietRepository = chungTuChiTietRepository;
        }

        /// <summary>
        /// thêm mới chứng từ chi tiết của chứng từ tổng hợp
        /// </summary>
        /// <param name="creation"></param>
        public void AddAggregateVoucherDetail(SettlementVoucherDetailCriteria creation)
        {
            _chungTuChiTietRepository.AddAggregateVoucherDetail(creation);
        }

        /// <summary>
        /// thêm mới chứng từ chi tiết
        /// </summary>
        /// <param name="voucherDetails">danh sách chứng từ chi tiết</param>
        public void AddRange(List<NsQtChungTuChiTiet> voucherDetails)
        {
            _chungTuChiTietRepository.AddRange(voucherDetails);
        }

        public void RemoveRange(List<NsQtChungTuChiTiet> entities)
        {
            _chungTuChiTietRepository.RemoveRange(entities);
        }

        public int Delete(Guid id)
        {
            NsQtChungTuChiTiet entity = _chungTuChiTietRepository.Find(id);
            if (entity != null)
                return _chungTuChiTietRepository.Delete(entity);
            return 1;
        }

        /// <summary>
        /// Lấy ra danh sách chứng từ chi tiết từ chứng từ id
        /// </summary>
        /// <param name="searchCondition">Điều kiện tìm kiếm</param>
        /// <returns></returns>
        public List<QtChungTuChiTietQuery> FindByCondition(SettlementVoucherDetailSearch searchCondition)
        {
            return _chungTuChiTietRepository.FindByCondition(searchCondition).ToList();
        }

        public void BulkInsertNsQtChungTuChiTiet(List<NsQtChungTuChiTiet> lstData)
        {
            _chungTuChiTietRepository.BulkInsert(lstData);
        }

        /// <summary>
        /// Tìm kiếm chứng từ chi tiết theo id
        /// </summary>
        /// <param name="id">id chứng từ chi tiết</param>
        /// <returns></returns>
        public NsQtChungTuChiTiet FindById(Guid id)
        {
            return _chungTuChiTietRepository.Find(id);
        }

        public List<NsQtChungTuChiTiet> FindBySummaryReport(SettlementVoucherDetailSearch searchCondition)
        {
            return _chungTuChiTietRepository.FindBySummaryReport(searchCondition).ToList();
        }

        public List<ReportQtDuToanQuyetToanQuery> FindForEstimateSettlementReport(ReportSettlementCriteria searchCondition)
        {
            return _chungTuChiTietRepository.FindForEstimateSettlementReport(searchCondition).ToList();
        }

        public List<ReportQtDuToanQuyetToanQuery> FindForEstimateSettlementReportClone(ReportSettlementCriteria searchCondition)
        {
            return _chungTuChiTietRepository.FindForEstimateSettlementReportClone(searchCondition).ToList();
        }

        public List<ReportQtChungTuChiTietQuery> FindForReportQuarterlySummary(ReportSettlementCriteria searchCondition)
        {
            return _chungTuChiTietRepository.FindForReportQuarterlySummary(searchCondition).ToList();
        }
        public List<ReportQtChungTuChiTietQuery> FindForReportQuarterlySummary2Year(ReportSettlementCriteria searchCondition)
        {
            return _chungTuChiTietRepository.FindForReportQuarterlySummary2Year(searchCondition).ToList();
        }

        public List<ReportQtThongTriDonViQuery> FindForSettlementAgencyReport(int yearOfWork, int yearOfBudget, int budgetSource, string agencyId, string quarterMonth, string LNS, int dvt, bool IsInTongHop, int iKhoi)
        {
            return _chungTuChiTietRepository.FindForSettlementAgencyReport(yearOfWork, yearOfBudget, budgetSource, agencyId, quarterMonth, LNS, dvt, IsInTongHop, iKhoi).ToList();
        }

        /// <summary>
        /// lấy data cho thông tri LNS
        /// </summary>
        /// <param name="searchCondition"></param>
        /// <returns></returns>
        public List<ReportQtThongTriLNSQuery> FindForSettlementLNSReport(SettlementVoucherDetailSearch searchCondition)
        {
            return _chungTuChiTietRepository.FindForSettlementLNSReport(searchCondition).ToList();
        }

        public List<ReportQtThongTriLNSQuery> FindForSettlementLNSSoChungTu(SettlementVoucherDetailSearch searchCondition)
        {
            return _chungTuChiTietRepository.FindForSettlementLNSSoChungTu(searchCondition).ToList();
        }

        /// <summary>
        /// lấy data cho báo cáo chứng từ thường xuyên
        /// </summary>
        /// <param name="searchCondition"></param>
        /// <returns></returns>
        public List<ReportQtChungTuChiTietQuery> FindForSettlementVoucherReport(SettlementVoucherDetailSearch searchCondition)
        {
            return _chungTuChiTietRepository.FindForSettlementVoucherReport(searchCondition).ToList();
        }

        public List<ReportQtTongHopNamQuery> FindForSummaryYearSettlementReport(ReportSettlementCriteria searchCondition)
        {
            return _chungTuChiTietRepository.FindForSummaryYearSettlementReport(searchCondition).ToList();
        }

        public List<ReportQtTongHopNamQuery> FindForSummaryYearSettlementReportUpdate(ReportSettlementCriteria searchCondition)
        {
            return _chungTuChiTietRepository.FindForSummaryYearSettlementReportUpdate(searchCondition).ToList();
        }

        public List<ReportQtNhanQuyetToanKinhPhiQuery> FindForReceiveSettlementReport(ReportSettlementCriteria searchCondition)
        {
            return _chungTuChiTietRepository.FindForReceiveSettlementReport(searchCondition).ToList();
        }


        /// <summary>
        /// Cập nhật chứng từ chi tiết
        /// </summary>
        /// <param name="voucherDetail">thông tin chứng từ chi tiết</param>

        public void Update(NsQtChungTuChiTiet voucherDetail)
        {
            _chungTuChiTietRepository.Update(voucherDetail);
        }

        public IEnumerable<NsQtChungTuChiTiet> FindByCondition(Expression<Func<NsQtChungTuChiTiet, bool>> predicate)
        {
            return _chungTuChiTietRepository.FindAll(predicate);
        }

        public void DeleteByVoucherId(Guid voucherId)
        {
            _chungTuChiTietRepository.DeleteByVoucherId(voucherId);
        }

        public void DeleteByListVoucherId(List<Guid> listVoucherId)
        {
            _chungTuChiTietRepository.DeleteByListVoucherId(listVoucherId);
        }

        public void UpdateRange(List<NsQtChungTuChiTiet> voucherDetails)
        {
            _chungTuChiTietRepository.UpdateRange(voucherDetails);
        }

        public NsQtChungTuChiTiet FindByChungTuIdAndMlnsId(Guid voucherId, string mlnsId)
        {
            return _chungTuChiTietRepository.FindByChungTuIdAndMlnsId(voucherId, mlnsId);
        }

        public List<ReportQtDuToanQuyetToanThangQuery> FindForEstimateSettlementMonthReport(ReportSettlementCriteria searchCondition)
        {
            return _chungTuChiTietRepository.FindForEstimateSettlementMonthReport(searchCondition).ToList();
        }

        public List<ReportQtDuToanQuyetToanThangQuery> FindForEstimateSettlementMonthReportClone(ReportSettlementCriteria searchCondition)
        {
            return _chungTuChiTietRepository.FindForEstimateSettlementMonthReportClone(searchCondition).ToList();
        }

        public List<ReportQtDuToanQuyetToanQuyQuery> FindForEstimateSettlementQuarterReport(ReportSettlementCriteria searchCondition)
        {
            return _chungTuChiTietRepository.FindForEstimateSettlementQuarterReport(searchCondition).ToList();
        }

        public List<ReportQtDuToanQuyetToanQuyQuery> FindForEstimateSettlementQuarterReportClone(ReportSettlementCriteria searchCondition)
        {
            return _chungTuChiTietRepository.FindForEstimateSettlementQuarterReportClone(searchCondition).ToList();
        }

        public List<ReportQtDuToanQuyetToanTongThangQuery> FindForEstimateSettlementSummaryMonthReport(ReportSettlementCriteria searchCondition)
        {
            return _chungTuChiTietRepository.FindForEstimateSettlementSummaryMonthReport(searchCondition).ToList();
        }

        public List<ReportQuyetToanTongHopDonViQuery> FindForSummaryAgencyReport(ReportSettlementCriteria searchCondition)
        {
            return _chungTuChiTietRepository.FindForSummaryAgencyReport(searchCondition).ToList();
        }

        public IEnumerable<string> GetLnsHasData(List<Guid> chungTuIds)
        {
            return _chungTuChiTietRepository.GetLnsHasData(chungTuIds);
        }

        public void UpdateMonth(Guid voucherId, int month, int monthType, string userName)
        {
            _chungTuChiTietRepository.UpdateMonth(voucherId, month, monthType, userName);
        }

        public IEnumerable<FindApprovalSettlementYearQuery> FindApprovalSettlementYear(int yearOfWork, string yearOfBuget, string listLns, string listUnit, int dataType, int budgetSource, int unit, string printType)
        {
            return _chungTuChiTietRepository.FindApprovalSettlementYear(yearOfWork, yearOfBuget, listLns, listUnit, dataType, budgetSource, unit, printType);
        }

        public IEnumerable<PrintYearSettlementQuery> FindSettlementYear(int yearOfWork, string yearOfBudget, string listLns, string listUnit, int dataType, int budgetSource, int unit, string printType)
        {
            return _chungTuChiTietRepository.FindSettlementYear(yearOfWork, yearOfBudget, listLns, listUnit, dataType, budgetSource, unit, printType);
        }
        public IEnumerable<NsQuyetToanChiTietCongKhaiQuery> FindQTChungTuChiTietCongKhai(EstimationVoucherDetailCriteria searchCondition)
        {
            return _chungTuChiTietRepository.FindQTChungTuChiTietCongKhai(searchCondition);
        }

    }
}
