using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace VTS.QLNS.CTC.Core.Domain
{
    [Table("NS_NC3Y_ChungTuChiTiet")]
    public partial class NsNc3YChungTuChiTiet : EntityBase
    {
        [Column("iID_CTNC3YChiTiet")]
        [Key]
        public override Guid Id { get; set; }
        public Guid IIdCtsoKiemTra { get; set; }
        public string IIdMaDonVi { get; set; }
        public Guid IIdMlskt { get; set; }
        public string SMoTa { get; set; }
        public double FDuToan { get; set; }
        public double FUocTH { get; set; }
        public double FNCNam1 { get; set; }
        public double FNCNam2 { get; set; }
        public double FNCNam3 { get; set; }
        public string SGhiChu { get; set; }
        public int ILoai { get; set; }
        public int INamLamViec { get; set; }
        public int? INamNganSach { get; set; }
        public int? IIdMaNguonNganSach { get; set; }
        public DateTime? DNgayTao { get; set; }
        public string SNguoiTao { get; set; }
        public DateTime? DNgaySua { get; set; }
        public string SNguoiSua { get; set; }
        public int? ILoaiChungTu { get; set; }
        public string SKyHieu { get; set; }
        [NotMapped]
        public string SKyHieuCu { get; set; }
        public string STenDonVi { get; set; }
        [NotMapped]
        public Guid? IdParent { get; set; }
        [NotMapped]
        public string Stt { get; set; }
        [NotMapped]
        public bool IsHangCha { get; set; }
        [NotMapped]
        public bool IsAdd { get; set; }
        [NotMapped]
        public bool IsFirstParentRow { get; set; }
        [NotMapped]
        public bool IsRemainRow { get; set; }
        [NotMapped]
        public string Nganh { get; set; }
        [NotMapped]
        public string SL { get; set; }
        [NotMapped]
        public string SK { get; set; }
        [NotMapped]
        public string SM { get; set; }
        [NotMapped]
        public string SNG { get; set; }
        [NotMapped]
        public string NganhParent { get; set; }
        [NotMapped]
        public double FCong { get; set; }
        public NsSktChungTu ChungTu { get; set; }
        [NotMapped]
        public string XauNoiMa { get; set; }
        [NotMapped]
        public Guid IIdCtsoKiemTraChild { get; set; }
        [NotMapped]
        public bool IsAuToFillTuChi { get; set; }
        [NotMapped]
        public string SKhoiDonVi { get; set; }
        [NotMapped]
        public bool ImportStatus { get; set; }
    }

    public class NsNc3YChungTuChiTietComparer : IEqualityComparer<NsNc3YChungTuChiTiet>
    {
        public bool Equals(NsNc3YChungTuChiTiet x, NsNc3YChungTuChiTiet y)
        {
            return x.SKyHieu == y.SKyHieu && x.IIdMaDonVi == y.IIdMaDonVi;
        }
        public int GetHashCode(NsNc3YChungTuChiTiet obj)
        {
            return obj.Id.GetHashCode();
        }
    }
}
