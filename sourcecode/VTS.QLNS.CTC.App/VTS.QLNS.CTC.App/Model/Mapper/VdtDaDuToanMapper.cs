using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace VTS.QLNS.CTC.App.Model.Mapper
{
    public class VdtDaDuToanMapper : Profile
    {
        public VdtDaDuToanMapper()
        {
            CreateMap<Core.Domain.VdtDaDuToan, VdtDuToanModel>();
            CreateMap<Core.Domain.Query.VdtDaDuToanQuery, VdtDuToanModel>();
            CreateMap<VdtDuToanModel, Core.Domain.VdtDaDuToan>();
            CreateMap<DuToanDetailModel, Core.Domain.VdtDaDuToanDmHangMuc>()
                .ForMember(entity => entity.Id, model => model.MapFrom(item => item.IdDuAnHangMuc))
                .ForMember(entity => entity.SMaHangMuc, model => model.MapFrom(item => item.MaHangMuc))
                .ForMember(entity => entity.STenHangMuc, model => model.MapFrom(item => item.TenHangMuc))
                .ForMember(entity => entity.IIdParentId, model => model.MapFrom(item => item.HangMucParentId));
            CreateMap<DuToanDetailModel, Core.Domain.VdtDaDuToanHangMuc>()
               .ForMember(entity => entity.FTienPheDuyet, model => model.MapFrom(item => item.GiaTriPheDuyet))
               .ForMember(entity => entity.Id, model => model.MapFrom(item => item.IdDuToanHangMuc))
               .ForMember(entity => entity.IIdHangMucId, model => model.MapFrom(item => item.IdDuAnHangMuc))
               //.ForMember(entity => entity.IIdChiPhiId, model => model.MapFrom(item => item.IdChiPhi))
               .ForMember(entity => entity.IIdDuAnChiPhi, model => model.MapFrom(item => item.IdDuAnChiPhi));
        }
    }
}
