using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VTS.QLNS.CTC.Core.Domain
{
    public partial class BhCpBsChungTu : EntityBase
    {
        [Column("iID_CTCapPhatBS")]
        [Key]
        public override Guid Id { get; set; }
        public string SSoChungTu { get; set; }
        public int? ISoChungTuIndex { get; set; }
        public DateTime? DNgayChungTu { get; set; }
        public string SSoQuyetDinh { get; set; }
        public DateTime? DNgayQuyetDinh { get; set; }
        public string SMoTa { get; set; }
        public string IIDMaDonVi { get; set; }
        public string SDslns { get; set; }
        public int? INamLamViec { get; set; }
        public DateTime? DNgayTao { get; set; }
        public string SNguoiTao { get; set; }
        public DateTime? DNgaySua { get; set; }
        public string SNguoiSua { get; set; }
        public bool BKhoa { get; set; }
        public double? FTongDaQuyetToan { get; set; }
        public double? FTongDaCapUng { get; set; }
        public double? FTongThuaThieu { get; set; }
        public double? FTongSoCapBoSung { get; set; }
        public string SDSSoChungTuTongHop { get; set; }
        public bool? BDaTongHop { get; set; }
        public string SCoSoYTe { get; set; }
        public int? IQuy { get; set; }
        public string NChiTietToi { get; set; }
        public int? ILoaiTongHop { get; set; }
        public int? ILoaiKinhPhi { get; set; }
        [NotMapped]
        public string SMoTaBaoCao => string.Format("{0}: {1}", SSoChungTu, SMoTa);
    }
}
