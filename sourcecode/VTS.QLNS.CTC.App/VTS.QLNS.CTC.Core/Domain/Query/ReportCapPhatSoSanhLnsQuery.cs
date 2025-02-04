using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VTS.QLNS.CTC.Core.Domain.Query
{
    public class ReportCapPhatSoSanhLnsQuery
    {
        public Guid MlnsId { get; set; }
        public Guid? MlnsIdCha { get; set; }
        public string LNS { get; set; }
        public string L { get; set; }
        public string K { get; set; }
        public string M { get; set; }
        public string TM { get; set; }
        public string TTM { get; set; }
        public string NG { get; set; }
        public string MoTa { get; set; }
        public string XauNoiMa { get; set; }
        public bool BHangCha { get; set; }
        public string MaDonVi { get; set; }
        public string TenDonVi { get; set; }
        public double CapPhat { get; set; }
        public double DuToan { get; set; }
        public double ConLai => DuToan - CapPhat;
        [NotMapped]
        public bool BHangChaLns { get; set; }
    }
}
