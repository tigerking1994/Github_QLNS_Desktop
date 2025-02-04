using System;
using System.Collections.Generic;
using VTS.QLNS.CTC.Core.Domain;

#nullable disable

namespace VTS.QLNS.CTC.Core.Domain
{
    public partial class VdtQtDeNghiQuyetToanChiTiet : EntityBase
    {
        public Guid Id { get; set; }
        public Guid IIdDeNghiQuyetToanId { get; set; }
        public Guid? IIdChiPhiId { get; set; }
        public Guid? IIdHangMucId { get; set; }
        public Guid? IIDGoiThauId { get; set; }       
        public double? FGiaTriKiemToan { get; set; }
        public double? FGiaTriDeNghiQuyetToan { get; set; }
        public double? FGiaTriQuyetToanAB { get; set; }
        public string SUserCreate { get; set; }
        public DateTime? DDateCreate { get; set; }
        public string SUserUpdate { get; set; }
        public DateTime? DDateUpdate { get; set; }
        public string SUserDelete { get; set; }
        public DateTime? DDateDelete { get; set; }
        public string SMaOrder { get; set; }
    }
}
