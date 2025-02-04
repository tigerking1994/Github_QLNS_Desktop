using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Criteria;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Repository;

namespace VTS.QLNS.CTC.Core.Service
{
    public interface IKhtBHXHService
    {
        BhKhtBHXH FindById(Guid id);
        int Add(BhKhtBHXH entity);
        int GetSoChungTuIndexByCondition(int yearOfWork);
        IEnumerable<BhKhtBHXHQuery> FindByCondition(int namLamViec);
        IEnumerable<BhKhtBHXH> FindChungTuDaTongHopBySCT(string sct, int namLamViec);
        int Update(BhKhtBHXH item);
        int Delete(BhKhtBHXH item);
        void LockOrUnlock(Guid id, bool isLock);
        bool IsExistChungTuTongHop( int namLamViec);
        void AddAggregate(KhtBHXHChiTietCriteria creation);
        IEnumerable<BhKhtBHXH> FindByCondition(int namLamViec, string maDonVi, int loaiChungTu);
        IEnumerable<BhKhtBHXHQuery> FindChungTuChiTietByCondition(int namLamViec, bool bDaTongHop);
        IEnumerable<BhKhtBHXHQuery> FindChungTuChiTietTongHopByCondition(int namLamViec, int loaiTongHop, string userName);
        List<string> FindCurrentUnits(int namLamViec);
        IEnumerable<BhKhtBHXH> FindByAggregateVoucher(List<string> voucherNoes, int yearOfWork);
        BhKhtBHXH FindAggregateVoucher(int yearOfWork);
        IEnumerable<BhKhtBHXH> FindByVoucherType(int namLamViec);
        IEnumerable<BhKhtBHXH> FindByVoucherAggregateType(int namLamViec);
    }
}
