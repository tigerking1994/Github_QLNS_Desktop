using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Repository;

namespace VTS.QLNS.CTC.Core.Service
{
    public interface INhDaHopDongNguonVonService
    {
        void Add(NhDaHopDongNguonVon nhDaHopDongNguonVon);
        void Delete(NhDaHopDongNguonVon nhDaHopDongNguonVon);
        int UpdateRange(IEnumerable<NhDaHopDongNguonVon> entities);
        IEnumerable<NhDaHopDongNguonVon> FindByIdHopDong(Guid idHopDong);
        IEnumerable<NhDaHopDongNguonVon> FindByHopDongIdQuyetDinhId(Guid? idHopDong, Guid? idQuyetDinh);
        IEnumerable<NhDaHopDongNguonVon> FindByHopDongIdGoiThauNguonVonId(Guid? idHopDong, Guid? idGoiThauNguonVon);
        IEnumerable<NhDaHopDongNguonVon> FindAll(Expression<Func<NhDaHopDongNguonVon, bool>> predicate);
    }
}
