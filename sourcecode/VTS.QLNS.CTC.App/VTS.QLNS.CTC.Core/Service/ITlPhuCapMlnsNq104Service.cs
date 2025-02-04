using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using VTS.QLNS.CTC.Core.Domain;

namespace VTS.QLNS.CTC.Core.Service
{
    public interface ITlPhuCapMlnsNq104Service
    {
        IEnumerable<TlPhuCapMlnNq104> FindAll();
        IEnumerable<TlPhuCapMlnNq104> FindByCondition(Expression<Func<TlPhuCapMlnNq104, bool>> predicate);
        int Add(TlPhuCapMlnNq104 tlPhuCapMln);
        int AddRange(IEnumerable<TlPhuCapMlnNq104> tlPhuCapMlns);
        int Update(TlPhuCapMlnNq104 tlPhuCapMln);
        int Delete(Guid id);
        int CountByYear(int year);
    }
}
