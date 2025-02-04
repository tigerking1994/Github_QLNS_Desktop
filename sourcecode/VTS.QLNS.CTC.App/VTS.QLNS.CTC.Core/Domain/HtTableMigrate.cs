using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace VTS.QLNS.CTC.Core.Domain
{
    public partial class HtTableMigrate : EntityBase
    {
        public string Object { get; set; }
        public string Description { get; set; }
        public bool IsMigrated { get; set; }
        public int MigrateFrequency { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        [NotMapped]
        public long SourceRowCount { get; set; }
        [NotMapped]
        public long DestinationRowCount { get; set; }
    }
}
