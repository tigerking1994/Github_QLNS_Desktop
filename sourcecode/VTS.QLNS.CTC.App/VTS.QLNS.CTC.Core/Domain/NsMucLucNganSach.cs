using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

#nullable disable

namespace VTS.QLNS.CTC.Core.Domain
{

    public partial class NsMucLucNganSach : EntityBase
    {
        [Column("iID")]
        [Key]
        public override Guid Id { get; set; }
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
        public string Tng1 { get; set; }
        public string Tng2 { get; set; }
        public string Tng3 { get; set; }
        public string MoTa { get; set; }
        public int? NamLamViec { get; set; }
        public bool BHangCha { get; set; }
        public int? ITrangThai { get; set; }
        public string IdPhongBan { get; set; }
        public DateTime? DNgayTao { get; set; }
        public string SNguoiTao { get; set; }
        public DateTime? DNgaySua { get; set; }
        public string SNguoiSua { get; set; }
        public string Tag { get; set; }
        public string Log { get; set; }
        public bool? ILock { get; set; }
        public string ILoai { get; set; }
        public int? ILoaiNganSach { get; set; }
        public string ChiTietToi { get; set; }
        public bool BNgay { get; set; }
        public bool BSoNguoi { get; set; }
        public bool BTonKho { get; set; }
        public bool BTuChi { get; set; }
        public bool BHangNhap { get; set; }
        public bool BHangMua { get; set; }
        public bool BHienVat { get; set; }
        public bool BDuPhong { get; set; }
        public bool BPhanCap { get; set; }
        public string SNhapTheoTruong { get; set; }
        public string IdMaDonVi { get; set; }
        public string SCPChiTietToi { get; set; }
        public bool? BHangChaDuToan { get; set; }
        public bool? BHangChaQuyetToan { get; set; }
        public string SQuyetToanChiTietToi { get; set; }
        public string SDuToanChiTietToi { get; set; }
        public string SMaCB { get; set; }
        public double FTienAn { get; set; }

        //public NsMucLucNganSach Parent { get; set; }
        //public ICollection<NsMucLucNganSach> Children { get; set; }
        [NotMapped]
        public string TenPhongBan { get; set; }
        [NotMapped]
        public NsMlsktMlns SktMucLucMap { get; set; }
        [NotMapped]
        public bool IsEditableMaCB { get; set; }
        [NotMapped]
        public bool IsEditableCPChiTietToi { get; set; }
        [NotMapped]
        public bool IsUsedDuToanChiTietToi { get; set; } = true;
        [NotMapped]
        public bool IsUsedQuyetToanChiTietToi { get; set; } = true;
        [NotMapped]
        public bool IsParent { get; set; }
        [NotMapped]
        public bool IsEditableStatus { get; set; }
        [NotMapped]
        public string SXauNoiMa => string.Join("-", new string[] { Lns, L, K, M, Tm, Ttm }.Where(s => !string.IsNullOrEmpty(s)));

        //Thêmmmmm
        public double TuChi { get; set; }
    }

    public class NsMucLucNganSachEqualityComparer : IEqualityComparer<NsMucLucNganSach>
    {
        public bool Equals(NsMucLucNganSach b1, NsMucLucNganSach b2)
        {
            if (b2 == null && b1 == null)
                return true;
            else if (b1 == null || b2 == null)
                return false;
            else if (b1.XauNoiMa.Equals(b2.XauNoiMa) && b1.NamLamViec.Equals(b2.NamLamViec))
                return true;
            else
                return false;
        }

        public int GetHashCode(NsMucLucNganSach bx)
        {
            int hCode = bx.XauNoiMa.GetHashCode() * bx.NamLamViec.Value;
            return hCode.GetHashCode();
        }
    }
}
