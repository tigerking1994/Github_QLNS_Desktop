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
    public class BhQtcQuyKPKService : IBhQtcQuyKPKService
    {
        private readonly IBhQtcQuyKPKRepostiory _repostiory;
        private readonly IBhQtcQuyKPKChiTietRepository _chiTietRepostiory;
        public BhQtcQuyKPKService(IBhQtcQuyKPKRepostiory repostiory, IBhQtcQuyKPKChiTietRepository chiTietRepository)
        {
            _repostiory = repostiory;
            _chiTietRepostiory = chiTietRepository;
        }

        public void Add(BhQtcQuyKPK entity)
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
                BhQtcQuyKPK entity = _repostiory.Find(id); ;
                if (entity != null)
                {
                    _repostiory.Delete(entity);
                    _chiTietRepostiory.DeleteByIdChungTu(entity.Id);
                    // Xóa chi tiết
                }

                transactionScope.Complete();
            }
        }

        public IEnumerable<BhQtcQuyKPK> FindByCondition(Expression<Func<BhQtcQuyKPK, bool>> predicate)
        {
            return _repostiory.FindAll(predicate);
        }

        public IEnumerable<DonVi> FindByDonViForNamLamViec(int namLamViec, int iQuy, int iLoaiChungTu, Guid loaiChiQT)
        {
            return _repostiory.FindByDonViForNamLamViec(namLamViec, iQuy, iLoaiChungTu, loaiChiQT); ;
        }

        public BhQtcQuyKPK FindById(Guid id)
        {
            return _repostiory.Find(id);
        }

        public IEnumerable<BhQtcQuyKPKQuery> FindIndex(int yearOfWork)
        {
            return _repostiory.FindIndex(yearOfWork);
        }

        public int GetSoChungTuIndexByCondition(int namLamViec)
        {
            return _repostiory.GetSoChungTuIndexByCondition(namLamViec);
        }

        public void LockOrUnlock(Guid id, bool status)
        {
            BhQtcQuyKPK entity = _repostiory.Find(id);
            if (entity != null)
            {
                entity.BIsKhoa = status;
                _repostiory.Update(entity);
            }
        }

        public void Update(BhQtcQuyKPK entity)
        {
            using (var transactionScope = new TransactionScope(
                TransactionScopeOption.Required,
                new TransactionOptions { IsolationLevel = IsolationLevel.ReadCommitted }))
            {
                _repostiory.Update(entity);

                transactionScope.Complete();
            }
        }

        public IEnumerable<BhQtcQuyKPK> FindByAggregateVoucher(List<string> voucherNos, int yearOfWork)
        {
            return _repostiory.FindByAggregateVoucher(voucherNos, yearOfWork);
        }
    }
}
