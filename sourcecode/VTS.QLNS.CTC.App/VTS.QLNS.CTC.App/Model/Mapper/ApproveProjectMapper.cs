using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace VTS.QLNS.CTC.App.Model.Mapper
{
    class ApproveProjectMapper : Profile
    {
        public ApproveProjectMapper()
        {
            CreateMap<Core.Domain.VdtDaQddauTu, ApproveProjectModel>()
               .ForMember(entity => entity.NgayQuyetDinhString, model => model.MapFrom(item => item.DNgayQuyetDinh.HasValue ? item.DNgayQuyetDinh.Value.ToString("dd/MM/yyyy") : string.Empty));
            CreateMap<Core.Domain.Query.ApproveProjectQuery, ApproveProjectModel>()
               .ForMember(entity => entity.NgayQuyetDinhString, model => model.MapFrom(item => item.DNgayQuyetDinh.HasValue ? item.DNgayQuyetDinh.Value.ToString("dd/MM/yyyy") : string.Empty));
            CreateMap<ApproveProjectModel, Core.Domain.VdtDaQddauTu>();
            CreateMap<ApproveProjectModel, Core.Domain.VdtDaDuAn>()
                .ForMember(entity => entity.FTongMucDauTu, model => model.MapFrom(item => item.FTongMucDauTuPheDuyet))
                .ForMember(x => x.Id, opt => opt.Ignore())
                .ForMember(x => x.IIdChuDauTuId, opt => opt.Ignore())
                .ForMember(x => x.IIdMaChuDauTuId, opt => opt.Ignore())
                .ForMember(x => x.IIdDonViQuanLyId, opt => opt.Ignore())
                .ForMember(x => x.IIdMaDonViQuanLy, opt => opt.Ignore())
                .ForMember(x => x.SMaDuAn, opt => opt.Ignore())
                .ForMember(x => x.IIdNhomDuAnId, opt => opt.Ignore())
                .ForMember(x => x.STenDuAn, opt => opt.Ignore());
        }
    }
}
