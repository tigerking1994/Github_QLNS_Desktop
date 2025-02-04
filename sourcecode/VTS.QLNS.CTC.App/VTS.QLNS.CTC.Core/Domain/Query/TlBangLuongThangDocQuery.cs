using System;
using System.Collections.Generic;
using System.Text;

namespace VTS.QLNS.CTC.Core.Domain.Query
{
    public class TlBangLuongThangDocQuery
    {
        public string MaCb { get; set; }
        public string MaDonVi { get; set; }
        public int QuanSo { get; set; }
        public decimal Tien { get; set; }
        public decimal HeSoLuong { get; set; }
    }
}
