using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VTS.QLNS.CTC.Core.Domain.Query
{
    public class CanCuDuToanQtCpSoNhuCauQuery
    {
        public Guid IdMlns { get; set; }
        public Guid? IdMlnsCha { get; set; }
        public string SXauNoiMa { get; set; }
        public double TuChi { get; set; }
        public double HangNhap { get; set; }
        public double HangMua { get; set; }
        public double PhanCap { get; set; }
        public double MuaHangHienVat { get; set; }
        public double DacThu { get; set; }
        public bool? BHangCha { get; set; }
        [NotMapped]
        public string KyHieu { get; set; }
    }
}
