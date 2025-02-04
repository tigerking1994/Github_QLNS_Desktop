using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using VTS.QLNS.CTC.App.Model.Control;
using VTS.QLNS.CTC.Core.Domain;

namespace VTS.QLNS.CTC.App.Model.Mapper
{
    public class TnDanhMucLoaiHinhMapper : Profile
    {
        public TnDanhMucLoaiHinhMapper()
        {
            CreateMap<TnDanhMucLoaiHinh, CheckBoxItem>()
               .ForMember(entity => entity.DisplayItem, model => model.MapFrom(z => string.Format("{0} - {1}", z.Lns, z.MoTa)))
               .ForMember(entity => entity.ValueItem, model => model.MapFrom(z => z.Lns));
            CreateMap<TnDanhMucLoaiHinh, TnDanhMucLoaiHinhModel>()
                .ForMember(entity => entity.IsHangCha, model => model.MapFrom(z => z.BLaHangCha));
            CreateMap<TnDanhMucLoaiHinhModel, TnDanhMucLoaiHinh>()
                .ForMember(entity => entity.BLaHangCha, model => model.MapFrom(z => z.IsHangCha));
            CreateMap<TnDanhMucLoaiHinh, RevenueExpenditureCategoryModel>();
            CreateMap<RevenueExpenditureCategoryModel, TnDanhMucLoaiHinh>();
        }
    }
}
