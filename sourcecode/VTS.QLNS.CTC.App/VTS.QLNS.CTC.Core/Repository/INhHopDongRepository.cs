using System;
using System.Collections.Generic;
using System.Text;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;

namespace VTS.QLNS.CTC.Core.Repository
{
    public interface INhHopDongRepository: IRepository<NhHopDong>
    {
        public IEnumerable<NhHopDongQuery> FindAllWithTiGia();
        NhHopDong FindById(Guid idHopDong);
        NhHopDong FindBySoHopSong(string soHopDong);
        bool IsExistByTiGia(Guid idTiGia);
    }
}
