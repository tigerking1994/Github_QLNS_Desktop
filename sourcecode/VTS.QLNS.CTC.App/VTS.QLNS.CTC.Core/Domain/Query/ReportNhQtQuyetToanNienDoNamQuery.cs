using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VTS.QLNS.CTC.Core.Domain.Query
{
    public class ReportNhQtQuyetToanNienDoNamQuery : ReportNhQtQuyetToanNienDoQuery
    {
        public double? FHopDongUsd { get; set; } // 1
        public double? FHopDongVnd { get; set; } // 2
        public double? FKeHoachTtcpUsd { get; set; } // 3
        public double? FKeHoachBqpUsd { get; set; } // 4
        public double? FQtKinhPhiDuyetCacNamTruocUsd { get; set; } // 5
        public double? FQtKinhPhiDuyetCacNamTruocVnd { get; set; } // 6
        public double? FQtKinhPhiDuocCapTongSoUsd { get; set; } // 7=9+11
        public double? FQtKinhPhiDuocCapTongSoVnd { get; set; } // 8=10+12
        public double? FQtKinhPhiDuocCapNamTruocChuyenSangUsd { get; set; } // 9
        public double? FQtKinhPhiDuocCapNamTruocChuyenSangVnd { get; set; } // 10
        public double? FQtKinhPhiDuocCapNamNayUsd { get; set; } // 11
        public double? FQtKinhPhiDuocCapNamNayVnd { get; set; } // 12
        public double? FDeNghiQtNamNayUsd { get; set; } // 13
        public double? FDeNghiQtNamNayVnd { get; set; } // 14
        public double? FDeNghiChuyenNamSauUsd { get; set; } // 15
        public double? FDeNghiChuyenNamSauVnd { get; set; } // 16
        public double? FThuaThieuKinhPhiTrongNamUsd { get; set; } // 17=7-13-15 
        public double? FThuaThieuKinhPhiTrongNamVnd { get; set; } // 18=8-14-16
        public double? FThuaNopNsnnUsd { get; set; } // 19
        public double? FThuaNopNsnnVnd { get; set; } // 20
        public double? FLuyKeKinhPhiDuocCapUsd { get; set; } // 21
        public double? FLuyKeKinhPhiDuocCapVnd { get; set; } // 22
        public double? FKeHoachChuaGiaiNganUsd { get; set; }  // 23=4-7
    }
}
