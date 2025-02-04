using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations.Schema;

namespace VTS.QLNS.CTC.Core.Domain.Query
{
    public class VdtDaGoiThauDetailQuery
    {
        public int Loai { get; set; }
        public string TenLoai { get; set; }
        public bool? IsHangCha { get; set; }
        public Guid? IdGoiThauChiPhi { get; set; }
        public Guid? IdChiPhi { get; set; }
        public Guid? IdGoiThauNguonVon { get; set; }
        public int? IdNguonVon { get; set; }
        public Guid? IdGoiThauHangMuc { get; set; }
        public Guid? IdHangMuc { get; set; }
        public Guid? IdGoiThau { get; set; }
        public string NoiDung { get; set; }
        public double? TongMucDT { get; set; }
        public double? GiaTriPheDuyet { get; set; }
        public double? GiaTriTruocDC { get; set; }
    }
}
