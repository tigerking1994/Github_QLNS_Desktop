using System;
using System.Collections.Generic;
using System.Text;

namespace VTS.QLNS.CTC.App.Model
{
    public class SktChungTuChiTietSearchModel
    {
        public Guid SktChungTuId { get; set; }
        public int NamLamViec { get; set; }
        public int NamNganSach { get; set; }
        public int NguonNganSach { get; set; }
        public int ILoai { get; set; }
        public string IdDonVi { get; set; }
        public string CurrentIdDonVi { get; set; }
        public int ITrangThai { get; set; }
        public string IdDonViTao { get; set; }
        public int LoaiChungTu { get; set; }
        public int HienThi { get; set; }
        public Guid IdMucLucSkt { get; set; }
    }
}
