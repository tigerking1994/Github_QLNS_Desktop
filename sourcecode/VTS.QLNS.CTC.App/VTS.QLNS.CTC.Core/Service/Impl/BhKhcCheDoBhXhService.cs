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
using VTS.QLNS.CTC.Core.Repository.Impl;

namespace VTS.QLNS.CTC.Core.Service.Impl
{
    public class BhKhcCheDoBhXhService : IBhKhcCheDoBhXhService
    {
        private readonly IBhKhcCheDoBhXhRepository _repository;
        public BhKhcCheDoBhXhService(IBhKhcCheDoBhXhRepository repository)
        {
            _repository = repository;
        }

        public void Add(BhKhcCheDoBhXh entity)
        {
            using (var transactionScope = new TransactionScope(
               TransactionScopeOption.Required,
               new TransactionOptions { IsolationLevel = IsolationLevel.ReadCommitted }))
            {
                _repository.Add(entity);

                transactionScope.Complete();
            }
        }

        public void Update(BhKhcCheDoBhXh entity)
        {
            using (var transactionScope = new TransactionScope(
                TransactionScopeOption.Required,
                new TransactionOptions { IsolationLevel = IsolationLevel.ReadCommitted }))
            {
                _repository.Update(entity);

                transactionScope.Complete();
            }
        }

        public void Delete(Guid id)
        {
            using (var transactionScope = new TransactionScope(
                TransactionScopeOption.Required,
                new TransactionOptions { IsolationLevel = IsolationLevel.ReadCommitted }))
            {
                BhKhcCheDoBhXh entity = _repository.Find(id); ;
                if (entity != null)
                {
                    _repository.Delete(entity);
                    // Xóa chi tiết
                }

                transactionScope.Complete();
            }
        }

        public void LockOrUnlock(Guid id, bool status)
        {
            BhKhcCheDoBhXh entity = _repository.Find(id);
            if (entity != null)
            {
                entity.BIsKhoa = status;
                _repository.Update(entity);
            }
        }
        public IEnumerable<BhKhcCheDoBhXhQuery> FindIndex()
        {
            return _repository.FindIndex();
        }

        public BhKhcCheDoBhXh FindById(Guid id)
        {
            return _repository.Find(id);
        }

        public int GetSoChungTuIndexByCondition(int iNamLamViec)
        {
            return _repository.GetSoChungTuIndexByCondition(iNamLamViec);
        }

        public bool IsExistChungTuTongHop(int loaiTongHop, int namLamViec)
        {
            return _repository.IsExistChungTuTongHop(loaiTongHop, namLamViec);
        }

        public IEnumerable<BhKhcCheDoBhXh> FindByCondition(Expression<Func<BhKhcCheDoBhXh, bool>> predicate)
        {
            return _repository.FindAll(predicate);
        }

        public List<DonVi> FindByDonViForNamLamViec(int namLamViec)
        {
            return _repository.FindByDonViForNamLamViec(namLamViec);
        }

        public BhKhcCheDoBhXh FindAggregateVoucher(int yearOfWork)
        {
            return _repository.FindAggregateVoucher(yearOfWork);
        }
    }
}
