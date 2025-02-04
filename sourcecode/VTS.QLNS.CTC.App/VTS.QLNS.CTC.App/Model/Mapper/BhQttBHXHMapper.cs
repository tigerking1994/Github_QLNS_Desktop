using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Domain;

namespace VTS.QLNS.CTC.App.Model.Mapper
{
    public class BhQttBHXHMapper : Profile
    {
        public BhQttBHXHMapper()
        {
            CreateMap<BhQttBHXH, BhQttBHXHModel>().ReverseMap();
            CreateMap<BhQttBHXHQuery, BhQttBHXHModel>().ReverseMap();
        }
    }
}
