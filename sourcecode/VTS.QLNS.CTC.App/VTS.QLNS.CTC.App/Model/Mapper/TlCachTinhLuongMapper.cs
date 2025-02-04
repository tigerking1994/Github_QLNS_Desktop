using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using VTS.QLNS.CTC.Core.Domain;

namespace VTS.QLNS.CTC.App.Model.Mapper
{
    public class TlCachTinhLuongMapper : Profile
    {
        public TlCachTinhLuongMapper()
        {
            CreateMap<TlCachTinhLuongModel, TlDmCachTinhLuongChuan>();
            CreateMap<TlDmCachTinhLuongChuan, TlCachTinhLuongModel>();
            CreateMap<TlCachTinhLuongModel, TlDmCachTinhLuongTruyLinh>();
            CreateMap<TlDmCachTinhLuongTruyLinh, TlCachTinhLuongModel>();
            CreateMap<TlCachTinhLuongModel, TlDmCachTinhLuongBaoHiem>();
            CreateMap<TlDmCachTinhLuongBaoHiem, TlCachTinhLuongModel>();
            CreateMap<TlDmCachTinhLuongTruyThu, TlCachTinhLuongModel>().ReverseMap();
        }
    }
}
