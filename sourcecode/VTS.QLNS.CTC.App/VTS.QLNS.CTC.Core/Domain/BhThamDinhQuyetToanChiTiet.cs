using System;
using System.ComponentModel.DataAnnotations;

namespace VTS.QLNS.CTC.Core.Domain
{
    public class BhThamDinhQuyetToanChiTiet : EntityBase
    {
        [Key]
        public override Guid Id { get; set; }
        public Guid IID_BH_TDQT_ChungTu { get; set; }
        public int INamLamViec { get; set; }
        public string IID_MaDonVi { get; set; }
        public double FSoBaoCao { get; set; }
        public double FSoThamDinh { get; set; }
        public double FQuanNhan { get; set; }
        public double FCNVLDHD { get; set; }
        public string SGhiChu { get; set; }
        public int IMa { get; set; }
    }
}
