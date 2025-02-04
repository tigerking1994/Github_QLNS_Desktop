using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Transactions;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Repository;
using VTS.QLNS.CTC.Core.Repository.Impl;

namespace VTS.QLNS.CTC.Core.Service.Impl
{
    public class NhKhTongTheNhiemVuChiService : INhKhTongTheNhiemVuChiService
    {
        private readonly INhKhTongTheNhiemVuChiRepository _repository;
        private readonly INhDmNhiemVuChiRepository _dmNhiemVuChiRepository;

        public NhKhTongTheNhiemVuChiService(INhKhTongTheNhiemVuChiRepository nhKhTongTheNhiemVuChiRepository, INhDmNhiemVuChiRepository dmNhiemVuChiRepository)
        {
            _repository = nhKhTongTheNhiemVuChiRepository;
            _dmNhiemVuChiRepository = dmNhiemVuChiRepository;
        }

        public int Add(NhKhTongTheNhiemVuChi nhKhTongTheNhiemVuChi)
        {
            return _repository.Add(nhKhTongTheNhiemVuChi);
        }

        public int Update(NhKhTongTheNhiemVuChi nhKhTongTheNhiemVuChi)
        {
            return _repository.Update(nhKhTongTheNhiemVuChi);
        }

        public int Delete(Guid id)
        {
            return _repository.Delete(id);
        }

        public int AddRange(IEnumerable<NhKhTongTheNhiemVuChi> entities)
        {
            return _repository.AddRange(entities);
        }

        public int RemoveRange(IEnumerable<NhKhTongTheNhiemVuChi> entities)
        {
            return _repository.RemoveRange(entities);
        }

        public int UpdateRange(IEnumerable<NhKhTongTheNhiemVuChi> entities)
        {
            return _repository.UpdateRange(entities);
        }

        public IEnumerable<NhKhTongTheNhiemVuChi> FindAll()
        {
            return _repository.FindAll();
        }

        public NhKhTongTheNhiemVuChi Find(Guid id)
        {
            return _repository.Find(id);
        }

        public NhKhTongTheNhiemVuChiQuery FindById(Guid id)
        {
            return _repository.FindById(id);
        }

        public IEnumerable<NhKhTongTheNhiemVuChi> FindAllByKhTongTheId(Guid idKhTongThe)
        {
            return _repository.FindAll(x => x.IIdKhTongTheId == idKhTongThe);
        }
        public IEnumerable<NhKhTongThe> FindKhTongTheByNvChiId(Guid idNvChi)
        {
            return _repository.FindKhTongTheByNvChiId(idNvChi);
        }
        public IEnumerable<NhKhTongTheNhiemVuChiQuery> FindAllDonViByKhTongTheId(Guid idKhTongThe)
        {
            return _repository.FindAllDonViByKhTongTheId(idKhTongThe);
        }
        public IEnumerable<NhKhTongTheNhiemVuChiQuery> FindByIdKhTongTheAndIdDonVi(Guid idKhTongThe, Guid idDonVi)
        {
            return _repository.FindByIdKhTongTheAndIdDonVi(idKhTongThe, idDonVi);
        }

        public IEnumerable<NhDaGoiThauQuery> FindByIdNhiemVuChi(Guid idNhiemVuChi)
        {
            return _repository.FindByIdNhiemVuChi(idNhiemVuChi);
        }

        public IEnumerable<NhKhTongTheNhiemVuChiQuery> FindByIdKhTongThe(Guid idKhTongThe)
        {
            return _repository.FindByIdKhTongThe(idKhTongThe);
        }

        public IEnumerable<NhKhTongTheNhiemVuChiQuery> FindAllNvcByIdKhTongTheGiaiDoan(Guid idKhTongThe)
        {
            return _repository.FindAllNvcByIdKhTongTheGiaiDoan(idKhTongThe);
        }

        public NhKhTongTheNhiemVuChiQuery FindOneByIdKhTongTheAndIdNhiemVuChi(Guid idKhTongThe, Guid idNhiemVuChi)
        {
            return _repository.FindOneByIdKhTongTheAndIdNhiemVuChi(idKhTongThe, idNhiemVuChi);
        }

        public IEnumerable<NhKhTongTheNhiemVuChiQuery> FindByIdKhTongTheAndMaDonVi(Guid idKhTongThe, string maDonVi)
        {
            return _repository.FindByIdKhTongTheAndMaDonVi(idKhTongThe, maDonVi);
        }
        public IEnumerable<NhKhTongTheNhiemVuChiQuery> FindByIdKhTongTheAndMaDonViID(Guid idKhTongThe, Guid maDonVi)
        {
            return _repository.FindByIdKhTongTheAndMaDonViID(idKhTongThe, maDonVi);
        }

        public IEnumerable<NhKhTongTheNhiemVuChiQuery> FindKHTongTheAndDmNhiemVuChi(Guid idKhTongThe)
        {
            return _repository.FindKHTongTheAndDmNhiemVuChi(idKhTongThe);
        }

        public void AddKHTongTheNhiemVuChi(IEnumerable<NhKhTongTheNhiemVuChi> khTongTheNhiemVuChis, IEnumerable<NhDmNhiemVuChi> dmNhiemVuChis)
        {
            using (var transactionScope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = IsolationLevel.ReadCommitted }))
            {
                _repository.AddRange(khTongTheNhiemVuChis);
                _dmNhiemVuChiRepository.AddRange(dmNhiemVuChis);

                transactionScope.Complete();
            }
        }

        public void UpdateKHTongTheNhiemVuChi(IEnumerable<NhKhTongTheNhiemVuChi> khTongTheNhiemVuChis, IEnumerable<NhDmNhiemVuChi> dmNhiemVuChis)
        {
            using (var transactionScope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = IsolationLevel.ReadCommitted }))
            {
                _repository.UpdateRange(khTongTheNhiemVuChis);
                _dmNhiemVuChiRepository.UpdateRange(dmNhiemVuChis);

                transactionScope.Complete();
            }
        }

        public void DeleteKHTongTheNhiemVuChi(IEnumerable<NhKhTongTheNhiemVuChi> khTongTheNhiemVuChis, IEnumerable<NhDmNhiemVuChi> dmNhiemVuChis)
        {
            using (var transactionScope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = IsolationLevel.ReadCommitted }))
            {
                _repository.RemoveRange(khTongTheNhiemVuChis);
                _dmNhiemVuChiRepository.RemoveRange(dmNhiemVuChis);

                transactionScope.Complete();
            }
        }

        public IEnumerable<NhKhTongTheNhiemVuChiQuery> FindByIdDonVi(Guid? idDonVi)
        {
            return _repository.FindByIdDonVi(idDonVi);
        }

        public IEnumerable<NhKhTongTheNhiemVuChi> FindByCondition(Expression<Func<NhKhTongTheNhiemVuChi, bool>> predicate)
        {
            return _repository.FindAll(predicate);
        }
    }
}
