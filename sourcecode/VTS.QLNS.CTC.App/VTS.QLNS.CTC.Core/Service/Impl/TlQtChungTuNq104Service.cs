using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Transactions;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Repository;

namespace VTS.QLNS.CTC.Core.Service.Impl
{
    public class TlQtChungTuNq104Service : ITlQtChungTuNq104Service
    {
        private readonly ITlQtChungTuNq104Repository _tlQtChungTuRepository;
        private readonly ITlQtChungTuChiTietNq104Repository _tlQtChungTuChiTietRepository;

        public TlQtChungTuNq104Service(ITlQtChungTuNq104Repository tlQtChungTuRepository, ITlQtChungTuChiTietNq104Repository tlQtChungTuChiTietRepository)
        {
            _tlQtChungTuRepository = tlQtChungTuRepository;
            _tlQtChungTuChiTietRepository = tlQtChungTuChiTietRepository;
        }

        public int Add(TlQtChungTuNq104 entity)
        {
            return _tlQtChungTuRepository.Add(entity);
        }

        public int Delete(Guid id)
        {
            return _tlQtChungTuRepository.Delete(id);
        }

        public IEnumerable<TlQtChungTuNq104> FindAll()
        {
            return _tlQtChungTuRepository.FindAll();
        }

        public IEnumerable<TlQtChungTuNq104> FindChungTuExist(int yearOfWork, int thang, string maDonVi)
        {
            return _tlQtChungTuRepository.FindChungTuExist(yearOfWork, thang, maDonVi);
        }

        public IEnumerable<TlQtChungTuNq104> FindAll(Expression<Func<TlQtChungTuNq104, bool>> predicate)
        {
            return _tlQtChungTuRepository.FindAll(predicate);
        }

        public void LockOrUnlock(Guid id, bool isLock)
        {
            var chungTu = _tlQtChungTuRepository.Find(id);
            chungTu.BKhoa = isLock;
            _tlQtChungTuRepository.Update(chungTu);
        }

        public TlQtChungTuNq104 FindById(Guid id)
        {
            return _tlQtChungTuRepository.Find(id);
        }

        public int Update(TlQtChungTuNq104 entity)
        {
            return _tlQtChungTuRepository.Update(entity);
        }

        public int GetSoChungTuIndexByCondition(int namLamViec)
        {
            return _tlQtChungTuRepository.GetSoChungTuIndexByCondition(namLamViec);
        }

        public void Add(IEnumerable<TlQtChungTuNq104> entities, IEnumerable<TlQtChungTuChiTietNq104> detailEntities)
        {
            using (var transactionScope = new TransactionScope(
                TransactionScopeOption.Required,
                new TransactionOptions { IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted }))
            {
                _tlQtChungTuRepository.BulkInsert(entities);
                _tlQtChungTuChiTietRepository.BulkInsert(detailEntities);

                transactionScope.Complete();
            }
        }

        public void CapNhatQuyetToan(List<TlQtChungTuNq104> lstChungTu)
        {

        }
    }
}
