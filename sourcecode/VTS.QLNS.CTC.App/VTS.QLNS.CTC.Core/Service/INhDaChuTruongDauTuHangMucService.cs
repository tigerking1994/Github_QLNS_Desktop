using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VTS.QLNS.CTC.Core.Domain;

namespace VTS.QLNS.CTC.Core.Service
{
    public interface INhDaChuTruongDauTuHangMucService
    {
        void AddRange(IEnumerable<NhDaChuTruongDauTuHangMuc> entities);
        void UpdateRange(IEnumerable<NhDaChuTruongDauTuHangMuc> entities);
        void RemoveRange(IEnumerable<NhDaChuTruongDauTuHangMuc> entities);
        IEnumerable<NhDaChuTruongDauTuHangMuc> FindByChuTruongDauTuId(Guid chuTruongDauTuId);
    }
}
