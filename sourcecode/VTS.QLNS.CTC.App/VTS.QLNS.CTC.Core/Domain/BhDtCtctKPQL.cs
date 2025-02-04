using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VTS.QLNS.CTC.Core.Domain
{
    [Table("BH_DuToan_CTCT_KPQL")]
    public partial class BhDtCtctKPQL : EntityBase
    {
        public override Guid Id { get; set; }
        public Guid? IIDChungTuChiTiet { get; set; }
        public string SXauNoiMa { get; set; }
        public Guid? IIDChungTu { get; set; }
        public string SLNS { get; set; }
        public string SM { get; set; }
        public string STM { get; set; }
        public string STMM { get; set; }
        public string SNG { get; set; }
        public string IIDMaDonVi { get; set; }
        public int INamLamViec { get; set; }
        public double? FSoTien { get; set; }
        public string SNguoiTao { get; set; }
        public string SNguoiSua { get; set; }
        public DateTime? DNgayTao { get; set; }
        public DateTime? DNgaySua { get; set; }
        public string SNoiDung { get; set; }
    }
}
