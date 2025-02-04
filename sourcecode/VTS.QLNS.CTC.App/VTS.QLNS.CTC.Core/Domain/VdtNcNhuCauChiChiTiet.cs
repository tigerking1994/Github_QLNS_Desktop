using System;
using System.Collections.Generic;

#nullable disable

namespace VTS.QLNS.CTC.Core.Domain
{
    public partial class VdtNcNhuCauChiChiTiet : EntityBase
    {
        public Guid? IIdNhuCauChiId { get; set; }
        public Guid? IIdDuAnId { get; set; }
        public Guid? IIdLoaiCongTrinhId { get; set; }
        public double? FGiaTriDeNghi { get; set; }
        public string SGhiChu { get; set; }
        public string SUserCreate { get; set; }
        public DateTime? DDateCreate { get; set; }
        public string SUserUpdate { get; set; }
        public DateTime? DDateUpdate { get; set; }
        public string SUserDelete { get; set; }
        public DateTime? DDateDelete { get; set; }
        public string SLoaiThanhToan { get; set; }
    }
}
