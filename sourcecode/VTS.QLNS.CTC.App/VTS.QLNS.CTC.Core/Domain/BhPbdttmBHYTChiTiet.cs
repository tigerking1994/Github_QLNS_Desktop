using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VTS.QLNS.CTC.Core.Domain
{
    [Table("BH_DTTM_BHYT_ThanNhan_PhanBo_ChiTiet")]
    public partial class BhPbdttmBHYTChiTiet : EntityBase
    {
        [Column("iID_DTTM_BHYT_ThanNhan_PhanBo_ChiTiet")]
        [Key]
        public override Guid Id { get; set; }

        [Column("iID_DTTM_BHYT_ThanNhan_PhanBo")]
        public Guid IID_DTTM_BHYT_ThanNhan_PhanBo { get; set; }

        [Column("iID_DTTM_BHYT_ThanNhan")]
        public Guid IID_DTTM_BHYT_ThanNhan { get; set; }

        [Column("sSoChungTu")]
        public string SSoChungTu { get; set; }

        [Column("dNgayChungTu")]
        public DateTime DNgayChungTu { get; set; }

        [Column("sSoQuyetDinh")]
        public string SSoQuyetDinh { get; set; }

        [Column("dNgayQuyetDinh")]
        public DateTime DNgayQuyetDinh { get; set; }

        [Column("iID_MaDonVi")]
        public string IID_MaDonVi { get; set; }

        [Column("iNamLamViec")]
        public int INamLamViec { get; set; }

        [Column("iID_MLNS")]
        public Guid IID_MLNS { get; set; }

        [Column("fDuToan")]
        public Double? FDuToan { get; set; }

        [Column("sNguoiTao")]
        public string SNguoiTao { get; set; }

        [Column("sNguoiSua")]
        public string SNguoiSua { get; set; }

        [Column("dNgayTao")]
        public DateTime? DNgayTao { get; set; }

        [Column("dNgaySua")]
        public DateTime? DNgaySua { get; set; }

        [Column("bIsKhoa")]
        public bool BIsKhoa { get; set; }

        [Column("sGhiChu")]
        public string SGhiChu { get; set; }

        [Column("sMoTa")]
        public string SMoTa { get; set; }

        [Column("sLNS")]
        public string SLNS { get; set; }
        [Column("sXauNoiMa")]
        public string SXauNoiMa { get;set; }
    }
}
