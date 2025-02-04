using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Criteria;
using VTS.QLNS.CTC.Core.Domain.Query;

namespace VTS.QLNS.CTC.Core.Repository
{
    public interface INsDtChungTuRepository : IRepository<NsDtChungTu>
    {
        IEnumerable<NsDtChungTu> FindByYearOfWorker(int yearOfWorker, string soChungTu);
        IEnumerable<NsDtChungTu> FindByYearOfWorker(int yearOfWorker);
        NsDtChungTu FindBySoChungTu(string soChungTu, int yearOfWork, int yearOfBudget, int budgetSource);
        List<NsDtChungTu> FindBySoQuyetDinh(string soQuyetDinh, int yearOfWork, int yearOfBudget, int budgetSource);
        IEnumerable<NsDtChungTu> FindCond(int yearOfWorker, string idDonVi);
        int LockOrUnLock(Guid id, bool lockStatus);
        int FindNextSoChungTuIndex(Expression<Func<NsDtChungTu, bool>> predicate);
        Dictionary<string, string> FindAllDict();
        IEnumerable<NsDtChungTu> FindByCondition(EstimationVoucherCriteria condition);
        IEnumerable<NsDtChungTu> FindByConditionInLuongView(EstimationVoucherCriteria condition);
        IEnumerable<NsDtChungTuQuery> FindHospitalByCondition(EstimationVoucherCriteria condition);
        void LockOrUnlockMultiple(List<NsDtChungTu> chungTus, bool isLock);
        IEnumerable<string> FindSoQuyetDinh(int yearOfWork, int yearOfBudget, int budgetSource);
        IEnumerable<NsDtChungTu> FindDotNhanByChungTuPhanBo(Guid idPhanBo);
        IEnumerable<NsDtChungTuDotNhanQuery> FindChungTuDotNhan(EstimationVoucherCriteria condition, bool isCreate);
        IEnumerable<NsDtChungTuDotNhanQuery> FindAllChungTuDotNhan(EstimationVoucherCriteria condition);
        bool CheckByIdAdjVoucher(Guid id);
    }
}
