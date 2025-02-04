using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;

namespace VTS.QLNS.CTC.App.Model.Mapper
{
    public class VdtKhvKeHoachVonUngDxMapper : Profile
    {
        public VdtKhvKeHoachVonUngDxMapper()
        {
            CreateMap<VdtKhvKeHoachVonUngDxQuery, VdtKhvKeHoachVonUngDxModel>()
                .ForMember(n => n.DNgayDeNghi, m => m.MapFrom(n => n.dNgayDeNghi))
                .ForMember(n => n.FGiaTriUng, m => m.MapFrom(n => n.fGiaTriUng))
                .ForMember(n => n.FTiGia, m => m.MapFrom(n => n.fTiGia))
                .ForMember(n => n.FTiGiaDonVi, m => m.MapFrom(n => n.fTiGiaDonVi))
                .ForMember(n => n.IIDDonViQuanLyID, m => m.MapFrom(n => n.iID_DonViQuanLyID))
                .ForMember(n => n.IIDDonViTienTeID, m => m.MapFrom(n => n.iID_DonViTienTeID))
                .ForMember(n => n.IIDMaDonViQuanLy, m => m.MapFrom(n => n.iID_MaDonViQuanLy))
                .ForMember(n => n.IIDNguonVonID, m => m.MapFrom(n => n.iID_NguonVonID))
                .ForMember(n => n.IIDNhomQuanLyID, m => m.MapFrom(n => n.iID_NhomQuanLyID))
                .ForMember(n => n.IIDTienTeID, m => m.MapFrom(n => n.iID_TienTeID))
                .ForMember(n => n.INamKeHoach, m => m.MapFrom(n => n.iNamKeHoach))
                .ForMember(n => n.SSoDeNghi, m => m.MapFrom(n => n.sSoDeNghi))
                .ForMember(n => n.STenDonViQuanLy, m => m.MapFrom(n => n.sTenDonViQuanLy))
                .ForMember(n => n.STenNguonVon, m => m.MapFrom(n => n.sTenNguonVon))
                .ForMember(n => n.SUserCreate, m => m.MapFrom(n => n.sUserCreate))
                .ForMember(n => n.STongHop, m => m.MapFrom(n => n.sTongHop))
                .ForMember(n => n.IIdParentId, m => m.MapFrom(n => n.iID_ParentId))
                .ForMember(n => n.BActive, m => m.MapFrom(n => n.bActive))
                .ForMember(n => n.BIsGoc, m => m.MapFrom(n => n.bIsGoc))
                .ForMember(n => n.SSoLanDieuChinh, m => m.MapFrom(n => n.sSoLanDieuChinh)); 

            CreateMap<VdtKhvKeHoachVonUngDxModel, VdtKhvKeHoachVonUngDxQuery>()
                .ForMember(n => n.dNgayDeNghi, m => m.MapFrom(n => n.DNgayDeNghi))
                .ForMember(n => n.fGiaTriUng, m => m.MapFrom(n => n.FGiaTriUng))
                .ForMember(n => n.fTiGia, m => m.MapFrom(n => n.FTiGia))
                .ForMember(n => n.fTiGiaDonVi, m => m.MapFrom(n => n.FTiGiaDonVi))
                .ForMember(n => n.iID_DonViQuanLyID, m => m.MapFrom(n => n.IIDDonViQuanLyID))
                .ForMember(n => n.iID_DonViTienTeID, m => m.MapFrom(n => n.IIDDonViTienTeID))
                .ForMember(n => n.iID_MaDonViQuanLy, m => m.MapFrom(n => n.IIDMaDonViQuanLy))
                .ForMember(n => n.iID_NguonVonID, m => m.MapFrom(n => n.IIDNguonVonID))
                .ForMember(n => n.iID_NhomQuanLyID, m => m.MapFrom(n => n.IIDNhomQuanLyID))
                .ForMember(n => n.iID_TienTeID, m => m.MapFrom(n => n.IIDTienTeID))
                .ForMember(n => n.iNamKeHoach, m => m.MapFrom(n => n.INamKeHoach))
                .ForMember(n => n.sSoDeNghi, m => m.MapFrom(n => n.SSoDeNghi))
                .ForMember(n => n.sTenNguonVon, m => m.MapFrom(n => n.STenNguonVon))
                .ForMember(n => n.sUserCreate, m => m.MapFrom(n => n.SUserCreate))
                .ForMember(n => n.sTongHop, m => m.MapFrom(n => n.STongHop))
                .ForMember(n => n.iID_ParentId, m => m.MapFrom(n => n.IIdParentId))
                .ForMember(n => n.bActive, m => m.MapFrom(n => n.BActive))
                .ForMember(n => n.bIsGoc, m => m.MapFrom(n => n.BIsGoc))
                .ForMember(n => n.sSoLanDieuChinh, m => m.MapFrom(n => n.SSoLanDieuChinh));

            CreateMap<VdtKhvKeHoachVonUngDxChiTietQuery, VdtKhvKeHoachVonUngDxChiTietModel>()
                .ForMember(n => n.FGiaTriDeNghi, m => m.MapFrom(n => n.fGiaTriDeNghi))
                .ForMember(n => n.FTiGia, m => m.MapFrom(n => n.fTiGia))
                .ForMember(n => n.FTiGiaDonVi, m => m.MapFrom(n => n.fTiGiaDonVi))
                .ForMember(n => n.FTongMucDauTuPheDuyet, m => m.MapFrom(n => n.fTongMucDauTuPheDuyet))
                .ForMember(n => n.IIDDonViTienTeID, m => m.MapFrom(n => n.iID_DonViTienTeID))
                .ForMember(n => n.IIDDuAnID, m => m.MapFrom(n => n.iID_DuAnID))
                .ForMember(n => n.IIDTienTeID, m => m.MapFrom(n => n.iID_DonViTienTeID))
                .ForMember(n => n.SGhiChu, m => m.MapFrom(n => n.sGhiChu))
                .ForMember(n => n.SMaDuAn, m => m.MapFrom(n => n.sMaDuAn))
                .ForMember(n => n.STenDuAn, m => m.MapFrom(n => n.sTenDuAn))
                .ForMember(n => n.STrangThaiDuAnDangKy, m => m.MapFrom(n => n.sTrangThaiDuAnDangKy))
                .ForMember(n => n.STenDonVi, m => m.MapFrom(n => n.sTenDonVi))
                .ForMember(n => n.IIDDonViQuanLyID, m => m.MapFrom(n => n.iID_DonViQuanLyID))
                .ForMember(n => n.IIDMaDonViQuanLy, m => m.MapFrom(n => n.iID_MaDonViQuanLy));

            CreateMap<VdtKhvKeHoachVonUngDxChiTietModel, VdtKhvKeHoachVonUngDxChiTietQuery>()
                .ForMember(n => n.fGiaTriDeNghi, m => m.MapFrom(n => n.FGiaTriDeNghi))
                .ForMember(n => n.fTiGia, m => m.MapFrom(n => n.FTiGia))
                .ForMember(n => n.fTiGiaDonVi, m => m.MapFrom(n => n.FTiGiaDonVi))
                .ForMember(n => n.fTongMucDauTuPheDuyet, m => m.MapFrom(n => n.FTongMucDauTuPheDuyet))
                .ForMember(n => n.iID_DonViTienTeID, m => m.MapFrom(n => n.IIDDonViTienTeID))
                .ForMember(n => n.iID_DuAnID, m => m.MapFrom(n => n.IIDDuAnID))
                .ForMember(n => n.iID_TienTeID, m => m.MapFrom(n => n.IIDDonViTienTeID))
                .ForMember(n => n.sGhiChu, m => m.MapFrom(n => n.SGhiChu))
                .ForMember(n => n.sMaDuAn, m => m.MapFrom(n => n.SMaDuAn))
                .ForMember(n => n.sTenDuAn, m => m.MapFrom(n => n.STenDuAn))
                .ForMember(n => n.sTrangThaiDuAnDangKy, m => m.MapFrom(n => n.STrangThaiDuAnDangKy))
                .ForMember(n => n.sTenDonVi, m => m.MapFrom(n => n.STenDonVi))
                .ForMember(n => n.iID_DonViQuanLyID, m => m.MapFrom(n => n.IIDDonViQuanLyID))
                .ForMember(n => n.iID_MaDonViQuanLy, m => m.MapFrom(n => n.IIDMaDonViQuanLy));

            CreateMap<VdtKhvKeHoachVonUngDxModel, VdtKhvKeHoachVonUngDx>()
                .ForMember(n => n.DNgayDeNghi, m => m.MapFrom(n => n.DNgayDeNghi))
                .ForMember(n => n.FGiaTriUng, m => m.MapFrom(n => n.FGiaTriUng))
                .ForMember(n => n.FTiGia, m => m.MapFrom(n => n.FTiGia))
                .ForMember(n => n.FTiGiaDonVi, m => m.MapFrom(n => n.FTiGiaDonVi))
                .ForMember(n => n.IIdDonViQuanLyId, m => m.MapFrom(n => n.IIDDonViQuanLyID))
                .ForMember(n => n.IIdDonViTienTeId, m => m.MapFrom(n => n.IIDDonViTienTeID))
                .ForMember(n => n.IIdMaDonViQuanLy, m => m.MapFrom(n => n.IIDMaDonViQuanLy))
                .ForMember(n => n.IIdNguonVonId, m => m.MapFrom(n => n.IIDNguonVonID))
                .ForMember(n => n.IIdNhomQuanLyId, m => m.MapFrom(n => n.IIDNhomQuanLyID))
                .ForMember(n => n.IIdTienTeId, m => m.MapFrom(n => n.IIDTienTeID))
                .ForMember(n => n.INamKeHoach, m => m.MapFrom(n => n.INamKeHoach))
                .ForMember(n => n.SSoDeNghi, m => m.MapFrom(n => n.SSoDeNghi))
                .ForMember(n => n.STongHop, m => m.MapFrom(n => n.STongHop))
                .ForMember(n => n.IIdParentId, m => m.MapFrom(n => n.IIdParentId))
                .ForMember(n => n.BActive, m => m.MapFrom(n => n.BActive))
                .ForMember(n => n.BIsGoc, m => m.MapFrom(n => n.BIsGoc));

            CreateMap<VdtKhvKeHoachVonUngDx, VdtKhvKeHoachVonUngDxModel>()
                .ForMember(n => n.DNgayDeNghi, m => m.MapFrom(n => n.DNgayDeNghi))
                .ForMember(n => n.FGiaTriUng, m => m.MapFrom(n => n.FGiaTriUng))
                .ForMember(n => n.FTiGia, m => m.MapFrom(n => n.FTiGia))
                .ForMember(n => n.FTiGiaDonVi, m => m.MapFrom(n => n.FTiGiaDonVi))
                .ForMember(n => n.IIDDonViQuanLyID, m => m.MapFrom(n => n.IIdDonViQuanLyId))
                .ForMember(n => n.IIDDonViTienTeID, m => m.MapFrom(n => n.IIdDonViTienTeId))
                .ForMember(n => n.IIDMaDonViQuanLy, m => m.MapFrom(n => n.IIdMaDonViQuanLy))
                .ForMember(n => n.IIDNguonVonID, m => m.MapFrom(n => n.IIdNguonVonId))
                .ForMember(n => n.IIDNhomQuanLyID, m => m.MapFrom(n => n.IIdNhomQuanLyId))
                .ForMember(n => n.INamKeHoach, m => m.MapFrom(n => n.INamKeHoach))
                .ForMember(n => n.SSoDeNghi, m => m.MapFrom(n => n.SSoDeNghi))
                .ForMember(n => n.STongHop, m => m.MapFrom(n => n.STongHop))
                .ForMember(n => n.IIdParentId, m => m.MapFrom(n => n.IIdParentId))
                .ForMember(n => n.BActive, m => m.MapFrom(n => n.BActive))
                .ForMember(n => n.BIsGoc, m => m.MapFrom(n => n.BIsGoc));

            CreateMap<VdtKhvKeHoachVonUngDxChiTietQuery, DuAnDenghiThanhToanModel>();
            CreateMap<DuAnDenghiThanhToanModel, VdtKhvKeHoachVonUngDxChiTietQuery>();
        }
    }
}
