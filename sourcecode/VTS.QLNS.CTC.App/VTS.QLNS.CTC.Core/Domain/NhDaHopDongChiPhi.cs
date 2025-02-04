using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace VTS.QLNS.CTC.Core.Domain
{
    public partial class NhDaHopDongChiPhi : EntityBase
    {
        [Column("ID")]
        public override Guid Id { get; set; }
        public Guid? IIdHopDongId { get; set; }
        public Guid? IIdChiPhiId { get; set; }
        public Guid? IIdParentId { get; set; }
        public Guid? IIdCacQuyetDinhChiPhiID { get; set; }
        public Guid? IIdCacQuyetDinhId { get; set; }
        public Guid? IIdGoiThauChiPhiId { get; set; }
        public double? FTienHopDongEUR { get; set; }
        public double? FTienHopDongVND { get; set; }
        public double? FTienHopDongUSD { get; set; }
        public double? FTienHopDongNgoaiTeKhac { get; set; }
        public string STenChiPhi { get; set; }
        public string SMaOrder { get; set; }
        public double? FGiaTriNgoaiTeKhac { get; set; }
        public double? FGiaTriUsd { get; set; }
        public double? FGiaTriVnd { get; set; }
        public double? FGiaTriEur { get; set; }
        public Guid? IIdHopDongGoiThauNhaThauId { get; set; }
        public Guid? IIdHopDongNguonVonId { get; set; }

        // Another properties 
        [NotMapped]
        public IEnumerable<NhDaHopDongHangMuc> HopDongHangMucs { get; set; }
    }
}
