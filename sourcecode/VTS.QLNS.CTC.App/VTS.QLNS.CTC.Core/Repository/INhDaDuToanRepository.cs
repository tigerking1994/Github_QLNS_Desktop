using System;
using System.Collections.Generic;
using VTS.QLNS.CTC.Core.Domain.Query;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VTS.QLNS.CTC.Core.Domain;

namespace VTS.QLNS.CTC.Core.Repository
{
    public interface INhDaDuToanRepository : IRepository<NhDaDuToan>
    {
        IEnumerable<NhDaDuToanQuery> FindIndex(int namLamViec, int iLoai);
        IEnumerable<NhDaDuToan> FindDuToanByIdDonVi(string maDonVi, int iLoai);
        IEnumerable<NhDaDuToan> FindDuToanMoTaMSByIdDonVi(string maDonVi, int iLoai);
        void DeleteDuToanChiTiet(Guid id);
        bool CheckDuplicateSoQD(string soQuyetDinh, Guid id);
        IEnumerable<NhDaDuToanNguonVonQuery> FindListDuToanNguonVonByDuAn(Guid duAnId);
        IEnumerable<NhDaDuToanNguonVonQuery> FindListDuToanNguonVonByDuToanId(Guid duToanId);
        IEnumerable<NhDaDuToanChiPhiQuery> FindListDuToanChiPhiByDuAn(Guid duAnId);
        IEnumerable<NhDaDuToanChiPhiQuery> FindListDuToanChiPhiByDuToanId(Guid duToanId);
        IEnumerable<NhDaDuToanHangMucQuery> FindListHangMucAllDetail(Guid duToanId);
        IEnumerable<NhDaDuToanHangMucQuery> GetQdDauTuHangMucByQdDautuChiPhiId(Guid qdDauTuChiPhiId);
        IEnumerable<NhDaDuToanHangMucQuery> GetDuToanTuHangMucByDuToanChiPhiId(Guid duToanChiPhiId);
        List<NhDaDuToan> FindDuAnInKhlcNhaThau(Guid iIdKhlcntId, Guid iIdDuAnID);
        List<NhDaDuToan> FindDuAnInKhlcNhaThauID( Guid iIdDuAnID);
    }
}
