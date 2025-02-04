using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.App.Model.Control;

namespace VTS.QLNS.CTC.App.Model.Mapper
{
    public class BhDmCoSoYTeMapper : Profile
    {
        public BhDmCoSoYTeMapper()
        {
            CreateMap<BhDmCoSoYTe, BhDmCoSoYTeModel>().ReverseMap();
            CreateMap<BhDmCoSoYTeModel, BhDmCoSoYTeQuery>().ReverseMap();
            CreateMap<BhDmCoSoYTe, CheckBoxItem>()
                .ForMember(entity => entity.DisplayItem, model => model.MapFrom(item => $"{item.IIDMaCoSoYTe} - {item.STenCoSoYTe}"))
                .ForMember(entity => entity.NameItem, model => model.MapFrom(item => item.STenCoSoYTe))
                .ForMember(entity => entity.ValueItem, model => model.MapFrom(item => item.IIDMaCoSoYTe));
            CreateMap<BhDmCoSoYTe, ComboboxItem>()
                .ForMember(entity => entity.DisplayItem, model => model.MapFrom(item => $"{item.IIDMaCoSoYTe} - {item.STenCoSoYTe}"))
                .ForMember(entity => entity.DisplayItemOption2, model => model.MapFrom(item => item.STenCoSoYTe))
                .ForMember(entity => entity.ValueItem, model => model.MapFrom(item => item.IIDMaCoSoYTe))
                .ForMember(entity => entity.HiddenValue, model => model.MapFrom(item => item.Id));
        }
    }
}
