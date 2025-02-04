using System;
using System.Collections.Generic;
using System.Text;
using VTS.QLNS.CTC.Core.Domain;

namespace VTS.QLNS.CTC.Core.Repository
{
    public interface INhDaHopDongNguonVonRepository : IRepository<NhDaHopDongNguonVon>
    {
        public IEnumerable<NhDaHopDongNguonVon> FindByIdHopDong(Guid idHopDong);
        public IEnumerable<NhDaHopDongNguonVon> FindByHopDongIdQuyetDinhId(Guid? idHopDong, Guid? idQuyetDinh);
        public IEnumerable<NhDaHopDongNguonVon> FindByHopDongIdGoiThauNguonVonId(Guid? idHopDong, Guid? idGoiThauNguonVon);
    }
}
