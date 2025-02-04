using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace VTS.QLNS.CTC.Core.Domain.Query
{
    public class DuToanDetailQuery
    {
        public Guid? Id { get; set; }
        public Guid? IdDuToanHangMuc { get; set; }
        public string MaHangMuc { get; set; }
        public string TenHangMuc { get; set; }
        public bool? IsHangCha { get; set; }
        public Guid? IdChiPhi { get; set; }
        public Guid? IdDuAnChiPhi { get; set; }
        public Guid? IdDuAnHangMuc { get; set; }
        public Guid? IIdDuToanId { get; set; }
        public Guid? HangMucParentId { get; set; }
        public Guid? IdLoaiCongTrinh { get; set; }
        public double? GiaTriPheDuyet { get; set; }
        public double? GiaTriPheDuyetQDDT { get; set; }
        public double? FGiaTriDieuChinh { get; set; }
        public double? GiaTriTruocDieuChinh { get; set; }
        public bool? IsHangMucOld { get; set; }
        public string MaOrDer { get; set; }
        public string TenChiPhi { get; set; }
        public string TenLoaiCT { get; set; }
        public double? FTienPheDuyetQDDT { get; set; }
        public double? FTienChenhLech { get; set; }
    }
}
