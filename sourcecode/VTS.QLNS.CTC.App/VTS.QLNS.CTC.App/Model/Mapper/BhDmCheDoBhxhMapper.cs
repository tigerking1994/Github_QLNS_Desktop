using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using VTS.QLNS.CTC.App.Model.Control;
using VTS.QLNS.CTC.Core.Domain;

namespace VTS.QLNS.CTC.App.Model.Mapper
{
    public class BhDmCheDoBhxhMapper : Profile
    {
        public BhDmCheDoBhxhMapper()
        {
            CreateMap<BhDmCheDoBhxh, BhDmCheDoBhxhModel>()
                .ReverseMap();
            CreateMap<BhDmCheDoBhxhModel, BhDmCheDoBhxh>()
                .ReverseMap();
        }
    }
}
