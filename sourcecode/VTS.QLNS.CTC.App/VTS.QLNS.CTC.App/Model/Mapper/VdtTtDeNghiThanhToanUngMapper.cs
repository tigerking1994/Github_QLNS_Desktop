using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;

namespace VTS.QLNS.CTC.App.Model.Mapper
{
    public class VdtTtDeNghiThanhToanUngMapper : Profile
    {
        public VdtTtDeNghiThanhToanUngMapper()
        {
            CreateMap<VdtTtDeNghiThanhToanUngModel, VdtTtDeNghiThanhToanUngQuery>();
            CreateMap<VdtTtDeNghiThanhToanUngQuery, VdtTtDeNghiThanhToanUngModel>();
            CreateMap<VdtTtDeNghiThanhToanUngChiTietQuery, VdtTtDeNghiThanhToanUngChiTietModel>();
            CreateMap<VdtTtDeNghiThanhToanUngChiTietModel, VdtTtDeNghiThanhToanUngChiTietQuery>();
            CreateMap<DuAnByDenghiThanhToanUngModel, VdtTtDeNghiThanhToanUngChiTietQuery>();
            CreateMap<VdtTtDeNghiThanhToanUngChiTietQuery, DuAnByDenghiThanhToanUngModel>();

            CreateMap<VdtTtDeNghiThanhToanUng, VdtTtDeNghiThanhToanUngModel>()
                .ForMember(n => n.dDateCreate, m => m.MapFrom(n => n.DDateCreate))
                .ForMember(n => n.dNgayDeNghi, m => m.MapFrom(n => n.DNgayDeNghi))
                .ForMember(n => n.fGiaTriTamUng, m => m.MapFrom(n => n.FGiaTriTamUng))
                .ForMember(n => n.fGiaTriThanhToan, m => m.MapFrom(n => n.FGiaTriThanhToan))
                .ForMember(n => n.fGiaTriThuHoi, m => m.MapFrom(n => n.FGiaTriThuHoi))
                .ForMember(n => n.fGiaTriThuHoiUngNgoaiChiTieu, m => m.MapFrom(n => n.FGiaTriThuHoiUngNgoaiChiTieu))
                .ForMember(n => n.iId_DonViQuanLyId, m => m.MapFrom(n => n.IIdDonViQuanLyId))
                .ForMember(n => n.iID_MaDonViQuanLy, m => m.MapFrom(n => n.IIDMaDonViQuanLy))
                .ForMember(n => n.lstDuAnId, m => m.MapFrom(n => n.lstDuAnId))
                .ForMember(n => n.sGhiChu, m => m.MapFrom(n => n.SGhiChu))
                .ForMember(n => n.sNguoiLap, m => m.MapFrom(n => n.SNguoiLap))
                .ForMember(n => n.sSoDeNghi, m => m.MapFrom(n => n.SSoDeNghi))
                .ForMember(n => n.sUserCreate, m => m.MapFrom(n => n.SUserCreate));

            CreateMap<VdtTtDeNghiThanhToanUngModel, VdtTtDeNghiThanhToanUng>()
                .ForMember(n => n.DDateCreate, m => m.MapFrom(n => n.dDateCreate))
                .ForMember(n => n.DNgayDeNghi, m => m.MapFrom(n => n.dNgayDeNghi))
                .ForMember(n => n.FGiaTriTamUng, m => m.MapFrom(n => n.fGiaTriTamUng))
                .ForMember(n => n.FGiaTriThanhToan, m => m.MapFrom(n => n.fGiaTriThanhToan))
                .ForMember(n => n.FGiaTriThuHoi, m => m.MapFrom(n => n.fGiaTriThuHoi))
                .ForMember(n => n.FGiaTriThuHoiUngNgoaiChiTieu, m => m.MapFrom(n => n.fGiaTriThuHoiUngNgoaiChiTieu))
                .ForMember(n => n.IIdDonViQuanLyId, m => m.MapFrom(n => n.iId_DonViQuanLyId))
                .ForMember(n => n.IIDMaDonViQuanLy, m => m.MapFrom(n => n.iID_MaDonViQuanLy))
                .ForMember(n => n.SGhiChu, m => m.MapFrom(n => n.sGhiChu))
                .ForMember(n => n.SNguoiLap, m => m.MapFrom(n => n.sNguoiLap))
                .ForMember(n => n.SSoDeNghi, m => m.MapFrom(n => n.sSoDeNghi))
                .ForMember(n => n.SUserCreate, m => m.MapFrom(n => n.sUserCreate));
        }
    }
}
