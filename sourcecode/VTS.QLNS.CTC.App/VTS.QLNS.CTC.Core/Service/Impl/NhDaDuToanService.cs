using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Repository;

namespace VTS.QLNS.CTC.Core.Service.Impl
{
    public class NhDaDuToanService : INhDaDuToanService
    {
        private readonly INhDaDuToanRepository _repository;
        private readonly INhDaDuToanChiPhiRepository _nhDaDuToanChiPhiRepository;
        private readonly INhDaDuToanNguonVonRepository _nhDaDuToanNguonVonRepository;
        private readonly INhDaDuToanHangMucRepository _nhDaDuToanHangMucRepository;

        public NhDaDuToanService(
            INhDaDuToanRepository nhDaDuToanRepository,
            INhDaDuToanChiPhiRepository nhDaDuToanChiPhiRepository,
            INhDaDuToanNguonVonRepository nhDaDuToanNguonVonRepository,
            INhDaDuToanHangMucRepository nhDaDuToanHangMucRepository)
        {
            _repository = nhDaDuToanRepository;
            _nhDaDuToanChiPhiRepository = nhDaDuToanChiPhiRepository;
            _nhDaDuToanNguonVonRepository = nhDaDuToanNguonVonRepository;
            _nhDaDuToanHangMucRepository = nhDaDuToanHangMucRepository;
        }

        public void Add(NhDaDuToan entity)
        {
            using (var transactionScope = new TransactionScope(
                TransactionScopeOption.Required,
                new TransactionOptions { IsolationLevel = IsolationLevel.ReadCommitted }))
            {
                _repository.Add(entity);

                // Cập nhật chi tiết
                _nhDaDuToanNguonVonRepository.AddOrUpdate(entity.Id, entity.DuToanNguonVons);
                var listChiPhi = entity.DuToanNguonVons.SelectMany(x => x.DuToanChiPhis);
                _nhDaDuToanChiPhiRepository.AddOrUpdate(listChiPhi);
                foreach (var item in entity.DuToanNguonVons)
                {
                    var listHangMuc = item.DuToanChiPhis.SelectMany(x => x.DuToanHangMucs);
                    _nhDaDuToanHangMucRepository.AddOrUpdate(listHangMuc);
                }
                transactionScope.Complete();
            }
        }

        public void Update(NhDaDuToan entity)
        {
            using (var transactionScope = new TransactionScope(
                TransactionScopeOption.Required,
                new TransactionOptions { IsolationLevel = IsolationLevel.ReadCommitted }))
            {
                _repository.Update(entity);

                // Cập nhật chi tiết
                _nhDaDuToanNguonVonRepository.AddOrUpdate(entity.Id, entity.DuToanNguonVons);
                _nhDaDuToanChiPhiRepository.AddOrUpdate(entity.DuToanNguonVons.SelectMany(x => x.DuToanChiPhis));
                foreach (var item in entity.DuToanNguonVons)
                {
                    _nhDaDuToanHangMucRepository.AddOrUpdate(item.DuToanChiPhis.SelectMany(x => x.DuToanHangMucs));
                }
                transactionScope.Complete();
            }
        }

        public void Adjust(NhDaDuToan entity)
        {
            using (var transactionScope = new TransactionScope(
                TransactionScopeOption.Required,
                new TransactionOptions { IsolationLevel = IsolationLevel.ReadCommitted }))
            {
                _repository.Add(entity);

                // Update BIsActive = false của bản ghi gốc
                NhDaDuToan parentEntity = _repository.Find(entity.IIdParentId);
                if (parentEntity != null)
                {
                    parentEntity.BIsActive = false;
                }
                _repository.Update(parentEntity);

                // Cập nhật chi tiết
                _nhDaDuToanNguonVonRepository.AddOrUpdate(entity.Id, entity.DuToanNguonVons);
                //_nhDaDuToanChiPhiRepository.AddOrUpdate(entity.DuToanNguonVons.SelectMany(x => x.DuToanChiPhis));
                _nhDaDuToanChiPhiRepository.AddAdjust(entity.DuToanNguonVons.FirstOrDefault().Id,entity.DuToanNguonVons.SelectMany(x => x.DuToanChiPhis));
                foreach (var item in entity.DuToanNguonVons)
                {
                    _nhDaDuToanHangMucRepository.AddOrUpdate(item.DuToanChiPhis.SelectMany(x => x.DuToanHangMucs));
                }
                transactionScope.Complete();
            }
        }

        public void Delete(NhDaDuToan entity)
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
                        NhDaDuToan entityParent = _repository.Find(entity.IIdParentId.Value);
                        if (entityParent != null)
                        {
                            entityParent.BIsActive = true;
                            _repository.Update(entityParent);
                        }
                    }
                    _repository.Delete(entity);

                    // Xóa chi tiết
                    _nhDaDuToanNguonVonRepository.DeleteByDuAnId(entity.Id);
                    _nhDaDuToanChiPhiRepository.DeleteByDuAnId(entity.Id);
                    // Xóa hạng mục chi phí
                }

                transactionScope.Complete();
            }
        }

        public void LockOrUnlock(Guid id, bool isLock)
        {
            NhDaDuToan entity = _repository.Find(id);
            if (entity != null)
            {
                entity.BIsKhoa = isLock;
                _repository.Update(entity);
            }
        }

        public NhDaDuToan FindById(Guid id)
        {
            return _repository.Find(id);
        }

        public IEnumerable<NhDaDuToanQuery> FindIndex(int namLamViec, int iLoai)
        {
            return _repository.FindIndex(namLamViec, iLoai);
        }

        public IEnumerable<NhDaDuToan> FindDuToanByIdDonVi(string maDonVi, int iLoai)
        {
            return _repository.FindDuToanByIdDonVi(maDonVi, iLoai);
        }

        public IEnumerable<NhDaDuToan> FindDuToanMoTaMSByIdDonVi(string maDonVi, int iLoai)
        {
            return _repository.FindDuToanMoTaMSByIdDonVi(maDonVi, iLoai);
        }

        // Optimize
        public int AddRangeDuToanChiPhi(IEnumerable<NhDaDuToanChiPhi> entities)
        {
            return _nhDaDuToanChiPhiRepository.AddRange(entities);
        }

        public int AddRangeDuToanHangMuc(IEnumerable<NhDaDuToanHangMuc> entities)
        {
            return _nhDaDuToanHangMucRepository.AddRange(entities);
        }

        public int AddRangeDuToanNguonVon(IEnumerable<NhDaDuToanNguonVon> entities)
        {
            return _nhDaDuToanNguonVonRepository.AddRange(entities);
        }

        public bool CheckDuplicateSoQD(string soQuyetDinh, Guid id)
        {
            return _repository.CheckDuplicateSoQD(soQuyetDinh, id);
        }

        public int DeleteDuToanChiPhi(Guid id)
        {
            return _nhDaDuToanChiPhiRepository.Delete(id);
        }

        public void DeleteDuToanChiTiet(Guid id)
        {
            _repository.DeleteDuToanChiTiet(id);
        }

        public int DeleteDuToanNguonVon(Guid id)
        {
            return _nhDaDuToanNguonVonRepository.Delete(id);
        }

        public NhDaDuToanChiPhi FindDuToanChiPhi(params object[] keyValues)
        {
            return _nhDaDuToanChiPhiRepository.Find(keyValues);
        }

        public NhDaDuToanNguonVon FindDuToanNguonVon(params object[] keyValues)
        {
            return _nhDaDuToanNguonVonRepository.Find(keyValues);
        }

        public IEnumerable<NhDaDuToanChiPhiQuery> FindListDuToanChiPhiByDuAn(Guid duAnId)
        {
            return _repository.FindListDuToanChiPhiByDuAn(duAnId);
        }

        public IEnumerable<NhDaDuToanChiPhiQuery> FindListDuToanChiPhiByDuToanId(Guid duToanId)
        {
            return _repository.FindListDuToanChiPhiByDuToanId(duToanId);
        }

        public IEnumerable<NhDaDuToanNguonVonQuery> FindListDuToanNguonVonByDuAn(Guid duAnId)
        {
            return _repository.FindListDuToanNguonVonByDuAn(duAnId);
        }

        public IEnumerable<NhDaDuToanNguonVonQuery> FindListDuToanNguonVonByDuToanId(Guid duToanId)
        {
            return _repository.FindListDuToanNguonVonByDuToanId(duToanId);
        }

        public int UpdateDuToanChiPhi(NhDaDuToanChiPhi entity)
        {
            return _nhDaDuToanChiPhiRepository.Update(entity);
        }

        public int UpdateDuToanNguonVon(NhDaDuToanNguonVon entity)
        {
            return _nhDaDuToanNguonVonRepository.Update(entity);
        }

        public NhDaDuToanHangMuc FindDuToanHangMuc(Guid duToanHangMucId)
        {
            return _nhDaDuToanHangMucRepository.Find(duToanHangMucId);
        }

        public int UpdateDuToanHangMuc(NhDaDuToanHangMuc entity)
        {
            return _nhDaDuToanHangMucRepository.Update(entity);
        }

        public int DeleteDuToanHangMucDetail(Guid id)
        {
            return _nhDaDuToanHangMucRepository.Delete(id);
        }

        public IEnumerable<NhDaDuToanHangMucQuery> FindListHangMucAllDetail(Guid duToanId)
        {
            return _repository.FindListHangMucAllDetail(duToanId);
        }



        public IEnumerable<NhDaDuToanHangMucQuery> GetQdDauTuHangMucByQdDautuChiPhiId(Guid qdDauTuChiPhiId)
        {
            return _repository.GetQdDauTuHangMucByQdDautuChiPhiId(qdDauTuChiPhiId);
        }

        public IEnumerable<NhDaDuToanHangMucQuery> GetDuToanTuHangMucByDuToanChiPhiId(Guid duToanChiPhiId)
        {
            return _repository.GetDuToanTuHangMucByDuToanChiPhiId(duToanChiPhiId);
        }

        public List<NhDaDuToan> FindDuAnInKhlcNhaThau(Guid iIdKhlcntId, Guid iIdDuAnID)
        {
            return _repository.FindDuAnInKhlcNhaThau(iIdKhlcntId, iIdDuAnID);
        }


        public List<NhDaDuToan> FindDuAnInKhlcNhaThauID(Guid iIdDuAnID)
        {
            return _repository.FindDuAnInKhlcNhaThauID(iIdDuAnID);
        }

        public IEnumerable<NhDaDetailNguonVonQuery> GetNguonVonByDuToanId(Guid iIdDuToanId)
        {
            return _nhDaDuToanNguonVonRepository.FindByDuToanId(iIdDuToanId);
        }

        public IEnumerable<NhDaDetailChiPhiQuery> GetChiPhiByDuToanId(Guid iIdDuToanId)
        {
            return _nhDaDuToanChiPhiRepository.GetAllByDuToanId(iIdDuToanId);
        }

        public IEnumerable<NhDaDetailHangMucQuery> GetHangMucByDuToanId(Guid iIdDuToanId)
        {
            return _nhDaDuToanHangMucRepository.GetHangMucByDuToanId(iIdDuToanId);
        }
    }
}
