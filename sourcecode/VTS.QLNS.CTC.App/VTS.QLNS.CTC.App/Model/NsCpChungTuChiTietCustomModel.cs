using System;
using System.Collections.Generic;
using System.Text;

namespace VTS.QLNS.CTC.App.Model
{
    public class NsCpChungTuChiTietCustomModel
    {
        public Guid Id { get; set; }
        public Guid? IIdCtcapPhat { get; set; }
        public Guid IIdMlns { get; set; }
        public Guid? IIdParentCha { get; set; }
        public string SXauNoiMa { get; set; }
        public string SLns { get; set; }
        public string SL { get; set; }
        public string SK { get; set; }
        public string SM { get; set; }
        public string STm { get; set; }
        public string STtm { get; set; }
        public string SNg { get; set; }
        public string STng { get; set; }
        public string SMoTa { get; set; }
        public string SChuong { get; set; }
        public int? INamLamViec { get; set; }
        public bool BHangCha { get; set; }
        public string ILoai { get; set; }
        public string IIdMaDonVi { get; set; }
        public string STenDonVi { get; set; }
        public double FTuChi { get; set; }
        public double FHienVat { get; set; }
        public string SGhiChu { get; set; }
        public DateTime? DNgayTao { get; set; }
        public string SNguoiTao { get; set; }
        public DateTime? DNgaySua { get; set; }
        public string SNguoiSua { get; set; }
        public int? IIdMaNguonNganSach { get; set; }
        public int? INamNganSach { get; set; }
        public double FDeNghiDonVi { get; set; }

        public string XauNoiMaToiMuc => string.Format("{0}-{1}-{2}-{3}", SLns, SL, SK, SM);
        public string XauNoiMaToiMucTieuMuc => string.Format("{0}-{1}-{2}-{3}-{4}", SLns, SL, SK, SM, STm);
    }
}
