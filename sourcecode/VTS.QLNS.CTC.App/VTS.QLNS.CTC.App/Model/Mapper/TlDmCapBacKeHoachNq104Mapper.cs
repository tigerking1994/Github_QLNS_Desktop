using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VTS.QLNS.CTC.Core.Domain;

namespace VTS.QLNS.CTC.App.Model.Mapper
{
    public class TlDmCapBacKeHoachNq104Mapper : Profile
    {
        public TlDmCapBacKeHoachNq104Mapper()
        {
            CreateMap<TlDmCapBacKeHoachNq104Model, TlDmCapBacKeHoachNq104>().ReverseMap();
        }
    }
}
