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
    public class NhDaGoiThauHangMucMapper : Profile
    {
        public NhDaGoiThauHangMucMapper()
        {
            CreateMap<NhDaGoiThauHangMuc, NhDaGoiThauHangMucModel>()
                .ForMember(entity => entity.FGiaTriUsd, model => model.MapFrom(item => item.FTienGoiThauUsd))
                .ForMember(entity => entity.FGiaTriVnd, model => model.MapFrom(item => item.FTienGoiThauVnd))
                .ForMember(entity => entity.FGiaTriEur, model => model.MapFrom(item => item.FTienGoiThauEur))
                .ForMember(entity => entity.FGiaTriNgoaiTeKhac, model => model.MapFrom(item => item.FTienGoiThauNgoaiTeKhac));

            CreateMap<NhDaGoiThauHangMucModel, NhDaGoiThauHangMuc>()
                .ForMember(entity => entity.FTienGoiThauUsd, model => model.MapFrom(item => item.FGiaTriUsd))
                .ForMember(entity => entity.FTienGoiThauVnd, model => model.MapFrom(item => item.FGiaTriVnd))
                .ForMember(entity => entity.FTienGoiThauEur, model => model.MapFrom(item => item.FGiaTriEur))
                .ForMember(entity => entity.FTienGoiThauNgoaiTeKhac, model => model.MapFrom(item => item.FGiaTriNgoaiTeKhac));

            CreateMap<NhDaGoiThauChiPhiQuery, NhDaGoiThauHangMucModel>()
                .ForMember(entity => entity.STenHangMuc, model => model.MapFrom(item => item.STenChiPhiQDDT != null ? item.STenChiPhiQDDT : item.STenChiPhiDT));
            CreateMap<NhDaGoiThauHangMucQuery, NhHdnkCacQuyetDinhChiPhiHangMucModel>()
                .ForMember(entity => entity.FGiaTriUSDGoiThau, model => model.MapFrom(item => item.FTienGoiThauUsd))
                .ForMember(entity => entity.FGiaTriVNDGoiThau, model => model.MapFrom(item => item.FTienGoiThauVnd))
                .ForMember(entity => entity.FGiaTriEURGoiThau, model => model.MapFrom(item => item.FTienGoiThauEur))
                .ForMember(entity => entity.FGiaTriNgoaiTeKhacGoiThau, model => model.MapFrom(item => item.FTienGoiThauNgoaiTeKhac))
                .ForMember(entity => entity.STenHangMuc, model => model.MapFrom(item => item.STenHangMucQDDT != null ? item.STenHangMucQDDT : item.STenHangMucDT));
            CreateMap<NhDaHangMucGoiThauQuery, NhHdnkCacQuyetDinhChiPhiHangMucModel>();
            CreateMap<NhDaGoiThauHangMucQuery, NhDaGoiThauHangMucModel>()
                .ForMember(model => model.STenHangMuc, entity => entity.MapFrom(item => item.STenHangMucDT != null ? item.STenHangMucDT : item.STenHangMucQDDT));
        }
    }
}
