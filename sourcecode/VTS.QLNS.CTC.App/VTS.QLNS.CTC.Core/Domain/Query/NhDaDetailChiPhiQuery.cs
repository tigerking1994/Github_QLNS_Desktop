using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VTS.QLNS.CTC.Core.Domain.Query
{
    public class NhDaDetailChiPhiQuery
    {
        public Guid? IIdChiPhiID { get; set; }
        public Guid? IIdParentID { get; set; }
        public string STenChiPhi { get; set; }
        public string SMaOrder { get; set; }
        public double FGiaTriNgoaiTeKhac { get; set; }
        public double FGiaTriUSD { get; set; }
        public double FGiaTriVND { get; set; }
        public double FGiaTriEUR { get; set; }
        public Guid? IIdGoiThauID { get; set; }
        public Guid? IIdNguonVonId { get; set; }
        public int? IIdNguonVonIdInt { get; set; }
        public bool IsChecked { get; set; }

    }
}
