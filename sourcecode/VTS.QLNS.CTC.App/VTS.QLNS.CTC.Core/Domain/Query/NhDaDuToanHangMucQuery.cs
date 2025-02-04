using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace VTS.QLNS.CTC.Core.Domain.Query
{
    public class NhDaDuToanHangMucQuery
    {
        public Guid? Id { get; set; }
        public Guid? IIdQDDauTuChiPhiId { get; set; }
        public Guid? IIdQDDauTuHangMucId { get; set; }
        public Guid? IIdQDDauTuHangMucParentId { get; set; }
        public Guid? IIdLoaiCongTrinh { get; set; }
        public Guid? IIdDuToanChiPhiId { get; set; }
        public string SMaHangMuc { get; set; }
        public string STenHangMuc { get; set; }
        public bool? IsHangCha { get; set; }
        public Guid? IIdDuToanId { get; set; }
        public Guid? IIdParentId { get; set; }
        public Guid? IIdDuToanHangMucId { get; set; }
        public Guid? IIdHangMucPhanChiaId { get; set; }
        //public double? GiaTriPheDuyet { get; set; }
        //public double? FGiaTriDieuChinh { get; set; }
        //public double? GiaTriTruocDieuChinh { get; set; }
        //public bool? IsHangMucOld { get; set; }
        public string SMaOrder { get; set; }
        //public string STenChiPhi { get; set; }
        //public double? FTienPheDuyetQDDT { get; set; }
        //public double? FTienChenhLech { get; set; }
        public double? FGiaTriNgoaiTeKhac { get; set; }
        public double? FGiaTriUSD { get; set; }
        public double? FGiaTriVND { get; set; }
        public double? FGiaTriEUR { get; set; }
        public double? FGiaTriNgoaiTeKhacQDDTPheDuyet { get; set; }
        public double? FGiaTriUSDQDDTPheDuyet { get; set; }
        public double? FGiaTriVNDQDDTPheDuyet { get; set; }
        public double? FGiaTriEURQDDTPheDuyet { get; set; }
        public bool IsNew { get; set; }
    }
}
