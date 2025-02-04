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
    public class NhQtChuyenQuyetToanService : INhQtChuyenQuyetToanService
    {
        private readonly INhQtChuyenQuyetToanRepository _repository;

        public NhQtChuyenQuyetToanService(INhQtChuyenQuyetToanRepository repository)
        {
            _repository = repository;
        }

        public void Add(NhQtChuyenQuyetToan nhQtChuyenQuyetToan)
        {
            using (var transactionScope = new TransactionScope(
                TransactionScopeOption.Required,
                new TransactionOptions { IsolationLevel = IsolationLevel.ReadCommitted }))
            {
                _repository.Add(nhQtChuyenQuyetToan);

                transactionScope.Complete();
            }
        }

        public void Update(NhQtChuyenQuyetToan nhQtChuyenQuyetToan)
        {
            using (var transactionScope = new TransactionScope(
                TransactionScopeOption.Required,
                new TransactionOptions { IsolationLevel = IsolationLevel.ReadCommitted }))
            {
                _repository.Update(nhQtChuyenQuyetToan);

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

        public IEnumerable<NhQtChuyenQuyetToan> FindAll()
        {
            return _repository.FindAll().OrderByDescending(x => x.dNgayTao).ThenBy(x => x.sSoChungTu);
        }

        public IEnumerable<NhQtChuyenQuyetToan> FindAll(Expression<Func<NhQtChuyenQuyetToan, bool>> predicate)
        {
            return _repository.FindAll(predicate);
        }

        public NhQtChuyenQuyetToan FindById(Guid id)
        {
            return _repository.Find(id);
        }

        public void Save(NhQtChuyenQuyetToan entity)
        {
            _repository.Save(entity);
        }

        public void SaveNhQtChuyenQuyetToanChiTiet(List<NhQtChuyenQuyetToanChiTiet> entities, Guid nhQtChuyenQuyetToanId)
        {
            _repository.SaveNhQtChuyenQuyetToanChiTiet(entities, nhQtChuyenQuyetToanId);
        }

        public IEnumerable<NhQtChuyenQuyetToanQuery> FindIndex()
        {
            return _repository.FindIndex();
        }

        public bool CheckExistsCQTByTimeAndDonvi(Guid idCQT, Guid iID_DonViID, int loaiThoiGian, int thoiGian)
        {
            return _repository.CheckExistsCQTByTimeAndDonvi(idCQT, iID_DonViID, loaiThoiGian, thoiGian);
        }
    }
}
