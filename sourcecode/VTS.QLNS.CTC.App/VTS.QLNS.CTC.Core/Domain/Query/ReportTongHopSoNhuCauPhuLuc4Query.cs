using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace VTS.QLNS.CTC.Core.Domain.Query
{
    public class ReportTongHopSoNhuCauPhuLuc4Query
    {
        public string Stt { get; set; }
        public string SSTTBC { get; set; }
        public Guid iID_MLSKT { get; set; }
        public Guid? iID_MLSKTCha { get; set; }
        public string SL { get; set; }
        public string SK { get; set; }
        public string SM { get; set; }
        public string SNG { get; set; }
        public string SKyHieu { get; set; }
        public string SMoTa { get; set; }
        public bool? BHangCha { get; set; }
        public double SoKiemTraDacThuNamTruoc { get; set; }
        public double DuToanDacThuNamTruoc { get; set; }
        public double SoNganhPhanCap { get; set; }
        public double KhungNganSachDuocDuyet { get; set; }
        public double DacThu { get; set; }
        public string SGhiChu { get; set; }

        [NotMapped]
        public double TongSo { get; set; }
        [NotMapped]
        public double Tang { get; set; }
        [NotMapped]
        public double Giam { get; set; }
        [NotMapped]
        public int Rank { get; set; }
    }
}
