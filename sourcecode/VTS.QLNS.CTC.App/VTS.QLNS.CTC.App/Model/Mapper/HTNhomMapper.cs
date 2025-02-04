using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VTS.QLNS.CTC.Core.Domain;

namespace VTS.QLNS.CTC.App.Model.Mapper
{
    public class HTNhomMapper : Profile
    {
        public HTNhomMapper()
        {
            CreateMap<HtNhom, HTNhomModel>()
                .ForMember(x => x.HTQuyenModels, y => y.MapFrom(z => z.SysGroupAuthorities.Select(p => p.HTQuyen)));
            CreateMap<HTNhomModel, HtNhom>();
        }
    }
}
