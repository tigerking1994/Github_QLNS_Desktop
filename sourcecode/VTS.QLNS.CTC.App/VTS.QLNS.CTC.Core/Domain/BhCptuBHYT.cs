using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VTS.QLNS.CTC.Core.Domain
{
    [Table("BH_CP_CapTamUng_KCB_BHYT")]
    public partial class BhCptuBHYT : EntityBase
    {

        [Column("iID_BH_CP_CapTamUng_KCB_BHYT")]
        [Key]
        public override Guid Id { get; set; }

        [Column("sSoChungTu")]
        public string SSoChungTu { get; set; }

        [Column("dNgayChungTu")]
        public DateTime DNgayChungTu { get; set; }

        [Column("sSoQuyetDinh")]
        public string SSoQuyetDinh { get; set; }

        [Column("dNgayQuyetDinh")]
        public DateTime DNgayQuyetDinh { get; set; }

        [Column("iQuy")]
        public int IQuy { get;set; }

        [Column("sMoTa")]
        public string SMoTa { get;set; }

        [Column("sDSID_CoSoYTe")]
        public string SDSID_CoSoYTe { get;set; }

        [Column("sDSLNS")]
        public string SDSLNS { get;set; }

        [Column("bIsKhoa")]
        public bool BIsKhoa { get; set; }

        [Column("bIsTongHop")]
        public bool BIsTongHop { get; set; }

        [Column("dNgayTao")]
        public DateTime? DNgayTao { get; set; }

        [Column("dNgaySua")]
        public DateTime? DNgaySua { get; set; }

        [Column("sNguoiSua")]
        public string SNguoiSua { get; set; }

        [Column("sNguoiTao")]
        public string SNguoiTao { get; set; }

        [Column("fQTQuyTruoc")]
        public Double? FQTQuyTruoc { get; set; }

        [Column("fTamUngQuyNay")]
        public Double? FTamUngQuyNay { get; set; }

        [Column("iNamLamViec")]
        public int INamLamViec { get; set; }

        [Column("sDSSoChungTuTongHop")]
        public string SDSSoChungTuTongHop { get; set; }
        [Column("iID_MaDonVi")]
        public string IIDMaDonVi { get; set; }
        public int? ILoaiKinhPhi { get; set; }

    }
}
