using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations.Schema;

namespace VTS.QLNS.CTC.Core.Domain.Query
{
    public class VdtQtBcQuyetToanNienDoQuery
    {
        public Guid Id { get; set; }
        public string SSoDeNghi { get; set; }
        public DateTime? DNgayDeNghi { get; set; }
        public int? INamKeHoach { get; set; }
        public int? IIDNguonVonID { get; set; }
        public int? ILoaiThanhToan { get; set; }
        public string IIDMaDonViQuanLy { get; set; }
        public Guid? IIDDonViQuanLyID { get; set; }
        public int? ICoQuanThanhToan { get; set; }
        public string STenDonVi { get; set; }
        public string STenNguonVon { get; set; }
        public bool BKhoa { get; set; }
        public string sTongHop { get; set; }
        public string SMoTa { get; set; }
        public string SUserCreate { get; set; }
    }
}
