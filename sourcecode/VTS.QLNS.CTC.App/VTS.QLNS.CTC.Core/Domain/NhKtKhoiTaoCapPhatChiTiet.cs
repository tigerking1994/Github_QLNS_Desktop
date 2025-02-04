using System;
using System.Collections.Generic;

namespace VTS.QLNS.CTC.Core.Domain
{
    public partial class NhKtKhoiTaoCapPhatChiTiet : EntityBase
    {
        public Guid? IIdKhoiTaoCapPhatID { get; set; }
        public Guid? IIdDuAnID { get; set; }
        public Guid? IIdHopDongID { get; set; }
        public double? FQTKinhPhiDuyetCacNamTruocUSD { get; set; }
        public double? FQTKinhPhiDuyetCacNamTruocVND { get; set; }
        public double? FDeNghiQTNamNayUSD { get; set; }
        public double? FDeNghiQTNamNayVND { get; set; }
        public double? FLuyKeKinhPhiDuocCapUSD { get; set; }
        public double? FLuyKeKinhPhiDuocCapVND { get; set; }
        public Guid? IIdParentID { get; set; }
        public double? FLuyKeKinhPhiDaGiaiNganTrongNamNayUsd { get; set; }
        public double? FLuyKeKinhPhiDaGiaiNganTrongNamNayVnd { get; set; }
        public double? FLuyKeKinhPhiDaGiaiNganKhoiDauDenHetNamNayUsd { get; set; }
        public double? FLuyKeKinhPhiDaGiaiNganKhoiDauDenHetNamNayVnd { get; set; }
        public double? FLuyKeSoDuTamUngKinhPhiKhoiDauDenHetNamNayUsd { get; set; }
        public double? FLuyKeSoDuTamUngKinhPhiKhoiDauDenHetNamNayVnd { get; set; }
        public double? FLuyKeSoDuTamUngCheDoKhoiDauDenHetNamNayUsd { get; set; }
        public double? FLuyKeSoDuTamUngCheDoKhoiDauDenHetNamNayVnd { get; set; }
        public double? FDeNghiChuyenNamSauUsd { get; set; }
        public double? FDeNghiChuyenNamSauVnd { get; set; }
        public double? FKinhPhiThuaNopNsnnUSD { get; set; }
        public double? FKinhPhiThuaNopNsnnVND { get; set; }
        public double? FConLaiChuaGiaiNganUSD { get; set; }
        public double? FConLaiChuaGiaiNganVND { get; set; }
        public Guid? IIdKhttNhiemVuChiID { get; set; }
        public Guid? IIdQuyetDinhKhacID { get; set; }
        public int? ILoaiNoiDung { get; set; }
        public string SMaNoiDung { get; set; }
        public string STenNoiDung { get; set; }
        public DateTime? DNgayNoiDung { get; set; }
        public Guid? IIdChiPhiID { get; set; }

    }
}
