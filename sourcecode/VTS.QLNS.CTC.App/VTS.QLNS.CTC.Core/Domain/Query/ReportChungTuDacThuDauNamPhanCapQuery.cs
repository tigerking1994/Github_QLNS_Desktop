using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;

namespace VTS.QLNS.CTC.Core.Domain.Query
{
    public class ReportChungTuDacThuDauNamPhanCapQuery
    {
        public Guid IdDTDauNamPhanCap { get; set; }
        public string MaDonVi { get; set; }
        public string TenDonVi { get; set; }
        public double TuChi { get; set; }
        public string XauNoiMa { get; set; }
        public string GhiChu { get; set; }
        public string NG { get; set; }
    }
}
