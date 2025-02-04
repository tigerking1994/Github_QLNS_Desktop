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
    public class TlBangLuongThangBHXHMapper : Profile
    {
        public TlBangLuongThangBHXHMapper()
        {
            CreateMap<TlBangLuongThangBHXH, TlBangLuongThangBHXHModel>().ReverseMap();
            CreateMap<TlBangLuongThangBHXHQuery, TlBangLuongThangBHXHModel>().ReverseMap();
            CreateMap<TlBangLuongThangBHXHQuery, ExportChiTietBangLuongBHXHModel>().ReverseMap();
            CreateMap<TlBangLuongThangBHXH, TlBangLuongThangBHXHQuery>().ReverseMap();
        }
    }
}
