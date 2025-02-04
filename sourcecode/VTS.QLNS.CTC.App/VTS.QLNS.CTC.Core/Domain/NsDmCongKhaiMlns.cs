using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VTS.QLNS.CTC.Core.Domain
{
    [Table("NS_DMCongKhai_MLNS")]
    public partial class NsDmCongKhaiMlns : EntityBase
    {
        public Guid iID_DMCongKhai { get; set; }

        [ForeignKey(nameof(iID_DMCongKhai))]
        public virtual NsDanhMucCongKhai DanhMucCongKhai { get; set; }

        [StringLength(50)]
        public string sNS_XauNoiMa { get; set; }

        public int iNamLamViec { get; set; }

        public DateTime? dNgayTao { get; set; }

        [StringLength(50)]
        public string sNguoiTao { get; set; }

        public DateTime? dNgaySua { get; set; }

        [StringLength(50)]
        public string sNguoiSua { get; set; }

        [StringLength(250)]
        public string Tag { get; set; }

        public string Log { get; set; }
    }
}
