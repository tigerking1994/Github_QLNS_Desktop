using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VTS.QLNS.CTC.Core.Domain.Query
{
    public class NhBaoCaoTinhHinhThucHienDuAnQuery
    {
        public Guid? ID { get; set; }
        public string sSoDeNghi { get; set; }
        public DateTime? dNgayDeNghi { get; set; }
        public string sChuDauTu { get; set; }
        public string TenNhaThau { get; set; }
        public int? iLoaiNoiDungChi { get; set; }
        public int? iCoQuanThanhToan { get; set; }
        public int? iLoaiDeNghi { get; set; }
        public string Mlns { get; set; }
        public Guid? IdHopDong { get; set; }
        public string SoHopDong { get; set; }       
        public double? fTongDeNghi_USD { get; set; }
        public double? fTongDeNghi_VND { get; set; }
        public double? fTongPheDuyet_BangSo_USD { get; set; }
        public double? fTongPheDuyet_BangSo_VND { get; set; }
        [NotMapped]
        public double? TongCongGiaiNgan { get; set; }
        [NotMapped]
        public bool? IsHangCha { get; set; }
        public double? fGiaTriDuocCap_USD { get; set; }
        public double? fGiaTriDuocCap_VND { get; set; }
        public double? fGiaTriTTTU_USD { get; set; }
        public double? fGiaTriTTTU_VND { get; set; }
    }
}
