using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VTS.QLNS.CTC.App.Model.Report
{
    public class QuyetToanTongHopDonViHeader
    {
        public string Header1 { get; set; }
        public string Header2 { get; set; }
        public string Header3 { get; set; }
        public string Header4 { get; set; }
        public string Header5 { get; set; }
        public string Header6 { get; set; }
        public string Header7 { get; set; }
        public string Header8 { get; set; }
        public string Header9 { get; set; }
        public string Header10 { get; set; }
    }

    public class QuyetToanDynamicValue
    {
        public double QuyetToanValue { get; set; }
        public double DuToanValue { get; set; }
        public double FvalCommon { get; set; }
    }

    public class QuyetToanTongHopDonViHeaderDynamic
    {
        public string Header { get; set; }
        public int STT { get; set; }
        public string MergeRange { get; set; }
    }
}
