using System;
using System.Collections.Generic;
using System.Text;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Utility;

namespace VTS.QLNS.CTC.Core.Service
{
    public interface IDmLoaiCongTrinhService
    {
        IEnumerable<VdtDmLoaiCongTrinh> FindAll(AuthenticationInfo authenticationInfo);
        List<VdtDmLoaiCongTrinh> GetAll();
    }
}
