using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace VTS.QLNS.CTC.Core.Domain.Query
{
    public class NhDaDuToanNguonVonQuery
    {
        public string TenNguonVon { get; set; }
        public Guid? IIdDuToanId { get; set; }
        public int? IIdNguonVonId { get; set; }
        public Guid? IIdQDDauTuNguonVonId { get; set; }
        public Guid? IIdDuToanNguonVonId { get; set; }
        public double? FGiaTriNgoaiTeKhac { get; set; }
        public double? FGiaTriUSD { get; set; }
        public double? FGiaTriVND { get; set; }
        public double? FGiaTriEUR { get; set; }
        public double? FGiaTriNgoaiTeKhacQDDTPheDuyet { get; set; }
        public double? FGiaTriUSDQDDTPheDuyet { get; set; }
        public double? FGiaTriVNDQDDTPheDuyet { get; set; }
        public double? FGiaTriEURQDDTPheDuyet { get; set; }
        public bool IsNew { get; set; }
    }
}
