using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VTS.QLNS.CTC.Core.Domain;

namespace VTS.QLNS.CTC.App.Model.Mapper
{
    public class TlBangLuongKeHoachNq104Mapper : Profile
    {
        public TlBangLuongKeHoachNq104Mapper()
        {
            CreateMap<TlBangLuongKeHoachNq104Model, TlBangLuongKeHoachNq104>().ReverseMap();
        }
    }
}
