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
    public class BhQtcQuyKinhPhiQuanLyService : IBhQtcQuyKinhPhiQuanLyService
    {
        private readonly IBhQtcQuyKinhPhiQuanLyRepostiory _repostiory;
        public BhQtcQuyKinhPhiQuanLyService(IBhQtcQuyKinhPhiQuanLyRepostiory repostiory)
        {
            _repostiory = repostiory;
        }

        public void Add(BhQtcQuyKinhPhiQuanLy entity)
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
                BhQtcQuyKinhPhiQuanLy entity = _repostiory.Find(id); ;
                if (entity != null)
                {
                    _repostiory.Delete(entity);
                    // Xóa chi tiết
                }

                transactionScope.Complete();
            }
        }

        public IEnumerable<BhQtcQuyKinhPhiQuanLy> FindByCondition(Expression<Func<BhQtcQuyKinhPhiQuanLy, bool>> predicate)
        {
            return _repostiory.FindAll(predicate);
        }

        public BhQtcQuyKinhPhiQuanLy FindById(Guid id)
        {
            return _repostiory.Find(id);
        }

        public IEnumerable<BhQtcQuyKinhPhiQuanLyQuery> FindIndex(int iNamChungTu)
        {
            return _repostiory.FindIndex(iNamChungTu);
        }

        public int GetSoChungTuIndexByCondition(int namLamViec)
        {
            return _repostiory.GetSoChungTuIndexByCondition(namLamViec);
        }

        public IEnumerable<DonVi> FindByDonViForNamLamViec(int namLamViec, int iQuy, int iLoaiChungTu)
        {
            return _repostiory.FindByDonViForNamLamViec(namLamViec, iQuy, iLoaiChungTu);
        }

        public void LockOrUnlock(Guid id, bool status)
        {
            BhQtcQuyKinhPhiQuanLy entity = _repostiory.Find(id);
            if (entity != null)
            {
                entity.BIsKhoa = status;
                _repostiory.Update(entity);
            }
        }

        public void Update(BhQtcQuyKinhPhiQuanLy entity)
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
