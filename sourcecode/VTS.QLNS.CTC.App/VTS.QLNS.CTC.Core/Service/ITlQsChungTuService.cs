using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using VTS.QLNS.CTC.Core.Domain;

namespace VTS.QLNS.CTC.Core.Service
{
    public interface ITlQsChungTuService 
    {
        IEnumerable<TlQsChungTu> FindAll();
        int Add(TlQsChungTu entity);
        int Delete(Guid id);
        int Update(TlQsChungTu entity);
        TlQsChungTu Find(Guid id);
        IEnumerable<TlQsChungTu> FindAll(Expression<Func<TlQsChungTu, bool>> predicate);
        int UpDateRange(List<TlQsChungTu> entities);
        void LockOrUnlock(Guid id, bool isLock);
        int GetSoChungTuIndexByCondition(int namLamViec);
    }
}
