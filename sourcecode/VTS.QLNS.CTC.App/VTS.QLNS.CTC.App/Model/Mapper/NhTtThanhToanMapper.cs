using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;

namespace VTS.QLNS.CTC.App.Model.Mapper
{
    public class NhTtThanhToanMapper : Profile
    {
        public NhTtThanhToanMapper()
        {
            CreateMap<NhTtThanhToan, NhTtThanhToanModel>().ReverseMap();
            CreateMap<NhTtThanhToanQuery, NhTtThanhToanModel>().ReverseMap();
        }
    }
}
