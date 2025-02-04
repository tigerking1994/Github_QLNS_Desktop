using System;
using System.Collections.Generic;
using System.Text;

namespace VTS.QLNS.CTC.App.Model
{
    public class ExportNguonVonPheDuyetQuyetToanDuAnHoanThanhModel
    {
        public string Stt { get; set; }
        public string MaNguonVon { get; set; }
        public string NoiDung { get; set; }
        public double GiaTriPheDuyet { get; set; }
        public double TienDeNghi { get; set; }
        public double TienQuyetToan { get; set; }
        public bool IsHangCha { get; set; }
        public int IdNguonVon { get; set; }
        public string MaDuAn { get; set; }
        public string TenDuAn { get; set; }
    }
}
