using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Criteria;
using VTS.QLNS.CTC.Core.Domain.Query;

namespace VTS.QLNS.CTC.Core.Service
{
    public interface INsDtChungTuService
    {
        IEnumerable<NsDtChungTu> FindAll();
        IEnumerable<NsDtChungTu> FindByCondition(Expression<Func<NsDtChungTu, bool>> predicate);
        IEnumerable<NsDtChungTu> FindByCondition(EstimationVoucherCriteria condition);
        IEnumerable<NsDtChungTu> FindByConditionInLuongView(EstimationVoucherCriteria condition);
        IEnumerable<NsDtChungTuQuery> FindHospitalByCondition(EstimationVoucherCriteria condition);
        IEnumerable<NsDtChungTu> FindByYearOfWorker(int yearOfWorker);
        NsDtChungTu FindBySoChungTu(string soChungTu, int yearOfWork, int yearOfBudget, int budgetSource);
        List<NsDtChungTu> FindBySoQuyetDinh(string soQuyetDinh, int yearOfWork, int yearOfBudget, int budgetSource);
        IEnumerable<NsDtChungTu> FindCond(int yearOfWorker, string idDonVi);
        NsDtChungTu FindById(Guid Id);
        int FindNextSoChungTuIndex(Expression<Func<NsDtChungTu, bool>> predicate);
        NsDtChungTu Add(NsDtChungTu entity);
        int Delete(Guid Id);
        int Update(NsDtChungTu item);
        int LockOrUnLock(Guid id, bool lockStatus);
        Dictionary<string, string> FindAllDict();
        int AddRange(IEnumerable<NsDtChungTu> entities);
        void LockOrUnlockMultiple(List<NsDtChungTu> chungTus, bool isLock);
        List<string> FindSoQuyetDinh(int yearOfWork, int yearOfBudget, int budgetSource);
        IEnumerable<NsDtChungTu> FindDotNhanByChungTuPhanBo(Guid idPhanBo);
        IEnumerable<NsDtChungTuDotNhanQuery> FindChungTuDotNhan(EstimationVoucherCriteria condition, bool isCreate);
        IEnumerable<NsDtChungTuDotNhanQuery> FindAllChungTuDotNhan(EstimationVoucherCriteria condition);
        void BulkInsert(List<NsDtChungTu> lstData);
        bool CheckByIdAdjVoucher(Guid id);
    }
}
