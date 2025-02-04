using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Transactions;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Criteria;
using VTS.QLNS.CTC.Core.Domain.Query;

namespace VTS.QLNS.CTC.Core.Repository
{
    public interface IQtcnKCBRepository : IRepository<BhQtcnKCB>
    {
        IEnumerable<BhQtcnKCB> FindByCondition(Expression<Func<BhQtcnKCB, bool>> predicate);
        IEnumerable<BhQtcnKCBQuery> GetDanhSachQuyetToanNamKCB(int iNamLamViec);
        IEnumerable<BhQtcnKCB> FindByYear(int namLamViec);
        int GetSoChungTuIndexByCondition(int iNamLamViec);
        int LockOrUnLock(Guid id, bool lockStatus);
        bool IsExistChungTuTongHop(int namLamViec);
        void UpdateTotalCPChungTu(string voucherId, string userModify);
        List<DonVi> FindByDonViForNamLamViec(int yearOfWork, int chungTu);
    }
}
