using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations.Schema;

namespace VTS.QLNS.CTC.Core.Domain.Query
{
    public class SktMucLucDtQuery
    {
        public Guid Id { get; set; }
        public Guid IdMucLuc { get; set; }
        public Guid? IdParent { get; set; }
        public string KyHieu { get; set; }
        public string M { get; set; }
        public string Stt { get; set; }
        public string MoTa { get; set; }
        public bool BHangCha { get; set; }
        public int NamLamViec { get; set; }
        public DateTime? DateCreated { get; set; }
        public string UserCreator { get; set; }
        public DateTime? DateModified { get; set; }
        public string UserModifier { get; set; }
        public string Muc { get; set; }
        public string Lns { get; set; }
        public double? TuChi { get; set; }
        public double HangMua { get; set; }
        public double HangNhap { get; set; }
        public double PhanCap { get; set; }
        public double MuaHangHienVat { get; set; }
        public double DacThu { get; set; }

        public double DtTuChi { get; set; }
        public double DtHangNhap { get; set; }
        public double DtHangMua { get; set; }
        public double DtPhanCap { get; set; }
        public double DtDuPhong { get; set; }
        public double DtChuaPhanCap { get; set; }
    }
}
