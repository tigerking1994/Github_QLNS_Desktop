using System;
using System.Collections.Generic;
using System.Text;

namespace VTS.QLNS.CTC.App.Model.Report
{
    public class DuToanDauNamChiThuongXuyenModel
    {
        public string SLNS
        {
            get => XauNoiMa?.Substring(0, 7);
        }
        public string L { get;set; }
        public string K { get;set; }
        public string M { get; set; }
        public string TM { get; set; }
        public string TTM { get; set; }
        public string NG { get; set; }
        public string TNG { get; set; }
        public string TNG1 { get; set; }
        public string TNG2 { get; set; }
        public string TNG3 { get; set; }
        public string MoTa { get; set; }
        public double TongSoNamNay
        {
            get => NganhPhanCapHienVatNamNay + NganhPhanCapBangTienNamNay + NganhChuaPhanCapNamNay;
        }
        public double NganhPhanCapHienVatNamNay { get; set; }
        public double NganhPhanCapBangTienNamNay { get; set; }
        public double NganhChuaPhanCapNamNay { get; set; }

        public double TongSoNam1
        {
            get => NganhPhanCapHienVatNam1 + NganhPhanCapBangTienNam1;
        }
        public double NganhPhanCapHienVatNam1 { get; set; }
        public double NganhPhanCapBangTienNam1 { get; set; }

        public double TongSoNam2
        {
            get => NganhPhanCapHienVatNam2 + NganhPhanCapBangTienNam2;
        }
        public double NganhPhanCapHienVatNam2 { get; set; }
        public double NganhPhanCapBangTienNam2 { get; set; }
        public bool IsHangCha { get; set; }

        public string XauNoiMa { get; set; }

        public Guid? MlnsId { get; set; }
        public Guid? MlnsIdParent { get; set; }
    }
}
