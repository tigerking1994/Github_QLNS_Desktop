using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using VTS.QLNS.CTC.Core.Domain;
namespace VTS.QLNS.CTC.Core.Service
{
    public interface IVdtDmNhaThauService
    {
        IEnumerable<VdtDmNhaThau> FindAll(Expression<Func<VdtDmNhaThau, bool>> predicate);
        VdtDmNhaThau Find(params object[] keyValues);
    }
}
