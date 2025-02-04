using System;
using System.Collections.Generic;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Criteria;
using VTS.QLNS.CTC.Core.Domain.Query;

namespace VTS.QLNS.CTC.Core.Service
{
    public interface ITnQtChungTuChiTietService
    {
        int AddRange(IEnumerable<TnQtChungTuChiTiet> entities);
        void Delete(Guid id);
        TnQtChungTuChiTiet FindById(Guid id);
        int Update(TnQtChungTuChiTiet entity);
        IEnumerable<TnQtChungTuChiTietQuery> FindByRealRevenueExpenditureCondition(EstimationVoucherDetailCriteria searchCondition);
        IEnumerable<TnQtChungTuChiTietReportQuery> FindByRealRevenueExpenditureReportCondition(EstimationVoucherDetailCriteria searchCondition);
        IEnumerable<TnQtChungTuChiTietReportQuery> FindByRealRevenueExpenditureResultCondition(EstimationVoucherDetailCriteria searchCondition);
        void AddAggregateVoucherDetail(SettlementVoucherDetailCriteria creation);
    }
}
