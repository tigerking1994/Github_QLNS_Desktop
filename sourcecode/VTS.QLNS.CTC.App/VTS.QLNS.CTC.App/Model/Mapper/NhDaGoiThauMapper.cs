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
    class NhDaGoiThauMapper : Profile
    {
        public NhDaGoiThauMapper()
        {
            CreateMap<NhDaGoiThau, NhDaGoiThauModel>()
                .ForMember(entity => entity.IIdDuAnId, model => model.MapFrom(item => item.IIdDuAnId));
            CreateMap<NhDaGoiThauModel, NhDaGoiThau>()
                .ForMember(entity => entity.IIdDuAnId, model => model.MapFrom(item => item.IIdDuAnId));
            CreateMap<NhDaGoiThauQuery, NhDaGoiThauModel>()
                .ForMember(entity => entity.DBatDauChonNhaThauString, model => model.MapFrom(item => item.DBatDauChonNhaThau.HasValue ? item.DBatDauChonNhaThau.Value.ToString("dd/MM/yyyy") : string.Empty))
                .ForMember(entity => entity.DKetThucChonNhaThauString, model => model.MapFrom(item => item.DKetThucChonNhaThau.HasValue ? item.DKetThucChonNhaThau.Value.ToString("dd/MM/yyyy") : string.Empty))
                .ForMember(entity => entity.DNgayQuyetDinhString, model => model.MapFrom(item => item.DNgayQuyetDinh.HasValue ? item.DNgayQuyetDinh.Value.ToString("dd/MM/yyyy") : string.Empty));

            CreateMap<NhDaGoiThauTrongNuocQuery, NhDaGoiThauModel>()
                .ForMember(entity => entity.DNgayQuyetDinhString, model => model.MapFrom(item => item.DNgayQuyetDinh.HasValue ? item.DNgayQuyetDinh.Value.ToString("dd/MM/yyyy") : string.Empty))
                .ForMember(entity => entity.FTongPheDuyetUSD, model => model.MapFrom(item => item.FQDDTTongPheDuyetUSD != null ? item.FQDDTTongPheDuyetUSD : item.FDTTongPheDuyetUSD))
                .ForMember(entity => entity.FTongPheDuyetVND, model => model.MapFrom(item => item.FQDDTTongPheDuyetVND != null ? item.FQDDTTongPheDuyetVND : item.FDTTongPheDuyetVND))
                .ForMember(entity => entity.FTongPheDuyetEUR, model => model.MapFrom(item => item.FQDDTTongPheDuyetEUR != null ? item.FQDDTTongPheDuyetEUR : item.FDTTongPheDuyetEUR))
                .ForMember(entity => entity.FTongPheDuyetNgoaiTeKhac, model => model.MapFrom(item => item.FQDDTTongPheDuyetNgoaiTeKhac != null ? item.FQDDTTongPheDuyetNgoaiTeKhac : item.FDTTongPheDuyetNgoaiTeKhac));
        }
    }
}
