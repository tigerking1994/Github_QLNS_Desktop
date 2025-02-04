using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;

namespace VTS.QLNS.CTC.Core.Repository
{
    public interface INhDaQdDauTuChiPhiRepository : IRepository<NhDaQdDauTuChiPhi>
    {
        IEnumerable<NhDaDetailChiPhiQuery> GetChiPhiByQdDauTuId(Guid iIdQdDauTuId);
        IEnumerable<NhDaQdDauTuChiPhi> FindByQdDauTuId(Guid qdDauTuId);
        void DeleteByQdDauTuId(Guid qdDauTuId);
        void DeleteByQdNguonVonId(Guid idNguonVon);
        IEnumerable<NhDaQdDauTuChiPhi> FindByQdDauTuNGuonVonId(Guid idNguonVon);
        IEnumerable<NhDaQdDauTuChiPhi> FindByQdDauTuByDuAnId(Guid iIdDuAn);
    }
}
