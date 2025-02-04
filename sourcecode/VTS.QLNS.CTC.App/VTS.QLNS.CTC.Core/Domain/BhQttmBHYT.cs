using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VTS.QLNS.CTC.Core.Domain
{
    [Table("BH_QTTM_BHYT_Chung_Tu")]
    public partial class BhQttmBHYT : EntityBase
    {
        [Column("iID_QTTM_BHYT_ChungTu")]
        [Key]
        public override Guid Id { get; set; }
        public string SSoChungTu { get; set; }
        public int? INamLamViec { get; set; }
        public DateTime DNgayChungTu { get; set; }
        public string IIDMaDonVi { get; set; }
        public string SMoTa { get; set; }
        public bool BIsKhoa { get; set; }
        public int IQuyNam { get; set; }
        public int IQuyNamLoai { get; set; }
        public string SQuyNamMoTa { get; set; }
        public DateTime? DNgayTao { get; set; }
        public DateTime? DNgaySua { get; set; }
        public string SNguoiTao { get; set; }
        public string SNguoiSua { get; set; }
        public string STongHop { get; set; }
        public string SDsMlns { get; set; }
        public int? ILoaiTongHop { get; set; }
        public bool? BDaTongHop { get; set; }
        public double? FDuToan { get; set; }
        public double? FDaQuyetToan { get; set; }
        public double? FConLai { get; set; }
        public double? FSoPhaiThu { get; set; }
    }
}
