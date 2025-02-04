using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using VTS.QLNS.CTC.Core.Domain;

namespace VTS.QLNS.CTC.App.Model.Mapper
{
    public class DanhSachBangLuongMapper : Profile
    {
        public DanhSachBangLuongMapper()
        {
            CreateMap<TlDsCapNhapBangLuong, DanhSachBangLuongModel>()
                .ForMember(entity => entity.TuNgayString, model => model.MapFrom(item => item.TuNgay.HasValue ? item.TuNgay.Value.ToString("dd/MM/yyyy") : string.Empty))
                .ForMember(entity => entity.DenNgayString, model => model.MapFrom(item => item.DenNgay.HasValue ? item.DenNgay.Value.ToString("dd/MM/yyyy") : string.Empty));
            CreateMap<DanhSachBangLuongModel, TlDsCapNhapBangLuong>();
        }
    }
}
