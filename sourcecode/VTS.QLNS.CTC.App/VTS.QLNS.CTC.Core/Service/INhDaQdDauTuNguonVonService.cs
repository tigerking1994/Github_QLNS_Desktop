using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;

namespace VTS.QLNS.CTC.Core.Service
{
    public interface INhDaQdDauTuNguonVonService
    {
        void AddRange(IEnumerable<NhDaQdDauTuNguonVon> entities);
        void UpdateRange(IEnumerable<NhDaQdDauTuNguonVon> entities);
        void RemoveRange(IEnumerable<NhDaQdDauTuNguonVon> entities);
        NhDaQdDauTuNguonVon FindById(Guid id);
        IEnumerable<NhDaQdDauTuNguonVon> FindByQdDauTuId(Guid qdDauTuId);
        IEnumerable<NhDaDetailNguonVonQuery> GetNguonVonByQdDauTuId(Guid iIdQdDauTuId);
        List<NHDAQDDauTuNguonVonQuery> FindByDuAnId(Guid id);
    }
}
