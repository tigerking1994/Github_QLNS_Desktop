using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace VTS.QLNS.CTC.Core.Domain
{
    public partial class TlPhuCapMln : EntityBase
    {
        public string MaPhuCap { get; set; }
        public string TenPhuCap { get; set; }
        public string MaCachTl { get; set; }
        public string XauNoiMa { get; set; }
        public string Lns { get; set; }
        public string L { get; set; }
        public string K { get; set; }
        public string M { get; set; }
        public string Tm { get; set; }
        public string Ttm { get; set; }
        public string Ng { get; set; }
        public string MoTa { get; set; }
        public string MaNguonNganSach { get; set; }
        public string NguonNganSach { get; set; }
        public DateTime? DateCreated { get; set; }
        public string UserCreator { get; set; }
        public DateTime? DateModified { get; set; }
        public string UserModifier { get; set; }
        public string ITrangThai { get; set; }
        public Guid? IdPhuCap { get; set; }
        public Guid? IdCachTinhLuong { get; set; }
        public Guid? IdNguonNganSach { get; set; }
        public Guid? IdMlns { get; set; }
        public string MaCb { get; set; }
        public string ChiTietToi { get; set; }
        public int? Nam { get; set; }
        public string Stng { get; set; }
        public string Stng1 { get; set; }
        public string Stng2 { get; set; }
        public string Stng3 { get; set; }
    }
}
