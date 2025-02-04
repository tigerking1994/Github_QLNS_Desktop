using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Controls;
using VTS.QLNS.CTC.App.Model.Control;
using VTS.QLNS.CTC.Core.Domain;

namespace VTS.QLNS.CTC.App.Model.Mapper
{
    public class ThemCachTinhLuongMapper : Profile
    {
        public ThemCachTinhLuongMapper()
        {
            CreateMap<TlDmThemCachTinhLuongModel, TlDmThemCachTinhLuong>();
            CreateMap<TlDmThemCachTinhLuong, TlDmThemCachTinhLuongModel>();
            CreateMap<TlDmThemCachTinhLuong, ComboboxItem>()
                .ForMember(x => x.DisplayItem, y => y.MapFrom(z => z.TenThemCachTl))
                .ForMember(x => x.ValueItem, y => y.MapFrom(z => z.Id))
                .ForMember(X => X.HiddenValue, y => y.MapFrom(z => z.MaThemCachTl));
        }
    }
}
