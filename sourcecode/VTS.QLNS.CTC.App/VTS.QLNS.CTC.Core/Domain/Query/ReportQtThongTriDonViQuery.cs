using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace VTS.QLNS.CTC.Core.Domain.Query
{
    public class ReportQtThongTriDonViQuery
    {
        [Column("iID_MaDonVi")]
        public string Id_DonVi { get; set; }
        [Column("sTenDonVi")]
        public string TenDonVi { get; set; }
        public double? TuChi { get; set; }
        public double? HienVat { get; set; }
        public double? SoNguoi { get; set; }
        public double? SoNgay { get; set; }
        public double? SoLuot { get; set; }
    }
}
