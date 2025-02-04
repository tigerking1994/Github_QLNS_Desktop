using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Transactions;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Repository;

namespace VTS.QLNS.CTC.Core.Service.Impl
{
    public class TlQtChungTuService : ITlQtChungTuService
    {
        private readonly ITlQtChungTuRepository _tlQtChungTuRepository;
        private readonly ITlQtChungTuChiTietRepository _tlQtChungTuChiTietRepository;

        public TlQtChungTuService(ITlQtChungTuRepository tlQtChungTuRepository, ITlQtChungTuChiTietRepository tlQtChungTuChiTietRepository)
        {
            _tlQtChungTuRepository = tlQtChungTuRepository;
            _tlQtChungTuChiTietRepository = tlQtChungTuChiTietRepository;
        }

        public int Add(TlQtChungTu entity)
        {
            return _tlQtChungTuRepository.Add(entity);
        }

        public int Delete(Guid id)
        {
            return _tlQtChungTuRepository.Delete(id);
        }

        public IEnumerable<TlQtChungTu> FindAll()
        {
            return _tlQtChungTuRepository.FindAll();
        }

        public IEnumerable<TlQtChungTu> FindChungTuExist(int yearOfWork, int thang, string maDonVi)
        {
            return _tlQtChungTuRepository.FindChungTuExist(yearOfWork, thang, maDonVi);
        }

        public IEnumerable<TlQtChungTu> FindAll(Expression<Func<TlQtChungTu, bool>> predicate)
        {
            return _tlQtChungTuRepository.FindAll(predicate);
        }

        public void LockOrUnlock(Guid id, bool isLock)
        {
            var chungTu = _tlQtChungTuRepository.Find(id);
            chungTu.BKhoa = isLock;
            _tlQtChungTuRepository.Update(chungTu);
        }

        public TlQtChungTu FindById(Guid id)
        {
            return _tlQtChungTuRepository.Find(id);
        }

        public int Update(TlQtChungTu entity)
        {
            return _tlQtChungTuRepository.Update(entity);
        }

        public int GetSoChungTuIndexByCondition(int namLamViec)
        {
            return _tlQtChungTuRepository.GetSoChungTuIndexByCondition(namLamViec);
        }

        public void Add(IEnumerable<TlQtChungTu> entities, IEnumerable<TlQtChungTuChiTiet> detailEntities)
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

        public void CapNhatQuyetToan(List<TlQtChungTu> lstChungTu)
        {

        }
    }
}
