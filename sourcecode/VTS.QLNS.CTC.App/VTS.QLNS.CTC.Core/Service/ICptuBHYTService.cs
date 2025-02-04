using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Criteria;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Repository;

namespace VTS.QLNS.CTC.Core.Service
{
    public interface ICptuBHYTService
    {
        IEnumerable<BhCptuBHYT> FindByCondition(Expression<Func<BhCptuBHYT, bool>> predicate);
        int GetSoChungTuIndexByCondition(int iNamLamViec);
        int Add(BhCptuBHYT item);
        int Update(BhCptuBHYT item);
        int Delete(BhCptuBHYT item);
        BhCptuBHYT FindById(Guid id);
        int LockOrUnLock(Guid id, bool lockStatus);
        void UpdateTotalCPChungTu(string voucherId, string userModify);
        void UpdateAggregateStatus(string voucherIds);
        IEnumerable<BhCptuBHYT> FindChungTuDaTongHopBySCT(string sct, int namLamViec);
        IEnumerable<BhCptuBHYT> FindByYear(int namLamViec);
        void LockOrUnlock(Guid id, bool isLock);
        IEnumerable<BhCptuBHYT> FindAggregateVoucher(int namLamViec);
        void AddAggregate(BhCpTUChungTuChiTietCriteria creation);
        bool IsExistChungTuTongHop(int namLamViec);
    }
}
