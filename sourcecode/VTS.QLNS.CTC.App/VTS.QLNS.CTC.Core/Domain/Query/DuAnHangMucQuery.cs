using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace VTS.QLNS.CTC.Core.Domain.Query
{
    public class DuAnHangMucQuery
    {
        public Guid? IdKhth { get; set; }
        public Guid? IdParentKhth { get; set; }
        public Guid? Id { get; set; }
        public Guid? IdParent { get; set; }
        public Guid? IdDuAn { get; set; }
        public string STenLoaiCongTrinh { get; set; }
        public int? IdNguonVon { get; set; }
        public Guid? IdLoaiCongTrinh { get; set; }
        public double? FHanMucDauTu { get; set; }
        public string SMaHangMuc { get; set; }
        public int? IndexHangMuc { get; set; }
    }
}
