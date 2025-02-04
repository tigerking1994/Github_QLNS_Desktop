using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VTS.QLNS.CTC.Core.Domain;

namespace VTS.QLNS.CTC.App.Model
{
    public class MLNSModel : DetailModelBase
    {
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
        public NsMucLucNganSach Parent { get; set; }
        public ICollection<NsMucLucNganSach> Children { get; set; }

    }
}
