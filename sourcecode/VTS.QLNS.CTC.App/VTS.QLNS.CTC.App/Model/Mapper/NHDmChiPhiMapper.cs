using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using VTS.QLNS.CTC.App.Model.Control;


namespace VTS.QLNS.CTC.App.Model.Mapper
{
    public class NHDmChiPhiMapper : Profile
    {
        public NHDmChiPhiMapper()
        {
            CreateMap<Core.Domain.NhDmChiPhi, ComboboxItem>()
                .ForMember(x => x.DisplayItem, y => y.MapFrom(z => z.STenChiPhi))
                .ForMember(x => x.ValueItem, y => y.MapFrom(z => z.IIdChiPhi));
            CreateMap<Core.Domain.NhDmChiPhi, NhDmChiPhiModel>();
            CreateMap<NhDmChiPhiModel, Core.Domain.NhDmChiPhi>();
        }
    }
}
