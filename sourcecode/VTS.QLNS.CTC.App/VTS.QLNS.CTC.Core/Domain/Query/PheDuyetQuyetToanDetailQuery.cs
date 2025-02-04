using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations.Schema;

namespace VTS.QLNS.CTC.Core.Domain.Query
{
    public class PheDuyetQuyetToanDetailQuery
    {
        public Guid? Id { get; set; }
        public bool IsHangCha { get; set; }
        public int Loai { get; set; }
        public string TenLoai { get; set; }
        public int IdNguonVon { get; set; }
        public Guid? IdChiPhi { get; set; }
        public string TenChiPhi { get; set; }
        public double? GtDuToan { get; set; }
        public double? GtQuyetToan { get; set; }
    }
}
