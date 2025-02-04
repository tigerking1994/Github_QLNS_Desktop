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
using VTS.QLNS.CTC.Utility;

namespace VTS.QLNS.CTC.Core.Service.Impl
{
    public class BhKhcKinhphiQuanlyService : IBhKhcKinhphiQuanlyService
    {
        private readonly IBhKhcKinhphiQuanlyRepostiory _repostiory;
        public BhKhcKinhphiQuanlyService(IBhKhcKinhphiQuanlyRepostiory repostiory)
        {
            _repostiory = repostiory;
        }

        public void Add(BhKhcKinhphiQuanly entity)
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
                BhKhcKinhphiQuanly entity = _repostiory.Find(id); ;
                if (entity != null)
                {
                    _repostiory.Delete(entity);
                    // Xóa chi tiết
                }

                transactionScope.Complete();
            }
        }

        public BhKhcKinhphiQuanly FindAggregateVoucher(int yearOfWork)
        {
            return _repostiory.FindAggregateVoucher(yearOfWork);
        }

        public IEnumerable<BhKhcKinhphiQuanly> FindByCondition(Expression<Func<BhKhcKinhphiQuanly, bool>> predicate)
        {
            return _repostiory.FindAll(predicate);
        }

        public List<DonVi> FindByDonViForNamLamViec(int yearOfWork)
        {
            return _repostiory.FindByDonViForNamLamViec(yearOfWork);
        }

        public BhKhcKinhphiQuanly FindById(Guid id)
        {
            return _repostiory.Find(id);
        }

        public IEnumerable<BhKhcKinhphiQuanlyQuery> FindIndex()
        {
            return _repostiory.FindIndex();
        }

        public int GetSoChungTuIndexByCondition(int NamLamViec)
        {
            return _repostiory.GetSoChungTuIndexByCondition(NamLamViec);
        }

        public void LockOrUnlock(Guid id, bool status)
        {
            BhKhcKinhphiQuanly entity = _repostiory.Find(id);
            if (entity != null)
            {
                entity.BIsKhoa = status;
                _repostiory.Update(entity);
            }
        }

        public void Update(BhKhcKinhphiQuanly entity)
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
