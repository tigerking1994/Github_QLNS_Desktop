using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using VTS.QLNS.CTC.Core.Domain;

namespace VTS.QLNS.CTC.Core.Service
{
    public interface ITlPhuCapMlnsService
    {
        IEnumerable<TlPhuCapMln> FindAll();
        IEnumerable<TlPhuCapMln> FindByCondition(Expression<Func<TlPhuCapMln, bool>> predicate);
        int Add(TlPhuCapMln tlPhuCapMln);
        int AddRange(IEnumerable<TlPhuCapMln> tlPhuCapMlns);
        int Update(TlPhuCapMln tlPhuCapMln);
        int Delete(Guid id);
        int CountByYear(int year);
    }
}
