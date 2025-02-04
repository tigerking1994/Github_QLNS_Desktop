using System;
using System.Collections.Generic;
using System.Text;
using VTS.QLNS.CTC.App.Service.Impl;

namespace VTS.QLNS.CTC.App.Model
{
    public class ReportDuToanNganSachTheoDotModel
    {
        public string Slns { get; set; }
        public string SM { get; set; }
        public string STm { get; set; }
        public string Sttm { get; set; }
        public string SNg { get; set; }
        public bool IsHangCha { get; set; }
        public string SMoTa { get; set; }
        public double Tong { get; set; }
        public List<DtChungTuChiTietModel> LstGiaTri { get; set; }
        public List<DtChungTuChiTietModel> LstGiaTriTotal { get; set; }
    }
}
