using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VTS.QLNS.CTC.Core.Domain;

namespace VTS.QLNS.CTC.Core.Service
{
    public interface INhDaChuTruongDauTuNguonVonService
    {
        void AddRange(IEnumerable<NhDaChuTruongDauTuNguonVon> entities);
        void UpdateRange(IEnumerable<NhDaChuTruongDauTuNguonVon> entities);
        void RemoveRange(IEnumerable<NhDaChuTruongDauTuNguonVon> entities);
        IEnumerable<NhDaChuTruongDauTuNguonVon> FindByChuTruongDauTuId(Guid chuTruongDauTuId);
        NhDaChuTruongDauTuNguonVon FindById(Guid id);
    }
}
