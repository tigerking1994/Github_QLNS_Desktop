using System;

namespace VTS.QLNS.CTC.App.Model.Report
{
    public class DuToanDauNamChiThuongXuyenChiTietModel
    {
        public string LNS { get; set; }
        public string M { get; set; }
        public string TM { get; set; }
        public string TTM { get; set; }
        public string NG { get; set; }
        public string TNG { get; set; }
        public string TNG1 { get; set; }
        public string TNG2 { get; set; }
        public string TNG3 { get; set; }
        public string MoTa { get; set; }
        public double TongSo
        {
            get => TongMuaNhap + TongPhanBo;
        }
        public double TongMuaNhap
        {
            get => MuaTrongNuoc + NhapKhau;
        }
        public double MuaTrongNuoc { get; set; }
        public double NhapKhau { get; set; }

        public double TongPhanBo
        {
            get => SoDaPhanBo + NganhChuaPhanBo;
        }
        public double SoDaPhanBo { get; set; }
        public double NganhChuaPhanBo { get; set; }
        public bool IsHangCha { get; set; }

        public string XauNoiMa { get; set; }
        public Guid? MlnsId { get; set; }
        public Guid? MlnsIdParent { get; set; }
    }
}
