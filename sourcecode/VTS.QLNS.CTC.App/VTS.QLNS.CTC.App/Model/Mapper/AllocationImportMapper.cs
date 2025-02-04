using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using VTS.QLNS.CTC.App.Model.Import;
using VTS.QLNS.CTC.Core.Domain.Query;

namespace VTS.QLNS.CTC.App.Model.Mapper
{
    public class AllocationImportMapper : Profile
    {
        public AllocationImportMapper()
        {
            CreateMap<AllocationImportModel, Core.Domain.NsCpChungTuChiTiet>()
                .ForMember(entity => entity.SLns, model => model.MapFrom(item => item.LNS))
                .ForMember(entity => entity.SL, model => model.MapFrom(item => item.L))
                .ForMember(entity => entity.SK, model => model.MapFrom(item => item.K))
                .ForMember(entity => entity.SM, model => model.MapFrom(item => item.M))
                .ForMember(entity => entity.STm, model => model.MapFrom(item => item.TM))
                .ForMember(entity => entity.STtm, model => model.MapFrom(item => item.TTM))
                .ForMember(entity => entity.SNg, model => model.MapFrom(item => item.NG))
                .ForMember(entity => entity.SXauNoiMa, model => model.MapFrom(item => item.XauNoiMa))
                .ForMember(entity => entity.SMoTa, model => model.MapFrom(item => item.MoTa))
                .ForMember(entity => entity.FTuChi, model => model.MapFrom(item => item.CapPhat))
                .ForMember(entity => entity.FDeNghiDonVi, model => model.MapFrom(item => item.DeNghiDonVi))
                .ForMember(entity => entity.SGhiChu, model => model.MapFrom(item => item.GhiChu));
            CreateMap<AllocationImportModel, Core.Domain.ImpCpChungTuChiTiet>()
                .ForMember(entity => entity.Lns, model => model.MapFrom(item => item.LNS))
                .ForMember(entity => entity.L, model => model.MapFrom(item => item.L))
                .ForMember(entity => entity.K, model => model.MapFrom(item => item.K))
                .ForMember(entity => entity.M, model => model.MapFrom(item => item.M))
                .ForMember(entity => entity.Tm, model => model.MapFrom(item => item.TM))
                .ForMember(entity => entity.Ttm, model => model.MapFrom(item => item.TTM))
                .ForMember(entity => entity.Ng, model => model.MapFrom(item => item.NG))
                .ForMember(entity => entity.XauNoiMa, model => model.MapFrom(item => item.XauNoiMa))
                .ForMember(entity => entity.MoTa, model => model.MapFrom(item => item.MoTa))
                .ForMember(entity => entity.TuChi, model => model.MapFrom(item => item.CapPhat))
                .ForMember(entity => entity.GhiChu, model => model.MapFrom(item => item.GhiChu));
        }
    }
}
