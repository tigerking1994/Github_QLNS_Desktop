using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using VTS.QLNS.CTC.App.Model.Control;

namespace VTS.QLNS.CTC.App.Model.Mapper
{ 
    public class CpDanhMucMapper : Profile
    {
        public CpDanhMucMapper()
        {
            CreateMap<AllocationType, Core.Domain.CpDanhMuc>()
                .ForMember(entity => entity.Id, model => model.MapFrom(item => item.Id))
                .ForMember(entity => entity.IIDMaDMCapPhat, model => model.MapFrom(item => item.IdCode))
                .ForMember(entity => entity.STen, model => model.MapFrom(item => item.Ten))
                .ForMember(entity => entity.STenThongTriCap, model => model.MapFrom(item => item.TenThongTriCap))
                .ForMember(entity => entity.STenThongTriThu, model => model.MapFrom(item => item.TenThongTriThu))
                .ForMember(entity => entity.Lns, model => model.MapFrom(item => item.Lns))
                .ForMember(entity => entity.SMoTa, model => model.MapFrom(item => item.MoTa))
                .ForMember(entity => entity.OrderIndex, model => model.MapFrom(item => item.OrderIndex))
                .ForMember(entity => entity.INamLamViec, model => model.MapFrom(item => item.NamLamViec))
                .ForMember(entity => entity.ITrangThai, model => model.MapFrom(item => item.ITrangThai))
                .ForMember(entity => entity.DNgayTao, model => model.MapFrom(item => item.DateCreated))
                .ForMember(entity => entity.SNguoiTao, model => model.MapFrom(item => item.UserCreator))
                .ForMember(entity => entity.DNgaySua, model => model.MapFrom(item => item.DateModified))
                .ForMember(entity => entity.SNguoiSua, model => model.MapFrom(item => item.UserModifier))
                .ForMember(entity => entity.Tag, model => model.MapFrom(item => item.Tag))
                .ForMember(entity => entity.Log, model => model.MapFrom(item => item.Log));
            CreateMap<Core.Domain.CpDanhMuc, ComboboxItem>()
                .ForMember(entity => entity.DisplayItem, model => model.MapFrom(item => string.Format("{0} - {1}", item.IIDMaDMCapPhat, item.STen)))
                .ForMember(entity => entity.ValueItem, model => model.MapFrom(item => item.IIDMaDMCapPhat));
            CreateMap<Core.Domain.CpDanhMuc, CpDanhMucModel>();
            CreateMap<CpDanhMucModel, Core.Domain.CpDanhMuc>();
        }
    }
}
