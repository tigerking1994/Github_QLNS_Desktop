using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;

namespace VTS.QLNS.CTC.App.Model.Mapper
{
    public class VdtNcNhuCauChiMapper : Profile
    {
        public VdtNcNhuCauChiMapper()
        {
            CreateMap<VdtNcNhuCauChi, VdtNcNhuCauChiModel>()
                .ForMember(n => n.dNgayDeNghi, m => m.MapFrom(n => n.DNgayDeNghi))
                .ForMember(n => n.Id, m => m.MapFrom(n => n.Id))
                .ForMember(n => n.iID_DonViQuanLyID, m => m.MapFrom(n => n.IIdDonViQuanLyId))
                .ForMember(n => n.iID_MaDonViQuanLy, m => m.MapFrom(n => n.IIdMaDonViQuanLy))
                .ForMember(n => n.iID_NguonVonID, m => m.MapFrom(n => n.IIdNguonVonId))
                .ForMember(n => n.iNamKeHoach, m => m.MapFrom(n => n.INamKeHoach))
                .ForMember(n => n.iQuy, m => m.MapFrom(n => n.IQuy))
                .ForMember(n => n.sNguoiLap, m => m.MapFrom(n => n.SNguoiLap))
                .ForMember(n => n.sSoDeNghi, m => m.MapFrom(n => n.SSoDeNghi));
            CreateMap<VdtNcNhuCauChiModel, VdtNcNhuCauChi>()
                .ForMember(n => n.DNgayDeNghi, m => m.MapFrom(n => n.dNgayDeNghi))
                .ForMember(n => n.Id, m => m.MapFrom(n => n.Id))
                .ForMember(n => n.IIdDonViQuanLyId, m => m.MapFrom(n => n.iID_DonViQuanLyID))
                .ForMember(n => n.IIdMaDonViQuanLy, m => m.MapFrom(n => n.iID_MaDonViQuanLy))
                .ForMember(n => n.IIdNguonVonId, m => m.MapFrom(n => n.iID_NguonVonID))
                .ForMember(n => n.INamKeHoach, m => m.MapFrom(n => n.iNamKeHoach))
                .ForMember(n => n.IQuy, m => m.MapFrom(n => n.iQuy))
                .ForMember(n => n.SNguoiLap, m => m.MapFrom(n => n.sNguoiLap))
                .ForMember(n => n.SSoDeNghi, m => m.MapFrom(n => n.sSoDeNghi));

            CreateMap<VdtNcNhuCauChiQuery, VdtNcNhuCauChiModel>();
            CreateMap<VdtNcNhuCauChiModel, VdtNcNhuCauChiQuery>();

            CreateMap<VdtNcNhuCauChiChiTietQuery, VdtNcNhuCauChiChiTietModel>();
            CreateMap<VdtNcNhuCauChiChiTietModel, VdtNcNhuCauChiChiTietQuery>();
        }
    }
}
