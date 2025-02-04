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
    public class TnDtdnChungTuMapper : Profile
    {
        public TnDtdnChungTuMapper()
        {
            CreateMap<TnDtdnChungTu, CheckBoxItem>()
                .ForMember(entity => entity.DisplayItem, model => model.MapFrom(item => item.SSoChungTu + " - " + item.SMoTa))
                .ForMember(entity => entity.ValueItem, model => model.MapFrom(item => item.SSoChungTu))
                .ForMember(entity => entity.NameItem, model => model.MapFrom(item => item.Id));
            CreateMap<TnDtdnChungTu, ComboboxItem>()
              .ForMember(entity => entity.DisplayItem, model => model.MapFrom(item => item.SSoChungTu + " - " + item.SMoTa))
              .ForMember(entity => entity.ValueItem, model => model.MapFrom(item => item.SSoChungTu));
            CreateMap<TnDtdnChungTu, TnDtdnChungTuModel>().ReverseMap();
        }
    }
}
