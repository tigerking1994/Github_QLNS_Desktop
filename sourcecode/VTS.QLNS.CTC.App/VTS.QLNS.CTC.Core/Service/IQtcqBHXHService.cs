using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Criteria;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Repository;


namespace VTS.QLNS.CTC.Core.Service
{
    public interface IQtcqBHXHService
    {
        BhQtcqBHXH FindById(Guid id);
        IEnumerable<BhQtcqBHXH> FindByYear(int namLamViec);
        int Add(BhQtcqBHXH item);
        int Delete(BhQtcqBHXH item);
        int Update(BhQtcqBHXH item);
        IEnumerable<BhQtcqBHXH> FindByCondition(Expression<Func<BhQtcqBHXH, bool>> predicate);
        int GetSoChungTuIndexByCondition(int iNamLamViec);
        int LockOrUnLock(Guid id, bool lockStatus);
        IEnumerable<BhQtcqBHXHQuery> GetDanhSachQuyetToanQuyBHXH(int iNamLamViec);
        void UpdateTotalCPChungTu(string voucherId, string userModify);
        IEnumerable<DonVi> FindByDonViForNamLamViec(int namLamViec, int iQuy, int iLoaiChungTu);
        int DeleteDupItem(Guid voucherID);
    }
}
