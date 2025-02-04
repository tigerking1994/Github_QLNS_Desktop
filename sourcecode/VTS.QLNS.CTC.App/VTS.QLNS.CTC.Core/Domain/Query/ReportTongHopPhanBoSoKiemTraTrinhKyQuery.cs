using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace VTS.QLNS.CTC.Core.Domain.Query
{
    public class ReportTongHopPhanBoSoKiemTraTrinhKyQuery
    {
        [NotMapped]
        public int Stt { get; set; }
        public string STenDonVi { get; set; }
        public string MaDonVi { get; set; }
        public double TongSncTuChi { get; set; }
        public double TongSncMuaHangHienVat { get; set; }
        public double TongSncDacThu { get; set; }
        public double TongSktTuChi { get; set; }
        public double TongSktMuaHangHienVat { get; set; }
        public double TongSktDacThu { get; set; }
        public double TongSktTuChiNamTruoc { get; set; }
        public double TongSktMuaHangHienVatNamTruoc { get; set; }
        public double TongSktDacThuNamTruoc { get; set; }

    }
}
