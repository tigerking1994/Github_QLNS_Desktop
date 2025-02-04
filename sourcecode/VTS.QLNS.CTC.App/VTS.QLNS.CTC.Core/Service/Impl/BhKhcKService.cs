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
    public class BhKhcKService : IBhKhcKService
    {
        private readonly IBhKhcKRepostiory _repostiory;
        public BhKhcKService(IBhKhcKRepostiory repostiory)
        {
            _repostiory = repostiory;
        }

        public void Add(BhKhcK entity)
        {
            using (var transactionScope = new TransactionScope(
               TransactionScopeOption.Required,
               new TransactionOptions { IsolationLevel = IsolationLevel.ReadCommitted }))
            {
                if (entity != null)
                {
                    _repostiory.Add(entity);
                }

                transactionScope.Complete();
            }
        }

        public void Delete(Guid id)
        {
            using (var transactionScope = new TransactionScope(
               TransactionScopeOption.Required,
               new TransactionOptions { IsolationLevel = IsolationLevel.ReadCommitted }))
            {
                BhKhcK entity = _repostiory.Find(id); ;
                if (entity != null)
                {
                    _repostiory.Delete(entity);
                    // Xóa chi tiết
                }

                transactionScope.Complete();
            }
        }

        public List<BhKhcK>  FindAggregateVoucher(int yearOfWork)
        {
            return _repostiory.FindAggregateVoucher(yearOfWork);
        }

        public IEnumerable<BhKhcK> FindByCondition(Expression<Func<BhKhcK, bool>> predicate)
        {
            return _repostiory.FindAll(predicate);
        }

        public List<DonVi> FindByDonViForNamLamViec(int yearOfWork, Guid IdLoaiChi)
        {
            return _repostiory.FindByDonViForNamLamViec(yearOfWork, IdLoaiChi);
        }

        public BhKhcK FindById(Guid id)
        {
            return _repostiory.Find(id);
        }

        public IEnumerable<BhKhcKQuery> FindIndex()
        {
            return _repostiory.FindIndex();
        }
        public int GetSoChungTuIndexByCondition(int yearOfWork)
        {
            return _repostiory.GetSoChungTuIndexByCondition(yearOfWork);
        }


        public void LockOrUnlock(Guid id, bool status)
        {
            BhKhcK entity = _repostiory.Find(id);
            if (entity != null)
            {
                entity.BIsKhoa = status;
                _repostiory.Update(entity);
            }
        }


        public void Update(BhKhcK entity)
        {
            using (var transactionScope = new TransactionScope(
                TransactionScopeOption.Required,
                new TransactionOptions { IsolationLevel = IsolationLevel.ReadCommitted }))
            {
                _repostiory.Update(entity);

                transactionScope.Complete();
            }
        }
    }
}
