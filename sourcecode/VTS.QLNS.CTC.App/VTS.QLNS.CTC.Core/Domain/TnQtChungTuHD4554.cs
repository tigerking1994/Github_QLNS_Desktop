using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VTS.QLNS.CTC.Core.Domain
{
    [Table("TN_QuyetToan_ChungTu_HD4554")]
    public class TnQtChungTuHD4554 : EntityBase
    {
        [Column("Id")]
        [Key]
        public Guid Id { get; set; }
        [Column("iThangQuy")]
        public int? IThangQuy { get; set; }
        [Column("iThangQuyLoai")]
        public int? IThangQuyLoai { get; set; }
        [Column("sThangQuy_MoTa")]
        public string SThangQuyMoTa { get; set; }
        [Column("bDaTongHop")]
        public bool? BDaTongHop { get; set; }
        [Column("sTongHop")]
        public string STongHop { get; set; }
        [Column("fTongSoTien")]
        public double? FTongSoTien { get; set; }
        [Column("iID_MaDonVi")]
        public string IIdMaDonVi { get; set; }
        [Column("iNamLamViec")]
        public int? INamLamViec { get; set; }
        [Column("sSoChungTu")]
        public string SSoChungTu { get; set; }
        [Column("bKhoa")]
        public bool? BKhoa { get; set; }
        [Column("dNgayChungTu")]
        public DateTime? DNgayChungTu { get; set; }
        [Column("sNguoiTao")]
        public string SNguoiTao { get; set; }
        [Column("sNguoiSua")]
        public string SNguoiSua { get; set; }
        [Column("dNgaySua")]
        public DateTime? DNgaySua { get; set; }
        [Column("dNgayTao")]
        public DateTime? DNgayTao { get; set; }
        [Column("sMoTa")]
        public string SMoTa { get; set; }
        [Column("bSent")]
        public bool? BSent { get; set; }
        [Column("iSoChungTuIndex")]
        public int? ISoChungTuIndex { get; set; }
        [Column("sDSLNS")]
        public string SDSLNS { get; set; }
        [Column("iNguonNganSach")]
        public int? INguonNganSach { get; set; }
        [Column("iNamNganSach")]
        public int? INamNganSach { get; set; }
    }
}
