using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace VTS.QLNS.CTC.App.Model.Mapper
{
    public class VdtChuTruongDauTuNguonVonMapper : Profile
    {
        public VdtChuTruongDauTuNguonVonMapper()
        {
            CreateMap<Core.Domain.VdtDaChuTruongDauTuNguonVon, ProjectManagerDetailModel>()
               .ForMember(entity => entity.IdQDChiPhi, model => model.MapFrom(item => item.Id))
               .ForMember(entity => entity.IdNguonVon, model => model.MapFrom(item => item.IIdNguonVonId))
               .ForMember(entity => entity.GiaTriPheDuyet, model => model.MapFrom(item => item.FTienPheDuyet));
            CreateMap<ProjectManagerDetailModel, Core.Domain.VdtDaChuTruongDauTuNguonVon>()
                .ForMember(entity => entity.IIdChuTruongDauTuId, model => model.MapFrom(item => item.IdChuTruongDauTu))
                .ForMember(entity => entity.IIdNguonVonId, model => model.MapFrom(item => item.IdNguonVon))
                .ForMember(entity => entity.FTienPheDuyet, model => model.MapFrom(item => item.GiaTriPheDuyet));
            CreateMap<ChuTruongDauTuDetailModel, Core.Domain.VdtDaChuTruongDauTuNguonVon>()
                .ForMember(entity => entity.Id, model => model.MapFrom(item => item.IdChuTruongNguonVon))
                .ForMember(entity => entity.IIdChuTruongDauTuId, model => model.MapFrom(item => item.IdChuTruongDauTu))
                .ForMember(entity => entity.IIdNguonVonId, model => model.MapFrom(item => item.IdNguonVon))
                .ForMember(entity => entity.FTienPheDuyet, model => model.MapFrom(item => item.GiaTriPheDuyet));
            CreateMap<Core.Domain.Query.VdtDaChuTruongDauTuNguonVonQuery, VdtDaChuTruongDTNguonVonModel>();
            CreateMap<VdtDaChuTruongDTNguonVonModel, Core.Domain.VdtDaChuTruongDauTuNguonVon>()
                .ForMember(entity => entity.Id, model => model.MapFrom(item => item.IdChuTruongNguonVon));
        }
    }
}
