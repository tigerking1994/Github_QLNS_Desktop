using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace VTS.QLNS.CTC.Core.Domain.Query
{
    public class VdtKhvPhanBoVonChiTietProAdjustementReportQuery
    {
        public int? CT { get; set; }
        public int? CPD { get; set; }
        public string MaThuTu { get; set; }
        public Guid? DonViQuanLyId { get; set; }
        public Guid? IIDDuAnId { get; set; }
        public string STenDuAn { get; set; }
        public int? IIDNguonVonId { get; set; }
        public Guid? IIDLoaiNguonVonId { get; set; }
        public Guid? IIDLoaiCongTrinhId { get; set; }
        public Guid? IIDCapPheDuyetId { get; set; }
        public Guid? IIDMucId { get; set; }
        public Guid? IIDTieuMucId { get; set; }
        public Guid? IIDTietMucId { get; set; }
        public Guid? IIDNganhId { get; set; }
        public string SSoQuyetDinh { get; set; }
        public DateTime? DNgayQuyetDinh { get; set; }
        public double? ChiTieuDauNam { get; set; }
        public double? Giam { get; set; }
        public double? Tang { get; set; }
        public Guid? OrderDonVi { get; set; }
        public bool IsHangCha { get; set; }
        public string IdDonVi { get; set; }
        public string M { get; set; }
        public string TM { get; set; }
        public string TTM { get; set; }
        public string NG { get; set; }

        [NotMapped]
        public string STenMuc { get; set; }
    }
}
