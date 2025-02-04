using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VTS.QLNS.CTC.Core.Domain.Query
{
    public class NhDaHopDongGoiThauNhaThauQuery
    {
        public Guid Id { get; set; }
        public Guid? IIdHopDongId { get; set; }
        public Guid? IIdGoiThauId { get; set; }
        public Guid? IIDGoiThauCheck { get; set; }
        public Guid? IIdNhaThauId { get; set; }
        public double? FGiaTriUsd { get; set; }
        public double? FGiaTriVnd { get; set; }
        public double? FGiaTriEur { get; set; }
        public double? FGiaTriNgoaiTeKhac { get; set; }
        public double? FGiaTriHopDong_Usd { get; set; }
        public double? FGiaTriHopDong_Vnd { get; set; }
        public double? FGiaTriHopDong_Eur { get; set; }
        public double? FGiaTriHopDong_NgoaiTeKhac { get; set; }
        public double? FGiaTriGoiThauUsd { get; set; }
        public double? FGiaTriGoiThauVnd { get; set; }
        public double? FGiaTriGoiThauEur { get; set; }
        public double? FGiaTriGoiThauNgoaiTeKhac { get; set; }
        public string STenGoiThau { get; set; }
        public string STenNhaThau { get; set; }
        public int IThoiGianThucHien { get; set; }
        public int IsCheck { get; set; }
        public Guid? IIdDonViTienTeId { get; set; }
    }
}
