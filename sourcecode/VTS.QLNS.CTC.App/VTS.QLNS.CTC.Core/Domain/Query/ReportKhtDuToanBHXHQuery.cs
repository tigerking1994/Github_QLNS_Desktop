using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace VTS.QLNS.CTC.Core.Domain.Query
{
    public class ReportKhtDuToanBHXHQuery
    {
        public Guid? IIdMlnsCha { get; set; }
        public Guid IIdMlns { get; set; }
        public string IdDonVi { get; set; }
        public string STenDonVi { get; set; }
        public string sXauNoiMa { get; set; }
        public int? STT { get; set; }
        public bool? BHangCha { get; set; }
        public string sNG { get; set; }
        public string sMoTa { get; set; }
        public string sNgCha { get; set; }
        public double? BHXHTongCong => Math.Round(BHXHTongCongDuToan.GetValueOrDefault()) + Math.Round(BHXHTongCongHachToan.GetValueOrDefault());
        public double? BhxhNldDongDuToan { get; set; }
        public double? BhxhNldDongHachToan { get; set; }
        public double? BhxhNsddDongDuToan { get; set; }
        public double? BhxhNsddDongHachToan { get; set; }
        public double? BHXHTongCongDuToan { get; set; }
        public double? BHXHTongCongHachToan { get; set; }

        public double? BhtnTongCong => BHTNTongCongDuToan + BHTNTongCongHachToan;
        public double? BhtnNldDongDuToan { get; set; }
        public double? BhtnNldDongHachToan { get; set; }
        public double? BhtnNsddDongDuToan { get; set; }
        public double? BhtnNsddDongHachToan { get; set; }
        public double? BHTNTongCongDuToan { get; set; }
        public double? BHTNTongCongHachToan { get; set; }

        public double? BhytTongCong => BHYTTongCongDuToan + BHYTTongCongHachToan;
        public double? BhytNldDongDuToan { get; set; }
        public double? BhytNldDongHachToan { get; set; }
        public double? BhytNsddDongDuToan { get; set; }
        public double? BhytNsddDongHachToan { get; set; }
        public double? BHYTTongCongDuToan { get; set; }
        public double? BHYTTongCongHachToan { get; set; }
        [Column("SoTien")]
        public double? FSoTien { get; set; }
        [Column("NoiDung")]
        public string SNoiDung { get; set; }
        public string SNoiDungDisplay => SNoiDung?.Split(',')[2] ?? "";
        public string STTDisplay => SNoiDung?.Split(',')[1] ?? "";
        public bool? HasData => BhxhNldDongDuToan.GetValueOrDefault() != 0 || BhxhNsddDongDuToan.GetValueOrDefault() != 0
            || BhxhNldDongHachToan.GetValueOrDefault() != 0 || BhxhNsddDongHachToan.GetValueOrDefault() != 0;
        public bool? IsParent { get; set; }

        public string SNumber { get; set; }
    }
}
