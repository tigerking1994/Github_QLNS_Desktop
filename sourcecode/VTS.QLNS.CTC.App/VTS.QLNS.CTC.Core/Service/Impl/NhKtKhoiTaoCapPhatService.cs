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
    public class NhKtKhoiTaoCapPhatService : INhKtKhoiTaoCapPhatService
    {
        private readonly INhKtKhoiTaoCapPhatRepository _repository;

        public NhKtKhoiTaoCapPhatService(
            INhKtKhoiTaoCapPhatRepository repository)
        {
            _repository = repository;
        }

        public void Add(NhKtKhoiTaoCapPhat entity)
        {
            using (var transactionScope = new TransactionScope(
                TransactionScopeOption.Required,
                new TransactionOptions { IsolationLevel = IsolationLevel.ReadCommitted }))
            {
                _repository.Add(entity);

                transactionScope.Complete();
            }
        }

        public void Update(NhKtKhoiTaoCapPhat entity)
        {
            using (var transactionScope = new TransactionScope(
                TransactionScopeOption.Required,
                new TransactionOptions { IsolationLevel = IsolationLevel.ReadCommitted }))
            {
                _repository.Update(entity);

                transactionScope.Complete();
            }
        }

        public void Delete(NhKtKhoiTaoCapPhat entity)
        {
            using (var transactionScope = new TransactionScope(
                TransactionScopeOption.Required,
                new TransactionOptions { IsolationLevel = IsolationLevel.ReadCommitted }))
            {
                entity = _repository.Find(entity.Id);
                if (entity != null)
                {
                    _repository.Delete(entity);
                    // Xóa chi tiết
                }

                transactionScope.Complete();
            }
        }

        public NhKtKhoiTaoCapPhat FindById(Guid id)
        {
            return _repository.Find(id);
        }

        public IEnumerable<NhKtKhoiTaoCapPhat> FindAll()
        {
            return _repository.FindAll().OrderByDescending(x => x.DNgayKhoiTao);
        }

        public IEnumerable<NhKtKhoiTaoCapPhat> FindAll(Expression<Func<NhKtKhoiTaoCapPhat, bool>> predicate)
        {
            return _repository.FindAll(predicate);
        }

        public IEnumerable<NhKtKhoiTaoCapPhatQuery> GetAll(int INamLamViec)
        {
            return _repository.GetAll(INamLamViec);
        }

        public void DeleteKhoiTaoTheoQuyetDinh(Guid idKhoiTao, int type)
        {
            _repository.DeleteKhoiTaoTheoQuyetDinh(idKhoiTao, type);
        }
    }
}
