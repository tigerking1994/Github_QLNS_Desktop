using System;
using System.Collections.Generic;

#nullable disable

namespace VTS.QLNS.CTC.Core.Domain
{
    public partial class VdtQtBcQuyetToanNienDoChiTiet : EntityBase
    {
        public Guid Id { get; set; }
        public Guid? IIdBcquyetToanNienDoId { get; set; }
        public Guid? IIdDuAnId { get; set; }
        public double? FGiaTriNamTruocChuyenNamSau { get; set; }
        public double? FGiaTriNamNayChuyenNamSau { get; set; }
        public string SUserCreate { get; set; }
        public DateTime? DDateCreate { get; set; }
        public string SUserUpdate { get; set; }
        public DateTime? DDateUpdate { get; set; }
        public string SUserDelete { get; set; }
        public DateTime? DDateDelete { get; set; }
        public bool? BIsCanBoDuyet { get; set; }
        public bool? BIsDuyet { get; set; }
    }
}
