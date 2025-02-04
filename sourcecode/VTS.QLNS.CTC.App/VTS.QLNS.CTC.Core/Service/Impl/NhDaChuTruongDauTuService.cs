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
    public class NhDaChuTruongDauTuService : INhDaChuTruongDauTuService
    {
        private readonly INhDaChuTruongDauTuRepository _repository;
        private readonly INhDaChuTruongDauTuNguonVonRepository _nhDaChuTruongDauTuNguonVonRepository;
        private readonly INhDaChuTruongDauTuHangMucRepository _nhDaChuTruongDauTuHangMucRepository;

        public NhDaChuTruongDauTuService(
            INhDaChuTruongDauTuRepository repository,
            INhDaChuTruongDauTuNguonVonRepository nhDaChuTruongDauTuNguonVonRepository,
            INhDaChuTruongDauTuHangMucRepository nhDaChuTruongDauTuHangMucRepository)
        {
            _repository = repository;
            _nhDaChuTruongDauTuNguonVonRepository = nhDaChuTruongDauTuNguonVonRepository;
            _nhDaChuTruongDauTuHangMucRepository = nhDaChuTruongDauTuHangMucRepository;
        }

        public void Add(NhDaChuTruongDauTu entity)
        {
            using (var transactionScope = new TransactionScope(
                TransactionScopeOption.Required,
                new TransactionOptions { IsolationLevel = IsolationLevel.ReadCommitted }))
            {
                _repository.Add(entity);
                _nhDaChuTruongDauTuNguonVonRepository.AddOrUpdate(entity.Id, entity.ChuTruongDauTuNguonVons);
                _nhDaChuTruongDauTuHangMucRepository.AddOrUpdate(entity.Id, entity.ChuTruongDauTuHangMucs);
                
                transactionScope.Complete();
            }
        }

        public void Update(NhDaChuTruongDauTu entity)
        {
            using (var transactionScope = new TransactionScope(
                TransactionScopeOption.Required,
                new TransactionOptions { IsolationLevel = IsolationLevel.ReadCommitted }))
            {
                _repository.Update(entity);
                _nhDaChuTruongDauTuNguonVonRepository.AddOrUpdate(entity.Id, entity.ChuTruongDauTuNguonVons);
                _nhDaChuTruongDauTuHangMucRepository.AddOrUpdate(entity.Id, entity.ChuTruongDauTuHangMucs);
                
                transactionScope.Complete();
            }
        }

        public void Adjust(NhDaChuTruongDauTu entity)
        {
            using (var transactionScope = new TransactionScope(
                TransactionScopeOption.Required,
                new TransactionOptions { IsolationLevel = IsolationLevel.ReadCommitted }))
            {
                _repository.Add(entity);

                // Update BActive = false của CTDT gốc
                NhDaChuTruongDauTu parentEntity = _repository.Find(entity.IIdParentId);
                if (parentEntity != null)
                {
                    parentEntity.BIsActive = false;
                }
                _repository.Update(parentEntity);

                // Cập nhật nguồn vốn, hạng mục
                _nhDaChuTruongDauTuNguonVonRepository.AddOrUpdate(entity.Id, entity.ChuTruongDauTuNguonVons);
                _nhDaChuTruongDauTuHangMucRepository.AddOrUpdate(entity.Id, entity.ChuTruongDauTuHangMucs);
                
                transactionScope.Complete();
            }
        }

        public void Delete(NhDaChuTruongDauTu entity)
        {
            using (var transactionScope = new TransactionScope(
                TransactionScopeOption.Required,
                new TransactionOptions { IsolationLevel = IsolationLevel.ReadCommitted }))
            {
                entity = _repository.Find(entity.Id);
                if (entity != null)
                {
                    // Nếu là xóa bản ghi điều chỉnh thì bản ghi gốc sẽ được update bactive = 1
                    if (entity.IIdParentId.HasValue)
                    {
                        NhDaChuTruongDauTu chuTruongParent = _repository.Find(entity.IIdParentId.Value);
                        if (chuTruongParent != null)
                        {
                            chuTruongParent.BIsActive = true;
                            _repository.Update(chuTruongParent);
                        }
                    }
                    _repository.Delete(entity);
                    _nhDaChuTruongDauTuNguonVonRepository.DeleteByChuTruongDauTuId(entity.Id);
                    _nhDaChuTruongDauTuHangMucRepository.DeleteByChuTruongDauTuId(entity.Id);
                }

                transactionScope.Complete();
            }
        }

        public void LockOrUnlock(Guid id, bool status)
        {
            NhDaChuTruongDauTu entity = _repository.Find(id);
            if (entity != null)
            {
                entity.BIsKhoa = status;
                _repository.Update(entity);
            }
        }

        public NhDaChuTruongDauTu FindById(Guid id)
        {
            return _repository.Find(id);
        }

        public NhDaChuTruongDauTu FindByDuAnId(Guid duAnId)
        {
            return _repository.FindAll(x => x.IIdDuAnId == duAnId).FirstOrDefault();
        }

        public IEnumerable<NhDaChuTruongDauTuQuery> FindIndex(int yearOfWork, int ILoai)
        {
            return _repository.FindIndex(yearOfWork, ILoai);
        }

        public IEnumerable<NhDaChuTruongDauTu> FindAll()
        {
            return _repository.FindAll().OrderByDescending(x => x.DNgayQuyetDinh).ThenByDescending(x => x.SSoQuyetDinh);
        }

        public IEnumerable<NhDaChuTruongDauTu> FindAll(Expression<Func<NhDaChuTruongDauTu, bool>> predicate)
        {
            return _repository.FindAll(predicate);
        }
    }
}
