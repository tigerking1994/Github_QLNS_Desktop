using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;

namespace VTS.QLNS.CTC.App.Model.Mapper
{
    public class NhDaGoiThauChiPhiMapper : Profile
    {
        public NhDaGoiThauChiPhiMapper()
        {
            CreateMap<NhDaGoiThauChiPhi, NhDaGoiThauChiPhiModel>()
                .ForMember(entity => entity.FGiaTriUsd, model => model.MapFrom(item => item.FTienGoiThauUsd))
                .ForMember(entity => entity.FGiaTriVnd, model => model.MapFrom(item => item.FTienGoiThauVnd))
                .ForMember(entity => entity.FGiaTriEur, model => model.MapFrom(item => item.FTienGoiThauEur))
                .ForMember(entity => entity.FGiaTriNgoaiTeKhac, model => model.MapFrom(item => item.FTienGoiThauNgoaiTeKhac));

            CreateMap<NhDaGoiThauChiPhiModel, NhDaGoiThauChiPhi>()
                .ForMember(entity => entity.FTienGoiThauUsd, model => model.MapFrom(item => item.FGiaTriUsd))
                .ForMember(entity => entity.FTienGoiThauVnd, model => model.MapFrom(item => item.FGiaTriVnd))
                .ForMember(entity => entity.FTienGoiThauEur, model => model.MapFrom(item => item.FGiaTriEur))
                .ForMember(entity => entity.FTienGoiThauNgoaiTeKhac, model => model.MapFrom(item => item.FGiaTriNgoaiTeKhac));

            CreateMap<NhDaGoiThauChiPhiQuery, NhDaGoiThauChiPhiModel>()
                .ForMember(entity => entity.STenChiPhi, model => model.MapFrom(item => item.STenChiPhiQDDT != null ? item.STenChiPhiQDDT : item.STenChiPhiDT));
            CreateMap<NhDaCacQuyetDinhChiPhiGoiThauQuery, NhHdnkCacQuyetDinhChiPhiModel>()
                .ForMember(entity => entity.FGiaTriUSDGoiThau, model => model.MapFrom(item => item.FTienGoiThauUsd))
                .ForMember(entity => entity.FGiaTriVNDGoiThau, model => model.MapFrom(item => item.FTienGoiThauVnd))
                .ForMember(entity => entity.FGiaTriEURGoiThau, model => model.MapFrom(item => item.FTienGoiThauEur))
                .ForMember(entity => entity.FGiaTriNgoaiTeKhacGoiThau, model => model.MapFrom(item => item.FTienGoiThauNgoaiTeKhac))
                .ForMember(entity => entity.FGiaTriUsd, model => model.MapFrom(item => item.FGiaTriUSD))
                .ForMember(entity => entity.FGiaTriVnd, model => model.MapFrom(item => item.FGiaTriVND))
                .ForMember(entity => entity.FGiaTriEur, model => model.MapFrom(item => item.FGiaTriEUR))
                .ForMember(entity => entity.FGiaTriNgoaiTeKhac, model => model.MapFrom(item => item.FGiaTriNgoaiTeKhac));

            CreateMap<NhDaGoiThauChiPhiQuery, NhDaHopDongTrongNuocChiPhiGoiThauModel>()
                .ForMember(entity => entity.IIdGoiThauChiPhiId, model => model.MapFrom(item => item.Id))
                .ForMember(entity => entity.FTienGoiThauUSD, model => model.MapFrom(item => item.FTienGoiThauUsd))
                .ForMember(entity => entity.FTienGoiThauVND, model => model.MapFrom(item => item.FTienGoiThauVnd))
                .ForMember(entity => entity.FTienGoiThauEUR, model => model.MapFrom(item => item.FTienGoiThauEur))
                .ForMember(entity => entity.FTienGoiThauNgoaiTeKhac, model => model.MapFrom(item => item.FTienGoiThauNgoaiTeKhac))
                .ForMember(entity => entity.STenChiPhi, model => model.MapFrom(item => item.STenChiPhiDT != null ? item.STenChiPhiDT : item.STenChiPhiQDDT));
        }
    }
}
