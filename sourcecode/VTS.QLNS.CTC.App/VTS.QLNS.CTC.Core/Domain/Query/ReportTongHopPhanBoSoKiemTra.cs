using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace VTS.QLNS.CTC.Core.Domain.Query
{
    public class ReportTongHopPhanBoSoKiemTra
    {
        public Guid? IIdMlsktCha { get; set; }
        public Guid IIdMlskt { get; set; }
        public string sM { get; set; }
        public string sL { get; set; }
        public string sK { get; set; }
        public string sNG { get; set; }
        public string sKyHieu { get; set; }
        public string sStt { get; set; }
        public string sMoTa { get; set; }
        public double SoKiemTraDuocThongBao { get; set; }
        public double TongTuChi { get; set; }
        public double ConLai => SoKiemTraDuocThongBao - TongTuChi;
        public double TongTuChiPB { get; set; }
        public double TuChiBanThan { get; set; }
        public bool? bHangCha { get; set; }
        public double TongMuaHangHienVat { get; set; }
        public double TongMuaHangHienVatPB { get; set; }
        public double TongDacThu { get; set; }
        public double TongDacThuPB { get; set; }
        [NotMapped]
        public double TongTuChiConLai { get; set; }
        [NotMapped]
        public double TuChiDV1 { get; set; }
        [NotMapped]
        public double TuChiDV2 { get; set; }
        [NotMapped]
        public double TuChiDV3 { get; set; }
        [NotMapped]
        public double TuChiDV4 { get; set; }
        [NotMapped]
        public double TuChiDV5 { get; set; }
        [NotMapped]
        public double TuChiDV6 { get; set; }
        [NotMapped]
        public double TuChiDV7 { get; set; }
        [NotMapped]
        public double TuChiDV8 { get; set; }
        [NotMapped]
        public double TuChiDV9 { get; set; }
        [NotMapped]
        public double TongMuaHangHienVatDacThu { get; set; }
        [NotMapped]
        public double TongMuaHangHienVatDacThuPB { get; set; }
        [NotMapped]
        public double TongMuaHangHienVatDacThuConLai { get; set; }
        [NotMapped]
        public double MuaHangHienVatDV1 { get; set; }
        [NotMapped]
        public double MuaHangHienVatDV2 { get; set; }
        [NotMapped]
        public double MuaHangHienVatDV3 { get; set; }
        [NotMapped]
        public double MuaHangHienVatDV4 { get; set; }
        [NotMapped]
        public double MuaHangHienVatDV5 { get; set; }
        [NotMapped]
        public double MuaHangHienVatDV6 { get; set; }
        [NotMapped]
        public double DacThuDV1 { get; set; }
        [NotMapped]
        public double DacThuDV2 { get; set; }
        [NotMapped]
        public double DacThuDV3 { get; set; }
        [NotMapped]
        public double DacThuDV4 { get; set; }
        [NotMapped]
        public double DacThuDV5 { get; set; }
        [NotMapped]
        public double DacThuDV6 { get; set; }
        [NotMapped]
        public double DacThuDV7 { get; set; }
        [NotMapped]
        public double DacThuDV8 { get; set; }
        [NotMapped]
        public double DacThuDV9 { get; set; }
        public List<DataReportDynamic> ListDataValue { get; set; } = new List<DataReportDynamic>();

        public string IIdMaDonVi { get; set; }

    }

    public class DataReportDynamic
    {
        public double FVal { get; set; }
        public int Stt { get; set; }
    }

    public class DataReportDynamic2
    {
        public double FHangNhap { get; set; }
        public double FHangMua { get; set; }
        public int Stt { get; set; }
    }
}
