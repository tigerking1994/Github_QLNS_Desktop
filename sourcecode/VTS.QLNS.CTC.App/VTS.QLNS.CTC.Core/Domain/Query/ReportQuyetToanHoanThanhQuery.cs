using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace VTS.QLNS.CTC.Core.Domain.Query
{
    public class ReportQuyetToanHoanThanhQuery
    {
        [NotMapped]
        public string Stt { get; set; }
        public string SoBaoCao { get; set; }
        public string TenDuAn { get; set; }
        public string ChuDauTu { get; set; }
        public int? SoDuAn { get; set; }
        public double? TongMucDauTu { get; set; }
        public double? GiaTriDeNghiQuyetToan { get; set; }
        public double? GiaTriQuyetToanDuocDuyet { get; set; }
        [NotMapped]
        public double? CheckLech
        {
            get
            {
                if (GiaTriQuyetToanDuocDuyet.HasValue || GiaTriDeNghiQuyetToan.HasValue)
                    return ((GiaTriQuyetToanDuocDuyet.HasValue ? GiaTriQuyetToanDuocDuyet.Value : 0) - (GiaTriDeNghiQuyetToan.HasValue ? GiaTriDeNghiQuyetToan.Value : 0));
                else
                    return null;
            }
        }
        [NotMapped]
        public double? VonConLai { get; set; }
        public string MaDonVi { get; set; }
        public Guid NhomDuAnId { get; set; }
        [NotMapped]
        public bool IsHangCha { get; set; }
        [NotMapped]
        public int Loai { get; set; }
        public string TenDonVi { get; set; }
        public string TenNhomDuAn { get; set; }
        [NotMapped]
        public Guid IdReport { get; set; }
        [NotMapped]
        public Guid IdParentReport { get; set; }
    }
}
