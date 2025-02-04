using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VTS.QLNS.CTC.Core.Domain
{
    public class VdtFtpRoot : EntityBase
    {
        public string SMaDonVi { get; set; }
        public string SIpAddress { get; set; }
        public string SFolderRoot { get; set; }
        public string SNguoiTao { get; set; }
        public DateTime DNgayTao { get; set; }

    }
}
