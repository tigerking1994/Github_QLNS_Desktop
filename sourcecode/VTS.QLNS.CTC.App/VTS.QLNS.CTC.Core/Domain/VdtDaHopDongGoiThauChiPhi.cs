using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace VTS.QLNS.CTC.Core.Domain
{
    public partial class VdtDaHopDongGoiThauChiPhi : EntityBase
    {
        public Guid? IIdHopDongGoiThauNhaThauId { get; set; }
        public double? FGiaTri { get; set; }
        public Guid? IIdDonViTienTeId { get; set; }
        public Guid? IIdTienTeId { get; set; }
        public double? FTiGiaDonVi { get; set; }
        public double? FTiGia { get; set; }
        public string SUserCreate { get; set; }
        public DateTime? DDateCreate { get; set; }
        public string SUserUpdate { get; set; }
        public DateTime? DDateUpdate { get; set; }
        public Guid? IIdChiPhiId { get; set; }
        public double? FGiaTriTruocDC { get; set; }
        [NotMapped]
        public ObservableCollection<VdtDaHopDongGoiThauHangMuc> ListHangMuc { get; set; }
        [NotMapped]
        public List<VdtDaHopDongDmHangMuc> ListVdtDaHopDongDmHangMuc { get; set; }
    }
}
