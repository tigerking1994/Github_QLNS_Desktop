using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace VTS.QLNS.CTC.Core.Domain.Query
{
    public class BhKhtmBHYTQuery
    {
        [Column("iID_KHTM_BHYT")]
        public Guid Id { get; set; }
        public string SSoChungTu { get; set; }
        public int? INamLamViec { get; set; }
        public string IIDMaDonVi { get; set; }
        public Guid? IIDDonVi { get; set; }
        public string STenDonVi { get; set; }
        public string SMoTa { get; set; }
        public bool BKhoa { get; set; }
        public bool? BDaTongHop { get; set; }
        public double? FTongKeHoach { get; set; }
        public string SSoQuyetDinh { get; set; }
        public DateTime? DNgayQuyetDinh { get; set; }
        public DateTime? DNgayTao { get; set; }
        public DateTime? DNgaySua { get; set; }
        public DateTime? DNgayChungTu { get; set; }
        public string SNguoiTao { get; set; }
        public string SNguoiSua { get; set; }
        public string STongHop { get; set; }
        public Guid? IIDTongHopID { get; set; }
        public int? ILoaiTongHop { get; set; }
        public int? ITongSoNguoi { get; set; }
        public int? ITongSoThang { get; set; }
        public double? FTongDinhMuc { get; set; }
        public double? FTongThanhTien { get; set; }
    }
}
