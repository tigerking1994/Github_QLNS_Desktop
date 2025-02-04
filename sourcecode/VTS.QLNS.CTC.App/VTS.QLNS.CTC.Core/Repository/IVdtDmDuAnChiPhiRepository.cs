using System;
using System.Collections.Generic;
using System.Text;
using VTS.QLNS.CTC.Core.Domain;

namespace VTS.QLNS.CTC.Core.Repository
{
    public interface IVdtDmDuAnChiPhiRepository : IRepository<VdtDmDuAnChiPhi>
    {
        public VdtDmDuAnChiPhi FindByName(string name);
        public VdtDmDuAnChiPhi FindByMaChiPhi(string ma);
    }
}
