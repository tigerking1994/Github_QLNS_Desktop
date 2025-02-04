using AutoMapper;
using VTS.QLNS.CTC.Core.Domain;

namespace VTS.QLNS.CTC.App.Model.Mapper
{
    class TlDsCapNhapBangLuongMapper : Profile
    {
        public TlDsCapNhapBangLuongMapper()
        {
            CreateMap<TlDSCapNhapBangLuongModel, TlDsCapNhapBangLuong>();
            CreateMap<Core.Domain.TlDsCapNhapBangLuong, TlDSCapNhapBangLuongModel>()
                .ForMember(entity => entity.TuNgayString, model => model.MapFrom(item => item.TuNgay.HasValue ? item.TuNgay.Value.ToString("dd/MM/yyyy") : string.Empty))
                .ForMember(entity => entity.DenNgayString, model => model.MapFrom(item => item.DenNgay.HasValue ? item.DenNgay.Value.ToString("dd/MM/yyyy") : string.Empty))
                .ForMember(entity => entity.NgayTaoBLString, model => model.MapFrom(item => item.NgayTaoBL.HasValue ? item.NgayTaoBL.Value.ToString("dd/MM/yyyy") : string.Empty))
                .ForMember(entity => entity.TenDonVi, model => model.MapFrom(item => item.TlDmDonVi == null ? string.Empty : item.TlDmDonVi.TenDonVi))
                .ForMember(entity => entity.MaDonVi, model => model.MapFrom(item => item.TlDmDonVi == null ? string.Empty : item.TlDmDonVi.MaDonVi));
        }
    }
}
