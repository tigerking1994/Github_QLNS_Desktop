using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Repository;

namespace VTS.QLNS.CTC.Core.Service.Impl
{
    public class SktChungTuService : ISktChungTuService
    {
        private readonly ISktChungTuRepository _sktChungTuRepository;

        public SktChungTuService(ISktChungTuRepository sktChungTuRepository)
        {
            _sktChungTuRepository = sktChungTuRepository;
        }

        public NsSktChungTu FindById(Guid id)
        {
            return _sktChungTuRepository.Find(id);
        }

        public int Add(NsSktChungTu entity)
        {
            return _sktChungTuRepository.Add(entity);
        }

        public int Delete(NsSktChungTu item)
        {
            return _sktChungTuRepository.Delete(item);
        }

        public void BulkInsert(List<NsSktChungTu> lstData)
        {
            _sktChungTuRepository.BulkInsert(lstData);
        }

        public int Update(NsSktChungTu item)
        {
            return _sktChungTuRepository.Update(item);
        }

        public int GetSoChungTuIndexByCondition(string loai, int namLamViec, int namNganSach, int nguonNganSach)
        {
            return _sktChungTuRepository.GetSoChungTuIndexByCondition(loai, namLamViec, namNganSach, nguonNganSach);
        }

        public IEnumerable<NsSktChungTu> FindByCondition(Expression<Func<NsSktChungTu, bool>> predicate)
        {
            return _sktChungTuRepository.FindAll(predicate);
        }

        public IEnumerable<NsSktChungTu> FindByCondition(int iLoai, int namLamViec, int namNganSach, int nguonNganSach, int iTrangThai, string currentIdDonVi)
        {
            return _sktChungTuRepository.FindByCondition(iLoai, namLamViec, namNganSach, nguonNganSach, iTrangThai, currentIdDonVi);
        }

        public void LockOrUnlock(Guid id, bool isLock)
        {
            var chungTu = _sktChungTuRepository.Find(id);
            chungTu.BKhoa = isLock;
            _sktChungTuRepository.Update(chungTu);
        }

        public IEnumerable<NsSktChungTu> FindByCondition(int iLoai, int namLamViec, int namNganSach, int nguonNganSach, int iTrangThai, string currentIdDonVi, string loaiDV, int loaiChungTu, string userName)
        {
            return _sktChungTuRepository.FindByCondition(iLoai, namLamViec, namNganSach, nguonNganSach, iTrangThai, currentIdDonVi, loaiDV, loaiChungTu, userName);
        }

        public IEnumerable<NsSktChungTu> FindChungTuIndexByCondition(int iLoai, int namLamViec, int namNganSach, int nguonNganSach, int loaiChungTu, string nganSach,  string userName, string proc)
        {
            return _sktChungTuRepository.FindChungTuIndexByCondition(iLoai, namLamViec, namNganSach, nguonNganSach, loaiChungTu, nganSach, userName, proc);
        }

        public IEnumerable<NsSktChungTuQuery> FindChungTuIndexByConditionOptimize(int iLoai, int namLamViec, int namNganSach, int nguonNganSach, int loaiChungTu, string userName, string proc)
        {
            return _sktChungTuRepository.FindChungTuIndexByConditionOptimize(iLoai, namLamViec, namNganSach, nguonNganSach, loaiChungTu, userName, proc);
        }
        
        public IEnumerable<NsSktChungTuQuery> FindChungTuIndexByConditionOptimizeClone(int iLoai, int namLamViec, int namNganSach, int nguonNganSach, int loaiChungTu, string userName, int loaiNguonNganSach, string proc)
        {
            return _sktChungTuRepository.FindChungTuIndexByConditionOptimizeClone(iLoai, namLamViec, namNganSach, nguonNganSach, loaiChungTu, userName, loaiNguonNganSach, proc);
        }

        public IEnumerable<NsSktChungTu> FindChungTuIndexByConditionBVTC(string iLoai, int namLamViec, int namNganSach, int nguonNganSach, int loaiChungTu, string userName, int loaiNguonNganSach, string proc)
        {
            return _sktChungTuRepository.FindChungTuIndexByConditionBVTC(iLoai, namLamViec, namNganSach, nguonNganSach, loaiChungTu, userName, loaiNguonNganSach, proc);
        }

        public bool IsExistChungTuTongHop(int loai, int namLamViec, int namNganSach, int nguonNganSach)
        {
            return _sktChungTuRepository.IsExistChungTuTongHop(loai, namLamViec, namNganSach, nguonNganSach);
        }

        public void DeletePhanBoDuToan(Guid iID_CTSoKiemTra, string iIDDonVi, int iNamLamViec)
        {
            _sktChungTuRepository.DeletePhanBoDuToan(iID_CTSoKiemTra, iIDDonVi, iNamLamViec);
        }
    }
}