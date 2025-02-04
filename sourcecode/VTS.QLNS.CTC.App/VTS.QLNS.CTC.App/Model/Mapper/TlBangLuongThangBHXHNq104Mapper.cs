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
    public class TlBangLuongThangBHXHNq104Mapper : Profile
    {
        public TlBangLuongThangBHXHNq104Mapper()
        {
            CreateMap<TlBangLuongThangBHXHNq104, TlBangLuongThangBHXHNq104Model>().ReverseMap();
            CreateMap<TlBangLuongThangBHXHNq104Query, TlBangLuongThangBHXHNq104Model>().ReverseMap();
            CreateMap<TlBangLuongThangBHXHNq104Query, ExportChiTietBangLuongBHXHNq104Model>().ReverseMap();
        }
    }
}
