using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using VTS.QLNS.CTC.App.Model.Control;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;

namespace VTS.QLNS.CTC.App.Model.Mapper
{
    public class TlDmPhuCapMapper : Profile
    {
        public TlDmPhuCapMapper()
        {
            CreateMap<TlDmPhuCap, TlDmPhuCapModel>();
            CreateMap<TlDmPhuCapModel, TlDmPhuCap>();
            CreateMap<TlDmPhuCap, ComboboxItem>()
                .ForMember(x => x.DisplayItem, y => y.MapFrom(z => z.MaPhuCap))
                .ForMember(x => x.ValueItem, y => y.MapFrom(z => z.Id))
                .ForMember(x => x.HiddenValue, y => y.MapFrom(z => z.TenPhuCap));
            CreateMap<ComboboxItem, TlDmPhuCap>()
                .ForMember(x => x.MaPhuCap, y => y.MapFrom(z => z.DisplayItem))
                .ForMember(x => x.Id, y => y.MapFrom(z => z.ValueItem));
            CreateMap<TlDmPhuCap, TlDmPhuCapHeThongModel>();
            CreateMap<TlDmPhuCapHeThongModel, TlDmPhuCap>();
            CreateMap<TlDmPhuCap, TlPhuCapQuery>().ReverseMap();
            CreateMap<TlDmPhuCap, TlDmPhuCapMapModel>().ReverseMap();
            CreateMap<TlDmPhuCapModel, TlDmPhuCapMapModel>().ReverseMap();
            
        }
    }
}
