using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using VTS.QLNS.CTC.Core.Domain;

namespace VTS.QLNS.CTC.Core.Service
{
    public interface ITlQsChungTuNq104Service
    {
        IEnumerable<TlQsChungTuNq104> FindAll();
        int Add(TlQsChungTuNq104 entity);
        int Delete(Guid id);
        int Update(TlQsChungTuNq104 entity);
        TlQsChungTuNq104 Find(Guid id);
        IEnumerable<TlQsChungTuNq104> FindAll(Expression<Func<TlQsChungTuNq104, bool>> predicate);
        int UpDateRange(List<TlQsChungTuNq104> entities);
        void LockOrUnlock(Guid id, bool isLock);
        int GetSoChungTuIndexByCondition(int namLamViec);
    }
}
