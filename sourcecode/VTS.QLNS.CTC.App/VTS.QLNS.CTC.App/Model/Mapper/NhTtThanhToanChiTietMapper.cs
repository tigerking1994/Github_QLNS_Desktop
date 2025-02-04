using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using VTS.QLNS.CTC.Core.Domain;

namespace VTS.QLNS.CTC.App.Model.Mapper
{
    public class NhTtThanhToanChiTietMapper : Profile
    {
        public NhTtThanhToanChiTietMapper()
        {
            CreateMap<NhTtThanhToanChiTiet, NhTtThanhToanChiTietModel>().ReverseMap();
        }
    }
}
