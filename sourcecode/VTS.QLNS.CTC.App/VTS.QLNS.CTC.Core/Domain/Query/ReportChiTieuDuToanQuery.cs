using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace VTS.QLNS.CTC.Core.Domain.Query
{
    public class ReportChiTieuDuToanQuery
    {
        public string LNS1 { get; set; }
        public string LNS3 { get; set; }
        public string LNS5 { get; set; }
        public string LNS { get; set; }
        public string L { get; set; }
        public string K { get; set; }
        public string M { get; set; }
        public string TM { get; set; }
        public string TTM { get; set; }
        public string NG { get; set; }
        public string TNG { get; set; }
        public string TNG1 { get; set; }
        public string TNG2 { get; set; }
        public string TNG3 { get; set; }
        public string XauNoiMa { get; set; }
        public string MoTa { get; set; }
        public string MaDonVi { get; set; }
        public string TenDonVi { get; set; }
        public double? TuChi { get; set; }
        public double? RutKBNN { get; set; }
        public double? HienVat { get; set; }
        public double? DuPhong { get; set; }
        public double? HangNhap { get; set; }
        public double? HangMua { get; set; }
        public double? PhanCap { get; set; }
        public double? CapBangTien { get; set; }
        public Guid MlnsId { get; set; }
        public Guid? MlnsIdParent { get; set; }
        [NotMapped]
        public double TongSo
        {
            get => (TuChi.HasValue ? TuChi.Value : 0) + (HienVat.HasValue ? HienVat.Value : 0);
        }
        [NotMapped]
        public double TongSoOld
        {
            get => (TuChi.HasValue ? TuChi.Value : 0) + (HienVat.HasValue ? HienVat.Value : 0) + (DuPhong.HasValue ? DuPhong.Value : 0)
                    + (HangNhap.HasValue ? HangNhap.Value : 0) + (HangMua.HasValue ? HangMua.Value : 0) + (PhanCap.HasValue ? PhanCap.Value : 0);
        }
        [NotMapped]
        public bool IsHangCha { get; set; }
    }
}
