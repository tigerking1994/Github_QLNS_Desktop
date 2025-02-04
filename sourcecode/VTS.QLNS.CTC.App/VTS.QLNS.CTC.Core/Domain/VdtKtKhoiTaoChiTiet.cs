using System;
using System.Collections.Generic;

#nullable disable

namespace VTS.QLNS.CTC.Core.Domain
{
    public partial class VdtKtKhoiTaoChiTiet : EntityBase
    {
        public Guid IIdKhoiTaoId { get; set; }
        public int IIdNguonVonId { get; set; }
        public Guid? IIdLoaiNguonVonId { get; set; }
        public double? FKhvonHetNamTruoc { get; set; }
        public double? FLuyKeThanhToanKlht { get; set; }
        public double? FLuyKeThanhToanTamUng { get; set; }
        public double? FThanhToanQuaKb { get; set; }
        public double? FTamUngQuaKb { get; set; }
        public Guid? IIdDonViTienTeId { get; set; }
        public double? FTiGiaDonVi { get; set; }
        public Guid? IIdTienTeId { get; set; }
        public double? FTiGia { get; set; }
        public string IIdMucId { get; set; }
        public string IIdTieuMucId { get; set; }
        public string IIdTietMucId { get; set; }
        public string IIdNganhId { get; set; }
        public double? FSoChuyenChiTieuDaCap { get; set; }
        public double? FSoChuyenChiTieuChuaCap { get; set; }
    }
}
