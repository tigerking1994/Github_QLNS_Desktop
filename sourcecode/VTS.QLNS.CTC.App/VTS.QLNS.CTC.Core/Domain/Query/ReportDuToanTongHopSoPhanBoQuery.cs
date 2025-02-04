using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace VTS.QLNS.CTC.Core.Domain.Query
{
    public class ReportDuToanTongHopSoPhanBoQuery
    {
        public string LNS1 { get; set; }
        public string LNS3 { get; set; }
        public string LNS5 { get; set; }
        public string LNS { get; set; }
        public string L { get; set; }
        public string K { get; set; }
        public string M { get; set; }
        public string TM { get; set; }
        public string TTM { get; set; }
        public string NG { get; set; }
        public string TNG { get; set; }
        public string TNG1 { get; set; }
        public string TNG2 { get; set; }
        public string TNG3 { get; set; }
        public string XauNoiMa { get; set; }
        public string MoTa { get; set; }
        public double? TuChi { get; set; }
        public double? HienVat { get; set; }
        [NotMapped]
        public double Col1Value { get; set; }
        [NotMapped]
        public double Col2Value { get; set; }
        [NotMapped]
        public double Col3Value { get; set; }
        [NotMapped]
        public double Col4Value { get; set; }
        [NotMapped]
        public double Col5Value { get; set; }
        [NotMapped]
        public double Col6Value { get; set; }
        [NotMapped]
        public double Col1_1Value { get; set; }
        [NotMapped]
        public double Col2_1Value { get; set; }
        [NotMapped]
        public double Col3_1Value { get; set; }
        [NotMapped]
        public double Col4_1Value { get; set; }
        [NotMapped]
        public double Col5_1Value { get; set; }
        [NotMapped]
        public double Col6_1Value { get; set; }
        [NotMapped]
        public double TongSo
        {
            get => (TuChi.HasValue ? TuChi.Value : 0) + (HienVat.HasValue ? HienVat.Value : 0);
        }
        [NotMapped]
        public bool IsHangCha { get; set; }
        public Guid MlnsId { get; set; }
        public Guid? MlnsIdParent { get; set; }
        public List<ReportDuToanTongHopSoPhanBoQueryDynamicColumn> LstGiaTri { get; set; } = new List<ReportDuToanTongHopSoPhanBoQueryDynamicColumn>();

    }

    public class ReportDuToanTongHopSoPhanBoQueryDynamicColumn
    {
        public int STT { get; set; }
        public string sMucLucNganSach { get; set; }
        public string sLNS { get; set; }
        public string sSoQuyetDinh { get; set; }
        public double fTuChi { get; set; }
        public double fHienVat { get; set; }
        public string Merge { get; set; }
    }
}
