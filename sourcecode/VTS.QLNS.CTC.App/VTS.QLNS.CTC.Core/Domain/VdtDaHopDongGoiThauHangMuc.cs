using System;
using System.Collections.Generic;
using System.Text;

#nullable disable

namespace VTS.QLNS.CTC.Core.Domain
{
    public class VdtDaHopDongGoiThauHangMuc : EntityBase
    {
        //public Guid Id { get; set; }
        public Guid? IIDHopDongGoiThauNhaThauID { get; set; }
        public Guid? IIDChiPhiID { get; set; }
        public Guid? IIDHangMucID { get; set; }
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
        public double? FGiaTriTruocDC { get; set; }
        public double? FGiaTriDuocDuyet { get; set; }
    }
}
