using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VTS.QLNS.CTC.App.Model
{
    public class NhTtThongTriCapPhatChiTietModel : ModelBase
    {
        public Guid? IIdThongTriCapPhatId { get; set; }
        public Guid? IIdPheDuyetThanhToanId { get; set; }
        public string SMaOrder { get; set; }
        public int? ITrangThai { get; set; }
        public string SSoDeNghi { get; set; }
        public string STenNhiemVuChi { get; set; }
        public string STenHopDong { get; set; }
        public string SLoaiDeNghi { get; set; }
        public double? FPheDuyetUsd { get; set; }
        public double? FPheDuyetVnd { get; set; }
        public double? FPheDuyetEur { get; set; }
        public double? FPheDuyetNgoaiTeKhac { get; set; }
        public string STenTrangThai { get; set; }
        public int iNamKeHoach { get; set; }
        public Guid? iID_DonVi { get; set; }
        public int iID_NguonVonID { get; set; }
    }
}
