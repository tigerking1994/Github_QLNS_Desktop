using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VTS.QLNS.CTC.Core.Domain.Query
{
    public class BhKhcKcbChiTietQuery
    {
        public Guid Id { get; set; }
        public Guid IID_KHC_KCB { get; set; }
        public Guid IID_MucLucNganSach { get; set; }
        public string SNoiDung { get; set; }
        public double? FTienDaThucHienNamTruoc { get; set; }
        public double? FTienUocThucHienNamTruoc { get; set; }
        public double? FTienKeHoachThucHienNamNay { get; set; }
        public string SGhiChu { get; set; }
        public DateTime? DNgaySua { get; set; }
        public DateTime? DNgayTao { get; set; }
        public string SNguoiSua { get; set; }
        public string SNguoiTao { get; set; }
        public string SXauNoiMa { get; set; }

        public int INamLamViec { get; set; }
        public string IIdMaDonVi { get; set; }
        public bool IsRemainRow { get; set; }
        public bool BHangCha { get; set; }
        public int IRemainRow { get; set; }

    }
}
