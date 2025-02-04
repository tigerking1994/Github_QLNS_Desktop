using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace VTS.QLNS.CTC.Core.Domain.Query
{
    public class BhDtcDcdToanChiChiTietQuery
    {
        [Column("iID_BH_DTC_ChiTiet")]
        public Guid ID { get; set; }
        public Guid IID_BH_DTC { get; set; }
        public Guid IID_MucLucNganSach { get; set; }
        [Column("sM")]
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
        public string SXauNoiMa { get; set; }
        public int INamLamViec { get; set; }
        [NotMapped]
        public string Stt { get; set; }
        [NotMapped]
        public string SMoTa { get; set; }

        [NotMapped]
        public string Nganh { get; set; }

        public string IIdMaDonVi { get; set; }
     
        public string STenDonVi { get; set; }

        public Guid IdParent { get; set; }
        [NotMapped]
        public bool IsAdd { get; set; }
        [NotMapped]
        public bool IsAuToFillTuChi { get; set; }
        public bool IsHangCha { get; set; }
        public bool BHangCha { get; set; }
        [NotMapped]
        public string STTM { get; set; }
        [NotMapped]
        public string SChiTietToi { get; set; }
        public string SLNS { get; set; }
      
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
        public bool? BHangChaDuToan { get; set; }
        public bool? BHangChaDuToanDieuChinh { get; set; }

        public Guid IID_MLNS { get; set; }

        public Guid? IID_MLNS_Cha { get; set; }
        [NotMapped]
        public string SCPChiTietToi { get; set; }

        public string SDuToanChiTietToi { get;set; }
        public int Type { get;set; }
        public bool IsDataNotNull => FTienDuToanDuocGiao > 0 || FTienThucHien06ThangDauNam > 0 || FTienUocThucHien06ThangCuoiNam > 0 || FTienUocThucHienCaNam > 0 || FTienSoSanhTang > 0 || FTienSoSanhGiam > 0;

        public double? FTienTangGiam { get; set; }

        public bool IsRemainRow { get; set; }

        public int IRemainRow { get; set; }
        public double? FTienGiaoDuToan { get; set; }
    }
}
