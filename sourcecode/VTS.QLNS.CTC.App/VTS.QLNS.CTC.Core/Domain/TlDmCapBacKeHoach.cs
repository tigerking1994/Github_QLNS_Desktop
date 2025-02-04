using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace VTS.QLNS.CTC.Core.Domain
{
    public partial class TlDmCapBacKeHoach : EntityBase
    {
        public string MaCb { get; set; }
        public string TenCb { get; set; }
        public bool? Splits { get; set; }
        public string Parent { get; set; }
        public bool? Readonly { get; set; }
        public string MoTa { get; set; }
        public decimal? LhtHs { get; set; }
        public decimal? BhxhCq { get; set; }
        public decimal? BhxhCn { get; set; }
        public decimal? BhytCq { get; set; }
        public decimal? BhytCn { get; set; }
        public decimal? BhtnCq { get; set; }
        public decimal? BhtnCn { get; set; }
        public decimal? KpcdCq { get; set; }
        public decimal? KpcdCn { get; set; }
        public int? ThoiHanTang { get; set; }
        public string MaCbKeHoach { get; set; }
        public string TenCbKeHoach { get; set; }
        public string MoTaKeHoach { get; set; }
        public int? TuoiHuuNam { get; set; }
        public int? TuoiHuuNu { get; set; }
        public double? PcrqTt { get; set; }
        public decimal? HsLuongKeHoach { get; set; }
        public Guid? IdHslKeHoach { get; set; }
        public Guid? IdHslHienTai { get; set; }
        public int? NamLamViec { get; set; }
        public string Nhom { get; set; }
        public Guid? IdHslTran { get; set; }
        public string MoTaLuongTran { get; set; }
        public decimal? HsLuongTran { get; set; }
        public string MaCbTran { get; set; }
        public decimal? HsVk { get; set; }
    }
}
