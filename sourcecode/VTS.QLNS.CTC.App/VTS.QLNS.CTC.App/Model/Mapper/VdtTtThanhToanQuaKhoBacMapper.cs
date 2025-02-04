using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;

namespace VTS.QLNS.CTC.App.Model.Mapper
{
    public class VdtTtThanhToanQuaKhoBacMapper : Profile
    {
        public VdtTtThanhToanQuaKhoBacMapper()
        {
            CreateMap<ThanhToanQuaKhoBacModel, VdtTtThanhToanQuaKhoBacQuery>();
            CreateMap<VdtTtThanhToanQuaKhoBacQuery, ThanhToanQuaKhoBacModel>();

            CreateMap<ThanhToanQuaKhoBacChiTietModel, ThanhToanQuaKhoBacChiTietQuery>();
            CreateMap<ThanhToanQuaKhoBacChiTietQuery, ThanhToanQuaKhoBacChiTietModel>();

            CreateMap<VdtTtThanhToanQuaKhoBac, ThanhToanQuaKhoBacModel>()
                .ForMember(n => n.dDateCreate, m => m.MapFrom(n => n.DDateCreate))
                .ForMember(n => n.dDateDelete, m => m.MapFrom(n => n.DDateDelete))
                .ForMember(n => n.dDateUpdate, m => m.MapFrom(n => n.DDateUpdate))
                .ForMember(n => n.dNgayThanhToan, m => m.MapFrom(n => n.DNgayThanhToan))
                .ForMember(n => n.fGiaTriTamUng, m => m.MapFrom(n => n.FGiaTriTamUng))
                .ForMember(n => n.fGiaTriThanhToan, m => m.MapFrom(n => n.FGiaTriThanhToan))
                .ForMember(n => n.fTiGia, m => m.MapFrom(n => n.FTiGia))
                .ForMember(n => n.fTiGiaDonVi, m => m.MapFrom(n => n.FTiGiaDonVi))
                .ForMember(n => n.iID_DonViNhanThanhToanID, m => m.MapFrom(n => n.IIdDonViNhanThanhToanId))
                .ForMember(n => n.iID_DonViQuanLyID, m => m.MapFrom(n => n.IIdDonViQuanLyId))
                .ForMember(n => n.iID_DonViTienTeID, m => m.MapFrom(n => n.IIdDonViTienTeId))
                .ForMember(n => n.iID_KhoanNganSach, m => m.MapFrom(n => n.IIdKhoanNganSach))
                .ForMember(n => n.iID_LoaiNganSach, m => m.MapFrom(n => n.IIdLoaiNganSach))
                .ForMember(n => n.iID_LoaiNguonVonID, m => m.MapFrom(n => n.IIdLoaiNguonVonId))
                .ForMember(n => n.iId_MaDonViNhanThanhToanID, m => m.MapFrom(n => n.IIdMaDonViNhanThanhToanID))
                .ForMember(n => n.iId_MaDonViQuanLyID, m => m.MapFrom(n => n.IIdMaDonViQuanLyID))
                .ForMember(n => n.iID_NguonVonID, m => m.MapFrom(n => n.IIdNguonVonId))
                .ForMember(n => n.iID_NhomQuanLyID, m => m.MapFrom(n => n.IIdNhomQuanLyId))
                .ForMember(n => n.iID_TienTeID, m => m.MapFrom(n => n.IIdTienTeId))
                .ForMember(n => n.iNamKeHoach, m => m.MapFrom(n => n.INamKeHoach))
                .ForMember(n => n.sGhiChu, m => m.MapFrom(n => n.SGhiChu))
                .ForMember(n => n.sNguoiLap, m => m.MapFrom(n => n.SNguoiLap))
                .ForMember(n => n.sSoThanhToan, m => m.MapFrom(n => n.SSoThanhToan))
                .ForMember(n => n.sUserCreate, m => m.MapFrom(n => n.SUserCreate))
                .ForMember(n => n.sUserDelete, m => m.MapFrom(n => n.SUserDelete))
                .ForMember(n => n.sUserUpdate, m => m.MapFrom(n => n.SUserUpdate));

            CreateMap<ThanhToanQuaKhoBacModel, VdtTtThanhToanQuaKhoBac>()
                .ForMember(n => n.DDateCreate, m => m.MapFrom(n => n.dDateCreate))
                .ForMember(n => n.DDateDelete, m => m.MapFrom(n => n.dDateDelete))
                .ForMember(n => n.DDateUpdate, m => m.MapFrom(n => n.dDateUpdate))
                .ForMember(n => n.DNgayThanhToan, m => m.MapFrom(n => n.dNgayThanhToan))
                .ForMember(n => n.FGiaTriTamUng, m => m.MapFrom(n => n.fGiaTriTamUng))
                .ForMember(n => n.FGiaTriThanhToan, m => m.MapFrom(n => n.fGiaTriThanhToan))
                .ForMember(n => n.FTiGia, m => m.MapFrom(n => n.fTiGia))
                .ForMember(n => n.FTiGiaDonVi, m => m.MapFrom(n => n.fTiGiaDonVi))
                .ForMember(n => n.IIdDonViNhanThanhToanId, m => m.MapFrom(n => n.iID_DonViNhanThanhToanID))
                .ForMember(n => n.IIdDonViQuanLyId, m => m.MapFrom(n => n.iID_DonViQuanLyID))
                .ForMember(n => n.IIdDonViTienTeId, m => m.MapFrom(n => n.iID_DonViTienTeID))
                .ForMember(n => n.IIdKhoanNganSach, m => m.MapFrom(n => n.iID_KhoanNganSach))
                .ForMember(n => n.IIdLoaiNganSach, m => m.MapFrom(n => n.iID_LoaiNganSach))
                .ForMember(n => n.IIdLoaiNguonVonId, m => m.MapFrom(n => n.iID_LoaiNguonVonID))
                .ForMember(n => n.IIdMaDonViNhanThanhToanID, m => m.MapFrom(n => n.iId_MaDonViNhanThanhToanID))
                .ForMember(n => n.IIdMaDonViQuanLyID, m => m.MapFrom(n => n.iId_MaDonViQuanLyID))
                .ForMember(n => n.IIdNguonVonId, m => m.MapFrom(n => n.iID_NguonVonID))
                .ForMember(n => n.IIdNhomQuanLyId, m => m.MapFrom(n => n.iID_NhomQuanLyID))
                .ForMember(n => n.IIdTienTeId, m => m.MapFrom(n => n.iID_TienTeID))
                .ForMember(n => n.INamKeHoach, m => m.MapFrom(n => n.iNamKeHoach))
                .ForMember(n => n.SGhiChu, m => m.MapFrom(n => n.sGhiChu))
                .ForMember(n => n.SNguoiLap, m => m.MapFrom(n => n.sNguoiLap))
                .ForMember(n => n.SSoThanhToan, m => m.MapFrom(n => n.sSoThanhToan))
                .ForMember(n => n.SUserCreate, m => m.MapFrom(n => n.sUserCreate))
                .ForMember(n => n.SUserDelete, m => m.MapFrom(n => n.sUserDelete))
                .ForMember(n => n.SUserUpdate, m => m.MapFrom(n => n.sUserUpdate));
        }
    }
}
