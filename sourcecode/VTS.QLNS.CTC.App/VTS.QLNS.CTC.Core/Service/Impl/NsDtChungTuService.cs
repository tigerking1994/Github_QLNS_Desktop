using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Criteria;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Repository;

namespace VTS.QLNS.CTC.Core.Service.Impl
{
    public class NsDtChungTuService : INsDtChungTuService
    {
        private readonly INsDtChungTuRepository _estimationRepository;

        public NsDtChungTuService(INsDtChungTuRepository estimationRepository)
        {
            _estimationRepository = estimationRepository;
        }

        public NsDtChungTu Add(NsDtChungTu entity)
        {
            _estimationRepository.Add(entity);
            return entity;
        }

        public int Delete(Guid id)
        {
            NsDtChungTu entity = _estimationRepository.Find(id);
            return _estimationRepository.Delete(entity);
        }

        public IEnumerable<NsDtChungTu> FindAll()
        {
            return _estimationRepository.FindAll();
        }

        public NsDtChungTu FindBySoChungTu(string soChungTu, int yearOfWork, int yearOfBudget, int budgetSource)
        {
            return _estimationRepository.FindBySoChungTu(soChungTu, yearOfWork, yearOfBudget, budgetSource);
        }

        public List<NsDtChungTu> FindBySoQuyetDinh(string soQuyetDinh, int yearOfWork, int yearOfBudget, int budgetSource)
        {
            return _estimationRepository.FindBySoQuyetDinh(soQuyetDinh, yearOfWork, yearOfBudget, budgetSource);
        }

        public int FindNextSoChungTuIndex(Expression<Func<NsDtChungTu, bool>> predicate)
        {
            return _estimationRepository.FindNextSoChungTuIndex(predicate);
        }

        public NsDtChungTu FindById(Guid id)
        {
            return _estimationRepository.Find(id);
        }

        public int Update(NsDtChungTu item)
        {
            return _estimationRepository.Update(item);
        }

        public int LockOrUnLock(Guid id, bool lockStatus)
        {
            return _estimationRepository.LockOrUnLock(id, lockStatus);
        }

        public IEnumerable<NsDtChungTu> FindByCondition(Expression<Func<NsDtChungTu, bool>> predicate)
        {
            return _estimationRepository.FindAll().Where(predicate.Compile());
        }
        
        public IEnumerable<NsDtChungTu> FindByYearOfWorker(int yearOfWorker)
        {
            return _estimationRepository.FindByYearOfWorker(yearOfWorker);
        }

        public IEnumerable<NsDtChungTu> FindCond(int yearOfWorker, string idDonVi)
        {
            return _estimationRepository.FindCond(yearOfWorker, idDonVi);
        }

        public Dictionary<string, string> FindAllDict()
        {
            return _estimationRepository.FindAllDict();
        }

        public int AddRange(IEnumerable<NsDtChungTu> entities)
        {
            return _estimationRepository.AddRange(entities);
        }

        public IEnumerable<NsDtChungTu> FindByCondition(EstimationVoucherCriteria condition)
        {
            return _estimationRepository.FindByCondition(condition);
        }

        public IEnumerable<NsDtChungTu> FindByConditionInLuongView(EstimationVoucherCriteria condition)
        {
            return _estimationRepository.FindByConditionInLuongView(condition);
        }

        public void LockOrUnlockMultiple(List<NsDtChungTu> chungTus, bool isLock)
        {
            _estimationRepository.LockOrUnlockMultiple(chungTus, isLock);
        }

        public List<string> FindSoQuyetDinh(int yearOfWork, int yearOfBudget, int budgetSource)
        {
            return _estimationRepository.FindSoQuyetDinh(yearOfWork, yearOfBudget, budgetSource).ToList();
        }

        public IEnumerable<NsDtChungTu> FindDotNhanByChungTuPhanBo(Guid idPhanBo)
        {
            return _estimationRepository.FindDotNhanByChungTuPhanBo(idPhanBo);
        }

        public IEnumerable<NsDtChungTuDotNhanQuery> FindChungTuDotNhan(EstimationVoucherCriteria condition, bool isCreate)
        {
            return _estimationRepository.FindChungTuDotNhan(condition, isCreate);
        }

        public IEnumerable<NsDtChungTuQuery> FindHospitalByCondition(EstimationVoucherCriteria condition)
        {
            return _estimationRepository.FindHospitalByCondition(condition);
        }

        public IEnumerable<NsDtChungTuDotNhanQuery> FindAllChungTuDotNhan(EstimationVoucherCriteria condition)
        {
            return _estimationRepository.FindAllChungTuDotNhan(condition);
        }
        public void BulkInsert(List<NsDtChungTu> lstData)
        {
            _estimationRepository.BulkInsert(lstData);
        }

        public bool CheckByIdAdjVoucher(Guid id)
        {
            return _estimationRepository.CheckByIdAdjVoucher(id);
        }
    }
}
