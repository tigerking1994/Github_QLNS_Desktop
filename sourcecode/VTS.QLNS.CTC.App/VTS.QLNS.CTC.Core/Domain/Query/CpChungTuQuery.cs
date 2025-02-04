using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace VTS.QLNS.CTC.Core.Domain.Query
{
    public class CpChungTuQuery
    {
        public Guid Id { get; set; }
        public string SoChungTu { get; set; }
        public int? SoChungTuIndex { get; set; }
        public DateTime? NgayChungTu { get; set; }
        public string SoQuyetDinh { get; set; }
        public DateTime? NgayQuyetDinh { get; set; }
        public string MoTa { get; set; }
        public string IdDonVi { get; set; }
        public string TenDonVi { get; set; }
        public string Lns { get; set; }
        public string ChiTietToi { get; set; }
        public string ITypeMoTa { get; set; }
        public int ILoai { get; set; }
        public int? NamLamViec { get; set; }
        public int? NamNganSach { get; set; }
        public DateTime? DateCreated { get; set; }
        public string UserCreator { get; set; }
        public DateTime? DateModified { get; set; }
        public string UserModifier { get; set; }
        public bool IsLocked { get; set; }
        public int? NguonNganSach { get; set; }
        public string TenLoai { get; set; }
        public double? SoTuChi { get; set; }
        public string LoaiCap { get; set; }
        public string DSSoChungTuTongHop { get; set; }
        public bool? BDaTongHop { get; set; }
    }
}
