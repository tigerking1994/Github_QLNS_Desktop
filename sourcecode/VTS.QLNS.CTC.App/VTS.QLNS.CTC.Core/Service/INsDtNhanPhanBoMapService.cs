using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using VTS.QLNS.CTC.Core.Domain;

namespace VTS.QLNS.CTC.Core.Service
{
    public interface INsDtNhanPhanBoMapService
    {
        IEnumerable<NsDtNhanPhanBoMap> FindByListIdNhanDuToan(IEnumerable<string> listIdNhanDuToan);
        IEnumerable<NsDtNhanPhanBoMap> FindByListIdPhanBo(IEnumerable<string> listIdPhanBo);
        IEnumerable<NsDtNhanPhanBoMap> Save(IEnumerable<NsDtNhanPhanBoMap> dtChungTuMaps);
        void DeleteByIdPhanBoDuToan(Guid idNhanDuToan);
        void RemoveDuplicate();
        void DeleteByIdNhanPhanBoDuToan(Guid idNhanDuToan);
        IEnumerable<NsDtNhanPhanBoMap> FindByIdPhanBoDuToan(string idPhanBoDuToan);
        IEnumerable<NsDtNhanPhanBoMap> FindByIdPhanBoDuToanDieuChinh(string idPhanBoDuToan);
        IEnumerable<NsDtNhanPhanBoMap> FindListIdByListIdPhanBo(IEnumerable<string> listIdPhanBo, int yearOfWork);
        IEnumerable<NsDtNhanPhanBoMap> FindByIdNhanDuToan(Guid idNhanDuToan);
        IEnumerable<NsDtNhanPhanBoMap> FindByCondition(Expression<Func<NsDtNhanPhanBoMap, bool>> predicate);
    }
}
