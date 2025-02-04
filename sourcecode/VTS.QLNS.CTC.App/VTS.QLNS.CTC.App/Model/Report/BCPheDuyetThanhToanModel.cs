using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VTS.QLNS.CTC.App.Model.Report
{
    public class BCPheDuyetThanhToanModel
    {
        public string sTen { get; set; }
        public string sNoiDung { get; set; }
        public double? fGiaTriUSD { get; set; }
        public double? fGiaTriVND { get; set; }
        public Guid? IdParent { get; set; }
        public Guid? Id { get; set; }
        public int Level { get; set; }
    }
}
