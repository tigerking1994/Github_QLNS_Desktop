using System;
using System.Collections.Generic;

#nullable disable

namespace VTS.QLNS.CTC.Core.Domain
{
    public class ErrorDatabaseLog : EntityBase
    {
        public string Object { get; set; }
        public string Description { get; set; }
        public string Reason { get; set; }
        public bool IsFixed { get; set; } = true;
        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}
