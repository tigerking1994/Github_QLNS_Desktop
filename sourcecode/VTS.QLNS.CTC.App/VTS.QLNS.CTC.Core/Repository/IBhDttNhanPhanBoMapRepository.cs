using System;
using System.Collections.Generic;
using VTS.QLNS.CTC.Core.Domain;

namespace VTS.QLNS.CTC.Core.Repository
{
    public interface IBhDttNhanPhanBoMapRepository : IRepository<BhDttNhanPhanBoMap>
    {
        public IEnumerable<BhDttNhanPhanBoMap> FindByIdNhanDuToan(Guid idNhanDuToan);
        void DeleteByIdPhanBoDuToan(Guid id);
        public IEnumerable<BhDttNhanPhanBoMap> FindByIdPhanBoDuToan(string idPhanBoDuToan);
        public IEnumerable<BhDttNhanPhanBoMap> FindByListIdNhanDuToan(IEnumerable<string> listIdNhanDuToan);
        public IEnumerable<BhDttNhanPhanBoMap> FindByListIdPhanBo(IEnumerable<string> listIdPhanBo);
    }
}
