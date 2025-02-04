using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace VTS.QLNS.CTC.Core.Domain.Query
{
    public class VdtThongTriChiTietQuery
    {
        public string SMaKieuThongTri { get; set; }
        public string SSoThongTri { get; set; }
        public Guid? IIdDuAnId { get; set; }
        public Guid? IIdNhaThauId { get; set; }
        public double FSoTien { get; set; }
        public Guid? IIdMucId { get; set; }
        public Guid? IIdTieuMucId { get; set; }
        public Guid? IIdTietMucId { get; set; }
        public Guid? IIdNganhId { get; set; }
        public Guid? IIdLoaiCongTrinhId { get; set; }
        public Guid? IIdLoaiNguonVonId { get; set; }
        public Guid? IIdCapPheDuyetId { get; set; }
        public Guid? IIdDeNghiThanhToanId { get; set; }
        public string STenDuAn { get; set; }
        public string SLns { get; set; }
        public string SL { get; set; }
        public string SK { get; set; }
        public string SM { get; set; }
        public string STm { get; set; }
        public string STtm { get; set; }
        public string SNg { get; set; }
        public string SDonViThuHuong { get; set; }
    }
}
