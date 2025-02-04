using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VTS.QLNS.CTC.Core.Domain
{
    [Table("BH_DTT_BHXH_DieuChinh_ChiTiet")]
    public class BhDcDuToanThuChiTiet : EntityBase
    {
        [Column("iID_DTT_BHXH_DieuChinh_ChiTiet")]
        [Key]
        public override Guid Id { get; set; }
        public Guid IIDDttDieuChinh { get; set; }
        public Guid IIDMLNS { get; set; }
        public string SLNS { get; set; }
        public string SNoiDung { get; set; }
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
        public DateTime? DNgaySua { get; set; }
        public DateTime? DNgayTao { get; set; }
        public string SNguoiSua { get; set; }
        public string SNguoiTao { get; set; }
        public string IIdMaDonVi { get; set; }
        public int? INamLamViec { get; set; }
        public string SGhiChu { get; set; }
        [NotMapped]
        public string Stt { get; set; }
        [NotMapped]
        public string SMoTa { get; set; }
        public string SXauNoiMa { get; set; }
        [NotMapped]
        public string Nganh { get; set; }
        [NotMapped]
        public string STenDonVi { get; set; }
        [NotMapped]
        public Guid? IdParent { get; set; }
        [NotMapped]
        public bool IsAdd { get; set; }
        [NotMapped]
        public bool IsAuToFillTuChi { get; set; }
        [NotMapped]
        public bool IsHangCha { get; set; }
        [NotMapped]
        public bool BHangCha { get; set; }
        [NotMapped]
        public string STTM { get; set; }
        [NotMapped]
        public string SChiTietToi { get; set; }
        [NotMapped]
        public string SL { get; set; }
        [NotMapped]
        public string SK { get; set; }
        [NotMapped]
        public string SM { get; set; }
        [NotMapped]
        public string STM { get; set; }
        [NotMapped]
        public string SNG { get; set; }
        [NotMapped]
        public string STNG { get; set; }
        [NotMapped]
        public string STNG1 { get; set; }
        [NotMapped]
        public string STNG2 { get; set; }
        [NotMapped]
        public string STNG3 { get; set; }
        [NotMapped]
        public string BHangChaDuToan { get; set; }
        [NotMapped]
        public Guid? IIDMLNSCha { get; set; }
    }
}
