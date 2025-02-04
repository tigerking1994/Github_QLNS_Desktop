using System;
using System.Collections.Generic;
using System.Text;

namespace VTS.QLNS.CTC.App.Model
{
    public class BaoCaoKetQuaGiaiNganChiKPDTModel
    {
        public int IStt { get; set; }
        public string STenDonVi { get; set; }
        public double FDuToanDeNghiChuyen { get; set; }
        public double FNamNay { get; set; }
        public double FCongDuToan { get; set; }
        public double FDuToanDuocThongBao { get; set; }
        public double FCucTaiChinhThanhToanTrucTiep { get; set; }
        public double FThanhToanKLHTBQP { get; set; }
        public double FTamUngBQP { get; set; }
        public double FCongBQP { get; set; }
        public double FDuToanDuocThongBaoKhoBac { get; set; }
        public double FThanhToanKLHTKhoBac { get; set; }
        public double FTamUngKhoBac { get; set; }
        public double FCongKhoBac { get; set; }
        public bool IsHangCha { get; set; }
        public string FTiLeBQP { get; set; }
        public string FTiLeKhoBac { get; set; }
    }
}
