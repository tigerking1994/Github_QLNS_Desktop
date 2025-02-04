using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VTS.QLNS.CTC.Core.Domain
{
    [Table("BH_DTTM_BHYT_ThanNhan_ChiTiet")]
    public class BhDtTmBHYTTNChiTiet : EntityBase
    {
        [Column("iID_DTTM_BHYT_ThanNhan_ChiTiet")]
        [Key]
        public override Guid Id { get; set; }
        public Guid IID_DTTM_BHYT_ThanNhan { get; set; }
        public string SNguoiTao { get; set; }
        public string SNguoiSua { get; set; }
        public DateTime? DNgayTao { get; set; }
        public DateTime? DNgaySua { get; set; }
        public int? INamLamViec { get; set; }
        public Guid IID_MLNS { get; set; }
        public string SGhiChu { get; set; }
        public double? FDuToan { get; set; }
        public string SLNS { get; set; }
        public string SNoiDung { get; set; }
        public string IIdMaDonVi { get; set; }
        public string SXauNoiMa { get; set; }

        [NotMapped]
        public string Stt { get; set; }
        [NotMapped]
        public string SMoTa { get; set; }

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
        public Guid IID_MLNS_Cha { get; set; }

    }
}
