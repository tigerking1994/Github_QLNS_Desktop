using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VTS.QLNS.CTC.App.Model.Report
{
    public class QuyetToanTongHopDonViTotal
    {
        public double TongDuToan { get; set; }
        public double TongQuyetToan { get; set; }
        public double TongTrongKy { get; set; }
        public double TongChenhLech => TongDuToan - TongQuyetToan;
        public double TongQuyetToan1 { get; set; }
        public double TongQuyetToan2 { get; set; }
        public double TongQuyetToan3 { get; set; }
        public double TongQuyetToan4 { get; set; }
        public double TongQuyetToan5 { get; set; }
        public double TongQuyetToan6 { get; set; }
        public double TongQuyetToan7 { get; set; }
        public double TongQuyetToan8 { get; set; }
        public double TongQuyetToan9 { get; set; }
        public double TongQuyetToan10 { get; set; }
        public double TongDuToan1 { get; set; }
        public double TongDuToan2 { get; set; }
        public double TongDuToan3 { get; set; }
        public double TongDuToan4 { get; set; }
        public double TongDuToan5 { get; set; }
        public double TongDuToan6 { get; set; }
        public double TongDuToan7 { get; set; }
        public double TongDuToan8 { get; set; }
        public double TongDuToan9 { get; set; }
        public double TongDuToan10 { get; set; }
        public double TongChenhLech1 => TongDuToan1 - TongQuyetToan1;
        public double TongChenhLech2 => TongDuToan2 - TongQuyetToan2;
        public double TongChenhLech3 => TongDuToan3 - TongQuyetToan3;
        public double TongChenhLech4 => TongDuToan4 - TongQuyetToan4;
        public double TongChenhLech5 => TongDuToan5 - TongQuyetToan5;
        public double TongChenhLech6 => TongDuToan6 - TongQuyetToan6;
        public double TongChenhLech7 => TongDuToan7 - TongQuyetToan7;
        public double TongChenhLech8 => TongDuToan8 - TongQuyetToan8;
        public double TongChenhLech9 => TongDuToan9 - TongQuyetToan9;
        public double TongChenhLech10 => TongDuToan10 - TongQuyetToan10;
    }
}
