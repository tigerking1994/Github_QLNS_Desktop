using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using VTS.QLNS.CTC.Core.Domain;

namespace VTS.QLNS.CTC.Core.Service
{
    public interface INsBkChungTuService
    {
        List<NsBkChungTu> FindByCondition(Expression<Func<NsBkChungTu, bool>> predicate);
        void LockOrUnlock(Guid id, bool isLock);
        NsBkChungTu FindById(Guid id);
        void Add(NsBkChungTu chungTu);
        void Update(NsBkChungTu chungTu);
        int UpdateRange(IEnumerable<NsBkChungTu> chungTu);
        void Delete(Guid id);
        void DeleteRange(List<NsBkChungTu> chungTus);
        void LockOrUnlockMultiple(List<NsBkChungTu> chungTus, bool isLock);
    }
}
