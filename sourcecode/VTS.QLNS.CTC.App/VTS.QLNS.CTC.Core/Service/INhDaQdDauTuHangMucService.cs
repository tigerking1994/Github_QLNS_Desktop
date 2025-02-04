using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;

namespace VTS.QLNS.CTC.Core.Service
{
    public interface INhDaQdDauTuHangMucService
    {
        void AddRange(IEnumerable<NhDaQdDauTuHangMuc> entities);
        void UpdateRange(IEnumerable<NhDaQdDauTuHangMuc> entities);
        void RemoveRange(IEnumerable<NhDaQdDauTuHangMuc> entities);
        IEnumerable<NhDaQdDauTuHangMuc> FindByQdDauTuChiPhiId(Guid qdDauTuChiPhiId);
        IEnumerable<NhDaQdDauTuHangMuc> FindByQdDauTuChiPhiIds(IEnumerable<Guid> qdDauTuChiPhiIds);
        IEnumerable<NhDaDetailHangMucQuery> GetHangMucByQdDauTuId(Guid iIdQdDauTuId);
        NhDaQdDauTuHangMuc FindById(Guid id);
    }
}
