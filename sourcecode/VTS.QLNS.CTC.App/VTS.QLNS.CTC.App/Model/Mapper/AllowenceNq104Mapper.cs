using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;

namespace VTS.QLNS.CTC.App.Model.Mapper
{
    public class AllowenceNq104Mapper : Profile
    {
        public AllowenceNq104Mapper()
        {
            CreateMap<AllowenceNq104Model, TlDmPhuCapNq104>();
            CreateMap<TlDmPhuCapNq104, AllowenceNq104Model>();
            CreateMap<AllowenceNq104Model, TlPhuCapNq104Query>();
            CreateMap<TlPhuCapNq104Query, AllowenceNq104Model>();
        }
    }
}
