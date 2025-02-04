using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace VTS.QLNS.CTC.Core.Domain
{
    public partial class NsBkChungTu : EntityBase
    {
        public NsBkChungTu()
        {
            NsBkChungTuChiTiets = new HashSet<NsBkChungTuChiTiet>();
        }

        [Column("iID_BKChungTu")]
        public Guid Id { get; set; }
        public string SSoChungTu { get; set; }
        public int? ISoChungTuIndex { get; set; }
        public DateTime? DNgayChungTu { get; set; }
        public string SSoQuyetDinh { get; set; }
        public DateTime? DNgayQuyetDinh { get; set; }
        public string IIdMaDonVi { get; set; }
        public string SXauNoiMa { get; set; }
        public string SMoTa { get; set; }
        public string SNoiDung { get; set; }
        public string SLoai { get; set; }
        public int IThangQuyLoai { get; set; }
        public int IThangQuy { get; set; }
        public string SThangQuyMoTa { get; set; }
        public double FTongTuChi { get; set; }
        public double FTongHienVat { get; set; }
        public int INamNganSach { get; set; }
        public int IIdMaNguonNganSach { get; set; }
        public int? INamLamViec { get; set; }
        public DateTime? DNgayTao { get; set; }
        public string SNguoiTao { get; set; }
        public DateTime? DNgaySua { get; set; }
        public string SNguoiSua { get; set; }
        public bool BKhoa { get; set; }
        public Guid? IIdDeTaiId { get; set; }

        public virtual ICollection<NsBkChungTuChiTiet> NsBkChungTuChiTiets { get; set; }
    }
}
