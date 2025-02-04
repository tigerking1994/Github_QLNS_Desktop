using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using VTS.QLNS.CTC.Core.Domain;

namespace VTS.QLNS.CTC.Core.Repository
{
    public interface IGroupRepository : IRepository<HtNhom>
    {
        HtNhom FindByPredicate(Expression<Func<HtNhom, bool>> predicate);

        IEnumerable<HtNhom> LoadEagerGroups(Expression<Func<HtNhom, bool>> predicate);

        void UpdateNhom(HtNhom entity);

        void DeleteGroup(Guid id);

        void LockUnLockGroup(Guid id, bool status);
    }
}
