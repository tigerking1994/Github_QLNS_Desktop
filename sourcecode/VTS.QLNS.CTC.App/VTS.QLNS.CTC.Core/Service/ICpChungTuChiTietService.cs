using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Criteria;
using VTS.QLNS.CTC.Core.Domain.Query;

namespace VTS.QLNS.CTC.Core.Service
{
    public interface ICpChungTuChiTietService
    {
        IEnumerable<NsCpChungTuChiTiet> FindAll();
        NsCpChungTuChiTiet Add(NsCpChungTuChiTiet entity);
        IEnumerable<CpChungTuChiTietQuery> FindChungTuChiTietByCondition(AllocationDetailCriteria searchCondition, bool bQueryAll);
        IEnumerable<CpChungTuChiTietDuToanQuery> FindChungTuChiTietDuToanByCondition(AllocationDetailCriteria searchCondition);
        IEnumerable<CpChungTuChiTietDaCapQuery> FindChungTuChiTietDaCapByCondition(AllocationDetailCriteria searchCondition);
        NsCpChungTuChiTiet Find(params object[] keyValues);
        int Update(NsCpChungTuChiTiet entity);
        int AddRange(IEnumerable<NsCpChungTuChiTiet> entities);
        int Delete(Guid id);
        bool CheckExitsByChungTuId(Guid chungtuId);
        IEnumerable<CpChungTuChiTietQuery> FindChungTuChiTietByConditionForExport(AllocationDetailCriteria searchCondition);
        public IEnumerable<NsCpChungTuChiTiet> FindByCondition(Expression<Func<NsCpChungTuChiTiet, bool>> predicate);
        void DeleteByVoucherId(Guid voucherId);
        void CreateVoudcherSummary(string idChungTu, string idDonVi, string nguoiTao, int namLamViec, int namNganSach, int nguonNganSach, string idChungTuSummary);
        int RemoveRange(IEnumerable<NsCpChungTuChiTiet> entities);
        IEnumerable<CpChungTuChiTietQuery> FindChungTuChiTietByConditionSummary(AllocationDetailCriteria searchCondition);
    }
}
