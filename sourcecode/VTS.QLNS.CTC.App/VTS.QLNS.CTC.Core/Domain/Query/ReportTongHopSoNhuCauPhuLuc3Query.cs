using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace VTS.QLNS.CTC.Core.Domain.Query
{
    public class ReportTongHopSoNhuCauPhuLuc3Query
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
        public decimal SoKiemTraNamTruoc { get; set; }
        public decimal DuToanDauNam { get; set; }
        public decimal TonKhoDenNgay { get; set; }
        public decimal HuyDongTonKho { get; set; }
        public decimal TuChi { get; set; }
        public string SGhiChu { get; set; }

        [NotMapped]
        public decimal TongSo { get; set; }
        [NotMapped]
        public decimal Tang { get; set; }
        [NotMapped]
        public decimal Giam { get; set; }
        [NotMapped]
        public bool HasData => SoKiemTraNamTruoc != 0 || DuToanDauNam != 0 || TonKhoDenNgay != 0 || HuyDongTonKho != 0 || TuChi != 0 || !string.IsNullOrEmpty(SGhiChu);
    }
}