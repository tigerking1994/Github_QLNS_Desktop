using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Transactions;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Repository;
using VTS.QLNS.CTC.Utility;

namespace VTS.QLNS.CTC.Core.Service.Impl
{
    public class NhDaHopDongService : IService<NhDaHopDong>, INhDaHopDongService
    {
        private readonly INhDaHopDongRepository _nhDaHopDongRepository;
        private readonly INhDaHopDongNguonVonRepository _nhDaHopDongNguonVonRepository;
        private readonly INhDaHopDongChiPhiRepository _nhDaHopDongChiPhiRepository;
        private readonly INhDaHopDongHangMucRepository _nhDaHopDongHangMucRepository;

        public NhDaHopDongService
        (
            INhDaHopDongRepository nhDaHopDongRepository,
            INhDaHopDongNguonVonRepository nhDaHopDongNguonVonRepository,
            INhDaHopDongChiPhiRepository nhDaHopDongChiPhiRepository,
            INhDaHopDongHangMucRepository nhDaHopDongHangMucRepository
        )
        {
            _nhDaHopDongRepository = nhDaHopDongRepository;
            _nhDaHopDongNguonVonRepository = nhDaHopDongNguonVonRepository;
            _nhDaHopDongChiPhiRepository = nhDaHopDongChiPhiRepository;
            _nhDaHopDongHangMucRepository = nhDaHopDongHangMucRepository;
        }

        public IEnumerable<NhDaHopDongQuery> FindAllHopDong(int? iThuocMenu = null) => _nhDaHopDongRepository.FindAllHopDong(iThuocMenu);
        public IEnumerable<NhDaHopDongQuery> FindAllHopDongtrongnuoc(int? iThuocMenu = null) => _nhDaHopDongRepository.FindAllHopDongtrongnuoc(iThuocMenu);

        public IEnumerable<NhDaHopDongQuery> FindAllHopDongNgoaiThuong(int? iThuocMenu = null) => _nhDaHopDongRepository.FindAllHopDongNgoaiThuong(iThuocMenu);

        public NhDaHopDong FindById(Guid? id) => _nhDaHopDongRepository.Find(id);

        public void AddHDNT(NhDaHopDong nhDaHopDong)
        {
            using (var transactionScope = new TransactionScope(
                TransactionScopeOption.Required,
                new TransactionOptions { IsolationLevel = IsolationLevel.ReadCommitted }))
            {
                _nhDaHopDongRepository.Add(nhDaHopDong);
                SaveHopDongChiPhiByList(nhDaHopDong.Id, nhDaHopDong.HopDongChiPhis);

                transactionScope.Complete();
            }
        }
        public void UpdateHDNT(NhDaHopDong entity)
        {
            using (var transactionScope = new TransactionScope(
                TransactionScopeOption.Required,
                new TransactionOptions { IsolationLevel = IsolationLevel.ReadCommitted }))
            {
                _nhDaHopDongRepository.Update(entity);
                SaveHopDongChiPhiByList(entity.Id, entity.HopDongChiPhis);
                transactionScope.Complete();
            }
        }

        public void AdjustHDNT(NhDaHopDong entity)
        {
            using (var transactionScope = new TransactionScope(
                TransactionScopeOption.Required,
                new TransactionOptions { IsolationLevel = IsolationLevel.ReadCommitted }))
            {
                _nhDaHopDongRepository.Add(entity);

                // Update BIsActive = false của bản ghi gốc
                NhDaHopDong parentEntity = _nhDaHopDongRepository.Find(entity.IIdParentId);
                if (parentEntity != null)
                {
                    parentEntity.BIsActive = false;
                    _nhDaHopDongRepository.Update(parentEntity);
                }

                SaveHopDongChiPhiByList(entity.Id, entity.HopDongChiPhis);
                transactionScope.Complete();
            }
        }
        public void Add(NhDaHopDong nhDaHopDong)
        {
            using (var transactionScope = new TransactionScope(
                TransactionScopeOption.Required,
                new TransactionOptions { IsolationLevel = IsolationLevel.ReadCommitted }))
            {
                _nhDaHopDongRepository.Add(nhDaHopDong);
                SaveHopDongHangMuc(nhDaHopDong.Id, nhDaHopDong.HopDongHangMucs);
                transactionScope.Complete();
            }
        }

        public void Update(NhDaHopDong entity)
        {
            using (var transactionScope = new TransactionScope(
                TransactionScopeOption.Required,
                new TransactionOptions { IsolationLevel = IsolationLevel.ReadCommitted }))
            {
                _nhDaHopDongRepository.Update(entity);
                SaveHopDongHangMuc(entity.Id, entity.HopDongHangMucs);
                transactionScope.Complete();
            }
        }

        public void Adjust(NhDaHopDong entity)
        {
            using (var transactionScope = new TransactionScope(
                TransactionScopeOption.Required,
                new TransactionOptions { IsolationLevel = IsolationLevel.ReadCommitted }))
            {
                _nhDaHopDongRepository.Add(entity);

                // Update BIsActive = false của bản ghi gốc
                NhDaHopDong parentEntity = _nhDaHopDongRepository.Find(entity.IIdParentId);
                if (parentEntity != null)
                {
                    parentEntity.BIsActive = false;
                    _nhDaHopDongRepository.Update(parentEntity);
                }

                SaveHopDongHangMuc(entity.Id, entity.HopDongHangMucs);
                transactionScope.Complete();
            }
        }

        public void Delete(NhHdnkPhuongAnNhapKhau entity)
        {
            using (var transactionScope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = IsolationLevel.ReadCommitted }))
            {
                var hopDong = _nhDaHopDongRepository.Find(entity.Id);
                if (hopDong != null)
                {
                    // Nếu là xóa bản ghi điều chỉnh thì bản ghi gốc sẽ được update bactive = 1
                    if (hopDong.IIdParentId.HasValue)
                    {
                        var pankParent = _nhDaHopDongRepository.Find(hopDong.IIdParentId.Value);
                        if (pankParent != null)
                        {
                            pankParent.BIsActive = true;
                            _nhDaHopDongRepository.Update(pankParent);
                        }
                    }

                    _nhDaHopDongRepository.Delete(hopDong);

                    // Xóa chi tiết
                    var listNguonVon = _nhDaHopDongNguonVonRepository.FindAll(s => s.IIdHopDongId == hopDong.Id);
                    if (!listNguonVon.IsEmpty())
                    {
                        DeleteNguonVon(listNguonVon);
                    }

                    transactionScope.Complete();
                }
            }
        }
        private void SaveHopDongHangMuc(Guid id, IEnumerable<NhDaHopDongHangMuc> items)
        {
            if (!items.IsEmpty())
            {
                List<NhDaHopDongHangMuc> lstAdded = items.Where(x => x.IsAdded && !x.IsDeleted && x.IsChecked).ToList();
                if (!lstAdded.IsEmpty())
                {
                    foreach (var item in lstAdded)
                    {
                        item.Id = Guid.NewGuid();
                        item.IIdHopDongId = id;
                    }
                    _nhDaHopDongHangMucRepository.AddRange(lstAdded);
                }

                List<NhDaHopDongHangMuc> lstModified = items.Where(x => x.IsModified && !x.IsAdded && !x.IsDeleted && x.IsChecked).ToList();
                if (!lstModified.IsEmpty())
                {
                    foreach (var item in lstModified)
                    {
                        item.IIdHopDongId = id;
                    }
                    _nhDaHopDongHangMucRepository.UpdateRange(lstModified);
                }

                List<NhDaHopDongHangMuc> lstDeleted = items.Where(x => x.IsDeleted && !x.IsAdded).ToList();
                if (!lstDeleted.IsEmpty())
                {
                    foreach (var item in lstDeleted)
                    {
                        _nhDaHopDongHangMucRepository.Delete(item);
                    }

                }
            }
        }

        private void SaveHopDongHangMucByList(Guid id, Guid idChiPhi, IEnumerable<NhDaHopDongHangMuc> items)
        {
            if (!items.IsEmpty())
            {
                List<NhDaHopDongHangMuc> lstAdded = items.Where(x => x.IsAdded && !x.IsDeleted).ToList();
                if (!lstAdded.IsEmpty())
                {
                    foreach (var item in lstAdded)
                    {
                        item.Id = Guid.NewGuid();
                        item.IIdHopDongId = id;
                    }
                    _nhDaHopDongHangMucRepository.AddRange(lstAdded);
                }

                List<NhDaHopDongHangMuc> lstModified = items.Where(x => x.IsModified && !x.IsAdded && !x.IsDeleted).ToList();
                if (!lstModified.IsEmpty())
                {
                    foreach (var item in lstModified)
                    {
                        item.IIdHopDongId = id;
                    }
                    _nhDaHopDongHangMucRepository.UpdateRange(lstModified);
                }

                List<NhDaHopDongHangMuc> lstDeleted = items.Where(x => x.IsDeleted && !x.IsAdded).ToList();
                if (!lstDeleted.IsEmpty())
                {
                    foreach (var item in lstDeleted)
                    {
                        _nhDaHopDongHangMucRepository.Delete(item);
                    }

                }
            }
        }


        private void SaveHopDongNguonVon(Guid id, IEnumerable<NhDaHopDongNguonVon> items)
        {
            if (!items.IsEmpty())
            {
                List<NhDaHopDongNguonVon> lstAdded = items.Where(x => x.IsAdded && !x.IsDeleted).ToList();
                if (!lstAdded.IsEmpty())
                {
                    foreach (var item in lstAdded)
                    {
                        item.Id = Guid.NewGuid();
                        item.IIdHopDongId = id;
                        SaveHopDongChiPhi(item.Id, item.HopDongChiPhis);
                    }
                    _nhDaHopDongNguonVonRepository.AddRange(lstAdded);
                }

                List<NhDaHopDongNguonVon> lstModified = items.Where(x => x.IsModified && !x.IsAdded && !x.IsDeleted).ToList();
                if (!lstModified.IsEmpty())
                {
                    foreach (var item in lstModified)
                    {
                        item.IIdHopDongId = id;
                        SaveHopDongChiPhi(item.Id, item.HopDongChiPhis);
                    }
                    _nhDaHopDongNguonVonRepository.UpdateRange(lstModified);
                }

                List<NhDaHopDongNguonVon> lstDeleted = items.Where(x => x.IsDeleted && !x.IsAdded).ToList();
                if (!lstDeleted.IsEmpty())
                {
                    DeleteNguonVon(lstDeleted);
                }
            }
        }

        private void DeleteNguonVon(IEnumerable<NhDaHopDongNguonVon> nguonVons)
        {
            if (nguonVons.Any())
            {
                foreach (var nv in nguonVons)
                {
                    var listDeletedChiPhi = _nhDaHopDongChiPhiRepository.FindAll(s => s.IIdHopDongNguonVonId == nv.Id);
                    if (listDeletedChiPhi.Any())
                    {
                        foreach (var cp in listDeletedChiPhi)
                        {
                            var listDeletedHangMuc = _nhDaHopDongHangMucRepository.FindAll(s => s.IIdHopDongChiPhiId == cp.Id);
                            _nhDaHopDongHangMucRepository.RemoveRange(listDeletedHangMuc);
                        }
                        _nhDaHopDongChiPhiRepository.RemoveRange(listDeletedChiPhi);
                    }
                }
                _nhDaHopDongNguonVonRepository.RemoveRange(nguonVons);
            }
        }

        private void SaveHopDongChiPhi(Guid idNguonVon, IEnumerable<NhDaHopDongChiPhi> items)
        {
            if (items.IsEmpty()) return;

            List<NhDaHopDongChiPhi> lstAdded = items.Where(x => x.IsAdded && !x.IsDeleted).ToList();
            if (!lstAdded.IsEmpty())
            {
                foreach (var item in lstAdded)
                {
                    item.Id = Guid.NewGuid();
                    item.IIdHopDongNguonVonId = idNguonVon;
                    _nhDaHopDongChiPhiRepository.Add(item);
                    _nhDaHopDongHangMucRepository.AddOrUpdate(item.Id, item.HopDongHangMucs);
                }
            }

            List<NhDaHopDongChiPhi> listModified = items.Where(x => x.IsModified && !x.IsAdded && !x.IsDeleted).ToList();
            if (!listModified.IsEmpty())
            {
                foreach (var item in listModified)
                {
                    item.IIdHopDongNguonVonId = idNguonVon;
                    _nhDaHopDongChiPhiRepository.Update(item);
                    _nhDaHopDongHangMucRepository.AddOrUpdate(item.Id, item.HopDongHangMucs);
                }
            }

            List<NhDaHopDongChiPhi> lstDeleted = items.Where(x => x.IsDeleted && !x.IsAdded).ToList();
            if (!lstDeleted.IsEmpty())
            {
                foreach (var item in lstDeleted)
                {
                    var listDeletedHangMuc = _nhDaHopDongHangMucRepository.FindAll(s => s.IIdHopDongChiPhiId == item.Id);
                    _nhDaHopDongHangMucRepository.RemoveRange(listDeletedHangMuc);
                }
                _nhDaHopDongChiPhiRepository.RemoveRange(lstDeleted);
            }
        }

        private void SaveHopDongChiPhiByList(Guid idHopDong, IEnumerable<NhDaHopDongChiPhi> items)
        {
            if (items.IsEmpty()) return;

            List<NhDaHopDongChiPhi> lstAdded = items.Where(x => x.IsAdded && !x.IsDeleted).ToList();
            if (!lstAdded.IsEmpty())
            {
                foreach (var item in lstAdded)
                {
                    item.IIdHopDongId = idHopDong;
                    _nhDaHopDongChiPhiRepository.Add(item);
                    _nhDaHopDongHangMucRepository.AddOrUpdateListHangMuc(item.Id, item.HopDongHangMucs);
                }
            }

            List<NhDaHopDongChiPhi> listModified = items.Where(x => x.IsModified && !x.IsAdded && !x.IsDeleted).ToList();
            if (!listModified.IsEmpty())
            {
                foreach (var item in listModified)
                {
                    item.IIdHopDongId = idHopDong;
                    _nhDaHopDongChiPhiRepository.Update(item);
                    _nhDaHopDongHangMucRepository.AddOrUpdateListHangMuc(item.Id, item.HopDongHangMucs);
                }
            }

            List<NhDaHopDongChiPhi> lstDeleted = items.Where(x => x.IsDeleted && !x.IsAdded).ToList();
            if (!lstDeleted.IsEmpty())
            {
                foreach (var item in lstDeleted)
                {
                    var listDeletedHangMuc = _nhDaHopDongHangMucRepository.FindAll(s => s.IIdHopDongChiPhiId == item.Id);
                    _nhDaHopDongHangMucRepository.RemoveRange(listDeletedHangMuc);
                }
                _nhDaHopDongChiPhiRepository.RemoveRange(lstDeleted);
            }
        }


        public void LockOrUnlock(Guid id, bool BIsKhoa)
        {
            NhDaHopDong entity = _nhDaHopDongRepository.Find(id);
            if (entity != null) entity.BIsKhoa = BIsKhoa;
            _nhDaHopDongRepository.Update(entity);
        }

        public void DeleteHopDong(Guid id)
        {
            _nhDaHopDongRepository.DeleteHopDong(id);
        }

        public IEnumerable<NhDaHopDong> FindByCondition(Expression<Func<NhDaHopDong, bool>> predicate)
        {
            return _nhDaHopDongRepository.FindAll(predicate);
        }

        public IEnumerable<NhDaHopDongTrongNuocQuery> FindAllHopDongTrongNuoc()
        {
            return _nhDaHopDongRepository.FindAllHopDongTrongNuoc();
        }

        public IEnumerable<NhDaHopDong> FindAll()
        {
            return _nhDaHopDongRepository.FindAll();
        }

        public IEnumerable<NhDaHopDong> FindByIdKHTongTheNhiemVuChi(Guid? idKHTongTheNhiemVuChi)
        {
            return _nhDaHopDongRepository.FindByIdKHTongTheNhiemVuChi(idKHTongTheNhiemVuChi);
        }

        public IEnumerable<NhDaHopDongQuery> FindByIdDonVi(Guid? IIdDonViQuanLyId)
        {
            return _nhDaHopDongRepository.FindByIdDonVi(IIdDonViQuanLyId);
        }

        public int AddRange(List<NhDaHopDong> entities)
        {
            return _nhDaHopDongRepository.AddRange(entities);
        }

        public void Delete(Guid id)
        {
            throw new NotImplementedException();
        }

        public override IEnumerable<NhDaHopDong> FindAll(AuthenticationInfo authenticationInfo)
        {
            return _nhDaHopDongRepository.FindAll(authenticationInfo);
        }

        public override void AddOrUpdateRange(IEnumerable<NhDaHopDong> listEntities, AuthenticationInfo authenticationInfo)
        {
            _nhDaHopDongRepository.AddOrUpdateRange(listEntities, authenticationInfo);
        }
        public IEnumerable<NhDmLoaiHopDong> GetAllLoaiHopDong()
        {
            return _nhDaHopDongRepository.GetAllLoaiHopDong();
        }
    }
}
