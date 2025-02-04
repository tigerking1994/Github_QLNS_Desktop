using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;

namespace VTS.QLNS.CTC.Core.Domain.Query
{
    public class ReportChiTietPhanBoKiemTraNsbdQuery
    {
        public Guid? IIdMlsktCha { get; set; }
        public Guid IIdMlskt { get; set; }
        public string sKyHieu { get; set; }
        public string STT { get; set; }
        public string SSTTBC { get; set; }
        public string sL { get; set; }
        public string sK { get; set; }
        public string sM { get; set; }
        public string sNG { get; set; }
        public string sMoTa { get; set; }
        public string sNgCha { get; set; }
        public bool? BHangCha { get; set; }
        public Int32 QuyetToan { get; set; }
        public Int32 DuToan { get; set; }
        public double TuChi { get; set; }
        public double PhanCap { get; set; }
        public double MuaHangHienVat { get; set; }
        public double DacThu { get; set; }
        public double ThongBaoDV { get; set; }
        public double HuyDongTonKho { get; set; }
    }
}
