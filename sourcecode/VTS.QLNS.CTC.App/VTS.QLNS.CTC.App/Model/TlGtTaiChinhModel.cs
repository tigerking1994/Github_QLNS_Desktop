using System;
using System.Collections.Generic;
using System.Text;

namespace VTS.QLNS.CTC.App.Model
{
    public class TlGtTaiChinhModel : ModelBase
    {
        public string MaCanBo { get; set; }
        public string TenCanBo { get; set; }
        public int Thang { get; set; }
        public int Nam { get; set; }
        public string MaCb { get; set; }
        public string MaCv { get; set; }
        public string TenCv { get; set; }
        public DateTime? NgayNn { get; set; }
        public DateTime? NgayXn { get; set; }
        public DateTime? NgayTn { get; set; }
        public string SoSoLuong { get; set; }
        public string NoiChuyenDen { get; set; }
        public string SoQd { get; set; }
        public DateTime? NgayKyQd { get; set; }
        public int CapPhatTiepThang { get; set; }
        public int CapPhatTiepNam { get; set; }
        public int? LoPhiDuocCap { get; set; }
        public int? LoPhiThanhToan { get; set; }
        public DateTime? NgayKy { get; set; }
        public string MaGiayGttc { get; set; }
        public string TenCapBac { get; set; }
        public string SoTaiKhoan { get; set; }
        public string NganHang { get; set; }
    }
}
