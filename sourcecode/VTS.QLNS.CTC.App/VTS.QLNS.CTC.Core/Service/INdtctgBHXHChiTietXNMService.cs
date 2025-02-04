using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;
using static Dapper.SqlMapper;

namespace VTS.QLNS.CTC.Core.Service
{
    public interface INdtctgBHXHChiTietXNMService
    {
        IEnumerable<BhDtctgBHXHChiTietXNM> FindByCondition(Expression<Func<BhDtctgBHXHChiTietXNM, bool>> predicate);
        int AddRange(IEnumerable<BhDtctgBHXHChiTietXNM> lstItems);
        int RemoveRange(IEnumerable<BhDtctgBHXHChiTietXNM> lstItems);
        int RemoveRange(Expression<Func<BhDtctgBHXHChiTietXNM, bool>> predicate);

    }
}
