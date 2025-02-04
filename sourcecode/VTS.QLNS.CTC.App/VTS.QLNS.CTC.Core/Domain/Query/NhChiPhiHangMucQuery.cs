using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace VTS.QLNS.CTC.Core.Domain.Query
{
    public class NhChiPhiHangMucQuery
    {
        public Guid? IdCreate { get; set; }
        public Guid? Id { get; set; }
        public int ILoai { get; set; }
        public Guid? IIdLoaiChiPhi { get; set; }
        public Guid? IId_HangMuc { get; set; }
        public Guid? IID_ChiPhi { get; set; }
        public int? IId_NguonVon { get; set; }
        public string SChiPhi { get; set; }
        public string SNoiDung { get; set; }
        public Guid? IID_ParentID { get; set; }
        public double? FTienPheDuyet { get; set; }
        public double? FTienGoiThau { get; set; }
        public double? FGiaTriConLai { get; set; }
        public Guid? IdLoaiCongTrinh { get; set; }
        public string SLoaiCongTrinh { get; set; }
        public string MaOrDer { get; set; }
        public int? IThuTu { get; set; }
        public bool? IsHangCha { get; set; }
        public bool? IsChecked { get; set; }
    }
}
