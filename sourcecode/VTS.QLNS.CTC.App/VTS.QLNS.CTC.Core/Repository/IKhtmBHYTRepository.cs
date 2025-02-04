using System;
using System.Collections.Generic;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Criteria;
using VTS.QLNS.CTC.Core.Domain.Query;

namespace VTS.QLNS.CTC.Core.Repository
{
    public interface IKhtmBHYTRepository : IRepository<BhKhtmBHYT>
    {
        IEnumerable<BhKhtmBHYTQuery> FindByCondition(int namLamViec);
        int GetSoChungTuIndexByCondition(int yearOfWork);
        IEnumerable<BhKhtmBHYT> FindByCondition(int namLamViec, string maDonVi, int loaiChungTu);
        IEnumerable<BhKhtmBHYT> FindChungTuDaTongHopBySCT(string sct, int namLamViec);
        void AddAggregate(KhtmBHYTChiTietCriteria creation);
        void LockOrUnLock(string id, bool isLock);
        bool IsExistChungTuTongHop(int namLamViec);
        IEnumerable<BhKhtmBHYTQuery> FindChungTuChiTietTongHopByCondition(int namLamViec, int loaiTongHop, bool bDaTongHop, string userName);
        List<string> FindCurrentUnits(int namLamViec);
        IEnumerable<BhKhtmBHYT> FindByAggregateVoucher(List<string> voucherNoes, int yearOfWork);
        BhKhtmBHYT FindAggregateVoucher(int yearOfWork);
        IEnumerable<BhKhtmBHYTQuery> FindChungTuChiTietDonVi(int namLamViec, int loaiTongHop, string userName);
    }
}
