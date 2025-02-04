using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query.Shared;
using VTS.QLNS.CTC.Utility;

namespace VTS.QLNS.CTC.App.Model.Mapper
{
    public class TlDsBangLuongKeHoachMapper : Profile
    {
        public TlDsBangLuongKeHoachMapper()
        {
            CreateMap<TlDsBangLuongKeHoachModel, TlDsBangLuongKeHoach>();
            CreateMap<TlDsBangLuongKeHoach, TlDsBangLuongKeHoachModel>();
            CreateMap<TlBangLuongKeHoachImportModel, TlBangLuongKeHoachExportQuery>()
                .ForMember(item => item.ILevel, model => model.MapFrom(x => (int)NumberUtils.ConvertTextToDouble(x.ILevel)))
                .ForMember(item => item.QSBQ, model => model.MapFrom(x => NumberUtils.ConvertTextToDouble(x.QSBQ)))
                .ForMember(item => item.LHT_TT, model => model.MapFrom(x => NumberUtils.ConvertTextToDouble(x.LHT_TT)))
                .ForMember(item => item.PCCV_TT, model => model.MapFrom(x => NumberUtils.ConvertTextToDouble(x.PCCV_TT)))
                .ForMember(item => item.PCTN_TT, model => model.MapFrom(x => NumberUtils.ConvertTextToDouble(x.PCTN_TT)))
                .ForMember(item => item.PCTNVK_TT, model => model.MapFrom(x => NumberUtils.ConvertTextToDouble(x.PCTNVK_TT)))
                .ForMember(item => item.HSBL_TT, model => model.MapFrom(x => NumberUtils.ConvertTextToDouble(x.HSBL_TT)))
                ;
            CreateMap<TlBangLuongKeHoachExportQuery, TlBangLuongKeHoachImportModel>()
               .ForMember(entity => entity.ILevel, model => model.MapFrom(item => item.ILevel.ToString()))
               .ForMember(item => item.QSBQ, model => model.MapFrom(x => x.QSBQ.ToString()))
               .ForMember(item => item.LHT_TT, model => model.MapFrom(x => x.LHT_TT.ToString()))
               .ForMember(item => item.PCCV_TT, model => model.MapFrom(x => x.PCCV_TT.ToString()))
               .ForMember(item => item.PCTN_TT, model => model.MapFrom(x => x.PCTN_TT.ToString()))
               .ForMember(item => item.PCTNVK_TT, model => model.MapFrom(x => x.PCTNVK_TT.ToString()))
               .ForMember(item => item.HSBL_TT, model => model.MapFrom(x => x.HSBL_TT.ToString()))
               ;
        }
    }
}
