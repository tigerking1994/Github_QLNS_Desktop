using System;
using System.Collections.Generic;

#nullable disable

namespace VTS.QLNS.CTC.Core.Domain
{
    public partial class HtAppUpdateLog
    {
        public int Id { get; set; }
        public string IdVersion { get; set; }
        public string IdUpdate { get; set; }
        public string Name { get; set; }
        public int UpdateType { get; set; }
        public int PriorityType { get; set; }
        public string Description { get; set; }
        public string File { get; set; }
        public int Status { get; set; }
        public bool IProccessed { get; set; }
        public bool ISuccess { get; set; }
        public int ICount { get; set; }
        public DateTime? DateCreated { get; set; }
        public string UserCreator { get; set; }
        public DateTime? DateModified { get; set; }
        public string UserModifier { get; set; }
        public string Tag { get; set; }
        public string Log { get; set; }
        public int? Index3 { get; set; }
        public int? Index4 { get; set; }
        public int? Index6 { get; set; }
        public int? Index7 { get; set; }
    }
}
