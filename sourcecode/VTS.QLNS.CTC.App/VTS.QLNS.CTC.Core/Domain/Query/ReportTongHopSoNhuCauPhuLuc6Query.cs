using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace VTS.QLNS.CTC.Core.Domain.Query
{
    public class ReportTongHopSoNhuCauPhuLuc6Query
    {
        public string Stt { get; set; }
        public Guid iID_MLSKT { get; set; }
        public Guid? iID_MLSKTCha { get; set; }
        public string SL { get; set; }
        public string SK { get; set; }
        public string SM { get; set; }
        public string SNG { get; set; }
        public string SKyHieu { get; set; }
        public string SSTTBC { get; set; }
        public string SMoTa { get; set; }
        public bool? BHangCha { get; set; }
        public double DuToan { get; set; }
        public double UocThucHien { get; set; }
        public double NhuCauNam1 { get; set; }
        [NotMapped]
        public double SSNam1
        {
            get => UocThucHien != 0 ? NhuCauNam1 * 100 / UocThucHien : 0;
        }
        public double NhuCauNam2 { get; set; }
        [NotMapped]
        public double SSNam2
        {
            get => NhuCauNam1 != 0 ? NhuCauNam2 * 100 / NhuCauNam1 : 0;
        }
        public double NhuCauNam3 { get; set; }
        [NotMapped]
        public double SSNam3
        {
            get => NhuCauNam2 != 0 ? NhuCauNam3 * 100 / NhuCauNam2 : 0;
        }
        [NotMapped]
        public double TongNhuCau { get => NhuCauNam1 + NhuCauNam2 + NhuCauNam3; }
        public string SGhiChu { get; set; }       
        [NotMapped]
        public bool HasData => DuToan != 0 || UocThucHien != 0 || NhuCauNam1 != 0 || NhuCauNam2 != 0 || NhuCauNam3 != 0 || !string.IsNullOrEmpty(SGhiChu);
    }
}