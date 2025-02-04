using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace VTS.QLNS.CTC.Core.Domain.Query
{
    public class ReportQtNhanQuyetToanKinhPhiQuery
    {
        [Column("sSTT")]
        public string STT { get; set; }
        [Column("sMoTa")]
        public string MoTa { get; set; }
        [Column("sMa")]
        public string Ma { get; set; }
        [Column("sMaCha")]
        public string MaCha { get; set; }
        public double? DuToanNganSach { get; set; }
        public double? KinhPhiDuocCap { get; set; }
        public double? KinhPhiDeNghi { get; set; }
        public double? ChuyenNamSauDaCap { get; set; }
        public double? ChuyenNamSauChuaCap => ChuyenNamSauTongSo - ChuyenNamSauDaCap;
        public double? ChuyenNamSauTongSo { get; set; }
        [Column("bHangCha")]
        public bool IsHangCha { get; set; }
        public bool HasData => DuToanNganSach.GetValueOrDefault(0) != 0
            || KinhPhiDuocCap.GetValueOrDefault(0) != 0
            || KinhPhiDeNghi.GetValueOrDefault(0) != 0
            || ChuyenNamSauDaCap.GetValueOrDefault(0) != 0
            || ChuyenNamSauTongSo.GetValueOrDefault(0) != 0;
    }
       
}
