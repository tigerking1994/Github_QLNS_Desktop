using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

#nullable disable

namespace VTS.QLNS.CTC.Core.Domain
{
    public class VdtDaHopDongGoiThauNhaThau : EntityBase
    {
        public Guid? IIDHopDongID { get; set; }
        public Guid? IIDGoiThauID { get; set; }
        public Guid? IIDNhaThauID { get; set; }
        public double? FGiaTri { get; set; }
        public double? FGiaTriTrungThau { get; set; }
        public Guid? IIDDonViTienTeID { get; set; }
        public Guid? IIDTienTeID { get; set; }
        public double? FTiGiaDonVi { get; set; }
        public double? FTiGia { get; set; }
        public string SUserCreate { get; set; }
        public DateTime? DDateCreate { get; set; }
        public string SUserUpdate { get; set; }
        public DateTime? DDateUpdate { get; set; }
        public double? FGiaTriTrungThauTruocDC { get; set; }
        public double FGiaTriHopDong { get; set; }
        public double? FGiaTriHopDongTruocDieuChinh { get; set; }
        [NotMapped]
        public ObservableCollection<VdtDaHopDongGoiThauChiPhi> ListChiPhi { get; set; }
    }
}
