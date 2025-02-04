using System;
using System.Collections.Generic;
using System.Text;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;

namespace VTS.QLNS.CTC.Core.Repository
{
    public interface ILbChungTuRepository : IRepository<NsNganhChungTu>
    {
        IEnumerable<LbChungTuCanCuDuToanDataQuery> GetCanCuDuToanData(int namLamViec, string idChungTu, int loaiChungTu, string idDonVi);
        IEnumerable<LbChungTuCanCuQuery> FindByCondition(int namLamViec, Guid idChungTu, string idDonVi);
        IEnumerable<LbChungTuQuery> FindByCondition(int namLamViec, int nguonNganSach, string donviUserId, int namNganSach, string userName);
        int GetSoChungTuIndexByCondition(int namLamViec, int nguonNganSach);
        int LockOrUnLock(Guid id, bool lockStatus);
        void UpdateTotalLbChungTu(string voucherId, string userModify);
    }
}