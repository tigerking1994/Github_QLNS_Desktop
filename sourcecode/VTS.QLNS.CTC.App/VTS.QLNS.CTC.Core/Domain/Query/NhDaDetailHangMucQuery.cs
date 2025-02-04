using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VTS.QLNS.CTC.Core.Domain.Query
{
    public class NhDaDetailHangMucQuery
    {
        public Guid IIdHangMucID { get; set; }
        public Guid? IIdParentID { get; set; }
        public Guid? IIdChiPhiID { get; set; }
        public string SMaHangMuc { get; set; }
        public string STenHangMuc { get; set; }
        public string SMaOrder { get; set; }
        public double FGiaTriNgoaiTeKhac { get; set; }
        public double FGiaTriUSD { get; set; }
        public double FGiaTriEUR { get; set; }
        public double FGiaTriVND { get; set; }
        public double FGiaTriPheDuyetNgoaiTeKhac { get; set; }
        public double FGiaTriPheDuyetUSD { get; set; }
        public double FGiaTriPheDuyetVND { get; set; }
        public double FGiaTriPheDuyetEUR { get; set; }
        public Guid? IIdGoiThauID { get; set; }
        public bool IsChecked { get; set; }
    }
}
