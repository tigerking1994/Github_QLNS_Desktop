using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VTS.QLNS.CTC.App.Model.Mapper
{
    public class HTChucNangMapper : Profile
    {
        public HTChucNangMapper()
        {
            CreateMap<Core.Domain.HtChucNang, HTChucNangModel>()
                .ForMember(model => model.SysAuthoritiesName, e => e.MapFrom(e => string.Join(";", e.SysFunctionAuthorities.Select(s => s.IIDMaQuyen))));
            CreateMap<HTChucNangModel, Core.Domain.HtChucNang>();
            CreateMap<Core.Domain.HtChucNang, DisplayHTChucNangModel>();
            CreateMap<DisplayHTChucNangModel, Core.Domain.HtChucNang>();
        }
    }
}
