using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations.Schema;

namespace VTS.QLNS.CTC.Core.Domain.Query
{
    public class ReportDuToanThongKeSoQuyetDinhQuery
    {
        public string SoQuyetDinh { get; set; }
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
        public bool BHangCha { get; set; }
        public string XauNoiMa { get; set; }
        public string MoTa { get; set; }
        public string MaDonVi { get; set; }
        public string TenDonVi { get; set; }
        public double? SoPhanBo { get; set; }
        public double? SoDuToan { get; set; }
        public double? TongSoPhanBo { get; set; }
        public double? ConLai { get; set; }
    }
}
