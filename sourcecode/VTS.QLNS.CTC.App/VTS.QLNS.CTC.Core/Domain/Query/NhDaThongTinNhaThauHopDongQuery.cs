using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;

namespace VTS.QLNS.CTC.Core.Domain.Query
{
    public class NhDaThongTinNhaThauHopDongQuery
    {
        public Guid? IIdGoiThauId { get; set; }
        public Guid? IIdHopDongId { get; set; }
        public string SSoHopDong { get; set; }
        public DateTime? DNgayHopDong { get; set; }
        public string STenNhaThau { get; set; }
        public string STenLoaiHopDong { get; set; }
        public double? FGiaTriUSD { get; set; }
        public double? FGiaTriVND { get; set; }
        public double? FGiaTriEUR { get; set; }
        public double? FGiaTriNgoaiTeKhac { get; set; }
    }
}
