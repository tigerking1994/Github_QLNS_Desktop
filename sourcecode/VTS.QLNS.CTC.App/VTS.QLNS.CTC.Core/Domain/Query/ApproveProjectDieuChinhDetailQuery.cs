using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace VTS.QLNS.CTC.Core.Domain.Query
{
    public class ApproveProjectDieuChinhDetailQuery
    {
        public int Loai { get; set; }
        public string TenLoai { get; set; }
        public bool? IsHangCha { get; set; }
        public Guid? IdQDChiPhi { get; set; }
        public Guid? IdChiPhi { get; set; }
        public Guid? IdQDNguonVon { get; set; }
        public int? IdNguonVon { get; set; }
        public Guid? IdQDHangMuc { get; set; }
        public Guid? IdHangMuc { get; set; }
        public string TenHangMuc { get; set; }
        public Guid? IIdQddauTuId { get; set; }
        public string NoiDung { get; set; }
        public double? GiaTriTruocDieuChinh { get; set; }
        public double? GiaTriDieuChinh { get; set; }
        public double? GiaTriSauDieuChinh { get; set; }
    }
}
