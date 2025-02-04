using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations.Schema;

namespace VTS.QLNS.CTC.Core.Domain.Query
{
    public class ThDChungTuQuery
    {
        public Guid Id { get; set; }
        public string SoChungTu { get; set; }
        public bool IsLocked { get; set; }
        public string MoTa { get; set; }
        public string SoQuyetDinh { get; set; }
        public DateTime? NgayQuyetDinh { get; set; }
        public DateTime? NgayChungTu { get; set; }
        public string IdDonVi { get; set; }
        public string TenDonVi { get; set; }
        public int? NamLamViec { get; set; }
        public int? NamNganSach { get; set; }
        public int? NguonNganSach { get; set; }
        public double? TuChi { get; set; }
        //public double? TuChiNganh { get; set; }
        public string UserCreator { get; set; }
        public int? ILoai { get; set; }
        public int? ILoaiChungTu { get; set; }
    }
}
