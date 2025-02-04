using System;
using System.Collections.Generic;

#nullable disable

namespace VTS.QLNS.CTC.Core.Domain
{
    public partial class TlMapColumnConfig : EntityBase
    {
        public string OldColumn { get; set; }
        public string NewColumn { get; set; }
        public bool? IsMapPhuCap { get; set; }
        public bool? IsMapValue { get; set; }
        public bool? UsePhuCapValue { get; set; }
        public string MapExpression { get; set; }
        public int Mau { get; set; }
    }
}
