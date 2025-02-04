using System;
using System.Collections.Generic;
using System.Text;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Domain.Criteria;
namespace VTS.QLNS.CTC.Core.Repository
{
    public interface ISktNganhThamDinhRepository : IRepository<NsSktNganhThamDinh>
    {
        IEnumerable<ThDChungTuQuery> FindByNamLamViec(int namLamViec, int namNganSach, int nguonNganSach, string userName, int loai, int loaiNganSach);
        int GetSoChungTuIndexByCondition(int namLamViec);
        bool CheckExitsByChungTuId(Guid chungtuId);
        int LockOrUnLock(Guid id, bool lockStatus);
        void CreateVoucherAggregate(string voucherId, string maDonVi, string tenDonVi, int namLamViec, string userCreate, int namNganSach, int nguonNganSach);
    }
}
