using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VTS.QLNS.CTC.Core.Domain.Query
{
    public class NhQTQuyetToanNienDoChiTietQuery
    {
        public Guid? Id { get; set; }
        public Guid? IIdQuyetToanNienDoId { get; set; }
        public Guid? IIdKhttNhiemVuChiId { get; set; }
        public Guid? IIdHopDongId { get; set; }
        public double? FHopDongDuAnUsd { get; set; }
        public double? FHopDongDuAnVnd { get; set; }
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
        public double? FQtKinhPhiDuocCapTongSoUsd { get; set; } //col 7 = 9 + 11
        public double? FQtKinhPhiDuocCapTongSoVnd { get; set; }//col 8 = 10 + 12
        public double? FQtKinhPhiDuocCapTongSoEur { get; set; }
        public double? FQtKinhPhiDuocCapTongSoNgoaiTeKhac { get; set; }
        public double? FQtKinhPhiDuocCapNamTruocChuyenSangUsd { get; set; }//col9
        public double? FQtKinhPhiDuocCapNamTruocChuyenSangVnd { get; set; }
        public double? FQtKinhPhiDuocCapNamTruocChuyenSangEur { get; set; }
        public double? FQtKinhPhiDuocCapNamTruocChuyenSangNgoaiTeKhac { get; set; }
        public double? FQtKinhPhiDuocCapNamNayUsd { get; set; }//col11
        public double? FQtKinhPhiDuocCapNamNayVnd { get; set; }//col12
        public double? FQtKinhPhiDuocCapNamNayEur { get; set; }
        public double? FQtKinhPhiDuocCapNamNayNgoaiTeKhac { get; set; }
        public double? FDeNghiQtNamNayUsd { get; set; }//col13
        public double? FDeNghiQtNamNayVnd { get; set; }//col14
        public double? FDeNghiQtNamNayEur { get; set; }
        public double? FDeNghiQtNamNayNgoaiTeKhac { get; set; }
        public double? FDeNghiChuyenNamSauUsd { get; set; }//col15
        public double? FDeNghiChuyenNamSauVnd { get; set; }//col16
        public double? FDeNghiChuyenNamSauEur { get; set; }
        public double? FDeNghiChuyenNamSauNgoaiTeKhac { get; set; }
        public double? FThuaThieuKinhPhiTrongNamUsd { get; set; }//col17 = 7 - 13 - 15
        public double? FThuaThieuKinhPhiTrongNamVnd { get; set; }//col 18 = 8 - 14 - 16
        public double? FThuaThieuKinhPhiTrongNamEur { get; set; }
        public double? FThuaThieuKinhPhiTrongNamNgoaiTeKhac { get; set; }
        public double? FThuaNopNsnnUsd { get; set; }//col 19
        public double? FThuaNopNsnnVnd { get; set; }
        public double? FThuaNopNsnnEur { get; set; }
        public double? FThuaNopNsnnNgoaiTeKhac { get; set; }
        public double? FLuyKeKinhPhiDuocCapUsd { get; set; }//col21
        public double? FLuyKeKinhPhiDuocCapVnd { get; set; }
        public double? FLuyKeKinhPhiDuocCapEur { get; set; }
        public double? FLuyKeKinhPhiDuocCapNgoaiTeKhac { get; set; }
        public double? FKeHoachChuaGiaiNganUsd { get; set; }//col 23 = 4 - 7
        public double? FKeHoachChuaGiaiNganVnd { get; set; }
        public Guid? IIdMucLucNganSachId { get; set; }
        public Guid? IIdMlnsId { get; set; }
        public Guid? IIdParentId { get; set; }
        public string SLNS { get; set; }
        public string SL { get; set; }
        public string SK { get; set; }
        public string SM { get; set; }
        public string STM { get; set; }
        public string STTM { get; set; }
        public string SLevel { get; set; }
        public string STenHopDong { get; set; }
        public string STenDuAn { get; set; }
        public string STenNhiemVuChi { get; set; }
        public string STenNoiDungChi { get; set; }
        public int? ILoaiNoiDungChi { get; set; }
        public string STT { get; set; }
        public Guid? IID_DonVi { get; set; }
        public Guid? IID_DuAnID { get; set; }
        public Guid? IID_ThanhToan_ChiTietID { get; set; }
        public bool IsData { get; set; }
        public Guid? IIdTiGiaId { get; set; }
        public bool BIsSaveTongHop { get; set; }
        public int ISum { get; set; }
        public bool IsChiKhac { get; set; }
        [NotMapped]
        public bool IsParent { get; set; }
        public double? FLuyKeKinhPhiDaGiaiNganTrongNamNayUsd { get; set; }//col24
        public double? FLuyKeKinhPhiDaGiaiNganTrongNamNayVnd { get; set; }
        public double? FLuyKeKinhPhiDaGiaiNganKhoiDauDenHetNamNayUsd { get; set; }
        public double? FLuyKeKinhPhiDaGiaiNganKhoiDauDenHetNamNayVnd { get; set; }
        public double? FLuyKeSoDuTamUngKinhPhiKhoiDauDenHetNamNayUsd { get; set; }
        public double? FLuyKeSoDuTamUngKinhPhiKhoiDauDenHetNamNayVnd { get; set; }
        public double? FLuyKeSoDuTamUngCheDoKhoiDauDenHetNamNayUsd { get; set; }
        public double? FLuyKeSoDuTamUngCheDoKhoiDauDenHetNamNayVnd { get; set; }

    }
}
