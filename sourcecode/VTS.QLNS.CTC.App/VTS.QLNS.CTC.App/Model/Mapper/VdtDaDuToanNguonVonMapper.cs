using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace VTS.QLNS.CTC.App.Model.Mapper
{
    public class VdtDaDuToanNguonVonMapper : Profile
    {
        public VdtDaDuToanNguonVonMapper()
        {
            CreateMap<Core.Domain.Query.VdtDaDuToanNguonVonQuery, VdtDaDuToanNguonVonModel>();
            CreateMap<VdtDaDuToanNguonVonModel, Core.Domain.VdtDaDuToanNguonvon>()
                .ForMember(entity => entity.Id, model => model.MapFrom(item => item.IdDuToanNguonVon))
                .ForMember(entity => entity.IIdNguonVonId, model => model.MapFrom(item => item.IdNguonVon))
                .ForMember(entity => entity.IIdDuToanId, model => model.MapFrom(item => item.IdDuToan))
                .ForMember(entity => entity.FTienPheDuyet, model => model.MapFrom(item => item.GiaTriPheDuyet));
        }
    }
}
