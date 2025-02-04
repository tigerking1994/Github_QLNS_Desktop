using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations.Schema;

namespace VTS.QLNS.CTC.Core.Domain.Query
{
    public class VdtDaChuTruongDauTuNguonVonQuery
    {
        public Guid? Id { get; set; }
        public Guid? IdChuTruongNguonVon { get; set; }
        public Guid? IIdChuTruongDauTuId { get; set; }
        public string TenNguonVon { get; set; }
        public int? IIdNguonVonId { get; set; }
        public double? FTienPheDuyet { get; set; }
        public double? FGiaTriDieuChinh { get; set; }
        public double? GiaTriTruocDieuChinh { get; set; }
    }
}
