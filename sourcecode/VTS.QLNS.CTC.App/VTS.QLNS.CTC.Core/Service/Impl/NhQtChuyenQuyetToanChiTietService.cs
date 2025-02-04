using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Repository;

namespace VTS.QLNS.CTC.Core.Service.Impl
{
    public class NhQtChuyenQuyetToanChiTietService : INhQtChuyenQuyetToanChiTietService
    {
        private readonly INhQtChuyenQuyetToanChiTietRepository _repository;

        public NhQtChuyenQuyetToanChiTietService(INhQtChuyenQuyetToanChiTietRepository repository)
        {
            _repository = repository;
        }

        public void Add(NhQtChuyenQuyetToanChiTiet nhQtChuyenQuyetToanChiTiet)
        {
            using (var transactionScope = new TransactionScope(
                TransactionScopeOption.Required,
                new TransactionOptions { IsolationLevel = IsolationLevel.ReadCommitted }))
            {
                _repository.Add(nhQtChuyenQuyetToanChiTiet);

                transactionScope.Complete();
            }
        }

        public void Update(NhQtChuyenQuyetToanChiTiet nhQtChuyenQuyetToanChiTiet)
        {
            using (var transactionScope = new TransactionScope(
                TransactionScopeOption.Required,
                new TransactionOptions { IsolationLevel = IsolationLevel.ReadCommitted }))
            {
                _repository.Update(nhQtChuyenQuyetToanChiTiet);

                transactionScope.Complete();
            }
        }

        public void Delete(Guid id)
        {
            using (var transactionScope = new TransactionScope(
                TransactionScopeOption.Required,
                new TransactionOptions { IsolationLevel = IsolationLevel.ReadCommitted }))
            {
                if (_repository.Find(id) != null)
                {
                    _repository.Delete(id);
                }

                transactionScope.Complete();
            }
        }

        public IEnumerable<NhQtChuyenQuyetToanChiTiet> FindAll()
        {
            return _repository.FindAll().OrderBy(x => x.sLNS);
        }

        public IEnumerable<NhQtChuyenQuyetToanChiTiet> FindAll(Expression<Func<NhQtChuyenQuyetToanChiTiet, bool>> predicate)
        {
            return _repository.FindAll(predicate);
        }

        public NhQtChuyenQuyetToanChiTiet FindById(Guid id)
        {
            return _repository.Find(id);
        }

        public void Save(NhQtChuyenQuyetToanChiTiet entity)
        {
            _repository.Save(entity);
        }

        public void DeleteByChuyenQuyetToanId(Guid id)
        {
            _repository.DeleteByChuyenQuyetToanId(id);
        }
    }
}
