using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace VTS.QLNS.CTC.Core.Domain
{
    public partial class NhDaHopDongNguonVon : EntityBase
    {
        [Column("ID")]
        public override Guid Id { get; set; }
        public Guid? IIdHopDongId { get; set; }
        public Guid? IIdCacQuyetDinhNguonVonId { get; set; }
        public Guid? IIdGoiThauNguonVonId { get; set; }
        public Guid? IIdCacQuyetDinhId { get; set; }
        public int IIdNguonVonId { get; set; }
        public double? FTienHopDongEUR { get; set; }
        public double? FTienHopDongVND { get; set; }
        public double? FTienHopDongUSD { get; set; }
        public double? FTienHopDongNgoaiTeKhac { get; set; }
        public double? FGiaTriNgoaiTeKhac { get; set; }
        public double? FGiaTriUsd { get; set; }
        public double? FGiaTriVnd { get; set; }
        public double? FGiaTriEur { get; set; }
        public string SMaOrder { get; set; }

        // Another properties 
        [NotMapped]
        public IEnumerable<NhDaHopDongChiPhi> HopDongChiPhis { get; set; }
    }
}
