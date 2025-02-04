using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace VTS.QLNS.CTC.App.Model.Mapper
{
    class TlDsCapNhapBangLuongNq104Mapper : Profile
    {
        public TlDsCapNhapBangLuongNq104Mapper()
        {
            CreateMap<TlDSCapNhapBangLuongNq104Model, Core.Domain.TlDsCapNhapBangLuongNq104>();
            CreateMap<Core.Domain.TlDsCapNhapBangLuongNq104, TlDSCapNhapBangLuongNq104Model>()
                .ForMember(entity => entity.TuNgayString, model => model.MapFrom(item => item.TuNgay.HasValue ? item.TuNgay.Value.ToString("dd/MM/yyyy") : string.Empty))
                .ForMember(entity => entity.DenNgayString, model => model.MapFrom(item => item.DenNgay.HasValue ? item.DenNgay.Value.ToString("dd/MM/yyyy") : string.Empty))
                .ForMember(entity => entity.NgayTaoBLString, model => model.MapFrom(item => item.NgayTaoBL.HasValue ? item.NgayTaoBL.Value.ToString("dd/MM/yyyy") : string.Empty))
                .ForMember(entity => entity.TenDonVi, model => model.MapFrom(item => item.TlDmDonViNq104 == null ? string.Empty : item.TlDmDonViNq104.TenDonVi))
                .ForMember(entity => entity.MaDonVi, model => model.MapFrom(item => item.TlDmDonViNq104 == null ? string.Empty : item.TlDmDonViNq104.MaDonVi));
        }
    }
}
