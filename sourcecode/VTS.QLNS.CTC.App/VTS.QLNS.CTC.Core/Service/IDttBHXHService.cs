using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Criteria;
using VTS.QLNS.CTC.Core.Domain.Query;

namespace VTS.QLNS.CTC.Core.Service
{
    public interface IDttBHXHService
    {
        BhDttBHXH FindById(Guid id);
        int Delete(BhDttBHXH item);
        IEnumerable<BhDttBHXHQuery> FindByCondition(int namLamViec);
        void LockOrUnlock(Guid id, bool isLock);
        int Add(BhDttBHXH entity);
        int Update(BhDttBHXH item);
        int GetSoChungTuIndexByCondition(int yearOfWork);
    }
}
