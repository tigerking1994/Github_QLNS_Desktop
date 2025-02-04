using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using VTS.QLNS.CTC.App.Model.Control;

namespace VTS.QLNS.CTC.App.Model.Mapper
{
    public class VDTDmChiPhiMapper : Profile
    {
        public VDTDmChiPhiMapper()
        {
            CreateMap<Core.Domain.VdtDmChiPhi, ComboboxItem>()
                .ForMember(x => x.DisplayItem, y => y.MapFrom(z => z.STenChiPhi))
                .ForMember(x => x.ValueItem, y => y.MapFrom(z => z.IIdChiPhi));
            CreateMap<Core.Domain.VdtDmChiPhi, VdtDmChiPhiModel>();
            CreateMap<VdtDmChiPhiModel, Core.Domain.VdtDmChiPhi>();
        }
    }
}
