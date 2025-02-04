using System;
using System.Collections.Generic;
using System.Text;
using VTS.QLNS.CTC.Core.Domain;

namespace VTS.QLNS.CTC.Core.Repository
{
    public interface INhDaHopDongCacQuyetDinhRepository : IRepository<NhDaHopDongCacQuyetDinh>
    {
        public IEnumerable<NhDaHopDongCacQuyetDinh> FindByHopDongIdQuyetDinhId(Guid? idHopDong, Guid? idQuyetDinh);
        public IEnumerable<NhDaHopDongCacQuyetDinh> FindByIdHopDong(Guid? idHopDong);
        public void DeleteQuyetDinh(Guid? idHopDong, Guid? idQuyetDinh);
    }
}
