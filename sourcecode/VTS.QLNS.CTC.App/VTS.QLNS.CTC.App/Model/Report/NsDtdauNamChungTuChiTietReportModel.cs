using System;
using System.Collections.Generic;
using System.Text;

namespace VTS.QLNS.CTC.App.Model.Report
{
    public class NsDtdauNamChungTuDacThuDonViHeaderReportModel
    {
        public string TenNganh { get; set; }
        public string MergeRange { get; set; }
        public List<TitleData> LstNganhHeader { get; set; }
        public List<TitleData> LstMucLuc { get; set; }
    }

    public class NsDtdauNamChungTuDacThuDonViReportModel
    {
        public int Stt { get; set; }
        public string MaDonVi { get; set; }
        public string TenDonVi { get; set; }
        public double TongCong { get; set; }
        public List<DetailMLNSData> LstGiaTri { get; set; }
        public List<DetailMLNSData> LstGiaTriTotal { get; set; }
    }

    public class DetailMLNSData
    {
        public double FTuChi { get; set; }
    }

    public class TitleData
    {
        public string SMoTa { get; set; }
        public string SSttBC { get; set; }
    }

    public class NsDtdauNamChungTuChiTietReportModel
    {
        public Guid IdDTDauNamPhanCap { get; set; }
        public string MaDonVi { get; set; }
        public string TenDonVi { get; set; }
        public double TuChi { get; set; }
        public string XauNoiMa { get; set; }
        public string GhiChu { get; set; }
        public string NG { get; set; }
        //public Guid Id { get; set; }
        //public string SXauNoiMa { get; set; }
        //public string SLns { get; set; }
        //public string SL { get; set; }
        //public string SK { get; set; }
        //public string SM { get; set; }
        //public string STm { get; set; }
        //public string STtm { get; set; }
        //public string SNg { get; set; }
        //public string STng { get; set; }
        //public string SMoTa { get; set; }
        //public string SChuong { get; set; }
        //public int INamNganSach { get; set; }
        //public int IIdMaNguonNganSach { get; set; }
        //public int INamLamViec { get; set; }
        //public bool BHangCha { get; set; }
        //public string IIdMaDonVi { get; set; }
        //public string STenDonVi { get; set; }
        //public double FTuChi { get; set; }
        //public double FHienVat { get; set; }
        //public double FHangNhap { get; set; }
        //public double FHangMua { get; set; }
        //public double FPhanCap { get; set; }
        //public double FDuPhong { get; set; }
        //public string SGhiChu { get; set; }
        //public DateTime? DNgayTao { get; set; }
        //public string SNguoiTao { get; set; }
        //public DateTime? DNgaySua { get; set; }
        //public string SNguoiSua { get; set; }
        //public bool? BKhoa { get; set; }
        //public string ILoaiChungTu { get; set; }
        //public double FChuaPhanCap { get; set; }
        //public Guid? IIdCtdtdauNam { get; set; }
        //public int ILoai { get; set; }
        //public double TotalValue
        //{
        //    get => FTuChi + FHienVat + FHangNhap + FHangMua + FPhanCap + FDuPhong + FChuaPhanCap;
        //}
    }
}
