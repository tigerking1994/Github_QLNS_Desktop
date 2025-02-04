using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

#nullable disable

namespace VTS.QLNS.CTC.Core.Domain
{
    [Table("HT_NguoiDung")]
    public partial class HtNguoiDung : EntityBase
    {
        public HtNguoiDung()
        {
            SysGroupUsers = new HashSet<HtNhomNguoiDung>();
            NsNguoiDungDonVis = new HashSet<NguoiDungDonVi>();
            NguoiDungLns = new HashSet<NsNguoiDungLns>();
            TlNguoiDungPhanHos = new HashSet<NguoiDungPhanHo>();
            BKichHoat = true;
        }

        [Column("sTaiKhoan")]
        public string STaiKhoan { get; set; }
        [Column("sMatKhau")]
        public string SMatKhau { get; set; }
        [Column("sHo")]
        public string SHo { get; set; }
        [Column("bKichHoat")]
        public bool BKichHoat { get; set; }
        [Column("sTen")]
        public string STen { get; set; }
        [Column("sDuongDanAnh")]
        public string SDuongDanAnh { get; set; }
        [Column("sEMAIL")]
        public string SEmail { get; set; }
        [Column("ACTIVATION_KEY")]
        public string ActivationKey { get; set; }
        [Column("RESET_KEY")]
        public string ResetKey { get; set; }
        [Column("sNguoiTao")]
        public string SNguoiTao { get; set; }
        [Column("DNgayTao")]
        public DateTime? DNgayTao { get; set; }
        [Column("dNgayCaiLai")]
        public DateTime? DNgayCaiLai { get; set; }
        [Column("sNguoiSua")]
        public string SNguoiSua { get; set; }
        [Column("dNgaySua")]
        public DateTime? DNgaySua { get; set; }
        [NotMapped]
        public List<string> Authorities { get; set; }
        [NotMapped]
        public Dictionary<string, List<string>> FuncAuthorities { get; set; }

        public virtual ICollection<HtNhomNguoiDung> SysGroupUsers { get; set; }
        [NotMapped]
        public virtual ICollection<NguoiDungDonVi> NsNguoiDungDonVis { get; set; }
        [NotMapped]
        public virtual ICollection<NguoiDungPhanHo> TlNguoiDungPhanHos { get; set; }
        [NotMapped]
        public virtual ICollection<NsNguoiDungLns> NguoiDungLns { get; set; }
    }
}
