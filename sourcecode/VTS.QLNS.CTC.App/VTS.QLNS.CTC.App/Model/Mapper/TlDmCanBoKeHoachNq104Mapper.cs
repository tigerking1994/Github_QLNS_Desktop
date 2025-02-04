using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VTS.QLNS.CTC.Core.Domain;

namespace VTS.QLNS.CTC.App.Model.Mapper
{
    public class TlDmCanBoKeHoachNq104Mapper : Profile
    {
        public TlDmCanBoKeHoachNq104Mapper()
        {
            CreateMap<TlDmCanBoKeHoachNq104, TlDmCanBoKeHoachNq104Model>().ReverseMap();
        }
    }
}
