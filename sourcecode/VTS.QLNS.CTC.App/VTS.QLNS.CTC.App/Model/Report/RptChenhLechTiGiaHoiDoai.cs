using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VTS.QLNS.CTC.App.Model.Report
{
    public class RptChenhLechTiGiaHoiDoai
    {
        public string position { get; set; }
        public string sTen { get; set; }
        public string sTenDisplay => string.Concat(position, ". ", sTen);
        public string sTienKHTTBQPCapUSD { get; set; }
        public string sTienKHTTBQPCapVND { get; set; }
        public string sTienTheoDuAnUSD { get; set; }
        public string sTienTheoDuAnVND { get; set; }
        public string sTienTheoHopDongUSD { get; set; }
        public string sTienTheoHopDongVND { get; set; }
        public string sKinhPhiDuocCapChoCDTUSD { get; set; }
        public string sKinhPhiDuocCapChoCDTVND { get; set; }
        public string sKinhPhiDaThanhToanUSD { get; set; }
        public string sKinhPhiDaThanhToanVND { get; set; }
        public string sTiGiaCLHopDongVsCDTUSD { get; set; }
        public string sTiGiaCLHopDongVsCDTVND { get; set; }
        public string sTiGiaCLKinhPhiDuocCapVsGiaiNganUSD { get; set; }
        public string sTiGiaCLKinhPhiDuocCapVsGiaiNganVND { get; set; }
        public bool IsHangCha { get; set; }
    }
}
