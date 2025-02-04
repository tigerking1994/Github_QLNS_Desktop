using System;
using System.Collections.Generic;

#nullable disable

namespace VTS.QLNS.CTC.Core.Domain
{
    public partial class HtNhatKyCapNhatDuLieu : EntityBase
    {
        public string ApplicationCode { get; set; }
        public string ServiceCode { get; set; }
        public string SessionId { get; set; }
        public string IpPortParentNode { get; set; }
        public string IpPortCurrentNode { get; set; }
        public string RequestContent { get; set; }
        public string ResponseContent { get; set; }
        public DateTime? StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public int? Duration { get; set; }
        public decimal? ErrorCode { get; set; }
        public string ErrorDescription { get; set; }
        public bool? TransactionStatus { get; set; }
        public string ActionName { get; set; }
        public string UserName { get; set; }
        public string Account { get; set; }
    }
}
