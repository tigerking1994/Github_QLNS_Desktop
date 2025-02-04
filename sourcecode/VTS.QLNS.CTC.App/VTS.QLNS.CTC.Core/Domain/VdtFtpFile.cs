using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VTS.QLNS.CTC.Core.Domain
{
    public class VdtFtpFile : EntityBase
    {
        public Guid IID_FtpRoot { get; set; }
        public string SFileName { get; set; }
        public string SRootPath { get; set; }
        public string SNguoiTao { get; set; }
        public DateTime DNgayTao { get; set; }
    }
}
