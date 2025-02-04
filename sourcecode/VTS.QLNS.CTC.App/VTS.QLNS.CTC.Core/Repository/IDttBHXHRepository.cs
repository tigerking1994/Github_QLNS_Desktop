using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;

namespace VTS.QLNS.CTC.Core.Repository
{
    public interface IDttBHXHRepository : IRepository<BhDttBHXH>
    {
        IEnumerable<BhDttBHXHQuery> FindByCondition(int namLamViec);
        void LockOrUnLock(string id, bool isLock);
        int GetSoChungTuIndexByCondition(int yearOfWork);
    }
}
