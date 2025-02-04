using System;
using System.Collections.Generic;
using VTS.QLNS.CTC.Core.Domain;

namespace VTS.QLNS.CTC.Core.Service
{
    public interface IBhDttNhanPhanBoMapService
    {
        IEnumerable<BhDttNhanPhanBoMap> FindByIdNhanDuToan(Guid idNhanDuToan);
        void DeleteByIdPhanBoDuToan(Guid idNhanDuToan);
        IEnumerable<BhDttNhanPhanBoMap> FindByIdPhanBoDuToan(string idPhanBoDuToan);
        IEnumerable<BhDttNhanPhanBoMap> FindByListIdNhanDuToan(IEnumerable<string> listIdNhanDuToan);
        IEnumerable<BhDttNhanPhanBoMap> FindByListIdPhanBo(IEnumerable<string> listIdPhanBo);
        IEnumerable<BhDttNhanPhanBoMap> Save(IEnumerable<BhDttNhanPhanBoMap> dtChungTuMaps);
    }
}
