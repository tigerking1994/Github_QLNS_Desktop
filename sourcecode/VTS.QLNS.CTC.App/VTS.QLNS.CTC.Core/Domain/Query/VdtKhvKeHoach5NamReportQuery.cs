using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace VTS.QLNS.CTC.Core.Domain.Query
{
    public class VdtKhvKeHoach5NamReportQuery
    {
        public Guid? IdLoaiCongTrinh { get; set; }
        public Guid? IdLoaiCongTrinhParent { get; set; }
        public string SMaLoaiCongTrinh { get; set; }
        public int? Loai { get; set; }
        public int? LoaiParent { get; set; }
        public string STenDuAn { get; set; }
        public double? FHanMucDauTu { get; set; }
        public double? FVonDaGiao { get; set; }
        public double? FTongVonBoTri { get; set; }
        public double? FGiaTriKeHoach { get; set; }
        public double? FVonBoTri { get; set; }
        public string GhiChu { get; set; }
        public bool IsHangCha { get; set; }
        [NotMapped]
        public string STT { get; set; }
    }
}
