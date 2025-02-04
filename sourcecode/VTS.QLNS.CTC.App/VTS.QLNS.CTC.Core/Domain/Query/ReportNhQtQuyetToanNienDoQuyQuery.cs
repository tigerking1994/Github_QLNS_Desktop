using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VTS.QLNS.CTC.Core.Domain.Query
{
    public class ReportNhQtQuyetToanNienDoQuyQuery : ReportNhQtQuyetToanNienDoQuery
    {
        public double? FHopDongUsd { get; set; } // 1
        public double? FHopDongVnd { get; set; } // 2
        public double? FKeHoachTtcpTongSoUsd { get; set; } // 3
        public double? FKeHoachTtcpGianDoanUsd { get; set; } // 4
        public double? FKeHoachBqpTongSoUsd { get; set; } // 5
        public double? FKeHoachBqpGiaiDoanUsd { get; set; } // 6
        public double? FKinhPhiDuocCapTongSoUsd { get; set; } // 7=9+11
        public double? FKinhPhiDuocCapTongSoVnd { get; set; } // 8=10+12
        public double? FKinhPhiDuocCapCacNamTruocUsd { get; set; } // 9
        public double? FKinhPhiDuocCapCacNamTruocVnd { get; set; } // 10
        public double? FKinhPhiDuocCapDenQuyNayUsd { get; set; } // 11
        public double? FKinhPhiDuocCapDenQuyNayVnd { get; set; } // 12
        public double? FKinhPhiGiaiNganTongSoUsd { get; set; } // 13=15+17
        public double? FKinhPhiGiaiNganTongSoVnd { get; set; } // 14=16+18
        public double? FKinhPhiGiaiNganCacNamTruocUsd { get; set; } // 15
        public double? FKinhPhiGiaiNganCacNamTruocVnd { get; set; } // 16
        public double? FKinhPhiGiaiNganDenQuyNayUsd { get; set; } // 17
        public double? FKinhPhiGiaiNganDenQuyNayVnd { get; set; } // 18
        public double? FKinhPhiDuocCapChuaChiDenQuyNayUsd { get; set; } // 19=7-13
        public double? FKinhPhiDuocCapChuaChiDenQuyNayVnd { get; set; } // 20=8-14
        public double? FKeHoachChuaGiaiNgan { get; set; } // 21=5-7
    }
}
