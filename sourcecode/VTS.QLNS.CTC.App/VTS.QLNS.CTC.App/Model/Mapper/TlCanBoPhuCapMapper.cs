using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;

namespace VTS.QLNS.CTC.App.Model.Mapper
{
    public class TlCanBoPhuCapMapper : Profile
    {
        public TlCanBoPhuCapMapper()
        {
            CreateMap<TlCanBoPhuCap, TlCanBoPhuCapModel>();
            CreateMap<TlCanBoPhuCapModel, TlCanBoPhuCap>();
            CreateMap<TLCanBoPhuCapQuery, TlCanBoPhuCap>().ReverseMap();
            CreateMap<TLCanBoPhuCapQuery, TlCanBoPhuCapModel>().ReverseMap();
        }
    }
}
