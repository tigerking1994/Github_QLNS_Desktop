using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace VTS.QLNS.CTC.Core.Domain.Query
{
    public class ReportSoNhuCauTongHopQuery
    {
        public Guid? IdParent { get; set; }
        public Guid IdMucLuc { get; set; }
        public string Stt { get; set; }
        public string KyHieu { get; set; }
        public string M { get; set; }
        public string MoTa { get; set; }
        public bool? bHangCha { get; set; }
        public double TuChi { get; set; }
        public double HuyDong { get; set; }
        public double PhanCap { get; set; }
        public double MuaHangHienVat { get; set; }
        public double TongCongNSSD { get; set; }
        public double TongCongNSBD { get; set; }
        [NotMapped]
        public string TenDonVi { get; set; }
        [NotMapped]
        public double TongCong { get; set; }
        [NotMapped]
        public List<NsSktChungTuChiTiet> LstGiaTri { get; set; }
        [NotMapped]
        public List<NsSktChungTuChiTiet> LstTong { get; set; }
        [NotMapped]
        public int Level { get; set; }
    }
}
