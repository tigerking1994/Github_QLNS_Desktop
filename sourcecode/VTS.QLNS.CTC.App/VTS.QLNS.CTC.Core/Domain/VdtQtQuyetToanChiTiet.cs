using System;
using System.Collections.Generic;
using VTS.QLNS.CTC.Core.Domain;

#nullable disable

namespace VTS.QLNS.CTC.Core.Domain
{
    public partial class VdtQtQuyetToanChiTiet : EntityBase
    {
        public Guid Id { get; set; }
        public Guid IIdQuyetToanId { get; set; }
        public Guid? IIdChiPhiId { get; set; }
        public Guid? IIdGoiThauId { get; set; }
        public Guid? IIdHangMucId { get; set; }
        public double? FGiaTriThamTra { get; set; }
        public double? FGiaTriQuyetToan { get; set; }
        public string SUserCreate { get; set; }
        public DateTime? DDateCreate { get; set; }
        public string SUserUpdate { get; set; }
        public DateTime? DDateUpdate { get; set; }
        public string SUserDelete { get; set; }
        public DateTime? DDateDelete { get; set; }
        public string SMaOrder { get; set; }
    }
}
