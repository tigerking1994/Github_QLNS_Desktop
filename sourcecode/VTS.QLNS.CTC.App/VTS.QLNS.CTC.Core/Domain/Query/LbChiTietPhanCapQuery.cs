using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace VTS.QLNS.CTC.Core.Domain.Query
{
    public class LbChiTietPhanCapQuery
    {
        public Guid MucLucID { get; set; }
        public Guid? MucLucParentId { get; set; }
        public string LNS { get; set; }
        public string L { get; set; }
        public string K { get; set; }
        public string M { get; set; }
        public string TM { get; set; }
        public string TTM { get; set; }
        public string NG { get; set; }
        public string TNG { get; set; }
        public string TNG1 { get; set; }
        public string TNG2 { get; set; }
        public string TNG3 { get; set; }
        public string MoTa { get; set; }
        public string XauNoiMa { get; set; }
        public string IdDonViMLNS { get; set; }
        public Guid? Id { get; set; }
        public Guid? SoLieuChiTietId { get; set; }
        public string IdDonVi { get; set; }
        public string TenDonVi { get; set; }
        public Guid? MLNSId { get; set; }
        public double PhanCap { get; set; }
        public double HienVat { get; set; }
        public string GhiChu { get; set; }
        public bool bHangCha { get; set; }
    }
}

