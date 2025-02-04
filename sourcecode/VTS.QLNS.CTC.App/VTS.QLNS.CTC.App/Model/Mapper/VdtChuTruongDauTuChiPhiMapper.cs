using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace VTS.QLNS.CTC.App.Model.Mapper
{
    public class VdtChuTruongDauTuChiPhiMapper : Profile
    {
        public VdtChuTruongDauTuChiPhiMapper()
        {
            CreateMap<Core.Domain.VdtDaChuTruongDauTuChiPhi, ProjectManagerDetailModel>()
               .ForMember(entity => entity.IdChuTruongDauTu, model => model.MapFrom(item => item.IIdChuTruongDauTuId))
               .ForMember(entity => entity.IdChiPhi, model => model.MapFrom(item => item.IIdChiPhiId))
               .ForMember(entity => entity.GiaTriPheDuyet, model => model.MapFrom(item => item.FTienPheDuyet));
            CreateMap<ProjectManagerDetailModel, Core.Domain.VdtDaChuTruongDauTuChiPhi>()
                .ForMember(entity => entity.IIdChuTruongDauTuId, model => model.MapFrom(item => item.IdChuTruongDauTu))
                .ForMember(entity => entity.IIdChiPhiId, model => model.MapFrom(item => item.IdChiPhi))
                .ForMember(entity => entity.FTienPheDuyet, model => model.MapFrom(item => item.GiaTriPheDuyet));
            CreateMap<ChuTruongDauTuDetailModel, Core.Domain.VdtDaChuTruongDauTuChiPhi>()
               .ForMember(entity => entity.Id, model => model.MapFrom(item => item.IdChuTruongChiPhi))
               .ForMember(entity => entity.IIdChuTruongDauTuId, model => model.MapFrom(item => item.IdChuTruongDauTu))
               .ForMember(entity => entity.IIdChiPhiId, model => model.MapFrom(item => item.IdChiPhi))
               .ForMember(entity => entity.FTienPheDuyet, model => model.MapFrom(item => item.GiaTriPheDuyet));
        }
    }
}
