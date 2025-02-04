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
    public class KhtmBHYTChiTietMapper : Profile
    {
        public KhtmBHYTChiTietMapper()
        {
            CreateMap<BhKhtmBHYTChiTiet, BhKhtmBHYTChiTietModel>().ReverseMap();
            CreateMap<BhKhtmBHYTChiTietQuery, BhKhtmBHYTChiTietModel>().ReverseMap();
            CreateMap<BhKhtmBHYTChiTietQuery, BhKhtmBHYTChiTiet>().ReverseMap();
        }
    }
}
