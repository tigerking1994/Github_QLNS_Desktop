using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace VTS.QLNS.CTC.App.Model.Mapper
{
    public class VdtQddtNguonVonMapper : Profile
    {
        public VdtQddtNguonVonMapper()
        {
            CreateMap<Core.Domain.VdtDaQddauTuNguonVon, ProjectManagerDetailModel>()
                .ForMember(entity => entity.IdQDNguonVon, model => model.MapFrom(item => item.Id))
                .ForMember(entity => entity.IdNguonVon, model => model.MapFrom(item => item.IIdNguonVonId))
                .ForMember(entity => entity.GiaTriPheDuyet, model => model.MapFrom(item => item.FTienPheDuyet));
            CreateMap<ProjectManagerDetailModel, Core.Domain.VdtDaQddauTuNguonVon>()
                .ForMember(entity => entity.Id, model => model.MapFrom(item => item.IdQDNguonVon))
                .ForMember(entity => entity.IIdQddauTuId, model => model.MapFrom(item => item.IdQdDauTu))
                .ForMember(entity => entity.IIdNguonVonId, model => model.MapFrom(item => item.IdNguonVon))
                .ForMember(entity => entity.FTienPheDuyet, model => model.MapFrom(item => item.GiaTriPheDuyet));
            CreateMap<QDDauTuChiPhiNguonVonDetailModel, Core.Domain.VdtDaQddauTuNguonVon>()
                .ForMember(entity => entity.Id, model => model.MapFrom(item => item.IdQDChiPhi))
                .ForMember(entity => entity.IIdNguonVonId, model => model.MapFrom(item => item.IdNguonVon))
                .ForMember(entity => entity.IIdQddauTuId, model => model.MapFrom(item => item.IdQDDauTu))
                .ForMember(entity => entity.FTienPheDuyet, model => model.MapFrom(item => item.GiaTriPheDuyet));
            CreateMap<VdtDaChuTruongDTNguonVonModel, Core.Domain.VdtDaQddauTuNguonVon>()
                .ForMember(entity => entity.IIdNguonVonId, model => model.MapFrom(item => item.IIdNguonVonId))
                .ForMember(entity => entity.FTienPheDuyet, model => model.MapFrom(item => item.FTienPheDuyet));
            CreateMap<Core.Domain.Query.VdtDaQDNguonVonQuery, VdtDaQddtNguonVonModel>();
            CreateMap<VdtDaQddtNguonVonModel, Core.Domain.VdtDaQddauTuNguonVon>()
                .ForMember(entity => entity.Id, model => model.MapFrom(item => item.IdQDNguonVon))
                .ForMember(entity => entity.IIdNguonVonId, model => model.MapFrom(item => item.IdNguonVon))
                .ForMember(entity => entity.IIdQddauTuId, model => model.MapFrom(item => item.IdQDDauTu))
                .ForMember(entity => entity.FGiaTriDieuChinh, model => model.MapFrom(item => item.GiaTriDieuChinh))
                .ForMember(entity => entity.FTienPheDuyet, model => model.MapFrom(item => item.GiaTriPheDuyet))
                .ForMember(entity => entity.FTienPheDuyetCTDT, model => model.MapFrom(item => item.GiaTriPheDuyetCTDT));
        }
    }
}
