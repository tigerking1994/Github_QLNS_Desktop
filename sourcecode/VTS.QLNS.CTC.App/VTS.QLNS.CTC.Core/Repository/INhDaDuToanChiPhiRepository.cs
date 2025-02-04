using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;

namespace VTS.QLNS.CTC.Core.Repository
{
    public interface INhDaDuToanChiPhiRepository : IRepository<NhDaDuToanChiPhi>
    {
        void AddOrUpdate(IEnumerable<NhDaDuToanChiPhi> entities);
        void AddAdjust(Guid duToanNguonVonId, IEnumerable<NhDaDuToanChiPhi> entities);
        void DeleteByDuAnId(Guid duToanId);
        IEnumerable<NhDaDetailChiPhiQuery> GetAllByDuToanId(Guid iIdDuToanId);
        IEnumerable<NhDaDuToanChiPhi> FindByDuToanId(Guid iIdDuToanNguonvonId);
    }
}
