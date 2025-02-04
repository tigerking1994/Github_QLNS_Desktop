using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VTS.QLNS.CTC.Core.Domain
{
    [Table("BH_DM_MucDongBHXH")]
    public class BhDmMucDongBHXH : EntityBase
    {
        [Column("iD")]
        [Key]
        public override Guid Id { get; set; }
        [Column("sMaMucDong")]
        public string SMaMucDong { get; set; }
        [Column("sNoiDung")]
        public string SNoiDung { get; set; }
        [Column("sBH_XauNoiMa")]
        public string SBH_XauNoiMa { get; set; }
        [Column("fTyLe_BHXH_NLD")]
        public double FTyLe_BHXH_NLD { get; set; }
        [Column("fTyLe_BHXH_NSD")]
        public double FTyLe_BHXH_NSD { get; set; }
        [Column("fTyLe_BHYT_NLD")]
        public double FTyLe_BHYT_NLD { get; set; }
        [Column("fTyLe_BHYT_NSD")]
        public double FTyLe_BHYT_NSD { get; set; }
        [Column("fTyLe_BHTN_NLD")]
        public double FTyLe_BHTN_NLD { get; set; }
        [Column("fTyLe_BHTN_NSD")]
        public double FTyLe_BHTN_NSD { get; set; }
        [Column("iTrangThai")]
        public int? ITrangThai { get; set; }
        [Column("iNamLamViec")]
        public int? INamLamViec { get; set; }
        public DateTime? DNgaySua { get; set; }
        public DateTime? DNgayTao { get; set; }
        public string SNguoiSua { get; set; }
        public string SNguoiTao { get; set; }
    }
}
