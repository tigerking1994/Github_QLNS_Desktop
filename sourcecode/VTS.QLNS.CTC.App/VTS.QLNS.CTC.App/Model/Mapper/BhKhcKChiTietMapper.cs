using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Domain;
using AutoMapper;

namespace VTS.QLNS.CTC.App.Model.Mapper
{
    public class BhKhcKChiTietMapper : Profile
    {
        public BhKhcKChiTietMapper()
        {
            CreateMap<BhKhcKChiTietModel, BhKhcKChiTiet>().ReverseMap();
            CreateMap<BhKhcKChiTietModel, BhKhcKChiTietQuery>().ReverseMap();
        }
    }
}
