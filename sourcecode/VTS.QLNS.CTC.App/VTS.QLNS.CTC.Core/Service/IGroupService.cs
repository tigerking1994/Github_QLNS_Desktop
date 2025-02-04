using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using VTS.QLNS.CTC.Core.Domain;

namespace VTS.QLNS.CTC.Core.Service
{
    public interface IGroupService
    {
        IEnumerable<HtNhom> FindAll(Expression<Func<HtNhom, bool>> predicate);
        HtNhom FindByPredicate(Expression<Func<HtNhom, bool>> predicate);
        void Update(HtNhom entity);
        void Add(HtNhom entity);
        void RemoveRange(IEnumerable<HtNhom> entities);
        IEnumerable<HtNhom> LoadEagerGroups(Expression<Func<HtNhom, bool>> predicate);
        HtNhom FindById(Guid id);
        void UpdateRange(IEnumerable<HtNhom> entities);
        void Delete(Guid id);
        void LockOrUnLock(Guid id, bool isActivated);
    }
}
