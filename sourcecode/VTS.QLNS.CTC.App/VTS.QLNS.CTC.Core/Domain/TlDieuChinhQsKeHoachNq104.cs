using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VTS.QLNS.CTC.Core.Domain
{
    public partial class TlDieuChinhQsKeHoachNq104 : EntityBase
    {
        public string MaDonVi { get; set; }
        public string TenDonVi { get; set; }
        public int? Thang { get; set; }
        public int? Nam { get; set; }
        public int? TangTuyenSinh { get; set; }
        public int? TangNhapNgu { get; set; }
        public int? GiamXuatNgu { get; set; }
        public int? GiamHuuTri { get; set; }
        public DateTime? DNgayTao { get; set; }
        public string SNguoiTao { get; set; }
        public DateTime? DNgaySua { get; set; }
        public string SNguoiSua { get; set; }
        public double? LuongTuyenSinh { get; set; }
        public double? PhuCapXuatNgu { get; set; }
    }
}
