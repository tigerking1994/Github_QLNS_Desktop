using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VTS.QLNS.CTC.Core.Domain;

namespace VTS.QLNS.CTC.Core.Repository
{
    public interface IDmDTChiRepository : IRepository<VdtDmDuToanChi>
    {
        IEnumerable<VdtDmDuToanChi> FindAllDTChi();
        new void AddOrUpdateRange(IEnumerable<VdtDmDuToanChi> entities);
    }
}
