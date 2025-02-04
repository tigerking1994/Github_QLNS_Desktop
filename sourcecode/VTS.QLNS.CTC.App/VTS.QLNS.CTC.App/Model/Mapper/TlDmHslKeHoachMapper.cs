using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using VTS.QLNS.CTC.App.Model.Control;
using VTS.QLNS.CTC.Core.Domain;

namespace VTS.QLNS.CTC.App.Model.Mapper
{
    public class TlDmHslKeHoachMapper : Profile
    {
        public TlDmHslKeHoachMapper()
        {
            CreateMap<TlDmHslKeHoach, TlDmHslKeHoachModel>();
            CreateMap<TlDmHslKeHoachModel, TlDmHslKeHoach>();
            CreateMap<TlDmHslKeHoachModel, ComboboxItem>()
                .ForMember(x => x.DisplayItem, y => y.MapFrom(z => z.Display))
                .ForMember(x => x.ValueItem, y => y.MapFrom(y => y.Id))
                .ForMember(x => x.HiddenValue, y => y.MapFrom(y => y.LhtHsKh));
        }
    }
}
