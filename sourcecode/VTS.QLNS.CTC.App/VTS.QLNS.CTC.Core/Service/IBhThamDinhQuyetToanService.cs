using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;

namespace VTS.QLNS.CTC.Core.Service
{
    public interface IBhThamDinhQuyetToanService
    {
        IEnumerable<BhThamDinhQuyetToan> FindAll();
        IEnumerable<BhThamDinhQuyetToanQuery> FindAll(int yearOfWork);
        int Add(BhThamDinhQuyetToan t);
        int Update(BhThamDinhQuyetToan t);
        int Delete(BhThamDinhQuyetToan t);
        BhThamDinhQuyetToan Find(Guid t);
        IEnumerable<BhThamDinhQuyetToan> FindAll(Expression<Func<BhThamDinhQuyetToan, bool>> predicate);
        void UpdateTotalChungTu(string voucherId, string userModify);
        string GetSoChungTuIndexByCondition(int namLamViec, int nguonNganSach, int namNganSach);
        IEnumerable<BhThamDinhQuyetToan> FindUnitVoucher(int yearOfWork);
        IEnumerable<BhThamDinhQuyetToan> FindUnitAggregateVoucher(int yearOfWork);
    }
}
