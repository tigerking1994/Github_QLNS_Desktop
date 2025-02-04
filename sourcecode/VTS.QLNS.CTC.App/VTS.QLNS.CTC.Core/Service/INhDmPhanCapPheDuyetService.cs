using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using VTS.QLNS.CTC.Core.Domain;

namespace VTS.QLNS.CTC.Core.Service
{
    public interface INhDmPhanCapPheDuyetService
    {
        IEnumerable<NhDmPhanCapPheDuyet> FindAll();
        IEnumerable<NhDmPhanCapPheDuyet> FindAll(Expression<Func<NhDmPhanCapPheDuyet, bool>> predicate);
    }
}
