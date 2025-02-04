using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace VTS.QLNS.CTC.Core.Domain.Query
{
    public class CanCuSoNhuCauQuery
    {
        public string KyHieu { get; set; }
        public double TuChi { get; set; }
        public double HangNhap { get; set; }
        public double HangMua { get; set; }
        public double PhanCap { get; set; }
        public double MuaHangHienVat { get; set; }
        public double DacThu { get; set; }
    }
}
