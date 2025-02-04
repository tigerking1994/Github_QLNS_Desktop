using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VTS.QLNS.CTC.Core.Domain.Query
{
    public class ReportTnQtChungTuChiTietHD4554Query
    {
        public int STT { get;set; }
        public string IIDMaDonVi { get;set; }
        public string STenDonVi { get;set; }
        public double? FTongSoTien { get;set; }
        public string SLNS { get;set; }
        public string SNoiDung { get;set; }
        public List<ReportChildTnQtChungTuChiTietHD4554Query> LstGiaTri { get;set; }
        public List<ReportChildTnQtChungTuChiTietHD4554Query> LstTong { get; set; }
        public List<ReportChildTnQtChungTuChiTietHD4554Query> LstTongTien { get; set; }
        public Guid IIdMlns { get; set; }
        public Guid IIdMlnsCha { get; set; }
        public bool BHangCha { get;set; }
        public string M { get;set; }
        public bool HasData => FTongSoTien > 0;
        public string SumTotal { get; set; }
    }
    public class ReportChildTnQtChungTuChiTietHD4554Query
    {
        public int STT { get;set;}
        public string IIDMaDonVi { get; set; }
        public string STenDonVi { get; set; }
        public double? FSoTien { get; set; }
        public string SLNS { get; set; }
        public string SNoiDung { get; set; }
        public bool BHangCha { get; set; }
        public string M { get; set; }
    }
}
