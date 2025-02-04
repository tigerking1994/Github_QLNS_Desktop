using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VTS.QLNS.CTC.Core.Domain.Query
{
    public class VdtQDTChiPhiHangMucExportQuery
    {
        public int Muc { get;set; }
        public string STT { get;set; }
        public string SLoai { get;set; }
        public string sTenHangMucCP { get;set; }
        public Double? fPheDuyetDA { get;set; }
        public Double? fPheDuyetTKTCTDT { get;set; }
        public bool IsHangCha { get;set; }
        public Guid? IdChiPhi { get;set; }
        public Guid? IdDAChiPhi { get;set; }
        public string SMaChiPhi { get; set; }
    }
}
