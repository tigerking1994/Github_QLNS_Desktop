using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using VTS.QLNS.CTC.Core.Domain;

namespace VTS.QLNS.CTC.App.Model.Mapper
{
    public class TlMapColumnConfigMapper : Profile
    {
        public TlMapColumnConfigMapper()
        {
            CreateMap<TlMapColumnConfigModel, TlMapColumnConfig>();
            CreateMap<TlMapColumnConfig, TlMapColumnConfigModel>();
        }
    }
}
