using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace VTS.QLNS.CTC.Core.Domain.Query
{
    public class VdtKhvVonNamDonViReportQuery
    {
        [NotMapped]
        public string STT { get; set; }
        public string STenDuAn { get; set; }
        public string SChuDauTu { get; set; }
        public double? TongMucDauTuDuocDuyet { get; set; }
        public double? LuyKeVonThucHienTruocNam { get; set; }
        public double? TongSoKeHoachVon { get; set; }
        public double? KeHoachVonDuocGiao { get; set; }
        public double? VonKeoDaiCacNamTruoc { get; set; }
        public double? UocThucHien { get; set; }
        public double? LuyKeVonDaBoTriHetNam { get; set; }
        public double? TongNhuCauVonNamSau { get; set; }
        public double? ThuHoiVonUngTruoc { get; set; }
        public double? ThanhToan { get; set; }
        public Guid? IIdLoaiCongTrinh { get; set; }
        public Guid? IIdLoaiCongTrinhParent { get; set; }
        public int? Loai { get; set; }
        public int? LoaiParent { get; set; }
        public bool IsHangCha { get; set; }
        public int LoaiDuAn { get; set; }
    }
}
