using System;
using System.Collections.Generic;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Criteria;
using VTS.QLNS.CTC.Core.Domain.Query;
namespace VTS.QLNS.CTC.Core.Repository
{
    public interface IKhtBHXHRepository : IRepository<BhKhtBHXH>
    {
        int GetSoChungTuIndexByCondition(int yearOfWork);
        IEnumerable<BhKhtBHXHQuery> FindByCondition(int namLamViec);
        IEnumerable<BhKhtBHXHQuery> FindChungTuChiTietByCondition(int namLamViec, bool bDaTongHop);
        IEnumerable<BhKhtBHXHQuery> FindChungTuChiTietTongHopByCondition(int namLamViec, int loaiTongHop, string userName);
        void LockOrUnLock(string id, bool isLock);
        bool IsExistChungTuTongHop(int namLamViec);
        void AddAggregate(KhtBHXHChiTietCriteria creation);
        IEnumerable<BhKhtBHXH> FindByCondition(int namLamViec, string maDonVi, int loaiChungTu);
        IEnumerable<BhKhtBHXH> FindChungTuDaTongHopBySCT(string sct, int namLamViec);
        List<string> FindCurrentUnits(int namLamViec);
        IEnumerable<BhKhtBHXH> FindByAggregateVoucher(List<string> voucherNoes, int yearOfWork);
        BhKhtBHXH FindAggregateVoucher(int yearOfWork);
        IEnumerable<BhKhtBHXH> FindByVoucherType(int namLamViec);
        IEnumerable<BhKhtBHXH> FindByVoucherAggregateType(int namLamViec);
    }
}
