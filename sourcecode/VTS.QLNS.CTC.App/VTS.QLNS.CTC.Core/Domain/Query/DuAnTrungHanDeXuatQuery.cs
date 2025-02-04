using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace VTS.QLNS.CTC.Core.Domain.Query
{
    public class DuAnTrungHanDeXuatQuery
    {
        public Guid IDDuAnID { get; set; }
        public string SMaDuAn { get; set; }
        public string STenDuAn { get; set; }
        public string SDiaDiem { get; set; }
        public int IGiaiDoanTu { get; set; }
        public int IGiaiDoanDen { get; set; }
        public double? FHanMucDauTu { get; set; }
        public Guid? IIdDonViId { get; set; }
        public string IIDMaDonVi { get; set; }
        public string STenDonVi { get; set; }
        public Guid? IIDLoaiCongTrinhID { get; set; }
        public string STenLoaiCongTrinh { get; set; }
        public int? IIDNguonVonID { get; set; }
        public string STenNguonVon { get; set; }
        public Guid? Id_DuAnKhthDeXuat { get; set; }
    }
}
