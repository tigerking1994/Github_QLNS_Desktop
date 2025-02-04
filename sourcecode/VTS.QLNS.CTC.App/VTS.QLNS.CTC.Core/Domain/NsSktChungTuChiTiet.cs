using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace VTS.QLNS.CTC.Core.Domain
{
    [Table("NS_SKT_ChungTuChiTiet")]
    public partial class NsSktChungTuChiTiet : EntityBase
    {
        [Column("iID_CTSoKiemTraChiTiet")]
        [Key]
        public override Guid Id { get; set; }
        public Guid IIdCtsoKiemTra { get; set; }
        public string IIdMaDonVi { get; set; }
        public Guid IIdMlskt { get; set; }
        public string SMoTa { get; set; }
        public double FHuyDongTonKho { get; set; }
        public double FKhungNganSachDuocDuyet { get; set; }
        public double FSoNganhPhanCap { get; set; }
        public double FTuChi { get; set; }
        public double FTuChiDeNghi { get; set; }
        public string SGhiChu { get; set; }
        public int ILoai { get; set; }
        public int INamLamViec { get; set; }
        public int? INamNganSach { get; set; }
        public int? IIdMaNguonNganSach { get; set; }
        public DateTime? DNgayTao { get; set; }
        public string SNguoiTao { get; set; }
        public DateTime? DNgaySua { get; set; }
        public string SNguoiSua { get; set; }
        public double FHienVat { get; set; }
        public double FPhanCap { get; set; }
        public double FTonKhoDenNgay { get; set; }
        public double FMuaHangCapHienVat { get; set; }
        public double FThongBaoDonVi { get; set; }
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
        public string SSttbc { get; set; }
        [NotMapped]
        public bool IsHangCha { get; set; }
        [NotMapped]
        public bool IsAdd { get; set; }
        [NotMapped]
        public bool IsFirstParentRow { get; set; }
        [NotMapped]
        public bool IsRemainRow { get; set; }
        [NotMapped]
        public double SoNhuCau { get; set; }
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
        public double SoNhuCauMHHV { get; set; }
        [NotMapped]
        public double SoNhuCauDT { get; set; }
        [NotMapped]
        public double DuToan { get; set; }
        [NotMapped]
        public double DuToanMHCHV { get; set; }
        [NotMapped]
        public double DuToanDT { get; set; }
        [NotMapped]
        public double SoKiemTra { get; set; }
        [NotMapped]
        public double SoKiemTraMHHV { get; set; }
        [NotMapped]
        public double SoKiemTraDT { get; set; }
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

    public class NsSktChungTuChiTietComparer : IEqualityComparer<NsSktChungTuChiTiet>
    {
        public bool Equals(NsSktChungTuChiTiet x, NsSktChungTuChiTiet y)
        {
            return x.SKyHieu == y.SKyHieu && x.IIdMaDonVi == y.IIdMaDonVi;
        }
        public int GetHashCode(NsSktChungTuChiTiet obj)
        {
            return obj.Id.GetHashCode();
        }
    }
}
