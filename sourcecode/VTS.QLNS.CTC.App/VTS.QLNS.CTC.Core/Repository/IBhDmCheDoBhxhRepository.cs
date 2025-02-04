using System;
using System.Collections.Generic;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Utility;

namespace VTS.QLNS.CTC.Core.Repository
{
    public interface IBhDmCheDoBhxhRepository : IRepository<BhDmCheDoBhxh>
    {
        BhDmCheDoBhxh FindByParentId(Guid id);
        int AddOrUpdateRange(IEnumerable<BhDmCheDoBhxh> listEntities, AuthenticationInfo authenticationInfo);
    }
}
