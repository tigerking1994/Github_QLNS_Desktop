using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;

namespace VTS.QLNS.CTC.App.Model.Mapper
{
    public class HopDongMapper : Profile
    {
        public HopDongMapper()
        {
            CreateMap<HopDongQuery, HopDongModel>();

            CreateMap<HopDongGoiThauQuery, HopDongGoiThauQueryModel>()
                .ForMember(entity => entity.NhaThauId, model => model.MapFrom(item => item.NhaThauId));            
            CreateMap<HopDongChiPhiHangMucQuery, HopDongChiPhiHangMucQueryModel>().ForMember(x => x.FGiaTriConLai, opt => opt.Ignore());
            CreateMap<HopDongChiPhiQuery, HopDongChiPhiQueryModel>();
            CreateMap<HopDongChiPhiQueryModel, HopDongChiPhiQueryModel>();

            CreateMap<HopDongGoiThauQueryModel, VdtDaHopDongGoiThauNhaThau>()
                .ForMember(entity => entity.Id, model => model.MapFrom(item => item.IdHopDongGoiThauNhaThau))
                .ForMember(entity => entity.IIDGoiThauID, model => model.MapFrom(item => item.GoiThauId))
                .ForMember(entity => entity.IIDNhaThauID, model => model.MapFrom(item => item.NhaThauId))
                .ForMember(entity => entity.FGiaTri, model => model.MapFrom(item => item.FGiaTriGoiThau))
                .ForMember(entity => entity.FGiaTriTrungThau, model => model.MapFrom(item => item.FGiaTriTrungThau))
                .ForMember(entity => entity.FGiaTriHopDong, model => model.MapFrom(item => item.FGiaTriHopDong));

            CreateMap<HopDongChiPhiQueryModel, VdtDaHopDongGoiThauChiPhi>()
                .ForMember(entity => entity.IIdHopDongGoiThauNhaThauId, model => model.MapFrom(item => item.IdHopDongGoiThauNhaThau))
                .ForMember(entity => entity.FGiaTri, model => model.MapFrom(item => item.FGiaTriChiPhi))
                .ForMember(entity => entity.IIdChiPhiId, model => model.MapFrom(item => item.IdChiPhi));

            CreateMap<HopDongChiPhiHangMucQueryModel, VdtDaHopDongGoiThauHangMuc>()
                .ForMember(entity => entity.IIDHopDongGoiThauNhaThauID, model => model.MapFrom(item => item.IdHopDongGoiThauNhaThau))
                .ForMember(entity => entity.FGiaTri, model => model.MapFrom(item => item.FGiatriSuDung))
                .ForMember(entity => entity.IIDChiPhiID, model => model.MapFrom(item => item.ChiPhiId))
                .ForMember(entity => entity.IIDHangMucID, model => model.MapFrom(item => item.HangMucId))
                .ForMember(entity => entity.FGiaTriDuocDuyet, model => model.MapFrom(item => item.FTienGoiThau));

            CreateMap<ThongTinHopDongModel, VdtDaTtHopDong>();
            CreateMap<VdtDaTtHopDong, ThongTinHopDongModel>();
            CreateMap<VdtDaHopDongGoiThauHangMuc, VdtDaHopDongGoiThauHangMuc>();
        }
    }
}
