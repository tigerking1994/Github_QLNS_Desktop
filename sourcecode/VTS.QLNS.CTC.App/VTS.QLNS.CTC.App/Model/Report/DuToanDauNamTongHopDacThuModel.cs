using System;

namespace VTS.QLNS.CTC.App.Model.Report
{
    public class DuToanDauNamTongHopDacThuModel
    {
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
        public string MoTa { get; set; }
        public double TongSoNamTruoc
        {
            get => DaPhanCapNamTruoc + ChuaPhanCapNamTruoc;
        }
        public double TongSoNamNay
        {
            get => DaPhanCapNamNay + ChuaPhanCapNamNay;
        }
        public double DaPhanCapNamTruoc { get; set; }
        public double DaPhanCapNamNay { get; set; }
        public double ChuaPhanCapNamNay { get; set; }
        public double ChuaPhanCapNamTruoc { get; set; }
        public bool IsHangCha { get; set; }

        public string XauNoiMa { get; set; }
        public Guid? MlnsId { get; set; }
        public Guid? MlnsIdParent { get; set; }
    }
}
