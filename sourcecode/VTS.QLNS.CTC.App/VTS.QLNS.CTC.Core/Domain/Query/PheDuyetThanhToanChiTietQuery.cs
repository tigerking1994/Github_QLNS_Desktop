using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations.Schema;

namespace VTS.QLNS.CTC.Core.Domain.Query
{
    public class PheDuyetThanhToanChiTietQuery
    {
        public int ILoaiDeNghi { get; set; }
        public int? ILoaiNamKeHoach { get; set; }
        public Guid? IIDKeHoachVonID { get; set; }
        public double FGiaTriNgoaiNuoc { get; set; }
        public double FGiaTriTrongNuoc { get; set; }
        public string LNS { get; set; }
        public string L { get; set; }
        public string K { get; set; }
        public string M { get; set; }
        public string TM { get; set; }
        public string TTM { get; set; }
        public string NG { get; set; }
        public Guid? IIdMuc { get; set; }
        public Guid? IIdTieuMuc { get; set; }
        public Guid? IIdTietMuc { get; set; }
        public Guid? IIdNganh { get; set; }
        public string SGhiChu { get; set; }
    }
}
