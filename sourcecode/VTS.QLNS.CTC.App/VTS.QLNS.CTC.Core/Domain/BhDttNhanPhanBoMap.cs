using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VTS.QLNS.CTC.Core.Domain
{
    public partial class BhDttNhanPhanBoMap : EntityBase
    {
        [Column("iID_DTTNhanPhanBoMap")]
        public override Guid Id { get; set; }
        public Guid IIdCtduToanNhan { get; set; }
        public Guid IIdCtduToanPhanBo { get; set; }
        public DateTime? DNgayTao { get; set; }
        public string SNguoiTao { get; set; }
        public DateTime? DNgaySua { get; set; }
        public string SNgaySua { get; set; }
        [NotMapped]
        public BhDtPhanBoChungTu ChungTuDuToanNhan { get; set; }
        [NotMapped]
        public BhDtPhanBoChungTu ChungTuDuToanPhanBo { get; set; }
    }
}
