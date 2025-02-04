using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace VTS.QLNS.CTC.Core.Domain.Query
{
    public class GoiThauChiPhiQuery
    {
        public Guid Id { get; set; }
        public string TenChiPhi { get; set; }
        public Guid? IdGoiThauChiPhi { get; set; }
        public Guid? IdChiPhi { get; set; }
        public double? GiaTriPheDuyet { get; set; }
        public double? GiaTriGoiThau { get; set; }
        public string MaOrDer { get; set; }
        public bool? IsHangCha { get; set; }
    }
}
