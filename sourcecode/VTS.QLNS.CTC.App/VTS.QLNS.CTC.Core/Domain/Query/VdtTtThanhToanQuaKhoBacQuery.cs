using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations.Schema;

namespace VTS.QLNS.CTC.Core.Domain.Query
{
    public class VdtTtThanhToanQuaKhoBacQuery
    {
        public Guid Id { get; set; }
        public string sNguoiLap { get; set; }
        public string sSoThanhToan { get; set; }
        public DateTime? dNgayThanhToan { get; set; }
        public Guid? iID_DonViNhanThanhToanID { get; set; }
        public Guid? iID_NhomQuanLyID { get; set; }
        public Guid? iID_DonViQuanLyID { get; set; }
        public int iNamKeHoach { get; set; }
        public Guid? iID_LoaiNguonVonID { get; set; }
        public Guid? iID_LoaiNganSach { get; set; }
        public Guid iID_KhoanNganSach { get; set; }
        public double? fGiaTriThanhToan { get; set; }
        public double? fGiaTriTamUng { get; set; }
        public Guid? iID_DonViTienTeID { get; set; }
        public double? fTiGiaDonVi { get; set; }
        public Guid? iID_TienTeID { get; set; }
        public double? fTiGia { get; set; }
        public string sGhiChu { get; set; }
        public string sUserCreate { get; set; }
        public DateTime? dDateCreate { get; set; }
        public string sUserUpdate { get; set; }
        public DateTime? dDateUpdate { get; set; }
        public string sUserDelete { get; set; }
        public DateTime? dDateDelete { get; set; }
        public string iId_MaDonViNhanThanhToanID { get; set; }
        public string iId_MaDonViQuanLyID { get; set; }
        public int? iID_NguonVonID { get; set; }
        public string sTenNguonVon { get; set; }
        public string sLoaiNguonVon { get; set; }
        public string sTenDonVi { get; set; }
    }
}
