using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using VTS.QLNS.CTC.Core.Domain;

namespace VTS.QLNS.CTC.Core.Service
{
    public interface INsQsMucLucService
    {
        IEnumerable<NsQsMucLuc> FindByCondition(int yearOfWork);
        int countMLQSByNamLamViec(int yearOfWork);
        IEnumerable<NsQsMucLuc> FindAll();
        NsQsMucLuc FindMaMLNS(string  MLNS);
        IEnumerable<NsQsMucLuc> FindAll(Expression<Func<NsQsMucLuc, bool>> predicate);
        NsQsMucLuc FirstOrDefault(Expression<Func<NsQsMucLuc, bool>> predicate);
    }
}
