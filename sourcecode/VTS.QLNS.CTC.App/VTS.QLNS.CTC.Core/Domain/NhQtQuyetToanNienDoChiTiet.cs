using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using VTS.QLNS.CTC.Core.Domain.Query;

namespace VTS.QLNS.CTC.Core.Domain
{
    public partial class NhQtQuyetToanNienDoChiTiet : EntityBase
    {
        public Guid? IIdQuyetToanNienDoId { get; set; }
        public Guid? IIdKhttNhiemVuChiId { get; set; }
        public Guid? IIdHopDongId { get; set; }
        public double? FHopDongUsd { get; set; }
        public double? FHopDongVnd { get; set; }
        public double? FKeHoachTtcpUsd { get; set; }
        public double? FKeHoachTtcpVnd { get; set; }
        public double? FKeHoachBqpUsd { get; set; }
        public double? FKeHoachBqpVnd { get; set; }
        public double? FQtKinhPhiDuyetCacNamTruocUsd { get; set; }
        public double? FQtKinhPhiDuyetCacNamTruocVnd { get; set; }
        public double? FQtKinhPhiDuyetCacNamTruocEur { get; set; }
        public double? FQtKinhPhiDuyetCacNamTruocNgoaiTeKhac { get; set; }
        public double? FQtKinhPhiDuocCapTongSoUsd { get; set; }
        public double? FQtKinhPhiDuocCapTongSoVnd { get; set; }
        public double? FQtKinhPhiDuocCapTongSoEur { get; set; }
        public double? FQtKinhPhiDuocCapTongSoNgoaiTeKhac { get; set; }
        public double? FQtKinhPhiDuocCapNamTruocChuyenSangUsd { get; set; }
        public double? FQtKinhPhiDuocCapNamTruocChuyenSangVnd { get; set; }
        public double? FQtKinhPhiDuocCapNamTruocChuyenSangEur { get; set; }
        public double? FQtKinhPhiDuocCapNamTruocChuyenSangNgoaiTeKhac { get; set; }
        public double? FQtKinhPhiDuocCapNamNayUsd { get; set; }
        public double? FQtKinhPhiDuocCapNamNayVnd { get; set; }
        public double? FQtKinhPhiDuocCapNamNayEur { get; set; }
        public double? FQtKinhPhiDuocCapNamNayNgoaiTeKhac { get; set; }
        public double? FDeNghiQtNamNayUsd { get; set; }
        public double? FDeNghiQtNamNayVnd { get; set; }
        public double? FDeNghiQtNamNayEur { get; set; }
        public double? FDeNghiQtNamNayNgoaiTeKhac { get; set; }
        public double? FDeNghiChuyenNamSauUsd { get; set; }
        public double? FDeNghiChuyenNamSauVnd { get; set; }
        public double? FDeNghiChuyenNamSauEur { get; set; }
        public double? FDeNghiChuyenNamSauNgoaiTeKhac { get; set; }
        public double? FThuaThieuKinhPhiTrongNamUsd { get; set; }
        public double? FThuaThieuKinhPhiTrongNamVnd { get; set; }
        public double? FThuaThieuKinhPhiTrongNamEur { get; set; }
        public double? FThuaThieuKinhPhiTrongNamNgoaiTeKhac { get; set; }
        public double? FThuaNopNsnnUsd { get; set; }
        public double? FThuaNopNsnnVnd { get; set; }
        public double? FThuaNopNsnnEur { get; set; }
        public double? FThuaNopNsnnNgoaiTeKhac { get; set; }
        public double? FLuyKeKinhPhiDuocCapUsd { get; set; }
        public double? FLuyKeKinhPhiDuocCapVnd { get; set; }
        public double? FLuyKeKinhPhiDuocCapEur { get; set; }
        public double? FLuyKeKinhPhiDuocCapNgoaiTeKhac { get; set; }
        public double? FKeHoachChuaGiaiNganUsd { get; set; }
        public double? FKeHoachChuaGiaiNganVnd { get; set; }
        public Guid? IIdMucLucNganSachId { get; set; }
        public Guid? IIdMlnsId { get; set; }
        public Guid? IIdParentId { get; set; }
        public virtual Guid? iID_DuAnID { get; set; }
        public Guid? iID_ThanhToan_ChiTietID { get; set; }
        public bool? BIsSaveTongHop { get; set; }
        public double? FLuyKeKinhPhiDaGiaiNganTrongNamNayUsd { get; set; }
        public double? FLuyKeKinhPhiDaGiaiNganTrongNamNayVnd { get; set; }
        public double? FLuyKeKinhPhiDaGiaiNganKhoiDauDenHetNamNayUsd { get; set; }
        public double? FLuyKeKinhPhiDaGiaiNganKhoiDauDenHetNamNayVnd { get; set; }
        public double? FLuyKeSoDuTamUngKinhPhiKhoiDauDenHetNamNayUsd { get; set; }
        public double? FLuyKeSoDuTamUngKinhPhiKhoiDauDenHetNamNayVnd { get; set; }
        public double? FLuyKeSoDuTamUngCheDoKhoiDauDenHetNamNayUsd { get; set; }
        public double? FLuyKeSoDuTamUngCheDoKhoiDauDenHetNamNayVnd { get; set; }

    }
}
