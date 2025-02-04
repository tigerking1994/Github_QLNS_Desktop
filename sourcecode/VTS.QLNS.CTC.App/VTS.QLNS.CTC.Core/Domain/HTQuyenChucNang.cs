using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

#nullable disable

namespace VTS.QLNS.CTC.Core.Domain
{
    public partial class HtQuyenChucNang : EntityBase
    {
        [NotMapped]
        public override Guid Id { get; set; }
        public string IIDMaChucNang { get; set; }
        public string IIDMaQuyen { get; set; }
        public HtChucNang HTChucNang { get; set; }
        public HtQuyen HTQuyen { get; set; }
    }
}
