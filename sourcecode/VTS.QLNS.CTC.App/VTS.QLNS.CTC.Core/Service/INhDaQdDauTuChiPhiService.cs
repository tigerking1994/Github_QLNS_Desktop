using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;

namespace VTS.QLNS.CTC.Core.Service
{
    public interface INhDaQdDauTuChiPhiService
    {
        void Add(NhDaQdDauTuChiPhi entity);
        void Update(NhDaQdDauTuChiPhi entity);
        void Remove(NhDaQdDauTuChiPhi entity);
        void AddRange(IEnumerable<NhDaQdDauTuChiPhi> entities);
        void UpdateRange(IEnumerable<NhDaQdDauTuChiPhi> entities);
        void RemoveRange(IEnumerable<NhDaQdDauTuChiPhi> entities);
        NhDaQdDauTuChiPhi FindById(Guid id);
        IEnumerable<NhDaQdDauTuChiPhi> FindByQdDauTuId(Guid qdDauTuId);
        IEnumerable<NhDaDetailChiPhiQuery> GetChiPhiByQdDauTuId(Guid iIdQdDauTuId);
        IEnumerable<NhDaQdDauTuChiPhi> FindByNguonVonId(Guid idNguonVon);
        IEnumerable<NhDaQdDauTuChiPhi> FindByQdDauTuByDuAnId(Guid idDuAn);
    }
}
