using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations.Schema;

namespace VTS.QLNS.CTC.Core.Domain.Query
{
    public class ReportTongHopThongTinDuAnQuery
    {
        public string STenDuAn { get; set; }
        public string TenDonVi { get; set; }
        public string TienTe { get; set; }
        public string SoQuyetDinhChuTruong { get; set; }
        public DateTime? NgayQuyetDinhChuTruong { get; set; }
        public double GiaTriDauTu { get; set; }
        public string SoQuyetDinhDauTu { get; set; }
        public DateTime? NgayQuyetDinhDauTu { get; set; }
        public double TongMucDauTu { get; set; }
        public double LuyKeVonNamTruoc { get; set; }
        public double KeHoachVonNamNay { get; set; }
        public double LuyKeVonNamNay 
        { 
            get
            {
                return LuyKeVonNamTruoc + KeHoachVonNamNay;
            }
        }
        public double DaThanhToan { get; set; }
        public double ChuaThanhToan 
        { 
            get 
            {
                return KeHoachVonNamNay - DaThanhToan;
            } 
        }
        public string SoQuyetDinhQuyetToan { get; set; }
        public DateTime? NgayQuyetDinhQuyetToan { get; set; }
        public double GiaTriQuyetToan { get; set; }
        public double ChenhLechQTThanhToan { get; set; }
        public string SGhiChu { get; set; }
    }
}
