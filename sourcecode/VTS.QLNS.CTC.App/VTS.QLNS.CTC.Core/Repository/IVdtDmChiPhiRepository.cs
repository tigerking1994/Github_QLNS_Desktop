using System;
using System.Collections.Generic;
using System.Text;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Utility;

namespace VTS.QLNS.CTC.Core.Repository
{
    public interface IVdtDmChiPhiRepository : IRepository<VdtDmChiPhi>
    {
        IEnumerable<VdtDmChiPhi> FindAll(AuthenticationInfo authenticationInfo);

        int UpdateVdtDmChiPhi(IEnumerable<VdtDmChiPhi> entities);
    }
}
