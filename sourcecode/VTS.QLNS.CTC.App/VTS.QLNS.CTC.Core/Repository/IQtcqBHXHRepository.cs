using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq.Expressions;
using System.Transactions;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Criteria;
using VTS.QLNS.CTC.Core.Domain.Query;

namespace VTS.QLNS.CTC.Core.Repository
{
    public interface IQtcqBHXHRepository : IRepository<BhQtcqBHXH>
    {
        IEnumerable<BhQtcqBHXH> FindByCondition(Expression<Func<BhQtcqBHXH, bool>> predicate);
        IEnumerable<BhQtcqBHXH> FindByYear(int namLamViec);
        int GetSoChungTuIndexByCondition(int iNamLamViec);
        int LockOrUnLock(Guid id, bool lockStatus);
        IEnumerable<BhQtcqBHXHQuery> GetDanhSachQuyetToanQuyBHXH(int iNamLamViec);
        void UpdateTotalCPChungTu(string voucherId, string userModify);
        IEnumerable<DonVi> FindByDonViForNamLamViec(int namLamViec, int iQuy, int iLoaiChungTu);
        int DeleteDupItem(Guid voucherID);
    }
}
