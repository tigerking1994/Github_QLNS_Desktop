using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace VTS.QLNS.CTC.App.Model.Mapper
{
    public class VdtDaGoiThauChiPhiMapper : Profile
    {
        public VdtDaGoiThauChiPhiMapper()
        {
            CreateMap<Core.Domain.VdtDaGoiThauChiPhi, GoiThauDetailModel>()
               .ForMember(entity => entity.IdGoiThau, model => model.MapFrom(item => item.IIdGoiThauId))
               .ForMember(entity => entity.IdChiPhi, model => model.MapFrom(item => item.IIdChiPhiId))
               .ForMember(entity => entity.GiaTriPheDuyet, model => model.MapFrom(item => item.FTienGoiThau));
            CreateMap<GoiThauDetailModel, Core.Domain.VdtDaGoiThauChiPhi>()
                .ForMember(entity => entity.Id, model => model.MapFrom(item => item.IdGoiThauChiPhi))
                .ForMember(entity => entity.IIdGoiThauId, model => model.MapFrom(item => item.IdGoiThau))
                .ForMember(entity => entity.IIdChiPhiId, model => model.MapFrom(item => item.IdChiPhi))
                .ForMember(entity => entity.FTienGoiThau, model => model.MapFrom(item => item.GiaTriPheDuyet));
        }
    }
}
