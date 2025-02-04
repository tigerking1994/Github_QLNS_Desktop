using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;

namespace VTS.QLNS.CTC.App.Model
{
    public class NhQtTaiSanChiTietModel : ModelBase
    {
        public Guid Id { get; set; }
        public Guid? IIdTaiSanId { get; set; }
        public string SHangMuc { get; set; }
        public int? ISoLuong { get; set; }
        public Guid? IIdDonViTinhId { get; set; }
        public Guid? IIdXuatXuId { get; set; }
        public Guid? IIdHopDongId { get; set; }
    }
}