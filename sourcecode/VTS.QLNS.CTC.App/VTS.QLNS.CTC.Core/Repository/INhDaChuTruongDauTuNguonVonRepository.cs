using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VTS.QLNS.CTC.Core.Domain;

namespace VTS.QLNS.CTC.Core.Repository
{
    public interface INhDaChuTruongDauTuNguonVonRepository : IRepository<NhDaChuTruongDauTuNguonVon>
    {
        void AddOrUpdate(Guid chuTruongDauTuId, IEnumerable<NhDaChuTruongDauTuNguonVon> entities);
        void DeleteByChuTruongDauTuId(Guid chuTruongDauTuId);
    }
}
