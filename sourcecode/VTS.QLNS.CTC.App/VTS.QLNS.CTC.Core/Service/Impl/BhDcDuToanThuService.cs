using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Repository;
using VTS.QLNS.CTC.Core.Repository.Impl;
using VTS.QLNS.CTC.Utility;

namespace VTS.QLNS.CTC.Core.Service.Impl
{
    public class BhDcDuToanThuService : IBhDcDuToanThuService
    {
        private readonly IBhDcDuToanThuRepository _repostiory;

        public BhDcDuToanThuService(IBhDcDuToanThuRepository repostiory)
        {
            _repostiory = repostiory;
        }

        public void Add(BhDcDuToanThu entity)
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
                BhDcDuToanThu entity = _repostiory.Find(id); ;
                if (entity != null)
                {
                    _repostiory.Delete(entity);
                    // Xóa chi tiết
                }

                transactionScope.Complete();
            }
        }

        public int Delete(BhDcDuToanThu item)
        {
            return _repostiory.Delete(item);
        }

        public IEnumerable<BhDcDuToanThu> FindByCondition(Expression<Func<BhDcDuToanThu, bool>> predicate)
        {
            return _repostiory.FindAll(predicate);
        }

        public IEnumerable<BhDcDuToanThu> FindByCondition(int namLamViec, string maDonVi, int loaiChungTu)
        {
            return _repostiory.FindByCondition(namLamViec, maDonVi, loaiChungTu);
        }

        public BhDcDuToanThu FindById(Guid id)
        {
            return _repostiory.Find(id);
        }

        public IEnumerable<BhDcDuToanThu> FindChungTuDaTongHopBySCT(string sct, int namLamViec)
        {
            return _repostiory.FindChungTuDaTongHopBySCT(sct, namLamViec);
        }

        public List<string> FindCurrentUnits(int namLamViec)
        {
            return _repostiory.FindCurrentUnits(namLamViec);
        }

        public IEnumerable<BhDcDuToanThuQuery> FindIndex(int namLamViec)
        {
            return _repostiory.FindIndex(namLamViec);
        }

        public IEnumerable<BhDcDuToanThuQuery> FindByYearOfWord(int namLamViec)
        {
            return _repostiory.FindByYearOfWord(namLamViec);
        }

        public IEnumerable<BhDcDuToanThu> FindByAggregateVoucher(List<string> voucherNoes, int yearOfWork)
        {
            return _repostiory.FindByAggregateVoucher(voucherNoes, yearOfWork);
        }

        public int GetSoChungTuIndexByCondition()
        {
            return _repostiory.GetSoChungTuIndexByCondition();
        }

        public void LockOrUnlock(Guid id, bool status)
        {
            var chungTuBHXH = _repostiory.Find(id);
            chungTuBHXH.BIsKhoa = status;
            _repostiory.Update(chungTuBHXH);
        }

        public void Update(BhDcDuToanThu entity)
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
