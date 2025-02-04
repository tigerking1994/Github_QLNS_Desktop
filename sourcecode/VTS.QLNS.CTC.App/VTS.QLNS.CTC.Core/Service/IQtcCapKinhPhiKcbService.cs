using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VTS.QLNS.CTC.Core.Domain;

namespace VTS.QLNS.CTC.Core.Service
{
    public interface IQtcCapKinhPhiKcbService
    {
        BhQtCapKinhPhiKcb FindById(Guid id);
        int Delete(BhQtCapKinhPhiKcb item);
        IEnumerable<BhQtCapKinhPhiKcb> FindByYear(int namLamViec);
        void LockOrUnlock(Guid id, bool isLock);
        int GetVoucherIndex(int yearOfWork);
        int Add(BhQtCapKinhPhiKcb entity);
        int Update(BhQtCapKinhPhiKcb item);
    }
}
