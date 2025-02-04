using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VTS.QLNS.CTC.App.Model.Mapper
{
    public class TlDmTangGiamNq104Mapper : Profile
    {
        public TlDmTangGiamNq104Mapper()
        {
            CreateMap<Core.Domain.TlDmTangGiamNq104, TlDmTangGiamNq104Model>();
            CreateMap<TlDmTangGiamNq104Model, Core.Domain.TlDmTangGiamNq104>();
        }
    }
}
