using System;
using System.Collections.Generic;
using System.Text;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Domain.Criteria;

namespace VTS.QLNS.CTC.Core.Repository
{
    public interface ITnQtChungTuChiTietRepository : IRepository<TnQtChungTuChiTiet>
    {
        IEnumerable<TnQtChungTuChiTietQuery> FindByRealRevenueExpenditureCondition(EstimationVoucherDetailCriteria searchCondition);
        IEnumerable<TnQtChungTuChiTietReportQuery> FindByRealRevenueExpenditureReportCondition(EstimationVoucherDetailCriteria searchCondition);
        IEnumerable<TnQtChungTuChiTietReportQuery> FindByRealRevenueExpenditureResultCondition(EstimationVoucherDetailCriteria searchCondition);
        void AddAggregateVoucherDetail(SettlementVoucherDetailCriteria creation);
    }
}
