using System;
using System.Collections.Generic;

#nullable disable

namespace VTS.QLNS.CTC.Core.Domain
{
    public partial class HtAppVersion : EntityBase
    {
        public string Version { get; set; }
        public string Description { get; set; }
        public byte[] Filestream { get; set; }
        public int? Status { get; set; }
        public long? Filesize { get; set; }
        public string Filename { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
    }
}
