using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VTS.QLNS.CTC.Core.Domain.Query
{
    public class ReportQuyetToanTongHopDonViQuery
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
        public string TNG { get; set; }
        public string TNG1 { get; set; }
        public string TNG2 { get; set; }
        public string TNG3 { get; set; }
        public bool IsHangCha { get; set; }
        public bool BHangChaDuToan { get; set; }
        public bool BHangChaQuyetToan { get; set; }
        public string XauNoiMa { get; set; }
        public string MoTa { get; set; }
        public string IdMaDonVi { get; set; }
        public double DuToan { get; set; }
        public double QuyetToan { get; set; }
        public double TrongKy { get; set; }
        public double QuyetToanDonVi { get; set; }
        public double DuToanDonVi { get; set; }
    }
}
