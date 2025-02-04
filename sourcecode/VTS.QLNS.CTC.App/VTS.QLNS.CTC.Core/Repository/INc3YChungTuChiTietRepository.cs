using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Criteria;
using VTS.QLNS.CTC.Core.Domain.Query;

namespace VTS.QLNS.CTC.Core.Repository
{
    public interface INc3YChungTuChiTietRepository : IRepository<NsNc3YChungTuChiTiet>
    {
        NsNc3YChungTuChiTiet FindById(Guid id);
        IEnumerable<NsNc3YChungTuChiTiet> FindByConditionForChildUnit(Nc3YChungTuChiTietCriteria searchModel);
        bool ExistChungTuChiTiet(Guid chungtuId);
        IEnumerable<NsNc3YChungTuChiTiet> FindByCondition(Expression<Func<NsNc3YChungTuChiTiet, bool>> predicate);
        void AddAggregate(DemandVoucherDetailCriteria creation);
    }
}