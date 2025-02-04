using System;
using System.Collections.Generic;
using System.Text;

namespace VTS.QLNS.CTC.App.Model
{
    public class HeaderReportSoNhuCauTongHopPhuLuc1
    {
        public String TenDonVi { get; set; }
        public string Cong => !string.IsNullOrEmpty(TenDonVi) ? "Cộng" : "";
        public String TenTuChi => !string.IsNullOrEmpty(TenDonVi) ? "Chi bằng tiền" : "";
        public String TenHuyDong => !string.IsNullOrEmpty(TenDonVi) ? "Huy động tồn kho" : "";
        public String TenMuaHangHienVat => !string.IsNullOrEmpty(TenDonVi) ? "Mua hàng cấp hiện vật" : "";
        public String TenDacThu => !string.IsNullOrEmpty(TenDonVi) ? "Đặc thù" : "";
    }
}
