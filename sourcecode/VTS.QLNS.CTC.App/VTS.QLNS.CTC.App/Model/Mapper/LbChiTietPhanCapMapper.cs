using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;

namespace VTS.QLNS.CTC.App.Model.Mapper
{
    public class LbChiTietPhanCapMapper : Profile
    {
        public LbChiTietPhanCapMapper()
        {
            CreateMap<LbChiTietPhanCapQuery, LevelBuggetDetailChildModel>()
                .ForMember(entity => entity.IsHangCha, model => model.MapFrom(item => item.bHangCha));

            CreateMap<LevelBuggetDetailChildModel, NsNganhChungTuChiTietPhanCap>()
                .ForMember(entity => entity.Id, model => model.MapFrom(item => item.Id))
                .ForMember(entity => entity.IIdCtnganhChiTiet, model => model.MapFrom(item => item.SoLieuChiTietId))
                .ForMember(entity => entity.IIdMaDonVi, model => model.MapFrom(item => item.IdDonVi))
                .ForMember(entity => entity.STenDonVi, model => model.MapFrom(item => item.TenDonVi))
                .ForMember(entity => entity.IIdMlns, model => model.MapFrom(item => item.MLNSId))
                .ForMember(entity => entity.FPhanCap, model => model.MapFrom(item => item.PhanCap))
                .ForMember(entity => entity.FHienVat, model => model.MapFrom(item => item.HienVat))
                .ForMember(entity => entity.SXauNoiMa, model => model.MapFrom(item => item.XauNoiMa))
                .ForMember(entity => entity.SGhiChu, model => model.MapFrom(item => item.GhiChu))
                .ForMember(entity => entity.IIdMlns, model => model.MapFrom(item => item.MucLucID));
        }
    }
}
