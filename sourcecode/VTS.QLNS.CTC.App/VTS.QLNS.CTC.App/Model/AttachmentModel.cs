using System;
using System.Collections.Generic;
using System.Text;

namespace VTS.QLNS.CTC.App.Model
{
    public class AttachmentModel : ModelBase
    {
        public string FilePath { get; set; }
        public string FileName { get; set; }
        public int? Module { get; set; }
        public Guid ObjectId { get; set; }
        public int UploadType { get; set; }
        public string SSoCanCu { get; set; }
        public int? ILoaiCanCu { get; set; }
        public DateTime? DNgayCanCu { get; set; }
        public DateTime? CreatedDate { get; set; }
    }
}
