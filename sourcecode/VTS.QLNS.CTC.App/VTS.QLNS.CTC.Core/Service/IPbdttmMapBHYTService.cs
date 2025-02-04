using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Criteria;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Repository;

namespace VTS.QLNS.CTC.Core.Service
{
    public interface IPbdttmMapBHYTService
    {
        IEnumerable<BhPbdttmMapBHYT> FindByCondition(Expression<Func<BhPbdttmMapBHYT, bool>> predicate);
        bool IsExistEstimate(Guid idNhanPhanBo, Guid idPhanBo);
        int Add(BhPbdttmMapBHYT item);
        int AddRange(List<BhPbdttmMapBHYT> items);
        int Update(BhPbdttmMapBHYT item);
        int Delete(BhPbdttmMapBHYT item);
        int RemoveRange(List<BhPbdttmMapBHYT> items);
        IEnumerable<BhPbdttmMapBHYT> FindByListIdNhanDuToan(IEnumerable<string> listIdNhanDuToan);
    }
}
