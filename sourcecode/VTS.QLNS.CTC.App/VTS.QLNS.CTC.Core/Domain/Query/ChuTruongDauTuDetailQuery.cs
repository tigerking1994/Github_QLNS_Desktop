using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace VTS.QLNS.CTC.Core.Domain.Query
{
    public class ChuTruongDauTuDetailQuery
    {
        public int Loai { get; set; }
        public string TenLoai { get; set; }
        public bool? IsHangCha { get; set; }
        public Guid? IdChuTruongChiPhi { get; set; }
        public Guid? IdChiPhi { get; set; }
        public Guid? IdChuTruongNguonVon { get; set; }
        public int? IdNguonVon { get; set; }
        public Guid? IdChuTruongDauTu { get; set; }
        public string NoiDung { get; set; }
        public double? GiaTriPheDuyet { get; set; }
    }
}
