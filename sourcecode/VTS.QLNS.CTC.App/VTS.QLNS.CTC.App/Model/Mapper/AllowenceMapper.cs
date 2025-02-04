using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;

namespace VTS.QLNS.CTC.App.Model.Mapper
{
    public class AllowenceMapper : Profile
    {
        public AllowenceMapper()
        {
            CreateMap<AllowenceModel, TlDmPhuCap>();
            CreateMap<TlDmPhuCap, AllowenceModel>();
            CreateMap<AllowenceModel, TlPhuCapQuery>();
            CreateMap<TlPhuCapQuery, AllowenceModel>();
        }
    }
}
