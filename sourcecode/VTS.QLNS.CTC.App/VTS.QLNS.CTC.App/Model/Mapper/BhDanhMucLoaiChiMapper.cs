using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.App.Model.Control;

namespace VTS.QLNS.CTC.App.Model.Mapper
{
    public class BhDanhMucLoaiChiMapper : Profile
    {
        public BhDanhMucLoaiChiMapper()
        {
            CreateMap<BhDanhMucLoaiChiModel, BhDanhMucLoaiChi>().ReverseMap();
            CreateMap<BhDanhMucLoaiChiModel, BhDanhMucLoaiChiQuery>().ReverseMap();
            CreateMap<BhDanhMucLoaiChi, BhDanhMucLoaiChiModel>().ReverseMap();
            CreateMap<CheckBoxItem, BhDanhMucLoaiChi>()
               .ForMember(entity => entity.STenDanhMucLoaiChi, model => model.MapFrom(item => item.DisplayItem))
               .ForMember(entity => entity.SMaLoaiChi, model => model.MapFrom(item => item.ValueItem))
                .ForMember(entity => entity.SLNS, model => model.MapFrom(item => item.NameItem));
            CreateMap<BhDanhMucLoaiChi, CheckBoxItem>()
              .ForMember(entity => entity.DisplayItem, model => model.MapFrom(item => $"{item.SMaLoaiChi.PadLeft(2, '0')} - {item.STenDanhMucLoaiChi}"))
              .ForMember(entity => entity.NameItem, model => model.MapFrom(item => item.SLNS))
              .ForMember(entity => entity.ValueItem, model => model.MapFrom(item => item.SMaLoaiChi));
        }
    }
}
