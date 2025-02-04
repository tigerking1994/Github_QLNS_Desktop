using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VTS.QLNS.CTC.Core.Domain
{
    [Table("BH_KHTM_BHYT_ChiTiet")]
    public partial class BhKhtmBHYTChiTiet : EntityBase
    {
        [Column("iID_KHTM_BHYT_ChiTiet")]
        [Key]
        public override Guid Id { get; set; }
        public Guid IdKhtmBHYT { get; set; }
        public int? ISoNguoi { get; set; }
        public int? ISoThang { get; set; }
        public double? FDinhMuc { get; set; }
        public double? FThanhTien { get; set; }
        public string SGhiChu { get; set; }
        public DateTime? DNgayTao { get; set; }
        public DateTime? DNgaySua { get; set; }
        public string SNguoiTao { get; set; }
        public string SNguoiSua { get; set; }
        public string IIDMaDonVi { get; set; }
        public string STenDonVi { get; set; }
        public Guid IIDNoiDung { get; set; }
        public string STenNoiDung { get; set; }
        public string SXauNoiMa { get; set; }
        public string SLNS { get; set; }
        public int? INamLamViec { get; set; }

        [NotMapped]
        public bool IsAdd { get; set; }
        [NotMapped]
        public bool IsAuToFillTuChi { get; set; }
        [NotMapped]
        public string STenBhMLNS { get; set; }
        [NotMapped]
        public bool IsHangCha { get; set; }
        [NotMapped]
        public Guid? IdParent { get; set; }
        [NotMapped]
        public decimal? DHeSoLCS { get; set; }
        [NotMapped]
        public decimal? DHeSoBHYT { get; set; }
        [NotMapped]
        public string SL { get; set; }
        [NotMapped]
        public string SK { get; set; }
        [NotMapped]
        public string SM { get; set; }
        [NotMapped]
        public string STM { get; set; }
        [NotMapped]
        public string STTM { get; set; }
        [NotMapped]
        public string SNG { get; set; }
        [NotMapped]
        public string STNG { get; set; }
    }
}
