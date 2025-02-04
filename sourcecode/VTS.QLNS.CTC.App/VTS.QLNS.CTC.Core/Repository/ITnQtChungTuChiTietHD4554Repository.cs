using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Criteria;
using VTS.QLNS.CTC.Core.Domain.Query;

namespace VTS.QLNS.CTC.Core.Repository
{
    public interface ITnQtChungTuChiTietHD4554Repository : IRepository<TnQtChungTuChiTietHD4554>
    {
        void AddAggregateVoucherDetail(SettlementVoucherDetailCriteria creation);
        List<TnQtChungTuChiTietHD4554Query> FindAllNSDCChungTuByCondition(EstimationVoucherDetailCriteria searchCondition);
        IEnumerable<TnQtChungTuChiTietHD4554> FindByIdChiTiet(Guid idChungTu);
        IEnumerable<TnQtChungTuChiTietHD4554Query> FindByRealRevenueExpenditureCondition(EstimationVoucherDetailCriteria searchCondition);
        IEnumerable<TnQtChungTuChiTietReportQuery> FindByRealRevenueExpenditureReportCondition(EstimationVoucherDetailCriteria searchCondition);
        IEnumerable<TnQtChungTuChiTietReportQuery> FindByRealRevenueExpenditureResultCondition(EstimationVoucherDetailCriteria searchCondition);
        IEnumerable<TnQtChungTuChiTietHD4554Query> GetDataChungTuDetail(EstimationVoucherDetailCriteria searchCondition);
        IEnumerable<string> GetLnsHasData(List<Guid> chungTuIds);
        void UpdateMonth(Guid voucherId, int month, int monthType, string userName);
    }
}
