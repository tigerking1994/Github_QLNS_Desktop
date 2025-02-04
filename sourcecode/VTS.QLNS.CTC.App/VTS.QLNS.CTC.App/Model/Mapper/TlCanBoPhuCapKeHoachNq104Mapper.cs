using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VTS.QLNS.CTC.Core.Domain;

namespace VTS.QLNS.CTC.App.Model.Mapper
{
    public class TlCanBoPhuCapKeHoachNq104Mapper : Profile
    {
        public TlCanBoPhuCapKeHoachNq104Mapper()
        {
            CreateMap<TlCanBoPhuCapKeHoachNq104, TlCanBoPhuCapKeHoachNq104Model>().ReverseMap();
        }
    }
}
