using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace VTS.QLNS.CTC.Core.Domain
{
    public partial class DonVi : EntityBase
    {
        public Guid? IdParent { get; set; }
        public string IIDMaDonVi { get; set; }
        public string TenDonVi { get; set; }
        public string KyHieu { get; set; }
        public string MoTa { get; set; }
        public string MaSoKBNN { get; set; }
        public string MaSoDVQHNS { get; set; }
        public string Loai { get; set; }
        public int? NamLamViec { get; set; }
        public int? ITrangThai { get; set; }
        public DateTime? DNgayTao { get; set; }
        public string SNguoiTao { get; set; }
        public DateTime? DNgaySua { get; set; }
        public string SNguoiSua { get; set; }
        public string Tag { get; set; }
        public string Log { get; set; }
        
        public int? LoaiNganSach { get; set; }
        public bool BCoNSNganh { get; set; }
        public string Khoi { get; set; }
        
        public DonVi Parent { get; set; }
        public ICollection<DonVi> Children { get; set; }

        [NotMapped]
        public string ParentName { get; set; }
        [NotMapped]
        public ICollection<DanhMuc> DanhMucChuyenNganh { get; set; }
        [NotMapped]
        public string TenDanhMuc { get; set; }
        [NotMapped]
        public ICollection<NsMucLucNganSach> LNS { get; set; }
        [NotMapped]
        public string TenLNS { get; set; }
        [NotMapped]
        public string MaTenDonVi => string.Format("{0} - {1}", IIDMaDonVi, TenDonVi);
        public int? iCapDonVi { get; set; }
        public bool? IsPhongBan { get; set; }
        [NotMapped]
        public string STT { get; set; }
    }
}
