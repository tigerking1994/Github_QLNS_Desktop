using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VTS.QLNS.CTC.Core.Domain.Query
{
    public class TlDmCanBoNq104Query
    {
        public Guid Id { get; set; }
        public string Ma_CanBo { get; set; }
        public string Ten_CanBo { get; set; }
        public string Dia_Chi { get; set; }
        public string Ma_CV { get; set; }
        public string Ma_BL { get; set; }
        public string Ma_CB { get; set; }
        public string Ma_PBan { get; set; }
        public decimal? GTGC { get; set; }
        public string Dien_Thoai { get; set; }
        public string MaSo_VAT { get; set; }
        public string Ten_DonVi { get; set; }
        public string So_CMT { get; set; }
        public string NoiCap_CMT { get; set; }
        public DateTime? NgayCap_CMT { get; set; }
        public string So_TaiKhoan { get; set; }
        public string Ten_KhoBac { get; set; }
        public string MaSo_DV_SDNS { get; set; }
        public string Ma_DiaBan_HC { get; set; }
        public string MaTk_LQ { get; set; }
        public string Parent { get; set; }
        public string ParentCu { get; set; }
        public string Ma_KhoBac { get; set; }
        public bool? Splits { get; set; }
        public bool? Readonly { get; set; }
        public bool? Khong_Luong { get; set; }
        public string Ma_Hieu_CanBo { get; set; }
        public int? Thang { get; set; }
        public int? Nam { get; set; }
        public DateTime? Ngay_NN { get; set; }
        public DateTime? Ngay_XN { get; set; }
        public DateTime? Ngay_TN { get; set; }
        public int? Nam_TN { get; set; }
        public int? Thang_TNN { get; set; }
        public int? Nam_VK { get; set; }
        public bool? Is_Nam { get; set; }
        public string Ma_TangGiam { get; set; }
        public string So_SoLuong { get; set; }
        public DateTime? Ngay_NhanCB { get; set; }
        public int? ThoiHan_TangCb { get; set; }
        public string Cb_KeHoach { get; set; }
        public string Cccd { get; set; }
        public string NoiCongTac { get; set; }
        public DateTime? NgaySinh { get; set; }
        public bool? TM { get; set; }
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
        public Guid? IdLuongTran { get; set; }
        public string Ma_CbCu { get; set; }
        public string Nhom { get; set; }
        public int iTrangThai { get; set; }
        public DateTime? NgayTruyLinh { get; set; }
        public string Ma_TangGiamCu { get; set; }
        public bool? bNuocNgoai { get; set; }
        public bool? bKhongTinhNTN { get; set; }
        public string CapBac { get; set; }
        public string ChucVu { get; set; }
        public bool? BTinhBHXH { get; set; }
        [Column("ma_cvd104")]
        public string MaCvd104 { get; set; }
        [Column("loai_doi_tuong")]
        public string LoaiDoiTuong { get; set; }
        [Column("ma_cb104")]
        public string MaCb104 { get; set; }
        [Column("loai")]
        public string Loai { get; set; }
        [Column("nhom_chuyen_mon")]
        public string NhomChuyenMon { get; set; }
        [Column("Ma_BL")]
        public string MaBacLuong { get; set; }
        [Column("tien_nang_luong_cb")]
        public decimal? TienNangLuongCb { get; set; }
        [Column("tien_luong_cb")]
        public decimal? TienLuongCb { get; set; }
        [Column("tien_luong_cvd")]
        public decimal? TienLuongCvd { get; set; }
        [Column("tien_nang_luong_cvd")]
        public decimal? TienNangLuongCvd { get; set; }
    }
}
