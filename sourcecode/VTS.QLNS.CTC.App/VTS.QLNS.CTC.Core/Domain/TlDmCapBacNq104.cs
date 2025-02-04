#nullable disable

using System.Collections.Generic;

namespace VTS.QLNS.CTC.Core.Domain
{
    public partial class TlDmCapBacNq104 : EntityBase
    {
        public string MaCb { get; set; }
        public string TenCb { get; set; }
        public string Parent { get; set; }
        public bool? IsReadonly { get; set; }
        public string Note { get; set; }
        public string XauNoiMa { get; set; }
        public int? Nam { get; set; }
    }
}
