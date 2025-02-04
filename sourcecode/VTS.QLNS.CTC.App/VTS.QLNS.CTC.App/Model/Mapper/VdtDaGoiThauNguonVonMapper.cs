using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace VTS.QLNS.CTC.App.Model.Mapper
{
    public class VdtDaGoiThauNguonVonMapper : Profile
    {
        public VdtDaGoiThauNguonVonMapper()
        {
            CreateMap<Core.Domain.VdtDaGoiThauNguonVon, GoiThauDetailModel>()
               .ForMember(entity => entity.IdGoiThau, model => model.MapFrom(item => item.IIdGoiThauId))
               .ForMember(entity => entity.IdNguonVon, model => model.MapFrom(item => item.IIdNguonVonId))
               .ForMember(entity => entity.GiaTriPheDuyet, model => model.MapFrom(item => item.FTienGoiThau));
            CreateMap<GoiThauDetailModel, Core.Domain.VdtDaGoiThauNguonVon>()
                .ForMember(entity => entity.Id, model => model.MapFrom(item => item.IdGoiThauNguonVon))
                .ForMember(entity => entity.IIdGoiThauId, model => model.MapFrom(item => item.IdGoiThau))
                .ForMember(entity => entity.IIdNguonVonId, model => model.MapFrom(item => item.IdNguonVon))
                .ForMember(entity => entity.FTienGoiThau, model => model.MapFrom(item => item.GiaTriPheDuyet));
        }
    }
}
