using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VTS.QLNS.CTC.Core.Domain.Query
{
    public class ReportCapPhatLoaiCapLnsQuery
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
        public bool BHangChaDonVi { get; set; }
        public bool BHangChaLns { get; set; }
        public string MaDonVi { get; set; }
        public string TenDonVi { get; set; }
        public double CapUng { get; set; }
        public double CapThanhKhoan { get; set; }
        public double CapHopThuc { get; set; }
        public double CapThu { get; set; }
    }
}
