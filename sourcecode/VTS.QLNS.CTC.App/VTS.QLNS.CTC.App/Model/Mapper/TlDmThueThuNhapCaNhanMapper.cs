using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace VTS.QLNS.CTC.App.Model.Mapper
{
    public class TlDmThueThuNhapCaNhanMapper : Profile
    {
       public TlDmThueThuNhapCaNhanMapper()
        {
            CreateMap<Core.Domain.TlDmThueThuNhapCaNhan, TlDmThueThuNhapCaNhanModel>()
            .ForMember(n => n.IIsThueThang, m => m.MapFrom(n => n.BIsThueThang ? "1" : "0"));
            CreateMap<TlDmThueThuNhapCaNhanModel, Core.Domain.TlDmThueThuNhapCaNhan>()
            .ForMember(n => n.BIsThueThang, m => m.MapFrom(n => n.IIsThueThang == "1"));
        }
    }
}
