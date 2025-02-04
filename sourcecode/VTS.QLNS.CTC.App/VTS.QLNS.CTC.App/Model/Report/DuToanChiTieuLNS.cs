using System.Collections.Generic;

namespace VTS.QLNS.CTC.App.Model.Report
{
    public class DuToanChiTieuLNS
    {
        public string TenDonVi { get; set; }
        public double Val { get; set; }
        public double ValTuChi { get; set; }
        public double ValHienVat { get; set; }
        public double Val1 { get; set; }
        public double Val2 { get; set; }
        public double Val3 { get; set; }
        public double Val4 { get; set; }
        public double Val5 { get; set; }
        public double Val6 { get; set; }
        public double Val7 { get; set; }
        public double Val8 { get; set; }
        public double Val9 { get; set; }
        public double Val10 { get; set; }
        public double ValConLai { get; set; }
        public double ValTuChiConLai { get; set; }
        public double ValHienVatConLai { get; set; }
        public double Val1ConLai { get; set; }
        public double Val2ConLai { get; set; }
        public double Val3ConLai { get; set; }
        public double Val4ConLai { get; set; }
        public double Val5ConLai { get; set; }
        public double Val6ConLai { get; set; }
        public double Val7ConLai { get; set; }
        public double Val8ConLai { get; set; }
        public double Val9ConLai { get; set; }
        public double Val10ConLai { get; set; }
        public List<DuToanChiTieuLNSDynamicColumn> LstGiaTri { get; set; }
    }

    public class DuToanChiTieuLNSDynamicColumn
    {
        public int STT { get; set; }
        public string sMucLucNganSach { get; set; }
        public string sLNS { get; set; }
        public double fTuChi { get; set; }
    }
}
