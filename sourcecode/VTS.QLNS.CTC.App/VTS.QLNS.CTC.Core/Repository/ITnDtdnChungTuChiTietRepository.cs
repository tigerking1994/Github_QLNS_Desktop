using System;
using System.Collections.Generic;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Criteria;
using VTS.QLNS.CTC.Core.Domain.Query;

namespace VTS.QLNS.CTC.Core.Repository
{
    public interface ITnDtdnChungTuChiTietRepository : IRepository<TnDtdnChungTuChiTiet>
    {
        IEnumerable<TnDtdnChungTuChiTietQuery> FindByDataDetailCondition(EstimationVoucherDetailCriteria searchCondition);
        IEnumerable<TnDtdnChungTuChiTiet> FindByChungTuId(Guid id);
        IEnumerable<TnDtdnChungTuChiTietQuery> FindDataReportAgencyDetailByCondition(EstimationVoucherDetailCriteria searchCondition);

    }
}
