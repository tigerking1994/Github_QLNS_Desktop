using System;
using System.Collections.Generic;
using System.Text;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Repository;

namespace VTS.QLNS.CTC.Core.Service
{
    public interface INhDaHopDongCacQuyetDinhService
    {
        void Add(NhDaHopDongCacQuyetDinh nhDaHopDongCacQuyetDinh);
        void DeleteQuyetDinh(Guid? idHopDong, Guid? idQuyetDinh);
        IEnumerable<NhDaHopDongCacQuyetDinh> FindByHopDongIdQuyetDinhId(Guid? idHopDong, Guid? idQuyetDinh);
        IEnumerable<NhDaHopDongCacQuyetDinh> FindByIdHopDong(Guid? idHopDong);
    }
}
