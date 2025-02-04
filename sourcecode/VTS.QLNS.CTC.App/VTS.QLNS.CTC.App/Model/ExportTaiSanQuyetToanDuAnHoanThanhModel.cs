using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace VTS.QLNS.CTC.App.Model
{
    public class ExportTaiSanQuyetToanDuAnHoanThanhModel : DetailModelBase
    {
        public int Stt { get; set; }
        public string MaDuAn { get; set; }
        public string TenDuAn { get; set; }
        public string Nhom { get; set; }
        public double CDTQuanLy { get; set; }
        public double DonViKhacQuanLy { get; set; }
        public string sCDTQuanLy
        {
            get
            {
                return CDTQuanLy != 0 ? CDTQuanLy.ToString("##,#", CultureInfo.GetCultureInfo("vi-VN")) : "";
            }
        }
        public string sDonViKhacQuanLy
        {
            get
            {
                return DonViKhacQuanLy != 0 ? DonViKhacQuanLy.ToString("##,#", CultureInfo.GetCultureInfo("vi-VN")) : "";
            }
        }
    }
}
