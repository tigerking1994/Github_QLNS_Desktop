using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Criteria;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Repository;

namespace VTS.QLNS.CTC.Core.Service
{
    public interface IPbdtcMapBHXHService
    {
        IEnumerable<BhdtcnpbMapBHXH> FindByCondition(Expression<Func<BhdtcnpbMapBHXH, bool>> predicate);
        int Add(BhdtcnpbMapBHXH item);
        int Update(BhdtcnpbMapBHXH item);
        IEnumerable<BhdtcnpbMapBHXH> Save(IEnumerable<BhdtcnpbMapBHXH> dtChungTuMaps);
        int RemoveRange(IEnumerable<BhdtcnpbMapBHXH> dtChungTuMaps);
        bool IsExistEstimate(Guid idNhanPhanBo, Guid idPhanBo);
        IEnumerable<BhdtcnpbMapBHXH> FindByListIdNhanDuToan(IEnumerable<string> listIdNhanDuToan);
    }
}
