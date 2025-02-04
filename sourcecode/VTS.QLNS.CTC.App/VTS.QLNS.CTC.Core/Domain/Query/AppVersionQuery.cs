using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace VTS.QLNS.CTC.Core.Domain.Query
{
    public class AppVersionQuery
    {
        public Guid Id { get; set; }
        public string Version { get; set; }
        public string Description { get; set; }
        public byte[]? Filestream { get; set; }
        public int Status { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public long FileSize { get; set; }
        public string FileName { get; set; }
    }
}
