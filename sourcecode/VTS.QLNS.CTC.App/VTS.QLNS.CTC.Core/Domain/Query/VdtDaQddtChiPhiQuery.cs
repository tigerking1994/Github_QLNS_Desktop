using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations.Schema;

namespace VTS.QLNS.CTC.Core.Domain.Query
{
    public class VdtDaQddtChiPhiQuery
    {
        public Guid? Id { get; set; }
        public string TenChiPhi { get; set; }
        public Guid? IdQDChiPhi { get; set; }
        public Guid? IdChiPhi { get; set; }
        public Guid? IdQDDauTu { get; set; }
        public double? GiaTriPheDuyet { get; set; }
        public Guid? IdChiPhiDuAn { get; set; }
        public bool? IsHangCha { get; set; }
        public bool? IsLoaiChiPhi { get; set; }
        public int? IThuTu { get; set; }
        public Guid? IdChiPhiDuAnParent { get; set; }
        public bool? IsEditHangMuc { get; set; }
        public double? GiaTriDieuChinh { get; set; }
        public double? GiaTriTruocDieuChinh { get; set; }
        public string SMaChiPhi { get; set; }
    }
}
