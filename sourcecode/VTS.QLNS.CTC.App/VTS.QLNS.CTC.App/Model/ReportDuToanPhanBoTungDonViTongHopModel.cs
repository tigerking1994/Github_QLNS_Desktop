using System;
using System.Collections.Generic;
using System.Text;

namespace VTS.QLNS.CTC.App.Model
{
    public class ReportDuToanPhanBoTungDonViTongHopModel
    {
        public Guid IIdMlns { get; set; }
        public Guid? IIdMlnsParent { get; set; }
        public string LNS { get; set; }
        public string L { get; set; }
        public string K { get; set; }
        public string M { get; set; }
        public string TM { get; set; }
        public string TTM { get; set; }
        public string NG { get; set; }
        public string TNG { get; set; }
        public string TNG1 { get; set; }
        public string TNG2 { get; set; }
        public string TNG3 { get; set; }
        public string XauNoiMa { get; set; }
        public bool IsHangCha { get; set; }
        public string MoTa { get; set; }
        public double TongCong { get; set; }
        public List<DuLieu> LstData { get; set; }
        public List<DuLieu> LstDataTotal { get; set; }
    }

    public class DuLieu
    {
        public double GiaTri { get; set; }
    }
}
