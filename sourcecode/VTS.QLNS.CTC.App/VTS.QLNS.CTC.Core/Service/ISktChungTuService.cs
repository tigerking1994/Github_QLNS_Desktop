using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;

namespace VTS.QLNS.CTC.Core.Service
{
    public interface ISktChungTuService
    {
        IEnumerable<NsSktChungTu> FindByCondition(Expression<Func<NsSktChungTu, bool>> predicate);
        IEnumerable<NsSktChungTu> FindByCondition(int iLoai, int namLamViec, int namNganSach, int nguonNganSach, int iTrangThai, string currentIdDonVi);
        void LockOrUnlock(Guid id, bool isLock);
        NsSktChungTu FindById(Guid id);
        int Add(NsSktChungTu entity);
        int Delete(NsSktChungTu item);
        int Update(NsSktChungTu item);
        void BulkInsert(List<NsSktChungTu> lstData);
        IEnumerable<NsSktChungTu> FindByCondition(int iLoai, int namLamViec, int namNganSach, int nguonNganSach, int iTrangThai, string currentIdDonVi, string loaiDV, int loaiChungTu, string userName);
        IEnumerable<NsSktChungTu> FindChungTuIndexByCondition(int iLoai, int namLamViec, int namNganSach, int nguonNganSach, int loaiChungTu, string nganSach, string userName, string proc);
        IEnumerable<NsSktChungTuQuery> FindChungTuIndexByConditionOptimize(int iLoai, int namLamViec, int namNganSach, int nguonNganSach, int loaiChungTu, string userName, string proc);
        IEnumerable<NsSktChungTuQuery> FindChungTuIndexByConditionOptimizeClone(int iLoai, int namLamViec, int namNganSach, int nguonNganSach, int loaiChungTu, string userName,int loaiNguonNganSach, string proc);
        IEnumerable<NsSktChungTu> FindChungTuIndexByConditionBVTC(string iLoai, int namLamViec, int namNganSach, int nguonNganSach, int loaiChungTu, string userName, int loaiNguonNganSach, string proc);
        int GetSoChungTuIndexByCondition(string loai, int namLamViec, int namNganSach, int nguonNganSach);
        bool IsExistChungTuTongHop(int loai, int namLamViec, int namNganSach, int nguonNganSach);
        void DeletePhanBoDuToan(Guid iID_CTSoKiemTra, string iIDDonVi, int iNamLamViec);
    }
}
