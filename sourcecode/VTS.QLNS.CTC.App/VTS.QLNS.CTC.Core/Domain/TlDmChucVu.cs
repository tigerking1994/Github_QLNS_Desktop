using System;
using System.Collections.Generic;


#nullable disable

namespace VTS.QLNS.CTC.Core.Domain
{
    public partial class TlDmChucVu : EntityBase
    {
        public string MaCv { get; set; }
        public string TenCv { get; set; }
        public decimal? HeSoCv { get; set; }
        public decimal? ThanhTienCv { get; set; }

        public ICollection<TlDmCanBo> TlDmCanBos { get; set; }
        public ICollection<TlDmCanBoKeHoach> TlDmCanBoKeHoachs { get; set; }
        public ICollection<TlDsCBNHKeHoach> TlDsCBNHKeHoachs { get; set; }
    }
}
