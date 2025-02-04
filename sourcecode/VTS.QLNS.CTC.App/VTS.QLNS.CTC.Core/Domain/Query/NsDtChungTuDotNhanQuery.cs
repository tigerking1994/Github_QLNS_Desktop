using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace VTS.QLNS.CTC.Core.Domain.Query
{
    public class NsDtChungTuDotNhanQuery
    {
        public Guid Id { get; set; }
        public string SSoChungTu { get; set; }
        public DateTime? DNgayChungTu { get; set; }
        public string SSoQuyetDinh { get; set; }
        public DateTime? DNgayQuyetDinh { get; set; }
        public string SDslns { get; set; }
        public string SDsidMaDonVi { get; set; }
        public double SoPhanBo { get; set; }
        public double SoChuaPhanBo { get; set; }
        public string IIDChungTuDieuChinh { get; set; }
        [NotMapped]
        public string IdLuyKe { get; set; }
    }
}
