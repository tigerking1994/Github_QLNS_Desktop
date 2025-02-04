using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace VTS.QLNS.CTC.App.Model.Mapper
{
    public class VdtDaDuToanChiPhiMapper : Profile
    {
        public VdtDaDuToanChiPhiMapper()
        {
            CreateMap<Core.Domain.VdtDaDuToanChiPhi, DuToanDetailModel>()
               .ForMember(entity => entity.IdChiPhi, model => model.MapFrom(item => item.IIdChiPhiId))
               .ForMember(entity => entity.GiaTriPheDuyet, model => model.MapFrom(item => item.FTienPheDuyet));
            CreateMap<DuToanDetailModel, Core.Domain.VdtDaDuToanChiPhi>()
                .ForMember(entity => entity.IIdChiPhiId, model => model.MapFrom(item => item.IdChiPhi))
                .ForMember(entity => entity.FTienPheDuyet, model => model.MapFrom(item => item.GiaTriPheDuyet));
            CreateMap<Core.Domain.Query.VdtDaDuToanChiPhiQuery, VdtDaDuToanChiPhiModel>();
            CreateMap<VdtDaDuToanChiPhiModel, Core.Domain.VdtDmDuAnChiPhi>()
                .ForMember(entity => entity.Id, model => model.MapFrom(item => item.IdChiPhiDuAn))
                .ForMember(entity => entity.IIdChiPhi, model => model.MapFrom(item => item.IdChiPhi))
                .ForMember(entity => entity.IIdChiPhiParent, model => model.MapFrom(item => item.IdChiPhiDuAnParent))
                .ForMember(entity => entity.STenChiPhi, model => model.MapFrom(item => item.TenChiPhi));
            CreateMap<VdtDaDuToanChiPhiModel, Core.Domain.VdtDaDuToanChiPhi>()
                .ForMember(entity => entity.Id, model => model.MapFrom(item => item.IdDuToanChiPhi))
                .ForMember(entity => entity.IIdChiPhiId, model => model.MapFrom(item => item.IdChiPhi))
                .ForMember(entity => entity.IIdDuAnChiPhi, model => model.MapFrom(item => item.IdChiPhiDuAn))
                .ForMember(entity => entity.IIdDuToanId, model => model.MapFrom(item => item.IdDuToan))
                .ForMember(entity => entity.FTienPheDuyet, model => model.MapFrom(item => item.GiaTriPheDuyet));
        }
    }
}
