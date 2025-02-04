using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VTS.QLNS.CTC.Core.Domain;

namespace VTS.QLNS.CTC.Core.Service
{
    public interface INhDaDuToanChiPhiService
    {
        IEnumerable<NhDaDuToanChiPhi> FindByDuToanId(Guid duToanId);
        IEnumerable<NhDaDuToanChiPhi> FindByNguonVonId(Guid NguonVonId);
    }
}
