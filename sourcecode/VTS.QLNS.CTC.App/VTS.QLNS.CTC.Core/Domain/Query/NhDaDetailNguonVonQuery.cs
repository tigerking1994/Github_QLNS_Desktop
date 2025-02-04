using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VTS.QLNS.CTC.Core.Domain.Query
{
    public class NhDaDetailNguonVonQuery
    {
        public Guid? Id { get; set; }
        public int? IIdNguonVonID { get; set; }
        public bool IsChecked { get; set; }
        public string STenNguonVon { get; set; }
        public double FGiaTriNgoaiTeKhac { get; set; }
        public double FGiaTriUSD { get; set; }
        public double FGiaTriVND { get; set; }
        public double FGiaTriEUR { get; set; }
        public Guid? IIdGoiThauID { get; set; }

    }
}
