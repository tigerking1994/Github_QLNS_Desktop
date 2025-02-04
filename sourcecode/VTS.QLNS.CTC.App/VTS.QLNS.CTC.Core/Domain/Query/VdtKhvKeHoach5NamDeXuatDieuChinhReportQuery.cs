using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace VTS.QLNS.CTC.Core.Domain.Query
{
    public class VdtKhvKeHoach5NamDeXuatDieuChinhReportQuery
    {
        [NotMapped]
        public string STT { get; set; }
        public string IdDonViQuanLy { get; set; }
        public string STenDuAn { get; set; }
        public double? FHanMucDauTuDuocDuyet { get; set; }
        public double? FTongSoDuocDuyet { get; set; }
        public double? FVonBoTriTuNamDenNamDuocDuyet { get; set; }
        public double? FVonBoTriSauNamDuocDuyet { get; set; }
        public string STrangThaiThucHien { get; set; }
        public double? FHanMucDauTuDeXuat { get; set; }
        public double? FTongSoDeXuat { get; set; }
        public double? FTongCongDeXuat { get; set; }
        public double? FGiaTriNamThuNhatDeXuat { get; set; }
        public double? FGiaTriNamThuHaiDeXuat { get; set; }
        public double? FGiaTriNamThuBaDeXuat { get; set; }
        public double? FGiaTriNamThuTuDeXuat { get; set; }
        public double? FGiaTriNamThuNamDeXuat { get; set; }
        public double? FGiaTriSauNamDeXuat { get; set; }
        public double? FHanMucDauTuChenhLech { get; set; }
        public double? FTongSoChenhLech { get; set; }
        public double? FVonBoTriTuNamDenNamChenhLech { get; set; }
        public double? FVonBoTriSauNamChenhLech { get; set; }
        public string SGhiChu { get; set; }
        public int? Loai { get; set; }
        public bool IsHangCha { get; set; }
    }
}
