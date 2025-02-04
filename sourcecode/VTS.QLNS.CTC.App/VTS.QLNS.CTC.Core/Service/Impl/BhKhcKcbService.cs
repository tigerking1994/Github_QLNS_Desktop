using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Repository;

namespace VTS.QLNS.CTC.Core.Service.Impl
{
    public class BhKhcKcbService : IBhKhcKcbService
    {
        private readonly IBhKhcKcbRepostiory _repostiory;
        public BhKhcKcbService(IBhKhcKcbRepostiory repostiory)
        {
            _repostiory = repostiory;
        }


        public void Add(BhKhcKcb entity)
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
                BhKhcKcb entity = _repostiory.Find(id); ;
                if (entity != null)
                {
                    _repostiory.Delete(entity);
                    // Xóa chi tiết
                }

                transactionScope.Complete();
            }
        }

        public BhKhcKcb FindAggregateVoucher(int yearOfWork)
        {
            return _repostiory.FindAggregateVoucher(yearOfWork);
        }

        public IEnumerable<BhKhcKcb> FindByCondition(Expression<Func<BhKhcKcb, bool>> predicate)
        {
            return _repostiory.FindAll(predicate);
        }

        public List<DonVi> FindByDonViForNamLamViec(int yearOfWork)
        {
            return _repostiory.FindByDonViForNamLamViec(yearOfWork);
        }

        public BhKhcKcb FindById(Guid id)
        {
            return _repostiory.Find(id);
        }

        public IEnumerable<BhKhcKcbQuery> FindIndex()
        {
            return _repostiory.FindIndex();
        }
        public int GetSoChungTuIndexByCondition(int yearOfWork)
        {
            return _repostiory.GetSoChungTuIndexByCondition(yearOfWork);
        }


        public void LockOrUnlock(Guid id, bool status)
        {
            BhKhcKcb entity = _repostiory.Find(id);
            if (entity != null)
            {
                entity.BIsKhoa = status;
                _repostiory.Update(entity);
            }
        }


        public void Update(BhKhcKcb entity)
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
