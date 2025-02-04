using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations.Schema;

namespace VTS.QLNS.CTC.Core.Domain.Query
{
    public class VdtTtDeNghiThanhToanUngQuery
    {
        public Guid Id { get; set; }
        public string sSoDeNghi { get; set; }
        public DateTime? dNgayDeNghi { get; set; }
        public Guid? iID_DonViQuanLyID { get; set; }
        public string iID_MaDonViQuanLy { get; set; }
        public Guid? iID_NhomQuanLyID { get; set; }
        public string sNguoiLap { get; set; }
        public int? iNamKeHoach { get; set; }
        public int? iID_NguonVonID { get; set; }
        public Guid? iID_LoaiNganSach { get; set; }
        public Guid? iID_KhoanNganSach { get; set; }
        public double? fGiaTriThanhToan { get; set; }
        public double? fGiaTriTamUng { get; set; }
        public double? fGiaTriThuHoiUngNgoaiChiTieu { get; set; }
        public double? fGiaTriThuHoi { get; set; }
        public string sGhiChu { get; set; }
        public string sUserCreate { get; set; }
        public DateTime? dDateCreate { get; set; }
        public string sTenDonVi { get; set; }
    }
}
