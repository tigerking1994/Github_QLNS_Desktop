using System.Collections.Generic;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;

namespace VTS.QLNS.CTC.Core.Repository
{
    public interface IBhThamDinhQuyetToanRepository : IRepository<BhThamDinhQuyetToan>
    {
        IEnumerable<BhThamDinhQuyetToanQuery> FindAll(int yearOfWork);
        void UpdateTotalChungTu(string voucherId, string userModify);
        string GetSoChungTuIndexByCondition(int namLamViec, int nguonNganSach, int namNganSach);
        IEnumerable<BhThamDinhQuyetToan> FindUnitVoucher(int yearOfWork);
        IEnumerable<BhThamDinhQuyetToan> FindUnitAggregateVoucher(int yearOfWork);
    }
}
