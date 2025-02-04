using System.Collections.Generic;
using System;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Criteria;
using VTS.QLNS.CTC.Core.Domain.Query;

namespace VTS.QLNS.CTC.Core.Service
{
    public interface ITnDtdnChungTuChiTietService
    {
        int AddRange(IEnumerable<TnDtdnChungTuChiTiet> entities);
        int RemoveRange(IEnumerable<TnDtdnChungTuChiTiet> entities);
        void Delete(Guid id);
        TnDtdnChungTuChiTiet FindById(Guid id);
        int Update(TnDtdnChungTuChiTiet entity);
        IEnumerable<TnDtdnChungTuChiTiet> FindAll();
        IEnumerable<TnDtdnChungTuChiTietQuery> FindByDataDetailCondition(EstimationVoucherDetailCriteria searchCondition);
        IEnumerable<TnDtdnChungTuChiTietQuery> FindDataReportAgencyDetailByCondition(EstimationVoucherDetailCriteria searchCondition);
        IEnumerable<TnDtdnChungTuChiTiet> FindByChungTuId(Guid id);

    }
}
