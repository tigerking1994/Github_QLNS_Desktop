using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using VTS.QLNS.CTC.App.Model.Control;

namespace VTS.QLNS.CTC.App.Model.Mapper
{
    class VdtQddtHangMucMapper : Profile
    {
        public VdtQddtHangMucMapper()
        {
            CreateMap<Core.Domain.VdtDaQddauTuHangMuc, ApproveProjectDetailModel>()
               .ForMember(entity => entity.GiaTriPheDuyet, model => model.MapFrom(item => item.FTienPheDuyet))
               .ForMember(entity => entity.IdDuAnHangMuc, model => model.MapFrom(item => item.IIdHangMucId))
               .ForMember(entity => entity.IdChiPhi, model => model.MapFrom(item => item.IIdChiPhiId))
               .ForMember(entity => entity.IdDuAnChiPhi, model => model.MapFrom(item => item.IIdDuAnChiPhi));
            CreateMap<ApproveProjectDetailModel,Core.Domain.VdtDaQddauTuHangMuc > ()
               .ForMember(entity => entity.FTienPheDuyet, model => model.MapFrom(item => item.GiaTriPheDuyet))
               .ForMember(entity => entity.FGiaTriDieuChinh, model => model.MapFrom(item => item.GiaTriDieuChinh))
               .ForMember(entity => entity.Id, model => model.MapFrom(item => item.IdQDHangMuc))
               .ForMember(entity => entity.IIdHangMucId, model => model.MapFrom(item => item.IdDuAnHangMuc))
               .ForMember(entity => entity.IIdChiPhiId, model => model.MapFrom(item => item.IdChiPhi))
               .ForMember(entity => entity.IIdDuAnChiPhi, model => model.MapFrom(item => item.IdDuAnChiPhi));
            CreateMap<ProjectManagerDetailModel, Core.Domain.VdtDaQddauTuHangMuc>()
               .ForMember(entity => entity.Id, model => model.MapFrom(item => item.IdQDHangMuc))
               .ForMember(entity => entity.FTienPheDuyet, model => model.MapFrom(item => item.GiaTriPheDuyet));
            CreateMap<Core.Domain.VdtDaDuAnHangMuc, ComboboxItem>()
                .ForMember(x => x.DisplayItem, y => y.MapFrom(z => z.STenHangMuc))
                .ForMember(x => x.ValueItem, y => y.MapFrom(z => z.Id));
        }
    }
}
