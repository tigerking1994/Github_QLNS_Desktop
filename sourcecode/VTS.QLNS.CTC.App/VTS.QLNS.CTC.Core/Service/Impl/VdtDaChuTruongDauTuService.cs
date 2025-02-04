using System;
using System.Collections.Generic;
using System.Transactions;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Repository;
using VTS.QLNS.CTC.Utility;

namespace VTS.QLNS.CTC.Core.Service.Impl
{
    public class VdtDaChuTruongDauTuService : IVdtDaChuTruongDauTuService
    {
        private readonly IVdtDaChuTruongDauTuRepository _repository;
        private readonly IVdtDaChuTruongDauTuChiPhiRepository _vdtDaChuTruongDauTuChiPhiRepository;
        private readonly IVdtDaChuTruongDauTuNguonVonRepository _vdtDaChuTruongDauTuNguonVonRepository;
        private readonly IVdtDaHangMucRepository _vdtDaHangMucRepository;
        private readonly IVdtDaChuTruongDauTuHangMucRepository _vdtDaChuTruongDauTuHangMucRepository;
        private readonly IVdtDaChuTruongDauTuDmHangMucRepository _vdtDaChuTruongDauTuDmHangMucRepository;

        public VdtDaChuTruongDauTuService(
            IVdtDaChuTruongDauTuRepository vdtDaChuTruongDauTuRepository,
            IVdtDaChuTruongDauTuChiPhiRepository vdtDaChuTruongDauTuChiPhiRepository,
            IVdtDaChuTruongDauTuNguonVonRepository vdtDaChuTruongDauTuNguonVonRepository,
            IVdtDaHangMucRepository vdtDaHangMucRepository,
            IVdtDaChuTruongDauTuHangMucRepository vdtDaChuTruongDauTuHangMucRepository,
            IVdtDaChuTruongDauTuDmHangMucRepository vdtDaChuTruongDauTuDmHangMucRepository)
        {
            _repository = vdtDaChuTruongDauTuRepository;
            _vdtDaChuTruongDauTuChiPhiRepository = vdtDaChuTruongDauTuChiPhiRepository;
            _vdtDaChuTruongDauTuNguonVonRepository = vdtDaChuTruongDauTuNguonVonRepository;
            _vdtDaHangMucRepository = vdtDaHangMucRepository;
            _vdtDaChuTruongDauTuHangMucRepository = vdtDaChuTruongDauTuHangMucRepository;
            _vdtDaChuTruongDauTuDmHangMucRepository = vdtDaChuTruongDauTuDmHangMucRepository;
        }

        public VdtDaChuTruongDauTu Add(VdtDaChuTruongDauTu entity)
        {
            _repository.Add(entity);
            return entity;
        }

        public VdtDaChuTruongDauTu Adjust(VdtDaChuTruongDauTu entity)
        {
            using (var transactionScope = new TransactionScope(
                TransactionScopeOption.Required,
                new TransactionOptions { IsolationLevel = IsolationLevel.ReadCommitted }))
            {
                _repository.Add(entity);

                // Update BActive = false của CTDT gốc
                VdtDaChuTruongDauTu parentEntity = _repository.Find(entity.IIdParentId);
                if (parentEntity != null)
                {
                    parentEntity.BActive = false;
                }
                _repository.Update(parentEntity);
                transactionScope.Complete();
                return entity;
            }
        }

        public int AddRangeChuTruongChiPhi(IEnumerable<VdtDaChuTruongDauTuChiPhi> entities)
        {
            return _vdtDaChuTruongDauTuChiPhiRepository.AddRange(entities);
        }

        public int AddRangeChuTruongDMHangMuc(IEnumerable<VdtDaChuTruongDauTuDmHangMuc> entities)
        {
            return _vdtDaChuTruongDauTuDmHangMucRepository.AddRange(entities);
        }

        public int AddRangeChuTruongHangMuc(IEnumerable<VdtDaChuTruongDauTuHangMuc> entities)
        {
            return _vdtDaChuTruongDauTuHangMucRepository.AddRange(entities);
        }

        public int AddRangeChuTruongNguonVon(IEnumerable<VdtDaChuTruongDauTuNguonVon> entities)
        {
            return _vdtDaChuTruongDauTuNguonVonRepository.AddRange(entities);
        }

        public bool CheckDuAnExistQDDauTu(Guid duAnId)
        {
            return _repository.CheckDuAnExistQDDauTu(duAnId);
        }

        public bool CheckDuplicateSoQD(string soQuyetDinh, Guid id)
        {
            return _repository.CheckDuplicateSoQD(soQuyetDinh,id);
        }

        public int DeleteChuTruongChiPhi(Guid id)
        {
            VdtDaChuTruongDauTuChiPhi entity = FindChuTruongChiPhi(id);
            if (entity != null)
            {
                return _vdtDaChuTruongDauTuChiPhiRepository.Delete(entity);
            }
            return 0;
        }

        public void DeleteChuTruongDauTu(Guid id, Guid? parentId)
        {
            _repository.DeleteChuTruongDauTu(id,parentId);
        }

        public int DeleteChuTruongDMHangMuc(Guid id)
        {
            return _vdtDaChuTruongDauTuDmHangMucRepository.Delete(id);
        }

        public int DeleteChuTruongHangMuc(Guid id)
        {
            return _vdtDaChuTruongDauTuHangMucRepository.Delete(id);
        }

        public int DeleteChuTruongNguonVon(Guid id)
        {
            VdtDaChuTruongDauTuNguonVon entity = FindChuTruongNguonVon(id);
            if (entity != null)
            {
                return _vdtDaChuTruongDauTuNguonVonRepository.Delete(entity);
            }
            return 0;
        }

        public IEnumerable<ChuTruongDauTuQuery> FindByCondition(int namLamViec, string userlogin)
        {
            return _repository.FindByCondition(namLamViec, userlogin);
        }

        public VdtDaChuTruongDauTu FindByDuAnId(Guid id)
        {
            return _repository.FindByDuAnId(id);
        }

        public VdtDaChuTruongDauTu FindById(Guid id)
        {
            return _repository.Find(id);
        }

        public ChuTruongDauTuQuery FindChuTruongById(Guid id)
        {
            return _repository.FindChuTruongById(id);
        }

        public VdtDaChuTruongDauTuChiPhi FindChuTruongChiPhi(params object[] keyValues)
        {
            return _vdtDaChuTruongDauTuChiPhiRepository.Find(keyValues);
        }

        public VdtDaChuTruongDauTuDmHangMuc FindChuTruongDMHangMuc(params object[] keyValues)
        {
            return _vdtDaChuTruongDauTuDmHangMucRepository.Find(keyValues);
        }

        public VdtDaChuTruongDauTuHangMuc FindChuTruongHangMuc(params object[] keyValues)
        {
            return _vdtDaChuTruongDauTuHangMucRepository.Find(keyValues);
        }

        public VdtDaChuTruongDauTuNguonVon FindChuTruongNguonVon(params object[] keyValues)
        {
            return _vdtDaChuTruongDauTuNguonVonRepository.Find(keyValues);
        }

        public IEnumerable<VdtDaDuAn> FindDuAnNotExistsInChuTruongDT(Guid chuTruongDT, string donViId, int namLV)
        {
            return _repository.FindDuAnNotExistsInChuTruongDT(chuTruongDT, donViId, namLV);
        }

        public IEnumerable<VdtDaChuTruongDauTuNguonVonQuery> FindListChuTruongDauTuNguonVonByDuAn(Guid duAnId)
        {
            return _repository.FindListChuTruongDauTuNguonVonByDuAn(duAnId);
        }

        public IEnumerable<VdtDaHangMucQuery> FindListChuTruongHangMucDieuChinhAdd(Guid chuTruongId)
        {
            return _vdtDaHangMucRepository.FindListChuTruongHangMucDieuChinhAdd(chuTruongId);
        }

        public IEnumerable<VdtDaHangMucQuery> FindListChuTruongHangMucDieuChinhUpdate(Guid chuTruongId)
        {
            return _vdtDaHangMucRepository.FindListChuTruongHangMucDieuChinhUpdate(chuTruongId);
        }

        public IEnumerable<VdtDaChuTruongDauTuNguonVonQuery> FindListChuTruongNguonVonDetail(Guid chuTruongId)
        {
            return _repository.FindListChuTruongNguonVonDetail(chuTruongId);
        }

        public IEnumerable<VdtDaChuTruongDauTuNguonVonQuery> FindListChuTruongNguonVonDieuChinhAdd(Guid chuTruongId)
        {
            return _repository.FindListChuTruongNguonVonDieuChinhAdd(chuTruongId);
        }

        public IEnumerable<VdtDaChuTruongDauTuNguonVonQuery> FindListChuTruongNguonVonDieuChinhUpdate(Guid chuTruongId)
        {
            return _repository.FindListChuTruongNguonVonDieuChinhUpdate(chuTruongId);
        }

        public IEnumerable<VdtDaHangMucQuery> FindListDAHangMucDetail(Guid chuTruongId)
        {
            return _vdtDaHangMucRepository.FindListDAHangMucDetail(chuTruongId);
        }

        public IEnumerable<VdtDaHangMucQuery> FindListDAHangMucDetailAfterSaveChuTruong(Guid chuTruongId)
        {
            return _vdtDaHangMucRepository.FindListDAHangMucDetailAfterSaveChuTruong(chuTruongId);
        }

        public IEnumerable<ChuTruongDauTuDetailQuery> FindListDetail(Guid chuTruongDT)
        {
            return _repository.FindListDetail(chuTruongDT);
        }

        public int Update(VdtDaChuTruongDauTu entity)
        {
            return _repository.Update(entity);
        }

        public int UpdateChuTruongChiPhi(VdtDaChuTruongDauTuChiPhi entity)
        {
            return _vdtDaChuTruongDauTuChiPhiRepository.Update(entity);
        }

        public int UpdateChuTruongDMHangMuc(VdtDaChuTruongDauTuDmHangMuc entity)
        {
            return _vdtDaChuTruongDauTuDmHangMucRepository.Update(entity);
        }

        public int UpdateChuTruongHangMuc(VdtDaChuTruongDauTuHangMuc entity)
        {
            return _vdtDaChuTruongDauTuHangMucRepository.Update(entity);
        }

        public int UpdateChuTruongNguonVon(VdtDaChuTruongDauTuNguonVon entity)
        {
            return _vdtDaChuTruongDauTuNguonVonRepository.Update(entity);
        }

        public void LockOrUnlock(Guid id, bool isLock)
        {
            VdtDaChuTruongDauTu chungTu = _repository.Find(id);
            chungTu.BKhoa = isLock;
            _repository.Update(chungTu);
        }

        public VdtDaChuTruongDauTu FindCTDTDieuChinhByDuAn(Guid id, Guid duAnId)
        {
            return _repository.FindCTDTDieuChinhByDuAn(id, duAnId);
        }

        public IEnumerable<VdtDaDuToanQuery> GetChuTruongDauTuByDuAnInKhlcNhaThauScreen(Guid iIdDuAnId)
        {
            return _repository.GetChuTruongDauTuByDuAnInKhlcNhaThauScreen(iIdDuAnId);
        }

        public IEnumerable<VdtDaDuToanQuery> GetChuTruongDauTuByIdInKhlcNhaThauScreen(Guid iIdChuTruongDauTuId)
        {
            return _repository.GetChuTruongDauTuByIdInKhlcNhaThauScreen(iIdChuTruongDauTuId);
        }

        public IEnumerable<ChuTruongDauTuQuery> FindByConditionUserLogin(string userlogin)
        {
            return _repository.FindByConditionUserLogin(userlogin);
        }

        public void DeleteChuTruongDauTuHangMuc(Guid id)
        {
            _repository.DeleteChuTruongDauTuHangMuc(id);
        }
    }
}
