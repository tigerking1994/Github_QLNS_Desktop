using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using VTS.QLNS.CTC.App.Model.Control;
using VTS.QLNS.CTC.Core.Domain;

namespace VTS.QLNS.CTC.App.Model.Mapper
{
    internal class NhDmTiGiaMapper : Profile
    {
        public NhDmTiGiaMapper()
        {
            CreateMap<NhDmTiGia, ComboboxItem>()
                .ForMember(entity => entity.DisplayItem, model => model.MapFrom(item => item.STenTiGia))
                .ForMember(entity => entity.HiddenValue, model => model.MapFrom(item => item.Id.ToString()))
                .ForMember(entity => entity.ValueItem, model => model.MapFrom(item => item.SMaTiGia));
            CreateMap<NhDmTiGia, NhDmTiGiaModel>();
            CreateMap<NhDmTiGiaModel, NhDmTiGia>();
            CreateMap<NhDmTiGiaChiTietModel, NhDmTiGiaChiTiet>();
            CreateMap<NhDmTiGiaChiTiet, NhDmTiGiaChiTietModel>();
        }
    }
}
