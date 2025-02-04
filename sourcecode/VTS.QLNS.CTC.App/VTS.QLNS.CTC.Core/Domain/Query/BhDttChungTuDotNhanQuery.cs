using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VTS.QLNS.CTC.Core.Domain.Query
{
    public class BhDttChungTuDotNhanQuery
    {
        public Guid Id { get; set; }
        public string SSoChungTu { get; set; }
        public DateTime? DNgayChungTu { get; set; }
        public string SSoQuyetDinh { get; set; }
        public DateTime? DNgayQuyetDinh { get; set; }
        public string SDslns { get; set; }
        public string SDsidMaDonVi { get; set; }
        public double FSoNhanPhanBo { get; set; }
        public double FSoPhanBo { get; set; }
        public double FSoChuaPhanBo { get; set; }
        [NotMapped]
        public string IdLuyKe { get; set; }
    }
}
