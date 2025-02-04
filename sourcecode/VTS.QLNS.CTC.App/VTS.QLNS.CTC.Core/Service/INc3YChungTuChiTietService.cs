using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Criteria;
using VTS.QLNS.CTC.Core.Domain.Query;

namespace VTS.QLNS.CTC.Core.Service
{
    public interface INc3YChungTuChiTietService
    {
        int AddRange(IEnumerable<NsNc3YChungTuChiTiet> nc3YChungTuChiTiets);
        int UpdateRange(IEnumerable<NsNc3YChungTuChiTiet> nc3YChungTuChiTiets);
        int RemoveRange(IEnumerable<NsNc3YChungTuChiTiet> nc3YChungTuChiTiets);
        int Add(NsNc3YChungTuChiTiet entity);
        int Delete(NsNc3YChungTuChiTiet entity);
        int Update(NsNc3YChungTuChiTiet entity);
        NsNc3YChungTuChiTiet FindById(Guid id);        
        IEnumerable<NsNc3YChungTuChiTiet> FindByConditionForChildUnit(Nc3YChungTuChiTietCriteria searchModel);
        bool ExistChungTuChiTiet(Guid chungtuId);
        IEnumerable<NsNc3YChungTuChiTiet> FindByCondition(Expression<Func<NsNc3YChungTuChiTiet, bool>> predicate);
        void AddAggregate(DemandVoucherDetailCriteria creation);
    }
}