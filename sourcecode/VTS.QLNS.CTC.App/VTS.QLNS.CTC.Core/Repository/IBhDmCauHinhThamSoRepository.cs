using Microsoft.AspNetCore.Http.Authentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Utility;

namespace VTS.QLNS.CTC.Core.Repository
{
    public interface IBhDmCauHinhThamSoRepository : IRepository<BhDmCauHinhThamSo>
    {
        int AddOrUpdateRange(IEnumerable<BhDmCauHinhThamSo> listEntities, AuthenticationInfo authenticationInfo);
    }
}
