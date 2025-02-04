using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VTS.QLNS.CTC.App.Model
{
    public class NhDaChenhLechTiGiaModel : ModelBase
    {
        public string position { get; set; }
        public string sTen { get; set; }
        public string sTenDisplay => IsHangCha ? string.Concat(position, ". ", sTen) : sTen;
        public double? fTienKHTTBQPCapUSD { get; set; }
        public double? fTienKHTTBQPCapVND { get; set; }
        public double? fTienTheoHopDongUSD { get; set; }
        public double? fTienTheoHopDongVND { get; set; }
        public double? fKinhPhiDuocCapChoCDTUSD { get; set; }
        public double? fKinhPhiDuocCapChoCDTVND { get; set; }
        public double? fKinhPhiDaThanhToanUSD { get; set; }
        public double? fKinhPhiDaThanhToanVND { get; set; }
        public double? fTiGiaCLHopDongVsCDTUSD { get; set; }
        public double? fTiGiaCLHopDongVsCDTVND { get; set; }
        public double? fTiGiaCLKinhPhiDuocCapVsGiaiNganUSD { get; set; }
        public double? fTiGiaCLKinhPhiDuocCapVsGiaiNganVND { get; set; }
        public double? fTienTheoDuAnUSD { get; set; }
        public double? fTienTheoDuAnVND { get; set; }
    }
}
