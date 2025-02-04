using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace VTS.QLNS.CTC.Core.Domain.Query
{
    public class ReportTinhHinhDuAnQuery
    {
        public bool IsHangCha { get; set; }
        public int? NguonVonId { get; set; }
        public int? Loai { get; set; }
        public string Mlns { get; set; }
        public string TenNhaThau { get; set; }
        public string SoHopDong { get; set; }
        public int? ThoiGianThucHien { get; set; }
        public double? TienHopDong { get; set; }
        public string SoDeNghi { get; set; }
        public Guid? IdDeNghiThanhToan { get; set; }
        public DateTime? NgayThanhToan { get; set; }
        public double? SoThanhToan { get; set; }
        public double? SoTamUng { get; set; }
        public double? SoThuHoiTamUng { get; set; }
        public double? TongCongGiaiNgan { get; set; }
        public DateTime? NgayCapUng { get; set; }
        public double? SoDaCapUng { get; set; }
    }
}
