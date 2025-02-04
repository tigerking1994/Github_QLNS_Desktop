using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Criteria;
using VTS.QLNS.CTC.Core.Domain.Query;

namespace VTS.QLNS.CTC.Core.Service
{
    public interface ITnQtChungTuChiTietHD4554Service
    {
        int AddRange(IEnumerable<TnQtChungTuChiTietHD4554> entities);
        void Delete(Guid id);
        TnQtChungTuChiTietHD4554 FindById(Guid id);
        int Update(TnQtChungTuChiTietHD4554 entity);
        IEnumerable<TnQtChungTuChiTietHD4554Query> FindByRealRevenueExpenditureCondition(EstimationVoucherDetailCriteria searchCondition);
        IEnumerable<TnQtChungTuChiTietReportQuery> FindByRealRevenueExpenditureReportCondition(EstimationVoucherDetailCriteria searchCondition);
        IEnumerable<TnQtChungTuChiTietReportQuery> FindByRealRevenueExpenditureResultCondition(EstimationVoucherDetailCriteria searchCondition);
        void AddAggregateVoucherDetail(SettlementVoucherDetailCriteria creation);
        IEnumerable<TnQtChungTuChiTietHD4554Query> GetDataChungTuDetail(EstimationVoucherDetailCriteria searchCondition);
        IEnumerable<string> GetLnsHasData(List<Guid> guids);
        IEnumerable<TnQtChungTuChiTietHD4554> FindByIdChiTiet(Guid IdChungTu);
        IEnumerable<TnQtChungTuChiTietHD4554> FindByCondition(Expression<Func<TnQtChungTuChiTietHD4554, bool>> predicate);
        void RemoveRange(List<TnQtChungTuChiTietHD4554> listChungTuChiTiet);
        void UpdateMonth(Guid voucherId, int month, int monthType, string userName);
        List<TnQtChungTuChiTietHD4554Query> FindAllNSDCChungTuByCondition(EstimationVoucherDetailCriteria searchCondition);
    }
}
