using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Repository;

namespace VTS.QLNS.CTC.Core.Service.Impl
{
    public class NhTtThanhToanService : IService<NhTtThanhToan>, INhTtThanhToanService
    {
        private readonly INhTtThanhToanRepository _repository;
        private readonly INhTtThanhToanChiTietRepository _iNhTtThanhToanChiTietRepository;

        public NhTtThanhToanService(INhTtThanhToanRepository repository,
            INhTtThanhToanChiTietRepository iNhTtThanhToanChiTietRepository)
        {
            _repository = repository;
            _iNhTtThanhToanChiTietRepository = iNhTtThanhToanChiTietRepository;
        }

        public void Add(NhTtThanhToan nhTtThanhToan)
        {
            using (var transactionScope = new TransactionScope(
                TransactionScopeOption.Required,
                new TransactionOptions { IsolationLevel = IsolationLevel.ReadCommitted }))
            {
                _repository.Add(nhTtThanhToan);
                _iNhTtThanhToanChiTietRepository.AddOrUpdate(nhTtThanhToan.Id, nhTtThanhToan.NhTtThanhToanChiTiets);

                transactionScope.Complete();
            }
        }

        public void Update(NhTtThanhToan nhTtThanhToan)
        {
            using (var transactionScope = new TransactionScope(
                TransactionScopeOption.Required,
                new TransactionOptions { IsolationLevel = IsolationLevel.ReadCommitted }))
            {
                _repository.Update(nhTtThanhToan);
                _iNhTtThanhToanChiTietRepository.AddOrUpdate(nhTtThanhToan.Id, nhTtThanhToan.NhTtThanhToanChiTiets);

                transactionScope.Complete();
            }
        }

        public void Delete(NhTtThanhToan entity)
        {
            using (var transactionScope = new TransactionScope(
                TransactionScopeOption.Required,
                new TransactionOptions { IsolationLevel = IsolationLevel.ReadCommitted }))
            {
                entity = _repository.Find(entity.Id);
                if (entity != null)
                {
                    _repository.Delete(entity);
                    _iNhTtThanhToanChiTietRepository.DeleteByDeNghiThanhToan(entity.Id);
                    _repository.RemoveParentIdOfChildren(entity.Id);
                }

                transactionScope.Complete();
            }
        }

        public void Delete(Guid id)
        {
            _repository.Delete(id);
        }

        public NhTtThanhToan FindById(Guid id)
        {
            return _repository.Find(id);
        }


        public IEnumerable<NhTtThanhToan> FindByCondition(Expression<Func<NhTtThanhToan, bool>> predicate)
        {
            return _repository.FindAll(predicate);
        }

        public IEnumerable<NhTtThanhToanQuery> FindIndex(int yearOfWork, int iTrangThai, bool bIsDeNghi)
        {
            return _repository.FindIndex(yearOfWork, iTrangThai, bIsDeNghi);
        }

        public void LockOrUnlock(Guid id, bool status)
        {
            NhTtThanhToan entity = _repository.Find(id);
            if (entity != null)
            {
                entity.BIsKhoa = status;
                _repository.Update(entity);
            }
        }

        public void TongHopDeNghiThanhToan(NhTtThanhToan nhTtDeNghiThanhToan, List<Guid> voucherAgregatesIds)
        {
            _repository.TongHopDeNghiThanhToan(nhTtDeNghiThanhToan, voucherAgregatesIds);
        }

        public List<NhTtThanhToan> FindDeNghiTongHop()
        {
            return _repository.FindDeNghiTongHop();
        }
    }
}
