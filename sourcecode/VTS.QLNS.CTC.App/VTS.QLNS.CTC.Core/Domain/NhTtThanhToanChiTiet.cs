using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace VTS.QLNS.CTC.Core.Domain
{
    public partial class NhTtThanhToanChiTiet : EntityBase
    {
        public Guid? IIdDeNghiThanhToanId { get; set; }
        public Guid? IIdPhuLucHopDongId { get; set; }
        public Guid? IIdNguonVonId { get; set; }
        public Guid? IIdMlnsMucId { get; set; }
        public Guid? IIdMlnsTieuMucId { get; set; }
        public Guid? IIdMlnsTietMucId { get; set; }
        public Guid? IIdMucId { get; set; }
        public Guid? IIdTieuMucId { get; set; }
        public Guid? IIdTietMucId { get; set; }
        public Guid? IIdLoaiNoiDungChiId { get; set; }
        public string STenNoiDungChi { get; set; }
        public double? FDeNghiCapKyNayNgoaiTeKhac { get; set; }
        public double? FDeNghiCapKyNayUsd { get; set; }
        public double? FDeNghiCapKyNayVnd { get; set; }
        public double? FDeNghiCapKyNayEur { get; set; }
        public double? FPheDuyetCapKyNayNgoaiTeKhac { get; set; }
        public double? FPheDuyetCapKyNayUsd { get; set; }
        public double? FPheDuyetCapKyNayVnd { get; set; }
        public double? FPheDuyetCapKyNayEur { get; set; }
        public double? FTongGiaTriTheoHoaDonVnd { get; set; }
        public double? FTongGiaTriTheoHoaDonEur { get; set; }
        public double? FTongGiaTriTheoHoaDonNgoaiTeKhac { get; set; }
        public double? FTongGiaTriTheoHoaDonUsd { get; set; }
        public double? FTiLeThanhToan { get; set; }
        public Guid? IIdMucLucNganSachId { get; set; }
        public Guid? IIdMlnsId { get; set; }
    }
}
