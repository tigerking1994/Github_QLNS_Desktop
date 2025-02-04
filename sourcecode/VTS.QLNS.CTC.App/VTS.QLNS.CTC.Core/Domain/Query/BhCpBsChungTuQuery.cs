using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VTS.QLNS.CTC.Core.Domain.Query
{
    public class BhCpBsChungTuQuery
    {
        public Guid Id { get; set; }
        public string SSoChungTu { get; set; }
        public int? ISoChungTuIndex { get; set; }
        public DateTime? DNgayChungTu { get; set; }
        public string SSoQuyetDinh { get; set; }
        public DateTime? DNgayQuyetDinh { get; set; }
        public string SMoTa { get; set; }
        public string IdDonVi { get; set; }
        public string TenDonVi { get; set; }
        public string Lns { get; set; }
        public string ChiTietToi { get; set; }
        public int? INamLamViec { get; set; }
        public DateTime? DNgayTao { get; set; }
        public DateTime? DNgaySua { get; set; }
        public string SNguoiTao { get; set; }
        public string SNguoiSua { get; set; }
        public bool BKhoa { get; set; }
        public double? FTongDaQuyetToan { get; set; }
        public double? FTongDaCapUng { get; set; }
        public double? FTongThuaThieu { get; set; }
        public double? FTongSoCapBoSung { get; set; }
        public string DSSoChungTuTongHop { get; set; }
        public bool? BDaTongHop { get; set; }
        public int? ILoaiTongHop { get; set; }
        public string IIDMaDonVi { get; set; }
    }
}
