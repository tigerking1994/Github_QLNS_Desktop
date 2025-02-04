using System;
using System.Collections.Generic;
using System.Text;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;

namespace VTS.QLNS.CTC.Core.Service
{
    public interface INhHopDongService
    {
        IEnumerable<NhHopDong> FindAll();

        IEnumerable<NhHopDongQuery> FindAllWithTiGia();

        void Add(NhHopDong nhHopDong);

        NhHopDong FindById(Guid idHopDong);

        void Update(NhHopDong nhHopDong);

        NhHopDong FindBySoHopSong(string soHopDong);
    }
}
