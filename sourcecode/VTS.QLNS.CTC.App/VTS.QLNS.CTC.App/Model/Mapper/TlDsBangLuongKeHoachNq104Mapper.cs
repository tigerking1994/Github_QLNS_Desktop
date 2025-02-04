using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VTS.QLNS.CTC.Core.Domain.Query.Shared;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Utility;

namespace VTS.QLNS.CTC.App.Model.Mapper
{
    public class TlDsBangLuongKeHoachNq104Mapper : Profile
    {
        public TlDsBangLuongKeHoachNq104Mapper()
        {
            CreateMap<TlDsBangLuongKeHoachNq104Model, TlDsBangLuongKeHoachNq104>().ReverseMap();
            CreateMap<TlBangLuongKeHoachNq104ImportModel, TlBangLuongKeHoachNq104ExportQuery>()
                .ForMember(item => item.ILevel, model => model.MapFrom(x => (int)NumberUtils.ConvertTextToDouble(x.ILevel)))
                .ForMember(item => item.QSBQ, model => model.MapFrom(x => NumberUtils.ConvertTextToDouble(x.QSBQ)))
                .ForMember(item => item.LHT_TT, model => model.MapFrom(x => NumberUtils.ConvertTextToDouble(x.LHT_TT)))
                .ForMember(item => item.PCCV_TT, model => model.MapFrom(x => NumberUtils.ConvertTextToDouble(x.PCCV_TT)))
                .ForMember(item => item.PCTN_TT, model => model.MapFrom(x => NumberUtils.ConvertTextToDouble(x.PCTN_TT)))
                .ForMember(item => item.PCTNVK_TT, model => model.MapFrom(x => NumberUtils.ConvertTextToDouble(x.PCTNVK_TT)))
                .ForMember(item => item.HSBL_TT, model => model.MapFrom(x => NumberUtils.ConvertTextToDouble(x.HSBL_TT))).ReverseMap()
                ;
        }
    }
}
