using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VTS.QLNS.CTC.App.Model
{
    public class NhDaThongTinNhaThauHopDongModel : BindableBase
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
        public string DNgayHopDongString => DNgayHopDong.HasValue ? DNgayHopDong.Value.ToString("dd/MM/yyyy") : string.Empty;
    }
}
