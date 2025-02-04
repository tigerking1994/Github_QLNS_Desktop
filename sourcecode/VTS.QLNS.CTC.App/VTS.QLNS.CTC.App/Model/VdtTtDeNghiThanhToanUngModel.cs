using System;
using System.Collections.Generic;
using System.Text;

namespace VTS.QLNS.CTC.App.Model
{
    public class VdtTtDeNghiThanhToanUngModel : BindableBase
    {
        public Guid Id { get; set; }
        public int iRowIndex { get; set; }
        public string sSoDeNghi { get; set; }
        public DateTime? dNgayDeNghi { get; set; }
        public Guid? iId_DonViQuanLyId { get; set; }
        public string iID_MaDonViQuanLy { get; set; }
        public Guid? iId_NhomQuanLyId { get; set; }
        public string sNguoiLap { get; set; }
        public int? iNamKeHoach { get; set; }
        public int? iId_NguonVonId { get; set; }
        public Guid? iId_LoaiNganSach { get; set; }
        public Guid? iId_KhoanNganSach { get; set; }
        public double? fGiaTriThanhToan { get; set; }
        public double? fGiaTriTamUng { get; set; }
        public double? fGiaTriThuHoiUngNgoaiChiTieu { get; set; }
        public double? fGiaTriThuHoi { get; set; }
        public string sGhiChu { get; set; }
        public string sUserCreate { get; set; }
        public DateTime? dDateCreate { get; set; }
        public string sUserUpdate { get; set; }
        public string sTenDonVi { get; set; }
        public List<Guid> lstDuAnId { get; set; }
        public bool IsEdit { get; set; }
    }
}
