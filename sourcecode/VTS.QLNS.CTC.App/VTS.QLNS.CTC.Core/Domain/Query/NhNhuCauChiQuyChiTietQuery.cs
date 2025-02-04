using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VTS.QLNS.CTC.Core.Domain.Query
{
    public class NhNhuCauChiQuyChiTietQuery
    {
        public Guid? IIdNhuCauChiQuyId { get; set; }
        public Guid? IIdKhttNhiemVuChiId { get; set; }
        public Guid? IIdHopDongId { get; set; }
        public string SNoiDung { get; set; }
        public double? FNhuCauQuyNayNgoaiTeKhac { get; set; }
        public double? FNhuCauQuyNayUsd { get; set; }
        public double? FNhuCauQuyNayEur { get; set; }
        public double? FNhuCauQuyNayVnd { get; set; }
        public Guid? IIdParentId { get; set; }
        public Guid? IIdDuAnId { get; set; }
        public double? FKinhPhiDuocCapCacNamTruocSetUSD { get; set; }
        public double? FKinhPhiDuocCapCacNamTruocSetVND { get; set; }
        public double? FKinhPhiDuocCapCacNamTruocSetEUR { get; set; }
        public double? FKinhPhiDuocCapCacNamTruocSetNgoaiTeKhac { get; set; }
        public double? FKinhPhiDaChiCacNamTruocSetUSD { get; set; }
        public double? FKinhPhiDaChiCacNamTruocSetVND { get; set; }
        public double? FKinhPhiDaChiCacNamTruocSetEUR { get; set; }
        public double? FKinhPhiDaChiCacNamTruocSetNgoaiTeKhac { get; set; }
        public double? FKinhPhiDuocCapDenCuoiQuyTruocSetUSD { get; set; }
        public double? FKinhPhiDuocCapDenCuoiQuyTruocSetVND { get; set; }
        public double? FKinhPhiDuocCapDenCuoiQuyTruocSetEUR { get; set; }
        public double? FKinhPhiDuocCapDenCuoiQuyTruocSetNgoaiTeKhac { get; set; }
        public double? FKinhPhiDaChiDenCuoiQuyTruocSetUSD { get; set; }
        public double? FKinhPhiDaChiDenCuoiQuyTruocSetVND { get; set; }
        public double? FKinhPhiDaChiDenCuoiQuyTruocSetEUR { get; set; }
        public double? FKinhPhiDaChiDenCuoiQuyTruocSetNgoaiTeKhac { get; set; }
        public double? FNoiDungChiUSD { get; set; }
        public double? FNoiDungChiVND { get; set; }
    }
}
