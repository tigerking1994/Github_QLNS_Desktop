using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;

namespace VTS.QLNS.CTC.App.Model.Mapper
{
    public class VdtKhvKeHoachVonUngMapper : Profile
    {
        public VdtKhvKeHoachVonUngMapper()
        {
            CreateMap<VdtKhvKeHoachVonUngQuery, VdtKhvKeHoachVonUngModel>();
            CreateMap<VdtKhvKeHoachVonUngModel, VdtKhvKeHoachVonUngQuery>();

            CreateMap<VdtKhvKeHoachVonUng, VdtKhvKeHoachVonUngModel>()
                .ForMember(n => n.dDateCreate, m => m.MapFrom(n => n.DDateCreate))
                .ForMember(n => n.dDateDelete, m => m.MapFrom(n => n.DDateDelete))
                .ForMember(n => n.dDateUpdate, m => m.MapFrom(n => n.DDateUpdate))
                .ForMember(n => n.dNgayQuyetDinh, m => m.MapFrom(n => n.DNgayQuyetDinh))
                .ForMember(n => n.fGiaTriUng, m => m.MapFrom(n => n.FGiaTriUng))
                .ForMember(n => n.fTiGia, m => m.MapFrom(n => n.FTiGia))
                .ForMember(n => n.fTiGiaDonVi, m => m.MapFrom(n => n.FTiGiaDonVi))
                .ForMember(n => n.iId_TienTeId, m => m.MapFrom(n => n.IIdTienTeId))
                .ForMember(n => n.iId_DonViQuanLyId, m => m.MapFrom(n => n.IIdDonViQuanLyId))
                .ForMember(n => n.iID_MaDonViQuanLy, m => m.MapFrom(n => n.IIDMaDonViQuanLy))
                .ForMember(n => n.iId_NguonVonId, m => m.MapFrom(n => n.IIdNguonVonId))
                .ForMember(n => n.iId_NhomQuanLyId, m => m.MapFrom(n => n.IIdNhomQuanLyId))
                .ForMember(n => n.iNamKeHoach, m => m.MapFrom(n => n.INamKeHoach))
                .ForMember(n => n.iID_LoaiNguonVonID, m => m.MapFrom(n => n.IIDLoaiNguonVonID))
                .ForMember(n => n.sKhoanNganSach, m => m.MapFrom(n => n.SKhoanNganSach))
                .ForMember(n => n.sLoaiNganSach, m => m.MapFrom(n => n.SLoaiNganSach))
                .ForMember(n => n.sSoQuyetDinh, m => m.MapFrom(n => n.SSoQuyetDinh))
                .ForMember(n => n.sUserCreate, m => m.MapFrom(n => n.SUserCreate))
                .ForMember(n => n.sUserDelete, m => m.MapFrom(n => n.SUserDelete))
                .ForMember(n => n.sUserUpdate, m => m.MapFrom(n => n.SUserUpdate))
                .ForMember(n => n.bActive, m => m.MapFrom(n => n.BActive))
                .ForMember(n => n.bIsGoc, m => m.MapFrom(n => n.BIsGoc))
                .ForMember(n => n.iId_ParentId, m => m.MapFrom(n => n.IIdParentId))
                .ForMember(n=> n.iID_KeHoachUngDeXuatID, m=>m.MapFrom(n=>n.IIDKeHoachUngDeXuatID));

            CreateMap<VdtKhvKeHoachVonUngModel, VdtKhvKeHoachVonUng>()
                .ForMember(n => n.DDateCreate, m => m.MapFrom(n => n.dDateCreate))
                .ForMember(n => n.DDateDelete, m => m.MapFrom(n => n.dDateDelete))
                .ForMember(n => n.DDateUpdate, m => m.MapFrom(n => n.dDateUpdate))
                .ForMember(n => n.DNgayQuyetDinh, m => m.MapFrom(n => n.dNgayQuyetDinh))
                .ForMember(n => n.FGiaTriUng, m => m.MapFrom(n => n.fGiaTriUng))
                .ForMember(n => n.FTiGia, m => m.MapFrom(n => n.fTiGia))
                .ForMember(n => n.FTiGiaDonVi, m => m.MapFrom(n => n.fTiGiaDonVi))
                .ForMember(n => n.IIdTienTeId, m => m.MapFrom(n => n.iId_TienTeId))
                .ForMember(n => n.IIdDonViQuanLyId, m => m.MapFrom(n => n.iId_DonViQuanLyId))
                .ForMember(n => n.IIDMaDonViQuanLy, m => m.MapFrom(n => n.iID_MaDonViQuanLy))
                .ForMember(n => n.IIdNguonVonId, m => m.MapFrom(n => n.iId_NguonVonId))
                .ForMember(n => n.IIdNhomQuanLyId, m => m.MapFrom(n => n.iId_NhomQuanLyId))
                .ForMember(n => n.INamKeHoach, m => m.MapFrom(n => n.iNamKeHoach))
                .ForMember(n => n.IIDLoaiNguonVonID, m => m.MapFrom(n => n.iID_LoaiNguonVonID))
                .ForMember(n => n.SKhoanNganSach, m => m.MapFrom(n => n.sKhoanNganSach))
                .ForMember(n => n.SLoaiNganSach, m => m.MapFrom(n => n.sLoaiNganSach))
                .ForMember(n => n.SSoQuyetDinh, m => m.MapFrom(n => n.sSoQuyetDinh))
                .ForMember(n => n.SUserCreate, m => m.MapFrom(n => n.sUserCreate))
                .ForMember(n => n.SUserDelete, m => m.MapFrom(n => n.sUserDelete))
                .ForMember(n => n.SUserUpdate, m => m.MapFrom(n => n.sUserUpdate))
                .ForMember(n => n.BActive, m => m.MapFrom(n => n.bActive))
                .ForMember(n => n.BIsGoc, m => m.MapFrom(n => n.bIsGoc))
                .ForMember(n => n.IIdParentId, m => m.MapFrom(n => n.iId_ParentId))
                .ForMember(n => n.IIDKeHoachUngDeXuatID, m => m.MapFrom(n => n.iID_KeHoachUngDeXuatID));


            CreateMap<VdtKhvKeHoachVonUngChiTiet, VdtKhvKeHoachVonUngChiTietModel>()
                .ForMember(n => n.fCapPhatBangLenhChi, m => m.MapFrom(n => n.FCapPhatBangLenhChi))
                .ForMember(n => n.fCapPhatTaiKhoBac, m => m.MapFrom(n => n.FCapPhatTaiKhoBac))
                .ForMember(n => n.fTiGia, m => m.MapFrom(n => n.FTiGia))
                .ForMember(n => n.fTiGiaDonVi, m => m.MapFrom(n => n.FTiGiaDonVi))
                .ForMember(n => n.iID_DonViTienTeID, m => m.MapFrom(n => n.IIdDonViTienTeId))
                .ForMember(n => n.iID_DuAnID, m => m.MapFrom(n => n.IIdDuAnId))
                .ForMember(n => n.iID_TienTeID, m => m.MapFrom(n => n.IIdTienTeId))
                .ForMember(n => n.sGhiChu, m => m.MapFrom(n => n.SGhiChu))
                .ForMember(n => n.iID_MucID, m => m.MapFrom(n => n.IIdMucId))
                .ForMember(n => n.iID_TieuMucID, m => m.MapFrom(n => n.IIdTieuMucId))
                .ForMember(n => n.iID_TietMucID, m => m.MapFrom(n => n.IIdTietMucId))
                .ForMember(n => n.iID_NganhID, m => m.MapFrom(n => n.IIdNganhId))
                .ForMember(n => n.sTrangThaiDuAnDangKy, m => m.MapFrom(n => n.STrangThaiDuAnDangKy));

            CreateMap<VdtKhvKeHoachVonUngChiTietModel, VdtKhvKeHoachVonUngChiTiet>()
                .ForMember(n => n.FCapPhatBangLenhChi, m => m.MapFrom(n => n.fCapPhatBangLenhChi))
                .ForMember(n => n.FCapPhatTaiKhoBac, m => m.MapFrom(n => n.fCapPhatTaiKhoBac))
                .ForMember(n => n.FTiGia, m => m.MapFrom(n => n.fTiGia))
                .ForMember(n => n.FTiGiaDonVi, m => m.MapFrom(n => n.fTiGiaDonVi))
                .ForMember(n => n.IIdDonViTienTeId, m => m.MapFrom(n => n.iID_DonViTienTeID))
                .ForMember(n => n.IIdDuAnId, m => m.MapFrom(n => n.iID_DuAnID))
                .ForMember(n => n.IIdTienTeId, m => m.MapFrom(n => n.iID_TienTeID))
                .ForMember(n => n.SGhiChu, m => m.MapFrom(n => n.sGhiChu))
                .ForMember(n => n.IIdMucId, m => m.MapFrom(n => n.iID_MucID))
                .ForMember(n => n.IIdTieuMucId, m => m.MapFrom(n => n.iID_TieuMucID))
                .ForMember(n => n.IIdTietMucId, m => m.MapFrom(n => n.iID_TietMucID))
                .ForMember(n => n.IIdNganhId, m => m.MapFrom(n => n.iID_NganhID))
                .ForMember(n => n.STrangThaiDuAnDangKy, m => m.MapFrom(n => n.sTrangThaiDuAnDangKy));

            CreateMap<VdtKhvKeHoachVonUngChiTietQuery, VdtKhvKeHoachVonUngChiTietModel>();

            CreateMap<VdtKhvKeHoachVonUngChiTietQuery, DuAnDenghiThanhToanModel>();
            CreateMap<DuAnDenghiThanhToanModel, VdtKhvKeHoachVonUngChiTietQuery>();
        }
    }
}
