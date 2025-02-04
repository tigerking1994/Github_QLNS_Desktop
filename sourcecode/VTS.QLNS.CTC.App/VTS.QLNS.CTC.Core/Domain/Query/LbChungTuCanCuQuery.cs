using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace VTS.QLNS.CTC.Core.Domain.Query
{
    public class LbChungTuCanCuQuery
    {
        public bool IsSelected { get; set; }
        public Guid Id { get; set; }
        public string SoCongVan { get; set; }
        public string SoChungTu { get; set; }
        public DateTime? NgayChungTu { get; set; }
        public string NguonNganSach { get; set; }
        public string IdDonVi { get; set; }
        public string TenDonVi { get; set; }
        public double? TuChiTaiNganh { get; set; }
        public double? TuChiTaiDonVi { get; set; }
        public double? HangNhap { get; set; }
        public double? HangMua { get; set; }
        public double? PhanCap { get; set; }
        public double? DuPhong { get; set; }
    }
}
