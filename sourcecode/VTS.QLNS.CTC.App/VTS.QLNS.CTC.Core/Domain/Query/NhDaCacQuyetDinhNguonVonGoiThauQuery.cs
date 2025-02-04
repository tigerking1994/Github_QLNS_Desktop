using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;

namespace VTS.QLNS.CTC.Core.Domain.Query
{
    public class NhDaCacQuyetDinhNguonVonGoiThauQuery
    {
        public Guid Id { get; set; }
        public Guid? IIdCacQuyetDinhNguonVonId { get; set; }
        public Guid? IIdQDDauTuNguonVonId { get; set; }
        public Guid? IIdDuToanNguonVonId { get; set; }
        public Guid? IIdGoiThauID { get; set; }
        public double? FTienGoiThauUsd { get; set; }
        public double? FTienGoiThauVnd { get; set; }
        public double? FTienGoiThauEur { get; set; }
        public double? FTienGoiThauNgoaiTeKhac { get; set; }
        public double? FGiaTriUSD { get; set; }
        public double? FGiaTriVND { get; set; }
        public double? FGiaTriEUR { get; set; }
        public double? FGiaTriNgoaiTeKhac { get; set; }
        public string STenNguonVon { get; set; }
    }
}
