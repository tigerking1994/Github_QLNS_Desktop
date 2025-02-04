using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Utility;

namespace VTS.QLNS.CTC.Core.Repository
{
    public interface IBhDmCoSoYTeRepository : IRepository<BhDmCoSoYTe>
    {
        IEnumerable<BhDmCoSoYTe> FindByCondition(Expression<Func<BhDmCoSoYTe, bool>> predicate);
        List<BhDmCoSoYTe> GetListCoSoYTe(int namLamViec);
        BhDmCoSoYTe GetCSYTByMa(string maCSYT, int namLamViec);
        bool ExistCSYT(string maCSYT, int namLamViec);
        int AddOrUpdateRange(IEnumerable<BhDmCoSoYTe> listEntities, AuthenticationInfo authenticationInfo);
        IEnumerable<BhDmCoSoYTe> FindAll(AuthenticationInfo authenticationInfo);
    }
}
