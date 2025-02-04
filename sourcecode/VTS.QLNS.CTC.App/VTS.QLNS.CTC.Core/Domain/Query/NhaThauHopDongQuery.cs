using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations.Schema;

namespace VTS.QLNS.CTC.Core.Domain.Query
{
    public class NhaThauHopDongQuery
    {
        public Guid? IIdHopDongId { get; set; }
        public Guid? IIdGoiThauId { get; set; }
        public Guid? IIdNhaThauId { get; set; }
        public string SSoHopDong { get; set; }
        public string STenNhaThau { get; set; }
        public string SHinhThucHopDong { get; set; }
        public double? FGiaTri { get; set; }
        public DateTime? DNgayHopDong { get; set; }
    }
}
