using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VTS.QLNS.CTC.Core.Domain
{
    [Table("BH_DTC_DieuChinhDuToanChi_ChiTiet")]
    public class BhDtcDcdToanChiChiTiet : EntityBase
    {
        [Column("iID_BH_DTC_ChiTiet")]
        [Key]
        public override Guid Id { get; set; }
        public Guid IID_BH_DTC { get; set; }
        public Guid IID_MucLucNganSach { get; set; }
        public string SM { get; set; }
        public string STM { get; set; }
        public string SNoiDung { get; set; }
        public double? FTienDuToanDuocGiao { get; set; }
        public double? FTienThucHien06ThangDauNam { get; set; }
        public double? FTienUocThucHien06ThangCuoiNam { get; set; }
        public double? FTienUocThucHienCaNam { get; set; }
        public double? FTienSoSanhTang { get; set; }
        public double? FTienSoSanhGiam { get; set; }
        public DateTime? DNgaySua { get; set; }
        public DateTime? DNgayTao { get; set; }
        public string SNguoiSua { get; set; }
        public string SNguoiTao { get; set; }
        public string SGhiChu { get; set; }
        public int INamLamViec { get; set; }
        public string SXauNoiMa { get; set; }
        public string IIdMaDonVi { get; set; }
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
        public string SLNS { get; set; }
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
        public Guid IID_MLNS { get; set; }
        [NotMapped]
        public Guid IID_MLNS_Cha { get; set; }
        [NotMapped]
        public string SDuToanChiTietToi { get;set; }


    }
}
