using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace VTS.QLNS.CTC.Core.Domain
{
    public partial class NsQtChungTu : EntityBase
    {
        [Column("iID_QTChungTu")]
        public override Guid Id { get; set; }
        public string SSoChungTu { get; set; }
        public int? ISoChungTuIndex { get; set; }
        public DateTime? DNgayChungTu { get; set; }
        public string SMoTa { get; set; }
        public string IIdMaDonVi { get; set; }
        public string SDslns { get; set; }
        public string SLoai { get; set; }
        public int IThangQuyLoai { get; set; }
        public int IThangQuy { get; set; }
        public int? ILoaiChungTu { get; set; }
        public int? ILanDieuChinh { get; set; }
        public string SThangQuyMoTa { get; set; }
        public double FTongTuChiDeNghi { get; set; }
        public double FTongTuChiPheDuyet { get; set; }
        public int INamNganSach { get; set; }
        public int IIdMaNguonNganSach { get; set; }
        public int? INamLamViec { get; set; }
        public bool BKhoa { get; set; }
        public DateTime? DNgayTao { get; set; }
        public string SNguoiTao { get; set; }
        public DateTime? DNgaySua { get; set; }
        public string SNguoiSua { get; set; }
        public string STongHop { get; set; }
        public bool? BDaTongHop { get; set; }
        [NotMapped]
        public List<NsQtChungTuChiTiet> Details { get; set; }
        [NotMapped]
        public bool ImportStatus { get; set; }
        public bool? IsSent { get; set; }
    }
}
