using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Domain.Criteria;

namespace VTS.QLNS.CTC.Core.Service
{
    public interface ISktNganhThamDinhService
    {
        NsSktNganhThamDinh Add(NsSktNganhThamDinh entity);
        NsSktNganhThamDinh Find(params object[] keyValues);
        int Update(NsSktNganhThamDinh entity);
        int Delete(Guid id);
        public IEnumerable<NsSktNganhThamDinh> FindByCondition(Expression<Func<NsSktNganhThamDinh, bool>> predicate);
        IEnumerable<ThDChungTuQuery> FindByNamLamViec(int namLamViec, int namNganSach, int nguonNganSach, string userName, int loai, int loaiNganSach);
        int GetSoChungTuIndexByCondition(int namLamViec);
        bool CheckExitsByChungTuId(Guid chungtuId);
        int UpdateStatusDisable(Guid id);
        int LockOrUnLock(Guid id, bool lockStatus);
        void CreateVoucherAggregate(string voucherId, string maDonVi, string tenDonVi, int namLamViec, string userCreate, int namNganSach, int nguonNganSach);
    }
}
