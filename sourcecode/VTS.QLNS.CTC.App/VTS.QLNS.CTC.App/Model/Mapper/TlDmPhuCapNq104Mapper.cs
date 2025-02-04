using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using VTS.QLNS.CTC.App.Model.Control;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;

namespace VTS.QLNS.CTC.App.Model.Mapper
{
    public class TlDmPhuCapNq104Mapper : Profile
    {
        public TlDmPhuCapNq104Mapper()
        {
            CreateMap<TlDmPhuCapNq104, TlDmPhuCapNq104Model>();
            CreateMap<TlDmPhuCapNq104Model, TlDmPhuCapNq104>();
            CreateMap<TlDmPhuCapNq104, ComboboxItem>()
                .ForMember(x => x.DisplayItem, y => y.MapFrom(z => z.MaPhuCap))
                .ForMember(x => x.ValueItem, y => y.MapFrom(z => z.Id))
                .ForMember(x => x.HiddenValue, y => y.MapFrom(z => z.TenPhuCap));
            CreateMap<ComboboxItem, TlDmPhuCapNq104>()
                .ForMember(x => x.MaPhuCap, y => y.MapFrom(z => z.DisplayItem))
                .ForMember(x => x.Id, y => y.MapFrom(z => z.ValueItem));
            CreateMap<TlDmPhuCapNq104, TlDmPhuCapHeThongNq104Model>();
            CreateMap<TlDmPhuCapHeThongNq104Model, TlDmPhuCapNq104>();
            CreateMap<TlDmPhuCapNq104, TlPhuCapNq104Query>().ReverseMap();
        }
    }
}
