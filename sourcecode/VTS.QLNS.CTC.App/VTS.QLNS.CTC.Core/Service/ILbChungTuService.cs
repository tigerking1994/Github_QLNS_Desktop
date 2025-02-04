using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;

namespace VTS.QLNS.CTC.Core.Service
{
    public interface ILbChungTuService
    {
        IEnumerable<LbChungTuCanCuDuToanDataQuery> GetCanCuDuToanData(int namLamViec, string idChungTu, int loaiChungTu, string idDonVi);
        IEnumerable<LbChungTuCanCuQuery> FindByCondition(int namLamViec, Guid idChungTu, string idDonVi);
        IEnumerable<LbChungTuQuery> FindByCondition(int namLamViec, int nguonNganSach, string donviUserId, int namNganSach, string userName);
        IEnumerable<NsNganhChungTu> FindByCondition(Expression<Func<NsNganhChungTu, bool>> predicate);
        NsNganhChungTu Add(NsNganhChungTu entity);
        int Delete(Guid id);
        int UpdateRange(IEnumerable<NsNganhChungTu> entities);
        NsNganhChungTu FindById(Guid id);
        int LockOrUnLock(Guid id, bool lockStatus);
        int Update(NsNganhChungTu entity);
        int GetSoChungTuIndexByCondition(int namLamViec, int nguonNganSach);
        void UpdateTotalLbChungTu(string voucherId, string userModify);
        int UpdateStatusDisable(Guid id);
    }
}
