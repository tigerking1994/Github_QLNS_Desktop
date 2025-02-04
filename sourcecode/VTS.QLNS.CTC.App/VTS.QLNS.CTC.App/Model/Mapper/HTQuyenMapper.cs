using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VTS.QLNS.CTC.Core.Domain;

namespace VTS.QLNS.CTC.App.Model.Mapper
{
    public class HTQuyenMapper : Profile
    {
        public HTQuyenMapper()
        {
            CreateMap<HtQuyen, HTQuyenModel>()
                .ForMember(model => model.SysFunctionModels, m => m.MapFrom(e => e.SysFunctionAuthorities.Select(t => t.HTChucNang)))
                .ForMember(model => model.SysFunctionName, m => m.MapFrom(e => string.Join("; ", e.SysFunctionAuthorities.Select(t => t.HTChucNang).Select(f => f.STenChucNang))))
                .ForMember(model => model.AuthorityTypeId, m => m.MapFrom(e => e.IIDLoaiQuyen.ToString()));
            ;
            CreateMap<HTQuyenModel, HtQuyen>()
                .ForMember(e => e.SysFunctionAuthorities, m => m.MapFrom(model => model.SysFunctionModels.Select(t => new HtQuyenChucNang { IIDMaChucNang = t.IIDMaChucNang })))
                .ForMember(e => e.IIDLoaiQuyen, m => m.MapFrom(model => Guid.Parse(model.AuthorityTypeId)));
        }
    }
}
