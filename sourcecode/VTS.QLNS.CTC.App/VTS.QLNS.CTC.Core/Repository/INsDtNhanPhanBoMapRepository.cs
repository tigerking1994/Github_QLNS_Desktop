using System;
using System.Collections.Generic;
using VTS.QLNS.CTC.Core.Domain;

namespace VTS.QLNS.CTC.Core.Repository
{
    public interface INsDtNhanPhanBoMapRepository : IRepository<NsDtNhanPhanBoMap>
    {
        void DeleteByIdNhanPhanBoDuToan(Guid id);
        void DeleteByIdPhanBoDuToan(Guid id);
        void RemoveDuplicate();
        public IEnumerable<NsDtNhanPhanBoMap> FindByListIdNhanDuToan(IEnumerable<string> listIdNhanDuToan);
        public IEnumerable<NsDtNhanPhanBoMap> FindByListIdPhanBo(IEnumerable<string> listIdPhanBo);
        public IEnumerable<NsDtNhanPhanBoMap> FindByIdPhanBoDuToan(string idPhanBoDuToan);
        public IEnumerable<NsDtNhanPhanBoMap> FindByIdPhanBoDuToanDieuChinh(string idPhanBoDuToan);
        public IEnumerable<NsDtNhanPhanBoMap> FindListIdByListIdPhanBo(IEnumerable<string> listIdPhanBo, int yearOfWork);
        public IEnumerable<NsDtNhanPhanBoMap> FindByIdNhanDuToan(Guid idNhanDuToan);
    }
}
