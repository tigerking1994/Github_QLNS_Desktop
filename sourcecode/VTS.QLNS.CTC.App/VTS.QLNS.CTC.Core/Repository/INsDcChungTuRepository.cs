using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Criteria;
using VTS.QLNS.CTC.Core.Domain.Query;

namespace VTS.QLNS.CTC.Core.Repository
{
    public interface INsDcChungTuRepository : IRepository<NsDcChungTu>
    {
        IEnumerable<NsDcChungTuQuery> FindByCondition(EstimationVoucherCriteria condition);
        int FindNextSoChungTuIndex(Expression<Func<NsDcChungTu, bool>> predicate);
        int LockOrUnLock(Guid id, bool lockStatus);
        void UpdateAggregateStatus(string voucherIds);
        IEnumerable<NsDcChungTuQuery> FindByCondition(int namLamViec, int loaiChungTu, Guid idNhan, int namNganSach, int loaiNganSach);
        List<string> GetDonViDieuChinh(string iDs, int namLamViec);
    }
}
