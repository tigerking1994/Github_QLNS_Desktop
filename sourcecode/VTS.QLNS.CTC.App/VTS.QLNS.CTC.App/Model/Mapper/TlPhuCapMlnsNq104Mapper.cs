using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VTS.QLNS.CTC.Core.Domain;

namespace VTS.QLNS.CTC.App.Model.Mapper
{
    public class TlPhuCapMlnsNq104Mapper : Profile
    {
        public TlPhuCapMlnsNq104Mapper()
        {
            CreateMap<TlPhuCapMlnsNq104Model, TlPhuCapMlnNq104>();
            CreateMap<TlPhuCapMlnNq104, TlPhuCapMlnsNq104Model>();
        }
    }
}
