using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using VTS.QLNS.CTC.App.Model.Control;

namespace VTS.QLNS.CTC.App.Model.Mapper
{
    public class VDTNhomDuAnMapper : Profile
    {
        public VDTNhomDuAnMapper()
        {
            CreateMap<Core.Domain.VdtDmNhomDuAn, ComboboxItem>()
                .ForMember(x => x.DisplayItem, y => y.MapFrom(z => z.STenNhomDuAn))
                .ForMember(x => x.ValueItem, y => y.MapFrom(z => z.Id));
        }
    }
}
