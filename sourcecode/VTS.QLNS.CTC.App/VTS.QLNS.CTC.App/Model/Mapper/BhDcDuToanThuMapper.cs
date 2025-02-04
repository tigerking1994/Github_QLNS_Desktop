using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VTS.QLNS.CTC.App.Model.Control;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Domain;

namespace VTS.QLNS.CTC.App.Model.Mapper
{
    public class BhDcDuToanThuMapper : Profile
    {
        public BhDcDuToanThuMapper()
        {
            CreateMap<BhDcDuToanThuModel, BhDcDuToanThu>().ReverseMap();
            CreateMap<BhDcDuToanThuModel, BhDcDuToanThuQuery>().ReverseMap();
            CreateMap<BhDcDuToanThuModel, BhDcDuToanThu>().ReverseMap();
            CreateMap<BhDcDuToanThu, BhDcDuToanThuQuery>().ReverseMap();
            CreateMap<BhDcDuToanThuModel, ComboboxManyItem>()
                .ForMember(entity => entity.DisplayItem, model => model.MapFrom(item => item.SSoChungTu))
                 .ForMember(entity => entity.Type, model => model.MapFrom(item => item.SLNS))
                .ForMember(entity => entity.ValueItem, model => model.MapFrom(item => item.Id)); ;
        }
    }
}
