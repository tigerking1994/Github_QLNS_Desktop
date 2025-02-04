using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace VTS.QLNS.CTC.Core.Domain.Query
{
    public class HopDongHangMucQuery
    {
        public Guid? Id { get; set; }
        public bool? IsChecked { get; set; }
        public Guid? IIDHopDongID { get; set; }
        public Guid? IIDGoiThauID { get; set; }
        public Guid? IdGoiThauNhaThau { get; set; }
        public Guid? IIDChiPhiID { get; set; }
        public Guid? IdChiPhiDuAnParent { get; set; }
        public Guid? IIDHangMucID { get; set; }
        public Guid? HangMucParentId { get; set; }
        public Guid? IIDNhaThauID { get; set; }
        public string STenChiPhi { get; set; }
        public string STenHangMuc { get; set; }
        public double? FTienGoiThau { get; set; }
        public double? FGiaTriConLai { get; set; }
        public int? IThuTu { get; set; }
        public string MaOrDer { get; set; }
        public bool? IsHangCha { get; set; }
        public double? FGiaTriDuocDuyet { get; set; }
        public double? FTienGoiThauTruocDC { get; set; }
    }
}
