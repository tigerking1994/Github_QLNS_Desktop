using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace VTS.QLNS.CTC.App.Model.Mapper
{
    public class VdtDaHangMucMapper : Profile
    {
        public VdtDaHangMucMapper()
        {
            CreateMap<Core.Domain.VdtDaDuAnHangMuc, ApproveProjectDetailModel>()
                .ForMember(entity => entity.IdDuAnHangMuc, model => model.MapFrom(item => item.Id))
                .ForMember(entity => entity.IIdDuAnId, model => model.MapFrom(item => item.IIdDuAnId))
                .ForMember(entity => entity.TenHangMuc, model => model.MapFrom(item => item.STenHangMuc))
                .ForMember(entity => entity.GiaTriPheDuyet, model => model.MapFrom(item => item.FTienHangMuc));
            CreateMap< ApproveProjectDetailModel, Core.Domain.VdtDaDuAnHangMuc>()
                .ForMember(entity => entity.Id, model => model.MapFrom(item => item.IdDuAnHangMuc))
                .ForMember(entity => entity.IIdDuAnId, model => model.MapFrom(item => item.IIdDuAnId))
                .ForMember(entity => entity.SMaHangMuc, model => model.MapFrom(item => item.MaHangMuc))
                .ForMember(entity => entity.STenHangMuc, model => model.MapFrom(item => item.TenHangMuc))
                .ForMember(entity => entity.FTienHangMuc, model => model.MapFrom(item => item.GiaTriPheDuyet));
            CreateMap<ProjectManagerDetailModel, Core.Domain.VdtDaDuAnHangMuc>()
                .ForMember(entity => entity.Id, model => model.MapFrom(item => item.IdHangMuc))
                .ForMember(entity => entity.IIdDuAnId, model => model.MapFrom(item => item.IIdDuAnId))
                .ForMember(entity => entity.STenHangMuc, model => model.MapFrom(item => item.TenHangMuc))
                .ForMember(entity => entity.FTienHangMuc, model => model.MapFrom(item => item.GiaTriPheDuyet));
            CreateMap<VdtDaHangMucModel, Core.Domain.VdtDaDuAnHangMuc>()
                .ForMember(entity => entity.IdLoaiCongTrinh, model => model.MapFrom(item => item.LoaiCongTrinhId))
                .ForMember(entity => entity.fHanMucDauTu, model => model.MapFrom(item => item.HanMucDT))
                .ForMember(entity => entity.Id, model => model.MapFrom(item => item.IdDuAnHangMuc));
            CreateMap<Core.Domain.VdtDaDuAnHangMuc, VdtDaHangMucModel>()
                .ForMember(entity => entity.IdDuAnHangMuc, model => model.MapFrom(item => item.Id));
            CreateMap<Core.Domain.Query.VdtDaHangMucQuery, VdtDaHangMucModel>();
            CreateMap<VdtDaHangMucModel, Core.Domain.Query.VdtDaHangMucQuery>();
            CreateMap<VdtDaHangMucModel, Core.Domain.VdtDaQddauTuDmHangMuc>()
                .ForMember(entity => entity.Id, model => model.MapFrom(item => item.IdDuAnHangMuc));
            CreateMap<ApproveProjectDetailModel, Core.Domain.VdtDaQddauTuDmHangMuc>()
                .ForMember(entity => entity.Id, model => model.MapFrom(item => item.IdDuAnHangMuc))
                .ForMember(entity => entity.IIdDuAnId, model => model.MapFrom(item => item.IIdDuAnId))
                .ForMember(entity => entity.SMaHangMuc, model => model.MapFrom(item => item.MaHangMuc))
                .ForMember(entity => entity.STenHangMuc, model => model.MapFrom(item => item.TenHangMuc))
                .ForMember(entity => entity.IIdParentId, model => model.MapFrom(item => item.HangMucParentId));
            CreateMap<VdtDaHangMucModel, Core.Domain.VdtDaChuTruongDauTuDmHangMuc>()
                .ForMember(entity => entity.IIdLoaiCongTrinhId, model => model.MapFrom(item => item.LoaiCongTrinhId))
                .ForMember(entity => entity.FTienHangMuc, model => model.MapFrom(item => item.HanMucDT))
                .ForMember(entity => entity.SmaOrder, model => model.MapFrom(item => item.MaOrDer))
                .ForMember(entity => entity.Id, model => model.MapFrom(item => item.IdDuAnHangMuc));

            CreateMap<VdtDaHangMucModel, Core.Domain.VdtDaChuTruongDauTuHangMuc>()
                .ForMember(entity => entity.IIdLoaiCongTrinhId, model => model.MapFrom(item => item.LoaiCongTrinhId))
                .ForMember(entity => entity.Id, model => model.MapFrom(item => item.IdChuTruongHangMuc))
                .ForMember(entity => entity.IIdChuTruongDauTuId, model => model.MapFrom(item => item.IdChuTruong))
                .ForMember(entity => entity.IIdHangMucId, model => model.MapFrom(item => item.IdDuAnHangMuc))
                .ForMember(entity => entity.FTienPheDuyet, model => model.MapFrom(item => item.HanMucDT));
        }
    }
}
