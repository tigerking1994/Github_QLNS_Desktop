using System.Collections.Generic;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Utility;

namespace VTS.QLNS.CTC.Core.Repository
{
    public interface IBhDmThamDinhQuyetToanRepository : IRepository<BhDmThamDinhQuyetToan>
    {
        IEnumerable<BhDmThamDinhQuyetToan> FindAll(AuthenticationInfo authenticationInfo);
        int AddOrUpdateRange(IEnumerable<BhDmThamDinhQuyetToan> listEntities, AuthenticationInfo authenticationInfo);

    }
}
