using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VTS.QLNS.CTC.Core.Domain.Query
{
    public class BhPbdtcBHXHChiBHXHReportQuery
    {
        public string STT { get; set; }
        public string STenDonVi { get; set; }
        public string IID_MaDonVi { get; set; }
        public double? FTienTroCapOmDau { get; set; }
        public double? FTienTroCapThaiSan { get; set; }
        public double? FTienTroCapTaiNan { get; set; }
        public double? FTienTroCapHuuTri { get; set; }
        public double? FTienTroCapPhucVien { get; set; }
        public double? FTienTroCapXuatNgu { get; set; }
        public double? FTienTroCapThoiViec { get; set; }
        public double? FTienTroCapTuTuat { get; set; }
        public bool IsHangCha { get; set; }
        public int RowNumber { get; set; }
        public double? FTongTienDuToan { get; set; }
        public double? Cong => FTienTroCapOmDau.GetValueOrDefault(0) + FTienTroCapThaiSan.GetValueOrDefault(0) + FTienTroCapTaiNan.GetValueOrDefault(0) + FTienTroCapHuuTri.GetValueOrDefault(0) +
            FTienTroCapPhucVien.GetValueOrDefault(0) + FTienTroCapXuatNgu.GetValueOrDefault(0) + FTienTroCapThoiViec.GetValueOrDefault(0) + FTienTroCapTuTuat.GetValueOrDefault(0);

        public double? FCong => Math.Round(FTongTienDuToan.GetValueOrDefault());
    }
}
