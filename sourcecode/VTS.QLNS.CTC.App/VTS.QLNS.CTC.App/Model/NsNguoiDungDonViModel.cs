using System;
using System.Collections.Generic;
using System.Text;

namespace VTS.QLNS.CTC.App.Model
{
    public class NsNguoiDungDonViModel : ModelBase
    {
        public int Id { get; set; }
        public string IIDMaNguoiDung { get; set; }
        public string IIdMaDonVi { get; set; }
        public int INamLamViec { get; set; }
        public int IStt { get; set; }
        public int ITrangThai { get; set; }
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
        public string STenDonVi { get; set; }
    }
}
