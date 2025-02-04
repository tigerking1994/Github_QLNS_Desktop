using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace VTS.QLNS.CTC.Core.Domain.Query
{
    public class HopDongQuery
    {
        public Guid Id { get; set; }
        public Guid IdHopDongGoc { get; set; }
        public string SoHopDong { get; set; }
        public string TenHopDong { get; set; }
        public DateTime NgayHopDong { get; set; }
        public Guid? IdDuAn { get; set; }
        public string TenDuAn { get; set; }
        public Guid? IdGoiThau { get; set; }
        public string TenGoiThau { get; set; }
        public double GiaTriSauDieuChinh { get; set; }
        public double DieuChinh { get; set; }
        public DateTime NgayTao { get; set; }
        public string TenDonVi { get; set; }
        public string NoiDungHopDong { get; set; }
        public string TenNhaThau { get; set; }
        public int ThoiGianThucHien { get; set; }
        public DateTime? KhoiCongDuKien { get; set; }
        public DateTime? KetThucDuKien { get; set; }
        public string HinhThucHopDong { get; set; }
        public string TenLoaiHopDong { get; set; }
        public string ChuDauTu { get; set; }
        public bool Active { get; set; }
        public int TotalFiles { get; set; }
        public string SUserCreate { get; set; }
        public bool? BKhoa { get; set; }
        public bool BIsGoc { get; set; }
        public int? ILandieuchinh { get; set; }
        public string? SSoTaiKhoan { get; set; }
    }
}
