using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using VTS.QLNS.CTC.Core.Domain.Query;

namespace VTS.QLNS.CTC.App.Model.Mapper
{
    public class VdtTtPheDuyetThanhToanMappper : Profile
    {
        public VdtTtPheDuyetThanhToanMappper()
        {
            CreateMap<VdtTtPheDuyetThanhToanQuery, VdtTtPheDuyetThanhToanModel>();
            CreateMap<VdtTtPheDuyetThanhToanModel, VdtTtPheDuyetThanhToanQuery>();

            CreateMap<VdtTtPheDuyetThanhToanChiTietQuery, VdtTtPheDuyetThanhToanChiTietModel>()
                .ForMember(n => n.IID_KeHoachVonID, m => m.MapFrom(n => n.iID_KeHoachVonID))
                .ForMember(n => n.ILoai, m => m.MapFrom(n => n.iLoai))
                .ForMember(n => n.ILoaiNamKeHoach, m => m.MapFrom(n => n.iLoaiNamKeHoach))
                .ForMember(n => n.ILoaiKeHoachVon, m => m.MapFrom(n => n.iLoaiKeHoachVon))
                .ForMember(n => n.SGhiChu, m => m.MapFrom(n => n.sGhiChu));
            CreateMap<VdtTtPheDuyetThanhToanChiTietModel, VdtTtPheDuyetThanhToanChiTietQuery>()
                .ForMember(n => n.iID_KeHoachVonID, m => m.MapFrom(n => n.IID_KeHoachVonID))
                .ForMember(n => n.iLoai, m => m.MapFrom(n => n.ILoai))
                .ForMember(n => n.iLoaiNamKeHoach, m => m.MapFrom(n => n.ILoaiNamKeHoach))
                .ForMember(n => n.iLoaiKeHoachVon, m => m.MapFrom(n => n.ILoaiKeHoachVon))
                .ForMember(n => n.sGhiChu, m => m.MapFrom(n => n.SGhiChu));

            CreateMap<PheDuyetThanhToanChiTietQuery, PheDuyetThanhToanChiTietModel>();
            CreateMap<PheDuyetThanhToanChiTietModel, PheDuyetThanhToanChiTietQuery>();

            CreateMap<KeHoachVonQuery, KeHoachVonModel>()
                .ForMember(n => n.DNgayQuyetDinh, m => m.MapFrom(n => n.dNgayQuyetDinh))
                .ForMember(n => n.IIDDonViQuanLyID, m => m.MapFrom(n => n.iID_DonViQuanLyID))
                .ForMember(n => n.IIDMaDonViQuanLy, m => m.MapFrom(n => n.iID_MaDonViQuanLy))
                .ForMember(n => n.IIDNguonVonID, m => m.MapFrom(n => n.iID_NguonVonID))
                .ForMember(n => n.INamKeHoach, m => m.MapFrom(n => n.iNamKeHoach))
                .ForMember(n => n.SMaNguonCha, m => m.MapFrom(n => n.sMaNguonCha))
                .ForMember(n => n.SSoQuyetDinh, m => m.MapFrom(n => n.sSoQuyetDinh));
            CreateMap<KeHoachVonModel, KeHoachVonQuery>()
                .ForMember(n => n.dNgayQuyetDinh, m => m.MapFrom(n => n.DNgayQuyetDinh))
                .ForMember(n => n.iID_DonViQuanLyID, m => m.MapFrom(n => n.IIDDonViQuanLyID))
                .ForMember(n => n.iID_MaDonViQuanLy, m => m.MapFrom(n => n.IIDMaDonViQuanLy))
                .ForMember(n => n.iID_NguonVonID, m => m.MapFrom(n => n.IIDNguonVonID))
                .ForMember(n => n.iNamKeHoach, m => m.MapFrom(n => n.INamKeHoach))
                .ForMember(n => n.sMaNguonCha, m => m.MapFrom(n => n.SMaNguonCha))
                .ForMember(n => n.sSoQuyetDinh, m => m.MapFrom(n => n.SSoQuyetDinh));
        }
    }
}
