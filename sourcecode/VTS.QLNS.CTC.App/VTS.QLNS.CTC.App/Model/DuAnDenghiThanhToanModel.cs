using System;
using System.Collections.Generic;
using System.Text;
using VTS.QLNS.CTC.App.Model.Control;

namespace VTS.QLNS.CTC.App.Model
{
    public class DuAnDenghiThanhToanModel : CheckBoxItem
    {
        public Guid iID_DuAnID { get; set; }
        public string sTenDuAn { get; set; }
        public string sMaDuAn { get; set; }
        public string TenPhanCap { get; set; }
        public string SoQuyetDinhDauTu { get; set; }
        public double? GiaTriDauTu { get; set; }
        public double? HanMucDauTu { get; set; }
        public double? fTongMucDauTuPheDuyet { get; set; }
        public string sTrangThaiDuAnDangKy { get; set; }
    }
}
