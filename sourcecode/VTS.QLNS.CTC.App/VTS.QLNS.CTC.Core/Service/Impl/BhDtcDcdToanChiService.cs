using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Criteria;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Repository;

namespace VTS.QLNS.CTC.Core.Service.Impl
{
    public class BhDtcDcdToanChiService : IBhDtcDcdToanChiService
    {
        private readonly IBhDtcDcdToanChiRepository _repostiory;

        public BhDtcDcdToanChiService(IBhDtcDcdToanChiRepository repostiory)
        {
            _repostiory = repostiory;
        }
        public void Add(BhDtcDcdToanChi entity)
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
                BhDtcDcdToanChi entity = _repostiory.Find(id); ;
                if (entity != null)
                {
                    _repostiory.Delete(entity);
                    // Xóa chi tiết
                }

                transactionScope.Complete();
            }
        }

        public IEnumerable<BhDtcDcdToanChi> FindByCondition(Expression<Func<BhDtcDcdToanChi, bool>> predicate)
        {
            return _repostiory.FindAll(predicate);
        }

        public List<DonVi> FindByDonViForNamLamViec(int yearOfWork, Guid iDLoaiChi)
        {
            return _repostiory.FindByDonViForNamLamViec(yearOfWork, iDLoaiChi);
        }

        public BhDtcDcdToanChi FindById(Guid id)
        {
            return _repostiory.Find(id);
        }

        public IEnumerable<BhDtcDcdToanChiQuery> FindIndex( int yearOfWork)
        {
            return _repostiory.FindIndex(yearOfWork);
        }

        public int GetSoChungTuIndexByCondition( int namLamViec)
        {
            return _repostiory.GetSoChungTuIndexByCondition(namLamViec);
        }

        public void LockOrUnlock(Guid id, bool status)
        {
            BhDtcDcdToanChi entity = _repostiory.Find(id);
            if (entity != null)
            {
                entity.BIsKhoa = status;
                _repostiory.Update(entity);
            }
        }

        public void Update(BhDtcDcdToanChi entity)
        {
            using (var transactionScope = new TransactionScope(
                TransactionScopeOption.Required,
                new TransactionOptions { IsolationLevel = IsolationLevel.ReadCommitted }))
            {
                _repostiory.Update(entity);

                transactionScope.Complete();
            }
        }

        public IEnumerable<BhDtcDcdToanChi> FindByAggregateVoucher(List<string> voucherNos, int yearOfWork)
        {
            return _repostiory.FindByAggregateVoucher(voucherNos, yearOfWork);
        }

        public List<BhDtcDcdToanChi> FindAggregateAdjustVoucher(int yearOfWork, Guid? iIDLoaiDanhMucChi, string sMaLoaiChi)
        {
            return _repostiory.FindAggregateAdjustVoucher(yearOfWork, iIDLoaiDanhMucChi, sMaLoaiChi);
        }
    }
}
