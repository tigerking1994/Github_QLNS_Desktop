using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace VTS.QLNS.CTC.App.Model.Mapper
{
    public class TlDmTangGiamMapper : Profile
    {
        public TlDmTangGiamMapper()
        {
            CreateMap<Core.Domain.TlDmTangGiam, TlDmTangGiamModel>();
            CreateMap<TlDmTangGiamModel, Core.Domain.TlDmTangGiam>();
        }
    }
}
