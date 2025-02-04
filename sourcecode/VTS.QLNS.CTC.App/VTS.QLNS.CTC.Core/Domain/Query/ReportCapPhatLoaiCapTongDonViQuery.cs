using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;

namespace VTS.QLNS.CTC.Core.Domain.Query
{
    public class ReportCapPhatLoaiCapTongDonViQuery
    {
        public string MaDonVi { get; set; }
        public string TenDonVi { get; set; }
        public double CapUng { get; set; }
        public double CapThanhKhoan { get; set; }
        public double CapHopThuc { get; set; }
        public double CapThu { get; set; }
    }
}
