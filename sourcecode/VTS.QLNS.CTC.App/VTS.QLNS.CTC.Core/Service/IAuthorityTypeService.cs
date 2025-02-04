using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using VTS.QLNS.CTC.Core.Domain;

namespace VTS.QLNS.CTC.Core.Service
{
    public interface IAuthorityTypeService
    {
        IEnumerable<HtLoaiQuyen> FindAll(Expression<Func<HtLoaiQuyen, bool>> predicate);
        IEnumerable<HtLoaiQuyen> FindAll();
    }
}
