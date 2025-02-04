using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VTS.QLNS.CTC.Core.Domain
{
    [Table("BH_DTT_BHXH_PhanBo_ChungTu")]
    public partial class BhDtPhanBoChungTu : EntityBase
    {
        [Column("iID_DTT_BHXH_PhanBo_ChungTu")]
        [Key]
        public override Guid Id { get; set; }
        public string SSoChungTu { get; set; }
        public int? ISoChungTuIndex { get; set; }
        public DateTime? DNgayChungTu { get; set; }
        public string SSoQuyetDinh { get; set; }
        public DateTime? DNgayQuyetDinh { get; set; }
        public string SMoTa { get; set; }
        public string SDsidMaDonVi { get; set; }
        public string SDslns { get; set; }
        public int? ILoaiDuToan { get; set; }
        public int? INamLamViec { get; set; }
        public string IIdDotNhan { get; set; }
        public double? FThuBHXHNLD { get; set; }
        public double? FThuBHXHNSD { get; set; }
        public double? FTongBHXH { get; set; }
        public double? FThuBHYTNLD { get; set; }
        public double? FThuBHYTNSD { get; set; }
        public double? FTongBHYT { get; set; }
        public double? FThuBHTNNLD { get; set; }
        public double? FThuBHTNNSD { get; set; }
        public double? FTongBHTN { get; set; }
        public double? FTongDuToan { get; set; }
        public bool BKhoa { get; set; }
        public DateTime? DNgayTao { get; set; }
        public string SNguoiTao { get; set; }
        public DateTime? DNgaySua { get; set; }
        public string SNguoiSua { get; set; }
        public bool? BLuongNhanDuLieu { get; set; }
        public string SDonViNhanDuLieu { get; set; }
        [NotMapped]
        public ICollection<BhDtPhanBoChungTuChiTiet> ChungTuChiTiets { get; set; }
        [NotMapped]
        public bool ImportStatus { get; set; }
        [NotMapped]
        public List<BhDtPhanBoChungTuChiTiet> ListDetailChiTiet { get; set; }
        [NotMapped]
        public bool IsHasDttData => FThuBHXHNLD != 0 || FThuBHXHNSD != 0 || FThuBHYTNLD != 0 || FThuBHYTNSD != 0 || FThuBHTNNLD != 0 | FThuBHTNNSD != 0;
    }
}
