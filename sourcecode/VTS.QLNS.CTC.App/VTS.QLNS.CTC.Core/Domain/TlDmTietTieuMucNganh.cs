using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace VTS.QLNS.CTC.Core.Domain
{
    public partial class TlDmTietTieuMucNganh : EntityBase
    {
        public string MaTtmNg { get; set; }
        public string TenTtmNg { get; set; }
    }
}
