using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations.Schema;

namespace VTS.QLNS.CTC.Core.Domain.Query
{
    public class VdtDaDuToanChiPhiQuery
    {
        public Guid? Id { get; set; }
        public string TenChiPhi { get; set; }
        public Guid? IdDuToanChiPhi { get; set; }
        public Guid? IdChiPhi { get; set; }
        public Guid? IdDuToan { get; set; }
        public double? GiaTriPheDuyet { get; set; }
        public Guid? IdChiPhiDuAn { get; set; }
        public bool? IsHangCha { get; set; }
        public bool? IsLoaiChiPhi { get; set; }
        public int? IThuTu { get; set; }
        public Guid? IdChiPhiDuAnParent { get; set; }
        public bool? IsDuAnChiPhiOld { get; set; }
        public bool? IsEditHangMuc { get; set; }
        public string MaOrder { get; set; }
        public double? FGiaTriDieuChinh { get; set; }
        public double? GiaTriTruocDieuChinh { get; set; }
        public double? FTienPheDuyetQDDT { get; set; }
    }
}
