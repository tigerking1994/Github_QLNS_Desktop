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
    public class NhDaGoiThauNguonVonMapper : Profile
    {
        public NhDaGoiThauNguonVonMapper()
        {
            CreateMap<NhDaGoiThauNguonVon, NhDaGoiThauNguonVonModel>()
                .ForMember(entity => entity.FGiaTriUsd, model => model.MapFrom(item => item.FTienGoiThauUsd))
                .ForMember(entity => entity.FGiaTriVnd, model => model.MapFrom(item => item.FTienGoiThauVnd))
                .ForMember(entity => entity.FGiaTriEur, model => model.MapFrom(item => item.FTienGoiThauEur))
                .ForMember(entity => entity.FGiaTriNgoaiTeKhac, model => model.MapFrom(item => item.FTienGoiThauNgoaiTeKhac));

            CreateMap<NhDaGoiThauNguonVonModel, NhDaGoiThauNguonVon>()
                .ForMember(entity => entity.FTienGoiThauUsd, model => model.MapFrom(item => item.FGiaTriUsd))
                .ForMember(entity => entity.FTienGoiThauVnd, model => model.MapFrom(item => item.FGiaTriVnd))
                .ForMember(entity => entity.FTienGoiThauEur, model => model.MapFrom(item => item.FGiaTriEur))
                .ForMember(entity => entity.FTienGoiThauNgoaiTeKhac, model => model.MapFrom(item => item.FGiaTriNgoaiTeKhac));

            CreateMap<NhDaGoiThauThongTinNguonVonQuery, NhDaGoiThauNguonVonModel>();
            CreateMap<NhDaCacQuyetDinhNguonVonGoiThauQuery, NhHdnkCacQuyetDinhNguonVonModel>()
                .ForMember(entity => entity.FGiaTriUSDGoiThau, model => model.MapFrom(item => item.FTienGoiThauUsd))
                .ForMember(entity => entity.FGiaTriVNDGoiThau, model => model.MapFrom(item => item.FTienGoiThauVnd))
                .ForMember(entity => entity.FGiaTriEURGoiThau, model => model.MapFrom(item => item.FTienGoiThauEur))
                .ForMember(entity => entity.FGiaTriNgoaiTeKhacGoiThau, model => model.MapFrom(item => item.FTienGoiThauNgoaiTeKhac))
                .ForMember(entity => entity.FGiaTriUsd, model => model.MapFrom(item => item.FGiaTriUSD))
                .ForMember(entity => entity.FGiaTriVnd, model => model.MapFrom(item => item.FGiaTriVND))
                .ForMember(entity => entity.FGiaTriEur, model => model.MapFrom(item => item.FGiaTriEUR))
                .ForMember(entity => entity.FGiaTriNgoaiTeKhac, model => model.MapFrom(item => item.FGiaTriNgoaiTeKhac))
                .ForMember(entity => entity.STenNguonVon, model => model.MapFrom(item => item.STenNguonVon));
        }
    }
}
