using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;

namespace VTS.QLNS.CTC.App.Model.Mapper
{
    public class PlanBeginYearMapper : Profile
    {
        public PlanBeginYearMapper()
        {
            CreateMap<DonViPlanBeginYearQuery, PlanBeginYearModel>()
                .ForMember(entity => entity.SMoTa, model => model.MapFrom(item => item.MoTa))
                .ForMember(entity => entity.SoKiemTra, model => model.MapFrom(item => item.SoKiemTra.HasValue ? item.SoKiemTra : 0))
                .ForMember(entity => entity.SoDuToan, model => model.MapFrom(item => item.SoDuToan.HasValue ? item.SoDuToan : 0));
        }
    }
}
