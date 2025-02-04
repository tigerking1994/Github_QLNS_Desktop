using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations.Schema;

namespace VTS.QLNS.CTC.Core.Domain.Query
{
    public class VdtKhvPhanBoVonDonViQuery
    {
        public Guid Id { get; set; }
        public string sSoQuyetDinh { get; set; }
        public DateTime dNgayQuyetDinh { get; set; }
        public string sNguoiLap { get; set; }
        public string sTruongPhong { get; set; }
        public int iNamKeHoach { get; set; }
        public int iId_NguonVonId { get; set; }
        public string sTenNguonVon { get; set; }
        public Guid iID_DonViQuanLyID { get; set; }
        public string iID_MaDonViQuanLy { get; set; }
        public string sTenDonVi { get; set; }
        public Guid? iID_ParentId { get; set; }
        public bool? bIsCanBoDuyet { get; set; }
        public bool? bIsDuyet { get; set; }
        public double? fThuHoiVonUngTruoc { get; set; }
        public double? fThanhToan { get; set; }
        public string SSoLanDieuChinh { get; set; }
        public string DieuChinhTu { get; set; }
        public bool? BActive { get; set; }
        public string SUserCreate { get; set; }
        public bool? BKhoa { get; set; }
        public string STongHop { get; set; }
    }
}
