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
    public class BhKhcCheDoBhXhMapper : Profile
    {
        public BhKhcCheDoBhXhMapper()
        {
            CreateMap<BhKhcCheDoBhXhModel, BhKhcCheDoBhXh>().ReverseMap();
            CreateMap<BhKhcCheDoBhXhModel, BhKhcCheDoBhXhQuery>().ReverseMap();
        }
    }
}
