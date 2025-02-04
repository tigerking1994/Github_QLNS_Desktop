using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using VTS.QLNS.CTC.Core.Domain.Query;

namespace VTS.QLNS.CTC.App.Model.Mapper
{
    public class AppVersionMapper : Profile
    {
        public AppVersionMapper()
        {
            CreateMap<Core.Domain.HtAppVersion, AppVersionModel>();
            CreateMap<AppVersionModel, Core.Domain.HtAppVersion>();
            CreateMap<AppVersionQuery, AppVersionModel>();
            CreateMap<AppVersionModel, AppVersionQuery>();
        }
    }
}
