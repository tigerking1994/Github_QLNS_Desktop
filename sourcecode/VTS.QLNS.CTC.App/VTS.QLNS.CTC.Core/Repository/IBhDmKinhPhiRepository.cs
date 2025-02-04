using System.Collections.Generic;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Utility;

namespace VTS.QLNS.CTC.Core.Repository
{
    public interface IBhDmKinhPhiRepository : IRepository<BhDmKinhPhi>
    {
        IEnumerable<BhDmKinhPhi> FindAll(AuthenticationInfo authenticationInfo);
        int AddOrUpdateRange(IEnumerable<BhDmKinhPhi> listEntities, AuthenticationInfo authenticationInfo);
    }
}
