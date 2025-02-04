using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Domain;

namespace VTS.QLNS.CTC.Core.Service
{
    public interface IBhDtTmBHYTTNService
    {
        BhDtTmBHYTTN FindById(Guid id);
        int Delete(BhDtTmBHYTTN item);
        IEnumerable<BhDtTmBHYTTNQuery> FindByCondition(int namLamViec);
        void LockOrUnlock(Guid id, bool isLock);
        int Add(BhDtTmBHYTTN entity);
        int Update(BhDtTmBHYTTN item);
        int GetSoChungTuIndexByCondition(int yearOfWork);
        IEnumerable<BhDtTmBHYTTNQuery> GetDanhSachDotNhanPhanBo(int yearOfWork, DateTime date, int iLoaiDuToan);
    }
}
