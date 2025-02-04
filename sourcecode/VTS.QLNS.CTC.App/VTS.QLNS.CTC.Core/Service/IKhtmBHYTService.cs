using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Criteria;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Repository;

namespace VTS.QLNS.CTC.Core.Service
{
    public interface IKhtmBHYTService
    {
        IEnumerable<BhKhtmBHYTQuery> FindByCondition(int namLamViec);
        BhKhtmBHYT FindById(Guid id);
        int Delete(BhKhtmBHYT item);
        IEnumerable<BhKhtmBHYT> FindByCondition(Expression<Func<BhKhtmBHYT, bool>> predicate);
        int Update(BhKhtmBHYT item);
        int GetSoChungTuIndexByCondition(int yearOfWork);
        IEnumerable<BhKhtmBHYT> FindByCondition(int namLamViec, string maDonVi, int loaiChungTu);
        int Add(BhKhtmBHYT entity);
        IEnumerable<BhKhtmBHYT> FindChungTuDaTongHopBySCT(string sct, int namLamViec);
        void AddAggregate(KhtmBHYTChiTietCriteria creation);
        void LockOrUnlock(Guid id, bool isLock);
        bool IsExistChungTuTongHop(int namLamViec);
        IEnumerable<BhKhtmBHYTQuery> FindChungTuChiTietTongHopByCondition(int namLamViec, int loaiTongHop, bool bDaTongHop, string userName);
        List<string> FindCurrentUnits(int namLamViec);
        IEnumerable<BhKhtmBHYT> FindByAggregateVoucher(List<string> voucherNoes, int yearOfWork);
        BhKhtmBHYT FindAggregateVoucher(int yearOfWork);
        IEnumerable<BhKhtmBHYTQuery> FindChungTuChiTietDonVi(int namLamViec, int loaiTongHop, string userName);
    }
}
