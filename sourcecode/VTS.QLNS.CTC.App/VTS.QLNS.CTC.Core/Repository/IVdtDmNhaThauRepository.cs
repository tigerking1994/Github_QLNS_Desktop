using System;
using System.Collections.Generic;
using System.Text;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Utility;

namespace VTS.QLNS.CTC.Core.Repository
{
    public interface IVdtDmNhaThauRepository : IRepository<VdtDmNhaThau>
    {
        IEnumerable<VdtDmNhaThau> FindAll(AuthenticationInfo authenticationInfo);

        int UpdateVdtDmNhaThauRepository(IEnumerable<VdtDmNhaThau> entities);
    }
}
