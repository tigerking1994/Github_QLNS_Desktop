using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Criteria;
using VTS.QLNS.CTC.Core.Domain.Query;

namespace VTS.QLNS.CTC.Core.Service
{
    public interface INsDcChungTuService
    {
        NsDcChungTu Add(NsDcChungTu entity);
        int Delete(Guid Id);
        IEnumerable<NsDcChungTuQuery> FindByCondition(EstimationVoucherCriteria condition);
        IEnumerable<NsDcChungTu> FindByCondition(Expression<Func<NsDcChungTu, bool>> predicate);
        NsDcChungTu FindById(Guid Id);
        int FindNextSoChungTuIndex(Expression<Func<NsDcChungTu, bool>> predicate);
        int LockOrUnLock(Guid id, bool lockStatus);
        int Update(NsDcChungTu item);
        void UpdateAggregateStatus(string voucherIds);
        void BulkInsert(List<NsDcChungTu> lstData);
        IEnumerable<NsDcChungTuQuery> FindByCondition(int namLamViec, int loaiChungTu, Guid idNhan, int namNganSach, int loaiNganSach);
        List<string> GetDonViDieuChinh(string iDs, int namLamViec);
        void UpdateRange(List<NsDcChungTu> listChungTu);
    }
}
