using System;
using System.Collections.Generic;
using System.Text;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Domain.Criteria;

namespace VTS.QLNS.CTC.Core.Repository
{
    public interface ITnDtChungTuChiTietRepository : IRepository<TnDtChungTuChiTiet>
    {
        IEnumerable<TnDtChungTuChiTietQuery> FindByApprovedAndPlanEstimationCondition(EstimationVoucherDetailCriteria searchCondition, int type);
        IEnumerable<TnDtChungTuChiTietQuery> FindByRevenueExpendDivisionCondition(EstimationVoucherDetailCriteria searchCondition);
        IEnumerable<TnDtChungTuChiTietReportQuery> FindByPlanBudgetReportCondition(EstimationVoucherDetailCriteria searchCondition, int type);
        void AddAggregateVoucherDetail(SettlementVoucherDetailCriteria creation);
        IEnumerable<TnDtChungTuChiTiet> FindByChungtuId(Guid chungTuId);
        IEnumerable<TnDtChungTuChiTiet> FindByChungtuNhanId(Guid chungTuNhanId);
        IEnumerable<TnDtChungTuChiTietQuery> FindByRevenueExpendDivisionReport(EstimationVoucherDetailCriteria searchCondition);
    }
}
