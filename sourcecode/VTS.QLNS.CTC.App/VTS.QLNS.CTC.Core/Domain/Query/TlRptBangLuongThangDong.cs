using System;
using System.Collections.Generic;
using System.Text;

namespace VTS.QLNS.CTC.Core.Domain.Query
{
    public class TlRptBangLuongThangDong
    {
        public bool IsSummary { get; set; }
        public string CapBac { get; set; }
        public decimal? HeSoLuong { get; set; }
        public TlRptBangLuongThangDongItem Item1 { get; set; } = new TlRptBangLuongThangDongItem();
        public TlRptBangLuongThangDongItem Item2 { get; set; } = new TlRptBangLuongThangDongItem();
        public TlRptBangLuongThangDongItem Item3 { get; set; } = new TlRptBangLuongThangDongItem();
        public TlRptBangLuongThangDongItem Item4 { get; set; } = new TlRptBangLuongThangDongItem();
        public TlRptBangLuongThangDongItem Item5 { get; set; } = new TlRptBangLuongThangDongItem();
        public TlRptBangLuongThangDongItem ItemSummary { get; set; } = new TlRptBangLuongThangDongItem();
    }

    public class TlRptBangLuongThangDongItem
    {
        public decimal? QuanSo { get; set; }
        public decimal? Tien { get; set; }
    }
}
