using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Criteria;
using VTS.QLNS.CTC.Core.Domain.Query;
namespace VTS.QLNS.CTC.Core.Repository
{
    public interface IPbdtcMapBHXHRepository : IRepository<BhdtcnpbMapBHXH>
    {
        IEnumerable<BhdtcnpbMapBHXH> FindByCondition(Expression<Func<BhdtcnpbMapBHXH, bool>> predicate);
        bool IsExistEstimate(Guid idNhanPhanBo, Guid idPhanBo);
        IEnumerable<BhdtcnpbMapBHXH> FindByListIdNhanDuToan(IEnumerable<string> listIdNhanDuToan);
    }
}
