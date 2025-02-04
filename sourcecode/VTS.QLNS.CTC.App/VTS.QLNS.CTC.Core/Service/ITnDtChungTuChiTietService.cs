using System;
using System.Collections.Generic;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Criteria;
using VTS.QLNS.CTC.Core.Domain.Query;

namespace VTS.QLNS.CTC.Core.Service
{
    public interface ITnDtChungTuChiTietService
    {
        IEnumerable<TnDtChungTuChiTietQuery> FindByApprovedAndPlanEstimationCondition(EstimationVoucherDetailCriteria searchCondition, int type);
        IEnumerable<TnDtChungTuChiTietQuery> FindByRevenueExpendDivisionCondition(EstimationVoucherDetailCriteria searchCondition);
        IEnumerable<TnDtChungTuChiTietReportQuery> FindByPlanBudgetReportCondition(EstimationVoucherDetailCriteria searchCondition, int type);
        int AddRange(IEnumerable<TnDtChungTuChiTiet> entities);
        int RemoveRange(IEnumerable<TnDtChungTuChiTiet> entities);
        void Delete(Guid id);
        TnDtChungTuChiTiet FindById(Guid id);
        int Update(TnDtChungTuChiTiet entity);
        IEnumerable<TnDtChungTuChiTiet> FindAll();
        void AddAggregateVoucherDetail(SettlementVoucherDetailCriteria creation);
        IEnumerable<TnDtChungTuChiTiet> FindByChungtuId(Guid chungTuId);
        IEnumerable<TnDtChungTuChiTiet> FindByChungtuNhanId(Guid chungTuNhanId);
        IEnumerable<TnDtChungTuChiTietQuery> FindByRevenueExpendDivisionReport(EstimationVoucherDetailCriteria searchCondition);


    }
}
