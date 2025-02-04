using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Utility;

namespace VTS.QLNS.CTC.Core.Repository
{
    public interface IBhDmMucLucBHXHMapRepository : IRepository<BhDmMucLucNganSach>
    {
        int AddOrUpdateRange(IEnumerable<BhDmMucLucNganSach> listEntities, AuthenticationInfo authenticationInfo);
    }    
}
