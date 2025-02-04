using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VTS.QLNS.CTC.App.Model.Control;
using VTS.QLNS.CTC.Core.Domain;

namespace VTS.QLNS.CTC.App.Model.Mapper
{
    public class NhDmNhaThauMapper : Profile
    {
        public NhDmNhaThauMapper()
        {
            CreateMap<NhDmNhaThau, NhDmNhaThauModel>().ReverseMap();
            CreateMap<NhDmNhaThauModel, ComboboxItem>()
                .ForMember(entity => entity.ValueItem, model => model.MapFrom(x => x.Id))
                .ForMember(entity => entity.DisplayItem, model => model.MapFrom(x => x.STenNhaThau));
        }
    }
}
