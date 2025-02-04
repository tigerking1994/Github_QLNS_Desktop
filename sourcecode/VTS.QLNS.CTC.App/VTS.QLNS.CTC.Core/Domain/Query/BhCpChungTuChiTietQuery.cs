using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VTS.QLNS.CTC.Utility;

namespace VTS.QLNS.CTC.Core.Domain.Query
{
    public class BhCpChungTuChiTietQuery
    {
        [Column("iID_CP_ChungTu_ChiTiet")]
        public Guid Id { get; set; }
        public Guid? IID_CP_ChungTu { get; set; }
        public Guid? IID_MucLucNganSach { get; set; }
        public string SM { get; set; }
        public string STM { get; set; }
        public string SNoiDung { get; set; }
        [Column("FTienDaCap")]
        public double? FTienDaCap { get; set; }
        public double? FTienDaCaQuyTruoc { get; set; }
        [Column("FTienDuToan")]
        public double? FTienDuToan { get; set; }
        [Column("FTienKeHoachCap")]
        public double? FTienKeHoachCap { get; set; }
        public double? FTienKeHoachCapQuyTruoc { get; set; }
        public Guid? IID_DonVi { get; set; }
        public string IID_MaDonVi { get; set; }
        public string SGhiChu { get; set; }
        public string SNguoiSua { get; set; }
        public string SNguoiTao { get; set; }
        public DateTime? DNgayTao { get; set; }
        public DateTime? DNgaySua { get; set; }
        [NotMapped]
        public string Stt { get; set; }
        [NotMapped]
        public string SMoTa { get; set; }
        public string SXauNoiMa { get; set; }
        public int? INamLamViec { get; set; }

        [NotMapped]
        public string Nganh { get; set; }

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
        public string SLK => string.IsNullOrEmpty(SL) && string.IsNullOrEmpty(SK) ? string.Empty : SL + StringUtils.DIVISION + SK;
        [NotMapped]
        public bool HasDataSummary => FTienDaCap.GetValueOrDefault() != 0 || FTienDuToan.GetValueOrDefault() != 0 || FTienKeHoachCap.GetValueOrDefault() != 0;
        [NotMapped]
        public string SDuToanChiTietToi { get;set; }
    }
}