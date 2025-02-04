using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using VTS.QLNS.CTC.Core.Domain;

namespace VTS.QLNS.CTC.App.Model.Mapper
{
    public class TlCachTinhLuongNq104Mapper : Profile
    {
        public TlCachTinhLuongNq104Mapper()
        {
            CreateMap<TlCachTinhLuongNq104Model, TlDmCachTinhLuongChuanNq104>();
            CreateMap<TlDmCachTinhLuongChuanNq104, TlCachTinhLuongNq104Model>();
            CreateMap<TlCachTinhLuongNq104Model, TlDmCachTinhLuongTruyLinhNq104>();
            CreateMap<TlDmCachTinhLuongTruyLinhNq104, TlCachTinhLuongNq104Model>();
            CreateMap<TlCachTinhLuongNq104Model, TlDmCachTinhLuongBaoHiemNq104>();
            CreateMap<TlDmCachTinhLuongBaoHiemNq104, TlCachTinhLuongNq104Model>();
        }
    }
}
