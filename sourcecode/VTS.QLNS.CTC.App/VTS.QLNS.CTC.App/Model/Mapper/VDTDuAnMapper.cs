using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using VTS.QLNS.CTC.App.Model.Control;
using VTS.QLNS.CTC.Core.Domain.Query;

namespace VTS.QLNS.CTC.App.Model.Mapper
{
    public class VDTDuAnMapper : Profile
    {
        public VDTDuAnMapper()
        {
            CreateMap<Core.Domain.VdtDaDuAn, ComboboxItem>()
                .ForMember(x => x.DisplayItem, y => y.MapFrom(z => z.SMaDuAn + " - " + z.STenDuAn))
                .ForMember(x => x.ValueItem, y => y.MapFrom(z => z.Id));
            CreateMap<Core.Domain.Query.ProjectManagerQuery, ComboboxItem>()
                .ForMember(x => x.DisplayItem, y => y.MapFrom(z => z.SMaDuAn + " - " + z.STenDuAn))
                .ForMember(x => x.ValueItem, y => y.MapFrom(z => z.Id));
            CreateMap<Core.Domain.VdtDaDuAn, ChuTruongDauTuModel>()
                .ForMember(x => x.Id, opt => opt.Ignore())
                .ForMember(x => x.IIdDuAnId, opt => opt.Ignore());

            CreateMap<DuAnDetailModel, ChiPhiHangMucQuery>()
                .ForMember(n => n.IID_ChiPhi, m => m.MapFrom(n => n.iIdChiPhi))
                .ForMember(n => n.IId_HangMuc, m => m.MapFrom(n => n.iIdHangMuc))
                .ForMember(n => n.IId_NguonVon, m => m.MapFrom(n => n.iIdNguonVon))
                .ForMember(n => n.IID_ParentID, m => m.MapFrom(n => n.iIdParentId));

            CreateMap<ChiPhiHangMucQuery, DuAnDetailModel>()
                .ForMember(n => n.iIdChiPhi, m => m.MapFrom(n => n.IID_ChiPhi))
                .ForMember(n => n.iIdHangMuc, m => m.MapFrom(n => n.IId_HangMuc))
                .ForMember(n => n.iIdNguonVon, m => m.MapFrom(n => n.IId_NguonVon))
                .ForMember(n => n.iIdParentId, m => m.MapFrom(n => n.IID_ParentID));

            CreateMap<Core.Domain.VdtDaDuAn, VdtDaDuAnModel>();
        }
    }
}
