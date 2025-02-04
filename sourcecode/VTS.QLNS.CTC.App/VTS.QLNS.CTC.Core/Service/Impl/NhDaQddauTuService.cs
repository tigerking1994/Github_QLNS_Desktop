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
    public class NhDaQdDauTuService : INhDaQdDauTuService
    {
        private readonly INhDaQdDauTuRepository _repository;
        private readonly INhDaQdDauTuNguonVonRepository _nhDaQdDauTuNguonVonRepository;
        private readonly INhDaQdDauTuChiPhiRepository _nhDaQdDauTuChiPhiRepository;
        private readonly INhDaQdDauTuHangMucRepository _nhDaQdDauTuHangMucRepository;

        public NhDaQdDauTuService(
            INhDaQdDauTuRepository repository,
            INhDaQdDauTuNguonVonRepository nhDaQdDauTuNguonVonRepository,
            INhDaQdDauTuChiPhiRepository nhDaQdDauTuChiPhiRepository,
            INhDaQdDauTuHangMucRepository nhDaQdDauTuHangMucRepository)
        {
            _repository = repository;
            _nhDaQdDauTuNguonVonRepository = nhDaQdDauTuNguonVonRepository;
            _nhDaQdDauTuChiPhiRepository = nhDaQdDauTuChiPhiRepository;
            _nhDaQdDauTuHangMucRepository = nhDaQdDauTuHangMucRepository;
        }

        public void Add(NhDaQdDauTu entity)
        {
            using (var transactionScope = new TransactionScope(
                TransactionScopeOption.Required,
                new TransactionOptions { IsolationLevel = IsolationLevel.ReadCommitted }))
            {
                _repository.Add(entity);
                SaveQdDauTuNguonVon(entity.Id, entity.QdDauTuNguonVons);
                //SaveQdDauTuChiPhi(entity.Id, entity.QdDauTuChiPhis);
                transactionScope.Complete();
            }
        }
        public void AddRange(IEnumerable<NhDaQdDauTu> data)
        {
            _repository.AddRange(data);
        }


        public void Update(NhDaQdDauTu entity)
        {
            using (var transactionScope = new TransactionScope(
                TransactionScopeOption.Required,
                new TransactionOptions { IsolationLevel = IsolationLevel.ReadCommitted }))
            {
                _repository.Update(entity);
                SaveQdDauTuNguonVon(entity.Id, entity.QdDauTuNguonVons);
                //SaveQdDauTuChiPhi(entity.Id, entity.QdDauTuChiPhis);
                transactionScope.Complete();
            }
        }

        public void Adjust(NhDaQdDauTu entity)
        {
            using (var transactionScope = new TransactionScope(
                TransactionScopeOption.Required,
                new TransactionOptions { IsolationLevel = IsolationLevel.ReadCommitted }))
            {
                _repository.Add(entity);

                // Update BIsActive = false của bản ghi gốc
                NhDaQdDauTu parentEntity = _repository.Find(entity.IIdParentId);
                if (parentEntity != null)
                {
                    parentEntity.BIsActive = false;
                }
                _repository.Update(parentEntity);

                SaveQdDauTuNguonVon(entity.Id, entity.QdDauTuNguonVons);
                //SaveQdDauTuChiPhi(entity.Id, entity.QdDauTuChiPhis);
                transactionScope.Complete();
            }
        }

        public void Delete(NhDaQdDauTu entity)
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
                        NhDaQdDauTu chuTruongParent = _repository.Find(entity.IIdParentId.Value);
                        if (chuTruongParent != null)
                        {
                            chuTruongParent.BIsActive = true;
                            _repository.Update(chuTruongParent);
                        }
                    }
                    _repository.Delete(entity);

                    // Xóa chi tiết
                    var lstNguonVonDeleted = _nhDaQdDauTuNguonVonRepository.FindByQdDauTuId(entity.Id);
                    foreach (var nguonVon in lstNguonVonDeleted)
                    {
                        var lstChiPhiDeleted = _nhDaQdDauTuChiPhiRepository.FindByQdDauTuNGuonVonId(nguonVon.Id);
                        foreach (var item in lstChiPhiDeleted)
                        {
                            _nhDaQdDauTuHangMucRepository.DeleteByQdDauTuChiPhiId(item.Id);
                        }
                        _nhDaQdDauTuChiPhiRepository.RemoveRange(lstChiPhiDeleted);
                    }
                    _nhDaQdDauTuNguonVonRepository.RemoveRange(lstNguonVonDeleted);
                }
                transactionScope.Complete();
            }
        }

        public void LockOrUnlock(Guid id, bool status)
        {
            NhDaQdDauTu entity = _repository.Find(id);
            if (entity != null)
            {
                entity.BIsKhoa = status;
                _repository.Update(entity);
            }
        }

        public void SaveQdDauTuNguonVon(Guid qdDauTuId, IEnumerable<NhDaQdDauTuNguonVon> items)
        {
            if (!items.IsEmpty())
            {
                List<NhDaQdDauTuNguonVon> lstAdded = items.Where(x => x.IsAdded && !x.IsDeleted && x.IIdNguonVonId.HasValue).ToList();
                if (!lstAdded.IsEmpty())
                {
                    foreach (var item in lstAdded)
                    {
                        item.IIdQdDauTuId = qdDauTuId;
                        SaveQdDauTuChiPhi(item.Id, qdDauTuId, item.QdDauTuChiPhis);
                    }
                    _nhDaQdDauTuNguonVonRepository.AddRange(lstAdded);
                }

                List<NhDaQdDauTuNguonVon> lstModified = items.Where(x => x.IsModified && !x.IsAdded && !x.IsDeleted && x.IIdNguonVonId.HasValue).ToList();
                if (!lstModified.IsEmpty())
                {
                    foreach (var item in lstModified)
                    {
                        item.IIdQdDauTuId = qdDauTuId;
                        SaveQdDauTuChiPhi(item.Id, qdDauTuId, item.QdDauTuChiPhis);
                    }
                    _nhDaQdDauTuNguonVonRepository.UpdateRange(lstModified);
                }

                List<NhDaQdDauTuNguonVon> lstDeleted = items.Where(x => x.IsDeleted && !x.IsAdded).ToList();
                if (!lstDeleted.IsEmpty())
                {
                    foreach (var item in lstDeleted)
                    {
                        _nhDaQdDauTuChiPhiRepository.DeleteByQdNguonVonId(item.Id);
                    }
                    _nhDaQdDauTuNguonVonRepository.RemoveRange(lstDeleted);
                }
            }
        }

        public void SaveQdDauTuChiPhi(Guid idNguonVon, Guid idQdDauTuId, IEnumerable<NhDaQdDauTuChiPhi> items)
        {
            List<NhDaQdDauTuChiPhi> lstAdded = items.Where(x => x.IsAdded && !x.IsDeleted).ToList();
            if (!lstAdded.IsEmpty())
            {
                foreach (var item in lstAdded)
                {
                    item.IIdQDDauTuNguonVonId = idNguonVon;
                    item.IIdQdDauTuId = idQdDauTuId;
                    _nhDaQdDauTuChiPhiRepository.Add(item);
                    _nhDaQdDauTuHangMucRepository.AddOrUpdate(item.Id, item.QdDauTuHangMucs);
                }
            }

            List<NhDaQdDauTuChiPhi> listModified = items.Where(x => x.IsModified && !x.IsAdded && !x.IsDeleted).ToList();
            if (!listModified.IsEmpty())
            {
                foreach (var item in listModified)
                {
                    item.IIdQDDauTuNguonVonId = idNguonVon;
                    item.IIdQdDauTuId = idQdDauTuId;
                    _nhDaQdDauTuChiPhiRepository.Update(item);
                    _nhDaQdDauTuHangMucRepository.AddOrUpdate(item.Id, item.QdDauTuHangMucs);
                }
            }

            List<NhDaQdDauTuChiPhi> lstDeleted = items.Where(x => x.IsDeleted && !x.IsAdded).ToList();
            if (!lstDeleted.IsEmpty())
            {
                _nhDaQdDauTuChiPhiRepository.RemoveRange(lstDeleted);
                foreach (var item in lstDeleted)
                {
                    _nhDaQdDauTuHangMucRepository.DeleteByQdDauTuChiPhiId(item.Id);
                }
            }
        }

        public NhDaQdDauTu FindById(Guid id)
        {
            return _repository.Find(id);
        }

        public IEnumerable<NhDaQdDauTuQuery> FindIndex(int yearOfWork, int iLoai)
        {
            return _repository.FindIndex(yearOfWork, iLoai);
        }

        public IEnumerable<NhDaQdDauTu> FindAll()
        {
            return _repository.FindAll().OrderByDescending(x => x.DNgayQuyetDinh).ThenByDescending(x => x.SSoQuyetDinh);
        }

        public IEnumerable<NhDaQdDauTu> FindAll(Expression<Func<NhDaQdDauTu, bool>> predicate)
        {
            return _repository.FindAll(predicate);
        }

        public NhDaQdDauTu FindByDuAnId(Guid duAnId)
        {
            return _repository.FindAll(x => x.IIdDuAnId == duAnId).FirstOrDefault();
        }
        public List<NhDaQdDauTu> FindListByDuAnId(Guid duAnId)
        {
            return _repository.FindAll(x => x.IIdDuAnId == duAnId).ToList();
        }

        public IEnumerable<NhDaDetailNguonVonQuery> GetNguonVonByQdDauTuId(Guid iIdQdDauTuId)
        {
            return _nhDaQdDauTuNguonVonRepository.GetNguonVonByQdDauTuId(iIdQdDauTuId);
        }

        public IEnumerable<NhDaDetailChiPhiQuery> GetChiPhiByQdDauTuId(Guid iIdQdDauTuId)
        {
            return _nhDaQdDauTuChiPhiRepository.GetChiPhiByQdDauTuId(iIdQdDauTuId);
        }

        public IEnumerable<NhDaDetailHangMucQuery> GetHangMucByQdDauTuId(Guid iIdQdDauTuId)
        {
            return _nhDaQdDauTuHangMucRepository.GetHangMucByQdDauTuId(iIdQdDauTuId);
        }
        public bool CheckDuplicateSoQD(string soQuyetDinh, Guid id)
        {
            return _repository.CheckDuplicateSoQD(soQuyetDinh, id);
        }
    }
}
