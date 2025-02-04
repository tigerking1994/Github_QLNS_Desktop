using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations.Schema;

namespace VTS.QLNS.CTC.Core.Domain.Query
{
    public class ReportQuanSoThuongXuyenQuery
    {
        public double RSQ { get; set; }
        public double RQNCN { get; set; }
        public double RCNVHD { get; set; }
        public double RHSQCS { get; set; }
        public double RSQ1 { get; set; }
        public double RQNCN1 { get; set; }
        public double RCNVHD1 { get; set; }
        public double RHSQCS1 { get; set; }
        public double RSQ2 { get; set; }
        public double RQNCN2 { get; set; }
        public double RCNVHD2 { get; set; }
        public double RHSQCS2 { get; set; }
        public double RSQ3 { get; set; }
        public double RQNCN3 { get; set; }
        public double RCNVHD3 { get; set; }
        public double RHSQCS3 { get; set; }
        public double RSQ4 { get; set; }
        public double RQNCN4 { get; set; }
        public double RCNVHD4 { get; set; }
        public double RHSQCS4 { get; set; }
        public string Id_DonVi { get; set; }
        public string TenDonVi { get; set; }
        public string MoTa { get; set; }
        public int iThangQuy { get; set; }
    }
}
