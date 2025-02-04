using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations.Schema;

namespace VTS.QLNS.CTC.Core.Domain.Query
{
    public class TlRptTruyLinhChuyenCheDoQuery
    {
        public string TenCanBo { get; set; }
        public string SoTaiKhoan { get; set; }
        public string MaCapBac { get; set; }
        public string MaDonVi { get; set; }
        public string NgayNhapNgu { get; set; }
        public string NgayXuatNgu { get; set; }
        public string NgayTaiNgu { get; set; }
        public decimal HeSoMoi { get; set; }
        public decimal HeSoCu { get; set; }
        public decimal TTL { get; set; }
        public decimal LhtTtMoi { get; set; }
        public decimal PctnTtMoi { get; set; }
        public decimal PccovTtMoi { get; set; }
        public decimal BhxhcnTtMoi { get; set; }
        public decimal LhtTtCu { get; set; }
        public decimal PctnTtCu { get; set; }
        public decimal PccovTtCu { get; set; }
        public decimal BhxhcnTtCu { get; set; }
        public decimal BhytcnTtCu { get; set; }
    }
}
