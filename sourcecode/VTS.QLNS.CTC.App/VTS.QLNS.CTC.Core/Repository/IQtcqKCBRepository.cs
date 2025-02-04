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
    public interface IQtcqKCBRepository : IRepository<BhQtcqKCB>
    {
        IEnumerable<BhQtcqKCB> FindByCondition(Expression<Func<BhQtcqKCB, bool>> predicate);
        IEnumerable<BhQtcqKCB> FindByYear(int namLamViec);
        int GetSoChungTuIndexByCondition(int iNamLamViec);
        int LockOrUnLock(Guid id, bool lockStatus);
        IEnumerable<BhQtcqKCBQuery> GetDanhSachQuyetToanKCB(int iNamLamViec);
        void UpdateTotalCPChungTu(string voucherId, string userModify);
        List<DonVi> FindByDonViForNamLamViec(int yearOfWork, int iQuy, int chungTu);
        IEnumerable<BhQtcqKCB> FindByAggregateVoucher(List<string> voucherNos, int yearOfWork);
        List<DonVi> FindByDonViTongChiForNamLamViec(int yearOfWork, int iQuy);
    }
}
