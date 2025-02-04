using System.Collections.Generic;
using System.Linq.Expressions;
using System;
using VTS.QLNS.CTC.Core.Domain;

namespace VTS.QLNS.CTC.Core.Service
{
    public interface INhDmCongThucOutputService
    {
        IEnumerable<NhDmCongThucOutput> FindAll();
        IEnumerable<NhDmCongThucOutput> FindAll(Expression<Func<NhDmCongThucOutput, bool>> predicate);
    }
}
