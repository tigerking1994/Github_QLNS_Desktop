using System;
using System.Collections.Generic;

#nullable disable

namespace VTS.QLNS.CTC.Core.Domain
{
    public partial class TlDmCapBac : EntityBase
    {
        public string MaCb { get; set; }
        public string TenCb { get; set; }
        public bool? Splits { get; set; }
        public string Parent { get; set; }
        public bool? Readonly { get; set; }
        public string Note { get; set; }
        public decimal? BhxhCq { get; set; }
        public decimal? HsBhxh { get; set; }
        public decimal? BhytCq { get; set; }
        public decimal? HsBhyt { get; set; }
        public decimal? BhtnCq { get; set; }
        public decimal? HsBhtn { get; set; }
        public decimal? KpcdCq { get; set; }
        public decimal? HsKpcd { get; set; }
        public decimal? BhcsCq { get; set; }
        public decimal? HsBhcs { get; set; }
        public decimal? LhtHs { get; set; }
        public decimal? PhuCapRaQuan { get; set; }
        public decimal? TiLeHuong { get; set; }
        public string XauNoiMa { get; set; }
        public decimal? HsTroCapOmDau { get; set; }

        public ICollection<TlDmCanBo> TlDmCanBos { get; set; }
        public ICollection<TlDmCanBoKeHoach> TlDmCanBoKeHoachs { get; set; }
        public ICollection<TlDsCBNHKeHoach> TlDsCBNHKeHoachs { get; set; }
    }
}
