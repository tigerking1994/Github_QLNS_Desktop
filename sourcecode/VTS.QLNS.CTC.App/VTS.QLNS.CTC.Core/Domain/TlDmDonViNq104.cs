using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VTS.QLNS.CTC.Core.Domain
{
    public partial class TlDmDonViNq104 : EntityBase
    {
        public string MaDonVi { get; set; }
        public string TenDonVi { get; set; }
        public string ParentId { get; set; }
        public string XauNoiMa { get; set; }
        public bool? ITrangThai { get; set; }

        [NotMapped]
        public string TenDonViCha { get; set; }
        [NotMapped]
        public bool BHangCha { get; set; }
        public ICollection<TlDmCanBoNq104> TlDmCanBoNq104s { get; set; }
        public ICollection<TlDsCapNhapBangLuongNq104> TlDsCapNhapBangLuongsNq104 { get; set; }
        public ICollection<TlQtChungTuNq104> TlQtChungTusNq104 { get; set; }
        public ICollection<TlQsChungTuNq104> TlQsChungTuNq104s { get; set; }
    }
}
