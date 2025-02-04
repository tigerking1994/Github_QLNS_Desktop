using System;
using System.Collections.Generic;
using System.Text;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;

namespace VTS.QLNS.CTC.Core.Repository
{
    public interface IVdtThongTriChiTietRepository : IRepository<VdtThongTriChiTiet>
    {
        IEnumerable<VdtThongTriChiTietQuery> GetVdtThongTriChiTiet(Guid iIdThongTri, string sMaDonVi, int iLoaiThongTri, int iNamKeHoach, DateTime dNgayThongTri,
            string sMaNguonVon);
        IEnumerable<VdtThongTriChiTietQuery> GetVdtThongTriQuyetToanChiTiet(string sMaDonVi, int iNamKeHoach, DateTime dNgayThongTri,
            string sMaNguonVon, DateTime? dNgayLapGanNhat, string sMaLoaiCongTrinh);
        IEnumerable<VdtThongTriChiTietQuery> GetVdtThongTriChiTietByParentId(Guid iId);
        void DeleteThongTriChiTietByParentId(Guid iIdThongTriId);
        IEnumerable<VdtCanCuThanhToanQuery> GetCanCuThanhToanByThongTri(Guid iIdThongTri, bool bIsThanhToan, string sMaDonVi, int iNamLamViec, int iNguonVon, DateTime dNgayLap);
        IEnumerable<VdtThongTriChiTietQuery> GetVdtThongTriChiTietByPheDuyet();
        IEnumerable<VdtThongTriChiTietQuery> FindByIdThongTri(Guid idThongTriId);
        IEnumerable<VdtThongTriQuyetToanQuery> GetVdtThongTriQuyetToan(Guid iIdQuyetToanId);
        IEnumerable<VdtThongTriQuyetToanQuery> GetVdtThongTriQuyetToanById(Guid iIdThongTri);
    }
}
