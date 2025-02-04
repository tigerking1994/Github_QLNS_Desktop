using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using VTS.QLNS.CTC.Core.Domain;

namespace VTS.QLNS.CTC.App.Model.Mapper
{
    public class VdtQddtChiPhiMapper : Profile
    {
        public VdtQddtChiPhiMapper()
        {
            CreateMap<Core.Domain.VdtDaQddauTuChiPhi, ApproveProjectDetailModel>()
                .ForMember(entity => entity.IdChiPhi, model => model.MapFrom(item => item.IIdChiPhiId))
                .ForMember(entity => entity.GiaTriPheDuyet, model => model.MapFrom(item => item.FTienPheDuyet));
            CreateMap<Core.Domain.VdtDaQddauTuChiPhi, ProjectManagerDetailModel>()
               .ForMember(entity => entity.IdQDChiPhi, model => model.MapFrom(item => item.Id))
               .ForMember(entity => entity.IdChiPhi, model => model.MapFrom(item => item.IIdChiPhiId))
               .ForMember(entity => entity.GiaTriPheDuyet, model => model.MapFrom(item => item.FTienPheDuyet));
            CreateMap<ApproveProjectDetailModel,Core.Domain.VdtDaQddauTuChiPhi > ()
                .ForMember(entity => entity.IIdChiPhiId, model => model.MapFrom(item => item.IdChiPhi))
                .ForMember(entity => entity.FTienPheDuyet, model => model.MapFrom(item => item.GiaTriPheDuyet));
            CreateMap<ProjectManagerDetailModel, Core.Domain.VdtDaQddauTuChiPhi>()
                .ForMember(entity => entity.Id, model => model.MapFrom(item => item.IdQDChiPhi))
                .ForMember(entity => entity.IIdQddauTuId, model => model.MapFrom(item => item.IdQdDauTu))
                .ForMember(entity => entity.IIdChiPhiId, model => model.MapFrom(item => item.IdChiPhi))
                .ForMember(entity => entity.FTienPheDuyet, model => model.MapFrom(item => item.GiaTriPheDuyet));
            CreateMap<QDDauTuChiPhiNguonVonDetailModel, Core.Domain.VdtDaQddauTuChiPhi>()
                .ForMember(entity => entity.Id, model => model.MapFrom(item => item.IdQDChiPhi))
                .ForMember(entity => entity.IIdChiPhiId, model => model.MapFrom(item => item.IdChiPhi))
                .ForMember(entity => entity.IIdQddauTuId, model => model.MapFrom(item => item.IdQDDauTu))
                .ForMember(entity => entity.FTienPheDuyet, model => model.MapFrom(item => item.GiaTriPheDuyet));
            CreateMap<Core.Domain.Query.VdtDaQddtChiPhiQuery, VdtDaQddtChiPhiModel>();
            CreateMap<VdtDaQddtChiPhiModel, Core.Domain.VdtDaQddauTuChiPhi>()
                .ForMember(entity => entity.Id, model => model.MapFrom(item => item.IdQDChiPhi))
                //.ForMember(entity => entity.IIdChiPhiId, model => model.MapFrom(item => item.IdChiPhi))
                .ForMember(entity => entity.IIdDuAnChiPhi, model => model.MapFrom(item => item.IdChiPhiDuAn))
                .ForMember(entity => entity.IIdQddauTuId, model => model.MapFrom(item => item.IdQDDauTu))
                .ForMember(entity => entity.FGiaTriDieuChinh, model => model.MapFrom(item => item.GiaTriDieuChinh))
                .ForMember(entity => entity.FTienPheDuyet, model => model.MapFrom(item => item.GiaTriPheDuyet));
            CreateMap<VdtDmChiPhi, Core.Domain.Query.VdtDaQddtChiPhiQuery>()
                .ForMember(entity => entity.TenChiPhi, model => model.MapFrom(item => item.STenChiPhi))
                .ForMember(entity => entity.IdChiPhi, model => model.MapFrom(item => item.IIdChiPhi));
            CreateMap<VdtDaQddtChiPhiModel, Core.Domain.VdtDmDuAnChiPhi>()
                .ForMember(entity => entity.Id, model => model.MapFrom(item => item.IdChiPhiDuAn))
                .ForMember(entity => entity.IIdChiPhi, model => model.MapFrom(item => item.IdChiPhi))
                .ForMember(entity => entity.IIdChiPhiParent, model => model.MapFrom(item => item.IdChiPhiDuAnParent))
                .ForMember(entity => entity.STenChiPhi, model => model.MapFrom(item => item.TenChiPhi));
            CreateMap<VdtDmChiPhi,Core.Domain.Query.VdtDaQddtChiPhiQuery>()
                .ForMember(entity => entity.IdChiPhi, model => model.MapFrom(item => item.Id))
                .ForMember(entity => entity.TenChiPhi, model => model.MapFrom(item => item.STenChiPhi));
        }
    }
}
