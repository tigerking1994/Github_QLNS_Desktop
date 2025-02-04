using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Transactions;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Criteria;
using VTS.QLNS.CTC.Core.Domain.Query;

namespace VTS.QLNS.CTC.Core.Repository
{
    public interface IQtcnBHXHRepository : IRepository<BhQtcnBHXH>
    {
        IEnumerable<BhQtcnBHXH> FindByCondition(Expression<Func<BhQtcnBHXH, bool>> predicate);
        IEnumerable<BhQtcnBHXHQuery> GetDanhSachQuyetToanNamBHXH(int iNamLamViec);
        IEnumerable<BhQtcnBHXH> FindByYear(int namLamViec);
        int GetSoChungTuIndexByCondition(int iNamLamViec);
        int LockOrUnLock(Guid id, bool lockStatus);
        bool IsExistChungTuTongHop(int namLamViec);
        void UpdateTotalCPChungTu(string voucherId, string userModify);
        List<DonVi> FindByDonViForNamLamViec(int yearOfWork, int chungTu);
        IEnumerable<BhQtcnBHXH> FindByAggregateVoucher(List<string> voucherNos, int yearOfWork);
    }
}
