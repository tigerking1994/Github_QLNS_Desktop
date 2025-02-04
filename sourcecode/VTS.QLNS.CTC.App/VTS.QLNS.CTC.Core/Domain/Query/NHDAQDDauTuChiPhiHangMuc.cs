using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VTS.QLNS.CTC.Core.Domain.Query
{
    public class NHDAQDDauTuChiPhiHangMuc
    {
        public Guid Id { get; set; }
        public string sTen { get; set; }
        public string SOrder { get; set; }
        public Guid? ParentId { get; set; }
        public Guid? ChiPhiId { get; set; }
        public int IType { get; set; }
        public string SLoai { get; set; }
        public double? EURODT { get; set; }
        public double? NgoaiTeDT { get; set; }
        public double? USDDT { get; set; }
        public double? VNDDT { get; set; }
        public double? EUROQT { get; set; }
        public double? NgoaiTeQT { get; set; }
        public double? USDQT { get; set; }
        public double? VNDQT { get; set; }
        public double? EUROKT { get; set; }
        public double? NgoaiTeKT { get; set; }
        public double? USDKT { get; set; }
        public double? VNDKT { get; set; }
        public double? EUROCDT { get; set; }
        public double? NgoaiTeCDT { get; set; }
        public double? USDCDT { get; set; }
        public double? VNDCDT { get; set; }
    }
}
