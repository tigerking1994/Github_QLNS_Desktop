using System;
using System.ComponentModel.DataAnnotations;

namespace VTS.QLNS.CTC.Core.Domain
{
    public class BhThamDinhQuyetToan : EntityBase
    {
        [Key]
        public override Guid Id { get; set; }
        public int INamLamViec { get; set; }
        public string IID_MaDonVi { get; set; }
        public double FSoBaoCao { get; set; }
        public double FSoThamDinh { get; set; }
        public double FQuanNhan { get; set; }
        public double FCNVLDHD { get; set; }
        public string SSoChungTu { get; set; }
        public DateTime? DNgayChungTu { get; set; }
        public bool BDaTongHop { get; set; }
        public string STongHop { get; set; }
        public bool BKhoa { get; set; }
        public string SNguoiTao { get; set; }
        public string SNguoiSua { get; set; }
        public DateTime? DNgayTao { get; set; }
        public DateTime? DNgaySua { get; set; }
        public string SMoTa { get; set; }
        public string SGiaiThichChenhLech { get; set; }
    }
}
