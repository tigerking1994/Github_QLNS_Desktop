using System.Collections.Generic;
using System.Linq.Expressions;
using System;
using VTS.QLNS.CTC.Core.Domain;

namespace VTS.QLNS.CTC.Core.Service
{
    public interface INhDmTaiKhoanService
    {
        IEnumerable<NhDmTaiKhoan> FindAll();
        IEnumerable<NhDmTaiKhoan> FindAll(Expression<Func<NhDmTaiKhoan, bool>> predicate);
    }
}
