using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Transactions;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Criteria;
using VTS.QLNS.CTC.Core.Domain.Query;
namespace VTS.QLNS.CTC.Core.Repository
{
    public interface ICptuBHYTRepository : IRepository<BhCptuBHYT>
    {
        IEnumerable<BhCptuBHYT> FindByCondition(Expression<Func<BhCptuBHYT, bool>> predicate);
        int GetSoChungTuIndexByCondition(int iNamLamViec);
        int LockOrUnLock(Guid id, bool lockStatus);
        void UpdateTotalCPChungTu(string voucherId, string userModify);
        void UpdateAggregateStatus(string voucherIds);
        IEnumerable<BhCptuBHYT> FindChungTuDaTongHopBySCT(string sct, int namLamViec);
        IEnumerable<BhCptuBHYT> FindByYear(int namLamViec);
        IEnumerable<BhCptuBHYT> FindAggregateVoucher(int namLamViec);
        void AddAggregate(BhCpTUChungTuChiTietCriteria creation);
        bool IsExistChungTuTongHop(int namLamViec);
    }
}
