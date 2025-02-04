using System;
using System.Collections.Generic;

#nullable disable

namespace VTS.QLNS.CTC.Core.Domain
{
    public partial class Attachment : EntityBase
    {
        public int ModuleType { get; set; }
        public Guid ObjectId { get; set; }
        public int? UploadType { get; set; }
        public string FileName { get; set; }
        public string FilePath { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string CreatedUser { get; set; }
        public string SSoCanCu { get; set; }
        public int? ILoaiCanCu { get; set; }
        public DateTime? DNgayCanCu { get; set; }
    }
}
