using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using VTS.QLNS.CTC.Core.Domain;

namespace VTS.QLNS.CTC.Core.Service
{
    public interface IBhDmThamDinhQuyetToanService
    {
        IEnumerable<BhDmThamDinhQuyetToan> FindAll(Expression<Func<BhDmThamDinhQuyetToan, bool>> predicate);
    }
}
