using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace VTS.QLNS.CTC.App.Model.Mapper
{ 
    public class AllocationTypeMapper : Profile
    {
        public AllocationTypeMapper()
        {
            CreateMap<Core.Domain.CpDanhMuc, AllocationType>()
                .ForMember(entity => entity.Id, model => model.MapFrom(item => item.Id))
                .ForMember(entity => entity.IdCode, model => model.MapFrom(item => item.IIDMaDMCapPhat))
                .ForMember(entity => entity.Ten, model => model.MapFrom(item => item.STen))
                .ForMember(entity => entity.TenThongTriCap, model => model.MapFrom(item => item.STenThongTriCap))
                .ForMember(entity => entity.TenThongTriThu, model => model.MapFrom(item => item.STenThongTriThu))
                .ForMember(entity => entity.Lns, model => model.MapFrom(item => item.Lns))
                .ForMember(entity => entity.MoTa, model => model.MapFrom(item => item.SMoTa))
                .ForMember(entity => entity.OrderIndex, model => model.MapFrom(item => item.OrderIndex))
                .ForMember(entity => entity.NamLamViec, model => model.MapFrom(item => item.INamLamViec))
                .ForMember(entity => entity.ITrangThai, model => model.MapFrom(item => item.ITrangThai))
                .ForMember(entity => entity.DateCreated, model => model.MapFrom(item => item.DNgayTao))
                .ForMember(entity => entity.UserCreator, model => model.MapFrom(item => item.SNguoiTao))
                .ForMember(entity => entity.DateModified, model => model.MapFrom(item => item.DNgaySua))
                .ForMember(entity => entity.UserModifier, model => model.MapFrom(item => item.SNguoiSua))
                .ForMember(entity => entity.Tag, model => model.MapFrom(item => item.Tag))
                .ForMember(entity => entity.Log, model => model.MapFrom(item => item.Log));
        }
    }
}
