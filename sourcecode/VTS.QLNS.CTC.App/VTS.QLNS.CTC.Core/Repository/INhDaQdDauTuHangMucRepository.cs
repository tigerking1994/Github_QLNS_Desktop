using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;

namespace VTS.QLNS.CTC.Core.Repository
{
    public interface INhDaQdDauTuHangMucRepository : IRepository<NhDaQdDauTuHangMuc>
    {
        IEnumerable<NhDaDetailHangMucQuery> GetHangMucByQdDauTuId(Guid iIdQdDauTuId);
        void AddOrUpdate(Guid qdDauTuChiPhiId, IEnumerable<NhDaQdDauTuHangMuc> items);
        void DeleteByQdDauTuChiPhiId(Guid qdDauTuChiPhiId);
    }
}
