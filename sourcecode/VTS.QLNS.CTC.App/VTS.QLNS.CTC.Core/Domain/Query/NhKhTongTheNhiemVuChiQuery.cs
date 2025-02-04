using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace VTS.QLNS.CTC.Core.Domain.Query
{
    public partial class NhKhTongTheNhiemVuChiQuery : EntityBase
    {
        [Column("iID_KHTongTheID")]
        public Guid IIdKhTongTheId { get; set; }

        [Column("iID_NhiemVuChiID")]
        public Guid IIdNhiemVuChiId { get; set; }

        [Column("iID_DonViThuHuongID")]
        public Guid IIdDonViThuHuongId { get; set; }

        public string SMaNhiemVuChi { get; set; }
        public string STenNhiemVuChi { get; set; }
        public int? ILoaiNhiemVuChi { get; set; }
        public double? FGiaTriKhTTCP { get; set; }
        public double? FGiaTriKhBQP { get; set; }
        public double? FGiaTriKhBQPVnd { get; set; }
        public string STenDonVi { get; set; }
        public string SMaDonViThuHuong { get; set; }
        public Guid? ParentNhiemVuChiId { get; set; }
        public string SMaOrder { get; set; }
        public string IIDMaDonVi { get; set; }
        public string TenDonVi { get; set; }
        public int NamLamViec { get; set; }
    }
}
