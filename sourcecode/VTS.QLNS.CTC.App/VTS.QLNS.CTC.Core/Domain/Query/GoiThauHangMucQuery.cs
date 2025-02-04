using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace VTS.QLNS.CTC.Core.Domain.Query
{
    public class GoiThauHangMucQuery
    {
        public Guid Id { get; set; }
        public string TenHangMuc { get; set; }
        public string TenLoaiCongTrinh { get; set; }
        public Guid? IdGoiThauHangMuc { get; set; }
        public Guid? IdLoaiCongTrinh { get; set; }
        public Guid? IdChiPhi { get; set; }
        public Guid? IdParent { get; set; }
        public double? GiaTriGoiThau { get; set; }
        public string MaOrDer { get; set; }
        public bool? IsHangCha { get; set; }
    }
}
