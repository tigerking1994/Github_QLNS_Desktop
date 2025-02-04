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
    public class NhQtPheDuyetQuyetToanDAHTService : INhQtPheDuyetQuyetToanDAHTService
    {
        private readonly INhQtPheDuyetQuyetToanDAHTRepository _repository;
        private readonly INhQtPheDuyetQuyetToanDAHTChiTietRepository _repositoryChiTiet;

        private readonly INhDmNhiemVuChiRepository _nhDmNhiemVuChiRepository;

        public NhQtPheDuyetQuyetToanDAHTService(
          INhQtPheDuyetQuyetToanDAHTRepository repository,
          INhQtPheDuyetQuyetToanDAHTChiTietRepository repositoryChiTiet,
          INhDmNhiemVuChiRepository nhDmNhiemVuChiRepository)
        {
            _repository = repository;
            _repositoryChiTiet = repositoryChiTiet;

            _nhDmNhiemVuChiRepository = nhDmNhiemVuChiRepository;
        }

        public void Add(NhQtPheDuyetQuyetToanDAHT entity)
        {
            using (var transactionScope = new TransactionScope(
                TransactionScopeOption.Required,
                new TransactionOptions { IsolationLevel = IsolationLevel.ReadCommitted }))
            {
                _repository.Add(entity);

                transactionScope.Complete();
            }
        }

        public void Update(NhQtPheDuyetQuyetToanDAHT entity)
        {
            using (var transactionScope = new TransactionScope(
                TransactionScopeOption.Required,
                new TransactionOptions { IsolationLevel = IsolationLevel.ReadCommitted }))
            {
                _repository.Update(entity);

                transactionScope.Complete();
            }
        }

        public IEnumerable<NhQtPheDuyetQuyetToanDAHTQuery> FindIndex(int yearOfWork)
        {
            return _repository.FindIndex(yearOfWork);
        }


        public void Delete(NhQtPheDuyetQuyetToanDAHT entity)
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
        public NhQtPheDuyetQuyetToanDAHT FindById(Guid id)
        {
            return _repository.Find(id);
        }
        public IEnumerable<NhQtPheDuyetQuyetToanDAHT> FindAll()
        {
            return _repository.FindAll().OrderByDescending(x => x.DNgayPheDuyet).ThenByDescending(x => x.SSoPheDuyet);
        }
        public IEnumerable<NhQtPheDuyetQuyetToanDAHTChiTietQuery> GetDataCreate(int iNamBaoCaoTu, int iNamBaoCaoDen, Guid? iID_DonViID, int? slbDonViUSD, int? slbDonViVND)
        {
            return _repository.GetDataCreate(iNamBaoCaoTu, iNamBaoCaoDen, iID_DonViID, slbDonViUSD, slbDonViVND);
        }
        public IEnumerable<NhQtPheDuyetQuyetToanDAHTChiTietQuery> GetDataDetail(Guid? iIDDonVi, int? devideDonViUSD, int? devideDonViVND, Guid? iDPheDuyetQuyetToan)
        {
            return _repository.GetDataDetail(iIDDonVi, devideDonViUSD, devideDonViVND, iDPheDuyetQuyetToan);
        }
        public IEnumerable<NhQtPheDuyetQuyetToanDAHTChiTietQuery> GetDataBaoCaoKetLuanDetail(Guid? iIDDonVi, int? devideDonViUSD, int? devideDonViVND, DateTime? dNgayPheDuyetTu, DateTime? dNgayPheDuyetDen)
        {
            return _repository.GetDataBaoCaoKetLuanDetail(iIDDonVi, devideDonViUSD, devideDonViVND, dNgayPheDuyetTu, dNgayPheDuyetDen);
        }
        public IEnumerable<NhTtThucHienNganSachGiaiDoanQuery> GetGiaiDoan()
        {
            return _repository.GetGiaiDoan();
        }
        public void AddOrUpdate(IEnumerable<NhQtPheDuyetQuyetToanDAHTChiTiet> entities)
        {
            using (var transactionScope = new TransactionScope(
                TransactionScopeOption.Required,
                new TransactionOptions { IsolationLevel = IsolationLevel.ReadCommitted }))
            {
                List<NhQtPheDuyetQuyetToanDAHTChiTiet> lstAdded = entities.Where(x => x.IsAdded && !x.IsDeleted).ToList();
                if (lstAdded.Any())
                {
                    _repositoryChiTiet.AddRange(lstAdded);
                }

                List<NhQtPheDuyetQuyetToanDAHTChiTiet> lstModified = entities.Where(x => x.IsModified && !x.IsAdded && !x.IsDeleted).ToList();
                if (lstModified.Any())
                {
                    _repositoryChiTiet.UpdateRange(lstModified);
                }

                List<NhQtPheDuyetQuyetToanDAHTChiTiet> lstDeleted = entities.Where(x => x.IsDeleted && !x.IsAdded).ToList();
                if (lstDeleted.Any())
                {
                    _repositoryChiTiet.RemoveRange(lstDeleted);
                }

                transactionScope.Complete();
            }
        }
    }
}
