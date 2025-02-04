using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations.Schema;

namespace VTS.QLNS.CTC.Core.Domain.Query
{
    public class ReportSoNhuCauTongHopDonViQuery
    {
        public Guid? IdParent { get; set; }
        public Guid IdMucLuc { get; set; }
        public string Stt { get; set; }
        public string SSTTBC { get; set; }
        public string KyHieu { get; set; }
        public string M { get; set; }
        public string MoTa { get; set; }
        public string IdDonVi { get; set; }
        public bool? BHangCha { get; set; }
        public double TuChi { get; set; }
        public double HuyDong { get; set; }
        public double PhanCap { get; set; }
        public double MuaHangHienVat { get; set; }
        public double TongCongNSSD { get; set; }
        public double TongCongNSBD { get; set; }
    }
}
