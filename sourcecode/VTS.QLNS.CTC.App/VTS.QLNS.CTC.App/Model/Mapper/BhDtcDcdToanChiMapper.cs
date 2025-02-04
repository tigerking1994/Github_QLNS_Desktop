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
    public class BhDtcDcdToanChiMapper : Profile
    {
        public BhDtcDcdToanChiMapper()
        {
            CreateMap<BhDtcDcdToanChiModel, BhDtcDcdToanChi>().ReverseMap();
            CreateMap<BhDtcDcdToanChiModel, BhDtcDcdToanChiQuery>().ReverseMap();
            CreateMap<BhDtcDcdToanChiModel, ComboboxManyItem>()
                .ForMember(entity => entity.DisplayItem, model => model.MapFrom(item => item.SSoChungTu))
                 .ForMember(entity => entity.Type, model => model.MapFrom(item => item.SLNS))
                .ForMember(entity => entity.DisplayItem1, model => model.MapFrom(item => item.DNgayChungTu.ToString("dd/MM/yyyy")))
                .ForMember(entity => entity.ValueItem, model => model.MapFrom(item => item.Id)); ;
        }
    }
}
