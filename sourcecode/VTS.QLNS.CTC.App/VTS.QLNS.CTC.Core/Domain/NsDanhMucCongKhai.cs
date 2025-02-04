using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VTS.QLNS.CTC.Core.Domain
{
    [Table("NS_DanhMucCongKhai")]
    public partial class NsDanhMucCongKhai : EntityBase
    {
        public string sMoTa { get; set; }

        public int? iNamLamViec { get; set; } = 0;

        public DateTime dNgayTao { get; set; }


        [StringLength(50)]
        public string sNguoiTao { get; set; }

        [StringLength(50)]
        public string sNguoiSua { get; set; }

        [StringLength(250)]
        public string Tag { get; set; }

        public string Log { get; set; }

        public Guid? iID_DMCongKhai_Cha { get; set; }
        public bool bHangCha { get; set; }
        public string sMa { get; set; }
        public string sMaCha { get; set; }
        public string STT { get; set; }
        public virtual ICollection<NsDmCongKhaiMlns> NsDmCongKhaiMlns { get; set; }
        [NotMapped]
        public bool IsSelected { get; set; }
        [NotMapped]
        public bool IsParent { get; set; }
    }
}
