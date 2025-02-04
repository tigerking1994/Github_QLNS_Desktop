using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VTS.QLNS.CTC.Core.Domain
{
    [Table("BH_DTT_BHXH_DieuChinh")]
    public class BhDcDuToanThu : EntityBase
    {
        [Column("iID_DTT_BHXH_DieuChinh")]
        [Key]
        public override Guid Id { get; set; }
        public string SSoChungTu { get; set; }
        public DateTime DNgayChungTu { get; set; }
        public int? INamLamViec { get; set; }
        [Column("iID_DonVi")]
        public Guid? IIdDonViId { get; set; }
        public string IIDMaDonVi { get; set; }
        public string SMoTa { get; set; }
        public string SLNS { get; set; }
        public double? FThuBHXHNLD { get; set; }
        public double? FThuBHXHNSD { get; set; }
        public double? FThuBHYTNLD { get; set; }
        public double? FThuBHYTNSD { get; set; }
        public double? FThuBHTNNLD { get; set; }
        public double? FThuBHTNNSD { get; set; }
        public double? FThuBHXHNLDQTDauNam { get; set; }
        public double? FThuBHXHNSDQTDauNam { get; set; }
        public double? FThuBHYTNLDQTDauNam { get; set; }
        public double? FThuBHYTNSDQTDauNam { get; set; }
        public double? FThuBHTNNLDQTDauNam { get; set; }
        public double? FThuBHTNNSDQTDauNam { get; set; }
        public double? FThuBHXHNLDQTCuoiNam { get; set; }
        public double? FThuBHXHNSDQTCuoiNam { get; set; }
        public double? FThuBHYTNLDQTCuoiNam { get; set; }
        public double? FThuBHYTNSDQTCuoiNam { get; set; }
        public double? FThuBHTNNLDQTCuoiNam { get; set; }
        public double? FThuBHTNNSDQTCuoiNam { get; set; }
        public double? FTongThuBHXHNLD { get; set; }
        public double? FTongThuBHXHNSD { get; set; }
        public double? FTongThuBHYTNLD { get; set; }
        public double? FTongThuBHYTNSD { get; set; }
        public double? FTongThuBHTNNLD { get; set; }
        public double? FTongThuBHTNNSD { get; set; }
        public double? FTongCong { get; set; }
        public double? FThuBHXHNLDTang { get; set; }
        public double? FThuBHXHNSDTang { get; set; }
        public double? FThuBHXHTang { get; set; }
        public double? FThuBHYTNLDTang { get; set; }
        public double? FThuBHYTNSDTang { get; set; }
        public double? FThuBHYTTang { get; set; }
        public double? FThuBHTNNLDTang { get; set; }
        public double? FThuBHTNNSDTang { get; set; }
        public double? FThuBHTNTang { get; set; }
        public double? FThuBHXHNLDGiam { get; set; }
        public double? FThuBHXHNSDGiam { get; set; }
        public double? FThuBHXHGiam { get; set; }
        public double? FThuBHYTNLDGiam { get; set; }
        public double? FThuBHYTNSDGiam { get; set; }
        public double? FThuBHYTGiam { get; set; }
        public double? FThuBHTNNLDGiam { get; set; }
        public double? FThuBHTNNSDGiam { get; set; }
        public double? FThuBHTNGiam { get; set; }
        public bool? BDaTongHop { get; set; }
        public string STongHop { get; set; }
        public Guid? IIDTongHopID { get; set; }
        public int ILoaiTongHop { get; set; }
        public bool BIsKhoa { get; set; }
        public DateTime? DNgaySua { get; set; }
        public DateTime? DNgayTao { get; set; }
        public string SNguoiSua { get; set; }
        public string SNguoiTao { get; set; }
    }
}
