using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VTS.QLNS.CTC.Core.Domain
{
    public partial class TlDmCanBoNq104 : EntityBase
    {
        public string MaCanBo { get; set; }
        public string TenCanBo { get; set; }
        public string DiaChi { get; set; }
        public string MaCv { get; set; }
        public string MaBl { get; set; }
        public string MaCb { get; set; }
        public string MaPban { get; set; }
        public decimal? Gtgc { get; set; }
        public string DienThoai { get; set; }
        public string MaSoVat { get; set; }
        public string TenDonVi { get; set; }
        public string SoCmt { get; set; }
        public string NoiCapCmt { get; set; }
        public DateTime? NgayCapCmt { get; set; }
        public string SoTaiKhoan { get; set; }
        public string TenKhoBac { get; set; }
        public string MaSoDvSdns { get; set; }
        public string MaDiaBanHc { get; set; }
        public string MaTkLq { get; set; }
        public string Parent { get; set; }
        public string MaKhoBac { get; set; }
        public bool? Splits { get; set; }
        public bool? Readonly { get; set; }
        public bool? KhongLuong { get; set; }
        public string MaHieuCanBo { get; set; }
        public int? Thang { get; set; }
        public int? Nam { get; set; }
        public DateTime? NgayNn { get; set; }
        public DateTime? NgayXn { get; set; }
        public DateTime? NgayTn { get; set; }
        public int? NamTn { get; set; }
        public int? ThangTnn { get; set; }
        public int? NamVk { get; set; }
        public bool? IsNam { get; set; }
        public string MaTangGiam { get; set; }
        public string SoSoLuong { get; set; }
        public DateTime? NgayNhanCb { get; set; }
        public int? ThoiHanTangCb { get; set; }
        public int? ThoiHanTangCvd { get; set; }
        public string CbKeHoach { get; set; }
        public string Cccd { get; set; }
        public string NoiCongTac { get; set; }
        public DateTime? NgaySinh { get; set; }
        public bool? Tm { get; set; }
        public bool? IsLock { get; set; }
        public bool? IsDelete { get; set; }
        public DateTime? DateCreated { get; set; }
        public DateTime? DateModified { get; set; }
        public string UserCreator { get; set; }
        public string UserModifier { get; set; }
        public bool? BHTN { get; set; }
        public bool? PCCV { get; set; }
        public decimal? HsLuongTran { get; set; }
        public decimal? HsLuongKeHoach { get; set; }
        public decimal? HeSoLuong { get; set; }
        //public decimal? SoNguoiPhuThuoc { get; set; }
        public Guid? IdLuongTran { get; set; }

        //public TlDmCapBacNq104 TlDmCapBacNq104 { get; set; }
        //public TlDmChucVuNq104 TlDmChucVuNq104 { get; set; }
        public string MaCbCu { get; set; }
        public string Nhom { get; set; }
        public ICollection<TlCanBoPhuCapNq104> TlCanBoPhuCapsNq104 { get; set; }

        public int ITrangThai { get; set; }
        public DateTime? NgayTruyLinh { get; set; }
        public string MaTangGiamCu { get; set; }
        public bool? BNuocNgoai { get; set; }
        public bool? bKhongTinhNTN { get; set; }
        public string ParentOld { get; set; }
        public bool? BTinhBHXH { get; set; }
        public string LoaiDoiTuong { get; set; }
        public string MaCb104 { get; set; }
        public string Loai { get; set; }
        public string NhomChuyenMon { get; set; }
        public string MaBacLuong { get; set; }
        public decimal? TienLuongCb { get; set; }
        public string MaCvd104 { get; set; }
        public decimal? TienLuongCvd { get; set; }
        public DateTime? NgayNhanCbTuNgay { get; set; }
        public DateTime? NgayNhanCbDenNgay { get; set; }
        public DateTime? NgayNhanCvdTuNgay { get; set; }
        public DateTime? NgayNhanCvdDenNgay { get; set; }
        public int? SoThangTinhBaoLuuCb { get; set; }
        public int? SoThangTinhBaoLuuCvd { get; set; }
        public decimal? TienBaoLuuCb { get; set; }
        public decimal? TienBaoLuuCvd { get; set; }
        public decimal? TyLeHuongNN { get; set; }
        public int? NamBaoLuuCb { get; set; }
        public int? NamBaoLuuCvd { get; set; }
        public int? SoNguoiPhuThuoc { get; set; }
        public bool IsNangLuongCb { get; set; }
        public bool IsNangLuongCvd { get; set; }
        public decimal? TienNangLuongCb { get; set; }
        public decimal? TienNangLuongCvd { get; set; }
        public string MaSoDinhDanh { get; set; }
        public string SoChungMinhQuanDoi { get; set; }
        public string DienQuanLy { get; set; }
        public string QueQuan { get; set; }
        public string NoiDangKyKhaiSinh { get; set; }
        public string DanToc { get; set; }
        public string TonGiao { get; set; }
        public string NhomMau { get; set; }
        public int? LanNangLuongCb { get; set; }
        public int? LanNangLuongCvd { get; set; }
        public TlDmDonViNq104 TlDmDonViNq104 { get; set; }
        public decimal? TienLuongCbCu { get; set; }
        public DateTime? NgayBaoLuuCb { get; set; }
        public decimal? TienLuongCvdCu { get; set; }
        public DateTime? NgayBaoLuuCvd { get; set; }
        public decimal? TienLuongKeHoach { get; set; }
    }
}
