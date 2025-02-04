using System;
using System.Collections.Generic;
using System.Text;

namespace VTS.QLNS.CTC.Core.Domain.Query
{
    public class ReportBangLuongTruyLinhDongPhuCapQuery
    {
        public int Stt { get; set; }
        public string TenCanBo { get; set; }
        public string CapBac { get; set; }
        public string SoTaiKhoan { get; set; }
        public decimal NamThamNien { get; set; }
        public string NgayNhapNgu { get; set; }
        public string NgayXuatNgu { get; set; }
        public string NgayTaiNgu { get; set; }
        public decimal SoTienDuocNhan { get; set; }
        public decimal SoThangTruyLinh { get; set; }
        public decimal LuongCoBan { get; set; }
        public decimal PhuCapChucVu { get; set; }
        public decimal PhuCapCongVu { get; set; }
        public decimal PhuCapThamNien { get; set; }
        public decimal TongCong { get; set; }
        public decimal TruBHXH { get; set; }
        public List<GiaTriTruyLinhDong> LstGiaTri { get; set; }
        public List<GiaTriTruyLinhDong> LstGiaTriTotal { get; set; }
    }

    public class GiaTriTruyLinhDong
    {
        public string MaPhuCap { get; set; }
        public decimal GiaTri { get; set; }
    }
}
