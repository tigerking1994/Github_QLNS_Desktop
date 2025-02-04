using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VTS.QLNS.CTC.Core.Domain
{
    public class VdtKtKhoiTaoDuLieuChiTietThanhToan : EntityBase
    {
        public Guid Id { get; set; }
        public Guid? IIdKhoiTaoDuLieuChiTietId { get; set; }
        public Guid? IIDHopDongId { get; set; }
        public double? FLuyKeTtklhtTnKhvn { get; set; }
        public double? FLuyKeTUChuaThuHoiTnKhvn { get; set; }
        public double? FLuyKeTtklhtNnKhvn { get; set; }
        public double? FLuyKeTUChuaThuHoiNnKhvn { get; set; }
        public double? FLuyKeTtklhtTnKhvu { get; set; }
        public double? FLuyKeTUChuaThuHoiTnKhvu { get; set; }
        public double? FLuyKeTtklhtNnKhvu { get; set; }
        public double? FLuyKeTUChuaThuHoiNnKhvu { get; set; }
        public Guid? IIDChiPhiId { get; set; }
        public int? ILoai { get; set; }
    }
}
