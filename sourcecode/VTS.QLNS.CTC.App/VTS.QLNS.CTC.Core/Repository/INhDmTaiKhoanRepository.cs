using System.Collections.Generic;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Utility;

namespace VTS.QLNS.CTC.Core.Repository
{
    public interface INhDmTaiKhoanRepository : IRepository<NhDmTaiKhoan>
    {
        IEnumerable<NhDmTaiKhoan> FindAll(AuthenticationInfo authenticationInfo);

    }
}
