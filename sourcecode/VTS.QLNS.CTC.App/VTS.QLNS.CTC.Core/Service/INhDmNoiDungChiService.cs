using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using VTS.QLNS.CTC.Core.Domain;

namespace VTS.QLNS.CTC.Core.Service
{
    public interface INhDmNoiDungChiService
    {
        IEnumerable<NhDmNoiDungChi> FindAll();
        IEnumerable<NhDmNoiDungChi> FindAll(Expression<Func<NhDmNoiDungChi, bool>> predicate);
    }
}
