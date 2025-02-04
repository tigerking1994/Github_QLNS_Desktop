using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VTS.QLNS.CTC.Core.Domain;

namespace VTS.QLNS.CTC.App.Model.Mapper
{
    public class TlPhuCapMlnsMapper : Profile
    {
        public TlPhuCapMlnsMapper()
        {
            CreateMap<TlPhuCapMlnsModel, TlPhuCapMln>();
            CreateMap<TlPhuCapMln, TlPhuCapMlnsModel>();
        }
    }
}
