using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace VTS.QLNS.CTC.Core.Domain.Query
{
    public class NhDaDuToanChiPhiQuery
    {
        public Guid? Id { get; set; }
        public string STenChiPhi { get; set; }
        public Guid? IIdQDDauTuChiPhiId { get; set; }
        public Guid? IIdQDDauTuChiPhiParentId { get; set; }
        public Guid? IIdDuToanChiPhiId { get; set; }
        public Guid? IIdDuToanId { get; set; }
        public Guid? IIdChiPhiId { get; set; }
        public Guid? IIdParentId { get; set; }
        public bool? IsNew { get; set; }
        public bool? IsEditHangMuc { get; set; }
        public string SMaOrder { get; set; }
        public bool IsHangCha { get; set; }
        //public double? FGiaTriDieuChinh { get; set; }
        //public double? GiaTriTruocDieuChinh { get; set; }
        public double? FGiaTriNgoaiTeKhac { get; set; }
        public double? FGiaTriUSD { get; set; }
        public double? FGiaTriVND { get; set; }
        public double? FGiaTriEUR { get; set; }
        public double? FGiaTriNgoaiTeKhacQDDTPheDuyet { get; set; }
        public double? FGiaTriUSDQDDTPheDuyet { get; set; }
        public double? FGiaTriVNDQDDTPheDuyet { get; set; }
        public double? FGiaTriEURQDDTPheDuyet { get; set; }
    }
}
