using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using VTS.QLNS.CTC.Core.Domain;

namespace VTS.QLNS.CTC.App.Model.Mapper
{
    public class DanhSachBangLuongNq104Mapper : Profile
    {
        public DanhSachBangLuongNq104Mapper()
        {
            CreateMap<TlDsCapNhapBangLuongNq104, DanhSachBangLuongNq104Model>()
                .ForMember(entity => entity.TuNgayString, model => model.MapFrom(item => item.TuNgay.HasValue ? item.TuNgay.Value.ToString("dd/MM/yyyy") : string.Empty))
                .ForMember(entity => entity.DenNgayString, model => model.MapFrom(item => item.DenNgay.HasValue ? item.DenNgay.Value.ToString("dd/MM/yyyy") : string.Empty));
            CreateMap<DanhSachBangLuongNq104Model, TlDsCapNhapBangLuongNq104>();
        }
    }
}
