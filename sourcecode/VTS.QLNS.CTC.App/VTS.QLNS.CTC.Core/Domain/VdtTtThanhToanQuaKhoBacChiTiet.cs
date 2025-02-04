using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace VTS.QLNS.CTC.Core.Domain
{
    public partial class VdtTtThanhToanQuaKhoBacChiTiet : EntityBase
    {
        public Guid Id { get; set; }
        public Guid? IIdThanhToanId { get; set; }
        public Guid? IIdMucId { get; set; }
        public Guid? IIdTieuMucId { get; set; }
        public Guid? IIdTietMucId { get; set; }
        public Guid? IIdNganhId { get; set; }
        public Guid? IIdDuAnId { get; set; }
        public Guid? IIdHopDongId { get; set; }
        public Guid? IIdNhaThauId { get; set; }
        public double? FGiaTriThanhToan { get; set; }
        public double? FGiaTriTamUng { get; set; }
        public Guid? IIdDonViTienTeId { get; set; }
        public double? FTiGiaDonVi { get; set; }
        public Guid? IIdTienTeId { get; set; }
        public double? FTiGia { get; set; }

        public virtual VdtTtThanhToanQuaKhoBac IIdThanhToan { get; set; }
    }
}
