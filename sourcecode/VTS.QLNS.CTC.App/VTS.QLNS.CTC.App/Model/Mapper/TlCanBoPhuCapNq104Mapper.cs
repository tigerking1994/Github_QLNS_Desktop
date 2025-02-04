using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;

namespace VTS.QLNS.CTC.App.Model.Mapper
{
    public class TlCanBoPhuCapNq104Mapper : Profile
    {
        public TlCanBoPhuCapNq104Mapper()
        {
            CreateMap<TlCanBoPhuCapNq104, TlCanBoPhuCapNq104Model>();
            CreateMap<TlCanBoPhuCapNq104Model, TlCanBoPhuCapNq104>();
            CreateMap<TLCanBoPhuCapNq104Query, TlCanBoPhuCapNq104>();
        }
    }
}
