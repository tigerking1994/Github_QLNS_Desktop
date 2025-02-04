using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VTS.QLNS.CTC.Core.Domain
{
    public partial class TlDmCheDoBHXH : EntityBase
    {
        public string SMaCheDo { get; set; }
        public string STenCheDo { get; set; }
        public int? ILoaiCheDo { get; set; }
        public bool? IsFormula { get; set; }
        public bool? IsDisplay { get; set; }
        public bool? IsCapNhatTruyThu { get; set; }
        public string SMaCheDoCha { get; set; }
        public string SLoaiTruyLinh { get; set; }
        public string SMoTa { get; set; }
        public string SXauNoiMa { get; set; }
        public decimal? FGiaTri { get; set; }
        public string SXauNoiMaMlnsBHXH { get; set; }
        public string SMlnsBHXH { get; set; }
        public bool? BTinhCN { get; set; }
        public bool? BTinhNgayLe { get; set; }
        public bool? BTinhT7 { get; set; }
        [NotMapped]
        public bool IsHangCha => string.IsNullOrEmpty(SMaCheDoCha);
        [NotMapped]
        public string LoaiCheDoDisplay { get; set; }
        [NotMapped]
        public string IsFormulaDisplay { get; set; }
    }
}
