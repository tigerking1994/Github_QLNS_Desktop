using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VTS.QLNS.CTC.Core.Domain.Query
{
    public partial class BhKhcKinhphiQuanlyChiTietQuery
    {
        public Guid iID_BH_KHC_KinhPhiQuanLy_ChiTiet { get; set; }
        public Guid? IID_KHC_KinhPhiQuanLy { get; set; }
        public Guid? IID_MucLucNganSach { get; set; }
        public string SM { get; set; }
        public string STM { get; set; }
        public string SNoiDung { get; set; }
        public double? FTienDaThucHienNamTruoc { get; set; }
        public double? FTienUocThucHienNamTruoc { get; set; }
        public double? FTienKeHoachThucHienNamNay { get; set; }
        public double? FTienCanBo { get; set; }
        public double? FTienQuanLuc { get; set; }
        public double? FTienTaiChinh { get; set; }
        public double? FTienQuanY { get; set; }
        public string SGhiChu { get; set; }
        public DateTime? DNgaySua { get; set; }
        public DateTime? DNgayTao { get; set; }
        public string SNguoiSua { get; set; }
        public string SNguoiTao { get; set; }
        public string IIDMaDonVi { get; set; }

        public int INamLamViec { get; set; }
        public string SXauNoiMa { get; set; }
    }
}
