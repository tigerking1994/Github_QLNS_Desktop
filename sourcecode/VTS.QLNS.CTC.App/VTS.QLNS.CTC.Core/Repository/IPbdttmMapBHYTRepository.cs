using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Criteria;
using VTS.QLNS.CTC.Core.Domain.Query;
namespace VTS.QLNS.CTC.Core.Repository
{
    public interface IPbdttmMapBHYTRepository : IRepository<BhPbdttmMapBHYT>
    {
        IEnumerable<BhPbdttmMapBHYT> FindByCondition(Expression<Func<BhPbdttmMapBHYT, bool>> predicate);
        bool IsExistEstimate(Guid idNhanPhanBo, Guid idPhanBo);
        IEnumerable<BhPbdttmMapBHYT> FindByListIdNhanDuToan(IEnumerable<string> listIdNhanDuToan);

    }
}
