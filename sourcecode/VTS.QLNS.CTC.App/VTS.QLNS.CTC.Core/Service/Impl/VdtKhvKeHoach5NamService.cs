using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Transactions;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Criteria;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Repository;
using VTS.QLNS.CTC.Utility;

namespace VTS.QLNS.CTC.Core.Service.Impl
{
    public class VdtKhvKeHoach5NamService : IVdtKhvKeHoach5NamService
    {
        private readonly IVdtKhvKeHoach5NamRepository _repository;
        private readonly IVdtKhvKeHoach5NamChiTietRepository _chitietRepository;

        public VdtKhvKeHoach5NamService(IVdtKhvKeHoach5NamRepository repository,
            IVdtKhvKeHoach5NamChiTietRepository chitietRepository)
        {
            _repository = repository;
            _chitietRepository = chitietRepository;
        }

        public VdtKhvKeHoach5Nam Add(VdtKhvKeHoach5Nam entity)
        {
            _repository.Add(entity);
            return entity;
        }

        public int Adjust(VdtKhvKeHoach5Nam entity)
        {
            using (var transactionScope = new TransactionScope(
                TransactionScopeOption.Required,
                new TransactionOptions { IsolationLevel = IsolationLevel.ReadCommitted }))
            {
                try
                {
                    // Thêm kế hoạch trung hạn điều chỉnh, clone từ kế hoạch trung hạn gốc
                    _repository.Add(entity);

                    VdtKhvKeHoach5Nam parentEntity = _repository.Find(entity.IIdParentId);
                    if (parentEntity != null)
                    {
                        parentEntity.BActive = false;
                    }
                    _repository.Update(parentEntity);

                    MidiumTermPlanCriteria dataCreation = new MidiumTermPlanCriteria();
                    dataCreation.VocherIDF = entity.IIdParentId.ToString();
                    dataCreation.VocherIDL = entity.Id.ToString();
                    _chitietRepository.CreateSettlementVoucherDetail(dataCreation);

                    transactionScope.Complete();
                    return DBContextSaveChangeState.SUCCESS;
                }
                catch (Exception)
                {
                    return DBContextSaveChangeState.ERROR;
                }
            }
        }

        public int Delete(Guid id)
        {
            VdtKhvKeHoach5Nam entity = _repository.Find(id);
            return _repository.Delete(entity);
        }

        public IEnumerable<VdtKhvKeHoach5Nam> FindByCondition(Expression<Func<VdtKhvKeHoach5Nam, bool>> predicate)
        {
            return _repository.FindAll(predicate);
        }

        public IEnumerable<VdtKhvKeHoach5Nam> FindByDonViQuanLy(MediumTermPlanIndexSearch condition)
        {
            return _repository.FindByDonViQuanLy(condition);
        }

        public VdtKhvKeHoach5Nam FindById(Guid id)
        {
            return _repository.Find(id);
        }

        public IEnumerable<VdtKhvKeHoach5Nam> FindByIdDonViParent(string idDonVi, int type, int iGiaiDoanTu, int iGiaiDoanDen)
        {
            return _repository.FindByIdDonViParent(idDonVi, type, iGiaiDoanTu, iGiaiDoanDen);
        }

        public IEnumerable<VdtKhvKeHoach5NamQuery> FindConditionIndex(int yearOfWork)
        {
            return _repository.FindConditionIndex(yearOfWork);
        }

        public void LockOrUnlock(Guid id, bool isLock)
        {
            VdtKhvKeHoach5Nam chungTu = _repository.Find(id);
            chungTu.BKhoa = isLock;
            _repository.Update(chungTu);
        }

        public int Update(VdtKhvKeHoach5Nam item)
        {
            return _repository.Update(item);
        }

        public Guid? FindIdKHTHByID(Guid? id)
        {
            return _repository.FindIdKHTHByID(id);
        }

        public bool IsExistSoQuyetDinh(string soQuyetDinh, Guid id)
        {
            return _repository.IsExistSoQuyetDinh(soQuyetDinh, id);
        }

        public IEnumerable<VdtKhvKeHoach5NamQuery> FindAllDuocDuyet()
        {
            return _repository.FindAllDuocDuyet();
        }
    }
}
