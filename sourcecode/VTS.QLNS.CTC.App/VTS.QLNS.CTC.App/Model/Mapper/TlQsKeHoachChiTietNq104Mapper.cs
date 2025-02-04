using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VTS.QLNS.CTC.Core.Domain;

namespace VTS.QLNS.CTC.App.Model.Mapper
{
    public class TlQsKeHoachChiTietNq104Mapper : Profile
    {
        public TlQsKeHoachChiTietNq104Mapper()
        {
            CreateMap<TlQsChungTuChiTietNq104Model, TlQsChungTuChiTietNq104>().ReverseMap();
        }
    }
}
