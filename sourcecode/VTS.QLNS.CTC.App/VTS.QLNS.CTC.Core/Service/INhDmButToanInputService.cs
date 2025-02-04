using System.Collections.Generic;
using System.Linq.Expressions;
using System;
using VTS.QLNS.CTC.Core.Domain;

namespace VTS.QLNS.CTC.Core.Service
{
    public interface INhDmButToanInputService
    {
        IEnumerable<NhDmButToanInput> FindAll();
        IEnumerable<NhDmButToanInput> FindAll(Expression<Func<NhDmButToanInput, bool>> predicate);
    }
}
