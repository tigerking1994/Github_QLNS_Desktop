using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

#nullable disable

namespace VTS.QLNS.CTC.Core.Domain
{
    [Table("NS_SKT_MucLuc")]
    public partial class NsSktMucLuc : EntityBase
    {
        [Column("IID")]
        [Key]
        public override Guid Id { get; set; }
        [Column("IID_MLSKT")]
        public Guid IIDMLSKT { get; set; }
        [StringLength(50)]
        public string SKyHieu { get; set; }
        [StringLength(50)]
        public string SKyHieuCu { get; set; }
        [Required]
        [StringLength(10)]
        public string SL { get; set; }
        [Required]
        [StringLength(10)]
        public string SK { get; set; }
        [Required]
        [StringLength(10)]
        public string SM { get; set; }
        [Required]
        [StringLength(10)]
        [Column("sNG_Cha")]
        public string SNGCha { get; set; }
        [Required]
        [StringLength(10)]
        public string SNg { get; set; }
        [Column("SSTT")]
        [StringLength(10)]
        public string SSTT { get; set; }
        [Column("SSttBC")]
        [StringLength(10)]
        public string SSttBC { get; set; }
        [Required]
        public string SMoTa { get; set; }
        [NotMapped]
        public string KyHieuCha { get; set; }
        [Column("bHangCha")]
        public bool BHangCha { get; set; }
        [Column("iTrangThai")]
        public int ITrangThai { get; set; }
        public bool? BCoDinhMuc { get; set; }
        public int INamLamViec { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DNgayTao { get; set; }
        [StringLength(50)]
        public string DNguoiTao { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DNgaySua { get; set; }
        [StringLength(50)]
        public string DNguoiSua { get; set; }
        [StringLength(250)]
        public string Tag { get; set; }
        public string Log { get; set; }
        [Required]
        [StringLength(10)]
        public string Muc { get; set; }
        [Column("IID_MLSKTCha")]
        public Guid? IIDMLSKTCha { get; set; }
        public string SLoaiNhap { get; set; }
        [NotMapped]
        public string IdDonVi { get; set; }
        [NotMapped]
        public string TenDonVi { get; set; }
        [NotMapped]
        public bool IsRemainRow { get; set; }

        [NotMapped]
        public bool IsFirstParentRow { get; set; }
        [NotMapped]
        public ICollection<NsMlsktMlns> SktMucLucMaps { get; set; }

        [NotMapped]
        public string MergeRangeChild { get; set; }
        [NotMapped]
        public int Rank { get; set; }

        [NotMapped]
        public string SKhoiDonVi { get; set; }

        public NsSktMucLuc BuildSktMucLucMap(ICollection<NsMlsktMlns> sktMucLucMaps)
        {
            SktMucLucMaps = sktMucLucMaps;
            return this;
        }

        public NsSktMucLuc Clone()
        {
            return (NsSktMucLuc) this.MemberwiseClone();
        }
    }
}
