using System;
using System.Collections.Generic;

#nullable disable

namespace VTS.QLNS.CTC.Core.Domain
{
    public partial class NsMucLucNganSachNganh
    {
        public Guid IId { get; set; }
        public string IIdMaNganh { get; set; }
        public string IIdMaNganhMlns { get; set; }
        public string STenNganh { get; set; }
        public string SMaNguoiQuanLy { get; set; }
        public int? ITrangThai { get; set; }
        public bool? BPublic { get; set; }
        public string IIdMaNhomNguoiDungPublic { get; set; }
        public string IIdMaNhomNguoiDungDuocGiao { get; set; }
        public string SIdMaNguoiDungDuocGiao { get; set; }
        public DateTime? DNgayTao { get; set; }
        public string SIdMaNguoiDungTao { get; set; }
        public int? ISoLanSua { get; set; }
        public DateTime? DNgaySua { get; set; }
        public string SIpsua { get; set; }
        public string SIdMaNguoiDungSua { get; set; }
    }
}
