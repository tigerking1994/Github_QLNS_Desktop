using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace VTS.QLNS.CTC.Core.Domain
{
    public partial class NhKhTongTheNhiemVuChi : EntityBase
    {
        [Column("ID")]
        public override Guid Id { get; set; }
        public Guid IIdKhTongTheId { get; set; }
        public Guid IIdNhiemVuChiId { get; set; }
        public Guid IIdDonViThuHuongId { get; set; }
        public double? FGiaTriKhTtcp { get; set; }
        public double? FGiaTriKhBqp { get; set; }
        public double? FGiaTriKhBqpVnd { get; set; }
        public string SMaDonViThuHuong { get; set; }
        public string SMaOrder { get; set; }
        public NhDmNhiemVuChi NhDmNhiemVuChi { get; set; }
        public ICollection<NhDaHopDong> NhDaHopDongs { get; set; }

        // Another property
        [NotMapped]
        public string STenNhiemVuChi { get; set; }
        [NotMapped]
        public Guid? ParentNhiemVuChiId { get; set; }
    }
}