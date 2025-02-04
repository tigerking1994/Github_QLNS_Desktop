using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

#nullable disable

namespace VTS.QLNS.CTC.Core.Domain
{
    public class HtChucNang : EntityBase
    {
        [Key]
        public string IIDMaChucNang { get; set; }
        [Column("iID_ChucNang")]
        public override Guid Id { get; set; }
        public Guid? IIDChucNangCha { get; set; }
        public string SSTT { get; set; }
        public bool ITrangThai { get; set; }
        public string STenChucNang { get; set; }
        public bool BHangCha { get; set; }
        [NotMapped]
        public string OldName { get; set; }
        [NotMapped]
        public string OldFuncCode { get; set; }
        [NotMapped]
        public ICollection<HtQuyen> HTQuyens { get; set; }
        public IList<HtQuyenChucNang> SysFunctionAuthorities { get; set; }

        public HtChucNang()
        {
            HTQuyens = new HashSet<HtQuyen>();
            SysFunctionAuthorities = new List<HtQuyenChucNang>();
        }
    }
}
