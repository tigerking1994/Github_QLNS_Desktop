using System;
using System.ComponentModel.DataAnnotations;

namespace VTS.QLNS.CTC.Core.Domain
{
    public partial class BhDmThamDinhQuyetToan : EntityBase
    {
        [Key]
        public override Guid Id { get; set; }
        public int IMa { get; set; }
        public int? IMaCha { get; set; }
        public int? ISTT { get; set; }
        public string SSTT { get; set; }
        public string SNoiDung { get; set; }
        public string SXauNoiMa { get; set; }
        public int IKieuChu { get; set; }
        public int INamLamViec { get; set; }
        public bool ITrangThai { get; set; }
        public bool ILock { get; set; }
        public string SNguoiTao { get; set; }
        public string SNguoiSua { get; set; }
    }
}
