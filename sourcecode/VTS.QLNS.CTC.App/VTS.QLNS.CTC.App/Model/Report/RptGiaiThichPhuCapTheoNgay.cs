using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VTS.QLNS.CTC.Core.Domain.Query;

namespace VTS.QLNS.CTC.App.Model.Report
{

    public class RptGiaiThichPhuCapTheoNgay : BindableBase
    {
        public int iStt { get; set; }
        public string sTenCbo { get; set; }
        public string sMaCb { get; set; }
        public string sMaCanbo { get; set; }
        public bool isParent { get; set; }
        public List<RptGiaiThichPhuCapTheoNgayData> ListGiaTri { get; set; }
        public List<RptGiaiThichPhuCapTheoNgayData> ListGiaTriTotal { get; set; }
        public decimal TongCong { get; set; }
    }

    public class RptGiaiThichPhuCapTheoNgayData
    {
        public decimal GiaTri { get; set; }
        public decimal SoNgay { get; set; }
        public decimal SoTienTheoNgay { get; set; }
    }


}
