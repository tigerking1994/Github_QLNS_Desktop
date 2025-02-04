using System;
using System.Collections.Generic;
using System.Text;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Utility;

namespace VTS.QLNS.CTC.Core.Repository
{
    public interface IDMLoaiCongTrinhRepository : IRepository<VdtDmLoaiCongTrinh>
    {
        IEnumerable<VdtDmLoaiCongTrinh> FindAll(AuthenticationInfo authenticationInfo);

        int UpdateDmLoaiCongTrinh(IEnumerable<VdtDmLoaiCongTrinh> entities);

        List<VdtDmLoaiCongTrinh> GetAll();
    }
}
