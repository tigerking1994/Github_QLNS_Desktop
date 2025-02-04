using System;
using System.Collections.Generic;
using System.Text;

namespace VTS.QLNS.CTC.App.Model
{
    public class ExportChiPhiQuyetToanDuAnHoanThanhModel
    {
        public string Stt { get; set; }
        public string NoiDung { get; set; }
        public string Ma { get; set; }
        public double TheoQuyetDinhPheDuyet { get; set; }
        public double KetQuaThanhTraKiemToan { get; set; }
        public double DeNghiQuyetToan { get; set; }
        public double TangGiamSoVoiDuToan { get; set; }
        public double QuyetToanAB { get; set; }
        public bool IsHangCha { get; set; }
        public string MaDuAn { get; set; }
        public string TenDuAn { get; set; }
    }
}
