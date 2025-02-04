using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using VTS.QLNS.CTC.Core.Domain;

namespace VTS.QLNS.CTC.Core.Service
{
    public interface INsQsChungTuService
    {
        NsQsChungTu FindById(Guid id);
        NsQsChungTu FindByMonth(int month, int yearOfWork);
        List<NsQsChungTu> FindByCondition(Expression<Func<NsQsChungTu, bool>> predicate);
        List<int> FindMonthOfArmy(int yearOfWork);
        string GenerateVoucherNo(int voucherNoIndex);
        void LockOrUnlock(Guid id, bool isLock);
        void Add(NsQsChungTu chungTu);
        void Update(NsQsChungTu chungTu);
        void Delete(Guid id);
        void RemoveRange(List<NsQsChungTu> entities);
    }
}
