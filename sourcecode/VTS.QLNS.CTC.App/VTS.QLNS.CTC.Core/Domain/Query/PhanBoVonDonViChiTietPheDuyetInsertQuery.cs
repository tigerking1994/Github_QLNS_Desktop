using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VTS.QLNS.CTC.Core.Domain.Query
{
    public class PhanBoVonDonViChiTietPheDuyetInsertQuery
    {
        public Guid? iID_PhanBoVonID { get; set; }
        public Guid? iID_DuAnID { get; set; }
        public string sTrangThaiDuAnDangKy { get; set; }
        public double? fGiaTrDeNghi { get; set; }
        public double? fGiaTrPhanBo { get; set; }
        public double? fGiaTriThuHoi { get; set; }
        public Guid? iID_DonViTienTeID { get; set; }
        public Guid? iID_TienTeID { get; set; }
        public double? fTiGiaDonVi { get; set; }
        public double? fTiGia { get; set; }
        public string sGhiChu { get; set; }
        public Guid? iID_LoaiNguonVonID { get; set; }
        public string sLNS { get; set; }
        public string sL { get; set; }
        public string sK { get; set; }
        public string sM { get; set; }
        public string sTM { get; set; }
        public string sTTM { get; set; }
        public string sNG { get; set; }
        public double? fCapPhatTaiKhoBac { get; set; }
        public double? fCapPhatBangLenhChi { get; set; }
        public double? fGiaTriThuHoiNamTruocKhoBac { get; set; }
        public double? fGiaTriThuHoiNamTruocLenhChi { get; set; }
        public double? fCapPhatTaiKhoBacDc { get; set; }
        public double? fCapPhatBangLenhChiDc { get; set; }
        public double? fGiaTriThuHoiNamTruocKhoBacDc { get; set; }
        public double? fGiaTriThuHoiNamTruocLenhChiDc { get; set; }
        public Guid? IIdParent { get; set; }
        public int? ILoaiDuAn { get; set; }
        public Guid? IIdLoaiCongTrinh { get; set; }
        public double? fThanhToanDeXuat { get; set; }
        public Guid? IID_DuAn_HangMucID { get; set; }
    }
}
