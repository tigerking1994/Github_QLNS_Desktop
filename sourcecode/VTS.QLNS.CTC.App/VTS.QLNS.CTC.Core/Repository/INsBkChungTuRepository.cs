using System;
using System.Collections.Generic;
using System.Text;
using VTS.QLNS.CTC.Core.Domain;

namespace VTS.QLNS.CTC.Core.Repository
{
    public interface INsBkChungTuRepository : IRepository<NsBkChungTu>
    {
        void LockOrUnlockMultiple(List<NsBkChungTu> chungTus, bool isLock);
    }
}
