using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Criteria;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Repository;


namespace VTS.QLNS.CTC.Core.Service
{
    public interface IQtcnBHXHService
    {
        BhQtcnBHXH FindById(Guid id);
        IEnumerable<BhQtcnBHXH> FindByYear(int namLamViec);
        int Add(BhQtcnBHXH item);
        int Delete(BhQtcnBHXH item);
        int Update(BhQtcnBHXH item);
        IEnumerable<BhQtcnBHXH> FindByCondition(Expression<Func<BhQtcnBHXH, bool>> predicate);
        int GetSoChungTuIndexByCondition(int iNamLamViec);
        int LockOrUnLock(Guid id, bool lockStatus);
        IEnumerable<BhQtcnBHXHQuery> GetDanhSachQuyetToanNamBHXH(int iNamLamViec);
        bool IsExistChungTuTongHop(int namLamViec);
        void UpdateTotalCPChungTu(string voucherId, string userModify);
        List<DonVi> FindByDonViForNamLamViec(int yearOfWork, int chungTu);
        IEnumerable<BhQtcnBHXH> FindByAggregateVoucher(List<string> voucherNos, int yearOfWork);
    }
}
