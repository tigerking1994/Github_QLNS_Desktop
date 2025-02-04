using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace VTS.QLNS.CTC.Core.Domain.Query
{
    public class LbChungTuChiTietQuery
    {
        public Guid Id { get; set; }
        public Guid? IdChungTu { get; set; }
        public Guid MlnsId { get; set; }
        public Guid? MlnsIdParent { get; set; }
        public string XauNoiMa { get; set; }
        public string Lns { get; set; }
        public string L { get; set; }
        public string K { get; set; }
        public string M { get; set; }
        public string Tm { get; set; }
        public string Ttm { get; set; }
        public string Ng { get; set; }
        public string Tng { get; set; }
        public string TNG1 { get; set; }
        public string TNG2 { get; set; }
        public string TNG3 { get; set; }
        public string MoTa { get; set; }
        public int? NamLamViec { get; set; }
        public bool BHangCha { get; set; }
        public string IdDonVi { get; set; }
        public string TenDonVi { get; set; }
        public double TuChi { get; set; }
        public double PhanCap { get; set; }
        public double ChuaPhanCap { get; set; }
        public double HangNhap { get; set; }
        public double HangMua { get; set; }
        public string GhiChu { get; set; }
        public DateTime? DateCreated { get; set; }
        public string UserCreator { get; set; }
        public DateTime? DateModified { get; set; }
        public string UserModifier { get; set; }
        public int? NguonNganSach { get; set; }
        public double SoChuaPhan { get; set; }
        public double PhanCapCon { get; set; }
    }
}
