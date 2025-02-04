using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VTS.QLNS.CTC.Core.Domain.Query
{
    public class NHDAQDDauTuNguonVonQuery
    {
        public Guid Id { get; set; }
        public Guid QddtId { get; set; }
        public int NguonVonId { get; set; }
        public double? FGiaTriNgoaiTeKhacQDDT { get; set; }
        public double? FGiaTriUSDQDDT { get; set; }
        public double? FGiaTriVNDQDDT { get; set; }
        public double? FGiaTriEurQDDT { get; set; }
        public string STenNguonVon { get; set; }
        public double? FGiaTriNgoaiTeKhacKH { get; set; }
        public double? FGiaTriUSDKH { get; set; }
        public double? FGiaTriVNDKH { get; set; }
        public double? FGiaTriEurKH { get; set; }
        public double? FGiaTriNgoaiTeKhacTT { get; set; }
        public double? FGiaTriUSDTT { get; set; }
        public double? FGiaTriVNDTT { get; set; }
        public double? FGiaTriEurTT { get; set; }

    }
}
