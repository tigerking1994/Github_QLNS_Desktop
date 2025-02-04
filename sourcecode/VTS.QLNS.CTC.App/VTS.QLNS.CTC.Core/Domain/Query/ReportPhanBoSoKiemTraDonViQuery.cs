using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations.Schema;

namespace VTS.QLNS.CTC.Core.Domain.Query
{
    public class ReportPhanBoSoKiemTraDonViQuery
    {
        public Guid? IdParent { get; set; }
        public Guid IdMucLuc { get; set; }
        public string Stt { get; set; }
        public string KyHieu { get; set; }
        public string L { get; set; }
        public string K { get; set; }
        public string M { get; set; }
        public string NG { get; set; }
        public string MoTa { get; set; }
        public bool? bHangCha { get; set; }
        public string SGhiChu { get; set; }
        public double TuChi { get; set; }
        public double HuyDong { get; set; }
        public double PhanCap { get; set; }
        public double MuaHangHienVat { get; set; }
        public double DacThu { get; set; }
        public double TongCongNSSD { get; set; }
        public double TongCongNSBD { get; set; }
    }
}
