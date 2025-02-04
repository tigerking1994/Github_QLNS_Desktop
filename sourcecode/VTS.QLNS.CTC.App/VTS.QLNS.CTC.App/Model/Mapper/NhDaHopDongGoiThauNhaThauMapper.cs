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
    public class NhDaHopDongGoiThauNhaThauMapper : Profile
    {
        public NhDaHopDongGoiThauNhaThauMapper()
        {
            CreateMap<NhDaHopDongGoiThauNhaThauModel, NhDaHopDongGoiThauNhaThau>();
            CreateMap<NhDaHopDongGoiThauNhaThau, NhDaHopDongGoiThauNhaThauModel>();

            CreateMap<NhDaGoiThauTrongNuocQuery, NhDaHopDongGoiThauNhaThauModel>()
                .ForMember(entity => entity.IIdGoiThauId, model => model.MapFrom(item => item.Id))
                .ForMember(entity => entity.FGiaTriGoiThauUsd, model => model.MapFrom(item => item.FGiaGoiThauUsd))
                .ForMember(entity => entity.FGiaTriGoiThauVnd, model => model.MapFrom(item => item.FGiaGoiThauVnd))
                .ForMember(entity => entity.FGiaTriGoiThauEur, model => model.MapFrom(item => item.FGiaGoiThauEur))
                .ForMember(entity => entity.FGiaTriGoiThauNgoaiTeKhac, model => model.MapFrom(item => item.FGiaGoiThauNgoaiTeKhac));

            CreateMap<NhDaHopDongGoiThauNhaThauQuery, NhDaHopDongGoiThauNhaThauModel>();
        }
    }
}
