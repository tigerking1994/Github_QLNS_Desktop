using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Criteria;
using VTS.QLNS.CTC.Core.Domain.Query;

namespace VTS.QLNS.CTC.Core.Service
{
    public interface IBhCpBsChungTuService
    {
        BhCpBsChungTu FindById(Guid id);
        int Delete(BhCpBsChungTu item);
        IEnumerable<BhCpBsChungTu> FindChungTuDaTongHopBySCT(string sct, int namLamViec);
        int Update(BhCpBsChungTu item);
        bool IsExistChungTuTongHop(int namLamViec);
        IEnumerable<BhCpBsChungTu> FindByYear(int namLamViec);
        void LockOrUnlock(Guid id, bool isLock);
        int GetSoChungTuIndexByCondition(int yearOfWork);
        int Add(BhCpBsChungTu entity);
        IEnumerable<BhCpBsChungTu> FindByCondition(int namLamViec, int loaiChungTu);
        void AddAggregate(BhCpBsChungTuChiTietCriteria creation);
        IEnumerable<BhCpBsChungTu> FindByAggregateVoucher(List<string> voucherNoes, int yearOfWork);
    }
}
