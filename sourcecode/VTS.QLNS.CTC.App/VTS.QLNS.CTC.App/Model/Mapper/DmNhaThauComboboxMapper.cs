using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using VTS.QLNS.CTC.App.Model.Control;

namespace VTS.QLNS.CTC.App.Model.Mapper
{
    public class DmNhaThauComboboxMapper : Profile
    {
        public DmNhaThauComboboxMapper()
        {
            CreateMap<Core.Domain.VdtDmNhaThau, ComboboxItem>()
               .ForMember(x => x.DisplayItem, y => y.MapFrom(z =>  z.STenNhaThau))
               .ForMember(x => x.ValueItem, y => y.MapFrom(z => z.Id));
        }
    }
}
