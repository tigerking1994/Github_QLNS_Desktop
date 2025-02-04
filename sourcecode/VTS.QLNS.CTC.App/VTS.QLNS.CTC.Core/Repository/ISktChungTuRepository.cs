using System;
using System.Collections.Generic;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;

namespace VTS.QLNS.CTC.Core.Repository
{
    public interface ISktChungTuRepository : IRepository<NsSktChungTu>
    {
        IEnumerable<NsSktChungTu> FindByCondition(int iLoai, int namLamViec, int namNganSach, int nguonNganSach, int iTrangThai, string currentIdDonVi);
        void LockOrUnLock(string id, bool isLock);
        NsSktChungTu FindById(Guid id);
        int GetSoChungTuIndexByCondition(string loai, int namLamViec, int namNganSach, int nguonNganSach);
        IEnumerable<NsSktChungTu> FindByCondition(int iLoai, int namLamViec, int namNganSach, int nguonNganSach, int iTrangThai, string currentIdDonVi, string loaiDV, int loaiChungTu, string userName);
        IEnumerable<NsSktChungTu> FindChungTuIndexByCondition(int iLoai, int namLamViec, int namNganSach, int nguonNganSach, int loaiChungTu, string nganSach, string userName, string proc);
        IEnumerable<NsSktChungTuQuery> FindChungTuIndexByConditionOptimize(int iLoai, int namLamViec, int namNganSach, int nguonNganSach, int loaiChungTu, string userName, string proc);
        IEnumerable<NsSktChungTuQuery> FindChungTuIndexByConditionOptimizeClone(int iLoai, int namLamViec, int namNganSach, int nguonNganSach, int loaiChungTu, string userName, int loaiNguonNganSach, string proc);
        IEnumerable<NsSktChungTu> FindChungTuIndexByConditionBVTC(string iLoai, int namLamViec, int namNganSach, int nguonNganSach, int loaiChungTu, string userName, int loaiNguonNganSach, string proc);
        bool IsExistChungTuTongHop(int loai, int namLamViec, int namNganSach, int nguonNganSach);
        void DeletePhanBoDuToan(Guid iID_CTSoKiemTra, string iIDDonVi, int iNamLamViec);
    }
}
