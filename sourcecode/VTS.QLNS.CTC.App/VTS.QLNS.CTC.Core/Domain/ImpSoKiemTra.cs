using System;
using System.Collections.Generic;
using System.Text;

#nullable disable

namespace VTS.QLNS.CTC.Core.Domain
{
    public partial class ImpSoKiemTra : EntityBase
    {
        public Guid? ImportId { get; set; }
        public string KyHieu { get; set; }
        public string Stt { get; set; }
        public string MoTa { get; set; }
        public double? HuyDong { get; set; }
        public double? TuChi { get; set; }
        public double? MuaHangHienVat { get; set; }
        public double? DacThu { get; set; }
        public string GhiChu { get; set; }
        public string Loai { get; set; }
    }
}
