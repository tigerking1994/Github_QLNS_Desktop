using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using VTS.QLNS.CTC.Core.Domain.Query;

#nullable disable

namespace VTS.QLNS.CTC.Core.Domain
{
    [Table("NS_SKT_ChungTu")]
    public partial class NsSktChungTu : EntityBase
    {
        [Column("iID_CTSoKiemTra")]
        [Key]
        public override Guid Id { get; set; }
        public string IIdMaDonVi { get; set; }
        public string SSoChungTu { get; set; }
        public DateTime DNgayChungTu { get; set; }
        public string SSoQuyetDinh { get; set; }
        public DateTime? DNgayQuyetDinh { get; set; }
        public string SMoTa { get; set; }
        public int ILoai { get; set; }
        public int? ILoaiNguonNganSach { get; set; }
        public bool BKhoa { get; set; }
        public int INamLamViec { get; set; }
        public int? INamNganSach { get; set; }
        public int? IIdMaNguonNganSach { get; set; }
        public DateTime? DNgayTao { get; set; }
        public string SNguoiTao { get; set; }
        public DateTime? DNgaySua { get; set; }
        public string SNguoiSua { get; set; }
        public int? ISoChungTuIndex { get; set; }
        public int? ILoaiChungTu { get; set; }
        public string SDssoChungTuTongHop { get; set; }
        public double? FTongTuChi { get; set; }
        public double? FTongPhanCap { get; set; }
        public double? FTongMuaHangCapHienVat { get; set; }
        public ICollection<NsSktChungTuChiTiet> ChungTuChiTiets { get; set; }
        public ICollection<NsNc3YChungTuChiTiet> NC3YChungTuChiTiets { get; set; }
        [NotMapped]
        public List<NsSktChungTuChiTiet> ListDetail { get; set; }
        [NotMapped]
        public List<JsonNsSktNganhThamDinhChiTietSktQuery> ListThamDinh { get; set; }
        public bool? BDaTongHop { get; set; }
        [NotMapped]
        public string STenDonVi { get; set; }
        [NotMapped]
        public int Index { get; set; }
        [NotMapped]
        public int IndexDonVi { get; set; }
        [NotMapped]
        public string iLoaiDV { get; set; }
        [NotMapped]
        public bool ImportStatus { get; set; }
        public bool? IsSent { get; set; }
    }
}
