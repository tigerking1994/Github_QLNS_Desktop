using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;

namespace VTS.QLNS.CTC.Core.Domain.Query
{
    public class VdtKtKhoiTaoDuLieuChiTietThanhToanQuery
    {
        public Guid? IIDHopDongId { get; set; }
        public Guid IIdKhoiTaoDuLieuChiTietId { get; set; }
        public string STenNhaThau { get; set; }
        public double? FTienHopDong { get; set;  }
        public double? FLuyKeTtklhtTnKhvn { get; set; }
        public double? FLuyKeTUChuaThuHoiTnKhvn { get; set; }
        public double? FLuyKeTtklhtNnKhvn { get; set; }
        public double? FLuyKeTUChuaThuHoiNnKhvn { get; set; }
        public double? FLuyKeTtklhttnKhvu { get; set; }
        public double? FLuyKeTUChuaThuHoiTnKhvu { get; set; }
        public double? FLuyKeTtklhtNnKhvu { get; set; }
        public double? FLuyKeTUChuaThuHoiNnKhvu { get; set; }
        public Guid? IIDChiPhiId { get; set; }
        public int? ILoai { get; set; }
    }
}
