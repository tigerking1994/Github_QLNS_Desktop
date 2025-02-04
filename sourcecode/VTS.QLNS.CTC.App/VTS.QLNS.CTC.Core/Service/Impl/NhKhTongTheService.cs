using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Transactions;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Repository;

namespace VTS.QLNS.CTC.Core.Service.Impl
{
    public class NhKhTongTheService : INhKhTongTheService
    {
        private readonly INhKhTongTheRepository _repository;
        private readonly INhKhTongTheNhiemVuChiRepository _khTongTheNhiemVuChiRepository;
        private readonly INhDmNhiemVuChiRepository _dmNhiemVuChiRepository;

        public NhKhTongTheService
        (
            INhKhTongTheRepository nhKhTongTheRepository,
            INhKhTongTheNhiemVuChiRepository khTongTheNhiemVuChiRepository,
            INhDmNhiemVuChiRepository dmNhiemVuChiRepository
        )
        {
            _repository = nhKhTongTheRepository;
            _khTongTheNhiemVuChiRepository = khTongTheNhiemVuChiRepository;
            _dmNhiemVuChiRepository = dmNhiemVuChiRepository;
        }

        public List<NhKhTongThe> FindAll() => (List<NhKhTongThe>)_repository.FindAll();

        public NhKhTongThe FindByPredicate(Guid id)
        {
            Expression<Func<NhKhTongThe, bool>> predicate = c => c.Id.Equals(id);
            return _repository.FindByPredicate(predicate);
        }

        public NhKhTongThe Find(Guid id) => _repository.Find(id);

        public int Add(NhKhTongThe nhKhTongThe) => _repository.Add(nhKhTongThe);

        public int Update(NhKhTongThe nhKhTongThe) => _repository.Update(nhKhTongThe);

        public int Delete(Guid id) => _repository.Delete(id);

        public IEnumerable<NhKhTongTheQuery> FindAllOverview() => _repository.FindAllOverview();

        public IEnumerable<NhKhTongThe> FindAllOrdered() => _repository.FindAllOrdered();

        public void LockOrUnLock(Guid id, bool isActivated)
        {
            NhKhTongThe nhKhTongThe = Find(id);
            nhKhTongThe.BIsKhoa = isActivated;
            _repository.Update(nhKhTongThe);
        }

        public IEnumerable<NhKhTongThe> FindAll(Expression<Func<NhKhTongThe, bool>> predicate)
        {
            return _repository.FindAll(predicate);
        }

        public bool IsExistKhTongTheNam(Guid idParent, int iNamKeHoach)
        {
            var khTongTheNam = _repository.FindByParentIdAndNamKeHoach(idParent, iNamKeHoach);
            return khTongTheNam != null;
        }

        public IEnumerable<NhKhTongThe> FindByParentId(Guid idParent)
        {
            return _repository.FindByParentId(idParent);
        }
        public IEnumerable<NhKhTongThe> FindByDonViId(Guid idDonVi)
        {
            return _repository.FindByDonViId(idDonVi);
        }

        public IEnumerable<NhKhTongTheNhiemVuChi> FindKHTongTheNVCByConditon(Expression<Func<NhKhTongTheNhiemVuChi, bool>> predicate)
        {
            return _khTongTheNhiemVuChiRepository.FindAll(predicate);
        }

        public void Add(NhKhTongThe nhKhTongThe, IEnumerable<NhKhTongTheNhiemVuChi> khTongTheNhiemVuChis)
        {
            using (var transactionScope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = IsolationLevel.ReadCommitted }))
            {
                _repository.Add(nhKhTongThe);
                _khTongTheNhiemVuChiRepository.AddOrUpdate(nhKhTongThe.Id, khTongTheNhiemVuChis);
                transactionScope.Complete();
            }
        }

        public void Update(NhKhTongThe nhKhTongThe, IEnumerable<NhKhTongTheNhiemVuChi> khTongTheNhiemVuChis)
        {
            using (var transactionScope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = IsolationLevel.ReadCommitted }))
            {
                _repository.Update(nhKhTongThe);
                _khTongTheNhiemVuChiRepository.AddOrUpdate(nhKhTongThe.Id, khTongTheNhiemVuChis);
                transactionScope.Complete();
            }
        }

        public void Adjust(NhKhTongThe nhKhTongThe, IEnumerable<NhKhTongTheNhiemVuChi> khTongTheNhiemVuChis)
        {
            using (var transactionScope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = IsolationLevel.ReadCommitted }))
            {
                _repository.Add(nhKhTongThe);
                // Update BActive = false của KhTongThe gốc
                NhKhTongThe parentEntity = _repository.Find(nhKhTongThe.IIdParentAdjustId);
                if (parentEntity != null)
                {
                    parentEntity.BIsActive = false;
                }
                _repository.Update(parentEntity);

                // Update KHTH Nhiệm vụ chi
                _khTongTheNhiemVuChiRepository.AddOrUpdate(nhKhTongThe.Id, khTongTheNhiemVuChis);
                transactionScope.Complete();
            }
        }
    }
}
