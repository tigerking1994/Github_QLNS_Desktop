using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;

namespace VTS.QLNS.CTC.Core.Service
{
    public interface INhDaDuToanService
    {
        void Add(NhDaDuToan entity);
        void Update(NhDaDuToan entity);
        void Adjust(NhDaDuToan entity);
        void Delete(NhDaDuToan entity);
        void LockOrUnlock(Guid id, bool isLock);
        NhDaDuToan FindById(Guid id);
        IEnumerable<NhDaDuToanQuery> FindIndex(int namLamViec, int iLoai);
        bool CheckDuplicateSoQD(string soQuyetDinh, Guid id);
        List<NhDaDuToan> FindDuAnInKhlcNhaThau(Guid iIdKhlcntId, Guid iIdDuAnID);
        List<NhDaDuToan> FindDuAnInKhlcNhaThauID( Guid iIdDuAnID);
        IEnumerable<NhDaDetailNguonVonQuery> GetNguonVonByDuToanId(Guid iIdDuToanId);
        IEnumerable<NhDaDetailChiPhiQuery> GetChiPhiByDuToanId(Guid iIdDuToanId);
        IEnumerable<NhDaDetailHangMucQuery> GetHangMucByDuToanId(Guid iIdDuToanId);
        IEnumerable<NhDaDuToan> FindDuToanByIdDonVi(string maDonVi, int iLoai);
        IEnumerable<NhDaDuToan> FindDuToanMoTaMSByIdDonVi(string maDonVi, int iLoai);
    }
}
