using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VTS.QLNS.CTC.Core.Domain.Query
{
    public class ReportNHTongHopThongTinDuAnQuery
    {
        public string STenDuAn { get; set; }
        public string TenDonVi { get; set; }
        public string TienTe { get; set; }
        public string SMaDonVi { get; set; }
        public string SoQuyetDinhChuTruong { get; set; }
        public DateTime? NgayQuyetDinhChuTruong { get; set; }
        public double? FGiaTriUSDChuTruong { get; set; }
        public double? FGiaTriEuroChuTruong { get; set; }
        public double? FGiaTriVNDChuTruong { get; set; }
        public double? FGiaTriKhacChuTruong { get; set; }
        public string SoQuyetDinhDauTu { get; set; }
        public DateTime? NgayQuyetDinhDauTu { get; set; }
        public double? FGiaTriUSDDauTu { get; set; }
        public double? FGiaTriEuroDauTu { get; set; }
        public double? FGiaTriVNDDauTu { get; set; }
        public double? FGiaTriKhacDauTu { get; set; }
    }
}
