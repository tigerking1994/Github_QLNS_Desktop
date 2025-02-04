using System;

namespace VTS.QLNS.CTC.Core.Domain.Query
{
    public class BhQttBHXHChiTietGiaiThichBangLoiQuery
    {
        public Guid? QttBHXHId { get; set; }
        public string IIDMaDonVi { get; set; }
        public int? INamLamViec { get; set; }
        public string SKienNghi { get; set; }
        public string SLoaiThu { get; set; }
        public double? FPhaiNopTrongQuyNam { get; set; }
        public double? FTruyThuQuyNamTruoc { get; set; }
        public double? FDaNopTrongQuyNam { get; set; }
        public double? FConPhaiNopTiep { get; set; }
    }
}
