using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace VTS.QLNS.CTC.Core.Domain
{
    public partial class VdtDmLoaiCongTrinh : EntityBase
    {
        [NotMapped]
        public override Guid Id {get;set;}
        public Guid IIdLoaiCongTrinh { get; set; }
        public Guid? IIdParent { get; set; }
        public string SMaLoaiCongTrinh { get; set; }
        public string STenVietTat { get; set; }
        public string STenLoaiCongTrinh { get; set; }
        public string SMoTa { get; set; }
        public int? IThuTu { get; set; }
        public bool? BActive { get; set; }
        public DateTime? DNgayTao { get; set; }
        public string SIdMaNguoiDungTao { get; set; }
        public int? ISoLanSua { get; set; }
        public DateTime? DNgaySua { get; set; }
        public string SIpsua { get; set; }
        public string SIdMaNguoiDungSua { get; set; }
        public virtual VdtDmLoaiCongTrinh Parent { get; set; }
        public virtual ICollection<VdtDmLoaiCongTrinh> Children { get; set; }
        public string Lns { get; set; }
        public string L { get; set; }
        public string K { get; set; }
        public string M { get; set; }
        public string Tm { get; set; }
        public string Ttm { get; set; }
        public string Ng { get; set; }
        public string Tng { get; set; }
        public string Tng1 { get; set; }
        public string Tng2 { get; set; }
        public string Tng3 { get; set; }
        public string XauNoiMa { get; set; }

        public VdtDmLoaiCongTrinh()
        {
            Children = new HashSet<VdtDmLoaiCongTrinh>();
        }
    }
}
