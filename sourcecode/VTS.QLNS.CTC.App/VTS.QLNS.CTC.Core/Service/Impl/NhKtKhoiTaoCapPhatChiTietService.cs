using System;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Repository;
using VTS.QLNS.CTC.Utility;

namespace VTS.QLNS.CTC.Core.Service.Impl
{
    public class NhKtKhoiTaoCapPhatChiTietService : INhKtKhoiTaoCapPhatChiTietService
    {
        private readonly INhKtKhoiTaoCapPhatChiTietRepository _repository;

        public NhKtKhoiTaoCapPhatChiTietService(
            INhKtKhoiTaoCapPhatChiTietRepository repository)
        {
            _repository = repository;
        }

        public void AddOrUpdate(IEnumerable<NhKtKhoiTaoCapPhatChiTiet> entities)
        {
            using (var transactionScope = new TransactionScope(
                TransactionScopeOption.Required,
                new TransactionOptions { IsolationLevel = IsolationLevel.ReadCommitted }))
            {
                List<NhKtKhoiTaoCapPhatChiTiet> lstAdded = entities.Where(x => x.IsAdded && !x.IsDeleted).ToList();
                if (lstAdded.Any())
                {
                    _repository.AddRange(lstAdded);
                }

                List<NhKtKhoiTaoCapPhatChiTiet> lstModified = entities.Where(x => x.IsModified && !x.IsAdded && !x.IsDeleted).ToList();
                if (lstModified.Any())
                {
                    _repository.UpdateRange(lstModified);
                }

                List<NhKtKhoiTaoCapPhatChiTiet> lstDeleted = entities.Where(x => x.IsDeleted && !x.IsAdded).ToList();
                if (lstDeleted.Any())
                {
                    _repository.RemoveRange(lstDeleted);
                }

                transactionScope.Complete();
            }
        }

        public IEnumerable<NhKtKhoiTaoCapPhatChiTiet> FindByKhoiTaoCapPhatId(Guid khoiTaoCapPhatId)
        {
            return _repository.FindAll(x => x.IIdKhoiTaoCapPhatID == khoiTaoCapPhatId);
        }

        public NhKtKhoiTaoCapPhatChiTiet FetchData(Guid khoiTaoCapPhatId, Guid hopDongId)
        {
            return _repository.FetchData(khoiTaoCapPhatId, hopDongId).FirstOrDefault();
        }

        public IEnumerable<NhKtKhoiTaoCapPhatChiTietQuery> FindById(Guid khoiTaoCapPhatId)
        {
            return _repository.FindById(khoiTaoCapPhatId);
        }

        public IEnumerable<NhKtKhoiTaoCapPhatChiTietQuery> FindDetailById(Guid khoiTaoCapPhatId)
        {
            return _repository.FindDetailById(khoiTaoCapPhatId);
        }
    }
}
