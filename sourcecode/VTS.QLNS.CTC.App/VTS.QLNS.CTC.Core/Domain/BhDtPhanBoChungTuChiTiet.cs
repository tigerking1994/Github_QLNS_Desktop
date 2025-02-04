using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VTS.QLNS.CTC.Core.Domain
{
    [Table("BH_DTT_BHXH_PhanBo_ChungTuChiTiet")]
    public partial class BhDtPhanBoChungTuChiTiet : EntityBase
    {
        [Column("iID_DTT_BHXH_ChungTu_ChiTiet")]
        [Key]
        public override Guid Id { get; set; }
        public Guid? IIdDttBHXH { get; set; }
        public Guid? IIdMlns { get; set; }
        public Guid? IIdMlnsCha { get; set; }
        public string SXauNoiMa { get; set; }
        public string SLns { get; set; }
        public string SL { get; set; }
        public string SK { get; set; }
        public string SM { get; set; }
        public string STm { get; set; }
        public string STtm { get; set; }
        public string SNg { get; set; }
        public string STng { get; set; }
        public string STng1 { get; set; }
        public string STng2 { get; set; }
        public string STng3 { get; set; }
        public string SMoTa { get; set; }
        public int? INamLamViec { get; set; }
        public string IIdMaDonVi { get; set; }
        public double? FBHXHNLD { get; set; }
        public double? FBHXHNSD { get; set; }
        public double? FThuBHXH { get; set; }
        public double? FBHYTNLD { get; set; }
        public double? FBHYTNSD { get; set; }
        public double? FThuBHYT { get; set; }
        public double? FBHTNNLD { get; set; }
        public double? FBHTNNSD { get; set; }
        public double? FThuBHTN { get; set; }
        public double? FTongCong { get; set; }
        public Guid? IIdCtduToanNhan { get; set; }
        public int? IDuLieuNhan { get; set; }
        public int? IPhanCap { get; set; }
        public DateTime? DNgayTao { get; set; }
        public string SNguoiTao { get; set; }
        public DateTime? DNgaySua { get; set; }
        public string SNguoiSua { get; set; }
        [NotMapped]
        public bool ImportStatus { get; set; }
        [NotMapped]
        public bool IsHasDttData => FBHXHNLD != 0 || FBHXHNSD != 0 || FBHYTNLD != 0 || FBHYTNSD != 0 || FBHTNNSD != 0 | FBHTNNLD != 0;
    }
}
