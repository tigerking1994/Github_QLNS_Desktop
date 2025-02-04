using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VTS.QLNS.CTC.Core.Domain
{
    [Table("BH_DTTM_BHYT_Nhan_PhanBo_Map")]
    public class BhPbdttmMapBHYT : EntityBase
    {
        [Column("iID_DTTM_BHYTNhanPhanBoMap")]
        [Key]
        public override Guid Id { get; set; }

        [Column("iID_DTTM_BHYT_NhanPhanBo")]
        public Guid IID_DTTM_BHYT_NhanPhanBo { get; set; }

        [Column("iID_DTTM_BHYT_PhanBo")]
        public Guid IID_DTTM_BHYT_PhanBo { get; set; }

        [Column("dNgaySua")]
        public DateTime DNgaySua { get; set; }

        [Column("dNgayTao")]
        public DateTime DNgayTao { get; set; }

        [Column("sNguoiSua")]
        public string SNguoiSua { get; set; }

        [Column("sNguoiTao")]
        public string SNguoiTao { get; set; }
    }
}
