using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations.Schema;

namespace VTS.QLNS.CTC.Core.Domain.Query
{
    public class QDDauTuChiPhiNguonVonDetailQuery
    {
        public int Loai { get; set; }
        public string TenLoai { get; set; }
        public bool? IsHangCha { get; set; }
        public Guid? IdQDChiPhi { get; set; }
        public Guid? IdChiPhi { get; set; }
        public int? IdNguonVon { get; set; }
        public Guid? IdQDDauTu { get; set; }
        public string NoiDung { get; set; }
        public double? GiaTriPheDuyet { get; set; }
    }
}
