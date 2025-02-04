using System;
using System.Collections.Generic;
using System.Text;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;

namespace VTS.QLNS.CTC.Core.Service
{
    public interface IVdtThongTriService
    {
        IEnumerable<VdtThongTriQuery> GetVdtThongTriIndex(Guid iIdLoaiThongTri, int openFromPheDuyetThanhToan);
        void DeleteThongTriThanhToan(VdtThongTri data);
        void Insert(VdtThongTri data, string sUserLogin);
        void Update(VdtThongTri data, string sUserLogin);
        IEnumerable<VdtDmLoaiThongTri> GetAllDmLoaiThongTri();
        IEnumerable<VdtThongTriChiTietQuery> GetVdtThongTriChiTiet(Guid iIdThongTri, string sMaDonVi, int iLoaiThongTri, int iNamKeHoach, DateTime dNgayThongTri,
            string sMaNguonVon, DateTime? dNgayLapGanNhat, string sMaLoaiCongTrinh = "");
        IEnumerable<VdtDmKieuThongTri> GetAllKieuThongTri();
        void InsertThongTriChiTiet(List<VdtThongTriChiTiet> lstData);
        void DeleteThongTriChiTietByParentId(Guid iIdThongTriId);
        IEnumerable<VdtThongTriChiTietQuery> GetVdtThongTriChiTietByParentId(Guid iId);
        IEnumerable<VdtThongTriChiTietQuery> GetVdtThongTriChiTietByPheDuyet();
        IEnumerable<VdtThongTriChiTietQuery> FindByIdThongTri(Guid idThongTriId);
        IEnumerable<VdtCanCuThanhToanQuery> GetCanCuThanhToanByThongTri(Guid iIdThongTri, bool bIsThanhToan, string sMaDonVi, int iNamLamViec, int iNguonVon, DateTime dNgayLap);
        List<VdtThongTriQuyetToanQuery> GetVdtThongTriQuyetToanById(Guid iIdThongTri);
        List<VdtThongTriQuyetToanQuery> GetVdtThongTriQuyetToanChiTiet(Guid iIdQuyetToanId);
    }
}
