using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VTS.QLNS.CTC.App.Model.Mapper
{
    public class TlDmThueThuNhapCaNhanNq104Mapper : Profile
    {
        public TlDmThueThuNhapCaNhanNq104Mapper()
        {
            CreateMap<Core.Domain.TlDmThueThuNhapCaNhanNq104, TlDmThueThuNhapCaNhanNq104Model>()
            .ForMember(n => n.IIsThueThang, m => m.MapFrom(n => n.BIsThueThang ? "1" : "0"));
            CreateMap<TlDmThueThuNhapCaNhanNq104Model, Core.Domain.TlDmThueThuNhapCaNhanNq104>()
            .ForMember(n => n.BIsThueThang, m => m.MapFrom(n => n.IIsThueThang == "1"));
        }
    }
}
