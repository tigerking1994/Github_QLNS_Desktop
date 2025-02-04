using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace VTS.QLNS.CTC.Core.Domain.Query
{
    public class ReportChiTieuDuToanDynamicMLNSQuery
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
        //public string TNG { get; set; }
        //public string TNG1 { get; set; }
        //public string TNG2 { get; set; }
        //public string TNG3 { get; set; }
        public string XauNoiMa { get; set; }
        public string MoTa { get; set; }
        public string MaDonVi { get; set; }
        public string TenDonVi { get; set; }
        public double? TonKho { get; set; }
        public double? TuChi { get; set; }
        public double? HienVat { get; set; }
        public double? DuPhong { get; set; }
        public double? HangNhap { get; set; }
        public double? HangMua { get; set; }
        public double? PhanCap { get; set; }
        public Guid MlnsId { get; set; }
        public Guid? MlnsIdParent { get; set; }
        [NotMapped]
        public double TongSo
        {
            get => (Col1.HasValue ? Col1.Value : 0)
                + (Col2.HasValue ? Col2.Value : 0)
                + (Col3.HasValue ? Col3.Value : 0)
                + (Col4.HasValue ? Col4.Value : 0)
                + (Col5.HasValue ? Col5.Value : 0)
                + (Col6.HasValue ? Col6.Value : 0)
                + (Col7.HasValue ? Col7.Value : 0);
        }
        [NotMapped]
        public double TongSoOld
        {
            get => (TonKho.HasValue ? TonKho.Value : 0) + (TuChi.HasValue ? TuChi.Value : 0) + (HienVat.HasValue ? HienVat.Value : 0) + (DuPhong.HasValue ? DuPhong.Value : 0)
                    + (HangNhap.HasValue ? HangNhap.Value : 0) + (HangMua.HasValue ? HangMua.Value : 0) + (PhanCap.HasValue ? PhanCap.Value : 0);
        }
        [NotMapped]
        public bool IsHangCha { get; set; }

        [NotMapped]
        public double? Col1 { get; set; }
        [NotMapped]
        public double? Col2 { get; set; }
        [NotMapped]
        public double? Col3 { get; set; }
        [NotMapped]
        public double? Col4 { get; set; }
        [NotMapped]
        public double? Col5 { get; set; }
        [NotMapped]
        public double? Col6 { get; set; }
        [NotMapped]
        public double? Col7 { get; set; }
        [NotMapped]
        public double TongAll { get; set; }
    }
}
