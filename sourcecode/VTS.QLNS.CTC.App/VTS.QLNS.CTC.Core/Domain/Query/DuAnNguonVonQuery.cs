using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace VTS.QLNS.CTC.Core.Domain.Query
{
    public class DuAnNguonVonQuery
    {
        public Guid? Id { get; set; }
        public Guid? IdDuAn { get; set; }
        public double? FHanMucDauTu { get; set; }
        public int? IdNguonVon { get; set; }
    }
}
