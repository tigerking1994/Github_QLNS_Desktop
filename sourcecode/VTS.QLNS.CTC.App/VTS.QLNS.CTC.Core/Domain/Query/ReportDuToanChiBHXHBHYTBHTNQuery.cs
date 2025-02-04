using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VTS.QLNS.CTC.Core.Domain.Query
{
    public class ReportDuToanChiBHXHBHYTBHTNQuery
    {
        public string STT { get; set; }
        public string STenDonVi { get; set; }
        public string SMaDonVi { get; set; }
        public double? FTienTroCapOmDau { get; set; }
        public double? FTienTroCapThaiSan { get; set; }
        public double? FTienTroTNLDBNN { get; set; }
        public double? FTienTroCapHuuTri { get; set; }
        public double? FTienTroCapPhucVien { get; set; }
        public double? FTienTroCapXuatNgu { get; set; }
        public double? FTienTroCapThoiViec { get; set; }
        public double? FTienTroCapTuTuat { get; set; }
        public double? FTienTongCongBHXH => FTienTroCapOmDau + FTienTroTNLDBNN + FTienTroCapThaiSan + FTienTroCapHuuTri + FTienTroCapPhucVien + FTienTroCapXuatNgu + FTienTroCapThoiViec + FTienTroCapTuTuat;
        public double? FTienHoTroCanBo { get; set; }
        public double? FTienChiTapHuan { get; set; }
        public double? FTienHoTroQuanLy { get; set; }
        public double? FTienQuanLyNganhCB { get; set; }
        public double? FTienQuanLyNganhQL { get; set; }
        public double? FTienQuanLyNganhTC { get; set; }
        public double? FTienQuanLyNganhQY { get; set; }
        public double? FTienTongCongKQPL => FTienHoTroCanBo.GetValueOrDefault(0) + FTienChiTapHuan.GetValueOrDefault(0) + FTienHoTroQuanLy.GetValueOrDefault(0) + FTienQuanLyNganhCB.GetValueOrDefault(0) + FTienQuanLyNganhQL.GetValueOrDefault(0) + FTienQuanLyNganhTC.GetValueOrDefault(0) + FTienQuanLyNganhQY.GetValueOrDefault(0);
        public double? FTienChiKCBQYDV { get; set; }
        public double? FTienChiKCBTSDK { get; set; }
        public double? FTienChiTNKDQKCBBHYT { get; set; }
        public double? FTienKPMSTTBYT { get; set; }
        public double? FTienChiKPCSSK { get; set; }
        public double? FTienChiHTBHTN { get; set; }
        public double? FTienTongCongAll => FTienTongCongBHXH + FTienTongCongKQPL + FTienChiKCBQYDV + FTienChiKCBTSDK + FTienChiTNKDQKCBBHYT + FTienKPMSTTBYT + FTienChiKPCSSK + FTienChiHTBHTN;
        public bool BHangCha { get; set; }
        public int? IdParent { get; set; }
        public int? Type { get; set; }
        public int? Child { get; set; }
    }
}
