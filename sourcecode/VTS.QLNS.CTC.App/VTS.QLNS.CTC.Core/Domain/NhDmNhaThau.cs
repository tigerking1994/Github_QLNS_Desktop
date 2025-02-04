using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace VTS.QLNS.CTC.Core.Domain
{
    public partial class NhDmNhaThau : EntityBase
    {
        public int? ILoai { get; set; }
        public string SMaNhaThau { get; set; }
        public string STenNhaThau { get; set; }
        public string SDiaChi { get; set; }
        public string SDaiDien { get; set; }
        public string SChucVu { get; set; }
        public string SDienThoai { get; set; }
        public string SFax { get; set; }
        public string SEmail { get; set; }
        public string SWebsite { get; set; }
        public string SSoTaiKhoan { get; set; }
        public string SNganHang { get; set; }
        public string SMaNganHang { get; set; }
        public string SMaSoThue { get; set; }
        public string SNguoiLienHe { get; set; }
        public string SDienThoaiLienHe { get; set; }
        public string SSoCmnd { get; set; }
        public string SNoiCapCmnd { get; set; }
        public DateTime? DNgayCapCmnd { get; set; }
        public DateTime? DNgayTao { get; set; }
        public string SNguoiTao { get; set; }
    }
}
