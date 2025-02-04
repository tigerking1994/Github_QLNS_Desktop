using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VTS.QLNS.CTC.Core.Domain
{
    public class TlDsCBNHKeHoach : EntityBase
    {
        public string MaCanBo { get; set; }
        public string TenCanBo { get; set; }
        //public string DiaChi { get; set; }
        public string MaCv { get; set; }
        //public string MaBl { get; set; }
        public string MaCb { get; set; }
        public string MaPban { get; set; }
        //public decimal? Gtgc { get; set; }
        //public string DienThoai { get; set; }
        //public string MaSoVat { get; set; }
        public string TenDonVi { get; set; }
        //public string SoCmt { get; set; }
        //public string NoiCapCmt { get; set; }
        //public DateTime? NgayCapCmt { get; set; }
        //public string SoTaiKhoan { get; set; }
        //public string TenKhoBac { get; set; }
        //public string MaSoDvSdns { get; set; }
        //public string MaDiaBanHc { get; set; }
        //public string MaTkLq { get; set; }
        public string Parent { get; set; }
        //public string MaKhoBac { get; set; }
        //public bool? Splits { get; set; }
        public bool? Readonly { get; set; }
        //public bool? KhongLuong { get; set; }
        public string MaHieuCanBo { get; set; }
        public int? Thang { get; set; }
        public int? Nam { get; set; }
        public DateTime? NgayNn { get; set; }
        public DateTime? NgayXn { get; set; }
        public DateTime? NgayTn { get; set; }
        public int? NamTn { get; set; }
        public int? ThangTnn { get; set; }
        //public int? NamVk { get; set; }
        public bool? IsNam { get; set; }
        public string MaTangGiam { get; set; }
        //public string SoSoLuong { get; set; }
        //public DateTime? NgayNhanCb { get; set; }
        //public int? ThoiHanTangCb { get; set; }
        //public string CbKeHoach { get; set; }
        //public string Cccd { get; set; }
        //public string NoiCongTac { get; set; }
        public DateTime? NgaySinh { get; set; }
        //public bool? Tm { get; set; }
        //public bool? BHTN { get; set; }
        public bool? PCCV { get; set; }
        public bool? IsDelete { get; set; }
        public DateTime? DateCreated { get; set; }
        public DateTime? DateModified { get; set; }
        public string UserCreator { get; set; }
        public string UserModifier { get; set; }
        //public decimal? HsLuongTran { get; set; }
        //public decimal? HsLuongKeHoach { get; set; }
        public decimal? HeSoLuong { get; set; }
        //public decimal? SoNguoiPhuThuoc { get; set; }
        public TlDmCapBac TlDmCapBac { get; set; }
        public TlDmChucVu TlDmChucVu { get; set; }
        //public string Nhom { get; set; }
        public string Loai { get; set; }
        //public DateTime? NgayTruyLinh { get; set; }
        //public bool? BNuocNgoai { get; set; }
        //public bool? BKhongTinhNTN { get; set; }
    }
}
