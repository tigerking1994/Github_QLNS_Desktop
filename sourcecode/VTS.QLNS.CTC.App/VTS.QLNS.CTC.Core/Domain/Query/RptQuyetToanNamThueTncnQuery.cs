using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations.Schema;

namespace VTS.QLNS.CTC.Core.Domain.Query
{
    public class RptQuyetToanNamThueTNCNQuery
    {
        public string TenCbo { get; set; }
        public string MaHieuCanBo { get; set; }
        public decimal? LhtTt { get; set; }
        public decimal? PcctTt { get; set; }
        public decimal? ThuongTt { get; set; }
        public decimal? ThuNhapKhacTt { get; set; }
        public decimal? BhcnTt { get; set; }
        public decimal? Gtnn { get; set; }
        public decimal? NguoiPhuThuoc { get; set; }
        public decimal? GtkhacTt { get; set; }
        public decimal? LuongthueTt { get; set; }
        public decimal? ThuetncnTt { get; set; }
        public decimal? GiamThueTt { get; set; }
        public decimal? ThueDaNopTt { get; set; }
    }
}
