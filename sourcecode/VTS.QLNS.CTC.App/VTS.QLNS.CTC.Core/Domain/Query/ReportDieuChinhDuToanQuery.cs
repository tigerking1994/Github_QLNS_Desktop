using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace VTS.QLNS.CTC.Core.Domain.Query
{
    public class ReportDieuChinhDuToanQuery
    {
        public Guid IdParent { get; set; }
        public string Lns { get; set; }
        public string Header1 { get; set; }
        public string SXauNoiMa { get; set; }
        public string L { get; set; }
        public string K { get; set; }
        public string M { get; set; }
        public string Tm { get; set; }
        public string Ttm { get; set; }
        public string Ng { get; set; }
        public string Tng { get; set; }
        public string Tng1 { get; set; }
        public string Tng2 { get; set; }
        public string Tng3 { get; set; }
        public string MoTa { get; set; }
        public double TongCong { get; set; }
        public double FCong { get; set; }
        public bool bHangCha { get; set; }
        public List<NsSktChungTuChiTiet> LstGiaTri { get; set; }
        public List<NsSktChungTuChiTiet> LstTong { get; set; }
        public Guid IIdMlns { get; set; }
        public Guid IIdMlnsCha { get; set; }
    }
}
