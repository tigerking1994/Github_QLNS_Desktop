using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VTS.QLNS.CTC.Core.Domain
{
    public partial class BhCpBsChungTuChiTiet : EntityBase
    {
        [Column("iID_CTCapPhatBSChiTiet")]
        [Key]
        public override Guid Id { get; set; }
        public Guid IIDCTCapPhatBS { get; set; }
        public Guid IIdMlns { get; set; }
        public Guid? IIdMlnsCha { get; set; }
        public string SXauNoiMa { get; set; }
        public string SLns { get; set; }
        public int? INamLamViec { get; set; }
        public string IIdMaDonVi { get; set; }
        public string STenDonVi { get; set; }
        public string SGhiChu { get; set; }
        public DateTime? DNgayTao { get; set; }
        public string SNguoiTao { get; set; }
        public DateTime? DNgaySua { get; set; }
        public string SNguoiSua { get; set; }
        public double? FDaQuyetToan { get; set; }
        public double? FDaCapUng { get; set; }
        public double? FThuaThieu { get; set; }
        public double? FSoCapBoSung { get; set; }
        public Guid IIDCoSoYTe { get; set; }
        public string IIDMaCoSoYTe { get; set; }
        [NotMapped]
        public string SCoSoYTe { get; set; }
        [NotMapped]
        public string STenMLNS { get; set; }
        [NotMapped]
        public bool IsAdd { get; set; }
        [NotMapped]
        public bool IsAuToFillTuChi { get; set; }
        [NotMapped]
        public bool IsHangCha { get; set; }
        [NotMapped]
        public Guid? IdParent { get; set; }
        [NotMapped]
        public string STenCSYT { get; set; }
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
