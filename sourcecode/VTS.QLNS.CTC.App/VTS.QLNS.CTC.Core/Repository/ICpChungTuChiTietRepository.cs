using System;
using System.Collections.Generic;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Criteria;
using VTS.QLNS.CTC.Core.Domain.Query;

namespace VTS.QLNS.CTC.Core.Repository
{
    public interface ICpChungTuChiTietRepository : IRepository<NsCpChungTuChiTiet>
    {
        IEnumerable<CpChungTuChiTietQuery> FindChungTuChiTietByCondition(AllocationDetailCriteria searchCondition, bool bQueryAll);
        IEnumerable<CpChungTuChiTietQuery> FindChungTuChiTietByConditionForExport(AllocationDetailCriteria searchCondition);
        bool CheckExitsByChungTuId(Guid chungtuId);
        IEnumerable<CpChungTuChiTietDuToanQuery> FindChungTuChiTietDuToanByCondition(AllocationDetailCriteria searchCondition);
        IEnumerable<CpChungTuChiTietDaCapQuery> FindChungTuChiTietDaCapByCondition(AllocationDetailCriteria searchCondition);
        void DeleteByVoucherId(Guid voucherId);
        void CreateVoudcherSummary(string idChungTu, string idDonVi, string nguoiTao, int namLamViec, int namNganSach, int nguonNganSach, string idChungTuSummary);
        IEnumerable<CpChungTuChiTietQuery> FindChungTuChiTietByConditionSummary(AllocationDetailCriteria searchCondition);
    }
}
