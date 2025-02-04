using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VTS.QLNS.CTC.Core.Domain.Query
{
    public class VdtKhvVonNamDonViPheDuyetDuocDuyetQuery
    {
        public Guid? IdLoaiCongTrinh { get; set; }
        public Guid? IdLoaiCongTrinhParent { get; set; }
        public string sTenDuAn { get; set; }
        public string sLNS { get; set; }
        public string sL { get; set; }
        public string sK { get; set; }
        public string sM { get; set; }
        public string sTM { get; set; }
        public string sTTM { get; set; }
        public string sNG { get; set; }
        public double? FCapPhatTaiKhoBac { get; set; }
        public double? FCapPhatBangLenhChi { get; set; }
        public double? FGiaTriThuHoiNamTruocKhoBac { get; set; }
        public double? FGiaTriThuHoiNamTruocLenhChi { get; set; }
        public double? TongSo { get; set; }
        public int? Loai { get; set; }
        public bool IsHangCha { get; set; }
        public int? LoaiParent { get; set; }
        public Guid? IIdDuAn { get; set; }
        public int? LoaiCongTrinh { get; set; }
    }
}
