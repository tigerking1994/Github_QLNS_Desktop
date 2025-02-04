using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using VTS.QLNS.CTC.Core.Domain;

namespace VTS.QLNS.CTC.Core.Service
{
    public interface ITlDmHslKeHoachService
    {
        IEnumerable<TlDmHslKeHoach> FindAll(Expression<Func<TlDmHslKeHoach, bool>> predicate);
        IEnumerable<TlDmHslKeHoach> FindAll();
        TlDmHslKeHoach FindById(Guid id);
    }
}
