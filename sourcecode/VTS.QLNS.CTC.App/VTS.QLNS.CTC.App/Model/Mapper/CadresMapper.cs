using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;

namespace VTS.QLNS.CTC.App.Model.Mapper
{
    public class CadresMapper : Profile
    {
        public CadresMapper()
        {
            CreateMap<CadresModel, TlDmCanBo>();
            CreateMap<TlDmCanBo, CadresModel>()
                .ForMember(entity => entity.NgayNnString, model => model.MapFrom(item => item.NgayNn.HasValue ? item.NgayNn.Value.ToString("dd/MM/yyyy") : string.Empty))
                .ForMember(entity => entity.NgayXnString, model => model.MapFrom(item => item.NgayXn.HasValue ? item.NgayXn.Value.ToString("dd/MM/yyyy") : string.Empty))
                .ForMember(entity => entity.NgayTnString, model => model.MapFrom(item => item.NgayTn.HasValue ? item.NgayTn.Value.ToString("dd/MM/yyyy") : string.Empty))
                .ForMember(entity => entity.CapBac, model => model.MapFrom(item => item.TlDmCapBac != null ? item.TlDmCapBac.Note : string.Empty))
                .ForMember(entity => entity.ChucVu, model => model.MapFrom(item => item.TlDmChucVu != null ? item.TlDmChucVu.TenCv : string.Empty))
                .ForMember(entity => entity.TlCanBoPhuCaps, model => model.MapFrom(item => item.TlCanBoPhuCaps.Select(x => new TlCanBoPhuCapModel
                {
                    Id = x.Id,
                    MaCbo = x.MaCbo,
                    MaPhuCap = x.MaPhuCap,
                    GiaTri = x.GiaTri,
                    HuongPcSn = x.HuongPcSn,
                    DateEnd = x.DateEnd,
                    DateStart = x.DateStart,
                    Flag = x.Flag,
                    ISoThangHuong = x.ISoThangHuong
                })));
            CreateMap<CadresModel, TlGtTaiChinhModel>();
            CreateMap<TlGtTaiChinhModel, CadresModel>();
            CreateMap<TlDmCanBoKeHoachModel, CadresModel>();
            CreateMap<CadresModel, TlDmCanBoKeHoachModel>();
            CreateMap<TlDsCBNHKeHoachModel, CadresModel>();
            CreateMap<CadresModel, TlDsCBNHKeHoachModel>();
            CreateMap<TlDmCanBoQuery, CadresModel>()
                .ForMember(e => e.MaCanBo, m => m.MapFrom(i => i.Ma_CanBo))
                .ForMember(e => e.TenCanBo, m => m.MapFrom(i => i.Ten_CanBo))
                .ForMember(e => e.DiaChi, m => m.MapFrom(i => i.Dia_Chi))
                .ForMember(e => e.MaCv, m => m.MapFrom(i => i.Ma_CV))
                .ForMember(e => e.MaCb, m => m.MapFrom(i => i.Ma_CB))
                .ForMember(e => e.DienThoai, m => m.MapFrom(i => i.Dien_Thoai))
                .ForMember(e => e.TenDonVi, m => m.MapFrom(i => i.Ten_DonVi))
                .ForMember(e => e.SoCmt, m => m.MapFrom(i => i.So_CMT))
                .ForMember(e => e.SoTaiKhoan, m => m.MapFrom(i => i.So_TaiKhoan))
                .ForMember(e => e.KhongLuong, m => m.MapFrom(i => i.Khong_Luong))
                .ForMember(e => e.MaHieuCanBo, m => m.MapFrom(i => i.Ma_Hieu_CanBo))
                .ForMember(e => e.NgayNn, m => m.MapFrom(i => i.Ngay_NN))
                .ForMember(e => e.NgayXn, m => m.MapFrom(i => i.Ngay_XN))
                .ForMember(e => e.NgayTn, m => m.MapFrom(i => i.Ngay_TN))
                .ForMember(e => e.NamTn, m => m.MapFrom(i => i.Nam_TN))
                .ForMember(e => e.ThangTnn, m => m.MapFrom(i => i.Thang_TNN))
                .ForMember(e => e.IsNam, m => m.MapFrom(i => i.Is_Nam))
                .ForMember(e => e.MaTangGiam, m => m.MapFrom(i => i.Ma_TangGiam))
                .ForMember(e => e.SoSoLuong, m => m.MapFrom(i => i.So_SoLuong))
                .ForMember(e => e.NgayNhanCb, m => m.MapFrom(i => i.Ngay_NhanCB))
                .ForMember(e => e.NgayNhanCb, m => m.MapFrom(i => i.Ngay_NhanCB))
                .ForMember(e => e.ThoiHanTangCb, m => m.MapFrom(i => i.ThoiHan_TangCb))
                .ForMember(e => e.CbKeHoach, m => m.MapFrom(i => i.Cb_KeHoach))
                .ForMember(e => e.Tm, m => m.MapFrom(i => i.TM))
                .ForMember(e => e.MaCbCu, m => m.MapFrom(i => i.Ma_CbCu))
                .ForMember(e => e.ITrangThai, m => m.MapFrom(i => i.iTrangThai))
                .ForMember(e => e.BNuocNgoai, m => m.MapFrom(i => i.bNuocNgoai))
                .ForMember(e => e.MaTangGiamCu, m => m.MapFrom(i => i.Ma_TangGiamCu))
                .ForMember(e => e.TenKhoBac, m => m.MapFrom(i => i.Ten_KhoBac))
                .ForMember(e => e.bKhongTinhNTN, m => m.MapFrom(i => i.bKhongTinhNTN))
                .ForMember(e => e.BTinhBHXH, m => m.MapFrom(i => i.BTinhBHXH));
            CreateMap<TlDmCanBoQuery, TlDmCanBo>()
                .ForMember(e => e.MaCanBo, m => m.MapFrom(i => i.Ma_CanBo))
                .ForMember(e => e.TenCanBo, m => m.MapFrom(i => i.Ten_CanBo))
                .ForMember(e => e.DiaChi, m => m.MapFrom(i => i.Dia_Chi))
                .ForMember(e => e.MaCv, m => m.MapFrom(i => i.Ma_CV))
                .ForMember(e => e.MaCb, m => m.MapFrom(i => i.Ma_CB))
                .ForMember(e => e.DienThoai, m => m.MapFrom(i => i.Dien_Thoai))
                .ForMember(e => e.TenDonVi, m => m.MapFrom(i => i.Ten_DonVi))
                .ForMember(e => e.SoCmt, m => m.MapFrom(i => i.So_CMT))
                .ForMember(e => e.SoTaiKhoan, m => m.MapFrom(i => i.So_TaiKhoan))
                .ForMember(e => e.KhongLuong, m => m.MapFrom(i => i.Khong_Luong))
                .ForMember(e => e.MaHieuCanBo, m => m.MapFrom(i => i.Ma_Hieu_CanBo))
                .ForMember(e => e.NgayNn, m => m.MapFrom(i => i.Ngay_NN))
                .ForMember(e => e.NgayXn, m => m.MapFrom(i => i.Ngay_XN))
                .ForMember(e => e.NgayTn, m => m.MapFrom(i => i.Ngay_TN))
                .ForMember(e => e.NamTn, m => m.MapFrom(i => i.Nam_TN))
                .ForMember(e => e.ThangTnn, m => m.MapFrom(i => i.Thang_TNN))
                .ForMember(e => e.IsNam, m => m.MapFrom(i => i.Is_Nam))
                .ForMember(e => e.MaTangGiam, m => m.MapFrom(i => i.Ma_TangGiam))
                .ForMember(e => e.SoSoLuong, m => m.MapFrom(i => i.So_SoLuong))
                .ForMember(e => e.NgayNhanCb, m => m.MapFrom(i => i.Ngay_NhanCB))
                .ForMember(e => e.NgayNhanCb, m => m.MapFrom(i => i.Ngay_NhanCB))
                .ForMember(e => e.ThoiHanTangCb, m => m.MapFrom(i => i.ThoiHan_TangCb))
                .ForMember(e => e.CbKeHoach, m => m.MapFrom(i => i.Cb_KeHoach))
                .ForMember(e => e.Tm, m => m.MapFrom(i => i.TM))
                .ForMember(e => e.MaCbCu, m => m.MapFrom(i => i.Ma_CbCu))
                .ForMember(e => e.ITrangThai, m => m.MapFrom(i => i.iTrangThai))
                .ForMember(e => e.BNuocNgoai, m => m.MapFrom(i => i.bNuocNgoai))
                .ForMember(e => e.MaTangGiamCu, m => m.MapFrom(i => i.Ma_TangGiamCu))
                .ForMember(e => e.TenKhoBac, m => m.MapFrom(i => i.Ten_KhoBac))
                .ForMember(e => e.bKhongTinhNTN, m => m.MapFrom(i => i.bKhongTinhNTN))
                .ForMember(e => e.BTinhBHXH, m => m.MapFrom(i => i.BTinhBHXH));
            CreateMap<TlDanhSachCanBoQuery, CadresModel>()
                .ForMember(e => e.MaCanBo, m => m.MapFrom(i => i.Ma_CanBo))
                .ForMember(e => e.TenCanBo, m => m.MapFrom(i => i.Ten_CanBo))
                .ForMember(e => e.DiaChi, m => m.MapFrom(i => i.Dia_Chi))
                .ForMember(e => e.MaCv, m => m.MapFrom(i => i.Ma_CV))
                .ForMember(e => e.MaCb, m => m.MapFrom(i => i.Ma_CB))
                .ForMember(e => e.DienThoai, m => m.MapFrom(i => i.Dien_Thoai))
                .ForMember(e => e.TenDonVi, m => m.MapFrom(i => i.Ten_DonVi))
                .ForMember(e => e.SoCmt, m => m.MapFrom(i => i.So_CMT))
                .ForMember(e => e.SoTaiKhoan, m => m.MapFrom(i => i.So_TaiKhoan))
                .ForMember(e => e.KhongLuong, m => m.MapFrom(i => i.Khong_Luong))
                .ForMember(e => e.MaHieuCanBo, m => m.MapFrom(i => i.Ma_Hieu_CanBo))
                .ForMember(e => e.NgayNn, m => m.MapFrom(i => i.Ngay_NN))
                .ForMember(e => e.NgayXn, m => m.MapFrom(i => i.Ngay_XN))
                .ForMember(e => e.NgayTn, m => m.MapFrom(i => i.Ngay_TN))
                .ForMember(e => e.NamTn, m => m.MapFrom(i => i.Nam_TN))
                .ForMember(e => e.ThangTnn, m => m.MapFrom(i => i.Thang_TNN))
                .ForMember(e => e.IsNam, m => m.MapFrom(i => i.Is_Nam))
                .ForMember(e => e.MaTangGiam, m => m.MapFrom(i => i.Ma_TangGiam))
                .ForMember(e => e.SoSoLuong, m => m.MapFrom(i => i.So_SoLuong))
                .ForMember(e => e.NgayNhanCb, m => m.MapFrom(i => i.Ngay_NhanCB))
                .ForMember(e => e.NgayNhanCb, m => m.MapFrom(i => i.Ngay_NhanCB))
                .ForMember(e => e.ThoiHanTangCb, m => m.MapFrom(i => i.ThoiHan_TangCb))
                .ForMember(e => e.CbKeHoach, m => m.MapFrom(i => i.Cb_KeHoach))
                .ForMember(e => e.Tm, m => m.MapFrom(i => i.TM))
                .ForMember(e => e.MaCbCu, m => m.MapFrom(i => i.Ma_CbCu))
                .ForMember(e => e.ITrangThai, m => m.MapFrom(i => i.iTrangThai))
                .ForMember(e => e.BNuocNgoai, m => m.MapFrom(i => i.bNuocNgoai))
                .ForMember(e => e.MaTangGiamCu, m => m.MapFrom(i => i.Ma_TangGiamCu))
                .ForMember(e => e.TenKhoBac, m => m.MapFrom(i => i.Ten_KhoBac))
                .ForMember(e => e.bKhongTinhNTN, m => m.MapFrom(i => i.bKhongTinhNTN))
                .ForMember(e => e.BTinhBHXH, m => m.MapFrom(i => i.BTinhBHXH));
            CreateMap<TlCanBoThueTncnQuery, IncomeTaxModel>();
            CreateMap<TlCanBoRaQuanQuery, TlCanBoRaQuanModel>();
        }
    }
}
