using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations.Schema;

namespace VTS.QLNS.CTC.Core.Domain.Query
{
    public class VdtTtPheDuyetThanhToanChiTietQuery
    {
        public string LNS { get; set; }
        public string L { get; set; }
        public string K { get; set; }
        public string M { get; set; }
        public string TM { get; set; }
        public string TTM { get; set; }
        public string NG { get; set; }
        public Guid? iID_MucID { get; set; }
        public Guid? iID_TieuMucID { get; set; }
        public Guid? iID_TietMucID { get; set; }
        public Guid? iID_NganhID { get; set; }
        public string STenMucLuc { get; set; }
        public string sGhiChu { get; set; }
        public int iLoai { get; set; }
        public double? fGiaTriThanhToanTN { get; set; }
        public double? fGiaTriThanhToanNN { get; set; }
        public double? fGiaTriNamTruocTrongNuoc { get; set; }
        public double? fGiaTriNamTruocNgoaiNuoc { get; set; }
        public double? fGiaTriNamNayTrongNuoc { get; set; }
        public double? fGiaTriNamNayNgoaiNuoc { get; set; }
        public Guid? iID_KeHoachVonID { get; set; }
        public Guid? iId_DeNghiTamUng { get; set; }
        public int? iLoaiKeHoachVon { get; set; }
        public int iLoaiNamKeHoach { get; set; }
    }
}
