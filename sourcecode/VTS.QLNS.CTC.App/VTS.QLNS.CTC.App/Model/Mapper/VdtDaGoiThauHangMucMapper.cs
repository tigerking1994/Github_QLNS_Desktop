using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace VTS.QLNS.CTC.App.Model.Mapper
{
    public class VdtDaGoiThauHangMucMapper : Profile
    {
        public VdtDaGoiThauHangMucMapper()
        {
            CreateMap<Core.Domain.VdtDaGoiThauHangMuc, GoiThauDetailModel>()
              .ForMember(entity => entity.IdGoiThau, model => model.MapFrom(item => item.IIdGoiThauId))
              .ForMember(entity => entity.IdHangMuc, model => model.MapFrom(item => item.IIdHangMucId))
              .ForMember(entity => entity.GiaTriPheDuyet, model => model.MapFrom(item => item.FTienGoiThau));
            CreateMap<GoiThauDetailModel, Core.Domain.VdtDaGoiThauHangMuc>()
                .ForMember(entity => entity.Id, model => model.MapFrom(item => item.IdGoiThauHangMuc))
                .ForMember(entity => entity.IIdGoiThauId, model => model.MapFrom(item => item.IdGoiThau))
                .ForMember(entity => entity.IIdHangMucId, model => model.MapFrom(item => item.IdHangMuc))
                .ForMember(entity => entity.FTienGoiThau, model => model.MapFrom(item => item.GiaTriPheDuyet));
        }
    }
}
