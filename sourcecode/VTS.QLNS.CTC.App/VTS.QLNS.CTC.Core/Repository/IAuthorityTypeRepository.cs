using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using VTS.QLNS.CTC.Core.Domain;

namespace VTS.QLNS.CTC.Core.Repository
{
    public interface IAuthorityTypeRepository : IRepository<HtLoaiQuyen>
    {
        IEnumerable<HtLoaiQuyen> LoadEagerAuthorityTypes(Expression<Func<HtLoaiQuyen, bool>> predicate);
    }
}
