using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;

namespace VTS.QLNS.CTC.App.Model.Mapper
{
    public class VdtThongTriMapper : Profile
    {
        public VdtThongTriMapper()
        {
            CreateMap<VdtThongTriModel, VdtThongTriQuery>();
            CreateMap<VdtThongTriQuery, VdtThongTriModel>();
            CreateMap<VdtThongTriChiTietQuery, VdtThongTriChiTietModel>();
            CreateMap<VdtThongTriChiTietModel, VdtThongTriChiTietQuery>();
            CreateMap<VdtCanCuThanhToanQuery, VdtCanCuThanhToanModel>();
            CreateMap<VdtCanCuThanhToanModel, VdtCanCuThanhToanQuery>();
            CreateMap<VdtThongTriQuyetToanQuery, VdtThongTriQuyetToanModel>();
            CreateMap<VdtThongTriQuyetToanModel, VdtThongTriQuyetToanQuery>();

            CreateMap<VdtThongTriModel, VdtThongTri>()
                .ForMember(n => n.BIsCanBoDuyet, m => m.MapFrom(n => n.bIsCanBoDuyet))
                .ForMember(n => n.BIsDuyet, m => m.MapFrom(n => n.bIsDuyet))
                .ForMember(n => n.BThanhToan, m => m.MapFrom(n => n.bThanhToan))
                .ForMember(n => n.DDateCreate, m => m.MapFrom(n => n.dDateCreate))
                .ForMember(n => n.DDateDelete, m => m.MapFrom(n => n.dDateDelete))
                .ForMember(n => n.DDateUpdate, m => m.MapFrom(n => n.dDateUpdate))
                .ForMember(n => n.DNgayThongTri, m => m.MapFrom(n => n.dNgayThongTri))
                .ForMember(n => n.IIdDonViId, m => m.MapFrom(n => n.iID_DonViID))
                .ForMember(n => n.IIdLoaiThongTriId, m => m.MapFrom(n => n.iID_LoaiThongTriID))
                .ForMember(n => n.iIDMaDonViID, m => m.MapFrom(n => n.iID_MaDonViID))
                .ForMember(n => n.IIdNhomQuanLyId, m => m.MapFrom(n => n.iID_NhomQuanLyID))
                .ForMember(n => n.INamThongTri, m => m.MapFrom(n => n.iNamThongTri))
                .ForMember(n => n.SMaLoaiCongTrinh, m => m.MapFrom(n => n.sMaLoaiCongTrinh))
                .ForMember(n => n.SMaNguonVon, m => m.MapFrom(n => n.sMaNguonVon))
                .ForMember(n => n.SMaThongTri, m => m.MapFrom(n => n.sMaThongTri))
                .ForMember(n => n.SNguoiLap, m => m.MapFrom(n => n.sNguoiLap))
                .ForMember(n => n.SThuTruongDonVi, m => m.MapFrom(n => n.sThuTruongDonVi))
                .ForMember(n => n.STruongPhong, m => m.MapFrom(n => n.sTruongPhong))
                .ForMember(n => n.SUserCreate, m => m.MapFrom(n => n.sUserCreate))
                .ForMember(n => n.SUserDelete, m => m.MapFrom(n => n.sUserDelete))
                .ForMember(n => n.SUserUpdate, m => m.MapFrom(n => n.sUserUpdate));

            CreateMap<VdtThongTri, VdtThongTriModel>()
                .ForMember(n => n.bIsCanBoDuyet, m => m.MapFrom(n => n.BIsCanBoDuyet))
                .ForMember(n => n.bIsDuyet, m => m.MapFrom(n => n.BIsDuyet))
                .ForMember(n => n.bThanhToan, m => m.MapFrom(n => n.BThanhToan))
                .ForMember(n => n.dDateCreate, m => m.MapFrom(n => n.DDateCreate))
                .ForMember(n => n.dDateDelete, m => m.MapFrom(n => n.DDateDelete))
                .ForMember(n => n.dDateUpdate, m => m.MapFrom(n => n.DDateUpdate))
                .ForMember(n => n.dNgayThongTri, m => m.MapFrom(n => n.DNgayThongTri))
                .ForMember(n => n.iID_DonViID, m => m.MapFrom(n => n.IIdDonViId))
                .ForMember(n => n.iID_LoaiThongTriID, m => m.MapFrom(n => n.IIdLoaiThongTriId))
                .ForMember(n => n.iID_MaDonViID, m => m.MapFrom(n => n.iIDMaDonViID))
                .ForMember(n => n.iID_NhomQuanLyID, m => m.MapFrom(n => n.IIdNhomQuanLyId))
                .ForMember(n => n.iNamThongTri, m => m.MapFrom(n => n.INamThongTri))
                .ForMember(n => n.sMaLoaiCongTrinh, m => m.MapFrom(n => n.SMaLoaiCongTrinh))
                .ForMember(n => n.sMaNguonVon, m => m.MapFrom(n => n.SMaNguonVon))
                .ForMember(n => n.sMaThongTri, m => m.MapFrom(n => n.SMaThongTri))
                .ForMember(n => n.sNguoiLap, m => m.MapFrom(n => n.SNguoiLap))
                .ForMember(n => n.sThuTruongDonVi, m => m.MapFrom(n => n.SThuTruongDonVi))
                .ForMember(n => n.sTruongPhong, m => m.MapFrom(n => n.STruongPhong))
                .ForMember(n => n.sUserCreate, m => m.MapFrom(n => n.SUserCreate))
                .ForMember(n => n.sUserDelete, m => m.MapFrom(n => n.SUserDelete))
                .ForMember(n => n.sUserUpdate, m => m.MapFrom(n => n.SUserUpdate));

            CreateMap<VdtThongTriChiTietModel, VdtThongTriChiTiet>();

            CreateMap<VdtThongTriChiTiet, VdtThongTriChiTietModel>();
        }
    }
}
